using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.SQLDAL;

public partial class Manage_I_Guest_AllSearch : System.Web.UI.Page
{
    B_Guest_Bar barBll = new B_Guest_Bar();
    B_Ask askBll = new B_Ask();
    B_GuestBookCate cateBll = new B_GuestBookCate();
    
    public string Skey { get { return string.IsNullOrEmpty(Request.QueryString["keyword"])?"": Request.QueryString["keyword"].Trim(); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
            Call.SetBreadCrumb(Master, "<li><a href='GuestCateMana.aspx?type=1'>工作台</a></li><li class='active'><a href='" + Request.RawUrl + "'>聚合搜索</a></li>");
        }
    }
    public void MyBind()
    {
        DataTable dt = barBll.SelAllByTitle(Skey,Request.QueryString["list"]);
        EGV.DataSource = dt;
        EGV.DataBind();
    }
    protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName.ToLower())
        {
            case "1":
                barBll.Del(DataConvert.CLng(e.CommandArgument));
                break;
            case "2":
                cateBll.Del(DataConvert.CLng(e.CommandArgument));
                break;
            case "3":
                askBll.Del(DataConvert.CLng(e.CommandArgument));
                break;
        }
        MyBind();
    }
    protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EGV.PageIndex = e.NewPageIndex;
        MyBind();
    }
    public string GetTieType()
    {
        string result = "";
        switch (Eval("SType").ToString())
        {
            case "1":
                result = "贴吧";
                break;
            case "2":
                result = "留言";
                break;
            case "3":
                result = "问答";
                break;
        }
        return result;
    }

    public string GetEditUrl()
    {
        string result = "";
        switch (Eval("SType").ToString())
        {
            case "1":
                result = "GuestBookShow.aspx?GID=" + Eval("ID")+"&CateID="+Eval("CateID");
                break;
            case "2":
                result = "GuestBookShow.aspx?GID=" + Eval("ID") + "&CateID=" + Eval("CateID");
                break;
            case "3":
                result = "WdAlter.aspx?ID=" + Eval("ID");
                break;
        }
        return result;
    }
}