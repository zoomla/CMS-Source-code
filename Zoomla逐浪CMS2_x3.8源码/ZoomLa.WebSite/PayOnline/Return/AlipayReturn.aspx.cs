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
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;
using ZoomLa.BLL;

public partial class PayOnline_AlipayReturn : System.Web.UI.Page
{
    private B_User buser = new B_User();
    private B_Order_PayLog paylogBll = new B_Order_PayLog();//支付日志类,用于记录用户付款信息
    private M_Order_PayLog paylogMod = new M_Order_PayLog();
    private B_CartPro cartBll = new B_CartPro();//数据库中购物车业务类
    private B_OrderList orderBll = new B_OrderList();
    private B_Payment payBll = new B_Payment();
    protected void Page_Load(object sender, EventArgs e)
    {
        SortedDictionary<string, string> sArrary = GetRequestGet();
        
        ///////////////////////以下参数是需要设置的相关配置参数，设置后不会更改的//////////////////////
        ZoomLa.BLL.B_PayPlat payBLL = new ZoomLa.BLL.B_PayPlat();
        DataTable pay = payBLL.GetPayPlatByClassid(12);
        ZoomLa.Model.M_Alipay_config con = new ZoomLa.Model.M_Alipay_config();
        if (pay == null || pay.Rows.Count < 1) function.WriteErrMsg("请先配置支付平台信息!![系统设置-->支付平台-->在线支付平台]");
        string partner = pay.Rows[0]["AccountID"].ToString();
        string key = pay.Rows[0]["MD5Key"].ToString();
        string input_charset = con.Input_charset;
        string sign_type = con.Sign_type;
        string transport = con.Transport;

        //////////////////////////////////////////////////////////////////////////////////////////////

        if (sArrary.Count > 0)//判断是否有带返回参数
        {
            ZoomLa.BLL.B_Alipay_notify aliNotify = new ZoomLa.BLL.B_Alipay_notify(sArrary, Request.QueryString["notify_id"], partner, key, input_charset, sign_type, transport);
            //AlipayNotify aliNotify = new AlipayNotify(sArrary, Request.QueryString["notify_id"], partner, key, input_charset, sign_type, transport);
            string responseTxt = aliNotify.ResponseTxt; //获取远程服务器ATN结果，验证是否是支付宝服务器发来的请求
            string sign = Request.QueryString["sign"];  //获取支付宝反馈回来的sign结果
            string mysign = aliNotify.Mysign;           //获取通知返回后计算后（验证）的签名结果

            //写日志记录（若要调试，请取消下面两行注释）
            //string sWord = "responseTxt=" + responseTxt + "\n return_url_log:sign=" + Request.QueryString["sign"] + "&mysign=" + mysign + "\n return回来的参数：" + aliNotify.PreSignStr;
            //AlipayFunction.log_result(Server.MapPath("log/" + DateTime.Now.ToString().Replace(":", "")) + ".txt",sWord);

            //判断responsetTxt是否为ture，生成的签名结果mysign与获得的签名结果sign是否一致
            //responsetTxt的结果不是true，与服务器设置问题、合作身份者ID、notify_id一分钟失效有关
            //mysign与sign不等，与安全校验码、请求时的参数格式（如：带自定义参数等）、编码格式有关
            if (responseTxt == "true" && sign == mysign)//验证成功
            {
                /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                //请在这里加上商户的业务逻辑程序代码

                //——请根据您的业务逻辑来编写程序（以下代码仅作参考）——
                //获取支付宝的通知返回参数，可参考技术文档中页面跳转同步通知参数列表
                string trade_no = Request.QueryString["trade_no"];              //支付宝交易号
                string order_no = Request.QueryString["out_trade_no"];	        //获取订单号
                string total_fee = Request.QueryString["total_fee"];	            //获取总金额
                string subject = Request.QueryString["subject"];                //商品名称、订单名称
                string body = Request.QueryString["body"];                      //商品描述、订单备注、描述
                string buyer_email = Request.QueryString["buyer_email"];        //买家支付宝账号
                //string receive_name = Request["receive_name"];      //收货人姓名
                //string receive_address = Request["receive_address"];//收货人地址
                //string receive_zip = Request["receive_zip"];        //收货人邮编
                //string receive_phone = Request["receive_phone"];    //收货人电话
                //string receive_mobile = Request["receive_mobile"];  //收货人手机
                string trade_status = Request.QueryString["trade_status"];      //交易状态

                //打印页面
                lbTrade_no.Text = trade_no;
                lbOut_trade_no.Text = order_no;
                lbTotal_fee.Text = total_fee;
                lbSubject.Text = subject;
                lbBody.Text = body;
                lbBuyer_email.Text = buyer_email;
                lbTrade_status.Text = trade_status;
                lbVerify.Text = "验证成功";

                if (Request.QueryString["trade_status"] == "WAIT_SELLER_SEND_GOODS" || Request.QueryString["trade_status"] == "TRADE_SUCCESS")//买家已经付款，等待卖家发货
                {
                    M_Payment payMod = payBll.SelModelByPayNo(order_no);
                    M_OrderList omod = orderBll.SelModelByOrderNo(payMod.PaymentNum.Split(',')[0]);
                    LbName.Text = omod.Receiver;
                    LbAddress.Text = omod.Jiedao;
                    LbZip.Text = omod.ZipCode;
                    LbPhone.Text = omod.Phone;
                    LbMobile.Text = omod.Mobile.ToString();
                    FinalStep(omod);
                }
                else if (Request.QueryString["trade_status"] == "TRADE_FINISHED")//交易成功结束
                {
                    lbVerify.Text = "该交易已经成功结束！";
                }
                else
                {
                    Response.Write("trade_status=" + Request.QueryString["trade_status"]);
                }
            }
            else//验证失败
            {
                lbVerify.Text = "验证失败";
            }
        }
        else
        {
            lbVerify.Text = "无返回参数";
        }
    }
    //需处理,这里只负责View部分,页务与逻辑操作全部放入Notify
    private void FinalStep(M_OrderList mod)
    {
        //对第一张订单处理,后期改为对支付单处理
      
        if (mod.Ordertype == (int)M_OrderList.OrderEnum.Domain)//域名订单
        {
            Response.Redirect("~/Plugins/Domain/DomReg2.aspx?OrderNo=" + mod.OrderNo);
        }
        else if (mod.Ordertype == (int)M_OrderList.OrderEnum.Purse)//余额充值,不支持银币
        {
            Response.Redirect("~/Plugins/Domain/ChargePurse.aspx?OrderNo=" + mod.OrderNo);
        }
        else if ((mod.Ordertype == (int)M_OrderList.OrderEnum.IDCRen))//IDC服务续费
        {
            B_Product proBll = new B_Product();
            //更新旧订单的期限
            if (string.IsNullOrEmpty(mod.Ordermessage))
            {
                function.WriteErrMsg("出错,无需续费订单信息,请联系管理员!!!");
            }
            M_CartPro newCartMod = cartBll.SelModByOrderID(mod.id);
            M_Product proMod = proBll.GetproductByid(newCartMod.ProID);
            //更新延长旧服务的到期时间，旧服务是存在CartPro的EndTime当中
            M_CartPro oldCartMod = cartBll.SelReturnModel(Convert.ToInt32(mod.Ordermessage));
            if (oldCartMod.EndTime < DateTime.Now) oldCartMod.EndTime = DateTime.Now;//如已过期，则将时间更新至今日
            //oldCartMod.EndTime = proBll.GetEndTime(proMod, newCartMod.Pronum, oldCartMod.EndTime);
            cartBll.UpdateByID(oldCartMod);
            paylogMod.Remind = "为" + mod.Ordermessage + "订单续费(购物车)";
            remindHtml.Text = "<span>付款成功--></span><a style='color:red;' href='/Plugins/Domain/ViewHave.aspx' title='已购服务'>点击查看已购服务</a><br />";
        }
        else if (mod.Ordertype == (int)M_OrderList.OrderEnum.Cloud)//云购订单
        {
            //根据份数生成幸运码,写入表中,并减去库存 ZL_Order_LuckCode
            remindHtml.Text = "<span style='color:red;'>付款成功,你的云购幸运码是<br/>" + orderBll.CreateLuckCode(mod) + "</span>";
        }
    }


    /// <summary>
    /// 获取支付宝GET过来通知消息，并以“参数名=参数值”的形式组成数组
    /// </summary>
    /// <returns>request回来的信息组成的数组</returns>
    public SortedDictionary<string, string> GetRequestGet()
    {
        int i = 0;
        SortedDictionary<string, string> sArray = new SortedDictionary<string, string>();
        NameValueCollection coll;
        //Load Form variables into NameValueCollection variable.
        coll = Request.QueryString;

        // Get names of all forms into a string array.
        String[] requestItem = coll.AllKeys;

        for (i = 0; i < requestItem.Length; i++)
        {
            sArray.Add(requestItem[i], Request.QueryString[requestItem[i]]);
        }

        return sArray;
    }
}
