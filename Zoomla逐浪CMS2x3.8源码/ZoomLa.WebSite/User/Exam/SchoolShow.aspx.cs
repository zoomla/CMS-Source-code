using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Common;

public partial class User_Exam_SchoolShow : System.Web.UI.Page
{
    B_School schBll = new B_School();
    public int SchoolID { get { return DataConverter.CLng(Request.QueryString["id"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
        }
    }
    public void MyBind()
    {
        M_School schmod = schBll.SelReturnModel(SchoolID);
        SchoolName_L.Text = schmod.SchoolName;
        SchoolAddress_L.Text = schmod.Province;
        SchoolType_L.Text = GetSchoolType(schmod.SchoolType);
        SchoolDis_L.Text = schmod.Visage == 1 ? "公办" : "私立";
        SchoolIcon_L.Text = GetIcon(schmod.Country);
        SchoolInfo_L.Text = schmod.SchoolInfo;
    }
    public string GetSchoolType(int type)
    {
        switch (type)
        {
            case 1:
                return "小学";
            case 2:
                return "中学";
            case 3:
                return "大学";
            default:
                return "其它";
        }
    }
    public string GetIcon(string icon)
    {
        icon = string.IsNullOrEmpty(icon) ? "/UploadFiles/nopic.gif" : icon;
        if (icon.Contains("/")||string.IsNullOrEmpty(icon))//判断是否为路径
        { return "<a href='"+icon+"' target='_bank'><img src='" + icon + "' onerror=\"this.src = '/UploadFiles/nopic.gif'\" style='width:30px; height:30px;' /></a>"; }
        return "<span class='" + icon + "'></span>";
    }

}