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

//本页已经完成
public partial class manage_Config_SiteInfo : System.Web.UI.Page
{
    protected B_Admin badmin = new B_Admin();
    protected void Page_Load(object sender, EventArgs e)
    {
        this.badmin.CheckMulitLogin();
        if (!badmin.ChkPermissions("SiteConfig"))
        {
            function.WriteErrMsg("没有权限进行此项操作");
        }
        if (!this.IsPostBack)
        {
            //this.TextBox1.Text = ZoomLa.Components.SiteConfig.ConfigInfo().SiteInfo.SiteName;
            //不必要使用 ConfigInfo()
            this.TextBox1.Text = ZoomLa.Components.SiteConfig.SiteInfo.SiteName;
            this.TextBox2.Text = ZoomLa.Components.SiteConfig.SiteInfo.SiteTitle;
            this.TextBox3.Text = ZoomLa.Components.SiteConfig.SiteInfo.SiteUrl;
            this.TextBox4.Text = ZoomLa.Components.SiteConfig.SiteInfo.LogoUrl;
            this.TextBox5.Text = ZoomLa.Components.SiteConfig.SiteInfo.BannerUrl;
            this.TextBox6.Text = ZoomLa.Components.SiteConfig.SiteInfo.Webmaster;
            this.TextBox7.Text = ZoomLa.Components.SiteConfig.SiteInfo.WebmasterEmail;
            this.TextBox8.Text = ZoomLa.Components.SiteConfig.SiteInfo.Copyright;
            this.TextBox9.Text = ZoomLa.Components.SiteConfig.SiteInfo.MetaKeywords;
            this.TextBox10.Text = ZoomLa.Components.SiteConfig.SiteInfo.MetaDescription;

        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        //ZoomLa.Components.SiteConfig.ConfigInfo().SiteInfo.SiteName = this.TextBox1.Text; //没有效果

        //obj.Update(SiteInfo.SiteName) = this.TextBox1.Text; //这也是错误的，因为()中的参数数据类型不匹配

        // ZoomLa.Components.SiteConfig.ConfigInfo() 实际只是获取类的实例,对其属性 SiteInfo.SiteName 的改变实际上只是改变了实例，而没有改变 xml 数据文件
        // 必须用 ZoomLa.Components.SiteConfig 的实例的 Update() 方法更新才有效。而 Update() 方法不是static ，所以必须声明新的对象才能使用
        ZoomLa.Components.SiteConfigInfo obj2 = ZoomLa.Components.SiteConfig.ConfigInfo();

        obj2.SiteInfo.SiteName = this.TextBox1.Text;
        obj2.SiteInfo.SiteTitle = this.TextBox2.Text;
        obj2.SiteInfo.SiteUrl = this.TextBox3.Text;
        obj2.SiteInfo.LogoUrl = this.TextBox4.Text;
        obj2.SiteInfo.BannerUrl = this.TextBox5.Text;
        obj2.SiteInfo.Webmaster = this.TextBox6.Text;
        obj2.SiteInfo.WebmasterEmail = this.TextBox7.Text;
        obj2.SiteInfo.Copyright = this.TextBox8.Text;
        obj2.SiteInfo.MetaKeywords = this.TextBox9.Text;
        obj2.SiteInfo.MetaDescription = this.TextBox10.Text;

        ZoomLa.Components.SiteConfig obj = new ZoomLa.Components.SiteConfig();
        obj.Update(obj2);
        Response.Write("<script>alert('配置保存成功')</script>");
        //被修改后，会要求重新加载 xml 文件
        //“以下文件中的行尾不一致，要将行尾标准化吗？” ？ windous(CR LF) ？

    }
}
