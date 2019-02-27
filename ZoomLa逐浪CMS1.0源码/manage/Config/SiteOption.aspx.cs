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

//本页已经完成
public partial class manage_Config_SiteOption : System.Web.UI.Page
{
    //xml 文件中少了4个属性,只有16个,所以处理的代码不全
    protected B_Admin badmin = new B_Admin();
    protected void Page_Load(object sender, EventArgs e)
    {
        this.badmin.CheckMulitLogin();
        if (!badmin.ChkPermissions("SiteConfig"))
        {
            function.WriteErrMsg("没有权限进行此项操作");
        }
        string s = "";
        if (!this.IsPostBack)
        {
            this.TextBox1.Text = SiteConfig.SiteOption.AdvertisementDir;           

            if(SiteConfig.SiteOption.EnableSiteManageCode)
            { this.RadioButton1.Checked = true; }
            else
            { this.RadioButton2.Checked = true; }
            if(SiteConfig.SiteOption.EnableSoftKey)
            { this.RadioButton3.Checked = true; }
            else
            { this.RadioButton4.Checked = true; }
            if(SiteConfig.SiteOption.EnableUploadFiles)
            { this.RadioButton5.Checked = true; }
            else
            { this.RadioButton6.Checked = true; }
            
            
            if(SiteConfig.SiteOption.IsAbsoluatePath)
            { this.RadioButton7.Checked = true; }
            else
            { this.RadioButton8.Checked = true; }            

            this.TextBox11.Text = SiteConfig.SiteOption.ManageDir;
            
            this.TextBox14.Text = SiteConfig.SiteOption.SiteManageCode;
            this.TextBox15.Text = SiteConfig.SiteOption.TemplateDir;
            this.TextBox2.Text = SiteConfig.SiteOption.IndexTemplate;
            this.TextBox3.Text = SiteConfig.SiteOption.CssDir;
            this.TextBox17.Text = SiteConfig.SiteOption.UploadDir;
            this.TextBox18.Text = SiteConfig.SiteOption.UploadFileExts;
            this.TextBox19.Text = SiteConfig.SiteOption.UploadFileMaxSize.ToString();

            s = SiteConfig.SiteOption.UploadFilePathRule;
            if (s.Equals(this.DropDownList1.Items[0].Text))
            { this.DropDownList1.Items[0].Selected = true; }
            if (s.Equals(this.DropDownList1.Items[1].Text))
            { this.DropDownList1.Items[1].Selected = true; }
            if (s.Equals(this.DropDownList1.Items[2].Text))
            { this.DropDownList1.Items[2].Selected = true; }
            if (s.Equals(this.DropDownList1.Items[3].Text))
            { this.DropDownList1.Items[3].Selected = true; }
            if (s.Equals(this.DropDownList1.Items[4].Text))
            { this.DropDownList1.Items[4].Selected = true; }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        SiteConfigInfo obj2 = SiteConfig.ConfigInfo();

        obj2.SiteOption.AdvertisementDir = this.TextBox1.Text;

        if (this.RadioButton1.Checked) { obj2.SiteOption.EnableSiteManageCode = true; }
        else { obj2.SiteOption.EnableSiteManageCode = false; }
        if (this.RadioButton3.Checked) { obj2.SiteOption.EnableSoftKey = true; }
        else { obj2.SiteOption.EnableSoftKey = false; }
        if (this.RadioButton5.Checked) { obj2.SiteOption.EnableUploadFiles = true; }
        else { obj2.SiteOption.EnableUploadFiles = false; }        
        
        
        if (this.RadioButton7.Checked) { obj2.SiteOption.IsAbsoluatePath = true; }
        else { obj2.SiteOption.IsAbsoluatePath = false; }
        

        obj2.SiteOption.ManageDir = this.TextBox11.Text;
        obj2.SiteOption.SiteManageCode = this.TextBox14.Text;
        obj2.SiteOption.TemplateDir = this.TextBox15.Text;
        obj2.SiteOption.CssDir = this.TextBox3.Text;
        obj2.SiteOption.IndexTemplate = this.TextBox2.Text;
        obj2.SiteOption.UploadDir = this.TextBox17.Text;
        obj2.SiteOption.UploadFileExts = this.TextBox18.Text;
        obj2.SiteOption.UploadFileMaxSize = int.Parse(this.TextBox19.Text);

        obj2.SiteOption.UploadFilePathRule = this.DropDownList1.SelectedItem.Text;

        ZoomLa.Components.SiteConfig obj = new ZoomLa.Components.SiteConfig();
        obj.Update(obj2);
        Response.Write("<script>alert('配置保存成功')</script>");
    }
}
