using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Common;
using System.Data;

public partial class User_Exam_StudentManage : System.Web.UI.Page
{
    B_Student stuBll = new B_Student();
    B_ClassRoom classBll = new B_ClassRoom();
    B_User buser = new B_User();
    public int ClassID { get { return DataConverter.CLng(Request.QueryString["cid"]); } }
    public int StuType { get { return DataConverter.CLng(Request.QueryString["stutype"]); } }

    public int Status { get { return string.IsNullOrEmpty(Request.QueryString["status"])? -1 : DataConverter.CLng(Request.QueryString["status"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
        }
    }
    public void MyBind()
    {
        M_ClassRoom classMod = classBll.SelReturnModel(ClassID);
        if (classMod.CreateUser == buser.GetLogin().UserID)//只有该班级创建者才有审核等操作
        {
            Option_Div.Visible = true;
        }
        DataTable dt = stuBll.SelByURid(ClassID, Status, StuType);
        EGV.DataSource = dt;
        EGV.DataBind();
        function.Script(this, "InitTab(" + Status + ");");
    }
    //获得成员角色
    public string GetStuType()
    {
        int stutype = DataConverter.CLng(Eval("StudentType"));
        switch (stutype)
        {
            case 1:
                return "学生";
            case 2:
                return "教师";
            case 3:
                return "家长";
            default:
                return "管理员";
        }
    }
    protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EGV.PageIndex = e.NewPageIndex;
        MyBind();
    }
    public string GetStatus()
    {
        switch (DataConverter.CLng(Eval("Auditing")))
        {
            case -1:
                return "<span style='color:gray'>无需审核</span>";
            case 0:
                return "<span style='color:red'>未审核</span>";
            case 1:
                return "<span style='color:green'>已审核</span>";
            default:
                return "";
        }
    }
    protected void Del_Btn_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.Form["idchk"]))
        {
            stuBll.DelByIDS(Request.Form["idchk"]);
        }
        MyBind();
    }

    protected void Auit_Btn_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.Form["idchk"]))
        {
            stuBll.AuditStatus(Request.Form["idchk"], true);
        }
        MyBind();
    }

    protected void UnAuit_Btn_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.Form["idchk"]))
        {
            stuBll.AuditStatus(Request.Form["idchk"], false);
        }
        MyBind();
    }


}