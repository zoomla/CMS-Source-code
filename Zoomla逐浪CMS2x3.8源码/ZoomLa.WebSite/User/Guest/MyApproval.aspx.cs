using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Components;
using ZoomLa.SQLDAL;

public partial class User_Guest_MyApproval : System.Web.UI.Page
{
    B_User buser = new B_User();
    protected void Page_Load(object sender, EventArgs e)
    {
        Bind(1);
    }
    protected void Bind(int page)
    {
        string name = buser.GetLogin().UserName;
        DataTable dt = SqlHelper.ExecuteTable(CommandType.Text, "select * from ZL_AskCommon where Type=0 and UserID=" + buser.GetLogin().UserID.ToString(), null);
        Rep_comment.DataSource = dt;
        Rep_comment.DataBind();
    }
    #region 分页导航
    /// <summary>
    /// 首页
    /// </summary>
    protected void First_Click(object sender, EventArgs e)
    {
        Bind(1);
    }
    /// <summary>
    /// 上一页
    /// </summary>
    protected void Prev_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ViewState["CurrentPage"]) > 1)
            Bind(Convert.ToInt32(ViewState["CurrentPage"]) - 1);
    }
    /// <summary>
    /// 下一页
    /// </summary>
    protected void Next_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ViewState["CurrentPage"]) < Convert.ToInt32(ViewState["PageCount"]))
            Bind(Convert.ToInt32(ViewState["CurrentPage"]) + 1);
    }
    /// <summary>
    /// 尾页
    /// </summary>
    protected void Last_Click(object sender, EventArgs e)
    {
        Bind(Convert.ToInt32(ViewState["PageCount"]));
    }
    /// <summary>
    /// 分页大小
    /// </summary>
    protected void txtPage_TextChanged(object sender, EventArgs e)
    {
        Bind(Convert.ToInt32(ViewState["CurrentPage"]));
    }
    /// <summary>
    /// 下拉列表分页导航
    /// </summary>
    protected void DropDownListPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        Bind(Convert.ToInt32(Request["DropDownListPage"]));
    }
    #endregion
    protected void Rep_comment_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        HtmlGenericControl lbAsk = e.Item.FindControl("lbAsk") as HtmlGenericControl;
        HtmlGenericControl lbAsw = e.Item.FindControl("lbAsw") as HtmlGenericControl;
        DataRowView drv = e.Item.DataItem as DataRowView;
        DataTable dt = SqlHelper.ExecuteTable(CommandType.Text, "select * from ZL_Ask where ID=" + drv.Row["AskID"], null);
        DataTable dt1 = SqlHelper.ExecuteTable(CommandType.Text, "select * from ZL_GuestAnswer where ID=" + drv.Row["AswID"], null);
        lbAsk.InnerHtml = "<a href='/Guest/Ask/SearchDetails.aspx?ID=" + drv.Row["AskID"].ToString() + "' target='_blank'>" + dt.Rows[0]["Qcontent"].ToString() + "</a>";
        lbAsw.InnerHtml = dt1.Rows[0]["Content"].ToString();
    }
}