namespace ZoomLa.WebSite.Manage.Template
{
    using System;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using ZoomLa.BLL;

    public partial class TemplateList : System.Web.UI.Page
    {
        protected string FilePathInput;
        protected void Page_Load(object sender, EventArgs e)
        {
            B_Admin badmin = new B_Admin();
            badmin.CheckMulitLogin();
            this.FilePathInput = base.Request.QueryString["OpenerText"];
        }
    }
}