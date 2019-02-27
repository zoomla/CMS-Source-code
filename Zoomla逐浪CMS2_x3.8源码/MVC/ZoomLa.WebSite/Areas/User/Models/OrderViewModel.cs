using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ZoomLa.BLL;
using ZoomLa.BLL.Shop;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;
using ZoomLa.Model.Shop;
using ZoomLa.SQLDAL;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLaCMS.Areas.User.Models
{
    public class OrderViewModel
    {
        B_User buser = new B_User();
        B_CartPro cartBll = new B_CartPro();
        B_OrderList orderBll = new B_OrderList();
        B_Order_IDC idcBll = new B_Order_IDC();
        OrderCommon orderCom = new OrderCommon();
        public PageSetting setting = new PageSetting();
        public M_UserInfo mu = null;
        public DataTable OrderDT = null;
        public DataTable OrderProDT = null;
        public DataTable StoreDT = null;
        public int OrderType = -1;
        public string Skey = "";
        //all,receive,needpay,comment
        public string Filter = "";
        public OrderViewModel(int cpage,int psize,M_UserInfo mu, HttpRequestBase Request)
        {
            this.mu = mu;
            this.OrderType = DataConvert.CLng(Request.QueryString["ordertype"], -1);
            this.Skey = DataConvert.CStr(Request.QueryString["skey"]);
            this.Filter = string.IsNullOrEmpty(Request.QueryString["filter"]) ? "all" : Request.QueryString["Filter"];
            string ids = "";
            setting = orderBll.U_SelPage(cpage, psize, mu.UserID, Filter, Skey, OrderType);
            OrderDT = setting.dt;
            foreach (DataRow dr in OrderDT.Rows) { ids += dr["ID"] + ","; }
            ids = ids.TrimEnd(',');
            OrderProDT = cartBll.U_SelForOrderList(ids);
            StoreDT = orderCom.SelStoreDT(OrderDT);
        }
        //获取订单操作按钮,分为已下单(未付款),已下单(已付款),已完结(显示)
        public string Getoperator(DataRow dr)
        {
            M_OrderList orderMod = new M_OrderList();
            string result = "";
            int orderStatus = DataConverter.CLng(dr["OrderStatus"]);
            int payStatus = DataConverter.CLng(dr["Paymentstatus"]);
            int oid = Convert.ToInt32(dr["ID"]);
            int aside = Convert.ToInt32(dr["Aside"]);
            string orderNo = dr["OrderNo"].ToString();
            //----------如果已经删除,则不执行其余的判断
            if (aside != 0)//如果已删除,则不进行其余的判断
            {
                result += "<div><a href='javascript:;' data-oid='" + oid + "' onclick='ConfirmAction(this,\"reconver\");'>还原订单</a></div>";
                result += "<div><a href='javascript:;' data-oid='" + oid + "' onclick='ConfirmAction(this,\"realdel\");'>彻底删除</a></div>";
                return result;
            }
            if (payStatus == (int)M_OrderList.PayEnum.NoPay)//未付款,显示倒计时和付款链接
            {
                bool isnormal = true;
                //订单过期判断
                if (SiteConfig.ShopConfig.OrderExpired > 0)
                {
                    int seconds = GetOrderUnix(dr);
                    if (seconds <= 0)
                    { result += "<div class='gray9'>订单关闭</div>"; isnormal = false; }
                    else
                    { result += "<div class='ordertime' data-time='" + seconds + "'></div>"; }
                }
                //订单未完成,且为正常状态,显示付款
                if (isnormal && OrderHelper.IsCanPay(dr))
                {
                    result += "<div><a href='/Payonline/OrderPay.aspx?OrderCode=" + orderNo + "' target='_blank' class='btn btn-primary'>前往付款</a></div>";
                }
                result += "<div><a href='javascript:;' data-oid='" + oid + "' onclick='ConfirmAction(this,\"del\");'>取消订单</a></div>";
            }
            else
            {
                //---物流
                switch (Convert.ToInt32(dr["StateLogistics"]))
                {
                    case 1:
                        if (Convert.ToInt32(dr["Ordertype"]) != 7 && Convert.ToInt32(dr["Ordertype"]) != 9)
                        {
                            result += "<div><a href='javascript:;' class='btn btn-primary' data-oid='" + oid + "' onclick='ConfirmAction(this,\"receive\");'>确认收货</a></div>";
                        }
                        break;
                }
                //已付款,但处理申请退款等状态
                if (orderStatus < (int)M_OrderList.StatusEnum.Normal)
                {
                    //result += "<a href='AddShare.aspx?CartID=" + dr["CartID"] + "'>取消退款</a><br />";
                }
                //已付款未完结,可申请退款
                if (orderStatus >= (int)M_OrderList.StatusEnum.Normal && orderStatus < (int)M_OrderList.StatusEnum.OrderFinish)
                {
                    result += "<a href='javascript:;' onclick='ShowDrawback(" + oid + ");'>取消订单</a><br />";
                }
                //已付款已完结,可评价晒单
                if (orderStatus >= (int)M_OrderList.StatusEnum.OrderFinish)//已完结
                {
                    if (!(dr["AddStatus"].ToString().Contains("comment")))
                    {
                        if (Convert.ToInt32(dr["OrderType"]) == 7 && Convert.ToInt32(dr["OrderStatus"]) == 99)
                        {
                            M_Order_IDC idcMod = idcBll.SelModelByOrderNo(orderNo);
                            if (idcMod != null)
                            {
                                result += "<a href='/cart/idcren.aspx?orderno=" + orderNo + "' target='_blank' title='充值续费'>续费</a><br />";
                            }
                        }
                        result += "<a href='AddShare.aspx?OrderID=" + dr["ID"] + "' title='评价'><i class='fa fa-comments'></i></a><br />";
                        //result += "<a href='/Shop/" + dr["ProID"] + ".aspx' target='_blank' class='btn btn-primary'>立即购买</a>";
                    }
                }
            }
            return result;
        }
        //还差多少分钟到期
        public int GetOrderUnix(DataRow dr)
        {
            DateTime addTime = Convert.ToDateTime(dr["AddTime"]);
            int seconds = (SiteConfig.ShopConfig.OrderExpired * 60 * 60) - (int)(DateTime.Now - addTime).TotalSeconds;
            return seconds;
        }
        public string GetRepair(DataRow dr)
        {
            //已完结状态才能返修,退款等售后,其他情况下申请订单退款
            string guess = DataConvert.CStr(dr["GuessXML"]);
            string result = "";
            int orderStatus = Convert.ToInt32(dr["OrderStatus"]);
            if (dr["AddStatus"].ToString().Contains("exchange"))
            {
                result += "<a href='javascript:;' class='gray9'>已申请换货</a>";
            }
            else if (dr["AddStatus"].ToString().Contains("repair"))
            {
                result += "<a href='javascript:;' class='gray9'>已申请返修</a>";
            }
            else if (dr["AddStatus"].ToString().Contains("drawback"))
            {
                result += "<a href='javascript:;' class='gray9'>已申请退货</a>";
            }
            else if (!string.IsNullOrEmpty(guess) && orderStatus >= (int)M_OrderList.StatusEnum.OrderFinish)
            {
                result += "<a href='ReqRepair.aspx?cid=" + dr["CartID"] + "' class='gray9'>返修/退换货</a>";
            }
            return result;
        }
        public string GetImgUrl(DataRow dr)
        {
            return function.GetImgUrl(dr["Thumbnails"]);
        }
        public string GetShopUrl(DataRow dr)
        {
            return orderCom.GetShopUrl(DataConvert.CLng(dr["StoreID"]), Convert.ToInt32(dr["ProID"]));
        }
        public string GetMessage(DataRow dr)
        {
            if (Convert.ToInt32(dr["OrderType"]) == 7)
            {
                return "<tr class='idm_tr'><td colspan='6'><p class='idcmessage'>详记：" + dr["Ordermessage"] + "</p></td></tr>";
            }
            else return "";
        }
        public string GetSnap(DataRow dr)
        {
            string result = "";
            int paystatus = Convert.ToInt32(dr["PaymentStatus"]);
            if (paystatus == (int)M_OrderList.PayEnum.HasPayed)
            {
                string dir = "/UploadFiles/SnapDir/" + mu.UserName + mu.UserID + "/" + dr["OrderNo"] + "/" + dr["ProID"] + ".mht";
                if (File.Exists(function.VToP(dir))) { result += "<a href='" + dir + "' target='_blank' title='查看快照'> [交易快照]</a>"; }
                if (Convert.ToInt32(dr["OrderType"]) == 7 && Convert.ToInt32(dr["OrderStatus"]) == 99)
                {
                    string orderNo = DataConvert.CStr(dr["OrderNo"]);
                    M_Order_IDC idcMod = idcBll.SelModelByOrderNo(orderNo);
                    if (idcMod != null)
                    {
                        result += "<span style='color:green;font-size:12px;'>(到期时间:" + idcMod.ETime.ToString("yyyy/MM/dd") + ") </span>";
                    }
                }
            }
            return result;
        }
        public string GetStoreName(int storeid)
        {
            DataRow[] dr = StoreDT.Select("ID=" + storeid);
            if (dr.Length > 0) { return dr[0]["StoreName"].ToString(); }
            else { return ""; }
        }
        public DataTable GetProByOrder(string orderNo) 
        {
            OrderProDT.DefaultView.RowFilter = "OrderNo='" + orderNo + "'";
            return OrderProDT.DefaultView.ToTable();
        }
        //绑定订单操作列
        public MvcHtmlString BindOrderOP(DataRow dr) 
        {
            int count = OrderProDT.Select("id=" + dr["ID"]).Length;
            //收货人 <td class='td_md gray9' rowspan='" + count + "'><i class='fa fa-user'> " + dr["AddUser"] + "</i></td>
            string html = "";
            //金额栏
            html += "<td class='td_md rowtd' rowspan='" + count + "'><i class='fa fa-rmb' style='font-weight:600;'>" + Convert.ToDouble(dr["OrdersAmount"]).ToString("f2") + "</i><br />";
            string _paytypeHtml = OrderHelper.GetStatus(dr, OrderHelper.TypeEnum.PayType);
            if (!string.IsNullOrEmpty(_paytypeHtml)) { _paytypeHtml = _paytypeHtml + "<br />"; }
            html += _paytypeHtml;
            html += "(" + OrderHelper.GetStatus(dr, OrderHelper.TypeEnum.Pay) + ")</td>";
            //订单状态
            html += "<td class='td_md rowtd' rowspan='" + count + "'><span class='gray9'>" + OrderHelper.GetStatus(dr, OrderHelper.TypeEnum.Order) + "</span> <br />";
            int ordertype = DataConvert.CLng(dr["OrderType"]);
            if (ordertype != 7 && ordertype != 9) { html += OrderHelper.GetExpStatus(Convert.ToInt32(dr["StateLogistics"])) + " <br/>"; }
            html += "</td>";
            //操作栏
            html += "<td class='td_md rowtd' rowspan='" + count + "'><a href='OrderProList?OrderNo=" + dr["OrderNo"] + "' class='order_bspan'>订单详情</a><br/>" + Getoperator(dr) + "</td>";
            return MvcHtmlString.Create(html);
        }
    }
}