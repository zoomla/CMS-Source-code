using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Common;

public partial class User_Exam_SelSchool : System.Web.UI.Page
{
    B_School schBll = new B_School();
    M_School schMod = new M_School();
    B_User buser = new B_User();
    B_Admin badmin = new B_Admin();
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!buser.IsTeach() && !badmin.CheckLogin()) { function.WriteErrMsg("必须教师才能访问!"); }
        if (!IsPostBack)
        {
            address_hid.Value = "北京,北京市,东城";
            MyBind();
        }

    }
    public void MyBind()
    {
        string provin = "", city = "", county = "";
        if (!string.IsNullOrEmpty(address_hid.Value))
        {
            provin = address_hid.Value.Split(',')[0];
            city = address_hid.Value.Split(',')[1];
            county = address_hid.Value.Split(',')[2];
        }
        EGV.DataSource = schBll.SelSchool(Search_T.Text.Trim(), provin, city, county);
        EGV.DataBind();
    }

    protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EGV.PageIndex = e.NewPageIndex;
        MyBind();
    }
    public string GetSchoolType(string type)
    {
        return schMod.GetScoolType(DataConverter.CLng(type));
    }
    public string GetIcon(string icon)
    {
        if (icon.Contains("/"))//判断是否为路径
        { return "<img src='" + icon + "' onerror=\"this.src = '/UploadFiles/nopic.gif'\" style='width:30px; height:30px;' />"; }
        return "<span class='" + icon + "'></span>";
    }

    protected void Search_Btn_Click(object sender, EventArgs e)
    {
        MyBind();
    }
}