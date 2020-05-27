using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;

public partial class Manage_Zone_ClassShow : CustomerPageAction
{
    B_ClassRoom classBll = new B_ClassRoom();
    B_School schBll = new B_School();
    B_User buser = new B_User();
    public int ClassID { get { return DataConverter.CLng(Request.QueryString["cid"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='" + customPath2 + "User/UserManage.aspx'>会员管理</a></li><li><a href='ZoneManage.aspx'>会员空间管理</a></li><li><a href='SnsSchool.aspx'>学校信息配置</a></li><li><a href='SnsClassRoom.aspx'>班级管理</a></li><li class='active'>班级浏览</li>");
        }
    }
    public void MyBind()
    {
        M_ClassRoom classmod = classBll.SelReturnModel(ClassID);
        ClassName_L.Text = classmod.RoomName;
        ClassIcon_L.Text = GetIcon(classmod.Monitor);
        if (classmod.SchoolID > 0)
        {
            SchoolName_L.Text = "<a href='SchoolShow.aspx?id=" + classmod.SchoolID + "'>" + schBll.SelReturnModel(classmod.SchoolID).SchoolName + "</a>";
        }
        Grade_L.Text = B_GradeOption.GetGradeOption(classmod.Grade).GradeName;
        CreateUser_L.Text = buser.SelReturnModel(classmod.CreateUser).UserName;
        IsDonw_L.Text = classmod.IsDone == 1 ? "<span class='fa fa-check'></span>" : "<span class='fa fa-remove'></span>";
        CreateTime_L.Text = classmod.Creation.ToString();
        ClassInfo_L.Text = classmod.Classinfo;
        Star_Li.Text = GetStar(classmod.ClassStar);
    }
    //评星
    public string GetStar(int star)
    {
        string result = "";
        int score = star;
        for (int i = 0; i < score; i++)
        {
            result += "<i class='staricon fa fa-star'></i>";
        }
        for (int i = 0; i < 5 - score; i++)
        {
            result += "<i class='staricon fa fa-star-o'></i>";
        }
        return result;
    }
    public string GetIcon(string classicon)
    {
        classicon = string.IsNullOrEmpty(classicon) ? "/UploadFiles/nopic.gif" : classicon;
        if (classicon.Contains("/")||string.IsNullOrEmpty(classicon))//判断是否为路径
        { return "<img src='" + classicon + "' onerror=\"shownopic(this);\" style='width:30px; height:30px;' />"; }
        return "<span class='" + classicon + "'></span>";
    }
}