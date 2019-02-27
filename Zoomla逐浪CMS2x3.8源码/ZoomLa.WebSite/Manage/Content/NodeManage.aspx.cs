using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ZoomLa.Web;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Components;
using System.Globalization;
using ZoomLa.Model;
using System.Xml;
using ZoomLa.SQLDAL;

public partial class Manage_I_Content_NodeManage : CustomerPageAction
{
    B_Node bll = new B_Node();
    B_Content conBll = new B_Content();
    B_Admin badmin = new B_Admin();
    XmlDocument doc = new XmlDocument();
    private enum NodeEnum { Root = 0, Node = 1, SPage = 2, OuterLink = 3 };
    public string Action { get { return (Request.QueryString["action"] ?? "").ToLower(); } }
    public string ViewMode { get { return Request.QueryString["view"] ?? ""; } }
    protected void Page_Load(object sender, EventArgs e)
    {
        B_ARoleAuth.CheckEx(ZLEnum.Auth.model, "NodeManage");
        if (function.isAjax() && Request.Form["want"] != null)
        {
            #region AJAX
            int nodeID = DataConvert.CLng(Request.Form["nid"]);
            if (nodeID == 0) { Response.Clear(); Response.End(); }
            string json = "";
            DataTable dt = bll.SelForShowAll(nodeID);
            if (dt != null && dt.Rows.Count > 0)
            {
                string[] cols = "Meta_Keywords,Meta_Description,Description".Split(',');
                foreach (string col in cols)
                {
                    dt.Columns.Remove(col);
                }
                dt.Columns.Add(new DataColumn("icon", typeof(string)));
                dt.Columns.Add(new DataColumn("type2", typeof(string)));
                dt.Columns.Add(new DataColumn("oper", typeof(string)));
                //dt.Columns.Add(new DataColumn("showhidden",typeof(string)));
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    dt.Rows[i]["icon"] = GetIcon(dr["NodeName"].ToString(), Convert.ToInt32(dr["NodeID"]),Convert.ToInt32(dr["Depth"]),
                        Convert.ToInt32(dr["NodeType"]), dr["nodeDir"].ToString(), Convert.ToInt32(dr["ChildCount"]),dr["ContentModel"].ToString());
                    dt.Rows[i]["type2"] = GetNodeType(dr["NodeType"].ToString());
                    dt.Rows[i]["oper"] = GetOper(dr);
                    dt.Rows[i]["Purview"] = "";
                }
                
                json = JsonHelper.JsonSerialDataTable(dt);
            }
            Response.Clear(); Response.Write(json); Response.End();
            #endregion
        }
        if (!IsPostBack)
        {
            DataTable nodetable = new DataTable();
            if (Action=="del")
            {
                EmptyNode();
                Response.Redirect("NodeManage.aspx"); return;
            }
            else if (Action == "showall")
            {
                nodetable = bll.SelForShowAll(0,true);
            }
            else
            {
                nodetable = bll.SelForShowAll(0);
            }
            DataRow dr = nodetable.NewRow();//根节点
            dr["NodeID"] = 0;
            dr["NodeType"] = (int)NodeEnum.Root;
            dr["NodeName"] = Call.SiteName;
            dr["NodeDir"] = "root";
            dr["Depth"] = 0;
            dr["ItemCount"] = nodetable.Compute("SUM(ItemCount)","ParentID=0");
            dr["ChildCount"] = nodetable.Rows.Count;
            dr["SafeGuard"] = 0;
            dr["NodeBySite"] = 0;
            nodetable.Rows.InsertAt(dr, 0);
            //--------------
            nodetable.DefaultView.RowFilter = "NodeBySite IS NULL OR NodeBySite=0";
            nodetable = nodetable.DefaultView.ToTable();
            RPT.DataSource = nodetable;
            RPT.DataBind();
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>"+Resources.L.工作台 + "</a></li><li><a href='" + customPath2 + "Config/SiteInfo.aspx'>" + Resources.L.系统设置 + "</a></li><li class='active dropdown'><a href='NodeManage.aspx'>" + Resources.L.节点管理 + "</a>  <a href='#' class='dropdown-toggle' data-toggle='dropdown'>" + Resources.L.便捷操作 + " <span class='caret'></span></a>   <ul class='dropdown-menu' role='menu'><li><a href='NodeManage.aspx?action=showall'>" + Resources.L.展开所有节点 + "</a></li><li><a href=\'NodeManage.aspx\' >" + Resources.L.收缩所有节点 + "</a></li> <li><a href=\'javascript:void(0)\' onclick=\'emptynode()\'>" + Resources.L.删除所有节点 + "</a></li><li><a href=\"" + customPath2 + "Config/EmptyData.aspx\">[" + Resources.L.初始化全站数据 + "]</a></li></ul></li>" + Call.GetHelp(15));
        }
    }
    public string ShowIcon() 
    {
        return GetIcon(Eval("NodeName",""),Convert.ToInt32(Eval("NodeID")),Convert.ToInt32(Eval("Depth")),
            Convert.ToInt32(Eval("NodeType")), Eval("NodeDir").ToString(), Convert.ToInt32(Eval("ChildCount")),Eval("contentModel").ToString());
    }
    //显示图标与名称
    public string GetIcon(string NodeName, int NodeID, int Depth, int nodetype,string nodeDir,int childCount,string contentModel)
    {
        string outstr = "";
        nodeDir = "[" + nodeDir + "]";
        if (Depth > 0)//深度处理
        {
            for (int i = 1; i <= Depth - 1; i++)
            {
                outstr = outstr + "<a href='ContentManage.aspx?NodeID=" + NodeID + "'><img src='/Images/TreeLineImages/tree_line4.gif' border='0' width='19' height='20' title='"+Resources.L.浏览内容管理+ "' /></a>";
            }
            outstr = outstr + "<a href='ContentManage.aspx?NodeID=" + NodeID + "'><img src='/Images/TreeLineImages/t.gif' border='0' title='" + Resources.L.浏览内容管理 + "' /></a>";
        }
        switch (nodetype)
        {
            case 0:
            case 1://普通栏目节点与根节点
                if (childCount > 0)//如果有子节点
                {
                    outstr = outstr + "<a href='ContentManage.aspx?NodeID=" + NodeID + "'><span data-type='icon' class='fa fa-folder' title='" + Resources.L.浏览内容管理 + "'></span></a>";
                }
                else
                {
                    outstr = outstr + "<a href='ContentManage.aspx?NodeID=" + NodeID + "'><span class='" + GetIconPath(contentModel) + "' title='" + Resources.L.浏览内容管理 + "'></span></a>";
                }
                if (NodeID == 0)
                {
                    outstr = outstr + "<span>" + NodeName + nodeDir + "</span>";
                }
                else
                {
                    outstr = outstr + "<span><a href='EditNode.aspx?NodeID=" + NodeID + "'>" + NodeName + nodeDir + "</a></span>";
                }
                break;
            case 2://单页
                outstr = outstr + "<a href='ContentManage.aspx?NodeID=" + NodeID + "'><span class='" + GetIconPath(contentModel) + "' title='" + Resources.L.浏览内容管理 + "'></span></a>";
                outstr = outstr + "<span><a href='EditSinglePage.aspx?NodeID=" + NodeID + "'>" + NodeName + nodeDir + "</a></span>";
                break;
            case 3://外部链接
                outstr = outstr + "<a href='ContentManage.aspx?NodeID=" + NodeID + "'><i class='fa fa-chain' title='" + Resources.L.浏览内容管理 + "'></i></a>";
                outstr = outstr + "<span><a href='AddOutLink.aspx?id=" + NodeID + "'>" + NodeName + nodeDir + "</a></span>";
                break;
        }
        return outstr;
    }
    //节点类型
    public string GetNodeType(string NodeType)
    {
        return B_Node.GetNodeType(DataConverter.CLng(NodeType));
    }
    public string GetOper()
    {
        return GetOper((GetDataItem() as DataRowView).Row);
    }
    //根据NodeType显示可用操作
    public string GetOper(DataRow dr)
    {
        int NodeID=Convert.ToInt32(dr["NodeID"]);
        int NodeType=Convert.ToInt32(dr["NodeType"]);
        int ChildCount=Convert.ToInt32(dr["ChildCount"]);
        string outstr = "";
        string viewHtml="";
        if (NodeType == (int)NodeEnum.Node|| NodeType == (int)NodeEnum.SPage)//仅适用于栏目节点
        {
            int tempcount = 0;//栏目数量
            string indexurl = "";//首栏目地址
            string viewTlp = "<li><a href='/Class_" + NodeID + "/{0}' target='_blank'>{1}</a></li>";
            if (!string.IsNullOrEmpty(dr["IndexTemplate"].ToString())) { viewHtml += string.Format(viewTlp, "Default.aspx", Resources.L.首页); tempcount++; indexurl = "Default.aspx"; }
            if (!string.IsNullOrEmpty(dr["ListTemplateFile"].ToString())) { viewHtml += string.Format(viewTlp, "NodePage.aspx", Resources.L.列表); tempcount++; indexurl = "NodePage.aspx"; }
            if (!string.IsNullOrEmpty(dr["LastinfoTemplate"].ToString())) { viewHtml += string.Format(viewTlp, "NodeNews.aspx", Resources.L.最新); tempcount++; indexurl = "NodeNews.aspx"; }
            if (!string.IsNullOrEmpty(dr["HotinfoTemplate"].ToString())) { viewHtml += string.Format(viewTlp, "NodeHot.aspx", Resources.L.热门); tempcount++; indexurl = "NodeHot.aspx"; }
            if (!string.IsNullOrEmpty(dr["ProposeTemplate"].ToString())) { viewHtml += string.Format(viewTlp, "NodeElite.aspx", Resources.L.推荐); tempcount++; indexurl = "NodeElite.aspx"; }
            if (!string.IsNullOrEmpty(viewHtml)&&tempcount>1)
            {
                viewHtml = "  <div class='dropdown' style='display:inline;'><a class=' dropdown-toggle'href='javascript:;' data-toggle='dropdown' aria-expanded='false' title='"+Resources.L.浏览列表 + "'><span class='fa fa-caret-square-o-down'></span></a> <ul class='dropdown-menu' role='menu'>" + viewHtml + "</ul></div>";
            }
            else if(tempcount>0)
            {
                viewHtml = "  <a href='/Class_" + NodeID + "/" + indexurl + "' target='_blank' title='浏览首页'><span class='fa fa-caret-square-o-right'></span> 浏览</a>";
            }
        }
        string addLink = "<a href='EditNode.aspx?ParentID=" + NodeID + "' class='option_style'><i class='fa fa-plus' title='" + Resources.L.添加 + "'></i>" + Resources.L.节点 + "</a>";
        string delLink = "<a href='DelNode.aspx?NodeID=" + NodeID + "' onclick='return delConfirm();' class='option_style'><i class='fa fa-trash-o' title='" + Resources.L.删除 + "'></i>" + Resources.L.删除 + "</a>";
        string createLink = "<a href='javascript:;' onclick='createHtml("+NodeID+");' title='生成静态页' class='option_style' target='_blank'><i class='fa fa-play'></i>生成</a>";
        switch ((NodeEnum)Convert.ToInt32(NodeType))
        {
            case NodeEnum.Root:
                outstr = addLink + "<a href='EditSinglePage.aspx?ParentID=" + NodeID + "' class='option_style'><i class='fa fa-plus-circle' title='" + Resources.L.添加 + "'></i>" + Resources.L.单页 + "</a> <a href='AddOutLink.aspx?ParentID=" + NodeID + "' class='option_style'><i class='fa fa-link' title='" + Resources.L.链接 + "'></i>" + Resources.L.链接 + "</a>" + " <a href='javascript:;' data-toggle=\"modal\" data-target=\"#addinfo_div\" onclick='open_page(0,1)' class='option_style'><i class='fa fa-list-ol' title='" + Resources.L.排序 + "'></i>" + Resources.L.排序 + "</a>" + delLink;
                createLink = "";
                break;
            case NodeEnum.Node:
                outstr = "<a href='EditNode.aspx?NodeID=" + NodeID + "' class='option_style' ><i class='fa fa-pencil' title='" + Resources.L.修改 + "'></i>修改</a> " + addLink + " <a href='EditSinglePage.aspx?ParentID=" + NodeID + "' class='option_style'><i class='fa fa-plus-circle' title='" + Resources.L.添加 + "'></i>" + Resources.L.单页 + "</a> <a href='AddOutLink.aspx?ParentID=" + NodeID + "' class='option_style'><i class='fa fa-link' title='" + Resources.L.链接 + "'></i>" + Resources.L.链接 + "</a> ";
                if (ChildCount > 0)
                {
                    outstr = outstr + " <a href='javascript:void(0)' onclick='open_page(" + NodeID + ",2)' class='option_style'><i class='fa fa-list-ol' title='" + Resources.L.排序 + "'></i>" + Resources.L.排序 + "</a>";
                }
                outstr += delLink;
                break;
            case NodeEnum.SPage:
                outstr = "<a href='EditSinglePage.aspx?NodeID=" + NodeID + "' class='option_style'><i class='fa fa-pencil' title='" + Resources.L.修改 + "'></i>修改</a>" + delLink;
                outstr += "<a href='/Class_" + NodeID + "/Default.aspx'  target='_blank' class='option_style'><i class='fa fa-globe' title='" + Resources.L.浏览 + "'></i>" + Resources.L.浏览 + "</a>";
                break;
            case NodeEnum.OuterLink:
                outstr = "<a href='AddOutLink.aspx?id=" + NodeID + "' class='option_style'><i class='fa fa-pencil' title='" + Resources.L.修改 + "'></i>修改</a>" + delLink;
                //outstr += viewHtml;
                break;
        }
        //outstr += "  <span title='" + Resources.L.文章数 + "'>" + dr["ItemCount"] + "</span>";
        outstr += viewHtml;
        outstr += createLink;
        return outstr;
    }
    //批量删除
    protected void Button3_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.Form["idchk"]))
        {
            foreach (string nid in Request.Form["idchk"].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (DataConvert.CLng(nid) < 1) { continue; }
                bll.Del(DataConvert.CLng(nid));
            }
            function.WriteSuccessMsg(Resources.L.删除成功, customPath2 + "Content/NodeManage.aspx");
        }
    }
    //展开节点
    protected string ShoworHidden(object id)
    {
        int sid = DataConverter.CLng(id);
        int count = DataConvert.CLng(Eval("ChildCount"));
        int pid = DataConvert.CLng(Eval("ParentID"));
        int depth = Convert.ToInt32(Eval("depth"));
        if (Request.QueryString["action"] == "showall")
        {
            if (pid != 0 && depth > 1)
            {
                if (count > 0)
                {
                    return " ondblclick='showlist(" + sid + ")' state='0'";
                }
                else
                {
                    return "";
                }
            }
            else
            {
                if (count > 0)
                {

                    return "ondblclick='showlist(" + sid + ")' state='1'";
                }
            }
        }
        else
        {
            //int showdepth = 2;//默认显示深度
            if (pid != 0 && depth > 1)
            {
                if (count > 0)
                {
                    return "style='display:none' ondblclick='showlist(" + sid.ToString() + ")' state='0'";
                }
                else
                {
                    return "style='display:none'";
                }
            }
            else
            {
                if (count > 0)
                {

                    return "ondblclick='showlist(" + sid.ToString() + ")' state='1'";
                }
            }
        }
        return "";
    }
    //取出节点绑定模型的图标,需要优化,能否一起取出
    private string GetIconPath(string ContentModel)
    {
        B_Model bmod = new B_Model();
        string ItemIcon = "fa fa-globe";
        if (!string.IsNullOrEmpty(ContentModel))
        {
            try
            {
                int ModelID = Convert.ToInt32(ContentModel.Split(',')[0]);
                ItemIcon = bmod.GetWhere("ItemIcon", " And ModelID=" + ModelID, "").Rows[0][0].ToString(); //ZL_Model
            }
            catch { }
        }
        return ItemIcon;
    }
    private void EmptyNode()
    {
        bll.DeleteAll();
        bll.UpdateNodeXMLAll();
        Response.Redirect("NodeManage.aspx");
    }
}