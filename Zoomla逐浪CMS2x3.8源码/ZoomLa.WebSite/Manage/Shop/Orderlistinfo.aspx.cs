using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Shop;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Model.Shop;

public partial class manage_Shop_Orderlistinfo : CustomerPageAction
{
    B_CartPro cartProBll = new B_CartPro();
    B_Model bmode = new B_Model();
    B_Product proBll = new B_Product();
    B_OrderList oll = new B_OrderList();
    B_PayPlat platBll = new B_PayPlat();
    B_Stock Sll = new B_Stock();
    B_InvtoType binvtype = new B_InvtoType();
    B_Order_Exp expBll = new B_Order_Exp();
    B_Order_PayLog paylogBll = new B_Order_PayLog();
    M_Order_PayLog paylogMod = new M_Order_PayLog();
    OrderCommon orderCom = new OrderCommon();
    B_User buser = new B_User();
    B_Admin badmin = new B_Admin();
    public M_OrderList orderinfo = null;
    //OrderID
    public int Mid { get { return DataConverter.CLng(Request.QueryString["ID"]); } }
    public string OrderNO { get { return ViewState["orderno"].ToString(); } set { ViewState["orderno"] = value; } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!B_ARoleAuth.Check(ZLEnum.Auth.shop, "OrderList")){function.WriteErrMsg("没有权限进行此项操作");}
        if (Mid < 1&&string.IsNullOrEmpty(OrderNO)) { function.WriteErrMsg("未指定订单"); }
        if (!IsPostBack)
        {
            if (Mid > 0) { orderinfo = oll.GetOrderListByid(Mid); }
            else if (!string.IsNullOrEmpty(Request["OrderNo"])) { orderinfo = oll.GetByOrder(Request["OrderNo"], "0"); }
            if (orderinfo == null || orderinfo.id < 1) { function.WriteErrMsg("订单不存在"); }
            OrderNO = orderinfo.OrderNo;
            //----------------------------------------------------------
            M_UserInfo mu=buser.SelReturnModel(orderinfo.Userid);
            HeadTitle_L.Text = "订 单 信 息（订单编号：" + orderinfo.OrderNo + "）";
            string giveurl = customPath2 + "User/Userexp.aspx?UserID=" + orderinfo.Userid
                              + "&orderid=" + orderinfo.id;
            give_score_a.HRef = giveurl + "&type=" + (int)M_UserExpHis.SType.Point;
            give_purse_a.HRef = giveurl + "&type=" + (int)M_UserExpHis.SType.Purse;
            OrderNo_L.Text = orderinfo.OrderNo;
            Orderamounts_L.Text = orderinfo.Ordersamount.ToString("f2");
            isCheckRe_L.Text = orderinfo.Guojia;
            Cdate_L.Text = orderinfo.AddTime.ToString("yyyy年MM月dd日 HH:mm");
            Reuser.Text = StringHelper.SubStr(orderinfo.Reuser,12);
            UName_L.Text = "<a href='javascript:;' onclick='showuinfo(" + mu.UserID + ");' title='查看用户'>" + mu.UserName + "</a>";
            if (orderinfo.StateLogistics != 0) { Exp_Send_Btn.Enabled = false; }
            if (orderinfo.StateLogistics == 1) { Exp_ClientSign_Btn.Enabled = true; }
            if (orderinfo.Paymentstatus >= (int)M_OrderList.PayEnum.HasPayed)
            {
                Paymentstatus.Text = "<span  style='color:green;>已经汇款</span>";
                Pay_Has_Btn.Enabled = false;
            }
            else
            {
                Paymentstatus.Text = "<span style='color:red;'>等待汇款</span>";
                Pay_Has_Btn.Enabled = true;
                Exp_Send_Btn.Enabled = false;
            }
            switch ((M_OrderList.StatusEnum)orderinfo.OrderStatus)
            {
                case M_OrderList.StatusEnum.Normal:
                    OS_Sure_Btn.Enabled = true;
                    break;
                case M_OrderList.StatusEnum.DrawBack:
                    OS_Sure_Btn.Enabled = false;
                    OS_NoSure_Btn.Enabled = false;
                    CheckReturn.Enabled = true;
                    UnCheckRetrun.Enabled = true;
                    Exp_Send_Btn.Enabled = false;
                    CompleteOrder_Btn.Enabled = false;
                    break;
                case M_OrderList.StatusEnum.UnDrawBack:
                case M_OrderList.StatusEnum.CheckDrawBack:
                    OS_Invoice_Btn.Enabled = false;
                    OS_Sure_Btn.Enabled = false;
                    OS_NoSure_Btn.Enabled = false;
                    Exp_Send_Btn.Enabled = false;
                    CompleteOrder_Btn.Enabled = false;
                    break;
                case M_OrderList.StatusEnum.OrderFinish:
                case M_OrderList.StatusEnum.UnitFinish:
                    CompleteOrder_Btn.Enabled = false;
                    break;
                default:
                    break;
            }
            #region 物流信息
            ExpStatus_L.Text = OrderHelper.GetExpStatus(orderinfo.StateLogistics);
            M_Order_Exp expMod = expBll.SelReturnModel(DataConverter.CLng(orderinfo.ExpressNum));
            if (expMod != null)
            {
                ExpName_L.Text = expMod.ExpComp;
                ExpCode_L.Text = expMod.ExpNo;
                ExpStatus_L.Text += "(公司：" + expMod.ExpComp + "/单号：" + expMod.ExpNo + ")";
            }
            switch ((M_OrderList.ExpEnum)orderinfo.StateLogistics)
            {
                case M_OrderList.ExpEnum.NoSend:
                    Exp_Cancel_Btn.Enabled = false;
                    break;
                case M_OrderList.ExpEnum.HasSend:
                    Exp_ClientSign_Btn.Enabled = true;
                    break;
                case M_OrderList.ExpEnum.HasReceived:
                    Exp_Send_Btn.Enabled = false;
                    break;
            }
            DrawBackStr.Text = orderinfo.Merchandiser;
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
            Reusers.Text = orderinfo.Reuser.ToString();//订货人
            Jiedao.Text = orderinfo.Shengfen + " " + orderinfo.Jiedao;//地址
            #endregion
            OrderStatus.Text = OrderHelper.GetOrderStatus(orderinfo.OrderStatus);
            adddate.Text = orderinfo.AddTime.ToShortDateString();
            OrderType_L.Text = orderinfo.AddTime.ToString();

            Invoiceneeds.Text = orderinfo.Invoiceneeds == 1 ? ComRE.Icon_OK : ComRE.Icon_Error;
            Developedvotes.Text = orderinfo.Developedvotes == 1 ? ComRE.Icon_OK : ComRE.Icon_Error;
            //如果已支付
            if (orderinfo.Integral > 0 && orderinfo.Paymentstatus >= (int)M_OrderList.PayEnum.HasPayed)
            {
                if (orderinfo.SendPointStatus == 1)
                {
                    ISsend.Text = "已送出";
                }
            }
            Pay_Has_Btn.Enabled = orderinfo.Paymentstatus == 1 ? false : true;//更改状态,已支付
            OS_Freeze_Btn.Enabled = orderinfo.Settle == 1 ? false : true;//冻结
            OS_Pause_Btn.Enabled = orderinfo.Suspended == 1 ? false : true;//暂停
            if (orderinfo.Aside == 1)//已作废
            {
                Pay_Has_Btn.Enabled = false;
                //ShowSend_Btn.Enabled = false;
                OS_Sure_Btn.Enabled = false;
                OS_NoSure_Btn.Enabled = false;
                Exp_ClientSign_Btn.Enabled = false;
                OS_Freeze_Btn.Enabled = false;
                OS_Invoice_Btn.Enabled = false;
                OS_Pause_Btn.Enabled = false;
                OS_Aside_Btn.Enabled = false;
            }
            else
            {
                OS_Aside_Btn.Enabled = true;
            }
            if (orderinfo.Settle == 1)//结清
            {
                Pay_Has_Btn.Enabled = false;
                //ShowSend_Btn.Enabled = false;
                OS_Sure_Btn.Enabled = false;
                OS_NoSure_Btn.Enabled = false;
                OS_Aside_Btn.Enabled = false;
                Exp_ClientSign_Btn.Enabled = false;
                OS_Invoice_Btn.Enabled = false;
                OS_Pause_Btn.Enabled = false;
            }
            if (orderinfo.Payment > 0)//支付后才有值
            {
                M_PayPlat payPlatMod = platBll.GetPayPlatByid(orderinfo.Payment);
                Payment.Text = payPlatMod.PayPlatName.ToString();
                B_Payment paymentBLL = new B_Payment();
            }
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
            AddUser.Text = orderinfo.AddUser.ToString();//负责跟单人员
            Internalrecords_T.Text = orderinfo.Internalrecords.ToString();//内部记录
            Ordermessage_T.Text = orderinfo.Ordermessage;//订货留言
            LabScore.Text = orderinfo.Integral.ToString();//积分
            //-------购物车
            DataTable cplist = cartProBll.GetCartProOrderID(Mid);
            Procart_RPT.DataSource = cplist;
            Procart_RPT.DataBind();
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
            Label31.Text = (orderinfo.Ordersamount).ToString("f2") + "元";
            ReceMoney_L.Text = orderinfo.Receivablesamount.ToString("f2") + "元";
            //判断订单所处状态
            {
                if (orderinfo.OrderStatus < (int)M_OrderList.StatusEnum.Normal || orderinfo.Aside == 1)
                {
                    prog_order_div.InnerHtml = OrderHelper.GetOrderStatus(orderinfo.OrderStatus, orderinfo.Aside, orderinfo.StateLogistics);
                }
                else
                {
                    int current = 2;
                    if (orderinfo.OrderStatus >= (int)M_OrderList.StatusEnum.OrderFinish) { current = 5; }
                    else if (orderinfo.Paymentstatus >= (int)M_OrderList.PayEnum.HasPayed)
                    {
                        current++;
                        switch (orderinfo.StateLogistics)
                        {
                            case (int)M_OrderList.ExpEnum.HasSend:
                                current++;
                                break;
                            case (int)M_OrderList.ExpEnum.HasReceived:
                                current += 2;
                                break;
                        }
                    }
                    function.Script(this, "$('#prog_order_div').ZLSteps('订单生成,等待用户支付,等待商户发货,等待用户签收,订单完结'," + current + ")");
                }
            }
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='ProductManage.aspx'>商城管理</a></li><li><a href='OrderList.aspx'>订单管理</a></li><li class='active'><a href='" + Request.RawUrl + "'>订单详情</a></li>");
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
    //获取价格
    public string GetPrice() 
    {
        return OrderHelper.GetPriceStr(Convert.ToDouble(Eval("AllMoney")), Eval("AllMoney_Json").ToString());
    }
    //获取期限
    public string GetServerTime(string cid)
    {
        //return proBll.GetServerPeriod(DataConverter.CLng(Eval("ServerPeriod")), DataConverter.CLng(Eval("ServerType")));
        return "";
    }
    public string GetLinPrice() 
    {
        return OrderHelper.GetPriceStr(Convert.ToDouble(Eval("LinPrice")), Eval("LinPrice_Json"));
    }
    protected string Getprotype()
    {
        if (DataConverter.CLng(Eval("Priority")) == 1 && DataConverter.CLng(Eval("Propeid")) > 0)
        {
            return "<span style='color:green;'>[绑]</span>";
        }
        return "";
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
        function.WriteSuccessMsg("删除成功","OrderList.aspx");
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
        ordermod.OrderStatus =(int)M_OrderList.StatusEnum.CheckDrawBack;
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
        if (txtMSnoAlipay.Text == "")
        {
            Response.Write("<script type=text/javascript>alert('快递单号必填！');</script>");
            return;
        }
        oll.UpdateExpressNum(txtMSnoAlipay.Text, Mid);
        if (DR_Company.SelectedItem.Value == "0")
        {
            Response.Write("<script type=text/javascript>alert('你还没选择物流公司！');</script>");
            return;
        }
        else if (DR_Company.SelectedItem.Value == "77" && kdgs.Value == "")
        {
            Response.Write("<script type=text/javascript>alert('你还没填写物流公司！');</script>");
            return;
        }
        //历遍清单所有商品数量，查找库存///////////////////
        //DataTable Unew = cartProBll.GetCartProOrderID(Mid); //获得详细清单列表
        //M_OrderList orlist = oll.GetOrderListByid(Mid);
        //fahuo(Mid, Unew, orlist);
        Response.Redirect(Request.RawUrl);
    }
    //------------前台禁止
    //完结订单
    protected void CompleteOrder_Btn_Click(object sender, EventArgs e)
    {
        //前使用必须修改,只更改状态,不执行FinalStep
        M_OrderList orderMod = oll.SelReturnModel(Mid);
        OrderHelper.FinalStep(orderMod);
        Response.Redirect(Request.RawUrl);
    }
    //查看服务记录
    protected void Button10_Click(object sender, EventArgs e)
    {
        Response.Redirect("../iServer/BiServer.aspx?orderid=" + Mid);

    }
    //点击赠送积分
    protected void sendScore_Click(object sender, EventArgs e)
    {
        //B_User buser = new B_User();
        //int id = DataConverter.CLng(Request.QueryString["id"]);
        //int score = DataConverter.CLng(LabScore.Text);
        //M_OrderList ordinfo = oll.GetOrderListByid(id);
        //M_UserInfo muser = buser.GetUserByName(badmin.GetAdminLogin().AdminName);
        //buser.ChangeVirtualMoney(ordinfo.Userid, new M_UserExpHis()
        //{
        //    score = score,
        //    detail = "管理员操作商城订单" + ordinfo.OrderNo + "赠送积分+" + score + "分 ",
        //    ScoreType = (int)M_UserExpHis.SType.Point
        //});
        //string str = "SendPointStatus=1";
        //oll.UpOrderinfo(str, id);
        //function.WriteSuccessMsg("成功添加用户" + score + "个积分");
        // Response.Redirect("UserOrderinfo.aspx?id=" + id);
    }
    protected void btnPre_Click(object sender, EventArgs e)
    {
        showData("pre");
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        showData("next");
    }
    private void showData(string str)
    {
        M_OrderList orderMod = oll.SelNext(Mid, str);
        if (str == "next")
        {
            if (orderMod == null)
            {
                btnNext.Text = "已是最后一个订单";
                btnNext.Enabled = false;
                btnNext2.Enabled = false;
            }
            else { Response.Redirect("Orderlistinfo.aspx?id=" + orderMod.id); }
        }
        else if (str == "pre")
        {
            if (orderMod == null)
            {
                btnPre.Text = "已是第一个订单";
                btnPre.Enabled = false;
                btnPre2.Enabled = false;
            }
            else { Response.Redirect("Orderlistinfo.aspx?id=" + orderMod.id); }
        }
    }
    //开发票
    protected void OS_Invoice_Btn_Click(object sender, EventArgs e)
    {
        string str = "Developedvotes=1";
        oll.UpOrderinfo(str, Mid);
        Response.Redirect(Request.RawUrl);
    }
    //恢复正常
    protected void OS_Normal_Btn_Click(object sender, EventArgs e)
    {
        string str = "Aside=0,Suspended=0,Settle=0,OrderStatus=" + (int)M_OrderList.StatusEnum.Normal;
        oll.UpOrderinfo(str, Mid);
        Response.Redirect(Request.RawUrl);
    }
    protected void SaveRemind_Btn_Click(object sender, EventArgs e)
    {
        orderinfo = oll.SelReturnModel(Mid);
        orderinfo.Internalrecords = Internalrecords_T.Text;
        orderinfo.Ordermessage = Ordermessage_T.Text;
        oll.UpdateByID(orderinfo);
        function.WriteSuccessMsg("修改成功");
    }
    #region 支付状态
    protected void Pay_Cancel_Btn_Click(object sender, EventArgs e)
    {
        oll.UpOrderinfo("Paymentstatus=" + (int)M_OrderList.PayEnum.NoPay, Mid);
        Response.Redirect(Request.RawUrl);
    }
    //已经支付
    protected void Pay_Has_Btn_Click(object sender, EventArgs e)
    {
        oll.UpOrderinfo("Paymentstatus=" + (int)M_OrderList.PayEnum.HasPayed, Mid);
        Response.Redirect(Request.RawUrl);
    }
    #endregion
    #region 订单状态 OS_
    //确认订单
    protected void OS_Sure_Btn_Click(object sender, EventArgs e)
    {
        oll.UpOrderinfo("OrderStatus=" + (int)M_OrderList.StatusEnum.Sured, Mid);
        PromotionComfirm(oll.GetOrderListByid(Mid));
        Response.Redirect(Request.RawUrl);
    }
    //取消确认
    protected void OS_NoSure_Btn_Click(object sender, EventArgs e)
    {
        string str = "OrderStatus=" + (int)M_OrderList.StatusEnum.Normal;
        oll.UpOrderinfo(str, Mid);
        function.WriteSuccessMsg("取消确认成功", "Orderlistinfo.aspx?id=" + Mid);
    }
    //暂停处理
    protected void OS_Pause_Btn_Click(object sender, EventArgs e)
    {
        oll.UpOrderinfo("Suspended=1", Mid);
        Response.Redirect(Request.RawUrl);
    }
    //订单作废
    protected void OS_Aside_Btn_Click(object sender, EventArgs e)
    {
        string str = "Aside=1";
        oll.UpOrderinfo(str, Mid);
        function.WriteSuccessMsg("订单作废成功", "Orderlistinfo.aspx?id=" + Mid);
    }
    //冻结订单
    protected void OS_Freeze_Btn_Click(object sender, EventArgs e)
    {
        string str = "Settle=1";
        oll.UpOrderinfo(str, Mid);
        PromotionComfirm(oll.GetOrderListByid(Mid));
        Response.Redirect(Request.RawUrl);
    }
    #endregion
    #region 物流管理 Exp_
    //取消发送
    protected void EXP_Cancel_Btn_Click(object sender, EventArgs e)
    {
        oll.UpOrderinfo("StateLogistics=" + (int)M_OrderList.ExpEnum.NoSend + ",ExpressNum=''", Mid);
        Response.Redirect(Request.RawUrl);
    }
    //客户已签收
    protected void Exp_ClientSign_Btn_Click(object sender, EventArgs e)
    {
        oll.UpOrderinfo("Signed=1,StateLogistics=" + (int)M_OrderList.ExpEnum.HasReceived, Mid);
        Response.Redirect(Request.RawUrl);
    }
    #endregion
}