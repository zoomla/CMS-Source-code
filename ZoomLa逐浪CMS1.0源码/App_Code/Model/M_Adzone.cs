namespace ZoomLa.Model
{

    using System;
    using System.Data;
    using System.Configuration;
    using System.Web;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Web.UI.WebControls.WebParts;
    using System.Web.UI.HtmlControls;

    /// <summary>
    /// Adzone 的摘要说明
    /// </summary>
    [Serializable]
    public class M_Adzone
    {
        private int m_ZoneID;
        private string m_ZoneName;
        private string m_ZoneJSName;
        private string m_ZoneIntro;
        private int m_ZoneType;
        private bool m_DefaultSetting;
        private string m_ZoneSetting;
        private int m_ZoneWidth;
        private int m_ZoneHeight;
        private bool m_Active;
        private int m_ShowType;
        private DateTime m_UpdateTime;
        private bool m_IsNull;

        //private 
        public M_Adzone()
        {
            
        }
        public M_Adzone(bool flag)
        {
            this.m_IsNull = flag;
        }
        /// <summary>
        /// 版位ID
        /// </summary>
        public int ZoneID
        {
            get
            {
                return this.m_ZoneID;

            }
            set
            {
                this.m_ZoneID = value;
            }
        }
        /// <summary>
        /// 版位名称
        /// </summary>
        public string ZoneName
        {
            get
            {
                return this.m_ZoneName;

            }
            set
            {
                this.m_ZoneName = value;
            }
        }
        /// <summary>
        /// 生成js文件名
        /// </summary>
        public string ZoneJSName
        {
            get
            {
                return this.m_ZoneJSName;

            }
            set
            {
                this.m_ZoneJSName = value;
            }
        }
        /// <summary>
        /// 版位介绍
        /// </summary>
        public string ZoneIntro
        {
            get
            {
                return this.m_ZoneIntro;

            }
            set
            {
                this.m_ZoneIntro = value;
            }
        }
        /// <summary>
        /// 版位类型
        /// </summary>
        public int ZoneType
        {
            get
            {
                return this.m_ZoneType;

            }
            set
            {
                this.m_ZoneType = value;
            }
        }
        /// <summary>
        /// 标识是否默认设置
        /// </summary>
        public bool DefaultSetting
        {
            get
            {
                return this.m_DefaultSetting;

            }
            set
            {
                this.m_DefaultSetting = value;
            }
        }
        /// <summary>
        /// 记录广告显示位置参数
        /// </summary>
        public string ZoneSetting
        {
            get
            {
                return this.m_ZoneSetting;

            }
            set
            {
                this.m_ZoneSetting = value;
            }
        }
        /// <summary>
        /// 版位宽
        /// </summary>
        public int ZoneWidth
        {
            get
            {
                return this.m_ZoneWidth;

            }
            set
            {
                this.m_ZoneWidth = value;
            }
        }
        /// <summary>
        /// 版位高
        /// </summary>
        public int ZoneHeight
        {
            get
            {
                return this.m_ZoneHeight;

            }
            set
            {
                this.m_ZoneHeight = value;
            }
        }
        /// <summary>
        /// 标识是否活动
        /// </summary>
        public bool Active
        {
            get
            {
                return this.m_Active;

            }
            set
            {
                this.m_Active = value;
            }
        }
        /// <summary>
        /// 显示类型方式
        /// </summary>
        public int ShowType
        {
            get
            {
                return this.m_ShowType;

            }
            set
            {
                this.m_ShowType = value;
            }
        }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdateTime
        {
            get
            {
                return this.m_UpdateTime;

            }
            set
            {
                this.m_UpdateTime = value;
            }
        }
        public bool IsNull
        {
            get { return this.m_IsNull; }
        }
    }
}