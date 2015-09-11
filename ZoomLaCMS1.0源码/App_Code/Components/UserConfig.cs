namespace ZoomLa.Components
{
    using System;
    /// <summary>
    /// 用户系统配置
    /// </summary>
    [Serializable]
    public class UserConfig
    {
        private bool m_AdminCheckReg;
        private bool m_EmailCheckReg;
        private string m_EmailOfRegCheck;
        private bool m_EnableCheckCodeOfLogin;
        private bool m_EnableCheckCodeOfReg;
        private bool m_EnableMultiLogin;
        private bool m_EnableMultiRegPerEmail;
        private bool m_EnableQAofReg;
        private bool m_EnableRegCompany;
        private bool m_EnableUserReg;
        private int m_GroupId;
        private bool m_IsNull;
        private double m_MoneyExchangePoint;
        private double m_MoneyExchangeRatioPoint;
        private double m_MoneyExchangeRatioValidDay;
        private double m_MoneyExchangeValidDay;
        private string m_PointName;
        private string m_PointUnit;
        private double m_PresentExp;
        private double m_PresentExpPerLogin;
        private double m_PresentMoney;
        private int m_PresentPoint;
        private int m_PresentValidNum;
        private int m_PresentValidUnit;
        private string m_RegAnswer1;
        private string m_RegAnswer2;
        private string m_RegAnswer3;
        private string m_RegFields_MustFill;
        private string m_RegFields_SelectFill;
        private string m_RegQuestion1;
        private string m_RegQuestion2;
        private string m_RegQuestion3;
        private double m_UserExpExchangePoint;
        private double m_UserExpExchangeRatioPoint;
        private double m_UserExpExchangeRatioValidDay;
        private double m_UserExpExchangeValidDay;
        private int m_UserGetPasswordType;
        private string m_UserName_RegDisabled;
        private int m_UserNameLimit;
        private int m_UserNameMax;

        public UserConfig()
        {
        }

        public UserConfig(bool value)
        {
            this.m_IsNull = value;
        }

        public bool AdminCheckReg
        {
            get
            {
                return this.m_AdminCheckReg;
            }
            set
            {
                this.m_AdminCheckReg = value;
            }
        }

        public bool EmailCheckReg
        {
            get
            {
                return this.m_EmailCheckReg;
            }
            set
            {
                this.m_EmailCheckReg = value;
            }
        }

        public string EmailOfRegCheck
        {
            get
            {
                return this.m_EmailOfRegCheck;
            }
            set
            {
                this.m_EmailOfRegCheck = value;
            }
        }

        public bool EnableCheckCodeOfLogin
        {
            get
            {
                return this.m_EnableCheckCodeOfLogin;
            }
            set
            {
                this.m_EnableCheckCodeOfLogin = value;
            }
        }

        public bool EnableCheckCodeOfReg
        {
            get
            {
                return this.m_EnableCheckCodeOfReg;
            }
            set
            {
                this.m_EnableCheckCodeOfReg = value;
            }
        }

        public bool EnableMultiLogin
        {
            get
            {
                return this.m_EnableMultiLogin;
            }
            set
            {
                this.m_EnableMultiLogin = value;
            }
        }

        public bool EnableMultiRegPerEmail
        {
            get
            {
                return this.m_EnableMultiRegPerEmail;
            }
            set
            {
                this.m_EnableMultiRegPerEmail = value;
            }
        }

        public bool EnableQAofReg
        {
            get
            {
                return this.m_EnableQAofReg;
            }
            set
            {
                this.m_EnableQAofReg = value;
            }
        }

        public bool EnableRegCompany
        {
            get
            {
                return this.m_EnableRegCompany;
            }
            set
            {
                this.m_EnableRegCompany = value;
            }
        }

        public bool EnableUserReg
        {
            get
            {
                return this.m_EnableUserReg;
            }
            set
            {
                this.m_EnableUserReg = value;
            }
        }

        public int GroupId
        {
            get
            {
                return this.m_GroupId;
            }
            set
            {
                this.m_GroupId = value;
            }
        }

        public bool IsNull
        {
            get
            {
                return this.m_IsNull;
            }
        }

        public double MoneyExchangePoint
        {
            get
            {
                return (this.MoneyExchangePointByMoney / this.MoneyExchangePointByPoint);
            }
        }

        public double MoneyExchangePointByMoney
        {
            get
            {
                return this.m_MoneyExchangePoint;
            }
            set
            {
                this.m_MoneyExchangePoint = value;
            }
        }

        public double MoneyExchangePointByPoint
        {
            get
            {
                return this.m_MoneyExchangeRatioPoint;
            }
            set
            {
                this.m_MoneyExchangeRatioPoint = value;
            }
        }

        public double MoneyExchangeValidDay
        {
            get
            {
                return (this.MoneyExchangeValidDayByMoney / this.MoneyExchangeValidDayByValidDay);
            }
        }

        public double MoneyExchangeValidDayByMoney
        {
            get
            {
                return this.m_MoneyExchangeValidDay;
            }
            set
            {
                this.m_MoneyExchangeValidDay = value;
            }
        }

        public double MoneyExchangeValidDayByValidDay
        {
            get
            {
                return this.m_MoneyExchangeRatioValidDay;
            }
            set
            {
                this.m_MoneyExchangeRatioValidDay = value;
            }
        }

        public string PointName
        {
            get
            {
                return this.m_PointName;
            }
            set
            {
                this.m_PointName = value;
            }
        }

        public string PointUnit
        {
            get
            {
                return this.m_PointUnit;
            }
            set
            {
                this.m_PointUnit = value;
            }
        }

        public double PresentExp
        {
            get
            {
                return this.m_PresentExp;
            }
            set
            {
                this.m_PresentExp = value;
            }
        }

        public double PresentExpPerLogin
        {
            get
            {
                return this.m_PresentExpPerLogin;
            }
            set
            {
                this.m_PresentExpPerLogin = value;
            }
        }

        public double PresentMoney
        {
            get
            {
                return this.m_PresentMoney;
            }
            set
            {
                this.m_PresentMoney = value;
            }
        }

        public int PresentPoint
        {
            get
            {
                return this.m_PresentPoint;
            }
            set
            {
                this.m_PresentPoint = value;
            }
        }

        public int PresentValidNum
        {
            get
            {
                return this.m_PresentValidNum;
            }
            set
            {
                this.m_PresentValidNum = value;
            }
        }

        public int PresentValidUnit
        {
            get
            {
                return this.m_PresentValidUnit;
            }
            set
            {
                this.m_PresentValidUnit = value;
            }
        }

        public string RegAnswer1
        {
            get
            {
                return this.m_RegAnswer1;
            }
            set
            {
                this.m_RegAnswer1 = value;
            }
        }

        public string RegAnswer2
        {
            get
            {
                return this.m_RegAnswer2;
            }
            set
            {
                this.m_RegAnswer2 = value;
            }
        }

        public string RegAnswer3
        {
            get
            {
                return this.m_RegAnswer3;
            }
            set
            {
                this.m_RegAnswer3 = value;
            }
        }

        public string RegFieldsMustFill
        {
            get
            {
                return this.m_RegFields_MustFill;
            }
            set
            {
                this.m_RegFields_MustFill = value;
            }
        }

        public string RegFieldsSelectFill
        {
            get
            {
                return this.m_RegFields_SelectFill;
            }
            set
            {
                this.m_RegFields_SelectFill = value;
            }
        }

        public string RegQuestion1
        {
            get
            {
                return this.m_RegQuestion1;
            }
            set
            {
                this.m_RegQuestion1 = value;
            }
        }

        public string RegQuestion2
        {
            get
            {
                return this.m_RegQuestion2;
            }
            set
            {
                this.m_RegQuestion2 = value;
            }
        }

        public string RegQuestion3
        {
            get
            {
                return this.m_RegQuestion3;
            }
            set
            {
                this.m_RegQuestion3 = value;
            }
        }

        public double UserExpExchangePoint
        {
            get
            {
                return (this.UserExpExchangePointByExp / this.UserExpExchangePointByPoint);
            }
        }

        public double UserExpExchangePointByExp
        {
            get
            {
                return this.m_UserExpExchangePoint;
            }
            set
            {
                this.m_UserExpExchangePoint = value;
            }
        }

        public double UserExpExchangePointByPoint
        {
            get
            {
                return this.m_UserExpExchangeRatioPoint;
            }
            set
            {
                this.m_UserExpExchangeRatioPoint = value;
            }
        }

        public double UserExpExchangeValidDay
        {
            get
            {
                return (this.UserExpExchangeValidDayByExp / this.UserExpExchangeValidDayByValidDay);
            }
        }

        public double UserExpExchangeValidDayByExp
        {
            get
            {
                return this.m_UserExpExchangeValidDay;
            }
            set
            {
                this.m_UserExpExchangeValidDay = value;
            }
        }

        public double UserExpExchangeValidDayByValidDay
        {
            get
            {
                return this.m_UserExpExchangeRatioValidDay;
            }
            set
            {
                this.m_UserExpExchangeRatioValidDay = value;
            }
        }

        public int UserGetPasswordType
        {
            get
            {
                return this.m_UserGetPasswordType;
            }
            set
            {
                this.m_UserGetPasswordType = value;
            }
        }

        public int UserNameLimit
        {
            get
            {
                return this.m_UserNameLimit;
            }
            set
            {
                this.m_UserNameLimit = value;
            }
        }

        public int UserNameMax
        {
            get
            {
                return this.m_UserNameMax;
            }
            set
            {
                this.m_UserNameMax = value;
            }
        }

        public string UserNameRegDisabled
        {
            get
            {
                return this.m_UserName_RegDisabled;
            }
            set
            {
                this.m_UserName_RegDisabled = value;
            }
        }
    }
}