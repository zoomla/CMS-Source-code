using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.BLL;
using ZoomLa.Model;
using System.Data;


namespace ZoomLaCMS.Manage.Exam
{
    public partial class Add_Exampoint : System.Web.UI.Page
    {
        private B_ExamPoint bep = new B_ExamPoint();
        protected B_Admin badmin = new B_Admin();
        public int AdminID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            B_ARoleAuth.CheckEx(ZLEnum.Auth.content, "ContentMange");
            if (!IsPostBack)
            {
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li> <li><a href='Papers_System_Manage.aspx'>教育模块</a></li> <li><a href='QuestionManage.aspx'>考试管理</a></li><li>添加考点</li>");
            }
            //Bind();
        }
        protected void EBtnSubmit_Click(object sender, EventArgs e)
        {
            M_ExamPoint mc = new M_ExamPoint();
            mc.AddTime = !string.IsNullOrEmpty(txtCoureTime.Text) ? Convert.ToDateTime(txtCoureTime.Text.Trim()) : DateTime.Now;
            mc.OrderBy = Convert.ToInt32(txt_End.Text.Trim());
            mc.AddUser = Convert.ToInt32(Request.QueryString["AdminId"]);
            int ids = bep.GetInsert(mc);
            if (ids > 0)
            {
                function.WriteSuccessMsg("添加成功！", "'ExamPointManage.aspx");
            }
            else
            {
                function.WriteErrMsg("添加失败！");
            }
        }
        protected void BtnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("ExamPointManage.aspx");
        }
    }
}