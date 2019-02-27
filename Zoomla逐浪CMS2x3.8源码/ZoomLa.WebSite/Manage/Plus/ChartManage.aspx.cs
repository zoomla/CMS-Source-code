using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using System.Data;
using ZoomLa.Components;

public partial class manage_Plus_ChartManage : CustomerPageAction
{
    ChartCall chart = new ChartCall();
    protected void Page_Load(object sender, EventArgs e)
    {
        Egv.txtFunc = txtPageFunc;
        if (!IsPostBack)
        {
            DataBind();
        }
        Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='" + customPath2 + "Plus/ADManage.aspx'>广告管理</a></li><li class='active'><a href='ChartManage.aspx'>图表管理</a>  <a href='../Flex/AddChart.aspx'>[添加图表]</a></li>" + Call.GetHelp(32));
    }
    public string GetGname(string name)
    {
        name = name.Replace(name.Substring(name.IndexOf('.')),"");
        return name;
    }
    private void DataBind(string key="")
    {
        DataTable dt = new DataTable();
        if (!string.IsNullOrEmpty(Request.QueryString["type"]))
        {
            if (Request.QueryString["type"] == "1")
                dt = B_ADZone.Select_Bytype("饼状图");
            if(Request.QueryString["type"] == "2")
                dt = B_ADZone.Select_Bytype("线状图");
            if(Request.QueryString["type"] == "3")
                dt = B_ADZone.Select_Bytype("柱状图");
        }
        else
            dt = B_ADZone.SelectChart();
        Egv.DataSource = dt;
        Egv.DataBind();
    }
    protected void txtPageFunc(string size)
    {
        int pageSize;
        if (!int.TryParse(size, out pageSize))//如果转换失败,即不是一个数字时
        {
            pageSize = Egv.PageSize;
        }
        else if (pageSize < 1)//小于1时,均恢复默认PageSize,默认PageSize是你给序的
        {
            pageSize = Egv.PageSize;
        }
        Egv.PageSize = pageSize;
        Egv.PageIndex = 0;//改变后回到首页
        size = pageSize.ToString();
        DataBind();
    }
    protected void Egv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Egv.PageIndex = e.NewPageIndex;
        DataBind();
    }
    protected void Egv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string id = e.CommandArgument.ToString();
        if (e.CommandName == "Edit")
            Page.Response.Redirect("../Flex/AddChart.aspx?ChartID=" + id);
        if (e.CommandName == "Del")
        {
            if (B_ADZone.Chart_Remove(id))
                function.WriteSuccessMsg("删除成功！");
            DataBind();
        }
        if (e.CommandName == "ifframe")
            Page.Response.Redirect("ShowChartCode.aspx?ChartID=" + id);
    }
}