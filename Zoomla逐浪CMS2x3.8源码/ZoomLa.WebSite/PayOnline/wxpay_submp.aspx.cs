using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.WxPayAPI;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;
/*
 * 微信子商户公众号支付,openid由服务商获取返回,签名由服务商key生成
 */ 
public partial class PayOnline_wxpay_submp : System.Web.UI.Page
{
    B_Payment payBll = new B_Payment();
    B_WX_APPID appBll = new B_WX_APPID();
    public string timestr = "";
    public string prepay_id = "";
    public string paySign = "";
    public string siteurl = "";
    string notifyUrl = SiteConfig.SiteInfo.SiteUrl + "/payonline/return/WxPayReturn.aspx";
    //public string SuccessUrl { get { return Request.QueryString["SuccessUrl"] ?? "/User"; } }
    public M_WX_APPID appMod = new M_WX_APPID();
    public string SiteUrl = SiteConfig.SiteInfo.SiteUrl.Replace(":", "%3a").Replace("/", "%2f");
    //public string PayNo { get { return (Request.QueryString["PayNo"] ?? "").Trim(); } }
    public string SuccessUrl { get { return Session["wxpay_submp_url"] == null ? "/User" : Session["wxpay_submp_url"].ToString(); } set { Session["wxpay_submp_url"] = value; } }
    public string PayNo { get { return Session["wxpay_submp_payno"]==null?"":Session["wxpay_submp_payno"].ToString(); } set { Session["wxpay_submp_payno"] = value; } }
    public string State { get { return Request.QueryString["state"] ?? ""; } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Payno改为存Session,用于规避其传参
            //appMod = WxPayApi.Pay_GetByID(AppID);
            appMod.APPID = "wx2b5f659a061522fc";
            appMod.Pay_AccountID = "1241317902";//服务商商户号
            appMod.Pay_APPID = "wx2b5f659a061522fc";//服务商APPID
            appMod.Pay_Key = "Lanhu7758258ASlcaop1123585201314";//服务商Key
            //siteurl = HttpUtility.UrlEncode(SiteConfig.SiteInfo.SiteUrl + "/PayOnline/wxpay_submp.aspx?payno=" + PayNo);
            //siteurl = SiteConfig.SiteInfo.SiteUrl + "/PayOnline/wxpay_submp_pay.aspx?payno=" + PayNo;
            siteurl=SiteConfig.SiteInfo.SiteUrl + "/PayOnline/wxpay_submp.aspx";
            //首次进入自动跳转获取支付code
            if (string.IsNullOrEmpty(Request["openid"]))//getOpenid   redirect
            {
                PayNo = Request["PayNo"];
                //https://open.weixin.qq.com/connect/oauth2/authorize?appid=wx2b5f659a061522fc&redirect_uri=".urlencode('http://www.foxweixin.com/getwst/getopenid.html?key=lanhu598498498794655&uri='.urlencode('http://www.baidu.com'))."&response_type=code&scope=snsapi_base&state=getOpenid#wechat_redirect
                //string url = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + appMod.Pay_APPID + "&redirect_uri=" + siteurl + "&response_type=code&scope=snsapi_userinfo&state=redirect#wechat_redirect";
                string url = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + appMod.APPID + "&redirect_uri=" + HttpUtility.UrlEncode("http://www.foxweixin.com/getwst/getopenid.html?key=lanhu598498498794655&uri=") + HttpUtility.UrlEncode(siteurl) + "&response_type=code&scope=snsapi_base&state=getOpenid#wechat_redirect";
                Response.Redirect(url);
            }
            timestr = WxAPI.HP_GetTimeStamp();
            WeiXinPay();
            string stringA = "appId=" + appMod.Pay_APPID + "&nonceStr=" + WxAPI.nonce + "&package=prepay_id=" + prepay_id + "&signType=MD5&timeStamp=" + timestr;
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
        //M_Payment payMod =new M_Payment();
        //payMod.PayNo = payBll.CreatePayNo();
        //payMod.MoneyReal = 0.01;
        //payMod.Remark="TestPay";

        //string code = Request.QueryString["code"];
        //string resultStr = APIHelper.GetWebResult("https://api.weixin.qq.com/sns/oauth2/access_token?appid=" + appMod.APPID + "&secret=" + appMod.Secret + "&code=" + code + "&grant_type=authorization_code");
        //JObject obj = (JObject)JsonConvert.DeserializeObject(resultStr);

        WxPayData wxdata = new WxPayData(appMod.Pay_Key);
        wxdata.SetValue("out_trade_no", payMod.PayNo);
        wxdata.SetValue("body", payMod.Remark);
        wxdata.SetValue("total_fee", (int)(payMod.MoneyReal * 100));
        wxdata.SetValue("trade_type", "JSAPI");
        wxdata.SetValue("notify_url", notifyUrl);
        wxdata.SetValue("product_id", "123");//必填
        wxdata.SetValue("openid", Request["openid"]);//公众号支付必填
        wxdata.SetValue("nonce_str", WxAPI.nonce);
        wxdata.SetValue("sub_mch_id","1395191902");//服务商给予的子商户号

        //获取预支付单号
        WxPayData result = UnifiedOrder(wxdata, appMod);
        if (result.GetValue("return_code").ToString().Equals("FAIL")) { function.WriteErrMsg("商户:" + result.GetValue("return_msg")); }
        prepay_id = result.GetValue("prepay_id").ToString();
    }
    public static WxPayData UnifiedOrder(WxPayData inputObj, M_WX_APPID appMod, int timeOut = 6)
    {
        string url = "https://api.mch.weixin.qq.com/pay/unifiedorder";
        //检测必填参数
        if (!inputObj.IsSet("out_trade_no"))
        {
            throw new WxPayException("缺少统一支付接口必填参数out_trade_no！");
        }
        else if (!inputObj.IsSet("body"))
        {
            throw new WxPayException("缺少统一支付接口必填参数body！");
        }
        else if (!inputObj.IsSet("total_fee"))
        {
            throw new WxPayException("缺少统一支付接口必填参数total_fee！");
        }
        else if (!inputObj.IsSet("trade_type"))
        {
            throw new WxPayException("缺少统一支付接口必填参数trade_type！");
        }

        //关联参数
        if (inputObj.GetValue("trade_type").ToString() == "JSAPI" && !inputObj.IsSet("openid"))
        {
            throw new WxPayException("统一支付接口中，缺少必填参数openid！trade_type为JSAPI时，openid为必填参数！");
        }
        if (inputObj.GetValue("trade_type").ToString() == "NATIVE" && !inputObj.IsSet("product_id"))
        {
            throw new WxPayException("统一支付接口中，缺少必填参数product_id！trade_type为JSAPI时，product_id为必填参数！");
        }

        //异步通知url未设置,必须在上层指定
        if (!inputObj.IsSet("notify_url"))
        {
            //inputObj.SetValue("notify_url", WxPayConfig.NOTIFY_URL);//异步通知url
            throw new Exception("未指定异步回调notify_url");
        }

        inputObj.SetValue("appid", appMod.Pay_APPID);//公众号APPID
        inputObj.SetValue("mch_id", appMod.Pay_AccountID);//商户号
        inputObj.SetValue("spbill_create_ip", "");//终端ip	 
        if (!inputObj.IsSet("nonce_str"))//必须指定
        {
            inputObj.SetValue("nonce_str", WxAPI.nonce);//随机字符串
        }
        //签名
        inputObj.SetValue("sign", inputObj.MakeSign());
        string xml = inputObj.ToXml();
        string response = HttpService.Post(xml, url, false, timeOut);
        WxPayData result = new WxPayData(appMod.Pay_Key);
        result.FromXml(response);
        return result;
    }
}