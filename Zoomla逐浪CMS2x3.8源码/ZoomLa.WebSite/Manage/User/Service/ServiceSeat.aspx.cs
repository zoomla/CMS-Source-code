using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Common;

public partial class manage_User_ServiceSeat : CustomerPageAction
{
    B_ServiceSeat seatBll = new B_ServiceSeat();
    B_Admin babll = new B_Admin();
    //页面加载事件
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
            Call.SetBreadCrumb(Master, "<li>后台管理</li><li>客服通</li><li><a href='" + Request.RawUrl + "'>客服列表</a>[<a href='AddSeat.aspx'>添加席位</a>]</li>" + Call.GetHelp(51));
        }
    }
    //绑定数据
    public void MyBind()
    {
        EGV.DataSource = seatBll.Select_All();
        EGV.DataBind();
    }
    //修改
    protected void lbtEdit_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow dli = (GridViewRow)lb.NamingContainer;
        string s = EGV.DataKeys[dli.RowIndex].Value.ToString();
        Response.Redirect("AddSeat.aspx?id=" + s);
    }
    //删除
    protected void lbtDel_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow dli = (GridViewRow)lb.NamingContainer;
        string s = EGV.DataKeys[dli.RowIndex].Value.ToString();
        seatBll.GetDelete(DataConverter.CLng(s));
        Response.Redirect("ServiceSeat.aspx");
    }
    //批量删除
    protected void BatDel_Btn_Click(object sender, EventArgs e)
    {
        string ids = Request.Form["idchk"];
        if (!string.IsNullOrEmpty(ids))
        {
            seatBll.DelByIDS(ids);
            MyBind();
        }
    }
    //分页功能
    protected void Egv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EGV.PageIndex = e.NewPageIndex;
        MyBind();
    }
    //GridView双击事件
    protected void Egv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row != null && e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["ondblclick"] = "javascript:getinfo('" + EGV.DataKeys[e.Row.RowIndex].Value + "');";
            e.Row.Attributes["style"] = "cursor:pointer;";
            e.Row.Attributes["title"] = "双击修改";
        }
    }
}
