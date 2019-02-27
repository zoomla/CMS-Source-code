using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Shop;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Model.Shop;
using ZoomLa.SQLDAL;

public partial class User_OrderProList : System.Web.UI.Page
{
    B_OrderList orderBll = new B_OrderList();
    M_OrderList orderMod = new M_OrderList();
    B_CartPro cartProBll = new B_CartPro();
    B_S_FloPack packbll = new B_S_FloPack();
    B_Cart cartBll = new B_Cart();
    B_User buser = new B_User();
    B_Order_Exp expBll = new B_Order_Exp();
    OrderCommon orderCom = new OrderCommon();
    //CartID,OrderID,OrderNo
    public string OrderNo { get { return Request.QueryString["OrderNo"]; } }
    //0:订单,1:购物车
    public int SType { get { return DataConvert.CLng(Request.QueryString["SType"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
        }
    }
    public void MyBind()
    {
        DataTable dt = new DataTable();
        M_UserInfo mu = buser.GetLogin();
        switch (SType)
        {
            case 0://订单
                orderMod = orderBll.SelModelByOrderNo(OrderNo);
                
                labelmoney01.Text = orderMod.Receivablesamount.ToString("f2");
                Ordermessage_T.Text = orderMod.Ordermessage;
                if (orderMod == null || orderMod.id == 0) function.WriteErrMsg("订单不存在");
                if (orderMod.Userid != mu.UserID) function.WriteErrMsg("该订单不属于你,无法查看");
                dt = cartProBll.SelByOrderID(orderMod.id);
                if(orderMod.Ordertype == 7||orderMod.Ordertype==9)
                {
                    exp_div.Visible = false;
                    Total_L.Text = orderMod.Ordersamount.ToString("f2") + "元";
                }
                else
                {
                    Total_L.Text = orderMod.Balance_price.ToString("f2") + "+" + orderMod.Freight.ToString("f2") + "=" + orderMod.Ordersamount.ToString("f2") + "元 (商品总价+运费)";
                    M_Order_Exp expMod = expBll.SelReturnModel(DataConverter.CLng(orderMod.ExpressNum));
                    if (expMod != null)
                    {
                        ExpressNum_T.Text = expMod.ExpComp;
                        ExpressDelivery_T.Text = expMod.ExpNo;
                    }
                }
                break;
            case 1://购物车
                dt = cartBll.GetCarProList(OrderNo);
                break;
        }
        if (orderMod.Paymentstatus != (int)M_OrderList.PayEnum.HasPayed)
        {
            PayUrl_A.Visible = true;
            PayUrl_A.HRef = "/PayOnline/OrderPay.aspx?OrderCode=" + orderMod.OrderNo;
        }
        if (dt.Rows.Count > 0 && !string.IsNullOrEmpty(dt.Rows[0]["Additional"].ToString()))
        {
            User_Div.Visible = true;
            M_Cart_Travel model = JsonConvert.DeserializeObject<M_Cart_Travel>(dt.Rows[0]["Additional"].ToString());
            List<M_Cart_Contract> modelList = new List<M_Cart_Contract>();
            modelList.AddRange(model.Guest);
            modelList.AddRange(model.Contract);
            UserRPT.DataSource = modelList;
            UserRPT.DataBind();
        }
        EGV.DataSource = dt;
        EGV.DataBind();
    }
    public string GetMyPrice()
    {
        return OrderHelper.GetPriceStr(DataConvert.CDouble(Eval("AllMoney")), Eval("AllMoney_Json").ToString());
    }
    protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName.ToLower())
        {
            case "del2":
                break;
        }
    }
    protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EGV.PageIndex = e.NewPageIndex;
        MyBind();
    }
    public string GetShopUrl() 
    {
        return orderCom.GetShopUrl(DataConvert.CLng(Eval("StoreID")), DataConvert.CLng(Eval("ProID")));
    }
    public string GetSnapUrl()
    {
        string url = "/UploadFiles/SnapDir/" +buser.GetLogin().UserID + "/" + OrderNo + "/"+Eval("ProID")+".html";
        if (File.Exists(Server.MapPath(url)))
        {
            return "<a href='" + url + "' target='_blank'>[交易快照]</a>";
        }
        return "";
    }
}