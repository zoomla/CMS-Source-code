using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Components;
using System.Data;

public partial class manage_User_PointGroup : CustomerPageAction
{
    B_PointGrounp bpointgroup = new B_PointGrounp();


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Bind();
        }
        Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>"+Resources.L.工作台 + "</a></li><li><a href='AdminManage.aspx'>" + Resources.L.用户管理 + "</a></li><li><a href='UserManage.aspx'>" + Resources.L.会员管理 + "</a></li><li class='active'>" + Resources.L.积分等级管理 + "<a href='AddPointGroup.aspx'>[" + Resources.L.添加积分等级 + "]</a></li>");
    }
    /// <summary>
    /// 分页绑定
    /// </summary>
    private void Bind()
    {
        DataTable accs = bpointgroup.Select_All();
        EGV.DataSource = accs;
        EGV.DataBind();
    }

    protected void BtnDelete_Click(object sender, EventArgs e)
    {
        
        Bind();
    }

    protected void txtPage_TextChanged(object sender, EventArgs e)
    {
        ViewState["page"] = "1";
        Bind();
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        Bind();
    }
    protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EGV.PageIndex = e.NewPageIndex;
        Bind();
    }
    public string GetPointIcon()
    {
        string iconstr = Eval("ImgUrl").ToString();
        return StringHelper.GetItemIcon(iconstr);

    }
    protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Upd")
        {
            Response.Redirect("AddPointGroup.aspx?id=" + e.CommandArgument);
        }
        if (e.CommandName == "del")
        {
            bool res = bpointgroup.DeleteByGroupID(DataConverter.CLng(e.CommandArgument));
            if (res)
            {
                function.WriteSuccessMsg(Resources.L.删除成功+ "！");
            }
            else
            {
                function.WriteErrMsg(Resources.L.删除失败+"！");
            }
            Bind();
        }
    }


    protected void EGV_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row != null && e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["ondblclick"] = "javascript:getinfo('" + EGV.DataKeys[e.Row.RowIndex].Value + "');";
            e.Row.Attributes["style"] = "cursor:pointer;";
            e.Row.Attributes["title"] =Resources.L.双击修改;
        }
    }
}