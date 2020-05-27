namespace ZoomLaCMS.Manage.Exam
{
    using System;
    using System.Collections.Generic;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using ZoomLa.BLL;
    using ZoomLa.Model;
    using System.Data;
    using ZoomLa.Common;
    using ZoomLa.Components;
    public partial class ExExamination : System.Web.UI.Page
    {
        protected B_ExAnswer all = new B_ExAnswer();
        protected B_ExClassgroup cll = new B_ExClassgroup();
        protected B_ExStudent sll = new B_ExStudent();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["menu"] != null && Request.QueryString["menu"] == "delete")
            {
                int id = DataConverter.CLng(Request.QueryString["id"]);
                all.DeleteByGroupID(id);
            }

            DataTable ttable = all.Select_All();
            Page_list(ttable);
            Call.SetBreadCrumb(Master, "<li>教育模块</li><li><a href='QuestionManage.aspx'>在线考试系统</a></li><li>考试成绩管理</li>");
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
                        all.DeleteByGroupID(DataConverter.CLng(itemarr[i]));
                    }
                }
                else
                {
                    int id = DataConverter.CLng(item);
                    all.DeleteByGroupID(id);
                }
                function.WriteSuccessMsg("操作成功!", "ExExamination.aspx");
            }
        }

        protected string GetGroup(string studid)
        {
            int stugroupid = sll.GetSelect(DataConverter.CLng(studid)).Stugroup;
            return cll.GetSelect(stugroupid).Regulationame;
        }

        #region 通用分页过程
        /// <summary>
        /// 通用分页过程　by h.
        /// </summary>
        /// <param name="Cll"></param>
        public void Page_list(DataTable Cll)
        {
            RPT.DataSource = Cll;
            RPT.DataBind();
        }
        #endregion
    }
}