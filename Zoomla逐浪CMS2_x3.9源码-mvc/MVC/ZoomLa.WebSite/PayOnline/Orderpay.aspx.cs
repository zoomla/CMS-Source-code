namespace ZoomLaCMS.PayOnline
{
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
    using System.Net;
    using ZoomLa.Components;
    using ZoomLa.BLL.Shop;

    /*
     * 币种与平台支付均在该页面指定
     * 现金支付的中转页,支付以其金额为准,支持传入订单号(多张订单以,切割)生成支付单或支付单号补完信息
     */
    public partial class Orderpay : System.Web.UI.Page
    {
        B_PayPlat platBll = new B_PayPlat();
        B_Payment payBll = new B_Payment();
        B_User buser = new B_User();
        B_OrderList orderBll = new B_OrderList();
        B_CartPro bcart = new B_CartPro();//加入购物车
        OrderCommon orderCom = new OrderCommon();
        B_MoneyManage moneyBll = new B_MoneyManage();
        double price = 0, fare = 0, arrive = 0, allamount = 0, point = 0;

        //支持以,号分隔,在该页生成支付单
        public string OrderNo { get { return Request.QueryString["OrderCode"]; } }
        //支付单号码
        //根据需要,可在自己页面生成,或传入OrderNo在本页生成
        public string PayNo { get { return Request.QueryString["PayNo"]; } }
        public double Money { get { return DataConverter.CDouble(Request.QueryString["Money"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            B_User.CheckIsLogged(Request.RawUrl);
            if (!IsPostBack)
            {
                M_Payment payMod = new M_Payment();
                DataTable orderDT = new DataTable();
                purseli.Visible = SiteConfig.SiteOption.SiteID.Contains("purse");
                siconli.Visible = SiteConfig.SiteOption.SiteID.Contains("sicon");
                pointli.Visible = SiteConfig.SiteOption.SiteID.Contains("point");
                if (Money > 0)//直接传要充多少，用于充值余额等,生成一条临时记录
                {
                    virtual_ul.Visible = false;
                    orderDT = orderBll.GetOrderbyOrderNo("-1");
                    DataRow dr = orderDT.NewRow();
                    dr["Balance_price"] = Money;
                    dr["Freight"] = 0;
                    dr["Ordersamount"] = Money;
                    orderDT.Rows.Add(dr);
                }
                else if (!string.IsNullOrEmpty(PayNo))
                {
                    payMod = payBll.SelModelByPayNo(PayNo);
                    OrderHelper.OrdersCheck(payMod);
                    orderDT = orderBll.GetOrderbyOrderNo(payMod.PaymentNum);
                }
                else
                {
                    M_OrderList orderMod = orderBll.SelModelByOrderNo(OrderNo);
                    OrderHelper.OrdersCheck(orderMod);
                    orderDT = orderBll.GetOrderbyOrderNo(OrderNo);
                }
                if (orderDT != null && orderDT.Rows.Count > 0)
                {
                    //如果是跳转回来的,检测其是否包含充值订单
                    foreach (DataRow dr in orderDT.Rows)
                    {
                        if (DataConverter.CLng(dr["Ordertype"]) == (int)M_OrderList.OrderEnum.Purse)
                        {
                            virtual_ul.Visible = false; break;
                        }
                    }
                    //总金额,如有支付单,以支付单的为准
                    GetTotal(orderDT, ref price, ref fare, ref allamount);
                    if (!string.IsNullOrEmpty(PayNo))
                    {
                        allamount = (double)payMod.MoneyPay;
                        arrive = payMod.ArriveMoney;
                        point = payMod.UsePoint;
                    }
                    TotalMoney_L.Text = allamount.ToString("f2");
                    TotalMoneyInfo_T.Text = price.ToString("f2") + " + " + fare.ToString("f2") + " - " + arrive.ToString("f2") + "-" + (point * 0.01).ToString("f2") + "(" + point + ")" + " = " + allamount.ToString("f2");
                    TotalMoneyInfo_T.Text = TotalMoneyInfo_T.Text + "　(商品总价+运费-优惠券-积分兑换=总额)";
                    if (string.IsNullOrEmpty(OrderNo) && Money > 0) { OrderNo_L.Text = "用户充值"; }
                    else { OrderNo_L.Text = OrderNo; }
                }
                //支付平台
                BindPlat();
            }
        }
        public void GetTotal(DataTable dt, ref double price, ref double fare, ref double allamount)
        {
            foreach (DataRow dr in dt.Rows)
            {
                price += Convert.ToDouble(dr["Balance_price"]);
                fare += Convert.ToDouble(dr["Freight"]);
                allamount += Convert.ToDouble(dr["Ordersamount"]);
            }
        }
        public string GetPlatImg()
        {
            string image = "";
            switch (DataConverter.CLng(Eval("PayClass")))
            {
                case 1:
                    image = "alipay.jpg";
                    break;
                case 2:
                    image = "99bill.jpg";
                    break;
                case 3:
                    image = "chinabank.jpg";
                    break;
                case 4:
                    image = "tenpay.jpg";
                    break;
                case 5:
                    image = "yeepay.jpg";
                    break;
                case 6:
                    image = "6.jpg";
                    break;
                case 8:
                    image = "15173.jpg";
                    break;
                case 9:
                    image = "chinaunion.jpg";
                    break;
                case 10:
                    image = "sdo.jpg";
                    break;
                case 12:
                    image = "alipayJS.jpg";
                    break;
                case 13:
                    image = "chinapnr.jpg";
                    break;
                case 15://支付宝单网银
                    //M_PayPlat info = new M_PayPlat();
                    //info = platBll.SelModelByClass(M_PayPlat.Plat.Alipay_Bank);
                    //string[] bankArr = info.PayPlatinfo.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    //for (int i = 0; i < bankArr.Length; i++)
                    //{
                    //    image = bankArr[i] + ".jpg";
                    //    ListItem newItem = new ListItem("", bankArr[i]);
                    //    newItem.Attributes.Add("id", "td_" + bankArr[i] + "_" + item.Value);
                    //    if (i > 0)
                    //        ids = item.Value + "" + ids;
                    //    DDLPayPlat.Items.Add(newItem);
                    //}
                    //item.Value = "";
                    break;
                case 14://交通银行
                    image = "transbank.jpg";
                    break;
                case 16://MO宝支付
                    image = "Mobao.jpg";
                    break;
                case 21://微信支付
                    image = "wxpay.jpg";
                    break;
                case 22://宝付
                    image = "baofa.jpg";
                    break;
                case 23://南昌工行
                    image = "ncicbc.jpg";
                    break;
                case 24:
                    image = "95epay.jpg";
                    break;
                case 25:
                    image = "ebatong.jpg";
                    break;
                case 26:
                    image = "CCB.jpg";
                    break;
                case 27://汇潮
                    image = "ecpss.jpg";
                    break;
                case 99://线下支付(转账,汇款)
                    image = "offline.jpg";
                    break;
                case 100://货到付款
                    image = "100.jpg";
                    break;
                default:
                    break;
            }
            return "/App_Themes/User/bankimg/" + image;
        }
        //绑定支付平台信息
        public void BindPlat()
        {
            //后期改为Repeater输出
            DataTable dt = platBll.GetPayPlatListAll(0);
            PayPlat_RPT.DataSource = dt;
            PayPlat_RPT.DataBind();
            if (dt == null || dt.Rows.Count < 1) function.WriteErrMsg("尚未定义支付平台");
            //string image = "";
            //string ids="";
            //foreach (DataRow row in table.Rows)
            //{
            //    ListItem item = new ListItem();
            //    item.Value = row["PayPlatID"].ToString();
            //    item.Text = "";
            //    ids = item.Value+"," + ids;
            //    switch (Convert.ToInt32(row["PayClass"]))
            //    {
            //        case 1:
            //            image = "alipay,.jpg";
            //            item.Attributes.Add("id", "td_alipay_"+item.Value);
            //            break;
            //        case 2:
            //            image = "99bill,.jpg";
            //            item.Attributes.Add("id", "td_99bill_"+item.Value);
            //            break;
            //        case 3:
            //            image = "chinabank,.jpg";
            //            item.Attributes.Add("id", "td_chinabank_"+item.Value);
            //            break;
            //        case 4:
            //            image = "tenpay,.jpg";
            //            item.Attributes.Add("id", "td_tenpay_" + item.Value);
            //            break;
            //        case 5:
            //            image = "yeepay,.jpg";
            //            item.Attributes.Add("id", "td_yeepay_" + item.Value);
            //            break;
            //        case 6:
            //            image = "6,.jpg";
            //            item.Attributes.Add("id", "td_6_" + item.Value);
            //            break;
            //        case 8:
            //            image = "15173,.jpg";
            //            item.Attributes.Add("id", "td_15173_" + item.Value);
            //            break;
            //        case 9:
            //            image = "chinaunion,.jpg";
            //            item.Attributes.Add("id", "td_chinaunion_" + item.Value);
            //            break;
            //        case 10:
            //            image = "sdo,.jpg";
            //            item.Attributes.Add("id", "td_sdo_" + item.Value);
            //            break;
            //        case 12:
            //            image = "alipayJS,.jpg";
            //            item.Attributes.Add("id", "td_alipayJS_" + item.Value);
            //            break;
            //        //case 13:
            //        //    image = "chinapnr,.jpg";
            //        //    item.Attributes.Add("id", "td_chinapnr_" + item.Value);
            //        //    break;
            //        case 15://支付宝单网银
            //            M_PayPlat info = new M_PayPlat();
            //            info = platBll.SelModelByClass(M_PayPlat.Plat.Alipay_Bank);
            //            string[] bankArr = info.PayPlatinfo.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            //            for (int i = 0; i < bankArr.Length; i++)
            //            {
            //                image = bankArr[i] + ",.jpg";
            //                ListItem newItem = new ListItem("", bankArr[i]);
            //                newItem.Attributes.Add("id", "td_" + bankArr[i] + "_" + item.Value);
            //                if(i>0)
            //                    ids = item.Value + "," + ids;
            //                //DDLPayPlat.Items.Add(newItem);
            //            }
            //            item.Value = "";
            //            break;
            //        case 14://交通银行
            //            image = "transbank,.jpg";
            //            item.Attributes.Add("id", "td_transbank_" + item.Value);
            //            break;
            //        case 16://MO宝支付
            //            image = "Mobao,"+image;
            //            item.Attributes.Add("id", "td_Mobao_" + item.Value);
            //            break;
            //        case 21://微信支付
            //            image = "wxpay,.jpg";
            //            item.Attributes.Add("id", "td_wxpay_" + item.Value);
            //            break;
            //        case 22://宝付
            //            image = "baofa,.jpg";
            //            item.Attributes.Add("id", "td_baofa_" + item.Value);
            //            break;
            //        case 23://南昌工行
            //             image = "ncicbc,.jpg";
            //            item.Attributes.Add("id", "td_ncicbc_" + item.Value);
            //            break;
            //        case 24:
            //            image = "95epay,.jpg";
            //            item.Attributes.Add("id", "td_95epay_" + item.Value);
            //            break;
            //        case 25:
            //            image = "ebatong,.jpg";
            //            item.Attributes.Add("id", "td_ebatong_"+item.Value);
            //            break;
            //        case 26:
            //            image = "CCB,.jpg";
            //            item.Attributes.Add("id", "td_CCB_"+item.Value);
            //            break;
            //        case 99://线下支付(转账,汇款)
            //            image = "offline,"+image;
            //            item.Attributes.Add("id", "td_offline_" + item.Value);
            //            break;
            //        case 100://货到付款
            //            image = "100,.jpg";
            //            item.Attributes.Add("id", "td_100_" + item.Value);
            //            break;
            //        default:
            //            item.Text = row["PayPlatName"].ToString();
            //            break;
            //    }
            //    //if (!string.IsNullOrEmpty(item.Value))
            //    //    DDLPayPlat.Items.Add(item);
            //} 
            //if (!string.IsNullOrEmpty(image))
            //{
            //    function.Script(this, "createImage('/App_Themes/User/bankimg/','.jpg".Trim(',') + "','" + ids.Trim(',') + "')");
            //}
            //DDLPayPlat.SelectedIndex = 0;
        }
        //确定,生成信息写入ZL_Payment
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            int platid = DataConverter.CLng(Request.Form["payplat_rad"]);
            M_UserInfo mu = buser.GetLogin(false);
            M_Payment pinfo = new M_Payment();
            M_OrderList orderMod = new M_OrderList();
            if (!string.IsNullOrEmpty(PayNo))//支付单付款
            {
                pinfo = payBll.SelModelByPayNo(PayNo);
                if (pinfo.Status != (int)M_Payment.PayStatus.NoPay) { function.WriteErrMsg("该支付单已付款,请勿重复支付"); return; }
            }
            else
            {
                //传入订单,或直接充值
                if (Money > 0)//直接充值
                {
                    orderMod.OrderNo = B_OrderList.CreateOrderNo(M_OrderList.OrderEnum.Purse);
                    orderMod.Ordertype = (int)M_OrderList.OrderEnum.Purse;//Purse等充值
                    orderMod.Userid = mu.UserID;
                    orderMod.Rename = mu.UserName;
                    orderMod.AddUser = mu.UserName;
                    orderMod.Ordersamount = Money;
                    if (orderMod.Ordersamount < 0.01) function.WriteErrMsg("错误,金额过小");
                    orderBll.Add(orderMod);
                    pinfo.PaymentNum = orderMod.OrderNo;
                    pinfo.MoneyPay = orderMod.Ordersamount;
                    pinfo.Remark = "用户充值";
                }
                else
                {
                    DataTable orderDT = orderBll.GetOrderbyOrderNo(OrderNo);
                    GetTotal(orderDT, ref price, ref fare, ref allamount);
                    if (allamount < 0.01) function.WriteErrMsg("每次划款金额不能低于0.01元");
                    if (orderDT != null && orderDT.Rows.Count > 0)
                    {
                        orderMod = orderBll.GetOrderListByid(DataConverter.CLng(orderDT.Rows[0]["id"]));
                        orderMod.Payment = platid;
                        orderBll.Update(orderMod);
                    }
                    pinfo.PaymentNum = OrderNo;
                    pinfo.MoneyPay = allamount;
                    DataTable cartDT = new DataTable();
                    if (orderMod.id > 0)
                    {
                        cartDT = bcart.GetCartProOrderID(orderMod.id);
                        pinfo.Remark = cartDT.Rows.Count > 1 ? "[" + cartDT.Rows[0]["ProName"] as string + "]等" : cartDT.Rows[0]["ProName"] as string;
                    }
                    else { pinfo.Remark = "没有对应订单"; }
                }
            }
            pinfo.UserID = mu.UserID;
            pinfo.PayPlatID = platid;
            pinfo.MoneyID = 0;
            pinfo.MoneyReal = pinfo.MoneyPay;
            //用于支付宝网银
            pinfo.PlatformInfo = Request.Form["payplat_rad"];
            if (!string.IsNullOrEmpty(PayNo)) { payBll.Update(pinfo); }
            else
            {
                pinfo.Status = (int)M_Payment.PayStatus.NoPay;
                pinfo.PayNo = payBll.CreatePayNo();
                payBll.Add(pinfo);
            }
            string url = "PayOnline.aspx?PayNo=" + pinfo.PayNo;
            if (pinfo.PayPlatID == 0)
            {
                url += "&method=" + Request.Form["payplat_rad"];
            }
            Response.Redirect(url);
        }
    }
}