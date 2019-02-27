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
using ZoomLa.API;
using ZoomLa.Model;
using ZoomLa.Common;
using ZoomLa.Components;
using System.Net;

namespace ZoomLa.WebSite.User
{
    public partial class Logout : System.Web.UI.Page
    {
        B_User buser = new B_User();
        protected void Page_Load(object sender, EventArgs e)
        {
            buser.ClearCookie();
            string url = "/User/Login.aspx";
            string ReturnUrl = Request.QueryString["ReturnUrl"];
            if (!string.IsNullOrEmpty(ReturnUrl)) { url += "?ReturnUrl=" + ReturnUrl; }
            Response.Write("<script>setTimeout(function(){location='" + url + "';},500);</script>");
        }
    }
}