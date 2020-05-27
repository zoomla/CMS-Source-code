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
using ZoomLa.Common;
using ZoomLa.Components;
using System.IO;
//using URLRewriter;
using System.Text.RegularExpressions;
using ZoomLa.Model;
//本页已经完成

namespace ZoomLaCMS.Manage.Config
{
    public partial class SiteInfo : CustomerPageAction
    {
        protected B_Admin badmin = new B_Admin();
        protected void Page_Init()
        {
            if (!IsPostBack)
            {
                Email_Reg.ValidationExpression = RegexHelper.S_Email;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!B_ARoleAuth.Check(ZLEnum.Auth.other, "SiteConfig"))
            {
                function.WriteErrMsg("没有权限进行此项操作");
            }
            if (!IsPostBack)
            {
                SiteName_T.Text = SiteConfig.SiteInfo.SiteName;
                SiteTitle_T.Text = SiteConfig.SiteInfo.SiteTitle;
                SiteUrl_T.Text = SiteConfig.SiteInfo.SiteUrl;
                LogoUrl_T.Text = SiteConfig.SiteInfo.LogoUrl;
                QRCode_T.Text = SiteConfig.SiteInfo.BannerUrl;
                Webmaster_T.Text = SiteConfig.SiteInfo.Webmaster;
                MasterPhone_T.Text = SiteConfig.SiteInfo.MasterPhone;
                WebmasterEmail_T.Text = SiteConfig.SiteInfo.WebmasterEmail;
                Copyright_T.Text = SiteConfig.SiteInfo.Copyright;
                MetaKeywords_T.Text = SiteConfig.SiteInfo.MetaKeywords;
                MetaDescription_T.Text = SiteConfig.SiteInfo.MetaDescription;
                LogoAdmin_T.Text = SiteConfig.SiteInfo.LogoAdmin;
                LogoPlatName_T.Text = SiteConfig.SiteInfo.LogoPlatName;
                CustomPath.Text = CustomerPageAction.baseCustomPath;
                allSiteJS.Text = SiteConfig.SiteInfo.AllSiteJS;
                ComName_T.Text = SiteConfig.SiteInfo.CompanyName;
                //取://到/之间的值
                Regex rg = new Regex("(?<=(://))[.\\s\\S]*?(?=(/))", RegexOptions.IgnoreCase);
                siteurl.Text = "http://" + rg.Match(Request.Url.AbsoluteUri).Value;
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='SiteInfo.aspx'>系统设置</a></li><li><a href='SiteInfo.aspx'>网站配置</a></li><li class=\"active\">网站信息配置</li>" + Call.GetHelp(2));
            }
        }
        //保存设置
    }
}