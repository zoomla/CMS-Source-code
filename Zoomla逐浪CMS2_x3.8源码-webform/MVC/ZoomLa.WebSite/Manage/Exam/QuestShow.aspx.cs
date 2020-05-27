namespace ZoomLaCMS.Manage.Exam
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using ZoomLa.Model;
    using ZoomLa.BLL;
    using ZoomLa.Common;
    using System.Text;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System.Data;
    public partial class QuestShow : System.Web.UI.Page
    {
        B_Exam_Sys_Questions questionBll = new B_Exam_Sys_Questions();
        B_ExamPoint pointBll = new B_ExamPoint();
        B_Exam_Class classBll = new B_Exam_Class();
        B_Questions_Knowledge knowBll = new B_Questions_Knowledge();

        public int QID { get { return DataConverter.CLng(Request.QueryString["id"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (QID <= 0) { function.WriteErrMsg("参数错误!"); }
                MyBind();
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li> <li><a href='Papers_System_Manage.aspx'>教育模块</a></li><li><a href='QuestionManage.aspx'>试题管理</a></li><li>试题预览</li>");
            }
        }
        public void MyBind()
        {
            M_Exam_Sys_Questions model = questionBll.GetSelect(QID);
            if (model == null) { function.WriteErrMsg("无效id!"); }
            Title_L.Text = model.p_title;
            Grade_L.Text = B_GradeOption.GetGradeOption(model.p_Views).GradeName;
            Diff_L.Text = questionBll.GetDiffStr(model.p_Difficulty);
            QType_L.Text = M_Exam_Sys_Questions.GetTypeStr(model.p_Type);
            if (!string.IsNullOrEmpty(model.Tagkey))
            { KeyWord_L.Text = knowBll.GetNamesByIDS(model.Tagkey); }
            Content_Li.Text = model.p_Content;
            if (model.p_Type == 10)
            {
                Content_Li.Text = model.LargeContent;
                Option_Li.Visible = false;
                Quest_RPT.Visible = true;
                DataTable dt = questionBll.SelByIDSForExam(model.p_id.ToString());
                dt.DefaultView.RowFilter = "pid>0";
                dt.DefaultView.Sort = "order desc";
                Quest_RPT.DataSource = dt;
                Quest_RPT.DataBind();
            }
            else
            {
                Option_Li.Text = questionBll.GetSubmit(model.p_id, model.p_Type, ref AngularJS);
            }
            Socre_L.Text = model.p_defaultScores.ToString();
            QuestNum_L.Text = model.p_ChoseNum.ToString();
            Answer_L.Text = model.p_Answer;
            AnswerHtml_Li.Text = model.p_shuming;
            Jiexi_Li.Text = model.Jiexi;
        }
        public string AngularJS = "";


        public string GetContent()
        {
            return questionBll.GetContent(DataConverter.CLng(Eval("p_id")), DataConverter.CLng(Eval("p_type")), Eval("p_Content").ToString());
        }

        public string GetSubmit()
        {
            return questionBll.GetSubmit(DataConverter.CLng(Eval("p_id")), DataConverter.CLng(Eval("p_type")), ref AngularJS);
        }
    }
}