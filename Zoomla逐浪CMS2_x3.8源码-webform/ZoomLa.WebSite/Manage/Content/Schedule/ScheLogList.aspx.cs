using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Content;
using ZoomLa.SQLDAL;

public partial class Manage_Content_Schedule_ScheLogList : System.Web.UI.Page
{
    B_Content_ScheLog logBll = new B_Content_ScheLog();
    B_Content_ScheTask taskBll = new B_Content_ScheTask();
    private int TaskID { get { return DataConvert.CLng(Request.QueryString["TaskID"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='Default.aspx'>计划任务</a></li><li class='active'>执行日志</li>");
        }
    }

    public void MyBind()
    {
        EGV.DataSource = logBll.Sel(50000,TaskID);
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
                logBll.Del(id);
                break;
        }
        MyBind();
    }
    public string GetResult() 
    {
        switch (Convert.ToInt32(Eval("Result")))
        {
            case 1:
                return "<span style='color:green;'>成功</span>";
            case -1:
            default:
                return "<span style='color:red;'>失败("+Eval("ErrMsg")+")</span>";
        }
    }
    public string GetExecuteType()
    {
        return taskBll.GetExecuteType(Convert.ToInt32(Eval("ExecuteType")));
    }
}