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
    public partial class QuestionTypeManage : System.Web.UI.Page
    {
        private B_Exam_Type bqt = new B_Exam_Type();
        private B_Admin badmin = new B_Admin();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["menu"]) && Request.QueryString["menu"] == "delete")
                {
                    int id = DataConverter.CLng(Request.QueryString["id"]);
                    bqt.GetDelete(id);
                }
                MyBind();
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li> <li><a href='Papers_System_Manage.aspx'>教育模块</a></li> <li><a href='QuestionManage.aspx'>考试管理</a></li> <li>题型管理</li>" + Call.GetHelp(78));
            }
        }
        public void MyBind()
        {
            ET_RPT.DataSource = bqt.SelAll();
            ET_RPT.DataBind();
        }
        public string GetType(string typeid)
        {
            //单选,多选,判断，填空,问答,组合
            switch (typeid)
            {
                case "1":
                    return "单选题";
                case "2":
                    return "多选题";
                case "3":
                    return "判断题";
                case "4":
                    return "填空题";
                case "5":
                    return "问答题";
                case "6":
                    return "组合题";
                default:
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
                        bqt.GetDelete(DataConverter.CLng(itemarr[i]));
                    }
                }
                else
                {
                    bqt.GetDelete(DataConverter.CLng(item));
                }
            }
            function.WriteSuccessMsg("操作成功!", "QuestionTypeManage.aspx");
        }
    }
}