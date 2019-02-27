namespace ZoomLaCMS.Manage.Exam
{
    using System;
    using System.Collections.Generic;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using ZoomLa.BLL;
    using ZoomLa.Common;
    using ZoomLa.Model;
    using ZoomLa.Components;
    using System.Data;
    public partial class ExTeacherManage : System.Web.UI.Page
    {
        private B_ExTeacher bet = new B_ExTeacher();
        private B_Exam_Class bqc = new B_Exam_Class();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["menu"]) && Request.QueryString["menu"] == "delete")
            {
                int id = DataConverter.CLng(Request.QueryString["id"]);
                bet.DeleteByGroupID(id);
            }
            if (!IsPostBack)
            {
                MyBind();
            }
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='Papers_System_Manage.aspx'>教育模块</a></li><li><a href='ExTeacherManage.aspx'>培训资源库</a></li><li>教师管理<a href='AddExTeacher.aspx'>[添加教师]</a></li>" + Call.GetHelp(83));
        }
        public void MyBind()
        {
            Repeater1.DataSource = bet.Select_All();
            Repeater1.DataBind();
        }
        public string GetTeachClass(string classid)
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
        protected void Button3_Click(object sender, EventArgs e)
        {
            string item = Request.Form["idchk"];
            if (item != null && item != "")
            {
                if (item.IndexOf(',') > -1)
                {
                    string[] itemarr = item.Split(',');
                    for (int i = 0; i < itemarr.Length; i++)
                    {
                        bet.DeleteByGroupID(DataConverter.CLng(itemarr[i]));
                    }
                }
                else
                {
                    bet.DeleteByGroupID(DataConverter.CLng(item));
                }
            }
            function.WriteSuccessMsg("操作成功!", "ExTeacherManage.aspx");
        }

        public string GetRemark(string remark)
        {
            if (remark.Length > 25)
            {
                return remark.Substring(0, 25) + "...";
            }
            else
            {
                return remark;
            }
        }
    }
}