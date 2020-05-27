using System;
using System.Collections.Generic;
using System.Web;
using ZoomLa.Model;

namespace ZoomLa.BLL.WxPayAPI
{
    /**
    * 	配置帐号信息
    */
    public class WxPayConfig
    {
        //=======【基本信息设置】=====================================
        /* 微信公众号信息配置
        * APPID：绑定支付的APPID（必须配置）
        * MCHID：商户号（必须配置）
        * KEY：商户支付密钥，参考开户邮件设置（必须配置）
        * APPSECRET：公众帐号secert（仅JSAPI支付的时候需要配置）
        */
        static WxPayConfig()
        {
            UpdateByMod(B_PayPlat.GetModelForWx());
        }
        public static void UpdateByMod(M_PayPlat WxPayMod)
        {
            if (WxPayMod != null)
            {
                APPID = WxPayMod.payType;
                MCHID = WxPayMod.AccountID;
                KEY = WxPayMod.MD5Key;
                APPSECRET = WxPayMod.PayPlatinfo;
                SSLCERT_PATH = WxPayMod.SellerEmail;
                SSLCERT_PASSWORD = WxPayMod.leadtoGroup;
            }
        }
        //=======【证书路径设置】===================================== 
        /* 证书路径,注意应该填写绝对路径（仅退款、撤销订单时需要）
        */
        public static string SSLCERT_PATH = "";// "cert/apiclient_cert.p12";
        public static string SSLCERT_PASSWORD = "";// "1233410002";
        //=======【支付结果通知url】===================================== 
        /* 支付结果通知回调url，用于商户接收支付结果(固定页面不给予用户配置)
        */
        //public const string NOTIFY_URL = "http://paysdk.weixin.qq.com/example/ResultNotifyPage.aspx";
        //=======【商户系统后台机器IP】===================================== 
        /* 此参数可手动配置也可在程序中自动获取(可为空)
        */
        public const string IP = "";
        //=======【代理服务器设置】===================================
        /* 默认IP和端口号分别为0.0.0.0和0，此时不开启代理（如有需要才设置）
        */
        public const string PROXY_URL = "http://0.0.0.0:0";
        //=======【上报信息配置】===================================
        /* 测速上报等级，0.关闭上报; 1.仅错误时上报; 2.全量上报
        */
        public const int REPORT_LEVENL = 1;
        //=======【日志级别】===================================
        /* 日志等级，0.不输出日志；1.只输出错误信息; 2.输出错误和正常信息; 3.输出错误信息、正常信息和调试信息
        */
        public const int LOG_LEVENL = 1;
        //--------------------------------------微信PC,公众号支付
        public static string APPID, MCHID, KEY, APPSECRET;
        //--------------------------------------微信APP支付
        public static string APP_APPID, APP_APPSecret, APP_MCHID;
        public static string APP_PayKey = "";//在商户平台--API安全中设置
    }
}