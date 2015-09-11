namespace ZoomLa.Components
{
    using System;
    using System.Collections.ObjectModel;
    using System.Xml.Serialization;

    [Serializable, XmlRoot("SiteConfig")]
    public class SiteConfigInfo
    {
        //动态页模板
        private Collection<FrontTemplate> m_FrontTemplateList;        
        //IP限制配置
        private IPLockConfig m_IPLockConfig;
        //邮件系统配置
        private MailConfig m_MailConfig;
        //商城配置
        private ShopConfig m_ShopConfig;
        //网站信息配置
        private SiteInfo m_SiteInfo;
        //网站参数配置
        private SiteOption m_SiteOption;
        //缩略图配置
        private ThumbsConfig m_ThumbsConfig;
        //用户系统配置
        private UserConfig m_UserConfig;
        //水印配置
        private WaterMarkConfig m_WaterMarkConfig;
        /// <summary>
        /// 构造函数
        /// </summary>
        public SiteConfigInfo()
        {
            if (this.m_MailConfig == null)
            {
                this.m_MailConfig = new MailConfig();
            }
            if (this.m_IPLockConfig == null)
            {
                this.m_IPLockConfig = new IPLockConfig();
            }            
            if (this.m_UserConfig == null)
            {
                this.m_UserConfig = new UserConfig();
            }
            if (this.m_ShopConfig == null)
            {
                this.m_ShopConfig = new ShopConfig();
            }
            if (this.m_ThumbsConfig == null)
            {
                this.m_ThumbsConfig = new ThumbsConfig();
            }
            if (this.m_WaterMarkConfig == null)
            {
                this.m_WaterMarkConfig = new WaterMarkConfig();
            }
            if (this.m_SiteOption == null)
            {
                this.m_SiteOption = new SiteOption();
            }
            if (this.m_SiteInfo == null)
            {
                this.m_SiteInfo = new SiteInfo();
            }            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="infoList"></param>
        public void CopyToTemplateInfoList(Collection<FrontTemplate> infoList)
        {
            this.m_FrontTemplateList = new Collection<FrontTemplate>();
            foreach (FrontTemplate template in infoList)
            {
                this.m_FrontTemplateList.Add(template);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public Collection<FrontTemplate> FrontTemplateList
        {
            get
            {
                if (this.m_FrontTemplateList == null)
                {
                    this.m_FrontTemplateList = new Collection<FrontTemplate>();
                }
                return this.m_FrontTemplateList;
            }
        }        
        /// <summary>
        /// 
        /// </summary>
        public IPLockConfig IPLockConfig
        {
            get
            {
                return this.m_IPLockConfig;
            }
            set
            {
                this.m_IPLockConfig = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public MailConfig MailConfig
        {
            get
            {
                return this.m_MailConfig;
            }
            set
            {
                this.m_MailConfig = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public ShopConfig ShopConfig
        {
            get
            {
                return this.m_ShopConfig;
            }
            set
            {
                this.m_ShopConfig = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public SiteInfo SiteInfo
        {
            get
            {
                return this.m_SiteInfo;
            }
            set
            {
                this.m_SiteInfo = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public SiteOption SiteOption
        {
            get
            {
                return this.m_SiteOption;
            }
            set
            {
                this.m_SiteOption = value;
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public ThumbsConfig ThumbsConfig
        {
            get
            {
                return this.m_ThumbsConfig;
            }
            set
            {
                this.m_ThumbsConfig = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public UserConfig UserConfig
        {
            get
            {
                return this.m_UserConfig;
            }
            set
            {
                this.m_UserConfig = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public WaterMarkConfig WaterMarkConfig
        {
            get
            {
                return this.m_WaterMarkConfig;
            }
            set
            {
                this.m_WaterMarkConfig = value;
            }
        }
    }
}