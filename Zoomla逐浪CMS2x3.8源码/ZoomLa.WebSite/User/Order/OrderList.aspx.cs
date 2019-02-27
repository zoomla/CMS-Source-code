using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Common;
using System.Data;
using ZoomLa.BLL.Shop;
using ZoomLa.SQLDAL;
using System.IO;
using ZoomLa.Components;
using ZoomLa.Model.Shop;

public partial class User_Order_OrderList : System.Web.UI.Page
{
    B_User buser = new B_User();
    B_CartPro cartBll = new B_CartPro();
    B_OrderList orderBll = new B_OrderList();
    B_Order_IDC idcBll = new B_Order_IDC();
    OrderCommon orderCom = new OrderCommon();
    public DataTable OrderDT
    {
        get
        {
            if (ViewState["OrderDT"] == null) { ViewState["OrderDT"] = cartBll.U_SelForOrderList(buser.GetLogin().UserID, Filter, Skey, OrderType); }
            return ViewState["OrderDT"] as DataTable;
        }
        set { ViewState["OrderDT"] = value; }
    }
    public DataTable StoreDT
    {
        get
        {
            if (ViewState["StoreDT"] == null) { ViewState["StoreDT"] = orderCom.SelStoreDT(OrderDT); }
            return ViewState["StoreDT"] as DataTable;
        }
        set { ViewState["StoreDT"] = value; }
    }
    public int OrderType
    {
        get
        {
            if (string.IsNullOrEmpty(Request.QueryString["ordertype"])) { return -1; }
            return DataConverter.CLng(Request.QueryString["ordertype"]);
        }
    }
    public string Skey
    {
        get { return Request.QueryString["Skey"]; }
    }
    //all,receive,needpay,comment
    public string Filter
    {
        get
        {
            string _status = Request.QueryString["Filter"] ?? "";
            if (string.IsNullOrEmpty(_status)) { _status = "all"; }
            return _status;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (function.isAjax())
        {
            M_UserInfo mu = buser.GetLogin();
            int oid = Convert.ToInt32(Request.Form["oid"]);
            string action = Request.Form["action"];
            string result = "-1";
            //-----
            M_OrderList orderMod = orderBll.SelReturnModel(oid);
            if (mu.UserID != orderMod.Userid) { Response.Write(result); Response.Flush(); Response.End(); }
            switch (action)
            {
                case "del":
                    {
                        orderBll.DelByIDS_U(oid.ToString(), mu.UserID);
                        result = "1";
                    }
                    break;
                case "receive":
                    {
                        if (orderMod.Paymentstatus < (int)M_OrderList.PayEnum.HasPayed) break;
                        orderBll.UpdateByField("StateLogistics", "2", oid);
                        result = "1";
                    }
                    break;
                case "reconver"://还原
                    orderBll.UpdateByField("Aside", "0", oid);
                    result = "1";
                    break;
                case "realdel"://彻底删除
                    orderBll.UpdateByField("Aside", "2", oid);
                    result = "1";
                    break;
                default:
                    break;
            }
            Response.Write(result); Response.Flush(); Response.End();
        }
        if (!IsPostBack)
        {
            MyBind();
        }
    }
    public void MyBind()
    {
        M_UserInfo mu = buser.GetLogin();
        DataTable dt = OrderDT.DefaultView.ToTable(true, "ID,OrderNo,AddUser,AddTime,StoreID,OrderType,OrderStatus,OrderMessage".Split(','));
        //dt.Columns.Add("OrderMessage");
        //for(int i = 0; i < dt.Rows.Count; i++)
        //{
        //    var query = (from order in OrderDT.AsEnumerable() where order["OrderNo"] == dt.Rows[i]["OrderNo"] select order).First();
        //    dt.Rows[i]["OrderMessage"] = query["OrderMessage"];
        //}
        Order_RPT.DataSource = dt;
        Order_RPT.DataBind();
        Skey_T.Text = Skey;
        function.Script(this, "CheckOrderType('" + Filter + "')");
    }
    //获取订单操作按钮,分为已下单(未付款),已下单(已付款),已完结(显示)
    public string Getoperator(DataRowView dr)
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
            if (isnormal && OrderHelper.IsCanPay(dr.Row))
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
    public int GetOrderUnix(DataRowView dr)
    {
        DateTime addTime = Convert.ToDateTime(dr["AddTime"]);
        int seconds = (SiteConfig.ShopConfig.OrderExpired * 60 * 60) - (int)(DateTime.Now - addTime).TotalSeconds;
        return seconds;
    }
    //是否允许返修,退货等售后服务
    public string GetRepair()
    {
        //已完结状态才能返修,退款等售后,其他情况下申请订单退款
        string guess = DataConvert.CStr(Eval("GuessXML"));
        string result = "";
        int orderStatus = Convert.ToInt32(Eval("OrderStatus"));
        if (Eval("AddStatus").ToString().Contains("exchange"))
        {
            result += "<a href='javascript:;' class='gray9'>已申请换货</a>";
        }
        else if (Eval("AddStatus").ToString().Contains("repair"))
        {
            result += "<a href='javascript:;' class='gray9'>已申请返修</a>";
        }
        else if (Eval("AddStatus").ToString().Contains("drawback"))
        {
            result += "<a href='javascript:;' class='gray9'>已申请退货</a>";
        }
        else if (!string.IsNullOrEmpty(guess) && orderStatus >= (int)M_OrderList.StatusEnum.OrderFinish)
        {
            result += "<a href='ReqRepair.aspx?cid=" + Eval("CartID") + "' class='gray9'>返修/退换货</a>";
        }
        return result;
    }
    public string GetImgUrl()
    {
        return function.GetImgUrl(Eval("Thumbnails"));
    }
    public string GetShopUrl()
    {
        return orderCom.GetShopUrl(DataConvert.CLng(Eval("StoreID")), Convert.ToInt32(Eval("ProID")));
    }
    public string GetMessage()
    {
        if (Convert.ToInt32(Eval("OrderType")) == 7)
        {
            return "<tr class='idm_tr'><td colspan='6'><p class='idcmessage'>详记：" + Eval("Ordermessage", "") + "</p></td></tr>";
        }
        else return "";
    }
    public string GetSnap()
    {
        string result = "";
        int paystatus = Convert.ToInt32(Eval("PaymentStatus"));
        if (paystatus == (int)M_OrderList.PayEnum.HasPayed)
        {
            M_UserInfo mu = buser.GetLogin();
            string dir = "/UploadFiles/SnapDir/" + mu.UserName + mu.UserID + "/" + Eval("OrderNo") + "/" + Eval("ProID") + ".mht";
            if (File.Exists(Server.MapPath(dir))) { result += "<a href='" + dir + "' target='_blank' title='查看快照'> [交易快照]</a>"; }
            if (Convert.ToInt32(Eval("OrderType")) == 7 && Convert.ToInt32(Eval("OrderStatus")) == 99)
            {
                string orderNo = Eval("OrderNo", "");
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
    //---------------RPT
    protected void Order_RPT_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Repeater rpt = e.Item.FindControl("Pro_RPT") as Repeater;
            Label storeName = e.Item.FindControl("Store_L") as Label;
            if (rpt == null || storeName == null)
            {
                return;
            }
            DataRowView drv = e.Item.DataItem as DataRowView;
            storeName.Text = GetStoreName(Convert.ToInt32(drv["StoreID"]));

            OrderDT.DefaultView.RowFilter = "OrderNo='" + drv["OrderNo"] + "'";
            rpt.DataSource = OrderDT.DefaultView.ToTable();
            rpt.DataBind();
        }
    }
    protected void Order_RPT_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        M_UserInfo mu = buser.GetLogin();
        switch (e.CommandName)
        {
            case "del2":
                {
                    int id = Convert.ToInt32(e.CommandArgument);
                    orderBll.DelByIDS_U(id.ToString(), mu.UserID);
                }
                break;
        }
        MyBind();
    }
    protected void Order_RPT_PreRender(object sender, EventArgs e)
    {
        ViewState["StoreDT"] = null;
        ViewState["OrderDT"] = null;
    }
    protected void Pro_RPT_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            //如果变复杂,将其分离为局部页
            if (e.Item.ItemIndex == 0)//首行判断
            {
                DataRowView dr = e.Item.DataItem as DataRowView;
                int count = OrderDT.Select("id=" + dr["ID"]).Length;
                //收货人 <td class='td_md gray9' rowspan='" + count + "'><i class='fa fa-user'> " + dr["AddUser"] + "</i></td>
                string html = "";
                //金额栏
                html += "<td class='td_md rowtd' rowspan='" + count + "'><i class='fa fa-rmb' style='font-weight:600;'>" + Convert.ToDouble(dr["OrdersAmount"]).ToString("f2") + "</i><br />";
                string _paytypeHtml = OrderHelper.GetStatus(dr.Row, OrderHelper.TypeEnum.PayType);
                if (!string.IsNullOrEmpty(_paytypeHtml)) { _paytypeHtml = _paytypeHtml + "<br />"; }
                html += _paytypeHtml;
                html += "(" + OrderHelper.GetStatus(dr.Row, OrderHelper.TypeEnum.Pay) + ")</td>";
                //订单状态
                html += "<td class='td_md rowtd' rowspan='" + count + "'><span class='gray9'>" + OrderHelper.GetStatus(dr.Row, OrderHelper.TypeEnum.Order) + "</span> <br />";
                int ordertype = DataConvert.CLng(dr["OrderType"]);
                if (ordertype != 7 && ordertype != 9) { html += OrderHelper.GetExpStatus(Convert.ToInt32(dr["StateLogistics"])) + " <br/>"; }
                html += "</td>";
                //操作栏
                html += "<td class='td_md rowtd' rowspan='" + count + "'><a href='/User/Order/OrderProList.aspx?OrderNo=" + dr["OrderNo"] + "' class='order_bspan'>订单详情</a><br/>" + Getoperator(dr) + "</td>";
                (e.Item.FindControl("Order_Lit") as Literal).Text = html;
            }
        }
    }
    protected void Search_Btn_Click(object sender, EventArgs e)
    {
        Response.Redirect("OrderList.aspx?Skey=" + Skey_T.Text);
    }
}