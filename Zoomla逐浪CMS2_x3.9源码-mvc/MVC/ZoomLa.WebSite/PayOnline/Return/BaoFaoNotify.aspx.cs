namespace ZoomLaCMS.PayOnline.Return
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using ZoomLa.BLL;
    using ZoomLa.BLL.API;
    using ZoomLa.BLL.Shop;
    using ZoomLa.Common;
    using ZoomLa.Model;
    //支付宝异步通知页
    public partial class BaoFaoNotify : System.Web.UI.Page
    {
        Pay_BaoFa baofa = new Pay_BaoFa();
        B_Payment payBll = new B_Payment();
        B_PayPlat platBll = new B_PayPlat();
        B_OrderList orderBll = new B_OrderList();
        OrderCommon orderCom = new OrderCommon();
        protected void Page_Load(object sender, EventArgs e)
        {
            string MemberID = Request.Params["MemberID"];//商户号
            string TerminalID = Request.Params["TerminalID"];//商户终端号
            string TransID = Request.Params["TransID"];//商户流水号
            string Result = Request.Params["Result"];//支付结果(1:成功,0:失败)
            string ResultDesc = Request.Params["ResultDesc"];//支付结果描述
            string FactMoney = Request.Params["FactMoney"];//实际成交金额
            string AdditionalInfo = Request.Params["AdditionalInfo"];//订单附加消息
            string SuccTime = Request.Params["SuccTime"];//交易成功时间
            string Md5Sign = Request.Params["Md5Sign"].ToLower();//md5签名
            M_Payment payMod = payBll.SelModelByPayNo(TransID);
            M_PayPlat platMod = new M_PayPlat();
            platMod = platBll.SelReturnModel(payMod.PayPlatID);
            if (platMod.PayClass != (int)M_PayPlat.Plat.BaoFo) { function.WriteErrMsg("回调页面错误"); }
            String mark = "~|~";//分隔符
            string _WaitSign = "MemberID=" + MemberID + mark + "TerminalID=" + TerminalID + mark + "TransID=" + TransID + mark + "Result=" + Result + mark + "ResultDesc=" + ResultDesc + mark
                 + "FactMoney=" + FactMoney + mark + "AdditionalInfo=" + AdditionalInfo + mark + "SuccTime=" + SuccTime
                 + mark + "Md5Sign=" + platMod.MD5Key;
            if (Md5Sign.ToLower() == StringHelper.MD5(_WaitSign).ToLower())
            {
                ZLLog.L(ZLEnum.Log.pay, "宝付:" + ResultDesc + " 支付结果:" + Result + " 支付单:" + TransID + " 金额:" + FactMoney);
                try
                {
                    M_Payment pinfo = payBll.SelModelByPayNo(TransID);
                    if (pinfo.Status != (int)M_Payment.PayStatus.NoPay) return;
                    pinfo.Status = (int)M_Payment.PayStatus.HasPayed;
                    pinfo.PlatformInfo = "宝付在线付款";    //平台反馈信息
                    pinfo.SuccessTime = DateTime.Now;//交易成功时间
                    pinfo.CStatus = true; //处理状态
                    pinfo.MoneyTrue = (Convert.ToDouble(FactMoney) / 100);//其以分为单位
                    payBll.Update(pinfo);
                    DataTable orderDT = orderBll.GetOrderbyOrderNo(pinfo.PaymentNum);
                    foreach (DataRow dr in orderDT.Rows)
                    {
                        M_Order_PayLog paylogMod = new M_Order_PayLog();
                        M_OrderList orderMod = orderBll.SelModelByOrderNo(dr["OrderNo"].ToString());
                        OrderHelper.FinalStep(pinfo, orderMod, paylogMod);
                        orderCom.SendMessage(orderMod, paylogMod, "payed");
                        //orderCom.SaveSnapShot(orderMod);
                    }
                    Response.Write("OK");
                    ZLLog.L(ZLEnum.Log.pay, "宝付平台支付成功!支付单:" + TransID);
                }
                catch (Exception ex)
                {
                    ZLLog.L(ZLEnum.Log.pay, new M_Log()
                    {
                        Action = "支付回调报错",
                        Message = "平台:宝付,支付单:" + TransID + ",原因:" + ex.Message
                    });
                }
            }
            else
            {
                ZLLog.L(ZLEnum.Log.pay, new M_Log()
                {
                    Action = "支付验证失败",
                    Message = "平台:宝付,支付单:" + TransID
                });
                Response.Write("Md5CheckFail");
            }
        }
    }
}