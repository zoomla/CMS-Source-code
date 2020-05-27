using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;

public partial class Manage_WeiXin_MsgTlpList : System.Web.UI.Page
{
    WxAPI api = null;
    public int AppID { get { return DataConverter.CLng(Request.QueryString["appid"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        api = WxAPI.Code_Get(AppID);
        if (!IsPostBack)
        {
            MyBind();
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='" + CustomerPageAction.customPath2 + "WeiXin/WxAppManage.aspx'>公众号管理</a></li><li class='active'>模板列表</li>");
        }
    }
    public void MyBind()
    {
        EGV.DataSource = api.Tlp_GetAllPteTlp();
        EGV.DataBind();
    }

    protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EGV.PageIndex = e.NewPageIndex;
        MyBind();
    }

    protected void EGV_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row != null && e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("ondblclick", "javascript:location.href='SendTlpMsg.aspx?id=" + EGV.DataKeys[e.Row.RowIndex].Value + "'&appid="+AppID);
        }
    }

    protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Del":
                api.Tlp_DelPteTlp(e.CommandArgument.ToString());
                MyBind();
                break;
        }
    }

    protected void Dels_B_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.Form["idchk"]))
        {
            string[] ids = Request.Form["idchk"].Split(',');
            foreach(string id in ids) { api.Tlp_DelPteTlp(id); }
            MyBind();
        }
    }
}