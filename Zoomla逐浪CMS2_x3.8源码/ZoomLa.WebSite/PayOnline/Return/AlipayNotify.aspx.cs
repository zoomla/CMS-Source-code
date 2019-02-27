using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Specialized;
using System.Collections.Generic;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Common;
using ZoomLa.BLL.Shop;

public partial class PayOnline_AlipayNotify : System.Web.UI.Page
{
    private string PayPlat = "支付宝在线支付:";
    B_Order_PayLog paylogBll = new B_Order_PayLog();//支付日志类,用于记录用户付款信息
    B_OrderList orderBll = new B_OrderList();
    B_CartPro cartBll = new B_CartPro();//数据库中购物车业务类
    B_User buser = new B_User();
    B_PayPlat payPlatBll = new B_PayPlat();
    B_Payment payBll = new B_Payment();
    OrderCommon orderCOM = new OrderCommon();
    protected void Page_Load(object sender, EventArgs e)
    {
        //DataTable pay = payPlatBll.GetPayPlatByClassid(12);
        M_PayPlat platMod = payPlatBll.SelModelByClass(M_PayPlat.Plat.Alipay_Instant);
        SortedDictionary<string, string> sArrary = GetRequestPost();
        ///////////////////////以下参数是需要设置的相关配置参数，设置后不会更改的//////////////////////
        ZoomLa.Model.M_Alipay_config con = new ZoomLa.Model.M_Alipay_config();
        string partner = platMod.AccountID;
        string key = platMod.MD5Key;
        string input_charset = con.Input_charset;
        string sign_type = con.Sign_type;
        string transport = con.Transport;
        //////////////////////////////////////////////////////////////////////////////////////////////
        if (sArrary.Count > 0)//判断是否有带返回参数
        {
            ZoomLa.BLL.B_Alipay_notify aliNotify = new ZoomLa.BLL.B_Alipay_notify(sArrary, Request.Form["notify_id"], partner, key, input_charset, sign_type, transport);
            string responseTxt = aliNotify.ResponseTxt; //获取远程服务器ATN结果，验证是否是支付宝服务器发来的请求
            string sign = Request.Form["sign"]; 
            string mysign = aliNotify.Mysign;           //获取通知返回后计算后（验证）的签名结果
            //判断responsetTxt是否为ture，生成的签名结果mysign与获得的签名结果sign是否一致
            //responsetTxt的结果不是true，与服务器设置问题、合作身份者ID、notify_id一分钟失效有关
            //mysign与sign不等，与安全校验码、请求时的参数格式（如：带自定义参数等）、编码格式有关
            string order_no = Request.Form["out_trade_no"];     //获取订单号
            ZLLog.L(ZLEnum.Log.pay, PayPlat + aliNotify.ResponseTxt + ":" + order_no + ":" + Request.Form["buyer_email"] + ":" + Request.Form["trade_status"] + ":" + Request.Form["price"] + ":" + Request.Form["subject"]);
            if (responseTxt == "true" && sign == mysign)//验证成功
            {
                //——请根据您的业务逻辑来编写程序（以下代码仅作参考）——
                //获取通知返回参数，可参考技术文档中服务器异步通知参数列表
                string trade_no = Request.Form["trade_no"];         //交易号
                string total_fee = Request.Form["price"];           //获取总金额
                string subject = Request.Form["subject"];           //商品名称、
                string body = Request.Form["body"];                 //商品描述、订单备注、描述
                string buyer_email = Request.Form["buyer_email"];   //买家账号
                string trade_status = Request.Form["trade_status"]; //交易状态
                if (Request.Form["trade_status"] == "WAIT_BUYER_PAY")//没有付款
                {
                    
                }
                else if(trade_status.Equals("WAIT_SELLER_SEND_GOODS"))//付款成功，但是卖家没有发货
                {

                }
                else if (trade_status.Equals("TRADE_SUCCESS"))//付款成功
                {
                    try
                    {
                        M_Payment pinfo = payBll.SelModelByPayNo(order_no);
                        if (pinfo.Status != (int)M_Payment.PayStatus.NoPay) return;
                        pinfo.Status = (int)M_Payment.PayStatus.HasPayed;
                        pinfo.PlatformInfo = PayPlat;    //平台反馈信息
                        pinfo.SuccessTime = DateTime.Now;//交易成功时间
                        pinfo.CStatus = true; //处理状态
                        pinfo.AlipayNO = trade_no;//保存支付宝交易号
                        pinfo.MoneyTrue = Convert.ToDouble(total_fee);
                        payBll.Update(pinfo);
                        DataTable orderDT = orderBll.GetOrderbyOrderNo(pinfo.PaymentNum);
                        foreach (DataRow dr in orderDT.Rows)
                        {
                            M_Order_PayLog paylogMod = new M_Order_PayLog();
                            M_OrderList orderMod = orderBll.SelModelByOrderNo(dr["OrderNo"].ToString());
                            OrderHelper.FinalStep(pinfo, orderMod, paylogMod);
                            orderCOM.SendMessage(orderMod, paylogMod, "payed");
                        }
                        Response.Write("success");
                        ZLLog.L(ZLEnum.Log.pay, PayPlat + "成功!支付单:" + order_no);
                    }
                    catch (Exception ex)
                    {
                        ZLLog.L(ZLEnum.Log.pay, new M_Log()
                        {
                            Action = "支付回调报错",
                            Message = PayPlat + ",支付单:" + order_no + ",原因:" + ex.Message
                        });
                    }
                }
                else if (Request.Form["trade_status"] == "WAIT_BUYER_CONFIRM_GOODS")//卖家已经发货，等待买家确认
                {

                }
                else if (Request.Form["trade_status"] == "TRADE_FINISHED")
                {
                }
                else//其他状态判断。普通即时到帐中，其他状态不用判断，直接打印success。
                {
                    ZLLog.L(PayPlat + "付款未成功截获,单号:[" + trade_status + "]");
                }
            }
            else//验证失败
            {
                ZLLog.L(ZLEnum.Log.pay, new M_Log()
                {
                    Action = "支付验证失败",
                    Message = PayPlat + ",支付单:" + order_no
                });
                Response.Write("fail");
            }
        }
        else
        {
            Response.Write("success"); 
        }
    }

    /// <summary>
    /// 获取POST过来通知消息，并以“参数名=参数值”的形式组成数组
    /// </summary>
    /// <returns>request回来的信息组成的数组</returns>
    public SortedDictionary<string, string> GetRequestPost()
    {
        int i = 0;
        SortedDictionary<string, string> sArray = new SortedDictionary<string, string>();
        NameValueCollection coll;
        //Load Form variables into NameValueCollection variable.
        coll = Request.Form;

        // Get names of all forms into a string array.
        String[] requestItem = coll.AllKeys;

        for (i = 0; i < requestItem.Length; i++)
        {
            sArray.Add(requestItem[i], Request.Form[requestItem[i]]);
        }

        return sArray;
    }
}
