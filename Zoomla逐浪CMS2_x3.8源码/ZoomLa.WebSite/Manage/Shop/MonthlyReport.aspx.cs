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
using ZoomLa.Model;
using System.Collections.Generic;
using System.Text;
using ZoomLa.Components;
using ZoomLa.SQLDAL;


public partial class manage_Shop_MonthlyReport : CustomerPageAction
{
    private B_OrderList bll = new B_OrderList();
    protected void Page_Load(object sender, EventArgs e)
    {
        B_Admin badmin = new B_Admin();
        badmin.CheckIsLogin();
        if (!this.Page.IsPostBack)
        {
            dBind();
        }
        Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='" + customPath2 + "Shop/ProductManage.aspx'>商城管理</a></li></li><li class='active'>月份报表</li>");
    }
    private void dBind()
    {      
        this.EGV.DataSource = getDB();  
        this.EGV.DataBind();   
    }
    private DataTable getDB()
    {
         DataTable dt = new DataTable();
 
        dt.Columns.Add("code");
        dt.Columns.Add("name");

        dt.Rows.Add(new object[] { DateTime.Now.Year + "-01", "一月" });
        dt.Rows.Add(new object[] { DateTime.Now.Year + "-02", "二月" });
        dt.Rows.Add(new object[] { DateTime.Now.Year + "-03", "三月" });
        dt.Rows.Add(new object[] { DateTime.Now.Year + "-04", "四月" });
        dt.Rows.Add(new object[] { DateTime.Now.Year + "-05", "五月" });
        dt.Rows.Add(new object[] { DateTime.Now.Year + "-06", "六月" });
        dt.Rows.Add(new object[] { DateTime.Now.Year + "-07", "七月" });
        dt.Rows.Add(new object[] { DateTime.Now.Year + "-08", "八月" });
        dt.Rows.Add(new object[] { DateTime.Now.Year + "-09", "九月" });
        dt.Rows.Add(new object[] { DateTime.Now.Year + "-10", "十月" });
        dt.Rows.Add(new object[] { DateTime.Now.Year + "-11", "十一月" });
        dt.Rows.Add(new object[] { DateTime.Now.Year + "-12", "十二月" });
 
        return dt;
     }
    protected string GetOrderNum(object dataItem)
    {
        //int units = Int32.Parse(DataBinder.Eval(dataItem, "name").ToString());
        string code = DataBinder.Eval(dataItem, "code").ToString();
        DataTable location;
        location = SqlHelper.ExecuteTable(CommandType.Text, "SELECT * FROM ZL_Orderinfo Where Convert(varchar(20),AddTime,120) like '%" + code + "%'", null);
        int OrderNum = location.Rows.Count;
        return Convert.ToString(OrderNum);
    }

    protected string GetOrderAmount(object dataItem)
    {
        //int units = Int32.Parse(DataBinder.Eval(dataItem, "name").ToString());
        string code = DataBinder.Eval(dataItem, "code").ToString();
        DataTable location;
        location = SqlHelper.ExecuteTable(CommandType.Text, "SELECT * FROM ZL_Orderinfo Where Convert(varchar(20),AddTime,120) like '%" + code + "%'", null);
        int num = 0;
        double ddmoney = 0.00;
        num = location.Rows.Count;
        for (int i = 0; i < num; i++)
        {
            ddmoney = ddmoney + Convert.ToDouble(getshijiage(location.Rows[i]["id"].ToString()));
        }
        string OrderAmount = "0";
        OrderAmount = ddmoney + ".00";
        return OrderAmount;
    }

    public string getshijiage(string id)
    {
        int Sid = DataConverter.CLng(id);
        //string shijiage = "";
        M_OrderList orders = bll.GetOrderListByid(Sid);
        DataTable tb = bll.GetOrderbyOrderNo(orders.OrderNo, "0,4");
        object s = tb.Compute("sum(ordersamount)", "orderno='" + orders.OrderNo + "'");
        if (orders.Ordertype != 4)
        {
            s = DataConverter.CDouble(s).ToString("0.00");
        }
        else
        {
            s = DataConverter.CLng(DataConverter.CDouble(s)).ToString();
        }
        return s.ToString();
    }

    protected void Lnk_Click(object sender, GridViewCommandEventArgs e) 
    {
        string code = e.CommandArgument.ToString();
        Response.Redirect("OrderList.aspx?Monthcode=" + code);
    }

    protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EGV.PageIndex = e.NewPageIndex;
        this.dBind();
    }
}

