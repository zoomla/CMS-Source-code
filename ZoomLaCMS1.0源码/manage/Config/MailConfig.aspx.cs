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
public partial class manage_Config_MailConfig : System.Web.UI.Page
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
            this.TextBox1.Text = ZoomLa.Components.SiteConfig.MailConfig.MailFrom;
            this.TextBox2.Text = ZoomLa.Components.SiteConfig.MailConfig.MailServer;
            this.TextBox3.Text = ZoomLa.Components.SiteConfig.MailConfig.Port.ToString();
            this.CheckBox1.Checked = ZoomLa.Components.SiteConfig.MailConfig.EnabledSsl;

            if (ZoomLa.Components.SiteConfig.MailConfig.AuthenticationType.ToString().Equals("None"))
            { this.RadioButton1.Checked = true; }
            if (ZoomLa.Components.SiteConfig.MailConfig.AuthenticationType.ToString().Equals("Basic"))
            { this.RadioButton2.Checked = true; }
            if (ZoomLa.Components.SiteConfig.MailConfig.AuthenticationType.ToString().Equals("Ntlm"))
            { this.RadioButton3.Checked = true; }

            this.TextBox5.Text = ZoomLa.Components.SiteConfig.MailConfig.MailServerUserName;
            this.TextBox6.Text = ZoomLa.Components.SiteConfig.MailConfig.MailServerPassWord;

        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        ZoomLa.Components.SiteConfigInfo obj2 = ZoomLa.Components.SiteConfig.ConfigInfo();

        obj2.MailConfig.MailFrom = this.TextBox1.Text;
        obj2.MailConfig.MailServer = this.TextBox2.Text;
        obj2.MailConfig.Port = int.Parse(this.TextBox3.Text);

        obj2.MailConfig.EnabledSsl = this.CheckBox1.Checked;

        // enum 数据类型怎么处理？
        //ZoomLa.ZLEnum.AuthenticationType obj3 = ZoomLa.ZLEnum.AuthenticationType();

        if (this.RadioButton1.Checked)
        { obj2.MailConfig.AuthenticationType = ZoomLa.ZLEnum.AuthenticationType.None; }
        if (this.RadioButton2.Checked)
        { obj2.MailConfig.AuthenticationType = ZoomLa.ZLEnum.AuthenticationType.Basic; }
        if (this.RadioButton3.Checked)
        { obj2.MailConfig.AuthenticationType = ZoomLa.ZLEnum.AuthenticationType.Ntlm; }

        obj2.MailConfig.MailServerUserName = this.TextBox5.Text;
        obj2.MailConfig.MailServerPassWord = this.TextBox6.Text;

        ZoomLa.Components.SiteConfig obj = new ZoomLa.Components.SiteConfig();
        obj.Update(obj2);
        Response.Write("<script>alert('配置保存成功')</script>");
    }
}
