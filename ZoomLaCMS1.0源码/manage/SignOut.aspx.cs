namespace ZoomLa.WebSite
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
    using ZoomLa.Web;
    using ZoomLa.Model;

    public partial class _SingOut : AdminPage
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            B_Admin.ClearCookie();
            
            HttpContext.Current.Response.Redirect("~/manage/login.aspx");
        }
    }
}