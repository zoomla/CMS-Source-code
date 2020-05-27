using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Components;

using System.Xml;
using System.Data;
using ZoomLa.SQLDAL;

namespace ZoomLaCMS.Manage.Content
{
    public partial class ContentShow :CustomerPageAction
    {
        B_Content cll = new B_Content();
        B_Model mll = new B_Model();
        B_Node nll = new B_Node();
        B_AuditingState auditBll = new B_AuditingState();
        private string ItemID
        {
            get { return Request.QueryString["Gid"]; }
        }
        public int Gid { get { return DataConvert.CLng(Request.QueryString["Gid"]); } }
        protected bool createnew = true;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Gid < 1) { function.WriteErrMsg("未指定内容ID"); }
            M_CommonData Cdata = this.cll.GetCommonData(Gid);
            M_Node nodeMod = nll.SelReturnModel(Cdata.NodeID);
            NodeName.Text = nodeMod.NodeName;
            ContentID_L.Text = Cdata.GeneralID.ToString();
            Title_T.Text = Cdata.Title;
            statess.Text = auditBll.SelByStatus(Cdata.Status).StateName;
            M_ModelInfo MM = mll.GetModelById(Cdata.ModelID);
            //【重新修改】  【继续添加】  【管理】  【查看内容】 【前台预览】 
            this.Label1.Text = "<a class='btn btn-primary' href='EditContent.aspx?GeneralID=" + Cdata.GeneralID + "'>重新修改</a>";
            this.Label2.Text = "<a class='btn btn-primary' href='AddContent.aspx?ModelID=" + Cdata.ModelID + "&NodeID=" + Cdata.NodeID + "'>继续添加</a>";
            this.Label3.Text = "<a class='btn btn-primary' href='ContentManage.aspx?NodeID=" + Cdata.NodeID + "'>管理" + MM.ItemName + "</a>";
            this.Label4.Text = "<a class='btn btn-primary' href='ShowContent.aspx?GID=" + Cdata.GeneralID + "&modeid=" + Cdata.ModelID + "'>后台预览</a>";
            this.Label5.Text = "<a class='btn btn-primary' href='/Item/" + Cdata.GeneralID + ".aspx' target='_blank'>前台预览</a>";
            if (Cdata.IsCreate == 1)
            {
                string indexshowinfo = "";
                string nodeshowinfo = "";
                string Pageshowinfo = "";
                if (this.createnew)
                {
                    if (nodeMod.ContentFileEx < 3)
                    {
                        Pageshowinfo = "<br>已生成静态内容页！";
                    }
                }
                this.Label9.Text = " <font color=red>" + indexshowinfo + nodeshowinfo + Pageshowinfo + "</font>";
            }
            if (string.IsNullOrEmpty(Cdata.PdfLink))
            {
                this.Label8.Visible = false;
            }
            else
            {
                string pdfpath = "";
                if (SiteConfig.SiteOption.PdfDirectory != "")
                {
                    pdfpath = "/" + SiteConfig.SiteOption.PdfDirectory + "/";
                }
                else
                {
                    pdfpath = "/";
                }
                this.Label8.Text = "<a class='btn btn-primary' href='" + pdfpath + Cdata.PdfLink + "'target=\"_blank\">查看PDF</a>";
            }
            if (Request.QueryString["type"] != null && Request.QueryString["type"].Equals("add"))
                this.Label7.Text = "添加" + MM.ItemName + "成功";
            if (Request.QueryString["type"] != null && Request.QueryString["type"].Equals("edit"))
                this.Label7.Text = "修改" + MM.ItemName + "成功";
            if (!IsPostBack)
            {
                string link = "<div class='pull-right hidden-xs'>";
                link += "<span><a href='" + customPath2 + "Content/SchedTask.aspx' title='查看计划任务'><span class='fa fa-clock-o' style='color:#28b462;'></span></a>";
                link += GetOpenView() + "<span onclick=\"opentitle('EditNode.aspx?NodeID=" + nodeMod.NodeID + "','配置本节点');\" class='fa fa-cog' title='配置本节点' style='cursor:pointer;margin-left:5px;'></span></span>";
                link += "</div>";
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='ContentManage.aspx'>内容管理</a></li><li><a href='ContentManage.aspx?NodeID=" + Cdata.NodeID + "'>" + nll.GetNodeXML(Cdata.NodeID).NodeName + "</a></li><li class='active'>文章预览</li>" + link);
            }

        }
        private void GetMethod(M_Node nodeinfo)
        {
            if (nodeinfo.Purview != null && nodeinfo.Purview != "")
            {
                DataTable auitdt = nll.GetNodeAuitDT(nodeinfo.Purview);
                DataRow auitdr = auitdt.Rows[0];
                string View_v = auitdr["View"].ToString();
                if (View_v.ToLower() == "alluser")
                {
                    this.createnew = this.createnew && true;
                }
                else
                {
                    this.createnew = this.createnew && false;
                }
            }
        }
        public string GetOpenView()
        {
            //string d = CNodeID == 0 ? "1" : CNodeID.ToString();
            //string IndexTemplate = "", outstr = "", strurl = string.Empty;
            //strurl = "Class_" + d + "/Default.aspx";
            //DataTable dt = nll.SelNodeByModel(CNodeID).DefaultView.ToTable("IndexTemplate");
            //if (dt.Rows.Count > 0)
            //    IndexTemplate = dt.Rows[0][0].ToString();
            //if (IndexTemplate != "")
            //{
            //    outstr = " <a href='/" + strurl + "'  target='_blank' title='前台浏览'><span class='fa fa-eye'></span></a>";
            //}
            //else
            //{
            string outstr = " <a href='/Item/" + ItemID + ".aspx' target='_blank' title='前台浏览'><span class='fa fa-eye'></span></a>";
            //}
            return outstr;
        }
    }
}