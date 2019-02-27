namespace ZoomLaCMS.PayOnline.Return
{
    using System;
    using System.Collections;
    using System.Configuration;
    using System.Data;
    using System.Web;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using System.Web.UI.WebControls.WebParts;
    using System.Xml;
    using ZoomLa.Model;
    using ZoomLa.BLL;
    using ZoomLa.Common;
    public partial class Notify : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            return;
            //string Amount = GetFormValue("Amount");//订单金额
            //string PayAmount = GetFormValue("PayAmount");//实际支付金额
            //string OrderNo = GetFormValue("OrderNo");//商户订单号
            //string SerialNo = GetFormValue("serialno");//支付序列号
            //string Status = GetFormValue("Status");//支付状态 "01"表示成功
            //string MerchantNo = GetFormValue("MerchantNo");//商户号
            //string PayChannel = GetFormValue("PayChannel");//实际支付渠道
            //string Discount = GetFormValue("Discount");//实际折扣率
            //string signType = GetFormValue("SignType");//签名方式。1-RSA 2-Md5
            //string PayTime = GetFormValue("PayTime");//支付时间
            //string CurrencyType = GetFormValue("CurrencyType");//货币类型
            //string ProductNo = GetFormValue("ProductNo");//产品编号
            //string ProductDesc = GetFormValue("ProductDesc");//产品描述
            //string Remark1 = GetFormValue("Remark1");//产品备注1
            //string Remark2 = GetFormValue("Remark2");//产品备注2
            //string ExInfo = GetFormValue("ExInfo");//额外的返回信息
            //string MAC = GetFormValue("MAC");//签名字符串



            //string str = Amount + "|" + PayAmount + "|" + OrderNo + "|" + SerialNo + "|" + Status + "|" + MerchantNo + "|" + PayChannel + "|" + Discount + "|" + signType + "|" + PayTime + "|" + CurrencyType + "|" + ProductNo + "|" + ProductDesc + "|" + Remark1 + "|" + Remark2 + "|" + ExInfo;

            //if (signType == "1")
            //{
            //    //RSA
            //    //使用盛大提供的公钥验证签名
            //    SDRSASign ra = new SDRSASign();
            //    if (!ra.VerifySign(str, MAC))
            //    {
            //        //签名失败
            //        throw new Exception("签名验证失败1");
            //    }
            //}
            //else if (signType == "2")
            //{
            //    //MD5
            //    //商户与盛大使用相同的Key
            //    string Key = "your_md5_key";
            //    str += "|" + Key;
            //    string MAC2 = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, System.Web.Configuration.FormsAuthPasswordFormat.MD5.ToString());
            //    if (string.Compare(MAC, MAC2) != 0)
            //    {
            //        throw new Exception("签名验证失败2");
            //    }
            //}
            //else
            //{
            //    throw new Exception("签名验证失败3");
            //}

            ////签名验证成功，需要输出"OK",以通知盛大支付系统:发货通知已收到
            //B_Payment pay = new B_Payment();
            //M_Payment pinfos = pay.SelModelByPayNo(OrderNo);
            //if (pinfos.Status == (int)M_Payment.PayStatus.HasPayed) return;
            //pinfos.Status = (int)M_Payment.PayStatus.HasPayed;
            //pinfos.PlatformInfo = "盛付通即时到账";
            //pinfos.SuccessTime = DateTime.Now;
            //pinfos.MoneyTrue = Convert.ToDouble(PayAmount) / 100;
            //pay.Update(pinfos);


            //B_OrderList oll = new B_OrderList();

            //DataTable orderinfo = oll.GetOrderbyOrderNo(OrderNo);
            //int oid = DataConverter.CLng(orderinfo.Rows[0]["id"]);
            //M_OrderList orinfo = oll.GetOrderListByid(oid);
            //oll.UpOrderinfo("Paymentstatus=1", oid);
            //oll.UpOrderinfo("OrderStatus=1", oid);
            //oll.UpOrderinfo("Receivablesamount=" + DataConverter.CDouble(PayAmount) / 100.00, oid);
        }


        string GetFormValue(string para)
        {
            string str = string.Empty;

            if (Request[para] != null)
                str = Request[para].ToString();

            if (String.IsNullOrEmpty(str))
                return "";
            else
                return str.Trim();
        }
    }
}