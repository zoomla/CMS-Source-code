<%@ WebHandler Language="C#" Class="yisi" %>

using System;
using System.Web;
using System.Data;
using ZoomLa.BLL;
using ZoomLa.BLL.API;
using ZoomLa.BLL.User;
using ZoomLa.Model;
using ZoomLa.SQLDAL;
using ZoomLa.Ajax.Temp;
using Newtonsoft.Json;

public class yisi :API_Base,IHttpHandler {
    B_User buser = new B_User();
    B_Payment payBll = new B_Payment();
    B_OrderList orderBll = new B_OrderList();
    YiSiHelper yshelp = new YiSiHelper();
    M_Yisi_Result ysret = new M_Yisi_Result();
    B_Yisi_Order ysBll = new B_Yisi_Order();
    /// <summary>
    /// life:水煤电,flow:流量,phone:话费,oil:油卡
    /// </summary>
    private string Type { get { return Req("type"); } }
    /// <summary>
    /// 账号|手机号|卡号
    /// </summary>
    private string Account { get { return Req("account"); } }
    /// <summary>
    /// 话费充值可不填
    /// </summary>
    private int ProID { get { return DataConvert.CLng(Req("proid")); } }
    private string OpenID { get { return Req("openid") ?? ""; } }
    public void ProcessRequest(HttpContext context)
    {
        M_UserInfo mu = B_User_API.GetLogin(OpenID);
        retMod.retcode = M_APIResult.Failed;
        retMod.callback = CallBack;
        if (mu.IsNull) { retMod.retmsg = "用户未登录"; RepToClient(retMod); }
        try
        {
            switch (Type)
            {
                case "life"://用户查询--生成订单--返回支付单号--调用微信付款--付款完成--调用易赛接口--易赛返回信息--更新易赛订单状态--通知用户缴费成功
                    #region 水煤电
                    switch (Action)
                    {
                        case "query"://查询返回账单
                            if (string.IsNullOrEmpty(Account) || ProID < 1) { retMod.retmsg = "帐号或产品ID为空"; }
                            else
                            {
                                ysret = yshelp.Life_QueryBill(ProID, Account);
                                retMod.result = JsonConvert.SerializeObject(ysret);
                                if (ysret.success)
                                {
                                    retMod.retcode = M_APIResult.Success;
                                }
                                else { retMod.retmsg = ysret.err; }
                            }
                            break;
                        case "neworder"://创建订单,生成支付单,向用户返回支付单--用户根据支付单发起对应的支付
                            {
                                double money = DataConvert.CDouble(Req("Money"));
                                M_OrderList orderMod = NewOrder(mu, money, "水电煤缴费");
                                orderMod.id = orderBll.insert(orderMod);
                                M_Payment payMod = OrderToPay(orderMod);
                                retMod.retcode = M_APIResult.Success;
                                retMod.result = payMod.PayNo;
                            }
                            break;
                        case "view"://查询指定订单信息
                            {
                                string orderno = Req("orderno");
                                M_Yisi_Order ysMod = ysBll.SelModelByOutOrder(orderno);
                                retMod.retcode = M_APIResult.Success;
                                retMod.result = JsonConvert.SerializeObject(ysMod);
                            }
                            break;
                        case "list":
                            {
                                retMod.retcode = M_APIResult.Success;
                                retMod.result = Sel(mu);
                            }
                            break;
                    }
                    #endregion
                    break;
                case "flow":
                    #region 流量
                    switch (Action)
                    {
                        case "neworder":
                            {
                                M_OrderList orderMod = NewOrder(mu, GetMoneyByProID(ProID), "流量充值");
                                orderMod.id = orderBll.insert(orderMod);
                                M_Payment payMod = OrderToPay(orderMod);
                                retMod.retcode = M_APIResult.Success;
                                retMod.result = payMod.PayNo;
                            }
                            break;
                        case "view":
                            break;
                        case "list":
                            {
                                retMod.retcode = M_APIResult.Success;
                                retMod.result = Sel(mu);
                            }
                            break;
                    }
                    #endregion
                    break;
                case "phone":
                    #region 话费
                    switch (Action)
                    {
                        case "neworder"://根据产品ID,生成话费充值订单(话费产品ID,由我们自生成)
                            {
                                M_OrderList orderMod = NewOrder(mu, GetMoneyByProID(ProID), "话费充值");
                                orderMod.id = orderBll.insert(orderMod);
                                M_Payment payMod = OrderToPay(orderMod);
                                retMod.retcode = M_APIResult.Success;
                                retMod.result = payMod.PayNo;
                            }
                            break;
                        case "view":
                            break;
                        case "list":
                            {
                                retMod.retcode = M_APIResult.Success;
                                retMod.result = Sel(mu);
                            }
                            break;
                    }
                    #endregion
                    break;
                case "oil":
                    break;
                default:
                    retMod.retmsg = "[" + Type + "]类型不存在";
                    break;
            }
        }
        catch (Exception ex) { retMod.retmsg = ex.Message; }
        RepToClient(retMod);
    }
    public bool IsReusable { get { return false; } }
    private string GetTbName() 
    {
        switch (Type)
        {
            case "life":
                return "ZL_C_sdm";
            case "flow":
                return "ZL_C_llcp";
            case "phone":
                return "ZL_C_hfmx";
            default:
                return "noexist";
        }
    }
    private double GetMoneyByProID(int proid)
    {
        string tbname = GetTbName();
        DataTable dt = DBCenter.Sel(tbname, "pronum=" + proid);
        if (dt.Rows.Count > 0) { return DataConvert.CDouble(dt.Rows[0]["money"]); }
        else { throw new Exception("产品编号[" + proid + "]无对应的数据"); }
    }
    private M_Payment OrderToPay(M_OrderList model)
    {
        M_Payment payMod = new M_Payment();
        payMod.PayNo = payBll.CreatePayNo();
        payMod.PaymentNum += model.OrderNo;
        payMod.MoneyPay += model.Ordersamount;
        if (payMod.MoneyPay <= 0) { payMod.MoneyPay = 0.01; }
        payMod.MoneyReal = payMod.MoneyPay;
        payMod.Remark = model.Balance_remark;
        payMod.UserID = model.Userid;
        payMod.Status = 1;
        payMod.PaymentID = payBll.Add(payMod);
        return payMod;

    }
    //根据信息生成订单
    private M_OrderList NewOrder(M_UserInfo mu, double money, string remind)
    {
        M_OrderList orderMod = new M_OrderList();
        orderMod.Ordertype = (int)M_OrderList.OrderEnum.Normal;
        orderMod.OrderNo = B_OrderList.CreateOrderNo(M_OrderList.OrderEnum.Normal);
        orderMod.Extend = Account + ":" + ProID + ":" + Type;
        orderMod.Ordersamount = money;
        orderMod.Userid = mu.UserID;
        orderMod.Balance_remark = remind;
        if (mu.UserID < 1) { throw new Exception("用户信息不正确"); }
        if (money < 1) { throw new Exception("订单金额不正确,最小值为1"); }
        if (string.IsNullOrEmpty(Account)) { throw new Exception("未指定缴费账户或充值号码"); }
        return orderMod;
    }
    private string Sel(M_UserInfo mu)
    {
        if (mu.UserID < 1) { return ""; }
        string skey = Req("skey");
        DataTable dt = ysBll.Sel(Type, mu.UserID, skey);
        return JsonConvert.SerializeObject(dt);
    }
}