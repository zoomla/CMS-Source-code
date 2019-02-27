using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.SQLDAL;
using System.Data.SqlClient;
using ZoomLa.Common;


namespace ZoomLaCMS.Manage.Content
{
    public partial class SchedTask : System.Web.UI.Page
    {
        string TaskType, Status, TaskContent, ExecuteTime;
        private B_Admin badmin = new B_Admin();
        private B_Content_ScheTask scheBll = new B_Content_ScheTask();
        private M_Content_ScheTask scheModel = new M_Content_ScheTask();
        public void Page_Load(object sender, EventArgs e)
        {
            Response.Redirect("Schedule/Default.aspx");
            //if (!IsPostBack)
            //{
            //    if (!string.IsNullOrEmpty(Request.QueryString["type"]))
            //    {
            //        EGV.DataSource = scheBll.SelByStatus(Request.QueryString["type"]);
            //        EGV.DataBind();
            //    }
            //    else
            //    {
            //        EGV.DataSource = scheBll.Sel();
            //        EGV.DataBind();
            //    }
            //}
            //Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='ContentManage.aspx'>内容管理</a></li><li class='active'>计划任务</li>");
        }

        public void DataBind(string key)
        {
            EGV.DataSource = SqlHelper.ExecuteTable(CommandType.Text, "Select * From ZL_Content_ScheTask");
            EGV.DataBind();
        }
        protected void EGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int Line = e.Row.RowIndex + 2;
                e.Row.Attributes.Add("ondblclick", "javascript:__doPostBack('EGV$ctl0" + Line + "$LinkButton1','')");
                e.Row.Attributes.Add("style", "cursor:pointer");
                e.Row.Attributes.Add("title", "双击修改");
            }
        }
        protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EGV.PageIndex = e.NewPageIndex;
            DataBind("");
        }

        public string GetTaskType(string taskType)
        {
            //string result = "";
            //switch (DataConvert.CLng(taskType))
            //{
            //    case (int)M_Content_ScheTask.TaskTypeEnum.AuditArt:
            //        result = "<span style='color:green;'>定时发布</span>";
            //        break;
            //    case (int)M_Content_ScheTask.TaskTypeEnum.CreateIndex:
            //        result = "<span style='color:blue;'>发布首页</span>";
            //        break;
            //    case (int)M_Content_ScheTask.TaskTypeEnum.UnAuditArt:
            //        result = "<span style='color:red;'>定时过期</span>";
            //        break;
            //}
            //return result;
            return "";
        }
        public string GetStatus(string status)
        {
            int executeType = Convert.ToInt32(Eval("executeType"));
            string result = "";
            if (executeType == (int)M_Content_ScheTask.ExecuteTypeEnum.EveryDay)
            {
                result = "循环执行(每日)";
            }
            else if (executeType == (int)M_Content_ScheTask.ExecuteTypeEnum.Interval)
            {
                result = "循环执行(间隔)";
            }
            else
            {
                switch (status)
                {
                    case "0":
                        result = "未执行";
                        break;
                    case "10":
                        result = "已执行";
                        break;
                }
            }
            return result;
        }
        //执行时间
        public string GetExTime()
        {
            int executeType = Convert.ToInt16(Eval("executeType"));
            string result = "";
            if (executeType == (int)M_Content_ScheTask.ExecuteTypeEnum.JustOnce)
            {
                result = Convert.ToDateTime(Eval("ExecuteTime")).ToString("yyyy年MM月dd日 HH:mm");
            }
            else if (executeType == (int)M_Content_ScheTask.ExecuteTypeEnum.EveryDay)
            {
                result = "每日：" + Convert.ToDateTime(Eval("ExecuteTime")).ToString("HH:mm");
            }
            else if (executeType == (int)M_Content_ScheTask.ExecuteTypeEnum.Interval)
            {
                result = "每隔：" + Eval("ExecuteTime").ToString() + "分钟";
            }
            return result;

        }
        public string GetID(string ID)
        {
            return ID;
        }
        public void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Edit1":
                    EGV.EditIndex = Convert.ToInt32(e.CommandArgument as string);
                    break;
                case "Save":
                    string[] s = e.CommandArgument.ToString().Split(':');
                    Update(DataConvert.CLng(s[0]), s[1]);
                    EGV.EditIndex = -1;
                    break;
                case "Del":
                    int id = DataConvert.CLng(e.CommandArgument as string);
                    SqlHelper.ExecuteScalar(CommandType.Text, "Delete From ZL_Content_ScheTask Where ID = " + id);
                    break;
                default:
                    break;
            }
            DataBind("");
        }
        public void EGV_RowEditing(object sender, GridViewEditEventArgs e)
        {
            EGV.EditIndex = e.NewEditIndex;
            DataBind("");
        }
        protected void EGV_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            EGV.EditIndex = -1;
            DataBind("");
        }
        private void Update(int rowNum, string id)
        {
            GridViewRow gr = EGV.Rows[rowNum];
            TaskType = ((TextBox)gr.FindControl("eTaskType")).Text.Trim();
            Status = ((TextBox)gr.FindControl("eStatus")).Text.Trim();
            //TaskContent = ((TextBox)gr.FindControl("eTaskContent")).Text.Trim();
            TaskContent = "";
            ExecuteTime = ((TextBox)gr.FindControl("eExecuteTime")).Text.Trim();
            SqlParameter[] sp = new SqlParameter[] {
            new SqlParameter("ID",id),
            new SqlParameter("TaskType",TaskType),
            new SqlParameter("Status",Status),
            new SqlParameter("TaskContent",TaskContent),
            new SqlParameter("ExecuteTime",ExecuteTime),
            };
            SqlHelper.ExecuteScalar(CommandType.Text, "Update ZL_Content_ScheTask set TaskType=@TaskType,Status=@Status,TaskContent=@TaskContent,ExecuteTime=@ExecuteTime Where ID=@ID", sp);
        }
    }
}