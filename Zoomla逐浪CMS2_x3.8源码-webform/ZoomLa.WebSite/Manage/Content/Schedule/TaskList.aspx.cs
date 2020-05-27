using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.HtmlLabel;
using ZoomLa.Model;

public partial class Manage_Content_Schedule_TaskList : System.Web.UI.Page
{
    B_Content_ScheTask scheBll = new B_Content_ScheTask();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='Default.aspx'>任务列表</a></li><li class='active'>任务队列</li>");
        }
    }
    public void MyBind()
    {
        DataTable dt = scheBll.Sel(0);
        dt.Columns.Add(new DataColumn("IsRun", typeof(string)));
        foreach (var model in TaskCenter.TaskList)
        {
            DataRow dr = dt.NewRow();
            dr["ID"] = model.scheMod.ID;
            dr["TaskName"] = model.scheMod.TaskName;
            dr["LastTime"] = model.scheMod.LastTime;
            dr["ExecuteType"] = model.scheMod.ExecuteType;
            dr["ExecuteTime"] = model.scheMod.ExecuteTime;
            dr["CDate"] = model.scheMod.CDate;
            dr["Interval"] = model.scheMod.Interval;
            dr["Remind"] = model.scheMod.Remind;
            dr["IsRun"] = model.flag ? "运行中" : "已停止";
            dt.Rows.Add(dr);
        }
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
        switch (e.CommandName)
        {
            case "del2":
                int id = Convert.ToInt32(e.CommandArgument);
                scheBll.Delete(id);
                TaskCenter.RemoveTask(id);
                break;
        }
        MyBind();
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
}