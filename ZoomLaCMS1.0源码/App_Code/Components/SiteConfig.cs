namespace ZoomLa.Components
{
    using System;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Security;
    using System.Web;
    using System.Web.Caching;
    using System.Xml.Serialization;

    public sealed class SiteConfig
    {
        //网站配置文件路径
        private string filePath;
        /// <summary>
        /// 构造函数
        /// </summary>
        public SiteConfig()
        {
            if (this.filePath == null)
            {
                HttpContext current = HttpContext.Current;
                if (current != null)
                {
                    this.filePath = current.Server.MapPath("~/Config/Site.config");
                }
                else
                {
                    this.filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config/Site.config");
                }
            }
        }
        /// <summary>
        /// 含路径的构造函数
        /// </summary>
        /// <param name="path">配置文件路径</param>
        public SiteConfig(string path)
        {
            this.filePath = path;
        }
        /// <summary>
        /// 获取配置信息 先从缓存中读取配置信息，若缓存中没有配置信息则从配置文件中读取，并将配置信息设置到缓存
        /// </summary>
        /// <returns></returns>
        public static SiteConfigInfo ConfigInfo()
        {
            SiteConfigInfo info;
            info = ConfigReadFromFile();            
            return info;
        }
        /// <summary>
        /// 从配置文件获取配置信息
        /// </summary>
        /// <returns>SiteConfigInfo</returns>
        public static SiteConfigInfo ConfigReadFromFile()
        {
            using (Stream stream = new FileStream(new SiteConfig().FilePath, FileMode.Open, FileAccess.Read))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(SiteConfigInfo));
                return (SiteConfigInfo)serializer.Deserialize(stream);
            }
        }
        /// <summary>
        /// 更新配置文件
        /// </summary>
        /// <param name="config"></param>
        public void Update(SiteConfigInfo config)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(SiteConfigInfo));
                using (Stream stream = new FileStream(this.filePath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
                    namespaces.Add("", "");
                    serializer.Serialize(stream, config, namespaces);
                }
            }
            catch (SecurityException exception)
            {
                throw new SecurityException(exception.Message, exception.DenySetInstance, exception.PermitOnlySetInstance, exception.Method, exception.Demanded, exception.FirstPermissionThatFailed);
            }
        }
        /// <summary>
        /// 配置文件路径属性
        /// </summary>
        public string FilePath
        {
            get
            {
                return this.filePath;
            }
            set
            {
                this.filePath = value;
            }
        }

        public static Collection<FrontTemplate> FrontTemplateList
        {
            get
            {
                return ConfigInfo().FrontTemplateList;
            }
        }        

        public static IPLockConfig IPLockConfig
        {
            get
            {
                return ConfigInfo().IPLockConfig;
            }
        }

        public static MailConfig MailConfig
        {
            get
            {
                return ConfigInfo().MailConfig;
            }
        }

        public static ShopConfig ShopConfig
        {
            get
            {
                return ConfigInfo().ShopConfig;
            }
        }

        public static SiteInfo SiteInfo
        {
            get
            {
                return ConfigInfo().SiteInfo;
            }
        }

        public static SiteOption SiteOption
        {
            get
            {
                return ConfigInfo().SiteOption;
            }
        }

        public static ThumbsConfig ThumbsConfig
        {
            get
            {
                return ConfigInfo().ThumbsConfig;
            }
        }

        public static UserConfig UserConfig
        {
            get
            {
                return ConfigInfo().UserConfig;
            }
        }

        public static WaterMarkConfig WaterMarkConfig
        {
            get
            {
                return ConfigInfo().WaterMarkConfig;
            }
        }
    }
}