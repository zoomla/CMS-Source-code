namespace ZoomLa.Model
{
    using System;
    using System.Data.SqlClient;
    public class M_OrderList : M_Base
    {
        /// <summary>
        /// 订单类型:0为正常订单,1:酒店 2:航班 3:旅游 4:积分 5:域名,6:Purse充值,7:IDC订单,8:虚拟订单,9:IDC服务续费,10:代购,11:捐赠订单
        /// API:20(付款后再由代码对接第三方接口)
        /// </summary>
        public enum OrderEnum
        {
            Normal = 0, Hotel = 1, Flight = 2, Trval = 3, Score = 4, Domain = 5, Purse = 6, IDC = 7, Cloud = 8, IDCRen = 9, Fast = 10,Donate = 11,
            Other = 20
        }
        /// <summary>
        /// 订单状态：-99为回收站,-5:拒绝订单(管理员取消订单),-3:确认退款,-2:拒绝退款,-1:申请退款,0为正常,1:管理员确认,90:订单已付款,等待发货
        /// 91:卖家确认发货,99:订单已完成(买家确认收货)100:订单已完成分成(仅用于古文化)--100以后的用于扩展
        /// </summary>
        public enum StatusEnum
        {
            Recycle = -99, CancelOrder = -5, CheckDrawBack = -3, UnDrawBack = -2, DrawBack = -1, Normal = 0, Sured = 1, Payed = 90, Sended = 91, OrderFinish = 99, UnitFinish = 100
        }
        /// <summary>
        /// 支付方式,0:正常,1:需预付(PreMoney),2:代理商代收
        /// </summary>
        public enum PayTypeEnum { Normal = 0, PrePay = 1, HelpReceive = 2 }
        /// <summary>
        /// -2:已退款,-1:退款中,0:未支付,1:已支付(顾客完成支付),10:(管理员|店主)确认收款(依需要判断)
        /// </summary>
        public enum PayEnum { Refunded = -2, RequestRefund = -1, NoPay = 0, HasPayed = 1, SurePayed = 10 }
        /// <summary>
        /// 收货|物流状态,0:未发货,1:已发货,2已收货,-1已退货,-2确认退货
        /// </summary>
        public enum ExpEnum { NoSend = 0, HasSend = 1, HasReceived = 2, ApplyBack = -1, HasBack = -2 }
        //----------------------------
        /// <summary>
        /// 用于存储对应的快递ID(ZL_Order_Exp)
        /// </summary>
        public string ExpressNum { get; set; } 
        ///<summary>
        /// 发货公司(改为存自定义的快递公司)
        /// </summary>
        public string Company { get; set; }
        /// <summary>
        /// 快递单号
        /// </summary>
        public string ExpressDelivery { get; set; }
        /// <summary>
        /// 发票类型ID
        /// </summary>
        public int InvoType { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string MobileNum { get; set; }
        /// <summary>
        /// 需预付金额
        /// </summary>
        public double PreMoney { get { return Service_charge; } set { Service_charge = value; } }
        /// <summary>
        /// 原代购服务费,改用于存预付金额
        /// </summary>
        private double Service_charge { get; set; }
        /// <summary>
        /// 商品汇款货币代码
        /// </summary>
        public string Money_code { get; set; }
        /// <summary>
        /// 商品汇率
        /// </summary>
        public double Money_rate { get; set; }
        /// <summary>
        /// 商品总价(未优惠前,不含运费)
        /// </summary>
        public double Balance_price { get; set; }
        /// <summary>
        /// 运费备注
        /// </summary>
        public string Freight_remark { get; set; }
        /// <summary>
        /// 订单支付备注,如预付多少
        /// </summary>
        public string Balance_remark { get; set; }
        /// <summary>
        /// 主订单ID(Disuse)
        /// </summary>
        public int ParentID { get; set; }
        /// <summary>
        /// 该订单是否赠送积分,大于0赠送,小于扣除
        /// </summary>
        public int SendPointStatus { get; set; }
        /// <summary>
        /// 本次交易用户所得积分
        /// </summary>
        public int Integral { get; set; }
        public int id { get; set; }
        public bool IsCount { get; set; }
        public string province { get; set; }
        public string city { get; set; }
        public string OrderNo { get; set; }
        /// <summary>
        /// 收货人
        /// </summary>
        public string Reuser { get; set; }
        /// <summary>
        /// 收货人
        /// </summary>
        public string Receiver { get; set; }
        public string Rename { get; set; }
        /// <summary>
        /// 管理员拒绝或同意退款理由
        /// </summary>
        public string Guojia { get; set; }
        /// <summary>
        /// 省份和城市(Disuse)
        /// </summary>
        public string Shengfen { get; set; }
        public string Chengshi { get; set; }
        public string Diqu { get; set; }
        public string Jiedao { get; set; }
        public string ZipCode { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int Mobile { get; set; }
        /// <summary>
        /// 支付类型
        /// </summary>
        public int PayType { get { return Delivery; } set { Delivery = value; } }
        private int Delivery { get; set; }
        /// <summary>
        /// 缺货时的处理方式
        /// </summary>
        public int Outstock { get; set; }
        /// <summary>
        /// 送货时间
        /// </summary>
        public int Deliverytime { get; set; }
        public int Payment { get; set; }
        /// <summary>
        /// 发票信息
        /// </summary>
        public string Invoice { get; set; }
        /// <summary>
        /// 订单备注
        /// </summary>
        public string Ordermessage { get; set; }
        /// <summary>
        /// 退款理由
        /// </summary>
        public string Merchandiser { get; set; }
        /// <summary>
        /// 内部记录
        /// </summary>
        public string Internalrecords { get; set; }
        /// <summary>
        /// 订单金额
        /// </summary>
        public Double Ordersamount { get; set; }
        /// <summary>
        /// 实际收到金额
        /// </summary>
        public Double Receivablesamount { get; set; }
        /// <summary>
        /// 订单金额(备份)
        /// </summary>
        public Double Specifiedprice { get; set; }
        /// <summary>
        /// 运费
        /// </summary>
        public Double Freight { get; set; }
        /// <summary>
        /// 是否需要发票
        /// </summary>
        public int Invoiceneeds { get; set; }
        /// <summary>
        /// (disuse)
        /// </summary>
        public int Developedvotes { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public int OrderStatus { get; set; }
        /// <summary>
        /// 付款状态
        /// </summary>
        public int Paymentstatus { get; set; }
        /// <summary>
        /// 收货|物流状态:-1已退货,-2确认退货,0:未发货,1:已发货,2已收货确认
        /// </summary>
        public int StateLogistics { get; set; }
        /// <summary>
        /// 签收(Disuse)
        /// </summary>
        public int Signed { get; set; }
        /// <summary>
        /// 结清(Disuse),暂用于与退货关联,记录订单申请退款前的状态
        /// </summary>
        public int Settle { get; set; }
        /// <summary>
        /// 是否作废 0:正常,1:前端回收站,2:前端删除
        /// </summary>
        public int Aside { get; set; }
        /// <summary>
        /// 挂起(Disuse)
        /// </summary>
        public int Suspended { get; set; }
        /// <summary>
        /// 订单类型
        /// </summary>
        public int Ordertype { get; set; }
        public DateTime AddTime { get; set; }
        public string AddUser { get; set; }
        public int Userid { get; set; }
        /// <summary>
        /// 临时用于IDC续费订单,存储对应的IDC订单ID
        /// </summary>
        public int Promoter { get; set; }
        //存JSON数据
        public string Extend { get; set; }
        /// <summary>
        /// 店铺ID,自营为0
        /// </summary>
        public int StoreID { get; set; }
        //附加的虚拟币金额
        public string AllMoney_Json { get; set; }
        //--------------------
        public string AddRess { get { return (Shengfen + " " + Chengshi + " " + Diqu + " " + Jiedao).Replace("  "," "); } }
        public override string TbName { get { return "ZL_OrderInfo"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"ID","Int","4"},
                                  {"OrderNo","VarChar","50"},
                                  {"Reuser","VarChar","255"},
                                  {"Receiver","VarChar","50"}, 
                                  {"Rename","VarChar","255"}, 
                                  {"Guojia","VarChar","255"}, 
                                  {"Shengfen","VarChar","255"}, 
                                  {"Chengshi","VarChar","255"}, 
                                  {"Diqu","VarChar","255"}, 
                                  {"Jiedao","VarChar","255"}, 
                                  {"ZipCode","VarChar","50"}, 
                                  {"Phone","VarChar","50"}, 
                                  {"Email","VarChar","50"}, 
                                  {"Mobile","Int","4"}, 
                                  {"Delivery","Int","4"}, 
                                  {"Outstock","Int","4"}, 
                                  {"Deliverytime","Int","4"}, 
                                  {"Payment","Int","4"}, 
                                  {"Invoice","Text","400"}, 
                                  {"Ordermessage","Text","400"}, 
                                  {"Merchandiser","VarChar","50"}, 
                                  {"Internalrecords","Text","400"}, 
                                  {"Ordersamount","Money","10"}, 
                                  {"Receivablesamount","Money","10"}, 
                                  {"Specifiedprice","Money","10"}, 
                                  {"Freight","Money","10"}, 
                                  {"Invoiceneeds","Money","10"}, 
                                  {"Developedvotes","Int","4"}, 
                                  {"OrderStatus","Int","4"}, 
                                  {"Paymentstatus","Int","4"}, 
                                  {"StateLogistics","Int","4"}, 
                                  {"Signed","Int","4"}, 
                                  {"Settle","Int","4"}, 
                                  {"Aside","Int","4"}, 
                                  {"Suspended","Int","4"}, 
                                  {"Ordertype","Int","4"}, 
                                  {"AddTime","DateTime","8"}, 
                                  {"AddUser","VarChar","255"}, 
                                  {"Userid","Int","4"}, 
                                  {"city","NVarChar","50"}, 
                                  {"IsCount","Bit","4"}, 
                                  {"Integral","Int","4"}, 
                                  {"parentID","Int","4"}, 
                                  {"service_charge","Float","16"}, 
                                  {"Balance_price","Float","16"}, 
                                  {"Freight_remark","NVarChar","1000"}, 
                                  {"Balance_remark","NVarChar","1000"}, 
                                  {"Money_rate","Money","10"}, 
                                  {"Money_code","VarChar","4"}, 
                                  {"MobileNum","NVarChar","20"}, 
                                  {"ExpressDelivery","NVarChar","500"}, 
                                  {"InvoType","Int","4"}, 
                                  {"Promoter","Int","4"}, 
                                  {"ExpressNum","VarChar","400"},
                                  {"Company","NVarChar","400"},
                                  {"SendPointStatus","Int","4"},
                                  {"Province","NVarChar","50"},
                                  {"Extend","NText","20000"},
                                  {"StoreID","Int","4"},
                                  {"AllMoney_Json","NVarChar","500"},
                                 };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_OrderList model = this;
            if (model.AddTime<=DateTime.MinValue) { model.AddTime = DateTime.Now; }
            if (model.Balance_price == 0) { model.Balance_price = model.Ordersamount; }
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.id;
            sp[1].Value = model.OrderNo;
            sp[2].Value = model.Reuser;
            sp[3].Value = model.Receiver;
            sp[4].Value = model.Rename;
            sp[5].Value = model.Guojia;
            sp[6].Value = model.Shengfen;
            sp[7].Value = model.Chengshi;
            sp[8].Value = model.Diqu;
            sp[9].Value = model.Jiedao;
            sp[10].Value = model.ZipCode;
            sp[11].Value = model.Phone;
            sp[12].Value = model.Email;
            sp[13].Value = model.Mobile;
            sp[14].Value = model.Delivery;
            sp[15].Value = model.Outstock;
            sp[16].Value = model.Deliverytime;
            sp[17].Value = model.Payment;
            sp[18].Value = model.Invoice;
            sp[19].Value = model.Ordermessage;
            sp[20].Value = model.Merchandiser;
            sp[21].Value = model.Internalrecords;
            sp[22].Value = model.Ordersamount;
            sp[23].Value = model.Receivablesamount;
            sp[24].Value = model.Specifiedprice;
            sp[25].Value = model.Freight;
            sp[26].Value = model.Invoiceneeds;
            sp[27].Value = model.Developedvotes;
            sp[28].Value = model.OrderStatus;
            sp[29].Value = model.Paymentstatus;
            sp[30].Value = model.StateLogistics;
            sp[31].Value = model.Signed;
            sp[32].Value = model.Settle;
            sp[33].Value = model.Aside;
            sp[34].Value = model.Suspended;
            sp[35].Value = model.Ordertype;
            sp[36].Value = model.AddTime;
            sp[37].Value = model.AddUser;
            sp[38].Value = model.Userid;
            sp[39].Value = model.city;
            sp[40].Value = model.IsCount;
            sp[41].Value = model.Integral;
            sp[42].Value = model.ParentID;
            sp[43].Value = model.Service_charge;
            sp[44].Value = model.Balance_price;
            sp[45].Value = model.Freight_remark;
            sp[46].Value = model.Balance_remark;
            sp[47].Value = model.Money_rate;
            sp[48].Value = model.Money_code;
            sp[49].Value = model.MobileNum;
            sp[50].Value = model.ExpressDelivery;
            sp[51].Value = model.InvoType;
            sp[52].Value = model.Promoter;
            sp[53].Value = model.ExpressNum;
            sp[54].Value = model.Company;
            sp[55].Value = model.SendPointStatus;
            sp[56].Value = model.province;
            sp[57].Value = model.Extend;
            sp[58].Value = model.StoreID;
            sp[59].Value = model.AllMoney_Json;
            return sp;
        }
        public M_OrderList GetModelFromReader(SqlDataReader rdr)
        {
            M_OrderList model = new M_OrderList();
            model.id = Convert.ToInt32(rdr["id"]);
            model.AddUser = rdr["AddUser"].ToString();
            model.OrderNo = rdr["OrderNo"].ToString();
            model.Reuser = ConverToStr(rdr["Reuser"]);
            model.Receiver = ConverToStr(rdr["Receiver"]);
            model.Rename = ConverToStr(rdr["Rename"]);
            model.Guojia = ConverToStr(rdr["Guojia"]);
            model.Shengfen = ConverToStr(rdr["Shengfen"]);
            model.Chengshi = ConverToStr(rdr["Chengshi"]);
            model.Diqu = ConverToStr(rdr["Diqu"]);
            model.Jiedao = ConverToStr(rdr["Jiedao"]);
            model.ZipCode = ConverToStr(rdr["ZipCode"]);
            model.Phone = ConverToStr(rdr["Phone"]);
            model.Email = ConverToStr(rdr["Email"]);
            model.Mobile = ConvertToInt(rdr["Mobile"]);
            model.Delivery = ConvertToInt(rdr["Delivery"]);
            model.Outstock = ConvertToInt(rdr["Outstock"]);
            model.Deliverytime = ConvertToInt(rdr["Deliverytime"]);
            model.Payment = ConvertToInt(rdr["Payment"]);
            model.Invoice = ConverToStr(rdr["Invoice"]);
            model.Ordermessage = ConverToStr(rdr["Ordermessage"]);
            model.Merchandiser = ConverToStr(rdr["Merchandiser"]);
            model.Internalrecords = ConverToStr(rdr["Internalrecords"]);
            model.Ordersamount = ConverToDouble(rdr["Ordersamount"]);
            model.Receivablesamount = ConverToDouble(rdr["Receivablesamount"]);
            model.Specifiedprice = ConverToDouble(rdr["Specifiedprice"]);
            model.Freight = ConverToDouble(rdr["Freight"]);
            model.Invoiceneeds = ConvertToInt(rdr["Invoiceneeds"]);
            model.Developedvotes = ConvertToInt(rdr["Developedvotes"]);
            model.OrderStatus = ConvertToInt(rdr["OrderStatus"]);
            model.Paymentstatus = ConvertToInt(rdr["Paymentstatus"]);
            model.StateLogistics = ConvertToInt(rdr["StateLogistics"]);
            model.Signed = ConvertToInt(rdr["Signed"]);
            model.Settle = ConvertToInt(rdr["Settle"]);
            model.Aside = ConvertToInt(rdr["Aside"]);
            model.Suspended = ConvertToInt(rdr["Suspended"]);
            model.Ordertype = ConvertToInt(rdr["Ordertype"]);
            model.AddTime = Convert.ToDateTime(rdr["AddTime"]);
            model.Userid = ConvertToInt(rdr["Userid"]);
            model.city = ConverToStr(rdr["city"]);
            model.IsCount = ConverToBool(rdr["IsCount"]);
            model.Integral = ConvertToInt(rdr["Integral"]);
            model.ParentID = ConvertToInt(rdr["parentID"]);
            model.Service_charge = ConverToDouble(rdr["service_charge"]);
            model.Balance_price = ConverToDouble(rdr["Balance_price"]);
            model.Freight_remark =ConverToStr( rdr["Freight_remark"]);
            model.Balance_remark = ConverToStr(rdr["Balance_remark"]);
            model.Money_rate = ConverToDouble(rdr["Money_rate"]);
            model.Money_code = ConverToStr(rdr["Money_code"]);
            model.MobileNum = ConverToStr(rdr["MobileNum"]);
            model.ExpressDelivery = ConverToStr(rdr["ExpressDelivery"]);
            model.InvoType = ConvertToInt(rdr["InvoType"]);
            model.Promoter = ConvertToInt(rdr["Promoter"]);
            model.ExpressNum = ConverToStr(rdr["ExpressNum"]);
            model.Company = ConverToStr(rdr["Company"]); 
            model.SendPointStatus = ConvertToInt(rdr["SendPointStatus"]);
            model.province = ConverToStr(rdr["Province"]);
            model.Extend = ConverToStr(rdr["Extend"]);
            model.StoreID = ConvertToInt(rdr["StoreID"]);
            model.AllMoney_Json = ConverToStr(rdr["AllMoney_Json"]);
            rdr.Close();
            return model;
        }
        public M_OrderList() 
        {
            OrderStatus = (int)StatusEnum.Normal;
            Ordertype = (int)OrderEnum.Normal;
            StateLogistics = (int)ExpEnum.NoSend;
            Paymentstatus = (int)PayEnum.NoPay;
        }
    }
}