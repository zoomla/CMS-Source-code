namespace ZoomLaCMS.PayOnline.Return
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Net.Mail;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using ZoomLa.BLL;
    using ZoomLa.BLL.API;
    using ZoomLa.BLL.Shop;
    using ZoomLa.Common;
    using ZoomLa.Components;
    using ZoomLa.Model;

    public partial class EPay95Notify : System.Web.UI.Page
    {
        Pay_BaoFa baofa = new Pay_BaoFa();
        B_Payment payBll = new B_Payment();
        B_PayPlat platBll = new B_PayPlat();
        B_OrderList orderBll = new B_OrderList();
        OrderCommon orderCom = new OrderCommon();
        protected void Page_Load(object sender, EventArgs e)
        {
            ZLLog.L(ZLEnum.Log.pay, "进入双乾支付页");
            string MerNo = Request.Params["MerNo"].ToString();
            string BillNo = Request.Params["BillNo"].ToString();
            string Amount = Request.Params["Amount"].ToString();
            string Succeed = Request.Params["Succeed"].ToString();
            string Result = Request.Params["Result"].ToString();
            string MD5info = Request.Params["MD5info"].ToString();
            string MerRemark = Request.Params["MerRemark"].ToString();

            M_Payment payMod = payBll.SelModelByPayNo(BillNo);
            M_PayPlat platMod = new M_PayPlat();
            platMod = platBll.SelReturnModel(payMod.PayPlatID);
            if (platMod.PayClass != (int)M_PayPlat.Plat.EPay95) { ZLLog.L(ZLEnum.Log.safe, "回调页面错误" + Request.RawUrl); }
            string MD5key = platMod.MD5Key;//Md5加密私钥[注册时产生]
            string md5md5 = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(MD5key, "MD5").ToUpper();
            string md5src = "Amount=" + Amount + "&BillNo=" + BillNo + "&MerNo=" + MerNo + "&Succeed=" + Succeed + "&" + md5md5;
            string md5str = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(md5src, "MD5").ToUpper();
            try { ZLLog.L(ZLEnum.Log.pay, "双乾详情:" + md5src); }
            catch (Exception ex) { ZLLog.L(ZLEnum.Log.pay, "双乾未完成记录" + ex.Message); }
            if (MD5info.Equals(md5str))
            {
                if (!Succeed.Equals("88")) { ZLLog.L(ZLEnum.Log.pay, "支付单:" + BillNo + ", 支付状态非成功,取消处理!" + md5src); return; }
                try
                {
                    M_Payment pinfo = payBll.SelModelByPayNo(BillNo);
                    if (pinfo.Status != (int)M_Payment.PayStatus.NoPay) return;
                    pinfo.Status = (int)M_Payment.PayStatus.HasPayed;
                    pinfo.PlatformInfo = "双乾坤支付";    //平台反馈信息
                    pinfo.SuccessTime = DateTime.Now;//交易成功时间
                    pinfo.CStatus = true; //处理状态
                    pinfo.MoneyTrue = Convert.ToDouble(Amount);
                    payBll.Update(pinfo);

                    //-------支付成功处理,发送邮件
                    M_OrderList orderMod1 = orderBll.SelModelByOrderNo(pinfo.PaymentNum);
                    B_User userBll = new B_User();
                    M_UserInfo info = userBll.GetSelect(pinfo.UserID);
                    MailInfo mailInfo = new MailInfo();
                    mailInfo.IsBodyHtml = true;
                    mailInfo.FromName = SiteConfig.SiteInfo.SiteName;
                    MailAddress address = new MailAddress(info.Email);
                    mailInfo.ToAddress = address;
                    mailInfo.MailBody = "尊敬的" + info.UserName + "，您的入金订单" + orderMod1.OrderNo + "购买生效<br/>金额:" + String.Format("{0:N2}", orderMod1.Ordersamount) + "<br/>您可进入<a href='" + SiteConfig.SiteInfo.SiteUrl + "/User/Info/ConsumeDetail?SType=1'>会员中心</a>查看充值记录<br/>如有任何问题请访问我们网站<a href='" + SiteConfig.SiteInfo.SiteUrl + "'>" + SiteConfig.SiteInfo.SiteUrl + "</a>，联系我们！";
                    mailInfo.Subject = SiteConfig.SiteInfo.SiteName + "网站会员入金提醒";
                    SendMail.Send(mailInfo);

                    DataTable orderDT = orderBll.GetOrderbyOrderNo(pinfo.PaymentNum);
                    foreach (DataRow dr in orderDT.Rows)
                    {
                        M_Order_PayLog paylogMod = new M_Order_PayLog();
                        M_OrderList orderMod = orderBll.SelModelByOrderNo(dr["OrderNo"].ToString());
                        FinalStep(pinfo, orderMod, paylogMod);
                        //orderCom.SendMessage(orderMod, paylogMod, "payed");
                        //orderCom.SaveSnapShot(orderMod);
                    }
                    //Response.Write("OK");
                    ZLLog.L(ZLEnum.Log.pay, "完成支付" + BillNo);
                }
                catch (Exception ex)
                {
                    ZLLog.L(ZLEnum.Log.pay, new M_Log()
                    {
                        Action = "支付回调报错",
                        Message = "平台:双乾,支付单:" + BillNo + ",原因:" + ex.Message
                    });
                }
            }
            else
            {
                ZLLog.L(ZLEnum.Log.pay, "双乾验证失败!支付单:" + BillNo);
            }
        }
        /// <summary>
        /// 异步回调后-->验证支付单状态-->如果正常,更新订单状态
        /// </summary>
        /// <param name="mod">订单模型</param>
        /// <param name="paylogMod">订单支付日志模型</param>
        public static void FinalStep(M_Payment pinfo, M_OrderList mod, M_Order_PayLog paylogMod)
        {
            B_OrderList orderBll = new B_OrderList();
            B_CartPro cartBll = new B_CartPro();
            B_Order_PayLog paylogBll = new B_Order_PayLog();
            B_User buser = new B_User();
            //订单已处理,避免重复（如已处理过,则继续处理下一张订单）
            if (mod.OrderStatus >= 99)
            {
                ZLLog.L(ZoomLa.Model.ZLEnum.Log.safe, new M_Log()
                {
                    Action = "支付回调异常,订单状态已为99",
                    Message = "订单号:" + mod.OrderNo + ",支付单:" + pinfo.PayNo
                });
                return;
            }
            //已经收到钱了,所以先执行
            orderBll.UpOrderinfo("Paymentstatus=1,Receivablesamount=" + pinfo.MoneyTrue, mod.id);
            if (mod.Ordertype == (int)M_OrderList.OrderEnum.Purse)//余额充值,不支持银币
            {
                buser.ChangeVirtualMoney(mod.Userid, new M_UserExpHis()
                {
                    score = (int)mod.Ordersamount,
                    ScoreType = (int)M_UserExpHis.SType.Purse,
                    detail = "余额充值,订单号:" + mod.OrderNo
                });
                orderBll.UpOrderinfo("OrderStatus=99", mod.id);//成功的订单
            }
            else if ((mod.Ordertype == (int)M_OrderList.OrderEnum.IDCRen))//IDC服务续费
            {
                orderBll.UpOrderinfo("OrderStatus=99", mod.id);
                B_Product proBll = new B_Product();
                //更新旧订单的期限
                if (string.IsNullOrEmpty(mod.Ordermessage))//购物车ID
                {
                    //function.WriteErrMsg("出错,无需续费订单信息,请联系管理员!!!");
                    throw new Exception("出错,无续费订单信息,请联系管理员");
                }
                M_CartPro newCartMod = cartBll.SelModByOrderID(mod.id);//新购物车只是取其商品ID与数量等
                M_Product proMod = proBll.GetproductByid(newCartMod.ProID);
                //更新延长旧服务的到期时间，旧服务是存在CartPro的EndTime当中
                M_CartPro oldCartMod = cartBll.SelReturnModel(Convert.ToInt32(mod.Ordermessage));
                if (oldCartMod.EndTime < DateTime.Now) oldCartMod.EndTime = DateTime.Now;//如已过期，则将时间更新至今日
                                                                                         //oldCartMod.EndTime = proBll.GetEndTime(proMod, newCartMod.Pronum, oldCartMod.EndTime);
                cartBll.UpdateByID(oldCartMod);
                //paylogMod.Remind = "为" + mod.Ordermessage + "订单续费(购物车)";
            }
            else if (mod.Ordertype == (int)M_OrderList.OrderEnum.Cloud)//云购订单
            {
                //根据份数生成幸运码,写入表中,并减去库存 ZL_Order_LuckCode
            }
            else//其他旅游订单等,只更新状态
            {
                orderBll.UpOrderinfo("OrderStatus=99", mod.id);//成功的订单
            }
            //-------支付成功处理,并写入日志
            paylogMod.Remind += "订单" + mod.OrderNo + "购买生效";
            paylogMod.OrderID = mod.id;
            paylogMod.PayMoney = mod.Ordersamount;
            paylogMod.PayMethod = (int)M_Order_PayLog.PayMethodEnum.Other;//外部指定
            paylogMod.PayPlatID = pinfo.PayPlatID;
            paylogBll.insert(paylogMod);

        }
        //用于后台确认支付
        public static void FinalStep(M_OrderList mod)
        {
            if (mod.id < 1) { throw new Exception("未指定订单ID"); }
            if (mod.Ordertype < 1) { throw new Exception("未指定订单类型"); }
            if (string.IsNullOrEmpty(mod.OrderNo)) { throw new Exception("未指定订单号"); }
            //M_AdminInfo adminMod = B_Admin.GetLogin();
            B_Payment payBll = new B_Payment();
            M_Payment pinfo = new M_Payment();
            pinfo.PaymentNum = mod.OrderNo;
            pinfo.UserID = mod.Userid;
            pinfo.PayNo = payBll.CreatePayNo();
            pinfo.MoneyPay = mod.Ordersamount;
            pinfo.MoneyTrue = pinfo.MoneyPay;//看是否需要支持手输
            pinfo.Status = (int)M_Payment.PayStatus.HasPayed;
            pinfo.CStatus = true;
            //pinfo.Remark = "管理员确认支付,ID:" + adminMod.AdminId + ",登录名:" + adminMod.AdminName + ",真实姓名:" + adminMod.AdminTrueName;
            pinfo.PaymentID = payBll.Add(pinfo);
            pinfo.SuccessTime = DateTime.Now;
            pinfo.PayPlatID = (int)M_PayPlat.Plat.CashOnDelivery;//默认为线下支付
            pinfo.PlatformInfo = "";
            M_Order_PayLog paylogMod = new M_Order_PayLog();
            FinalStep(pinfo, mod, paylogMod);
        }
    }
}