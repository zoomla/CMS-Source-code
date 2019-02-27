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
using ZoomLa.Components;
using ZoomLa.Model;
using ZoomLa.Common;

public partial class User_UserZone_School_AddRoom : System.Web.UI.Page
{
    B_ClassRoom cll = new B_ClassRoom();
    B_School sll = new B_School();
    B_User bu = new B_User();
    B_Student bst = new B_Student();
    public string labe="创建班级信息";
    protected string SchoolName;
    protected int SchoolID
    {
        get
        {
            if (ViewState["SchoolID"] != null)
                return int.Parse(ViewState["SchoolID"].ToString());
            else
                return 0;
        }
        set { ViewState["SchoolID"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        bu.CheckIsLogin();
        if (!IsPostBack)
        {
            if (Request.QueryString["Sid"] != null)
            {
                SchoolID = DataConverter.CLng(Request.QueryString["Sid"]);
                M_School ms = sll.GetSelect(SchoolID);
                SchoolName = ms.SchoolName; 
                this.lblSchool.Text = ms.SchoolName;
                DDLBind(ms.SchoolType);
                for (int i = 1949; i <= DateTime.Now.Year; i++)
                {
                    ddlYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                ddlYear.SelectedValue = "2008";
                string menu = Request.QueryString["menu"];
                int id = DataConverter.CLng(Request.QueryString["id"]);
                if (menu == "edit")
                {
                    labe = "修改班级信息";
                    M_ClassRoom cmdel = cll.GetSelect(id);
                    this.txtRoomName.Text = cmdel.RoomName;
                    //this.txtRoomID.Value = cmdel.RoomID.ToString();
                    this.lblSchool.Text = sll.GetSelect(cmdel.SchoolID).SchoolName;
                    this.ddlClass.SelectedItem.Text = cmdel.ClassName;
                    this.txtRoom.Text = cmdel.Room;
                    //this.txtSchoolName.Text = sll.GetSelect(cmdel.SchoolID).SchoolName;
                    this.txtTeacherName.Text = cmdel.Teacher;
                    //this.txtIsTrue.Text = cmdel.IsTrue.ToString();
                    ddlYear.SelectedValue = cmdel.Grade.ToString();
                    //this.txtCreateUser.Text = cmdel.CreateUser.ToString();
                    this.txtMonitorName.Text = cmdel.Monitor.ToString();
                    this.txtClassInfo.Text = cmdel.Classinfo.ToString();
                    //this.txtAdviser.Text = cmdel.Adviser.ToString();
                    this.Button1.Text = "修改信息";
                }
            }
        }
    }
    private void DDLBind(int i)
    {
        switch (i)
        {
            case 1:
                ddlClass.Items.Add(new ListItem("小学一年"));
                ddlClass.Items.Add(new ListItem("小学二年"));
                ddlClass.Items.Add(new ListItem("小学三年"));
                ddlClass.Items.Add(new ListItem("小学四年"));
                ddlClass.Items.Add(new ListItem("小学五年"));
                ddlClass.Items.Add(new ListItem("小学六年"));
                break;
            case 2:
                ddlClass.Items.Add(new ListItem("初中一年"));
                ddlClass.Items.Add(new ListItem("初中二年"));
                ddlClass.Items.Add(new ListItem("初中三年"));
                ddlClass.Items.Add(new ListItem("高中一年"));
                ddlClass.Items.Add(new ListItem("高中二年"));
                ddlClass.Items.Add(new ListItem("高中三年"));
                break;
            case 3:
                ddlClass.Items.Add(new ListItem("大学一年"));
                ddlClass.Items.Add(new ListItem("大学二年"));
                ddlClass.Items.Add(new ListItem("大学三年"));
                ddlClass.Items.Add(new ListItem("大学四年"));
                break;
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        M_ClassRoom cinfo = new M_ClassRoom();
        cinfo.RoomID = DataConverter.CLng(Request.QueryString["id"]);
        cinfo.Classinfo = txtClassInfo.Text ;
        cinfo.CreateUser =bu.GetLogin().UserID;
        cinfo.Creation = DateTime.Now;
        cinfo.Grade = int.Parse(ddlYear.SelectedValue);
        cinfo.Integral = 0;
        cinfo.IsTrue = 0;
        cinfo.Monitor = txtMonitorName.Text ;
        cinfo.RoomName = txtRoomName.Text ;
        cinfo.SchoolID = DataConverter.CLng(Request.QueryString["Sid"]);
        cinfo.Teacher = this.txtTeacherName.Text ;
        cinfo.ClassName = this.ddlClass.Text;
        cinfo.Room = this.txtRoom.Text;
        int rid=cll.GetInsert(cinfo);
        if (cinfo.RoomID == 0)
        {
            M_Student ms = new M_Student();
            ms.RoomID = rid;
            ms.SchoolID = DataConverter.CLng(Request.QueryString["Sid"] );
            ms.UserID = bu.GetLogin().UserID;
            ms.UserName = bu.GetLogin().UserName;
            ms.StatusType = int.Parse(rdlStatusType.SelectedValue);
            if(ms.StatusType==1)
            {
                ms.StudentType = 1;
                ms.StudentTypeTitle = "学生";
            }
            ms.Auditing = 1;
            ms.Addtime = DateTime.Now;
            bst.GetInsert(ms);
            Response.Write("<script language=javascript>alert('添加成功!请等待管理员审核!');location.href='MySchoolList.aspx';</script>");
        }
        //else
        //{
        //    Response.Write("<script language=javascript>alert('修改成功!');location.href='SnsClassRoom.aspx';</script>");
        //}
    }
}
