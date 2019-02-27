namespace ZoomLa.Components
{
    using System;
    /// <summary>
    /// 用户系统配置
    /// </summary>
    [Serializable]
    public class UserConfig
    {
        private bool m_EmailTell;//注册成功邮件提醒
        private string m_EmailOfRegCheck;
        private string m_MobileRegInfo;//注册手机短信验证内容
        
        private bool m_EnableCheckCodeOfLogin;
        private bool m_EnableCheckCodeOfReg;
        private bool m_EnableMultiLogin;
        private bool m_EnableMultiRegPerEmail;
        private bool m_EnableQAofReg;
        private bool m_EnableRegCompany;
        private bool m_EnableUserReg;
        private int m_GroupId;
        private double m_MoneyExchangePoint;
        private double m_MoneyExchangeRatioPoint;
        private double m_MoneyExchangeDummyPurseByMoney;
        private double m_MoneyExchangeDummyPurseByDummyPurse;
        private double m_MoneyExchangeRatioValidDay;
        private double m_MoneyExchangeValidDay;
        private string m_PointName;
        private string m_PointUnit;
        private double m_PresentExp;
        private double m_PresentMoney;
        private double m_PresentExpPerLogin;
        private double m_PointMoney;
        private double m_PointExp;
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
        private int m_CommentRule;
        private int m_InfoRule;
        private int m_RecommandRule;
        private int m_LoginRule;
        private bool m_UserValidateType;
        private int m_Promotion;
        private int m_PromotionType;
        private bool m_EmailRegis;
        private string m_RegRule;
        private string m_Agreement;
        private int m_PunchType;
        private int m_PunchVal;
        private double integral;
        private double m_ChangeSilverCoinByExp;
        private double m_PointSilverCoin;
        protected bool m_UserIDlgn;
        protected bool m_MobileReg;
        public double IntegralPercentage
        {
            get;
            set;
        }
        /// <summary>
        /// 用户邀请码最大数量
        /// </summary>
        public int InviteCodeCount
        {
            get;
            set;
        }
        public int PresentPointAll
        {
            get;
            set;
        }
        public int PresentPointTg
        {
            get;
            set;
        }
        /// <summary>
        /// 可使用站内短信功能的用户组
        /// </summary>
        private string m_MessageGroup;
        public bool AdminCheckReg
        {
            get;
            set;
        }
        public bool EmailCheckReg
        {
            get;
            set;
        }
        /// <summary>
        /// 是否开启Email注册登录
        /// </summary>
        public bool EmailRegis
        {
            get { return this.m_EmailRegis; }
            set { this.m_EmailRegis = value; }
        }
        //注册成功邮件提醒
        public bool EmailTell
        {
            get { return this.m_EmailTell; }
            set { this.m_EmailTell = value; }
        }
        public string EmailPlatReg
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
        public string MobileRegInfo
        {
            get { return this.m_MobileRegInfo; }
            set { this.m_MobileRegInfo = value; }
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
        /// <summary>
        /// 是否开启会员注册
        /// </summary>
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
        public double MoneyExchangeDummyPurseByMoney
        {
            get
            {
                return this.m_MoneyExchangeDummyPurseByMoney;
            }
            set
            {
                this.m_MoneyExchangeDummyPurseByMoney = value;
            }
        }
        public double MoneyExchangeDummyPurseByDummyPurse
        {
            get
            {
                return this.m_MoneyExchangeDummyPurseByDummyPurse;
            }
            set
            {
                this.m_MoneyExchangeDummyPurseByDummyPurse = value;
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
        /// <summary>
        /// 积分兑换金钱
        /// </summary>
        public double PointMoney
        {
            get
            {
                return this.m_PointMoney;
            }
            set
            {
                this.m_PointMoney = value;
            }
        }
        /// <summary>
        /// 兑换银币的积分
        /// </summary>
        public double ChangeSilverCoinByExp
        {
            get 
            {
                return this.m_ChangeSilverCoinByExp;
            }
            set
            {
                this.m_ChangeSilverCoinByExp = value;
            }
        }
        /// <summary>
        /// 积分兑换银币
        /// </summary>
        public double PointSilverCoin
        {
            get
            {
                return this.m_PointSilverCoin;
            }
            set
            {
                this.m_PointSilverCoin = value;
            }
        }
        /// <summary>
        /// 兑换积分
        /// </summary>
        public double PointExp
        {
            get
            {
                return this.m_PointExp;
            }
            set
            {
                this.m_PointExp = value;
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
        public double SigninPurse { get; set; }
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
        public double Integral {
            get
            {
                return this.integral;
            }
            set {
                this.integral=value;
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
        /// <summary>
        /// 注册时选填项目
        /// </summary>
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

        /// <summary>
        /// 新会员注册时用户名最少字符数
        /// </summary>
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
        /// <summary>
        /// 禁止注册的用户名
        /// </summary>
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
        /// <summary>
        /// 评论积分赠送
        /// </summary>
        public int CommentRule
        {
            get { return this.m_CommentRule; }
            set { this.m_CommentRule = value; }
        }
        public int InfoRule
        {
            get { return this.m_InfoRule; }
            set { this.m_InfoRule = value; }
        }
        public int RecommandRule
        {
            get { return this.m_RecommandRule; }
            set { this.m_RecommandRule = value; }
        }
        public int LoginRule
        {
            get { return this.m_LoginRule; }
            set { this.m_LoginRule = value; }
        }
        public bool UserValidateType
        {
            get { return this.m_UserValidateType; }
            set { this.m_UserValidateType = value; }
        }
        /// <summary>
        /// 可使用站内短信用户组
        /// </summary>
        public string MessageGroup
        {
            get { return this.m_MessageGroup; }
            set { this.m_MessageGroup = value; }
        }
        /// <summary>
        /// 推广获得类型
        /// </summary>
        public int PromotionType
        {
            get { return this.m_PromotionType; }
            set { this.m_PromotionType = value; }
        }
        /// <summary>
        /// 推广点数
        /// </summary>
        public int Promotion
        {
            get { return this.m_Promotion; }
            set { this.m_Promotion = value; }
        }
        /// <summary>
        /// 注册用户名规则
        /// </summary>
        public string RegRule
        {
            get { return this.m_RegRule; }
            set { this.m_RegRule = value; }
        }
        /// <summary>
        /// 注册协议显示方式
        /// </summary>
        public string Agreement
        {
            get { return this.m_Agreement; }
            set { this.m_Agreement = value; }
        }
        /// <summary>
        /// 是否启用支付宝登录
        /// </summary>
        public bool EnableAlipayCheckReg
        {
            get;
            set;
        }
        /// <summary>
        /// 打卡奖励领取的类型:0为不奖励,1为金额,2为虚拟币,3为积分,4为点劵
        /// </summary>
        public int PunchType
        {
            get { return this.m_PunchType; }
            set { this.m_PunchType = value; }
        }

        /// <summary>
        /// 打卡奖励领取的值
        /// </summary>
        public int PunchVal
        {
            get { return this.m_PunchVal; }
            set { this.m_PunchVal = value; }
        }
       
        //是否开启UserID登录
        public bool UserIDlgn
        {
            get { return this.m_UserIDlgn; }
            set { this.m_UserIDlgn = value; }
        }
        //是否开启手机注册
        public bool MobileReg
        {
            get { return this.m_MobileReg; }
            set { this.m_MobileReg = value; }
        }
        private int _mobileCodeNum = 4;
        //手机验证码位数
        public int MobileCodeNum { get { if (_mobileCodeNum < 4) { _mobileCodeNum = 4; } return _mobileCodeNum; } set { _mobileCodeNum = value; } }
        /// <summary>
        /// 验证码生成规则 0:数字,1:字母,2:数字+字母
        /// </summary>
        public int MobileCodeType { get; set; }
        //0:用户必须手机验证才能修改,1:修改用户自由修改手机号
        public string UserMobilAuth { get; set; }
        //是否启用Discuz!NT论坛
        public bool DisCuzNT
        {
            get;
            set;
        }
        //需要统计完成度的用户字段
        public string CountUserField { get; set; }
        
    }
}