using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;

public partial class manage_Site_Share : CustomerPageAction
{
    B_User bll = new B_User();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!string.IsNullOrEmpty(Request.QueryString["identity"]))
        {
            if (bll.GetUserIDByUserName(Request.QueryString["identity"]).IsNull)
                function.WriteErrMsg("该链接无效");
            else TxtUserName.Text = Request.QueryString["identity"];
        }

        if (!IsPostBack)
        {
            if (SiteConfig.UserConfig.EnableCheckCodeOfLogin)
                codeSpan.Visible = true;
        }
    }
    protected void loginBtn_Click(object sender, EventArgs e)
    {
        if (SiteConfig.UserConfig.EnableCheckCodeOfLogin)
        {
            string vCode = this.Session["ValidateCode"].ToString();
            if (string.IsNullOrEmpty(vCode))
            {
                function.WriteErrMsg("<li>验证码无效，请刷新验证码重新登录</li>", "/User/Login.aspx");
            }
            if (string.Compare(this.TxtValidateCode.Text.Trim(), vCode, true) != 0)
            {
                function.WriteErrMsg("<li>验证码不正确</li>", "/User/Login.aspx");
            }
        }
        //根据用户名和密码验证会员身份，并取得会员信息
        string AdminName = this.TxtUserName.Text.Trim();
        string AdminPass = this.TxtPassword.Text.Trim();
        M_UserInfo info = new M_UserInfo();
        info = bll.AuthenticateUser(AdminName, AdminPass); 
        if (info.IsNull)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(),"","alert('用户名或密码错误!!')",true);
        }
        else
        {
            if (info.Status != 0)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('你的帐户未通过验证或被锁定，请与网站管理员联系!!')", true);
            }
            bll.SetLoginState(info, "Day");
            M_Site_SiteList siteM = new M_Site_SiteList();
            B_Site_SiteList siteBll = new B_Site_SiteList();
            siteM = siteBll.SelByUserID(info.UserID.ToString());
            if (siteM == null)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('你无对应的权限!!')", true);
            }
            else
            {
                Response.Redirect(CustomerPageAction.customPath + "Site/SiteDetail.aspx?siteID=" + siteM.SiteID);
            }

        }
    }
}