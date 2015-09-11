namespace ZoomLa.WebSite.Manage
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

    public partial class Worktable : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            B_Admin badmin = new B_Admin();
            badmin.CheckMulitLogin();
            this.litUserName.Text = HttpContext.Current.Request.Cookies["ManageState"]["LoginName"].ToString();
            this.litDate.Text = DateTime.Now.ToShortDateString();
        }
    }
}