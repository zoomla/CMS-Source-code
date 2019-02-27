using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Web;
using ZoomLa.BLL;
using ZoomLa.BLL.WxPayAPI;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;

namespace ZoomLaCMS.API.pay
{
    /*
     * @公众号支付,支付安全目录请指定为/PayOnline/,通过TranServer或路由完成跳转
     */ 
    public partial class wxpay_mp : System.Web.UI.Page
    {
        B_Payment payBll = new B_Payment();
        B_WX_APPID appBll = new B_WX_APPID();
        public string timestr = "";
        public string prepay_id = "";
        public string paySign = "";
        string notifyUrl = SiteConfig.SiteInfo.SiteUrl + "/payonline/return/WxPayReturn.aspx";
        public string SuccessUrl { get { return Request.QueryString["SuccessUrl"] ?? "/User"; } }
        public M_WX_APPID appMod = new M_WX_APPID();
        public string PayNo { get { return (Request.QueryString["PayNo"] ?? "").Trim(); } }
        public int AppID { get { return DataConverter.CLng(Request.QueryString["appid"]); } }
        public string State { get { return Request.QueryString["state"] ?? ""; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                appMod = WxPayApi.Pay_GetByID(AppID);
                if (string.IsNullOrEmpty(appMod.APPID)) {throw new Exception("未设置APPID"); }
                if (string.IsNullOrEmpty(appMod.Secret)) {throw new Exception("未指定Secret"); }
                if (string.IsNullOrEmpty(appMod.Pay_AccountID)) {throw new Exception("未设置商户号"); }
                if (string.IsNullOrEmpty(appMod.Pay_Key)) {throw new Exception("未设置支付Key"); }
               
                //首次进入自动跳转获取支付code
                if (!State.Equals("redirect"))
                {
                    string siteurl = HttpUtility.UrlEncode(SiteConfig.SiteInfo.SiteUrl + "/PayOnline/wxpay_mp.aspx?payno=" + PayNo + "&appid=" + appMod.APPID + "&SuccessUrl=" + SuccessUrl);
                    string url = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=" +appMod.APPID + "&redirect_uri=" + siteurl + "&response_type=code&scope=snsapi_userinfo&state=redirect#wechat_redirect";
                    Response.Redirect(url);
                }
                WeiXinPay();//统一下单,并填充预付单号
                timestr = WxAPI.HP_GetTimeStamp();
                string stringA = "appId=" + appMod.APPID + "&nonceStr=" + WxAPI.nonce + "&package=prepay_id=" + prepay_id + "&signType=MD5&timeStamp=" + timestr;
                string stringSignTemp = stringA + "&key=" + appMod.Pay_Key;
                paySign = StringHelper.MD5(stringSignTemp).ToUpper();
            }
        }
        public void WeiXinPay()
        {
            M_Payment payMod = payBll.SelModelByPayNo(PayNo);
            if (string.IsNullOrEmpty(PayNo)) { function.WriteErrMsg("0x53,支付单号或为空"); }
            else if (payMod == null) { function.WriteErrMsg("支付单号不存在"); }
            else if (payMod.Status != (int)M_Payment.PayStatus.NoPay) { function.WriteErrMsg("0x14,支付单已付过款,不能重复支付"); }
            else if (payMod.MoneyReal <= 0) { function.WriteErrMsg("0x56,支付单金额异常"); }

            string code = Request.QueryString["code"];
            string resultStr = APIHelper.GetWebResult("https://api.weixin.qq.com/sns/oauth2/access_token?appid=" + appMod.APPID + "&secret=" + appMod.Secret + "&code=" + code + "&grant_type=authorization_code");
            JObject obj = (JObject)JsonConvert.DeserializeObject(resultStr);

            WxPayData wxdata = new WxPayData();
            wxdata.SetValue("out_trade_no", payMod.PayNo);
            wxdata.SetValue("body", payMod.Remark);
            wxdata.SetValue("total_fee", (int)(payMod.MoneyReal * 100));
            wxdata.SetValue("trade_type", "JSAPI");
            wxdata.SetValue("notify_url", notifyUrl);
            wxdata.SetValue("product_id", "123");//必填
            wxdata.SetValue("openid", obj["openid"].ToString());//公众号支付必填
            wxdata.SetValue("nonce_str", WxAPI.nonce);

            //获取预支付单号
            WxPayData result = WxPayApi.UnifiedOrder(wxdata, appMod);
            if (result.GetValue("return_code").ToString().Equals("FAIL")) { function.WriteErrMsg("商户" + result.GetValue("return_msg")); }
            prepay_id = result.GetValue("prepay_id").ToString();
            payBll.UpdatePlat(payMod.PaymentID, M_PayPlat.Plat.WXPay, appMod.ID.ToString());
        }
    }
}