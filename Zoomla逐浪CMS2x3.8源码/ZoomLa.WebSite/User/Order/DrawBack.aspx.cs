using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Shop;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Components;

public partial class User_Order_DrawBack : System.Web.UI.Page
{
    B_OrderList orderBll = new B_OrderList();
    B_User buser = new B_User();
    public int Mid { get { return Convert.ToInt32(Request.QueryString["ID"]); } }
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
        M_OrderList orderMod = orderBll.SelReturnModel(Mid);
        if (orderMod == null) { function.WriteErrMsg("订单不存在"); }
        if (orderMod.Userid != mu.UserID) { function.WriteErrMsg("订单不属于你,拒绝操作"); }
        //只有已付款订单,并且未退款才可申请
        if (orderMod.Paymentstatus != (int)M_OrderList.PayEnum.HasPayed) { function.WriteErrMsg("该订单当前支付状态无法退款"); }
        if (orderMod.OrderStatus != 99 && orderMod.OrderStatus != 1 && orderMod.OrderStatus != 0) { function.WriteErrMsg("订单属于不可退款状态"); }
        if (SiteConfig.SiteOption.THDate!=0&&(DateTime.Now - orderMod.AddTime).TotalDays > SiteConfig.SiteOption.THDate) { function.WriteErrMsg("订单已超过"+SiteConfig.SiteOption.THDate+"天,无法申请退款"); }
        //if (orderMod.OrderStatus != 99 && orderMod.OrderStatus!=1) { function.WriteErrMsg("订单属于不可退款状态"); }
        OrderNo_L.Text = orderMod.OrderNo;
        Cdate_L.Text = orderMod.AddTime.ToString("yyyy年MM月dd日 HH:mm");
        Orderamounts_L.Text = OrderHelper.GetPriceStr(orderMod);
    }
    protected void Sure_Btn_Click(object sender, EventArgs e)
    {
        if (Back_T.Text.Length < 10) { function.WriteErrMsg("退款说明最少需十个字符"); }
        M_OrderList orderMod = orderBll.SelReturnModel(Mid);
        orderMod.Settle = orderMod.OrderStatus;
        orderMod.OrderStatus = -1;
        orderMod.Merchandiser = Back_T.Text;
        orderBll.UpdateByID(orderMod);
        function.Script(this, "CloseCur()");
    }
}