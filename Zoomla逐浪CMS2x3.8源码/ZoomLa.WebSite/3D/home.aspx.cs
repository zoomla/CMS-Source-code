using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;

public partial class test_astar2_home : System.Web.UI.Page
{
    B_User buser=new B_User();
    protected void Page_Load(object sender, EventArgs e)
    {
        buser.CheckIsLogin(Request.RawUrl);
        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "BeginLogin('"+buser.GetLogin().UserName+"');", true);
        uName.Value = buser.GetLogin().UserName;
    }
}