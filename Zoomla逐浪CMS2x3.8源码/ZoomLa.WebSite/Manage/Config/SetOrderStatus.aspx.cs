using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;

public partial class Manage_Shop_SetOrderStatus : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Call.SetBreadCrumb(Master, "<li><a href='SiteInfo.aspx'>系统设置</a></li><li><a href='SiteInfo.aspx'>网站配置</a></li><li class=\"active\"><a href='SetOrderStatus.aspx'>订单配置</a></li>");
            MyBind();
        }
    }
    public void MyBind()
    {
        //订单状态
        OrderNormal_T.Text = OrderConfig.GetOrderStatus((int)M_OrderList.StatusEnum.Normal);
        OrderSured_T.Text = OrderConfig.GetOrderStatus((int)M_OrderList.StatusEnum.Sured);
        OrderFinish_T.Text = OrderConfig.GetOrderStatus((int)M_OrderList.StatusEnum.OrderFinish);
        UnitFinish_T.Text = OrderConfig.GetOrderStatus((int)M_OrderList.StatusEnum.UnitFinish);
        DrawBack_T.Text = OrderConfig.GetOrderStatus((int)M_OrderList.StatusEnum.DrawBack);
        UnDrawBack_T.Text = OrderConfig.GetOrderStatus((int)M_OrderList.StatusEnum.UnDrawBack);
        CheckDrawBack_T.Text = OrderConfig.GetOrderStatus((int)M_OrderList.StatusEnum.CheckDrawBack);
        CancelOrder_T.Text = OrderConfig.GetOrderStatus((int)M_OrderList.StatusEnum.CancelOrder);
        Recycle_T.Text = OrderConfig.GetOrderStatus((int)M_OrderList.StatusEnum.Recycle);
        //物流状态
        UnDelivery_T.Text = OrderConfig.GetExpStatus(0);
        Delivery_T.Text = OrderConfig.GetExpStatus(1);
        Signed_T.Text =  OrderConfig.GetExpStatus(2);
        UnSiged_T.Text = OrderConfig.GetExpStatus(-1);
        //支付状态
        NoPay_T.Text = OrderConfig.GetPayStatus((int)M_OrderList.PayEnum.NoPay);
        HasPayed_T.Text = OrderConfig.GetPayStatus((int)M_OrderList.PayEnum.HasPayed);
        RequestRefund_T.Text = OrderConfig.GetPayStatus((int)M_OrderList.PayEnum.RequestRefund);
        Refunded_T.Text = OrderConfig.GetPayStatus((int)M_OrderList.PayEnum.Refunded);
        SurePayed_T.Text = OrderConfig.GetPayStatus((int)M_OrderList.PayEnum.SurePayed);
        //支付方式
        PayNormal_T.Text = OrderConfig.GetPayType((int)M_OrderList.PayTypeEnum.Normal);
        PrePay_T.Text = OrderConfig.GetPayType((int)M_OrderList.PayTypeEnum.PrePay);
        HelpReceive_T.Text = OrderConfig.GetPayType((int)M_OrderList.PayTypeEnum.HelpReceive);
    }

    protected void SaveConfig_Btn_Click(object sender, EventArgs e)
    {
        //订单
        OrderConfig.SetOrderStatus((int)M_OrderList.StatusEnum.Normal, OrderNormal_T.Text);
        OrderConfig.SetOrderStatus((int)M_OrderList.StatusEnum.Sured, OrderSured_T.Text);
        OrderConfig.SetOrderStatus((int)M_OrderList.StatusEnum.OrderFinish, OrderFinish_T.Text);
        OrderConfig.SetOrderStatus((int)M_OrderList.StatusEnum.UnitFinish, UnitFinish_T.Text);
        OrderConfig.SetOrderStatus((int)M_OrderList.StatusEnum.DrawBack, DrawBack_T.Text);
        OrderConfig.SetOrderStatus((int)M_OrderList.StatusEnum.UnDrawBack, UnDrawBack_T.Text);
        OrderConfig.SetOrderStatus((int)M_OrderList.StatusEnum.CheckDrawBack, CheckDrawBack_T.Text);
        OrderConfig.SetOrderStatus((int)M_OrderList.StatusEnum.CancelOrder, CancelOrder_T.Text);
        OrderConfig.SetOrderStatus((int)M_OrderList.StatusEnum.Recycle, Recycle_T.Text);
        //物流
        OrderConfig.SetExpStatus(0, UnDelivery_T.Text);
        OrderConfig.SetExpStatus(1, Delivery_T.Text);
        OrderConfig.SetExpStatus(2, Signed_T.Text);
        OrderConfig.SetExpStatus(-1, UnSiged_T.Text);
        //支付状态
        OrderConfig.SetPayStatus((int)M_OrderList.PayEnum.NoPay, NoPay_T.Text);
        OrderConfig.SetPayStatus((int)M_OrderList.PayEnum.HasPayed, HasPayed_T.Text);
        OrderConfig.SetPayStatus((int)M_OrderList.PayEnum.RequestRefund, RequestRefund_T.Text);
        OrderConfig.SetPayStatus((int)M_OrderList.PayEnum.Refunded, Refunded_T.Text);
        OrderConfig.SetPayStatus((int)M_OrderList.PayEnum.SurePayed, SurePayed_T.Text);
        //支付方式
        OrderConfig.SetPayType((int)M_OrderList.PayTypeEnum.Normal, PayNormal_T.Text);
        OrderConfig.SetPayType((int)M_OrderList.PayTypeEnum.PrePay, PrePay_T.Text);
        OrderConfig.SetPayType((int)M_OrderList.PayTypeEnum.HelpReceive, HelpReceive_T.Text);

        OrderConfig.Update();
        function.WriteSuccessMsg("保存成功!");
    }

    protected void ReSet_Btn_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }
}