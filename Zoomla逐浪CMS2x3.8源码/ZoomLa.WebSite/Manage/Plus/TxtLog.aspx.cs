using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;

public partial class Manage_Plus_TxtLog : System.Web.UI.Page
{
    private string LogType { get { return Request.QueryString["LogType"]; } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='LogManage.aspx'>日志管理</a></li><li class='active'>" + GetLogStr() + "</li>");
        }
    }
    private void MyBind()
    {
        DataTable dt = FileSystemObject.GetDirectoryInfos(function.VToP(GetLogPath()), FsoMethod.File);
        dt.DefaultView.Sort = "CreateTime DESC";
        EGV.DataSource = dt.DefaultView.ToTable();
        EGV.DataBind();
    }
    protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EGV.PageIndex = e.NewPageIndex;
        MyBind();
    }
    protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string fname = e.CommandArgument.ToString();
        string logpath = GetLogPath() + fname;
        switch (e.CommandName)
        {
            case "view":
                viewdiv.Visible = true;
                egvdiv.Visible = false;
                Curfname_Hid.Value = fname;
                string text=SafeSC.ReadFileStr(logpath);
                if (text.Trim().Length < 30) { text = "该日志无记录"; }
                LogTxt_Li.Text = text.Replace("\r\n","<br />");//替换文本换行
                break;
            case "down":
                SafeSC.DownFile(logpath);
                break;
            case "del2":
                SafeSC.DelFile(logpath);
                MyBind();
                break;
        }
    }
    //----------------Tools
    private string GetLogStr()
    {
        switch (LogType)
        {
            case "safe":
                return "安全日志";
            case "fileup":
                return "上传日志";
            case "exception":
                return "异常日志";
            case "labelex":
                return "标签异常日志";
            default:
                return "未知日志";
        }
    }
    private string GetLogPath()
    {
        return "/Log/" + LogType + "/";
    }
    protected void DownLog_Btn_Click(object sender, EventArgs e)
    {
        string fname = Curfname_Hid.Value;
        string logpath = GetLogPath() + fname;
        SafeSC.DownFile(logpath);
    }
}