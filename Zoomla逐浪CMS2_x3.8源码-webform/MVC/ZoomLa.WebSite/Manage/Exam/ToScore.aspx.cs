namespace ZoomLaCMS.Manage.Exam
{
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

    public partial class ToScore : System.Web.UI.Page
    {
        B_Exam_Answer answerBll = new B_Exam_Answer();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyBind();
                Call.SetBreadCrumb(Master, "<li><a href='Papers_System_Manage.aspx'>教育模块</a></li><li><a href='ToScore.aspx'>阅卷中心</a></li><li>评阅试卷</li>" + Call.GetHelp(79));
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
}