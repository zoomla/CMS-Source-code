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

public partial class User_Guest_MyAsk : System.Web.UI.Page
{
    protected B_User buser = new B_User();
    protected B_Ask b_Ask = new B_Ask();
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
        string strWhere = "";
        if (!string.IsNullOrEmpty(Request.QueryString["type"]))
        {
            string type = Request.QueryString["type"];
            if(type=="1")
            {
                strWhere = " and Status=1";
            }
            if (type == "2")
            {
                strWhere = " and Status=2";
            }
        }
        DataTable dt = b_Ask.Sel(" UserName=" + "'" + name + "'"+strWhere, "",null);
        if (dt.Rows.Count > 0)
        {
            Repeater_ask.DataSource = dt;
            Repeater_ask.DataBind(); 
        }
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
        if (Convert.ToInt32(ViewState["Page"]) > 1)
            Bind(Convert.ToInt32(ViewState["Page"]) - 1);
    }
    /// <summary>
    /// 下一页
    /// </summary>
    protected void Next_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ViewState["Page"]) < Convert.ToInt32(ViewState["PageCount"]))
            Bind(Convert.ToInt32(ViewState["Page"]) + 1);
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
        Bind(Convert.ToInt32(ViewState["Page"]));
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
        //HtmlGenericControl txtView = (HtmlGenericControl)e.Item.FindControl("lbUpdate");
        DataRowView drv = (DataRowView)e.Item.DataItem;
        if (drv.Row["Status"].ToString() == "2")
        {
            lbstatus.InnerHtml = "已解决";
            //txtView.InnerHtml = "√";
        }
        else
        {
            lbstatus.InnerHtml = "<a href='/Guest/Ask/Interactive.aspx?ID=" + drv.Row["ID"].ToString() + "' target='_blank'>待解决</a>";
        }
         
        DataTable dt = SqlHelper.ExecuteTable(CommandType.Text, "select * from ZL_GuestAnswer where supplymentid<>1 and Queid=" + drv.Row["ID"].ToString(), null);
        count.InnerHtml = dt.Rows.Count.ToString();
    }

    //protected void RepeaterAsk_ItemCommand(object sender, RepeaterCommandEventArgs e)
    //{
    // string id = e.CommandArgument.ToString();
    // if (e.CommandName == "Update")
    // { 
     
    // }
    //}

}