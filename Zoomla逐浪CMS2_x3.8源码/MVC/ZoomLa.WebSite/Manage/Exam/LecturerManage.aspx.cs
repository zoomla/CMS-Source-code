namespace ZoomLaCMS.Manage.Exam
{
    using System;
    using System.Collections.Generic;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using ZoomLa.BLL;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using ZoomLa.Components;
    using System.Data;
    public partial class LecturerManage : System.Web.UI.Page
    {
        private B_Exam_Class bqc = new B_Exam_Class();
        private B_ExLecturer bel = new B_ExLecturer();
        private B_Admin badmin = new B_Admin();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["menu"]) && Request.QueryString["menu"] == "delete")
            {
                int id = DataConverter.CLng(Request.QueryString["id"]);
                bel.DeleteByGroupID(id);
            }
            if (!IsPostBack)
            {
                MyBind();
            }
            Call.SetBreadCrumb(Master, "<li>教育模块</li><li><a href='QuestionManage.aspx'>在线考试系统</a></li><li>讲师管理<a href='AddLecturer.aspx'>[添加讲师]</a></li>");
        }
        public void MyBind()
        {
            ExLec_RPT.DataSource = bel.Select_All();
            ExLec_RPT.DataBind();
        }
        public string GetSex(string sex)
        {
            if (sex == "0")
            {
                return "男";
            }
            else
            {
                return "女";
            }
        }
        public string GetClass(string classid)
        {
            M_Exam_Class mqc = bqc.GetSelect(DataConverter.CLng(classid));
            if (mqc != null && mqc.C_id > 0)
            {
                return mqc.C_ClassName;
            }
            else
            {
                return "";
            }
        }
        public string GetContent(string cont)
        {
            if (cont.Length > 12)
            {
                return cont.Substring(0, 12);
            }
            else
            {
                return cont;
            }
        }
        protected void Button3_Click(object sender, EventArgs e)
        {
            string item = Request.Form["item"];
            if (item != null && item != "")
            {
                if (item.IndexOf(',') > -1)
                {
                    string[] itemarr = item.Split(',');
                    for (int i = 0; i < itemarr.Length; i++)
                    {
                        bel.DeleteByGroupID(DataConverter.CLng(itemarr[i]));
                    }
                }
                else
                {
                    bel.DeleteByGroupID(DataConverter.CLng(item));
                }
            }
            function.WriteSuccessMsg("操作成功!", "ExTeacherManage.aspx");
        }
    }
}