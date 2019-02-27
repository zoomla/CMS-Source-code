using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Content;
using ZoomLa.Common;
using ZoomLa.HtmlLabel;
using ZoomLa.Model;
using ZoomLa.Model.Content;
using ZoomLa.SQLDAL;

public partial class Manage_Content_Addon_Schedule_Default : System.Web.UI.Page
{
    B_Content_ScheTask scheBll = new B_Content_ScheTask();
    B_Content conBll = new B_Content();
    B_Admin badmin = new B_Admin();
    public int TaskType
    {
        get
        {
            return DataConverter.CLng(Request["TaskType"]) < 1 ? 1 : Convert.ToInt32(Request["TaskType"]);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!B_ARoleAuth.Check(ZLEnum.Auth.other, "DevCenter")) { function.WriteErrMsg("你没有对该模块的访问权限"); }
        if (!IsPostBack)
        {
            if (!badmin.CheckSPwd(Session["Spwd"] as string)) { SPwd.Visible = true; }
            else { maindiv.Visible = true; }
            MyBind();
            if (TaskType == 3) { EGV.Columns[3].Visible = true; }
            //Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li class='active'><a href='Default.aspx'>任务列表</a> [<a href='AddSche.aspx'>添加任务</a>]</li>");
        }
    }
    public void MyBind(string skey = "")
    {
        DataTable dt = null;
        if (string.IsNullOrEmpty(skey)) { dt = scheBll.Sel(TaskType); }
        else { dt = scheBll.Sel(TaskType, skey); }
        EGV.DataSource = dt;
        EGV.DataBind();
    }
    protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EGV.PageIndex = e.NewPageIndex;
        MyBind();
    }
    protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int id = Convert.ToInt32(e.CommandArgument);
        switch (e.CommandName)
        {
            case "del2":
                scheBll.Delete(id);
                TaskCenter.RemoveTask(id);
                break;
            case "execNow":
                ExecuteNow(id);
                break;
        }
        MyBind();
    }
    /// <summary>
    /// 强制执行一次任务，不改变任务状态
    /// </summary>
    /// <param name="scheID"></param>
    public void ExecuteNow(int scheID)
    {
        M_Content_ScheTask taskMod = scheBll.SelReturnModel(scheID);
        switch (taskMod.TaskType)
        {
            case (int)M_Content_ScheTask.TaskTypeEnum.ExecuteSQL:
                {
                    if (taskMod.TaskContent.StartsWith("/")) //若以'/'或'\'开头则为脚本
                    {
                        DBHelper.ExecuteSqlScript(DBCenter.DB.ConnectionString, function.VToP(taskMod.TaskContent));
                    }
                    else
                    {
                        SqlHelper.ExecuteSql(taskMod.TaskContent);
                    }
                }
                break;
            //case (int)M_Content_ScheTask.TaskTypeEnum.Release:
            //    break;
            case (int)M_Content_ScheTask.TaskTypeEnum.Content:
                {
                    conBll.UpdateStatus(taskMod.TaskContent, 99);
                    scheBll.UpdateStatus(taskMod.ID.ToString(), 100);
                }
                break;
        }
        //增加一条日志
        taskMod.LastTime = DateTime.Now.ToString();
        DBCenter.UpdateSQL(taskMod.TbName, "LastTime='" + DateTime.Now + "'", "ID=" + taskMod.ID);
        B_Content_ScheLog logBll = new B_Content_ScheLog();
        M_Content_ScheLog logMod = new M_Content_ScheLog();
        logMod.TaskID = taskMod.ID;
        logMod.TaskName = taskMod.TaskName;
        logMod.Result = 1;
        logBll.Insert(logMod);
    }
    protected void EGV_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row != null && e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView dr = e.Row.DataItem as DataRowView;
            e.Row.Attributes.Add("ondblclick", "location='AddSche.aspx?ID=" + dr["ID"] + "'");
        }
    }
    //--------------------------------------------------------------------------------------------
    protected void InitTask_Btn_Click(object sender, EventArgs e)
    {
        TaskCenter.Init();
        TaskCenter.Start();
        function.WriteSuccessMsg("初始化成功");
    }
    protected void Search_B_Click(object sender, EventArgs e)
    {
        string skey = Skey_T.Text;
        if (!string.IsNullOrEmpty(skey)) { sel_box.Attributes.Add("style", "display:inline;"); }
        else { sel_box.Attributes.Add("style", "display:none;"); }
        MyBind(skey);
    }
    public string GetContentLink(string ids)
    {
        if (TaskType != 3) { return ""; }
        string links = "";
        DataTable dt = conBll.SelByIDS(ids);
        foreach (DataRow row in dt.Rows)
        {
            links += "<a href='" + CustomerPageAction.customPath2 + "Content/ShowContent.aspx?Gid=" + row["GeneralID"] + "'>" + row["Title"] + "</a><br />";
        }
        return links;
    }
    protected void StopTask_Btn_Click(object sender, EventArgs e)
    {
        string[] idArr = (Request.Form["idchk"] ?? "").Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
        if (idArr.Length < 1) { function.WriteErrMsg("未选定需要操作的任务"); }
        for (int i = 0; i < idArr.Length; i++)
        {
            int id = Convert.ToInt32(idArr[i]);
            var taskMod = TaskCenter.TaskList.FirstOrDefault(p => p.scheMod.ID == id);
            if (taskMod != null) { TaskCenter.TaskList.Remove(taskMod); }
        }
        scheBll.UpdateStatus(Request.Form["idchk"], -1);
        function.WriteSuccessMsg("操作成功,已从任务队列中移除指定任务");
    }

    protected void StartTask_Btn_Click(object sender, EventArgs e)
    {
        //1,非停用的任务,自动忽略,
        //2.已停用的任务,修改状态并加入任务队列
        string ids = Request.Form["idchk"];
        if (string.IsNullOrWhiteSpace(ids)) { function.WriteErrMsg("未选定需要操作的任务"); }
        DataTable dt = scheBll.SelByIDS(ids);
        foreach (DataRow dr in dt.Rows)
        {
            if (Convert.ToInt32(dr["Status"]) == (int)ZLEnum.Common.Stop)
            {
                int id = Convert.ToInt32(dr["ID"]);
                scheBll.UpdateStatus(id.ToString(), (int)ZLEnum.Common.Normal);
                M_Content_ScheTask model = scheBll.SelReturnModel(id);
                TaskCenter.AddTask(model);
            }
        }
        function.WriteSuccessMsg("操作成功,已将停用的任务加入队列");
    }
    //----------------------------
    public string GetExecuteType()
    {
        return scheBll.GetExecuteType(Convert.ToInt32(Eval("ExecuteType")));
    }
    public string GetExecuteTime()
    {
        string date = Eval("ExecuteTime", "");
        string interval = Eval("Interval", "");
        try
        {
            switch (Convert.ToInt32(Eval("ExecuteType")))
            {
                case (int)M_Content_ScheTask.ExecuteTypeEnum.Interval:
                    return "每隔:" + interval + "分钟";
                case (int)M_Content_ScheTask.ExecuteTypeEnum.EveryDay:
                    return "每日:" + Convert.ToDateTime(date).ToString("HH:mm:ss");
                case (int)M_Content_ScheTask.ExecuteTypeEnum.JustOnce:
                default:
                    return date;
            }
        }
        catch { return date; }
    }
    public string GetLastTime()
    {
        string last = Eval("LastTime", "");
        try
        {
            if (string.IsNullOrEmpty(last)) { return "尚未执行"; }
            else { return Convert.ToDateTime(last).ToString(); }
        }
        catch (Exception) { return "<span style='color:red;'>转换失败:[" + last + "]</span>"; }
    }
    public string GetStatus()
    {
        if (Convert.ToInt32(Eval("ExecuteType")) == (int)M_Content_ScheTask.ExecuteTypeEnum.Passive)
        {
            return "正常";
        }
        switch (Convert.ToInt32(Eval("Status")))
        {
            case 0:
                return "正常";
            case 99:
            case 100:
                return "已完成";
            case -1:
                return "<span style='color:red;'>停用</span>";
            default:
                return "未知状态";
        }
    }
    //----------------------------
}