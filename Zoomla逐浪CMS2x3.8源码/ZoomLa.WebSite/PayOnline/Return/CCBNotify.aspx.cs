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
/// <summary>
/// 建行异步回调页面
/// </summary>
public partial class PayOnline_Return_CCBNotify : System.Web.UI.Page
{
    Pay_BaoFa baofa = new Pay_BaoFa();
    B_Payment payBll = new B_Payment();
    B_PayPlat platBll = new B_PayPlat();
    B_OrderList orderBll = new B_OrderList();
    OrderCommon orderCom = new OrderCommon();
    protected void Page_Load(object sender, EventArgs e)
    {
        string posid = Request.Params["POSID"];//商户柜台代码
        string branchid = Request.Params["BRANCHID"];//分行代码
        string orderid = Request.Params["ORDERID"];//定单号
        string payment = Request.Params["PAYMENT"];//付款金额
        string curcode = Request.Params["CURCODE"];//币种
        string remark1 = Request.Params["REMARK1"];//备注1
        string remark2 = Request.Params["REMARK2"];//备注2
        string acc_type = Request.Params["ACC_TYPE"];//账户类型
        string success = Request.Params["SUCCESS"];
        string sign = Request.Params["SIGN"];//数字签名

        M_Payment payMod = payBll.SelModelByPayNo(branchid);
        M_PayPlat platMod = new M_PayPlat();
        platMod = platBll.SelReturnModel(payMod.PayPlatID);
        if (platMod.PayClass != (int)M_PayPlat.Plat.CCB) { ZLLog.L(ZLEnum.Log.pay, "回调页面错误" + Request.RawUrl); }
        string md5key = string.Format("POSID={0}&BRANCHID={1}&ORDERID={2}&PAYMENT={3}&CURCODE={4}&REMARK1={5}&REMARK2={6}&SUCCESS={7}", posid, branchid, orderid, payment, curcode, remark1, remark2, success);//ACC_TYPE参数(待测试)
        string md5md5 = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(md5key, "MD5").ToLower();
        if (md5md5.Equals(sign))
        {
            if (!success.Equals("Y")) { ZLLog.L(ZLEnum.Log.pay, "建行交易失败!支付单:"+orderid); }
            ZLLog.L(ZLEnum.Log.pay, "建行:"+remark1+" 支付单:"+orderid+" 金额:"+payment);
            try
            {
                M_Payment pinfo = payBll.SelModelByPayNo(orderid);
                if (pinfo.Status != (int)M_Payment.PayStatus.NoPay) return;
                pinfo.Status = (int)M_Payment.PayStatus.HasPayed;
                pinfo.PlatformInfo = "建行支付";    //平台反馈信息
                pinfo.SuccessTime = DateTime.Now;//交易成功时间
                pinfo.CStatus = true; //处理状态
                pinfo.MoneyTrue = Convert.ToDouble(payment);
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
                Response.Write("OK");
                ZLLog.L(ZLEnum.Log.pay, "建行支付成功!支付单:" + orderid);
            }
            catch (Exception ex)
            {
                ZLLog.L(ZLEnum.Log.pay, new M_Log()
                {
                    Action = "支付回调报错",
                    Message = "平台:建行,支付单:" + orderid + ",原因:" + ex.Message
                });
            }
        }
        else
        {
            ZLLog.L(ZLEnum.Log.pay, "建行验证失败!支付单:"+orderid);
        }
    }
}