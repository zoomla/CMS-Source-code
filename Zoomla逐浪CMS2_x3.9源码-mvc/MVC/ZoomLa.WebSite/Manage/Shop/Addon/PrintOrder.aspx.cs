using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.SQLDAL;

namespace ZoomLaCMS.Manage.Shop.Addon
{
    public partial class PrintOrder : System.Web.UI.Page
    {
        B_OrderList orderBll = new B_OrderList();
        B_CartPro cartProBll = new B_CartPro();
        private int Mid { get { return DataConvert.CLng(Request.QueryString["id"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Call.HideBread(Master);
                M_OrderList orderMod = orderBll.SelReturnModel(Mid);
                if (orderMod == null) { function.WriteErrMsg("订单不存在"); }
                OrderNo_T.Text = orderMod.OrderNo;
                AddTime_T.Text = orderMod.AddTime.ToString("yyyy-MM-dd HH:mm:ss");
                ReUserName_L.Text = orderMod.Receiver;
                Mobile_L.Text = orderMod.MobileNum;
                Address_L.Text = orderMod.AddRess;
                //----------
                DataTable dt = cartProBll.SelByOrderID(Mid);
                RPT.DataSource = dt;
                RPT.DataBind();
                //----------
                //P_Arrive_L.Text = null;
                //P_Point_L.Text = null;
                P_Pro_L.Text = orderMod.Balance_price.ToString("f2");
                P_Exp_L.Text = orderMod.Freight.ToString("f2");
                TotalMoney_L.Text = orderMod.Ordersamount.ToString("f2");
            }
        }
    }
}