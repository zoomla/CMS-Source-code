namespace ZoomLa.Model
{
    using System;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;
    public class M_Payment : M_Base
    {
        public int PaymentID
        {
            get;
            set;
        }
        //支付宝交易ID(仅支付宝使用)
        public String AlipayNO
        {
            get;
            set;
        }
        public int UserID
        {
            get;
            set;
        }
        /// <summary>
        /// 订单号,支持以,切割
        /// </summary>
        public string PaymentNum
        {
            get;
            set;
        }
        /// <summary>
        /// 支付平台ID
        /// </summary>
        public int PayPlatID
        {
            get;
            set;
        }
        /// <summary>
        /// RMB支付金额,以此计算费率
        /// </summary>
        public double MoneyPay
        {
            get;
            set;
        }
        /// <summary>
        /// 支付币种,0则为默认人民币,否则为币种ID
        /// </summary>
        public int MoneyID { get; set; }
        /// <summary>
        /// 计算货币汇率之后的金额,用于支付(OrderPay完成计算,PayOnline页面使用此值)
        /// </summary>
        public double MoneyReal { get; set; }
        /// <summary>
        /// 实际收到的金额
        /// </summary>
        public double MoneyTrue
        {
            get;
            set;
        }
        /// <summary>
        /// 交易时间
        /// </summary>
        public DateTime PayTime
        {
            get;
            set;
        }
        /// <summary>
        /// 交易成功时间
        /// </summary>
        public DateTime SuccessTime
        {
            get;
            set;
        }
        /// <summary>
        /// 交易状态1未付款,3已付款,5已充值游戏币
        /// </summary>
        public int Status
        {
            get;
            set;
        }
        /// <summary>
        /// 1:已删除,0:正常
        /// </summary>
        public int IsDel { get; set; }
        /// <summary>
        /// 1:未支付,3:已支付,5:已充值入账
        /// </summary>
        public enum PayStatus { NoPay = 1, HasPayed = 3, HasCharged = 5 }
        /// <summary>
        /// 处理状态(disuse,不参与逻辑) true:已处理
        /// </summary>
        public bool CStatus
        {
            get;
            set;
        }
        /// <summary>
        /// 支付平台中文信息(支付宝网银ID,微信Mid,支付后平台信息)
        /// </summary>
        public string PlatformInfo
        {
            get;
            set;
        }
        /// <summary>
        /// 备注(存优惠券信息等)
        /// </summary>
        public string Remark
        {
            get;
            set;
        }
        /// <summary>
        /// 用于支付的序号(因为不支持带,号的多订单符号)
        /// </summary>
        public string PayNo { get; set; }
        /// <summary>
        /// 已使用的优惠券面值(如多张的话则为总和)
        /// </summary>
        public double ArriveMoney { get; set; }
        /// <summary>
        /// 优惠券号码|密码,格式:号码|密码,号码2|密码2
        /// </summary>
        public string ArriveDetail { get; set; }
        /// <summary>
        /// 该支付单使用了多少积分
        /// </summary>
        public double UsePoint { get; set; }
        /// <summary>
        /// 系统备注,标注此支付单的信息,如捐赠:donate
        /// </summary>
        public string SysRemark { get; set; }
        public override string PK { get { return "PaymentID"; } }
        public override string TbName { get { return "ZL_Payment"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"PaymentID","Int","4"},
                                  {"UserID","Int","4"},
                                  {"PaymentNum","NVarChar","200"},
                                  {"PayPlatID","Int","4"},
                                  {"MoneyPay","Money","8"},
                                  {"MoneyTrue","Money","8"},
                                  {"PayTime","DateTime","8"},
                                  {"SuccessTime","DateTime","8"},
                                  {"Status","Int","4"},
                                  {"PlatformInfo","NVarChar","200"},
                                  {"Remark","NVarChar","255"},
                                  {"CStatus","Bit","4"},
                                  {"AlipayNO","NVarChar","100"},
                                  {"PayNo","NVarChar","200"},
                                  {"ArriveMoney","Money","8"},
                                  {"ArriveDetail","NVarChar","500"},
                                  {"MoneyID","Int","4"},
                                  {"MoneyReal","Money","8"},
                                  {"IsDel","Int","4"},
                                  {"UsePoint","Money","8" },
                                  {"SysRemark","NVarChar","255" }
                                 };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_Payment model = this;
            if (model.PayTime <= DateTime.MinValue) model.PayTime = DateTime.Now;
            if (model.SuccessTime <= DateTime.MinValue) model.SuccessTime = DateTime.Now;
            if (model.MoneyReal == 0) { model.MoneyReal = model.MoneyPay; }
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.PaymentID;
            sp[1].Value = model.UserID;
            sp[2].Value = model.PaymentNum;
            sp[3].Value = model.PayPlatID;
            sp[4].Value = model.MoneyPay;
            sp[5].Value = model.MoneyTrue;
            sp[6].Value = model.PayTime;
            sp[7].Value = model.SuccessTime;
            sp[8].Value = model.Status;
            sp[9].Value = model.PlatformInfo;
            sp[10].Value = model.Remark;
            sp[11].Value = model.CStatus;
            sp[12].Value = model.AlipayNO;
            sp[13].Value = model.PayNo;
            sp[14].Value = model.ArriveMoney;
            sp[15].Value = model.ArriveDetail;
            sp[16].Value = model.MoneyID;
            sp[17].Value = model.MoneyReal;
            sp[18].Value = model.IsDel;
            sp[19].Value = model.UsePoint;
            sp[20].Value = model.SysRemark;
            return sp;
        }
        public M_Payment GetModelFromReader(DbDataReader rdr)
        {
            M_Payment model = new M_Payment();
            model.PaymentID = Convert.ToInt32(rdr["PaymentID"]);
            model.UserID = Convert.ToInt32(rdr["UserID"]);
            model.PaymentNum = rdr["PaymentNum"].ToString();
            model.PayPlatID = Convert.ToInt32(rdr["PayPlatID"]);
            model.MoneyPay = Convert.ToDouble(rdr["MoneyPay"]);
            model.MoneyTrue = Convert.ToDouble(rdr["MoneyTrue"]);
            model.PayTime = Convert.ToDateTime(rdr["PayTime"]);
            model.SuccessTime = Convert.ToDateTime(rdr["SuccessTime"]);
            model.Status = Convert.ToInt32(rdr["Status"]);
            model.PlatformInfo = ConverToStr(rdr["PlatformInfo"]);
            model.Remark = ConverToStr(rdr["Remark"]);
            model.CStatus = Convert.ToBoolean(rdr["CStatus"]);
            model.AlipayNO = ConverToStr(rdr["AlipayNO"]);
            model.PayNo = ConverToStr(rdr["PayNo"]);
            model.ArriveMoney = ConverToDouble(rdr["ArriveMoney"]);
            model.ArriveDetail = ConverToStr(rdr["ArriveDetail"]);
            model.MoneyID = ConvertToInt(rdr["MoneyID"]);
            model.MoneyReal = Convert.ToDouble(rdr["MoneyReal"]);
            model.IsDel = ConvertToInt(rdr["IsDel"]);
            model.UsePoint = ConverToDouble(rdr["UsePoint"]);
            model.SysRemark = ConverToStr(rdr["SysRemark"]);
            //兼容旧系统
            if (MoneyReal < 0) { MoneyReal = MoneyPay; }
            rdr.Close();
            return model;
        }
    }
}