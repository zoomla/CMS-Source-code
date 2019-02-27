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
using System.Text;
using System.Net.Mail;
using System.Xml;
using System.Globalization;
using System.Collections.Generic;
using ZoomLa.BLL.Shop;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;

using Newtonsoft.Json;

public partial class User_UserShop_Orderlistinfo : System.Web.UI.Page
{
    private B_CartPro cartProBll = new B_CartPro();
    private B_Model bmode = new B_Model();
    private B_Product proBll = new B_Product();
    private B_OrderList oll = new B_OrderList();
    private B_PayPlat platBll = new B_PayPlat();
    protected B_Stock Sll = new B_Stock();
    protected B_InvtoType binvtype = new B_InvtoType();
    protected B_Order_PayLog paylogBll = new B_Order_PayLog();
    protected M_Order_PayLog paylogMod = new M_Order_PayLog();
    protected OrderCommon orderCom = new OrderCommon();
    B_User buser = new B_User();
    public int PayClass = 0;
    //----------用户中心
    B_Content conBll = new B_Content();
    //OrderID
    public int Mid { get { return Convert.ToInt32(Request.QueryString["ID"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Mid < 1) { function.WriteErrMsg("未指定订单"); }
        if (!IsPostBack)
        {
            M_UserInfo mu = buser.GetLogin();
            M_CommonData storeMod = conBll.SelMyStore(mu.UserName);
            M_OrderList orderinfo = oll.GetOrderListByid(Mid);
            if (storeMod == null) { function.WriteErrMsg("你还没有开通店铺"); }
            if (orderinfo == null || orderinfo.id < 1) { function.WriteErrMsg("订单不存在"); }
            if (orderinfo.StoreID != storeMod.GeneralID) { function.WriteErrMsg("该订单不属于你店铺"); }
            //----------------------
            Label1.Text = "订 单 信 息（订单编号：" + orderinfo.OrderNo + "）";
            OrderNo_L.Text = orderinfo.OrderNo;
            Orderamounts_L.Text = orderinfo.Ordersamount.ToString("f2");
            ReturnStatu_L.Text = orderinfo.OrderStatus == -2 ? "拒绝" : "通过";
            isCheckRe_L.Text = orderinfo.Guojia;
            Cdate_L.Text = orderinfo.AddTime.ToString("yyyy年MM月dd日 HH:mm");
            Reuser.Text = orderinfo.Reuser.ToString();
            Rename.InnerHtml = " <a onclick=\"opentitle('../User/Userinfo.aspx?id=" + orderinfo.Userid + "','查看会员')\" href='###' title='查看会员'>" + orderinfo.Rename.ToString() + "</a>";
            if (orderinfo.StateLogistics != 0) { ShowSend_Btn.Enabled = false; }
            if (orderinfo.StateLogistics != 1) { Button7.Enabled = false; }
            if (orderinfo.Paymentstatus == (int)M_OrderList.PayEnum.HasPayed)
            {
                Paymentstatus.Text = "<font color=blue>已经汇款</font>";
                Button2.Enabled = false;
            }
            else
            {
                Paymentstatus.Text = "<font color=red>等待汇款</font>";
                Button2.Enabled = true;
                ShowSend_Btn.Enabled = false;
            }
            switch ((M_OrderList.StatusEnum)orderinfo.OrderStatus)
            {
                case M_OrderList.StatusEnum.Normal:
                    SureOrder_Btn.Enabled = true;
                    break;
                case M_OrderList.StatusEnum.DrawBack:
                    SureOrder_Btn.Enabled = false;
                    Button5.Enabled = false;
                    CheckReturn.Enabled = true;
                    UnCheckRetrun.Enabled = true;
                    ShowSend_Btn.Enabled = false;
                    CompleteOrder_Btn.Enabled = false;
                    break;
                case M_OrderList.StatusEnum.UnDrawBack:
                case M_OrderList.StatusEnum.CheckDrawBack:
                    Button15.Enabled = false;
                    Button11.Enabled = false;
                    SureOrder_Btn.Enabled = false;
                    Button5.Enabled = false;
                    ShowSend_Btn.Enabled = false;
                    CompleteOrder_Btn.Enabled = false;
                    break;
                case M_OrderList.StatusEnum.OrderFinish:
                case M_OrderList.StatusEnum.UnitFinish:
                    CompleteOrder_Btn.Enabled = false;
                    break;
                default:
                    break;
            }
            switch (orderinfo.StateLogistics)
            {
                case 1:
                    StateLogistics.Text = "<font color=blue>已发货</font>(快递单号：" + orderinfo.Company + "/" + orderinfo.ExpressNum + ")";
                    break;
                default:
                    StateLogistics.Text = OrderHelper.GetExpStatus(orderinfo.StateLogistics);
                    break;
            }
            DrawBackStr.Text = orderinfo.Merchandiser;
            OrderStatus.Text = OrderHelper.GetOrderStatus(orderinfo.OrderStatus);
            adddate.Text = orderinfo.AddTime.ToShortDateString();
            addtime.Text = orderinfo.AddTime.ToString();
            if (orderinfo.Invoiceneeds == 1)
            {
                Invoiceneeds.Text = "<font color=blue>√</font>";
            }
            else
            {
                Invoiceneeds.Text = "<font color=red>×</font>";
            }
            if (orderinfo.Developedvotes == 1)
            {
                Developedvotes.Text = "<font color=blue>√</font>";
            }
            else
            {
                Developedvotes.Text = "<font color=red>×</font>";
            }
            if (orderinfo.Invoiceneeds == 1)
            {
                M_InvtoType invtype = binvtype.GetSelect(orderinfo.InvoType);
                if (invtype != null && invtype.ID > 0)
                {
                    Developedvotes.Text += " 发票类型：" + invtype.InvtoType;
                    lblInv.Text = invtype.Invto + "%";
                }
            }
            if (orderinfo.Integral > 0 && orderinfo.Paymentstatus == (int)M_OrderList.PayEnum.HasPayed)
            {
                if (orderinfo.SendPointStatus == 1)
                {
                    sendScore.Visible = false;
                    ISsend.Text = "已送出";
                }
            }
            else
            {
                sendScore.Visible = false;
            }
            //Button7.Enabled = orderinfo.Signed == 1 ? false : true;//更改状态,客户已签收
            Button2.Enabled = orderinfo.Paymentstatus == (int)M_OrderList.PayEnum.HasPayed ? false : true;//更改状态,已支付
            Button9.Enabled = orderinfo.Settle == 1 ? false : true;//冻结
            Button13.Enabled = orderinfo.Suspended == 1 ? false : true;//暂停
            if (orderinfo.Aside == 1)//已作废
            {
                Button2.Enabled = false;
                //ShowSend_Btn.Enabled = false;
                SureOrder_Btn.Enabled = false;
                Button5.Enabled = false;
                Button7.Enabled = false;
                Button9.Enabled = false;
                Button11.Enabled = false;
                Button13.Enabled = false;
                Button6.Enabled = false;
            }
            else
            {
                Button6.Enabled = true;
            }
            if (orderinfo.Settle == 1)//结清
            {
                Button2.Enabled = false;
                //ShowSend_Btn.Enabled = false;
                SureOrder_Btn.Enabled = false;
                Button5.Enabled = false;
                Button6.Enabled = false;
                Button7.Enabled = false;
                Button11.Enabled = false;
                Button13.Enabled = false;
            }
            if (orderinfo.Payment > 0)//支付后才有值
            {
                M_PayPlat payPlatMod = platBll.GetPayPlatByid(orderinfo.Payment);
                PayClass = payPlatMod.PayClass;
                Payment.Text = payPlatMod.PayPlatName.ToString();
                B_Payment paymentBLL = new B_Payment();
            }
            Reusers.Text = orderinfo.Reuser.ToString();//订货人
            Jiedao.Text = orderinfo.Shengfen + " " + orderinfo.Jiedao;//地址
            Email.Text = orderinfo.Email.ToString();//电子信
            Invoice.Text = orderinfo.Invoice.ToString();//发票信息
            if (orderinfo.Outstock == 1)
            {
                Outstock.Text = "缺货时，取消此订单";
            }
            else
            {
                Outstock.Text = "缺货时，将有货的商品发出，取消无货商品的订购";
            }
            Deliverytime.Text = orderinfo.Deliverytime.ToString();//送货时间
            switch (orderinfo.Deliverytime)
            {
                case 1:
                    Deliverytime.Text = "对送货时间没有特殊要求";
                    break;
                case 2:
                    Deliverytime.Text = "双休日或者周一至周五的晚上送达";
                    break;
                case 3:
                    Deliverytime.Text = "周一至周五的白天送达";
                    break;
                default:
                    break;
            }
            Phone.Text = orderinfo.Phone.ToString();//联系电话
            ZipCode.Text = orderinfo.ZipCode.ToString();//邮政编码
            Mobile.Text = orderinfo.MobileNum;//手机
            AddUser.Text = orderinfo.AddUser.ToString();//负责跟单人员
            Internalrecords.Text = orderinfo.Internalrecords.ToString();//内部记录
            inter_Div.InnerHtml = orderinfo.Internalrecords;
            inter_Text.Text = orderinfo.Internalrecords;
            Ordermessage.Text = orderinfo.Ordermessage;//订货留言
            omg_Text.Text = orderinfo.Ordermessage;
            LabScore.Text = orderinfo.Integral.ToString();//积分
            //-------购物车
            DataTable cplist = cartProBll.GetCartProOrderID(Mid);
            procart.DataSource = cplist;
            procart.DataBind();
            if (cplist.Rows.Count > 0 && !string.IsNullOrEmpty(cplist.Rows[0]["Additional"].ToString()))
            {
                M_Cart_Travel model = JsonConvert.DeserializeObject<M_Cart_Travel>(cplist.Rows[0]["Additional"].ToString());
                List<M_Cart_Contract> modelList = new List<M_Cart_Contract>();
                modelList.AddRange(model.Guest);
                modelList.AddRange(model.Contract);
                UserRPT.DataSource = modelList;
                UserRPT.DataBind();
            }
            DataTable newtable = cplist.DefaultView.ToTable(false, "Shijia", "Pronum");
            double allmoney = 0;
            for (int i = 0; i < newtable.Rows.Count; i++)
            {
                allmoney = allmoney + DataConverter.CDouble(cplist.Rows[i]["Allmoney"]);
            }
            //-------
            Label2.Text = orderinfo.Ordersamount.ToString();
            Label29.Text = orderinfo.Ordersamount.ToString("f2");
            Label31.Text = (orderinfo.Ordersamount).ToString("f2") + "元";// tempalljia.ToString();
            Label28.Text = orderinfo.Receivablesamount.ToString("f2") + "元";
            if (orderinfo.Paymentstatus == (int)M_OrderList.PayEnum.HasPayed)
            {
                paylogMod = paylogBll.SelModelByOrderID(orderinfo.id);
                if (paylogMod != null)
                {
                    switch (paylogMod.PayMethod)
                    {
                        case (int)M_Order_PayLog.PayMethodEnum.Purse:
                            PayType.Text = "余额";
                            PayMoney.Text = paylogMod.PayMoney.ToString(".00") + "元";
                            break;
                        case (int)M_Order_PayLog.PayMethodEnum.Silver:
                            PayType.Text = "银币";
                            PayMoney.Text = paylogMod.PayMoney.ToString(".00") + "元";
                            break;
                        case (int)M_Order_PayLog.PayMethodEnum.Other:
                            PayType.Text = "其他";
                            break;
                        default:
                            PayType.Text = "";
                            break;
                    }
                }
            }
            else
            {
                payed.Visible = false;
                nopayed.Visible = true;
            }
        }
    }
    //获取价格
    public string GetProPrice(string proclass, string type, string proid)
    {
        int pid = DataConverter.CLng(proid);
        M_Product proMod = proBll.GetproductByid(pid);
        switch (type)
        {
            case "1":
                return proMod.ShiPrice.ToString("f2");
            case "2":
            default:
                return proMod.LinPrice.ToString("f2");
        }
    }
    //商品备注
    public string beizhu(string proid)
    {
        M_Product proMod = proBll.GetproductByid(DataConverter.CLng(proid));
        switch (proMod.ProClass)
        {
            default:
                break;
        }
        return "";
    }
    //获取价格
    public string GetPrice()
    {
        return OrderHelper.GetPriceStr(Convert.ToDouble(Eval("AllMoney")), Eval("AllMoney_Json").ToString());
    }
    //获取期限
    public string qixian(string cid)
    {
        return "";
    }
    public string GetLinPrice()
    {
        return OrderHelper.GetPriceStr(Convert.ToDouble(Eval("LinPrice")), Eval("LinPrice_Json"));
    }
    //客户已签收
    protected void Button7_Click(object sender, EventArgs e)
    {
        oll.UpOrderinfo("Signed=1", Mid);
        Response.Redirect(Request.RawUrl);
    }
    protected string Getprotype()
    {
        if (DataConverter.CLng(Eval("Priority")) == 1 && DataConverter.CLng(Eval("Propeid")) > 0)
        {
            return "<span style='color:green;'>[绑]</span>";
        }
        return "";
    }
    //暂停处理
    protected void Button13_Click(object sender, EventArgs e)
    {
        oll.UpOrderinfo("Suspended=1", Mid);
        Response.Redirect(Request.RawUrl);
    }
    //已经支付
    protected void Button2_Click(object sender, EventArgs e)
    {
        M_OrderList orderMod = oll.SelReturnModel(Mid);
        orderMod.Paymentstatus = (int)M_OrderList.PayEnum.HasPayed;
        oll.UpdateByID(orderMod);
        Response.Redirect(Request.RawUrl);
    }
    //取消确认
    protected void Button5_Click(object sender, EventArgs e)
    {
        string str = "OrderStatus=" + (int)M_OrderList.StatusEnum.Normal;
        oll.UpOrderinfo(str, Mid);
        function.WriteSuccessMsg("取消确认成功", "Orderlistinfo.aspx?id=" + Mid);
    }
    //开发票
    protected void Button11_Click(object sender, EventArgs e)
    {
        string str = "Developedvotes=1";
        oll.UpOrderinfo(str, Mid);
        Response.Redirect(Request.RawUrl);
    }
    //恢复正常
    protected void Button14_Click(object sender, EventArgs e)
    {
        string str = "Aside=0,Suspended=0,Settle=0,OrderStatus=" + (int)M_OrderList.StatusEnum.Normal;
        oll.UpOrderinfo(str, Mid);
        Response.Redirect(Request.RawUrl);
    }
    //发货(disuse)
    private void fahuo(int id, DataTable Unew, M_OrderList orlist)
    {
        for (int s = 0; s < Unew.Rows.Count; s++)
        {
            int Onum = DataConverter.CLng(Unew.Rows[s]["Pronum"]);
            int Opid = DataConverter.CLng(Unew.Rows[s]["ProID"]);

            M_Product pdin = proBll.GetproductByid(Opid);//获得商品信息

            if (pdin.JisuanFs == 0)
            {
                int pstock = pdin.Stock - Onum;//库存结果,返回的商品数量
                proBll.ProUpStock(Opid, pstock);
            }
            M_Stock SData = new M_Stock();
            SData.id = 0;
            SData.proid = Opid;
            SData.stocktype = 1;
            SData.proname = pdin.Proname;
            SData.danju = "CK" + orlist.OrderNo.ToString();
            SData.adduser = orlist.Reuser.ToString();
            SData.addtime = DateTime.Now;
            SData.content = "订单:" + orlist.Reuser.ToString() + "发货";
            SData.Pronum = DataConverter.CLng(Unew.Rows[0]["Pronum"]);
            Sll.AddStock(SData);
        }

        string str = "StateLogistics=1";
        if (!string.IsNullOrEmpty(txtMS.Text))
        {
            str += ",ExpressDelivery='" + txtMS.Text + "'";
        }
        oll.UpOrderinfo(str, id);
        PromotionComfirm(orlist);
    }
    // 如果是推广商品就添加推广信息
    public void PromotionComfirm(M_OrderList orlist)
    {
        B_ArticlePromotion bap = new B_ArticlePromotion();
        DataTable mcp;
        mcp = cartProBll.GetCartProOrderID(orlist.id);
        if (mcp != null && mcp.Rows.Count > 0)
        {
            if (orlist.Settle == 1)
            {
                for (int i = 0; i < mcp.Rows.Count; i++)
                {
                    Response.Write(mcp.Rows[i]["id"].ToString());
                    M_ArticlePromotion map = bap.GetSelectBySqlParams("select * from ZL_ArticlePromotion where cartproid=" + mcp.Rows[i]["id"].ToString(), null);
                    if (map.Id > 0)
                    {
                        map.IsEnable = true;
                        bap.GetUpdate(map);
                    }
                }
            }
        }
    }
    //订单作废
    protected void Button6_Click(object sender, EventArgs e)
    {
        string str = "Aside=1";
        oll.UpOrderinfo(str, Mid);
        function.WriteSuccessMsg("订单作废成功", "Orderlistinfo.aspx?id=" + Mid);
    }
    //结清订单
    protected void Button9_Click(object sender, EventArgs e)
    {
        string str = "Settle=1";
        oll.UpOrderinfo(str, Mid);
        PromotionComfirm(oll.GetOrderListByid(Mid));
        Response.Redirect(Request.RawUrl);
    }
    //删除订单
    protected void Button12_Click(object sender, EventArgs e)
    {
        //历遍清单所有商品数量，查找库存
        DataTable Unew = cartProBll.GetCartProOrderID(Mid); //获得详细清单列表
        for (int s = 0; s < Unew.Rows.Count; s++)
        {
            int Onum = DataConverter.CLng(Unew.Rows[s]["Pronum"]);
            int Opid = DataConverter.CLng(Unew.Rows[s]["ProID"]);
            M_Product pdin = proBll.GetproductByid(Opid);//获得商品信息
            if (pdin.JisuanFs == 1)
            {
                int pstock = pdin.Stock + Onum;//库存结果,返回的商品数量
                proBll.ProUpStock(Opid, pstock);
            }
        }
        oll.DeleteByID(Mid);
        function.WriteSuccessMsg("删除成功", "OrderList.aspx");
    }
    // 获取购买商品得到的积分
    public void cartinfo_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Repeater rpColumnNews = (Repeater)e.Item.FindControl("Repeater1");
            DataRowView rowv = (DataRowView)e.Item.DataItem;
            string proid = rowv["Bindpro"].ToString();
            if (!string.IsNullOrEmpty(proid))
            {
                DataTable ddinfos = proBll.Souchprolist(proid);
                rpColumnNews.DataSource = ddinfos.DefaultView;
                rpColumnNews.DataBind();
            }
        }
    }
    //修改合计金额
    protected void Button8_Click(object sender, EventArgs e)
    {
        int sid = DataConverter.CLng(Request.QueryString["id"]);
        M_OrderList orderinfo = oll.GetOrderListByid(sid);
        orderinfo.Ordersamount = DataConverter.CDouble(Label2.Text);
        oll.Update(orderinfo);
        Response.Redirect("Orderlistinfo.aspx?id=" + sid);
    }
    //拆单
    protected void BT_FreeSplit_Click(object sender, EventArgs e)
    {
        DataTable cplist = cartProBll.GetCartProOrderID(Mid);
        if (cplist.Rows.Count < 1)
        {
            Response.Write("<script type=text/javascript>alert('没有商品，不能再拆分了！');</script>");
            return;
        }
        else if (cplist.Rows.Count == 1)
        {
            Response.Write("<script type=text/javascript>alert('只有一件商品，不能再拆分了！');</script>");
            return;
        }
        else if (cplist.Rows.Count > 1)
        {
            string url = "OrderFreeSplit.aspx?id=" + Mid;
            Response.Write("<script> window.open('" + url + "', 'newWin', 'modal=yes,width=900,height=600,resizable=yes,scrollbars=yes'); </script>");
        }
    }
    protected void SureDelBtn_Click(object sender, EventArgs e)
    {
        oll.UpOrderinfo("OrderStatus=" + (int)M_OrderList.StatusEnum.Recycle, Mid);
        function.WriteSuccessMsg("删除订单入回收站", "OrderList.aspx");
    }
    public string GetShopUrl()
    {
        return orderCom.GetShopUrl(0, Convert.ToInt32(Eval("ProID")));
    }
    //确认退款
    protected void CheckReturn_Click(object sender, EventArgs e)
    {
        if (Back_T.Text.Length < 10) { function.WriteErrMsg("理由最少需十个字符"); }
        M_OrderList ordermod = oll.SelReturnModel(Mid);
        ordermod.OrderStatus = (int)M_OrderList.StatusEnum.CheckDrawBack;
        ordermod.Guojia = Back_T.Text;
        oll.UpdateByID(ordermod);
        Response.Redirect(Request.RawUrl);
    }
    //拒绝退款
    protected void UnCheckRetrun_Click(object sender, EventArgs e)
    {
        if (Back_T.Text.Length < 5) { function.WriteErrMsg("理由不能少于五个字符"); }
        M_OrderList ordermod = oll.SelReturnModel(Mid);
        ordermod.OrderStatus = (int)M_OrderList.StatusEnum.UnDrawBack;
        ordermod.Guojia = Back_T.Text;
        oll.UpdateByID(ordermod);
        Response.Redirect(Request.RawUrl);
    }
    protected void SendGoods_Btn_Click(object sender, EventArgs e)
    {
       
    }
    //确认订单
    protected void SureOrder_Btn_Click(object sender, EventArgs e)
    {
        oll.UpOrderinfo("OrderStatus=" + (int)M_OrderList.StatusEnum.Sured, Mid);
        PromotionComfirm(oll.GetOrderListByid(Mid));
        Response.Redirect(Request.RawUrl);
    }
    //完结订单
    protected void CompleteOrder_Btn_Click(object sender, EventArgs e)
    {
        M_OrderList orderMod = oll.SelReturnModel(Mid);
        orderMod.Paymentstatus = (int)M_OrderList.PayEnum.HasPayed;
        orderMod.StateLogistics = 2;
        orderMod.OrderStatus = (int)M_OrderList.StatusEnum.OrderFinish;
        Response.Redirect(Request.RawUrl);
    }
    //------------前台禁止
    //查看服务记录
}
