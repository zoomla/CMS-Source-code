namespace ZoomLa.WebSite.Manage.AddOn
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

    public partial class CorrectManage : System.Web.UI.Page
    {
        private B_Correct bll = new B_Correct();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                B_Admin badmin = new B_Admin();
                badmin.CheckMulitLogin();
                string title = "";
                this.ViewState["title"] = title;
                dBind();
            }
        }

        private void dBind()
        {
            string title = this.ViewState["title"].ToString();
            this.Gdv.DataSource = this.bll.GetList(title);
            this.Gdv.DataKeyNames = new string[] { "CorrectID" };
            this.Gdv.DataBind();
        }
        protected void Egv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.Gdv.PageIndex = e.NewPageIndex;
            dBind();
        }
        protected void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i <= Gdv.Rows.Count - 1; i++)
            {
                CheckBox cbox = (CheckBox)Gdv.Rows[i].FindControl("chkSel");
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
        public string GetCTitle(string tType)
        {
            int t = DataConverter.CLng(tType);
            string re = "";
            switch (t)
            {
                case 0:
                    re = "内容错误";
                    break;
                case 1:
                    re = "错别字";
                    break;
                case 2:
                    re = "图片错误";
                    break;
                case 3:
                    re = "链接错误";
                    break;
            }
            return re;
        }
        protected void Lnk_Click(object sender, GridViewCommandEventArgs e)
        {
            if (this.Page.IsValid)
            {
                if (e.CommandName == "Del")
                {
                    string Id = e.CommandArgument.ToString();
                    this.bll.Delete(DataConverter.CLng(Id));
                    dBind();
                }
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.TxtTitle.Text.Trim()))
            {
                this.ViewState["title"] = this.TxtTitle.Text.Trim();
                dBind();
            }
        }
        protected void btnDeleteAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= Gdv.Rows.Count - 1; i++)
            {
                CheckBox cbox = (CheckBox)Gdv.Rows[i].FindControl("chkSel");
                if (cbox.Checked == true)
                {
                    int itemID = Convert.ToInt32(Gdv.DataKeys[i].Value);
                    this.bll.Delete(itemID);
                }
            }
            dBind();
        }
}
}