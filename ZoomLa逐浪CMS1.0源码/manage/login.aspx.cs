namespace ZoomLa.WebSite.Manage
{
    using System;
    using System.Web;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using ZoomLa.Components;
    using ZoomLa.Common;
    using ZoomLa.Model;
    using ZoomLa.SQLDAL;
    using ZoomLa.IDAL;
    using ZoomLa.ZLEnum;
    using ZoomLa.Web;
    using ZoomLa.BLL;
    using ZoomLa.DALFactory;
    

    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!SiteConfig.SiteOption.EnableSiteManageCode)
            {
                this.LiSiteManageCode.Visible = false;
                this.ValrAdminValidateCode.Enabled = false;
            }
            if (!this.Page.IsPostBack && SiteConfig.SiteOption.EnableSoftKey)
            {
                this.TxtPassword.Attributes.Add("onfocus", "ShowSoftKeyboard(this)");
            }
        }
        protected void IbtnEnter_Click(object sender, ImageClickEventArgs e)
        {
            string vCode = this.Session["ValidateCode"].ToString();
            if (string.IsNullOrEmpty(vCode))
            {
                function.WriteErrMsg("<li>验证码无效，请刷新验证码重新登录</li>", "../Login.aspx");
            }
            if (string.Compare(this.TxtValidateCode.Text.Trim(), vCode, true) != 0)
            {
                function.WriteErrMsg("<li>验证码不正确</li>", "../Login.aspx");
            }
            if (SiteConfig.SiteOption.EnableSiteManageCode && (this.TxtAdminValidateCode.Text.Trim() != SiteConfig.SiteOption.SiteManageCode))
            {
                function.WriteErrMsg("<li>管理验证码不正确</li>", "../Login.aspx");
            }
            //根据用户名和密码验证管理员身份，并取得管理员信息
            string AdminName = this.TxtUserName.Text.Trim();
            string AdminPass = this.TxtPassword.Text.Trim();
            M_AdminInfo info = B_Admin.AuthenticateAdmin(AdminName, AdminPass);
            //如果管理员Model是空对象则表明登录失败
            if (info.IsNull)
            {
                function.WriteErrMsg("<li>用户名或密码错误！</li>", "../login.aspx");
            }
            else
            {
                if (info.IsLock)
                {
                    function.WriteErrMsg("<li>你的帐户被锁定，请与超级管理员联系</li>", "../Login.aspx");
                }
                B_Admin.SetLoginState(info);
            }
        }
    }
}