using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model;

public partial class User_Cloud_Jwplayer_PlayM : System.Web.UI.Page
{
    protected B_User ull = new B_User();
    protected M_UserInfo uinfo;
    public string loginName;
    protected void Page_Load(object sender, EventArgs e)
    {
        ull.CheckIsLogin();
        this.uinfo = ull.GetLogin();
        loginName = uinfo.UserName;
    }
}