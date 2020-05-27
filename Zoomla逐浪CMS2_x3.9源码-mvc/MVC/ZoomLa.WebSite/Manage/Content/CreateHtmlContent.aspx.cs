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
using ZoomLa.BLL;
using ZoomLa.Components;
using ZoomLa.Common;
using System.Xml;
using ZoomLa.Model;
using Newtonsoft.Json;
using System.IO;
using ZoomLa.Model.CreateJS;
using ZoomLa.BLL.CreateJS;

namespace ZoomLaCMS.Manage.Content
{
    public partial class CreateHtmlContent : CustomerPageAction
    {
        B_Node nodeBll = new B_Node();
        B_Spec spBll = new B_Spec();
        B_Content_ScheTask scheBll = new B_Content_ScheTask();
        M_Content_ScheTask scheMod = new M_Content_ScheTask();
        M_Release relMod = new M_Release();
        B_Release relBll = new B_Release();
        private enum NodeEnum { Root = 0, Node = 1, SPage = 2, OuterLink = 3 };
        private string CType { get { return (Request.QueryString["CType"] ?? "").ToLower(); } }
        private int NodeID { get { return DataConverter.CLng(Request.QueryString["NodeID"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (function.isAjax())
            {
                int nid = Convert.ToInt32(Request.Form["nid"]);
                DataTable dt = nodeBll.GetNodeChildList(nid);
                dt.Columns.Add(new DataColumn("icon", typeof(string)));
                dt.Columns.Add(new DataColumn("oper", typeof(string)));
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    dt.Rows[i]["icon"] = ShowIcon(Convert.ToInt32(dr["NodeID"]), dr["NodeName"].ToString(), dr["NodeDir"].ToString(), Convert.ToInt32(dr["ChildCount"]));
                    dt.Rows[i]["oper"] = GetOper(dr);
                }
                string json = JsonConvert.SerializeObject(dt);
                Response.Write(json); Response.Flush(); Response.End();
            }
            if (!IsPostBack)
            {
                B_ARoleAuth.CheckEx(ZLEnum.Auth.content, "CreateHtmL");
                //----------单节点直接生成
                if (!string.IsNullOrEmpty(CType) && NodeID > 0)
                {
                    switch (CType)
                    {
                        case "node":
                            relMod.MyRType = M_Release.RType.NodeIDS;
                            break;
                        case "content":
                            relMod.MyRType = M_Release.RType.ByNodeIDS;
                            break;
                        case "spage":
                            relMod.MyRType = M_Release.RType.SPage;
                            break;
                        case "spec":
                            relMod.MyRType = M_Release.RType.Special;
                            break;
                    }
                    relMod.NodeIDS = NodeID.ToString();
                    Insert(relMod); Response.End(); return;
                }
                //----------常规逻辑
                switch (SiteConfig.SiteOption.IndexEx)
                {
                    case "0":
                        Label1.Text = Resources.L.当前首页格式为HTML;
                        break;
                    case "1":
                        Label1.Text = Resources.L.当前首页格式为HTM;
                        break;
                    case "2":
                        Label1.Text = Resources.L.当前首页格式为SHTML;
                        break;
                    case "3":
                        Label1.Text = Resources.L.未设置生成静态首页 + "，" + Resources.L.如需生成静态首页 + " " + Resources.L.请 + "<a href='" + customPath2 + "Config/SiteOption.aspx' style='margin-left:5px;' class='btn btn-default'>" + Resources.L.前往设置 + ">></a>";
                        btnCreate.Visible = false;
                        break;
                }
                MyBind();
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='ContentManage.aspx'>" + Resources.L.内容管理 + "</a></li><li class='active'><a href='" + Request.RawUrl + "'>" + Resources.L.生成发布 + "</a></li>");
            }
        }
        private void MyBind()
        {
            DataTable dt = nodeBll.GetNodeChildList(0);
            dt.DefaultView.RowFilter = "NodeType IN(1)";
            NodeRPT.DataSource = dt.DefaultView.ToTable();
            NodeRPT.DataBind();
            dt.DefaultView.RowFilter = "NodeType IN(2)";
            DataTable spageDT = dt.DefaultView.ToTable();
            SPageRPT.DataSource = spageDT;
            SPageRPT.DataBind();
            SpecRPT.DataSource = spBll.SelAsNode();
            SpecRPT.DataBind();
            //计划任务
            DataTable schDT = null;// scheBll.SelByTaskType(M_Content_ScheTask.TaskTypeEnum.Release);
            if (schDT != null && schDT.Rows.Count > 0)
            {
                scheMod = scheMod.GetModelFromDR(schDT.Rows[0]);
                scheTime_T.Text = scheMod.ExecuteTime2.ToString("HH:mm");
            }
        }
        // 发布站点主页
        protected void btnCreate_Click(object sender, EventArgs e)
        {
            relMod.MyRType = M_Release.RType.Index;
            Insert(relMod);
        }
        // 发布所有内容
        protected void btnNewsContent_Click(object sender, EventArgs e)
        {
            relMod.MyRType = M_Release.RType.ALL;
            Insert(relMod);
        }
        // 按ID发布内容
        protected void btnCreateId_Click(object sender, EventArgs e)
        {
            relMod.MyRType = M_Release.RType.IDRegion;
            relMod.SGid = DataConverter.CLng(txtIdStart.Text);
            relMod.EGid = DataConverter.CLng(txtIdEnd.Text);
            if (relMod.SGid == 0 && relMod.SGid == relMod.EGid)
            {
                function.WriteErrMsg(Resources.L.起始ID不能为空);
            }
            Insert(relMod);
        }
        // 发布最新数量的内容
        protected void btnNewsCount_Click(object sender, EventArgs e)
        {
            relMod.MyRType = M_Release.RType.Newest;
            relMod.Count = DataConverter.CLng(txtNewsCount.Text.Trim());
            if (relMod.Count < 1) { function.WriteErrMsg(Resources.L.指定的数值不正确 + "," + Resources.L.最少生成最近1篇); }
            Insert(relMod);
        }
        // 按日期发布内容
        protected void RDate_Click(object sender, EventArgs e)
        {
            relMod.MyRType = M_Release.RType.DateRegion;
            relMod.STime = DataConverter.CDate(STime_T.Text);
            relMod.ETime = DataConverter.CDate(ETime_T.Text);
            if ((relMod.ETime - relMod.STime).TotalMinutes < 1)
            {
                function.WriteErrMsg(Resources.L.时间不正确 + "," + Resources.L.开始时间必须小于结束时间);
            }
            Insert(relMod);
        }
        // 按栏目发布内容
        protected void btnColumnCreate_Click(object sender, EventArgs e)
        {
            string nids = Request.Form["nodechk"];
            if (string.IsNullOrEmpty(nids)) { function.WriteErrMsg(Resources.L.未选定栏目); }
            nids = nids.TrimEnd(',');
            relMod.MyRType = M_Release.RType.ByNodeIDS;
            relMod.NodeIDS = nids;
            Insert(relMod);
        }
        // 发布所有栏目页
        protected void btnCreateColumnAll_Click(object sender, EventArgs e)
        {
            relMod.MyRType = M_Release.RType.ALLNode;
            Insert(relMod);
        }
        // 发布选定的栏目页
        protected void btnCreateColumn_Click(object sender, EventArgs e)
        {
            string nids = Request.Form["nodechk"];
            if (string.IsNullOrEmpty(nids)) { function.WriteErrMsg(Resources.L.未选定栏目); }
            nids = nids.TrimEnd(',');
            relMod.MyRType = M_Release.RType.NodeIDS;
            relMod.NodeIDS = nids;
            Insert(relMod);
        }
        // 发布所有单页
        protected void btnCreateSingleAll_Click(object sender, EventArgs e)
        {
            relMod.MyRType = M_Release.RType.ALLSPage;
            Insert(relMod);
        }
        // 发布选定的单页
        protected void btnCreateSingle_Click(object sender, EventArgs e)
        {
            string nids = Request.Form["spagechk"];
            if (string.IsNullOrEmpty(nids)) { function.WriteErrMsg(Resources.L.未选定栏目); }
            nids = nids.TrimEnd(',');
            relMod.MyRType = M_Release.RType.SPage;
            relMod.NodeIDS = nids;
            Insert(relMod);
        }
        // 发布选定的专题
        protected void btnCreateSpeacil_Click(object sender, EventArgs e)
        {
            string nids = Request.Form["spchk"];
            if (string.IsNullOrEmpty(nids)) { function.WriteErrMsg(Resources.L.未选定专题); }
            nids = nids.TrimEnd(',');
            relMod.MyRType = M_Release.RType.Special;
            relMod.NodeIDS = nids;
            Insert(relMod);
        }
        //新建定时发布任务
        protected void scheSure_Btn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(scheTime_T2.Text) && string.IsNullOrEmpty(scheTime_T.Text)) return;
            scheTime_T.Text = scheTime_T.Text.Trim();
            DataTable schDT = null;// scheBll.SelByTaskType(M_Content_ScheTask.TaskTypeEnum.Release);
            if (schDT != null && schDT.Rows.Count > 0)
            {
                scheMod = scheMod.GetModelFromDR(schDT.Rows[0]);
                scheMod.ExecuteTime = DateTime.Now.ToString("yyyy/MM/dd ") + scheTime_T.Text;
                scheBll.Update(scheMod);
            }
            else
            {
                scheMod.TaskName = "定时发布首页";
                scheMod.TaskContent = "index";
                scheMod.TaskType = (int)M_Content_ScheTask.TaskTypeEnum.Release;
                scheMod.ExecuteType = (int)M_Content_ScheTask.ExecuteTypeEnum.EveryDay;//暂时无效,原本用来决定是否IsLoop
                scheMod.ExecuteTime = DateTime.Now.ToString("yyyy/MM/dd ") + scheTime_T.Text;
                scheMod.Status = 0;
                scheBll.Add(scheMod);
            }
            Response.Redirect("SchedTask.aspx");
        }
        public void Insert(M_Release model)
        {
            relBll.Insert(model);
            Response.Redirect("CreateHtml.aspx");
        }
        public string GetNodeType()
        {
            switch (Convert.ToInt32(Eval("NodeType")))
            {
                case 0:
                    return Resources.L.根节点;
                case 1:
                    return Resources.L.栏目节点;
                case 2:
                    return Resources.L.单页节点;
                case 3:
                    return Resources.L.外部链接;
                default:
                    return "";
            }
        }
        public string GetIcon()
        {
            DataRowView dr = GetDataItem() as DataRowView;
            return ShowIcon(Convert.ToInt32(dr["NodeID"]), dr["NodeName"].ToString(), dr["NodeDir"].ToString(), Convert.ToInt32(dr["ChildCount"]));
        }
        public string ShowIcon(int nid, string nodeName, string nodeDir, int childCount)
        {
            nodeName = "" + nodeName + "[" + nodeDir + "]";
            string html = "<img src='/Images/TreeLineImages/t.gif' border='0'>";
            if (childCount > 0)
            {
                html += "<span data-type='icon' class='fa fa-folder'></span><span>" + nodeName + "</span>";
            }
            else
            {
                html += "<span>" + nodeName + "</span>";
            }
            return html;
        }
        public string GetOper()
        {
            return GetOper((GetDataItem() as DataRowView).Row);
        }
        public string GetOper(DataRow dr)
        {
            int NodeID = Convert.ToInt32(dr["NodeID"]);
            int NodeType = Convert.ToInt32(dr["NodeType"]);
            int ChildCount = Convert.ToInt32(dr["ChildCount"]);
            string viewHtml = "";
            if (NodeType == (int)NodeEnum.Node || NodeType == (int)NodeEnum.SPage)//仅适用于栏目节点
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
                    viewHtml = "  <div class='dropdown' style='display:inline;'><a class=' dropdown-toggle'href='javascript:;' data-toggle='dropdown' aria-expanded='false' title='" + Resources.L.浏览列表 + "'><span class='fa fa-caret-square-o-down'></span>浏览</a> <ul class='dropdown-menu' role='menu'>" + viewHtml + "</ul></div>";
                }
                else if (tempcount > 0)
                {
                    viewHtml = "  <a href='/Class_" + NodeID + "/" + indexurl + "' target='_blank' title='浏览首页'><span class='fa fa-caret-square-o-right'></span>浏览</a>";
                }
            }
            if (NodeType == (int)NodeEnum.SPage)
            {
                viewHtml = "<a href='/Class_" + NodeID + "/Default.aspx'  target='_blank' class='option_style'><i class='fa fa-globe' title='" + Resources.L.浏览 + "'></i>" + Resources.L.浏览 + "</a>";
            }
            return viewHtml;
        }
    }
}