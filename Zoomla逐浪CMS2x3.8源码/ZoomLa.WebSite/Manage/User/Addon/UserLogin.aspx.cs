using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Helper;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.SQLDAL;

public partial class Manage_User_Addon_UserLogin : System.Web.UI.Page
{
    //用于支持作为此用户登录功能
    B_User buser = new B_User();
    protected void Page_Load(object sender, EventArgs e)
    {
        B_Admin.CheckIsLogged();
        string uname =HttpUtility.UrlDecode(Request.QueryString["uname"]);
        string upwd = Request.QueryString["upwd"];
        if (StrHelper.StrNullCheck(uname, upwd))
        {
            function.WriteErrMsg("用户名或密码不能为空");
        }
        M_UserInfo mu = buser.GetUserByName(uname, upwd);
        if (mu.IsNull) { function.WriteErrMsg("[" + uname + "]用户不存在"); }
        buser.SetLoginState(mu);
        Response.Redirect("/User/Default.aspx");
    }
}