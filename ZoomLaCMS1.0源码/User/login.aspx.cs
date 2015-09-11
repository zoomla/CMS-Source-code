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

using ZoomLa.Model;
using ZoomLa.BLL;
using ZoomLa.Components;
using ZoomLa.Common;
namespace ZoomLa.WebSite.User
{
    public partial class User_login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void IbtnEnter_Click(object sender, ImageClickEventArgs e)
        {
            B_User bll = new B_User();
            string vCode = this.Session["ValidateCode"].ToString();
            if (string.IsNullOrEmpty(vCode))
            {
                function.WriteErrMsg("<li>验证码无效，请刷新验证码重新登录</li>", "/User/Login.aspx");
            }
            if (string.Compare(this.TxtValidateCode.Text.Trim(), vCode, true) != 0)
            {
                function.WriteErrMsg("<li>验证码不正确</li>", "Login.aspx");
            }            
            //根据用户名和密码验证会员身份，并取得会员信息
            string AdminName = this.TxtUserName.Text.Trim();
            string AdminPass = this.TxtPassword.Text.Trim();
            M_UserInfo info = bll.AuthenticateUser(AdminName, AdminPass);
            //如果用户Model是空对象则表明登录失败
            if (info.IsNull)
            {
                function.WriteErrMsg("<li>用户名或密码错误！</li>", "/User/Login.aspx");
            }
            else
            {
                if (SiteConfig.UserConfig.AdminCheckReg)
                {
                    if (info.Status != 0)
                    {
                        function.WriteErrMsg("<li>你的帐户未通过验证，请与超级管理员联系</li>", "/User/Login.aspx");
                    }
                }
                bll.SetLoginState(info);
                HttpContext.Current.Response.Redirect("Default.aspx");
            }
        }
    }
}