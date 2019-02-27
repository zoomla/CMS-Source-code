using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Components;
using ZoomLa.SQLDAL;

public partial class User_Guest_MyAnswer : System.Web.UI.Page
{
    protected B_User buser = new B_User();
    protected B_GuestAnswer b_answer = new B_GuestAnswer();
    protected B_Ask b_ask = new B_Ask();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Bind(1);
        }
    }
    protected void Bind(int page)
    {
        string name = buser.GetLogin().UserName;
        SqlParameter[] sp = new SqlParameter[] { new SqlParameter("name", name) };
        DataTable dt = b_ask.Sel("ID in (select QueId from ZL_GuestAnswer where UserName=@name And supplymentid<>3) ", " AddTime desc", sp);
        Repeater_ask.DataSource = dt;
        Repeater_ask.DataBind();
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
    protected void Repeater_ask_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        HtmlGenericControl lbstatus = (HtmlGenericControl)e.Item.FindControl("lbstatus");
        HtmlGenericControl count = (HtmlGenericControl)e.Item.FindControl("count");
        DataRowView drv = (DataRowView)e.Item.DataItem;
        //被采纳  未采纳 待解决问题
        DataTable dt1 = b_answer.Sel("supplymentid<>1 and Queid=" + drv.Row["ID"].ToString() + " and UserName='" + buser.GetLogin().UserName + "'", "AddTime Desc", null);
        if (dt1.Rows.Count < 1)
            return;
        if (drv.Row["Status"].ToString() == "1" && dt1.Rows[0]["Status"].ToString() == "0")
        {
            lbstatus.InnerHtml = "未采纳";
        }
        else if (dt1.Rows[0]["Status"].ToString() == "1")
        {
            lbstatus.InnerHtml = "已采纳";
        }
        else//drv.Row["Status"].ToString() == "0"
        {
            lbstatus.InnerHtml = "未解决";
        }
        DataTable dt = b_answer.Sel("supplymentid<>1 and Queid=" + drv.Row["ID"].ToString(), "AddTime Desc", null);
        count.InnerHtml = dt.Rows.Count.ToString();
    }
}