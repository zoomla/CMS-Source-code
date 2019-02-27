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
using ZoomLa.Sns;
using System.Collections.Generic;
using ZoomLa.BLL;
using ZoomLa.Common;

public partial class manage_Zone_CarManage : CustomerPageAction 
{
    Parking_BLL pl = new Parking_BLL();
    protected void Page_Load(object sender, EventArgs e)
    {
        Egv.txtFunc = txtPageFunc;
        B_Admin admin = new B_Admin();
        if (!IsPostBack)
        {
            DataBind();
        }
        Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='" + customPath2 + "User/UserManage.aspx'>会员管理</a></li><li><a href='ZoneManage.aspx'>会员空间管理</a></li><li class='active'>车辆列表<a href='CarAdd.aspx'>[添加车辆]</a><a href='CarRuleManage.aspx'>[抢车位规则管理]</a></li>");
    }
    public void DataBind(string key="")
    {
        Egv.DataSource = pl.GetCarList();
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
    protected string GetStr(string str)
    {
        if (str == "1")
            return "停售";
        else
            return "出售";
    }
    protected void Egv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName.ToLower())
        {
            case "stop":
                pl.UpdateCarListCheck(Convert.ToInt32(e.CommandArgument.ToString()),1);
                break;
            default :
                break;
        }
        DataBind();
    }
}
