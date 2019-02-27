namespace ZoomLa.Model
{
    using System;
    using System.Data;

    /// <summary>
    /// M_Advertisement 的摘要说明
    /// </summary>
    
    public class M_Advertisement
    {
        private int m_AdId;
        private int m_UserId;
        private int m_AdType;
        private string m_AdName;
        private string m_ImgUrl;
        private int m_ImgWidth;
        private int m_ImgHeight;
        private int m_FlashWmode;
        private string m_ADIntro;
        private string m_LinkUrl;
        private int m_LinkTarget;
        private string m_LinkAlt;
        private int m_Priority;
        private string m_Setting;
        private bool m_CountView;
        private int m_Views;
        private bool m_CountClick;
        private int m_Clicks;
        private bool m_Passed;
        private DateTime m_OverdueDate;
        private bool m_IsNull;
        private int m_ZoneID;
        
        /// <summary>
        /// 广告标识
        /// </summary>
        public int AdId
        {
            get
            {
                return this.m_AdId;

            }
            set
            {
                this.m_AdId = value;
            }
        }
        /// <summary>
        /// 用户标识
        /// </summary>
        public int UserId
        {
            get
            {
                return this.m_UserId;

            }
            set
            {
                this.m_UserId = value;
            }
        }
        /// <summary>
        /// 广告类型
        /// </summary>
        public int AdType
        {
            get
            {
                return this.m_AdType;

            }
            set
            {
                this.m_AdType = value;
            }
        }
        /// <summary>
        /// 广告名称
        /// </summary>
        public string AdName
        {
            get
            {
                return this.m_AdName;

            }
            set
            {
                this.m_AdName = value;
            }
        }
        /// <summary>
        /// 图片地址
        /// </summary>
        public string ImgUrl
        {
            get
            {
                return this.m_ImgUrl;

            }
            set
            {
                this.m_ImgUrl = value;
            }
        }
        /// <summary>
        /// 图片广告的宽度
        /// </summary>
        public int ImgWidth
        {
            get
            {
                return this.m_ImgWidth;

            }
            set
            {
                this.m_ImgWidth = value;
            }
        }
        /// <summary>
        /// 图片广告的高度
        /// </summary>
        public int ImgHeight
        {
            get
            {
                return this.m_ImgHeight;

            }
            set
            {
                this.m_ImgHeight = value;
            }
        }
        /// <summary>
        /// 广告是flash类型/(透明/或不透明)
        /// </summary>
        public int FlashWmode
        {
            get
            {
                return this.m_FlashWmode;

            }
            set
            {
                this.m_FlashWmode = value;
            }
        }
        /// <summary>
        /// 广告介绍/内容
        /// </summary>
        public string ADIntro
        {
            get
            {
                return this.m_ADIntro;

            }
            set
            {
                this.m_ADIntro = value;
            }
        }
        /// <summary>
        /// 目标地址
        /// </summary>
        public string LinkUrl
        {
            get
            {
                return this.m_LinkUrl;

            }
            set
            {
                this.m_LinkUrl = value;
            }
        }
        /// <summary>
        ///新窗口,原窗口
        /// </summary>
        public int LinkTarget
        {
            get
            {
                return this.m_LinkTarget;

            }
            set
            {
                this.m_LinkTarget = value;
            }
        }
        /// <summary>
        ///联接接提示 
        /// </summary>
        public string LinkAlt
        {
            get
            {
                return this.m_LinkAlt;

            }
            set
            {
                this.m_LinkAlt = value;
            }
        }
        /// <summary>
        /// 权重
        /// </summary>
        public int Priority
        {
            get
            {
                return this.m_Priority;

            }
            set
            {
                this.m_Priority = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Setting
        {
            get
            {
                return this.m_Setting;

            }
            set
            {
                this.m_Setting = value;
            }
        }
        /// <summary>
        ///是否统计浏览次数
        /// </summary>
        public bool CountView
        {
            get
            {
                return this.m_CountView;

            }
            set
            {
                this.m_CountView = value;
            }
        }
        /// <summary>
        /// 浏览次数
        /// </summary>
        public int Views
        {
            get
            {
                return this.m_Views;

            }
            set
            {
                this.m_Views = value;
            }
        }
        /// <summary>
        /// 是否统计点击数
        /// </summary>
        public bool CountClick
        {
            get
            {
                return this.m_CountClick;

            }
            set
            {
                this.m_CountClick = value;
            }
        }
        /// <summary>
        /// 点击数
        /// </summary>
        public int Clicks
        {
            get
            {
                return this.m_Clicks;

            }
            set
            {
                this.m_Clicks = value;
            }
        }
        /// <summary>
        /// 是否设定过期
        /// </summary>
        public bool Passed
        {
            get
            {
                return this.m_Passed;

            }
            set
            {
                this.m_Passed = value;
            }
        }
        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime OverdueDate
        {
            get
            {
                return this.m_OverdueDate;

            }
            set
            {
                this.m_OverdueDate = value;
            }
        }
        /// <summary>
        /// 当前版位ID
        /// </summary>
        public int ZoneID
        {
            get { return this.m_ZoneID; }
            set { this.m_ZoneID = value; }
        }
        /// <summary>
        /// 离过期时间相差的天数
        /// </summary>
        public int Days
        {
            get
            {
                TimeSpan span = (TimeSpan)(this.m_OverdueDate.Date - DateTime.Today.Date);
                return span.Days;
            }
        }
        public bool IsNull
        {
            get { return this.m_IsNull; }
        }
        public M_Advertisement()
        {
            
        }
        public M_Advertisement(bool value)
        {
            this.m_IsNull = value;
        }
        
    }
}