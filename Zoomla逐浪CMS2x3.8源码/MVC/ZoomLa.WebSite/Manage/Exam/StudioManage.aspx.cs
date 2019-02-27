namespace ZoomLaCMS.Manage.Exam
{
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
    public partial class StudioManage : System.Web.UI.Page
    {
        protected B_ExStudent csll = new B_ExStudent();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["menu"] != null && Request.QueryString["menu"] == "delete")
                {
                    string sid = Request.QueryString["id"];
                    csll.DeleteByGroupID(DataConverter.CLng(sid));
                }
            }

            DataTable cstable = csll.Select_All();
            Page_list(cstable);
            Call.SetBreadCrumb(Master, "<li>教育模块</li><li><a href='QuestionManage.aspx'>在线考试系统</a></li><li>学员管理<a href='AddStudio.aspx'>[添加学员]</a></li>");
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
        protected void Button3_Click(object sender, EventArgs e)
        {
            if (Request.Form["item"] != null && Request.Form["item"] != "")
            {
                string item = Request.Form["item"];
                if (item.IndexOf(',') > -1)
                {
                    string[] itemarr = item.Split(',');
                    for (int i = 0; i < itemarr.Length; i++)
                    {
                        csll.DeleteByGroupID(DataConverter.CLng(i));
                    }
                }
                else
                {
                    csll.DeleteByGroupID(DataConverter.CLng(item));
                }
                function.WriteSuccessMsg("操作成功!", "StudioManage.aspx");
            }
        }
    }
}