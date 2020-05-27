using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL.Mobile;

public partial class Manage_Mobile_Push_APIList : System.Web.UI.Page
{
    B_Mobile_PushAPI apiBll = new B_Mobile_PushAPI();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
            Call.HideBread(Master);
            //Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='Default.aspx'>消息推送</a></li><li class='active'><a href='"+Request.RawUrl+"'>API列表</a> [<a href='AddAPI.aspx'>添加API</a>]</li>");
        }
    }
    private void MyBind() 
    {
        EGV.DataSource = apiBll.Sel();
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
                apiBll.Del(id);
                break;
        }
        MyBind();
    }
    protected void EGV_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row != null && e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView dr = e.Row.DataItem as DataRowView;
            e.Row.Attributes.Add("ondblclick", "location='AddAPI.aspx?ID=" + dr["ID"] + "'");
        }
    }
}