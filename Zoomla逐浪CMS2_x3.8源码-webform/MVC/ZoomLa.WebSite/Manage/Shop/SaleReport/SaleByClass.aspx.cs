using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Shop;
using ZoomLa.Model;
using ZoomLa.SQLDAL;

namespace ZoomLaCMS.Manage.Shop.SaleReport
{
    //有商城与店铺节点
    public partial class SaleByClass : System.Web.UI.Page
    {
        B_OrderList orderBll = new B_OrderList();
        B_Node nodeBll = new B_Node();
        B_Shop_SaleReport rpBll=new B_Shop_SaleReport();
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
            if (!IsPostBack)
            {
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
            }
        }
        private void MyBind()
        {
            DataTable dt = Report_SelByClass(STime, ETime);
            RPT.DataSource = dt;
            RPT.DataBind();
            //PayOnline_L.Text = DataConvert.CDouble(dt.Compute("SUM(pay_online)", "")).ToString("F2");
            //PayPurse_L.Text = DataConvert.CDouble(dt.Compute("SUM(pay_purse)", "")).ToString("F2");
            TotalSale_L.Text = DataConvert.CDouble(dt.Compute("SUM(ALLMoney)", "")).ToString("F2");
        }
        protected void Search_Btn_Click(object sender, EventArgs e)
        {
            MyBind();
        }
        private DataTable Report_SelByClass(DateTime stime, DateTime etime)
        {
            //            SELECT A.*,B.Paymentno,B.PayTime FROM 
            //(
            //SELECT A.AllMoney,A.Orderlistid,B.Proname,B.Nodeid
            //FROM ZL_CartPro A LEFT JOIN ZL_Commodities B ON A.ProID=B.ID
            //)A LEFT JOIN ZL_Order_PayedView B ON A.Orderlistid=B.ID 
            //WHERE (B.PaymentNo IS NOT NULL AND B.PaymentNo!='')
            //string field = "SUM(AllMoney) AS AllMoney,Nodeid";
            //string mtable="(SELECT A.AllMoney,A.Orderlistid,B.Proname,B.Nodeid FROM ZL_CartPro A LEFT JOIN ZL_Commodities B ON A.ProID=B.ID)";
            //string where="(B.PaymentNo IS NOT NULL AND B.PaymentNo!='') GROUP BY NodeID";
            string where = "(PaymentNO IS NOT NULL AND PaymentNO!='') AND OrderStatus=99 ";
            where += " AND PayTime>='" + stime.ToString("yyyy/MM/dd 00:00:00") + "'";
            where += " AND PayTime<='" + etime.ToString("yyyy/MM/dd 23:59:59") + "'";
            //return DBCenter.JoinQuery(field,mtable,"ZL_Order_PayedView","A.Orderlistid=B.ID",where," ALLMoney DESC");
            return DBCenter.SelWithField("ZL_Order_ProSaleView", "SUM(AllMoney)AS AllMoney,NodeID,NodeName", where+" GROUP BY NodeID,NodeName", "ALLMoney DESC");
        }
    }
}