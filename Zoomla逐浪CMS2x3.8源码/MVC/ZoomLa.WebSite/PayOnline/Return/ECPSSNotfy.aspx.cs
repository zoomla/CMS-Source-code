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
    using ZoomLa.BLL.Ebatong;
    using System.Web.Security;
    public partial class ECPSSNotfy : System.Web.UI.Page
    {
        B_Payment payBll = new B_Payment();
        B_PayPlat platBll = new B_PayPlat();
        B_OrderList orderBll = new B_OrderList();
        OrderCommon orderCom = new OrderCommon();
        protected void Page_Load(object sender, EventArgs e)
        {
            M_PayPlat platmod = platBll.SelModelByClass(M_PayPlat.Plat.ECPSS);
            string md5key = platmod.MD5Key;
            string billno = Request["BillNo"];
            string amount = Request["Amount"];
            string result = Request["Result"];
            string Succeed = Request["Succeed"];
            string signinfo = Request["SignMD5info"];
            string md5src = billno + "&" + amount + "&" + Succeed + "&" + md5key;
            if (signinfo.Equals(FormsAuthentication.HashPasswordForStoringInConfigFile(md5src, "MD5")))
            {
                ZLLog.L(ZLEnum.Log.pay, "汇潮支付 交易状态:" + result + " 支付单:" + billno + " 金额:" + amount);
                if (!Succeed.Equals("88")) { ZLLog.L(ZLEnum.Log.pay, "贝付交易失败!支付单:" + billno); return; }
                try
                {
                    M_Payment pinfo = payBll.SelModelByPayNo(billno);
                    if (pinfo.Status != (int)M_Payment.PayStatus.NoPay) return;
                    pinfo.Status = (int)M_Payment.PayStatus.HasPayed;
                    pinfo.PlatformInfo = "汇潮支付";    //平台反馈信息
                    pinfo.SuccessTime = DateTime.Now;//交易成功时间
                    pinfo.CStatus = true; //处理状态
                    pinfo.MoneyTrue = Convert.ToDouble(amount);
                    payBll.Update(pinfo);
                    DataTable orderDT = orderBll.GetOrderbyOrderNo(pinfo.PaymentNum);
                    foreach (DataRow dr in orderDT.Rows)
                    {
                        M_Order_PayLog paylogMod = new M_Order_PayLog();
                        M_OrderList orderMod = orderBll.SelModelByOrderNo(dr["OrderNo"].ToString());
                        OrderHelper.FinalStep(pinfo, orderMod, paylogMod);
                    }
                    Response.Write("ok");
                    ZLLog.L(ZLEnum.Log.pay, "汇潮平台支付成功!支付单:" + billno);
                }
                catch (Exception ex)
                {
                    ZLLog.L(ZLEnum.Log.pay, new M_Log()
                    {
                        Action = "支付回调报错",
                        Message = "平台:汇潮,支付单:" + billno + ",原因:" + ex.Message
                    });
                    Response.Write("erro");
                }
            }
            else
            {
                ZLLog.L(ZLEnum.Log.safe, "汇潮验证失败!支付单:" + billno);
                Response.Write("erro");
            }
        }
    }
}