using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;

public partial class manage_Lockin : CustomerPageAction
{
    private B_Admin badmin = new B_Admin();
    protected void Page_Load(object sender, EventArgs e)
    {
        B_Admin.CheckIsLogged();
        if (!IsPostBack)
        {
            tips.InnerHtml = "";
        }
    }
    protected void btn_Click1(object sender, EventArgs e)
    {
        //根据用户名和密码验证管理员身份，并取得管理员信息
        string AdminName = "";
        string loginName = badmin.GetAdminLogin().AdminName;
        if (!string.IsNullOrEmpty(loginName))
        {
            AdminName = loginName;
        }
        string AdminPass = this.TxtPassword.Text.Trim();
        M_AdminInfo info = B_Admin.AuthenticateAdmin(AdminName, AdminPass);
        //如果管理员Model是空对象则表明登录失败
        if (info==null)
        {
            tips.InnerHtml = "密码错误！";
        }
        else
        {
            if (info.IsLock)
            {
                tips.InnerHtml = "你的帐户被锁定，请与超级管理员联系";
            }
            function.Script(this, " setCookie('0');parent.document.getElementById('infoDiv').style.display='none';parent.document.getElementById('tranDiv').style.display='none'; ");
        }
    }
}