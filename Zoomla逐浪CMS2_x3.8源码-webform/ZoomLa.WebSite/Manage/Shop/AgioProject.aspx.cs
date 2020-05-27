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

public partial class manage_Shop_AgioProject : CustomerPageAction
{
    B_Scheme bs = new B_Scheme();
    B_SchemeInfo bsi = new B_SchemeInfo();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack )
        {
            DataBind();
        }
        Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='ProductManage.aspx'>商城管理</a></li><li><a href='PresentProject.aspx'>促销方案管理</a></li><li class='active'>打折方案管理<a href='AddAgioProject.aspx'>[添加打折方案]</a></li>");
    }
    public void DataBind(string key="")
    {
        DataTable dt = bs.Select_All();
        Egv.DataSource=dt;
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
    protected void Egv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName.ToLower())
        {
            case "del":
                int id = Convert.ToInt32(e.CommandArgument.ToString());
                bsi.DelBySID(id);
                bs.GetDelete(id);
                break;
        }
        DataBind();
    }
    protected string GetDate(string stime,string etime)
    {
        DateTime dt1 = DateTime.Parse(stime);
        DateTime dt2 = DateTime.Parse(etime);
        if (dt1 != dt2)
            return dt1.ToShortDateString() + " 至 " + dt2.ToShortDateString();
        else
            return "无限期";
    }
    protected string Gettype(string str)
    {
        if (str == "1")
            return "按商品打折";
        else
            return "按商品类型打折";
    }
}
