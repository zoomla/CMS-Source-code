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

public partial class manage_Shop_AgioList : CustomerPageAction
{
    B_SchemeInfo bsi = new B_SchemeInfo();
    B_Scheme bs = new B_Scheme();
    protected string proName = "";
    private  int AID
    {
        get
        {
            if (ViewState["AID"] != null)
                return int.Parse(ViewState["AID"].ToString());
            else
                return 0;
        }
        set { ViewState["AID"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["ID"] != null)
            {
                AID = int.Parse(Request.QueryString["ID"].ToString());
                proName = bs.GetSelect(AID).SName;
            }
            DataBind();
        }
        Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='ProductManage.aspx'>商城管理</a></li><li><a href='PresentProject.aspx'>促销方案管理</a></li><li><a href='AgioProject.aspx'>打折方案管理</a></li><li class='active'>打折信息管理<a href='AddAgio.aspx?AID=" + Request.QueryString["ID"] + "'>[添加打折信息]</a></li>");
    }
    public void DataBind(string key="")
    {
        DataTable dt= new DataTable();
        if(!string.IsNullOrEmpty(Request.QueryString["ID"]))
        {
            AID=Convert.ToInt32(Request.QueryString["ID"]);
            dt = bsi.SelectAgioList(AID.ToString());
        }
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
    protected void Egv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row != null && e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["ondblclick"] = "javascript:location.href='AddAgio.aspx?AID=" + AID + "&ID=" + Egv.DataKeys[e.Row.RowIndex].Value + "';";
            e.Row.Attributes["title"] = "双击修改";
        }
    }
    protected void Egv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName.ToLower())
        {
            case "del":
                bsi.GetDelete(Convert.ToInt32(e.CommandArgument.ToString()));
                break;
        }
        DataBind();
    }
}
