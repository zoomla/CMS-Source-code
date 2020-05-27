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
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
using System.Text;
using ZoomLa.Components;
using ZoomLa.BLL.Shop;
using ZoomLa.SQLDAL;

public partial class User_UserShop_OrderList : System.Web.UI.Page
{
    B_OrderList orderBll = new B_OrderList();
    B_Product Pll = new B_Product();
    B_CartPro Cll = new B_CartPro();
    B_User buser = new B_User();
    B_Content conBll = new B_Content();
    OrderCommon orderCom = new OrderCommon();

    public string OrderStatus { get { return Request.QueryString["orderstatus"] ?? ""; } }
    public string PayStatus { get { return Request.QueryString["PayStatus"] ?? ""; } }
    public int OrderType { get { return DataConverter.CLng(Request.QueryString["ordertype"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
        }
    }
    public void MyBind()
    {
        M_UserInfo mu = buser.GetLogin();
        M_CommonData storeMod = conBll.SelMyStore_Ex();
        int quick = Convert.ToInt32(QuickSearch_DP.SelectedValue);
        int skeyType = Convert.ToInt32(SkeyType_DP.SelectedValue);
        DataTable dt = orderBll.SearchByQuickAndSkey(OrderType.ToString(), OrderStatus, PayStatus, quick, skeyType, Skey_T.Text, storeMod.GeneralID);
        TotalSum_Hid.Value = DataConvert.CDouble(dt.Compute("SUM(ordersamount)", "")).ToString("f2");
        Store_RPT.DataSource = dt;
        Store_RPT.DataBind();
    }
    //实际金额
    public string GetPriceStr(string id)
    {
        M_OrderList orders = orderBll.GetOrderListByid(Convert.ToInt32(id));
        return OrderHelper.GetPriceStr(orders);
    }
    //订单,支付,物流状态
    public string formatzt(string zt, string selects)
    {
        string result = "";
        int status = DataConverter.CLng(zt);
        int type = DataConverter.CLng(selects);
        string url = "OrderList.aspx?OrderType=" + OrderType + "&OrderStatus=" + OrderStatus;
        switch (type)
        {
            case 0:
                result = "<a href='" + url + "&OrderStatus=" + status + "' title='按订单状态筛选'>" + OrderHelper.GetOrderStatus(status) + "</a>";
                break;
            case 1:
                result = "<a href='" + url + "&PayStatus=" + status + "' title='按支付状态筛选'>" + OrderHelper.GetPayStatus(status) + "</a>";
                break;
            case 2:
                result = OrderHelper.GetExpStatus(status);
                break;
            case 3:
                result = OrderHelper.GetPayType(status, DataConverter.CDouble(Eval("Service_charge")));
                break;
            default:
                result = "";
                break;
        }
        return result;
    }
    public string GetOrderNo(string id)
    {
        int aside = Convert.ToInt32(Eval("Aside"));
        return orderCom.GetOrderNo(Convert.ToInt32(id), aside, Eval("OrderNo").ToString());
    }
    public string IsNeedInvo()
    {
        switch (Eval("Developedvotes", ""))
        {
            case "0":
                return "<i class='fa fa-close' style='color:red;'></i>";
            default:
                return "<i class='fa fa-check' style='color:green;'></i>";
        }
    }
    public string GetChkStatus()
    {
        string result = "";
        int payStatus = Convert.ToInt32(Eval("Paymentstatus"));
        if (payStatus >= (int)M_OrderList.PayEnum.HasPayed || Eval("Settle", "").Equals("1"))
        {
            result = "<input name=\"idchk\"  disabled=\"disabled\" type=\"checkbox\"/>";
        }
        else
        {
            result = "<input name=\"idchk\" type=\"checkbox\" value=\"" + Eval("ID") + "\"/>";
        }
        return result;
    }
    public string GetTotalSum() { return TotalSum_Hid.Value; }
    public string GetOP()
    {
        return "<a href='javascript:ShowDetail(\"" + Eval("OrderNo") + "\");'>处理</a>";
    }
    //------------------------------事件处理
    protected void BatDel_Btn_Click(object sender, EventArgs e)
    {
        string CID = Request.Form["idchk"];//订单ID列表
        if (!String.IsNullOrEmpty(CID))
        {
            DataTable Ode = orderBll.GetOrderbyOrderlist(CID);//获得订单列表
            int odcount = Ode.Rows.Count;
            for (int p = 0; p < odcount; p++)
            {
                int CartproOrderid = DataConverter.CLng(Ode.Rows[p]["id"]); //订单ID

                //历遍清单所有商品数量，查找库存///////////////////
                DataTable Unew = Cll.GetCartProOrderID(CartproOrderid); //获得详细清单列表

                for (int s = 0; s < Unew.Rows.Count; s++)
                {
                    int Onum = DataConverter.CLng(Unew.Rows[s]["Pronum"]);
                    int Opid = DataConverter.CLng(Unew.Rows[s]["ProID"]);

                    M_Product pdin = Pll.GetproductByid(Opid);//获得商品信息

                    if (pdin.JisuanFs == 1)
                    {
                        int pstock = pdin.Stock + Onum;//库存结果,返回的商品数量
                        Pll.ProUpStock(Opid, pstock);
                    }
                }
                //////////////////////////////////////////////////////
            }
            orderBll.Delorderlist(CID);
            function.WriteSuccessMsg("删除成功", "OrderList.aspx");
        }
    }
    protected void QuickSearch_DP_SelectedIndexChanged(object sender, EventArgs e)
    {
        MyBind();
    }
    protected void Skey_Btn_Click(object sender, EventArgs e)
    {
        MyBind();
    }
}
