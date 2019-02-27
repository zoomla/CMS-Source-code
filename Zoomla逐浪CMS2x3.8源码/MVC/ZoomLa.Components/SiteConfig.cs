namespace ZoomLa.Components
{
    using System;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Security;
    using System.Web;
    using System.Web.Caching;
    using System.Xml.Serialization;
    using System.Xml;
    public sealed class SiteConfig
    {
        //网站配置文件路径
        private static string filePath = AppDomain.CurrentDomain.BaseDirectory + @"\Config\Site.config"; 
        private static SiteConfigInfo configMod = null;
        private static object Lockobj = new object();
        private SiteConfig() { }
        //实例化
        private static SiteConfigInfo GetInstance() 
        {
            if (configMod == null)
            {
                configMod = ConfigReadFromFile();
            }
            return configMod;
        }
        //从配置文件获取配置信息
        private static SiteConfigInfo ConfigReadFromFile()
        {
            using (Stream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(SiteConfigInfo));
                return (SiteConfigInfo)serializer.Deserialize(stream);
            }
        }
        public static void Update()
        {
            lock (Lockobj)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(SiteConfigInfo));
                using (Stream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
                {
                    XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
                    namespaces.Add("", "");//加上这句，否则会自动在根节点上加两个属性
                    serializer.Serialize(stream, GetInstance(), namespaces);
                    stream.Close(); stream.Dispose();
                }
            }
        }
        /*--------------------------------------------------------------------*/
        public static SiteInfo SiteInfo
        {
            get
            {
                return GetInstance().SiteInfo;
            }
        }
        public static SiteOption SiteOption
        {
            get
            {
                return GetInstance().SiteOption;
            }
        }
        public static ShopConfig ShopConfig
        {
            get
            {
                return GetInstance().ShopConfig;
            }
        }
        public static UserConfig UserConfig
        {
            get
            {
                return GetInstance().UserConfig;
            }
        }
        public static ThumbsConfig ThumbsConfig
        {
            get
            {
                return GetInstance().ThumbsConfig;
            }
        }
        public static IPLockConfig IPLockConfig
        {
            get
            {
                return GetInstance().IPLockConfig;
            }
        }
        public static MailConfig MailConfig
        {
            get
            {
                return GetInstance().MailConfig;
            }
        }
        public static WaterMarkConfig WaterMarkConfig
        {
            get
            {
                return GetInstance().WaterMarkConfig;
            }
        }
        public static YPage YPage { get { return GetInstance().YPage; } }
        public static string SiteMapath()
        {
            return AppDomain.CurrentDomain.BaseDirectory;
        }
    }
}