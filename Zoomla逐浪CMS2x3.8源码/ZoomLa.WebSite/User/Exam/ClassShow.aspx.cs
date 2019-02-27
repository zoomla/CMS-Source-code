using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Common;
using ZoomLa.BLL;
using ZoomLa.Model;
using System.Data;

public partial class User_Exam_ClassShow : System.Web.UI.Page
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
        if (classmod.CreateUser == buser.GetLogin().UserID)//当前用户是否是班级创建者
        {
            Edit_A.HRef = "AddClass.aspx?cid="+ClassID;
            Edit_A.Visible = true;
        }
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
        { return "<img src='" + classicon + "' onerror=\"this.src = '/UploadFiles/nopic.gif'\" style='width:30px; height:30px;' />"; }
        return "<span class='" + classicon + "'></span>";
    }
}