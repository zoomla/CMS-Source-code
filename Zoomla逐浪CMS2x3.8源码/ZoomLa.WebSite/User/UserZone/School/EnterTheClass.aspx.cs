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
using ZoomLa.Model;

public partial class User_UserZone_School_EnterTheClass : System.Web.UI.Page
{
    B_ClassRoom bcr = new B_ClassRoom();
    B_User ubll = new B_User();
    B_School bs = new B_School();
    B_Student bst = new B_Student();
    protected void Page_Load(object sender, EventArgs e)
    {
        ubll.CheckIsLogin();
        M_ClassRoom r=bcr.GetSelect(int.Parse(Request.QueryString["rID"].ToString()));
        //学校名称
        tdSchool.InnerHtml = bs.GetSelect(r.SchoolID).SchoolName;
        //班级名称
        tdClass.InnerHtml = r.RoomName;
        //老师
        tdTeacher.InnerHtml = r.Teacher;
        //班级副管理员
        tdAdviser.InnerHtml = r.Adviser;
        //班级介绍
        tdClassinfo.InnerHtml = r.Classinfo;
        //管理员（创建人）
        tdCreateUser.InnerHtml = ubll.GetUserByUserID(r.CreateUser).UserName;
        //创建时间
        tdCreation.InnerHtml = r.Creation.ToString();
        //班长
        tdMonitor.InnerHtml = r.Monitor;
        
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        ubll.CheckIsLogin();
        M_ClassRoom r = bcr.GetSelect(int.Parse(Request.QueryString["rID"].ToString()));
        if (bst.SelByURid(r.RoomID,0,ubll.GetLogin().UserID).Rows.Count <= 0)
        {
            M_Student ms = new M_Student();
            ms.RoomID = r.RoomID;
            ms.SchoolID = r.SchoolID;
            ms.UserID = ubll.GetLogin().UserID;
            ms.UserName = ubll.GetLogin().UserName;
            ms.StatusType = int.Parse(rdlStatusType.SelectedValue);
            if (ms.StatusType==1)
            {
                ms.StudentType = 1;
                ms.StudentTypeTitle = "学生";
            }
            ms.Auditing = 0 ;
            ms.Addtime = DateTime.Now;
            ms.AuditingContext = txtContext.Text;
            bst.GetInsert(ms);
            Page.ClientScript.RegisterStartupScript(typeof(string), "TabJs", "<script language='javascript'>window.returnVal='undefined';window.close();</script>");
        }
    }
}
