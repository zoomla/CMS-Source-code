using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using System.Data;
using ZoomLa.BLL.User;
using ZoomLa.Model;
using ZoomLa.Model.User;

public partial class manage_Counter_Browser : CustomerPageAction
{
    B_Com_VisitCount visitBll = new B_Com_VisitCount();
    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
        }
        Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li> <li><a href='Counter.aspx'>访问统计</a></li><li><a href='Browser.aspx'>浏览器统计报表</a></li>");
    }
    public void MyBind()
    {
        DataTable dt = visitBll.SelByBrowser();
        CountRPT.DataSource = dt;
        CountRPT.DataBind();
        dt = visitBll.SelectAll();
        EGV.DataSource = dt;
        EGV.DataBind();
    }
    public string GetBrowserInfo()
    {
        return VisitCounter.User.BrowserIcon(Eval("BrowerVersion").ToString());
    }

    protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EGV.PageIndex = e.NewPageIndex;
        MyBind();
    }
}