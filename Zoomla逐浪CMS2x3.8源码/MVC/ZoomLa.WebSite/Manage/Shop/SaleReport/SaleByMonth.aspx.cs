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
    public partial class SaleByMonth : System.Web.UI.Page
    {
        B_OrderList orderBll = new B_OrderList();
        B_Shop_SaleReport rpBll = new B_Shop_SaleReport();
        private DateTime STime
        {
            get
            {
                if (!string.IsNullOrEmpty(SDate_T.Text)) { return Convert.ToDateTime(SDate_T.Text); }
                else { return Convert.ToDateTime(DateTime.Now.ToString("yyyy/01")); }
            }
        }
        private DateTime ETime
        {
            get
            {
                if (!string.IsNullOrEmpty(EDate_T.Text)) { return Convert.ToDateTime(EDate_T.Text); }
                else { return Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM")); }
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!B_ARoleAuth.Check(ZLEnum.Auth.shop, "TotalSale")) { function.WriteErrMsg("没有权限进行此项操作"); }
            if (!IsPostBack)
            {
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='ProductManage.aspx'>商城管理</a></li><li><a href='TotalSale.aspx'>按日销售统计</a></li>");
                //按支付平台和日为单位统计,必须指定结束日期
                if (!string.IsNullOrEmpty(Request.QueryString["stime"]))
                {
                    SDate_T.Text = Request.QueryString["stime"];
                    EDate_T.Text = Request.QueryString["etime"];
                }
                else
                {
                    SDate_T.Text = STime.ToString("yyyy/01");
                    EDate_T.Text = ETime.ToString("yyyy/MM");
                }
                Years_Li.Text = rpBll.Html_Date("year", STime.Year.ToString());
                MyBind();
            }
        }
        private void MyBind()
        {
            DataTable saleDT = orderBll.Report_SelByDate(Convert.ToDateTime(STime.ToString("yyyy/MM/01")), Convert.ToDateTime(ETime.ToString("yyyy/MM/"+DateTime.DaysInMonth(ETime.Year,ETime.Month))));
            DataTable monthDT = new DataTable();
            monthDT.Columns.Add("date", typeof(string));
            monthDT.Columns.Add("total", typeof(double));
            monthDT.Columns.Add("pay_online", typeof(double));
            monthDT.Columns.Add("pay_purse", typeof(double));

            for (DateTime s = STime; s <= ETime; s = s.AddMonths(1))
            {
                DataRow month = monthDT.NewRow();
                //DateTime sdate = Convert.ToDateTime("{0}/{1}/{2} 00:00:00");
                string sdate = s.ToString("#yyyy/MM/01 00:00:00#"), edate = s.AddMonths(1).ToString("#yyyy/MM/01 00:00:00#");
                saleDT.DefaultView.RowFilter = "PayTime>= " + sdate + " AND PayTime< " + edate;
                month["date"] = s.ToString("yyyy-MM");
                month["total"] = 0;
                month["pay_online"] = 0;
                month["pay_purse"] = 0;
                foreach (DataRow dr in saleDT.DefaultView.ToTable().Rows)
                {
                    month["Total"] = DataConvert.CDouble(month["Total"]) + DataConvert.CDouble(dr["OrdersAmount"]);
                    if (Convert.ToInt32(dr["PayPlatID"]) == 0) { month["pay_purse"] = DataConvert.CDouble(month["pay_purse"]) + DataConvert.CDouble(dr["OrdersAmount"]); }
                    else { month["pay_online"] = DataConvert.CDouble(month["pay_online"]) + DataConvert.CDouble(dr["OrdersAmount"]); }
                }
                monthDT.Rows.Add(month);
            }
            Day_RPT.DataSource = monthDT;
            Day_RPT.DataBind();
            PayOnline_L.Text = DataConvert.CDouble(monthDT.Compute("SUM(pay_online)", "")).ToString("F2");
            PayPurse_L.Text = DataConvert.CDouble(monthDT.Compute("SUM(pay_purse)", "")).ToString("F2");
            TotalSale_L.Text = DataConvert.CDouble(monthDT.Compute("SUM(total)", "")).ToString("F2");
        }
        protected void Search_Btn_Click(object sender, EventArgs e)
        {
            MyBind();
        }
    }
}