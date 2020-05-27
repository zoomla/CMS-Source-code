using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZoomLa.BLL;
using ZoomLa.Model;

namespace ZoomLa.Extend
{
    public class B_OZ_Order
    {
        public static void FinalStep(M_Payment pinfo, M_OrderList mod, M_Order_PayLog paylogMod)
        {
            B_OrderList orderBll = new B_OrderList();
            B_Order_PayLog paylogBll = new B_Order_PayLog();
            B_User buser = new B_User();
            //订单已处理,避免重复（如已处理过,则继续处理下一张订单）
            if (mod.OrderStatus >= 99)
            {
                ZLLog.L(ZoomLa.Model.ZLEnum.Log.safe, new M_Log() { Action = "支付回调异常,订单状态已为99", Message = "订单号:" + mod.OrderNo + ",支付单:" + pinfo.PayNo });
                return;
            }
            //已经收到钱了,所以先执行
            orderBll.UpOrderinfo("Paymentstatus=1,Receivablesamount=" + pinfo.MoneyTrue, mod.id);
            if (mod.Ordertype == (int)M_OrderList.OrderEnum.Purse)//余额充值,不支持银币
            {
                if (mod.Freight_remark.Contains("dummy"))
                {
                    buser.ChangeVirtualMoney(mod.Userid, new M_UserExpHis()
                    {
                        score = (int)mod.Ordersamount,
                        ScoreType = (int)M_UserExpHis.SType.DummyPoint,
                        detail = "佣金充值,订单号:" + mod.OrderNo
                    });
                }
                else
                {
                    buser.ChangeVirtualMoney(mod.Userid, new M_UserExpHis()
                    {
                        score = (int)mod.Ordersamount,
                        ScoreType = (int)M_UserExpHis.SType.Purse,
                        detail = "余额充值,订单号:" + mod.OrderNo
                    });
                }
                //检测复益账户是否有资金,有则将最大=(充值金额*10%)转入佣金账户
                try
                {
                    M_User_Money moneyMod = B_VMoney.Ex_SelModel(mod.Userid);
                    if (moneyMod.UMoney > 0)
                    {
                        double money = mod.Ordersamount * 0.1;
                        if (money > moneyMod.UMoney) { money = moneyMod.UMoney; }
                        B_VMoney.Ex_ChangeMoney(B_VMoney.ExSType.UMoney, mod.Userid, -money, "用户充值,单号[" + mod.OrderNo + "],复益转入佣金");
                        buser.ChangeVirtualMoney(mod.Userid, new M_UserExpHis()
                        {
                            score = (int)money,
                            ScoreType = (int)M_UserExpHis.SType.DummyPoint,
                            detail = "用户充值,复益转入佣金"
                        });
                    }
                }
                catch (Exception ex) { ZLLog.L(ZLEnum.Log.safe, "复益转入佣金失败,订单号:" + mod.OrderNo + ",原因:" + ex.Message); }
                orderBll.UpOrderinfo("OrderStatus=99", mod.id);//成功的订单
            }
            //-------支付成功处理,快照并写入日志
            paylogMod.Remind += "订单" + mod.OrderNo + "购买生效";
            paylogMod.OrderID = mod.id;
            paylogMod.PayMoney = mod.Ordersamount;
            paylogMod.PayMethod = (int)M_Order_PayLog.PayMethodEnum.Other;//外部指定
            paylogMod.PayPlatID = pinfo.PayPlatID;
            paylogBll.insert(paylogMod);
        }
    }
}
