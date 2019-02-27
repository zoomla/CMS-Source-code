using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;

public partial class Manage_WeiXin_ReplyList : System.Web.UI.Page
{
    B_WX_ReplyMsg rpBll = new B_WX_ReplyMsg();
    B_WX_APPID appBll = new B_WX_APPID();
    public int AppId { get { return DataConverter.CLng(Request.QueryString["appid"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
        }
    }
    public void MyBind()
    {
        M_WX_APPID appmod = new M_WX_APPID();
        appmod = appBll.SelReturnModel(AppId);
        string alias = " [公众号:" + appmod.Alias + "]";
        Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='" + CustomerPageAction.customPath2 + "WeiXin/WxAppManage.aspx'>公众号管理</a></li><li class='active'>回复管理<a href='AddReply.aspx?appid=" + appmod.ID + "'>[添加回复]</a>" + alias + "</li>");
        DataTable dt = rpBll.SelByAppID(appmod.ID);
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
                break;
        }
        MyBind();
    }
    protected void BatDel_Btn_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.Form["idchk"]))
        {
            rpBll.DelByIDS(Request.Form["idchk"]);
            MyBind();
        }
    }
    public string GetIsDefault()
    {
        if (DataConverter.CLng(Eval("IsDefault")) == 1)
        {
            return "<i class='fa fa-check' style='color:green;'></i>";
        }
        else { return "<i class='fa fa-close' style='color:red;'></i>"; }
    }
    public string GetMsgType()
    {
        return WxAPI.GetMsgTypeStr(DataConverter.CLng(Eval("MsgType")));
    }

    protected void EGV_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row != null && e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView dr = e.Row.DataItem as DataRowView;
            e.Row.Attributes.Add("ondblclick", "location='AddReply.aspx?ID=" + dr["ID"] + "&appid=" + AppId + "'");
            e.Row.Attributes["style"] = "cursor:pointer;";
            e.Row.Attributes["title"] = Resources.L.双击修改;
        }
    }
}