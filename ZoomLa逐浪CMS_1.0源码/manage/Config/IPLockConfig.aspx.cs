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
public partial class manage_Config_IPLockConfig : System.Web.UI.Page
{
    protected B_Admin badmin = new B_Admin();
    protected void Page_Load(object sender, EventArgs e)
    {
        //try
        //{
        //    this.DataList1.DataSource = new Tool.MyDataBind().bbsAll_ascx();
        //    this.DataBind();
        //}
        //catch (System.Exception ee)
        //{
        //    Response.Redirect("error.aspx?message=" + ee.Message);
        //    this.Response.Redirect("../Dxsdv/False.aspx?message=" + ee.Message);
        //}
        this.badmin.CheckMulitLogin();
        if (!badmin.ChkPermissions("SiteConfig"))
        {
            function.WriteErrMsg("没有权限进行此项操作");
        }
        if (!this.IsPostBack)
        {
            try
            {
            //<asp:RadioButton ID="RadioButton1" runat="server" GroupName="LockIPType" TabIndex="1" Checked="True"/>不启用来访限定功能，任何IP都可以访问本站。<br />

            string i = ZoomLa.Components.SiteConfig.IPLockConfig.LockIPType;
            if (i.Equals("0")) { this.RadioButton1.Checked = true; }
            if (i.Equals("1")) { this.RadioButton2.Checked = true; }
            if (i.Equals("2")) { this.RadioButton3.Checked = true; }
            if (i.Equals("3")) { this.RadioButton4.Checked = true; }
            if (i.Equals("4")) { this.RadioButton5.Checked = true; }

            this.TextBox2.Text = ZoomLa.Components.SiteConfig.IPLockConfig.LockIPWhite;
            this.TextBox3.Text = ZoomLa.Components.SiteConfig.IPLockConfig.LockIPBlack;

            string j = ZoomLa.Components.SiteConfig.IPLockConfig.AdminLockIPType;//教训：组名必须注意要不同
            if (j.Equals("0")) { this.RadioButton6.Checked = true; }
            if (j.Equals("1")) { this.RadioButton7.Checked = true; }
            if (j.Equals("2")) { this.RadioButton8.Checked = true; }
            if (j.Equals("3")) { this.RadioButton9.Checked = true; }
            if (j.Equals("4")) { this.RadioButton10.Checked = true; }

            this.TextBox5.Text = ZoomLa.Components.SiteConfig.IPLockConfig.AdminLockIPWhite;
            this.TextBox6.Text = ZoomLa.Components.SiteConfig.IPLockConfig.AdminLockIPBlack;
            }
            catch (System.Exception ee)
            {
                //Response.Redirect("error.aspx?message=" + ee.Message);
                this.Response.Redirect("False.aspx?message=" + ee.Message);
            }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        ZoomLa.Components.SiteConfigInfo obj2 = ZoomLa.Components.SiteConfig.ConfigInfo();

        if (this.RadioButton1.Checked.Equals(true)) { obj2.IPLockConfig.LockIPType = "0"; }
        if (this.RadioButton2.Checked.Equals(true)) { obj2.IPLockConfig.LockIPType = "1"; }
        if (this.RadioButton3.Checked.Equals(true)) { obj2.IPLockConfig.LockIPType = "2"; }
        if (this.RadioButton4.Checked.Equals(true)) { obj2.IPLockConfig.LockIPType = "3"; }
        if (this.RadioButton5.Checked.Equals(true)) { obj2.IPLockConfig.LockIPType = "4"; }

        obj2.IPLockConfig.LockIPWhite = this.TextBox2.Text;
        obj2.IPLockConfig.LockIPBlack = this.TextBox3.Text;

        if (this.RadioButton6.Checked.Equals(true)) { obj2.IPLockConfig.AdminLockIPType = "0"; }
        if (this.RadioButton7.Checked.Equals(true)) { obj2.IPLockConfig.AdminLockIPType = "1"; }
        if (this.RadioButton8.Checked.Equals(true)) { obj2.IPLockConfig.AdminLockIPType = "2"; }
        if (this.RadioButton9.Checked.Equals(true)) { obj2.IPLockConfig.AdminLockIPType = "3"; }
        if (this.RadioButton10.Checked.Equals(true)) { obj2.IPLockConfig.AdminLockIPType = "4"; }

        obj2.IPLockConfig.AdminLockIPWhite = this.TextBox5.Text;
        obj2.IPLockConfig.AdminLockIPBlack = this.TextBox6.Text;

        ZoomLa.Components.SiteConfig obj = new ZoomLa.Components.SiteConfig();
        obj.Update(obj2);
        Response.Write("<script>alert('配置保存成功')</script>");
    }
}
