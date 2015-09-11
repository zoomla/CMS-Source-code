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
using ZoomLa.Common;

using ZoomLa.Model;
using System.Text;
namespace ZoomLa.WebSite.Manage
{
    public partial class ContentManage : System.Web.UI.Page
    {
        private B_Content bll = new B_Content();
        private B_Node bNode = new B_Node();
        private B_Model bmode = new B_Model();
        protected int NodeID;
        protected string flag;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                B_Admin badmin = new B_Admin();
                badmin.CheckMulitLogin();
                if (string.IsNullOrEmpty(base.Request.QueryString["NodeID"]))
                {
                    this.NodeID = this.bNode.GetFirstNode(0);

                }
                else
                {
                    this.NodeID = DataConverter.CLng(base.Request.QueryString["NodeID"]);
                    if (this.NodeID == 0)
                        this.NodeID = this.bNode.GetFirstNode(0);
                }
                if (this.NodeID == 0)
                    function.WriteErrMsg("没有指定栏目节点ID或没有建立栏目节点");
                this.flag = string.IsNullOrEmpty(base.Request.QueryString["type"]) ? "" : base.Request.QueryString["type"];
                this.ViewState["NodeID"] = this.NodeID.ToString();
                this.ViewState["flag"] = this.flag;
                RepNodeBind();

                M_Node nod = this.bNode.GetNode(this.NodeID);
                string ModeIDList = nod.ContentModel;
                string[] ModelID = ModeIDList.Split(',');
                string AddContentlink = "";

                for (int i = 0; i < ModelID.Length; i++)
                {
                    AddContentlink = AddContentlink + "&nbsp;|&nbsp;<a href=\"AddContent.aspx?ModelID=" + ModelID[i] + "&NodeID=" + this.NodeID + "\">添加" + this.bmode.GetModelById(DataConverter.CLng(ModelID[i])).ItemName + "</a>";
                }
                this.lblAddContent.Text = AddContentlink;
            }
        }
        public void RepNodeBind()
        {
            this.NodeID = DataConverter.CLng(this.ViewState["NodeID"].ToString());
            this.flag = this.ViewState["flag"].ToString();
            this.Egv.DataSource = this.bll.GetContentList(this.NodeID, this.flag);
            this.Egv.DataKeyNames = new string[] { "GeneralID" };
            this.Egv.DataBind();
        }
        /// <summary>
        /// 绑定的行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Egv_RowDataBound(Object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Display the company name in italics.
                e.Row.Cells[1].Text =  e.Row.Cells[1].Text;

            }
        }
        protected void Egv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.Egv.PageIndex = e.NewPageIndex;
            this.RepNodeBind();
        }
        /// <summary>
        /// 全部选择控件设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i <= Egv.Rows.Count - 1; i++)
            {
                CheckBox cbox = (CheckBox)Egv.Rows[i].FindControl("chkSel");
                if (CheckBox2.Checked == true)
                {
                    cbox.Checked = true;
                }
                else
                {
                    cbox.Checked = false;
                }
            }
        }
        /// <summary>
        /// 审核通过
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAudit_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= Egv.Rows.Count - 1; i++)
            {
                CheckBox cbox = (CheckBox)Egv.Rows[i].FindControl("chkSel");
                if (cbox.Checked == true)
                {
                    int itemID = Convert.ToInt32(Egv.DataKeys[i].Value);
                    this.bll.SetAudit(itemID);
                }
            }
            RepNodeBind();
        }
        protected void btnDeleteAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= Egv.Rows.Count - 1; i++)
            {
                CheckBox cbox = (CheckBox)Egv.Rows[i].FindControl("chkSel");
                if (cbox.Checked == true)
                {
                    int itemID = Convert.ToInt32(Egv.DataKeys[i].Value);
                    this.bll.SetDel(itemID);
                }
            }
            RepNodeBind();
        }
        protected void Lnk_Click(object sender, GridViewCommandEventArgs e)
        {
            
                if (e.CommandName == "Edit")
                    Page.Response.Redirect("EditContent.aspx?GeneralID=" + e.CommandArgument.ToString());
                if (e.CommandName == "Del")
                {
                    string Id = e.CommandArgument.ToString();
                    this.bll.SetDel(DataConverter.CLng(Id));
                    RepNodeBind();
                }
            
        }
        public string GetElite(string Elite)
        {
            if (DataConverter.CLng(Elite) > 0)
                return "推荐";
            else
                return "未推荐";
        }
        public string GetTitle(string ItemID,string NID,string Title)
        {
            
            string n = "";
            if (DataConverter.CLng(NID) == this.NodeID)
                n = "<a href=\"EditContent.aspx?GeneralID=" + ItemID + "\">" + Title + "</a>";
            else
            {
                B_Node bl = new B_Node();
                n = "<a href=\"EditContent.aspx?GeneralID=" + ItemID + "\"><strong>[" + bl.GetNode(DataConverter.CLng(NID)).NodeName + "]</strong>&nbsp;" + Title + "</a>";
            }
            return n;
        }
        public string GetStatus(string status)
        {
            int s = DataConverter.CLng(status);
            if (s == 0)
                return "待审核";
            if (s == 99)
                return "已审核";
            if (s == -2)
                return "回收站";
            return "退档";
        }
        public string GetCteate(string IsCreate)
        {
            int s = DataConverter.CLng(IsCreate);
            if (s != 1)
                return "<font color=red>×</font>";
            else
                return "<font color=green>√</font>";
        }
        protected void btnMove_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i <= Egv.Rows.Count - 1; i++)
            {
                CheckBox cbox = (CheckBox)Egv.Rows[i].FindControl("chkSel");
                if (cbox.Checked == true)
                {
                    if (sb.Length == 0)
                        sb.Append(Egv.DataKeys[i].Value.ToString());
                    else
                        sb.Append("," + Egv.DataKeys[i].Value.ToString());
                }
            }
            Response.Redirect("ContentMove.aspx?id=" + sb.ToString());
        }
}
}