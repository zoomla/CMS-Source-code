using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ZoomLa.BLL;
using ZoomLa.Common;

public partial class manage_Zone_LargessMoney : CustomerPageAction
{
    B_User ubll = new B_User();
    B_Admin abll = new B_Admin();
    protected void Page_Load(object sender, EventArgs e)
    {
        Egv.txtFunc = txtPageFunc;
        if (!IsPostBack)
        {
            DataBind();
        }
        Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='" + customPath2 + "User/UserManage.aspx'>会员管理</a></li><li><a href='ZoneManage.aspx'>会员空间管理</a></li><li class='active'>赠送游戏币</li>");
    }
    public void DataBind(string key="")
    {
        DataTable dt = ubll.Sel();
        Egv.DataSource = dt;
        Egv.DataBind();
    }
    protected void txtPageFunc(string size)
    {
        int pageSize;
        if (!int.TryParse(size, out pageSize))//如果转换失败,即不是一个数字时
        {
            pageSize = Egv.PageSize;
        }
        else if (pageSize < 1)//小于1时,均恢复默认PageSize,默认PageSize是你给序的
        {
            pageSize = Egv.PageSize;
        }
        Egv.PageSize = pageSize;
        Egv.PageIndex = 0;//改变后回到首页
        size = pageSize.ToString();
        DataBind();
    }
    protected void Egv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Egv.PageIndex = e.NewPageIndex;
        DataBind();
    }
    protected void Egv_RowEditing(object sender, GridViewEditEventArgs e)
    {
        Egv.EditIndex = e.NewEditIndex;
        DataBind();
    }
    protected void Egv_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
      
    }
    protected void Egv_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        Egv.EditIndex = -1;
        DataBind();
    }
}
