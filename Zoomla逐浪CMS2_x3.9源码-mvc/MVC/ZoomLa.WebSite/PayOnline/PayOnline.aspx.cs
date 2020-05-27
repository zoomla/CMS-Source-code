namespace ZoomLaCMS.PayOnline
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Text;
    using ZoomLa.BLL;
    using ZoomLa.BLL.API;
    using ZoomLa.BLL.AlipayBank;
    using ZoomLa.BLL.Ebatong;
    using ZoomLa.BLL.MobaoPay;
    using ZoomLa.BLL.User.Develop;
    using ZoomLa.BLL.Helper;
    using ZoomLa.BLL.WxPayAPI;
    using ZoomLa.BLL.Shop;
    using ZoomLa.Common;
    using ZoomLa.Components;
    using ZoomLa.Model;
    using ZoomLa.Model.User.Develop;
    using ZoomLa.SQLDAL;
    public partial class PayOnline : System.Web.UI.Page
    {

        private M_Payment pinfo = new M_Payment();//支付信息，同于支付日志类,不过只记录用现金，银联等付款
        private M_Order_PayLog paylogMod = new M_Order_PayLog();
        private M_PayPlat payPlat = new M_PayPlat();
        private B_PayPlat payPlatBll = new B_PayPlat();//支付平台
        private B_Payment paymentBll = new B_Payment();
        private B_OrderList orderBll = new B_OrderList();//订单业务类
        private B_CartPro cartBll = new B_CartPro();//数据库中购物车业务类
        private B_Order_PayLog paylogBll = new B_Order_PayLog();//支付日志类,用于记录用户付款信息
        private B_User buser = new B_User();
        private OrderCommon orderCom = new OrderCommon();
        private string urlReq1 = "";//网站路径,用于设置回调页面(Disuse)
        public string PayMethod { get { return Request.QueryString["method"]; } }
        public string PayNo { get { return Request.QueryString["PayNo"]; } }
        /*
         * 仅支持支付单
         */
        protected void Page_Load(object sender, EventArgs e)
        {
            B_User.CheckIsLogged(Request.RawUrl);
            M_UserInfo mu = buser.GetLogin(false);
            if (string.IsNullOrEmpty(SiteConfig.SiteInfo.SiteUrl)) { function.WriteErrMsg("错误,管理员未定义网站地址,SiteUrl"); }
            string siteurl = (SiteConfig.SiteInfo.SiteUrl.TrimEnd('/') + "/PayOnline/");
            if (string.IsNullOrEmpty(PayNo)) { function.WriteErrMsg("请传入支付单号"); }
            pinfo = paymentBll.SelModelByPayNo(PayNo);
            if (pinfo == null || pinfo.PaymentID < 1) { function.WriteErrMsg("支付单不存在"); }
            M_PayPlat payPlat = payPlatBll.GetPayPlatByid(pinfo.PayPlatID);
            if (!IsPostBack)
            {
                #region 母版页中信息
                logged_div.Visible = true;
                #endregion
                if (pinfo.Status != (int)M_Payment.PayStatus.NoPay) { function.WriteErrMsg("支付单不能重复付款"); }
                Rurl_Href.NavigateUrl = "/User/Order/OrderList.aspx";//返回我的订单
                if (pinfo.PaymentNum.Contains("IDC"))
                {
                    Rurl_Href.NavigateUrl = "/Plugins/Domain/ViewHave.aspx";
                }
                string url = Request.CurrentExecutionFilePath;
                urlReq1 = Request.Url.AbsoluteUri.ToString().Substring(0, Request.Url.AbsoluteUri.ToString().LastIndexOf('/'));
                double vmoney = pinfo.MoneyReal;//支付金额 
                string v_amount = pinfo.MoneyReal.ToString("f2"); //实际支付金额
                if (string.IsNullOrEmpty(PayMethod))
                {
                    #region 现金支付
                    DataTable orderDB1 = orderBll.GetOrderbyOrderNo(pinfo.PaymentNum);//订单表,ZL_OrderInfo
                    int orderType = 0;
                    if (orderDB1.Rows.Count > 0)
                    {
                        orderType = DataConvert.CLng(orderDB1.Rows[0]["OrderType"]);
                    }
                    if (orderType == 8)//有需要检测库存的订单类型，放此
                    {
                        if (!cartBll.CheckStock(Convert.ToInt32(orderDB1.Rows[0]["OrderType"])))
                        {
                            function.WriteErrMsg("商品库存数量不足，请重新购买");
                        }
                    }
                    DataTable ordertable = orderBll.GetOrderbyOrderNo(pinfo.PaymentNum);
                    int DeliveryID = 0; //送货方式ID
                    if (ordertable != null && ordertable.Rows.Count > 0)
                    {
                        DeliveryID = DataConverter.CLng(ordertable.Rows[0]["Delivery"]);
                    }
                    if (pinfo.PayPlatID == 0 && !string.IsNullOrEmpty(pinfo.PlatformInfo))//支付宝网银支付 
                    {
                        payPlat = payPlatBll.GetPayPlatByClassID("15");
                        alipayBank(pinfo.PlatformInfo);
                    }
                    if (payPlat.PayClass == 99)//线下支付
                    {
                        function.WriteSuccessMsg("信息已记录,请等待商家联系完成线下付款", Rurl_Href.NavigateUrl);
                    }
                    if (payPlat == null || payPlat.PayPlatID < 1)
                    {
                        function.WriteErrMsg("没有找到对应的支付平台信息!");
                    }
                    StringBuilder v_urlBuilder = new StringBuilder();    //构造返回URL
                    StringBuilder strHiddenField = new StringBuilder();
                    string applicationName = paymentBll.GetApplicationName();
                    if (!applicationName.EndsWith("/"))
                    {
                        applicationName = applicationName + "/";
                    }
                    v_urlBuilder.Append("http://");
                    v_urlBuilder.Append(applicationName);
                    string v_ShowResultUrl = v_urlBuilder.ToString() + "PayOnline/ShowReturn.aspx?";
                    if (payPlat.PayClass == 100)//货到付款
                    {
                        payinfo_div.Visible = false;
                        AfterPay_Tb.Visible = true;
                        Title = "下单成功！";
                    }
                    else
                    {
                        payinfo_div.Visible = true;
                        AfterPay_Tb.Visible = false;
                    }
                    switch ((M_PayPlat.Plat)payPlat.PayClass)//现仅开通 12:支付宝即时到账和支付宝网银服务,15支付宝网银服务(上方代码中处理),银币与余额服务
                    {
                        #region 各种支付方式
                        case M_PayPlat.Plat.UnionPay:
                            #region 银联在线
                            //gateway = "https://pay3.chinabank.com.cn/PayGate?encoding=UTF-8";
                            //必要的交易信息
                            string wv_amount = v_amount;       // 订单金额
                            string wv_moneytype = "CNY";    // 币种
                            string wv_md5info;      // 对拼凑串MD5私钥加密后的值
                            string wv_mid = payPlat.AccountID;		 // 商户号
                            //v_urlBuilder.Append("http://localhost:86/PayOnline/PayReceive.aspx?PayID=" +payid);
                            string wv_url = urlReq1 + "/PayReceive.aspx?PayNo=" + PayNo;		 // 返回页地址
                            string wv_oid = pinfo.PayNo; // 推荐订单号构成格式为 年月日-商户号-小时分钟秒
                            //两个备注

                            // wv_mid = "1001";				 商户号，这里为测试商户号20000400，替换为自己的商户号即可
                            // wv_url = "http://localhost/chinabank/Receive.aspx";  商户自定义返回接收支付结果的页面
                            // MD5密钥要跟订单提交页相同，如Send.asp里的 key = "test" ,修改""号内 test 为您的密钥
                            string wkey = payPlat.MD5Key;				 // 如果您还没有设置MD5密钥请登录我们为您提供商户后台，地址：https://merchant3.chinabank.com.cn/
                            // 登录后在上面的导航栏里可能找到“B2C”，在二级导航栏里有“MD5密钥设置”
                            // 建议您设置一个16位以上的密钥或更高，密钥最多64位，但设置16位已经足够了
                            wv_amount = v_amount;       // 订单金额
                            wv_moneytype = "CNY";    // 币种
                            //对拼凑串MD5私钥加密后的值
                            wv_mid = payPlat.AccountID;		 // 商户号
                            wv_oid = pinfo.PayNo;// 推荐订单号构成格式为 年月日-商户号-小时分钟秒

                            if (wv_oid == null || wv_oid.Equals(""))
                            {
                                DateTime dt = DateTime.Now;
                                string wv_ymd = dt.ToString("yyyyMMdd"); // yyyyMMdd
                                string wtimeStr = dt.ToString("HHmmss"); // HHmmss
                                wv_oid = wv_ymd + wv_mid + wtimeStr;
                            }
                            string text = wv_amount + wv_moneytype + wv_oid + wv_mid + wv_url + wkey; // 拼凑加密串
                            wv_md5info = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(text, "md5").ToUpper();
                            strHiddenField.Append("<input type='hidden' name='v_md5info' value='" + wv_md5info + "'>\n");
                            strHiddenField.Append("<input type='hidden' name='v_mid' value='" + wv_mid + "'>\n");
                            strHiddenField.Append("<input type='hidden' name='v_oid' value='" + wv_oid + "'>\n");
                            strHiddenField.Append("<input type='hidden' name='v_amount' value='" + wv_amount + "' >\n");
                            strHiddenField.Append("<input type='hidden' name='v_moneytype' value='" + wv_moneytype + "'>\n");
                            strHiddenField.Append("<input type='hidden' name='v_url' value='" + wv_url + "'>\n");
                            //    //以下几项只是用来记录客户信息，可以不用，不影响支付 
                            //    strHiddenField.Append("<input type='hidden' name='v_rcvname' value='" + "" + "'>\n");
                            //    strHiddenField.Append("<input type='hidden' name='v_rcvaddr' value='" + "" + "'>\n");
                            //    strHiddenField.Append("<input type='hidden' name='v_rcvtel' value='" + "" + "'>\n");
                            //    strHiddenField.Append("<input type='hidden' name='v_rcvpost' value='" + "" + "' >\n");
                            //    strHiddenField.Append("<input type='hidden' name='v_rcvemail' value='" + "" + "'>\n");
                            //    strHiddenField.Append("<input type='hidden' name='v_rcvmobile' value='" + "" + "'>\n");
                            //    strHiddenField.Append("<input type='hidden' name='v_ordername' value='" + "" + "' >\n");
                            //    strHiddenField.Append("<input type='hidden' name='v_orderaddr' value='" + "" + "'>\n");
                            //    strHiddenField.Append("<input type='hidden' name='v_ordertel' value='" + "" + "'>\n");
                            //    strHiddenField.Append("<input type='hidden' name='v_orderpost' value='" + "" + "' >\n");
                            //    strHiddenField.Append("<input type='hidden' name='v_orderemail' value='" + "" + "'>\n");
                            //    strHiddenField.Append("<input type='hidden' name='v_ordermobile' value='" + "" + "'>\n");
                            //    strHiddenField.Append("<input type='hidden' name='Package' value='" + Request.QueryString["Package"] + "'>\n");
                            #endregion
                            break;
                        case M_PayPlat.Plat.Alipay_Instant:
                            #region 支付宝[即时到帐]
                            string input_charset1 = "utf-8";
                            string notify_url1 = urlReq1 + "/Return/AlipayNotify.aspx";//付完款后服务器AJAX通知的页面 要用 http://格式的完整路径，不允许加?id=123这类自定义参数
                            string return_url1 = urlReq1 + "/Return/AlipayReturn.aspx";//付完款后跳转的页面 要用 http://格式的完整路径，不允许加?id=123这类自定义参数
                            string show_url1 = "";
                            string sign_type1 = "MD5";
                            ///////////////////////以下参数是需要通过下单时的订单数据传入进来获得////////////////////////////////
                            //必填参数
                            string price1 = pinfo.MoneyReal.ToString("f2");//订单总金额，显示在支付宝收银台里的“商品单价”里
                            string logistics_fee1 = "0.00";//物流费用，即运费。
                            string logistics_type1 = "POST";//物流类型，三个值可选：EXPRESS（快递）、POST（平邮）、EMS（EMS）
                            string logistics_payment1 = "SELLER_PAY";//物流支付方式，两个值可选：SELLER_PAY（卖家承担运费）、BUYER_PAY（买家承担运费）
                            string out_trade_no1 = pinfo.PayNo;            //请与贵网站订单系统中的唯一订单号匹配
                            string subject1 = pinfo.Remark;                //订单名称，显示在支付宝收银台里的“商品名称”里，显示在支付宝的交易管理的“商品名称”的列表里。
                            string body1 = pinfo.Remark;                   //订单描述、订单详细、订单备注，显示在支付宝收银台里的“商品描述”里         		
                            string quantity1 = "1";              		   //商品数量，建议默认为1，不改变值，把一次交易看成是一次下订单而非购买一件商品。
                            string receive_name1 = "";                     //收货人姓名，如：张三
                            string receive_address1 = "";		           //收货人地址，如：XX省XXX市XXX区XXX路XXX小区XXX栋XXX单元XXX号
                            string receive_zip1 = "";             		   //收货人邮编，如：123456
                            string receive_phone1 = "";	                   //收货人电话号码，如：0571-81234567
                            string receive_mobile1 = "";           	       //收货人手机号码，如：13312341234
                            //---------------------
                            string receive_name = orderDB1.Rows[0]["Reuser"] + "";                 //收货人姓名，如：张三
                            string receive_address = orderDB1.Rows[0]["Jiedao"] + "";		                //收货人地址，如：XX省XXX市XXX区XXX路XXX小区XXX栋XXX单元XXX号
                            string receive_zip = orderDB1.Rows[0]["ZipCode"] + "";              			    //收货人邮编，如：123456
                            string receive_phone = orderDB1.Rows[0]["Phone"] + "";		    //收货人电话号码，如：0571-81234567
                            string receive_mobile = orderDB1.Rows[0]["MobileNum"] + "";            		    //收货人手机号码，如：13312341234
                            //扩展参数——第二组物流方式
                            //物流方式是三个为一组成组出现。若要使用，三个参数都需要填上数据；若不使用，三个参数都需要为空
                            //有了第一组物流方式，才能有第二组物流方式，且不能与第一个物流方式中的物流类型相同，
                            //即logistics_type="EXPRESS"，那么logistics_type_1就必须在剩下的两个值（POST、EMS）中选择
                            string logistics_fee_3 = "";                					//物流费用，即运费。
                            string logistics_type_3 = "";               					//物流类型，三个值可选：EXPRESS（快递）、POST（平邮）、EMS（EMS）
                            string logistics_payment_3 = "";           					    //物流支付方式，两个值可选：SELLER_PAY（卖家承担运费）、BUYER_PAY（买家承担运费）

                            //扩展参数——第三组物流方式
                            //物流方式是三个为一组成组出现。若要使用，三个参数都需要填上数据；若不使用，三个参数都需要为空
                            //有了第一组物流方式和第二组物流方式，才能有第三组物流方式，且不能与第一组物流方式和第二组物流方式中的物流类型相同，
                            //即logistics_type="EXPRESS"、logistics_type_1="EMS"，那么logistics_type_2就只能选择"POST"
                            string logistics_fee_4 = "";                					//物流费用，即运费。
                            string logistics_type_4 = "";               					//物流类型，三个值可选：EXPRESS（快递）、POST（平邮）、EMS（EMS）
                            string logistics_payment_4 = "";            					//物流支付方式，两个值可选：SELLER_PAY（卖家承担运费）、BUYER_PAY（买家承担运费）
                            //扩展功能参数——其他
                            string buyer_email1 = "";                    					//默认买家支付宝账号
                            string discount1 = "";                       					//折扣，是具体的金额，而不是百分比。若要使用打折，请使用负数，并保证小数点最多两位数
                            /////////////////////////////////////////////////////////////////////////////////////////////////////
                            //构造请求函数，无需修改
                            B_AliPay_trades_Services aliService1 = new B_AliPay_trades_Services(
                            payPlat.AccountID,
                            payPlat.SellerEmail,
                            return_url1,
                            notify_url1,
                            show_url1,
                            out_trade_no1,
                            subject1,
                            body1,
                            price1,
                            logistics_fee1,
                            logistics_type1,
                            logistics_payment1,
                            quantity1,
                            receive_name1,
                            receive_address1,
                            receive_zip1,
                            receive_phone1,
                            receive_mobile1,
                            logistics_fee_3,
                            logistics_type_3,
                            logistics_payment_3,
                            logistics_fee_4,
                            logistics_type_4,
                            logistics_payment_4,
                            buyer_email1,
                            discount1,
                            payPlat.MD5Key,
                            input_charset1,
                            sign_type1);
                            Alipay_Btn.Visible = true;
                            LblHiddenValue.InnerHtml = aliService1.Build_Form();
                            #endregion
                            break;
                        case M_PayPlat.Plat.Alipay_Bank://支付宝网银,已上方处理
                            break;
                        case M_PayPlat.Plat.Mobo:
                            #region 成都MO宝支付
                            {
                                string pfxFilePath = Server.MapPath("/muzi.pfx");
                                string cerFilePath = Server.MapPath("/mobaopay.cer");
                                string passwd = payPlat.MD5Key;
                                if (!File.Exists(pfxFilePath) || !File.Exists(cerFilePath)) { function.WriteErrMsg("证书文件不存在"); }
                                Mobo360SignUtil.Instance.init(pfxFilePath, cerFilePath, passwd);
                                //--------------------------------------------------------------------
                                Dictionary<string, string> payData = new Dictionary<string, string>();
                                payData.Add("apiName", "WEB_PAY_B2C");
                                payData.Add("apiVersion", "1.0.0.0");
                                payData.Add("platformID", payPlat.PayPlatinfo);//平台ID,商户号
                                payData.Add("merchNo", payPlat.AccountID);
                                payData.Add("orderNo", pinfo.PayNo);//订单号,
                                payData.Add("tradeDate", DateTime.Now.ToString("yyyyMMdd"));
                                payData.Add("amt", pinfo.MoneyReal.ToString("f2"));
                                payData.Add("merchUrl", urlReq1 + "/Return/MoBaoReturn.aspx");
                                payData.Add("merchParam", "");
                                payData.Add("tradeSummary", pinfo.Remark);
                                payData.Add("signMsg", Mobo360SignUtil.Instance.sign(payData));
                                string payReqUrl = Mobo360Merchant.Instance.generatePayUrl(payData, "https://trade.mobaopay.com/cgi-bin/netpayment/pay_gate.cgi");
                                Response.ContentEncoding = System.Text.Encoding.UTF8;
                                Alipay_Btn.Visible = true;
                                LblHiddenValue.InnerHtml = new Pay_BaoFa().BuildForm(payReqUrl, null);
                                //Response.Redirect(payReqUrl);// 重定向客户端到收银台
                                //Confirm_VMoney_Btn.Visible = true;
                            }
                            #endregion
                            break;
                        case M_PayPlat.Plat.WXPay:
                            #region 微信扫码支付
                            {
                                WxPayData wxdata = new WxPayData();
                                wxdata.SetValue("out_trade_no", pinfo.PayNo);
                                wxdata.SetValue("body", pinfo.Remark);
                                wxdata.SetValue("total_fee", Convert.ToInt32(pinfo.MoneyReal * 100));
                                wxdata.SetValue("trade_type", "NATIVE");
                                wxdata.SetValue("notify_url", urlReq1 + "/Return/WxPayReturn.aspx");
                                wxdata.SetValue("product_id", "1");
                                WxPayData result = WxPayApi.UnifiedOrder(wxdata, WxPayApi.Pay_GetByID());
                                if (result.GetValue("return_code").ToString().Equals("FAIL"))
                                    function.WriteErrMsg("商户" + result.GetValue("return_msg"));
                                Response.Redirect("/PayOnline/WxCodePay.aspx?PayNo=" + pinfo.PayNo + "&wxcode=" + result.GetValue("code_url"));
                            }
                            #endregion
                            break;
                        case M_PayPlat.Plat.BaoFo:
                            #region 宝付
                            {
                                Pay_BaoFa baofaBll = new Pay_BaoFa();
                                //测试使用
                                payPlat.AccountID = "100000178";
                                payPlat.MD5Key = "abcdefg";
                                payPlat.PayPlatinfo = "10000001";
                                Dictionary<string, string> dics = new Dictionary<string, string>();
                                dics.Add("MemberID", payPlat.AccountID);//"100000178",
                                dics.Add("PayID", "");
                                dics.Add("TradeDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
                                dics.Add("TerminalID", payPlat.PayPlatinfo);//19534 
                                dics.Add("InterfaceVersion", "4.0");
                                dics.Add("KeyType", "1");
                                dics.Add("TransID", pinfo.PayNo);
                                dics.Add("OrderMoney", (pinfo.MoneyReal * 100).ToString("f0"));//金额必须相等,否则会报签名失败
                                dics.Add("ProductName", pinfo.Remark);
                                dics.Add("Amount", pinfo.MoneyReal.ToString("f2"));
                                dics.Add("Username", mu.UserName);
                                dics.Add("AdditionalInfo", "");//可填sessionid等
                                dics.Add("PageUrl", siteurl + "/Return/BaoFaoShow.aspx");
                                dics.Add("ReturnUrl", siteurl + "/Return/BaoFaoNotify.aspx");//是否必须正式才会有回调
                                dics.Add("NoticeType", "1");
                                string signature = baofaBll.GetMd5Sign(dics["MemberID"], dics["PayID"], dics["TradeDate"]
                                   , dics["TransID"], dics["OrderMoney"]
                                   , dics["PageUrl"], dics["ReturnUrl"], dics["NoticeType"], payPlat.MD5Key);
                                dics.Add("Signature", signature);
                                Alipay_Btn.Visible = true;//测试URL:http://tgw.bfopay.com/payindex   //正式:http://gw.bfopay.com/payindex
                                LblHiddenValue.InnerHtml = baofaBll.BuildForm("http://tgw.bfopay.com/payindex", dics);
                            }
                            #endregion
                            break;
                        case M_PayPlat.Plat.EPay95:
                            #region 双乾
                            {
                                Dictionary<string, string> epay_dics = new Dictionary<string, string>();
                                epay_dics.Add("MerNo", payPlat.AccountID);
                                epay_dics.Add("BillNo", pinfo.PayNo);
                                epay_dics.Add("Amount", pinfo.MoneyReal.ToString("f2"));
                                epay_dics.Add("ReturnURL", siteurl + "Return/EPay95Result.aspx");
                                epay_dics.Add("NotifyURL", siteurl + "Return/EPay95Notify.aspx");
                                string EpayMD5Key = payPlat.MD5Key;
                                string md5md5 = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(EpayMD5Key, "MD5").ToUpper();
                                //注意这里和示例不同,其示例是错误的.
                                string md5src = StringHelper.MD5("Amount=" + pinfo.MoneyReal.ToString("f2") + "&BillNo=" + pinfo.PayNo + "&MerNo=" + payPlat.AccountID + "&ReturnURL=" + siteurl + "EPay95Result.aspx" + "&" + md5md5).ToUpper();
                                epay_dics.Add("MD5info", md5src);
                                epay_dics.Add("PayType", "CSPAY");
                                epay_dics.Add("MerRemark", "双乾支付");
                                epay_dics.Add("products", pinfo.Remark);
                                LblHiddenValue.InnerHtml = new Pay_BaoFa().BuildForm("https://www.95epay.cn/sslpayment", epay_dics);
                                Alipay_Btn.Visible = true;
                            }
                            #endregion
                            break;
                        case M_PayPlat.Plat.Ebatong:
                            #region Ebatong
                            {
                                Dictionary<string, string> ebatong_dics = new Dictionary<string, string>();
                                ebatong_dics.Add("sign_type", "MD5");
                                ebatong_dics.Add("service", "create_direct_pay_by_user");
                                ebatong_dics.Add("partner", payPlat.AccountID);
                                ebatong_dics.Add("input_charset", "UTF-8");
                                ebatong_dics.Add("notify_url", siteurl + "Return/EbatongNotify.aspx");//服务器异步通知页面路径
                                ebatong_dics.Add("return_url", siteurl + "Return/EbatongReturn.aspx");//服务器跳转页面
                                ebatong_dics.Add("out_trade_no", pinfo.PayNo);
                                ebatong_dics.Add("subject", pinfo.Remark);
                                ebatong_dics.Add("exter_invoke_ip", Request.UserHostAddress);
                                ebatong_dics.Add("payment_type", "1");
                                ebatong_dics.Add("seller_id", payPlat.AccountID);
                                ebatong_dics.Add("total_fee", pinfo.MoneyReal.ToString("f2"));
                                ebatong_dics.Add("error_notify_url", "");
                                ebatong_dics.Add("anti_phishing_key", new ZoomLa.BLL.Ebatong.AskForTimestamp().askFor(payPlat.AccountID, payPlat.MD5Key));
                                ebatong_dics.Add("seller_email", "");
                                ebatong_dics.Add("buyer_email", "");
                                ebatong_dics.Add("buyer_id", "");
                                ebatong_dics.Add("price", "");
                                ebatong_dics.Add("quantity", "");
                                ebatong_dics.Add("body", "");
                                ebatong_dics.Add("show_url", "");
                                ebatong_dics.Add("pay_method", "bankPay");
                                ebatong_dics.Add("extra_common_para", "");
                                ebatong_dics.Add("extend_param", "");
                                ebatong_dics.Add("it_b_pay", "");
                                ebatong_dics.Add("royalty_type", "");
                                ebatong_dics.Add("royalty_parameters", "");
                                ebatong_dics.Add("default_bank", "");
                                string[] paramts = new string[ebatong_dics.Keys.Count];//参数排序数组
                                ebatong_dics.Keys.CopyTo(paramts, 0);
                                Array.Sort(paramts);//参数排序操作
                                string paramstr = "";
                                foreach (string item in paramts)
                                {
                                    paramstr += string.Format("{0}={1}&", item, ebatong_dics[item]);
                                }
                                //throw new Exception(paramstr.Trim('&'));
                                string md5md5 = new ZoomLa.BLL.Ebatong.CommonHelper().md5("UTF-8", paramstr.Trim('&') + payPlat.MD5Key).ToLower();
                                ebatong_dics.Add("sign", md5md5);
                                LblHiddenValue.InnerHtml = new Pay_BaoFa().BuildForm("https://www.ebatong.com/direct/gateway.htm", ebatong_dics);
                                Alipay_Btn.Visible = true;
                            }
                            #endregion
                            break;
                        case M_PayPlat.Plat.CCB:
                            #region 江西建行
                            {
                                Dictionary<string, string> ccb_dics = new Dictionary<string, string>();
                                ccb_dics.Add("MERCHANTID", payPlat.AccountID);//商户代码
                                ccb_dics.Add("POSID", payPlat.PrivateKey);//柜台代码
                                ccb_dics.Add("BRANCHID", payPlat.PublicKey);//分行代码
                                ccb_dics.Add("ORDERID", pinfo.PayNo);
                                ccb_dics.Add("PAYMENT", pinfo.MoneyReal.ToString("f2"));
                                ccb_dics.Add("CURCODE", "01");
                                ccb_dics.Add("REMARK1", "");//备注信息1(具体信息待测试)
                                ccb_dics.Add("REMARK2", "");//备注信息2
                                ccb_dics.Add("TXCODE", "520100");
                                string ccb_paramstr = "";
                                foreach (String item in ccb_dics.Keys)
                                {
                                    ccb_paramstr += string.Format("{0}={1}&", item, ccb_dics[item]);
                                }
                                string md5str = new ZoomLa.BLL.Ebatong.CommonHelper().md5("UTF-8", ccb_paramstr.Trim('&')).ToLower();
                                ccb_dics.Add("MAC", md5str);
                                LblHiddenValue.InnerHtml = new Pay_BaoFa().BuildForm("https://ibsbjstar.ccb.com.cn/app/ccbMain", ccb_dics);
                                Alipay_Btn.Visible = true;
                                //Response.Redirect("https://ibsbjstar.ccb.com.cn/app/ccbMain?" + ccb_paramstr + "MAC=" + md5str);
                            }
                            #endregion
                            break;
                        case M_PayPlat.Plat.ECPSS:
                            #region 汇潮支付
                            {
                                Dictionary<string, string> payData = new Dictionary<string, string>();
                                payData.Add("OrderDesc", "test");//订单描述
                                payData.Add("Remark", "汇潮支付");//备注
                                payData.Add("AdviceURL", siteurl + "Return/ECPSSNotfy.aspx");//回调通知地址
                                payData.Add("ReturnURL", siteurl + "Return/ECPSSResult.aspx");//返回地址
                                payData.Add("BillNo", pinfo.PayNo);//订单号
                                payData.Add("MerNo", payPlat.AccountID);//商户号
                                payData.Add("Amount", pinfo.MoneyReal.ToString("f2"));//交易价格
                                string md5key = payPlat.MD5Key;
                                string md5str = payData["MerNo"] + "&" + payData["BillNo"] + "&" + payData["Amount"] + "&" + payData["ReturnURL"] + "&" + md5key;
                                payData.Add("SignInfo", System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(md5str, "MD5"));//签名
                                payData.Add("defaultBankNumber", "");//银行代码(选填)
                                payData.Add("orderTime", DateTime.Now.ToString("yyyyMMddHHmmss"));//交易时间yyyyMMddHHmmss
                                payData.Add("products", pinfo.Remark);//物品信息
                                LblHiddenValue.InnerHtml = new Pay_BaoFa().BuildForm("https://pay.ecpss.com/sslpayment", payData);
                                Alipay_Btn.Visible = true;
                            }
                            #endregion
                            break;
                        case M_PayPlat.Plat.ICBC_NC:
                            #region 南昌工行
                            {
                                function.WriteErrMsg("工行支付组件未注册,请联系管理员");
                                //ICBCHelper icbc = new ICBCHelper();
                                //infosecapiLib.infosecClass obj = new infosecapiLib.infosecClass();
                                //Dictionary<string, string> dics = new Dictionary<string, string>();
                                //string posturl = "https://B2C.icbc.com.cn/servlet/ICBCINBSEBusinessServlet";
                                //---Debug
                                //payPlat.AccountID = "1502EC24392836";
                                //payPlat.SellerEmail = "1502201009004747554";
                                //payPlat.PrivateKey = "/Cert/cs.key";
                                //payPlat.PublicKey = "/Cert/cs.cer";
                                //posturl = "https://myipad.dccnet.com.cn/servlet/NewB2cMerPayReqServlet";
                                //throw new Exception(payPlat.PrivateKey + ":" + payPlat.PublicKey + ":" + payPlat.AccountID + ":" + payPlat.SellerEmail);
                                //Debug End
                                //*.z01.com根据需要更改如*.hx008.com
                                //string data = icbc.SpliceTranData(pinfo, payPlat.AccountID.Trim(), payPlat.SellerEmail.Trim(), "*.z01.com", siteurl + "ICBCNotify.aspx");
                                //string sign = obj.sign(data, Server.MapPath(payPlat.PrivateKey), payPlat.MD5Key.Trim());//私钥虚拟路径与私钥密钥
                                //dics.Add("interfaceName", "ICBC_PERBANK_B2C");
                                //dics.Add("interfaceVersion", "1.0.0.11");
                                //dics.Add("tranData", obj.base64enc(data));
                                //dics.Add("merSignMsg", sign);
                                //dics.Add("merCert", icbc.ReadCertToBase64(payPlat.PublicKey));//公钥路径
                                //LblHiddenValue.InnerHtml = new Pay_BaoFa().BuildForm(posturl, dics);
                                //Alipay_Btn.Visible = true;
                            }
                            #endregion
                            break;
                        case M_PayPlat.Plat.CashOnDelivery:
                            #region 货到付款
                            zfpt.Text = payPlat.PayPlatName;
                            ddh.Text = pinfo.PaymentNum;
                            PayNum_L2.Text = Convert.ToDecimal(vmoney).ToString("F2") + " 元";
                            sxf.Text = payPlat.Rate.ToString() + " %";
                            sjhbje.Text = v_amount + " 元";
                            #endregion
                            break;
                        default:
                            throw new Exception("错误:支付方式不存在");
                            #endregion
                    }
                    VMoneyPayed_L.Text = payPlat.PayPlatName;
                    OrderNo_L.Text = pinfo.PaymentNum;
                    LblRate.Text = payPlat.Rate.ToString() + " %";
                    #endregion
                }
                else//非现金支付,给用户显示确认支付页,实际支付行为在Confirm_Click
                {
                    Confirm_VMoney_Btn.Visible = true;
                    payinfo_div.Visible = true;
                    AfterPay_Tb.Visible = false;
                    OrderNo_L.Text = pinfo.PaymentNum;
                    DataTable ordertable = orderBll.GetOrderbyOrderNo(pinfo.PaymentNum);
                    if (ordertable != null && ordertable.Rows.Count > 0)
                    {
                        switch (PayMethod)
                        {
                            case "Purse":
                                //Titles.Text = "余额支付操作（确认支付款项)";
                                Fee.Text = "帐户余额：";
                                LblRate.Text = mu.Purse + " 元";
                                VMoneyPayed_L.Text = "帐户余额";
                                break;
                            case "SilverCoin":
                                //Titles.Text = "银币支付操作（确认支付款项)";
                                Fee.Text = "帐户银币：";
                                LblRate.Text = mu.SilverCoin + " 个";
                                VMoneyPayed_L.Text = "账户银币";
                                break;
                            case "Score":
                                //Titles.Text = "积分支付操作（确认支付款项)";
                                Fee.Text = "帐户积分：";
                                LblRate.Text = mu.UserExp + " 分";
                                VMoneyPayed_L.Text = "账户积分";
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        payinfo_div.Visible = false;
                        AfterPay_Tb.Visible = false;
                        function.WriteErrMsg("订单不存在");
                    }
                }
                //显示金额信息
                LblPayMoney.Text = pinfo.MoneyReal.ToString("F2") + " 元";//应付金额
                //string priceJson = orderCom.GetTotalJson(ordertable);//多虚拟币支付,暂取消
                //M_LinPrice priceMod = JsonConvert.DeserializeObject<M_LinPrice>(priceJson);
                //if (orderCom.HasPrice(priceJson))
                //{
                //    if (priceMod.Purse > 0)
                //    {
                //        LblPayMoney.Text += " 余额:" + priceMod.Purse.ToString("f2");
                //    }
                //    if (priceMod.Sicon > 0)
                //    { LblPayMoney.Text += " 银币:" + priceMod.Sicon.ToString("f2"); }
                //    if (priceMod.Point > 0)
                //    {
                //        LblPayMoney.Text += " 积分:" + priceMod.Point.ToString("f2");
                //    }
                //}
            }
            //------------------检测End;
        }
        //虚拟币确认
        protected void Confirm_Click(object sender, EventArgs e)
        {
            M_UserInfo mu = buser.GetLogin(false);
            DataTable orderDT = new DataTable();
            M_Payment paymentMod = new M_Payment();
            orderDT = orderBll.GetOrderbyOrderNo(pinfo.PaymentNum);
            //-------------------
            #region 扣除附加币,暂注释
            //string priceJson = orderCom.GetTotalJson(orderDT);
            //if (orderCom.HasPrice(priceJson))//扣除附加
            //{
            //    M_LinPrice priceMod = JsonConvert.DeserializeObject<M_LinPrice>(priceJson);
            //    if (mu.Purse < priceMod.Purse)
            //    {
            //        function.WriteErrMsg("你的余额仅有" + mu.Purse + ",不够支付");
            //    }
            //    if (mu.SilverCoin < priceMod.Sicon)
            //    {
            //        function.WriteErrMsg("你的银币仅有" + mu.Purse + ",不够支付");
            //    }
            //    if (mu.UserExp < priceMod.Point)
            //    {
            //        function.WriteErrMsg("你的积分仅有" + mu.Purse + ",不够支付");
            //    }
            //    //将判断与扣减分开,避免出现扣减失误情况
            //    if (priceMod.Purse > 0)
            //    { buser.ChangeVirtualMoney(mu.UserID, new M_UserExpHis() { score = (int)-priceMod.Purse, ScoreType = 1, detail = pinfo.PaymentNum + "商城购物使用" + priceMod.Purse }); }
            //    if (priceMod.Sicon > 0)
            //    { buser.ChangeVirtualMoney(mu.UserID, new M_UserExpHis() { score = (int)-priceMod.Sicon, ScoreType = 2, detail = pinfo.PaymentNum + "商城购物使用" + priceMod.Sicon }); }
            //    if (priceMod.Point > 0)
            //    { buser.ChangeVirtualMoney(mu.UserID, new M_UserExpHis() { score = (int)-priceMod.Point, ScoreType = 3, detail = pinfo.PaymentNum + "商城购物使用" + priceMod.Point }); }
            //}
            #endregion
            //--------------------------------------------
            if (string.IsNullOrEmpty(PayMethod) && !string.IsNullOrEmpty(PayNo))//现金支付,跳转
            {

            }
            else if (!string.IsNullOrEmpty(PayMethod))//虚拟币支付
            {
                paymentMod = paymentBll.SelModelByPayNo(PayNo);
                PayByVirtualMoney(PayMethod, paymentMod);
            }
            else { function.WriteErrMsg("支付出错,支付单不存在"); }
        }
        //支付单虚拟币付款
        private void PayByVirtualMoney(string payMethod, M_Payment payMod)
        {
            M_UserInfo mui = buser.GetLogin(false);
            List<M_OrderList> orderList = OrderHelper.OrdersCheck(payMod);
            ActualAmount.Visible = false;
            paylogMod.UserID = mui.UserID;
            switch (payMethod)//完成支付
            {
                case "Purse":
                    if (!SiteConfig.SiteOption.SiteID.Contains("purse")) { function.WriteErrMsg("管理员已关闭余额支付功能!"); }
                    if (mui.Purse < (double)payMod.MoneyReal) { function.WriteErrMsg("对不起,余额不足! 请<a href='/PayOnline/OrderPay.aspx?Money=" + payMod.MoneyReal + "' target='_blank' style='margin-left:5px;color:#f00;'>充值!</a>"); }
                    buser.ChangeVirtualMoney(mui.UserID, new M_UserExpHis()
                    {
                        score = -(double)payMod.MoneyReal,
                        ScoreType = (int)M_UserExpHis.SType.Purse,
                        detail = "支付成功,支付单号:" + payMod.PayNo
                    });
                    mui = buser.GetLogin(false);
                    zfpt.Text = "余额";
                    fees.Text = "帐户余额：";
                    sxf.Text = mui.Purse + " 元";
                    break;
                case "SilverCoin":
                    if (!SiteConfig.SiteOption.SiteID.Contains("sicon")) { function.WriteErrMsg("管理员已关闭银币支付功能!"); }
                    if (mui.SilverCoin < (double)payMod.MoneyReal) { function.WriteErrMsg("对不起,银币不足!"); }
                    buser.ChangeVirtualMoney(mui.UserID, new M_UserExpHis()
                    {
                        score = -(double)payMod.MoneyReal,
                        ScoreType = (int)M_UserExpHis.SType.SIcon,
                        detail = "支付成功,支付单号:" + payMod.PayNo
                    });
                    mui = buser.GetLogin(false);
                    zfpt.Text = "银币";
                    fees.Text = "帐户银币：";
                    sxf.Text = mui.SilverCoin + " 个";
                    break;
                case "Score":
                    if (!SiteConfig.SiteOption.SiteID.Contains("point")) { function.WriteErrMsg("管理员已关闭积分支付功能!"); }
                    if (mui.UserExp < (double)payMod.MoneyReal) { function.WriteErrMsg("对不起,积分不足!"); }
                    buser.ChangeVirtualMoney(mui.UserID, new M_UserExpHis()
                    {
                        score = -(double)payMod.MoneyReal,
                        ScoreType = (int)M_UserExpHis.SType.Point,
                        detail = "支付成功,支付单号:" + payMod.PayNo
                    });
                    mui = buser.GetLogin(false);
                    zfpt.Text = "积分";
                    fees.Text = "帐户积分：";
                    sxf.Text = mui.UserExp + " 分";
                    break;
                default:
                    function.WriteErrMsg("指定的支付方式不存在,请检查大小写是否正确!");
                    break;
            }
            for (int i = 0; i < orderList.Count; i++)//更改订单状态
            {
                M_OrderList orderMod = orderList[i];
                OrderHelper.SaveSnapShot(orderMod);
                #region 写入日志,更新订单状态
                switch (payMethod)
                {
                    case "Purse":
                        orderMod.Paymentstatus = (int)M_OrderList.PayEnum.HasPayed;
                        orderMod.Receivablesamount = orderMod.Ordersamount;
                        if (orderBll.Update(orderMod))
                        {
                            orderCom.SendMessage(orderMod, paylogMod, "payed");
                            paylogMod.PayMethod = (int)M_Order_PayLog.PayMethodEnum.Purse;
                            paylogMod.Remind += "商城订单" + orderMod.OrderNo + "余额付款成功";
                        }
                        break;
                    case "SilverCoin":
                        orderMod.Paymentstatus = (int)M_OrderList.PayEnum.HasPayed;
                        orderMod.Receivablesamount = orderMod.Ordersamount;
                        if (orderBll.Update(orderMod))
                        {
                            orderCom.SendMessage(orderMod, paylogMod, "payed");
                            paylogMod.PayMethod = (int)M_Order_PayLog.PayMethodEnum.Silver;
                            paylogMod.Remind += "商城订单" + orderMod.OrderNo + "银币付款成功";
                        }
                        break;
                    case "Score":
                        orderMod.Paymentstatus = (int)M_OrderList.PayEnum.HasPayed;
                        orderMod.Receivablesamount = orderMod.Ordersamount;
                        if (orderBll.Update(orderMod))
                        {
                            orderCom.SendMessage(orderMod, paylogMod, "payed");
                            paylogMod.PayMethod = (int)M_Order_PayLog.PayMethodEnum.Score;
                            paylogMod.Remind = "商城订单" + orderMod.OrderNo + "积分付款成功";
                        }
                        break;
                    default:
                        function.WriteErrMsg("指定的支付方式不存在,请检查大小写是否正确!");
                        break;
                }
                //-----------------------付款后处理区域
                //orderCom.SaveSnapShot(orderMod);
                paylogMod.UserID = mui.UserID;
                paylogMod.OrderID = orderMod.id;
                paylogMod.PayMoney = orderMod.Ordersamount;
                paylogMod.PayPlatID = 0;
                OrderHelper.FinalStep(orderMod);//支付成功后处理步步骤,允许操作paylogMod
                paylogBll.insert(paylogMod);
                #endregion
            }
            //-----------------For End
            ddh.Text = payMod.PaymentNum;
            PayNum_L2.Text = payMod.MoneyReal.ToString("f2");
            sjhbje.Text = payMod.MoneyReal.ToString("f2");
            payMod.Status = (int)M_Payment.PayStatus.HasPayed;
            payMod.MoneyTrue = payMod.MoneyReal;
            paymentBll.Update(payMod);
            payinfo_div.Visible = false;
            AfterPay_Tb.Visible = true;
        }
        //支付单现金跳转付款
        //private void PayByMoney(M_Payment payMod)
        //{
        //    switch ((M_PayPlat.Plat)payPlat.PayClass)
        //    {
        //        case M_PayPlat.Plat.ICBC_NC://南昌工行
        //            break;
        //        default:
        //            throw new Exception("付款方式不存在");
        //    }
        //}
        //支付宝单网银
        public void alipayBank(string bankName)
        {
            B_Alipay_Bank_Submit sub = new B_Alipay_Bank_Submit();//网银专用
            //-------------------支付宝网银支付
            ////////////////////////////////////////////请求参数////////////////////////////////////////////
            //支付类型
            string payment_type = "1";
            //必填，不能修改
            //服务器异步通知页面路径
            string notify_url = urlReq1 + "/AlipayReturn.aspx";
            //需http://格式的完整路径，不能加?id=123这类自定义参数
            //页面跳转同步通知页面路径
            string return_url = urlReq1 + "/AlipayNotify.aspx";
            //需http://格式的完整路径，不能加?id=123这类自定义参数，不能写成http://localhost/
            //卖家支付宝帐户
            string seller_email = payPlat.SellerEmail;
            //必填
            //商户订单号
            string out_trade_no = pinfo.PaymentNum;
            //商户网站订单系统中唯一订单号，必填
            //订单名称
            string subject = pinfo.Remark;
            //必填
            //付款金额
            string total_fee = pinfo.MoneyReal.ToString("f2");
            //订单描述
            string body = pinfo.Remark;
            //默认支付方式
            string paymethod = "bankPay";
            //必填
            //默认网银
            string defaultbank = bankName;
            //必填，银行简码请参考接口技术文档

            //商品展示地址
            string show_url = pinfo.PaymentNum;
            //需以http://开头的完整路径，例如：http://www.xxx.com/myorder.html

            //防钓鱼时间戳
            string anti_phishing_key = "";
            //若要使用请调用类文件submit中的query_timestamp函数
            //客户端的IP地址
            string exter_invoke_ip = "";
            //非局域网的外网IP地址，如：221.0.0.1
            ////////////////////////////////////////////////////////////////////////////////////////////////

            //把请求参数打包成数组
            SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
            sParaTemp.Add("partner", payPlat.AccountID.Trim());
            sParaTemp.Add("_input_charset", "utf-8");
            sParaTemp.Add("service", "create_direct_pay_by_user");
            sParaTemp.Add("payment_type", payment_type);
            sParaTemp.Add("notify_url", notify_url);
            sParaTemp.Add("return_url", return_url);
            sParaTemp.Add("seller_email", seller_email);
            sParaTemp.Add("out_trade_no", out_trade_no);
            sParaTemp.Add("subject", subject);
            sParaTemp.Add("total_fee", total_fee);
            sParaTemp.Add("body", body);
            sParaTemp.Add("paymethod", paymethod);//支付方式,  string paymethod = "bankPay";
            sParaTemp.Add("defaultbank", defaultbank);//银行
            sParaTemp.Add("show_url", show_url);
            sParaTemp.Add("anti_phishing_key", anti_phishing_key);
            sParaTemp.Add("exter_invoke_ip", exter_invoke_ip);
            //建立请求
            string sHtmlText = sub.BuildRequest(sParaTemp, "get", "确认");
            Response.Write(sHtmlText);
        }
    }
}