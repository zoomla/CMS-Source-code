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
using ZoomLa.Model;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Components;

public partial class manage_Zone_AddClassRoom : CustomerPageAction
{
    B_ClassRoom classBll = new B_ClassRoom();
    B_School sll = new B_School();
    B_Admin badmin = new B_Admin();
    B_User buser = new B_User();
    B_GradeOption GradeBll = new B_GradeOption();
    B_Student stuBll = new B_Student();
    public int ClassID { get { return DataConverter.CLng(Request.QueryString["id"]); } }
    public int SchoolID { get { return DataConverter.CLng(Request.QueryString["sid"]); } }

    protected void Page_Load(object sender, EventArgs e)
    {
        ZoomLa.Common.function.AccessRulo();
        badmin.CheckIsLogin();
        if (!IsPostBack)
        {
            GradeBind();
            if (ClassID>0)
            {
                M_ClassRoom cmdel = classBll.GetSelect(ClassID);
                txtRoomName.Text = cmdel.RoomName;
                txtRoomID.Value = cmdel.RoomID.ToString();
                txtSchoolID.Value = cmdel.SchoolID.ToString();
                //txtTeacher.Text = cmdel.Teacher;
                txtIsTrue.Checked = cmdel.IsTrue == 1 ? true : false;
                GradeList_Drop.SelectedValue = cmdel.Grade.ToString();
                Manager_Hid.Value = cmdel.CreateUser.ToString();
                Manager_T.Text = buser.SelReturnModel(cmdel.CreateUser).UserName;
                ClassNum_T.Text = cmdel.Integral.ToString();
                //this.txtCreateUser.Text = cmdel.CreateUser.ToString();
                star_hid.Value = cmdel.ClassStar.ToString();
                IsDone_Check.Checked = cmdel.IsDone == 1;
                ClassIcon_T.Text = cmdel.Monitor;
                txtClassinfo.Text = cmdel.Classinfo.ToString();
                if (cmdel.SchoolID > 0)
                {
                    SchoolName_T.Text = sll.SelReturnModel(cmdel.SchoolID).SchoolName;
                }
                //txtAdviser.Text = cmdel.Adviser.ToString();
                Button1.Text = "修改信息";
            }
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='../Exam/Papers_System_Manage.aspx'>教育模块</a></li><li><a href='SnsClassRoom.aspx'>班级管理</a></li><li class='active'>添加班级</li>");
        }
    }
   
    public void GradeBind()
    {
        GradeList_Drop.DataSource = B_GradeOption.GetGradeList(6, 0);
        GradeList_Drop.DataBind();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        M_ClassRoom cinfo = new M_ClassRoom();
        if (ClassID > 0) { cinfo = classBll.SelReturnModel(ClassID); }
        int olderuser = cinfo.CreateUser;//记录原教师id
        cinfo.RoomID = DataConverter.CLng(txtRoomID.Value);
        //cinfo.Adviser = txtAdviser.Text.Trim();
        cinfo.Classinfo = txtClassinfo.Text.Trim();
        M_UserInfo mu = buser.SelReturnModel(DataConverter.CLng(Manager_Hid.Value));
        cinfo.CreateUser = DataConverter.CLng(Manager_Hid.Value);
        cinfo.Creation = DateTime.Now;
        cinfo.Grade = DataConverter.CLng(GradeList_Drop.SelectedValue);
        cinfo.Integral = DataConverter.CLng(ClassNum_T.Text);
        cinfo.IsTrue = txtIsTrue.Checked ? 1 : 0;
        M_AdminInfo admininfo = badmin.GetAdminLogin();
        string saveurl = SiteConfig.SiteOption.UploadDir + "Exam/" + admininfo.AdminName+admininfo.AdminId + "/";
        cinfo.Monitor = ClassIcon_T.Text;
        cinfo.ClassStar = DataConverter.CLng(star_hid.Value);
        cinfo.IsDone = IsDone_Check.Checked ? 1 : 0;
        cinfo.RoomName = txtRoomName.Text.Trim();
        DataTable tempschools = sll.SelByName(SchoolName_T.Text.Trim());
        //添加或选择学校操作
        if (string.IsNullOrEmpty(SchoolName_T.Text)) { cinfo.SchoolID = 0; }
        else if (tempschools.Rows.Count > 0) { cinfo.SchoolID = DataConverter.CLng(tempschools.Rows[0]["ID"]); }//选择学校
        else { cinfo.SchoolID = InsertSchool(SchoolName_T.Text.Trim()); }//添加学校
        //cinfo.Teacher = txtTeacher.Text.Trim();
        if (ClassID>0)
        {
            if (!olderuser.ToString().Equals(Manager_Hid.Value))//更改了班主任
            {
                ChangeTearch(olderuser, mu, ClassID);
            }
            classBll.GetUpdate(cinfo);
            function.WriteSuccessMsg("修改成功!","ClassShow.aspx?cid="+ClassID);
        }
        int classid = classBll.GetInsert(cinfo);
        InsertTearcher(mu, classid);
        function.WriteSuccessMsg("添加成功!", "ClassShow.aspx?cid=" + classid);
        
    }
    //添加班级时,创建者也是教师成员
    private void InsertTearcher(M_UserInfo mu, int roomid)
    {
        M_Student stuMod = new M_Student();
        stuMod.Addtime = DateTime.Now;
        stuMod.UserID = mu.UserID;
        stuMod.UserName = mu.UserName;
        stuMod.StudentType = 2;
        stuMod.Auditing = -1;
        stuMod.AuditingContext = "班主任";
        stuMod.RoomID = roomid;
        stuBll.insert(stuMod);
    }
    private int InsertSchool(string name)
    {
        M_School schoolmod = new M_School();
        schoolmod.SchoolName = name;
        schoolmod.SchoolType = 1;
        schoolmod.Visage = 1;
        schoolmod.Addtime = DateTime.Now;
        schoolmod.SchoolInfo = "自定义学校";
        return sll.insert(schoolmod);
    }
    //更改班主任时，该班级教师成员随之变化
    private void ChangeTearch(int oldeuser,M_UserInfo newuser,int roomid)
    {
        DataTable dt= stuBll.SelByURid(roomid, -1, 2,oldeuser);
        stuBll.UpDateStatus(dt.Rows[0]["Noteid"].ToString(), 1);//将原教师成员的状态改为已审核
        dt= stuBll.SelByURid(roomid, -1, 2, newuser.UserID);
        if (dt.Rows.Count > 0)//如果新任教师已存在该班级，将其状态改为无需审核状态
        {
            stuBll.UpDateStatus(dt.Rows[0]["Noteid"].ToString(), -1);
        }
        else//如果不存在则将该教师加入该班级成员
        {
            InsertTearcher(newuser, roomid);
        }

    }

}
