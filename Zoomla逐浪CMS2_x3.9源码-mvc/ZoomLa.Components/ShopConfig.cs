namespace ZoomLa.Components
{
    using System;
    /// <summary>
    /// 商城配置
    /// </summary>
    [Serializable]
    public class ShopConfig
    {
        public ShopConfig() { }
        public ShopConfig(bool value)
        {
            m_IsNull = value;
        }
        private bool m_IsNull = false;
        private string m_ItemRegular = "";
        /// <summary>
        /// 虚拟商品单位
        /// </summary>
        public string Unit
        {
            get;
            set;
        }
        public string AgainBuyUrl
        {
            get;
            set;
        }
        public string City
        {
            get;
            set;
        }
        public string ContinueBuy
        {
            get;
            set;
        }
        public string Country
        {
            get;
            set;
        }
        public string EmailOfDeliver
        {
            get;
            set;
        }
        public string EmailOfInvoice
        {
            get;
            set;
        }
        public string EmailOfOrderConfirm
        {
            get;
            set;
        }
        public string EmailOfReceiptMoney
        {
            get;
            set;
        }
        public string EmailOfRefund
        {
            get;
            set;
        }
        public string EmailOfSendCard
        {
            get;
            set;
        }
        public bool EnableCoupon
        {
            get;
            set;
        }
        public bool EnableGuestBuy
        {
            get;
            set;
        }
        public bool EnablePartPay
        {
            get;
            set;
        }
        public string Functionary
        {
            get;
            set;
        }
        public bool IsNull
        {
            get
            {
                return this.m_IsNull;
            }
        }
        public decimal MoneyPresentPoint
        {
            get;
            set;
        }
        public string PostCode
        {
            get;
            set;
        }
        public string PrefixOrderFormNum
        {
            get;
            set;
        }
        public string PrefixPaymentNum
        {
            get;
            set;
        }
        public string Province
        {
            get;
            set;
        }
        public float TaxRate
        {
            get;
            set;
        }
        public int TaxRateType
        {
            get;
            set;
        }
        /// <summary>
        /// 商品是否确认后支付 1:是
        /// </summary>
        public int IsCheckPay { get; set; }
        //---------新增
        /// <summary>
        /// Item生成规则(21yyyyMMddHH)+5位数字随机
        /// </summary>
        public string ItemRegular
        {
            get { if (string.IsNullOrEmpty(m_ItemRegular)) { m_ItemRegular = "21yyyyMMdd"; } return m_ItemRegular; }
            set { m_ItemRegular = value; }
        }
        /// <summary>
        /// 订单过期时间 单位:小时
        /// </summary>
        public int OrderExpired { get; set; }
        
        /// <summary>
        /// 订单可用积分支付比率,默认:10==10%
        /// </summary>
        public double PointRatiot { get; set; }
        /// <summary>
        /// 积分与现金兑换比率,默认:1积分==0.01
        /// </summary>
        public double PointRate { get; set; }

    }
}