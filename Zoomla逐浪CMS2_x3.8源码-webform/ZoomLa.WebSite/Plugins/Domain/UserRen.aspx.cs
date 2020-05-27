using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Site;
using ZoomLa.Common;
using ZoomLa.Model;

/*
 * IDC续费页面，接受订单的OrderNo
 * 续费后,原订单终止并到期,新订单重新生效
 */ 
public partial class Plugins_Domain_UserRen : System.Web.UI.Page
{
    //订单
    protected B_OrderList orderBll = new B_OrderList();
    protected M_OrderList orderMod = new M_OrderList();
    protected OrderCommon orderCom = new OrderCommon();
    //购物车
    protected B_CartPro cartBll = new B_CartPro();
    private B_Product bll = new B_Product();
    protected B_User buser = new B_User();

    protected B_Product proBll = new B_Product();
    protected M_Product proModel = new M_Product();
    private string OrderNO { get { return Request.QueryString["OrderNo"]; } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            if (string.IsNullOrEmpty(OrderNO)) { function.WriteErrMsg("未指定订单号"); }
            DataBind();
        }
    }
    public void DataBind(string key="") 
    {
        M_UserInfo mu = buser.GetLogin();
        M_OrderList orderMod = orderBll.SelModelByOrderNo(OrderNO);
        DataTable dt = orderBll.SelByOrderNo(OrderNO);
        if (dt == null || dt.Rows.Count < 1) { function.WriteErrMsg("订单不存在,无法续费"); return; }
        if (orderMod.Userid != mu.UserID) { function.WriteErrMsg("非本人订单,拒绝查看"); }
        OrderRP.DataSource = dt;
        OrderRP.DataBind();
        GetToPay(dt.Rows[0]["ProID"].ToString(), 1);
    }
    public void GetToPay(string proID, int num)
    {
        num = num < 1 ? 1 : num;
        dataField.Value = proID;
        proModel = proBll.GetproductByid(Convert.ToInt32(proID));
        if (proModel.ProClass != (int)M_Product.ClassType.IDC) function.WriteErrMsg("该商品并不是IDC服务商品,不允许在该页购买!!!");
        proNameL.Text = proModel.Proname;
        proNameL.NavigateUrl = "/Shop.aspx?ID=" + proModel.ItemID;
        proNameL.Target = "_ViewDetail";
        proNum.Text = num.ToString();
        //proPeriod.Text = bll.GetServerPeriod(proModel.ServerPeriod, proModel.ServerType);
        proRemind.Text = proModel.ExpRemind > 0 ? "提前" + proModel.ExpRemind + "天" : "不提醒";
        proPic.Src = proModel.ThumbPath;
        proPrice.Text = proModel.LinPrice.ToString(".00");
        proDetail.Text = proModel.Proinfo;
    }
    //绑定主机
    public string BindSite(object e) 
    {
        return orderCom.IsBindSite(e);
    }
    //是否到期
    public string IsExpire(object e)
    {
        return orderCom.IsExpire(e);
    }
    //确定续费
    protected void sureBtn_Click(object sender, EventArgs e)
    {
        CreateNewOrder();
    }
    //生成新订单
    public void CreateNewOrder() 
    {
        M_UserInfo mu = buser.GetLogin();
        proModel = proBll.GetproductByid(Convert.ToInt32(dataField.Value));
        int num = Convert.ToInt32(proNum.Text.Trim());

        orderMod.Ordersamount = Convert.ToDouble(num * proModel.LinPrice);
        orderMod.OrderNo = B_OrderList.CreateOrderNo(M_OrderList.OrderEnum.IDCRen);
        orderMod.Ordertype = (int)M_OrderList.OrderEnum.IDCRen;//IDC服务商品续费
        orderMod.Receiver = mu.UserName;
        orderMod.Reuser = mu.UserName;
        orderMod.Userid = mu.UserID;
        //检测旧订单是否存在，如存在，则将OrderID存入，否则报错
        DataTable oldDT = orderBll.SelByOrderNo(OrderNO);
        if (oldDT == null || oldDT.Rows.Count < 1)
        {
            function.WriteErrMsg("出错,需续费订单不存在,请联系管理员!!!");
        }
        orderMod.Ordermessage = oldDT.Rows[0]["ID"].ToString();//旧购物车ID
        //Odata.AddUser = siteListDP.SelectedValue;//跟单员,此处记录对应ID
        //Odata.Internalrecords = siteListDP.SelectedItem.Text;//内部记录,此处用来存与主机的关联信息
        //添加订单，添加数据库购物车
        orderMod.id = orderBll.insert(orderMod);
        M_CartPro cartModel = new M_CartPro();
        cartModel.Orderlistid = orderMod.id;
        cartModel.ProID = proModel.ID;
        cartModel.Pronum = num;
        cartModel.Proname = proModel.Proname;
        cartModel.Shijia = proModel.LinPrice;
        cartModel.AllMoney = orderMod.Ordersamount;
        //cartModel.EndTime = proBll.GetEndTime(proModel, num);
        cartModel.type = (int)M_OrderList.OrderEnum.IDCRen;
        cartBll.Add(cartModel);
        Response.Redirect("~/PayOnline/Orderpay.aspx?OrderCode=" + orderMod.OrderNo);
    }
}