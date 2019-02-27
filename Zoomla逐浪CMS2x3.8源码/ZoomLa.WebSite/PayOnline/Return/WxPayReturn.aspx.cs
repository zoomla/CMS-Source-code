using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Shop;
using ZoomLa.BLL.WxPayAPI;
using ZoomLa.Components;
using ZoomLa.Model;
public partial class PayOnline_WxPayReturn : System.Web.UI.Page
{
    private string PayPlat = "微信PC扫码|公众号支付";
    ZoomLa.BLL.WxPayAPI.Notify wxnotify = null;
    B_Payment payBll = new B_Payment();
    B_Order_PayLog paylogBll = new B_Order_PayLog();//支付日志类,用于记录用户付款信息
    B_OrderList orderBll = new B_OrderList();
    OrderCommon orderCom = new OrderCommon();
    B_User buser = new B_User();
    protected void Page_Load(object sender, EventArgs e)
    {

        wxnotify = new ZoomLa.BLL.WxPayAPI.Notify(this);
        string result = ProcessNotify();
        Response.Clear(); Response.Write(result); Response.Flush(); Response.End();
    }
    public string ProcessNotify()
    {
        //如果有多个公众号支付,此处要取返回中的公众号ID,再取Key验证
        WxPayData notifyData = wxnotify.GetNotifyData(WxPayApi.Pay_GetByID().Pay_Key);
        //检查支付结果中transaction_id是否存在
        if (!notifyData.IsSet("out_trade_no"))
        {
            //若transaction_id不存在，则立即返回结果给微信支付后台
            WxPayData res = GetWxDataMod();
            res.SetValue("return_code", "FAIL");
            res.SetValue("return_msg", "支付结果中订单号不存在");
            ZLLog.L(ZLEnum.Log.pay, new M_Log()
            {
                Action = "支付平台异常",
                Message = PayPlat + ",原因:支付结果中订单号不存在!XML:" + notifyData.ToXml()
            });
            return res.ToXml();
        }
        string transaction_id = notifyData.GetValue("out_trade_no").ToString();
        //查询订单，判断订单真实性
        if (!QueryOrder(transaction_id))
        {
            //若订单查询失败，则立即返回结果给微信支付后台
            WxPayData res = GetWxDataMod();
            res.SetValue("return_code", "FAIL");
            res.SetValue("return_msg", "订单查询失败");
            ZLLog.L(ZLEnum.Log.pay, new M_Log()
            {
                Action = "支付平台异常",
                Message = PayPlat + ",支付单:" + transaction_id + ",原因:订单查询失败!XML:" + notifyData.ToXml()
            });
            return res.ToXml();
        }
        //查询订单成功
        else
        {
            WxPayData res = GetWxDataMod();
            res.SetValue("return_code", "SUCCESS");
            res.SetValue("return_msg", "OK");
            try
            {
                PayOrder_Success(notifyData);
            }
            catch (Exception ex)
            {
                ZLLog.L(ZLEnum.Log.pay, new M_Log() { Action = PayPlat + "报错,支付单:" + transaction_id, Message = ex.Message + "||XML:" + notifyData.ToXml() });
            }
            return res.ToXml();
        }
    }
    //支付成功时执行的操作
    private void PayOrder_Success(WxPayData result)
    {
        ZLLog.L(ZLEnum.Log.pay, PayPlat + " 支付单:" + result.GetValue("out_trade_no") + " 金额:" + result.GetValue("total_fee"));
        try
        {
            M_Order_PayLog paylogMod = new M_Order_PayLog();
            M_Payment pinfo = payBll.SelModelByPayNo(result.GetValue("out_trade_no").ToString());
            if (pinfo == null) { throw new Exception("支付单不存在"); }//支付单检测合为一个方法
            if (pinfo.Status != (int)M_Payment.PayStatus.NoPay) { throw new Exception("支付单状态不为未支付"); }
            pinfo.Status = (int)M_Payment.PayStatus.HasPayed;
            pinfo.PlatformInfo = PayPlat;
            pinfo.SuccessTime = DateTime.Now;
            pinfo.PayTime = DateTime.Now;
            pinfo.CStatus = true;
            //1=100,
            double tradeAmt = Convert.ToDouble(result.GetValue("total_fee")) / 100;
            pinfo.MoneyTrue = tradeAmt;
            payBll.Update(pinfo);
            DataTable orderDT = orderBll.GetOrderbyOrderNo(pinfo.PaymentNum);
            foreach (DataRow dr in orderDT.Rows)
            {
                M_OrderList orderMod = orderBll.SelModelByOrderNo(dr["OrderNo"].ToString());
                OrderHelper.FinalStep(pinfo, orderMod, paylogMod);
                //if (orderMod.Ordertype == (int)M_OrderList.OrderEnum.Purse)
                //{

                //    M_UserInfo mu = buser.SelReturnModel(orderMod.Userid);
                //    new B_Shop_MoneyRegular().AddMoneyByMin(mu, orderMod.Ordersamount, ",订单号[" + orderMod.OrderNo + "]");
                //}
                orderCom.SendMessage(orderMod, paylogMod, "payed");
                //orderCom.SaveSnapShot(orderMod);
            }
            ZLLog.L(ZLEnum.Log.pay, PayPlat + "成功!支付单:" + result.GetValue("out_trade_no").ToString());
        }
        catch (Exception ex)
        {
            ZLLog.L(ZLEnum.Log.pay, new M_Log()
            {
                Action = "支付回调报错",
                Message = PayPlat + ",支付单:" + result.GetValue("out_trade_no").ToString() + ",原因:" + ex.Message
            });
        }
    }
    //查询订单
    private bool QueryOrder(string transaction_id)
    {
        WxPayData req = GetWxDataMod();
        req.SetValue("out_trade_no", transaction_id);
        WxPayData res = WxPayApi.OrderQuery(req,WxPayApi.Pay_GetByID());
        if (res.GetValue("return_code").ToString() == "SUCCESS" &&
            res.GetValue("result_code").ToString() == "SUCCESS")
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private WxPayData GetWxDataMod() { return new WxPayData(); }
}