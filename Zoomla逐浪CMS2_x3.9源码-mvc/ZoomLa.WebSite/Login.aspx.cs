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
using ZoomLa.Common;
using ZoomLa.API;

namespace ZoomLaCMS
{
    public partial class Login : System.Web.UI.Page
    {
        private B_User buser = new B_User();
        public string style;
        protected void Page_Load(object sender, EventArgs e)
        {
            style = Server.HtmlEncode(Request.QueryString["style"]);
            if (buser.CheckLogin())
            {
                M_UserInfo info = buser.GetLogin();
                SetStatus(info);
            }
            else
            {
                this.PnlLogin.Visible = true;
                if (SiteConfig.UserConfig.EnableCheckCodeOfLogin)
                {
                    this.PhValCode.Visible = true;
                }
                else
                {
                    this.PhValCode.Visible = false;
                }
                this.PnlLoginStatus.Visible = false;
                this.PnlLoginMessage.Visible = false;
            }
        }
        // 登录按钮
        protected void BtnLogin_Click(object sender, EventArgs e)
        {
            //if (SiteConfig.UserConfig.EnableCheckCodeOfLogin)
            //{
            //    if (!ZoomlaSecurityCenter.VCodeCheck(TxtValidateCode.Text.Trim()))
            //    {
            //        function.WriteErrMsg("验证码不正确", Request.RawUrl);
            //        this.PnlLogin.Visible = false;
            //        this.PnlLoginStatus.Visible = false;
            //        this.PnlLoginMessage.Visible = true;
            //    }
            //}
            //根据用户名和密码验证会员身份，并取得会员信息
            string AdminName = this.TxtUserName.Text.Trim();
            string AdminPass = this.TxtPassword.Text.Trim();

            M_UserInfo info = buser.AuthenticateUser(AdminName, AdminPass);
            //if(info.IsNull)
            //    info = buser.AuthenticateWorkNum(AdminName, AdminPass);
            int result = 0;
            //如果管理员Model是空对象则表明登录失败
            if (info.IsNull)
            {
                this.LitErrorMessage.Text = "<li>用户名或密码错误！</li>";

                this.PnlLogin.Visible = false;
                this.PnlLoginStatus.Visible = false;
                this.PnlLoginMessage.Visible = true;
            }
            else
            {
                result = 0;
                if (info.Status != 0)
                {
                    this.LitErrorMessage.Text = "<li>你的帐户未通过验证或被锁定，请与网站管理员联系</li>";
                    this.PnlLogin.Visible = false;
                    this.PnlLoginStatus.Visible = false;
                    this.PnlLoginMessage.Visible = true;
                    result = -1;
                    function.WriteErrMsg("你的帐户未通过验证或被锁定，请与网站管理员联系", "/Login.aspx");
                    return;
                }
                buser.SetLoginState(info, "Day");
                SetStatus(info);
                function.Script(this, "loginSec('" + result + "');");
            }
        }

        private void SetStatus(M_UserInfo info)
        {
            this.LitUserName.Text = info.UserName;
            this.LitLoginTime.Text = "登录次数：" + info.LoginTimes + "次";
            this.LitLoginDate.Text = "最近登录：" + info.LastLoginTimes.ToString();
            this.LitMessage.Text = "未读信息：" + B_Message.UserMessCount(info.UserID);
            this.PnlLoginStatus.Visible = true;
            this.PnlLogin.Visible = false;
            this.PnlLoginMessage.Visible = false;
        }
        protected void BtnReturn_Click(object sender, EventArgs e)
        {

        }
    }
}