using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;
using System.Data;

public partial class manage_Question_AddClass : CustomerPageAction
{
    B_ClassRoom classBll = new B_ClassRoom();
    B_School sll = new B_School();
    B_User buser = new B_User();
    B_Student stuBll = new B_Student();
    public int RoomID { get { return DataConverter.CLng(Request.QueryString["cid"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!buser.IsTeach())
        //{
        //    function.WriteErrMsg("必须是教师才能访问!");
        //}
        if (!IsPostBack)
        {
            GradeBind();
            if (RoomID>0)
            {
                M_ClassRoom cmdel = classBll.GetSelect(RoomID);
                ClassName_T.Text = cmdel.RoomName;
                //txtTeacher.Text = cmdel.Teacher;
                GradeList_Drop.SelectedValue = cmdel.Grade.ToString();
                //this.txtCreateUser.Text = cmdel.CreateUser.ToString();
                IsDone_Check.Checked = cmdel.IsDone == 1;
                ClassIcon_T.Text = cmdel.Monitor;
                ClassDeailt_T.Text = cmdel.Classinfo.ToString();
                ClassNum_T.Text = cmdel.Integral.ToString();
                if (cmdel.SchoolID > 0)
                {
                    SchoolName_T.Text = sll.SelReturnModel(cmdel.SchoolID).SchoolName;
                    SchoolName_Hid.Value = SchoolName_T.Text;
                }
                //txt_Endtime.Text = cinfo.Endtime.ToString();
                this.EBtnSubmit.Text = "修改班级";
            }
        }
    }

   
    protected void EBtnSubmit_Click(object sender, EventArgs e)
    {
        M_ClassRoom cinfo = RoomID>0?classBll.SelReturnModel(RoomID):new M_ClassRoom();
        M_UserInfo mu = buser.GetLogin();
        //exinfo.Actualnumber =DataConverter.CLng(this.txt_Actualnumber.Text);
        //exinfo.Endtime = DataConverter.CDate(txt_Endtime.Text);
        //cinfo.Adviser = txtAdviser.Text.Trim();
        cinfo.Classinfo = ClassDeailt_T.Text.Trim();
        cinfo.CreateUser = mu.UserID;
        cinfo.Creation = DateTime.Now;
        cinfo.Grade = DataConverter.CLng(GradeList_Drop.SelectedValue);
        cinfo.Integral =DataConverter.CLng(ClassNum_T.Text);
        string saveurl = SiteConfig.SiteOption.UploadDir + "Exam/" + mu.UserName+mu.UserID + "/";
        cinfo.Monitor = ClassIcon_T.Text;
        //cinfo.Monitor = SFile_Up.SaveFile();
        cinfo.IsDone = IsDone_Check.Checked ? 1 : 0;
        cinfo.RoomName = ClassName_T.Text.Trim();
        //选择学校
        M_School schMod = sll.SelModelByName(SchoolName_Hid.Value);
        if (schMod == null) { cinfo.SchoolID = 0; }
        else { cinfo.SchoolID = schMod.ID; }
        if (RoomID>0)
        {
            classBll.UpdateByID(cinfo);
            function.WriteSuccessMsg("修改成功!","ClassShow.aspx?cid="+RoomID);
        }
        else
        {
            int classid = classBll.insert(cinfo);
            InsertTearcher(mu, classid);
            function.WriteSuccessMsg("添加成功,请等待管理员审核!", "ClassShow.aspx?cid="+ classid);
        }
    }
    //添加班级时,创建者也是教师成员
    private void InsertTearcher(M_UserInfo mu,int roomid)
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
    public void GradeBind()
    {
        GradeList_Drop.DataSource = B_GradeOption.GetGradeList(6, 0);
        GradeList_Drop.DataBind();
    }
}