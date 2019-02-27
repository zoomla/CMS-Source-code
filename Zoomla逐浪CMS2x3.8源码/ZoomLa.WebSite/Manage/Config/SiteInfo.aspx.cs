
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
using URLRewriter;
using System.Text.RegularExpressions;
using ZoomLa.Model;
//本页已经完成
public partial class Manage_I_Config_SiteInfo : CustomerPageAction
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
            siteurl.Text ="http://"+ rg.Match(Request.Url.AbsoluteUri).Value;
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='SiteInfo.aspx'>系统设置</a></li><li><a href='SiteInfo.aspx'>网站配置</a></li><li class=\"active\">网站信息配置</li>" + Call.GetHelp(2));
        }
    }
    //保存设置
    protected void Button1_Click(object sender, EventArgs e)
    {
        //必须用 SiteConfig 的实例的 Update() 方法更新才有效。而 Update() 方法不是static ，所以必须声明新的对象才能使用
        CustomerPageAction.SetCustomPath();
        SiteConfig.SiteInfo.SiteName = SiteName_T.Text;
        SiteConfig.SiteInfo.SiteTitle = SiteTitle_T.Text;
        SiteConfig.SiteInfo.SiteUrl = SiteUrl_T.Text;
        SiteConfig.SiteInfo.LogoUrl = LogoUrl_T.Text;
        SiteConfig.SiteInfo.BannerUrl = QRCode_T.Text;
        SiteConfig.SiteInfo.Webmaster = Webmaster_T.Text;
        SiteConfig.SiteInfo.MasterPhone = MasterPhone_T.Text;
        SiteConfig.SiteInfo.WebmasterEmail = WebmasterEmail_T.Text;
        SiteConfig.SiteInfo.Copyright = Copyright_T.Text;
        SiteConfig.SiteInfo.MetaKeywords = MetaKeywords_T.Text;
        SiteConfig.SiteInfo.MetaDescription = MetaDescription_T.Text;
        SiteConfig.SiteInfo.LogoAdmin = LogoAdmin_T.Text;
        SiteConfig.SiteInfo.LogoPlatName = LogoPlatName_T.Text;
        SiteConfig.SiteInfo.AllSiteJS = allSiteJS.Text.Trim();
        SiteConfig.SiteInfo.CompanyName = ComName_T.Text;
        CustomPath.Text = CustomPath.Text.Replace(" ", "");
        SiteConfig.SiteOption.ManageDir = CustomPath.Text;
        if (!CustomerPageAction.baseCustomPath.Equals(CustomPath.Text) && CustomPath.Text.Length >= 3 && CustomPath.Text.Length <= 10)
        {
            //这里保存了就不需要下面的了
            URLRewriter.Config.RewriteConfigUpdate rupdate = new URLRewriter.Config.RewriteConfigUpdate();
            rupdate.Update(HttpContext.Current, CustomPath.Text.Replace(" ", ""));
            SiteConfig.SiteOption.ManageDir = CustomPath.Text;
            SiteConfig.Update();
            CustomerPageAction.SetCustomPath();
            function.WriteSuccessMsg("更新个性化路径，正在配置，请等待八秒左右 <script>top.location.href='/" + CustomPath.Text + "/Default.aspx';</script>", "");
        }
        else
        {
            try
            {
                SiteConfig.Update();
                function.WriteSuccessMsg("网站信息配置保存成功！", CustomerPageAction.customPath + "Config/SiteInfo.aspx");
            }
            catch (FileNotFoundException)
            {
                function.WriteErrMsg("文件未找到！", CustomerPageAction.customPath + "Config/SiteInfo.aspx");
            }
            catch (UnauthorizedAccessException)
            {
                function.WriteErrMsg("检查您的服务器是否给配置文件或文件夹配置了写入权限。", CustomerPageAction.customPath + "Config/SiteInfo.aspx");
            }
        }
    }
}