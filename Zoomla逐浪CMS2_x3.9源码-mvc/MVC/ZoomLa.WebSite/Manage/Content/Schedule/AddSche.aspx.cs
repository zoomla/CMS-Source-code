using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.HtmlLabel;
using ZoomLa.Model;
using ZoomLa.SQLDAL;

namespace ZoomLaCMS.Manage.Content.Schedule
{
    public partial class AddSche : System.Web.UI.Page
    {

        //修改状态后,必须初始化或重启网站才会生效
        B_Content_ScheTask scheBll = new B_Content_ScheTask();
        M_Content_ScheTask scheMod = new M_Content_ScheTask();
        B_Admin badmin = new B_Admin();
        private int Mid { get { return DataConvert.CLng(Request.QueryString["ID"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!B_ARoleAuth.Check(ZLEnum.Auth.other, "DevCenter")) { function.WriteErrMsg("你没有对该模块的访问权限"); }
            if (!IsPostBack)
            {
                if (!badmin.CheckSPwd(Session["Spwd"] as string))
                    SPwd.Visible = true;
                else
                    maindiv.Visible = true;
                MyBind();
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='Default.aspx'>计划任务</a></li><li class='active'>任务管理</li>");
            }
        }
        private void MyBind()
        {
            if (Mid > 0)
            {
                scheMod = scheBll.GetModel(Mid);
                TaskName_T.Text = scheMod.TaskName;
                TaskContent_T.Text = scheMod.TaskContent;
                if (scheMod.ExecuteType == (int)M_Content_ScheTask.ExecuteTypeEnum.JustOnce) { ExecuteTime_T1.Text = scheMod.ExecuteTime.ToString(); }
                if (scheMod.ExecuteType == (int)M_Content_ScheTask.ExecuteTypeEnum.EveryDay) { ExecuteTime_T2.Text = scheMod.ExecuteTime.ToString(); }
                Interval_T.Text = scheMod.Interval > 0 ? scheMod.Interval.ToString() : "";
                Remind_T.Text = scheMod.Remind;
                Save_Btn.Text = "修改任务";
                function.Script(this, "SetRadVal('taskType_rad','" + scheMod.TaskType + "');");
                function.Script(this, "DisTaskTypeRad();");
                function.Script(this, "SetRadVal('executeType_rad','" + scheMod.ExecuteType + "');");
                function.Script(this, "SetRadVal('status_rad','" + scheMod.Status + "');");
            }
        }
        protected void Save_Btn_Click(object sender, EventArgs e)
        {
            if (Mid > 0) { scheMod = scheBll.GetModel(Mid); }
            scheMod.TaskName = TaskName_T.Text;
            //scheMod.ExecuteTime = ExecuteTime_T1.Text;
            scheMod.Interval = DataConvert.CLng(Interval_T.Text);
            if (TaskContent_T.Text.StartsWith("/"))//任务内容为脚本路径时
            {
                if (!File.Exists(function.VToP(TaskContent_T.Text))) { function.WriteErrMsg("脚本不存在"); }
                else { scheMod.TaskContent = TaskContent_T.Text; }
            }
            else
            {
                scheMod.TaskContent = TaskContent_T.Text;
            }
            scheMod.Remind = Remind_T.Text;
            //任务类型不允许修改
            if (Mid <= 0) { scheMod.TaskType = DataConvert.CLng(Request.Form["taskType_rad"]); }
            scheMod.ExecuteType = DataConvert.CLng(Request.Form["executeType_rad"]);
            scheMod.Status = DataConvert.CLng(Request.Form["status_rad"]);
            if (scheMod.ExecuteType == (int)M_Content_ScheTask.ExecuteTypeEnum.Interval)
            {
                if (scheMod.Interval <= 0) { function.WriteErrMsg("未指定正确的间隔时间"); }
            }
            else if (scheMod.ExecuteType == (int)M_Content_ScheTask.ExecuteTypeEnum.JustOnce)
            {
                scheMod.ExecuteTime = ExecuteTime_T1.Text;
                if (DataConvert.CDate(scheMod.ExecuteTime) < DateTime.Now) { function.WriteErrMsg("执行时间无效"); }
            }
            else if (scheMod.ExecuteType == (int)M_Content_ScheTask.ExecuteTypeEnum.EveryDay)
            {
                scheMod.ExecuteTime = ExecuteTime_T2.Text;
            }
            if (Mid > 0)
            {
                scheBll.Update(scheMod);
            }
            else
            {
                M_AdminInfo adminMod = B_Admin.GetLogin();
                scheMod.CDate = DateTime.Now;
                scheMod.AdminID = adminMod.AdminId;
                scheMod.ID = scheBll.Add(scheMod);
            }
            TaskCenter.AddTask(scheMod);
            function.WriteSuccessMsg("操作成功", "Default.aspx");
        }
    }
}