using System;
using System.Web;
using System.Web.UI;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Components;
using ZoomLa.Common;
using System.Xml;
/*
 * 用于单点登录页面,登录成功,默认关闭本页面,刷新父页面
 */
public partial class User_SimpleLogin : System.Web.UI.Page
{
    B_User bll = new B_User();
    protected void Page_Load(object sender, EventArgs e)
    {

        string sHtmlText = String.Empty;
        if (!IsPostBack)
        {
            bool postlogin = false;
            if (Request.Form["postlogin"] != null)
            {
                if (Request.Form["postlogin"].ToString().Trim() == "True")
                {
                    postlogin = true;
                }
            }
            //打印页面
            if (Request.QueryString["RegID"] != null)
            {
                int regid = DataConverter.CLng(Request.QueryString["RegID"]);
                ViewState["RegID"] = regid;
                if (regid == 1)  //Email注册
                {
                    if (SiteConfig.UserConfig.EmailRegis)
                    {
                        lblUser.Text = " &nbsp;E-Mail： ";
                        hlReg.Text = "用户名登录";
                        hlReg.NavigateUrl = "Login.aspx?RegID=0";
                    }
                    else
                    {
                        function.WriteErrMsg("系统未启用Email注册登录功能!", "/User/Login.aspx?RegID=0");
                    }
                }
                else if (regid == 2)  //Email注册
                {
                    if (SiteConfig.UserConfig.UserIDlgn)
                    {
                        lblUser.Text = "用户ID： ";
                        uidReg.Text = "用户名登录";
                        uidReg.NavigateUrl = "Login.aspx?RegID=0";
                    }
                    else
                    {
                        function.WriteErrMsg("系统未启用ID注册登录功能!", "/User/Login.aspx?RegID=0");
                    }
                }
                if (postlogin)
                {
                    //根据用户名和密码验证会员身份，并取得会员信息
                    if (Request.Form["UserName"] == null) { Response.End(); }
                    if (Request.Form["Password"] == null) { Response.End(); }
                    if (Request.Form["DropExpiration"] == null) { Response.End(); }
                    string AdminName = Request.Form["UserName"].ToString().Trim();
                    string AdminPass = Request.Form["Password"].ToString().Trim();
                    string CookieStatus = Request.Form["DropExpiration"].ToString().Trim();
                    M_UserInfo info = new M_UserInfo();

                    if (regid == 1)
                    {
                        info = bll.AuthenticateEmail(AdminName, AdminPass);
                    }
                    else if (regid == 2)
                    {
                        info = bll.AuthenticateID(Convert.ToInt32(AdminName), AdminPass);
                    }
                    else
                    {
                        info = bll.AuthenticateUser(AdminName, AdminPass);
                    }
                    if (info.IsNull)
                    {
                        function.WriteErrMsg("用户名或密码错误！", "/User/Login.aspx");
                    }
                    else
                    {
                        if (info.Status != 0)
                        {
                            function.WriteErrMsg("你的帐户未通过验证或被锁定，请与网站管理员联系", "/User/Login.aspx");
                        }
                        bll.SetLoginState(info, CookieStatus);
                        if (Request.Form["ajaxlogin"] != null)
                        {
                            Response.Write("location.href = 'user/Default.aspx';");
                            Response.End();
                        }
                    }
                }
            }
        }
        if (SiteConfig.UserConfig.EnableCheckCodeOfLogin)
        {
            this.trVcodeRegister.Visible = true;
        }
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        //if (SiteConfig.UserConfig.EnableCheckCodeOfLogin)
        //{
        //    if (!ZoomlaSecurityCenter.VCodeCheck(TxtValidateCode.Text.Trim()))
        //    {
        //        function.WriteErrMsg("验证码不正确", Request.RawUrl);
        //    }
        //}
        //根据用户名和密码验证会员身份，并取得会员信息
        string AdminName = this.TxtUserName.Text.Trim();
        string AdminPass = this.TxtPassword.Text.Trim();
        M_UserInfo info = new M_UserInfo();
        int regID = 0;
        if (DataConverter.CLng(ViewState["RegID"]) == 1)
        {
            if (SiteConfig.UserConfig.EmailRegis)
            {
                info = bll.AuthenticateEmail(AdminName, AdminPass);
            }
        }
        else if (DataConverter.CLng(ViewState["RegID"]) == 2)
        {
            if (SiteConfig.UserConfig.UserIDlgn)
            {
                info = bll.AuthenticateID(Convert.ToInt32(AdminName), AdminPass);
            }
        }
        else info = bll.AuthenticateUser(AdminName, AdminPass);
        //如果用户Model是空对象则表明登录失败
        if (info.IsNull)
        {
            function.WriteErrMsg("您的帐号或密码错误！", "/User/Login.aspx?RegID=" + regID);
        }
        else
        {
            if (info.Status != 0)
            {
                function.WriteErrMsg("你的帐户未通过验证或被锁定，请与网站管理员联系", "/User/Login.aspx");
            }
            bll.SetLoginState(info, "Day");
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "window.close();", true);
        }
    }
}