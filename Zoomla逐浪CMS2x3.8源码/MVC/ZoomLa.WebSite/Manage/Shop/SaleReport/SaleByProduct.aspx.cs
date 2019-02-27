using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL.Shop;
using ZoomLa.SQLDAL;

namespace ZoomLaCMS.Manage.Shop.SaleReport
{
    public partial class SaleByProduct : System.Web.UI.Page
    {
        B_Shop_SaleReport rpBll=new B_Shop_SaleReport();
        public int NodeID { get { return DataConvert.CLng(Request.QueryString["NodeID"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["stime"]))
                {
                    SDate_T.Text = Request.QueryString["stime"];
                    EDate_T.Text = Request.QueryString["etime"];
                }
                string year = string.IsNullOrEmpty(SDate_T.Text) ? "0" : Convert.ToDateTime(SDate_T.Text).Year.ToString();
                string month = string.IsNullOrEmpty(SDate_T.Text) ? "0" : Convert.ToDateTime(SDate_T.Text).Month.ToString();
                Years_Li.Text = rpBll.Html_Date("year", year, true);
                Months_Li.Text = rpBll.Html_Date("month", month, true);
                MyBind();
            }
        }
        private void MyBind()
        {
            DataTable dt = Report_SelByProduct(SDate_T.Text, EDate_T.Text);
            RPT.DataSource = dt;
            RPT.DataBind();
            //PayOnline_L.Text = DataConvert.CDouble(dt.Compute("SUM(pay_online)", "")).ToString("F2");
            //PayPurse_L.Text = DataConvert.CDouble(dt.Compute("SUM(pay_purse)", "")).ToString("F2");
           TotalSale_L.Text = DataConvert.CDouble(dt.Compute("SUM(ALLMoney)", "")).ToString("F2");
        }
        private DataTable Report_SelByProduct(string stime, string etime)
        {
            List<SqlParameter> sp = new List<SqlParameter>();
            string where = "(PaymentNO IS NOT NULL AND PaymentNO!='') AND OrderStatus=99 ";
            if (!string.IsNullOrEmpty(stime))
            {
                where += " AND PayTime>=@stime";
                sp.Add(new SqlParameter("stime", Convert.ToDateTime(stime).ToString("yyyy/MM/dd 00:00:00")));
            }
            if (!string.IsNullOrEmpty(etime))
            {
                where += " AND PayTime<=@etime";
                sp.Add(new SqlParameter("etime", Convert.ToDateTime(etime).ToString("yyyy/MM/dd 23:59:59")));
            }
            if (NodeID > 0) { where += " AND NodeID=" + NodeID; }
            string mtable = "(SELECT SUM(AllMoney)AS AllMoney,SUM(Pronum)AS Pronum,ProID,ProName,Nodeid FROM ZL_Order_ProSaleView  WHERE " + where + " GROUP BY ProID,ProName,Nodeid)";
            return DBCenter.JoinQuery("A.*,B.NodeName", mtable, "ZL_Node", "A.NodeID=B.NodeID", "", "AllMoney DESC",sp.ToArray());
        }
        protected void Search_Btn_Click(object sender, EventArgs e)
        {
            MyBind();
        }
    }
}