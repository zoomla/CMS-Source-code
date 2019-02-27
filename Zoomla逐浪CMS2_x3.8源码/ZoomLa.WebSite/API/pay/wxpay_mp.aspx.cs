using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using ZoomLa.Common;
using System.Xml;
using ZoomLa.BLL;
using ZoomLa.Model;
using Newtonsoft.Json;
using System.Web.Security;
using ZoomLa.Components;
using ZoomLa.Model.Plat;
using ZoomLa.BLL.Plat;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Text;
using ZoomLa.BLL.WxPayAPI;
//公众号支付,必须在微信浏览器内,通过JS API完成支付申请
public partial class API_pay_wxpay_mp : System.Web.UI.Page
{
    B_Payment payBll = new B_Payment();
    B_WX_APPID appBll = new B_WX_APPID();
    public string timestr = "";
    public string prepay_id = "";
    public string paySign = "";
    public string siteurl = "";
    string notifyUrl = SiteConfig.SiteInfo.SiteUrl + "/payonline/return/WxPayReturn.aspx";
	public string SuccessUrl { get { return Request.QueryString["SuccessUrl"] ?? "/User"; } }
    public M_WX_APPID appMod = new M_WX_APPID();
    public string SiteUrl = SiteConfig.SiteInfo.SiteUrl.Replace(":", "%3a").Replace("/", "%2f");
    public string PayNo { get { return (Request.QueryString["PayNo"] ?? "").Trim(); } }
    public int AppID { get { return DataConverter.CLng(Request.QueryString["appid"]); } }
    public string State { get { return Request.QueryString["state"] ?? ""; } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            appMod = WxPayApi.Pay_GetByID(AppID);
            siteurl = HttpUtility.UrlEncode(SiteConfig.SiteInfo.SiteUrl + "/PayOnline/wxpay_mp.aspx?payno=" + PayNo + "&appid=" + AppID + "&SuccessUrl=" + SuccessUrl);
            //首次进入自动跳转获取支付code
            if (!State.Equals("redirect"))
            {
                string url = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + appMod.Pay_APPID + "&redirect_uri=" + siteurl + "&response_type=code&scope=snsapi_userinfo&state=redirect#wechat_redirect";
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
    }
}