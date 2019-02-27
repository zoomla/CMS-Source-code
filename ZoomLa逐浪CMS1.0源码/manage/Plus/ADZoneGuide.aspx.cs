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

public partial class manage_Plus_ADZoneGuide : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string condition = "";
        if (this.DropDownList1.SelectedItem.Text.Equals("版位名称"))
        {
            condition = "where ZoneName like ' %" + this.DropDownList1.SelectedItem.Text + "%'";
        }
        else { condition = " where ZoneIntro like '%" + this.DropDownList1.SelectedItem.Text + "%'"; }
        Response.Redirect("<script>window.open('ADZoneManage.aspx?condition=" + condition + "','main_right');</script>");
    }
}
