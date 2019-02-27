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
using ZoomLa.BLL.Ebatong;

public partial class PayOnline_EbatongNotify : System.Web.UI.Page
{
    B_Payment payBll = new B_Payment();
    B_PayPlat platBll = new B_PayPlat();
    B_OrderList orderBll = new B_OrderList();
    OrderCommon orderCom = new OrderCommon();
    protected void Page_Load(object sender, EventArgs e)
    {
        string md5sign = Request.Params["sign"];
        string notify_id = Request.Params["notify_id"];
        string out_trade_no = Request.Params["out_trade_no"];
        string subject = Request.Params["subject"];
        string payment_type = Request.Params["payment_type"];
        string trade_status = Request.Params["trade_status"];//交易状态,TRADE_FINISHED 表示成功
        string total_fee = Request.Params["total_fee"];
        string seller_id = Request.Params["seller_id"];
        string[] paramkeys = new string[Request.QueryString.AllKeys.Length];
         Request.QueryString.AllKeys.CopyTo(paramkeys,0);
        Array.Sort(paramkeys);
        string paramstr = "";
        foreach (string item in paramkeys)
        {
            if (!item.Equals("sign"))
            { paramstr += string.Format("{0}={1}&",item,Request.QueryString[item]); }
        }
        M_Payment payMod = payBll.SelModelByPayNo(out_trade_no);
        M_PayPlat platMod = new M_PayPlat();
        platMod = platBll.SelReturnModel(payMod.PayPlatID);
        if (platMod.PayClass != (int)M_PayPlat.Plat.Ebatong) { ZLLog.L(ZLEnum.Log.safe, "回调页面错误" + Request.RawUrl); }
        string sign = new CommonHelper().md5("UTF-8", paramstr.Trim('&') + platMod.MD5Key).ToLower();
        if (sign.Equals(md5sign))
        {
            ZLLog.L(ZLEnum.Log.pay, "贝付:"+subject+" 交易状态:"+trade_status+" 支付单:"+out_trade_no+" 金额:"+total_fee);

            if (!trade_status.Equals("TRADE_FINISHED"))
            { ZLLog.L(ZLEnum.Log.pay, "贝付交易失败!支付单:"+out_trade_no); }
            try
            {
                M_Payment pinfo = payBll.SelModelByPayNo(out_trade_no);
                if (pinfo.Status != (int)M_Payment.PayStatus.NoPay) return;
                pinfo.Status = (int)M_Payment.PayStatus.HasPayed;
                pinfo.PlatformInfo = "贝付通";    //平台反馈信息
                pinfo.SuccessTime = DateTime.Now;//交易成功时间
                pinfo.CStatus = true; //处理状态
                pinfo.MoneyTrue = Convert.ToDouble(total_fee);
                payBll.Update(pinfo);
                DataTable orderDT = orderBll.GetOrderbyOrderNo(pinfo.PaymentNum);
                foreach (DataRow dr in orderDT.Rows)
                {
                    M_Order_PayLog paylogMod = new M_Order_PayLog();
                    M_OrderList orderMod = orderBll.SelModelByOrderNo(dr["OrderNo"].ToString());
                    OrderHelper.FinalStep(pinfo, orderMod, paylogMod);
                    //orderCom.SendMessage(orderMod, paylogMod, "payed");
                    //orderCom.SaveSnapShot(orderMod);
                }
                Response.Write(notify_id);
                ZLLog.L(ZLEnum.Log.pay, "贝付平台支付成功!支付单:" + out_trade_no);
            }
            catch (Exception ex)
            {
                ZLLog.L(ZLEnum.Log.pay, new M_Log()
                {
                    Action = "支付回调报错",
                    Message = "平台:贝付,支付单:" + out_trade_no + ",原因:" + ex.Message
                });
            }
        }
        else
        {
            ZLLog.L(ZLEnum.Log.safe, "贝付通验证失败!支付单:"+out_trade_no);
        }

    }
}