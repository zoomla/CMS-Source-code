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

namespace ZoomLaCMS.Manage.Shop.Diag
{
    public partial class OrderPay : System.Web.UI.Page
    {
        B_OrderList orderBll = new B_OrderList();
        B_PayPlat platBll = new B_PayPlat();
        B_Payment payBll = new B_Payment();
        B_Admin badmin = new B_Admin();
        B_User buser = new B_User();
        public int OrderID { get { return DataConverter.CLng(Request.QueryString["orderid"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (OrderID < 1) { function.WriteErrMsg("未指定订单编号"); }
                M_OrderList orderMod = orderBll.SelReturnModel(OrderID);
                if (orderMod == null) { function.WriteErrMsg("指定的订单不存在"); }
                OrderID_L.Text = orderMod.OrderNo;
                Money_T.Text = orderMod.Ordersamount.ToString();
                Plat_RBT.DataSource = platBll.GetPayPlatListAll(0);
                Plat_RBT.DataBind();
                ListItem item1 = new ListItem("余额支付", "Purse");
                ListItem item2 = new ListItem("银币支付", "SilverCoin");
                ListItem item3 = new ListItem("积分支付", "Score");
                ListItem[] items = new ListItem[] { item1, item2, item3 };
                Plat_RBT.Items.AddRange(items);
                Plat_RBT.SelectedIndex = Plat_RBT.Items.IndexOf(item1);
            }
        }

        protected void OrderPay_B_Click(object sender, EventArgs e)
        {
            string plat = Plat_RBT.SelectedValue;
            double money = DataConverter.CDouble(Money_T.Text);
            M_OrderList orderMod = orderBll.SelReturnModel(OrderID);
            M_Payment payMod = payBll.CreateByOrder(orderMod);
            M_UserInfo mu = buser.GetSelect(orderMod.Userid);
            string adminName= badmin.GetAdminLogin().AdminName;
            string remind="管理员[" + adminName + "]确认支付,支付单号:" + payMod.PayNo;
            //虚拟币支付
            switch (plat)
            {
                case "Purse":
                    {
                        if (SyncDeduct_Chk.Checked)
                        {
                            if (mu.Purse < money) { function.WriteErrMsg("该用户余额不足"); }
                            buser.MinusVMoney(mu.UserID, money, M_UserExpHis.SType.Purse, remind);
                        }
                        payMod.PlatformInfo = plat;
                    }
                    break;
                case "SilverCoin":
                    {
                        if (SyncDeduct_Chk.Checked)
                        {
                            if (mu.SilverCoin < money) { function.WriteErrMsg("该用户银币不足"); }
                            buser.MinusVMoney(mu.UserID, money, M_UserExpHis.SType.SIcon, remind);
                        }
                        payMod.PlatformInfo = plat;
                    }
                    break;
                case "Score":
                    {
                        if (SyncDeduct_Chk.Checked)
                        {
                            if (mu.UserExp < money) { function.WriteErrMsg("该用户积分不足"); }
                            buser.MinusVMoney(mu.UserID, money, M_UserExpHis.SType.Point, remind);
                        }
                        payMod.PlatformInfo = plat;
                    }
                    break;
            }
            payMod.PayPlatID = DataConverter.CLng(plat);
            payMod.MoneyPay = money;
            payMod.Remark = "";
            payMod.SysRemark = "管理员[" + adminName + "]确认支付";
            payMod.PaymentID = payBll.Add(payMod);
            
            orderMod.PaymentNo = payMod.PayNo;
            orderMod.Paymentstatus = (int)M_OrderList.PayEnum.HasPayed;
            orderBll.UpdateByID(orderMod);
            function.Script(this, "parent.CloseDiag();parent.location.reload();");
        }
    }
}