<%@ WebHandler Language="C#" Class="order" %>

using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using ZoomLa.BLL;
using ZoomLa.BLL.API;
using ZoomLa.BLL.User;
using ZoomLa.BLL.Shop;
using ZoomLa.Model;
using ZoomLa.Model.Shop;
using ZoomLa.SQLDAL;
using ZoomLa.SQLDAL.SQL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

//订单相关操作
public class order :API_Base, IHttpHandler {

    B_User_API buapi=new B_User_API();
    B_OrderList orderBll = new B_OrderList();
    B_Product proBll = new B_Product();
    B_Cart cartBll = new B_Cart();
    B_CartPro cartProBll = new B_CartPro();
    B_Shop_FareTlp fareBll = new B_Shop_FareTlp();
    B_Payment payBll = new B_Payment();
    B_UserRecei receBll = new B_UserRecei();
    OrderCommon orderCom = new OrderCommon();
    private string OpenID { get { return Req("openid") ?? ""; } }
    //return DataConvert.CLng(Req("ProClass"));,暂不划分商品
    private int ProClass { get { return -100; } }
    public void ProcessRequest(HttpContext context)
    {
        M_UserInfo mu = B_User_API.GetLogin(OpenID);
        retMod.retcode = M_APIResult.Failed;
        retMod.callback = CallBack;
        if (mu.IsNull) { retMod.retmsg = "用户未登录"; RepToClient(retMod); }
        try
        {
            switch (Action)
            {
                case "carttoorder"://购物车生成订单,计算出当前金额和运费等返回()
                    {
                        string ids = Req("ids") ?? "";
                        if (string.IsNullOrEmpty(ids)) { retMod.retmsg = "未选定需要生成订单的商品"; }
                        else
                        {
                            //将商品按商店归类,并计算出价值
                            DataTable CartDT = cartBll.SelByCartID(mu.UserID.ToString(),mu.UserID, ProClass, ids);
                            OrderInfo ret = new OrderInfo();
                            int count=Convert.ToInt32(CartDT.Compute("SUM(Pronum)",""));//商品总数
                            double allmoney = UpdateCartAllMoney(CartDT);
                            DataTable storeDT = orderCom.SelStoreDT(CartDT);
                            foreach (DataRow dr in storeDT.Rows)
                            {
                                CartDT.DefaultView.RowFilter = "StoreID=" + dr["ID"];
                                DataTable dt = CartDT.DefaultView.ToTable();
                                if (dt.Rows.Count < 1) { continue; }
                                else
                                {
                                    OrderInfo.StoreInfo storeMod = new OrderInfo.StoreInfo();
                                    storeMod.id = Convert.ToInt32(dr["ID"]);
                                    storeMod.name = dr["StoreName"].ToString();
                                    storeMod.prodt = dt;
                                    storeMod.faredt = PreForAngular(GetFareDT(dt));//为每个店铺生成运费
                                    ret.list.Add(storeMod);
                                }
                            }
                            //订单信息
                            retMod.addon = "{\"money\":\"" + allmoney.ToString("f2") + "\",\"count\":\"" + count + "\",\"fare\":\"0\"}";
                            retMod.result = JsonConvert.SerializeObject(ret.list);
                            retMod.retcode = M_APIResult.Success;
                        }
                    }
                    break;
                case "createpayment"://选定好送货与地址后生成支付单(返回支付单)-->选择支付方式
                    {
                        //暂时不计算运费(运费需要确定好样式)
                        int receID = DataConvert.CLng(Req("receID"));
                        //object obj = CacheCenter.Pop(guid);
                        string ids = Req("ids") ?? "";
                        DataTable fareList = new DataTable();//用户所选定的每个店铺的快递方式
                        if (!string.IsNullOrEmpty(Req("fareList")))
                        {
                            fareList = JsonConvert.DeserializeObject<DataTable>(Req("fareList"));
                        }
                        M_UserRecei receMod = receBll.GetSelect(receID, mu.UserID);
                        if (string.IsNullOrEmpty(ids)) { retMod.retmsg = "未指定商品"; }
                        else if (receMod == null) { retMod.retmsg = "收货地址未指定"; }
                        else
                        {
                            M_Payment payMod = OrderToPayment(mu, receMod, ids, fareList);
                            retMod.result = payMod.PayNo;
                            retMod.retcode = M_APIResult.Success;
                        }
                    }
                    break;
                case "del":
                    {
                        string ids = Req("ids") ?? "";
                        orderBll.DelByIDS_U(ids, mu.UserID);
                        retMod.retcode = M_APIResult.Success;
                    }
                    break;
                case "list"://我的订单
                    {
                        PageSetting setting = GetPageSetting();
                        setting.where = "UserID=" + mu.UserID;
                        setting.order = "ID DESC";
                        //if (!string.IsNullOrEmpty(Req("skey"))) { }
                        setting.t1 = "ZL_OrderInfo";
                        DataTable dt = DBCenter.SelPage(setting);
                        if (Req("withPro") == "true")
                        {
                            dt.Columns.Add(new DataColumn("ProList", typeof(string)));
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                DataRow dr = dt.Rows[i];
                                dr["ProList"] = JsonConvert.SerializeObject(DBCenter.JoinQuery("A.ProID,A.Pronum,A.Proname,A.AllMoney,B.Thumbnails", "ZL_CartPro", "ZL_Commodities", "A.ProID=B.ID", "A.OrderListID=" + dr["ID"]));
                            }
                        }
                        retMod.result = JsonConvert.SerializeObject(dt);
                        retMod.page = new M_API_Page() { cpage = setting.cpage, itemCount = setting.itemCount, pageCount = setting.pageCount, psize = setting.psize };
                        retMod.retcode = M_APIResult.Success;
                    }
                    break;
                case "listWithPro"://订单数据,并包含商品
                    {
                        
                    }
                    break;
                case "get":
                    {
                        int id = DataConvert.CLng(Req("id"));
                        M_OrderList orderMod = orderBll.GetOrderListByid(id);
                        if (null == orderMod) { retMod.retmsg = "订单不存在!"; }
                        else if (orderMod.Userid != mu.UserID) { retMod.retmsg = "你无权访问该信息!"; }
                        else
                        {
                            retMod.result = JsonConvert.SerializeObject(orderMod);
                            retMod.retcode = M_APIResult.Success;
                        }
                    }
                    break;
                default:
                    retMod.retmsg = "[" + Action + "]接口不存在";
                    break;
            }
        }
        catch (Exception ex) { retMod.retmsg = ex.Message; }
        RepToClient(retMod);
    }

    public bool IsReusable { get { return false; } }
    /// <summary>
    /// 仅用于临时返回数据
    /// </summary>
    public class OrderInfo
    {
        public double allmoney = 0;
        public List<StoreInfo> list = new List<StoreInfo>();
        [JsonIgnore]
        public DateTime cdate = DateTime.Now;
        //店铺信息,与店铺下的商品与运费信息
        public class StoreInfo
        {
            public int id = 0;
            public string name = "";
            //该店所选购的商品表
            public DataTable prodt = new DataTable();
            //该店的运费
            public DataTable faredt = new DataTable();
        }
    }
    /// <summary>
    /// 计算费用,并生成支付单
    /// </summary>
    /// <param name="fareList">用户选择的快递方式,按每个店铺存储</param>
    private M_Payment OrderToPayment(M_UserInfo mu, M_UserRecei receMod, string ids, DataTable fareList)
    {
        DataTable cartDT = cartBll.SelByCookID(mu.UserID.ToString(), ProClass, ids);//需要购买的商品
        DataTable storeDT = cartDT.DefaultView.ToTable(true, "StoreID");
        M_Payment payMod = new M_Payment();
        payMod.PayNo = payBll.CreatePayNo();
        List<M_OrderList> orderList = new List<M_OrderList>();//用于生成临时订单,统计计算(Disuse)
        foreach (DataRow dr in storeDT.Rows)
        {
            M_OrderList Odata = new M_OrderList();
            Odata.Ordertype = OrderHelper.GetOrderType(ProClass);
            Odata.OrderNo = B_OrderList.CreateOrderNo((M_OrderList.OrderEnum)Odata.Ordertype);
            Odata.StoreID = Convert.ToInt32(dr["StoreID"]);
            cartDT.DefaultView.RowFilter = "StoreID=" + Odata.StoreID;
            DataTable storeCartDT = cartDT.DefaultView.ToTable();
            Odata.Receiver = receMod.ReceivName;
            Odata.Reuser = receMod.ReceivName;
            Odata.Phone = receMod.phone;
            Odata.MobileNum = receMod.MobileNum;
            Odata.Email = receMod.Email;
            Odata.Shengfen = receMod.Provinces;
            Odata.Jiedao = receMod.Street;
            Odata.ZipCode = receMod.Zipcode;
            //Odata.Invoiceneeds = DataConvert.CLng(Request.Form["invoice_rad"]);//是否需开发票
            //Odata.Invoice = Odata.Invoiceneeds == 0 ? "" : InvoTitle_T.Text + "||" + Invoice_T.Text;
            Odata.Rename = mu.UserName;
            Odata.Outstock = 0;//缺货处理
            //Odata.Ordermessage = ORemind_T.Text;//订货留言
            Odata.Merchandiser = "";//跟单员
            Odata.Internalrecords = ""; //内部记录
            Odata.IsCount = false;
            //-----金额计算
            Odata.Balance_price = GetTotalMoney(storeCartDT);
            Odata.Freight = GetFarePrice(storeCartDT, Odata.StoreID,fareList);//运费计算
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
            Odata.AddTime = DateTime.Now;
            Odata.AddUser = mu.UserName; ;
            Odata.Userid = mu.UserID;
            //Odata.Integral = DataConvert.CLng(Request.QueryString["jifen"]);
            Odata.Freight_remark = " ";
            Odata.Balance_remark = "";
            Odata.Promoter = 0;
            Odata.id = orderBll.Adds(Odata);
            cartProBll.CopyToCartPro(mu,storeCartDT, Odata.id);
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
        foreach (M_OrderList model in orderList)
        {
            payMod.PaymentNum += model.OrderNo + ",";
            payMod.MoneyPay += model.Ordersamount;
        }
        //优惠券
        //if (!string.IsNullOrEmpty(Arrive_T.Text))
        //{
        //    double arriveAmount = arriveBll.UserArrive(Arrive_T.Text, Arrive_Pwd.Text);
        //    payMod.MoneyPay = payMod.MoneyPay - arriveAmount;
        //    payMod.ArriveMoney = arriveAmount;
        //    payMod.ArriveDetail = Arrive_T.Text + "|" + Arrive_Pwd.Text;
        //}
        ////积分处理
        //if (point_body.Visible && DataConvert.CLng(Point_T.Text) > 0)
        //{
        //    int point = DataConvert.CLng(Point_T.Text);
        //    int maxPoint = (int)((SiteConfig.ShopConfig.PointRatiot * 0.01) * (double)payMod.MoneyPay / SiteConfig.ShopConfig.PointRate);
        //    //if (point <= 0) { function.WriteErrMsg("积分数值不正确"); }
        //    if (point > mu.UserExp) { function.WriteErrMsg("您的积分不足!"); }
        //    if (point > maxPoint) { function.WriteErrMsg("积分不能大于可兑换金额!"); }
        //    //生成支付单时扣除用户积分
        //    buser.ChangeVirtualMoney(mu.UserID, new M_UserExpHis() { ScoreType = (int)M_UserExpHis.SType.Point, score = -point, detail = "积分抵扣,支付单号:" + payMod.PayNo });
        //    payMod.MoneyPay = payMod.MoneyPay - (point * SiteConfig.ShopConfig.PointRate);
        //    payMod.UsePoint = point;
        //}
        if (payMod.MoneyPay <= 0) { payMod.MoneyPay = 0.01; }
        payMod.MoneyReal = payMod.MoneyPay;
        payMod.PaymentNum = payMod.PaymentNum.TrimEnd(',');
        payMod.Remark = cartDT.Rows.Count > 1 ? "[" + cartDT.Rows[0]["ProName"] as string + "]等" : cartDT.Rows[0]["ProName"] as string;
        payMod.UserID = mu.UserID;
        payMod.Status = 1;
        payMod.PaymentID = payBll.Add(payMod);
        return payMod;
    }
    //-----------------------------------------------------------
    //更新购物车中的AllMoney(实际购买总价),便于后期查看详情
    private double UpdateCartAllMoney(DataTable dt)
    {
        double allmoney = 0;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow dr = dt.Rows[i];
            M_Cart cartMod = cartBll.GetCartByid(Convert.ToInt32(dr["ID"]));
            M_Product proMod = proBll.GetproductByid(Convert.ToInt32(dr["Proid"]));
            //--附加币值计价
            if (orderCom.HasPrice(proMod.LinPrice_Json))
            {
                M_LinPrice priceMod = JsonConvert.DeserializeObject<M_LinPrice>(proMod.LinPrice_Json);
                priceMod.Purse = priceMod.Purse * cartMod.Pronum;
                priceMod.Sicon = priceMod.Sicon * cartMod.Pronum;
                priceMod.Point = priceMod.Point * cartMod.Pronum;
                dr["AllMoney_Json"] = JsonConvert.SerializeObject(priceMod);
                cartMod.AllMoney_Json = DataConvert.CStr(dr["AllMoney_Json"]);
            }
            //--多价格编号,则使用多价格编号的价钱,ProName(已在购物车页面更新)
            double price = proMod.LinPrice;
            proBll.GetPriceByCode(dr["code"], proMod.Wholesalesinfo, ref price);
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
    private double GetFarePrice(DataTable storecartDT, int storeid, DataTable fareList)
    {
        if (fareList.Rows.Count < 1) { return 0; }
        string selectedVal = "";
        DataRow[] drs = fareList.Select("StoreID='" + storeid + "'");
        if (drs.Length > 0) { selectedVal = drs[0]["expid"].ToString(); }
        if (string.IsNullOrEmpty(selectedVal)) { return 0; }
        else
        {
            DataTable faredt = GetFareDT(storecartDT);
            faredt.DefaultView.RowFilter = "ID='" + selectedVal + "'";
            DataRow dr = faredt.DefaultView.ToTable().Rows[0];
            return Convert.ToDouble(dr["Price"]) + DataConvert.CDouble(dr["Plus"]);
        }
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
        DataTable result= FareDT(cartdt, faredt, fareList);
        //-----------------
        //int[] fareArr = new int[] { 8, 10, 15 };
        //for (int i = 0; i < result.Rows.Count; i++)
        //{
        //    result.Rows[i]["Enabled"] = true;
        //    result.Rows[i]["Price"] = fareArr[i];
        //    result.Rows[i]["Plus"] = 1;
        //}
        //-----------------
        return result;
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
    //为Angular进行预处理,便于绑定数据,同于CreateFareHtml
    private DataTable PreForAngular(DataTable dt)
    {
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
        }
        dt.DefaultView.RowFilter = "Enabled='true'";
        return dt.DefaultView.ToTable();
    }
    //-------------------------Tools
    //从传参中获取信息,如无,则取默认值
    public PageSetting GetPageSetting() 
    {
        PageSetting setting = new PageSetting();
        setting.cpage = DataConvert.CLng(Req("cpage"));
        setting.psize = DataConvert.CLng(Req("psize"));
        if (setting.cpage < 1) { setting.cpage = 1; }
        if (setting.psize < 1) { setting.psize = 10; }
        return setting;
    }
}