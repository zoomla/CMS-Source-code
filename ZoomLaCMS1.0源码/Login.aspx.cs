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
using ZoomLa.Components;

public partial class Login : System.Web.UI.Page
{
    private B_User buser = new B_User();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (buser.CheckLogin())
        {
            string loginName = HttpContext.Current.Request.Cookies["UserState"]["LoginName"];
            string password = HttpContext.Current.Request.Cookies["UserState"]["Password"];
            M_UserInfo info = buser.GetLoginUser(loginName, password);
            SetStatus(info);
        }
        else
        {
            this.PnlLogin.Visible = true;
            this.PnlLoginStatus.Visible = false;
            this.PnlLoginMessage.Visible = false;
        }
    }
    protected void BtnLogin_Click(object sender, EventArgs e)
    {
        string vCode = this.Session["ValidateCode"].ToString();
        if (string.IsNullOrEmpty(vCode))
        {
            this.LitErrorMessage.Text="<li>验证码无效，请刷新验证码重新登录</li>";
            this.PnlLogin.Visible = false;
            this.PnlLoginStatus.Visible = false;
            this.PnlLoginMessage.Visible = true;
        }
        if (string.Compare(this.TxtValidateCode.Text.Trim(), vCode, true) != 0)
        {
            this.LitErrorMessage.Text="<li>验证码不正确</li>";
            this.PnlLogin.Visible = false;
            this.PnlLoginStatus.Visible = false;
            this.PnlLoginMessage.Visible = true;
        }
        //根据用户名和密码验证会员身份，并取得会员信息
        string AdminName = this.TxtUserName.Text.Trim();
        string AdminPass = this.TxtPassword.Text.Trim();

        M_UserInfo info = buser.AuthenticateUser(AdminName, AdminPass);
        //如果管理员Model是空对象则表明登录失败
        if (info.IsNull)
        {
            this.LitErrorMessage.Text="<li>用户名或密码错误！</li>";
            this.PnlLogin.Visible = false;
            this.PnlLoginStatus.Visible = false;
            this.PnlLoginMessage.Visible = true;
        }
        else
        {
            if (SiteConfig.UserConfig.AdminCheckReg)
            {
                if (info.Status != 0)
                {
                    this.LitErrorMessage.Text="<li>你的帐户未通过验证，请与超级管理员联系</li>";
                    this.PnlLogin.Visible = false;
                    this.PnlLoginStatus.Visible = false;
                    this.PnlLoginMessage.Visible = true;
                }
            }
            buser.SetLoginState(info);
            SetStatus(info);
        }
    }

    private void SetStatus(M_UserInfo info)
    {
        this.LitUserName.Text = info.UserName;
        this.LitLoginTime.Text = "登录次数：" + info.LoginTimes + "次";
        this.LitLoginDate.Text = "最近登录：" + info.LastLoginTimes.ToString();
        this.LitMessage.Text = "未读信息：" + B_Message.UserMessCount(info.UserName);
        this.PnlLoginStatus.Visible = true;
        this.PnlLogin.Visible = false;
        this.PnlLoginMessage.Visible = false;
    }
    protected void BtnReturn_Click(object sender, EventArgs e)
    {

    }
}
