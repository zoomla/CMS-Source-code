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
public partial class manage_Config_WaterMarkConfig : System.Web.UI.Page
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
            string s = "";
            // WaterMarkConfig 类的2个属性分别又是一个类
            this.RadioButtonList1.SelectedIndex = ZoomLa.Components.SiteConfig.ConfigInfo().WaterMarkConfig.WaterMarkType - 1;//对应的cs文件是WaterMarkText,不是WaterMarkTextInfo    //对应的名称不对,只有WaterMarkConfig

            this.TextBox2.Text = ZoomLa.Components.SiteConfig.ConfigInfo().WaterMarkConfig.WaterMarkTextInfo.Text;
            this.TextBox3.Text = ZoomLa.Components.SiteConfig.ConfigInfo().WaterMarkConfig.WaterMarkTextInfo.FoneType;
            this.TextBox4.Text = ZoomLa.Components.SiteConfig.ConfigInfo().WaterMarkConfig.WaterMarkTextInfo.FoneSize.ToString();
            this.TextBox5.Text = ZoomLa.Components.SiteConfig.ConfigInfo().WaterMarkConfig.WaterMarkTextInfo.FoneColor;
            this.TextBox6.Text = ZoomLa.Components.SiteConfig.ConfigInfo().WaterMarkConfig.WaterMarkTextInfo.FoneStyle;
            this.TextBox7.Text = ZoomLa.Components.SiteConfig.ConfigInfo().WaterMarkConfig.WaterMarkTextInfo.FoneBorderColor;
            this.RadioButtonList2.SelectedIndex = 1 - ZoomLa.Components.SiteConfig.ConfigInfo().WaterMarkConfig.WaterMarkTextInfo.FoneBorder;
            s = ZoomLa.Components.SiteConfig.ConfigInfo().WaterMarkConfig.WaterMarkTextInfo.WaterMarkPosition;
            if (s.Trim().Equals("WM_TOP_LEFT")) { this.RadioButtonList3.SelectedIndex = 0; }
            if (s.Trim().Equals("WM_TOP_CENTER")) { this.RadioButtonList3.SelectedIndex = 1; }
            if (s.Trim().Equals("WM_TOP_RIGHT")) { this.RadioButtonList3.SelectedIndex = 2; }
            this.TextBox10.Text = ZoomLa.Components.SiteConfig.ConfigInfo().WaterMarkConfig.WaterMarkTextInfo.WaterMarkPositionX.ToString();
            this.TextBox11.Text = ZoomLa.Components.SiteConfig.ConfigInfo().WaterMarkConfig.WaterMarkTextInfo.WaterMarkPositionY.ToString();//对应的cs文件是WaterMarkText,不是WaterMarkTextInfo    //对应的名称不对,只有WaterMarkConfig


            this.TextBox12.Text = ZoomLa.Components.SiteConfig.ConfigInfo().WaterMarkConfig.WaterMarkImageInfo.ImagePath;
            s = ZoomLa.Components.SiteConfig.ConfigInfo().WaterMarkConfig.WaterMarkImageInfo.WaterMarkPosition;//惨痛教训：当 RadioButtonList4 使用较多时，必须严重注意读取数据是不是写的重合了，很容易出问题
            if (s.Trim().Equals("WM_TOP_LEFT")) { this.RadioButtonList4.SelectedIndex = 0; }
            if (s.Trim().Equals("WM_TOP_CENTER")) { this.RadioButtonList4.SelectedIndex = 1; }
            if (s.Trim().Equals("WM_TOP_RIGHT")) { this.RadioButtonList4.SelectedIndex = 2; }
            this.TextBox14.Text = ZoomLa.Components.SiteConfig.ConfigInfo().WaterMarkConfig.WaterMarkImageInfo.WaterMarkPositionX.ToString();
            this.TextBox15.Text = ZoomLa.Components.SiteConfig.ConfigInfo().WaterMarkConfig.WaterMarkImageInfo.WaterMarkPositionY.ToString();
        } 
    }
    
    protected void Button1_Click1(object sender, EventArgs e)
    {
        ZoomLa.Components.SiteConfigInfo obj2 = ZoomLa.Components.SiteConfig.ConfigInfo();

        obj2.WaterMarkConfig.WaterMarkType = this.RadioButtonList1.SelectedIndex + 1;

        ZoomLa.Components.SiteConfig obj = new ZoomLa.Components.SiteConfig();
        obj.Update(obj2);
        Response.Write("<script>alert('配置保存成功')</script>");
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        ZoomLa.Components.SiteConfigInfo obj2 = ZoomLa.Components.SiteConfig.ConfigInfo();

        obj2.WaterMarkConfig.WaterMarkTextInfo.Text = this.TextBox2.Text;
        obj2.WaterMarkConfig.WaterMarkTextInfo.FoneType = this.TextBox3.Text;
        obj2.WaterMarkConfig.WaterMarkTextInfo.FoneSize = int.Parse(this.TextBox4.Text);
        obj2.WaterMarkConfig.WaterMarkTextInfo.FoneColor = this.TextBox5.Text;
        obj2.WaterMarkConfig.WaterMarkTextInfo.FoneStyle = this.TextBox6.Text;
        obj2.WaterMarkConfig.WaterMarkTextInfo.FoneBorderColor = this.TextBox7.Text;
        obj2.WaterMarkConfig.WaterMarkTextInfo.FoneBorder = 1 - this.RadioButtonList2.SelectedIndex;
        obj2.WaterMarkConfig.WaterMarkTextInfo.WaterMarkPosition = this.RadioButtonList3.SelectedItem.Text.Trim();
        obj2.WaterMarkConfig.WaterMarkTextInfo.WaterMarkPositionX = int.Parse(this.TextBox10.Text);
        obj2.WaterMarkConfig.WaterMarkTextInfo.WaterMarkPositionY = int.Parse(this.TextBox11.Text);

        ZoomLa.Components.SiteConfig obj = new ZoomLa.Components.SiteConfig();
        obj.Update(obj2);
        Response.Write("<script>alert('配置保存成功')</script>");
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        ZoomLa.Components.SiteConfigInfo obj2 = ZoomLa.Components.SiteConfig.ConfigInfo();

        obj2.WaterMarkConfig.WaterMarkImageInfo.ImagePath = this.TextBox12.Text.Trim();
        obj2.WaterMarkConfig.WaterMarkImageInfo.WaterMarkPosition = this.RadioButtonList4.SelectedItem.Text.Trim();
        //惨痛教训：更新没有效果时，有很大可能是 load 方法的代码错误，而不是更新的代码的错误
        obj2.WaterMarkConfig.WaterMarkImageInfo.WaterMarkPositionX = int.Parse(this.TextBox14.Text.Trim());
        obj2.WaterMarkConfig.WaterMarkImageInfo.WaterMarkPositionY = int.Parse(this.TextBox15.Text.Trim());

        
        ZoomLa.Components.SiteConfig obj = new ZoomLa.Components.SiteConfig();
        obj.Update(obj2);
        Response.Write("<script>alert('配置保存成功')</script>");
    }
}
