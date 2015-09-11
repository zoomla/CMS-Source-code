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
public partial class manage_Config_ThumbConfig : System.Web.UI.Page
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
            this.TextBox1.Text = ZoomLa.Components.SiteConfig.ThumbsConfig.ThumbsWidth.ToString();
            this.TextBox2.Text = ZoomLa.Components.SiteConfig.ThumbsConfig.ThumbsHeight.ToString();
            this.RadioButtonList1.SelectedIndex = ZoomLa.Components.SiteConfig.ThumbsConfig.ThumbsMode;
            this.TextBox4.Text = ZoomLa.Components.SiteConfig.ThumbsConfig.AddBackColor;

        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        ZoomLa.Components.SiteConfigInfo obj2 = ZoomLa.Components.SiteConfig.ConfigInfo();

        obj2.ThumbsConfig.ThumbsWidth = int.Parse(this.TextBox1.Text);
        obj2.ThumbsConfig.ThumbsHeight = int.Parse(this.TextBox2.Text);
        obj2.ThumbsConfig.ThumbsMode = this.RadioButtonList1.SelectedIndex;    // 亏大了，以后坚决不用 RadioButton ，只用 RadioButtonList
        obj2.ThumbsConfig.AddBackColor = this.TextBox4.Text;

        ZoomLa.Components.SiteConfig obj = new ZoomLa.Components.SiteConfig();
        obj.Update(obj2);
        Response.Write("<script>alert('配置保存成功')</script>");
    }
}
