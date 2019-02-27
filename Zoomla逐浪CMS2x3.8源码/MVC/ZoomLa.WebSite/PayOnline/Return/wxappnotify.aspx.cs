namespace ZoomLaCMS.PayOnline.Return
{
    using System;
    using System.Data;
    using ZoomLa.BLL;
    using ZoomLa.BLL.Shop;
    using ZoomLa.BLL.WxPayAPI;
    using ZoomLa.Components;
    using ZoomLa.Model;
    public partial class wxappnotify : System.Web.UI.Page
    {
        private string PayPlat = "微信APP支付:";
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
            WxPayData notifyData = wxnotify.GetNotifyData(PlatConfig.WXPay_Key);
            //检查支付结果中transaction_id是否存在
            if (!notifyData.IsSet("out_trade_no"))
            {
                //若transaction_id不存在，则立即返回结果给微信支付后台
                WxPayData res = GetWxDataMod();
                res.SetValue("return_code", "FAIL");
                res.SetValue("return_msg", "支付结果中微信订单号不存在");
                ZLLog.L(ZLEnum.Log.pay, new M_Log()
                {
                    Action = "支付平台异常",
                    Message = PayPlat + "原因:支付结果中微信订单号不存在!"
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
                    Message = PayPlat + ":支付单:" + transaction_id + ",原因:订单查询失败!"
                });
                return res.ToXml();
            }
            //查询订单成功
            else
            {
                WxPayData res = GetWxDataMod();
                res.CheckSign();
                res.SetValue("return_code", "SUCCESS");
                res.SetValue("return_msg", "OK");
                try
                {
                    PayOrder_Success(notifyData);
                }
                catch (Exception ex)
                {
                    ZLLog.L(ZLEnum.Log.pay, new M_Log() { Action = PayPlat + ",支付单:" + transaction_id, Message = ex.Message });
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
                pinfo.PlatformInfo += PayPlat;
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
                    orderCom.SendMessage(orderMod, paylogMod, "payed");
                    //orderCom.SaveSnapShot(orderMod);
                }
                ZLLog.L(ZLEnum.Log.pay, PayPlat + "支付成功,支付单:" + result.GetValue("out_trade_no").ToString());
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
            WxPayData res = OrderQuery(req);
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
        private WxPayData GetWxDataMod() { return new WxPayData(PlatConfig.WXPay_Key); }
        public WxPayData OrderQuery(WxPayData inputObj, int timeOut = 6)
        {
            M_WX_APPID appMod = WxAPI.Code_Get().AppId;//默认取第一个,如有多个的话,请按需修改
            string url = "https://api.mch.weixin.qq.com/pay/orderquery";
            //检测必填参数
            if (!inputObj.IsSet("out_trade_no") && !inputObj.IsSet("transaction_id"))
            {
                throw new WxPayException("订单查询接口中，out_trade_no、transaction_id至少填一个！");
            }
            inputObj.SetValue("appid", appMod.Pay_APPID);//公众帐号ID
            inputObj.SetValue("mch_id", appMod.Pay_AccountID);//商户号
            inputObj.SetValue("nonce_str", WxAPI.nonce);//随机字符串
            inputObj.SetValue("sign", inputObj.MakeSign());//签名
            string xml = inputObj.ToXml();
            string response = HttpService.Post(xml, url, false, timeOut, appMod);//调用HTTP通信接口提交数据
            WxPayData result = GetWxDataMod();
            result.FromXml(response);
            return result;
        }
    }
}