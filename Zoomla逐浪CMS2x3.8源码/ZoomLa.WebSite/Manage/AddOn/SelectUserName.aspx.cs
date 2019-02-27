using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.BLL;
using ZoomLa.Model;
using System.Data;

public partial class manage_AddOn_SelectProjectName : System.Web.UI.Page
{
    private B_Client_Basic bll = new B_Client_Basic();
    private B_Client_Enterprise ell = new B_Client_Enterprise();
    private B_Client_Penson pll = new B_Client_Penson();
    protected void Page_Load(object sender, EventArgs e)
    {　
        if (Request.QueryString["menu"] != null && Request.QueryString["menu"] == "select")
        {
            int id = DataConverter.CLng(Request.QueryString["id"]);
            M_Client_Basic Binfo = bll.GetSelect(id);
            string types = Binfo.Client_Type;
            string Code = Binfo.Code;
            string scripttxt = "setvalue('TxtUserName','" + Binfo.P_name + "');";
            function.Script(Page,scripttxt + ";onstr();");
        }
        if (!IsPostBack)
        {
            DataBind();
        }
        DataTable tables = bll.Select_All();
        tables.DefaultView.Sort = "Flow desc";
    }
    public void DataBind(string key = "")
    {
        DataTable dt = bll.Select_All();
        dt.DefaultView.Sort = "Flow desc";
        Egv.DataSource = dt.DefaultView.ToTable();
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
}
