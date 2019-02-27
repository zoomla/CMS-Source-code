using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Shop;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.SQLDAL;

namespace ZoomLaCMS.Manage.Shop.SaleReport
{
    public partial class SaleByDay : System.Web.UI.Page
    {
        /*
       * 1,按付款日期统计
       */
        B_OrderList orderBll = new B_OrderList();
        B_Shop_SaleReport rpBll = new B_Shop_SaleReport();
        private DateTime STime
        {
            get
            {
                if (!string.IsNullOrEmpty(SDate_T.Text)) { return Convert.ToDateTime(SDate_T.Text); }
                else { return Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/01")); }
            }
        }
        private DateTime ETime
        {
            get
            {
                if (!string.IsNullOrEmpty(EDate_T.Text)) { return Convert.ToDateTime(EDate_T.Text); }
                else { return Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd")); }
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!B_ARoleAuth.Check(ZLEnum.Auth.shop, "TotalSale")) { function.WriteErrMsg("没有权限进行此项操作"); }
            if (!IsPostBack)
            {
                //按支付平台和日为单位统计,必须指定结束日期
                if (!string.IsNullOrEmpty(Request.QueryString["stime"]))
                {
                    SDate_T.Text = Request.QueryString["stime"];
                    EDate_T.Text = Request.QueryString["etime"];
                }
                else
                {
                    SDate_T.Text = STime.ToString("yyyy/MM/dd");
                    EDate_T.Text = ETime.ToString("yyyy/MM/dd");
                }
                Years_Li.Text = rpBll.Html_Date("year", STime.Year.ToString());
                Months_Li.Text = rpBll.Html_Date("month", STime.Month.ToString());
                MyBind();
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='ProductManage.aspx'>商城管理</a></li><li><a href='TotalSale.aspx'>按日销售统计</a></li>");
            }
        }
        private void MyBind()
        {
            DataTable saleDT = orderBll.Report_SelByDate(STime, ETime);
            DataTable dayDT = new DataTable();
            dayDT.Columns.Add("date", typeof(string));
            dayDT.Columns.Add("total", typeof(double));
            dayDT.Columns.Add("pay_online", typeof(double));
            dayDT.Columns.Add("pay_purse", typeof(double));
            for (DateTime s = STime; s <= ETime; s = s.AddDays(1))
            {
                DataRow day = dayDT.NewRow();
                //DateTime sdate = Convert.ToDateTime("{0}/{1}/{2} 00:00:00");
                string sdate = s.ToString("#yyyy/MM/dd 00:00:00#"), edate = s.ToString("#yyyy/MM/dd 23:59:59#");
                saleDT.DefaultView.RowFilter = "PayTime>= " + sdate + " AND PayTime<= " + edate;
                day["date"] = s.ToString("yyyy-MM-dd");
                day["total"] = 0;
                day["pay_online"] = 0;
                day["pay_purse"] = 0;
                foreach (DataRow dr in saleDT.DefaultView.ToTable().Rows)
                {
                    day["Total"] = DataConvert.CDouble(day["Total"]) + DataConvert.CDouble(dr["OrdersAmount"]);
                    if (Convert.ToInt32(dr["PayPlatID"]) == 0) { day["pay_purse"] = DataConvert.CDouble(day["pay_purse"]) + DataConvert.CDouble(dr["OrdersAmount"]); }
                    else { day["pay_online"] = DataConvert.CDouble(day["pay_online"]) + DataConvert.CDouble(dr["OrdersAmount"]); }
                }
                dayDT.Rows.Add(day);
            }
            Day_RPT.DataSource = dayDT;
            Day_RPT.DataBind();
            PayOnline_L.Text = DataConvert.CDouble(dayDT.Compute("SUM(pay_online)", "")).ToString("F2");
            PayPurse_L.Text = DataConvert.CDouble(dayDT.Compute("SUM(pay_purse)", "")).ToString("F2");
            TotalSale_L.Text = DataConvert.CDouble(dayDT.Compute("SUM(total)", "")).ToString("F2");
        }
        protected void Search_Btn_Click(object sender, EventArgs e)
        {
            MyBind();
        }
    }
}