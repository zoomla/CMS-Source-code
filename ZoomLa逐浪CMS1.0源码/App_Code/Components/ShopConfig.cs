namespace ZoomLa.Components
{
    using System;
    /// <summary>
    /// 商城配置
    /// </summary>
    [Serializable]
    public class ShopConfig
    {
        private string m_AgainBuyUrl;
        private string m_City;
        private string m_ContinueBuy;
        private string m_Country;
        private string m_EmailOfDeliver;
        private string m_EmailOfInvoice;
        private string m_EmailOfOrderConfirm;
        private string m_EmailOfReceiptMoney;
        private string m_EmailOfRefund;
        private string m_EmailOfSendCard;
        private bool m_EnableCoupon;
        private bool m_EnableGuestBuy;
        private bool m_EnablePartPay;
        private string m_Functionary;
        private bool m_IsNull;
        private decimal m_MoneyPresentPoint;
        private string m_PostCode;
        private string m_PrefixOrderFormNum;
        private string m_PrefixPaymentNum;
        private string m_Province;
        private float m_TaxRate;
        private int m_TaxRateType;

        public ShopConfig()
        {
        }

        public ShopConfig(bool value)
        {
            this.m_IsNull = value;
        }

        public string AgainBuyUrl
        {
            get
            {
                return this.m_AgainBuyUrl;
            }
            set
            {
                this.m_AgainBuyUrl = value;
            }
        }

        public string City
        {
            get
            {
                return this.m_City;
            }
            set
            {
                this.m_City = value;
            }
        }

        public string ContinueBuy
        {
            get
            {
                return this.m_ContinueBuy;
            }
            set
            {
                this.m_ContinueBuy = value;
            }
        }

        public string Country
        {
            get
            {
                return this.m_Country;
            }
            set
            {
                this.m_Country = value;
            }
        }

        public string EmailOfDeliver
        {
            get
            {
                return this.m_EmailOfDeliver;
            }
            set
            {
                this.m_EmailOfDeliver = value;
            }
        }

        public string EmailOfInvoice
        {
            get
            {
                return this.m_EmailOfInvoice;
            }
            set
            {
                this.m_EmailOfInvoice = value;
            }
        }

        public string EmailOfOrderConfirm
        {
            get
            {
                return this.m_EmailOfOrderConfirm;
            }
            set
            {
                this.m_EmailOfOrderConfirm = value;
            }
        }

        public string EmailOfReceiptMoney
        {
            get
            {
                return this.m_EmailOfReceiptMoney;
            }
            set
            {
                this.m_EmailOfReceiptMoney = value;
            }
        }

        public string EmailOfRefund
        {
            get
            {
                return this.m_EmailOfRefund;
            }
            set
            {
                this.m_EmailOfRefund = value;
            }
        }

        public string EmailOfSendCard
        {
            get
            {
                return this.m_EmailOfSendCard;
            }
            set
            {
                this.m_EmailOfSendCard = value;
            }
        }

        public bool EnableCoupon
        {
            get
            {
                return this.m_EnableCoupon;
            }
            set
            {
                this.m_EnableCoupon = value;
            }
        }

        public bool EnableGuestBuy
        {
            get
            {
                return this.m_EnableGuestBuy;
            }
            set
            {
                this.m_EnableGuestBuy = value;
            }
        }

        public bool EnablePartPay
        {
            get
            {
                return this.m_EnablePartPay;
            }
            set
            {
                this.m_EnablePartPay = value;
            }
        }

        public string Functionary
        {
            get
            {
                return this.m_Functionary;
            }
            set
            {
                this.m_Functionary = value;
            }
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
            get
            {
                if (this.m_MoneyPresentPoint <= 0M)
                {
                    return 1M;
                }
                return this.m_MoneyPresentPoint;
            }
            set
            {
                this.m_MoneyPresentPoint = value;
            }
        }

        public string PostCode
        {
            get
            {
                return this.m_PostCode;
            }
            set
            {
                this.m_PostCode = value;
            }
        }

        public string PrefixOrderFormNum
        {
            get
            {
                return this.m_PrefixOrderFormNum;
            }
            set
            {
                this.m_PrefixOrderFormNum = value;
            }
        }

        public string PrefixPaymentNum
        {
            get
            {
                return this.m_PrefixPaymentNum;
            }
            set
            {
                this.m_PrefixPaymentNum = value;
            }
        }

        public string Province
        {
            get
            {
                return this.m_Province;
            }
            set
            {
                this.m_Province = value;
            }
        }

        public float TaxRate
        {
            get
            {
                return this.m_TaxRate;
            }
            set
            {
                this.m_TaxRate = value;
            }
        }

        public int TaxRateType
        {
            get
            {
                return this.m_TaxRateType;
            }
            set
            {
                this.m_TaxRateType = value;
            }
        }
    }
}