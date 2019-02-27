using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using System.Data;
using ZoomLa.Model;
using ZoomLa.Common;
using ZoomLa.BLL.Exam;
/*
 * 教师批阅试卷
 * 1,是否仅显示自己学生的试卷(如何关联)
 */ 
public partial class manage_Exam_ToScore : CustomerPageAction
{
    B_Exam_Answer answerBll = new B_Exam_Answer();
    B_User buser = new B_User();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!buser.IsTeach()) { function.WriteErrMsg("当前页面只有教师才可访问"); }
        if (!IsPostBack)
        {
            MyBind();
        }
    }
    protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EGV.PageIndex = e.NewPageIndex;
        MyBind();
    }
    public void MyBind()
    {
        DataTable dt = answerBll.SelAllAnswer();
        EGV.DataSource = dt;
        EGV.DataBind();
    }
}