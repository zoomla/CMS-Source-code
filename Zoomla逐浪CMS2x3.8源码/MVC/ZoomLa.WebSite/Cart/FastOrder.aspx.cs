using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;

namespace ZoomLaCMS.Cart
{
    public partial class FastOrder : System.Web.UI.Page
    {
        /*
     * 生成快速代购订单
     */
        B_User buser = new B_User();
        B_OrderList orderBll = new B_OrderList();
        B_Payment payBll = new B_Payment();
        public string Method { get { return Request["method"]; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            B_User.CheckIsLogged();
            if (!IsPostBack)
            {
                M_FastOrder model = new M_FastOrder()
                {
                    ProUrl = Request["ProUrl"],
                    ProName = Request["ProName"],
                    ProSeller = Request["ProSeller"],
                    Price = Convert.ToDouble(Request["Price"]),
                    Pronum = DataConverter.CLng(Request["Pronum"]),
                    ProClass = DataConverter.CLng(Request["ProClass"]),
                    Proinfo = Request["Proinfo"],
                    Phone = Request["Phone"],
                    Attribute = Request["Attribute"]
                };
                model.Pronum = model.Pronum < 1 ? 1 : model.Pronum;
                if (model.Price < 1) { function.WriteErrMsg("金额不正确"); }
                CreateOrder(model);
            }
        }
        public void CreateOrder(M_FastOrder model)
        {
            M_UserInfo mu = buser.GetLogin(false);
            M_OrderList Odata = new M_OrderList();
            Odata.Ordertype = 10;
            Odata.OrderNo = B_OrderList.CreateOrderNo((M_OrderList.OrderEnum)Odata.Ordertype);
            Odata.StoreID = 0;
            Odata.Rename = mu.UserName;
            Odata.Outstock = 0;//缺货处理
            Odata.Ordermessage = model.Proinfo;
            Odata.Merchandiser = "";//跟单员
            Odata.Internalrecords = ""; //内部记录
            Odata.IsCount = false;
            //-----金额计算
            Odata.Balance_price = model.Price;
            Odata.Freight = 0;//运费计算
            Odata.Ordersamount = Odata.Balance_price + Odata.Freight;//订单金额
            Odata.AllMoney_Json = "";
            Odata.Specifiedprice = Odata.Ordersamount; //订单金额;
            Odata.Receivablesamount = 0;//收款金额
            Odata.Developedvotes = 0;
            Odata.OrderStatus = 0;//订单状态
            Odata.Paymentstatus = 0;//付款状态
            Odata.StateLogistics = 0;//物流状态
            Odata.Signed = 0;//签收
            Odata.Settle = 0;//结清
            Odata.Aside = 0;//作废
            Odata.Suspended = 0;//暂停
            Odata.AddTime = DateTime.Now;
            Odata.AddUser = mu.UserName; ;
            Odata.Userid = mu.UserID;
            Odata.Freight_remark = " ";
            Odata.Balance_remark = "";
            Odata.Promoter = 0;
            Odata.id = orderBll.Adds(Odata);
            M_Payment payMod = new M_Payment();
            payMod.PaymentNum = Odata.OrderNo;
            payMod.MoneyPay = Odata.Ordersamount;
            payMod.Remark = model.ProName;
            payMod.PayNo = payBll.CreatePayNo();
            payMod.UserID = mu.UserID;
            payMod.Status = 1;
            payMod.PaymentID = payBll.Add(payMod);
            if (string.IsNullOrEmpty(Method))
            {
                Response.Redirect("/PayOnline/Orderpay.aspx?PayNo=" + payMod.PayNo);
            }
            else
            {
                Response.Redirect("/PayOnline/PayOnline.aspx?Method=" + Method + "&PayNo=" + payMod.PayNo);
            }
        }
        public class M_FastOrder
        {
            //商品网址
            public string ProUrl;
            //商品名
            public string ProName;
            //来源卖家
            public string ProSeller;
            //总金额,非商品单价
            public double Price;
            public int Pronum;
            //商品类型
            public int ProClass;
            public string Proinfo;
            public string Phone;
            //商品属性
            public string Attribute;
        }
    }
}