using System;
using System.Data;
using System.IO;
using ZoomLa.BLL;
using ZoomLa.BLL.Shop;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.SQLDAL;
using ZoomLa.SQLDAL.SQL;
using Winista.Text.HtmlParser.Visitors;
using ZoomLa.BLL.Helper;
using Winista.Text.HtmlParser.Filters;
using System.Web;
using ZoomLa.Components;
using System.Web.UI.WebControls;

/*
 * 商城,店铺,积分订单
 */

namespace ZoomLaCMS.Manage.Shop
{
    public partial class OrderList : CustomerPageAction
    {
        B_OrderList orderBll = new B_OrderList();
        B_Product proBll = new B_Product();
        B_CartPro cpBll = new B_CartPro();
        B_User buser = new B_User();
        B_Payment payBll = new B_Payment();
        OrderCommon orderCom = new OrderCommon();
        public DataTable OrderDT
        {
            get
            {
                if (ViewState["OrderDT"] == null)
                {
                    ViewState["OrderDT"] = cpBll.SelForOrderList(OrderType.ToString(), Filter, ProName_T.Text, OrderNo_T.Text, ReUser_T.Text, Mobile_T.Text, UserID_T.Text, SkeyType_DP.SelectedValue, Skey_T.Text, STime_T.Text, ETime_T.Text,ExpSTime_T.Text,ExpETime_T.Text);
                }
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
                return DataConvert.CStr(Request.QueryString["Filter"]);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            B_Admin.CheckIsLogged(Request.RawUrl);
            if (!B_ARoleAuth.Check(ZLEnum.Auth.shop, "OrderList"))
            {
                function.WriteErrMsg("没有权限进行此项操作");
            }
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request["UserID"])) { UserID_T.Text = Request["UserID"]; }
                DataRow countdr = cpBll.SelOrderCount().Rows[0];
                order_all_l.Text += "(" + countdr["all"] + ")";
                order_unexp_l.Text += "(" + countdr["unexp"] + ")";
                order_unpaid_l.Text += "(" + countdr["unpaid"] + ")";
                order_exped_l.Text += "(" + countdr["exped"] + ")";
                order_finished_l.Text += "(" + countdr["finished"] + ")";
                order_unrefund_l.Text += "(" + countdr["unrefund"] + ")";
                order_refunded_l.Text += "(" + countdr["refunded"] + ")";
                order_recycle_l.Text += "(" + countdr["recycle"] + ")";
                MyBind();
                if (Filter.Equals("recycle")) { op_recycle.Visible = true; }
                else { op_normal.Visible = true; }
                //string bread = "<li><a href='ProductManage.aspx'>商城管理</a></li><li><a href='OrderList.aspx'>订单管理</a></li><li class='active'><a href='OrderList.aspx'>订单列表</a>";
                //bread += " [<a href='AddOrder.aspx?" + Request.QueryString + "' >添加订单</a>]";
                //Call.SetBreadCrumb(Master, bread);
            }
        }
        private void MyBind()
        {
            string fields = "ID,OrderNo,AddUser,AddTime,StoreID,PaymentStatus,OrderType,OrderStatus,OrderMessage,ExpressNum,ParentUserID,UserID,HoneyName,ExpSTime";
            DataTable dt = OrderDT.DefaultView.ToTable(true, fields.Split(','));
            Order_RPT.DataSource = dt;
            Order_RPT.DataBind();
            Skey_T.Text = Skey;
            empty_div.Visible = dt.Rows.Count < 1;
            TotalSum_Hid.Value = DataConvert.CDouble(OrderDT.Compute("SUM(ordersamount)", "")).ToString("f2");
            //function.Script(this, "CheckOrderType('" + Filter + "')");
        }
        protected void Export_Btn_Click(object sender, EventArgs e)
        {

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
            //if (aside != 0)//如果已删除,则不进行其余的判断
            //{
            //    result += "<div><a href='javascript:;' data-oid='" + oid + "' onclick='ConfirmAction(this,\"reconver\");'>还原订单</a></div>";
            //    result += "<div><a href='javascript:;' data-oid='" + oid + "' onclick='ConfirmAction(this,\"realdel\");'>彻底删除</a></div>";
            //    return result;
            //}
            if (payStatus == (int)M_OrderList.PayEnum.NoPay)//未付款,显示倒计时和付款链接
            {
                //bool isnormal = true;
                //订单过期判断
                if (SiteConfig.ShopConfig.OrderExpired > 0)
                {
                    int seconds = GetOrderUnix(dr);
                    if (seconds <= 0)
                    { result += "<div class='gray9'>订单关闭</div>"; }//isnormal = false;
                    else
                    { result += "<div class='ordertime' data-time='" + seconds + "'></div>"; }
                }
                ////订单未完成,且为正常状态,显示付款
                //if (isnormal && OrderHelper.IsCanPay(dr.Row))
                //{
                //    result += "<div><a href='/Payonline/OrderPay.aspx?OrderCode=" + orderNo + "' target='_blank' class='btn btn-primary'>前往付款</a></div>";
                //}
                //result += "<div><a href='javascript:;' data-oid='" + oid + "' onclick='ConfirmAction(this,\"del\");'>移除订单</a></div>";
            }
            else
            {
                //已付款,但处理申请退款等状态
                if (orderStatus < (int)M_OrderList.StatusEnum.Normal)
                {
                    //result += "<a href='AddShare.aspx?CartID=" + dr["CartID"] + "'>取消退款</a><br />";
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
        public string GetStoreName(int storeid)
        {
            DataRow[] dr = StoreDT.Select("ID=" + storeid);
            if (dr.Length > 0) { return dr[0]["StoreName"].ToString(); }
            else { return ""; }
        }
        public string GetPayInfo()
        {
            if (Convert.ToInt32(Eval("PaymentStatus")) == (int)M_OrderList.PayEnum.HasPayed)
            {
                M_Payment payMod = payBll.SelModelByOrderNo(Eval("OrderNo", ""));
                return payMod == null ? "未找到支付单" : payMod.PayNo;
            }
            else { return OrderHelper.GetPayStatus(Convert.ToInt32(Eval("PaymentStatus"))); }
        }
        public string GetExpSTime()
        {
            if (DataConvert.CLng(Eval("ExpressNum")) < 1) { return "<span style='color:red;'>未发货</span>"; }
            else
            {
                return Eval("ExpSTime").ToString();
            }
        }
        public string GetPUserInfo()
        {
            int puid = DataConvert.CLng(Eval("ParentUserID"));
            if (puid < 1) { return "无推荐人"; }
            else
            {
                string puname = buser.GetUserNameByIDS(puid.ToString());
                return "<a href=\"javascript:;\" onclick=\"user.showuinfo('" + puid + "')\">" + puname + "(" + puid + ")</a>";
            }
        }
        //-----------------------------------------------------
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
            int id = Convert.ToInt32(e.CommandArgument);
            switch (e.CommandName)
            {
                case "del2":
                    {
                        orderBll.ChangeStatus(id.ToString(),"recycle");
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
                    string html = "";
                    //收货人
                    html += "<td class='td_l rowtd' rowspan='" + count + "'>";
                    html += "<i class='fa fa-user'></i> <a href='OrderList.aspx?UserID=" + dr["UserID"] + "' title='按用户筛选'>" + B_User.GetUserName(dr["HoneyName"], dr["AddUser"]) + "</a>";
                    html += "(<a href='javascript:;' onclick='user.showuinfo("+dr["UserID"]+");' title='查看用户信息'><span style='color:green;'>" + dr["UserID"] + "</span></a>)";
                    html += "</td>";
                    //金额栏
                    html += "<td class='td_l rowtd' rowspan='" + count + "'>总计:<i class='fa fa-rmb'></i> " + Convert.ToDouble(dr["OrdersAmount"]).ToString("f2") + "<br />";
                    string _paytypeHtml = OrderHelper.GetStatus(dr.Row, OrderHelper.TypeEnum.PayType);
                    if (!string.IsNullOrEmpty(_paytypeHtml)) { _paytypeHtml = _paytypeHtml + "<br />"; }
                    html += _paytypeHtml;
                    html += "<div>商品:<i class='fa fa-rmb'></i> " + (DataConvert.CDouble(dr["OrdersAmount"]) - DataConvert.CDouble(dr["Freight"])).ToString("F2") + "</div>";
                    html += "<div>运费:<i class='fa fa-rmb'></i> " + DataConvert.CDouble(dr["Freight"]).ToString("F2") + "</div>";
                    html += "(" + OrderHelper.GetStatus(dr.Row, OrderHelper.TypeEnum.Pay) + ")</td>";
                    //订单状态
                    html += "<td class='td_md rowtd' rowspan='" + count + "'><span class='gray9'>" + OrderHelper.GetStatus(dr.Row, OrderHelper.TypeEnum.Order) + "</span> <br />";
                    int ordertype = DataConvert.CLng(dr["OrderType"]);
                    //非旅游与酒店订单,则显示快递信息
                    int expid = DataConvert.CLng(dr["ExpressNum"]);
                    if (ordertype != 7 && ordertype != 9)
                    {
                        html += "<a href='javascript:;' class='expview_a' data-expid='" + expid + "' id='expview_" + dr["ID"] + "_a' onclick=\"exp.apilink(this,'" + expid + "');\">" + OrderHelper.GetExpStatus(Convert.ToInt32(dr["StateLogistics"])) + "</span> <br/>";
                    }
                    html += "</td>";
                    //操作栏
                    html += "<td class='td_md rowtd' rowspan='" + count + "'><a href='OrderListInfo.aspx?ID=" + dr["ID"] + "' class='order_bspan'>订单详情</a><br/>" + Getoperator(dr) + "</td>";
                    (e.Item.FindControl("Order_Lit") as Literal).Text = html;
                }
            }
        }
        //-----------------------------------------------------
        protected void DownDetails_B_Click(object sender, EventArgs e)
        {
            Session["Temp_OrderList"] = OrderDT;
            HtmlHelper htmlHelp = new HtmlHelper();
            StringWriter sw = new StringWriter();
            Server.Execute("OrderlistToExcel.aspx", sw);
            string html = sw.ToString();
            HtmlPage page = htmlHelp.GetPage(html);
            html = page.Body.ExtractAllNodesThatMatch(new HasAttributeFilter("id", "orderlist"), true).ToHtml();

            string name = DateTime.Now.ToString("yyyyMMdd") + "订单详单";
            name = HttpUtility.UrlPathEncode(name);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel.numberformat:@";
            Response.Charset = "UTF-8";
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");//设置输出流为简体中文  
            Response.AppendHeader("Content-Disposition", "attachment;filename=" + name + ".xls");
            Response.Write(html);
            Response.End();
            //SafeSC.DownStr(helper.ExportExcel(newDt, colnames), DateTime.Now.ToString("yyyyMMdd") + "订单详单.xls");
        }
        protected void Skey_Btn_Click(object sender, EventArgs e)
        {
            MyBind();
        }
        protected void Sure_Btn_Click(object sender, EventArgs e)
        {
            string ids = Request.Form["idchk"];
            if (!string.IsNullOrEmpty(ids))
            {
                orderBll.ChangeStatus(ids, M_OrderList.StatusEnum.Sured);
            }
            function.WriteSuccessMsg("确认订单成功", "OrderList.aspx");
        }
        protected void Recycle_Btn_Click(object sender, EventArgs e)
        {
            string ids = Request.Form["idchk"];
            if (!string.IsNullOrEmpty(ids))
            {
                orderBll.ChangeStatus(ids,"recycle");
            }
            function.WriteSuccessMsg("移入回收站成功", "OrderList.aspx");
        }
        protected void BatDel_Btn_Click(object sender, EventArgs e)
        {
            string ids = Request.Form["idchk"];
            if (!string.IsNullOrEmpty(ids))
            {
                orderBll.DelByIDS(ids);
                function.WriteSuccessMsg("删除订单成功");
            }
            MyBind();
        }
        protected void Clear_Btn_Click(object sender, EventArgs e)
        {
            orderBll.ClearRecycle();
            function.WriteSuccessMsg("订单回收站清除成功");
        }
        protected void BatRecover_Btn_Click(object sender, EventArgs e)
        {
            string ids = Request.Form["idchk"];
            if (!string.IsNullOrEmpty(ids))
            {
                orderBll.ChangeStatus(ids,"recover");
                function.WriteSuccessMsg("订单恢复成功");
            }
            MyBind();
        }
    }
}