namespace ZoomLaCMS.PayOnline.Return
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Xml;
    using ZoomLa.BLL;
    using ZoomLa.BLL.Shop;
    using ZoomLa.Common;
    using ZoomLa.Model;
    public partial class ICBCNotify : System.Web.UI.Page
    {
        B_Payment payBll = new B_Payment();
        B_PayPlat platBll = new B_PayPlat();
        B_OrderList orderBll = new B_OrderList();
        OrderCommon orderCom = new OrderCommon();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string tranData = Request["notifydata"];
                string sign = Request["signMsg"];
                ZLLog.L(ZLEnum.Log.pay, "工商银行回调:" + tranData + ",,,签名:" + sign);
                //M_PayPlat platMod = platBll.SelModelByClass(M_PayPlat.Plat.ICBC_NC);
                //infosecapiLib.infosecClass obj = new infosecapiLib.infosecClass();
                ////有时其返回的数据是编过码的,需要解码
                ////tranData = obj.base64dec(tranData.Replace("%2B", "+").Replace("%3D", "="));
                ////sign = sign.Replace("%2B", "+").Replace("%3D", "=").Replace("%2F", "/");

                //tranData = obj.base64dec(tranData);
                //int ret = obj.verifySign(tranData, function.VToP(platMod.Other), sign);//使用生产证书解密
                //string payno = "";
                //if (ret != 0 || !GetValue(tranData, "tranStat").Equals("1"))
                //{
                //    //签名错误
                //    ZLLog.L(ZLEnum.Log.pay, "工商签名错误:" + ret + ":" + GetValue(tranData, "tranStat").Equals("1"));
                //    Response.Write("failed"); Response.End();
                //}
                //try
                //{
                //    payno = GetValue(tranData, "orderid");
                //    M_Payment pinfo = payBll.SelModelByPayNo(payno);
                //    if (pinfo.Status != (int)M_Payment.PayStatus.NoPay) return;
                //    pinfo.Status = (int)M_Payment.PayStatus.HasPayed;
                //    pinfo.PlatformInfo = "工商银行";    //平台反馈信息
                //    pinfo.SuccessTime = DateTime.Now;//交易成功时间
                //    pinfo.CStatus = true; //处理状态
                //    pinfo.MoneyTrue = (Convert.ToDouble(GetValue(tranData, "amount")) / 100);
                //    payBll.Update(pinfo);
                //    DataTable orderDT = orderBll.GetOrderbyOrderNo(pinfo.PaymentNum);
                //    foreach (DataRow dr in orderDT.Rows)
                //    {
                //        M_Order_PayLog paylogMod = new M_Order_PayLog();
                //        M_OrderList orderMod = orderBll.SelModelByOrderNo(dr["OrderNo"].ToString());
                //        OrderHelper.FinalStep(pinfo, orderMod, paylogMod);
                //        orderCom.SendMessage(orderMod, paylogMod, "payed");
                //        //orderCom.SaveSnapShot(orderMod);
                //    }
                //    Response.Write("OK");
                //}
                //catch (Exception ex)
                //{
                //    ZLLog.L(ZLEnum.Log.pay, new M_Log()
                //    {
                //        Action = "支付回调报错",
                //        Message = "平台:工商银行,支付单:" + payno + ",原因:" + ex.Message
                //    });
                //}
            }
        }
        public string GetValue(string xml, string nodename)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xml);
                XmlNode node = doc.SelectSingleNode("//" + nodename);
                if (node == null) { return "none"; }
                return node.InnerText;
            }
            catch { return ""; }
        }
    }
}