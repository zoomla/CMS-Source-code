using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Helper;
using ZoomLa.Common;

public partial class Manage_Exam_Setting : System.Web.UI.Page
{
    B_Exam_Sys_Questions questBll = new B_Exam_Sys_Questions();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li> <li><a href='Papers_System_Manage.aspx'>教育模块</a></li> <li><a href='QuestionManage.aspx'>考试管理</a></li><li class='active'>配置中心</li>");
        }
    }
    private void MyBind()
    {
        string stime = "", etime = "";
        DateHelper.GetWeekSE(DateTime.Now.AddDays(-7), ref stime, ref etime);
        STime_T.Text = stime; ETime_T.Text = etime;
    }
    protected void UpdateDiff_Btn_Click(object sender, EventArgs e)
    {
        questBll.CountDiffcult(STime_T.Text,ETime_T.Text);
        function.WriteSuccessMsg("难度更新成功");
    }
}