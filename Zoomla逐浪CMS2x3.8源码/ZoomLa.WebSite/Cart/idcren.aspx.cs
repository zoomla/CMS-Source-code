using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Shop;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Model.Shop;

public partial class Cart_idcren : System.Web.UI.Page
{
    B_OrderList orderBll = new B_OrderList();
    B_Order_IDC idcBll = new B_Order_IDC();
    B_Product proBll = new B_Product();
    B_Payment payBll = new B_Payment();
    B_User buser = new B_User();
    public string OrderNo { get { return Request.QueryString["OrderNo"] ?? ""; } }
    protected void Page_Load(object sender, EventArgs e)
    {
        B_User.CheckIsLogged(Request.RawUrl);
        if (!IsPostBack)
        {
            M_UserInfo mu = buser.GetLogin();
            M_OrderList orderMod = orderBll.SelModelByOrderNo(OrderNo);
            M_Order_IDC idcMod = idcBll.SelModelByOrderNo(orderMod.OrderNo);
            if (orderMod == null) { function.WriteErrMsg("订单不存在"); }
            if (orderMod.Userid != mu.UserID) { function.WriteErrMsg("你无权对该订单续费"); }
            if (idcMod == null) { function.WriteErrMsg("idc订单不存在"); }
            M_Product proMod = proBll.GetproductByid(idcMod.ProID);
            if (proMod == null) { function.WriteErrMsg("需要续费的商品不存在"); }
            OrderNo_L.Text = orderMod.OrderNo;
            Domain_L.Text = idcMod.Domain;
            Proname_L.Text = proMod.Proname;
            ETime_L.Text = idcMod.ETime.ToString("yyyy/MM/dd");
            DataTable timedt = idcBll.P_GetValid(proMod.IDCPrice);
            IDCTime_DP.DataSource = timedt;
            IDCTime_DP.DataBind();
        }
    }
    protected void Submit_Btn_Click(object sender, EventArgs e)
    {
        //创建一张新订单，完毕后更新老订单时间
        M_OrderList orderMod = orderBll.SelModelByOrderNo(OrderNo);
        M_Order_IDC idcMod = idcBll.SelModelByOrderNo(OrderNo);
        M_Product proMod = proBll.GetproductByid(idcMod.ProID);
        M_UserInfo mu = buser.SelReturnModel(orderMod.Userid);
        if (mu.IsNull) { function.WriteErrMsg("订单未绑定用户[" + orderMod.Userid + "],或用户不存在"); }
        //根据所选,生成新的ID充值订单
        DataRow timeMod = idcBll.GetSelTime(proMod, IDCTime_DP.SelectedValue);
        M_OrderList newMod = orderBll.NewOrder(mu, M_OrderList.OrderEnum.IDCRen);
        newMod.Ordersamount = Convert.ToDouble(timeMod["price"]);
        newMod.Promoter = idcMod.ID;
        newMod.Ordermessage = idcBll.ToProInfoStr(timeMod);
        newMod.id = orderBll.insert(newMod);
        //-----------------------------------------------
        M_Payment payMod = payBll.CreateByOrder(newMod);
        payBll.Add(payMod);
        Response.Redirect("/PayOnline/Orderpay.aspx?PayNo=" + payMod.PayNo);
    }
}