namespace ZoomLa.WebSite.Manage.Content
{
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

    public partial class ContentRecycle : System.Web.UI.Page
    {
        private B_Content bll = new B_Content();
        private B_Node bNode = new B_Node();
        private B_Model bmode = new B_Model();
        private int NodeID;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                B_Admin badmin = new B_Admin();
                badmin.CheckMulitLogin();
                if (!badmin.ChkPermissions("ContentRecycle"))
                {
                    function.WriteErrMsg("没有权限进行此项操作");
                }
                if (string.IsNullOrEmpty(base.Request.QueryString["NodeID"]))
                    this.NodeID = 0;
                else
                    this.NodeID = DataConverter.CLng(base.Request.QueryString["NodeID"]);
                this.ViewState["NodeID"] = this.NodeID.ToString();
                RepNodeBind();
            }
        }
        public void RepNodeBind()
        {
            this.NodeID = DataConverter.CLng(this.ViewState["NodeID"].ToString());
            this.Egv.DataSource = this.bll.GetContentRecycle(this.NodeID);
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
                e.Row.Cells[1].Text = e.Row.Cells[1].Text;

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
        /// 还原
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
                    this.bll.Reset(itemID);
                }
            }
            RepNodeBind();
        }
        /// <summary>
        /// 清除所选内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= Egv.Rows.Count - 1; i++)
            {
                CheckBox cbox = (CheckBox)Egv.Rows[i].FindControl("chkSel");
                if (cbox.Checked == true)
                {
                    int itemID = Convert.ToInt32(Egv.DataKeys[i].Value);
                    this.bll.DelContent(itemID);
                }
            }
            RepNodeBind();
        }
        /// <summary>
        /// 清空回收站
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDeleteAll_Click(object sender, EventArgs e)
        {
            this.bll.DelRecycle();
            RepNodeBind();
        }
        /// <summary>
        /// 还原所有内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnReset_Click(object sender, EventArgs e)
        {
            this.bll.ResetAll();
            RepNodeBind();
        }

        protected void Lnk_Click(object sender, GridViewCommandEventArgs e)
        {
            
                if (e.CommandName == "Reset")
                {
                    string Id = e.CommandArgument.ToString();
                    this.bll.Reset(DataConverter.CLng(Id));
                }
                if (e.CommandName == "Del")
                {
                    string Id = e.CommandArgument.ToString();
                    this.bll.DelContent(DataConverter.CLng(Id));                    
                }
                RepNodeBind();
            
        }
        public string GetNodeName(string nodeid)
        {
            return this.bNode.GetNode(DataConverter.CLng(nodeid)).NodeName;
        }
        public string GetModelName(string modelid)
        {
            return this.bmode.GetModelById(DataConverter.CLng(modelid)).ModelName;
        }
    }
}