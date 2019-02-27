using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL.Third;

public partial class Manage_Config_PlatInfoList : System.Web.UI.Page
{
    B_Third_PlatInfo infoBll = new B_Third_PlatInfo();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
            Call.SetBreadCrumb(Master, "<li>扩展功能</li><li><a href='RunSql.aspx'>开发中心</a></li><li class='active'>平台信息 [<a href='AddPlatInfo.aspx'>添加信息</a>]</li>");// 
        }
    }
    private void MyBind()
    {
        EGV.DataSource = infoBll.Sel();
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
                infoBll.Del(id);
                break;
        }
        MyBind();
    }
    protected void EGV_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row != null && e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView dr = e.Row.DataItem as DataRowView;
            e.Row.Attributes.Add("ondblclick", "location='AddPlatInfo.aspx?ID=" + dr["ID"] + "'");
        }
    }
    //--------------------------------------------------------------------
    public string GetCallBack()
    {
        string url = Eval("CallBack", "");
        if (string.IsNullOrEmpty(url)) { return "未指定"; }
        else { return "<a href='" + url + "' target='_blank'>" + url + "</a>"; }
    }
}