using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Shop;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.SQLDAL;

public partial class Manage_Pay_DepostDetail : CustomerPageAction
{
    B_Payment payBll = new B_Payment();
    B_PayPlat platBll = new B_PayPlat();
    B_OrderList orderBll = new B_OrderList();
    B_User buser = new B_User();
    public string ZType { get { return Request.QueryString["ztype"] ?? "payment"; } }
    private int Mid { get { return DataConvert.CLng(Request.QueryString["ID"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        B_ARoleAuth.CheckEx(ZLEnum.Auth.other, "PayManage");
        if (!IsPostBack)
        {
            MyBind();
            string returnUrl = "<a href='PaymentList.aspx'>充值信息管理</a>";
            if (ZType.Equals("pay")) { returnUrl = "<a href='../Shop/PayList.aspx'>支付明细</a>"; }
            Call.SetBreadCrumb(Master, "<li><a href='" + customPath2 + "Config/SiteInfo.aspx'>系统设置</a></li><li>" + returnUrl + "</li><li class='active'>充值单详情</li>");
        }
    }
    private void MyBind()
    {
        M_Payment payMod = payBll.SelReturnModel(Mid);
        M_OrderList orderMod = orderBll.SelModelByOrderNo(payMod.PaymentNum);
        M_UserInfo mu = buser.GetSelect(orderMod.Userid);
        OrderNo_L.Text = orderMod.OrderNo;
        AddTime_L.Text = orderMod.AddTime.ToString();
        MoneyPay_L.Text = payMod.MoneyPay.ToString("f2");
        PayStatus_L.Text = OrderHelper.GetPayStatus(orderMod.Paymentstatus);
        Remark_L.Text = payMod.Remark;
        if (payMod.Status == 3)//已支付
        {
            MoneyTrue_L.Text = payMod.MoneyTrue.ToString("f2");
            PayedTime_L.Text = payMod.SuccessTime.ToString();
            if (payMod.PayPlatID > 0)
            {
                M_PayPlat platMod = platBll.SelReturnModel(payMod.PayPlatID);
                PayPlat_L.Text = platMod.PayPlatName;
            }
        }
        UserName_L.Text = "<a href='javascript:;' onclick='showuser(" + mu.UserID + ");' title='查看用户'>" + mu.UserName + "</a>";
        //UserName_L.Text += "<span> (现有余额：<span style='color:red;'>" + mu.Purse.ToString("f2") + "</span>)</span>";
        if (ZType.Equals("pay")) { Return_L.Text = "<a href='../Shop/PayList.aspx' class='btn btn-primary'>返回列表</a>"; }
        else { Return_L.Text = "<a href='PaymentList.aspx' class='btn btn-primary'>返回列表</a>"; }
    }
}