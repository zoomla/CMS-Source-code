using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;


namespace ZoomLaCMS.Manage.Zone
{
    public partial class SchoolShow :CustomerPageAction
    {
        B_School schBll = new B_School();
        public int SchoolID { get { return DataConverter.CLng(Request.QueryString["id"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyBind();
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='" + customPath2 + "User/UserManage.aspx'>会员管理</a></li><li><a href='ZoneManage.aspx'>会员空间管理</a></li><li><a href='SnsSchool.aspx'>学校信息</a></li><li class='active'>学校浏览</li>");
            }
        }
        public void MyBind()
        {
            M_School schmod = schBll.SelReturnModel(SchoolID);
            SchoolName_L.Text = schmod.SchoolName;
            SchoolAddress_L.Text = schmod.Province + " " + schmod.City + " " + schmod.County;
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
            if (icon.Contains("/") || string.IsNullOrEmpty(icon))//判断是否为路径
            { return "<a href='" + icon + "' target='_bank'><img src='" + icon + "' onerror=\"shownopic(this);\" style='width:30px; height:30px;' /></a>"; }
            return "<span class='" + icon + "'></span>";
        }
    }
}