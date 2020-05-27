using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.MobaoPay;
using ZoomLa.Common;
using ZoomLa.Model;
//磨宝支付异步页(古文化)
public partial class PayOnline_MoBaoReturn : System.Web.UI.Page
{
    private B_Order_PayLog paylogBll = new B_Order_PayLog();//支付日志类,用于记录用户付款信息
    private M_Order_PayLog paylogMod = new M_Order_PayLog();
    private B_OrderList orderBll = new B_OrderList();
    private B_OrderList orderbll = new B_OrderList();
    private B_CartPro cartBll = new B_CartPro();//数据库中购物车业务类
    private B_User buser = new B_User();
    private B_Payment payBll = new B_Payment();
    private OrderCommon orderCom = new OrderCommon();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string spliteFlag = "&signMsg=";
            string requestStr = Request.Form.ToString();
            string srcStr = requestStr.Substring(0, requestStr.IndexOf(spliteFlag));
            string signStr = requestStr.Substring(requestStr.IndexOf(spliteFlag) + spliteFlag.Length);
            string spliteFlag2 = "&notifyType=";//0为页面通知,1为异步通知
            int pos = signStr.IndexOf(spliteFlag2);
            if (pos > 0)
            {
                signStr = signStr.Substring(0, pos);
            }
            else
            {
                pos = srcStr.IndexOf(spliteFlag2);
                if (pos > 0)
                {
                    srcStr = srcStr.Substring(0, pos);
                }
            }
            signStr = System.Web.HttpUtility.UrlDecode(signStr);
            bool verifyResult = Mobo360SignUtil.Instance.verifyData(signStr, srcStr);
            //NotifyType 1:异步,0:同步
            //apiName=PAY_RESULT_NOTIFY&notifyTime=20150327093653&tradeAmt=0.01&merchNo=210001110003583&merchParam=&orderNo=PD12&tradeDate=20150327&accNo=761003&accDate=20150327&orderStatus=1&signMsg=YIfQ7JGp4MIe5hu19lEmWF22aM9xcaL5LqMKlddEv4L7V2vv36qtPKwdS40HOLX1aaVHXgCnwoXnHacrXghRxvM3B1yFuKcCC2q5HnRBnN3Pxg%2bmBUt5WKMJwOC6VbJgqAQvW4UYaubVl7V4TGbAoYGjWuuIFWRthAacPdpK%2bH4%3d&notifyType=1
            //apiName=PAY_RESULT_NOTIFY&notifyTime=20150327094538&tradeAmt=0.01&merchNo=210001110003583&merchParam=&orderNo=PD13&tradeDate=20150327&accNo=761140&accDate=20150327&orderStatus=1&notifyType=0&signMsg=dIUoegWS2HgtHlHwz1i62oYPfGyqgNi5HW%2bew734APG0O9xKLHXbR9atFDxwZpXtovQ7wjPuYgqtwD0R0VsNIr5ceA8dlRETepKo0D8Gi1Z4iGtW3UZ%2f92T8ILMRXEw3fUOMTMxyxk265A0VlAla9pCqMMd8QC5pxNoPWOwKFfo%3d
            #region 校验并更改状态
            //if (verifyResult)//其无法通过自己校验
            //{
              
            //}
            //else { B_Site_Log.Insert("MO宝校验失败", requestStr); }
            string orderNo = Request.Form["orderNo"];//订单号
            string notifyType = Request.Form["notifyType"];
            double tradeAmt = DataConverter.CDouble(Request.Form["tradeAmt"]);
            M_UserInfo mu = buser.GetLogin(false);
            try
            {
                if (notifyType.Equals("0")) { function.WriteErrMsg("支付成功"); return; }
                if (notifyType.Equals("1"))
                {
                    M_Payment pinfo = payBll.SelModelByPayNo(orderNo);
                    if (pinfo.Status == 3) return;
                    pinfo.Status = 3;
                    pinfo.PlatformInfo = "MO宝在线付款";
                    pinfo.SuccessTime = DateTime.Now;
                    pinfo.PayTime = DateTime.Now;
                    pinfo.CStatus = true;
                    pinfo.MoneyTrue = tradeAmt;
                    payBll.Update(pinfo);
                    DataTable orderDT = orderBll.GetOrderbyOrderNo(pinfo.PaymentNum);
                    foreach (DataRow dr in orderDT.Rows)
                    {
                        M_OrderList orderMod = orderBll.SelModelByOrderNo(dr["OrderNo"].ToString());
                        if (orderMod.OrderStatus >= 99) return;//订单已处理,避免重复
                        else { orderBll.UpOrderinfo("Paymentstatus=1,Receivablesamount=" + tradeAmt, orderMod.id); }
                        orderCom.SendMessage(orderMod, paylogMod, "payed");
                        paylogMod.Remind += "订单" + orderMod.OrderNo + "购买生效";
                        FinalStep(orderMod);
                        //-------支付成功处理,并写入日志
                        paylogMod.OrderID = orderMod.id;
                        paylogMod.UserID = mu.UserID;
                        paylogMod.PayMethod = (int)M_Order_PayLog.PayMethodEnum.Other;
                        paylogMod.PayMoney = orderMod.Ordersamount;
                        paylogMod.PayPlatID = 16;
                        paylogBll.insert(paylogMod);
                        Response.Write("SUCCESS"); // 验证签名通过后，商户系统回写“SUCCESS”以表明商户收到了通知
                        B_Site_Log.Insert(pinfo.PaymentNum + ":MO宝处理成功", "状态：" + requestStr);
                    }
                }
            }
            catch (Exception ex) { B_Site_Log.Insert(orderNo + ":MO宝支付处理失败", "原因：" + ex.Message+":"+requestStr); }
            #endregion
        }
    }
    //仅接受充值与普通订单的处理
    private void FinalStep(M_OrderList mod)
    {
        if (mod.Ordertype == (int)M_OrderList.OrderEnum.Purse)
        {
            buser.ChangeVirtualMoney(mod.Userid, new M_UserExpHis() { detail = "文币充值,订单号:" + mod.OrderNo, score = (int)mod.Ordersamount, ScoreType = (int)M_UserExpHis.SType.Point });
            orderBll.UpOrderinfo("OrderStatus=99", mod.id);//成功的订单 
        }
        else
        { orderBll.UpOrderinfo("OrderStatus=99", mod.id); }
    }
}