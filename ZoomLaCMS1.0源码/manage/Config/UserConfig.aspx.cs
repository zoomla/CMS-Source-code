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
public partial class manage_Config_UserConfig : System.Web.UI.Page
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
            if (ZoomLa.Components.SiteConfig.UserConfig.EnableUserReg)//true 则为1
            { this.RadioButtonList1.SelectedIndex = 0; }
            else { this.RadioButtonList1.SelectedIndex = 1; }
            if (ZoomLa.Components.SiteConfig.UserConfig.EmailCheckReg)
            { this.RadioButtonList2.SelectedIndex = 0; }
            else { this.RadioButtonList2.SelectedIndex = 1; }
            if (ZoomLa.Components.SiteConfig.UserConfig.AdminCheckReg)
            { this.RadioButtonList3.SelectedIndex = 0; }
            else { this.RadioButtonList3.SelectedIndex = 1; }
            if (ZoomLa.Components.SiteConfig.UserConfig.EnableMultiRegPerEmail)
            { this.RadioButtonList4.SelectedIndex = 0; }
            else { this.RadioButtonList4.SelectedIndex = 1; }
            if (ZoomLa.Components.SiteConfig.UserConfig.EnableCheckCodeOfReg)
            { this.RadioButtonList5.SelectedIndex = 0; }
            else { this.RadioButtonList5.SelectedIndex = 1; }

            this.TextBox6.Text = ZoomLa.Components.SiteConfig.UserConfig.UserNameLimit.ToString();
            this.TextBox7.Text = ZoomLa.Components.SiteConfig.UserConfig.UserNameMax.ToString();
            this.TextBox8.Text = ZoomLa.Components.SiteConfig.UserConfig.UserNameRegDisabled;

            if (ZoomLa.Components.SiteConfig.UserConfig.EnableCheckCodeOfLogin)
            { this.RadioButtonList6.SelectedIndex = 0; }
            else { this.RadioButtonList6.SelectedIndex = 1; }
            if (ZoomLa.Components.SiteConfig.UserConfig.EnableMultiLogin)
            { this.RadioButtonList7.SelectedIndex = 0; }
            else { this.RadioButtonList7.SelectedIndex = 1; }

            if (ZoomLa.Components.SiteConfig.UserConfig.UserGetPasswordType.Equals(1)) { this.RadioButtonList8.SelectedIndex = 0; }
            if (ZoomLa.Components.SiteConfig.UserConfig.UserGetPasswordType.Equals(2)) { this.RadioButtonList8.SelectedIndex = 1; }

            this.TextBox12.Text = ZoomLa.Components.SiteConfig.UserConfig.EmailOfRegCheck;

        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        ZoomLa.Components.SiteConfigInfo obj2 = ZoomLa.Components.SiteConfig.ConfigInfo();

        obj2.UserConfig.EnableUserReg = this.RadioButtonList1.Items[0].Selected;//莫名其妙，这里居然可以使用 ture ，而 SiteOption.aspx 页面却不可以
        obj2.UserConfig.EmailCheckReg = this.RadioButtonList2.Items[0].Selected;
        obj2.UserConfig.AdminCheckReg = this.RadioButtonList3.Items[0].Selected;
        obj2.UserConfig.EnableMultiRegPerEmail = this.RadioButtonList4.Items[0].Selected;
        obj2.UserConfig.EnableCheckCodeOfReg = this.RadioButtonList5.Items[0].Selected;

        obj2.UserConfig.UserNameLimit = int.Parse(this.TextBox6.Text);
        obj2.UserConfig.UserNameMax = int.Parse(this.TextBox7.Text);
        obj2.UserConfig.UserNameRegDisabled = this.TextBox8.Text;

        obj2.UserConfig.EnableCheckCodeOfLogin = this.RadioButtonList6.Items[0].Selected;
        obj2.UserConfig.EnableMultiLogin = this.RadioButtonList7.Items[0].Selected;

        if (this.RadioButtonList8.SelectedIndex.Equals(0))
        //if (this.RadioButtonList8.Items[0].Selected) 这种写法也是对的，之前的错误是使用了 ZoomLa.Components.SiteConfig.UserConfig ，而不是 obj2.UserConfig
        { obj2.UserConfig.UserGetPasswordType = 1; }
        else { obj2.UserConfig.UserGetPasswordType = 2; }

        obj2.UserConfig.EmailOfRegCheck = this.TextBox12.Text;

        ZoomLa.Components.SiteConfig obj = new ZoomLa.Components.SiteConfig();
        obj.Update(obj2);
        Response.Write("<script>alert('配置保存成功')</script>");
    }
}
