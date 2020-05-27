namespace ZoomLaCMS.PayOnline
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using ZoomLa.BLL;
    using ZoomLa.BLL.Helper;
    using ZoomLa.BLL.WxPayAPI;
    using ZoomLa.Common;
    using ZoomLa.Components;
    using ZoomLa.Model;
    public partial class Donate : System.Web.UI.Page
    {
        B_Payment payBll = new B_Payment();
        B_OrderList orderBll = new B_OrderList();
        B_User buser = new B_User();
        M_UserInfo mu = new M_UserInfo();
        public double Money { get { return DataConverter.CDouble(Request.QueryString["money"]); } }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Money > 0) { GetDonate(Money); }
            }
        }
        protected void Donate_B_Click(object sender, EventArgs e)
        {
            double IMoney = DataConverter.CDouble(Money_T.Text);
            GetDonate(IMoney);
        }
        public void GetDonate(double Money)
        {
            if (Money < 0.01) { function.WriteErrMsg("请输入有效的金额"); }
            //mu = buser.SelReturnModel(Identity);
            mu = buser.GetLogin();
            M_OrderList Odata = orderBll.NewOrder(mu, M_OrderList.OrderEnum.Donate);
            Odata.Ordermessage = "打赏：" + Money + "元";
            Odata.Ordersamount = Money;
            Odata.Userid = mu.IsNull ? Int32.MaxValue : mu.UserID;
            Odata.Balance_price = Odata.Ordersamount;
            Odata.Specifiedprice = Odata.Ordersamount;
            Odata.id = orderBll.Adds(Odata);

            M_Payment payMod = payBll.CreateByOrder(Odata);
            payMod.Remark = "打赏：" + Money + "元";
            payMod.SysRemark = "donate";
            payMod.PaymentID = payBll.Add(payMod);

            //判断是不是来自微信
            if (DeviceHelper.GetBrower() == DeviceHelper.Brower.Micro)
            {
                //弹出微信支付窗口
                Response.Redirect("/PayOnline/wxpay_mp.aspx?PayNo=" + payMod.PayNo);
            }
            else
            {
                Response.Redirect("/PayOnline/Orderpay.aspx?PayNo=" + payMod.PayNo + "&OrderCode=" + Odata.OrderNo);
            }
        }
    }
}