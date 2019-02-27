using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.API;
using ZoomLa.BLL.User;
using ZoomLa.BLL.WxPayAPI;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;
//微信APP支付
public partial class API_pay_wxpay_app : System.Web.UI.Page
{
    B_Payment payBll = new B_Payment();
    string nonceStr = "5K8264ILTKCH16CQ2502SI8ZNMTM67VS";
    string timestr = "";//统一下单时为订单号
    string ip = "";
    //APP支付,服务端链接可自定义,公众号号则必用指定链接
    string notifyUrl = SiteConfig.SiteInfo.SiteUrl + "/payonline/return/wxappnotify.aspx";
    //------------------------------官方示例,用于验证签名
    //string appid = "wx2421b1c4370ec43b";
    //string secret = "";
    //static string nonceStr = "1add1a30ac87aa2db72f57a2375d8fec";
    //string partnerid = "10000100";//商户号
    //string timestr = "1415659990";
    //string ip = "14.23.150.211";
    //string notifyUrl = "http://wxpay.weixin.qq.com/pub_v2/pay/notify.v2.php";
    M_WX_APPID appMod = null;//请填充该项
    private string PayNo { get { return (Request["PayNo"] ?? "").Trim(); } }
    private string OpenID { get { return (Request["OpenID"] ?? "").Trim(); } }
    protected void Page_Load(object sender, EventArgs e)
    {
      
        M_APIResult retMod = new M_APIResult();
        retMod.retcode = M_APIResult.Failed;
        retMod.callback = Request["callback"];
        if (string.IsNullOrEmpty(PayNo) || string.IsNullOrEmpty(OpenID)) { retMod.retmsg = "0x53,支付单号或OpenID为空"; RepToClient(retMod); }
        try
        {
            M_UserInfo mu = B_User_API.GetLogin(OpenID);
            M_Payment payMod = payBll.SelModelByPayNo(PayNo);
            if (mu == null) { retMod.retmsg = "0x59,用户未登录,或登录已失效"; }
            else if (payMod == null) { retMod.retmsg = "0x82,支付单号不存在"; }
            else if (payMod.Status != (int)M_Payment.PayStatus.NoPay) { retMod.retmsg = "0x14,支付单已付过款,不能重复支付"; }
            else if (payMod.MoneyReal <= 0) { retMod.retmsg = "0x56,支付单金额异常"; }
            //else if (payMod.UserID != mu.UserID) { retMod.retmsg = "x058,该支付单并非你所有"; }
            else
            {
                //*不要看程序内部的报错,直接看Repsonse返回,其才是真正的报错
                TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
                timestr = Convert.ToInt64(ts.TotalSeconds).ToString();
                //------------统一下单,获取预付单号
                WxPayData wxdata = new WxPayData(PlatConfig.WXPay_Key);
                wxdata.SetValue("appid", PlatConfig.WXPay_APPID);
                wxdata.SetValue("attach", "attach");//test
                wxdata.SetValue("body", "pay");//不允许为空,中文需要单独处理
                wxdata.SetValue("mch_id", PlatConfig.WXPay_MCHID);
                wxdata.SetValue("nonce_str", nonceStr);
                wxdata.SetValue("notify_url", notifyUrl);
                wxdata.SetValue("out_trade_no", PayNo);//支付单号
                wxdata.SetValue("spbill_create_ip", ip);//Can be empty
                wxdata.SetValue("total_fee", (payMod.MoneyReal * 100).ToString("f0"));//(payMod.MoneyReal * 100).ToString("f0")
                wxdata.SetValue("trade_type", "APP");

                WxPayData result = UnifiedOrder(wxdata);
                //if (result.GetValue("return_code") != "SUCCESS") { retMod.retmsg = result.GetValue("return_msg").ToString(); RepToClient(retMod); }
                //------------生成返回给APP端的数据
                timestr = Convert.ToInt64(ts.TotalSeconds).ToString();
                WxPayData appdata = new WxPayData(PlatConfig.WXPay_Key);
                appdata.SetValue("appid", PlatConfig.WXPay_APPID);
                appdata.SetValue("partnerid", PlatConfig.WXPay_MCHID);
                appdata.SetValue("package", "Sign=WXPay");
                appdata.SetValue("noncestr", nonceStr);
                appdata.SetValue("timestamp", timestr);
                appdata.SetValue("prepayid", result.GetValue("prepay_id").ToString());
                appdata.SetValue("sign", appdata.MakeSign());

                retMod.result = "{\"appid\":\"" + PlatConfig.WXPay_APPID + "\",\"partnerid\":\"" + PlatConfig.WXPay_MCHID + "\",\"package\":\"Sign=WXPay\",\"noncestr\":\"" + nonceStr + "\",\"timestamp\":" + timestr + ",\"prepayid\":\"" + result.GetValue("prepay_id").ToString() + "\",\"sign\":\"" + appdata.GetValue("sign") + "\"}";
                retMod.retcode = M_APIResult.Success;
                ZLLog.L("生成微信预付单完成" + retMod.result);
                payBll.UpdatePlat(payMod.PaymentID, M_PayPlat.Plat.WXPay, appMod.ID.ToString());
            }
        }
        catch (Exception ex) { retMod.retcode = M_APIResult.Failed; retMod.retmsg = "server exception:" + ex.Message; ZLLog.L("微信预付单报错" + retMod.retmsg); }
        RepToClient(retMod);
        //正确返回
        //<xml><return_code><![CDATA[SUCCESS]]></return_code>
        //<return_msg><![CDATA[OK]]></return_msg>
        //<appid><![CDATA[wxe4703a7618738bed]]></appid>
        //<mch_id><![CDATA[1342097701]]></mch_id>
        //<nonce_str><![CDATA[LYJFMZbGIaa2LezS]]></nonce_str>
        //<sign><![CDATA[440C7616B9658FD7102805895E6808CC]]></sign>
        //<result_code><![CDATA[SUCCESS]]></result_code>
        //<prepay_id><![CDATA[wx201605160903248f02bc5ab10976835276]]></prepay_id>
        //<trade_type><![CDATA[APP]]></trade_type>
        //</xml>
        //转成Json回发给APP
        //{"appid":"wxb4ba3c02aa476ea1","partnerid":"1305176001","package":"Sign=WXPay","noncestr":"717cbf90679a1f26d6b8211efaed3ffb","timestamp":1463394698,"prepayid":"wx20160516183138c3ac06576e0471509227","sign":"414C383FA311DAC74DC882BA89A5E296"}
    }
    private void RepToClient(M_APIResult retMod) { Response.Clear(); Response.Write(retMod.ToString()); Response.Flush(); Response.End(); }
    //APP专用统一下单
    public WxPayData UnifiedOrder(WxPayData wxdata, int timeOut = 6)
    {
        string url = "https://api.mch.weixin.qq.com/pay/unifiedorder";
        //检测必填参数
        if (!wxdata.IsSet("out_trade_no"))
        {
            throw new WxPayException("缺少统一支付接口必填参数out_trade_no！");
        }
        else if (!wxdata.IsSet("body"))
        {
            throw new WxPayException("缺少统一支付接口必填参数body！");
        }
        else if (!wxdata.IsSet("total_fee"))
        {
            throw new WxPayException("缺少统一支付接口必填参数total_fee！");
        }
        else if (!wxdata.IsSet("trade_type"))
        {
            throw new WxPayException("缺少统一支付接口必填参数trade_type！");
        }

        //关联参数
        if (wxdata.GetValue("trade_type").ToString() == "JSAPI" && !wxdata.IsSet("openid"))
        {
            throw new WxPayException("统一支付接口中，缺少必填参数openid！trade_type为JSAPI时，openid为必填参数！");
        }
        if (wxdata.GetValue("trade_type").ToString() == "NATIVE" && !wxdata.IsSet("product_id"))
        {
            throw new WxPayException("统一支付接口中，缺少必填参数product_id！trade_type为JSAPI时，product_id为必填参数！");
        }
        if (!wxdata.IsSet("notify_url"))
        {
            throw new WxPayException("notify_url,的值不能为空");
        }
        //签名
        wxdata.SetValue("sign", wxdata.MakeSign());
        string xml = wxdata.ToXml();
        string response = HttpService.Post(xml, url, false, timeOut,appMod);
        //        <xml><return_code><![CDATA[FAIL]]></return_code>
        //<return_msg><![CDATA[body参数格式错误]]></return_msg>
        //</xml>
        //throw new Exception(response); return null;
        WxPayData result = new WxPayData(appMod.Pay_Key);
        result.FromXml(response);
        return result;
    }
}