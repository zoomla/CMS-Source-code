using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;
using ZoomLa.SQLDAL;
namespace ZoomLaCMS.Manage.Content
{
    public partial class NodeSearch : System.Web.UI.Page
    {
        B_Node bll = new B_Node();
        B_Model modBll = new B_Model();
        B_Admin badmin = new B_Admin();
        private enum NodeEnum { Root = 0, Node = 1, SPage = 2, OuterLink = 3 };
        public string Skey { get { return HttpUtility.UrlDecode((Request.QueryString["NodeID"] ?? "")).Trim(); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyBind();
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='" + CustomerPageAction.customPath2 + "Config/SiteInfo.aspx'>系统设置</a></li><li class='active dropdown'><a href='NodeManage.aspx'>节点管理</a></li><li><a href='NodeManage.aspx'>快速操作</a></li>");
            }
        }
        public void MyBind()
        {
            DataTable dt = bll.SelByPid(0, true);
            dt.Columns.Add(new DataColumn("NodeIDStr", typeof(string)));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["NodeIDStr"] = dt.Rows[i]["NodeID"];
            }
            if (string.IsNullOrEmpty(Skey))
            {
                function.WriteErrMsg("请输入需要查询的值");
            }
            else
            {
                dt.DefaultView.RowFilter = "NodeIDStr like '%" + Skey + "%' OR NodeName  Like '%" + Skey + "%'";
                dt.DefaultView.Sort = "NodeID Asc";
                dt = dt.DefaultView.ToTable();
            }
            //if (dt.Rows.Count == 1)
            //{
            //    Response.Redirect("EditNode.aspx?NodeID=" + dt.Rows[0]["NodeID"]);
            //}
            RPT.DataSource = dt;
            RPT.DataBind();
        }
        public string GetNodeType(string NodeType)
        {
            return B_Node.GetNodeType(DataConvert.CLng(NodeType));
        }
        public string GetIconPath(int NodeID)
        {
            M_Node nodeMod = bll.SelReturnModel(NodeID);
            if (string.IsNullOrEmpty(nodeMod.ContentModel)) { return ""; }
            int modelid = DataConvert.CLng(nodeMod.ContentModel.Split(',')[0]);
            M_ModelInfo modelMod = modBll.SelReturnModel(modelid);
            return StringHelper.GetItemIcon(modelMod.ItemIcon);
        }

        //获取栏目首页模板
        public string GetTempName(int NodeID)
        {
            M_Node node = bll.GetNodeXML(DataConverter.CLng(NodeID));
            return node.IndexTemplate;
        }
        //获取内容模板
        public string GetTemplate(int NodeID)
        {
            M_Node node = bll.GetNodeXML(DataConverter.CLng(NodeID));
            string modelArr = node.ContentModel;
            string result = "";
            string[] arr = modelArr.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < arr.Length; i++)
            {
                M_ModelInfo model = modBll.GetModelById(DataConverter.CLng(arr[i]));
                string name = "<span class='" + model.ItemIcon + "'></span>" + model.ModelName;
                if (string.IsNullOrEmpty(name)) continue;
                result += name + ",";
            }
            return result.TrimEnd(',');
        }
        public string GetTemplateurl(int NodeID)
        {
            string tempurl = GetTempName(NodeID);
            return Server.UrlEncode(tempurl);
        }
        public string GetOper()
        {
            return GetOper((GetDataItem() as DataRowView).Row);
        }
        //根据NodeType显示可用操作
        public string GetOper(DataRow dr)
        {
            int NodeID = Convert.ToInt32(dr["NodeID"]);
            int NodeType = Convert.ToInt32(dr["NodeType"]);
            int ChildCount = Convert.ToInt32(dr["ChildCount"]);
            string outstr = "";
            string viewHtml = "";
            if (NodeType == (int)NodeEnum.Node)//仅适用于栏目节点
            {
                int tempcount = 0;//栏目数量
                string indexurl = "";//首栏目地址
                string viewTlp = "<li><a href='/Class_" + NodeID + "/{0}' target='_blank'>{1}</a></li>";
                if (!string.IsNullOrEmpty(dr["IndexTemplate"].ToString())) { viewHtml += string.Format(viewTlp, "Default.aspx", Resources.L.首页); tempcount++; indexurl = "Default.aspx"; }
                if (!string.IsNullOrEmpty(dr["ListTemplateFile"].ToString())) { viewHtml += string.Format(viewTlp, "NodePage.aspx", Resources.L.列表); tempcount++; indexurl = "NodePage.aspx"; }
                if (!string.IsNullOrEmpty(dr["LastinfoTemplate"].ToString())) { viewHtml += string.Format(viewTlp, "NodeNews.aspx", Resources.L.最新); tempcount++; indexurl = "NodeNews.aspx"; }
                if (!string.IsNullOrEmpty(dr["HotinfoTemplate"].ToString())) { viewHtml += string.Format(viewTlp, "NodeHot.aspx", Resources.L.热门); tempcount++; indexurl = "NodeHot.aspx"; }
                if (!string.IsNullOrEmpty(dr["ProposeTemplate"].ToString())) { viewHtml += string.Format(viewTlp, "NodeElite.aspx", Resources.L.推荐); tempcount++; indexurl = "NodeElite.aspx"; }
                if (!string.IsNullOrEmpty(viewHtml) && tempcount > 1)
                {
                    viewHtml = "  <div class='dropdown' style='display:inline;'><a class=' dropdown-toggle'href='javascript:;' data-toggle='dropdown' aria-expanded='false' title='" + Resources.L.浏览列表 + "'><span class='fa fa-caret-square-o-down'></span></a> <ul class='dropdown-menu' role='menu'>" + viewHtml + "</ul></div>";
                }
                else if (tempcount > 0)
                {
                    viewHtml = "  <a href='/Class_" + NodeID + "/" + indexurl + "' target='_blank' title='浏览首页'><span class='fa fa-caret-square-o-right'></span> 浏览</a>";
                }
            }
            string delLink = "<a href='DelNode.aspx?NodeID=" + NodeID + "' onclick='return delConfirm();' class='option_style'><i class='fa fa-trash-o' title='" + Resources.L.删除 + "'></i>" + Resources.L.删除 + "</a>";
            string createLink = "";
            switch ((NodeEnum)Convert.ToInt32(NodeType))
            {
                case NodeEnum.Root:
                    outstr = "<a href='AddOutLink.aspx?ParentID=" + NodeID + "' class='option_style'><i class='fa fa-link' title='" + Resources.L.链接 + "'></i>" + Resources.L.链接 + "</a>" + " <a href='javascript:;' data-toggle=\"modal\" data-target=\"#addinfo_div\" onclick='open_page(0,1)' class='option_style'><i class='fa fa-list-ol' title='" + Resources.L.排序 + "'></i>" + Resources.L.排序 + "</a>" + delLink;
                    createLink = "";
                    break;
                case NodeEnum.Node:
                    outstr = "<a href='EditNode.aspx?NodeID=" + NodeID + "' class='option_style' ><i class='fa fa-pencil' title='" + Resources.L.修改 + "'></i>修改</a> " + " <a href='AddOutLink.aspx?ParentID=" + NodeID + "' class='option_style'><i class='fa fa-link' title='" + Resources.L.链接 + "'></i>" + Resources.L.链接 + "</a> ";
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
    }
}