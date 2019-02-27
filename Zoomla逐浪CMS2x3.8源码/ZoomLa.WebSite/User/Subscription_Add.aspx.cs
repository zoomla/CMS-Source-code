using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Common;
using ZoomLa.BLL;
using ZoomLa.Model;

public partial class Subscription_Add : System.Web.UI.Page
{
    B_Content bcontent = new B_Content();
    B_User buser = new B_User();

    protected void Page_Load(object sender, EventArgs e)
    {
        buser.CheckIsLogin();
    }
}
