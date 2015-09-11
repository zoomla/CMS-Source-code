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
using ZoomLa.Model;
namespace ZoomLa.WebSite.User
{
    public partial class _Default : System.Web.UI.Page
    {
        private B_User buser = new B_User();
        protected void Page_Load(object sender, EventArgs e)
        {
            buser.CheckIsLogin();            
        }
    }
}