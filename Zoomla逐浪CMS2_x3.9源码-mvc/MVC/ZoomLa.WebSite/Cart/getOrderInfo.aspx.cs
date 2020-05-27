using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Shop;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;
using ZoomLa.Model.Shop;
using ZoomLa.SQLDAL;


namespace ZoomLaCMS.Cart
{
    public partial class getOrderInfo : System.Web.UI.Page
    {
        B_Arrive avBll = new B_Arrive();
        B_Cart cartBll = new B_Cart();
        B_CartPro cartProBll = new B_CartPro();
        B_User buser = new B_User();
        B_UserRecei receBll = new B_UserRecei();
        B_OrderList orderBll = new B_OrderList();
        B_OrderBaseField fieldBll = new B_OrderBaseField();
        B_Shop_FareTlp fareBll = new B_Shop_FareTlp();
        B_Product proBll = new B_Product();
        B_Payment payBll = new B_Payment();
        B_Shop_RegionPrice regionBll = new B_Shop_RegionPrice();
        OrderCommon orderCom = new OrderCommon();
        private double allmoney = 0;//购物车中商品金额统计
        public int ProClass { get { return DataConvert.CLng(Request.QueryString["Proclass"]); } }
        public string ids { get { return Request.QueryString["ids"]; } }
        //用于区域价格
        private string Region { get { return ViewState["Region"] as string; } set { ViewState["Region"] = value; } }
        private DataTable CartDT
        {
            get
            {
                return (DataTable)ViewState["CartDT"];
            }
            set { ViewState["CartDT"] = value; }
        }
        /*----------------------------------------------------------------------------------------------------*/
        protected void Page_Load(object sender, EventArgs e)
        {
            B_User.CheckIsLogged(Request.RawUrl);
            if (!IsPostBack)
            {
                if (string.IsNullOrEmpty(ids))
                { function.WriteErrMsg("请先选定需要购买的商品"); }
                switch (ProClass)
                {
                    case (int)M_Product.ClassType.YG://不需要地址
                        Address_Div.Visible = false;
                        break;
                    case (int)M_Product.ClassType.LY:
                    case (int)M_Product.ClassType.JD:
                        Address_Div.Visible = false;
                        ReUrl_A2.Visible = true;
                        ReUrl_A2.HRef = "/Prompt/ShopCart/Passengers.aspx?IDS=" + ids;
                        break;
                    default:
                        ReUrl_A1.HRef = "/Cart/Cart.aspx?Proclass=" + ProClass;
                        ReUrl_A1.Visible = true;
                        break;
                }
                //purseli.Visible = SiteConfig.SiteOption.SiteID.Contains("purse");
                //siconli.Visible = SiteConfig.SiteOption.SiteID.Contains("sicon");
                //pointli.Visible = SiteConfig.SiteOption.SiteID.Contains("point");
                MyBind();
            }
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            CartDT = null;
        }
        public void MyBind()
        {
            M_UserInfo mu = buser.GetLogin();
            CartDT = cartBll.SelByCartID(B_Cart.GetCartID(), mu.UserID, ProClass, ids);
            if (CartDT.Rows.Count < 1)
            {
                function.WriteErrMsg("你尚未选择商品,<a href='/User/Order/OrderList.aspx'>查看我的订单</a>");
            }
            //旅游,酒店等不需要检测地址栏
            switch (DataConvert.CLng(CartDT.Rows[0]["ProClass"]))
            {
                case (int)M_Product.ClassType.LY:
                    {
                        userli.Visible = true;
                        M_Cart_Travel model = JsonConvert.DeserializeObject<M_Cart_Travel>(CartDT.Rows[0]["Additional"].ToString());
                        model.Guest.AddRange(model.Contract);
                        UserRPT.DataSource = model.Guest;
                        UserRPT.DataBind();
                    }
                    break;
                case (int)M_Product.ClassType.JD:
                    {
                        userli.Visible = true;
                        M_Cart_Hotel model = JsonConvert.DeserializeObject<M_Cart_Hotel>(CartDT.Rows[0]["Additional"].ToString());
                        model.Guest.AddRange(model.Contract);
                        UserRPT.DataSource = model.Guest;
                        UserRPT.DataBind();
                    }
                    break;
                default: //------地址
                    DataTable addressDT = receBll.SelByUID(buser.GetLogin().UserID);
                    AddressRPT.DataSource = addressDT;
                    AddressRPT.DataBind();
                    EmptyDiv.Visible = addressDT.Rows.Count < 1;//地址为空提醒
                    break;
            }
            //------核算费用
            allmoney = UpdateCartAllMoney(CartDT);
            //------费用统计
            itemnum_span.InnerText = CartDT.Rows.Count.ToString();
            totalmoney_span1.InnerText = allmoney.ToString("f2");
            //------店铺
            Store_RPT.DataSource = orderCom.SelStoreDT(CartDT);
            Store_RPT.DataBind();
            //------发票绑定
            DataTable invoceDT = new DataTable();//orderBll.SelInvoByUser(buser.GetLogin().UserID);
            if (invoceDT.Rows.Count > 0)
            {
                Invoice_RPT.DataSource = invoceDT;
                Invoice_RPT.DataBind();
            }
            else { Invoice_RPT.Visible = false; }
            //------积分抵扣
            if (SiteConfig.ShopConfig.PointRatiot > 0 && SiteConfig.ShopConfig.PointRatiot < 100 && SiteConfig.ShopConfig.PointRate > 0)
            {
                point_body.Visible = true;
                M_UserInfo usermod = buser.GetLogin();
                Point_L.Text = usermod.UserExp.ToString();
                int usepoint = (int)(allmoney * (SiteConfig.ShopConfig.PointRatiot * 0.01) / SiteConfig.ShopConfig.PointRate);
                function.Script(this, "SumByPoint(" + usepoint + ");");
                PointRate_Hid.Value = SiteConfig.ShopConfig.PointRate.ToString();
            }
            else
            {
                point_tips.Visible = true;
            }
            //------用户有哪些优惠券
            DataTable avdt = avBll.U_Sel(mu.UserID, -100, 1);
            if (avdt.Rows.Count > 0)
            {
                arrive_div.Style.Add("display", "block");
                arrive_data_div.Visible = true;
                avdt.Columns.Add("enable", typeof(int));
                avdt.Columns.Add("err", typeof(string));
                for (int i = 0; i < avdt.Rows.Count; i++)
                {
                    DataRow dr = avdt.Rows[i];
                    double money = allmoney;
                    string err = "";
                    dr["enable"] = avBll.U_CheckArrive(new M_Arrive().GetModelFromReader(dr), mu.UserID, ref money, ref err) ? 1 : 0;
                    dr["err"] = err;
                }
                avdt.DefaultView.RowFilter = "enable='1'";
                //function.WriteErrMsg(avdt.DefaultView.ToTable().Rows.Count.ToString());
                Arrive_Active_RPT.DataSource = avdt.DefaultView.ToTable();
                Arrive_Active_RPT.DataBind();
                avdt.DefaultView.RowFilter = "enable='0'";
                Arrive_Disable_RPT.DataSource = avdt.DefaultView.ToTable();
                Arrive_Disable_RPT.DataBind();
            }
            else { arrive_empty_div.Visible = true; }
        }
        protected void Store_RPT_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView drv = e.Item.DataItem as DataRowView;
                //按店铺展示商品列表
                Repeater rpt = e.Item.FindControl("ProRPT") as Repeater;
                CartDT.DefaultView.RowFilter = "StoreID=" + drv["ID"];
                DataTable dt = CartDT.DefaultView.ToTable();
                if (dt.Rows.Count < 1) { e.Item.Visible = false; }
                else
                {
                    rpt.DataSource = dt;
                    rpt.DataBind();
                    //运费计算";
                    Literal html_lit = e.Item.FindControl("FareType_L") as Literal;
                    DataTable fareDT = GetFareDT(dt);
                    html_lit.Text = CreateFareHtml(fareDT);
                }
            }
        }
        //提交订单
        protected void AddOrder_Btn_Click(object sender, EventArgs e)
        {
            if (Address_Div.Visible && DataConvert.CLng(Request.Form["address_rad"]) < 1) function.WriteErrMsg("尚未选定收货地址");
            //1,生成订单,2,关联购物车中商品为已绑定订单
            M_UserInfo mu = buser.GetLogin(false);
            M_UserRecei receMod = receBll.GetSelect(Convert.ToInt32(Request.Form["address_rad"]), mu.UserID);
            DataTable cartDT = cartBll.SelByCartID(B_Cart.GetCartID(), mu.UserID, ProClass, ids);//需要购买的商品
            if (cartDT.Rows.Count < 1) function.WriteErrMsg("你尚未选择商品,<a href='/User/Order/OrderList.aspx'>查看我的订单</a>");
            //------生成订单前检测区
            foreach (DataRow dr in cartDT.Rows)
            {
                if (!HasStock(dr["Allowed"], DataConvert.CLng(dr["stock"]), Convert.ToInt32(dr["Pronum"])))
                    function.WriteErrMsg("购买失败," + dr["proname"] + "的库存数量不足");
            }
            //------检测End
            //按店铺生成订单,统一存ZL_Orderinfo
            DataTable storeDT = cartDT.DefaultView.ToTable(true, "StoreID");
            List<M_OrderList> orderList = new List<M_OrderList>();//用于生成临时订单,统计计算(Disuse)
            foreach (DataRow dr in storeDT.Rows)
            {
                #region 暂不使用字段
                //Odata.province = this.DropDownList1.SelectedValue;
                //Odata.city = this.DropDownList2.SelectedValue;//将地址省份与市ID存入,XML数据源
                //Odata.Guojia = "";//国家
                //Odata.Chengshi = DropDownList2.SelectedItem.Text;//城市
                //Odata.Diqu = DropDownList3.SelectedItem.Text;//地区
                //Odata.Delivery = DataConverter.CLng(Request.Form["Delivery"]);
                //Odata.Deliverytime = DataConverter.CLng(this.Deliverytime.Text);
                //Odata.Mobile = receMod.MobileNum;
                #endregion
                M_OrderList Odata = new M_OrderList();
                Odata.Ordertype = OrderHelper.GetOrderType(ProClass);
                Odata.OrderNo = B_OrderList.CreateOrderNo((M_OrderList.OrderEnum)Odata.Ordertype);
                Odata.StoreID = Convert.ToInt32(dr["StoreID"]);
                cartDT.DefaultView.RowFilter = "StoreID=" + Odata.StoreID;
                DataTable storeCartDT = cartDT.DefaultView.ToTable();
                switch (ProClass)//旅游机票等,以联系人信息为地址
                {
                    case 7:
                    case 8:
                        M_Cart_Travel model = JsonConvert.DeserializeObject<M_Cart_Travel>(storeCartDT.Rows[0]["Additional"].ToString());
                        M_Cart_Contract user = model.Contract[0];
                        Odata.Receiver = user.Name;
                        Odata.Reuser = user.Name;
                        Odata.Phone = user.Mobile;
                        Odata.MobileNum = user.Mobile;
                        Odata.Email = user.Email;
                        break;
                    default:
                        if (Address_Div.Visible)
                        {
                            Odata.Receiver = receMod.ReceivName;
                            Odata.Reuser = receMod.ReceivName;
                            Odata.Phone = receMod.phone;
                            Odata.MobileNum = receMod.MobileNum;
                            Odata.Email = receMod.Email;
                            Odata.Shengfen = receMod.Provinces;
                            Odata.Jiedao = receMod.Street;
                            Odata.ZipCode = receMod.Zipcode;
                        }
                        break;
                }
                Odata.Invoiceneeds = DataConverter.CLng(Request.Form["invoice_rad"]);//是否需开发票
                Odata.Invoice = Odata.Invoiceneeds == 0 ? "" : InvoTitle_T.Text + "||" + Invoice_T.Text;
                Odata.Rename = mu.UserName;
                Odata.Outstock = 0;//缺货处理
                Odata.Ordermessage = ORemind_T.Text;//订货留言
                Odata.Merchandiser = "";//跟单员
                Odata.Internalrecords = ""; //内部记录
                Odata.IsCount = false;
                //-----金额计算
                Odata.Balance_price = GetTotalMoney(storeCartDT);
                Odata.Freight = GetFarePrice(storeCartDT, Odata.StoreID);//运费计算
                Odata.Ordersamount = Odata.Balance_price + Odata.Freight;//订单金额
                Odata.AllMoney_Json = orderCom.GetTotalJson(storeCartDT);//附加需要的虚拟币
                Odata.Specifiedprice = Odata.Ordersamount; //订单金额;
                Odata.Receivablesamount = 0;//收款金额
                Odata.Developedvotes = 0;
                Odata.OrderStatus = (int)M_OrderList.StatusEnum.Normal;//订单状态
                Odata.Paymentstatus = (int)M_OrderList.PayEnum.NoPay;//付款状态
                Odata.StateLogistics = 0;//物流状态
                Odata.Signed = 0;//签收
                Odata.Settle = 0;//结清
                Odata.Aside = 0;//作废
                Odata.Suspended = 0;//暂停
                Odata.AddUser = mu.UserName; ;
                Odata.Userid = mu.UserID;
                Odata.Integral = DataConverter.CLng(Request.QueryString["jifen"]);
                Odata.Freight_remark = " ";
                Odata.Balance_remark = "";
                Odata.Promoter = 0;
                Odata.id = orderBll.Adds(Odata);
                cartProBll.CopyToCartPro(mu, storeCartDT, Odata.id);
                orderList.Add(Odata);
                orderCom.SendMessage(Odata, null, "ordered");
            }
            cartBll.DelByids(ids);
            //-----------------订单生成后处理
            //进行减库存等操作
            foreach (DataRow dr in cartDT.Rows)
            {
                M_Product model = proBll.GetproductByid(Convert.ToInt32(dr["Proid"]));
                model.Stock = model.Stock - DataConvert.CLng(dr["Pronum"]);
                SqlHelper.ExecuteSql("Update ZL_Commodities Set Stock=" + model.Stock + " Where ID=" + model.ID);
            }
            //生成支付单,处理优惠券,并进入付款步骤
            M_Payment payMod = payBll.CreateByOrder(orderList);
            //优惠券
            if (!string.IsNullOrEmpty(Arrive_Hid.Value))
            {
                M_Arrive avMod = avBll.SelModelByFlow(Arrive_Hid.Value, mu.UserID);
                double money = payMod.MoneyPay;
                string err = "";
                string remind = "支付单抵扣[" + payMod.PayNo + "]";
                if (avBll.U_UseArrive(avMod, mu.UserID, ref money, ref err, remind))
                {
                    payMod.MoneyPay = money;
                    payMod.ArriveMoney = avMod.Amount;
                    payMod.ArriveDetail = avMod.ID.ToString();
                }
                else { payMod.ArriveDetail = "优惠券[" + avMod.ID + "]异常 :" + err; }
            }
            //积分处理
            if (point_body.Visible && DataConvert.CLng(Point_T.Text) > 0)
            {
                int point = DataConvert.CLng(Point_T.Text);
                int maxPoint = (int)((SiteConfig.ShopConfig.PointRatiot * 0.01) * (double)payMod.MoneyPay / SiteConfig.ShopConfig.PointRate);
                //if (point <= 0) { function.WriteErrMsg("积分数值不正确"); }
                if (point > mu.UserExp) { function.WriteErrMsg("您的积分不足!"); }
                if (point > maxPoint) { function.WriteErrMsg("积分不能大于可兑换金额!"); }
                //生成支付单时扣除用户积分
                buser.ChangeVirtualMoney(mu.UserID, new M_UserExpHis() { ScoreType = (int)M_UserExpHis.SType.Point, score = -point, detail = "积分抵扣,支付单号:" + payMod.PayNo });
                payMod.MoneyPay = payMod.MoneyPay - (point * SiteConfig.ShopConfig.PointRate);
                payMod.UsePoint = point;
            }
            if (payMod.MoneyPay <= 0) { payMod.MoneyPay = 0.01; }
            payMod.Remark = cartDT.Rows.Count > 1 ? "[" + cartDT.Rows[0]["ProName"] as string + "]等" : cartDT.Rows[0]["ProName"] as string;
            payMod.PaymentID = payBll.Add(payMod);
            Response.Redirect("/PayOnline/Orderpay.aspx?PayNo=" + payMod.PayNo);
        }
        //-----------------------------------------------------------------
        public string GetAddress()
        {
            string tlp = "{0} {1}({2} 收) {3} ";
            return string.Format(tlp, Eval("Provinces"), Eval("Street"), Eval("ReceivName"), Eval("MobileNum"));
        }
        public string GetIsDef()
        {
            if (Eval("isDefault").ToString().Equals("1"))
            {
                return "<span class='grayremind'>默认地址</span>";
            }
            else
                return "";
            //else
            //{
            //    return "<a href='javascript:;' onclick='SetDefault();'>设为默认地址</a>";
            //}
        }
        //用于酒店订单等
        public string GetAddition()
        {
            string additional = Eval("Additional").ToString(), result = "", contract = "";
            if (string.IsNullOrEmpty(additional)) return "";
            switch (ProClass)
            {
                case 7://旅游,酒店,机票
                    {
                        string tlp = "入住时间:{0}<br/>联系人:{1}";
                        M_Cart_Travel model = JsonConvert.DeserializeObject<M_Cart_Travel>(additional);
                        foreach (M_Cart_Contract m in model.Contract)
                        {
                            contract += m.Name + "," + m.Mobile + "|";
                        }
                        contract = contract.TrimEnd('|');
                        string another = string.IsNullOrEmpty(model.ProList[0].Remind) ? "" : DataConvert.CDate(model.ProList[0].Remind).ToString("MM-dd HH:mm");
                        result = string.Format(tlp, model.ProList[0].GoDate.ToString("MM-dd HH:mm --") + another, contract);
                    }
                    break;
                case 8:
                    {
                        string tlp = "订单信息:{0},{1}人,时间:{2}--{3}<br/>入住人:{4}<br/>联系人:{5}";
                        M_Cart_Hotel model = JsonConvert.DeserializeObject<M_Cart_Hotel>(additional);
                        foreach (M_Cart_Contract m in model.Contract)
                        {
                            contract += m.Name + "," + m.Mobile + "|";
                        }
                        contract = contract.TrimEnd('|');
                        result = string.Format(tlp, model.HotelName, model.PeopleNum, model.ProList[0].GoDate, model.ProList[0].OutDate, model.Guest[0].Name, contract);
                    }
                    break;
                case 9:
                    break;
                default:
                    break;
            }
            return result;
        }
        //附加货币计算
        public string GetAllMoney_Json()
        {
            string json = DataConvert.CStr(Eval("AllMoney_Json"));
            string html = "";
            if (orderCom.HasPrice(json))
            {
                M_LinPrice priceMod = JsonConvert.DeserializeObject<M_LinPrice>(json);
                if (priceMod.Purse > 0)
                {
                    html += "<br />余额:<span class='purse_sp'>" + priceMod.Purse.ToString("f2") + "</span>";
                }
                if (priceMod.Sicon > 0)
                {
                    html += "|银币:<span class='sicon_sp'>" + priceMod.Sicon.ToString("f0") + "</span>";
                }
                if (priceMod.Point > 0)
                {
                    html += "|积分:<span class='point_sp'>" + priceMod.Point.ToString("f0") + "</span>";
                }
            }
            return html;
        }
        #region 重算商品金额
        //更新购物车中的AllMoney(实际购买总价),便于后期查看详情
        private double UpdateCartAllMoney(DataTable dt)
        {
            M_UserInfo mu = buser.GetLogin();
            double allmoney = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                M_Cart cartMod = cartBll.GetCartByid(Convert.ToInt32(dr["ID"]));
                M_Product proMod = proBll.GetproductByid(Convert.ToInt32(dr["Proid"]));
                //--附加币值计价
                //if (orderCom.HasPrice(proMod.LinPrice_Json))
                //{
                //    M_LinPrice priceMod = JsonConvert.DeserializeObject<M_LinPrice>(proMod.LinPrice_Json);
                //    priceMod.Purse = priceMod.Purse * cartMod.Pronum;
                //    priceMod.Sicon = priceMod.Sicon * cartMod.Pronum;
                //    priceMod.Point = priceMod.Point * cartMod.Pronum;
                //    dr["AllMoney_Json"] = JsonConvert.SerializeObject(priceMod);
                //    cartMod.AllMoney_Json = DataConvert.CStr(dr["AllMoney_Json"]);
                //}
                //根据商品价格类型,看使用  零售|批发|会员|会员组价格
                //double price = proBll.P_GetByUserType(proMod, mu);
                //多区域价格
                if (string.IsNullOrEmpty(Region))
                {
                    Region = buser.GetRegion(mu.UserID);
                }
                double price = regionBll.GetRegionPrice(proMod.ID, proMod.LinPrice, Region, mu.GroupID);
                //--多价格编号,则使用多价格编号的价钱,ProName(已在购物车页面更新)
                //proBll.GetPriceByCode(dr["code"], proMod.Wholesalesinfo, ref price);
                cartMod.AllMoney = price * cartMod.Pronum;
                cartMod.AllIntegral = cartMod.AllMoney;
                //----计算折扣
                dr["AllMoney"] = cartMod.AllMoney;
                dr["AllIntegral"] = cartMod.AllIntegral;
                if (proMod.Recommend > 0)
                {
                    dr["AllMoney"] = (cartMod.AllIntegral - (cartMod.AllIntegral * ((double)proMod.Recommend / 100)));
                    cartMod.AllMoney = Convert.ToDouble(dr["AllMoney"]);
                }
                cartBll.UpdateByID(cartMod);
                allmoney += cartMod.AllMoney;
            }
            return allmoney;
        }
        //获取总金额
        private double GetTotalMoney(DataTable dt)
        {
            //不需要再重新计算,因为每次进入页面都会重算
            return Convert.ToDouble(dt.Compute("SUM(AllMoney)", ""));
        }
        //获取前端所选定的ID,返回运费价
        private double GetFarePrice(DataTable storecartDT, int storeid)
        {
            DataTable faredt = GetFareDT(storecartDT);
            string selectedVal = Request.Form["fare_" + storeid];//前台选定的快递方式,后台重新计价
            faredt.DefaultView.RowFilter = "ID=" + selectedVal;
            if (faredt.DefaultView.ToTable().Rows.Count < 1) function.WriteErrMsg("运费出错");
            DataRow dr = faredt.DefaultView.ToTable().Rows[0];
            return Convert.ToDouble(dr["Price"]) + DataConvert.CDouble(dr["Plus"]);
        }
        /// <summary>
        /// 根据模板和购物车商品数量/金额,计算出邮费的DataTable并返回
        /// </summary>
        /// <param name="cartdt">某一店铺的购物车</param>
        /// <param name="faredt">运费dt</param>
        private DataTable GetFareDT(DataTable cartdt)
        {
            //以初始运费高的模板为准(运费,免邮条件等)(避免有漏造成商户损失)
            List<M_Shop_Fare> fareList = new List<M_Shop_Fare>();
            M_Shop_Fare expMod = new M_Shop_Fare() { name = "exp", price = "0", plus = "0" };
            M_Shop_Fare emsMod = new M_Shop_Fare() { name = "ems", price = "0", plus = "0", enabled = false };
            M_Shop_Fare mailMod = new M_Shop_Fare() { name = "mail", price = "0", plus = "0", enabled = false };
            DataTable tlpDT = cartdt.DefaultView.ToTable(true, "FarePrice1");//有多少运费模板
            for (int i = 0; i < tlpDT.Rows.Count; i++)
            {
                int id = DataConvert.CLng(tlpDT.Rows[i]["FarePrice1"]);
                if (id < 1) continue;
                M_Shop_FareTlp fareMod = fareBll.SelReturnModel(id);
                JArray arr = JsonConvert.DeserializeObject<JArray>(fareMod.Express);
                //选出条件寄送方式不同,未禁用,价格最高的三种寄送方式
                foreach (JObject obj in arr)
                {
                    M_Shop_Fare model = JsonConvert.DeserializeObject<M_Shop_Fare>(obj.ToString());
                    if (!model.enabled) continue;
                    switch (model.name)
                    {
                        case "exp":
                            if (model.Price >= expMod.Price) { expMod = model; }
                            break;
                        case "ems":
                            if (model.Price >= emsMod.Price) { emsMod = model; }
                            break;
                        case "mail":
                            if (model.Price >= mailMod.Price) { mailMod = model; }
                            break;
                        default:
                            throw new Exception("快递类型异常");
                    }
                }
            }
            fareList.Add(expMod); fareList.Add(emsMod); fareList.Add(mailMod);
            DataTable faredt = CreateFareDT(Convert.ToInt32(cartdt.Rows[0]["StoreID"]));
            return FareDT(cartdt, faredt, fareList);
        }
        //实际运算填充faredt
        private DataTable FareDT(DataTable cartdt, DataTable faredt, List<M_Shop_Fare> fareList)
        {
            int pronum = Convert.ToInt32(cartdt.Compute("sum(Pronum)", ""));//统计金额和件数,看其是否满足包邮条件
            double allmoney = Convert.ToDouble(cartdt.Compute("sum(AllMoney)", ""));
            for (int i = 0; i < fareList.Count; i++)
            {
                bool isfree = false;
                M_Shop_Fare model = fareList[i];
                DataRow dr = faredt.Select("Name='" + model.Alias + "'")[0];
                if (!model.enabled)//未启用直接跳过 
                {
                    dr["Enabled"] = false;
                    continue;
                }
                switch (model.free_sel)
                {
                    case 0:
                        break;
                    case 1:
                        if (pronum >= model.Free_num)
                        { isfree = true; }
                        break;
                    case 2:
                        if (allmoney >= model.Free_Money)
                        { isfree = true; }
                        break;
                    case 3:
                        if (pronum >= model.Free_num || allmoney >= model.Free_Money)
                        { isfree = true; }
                        break;
                }
                if (isfree) { continue; }
                else
                {
                    if (model.Price > Convert.ToDouble(dr["Price"])) { dr["Price"] = model.Price; }
                    dr["Plus"] = Convert.ToDouble(dr["Plus"]) + model.Plus * (pronum - 1);
                }
            }//for end;
            return faredt;
        }
        //创建一个空的运费基础表
        private DataTable CreateFareDT(int storeid)
        {
            string[] arr = "快递,EMS,平邮".Split(',');
            DataTable faredt = new DataTable();
            faredt.Columns.Add(new DataColumn("StoreID", typeof(int)));
            faredt.Columns.Add(new DataColumn("Enabled", typeof(bool)));
            faredt.Columns.Add(new DataColumn("ID", typeof(int)));
            faredt.Columns.Add(new DataColumn("Name", typeof(string)));//Alias
            faredt.Columns.Add(new DataColumn("Price", typeof(double)));//基础运费
            faredt.Columns.Add(new DataColumn("Plus", typeof(double)));//续件运费
            faredt.Columns.Add(new DataColumn("Total", typeof(double)));//续件运费
            faredt.Columns.Add(new DataColumn("Desc", typeof(string)));
            for (int i = 0; i < arr.Length; i++)
            {
                DataRow dr = faredt.NewRow();
                dr["StoreID"] = storeid;
                dr["Enabled"] = true;
                dr["ID"] = i;
                dr["name"] = arr[i];
                dr["Price"] = 0;
                dr["Plus"] = 0;
                dr["Desc"] = "";
                faredt.Rows.Add(dr);
            }
            return faredt;
        }
        //创建运费下拉表单
        private string CreateFareHtml(DataTable dt)
        {
            string selectTlp = "<select name='{0}' class='fare_select'>{1}</select>";//fare_storeid,optinohtml
            string optionTlp = "<option data-price='{0}' value='{1}'>{2}</option>";//price,id,Desc
            string html = "", result = "";
            if (dt.Select("Enabled='true'").Length < 1) { dt.Rows[0]["Enabled"] = true; }
            foreach (DataRow dr in dt.Rows)
            {
                if (!DataConvert.CBool(dr["Enabled"].ToString()))
                {
                    continue;
                }
                dr["Total"] = Convert.ToDouble(dr["Price"]) + Convert.ToDouble(dr["Plus"]);
                if (Convert.ToDouble(dr["Total"]) == 0)
                {
                    dr["Desc"] = dr["Name"] + " 免邮";
                }
                else
                {
                    dr["Desc"] = dr["Name"] + " " + dr["Total"] + "元";
                }
                html += string.Format(optionTlp, dr["Total"], dr["ID"], dr["Desc"]);
            }
            result = string.Format(selectTlp, "fare_" + dt.Rows[0]["StoreID"], html);
            return result;
        }
        #endregion
        #region Common
        public string GetShopUrl()
        {
            return orderCom.GetShopUrl(DataConvert.CLng(Eval("StoreID")), Convert.ToInt32(Eval("ProID")));
        }
        public string GetImgUrl(object o)
        {
            return function.GetImgUrl(o);
        }
        public string GetStockStatus()
        {
            int pronum = Convert.ToInt32(Eval("Pronum"));
            int stock = Convert.ToInt32(Eval("Stock"));
            if (HasStock(Eval("Allowed"), stock, pronum))
            {
                return "<span class='r_green_x'>有货</span>";
            }
            else
            {
                return "<span class='r_red_x stockout'>缺货</span>";
            }

        }
        //已折扣的金额
        public string GetDisCount()
        {
            return "- " + (Convert.ToDouble(Eval("AllIntegral")) - Convert.ToDouble(Eval("AllMoney"))).ToString("f2");
        }
        //发票
        public string getInvoText(int flag)
        {
            return flag == 0 ? Regex.Split(Eval("Invoice").ToString(), Regex.Escape("||"))[0] : Regex.Split(Eval("Invoice").ToString(), Regex.Escape("||"))[1];
        }
        /// <summary>
        /// True有库存
        /// </summary>
        public bool HasStock(object allowed, int stock, int pronum)
        {
            bool flag = true;
            if (allowed.ToString().Equals("0") && stock < pronum)
            {
                flag = false;
            }
            return flag;
        }
        #endregion
        #region 优惠券
        public string Arrive_GetMoneyRegion() { return avBll.GetMoneyRegion(DataConvert.CDouble(Eval("MinAmount")), DataConvert.CDouble(Eval("MaxAMount"))); }
        public string Arrive_GetTypeStr() { return avBll.GetTypeStr(Convert.ToInt32(Eval("Type"))); }
        #endregion
    }
}