using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using ZoomLa.BLL;
using ZoomLa.BLL.Helper;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;
using ZoomLa.Model.Shop;
using ZoomLa.SQLDAL;


namespace ZoomLa.BLL.Shop
{
    public class OrderHelper
    {
        private static B_OrderList orderBll = new B_OrderList();
        private static B_CartPro cartProBll = new B_CartPro();
        //----------------生成状态
        //初始(无色),进程中(淡蓝色),退货等(红色),比红色重要(橙色),完成(绿色),删除(灰色)
        /// <summary>
        /// 付款,支付方式,订单,物流,是否删除
        /// </summary>
        public enum TypeEnum { Pay, PayType, Order, Express, Aside };
        /// <summary>
        /// DataRowView.Row
        /// </summary>
        public static string GetStatus(DataRow dr, TypeEnum type)
        {
            M_OrderList orderMod = new M_OrderList();
            orderMod.Paymentstatus = Convert.ToInt32(dr["Paymentstatus"]);
            orderMod.OrderStatus = Convert.ToInt32(dr["OrderStatus"]);
            orderMod.StateLogistics = Convert.ToInt32(dr["StateLogistics"]);
            orderMod.Aside = Convert.ToInt32(dr["Aside"]);
            orderMod.PayType = Convert.ToInt32(dr["Delivery"]);
            orderMod.PreMoney = DataConvert.CDouble(dr["Service_charge"]);
            return GetStatus(orderMod, type);
        }
        /// <summary>
        /// 返回状态信息
        /// </summary>
        public static string GetStatus(M_OrderList orderMod, TypeEnum type)
        {
            switch (type)
            {
                case TypeEnum.Pay:
                    return GetPayStatus(orderMod.Paymentstatus);
                case TypeEnum.Order:
                    return GetOrderStatus(orderMod.OrderStatus);
                case TypeEnum.Express:
                    return GetExpStatus(orderMod.StateLogistics);
                case TypeEnum.Aside:
                    return GetOrderStatus(orderMod.OrderStatus,orderMod.Aside,orderMod.StateLogistics);
                case TypeEnum.PayType:
                    return GetPayType(orderMod.PayType, orderMod.PreMoney);
                default:
                    throw new Exception(type.ToString() + "状态码错误");
            }
        }
        public static string GetOrderStatus(int orderStatus, int aside, int exp)
        {
            if (aside == 1) { return "<span style='color:gray;'>已作废</span>"; }
            else { return GetOrderStatus(orderStatus); }
        }
        //订单状态
        public static string GetOrderStatus(int orderstaus)
        {
            try { return OrderConfig.GetOrderStatus(orderstaus); }
            catch { return "<span style='color:orange'>异常状态</span>"; }
        }
        // 是否应该分离为支付类型与是否已支付?
        public static string GetPayStatus(int payemntStatus)
        {
            try{ return OrderConfig.GetPayStatus(payemntStatus); }
            catch (Exception){ return "<span style='color:orange;'>异常状态</span>"; }
        }
        //物流状态
        public static string GetExpStatus(int status)
        {
            try{return OrderConfig.GetExpStatus(status);}
            catch (Exception){ return "<span color=red>异常状态</span>"; }
        }
        // 支付方式
        public static string GetPayType(int type, double money)
        {
            switch ((M_OrderList.PayTypeEnum)type)
            {
                case M_OrderList.PayTypeEnum.PrePay:
                    return OrderConfig.GetPayType(type).Replace("@money",money.ToString("f2"));
                default:
                    return OrderConfig.GetPayType(type);
            }
        }
        //----------------状态判断返回True,False
        /// <summary>
        /// 该订单是否允许支付
        /// </summary>
        public static bool IsCanPay(DataRow dr)
        {
            OrderInfo model = new OrderInfo(dr);
            if (model.Aside != 0) { return false; }
            if (model.PayStatus != (int)M_OrderList.PayEnum.NoPay) { return false; }
            if (model.OrderStatus < (int)M_OrderList.StatusEnum.Normal) { return false; }
            if (model.OrderStatus >= (int)M_OrderList.StatusEnum.OrderFinish) { return false; }
            return true;
        }
        /// <summary>
        /// 该订单是否已付款
        /// </summary>
        public static bool IsHasPayed(DataRow dr)
        {
            OrderInfo model = new OrderInfo(dr);
            return (model.PayStatus >= (int)M_OrderList.PayEnum.HasPayed);
        }
        //如果列中无此值,则默认为有效状态
        private class OrderInfo 
        {
            public OrderInfo(DataRow dr) 
            {
                PayStatus = Convert.ToInt32(dr["PaymentStatus"]);
                OrderStatus = Convert.ToInt32(dr["OrderStatus"]);
                if (dr.Table.Columns.Contains("Aside"))
                { Aside = Convert.ToInt32(dr["Aside"]); }
            }
            public int PayStatus = 0;
            public int OrderStatus = 0;
            public int Aside = 0;
        }
        //----------------付款后回调
        /// <summary>
        /// 用于后台确认支付
        /// </summary>
        public static void FinalStep(M_OrderList mod)
        {
            if (mod.id < 1) { throw new Exception("未指定订单ID"); }
            if (mod.Ordertype < 0) { throw new Exception("未指定订单类型"); }
            if (string.IsNullOrEmpty(mod.OrderNo)) { throw new Exception("未指定订单号"); }
            M_AdminInfo adminMod = B_Admin.GetLogin();
            B_Payment payBll = new B_Payment();
            M_Payment pinfo = new M_Payment();
            pinfo.PaymentNum = mod.OrderNo;
            pinfo.UserID = mod.Userid;
            pinfo.PayNo = payBll.CreatePayNo();
            pinfo.MoneyPay = mod.Ordersamount;
            pinfo.MoneyTrue = pinfo.MoneyPay;//看是否需要支持手输
            pinfo.Status = (int)M_Payment.PayStatus.HasPayed;
            pinfo.CStatus = true;
            if (adminMod != null)
            {
                pinfo.Remark = "管理员确认支付,ID:" + adminMod.AdminId + ",登录名:" + adminMod.AdminName + ",真实姓名:" + adminMod.AdminTrueName;
            }
            pinfo.PaymentID = payBll.Add(pinfo);
            pinfo.SuccessTime = DateTime.Now;
            pinfo.PayPlatID = (int)M_PayPlat.Plat.CashOnDelivery;//默认为线下支付
            pinfo.PlatformInfo = "";
            M_Order_PayLog paylogMod = new M_Order_PayLog();
            FinalStep(pinfo, mod, paylogMod);
        }
        /// <summary>
        /// 异步回调后-->验证支付单状态-->如果正常,更新订单状态
        /// 多张订单在外层循环,这里只处理单订单
        /// </summary>
        /// <param name="mod">订单模型</param>
        /// <param name="paylogMod">订单支付日志模型</param>
        public static void FinalStep(M_Payment pinfo,M_OrderList mod,M_Order_PayLog paylogMod)
        {
            B_Order_PayLog paylogBll = new B_Order_PayLog();
            B_User buser = new B_User();
            //订单已处理,避免重复（如已处理过,则继续处理下一张订单）
            if (mod.OrderStatus >= 99)
            {
                ZLLog.L(Model.ZLEnum.Log.safe, new M_Log()
                {
                    Action = "支付回调异常,订单状态已为99",
                    Message = "订单号:" + mod.OrderNo + ",支付单:" + pinfo.PayNo
                });
                return;
            }
            //已经收到钱了,所以先执行(如多订单,则该值需要看支付单)
            orderBll.UpOrderinfo("Paymentstatus=1,Receivablesamount=" + pinfo.MoneyTrue, mod.id);
            if (mod.Ordertype == (int)M_OrderList.OrderEnum.Domain)//域名订单
            {
                orderBll.UpOrderinfo("OrderStatus=1,PaymentNo='" + pinfo.PayNo + "'", mod.id);
                //Response.Redirect("~/Plugins/Domain/DomReg2.aspx?OrderNo=" + mod.OrderNo);
            }
            else if (mod.Ordertype == (int)M_OrderList.OrderEnum.IDC)//IDC服务
            {
                B_Order_IDC idcBll = new B_Order_IDC();
                orderBll.FinishOrder(mod.id, pinfo);
                idcBll.UpdateEndTimeByNo(mod.OrderNo);
            }
            else if ((mod.Ordertype == (int)M_OrderList.OrderEnum.IDCRen))//IDC服务续费
            {
                B_Order_IDC idcBll = new B_Order_IDC();
                orderBll.FinishOrder(mod.id, pinfo);
                idcBll.RennewTime(mod);
            }
            else if (mod.Ordertype == (int)M_OrderList.OrderEnum.Purse)//余额充值,不支持银币
            {
                buser.ChangeVirtualMoney(mod.Userid, new M_UserExpHis()
                {
                    score = mod.Ordersamount,
                    ScoreType = (int)M_UserExpHis.SType.Purse,
                    detail = "余额充值,订单号:" + mod.OrderNo
                });
                orderBll.FinishOrder(mod.id, pinfo);//成功的订单
            }
            else if (mod.Ordertype == (int)M_OrderList.OrderEnum.Cloud)//虚拟商品订单
            {
               orderBll.FinishOrder(mod.id, pinfo);
            }
            else//其他旅游订单等,只更新状态
            {
                orderBll.FinishOrder(mod.id, pinfo);//成功的订单
            }
            //-------支付成功处理,快照并写入日志
            SaveSnapShot(mod);
            paylogMod.Remind += "订单" + mod.OrderNo + "购买生效";
            paylogMod.OrderID = mod.id;
            paylogMod.PayMoney = mod.Ordersamount;
            paylogMod.PayMethod = (int)M_Order_PayLog.PayMethodEnum.Other;//外部指定
            paylogMod.PayPlatID = pinfo.PayPlatID;
            paylogBll.insert(paylogMod);
            //------商品是否赠送积分
            {
                DataTable prodt = DBCenter.JoinQuery("A.ProID,B.PointVal", "ZL_CartPro", "ZL_Commodities", "A.ProID=B.ID", "A.OrderListID=" + mod.id);
                foreach (DataRow dr in prodt.Rows)
                {
                    double point = DataConvert.CDouble(dr["PointVal"]);
                    if (point > 0)
                    {
                        buser.AddMoney(mod.Userid, point, M_UserExpHis.SType.Point, "购买商品[" + dr["ProID"] + "],赠送积分");
                    }
                }
            }

        }
        /// <summary>
        /// 商品快照,存为mht,
        /// mht缺点 :在于非IE下只能下载,但360等双核浏览器可以自动切换
        /// html缺点:只是保存了页面不存图片和css,这样如果页面删除,则快照失效
        /// </summary>
        public static void SaveSnapShot(M_OrderList orderMod)
        {
            try
            {
                string snapDir = "/UploadFiles/SnapDir/" + orderMod.Rename + orderMod.Userid + "/" + orderMod.OrderNo + "/";
                DataTable dt = cartProBll.SelByOrderID(orderMod.id);
                foreach (DataRow dr in dt.Rows)
                {
                    int storeid = DataConverter.CLng(dr["StoreID"]);
                    int proid = DataConverter.CLng(dr["Proid"]);
                    string url = SiteConfig.SiteInfo.SiteUrl + GetShopUrl(storeid, proid);
                    new HtmlHelper().DownToMHT(url, snapDir + proid + ".mht");
                }
            }
            catch (Exception ex) { ZLLog.L(Model.ZLEnum.Log.exception, "订单:" + orderMod.OrderNo + "快照保存失败,原因:" + ex.Message); }
        }
        //-------------------附加币价格判断
        public static string GetPriceStr(double price, object price_obj)
        {
            if (!HasPrice(price_obj)) //无附加商品价
            {
                return price.ToString("f2");
            }
            string price_json = price_obj.ToString();
            string html = "现金:<span>" + price.ToString("f2") + "</span>";
            if (HasPrice(price_json))
            {
                string json = DataConvert.CStr(price_json);
                M_LinPrice priceMod = JsonConvert.DeserializeObject<M_LinPrice>(json);
                if (priceMod.Purse > 0)
                {
                    html += "|余额:<span>" + priceMod.Purse.ToString("f2") + "</span>";
                }
                if (priceMod.Sicon > 0)
                {
                    html += "|银币:<span>" + priceMod.Sicon.ToString("f0") + "</span>";
                }
                if (priceMod.Point > 0)
                {
                    html += "|积分:<span>" + priceMod.Point.ToString("f0") + "</span>";
                }
            }
            return html;
        }
        public static string GetPriceStr(M_Product model)
        {
            return GetPriceStr(model.LinPrice, model.LinPrice_Json);
        }
        public static string GetPriceStr(M_OrderList model)
        {
            return GetPriceStr(model.Ordersamount, model.AllMoney_Json);
        }
        public static string GetPriceStr(M_Cart model)
        {
            return GetPriceStr(model.AllMoney, model.AllMoney_Json);
        }
        /// <summary>
        /// 是否有附加商品价True:有
        /// </summary>
        public static bool HasPrice(object obj)
        {
            if (obj == null || obj == DBNull.Value || string.IsNullOrEmpty(obj.ToString())) return false;
            string json = DataConvert.CStr(obj);
            M_LinPrice priceMod = JsonConvert.DeserializeObject<M_LinPrice>(json);
            return (priceMod.Purse > 0 || priceMod.Sicon > 0 || priceMod.Point > 0);
        }
        //-------------------支付前订单和支付单检测
        /// <summary>
        /// 订单状态检测,支持支付单与订单,检测状态,过期时间,金额
        /// </summary>
        public static List<M_OrderList> OrdersCheck(M_Payment payMod)
        {
            string[] orders = payMod.PaymentNum.Split(',');
            List<M_OrderList> orderList = new List<M_OrderList>();
            for (int i = 0; i < orders.Length; i++)//全部检测,支付单中订单有任一不符合条件则不允许购买
            {
                M_OrderList orderMod = orderBll.SelModelByOrderNo(orders[i]);
                OrdersCheck(orderMod);
                orderList.Add(orderMod);
            }
            return orderList;
        }
        public static void OrdersCheck(M_OrderList orderMod)
        {
            if (orderMod == null) { function.WriteErrMsg(orderMod.OrderNo + "订单不存在"); }
            if (orderMod.OrderStatus < (int)M_OrderList.OrderEnum.Normal) { function.WriteErrMsg(orderMod.OrderNo + "订单状态异常,无法完成支付"); }
            if (orderMod.Ordersamount <= 0) { function.WriteErrMsg(orderMod.OrderNo + "订单应付金额异常"); }
            if (orderMod.Paymentstatus != (int)M_OrderList.PayEnum.NoPay) { function.WriteErrMsg(orderMod.OrderNo + "订单已支付过,不能重复付款!"); }
            //配置文件,开放性检测
            if (SiteConfig.ShopConfig.IsCheckPay == 1 && orderMod.OrderStatus == (int)M_OrderList.OrderEnum.Normal) { function.WriteErrMsg(orderMod.OrderNo + "订单未确认,请等待确认后再支付!"); }
            if (SiteConfig.ShopConfig.OrderExpired > 0 && (DateTime.Now - orderMod.AddTime).TotalHours > SiteConfig.ShopConfig.OrderExpired) { function.WriteErrMsg(orderMod.OrderNo + "订单已过期,关闭支付功能!"); }
            //if (orderMod.Ordertype == 8)//需要检测库存的商品,如有任意一项不足，则订单不允许进行,主用于云购
            //{
            //    if (!cartProBll.CheckStock(orderMod.id)) function.WriteErrMsg(orderMod.OrderNo + "中的商品库存数量不足,取消购买");
            //}
        }
        //-------------------其它公用方法
        /// <summary>
        /// 获取店铺链接
        /// </summary>
        public static string GetShopUrl(object storeid, object proid)
        {
            return GetShopUrl(DataConvert.CLng(storeid), Convert.ToInt32(proid));
        }
        public static string GetShopUrl(int storeid, int proid)
        {
            return "/Shop/" + proid + ".aspx";
        }
        public static string GetSnapUrl(object uid, object orderNo, object proid)
        {
            if (StrHelper.StrNullCheck(uid.ToString(), orderNo.ToString(), proid.ToString())) { return ""; }
            string url = "/UploadFiles/SnapDir/" + uid + "/" + orderNo + "/" + proid + ".html";
            if (File.Exists(function.VToP(url)))
            {
                return "<a href='" + url + "' target='_blank'>[交易快照]</a>";
            }
            return "";
        }
        /// <summary>
        /// 商品类型与订单类型的转换,后期优化
        /// </summary>
        public static int GetOrderType(int proclass)
        {
            int type = 0;
            switch (proclass)
            {
                case 3://积分
                    type = (int)M_OrderList.OrderEnum.Score;
                    break;
                case 5://虚拟商品
                    type = (int)M_OrderList.OrderEnum.Cloud;
                    break;
                case 6:
                    type = (int)M_OrderList.OrderEnum.IDC;
                    break;
                case 8:
                    type = (int)M_OrderList.OrderEnum.Hotel;
                    break;
                case 7:
                    type = (int)M_OrderList.OrderEnum.Flight;
                    break;
                default:
                    break;
            }
            return type;
        }
    }
}
