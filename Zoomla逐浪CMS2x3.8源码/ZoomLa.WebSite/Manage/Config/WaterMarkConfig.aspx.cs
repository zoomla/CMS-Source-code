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
using ZoomLa.Model;
using ZoomLa.Components;

public partial class manage_Config_WaterMarkConfig : CustomerPageAction
{
    protected B_Admin badmin = new B_Admin();
    protected void Page_Load(object sender, EventArgs e)
    {
        function.AccessRulo(); 
        badmin.CheckIsLogin();

        if (!B_ARoleAuth.Check(ZLEnum.Auth.other, "SiteConfig"))
        {
            function.WriteErrMsg("没有权限进行此项操作");
        }
        if (!this.IsPostBack)
        {
            string s = "";
            // WaterMarkConfig 类的2个属性分别又是一个类
            this.RadioButtonList1.SelectedIndex = SiteConfig.WaterMarkConfig.WaterMarkType - 1;//对应的cs文件是WaterMarkText,不是WaterMarkTextInfo    //对应的名称不对,只有WaterMarkConfig

            this.TextBox2.Text = SiteConfig.WaterMarkConfig.WaterMarkTextInfo.Text;
            this.TextBox3.Text = SiteConfig.WaterMarkConfig.WaterMarkTextInfo.FoneType;
            this.TextBox4.Text = SiteConfig.WaterMarkConfig.WaterMarkTextInfo.FoneSize.ToString();
            this.TextBox5.Text = SiteConfig.WaterMarkConfig.WaterMarkTextInfo.FoneColor;
            this.TextBox6.Text = SiteConfig.WaterMarkConfig.WaterMarkTextInfo.FoneStyle;
            this.TextBox7.Text = SiteConfig.WaterMarkConfig.WaterMarkTextInfo.FoneBorderColor;
            this.RadioButtonList2.SelectedIndex = 1 - SiteConfig.WaterMarkConfig.WaterMarkTextInfo.FoneBorder;
            s = SiteConfig.WaterMarkConfig.WaterMarkTextInfo.WaterMarkPosition;
            if (s.Trim().Equals("WM_TOP_LEFT")) { this.RadioButtonList3.SelectedIndex = 0; }
            if (s.Trim().Equals("WM_TOP_CENTER")) { this.RadioButtonList3.SelectedIndex = 1; }
            if (s.Trim().Equals("WM_TOP_RIGHT")) { this.RadioButtonList3.SelectedIndex = 2; }
            this.TextBox10.Text = SiteConfig.WaterMarkConfig.WaterMarkTextInfo.WaterMarkPositionX.ToString();
            this.TextBox11.Text = SiteConfig.WaterMarkConfig.WaterMarkTextInfo.WaterMarkPositionY.ToString();//对应的cs文件是WaterMarkText,不是WaterMarkTextInfo    //对应的名称不对,只有WaterMarkConfig


            this.TextBox12.Text = SiteConfig.WaterMarkConfig.WaterMarkImageInfo.ImagePath;
            s = SiteConfig.WaterMarkConfig.WaterMarkImageInfo.WaterMarkPosition;
            if (s.Trim().Equals("WM_TOP_LEFT")) { this.RadioButtonList4.SelectedIndex = 0; }
            if (s.Trim().Equals("WM_TOP_CENTER")) { this.RadioButtonList4.SelectedIndex = 1; }
            if (s.Trim().Equals("WM_TOP_RIGHT")) { this.RadioButtonList4.SelectedIndex = 2; }
            this.TextBox14.Text = SiteConfig.WaterMarkConfig.WaterMarkImageInfo.WaterMarkPositionX.ToString();
            this.TextBox15.Text = SiteConfig.WaterMarkConfig.WaterMarkImageInfo.WaterMarkPositionY.ToString();
        } 
    }
    
    protected void Button1_Click1(object sender, EventArgs e)
    {
        SiteConfig.WaterMarkConfig.WaterMarkType = this.RadioButtonList1.SelectedIndex + 1;
        SiteConfig.Update();
        function.WriteSuccessMsg("配置保存成功");
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        SiteConfig.WaterMarkConfig.WaterMarkTextInfo.Text = this.TextBox2.Text;
        SiteConfig.WaterMarkConfig.WaterMarkTextInfo.FoneType = this.TextBox3.Text;
        SiteConfig.WaterMarkConfig.WaterMarkTextInfo.FoneSize = int.Parse(this.TextBox4.Text);
        SiteConfig.WaterMarkConfig.WaterMarkTextInfo.FoneColor = this.TextBox5.Text;
        SiteConfig.WaterMarkConfig.WaterMarkTextInfo.FoneStyle = this.TextBox6.Text;
        SiteConfig.WaterMarkConfig.WaterMarkTextInfo.FoneBorderColor = this.TextBox7.Text;
        SiteConfig.WaterMarkConfig.WaterMarkTextInfo.FoneBorder = 1 - this.RadioButtonList2.SelectedIndex;
        SiteConfig.WaterMarkConfig.WaterMarkTextInfo.WaterMarkPosition = this.RadioButtonList3.SelectedItem.Text.Trim();
        SiteConfig.WaterMarkConfig.WaterMarkTextInfo.WaterMarkPositionX = int.Parse(this.TextBox10.Text);
        SiteConfig.WaterMarkConfig.WaterMarkTextInfo.WaterMarkPositionY = int.Parse(this.TextBox11.Text);
        SiteConfig.Update();
        function.WriteSuccessMsg("配置保存成功");
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        SiteConfig.WaterMarkConfig.WaterMarkImageInfo.ImagePath = this.TextBox12.Text.Trim();
        SiteConfig.WaterMarkConfig.WaterMarkImageInfo.WaterMarkPosition = this.RadioButtonList4.SelectedItem.Text.Trim();
        SiteConfig.WaterMarkConfig.WaterMarkImageInfo.WaterMarkPositionX = int.Parse(this.TextBox14.Text.Trim());
        SiteConfig.WaterMarkConfig.WaterMarkImageInfo.WaterMarkPositionY = int.Parse(this.TextBox15.Text.Trim());
        SiteConfig.Update();
        function.WriteSuccessMsg("配置保存成功");
    }
}
