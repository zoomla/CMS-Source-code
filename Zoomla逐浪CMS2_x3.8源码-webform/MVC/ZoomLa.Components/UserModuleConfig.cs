using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Security;
using System.Web;
using System.Web.Caching;
using System.Xml.Serialization;

namespace ZoomLa.Components
{
    public class UserModuleConfig
    {
        //用户模块配置文件路径
        private string filePath;

        /// <summary>
        /// 构造函数
        /// </summary>
        public UserModuleConfig()
        {
            if (this.filePath == null)
            {
                HttpContext current = HttpContext.Current;
                if (current != null)
                {
                    this.filePath = current.Server.MapPath("~/Config/ModuleConfig.config");
                }
                else
                {
                    this.filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config/ModuleConfig.config");
                }
            }
        }
        /// <summary>
        /// 获取配置信息 先从缓存中读取配置信息，若缓存中没有配置信息则从配置文件中读取，并将配置信息设置到缓存
        /// </summary>
        /// <returns></returns>
        public static UserModuleConfigInfo ConfigInfo()
        {
            UserModuleConfigInfo info;
            info = ConfigReadFromFile();
            return info;
        }
        /// <summary>
        /// 从配置文件获取配置信息
        /// </summary>
        /// <returns>SiteConfigInfo</returns>
        public static UserModuleConfigInfo ConfigReadFromFile()
        {
            using (Stream stream = new FileStream(new UserModuleConfig().FilePath, FileMode.Open, FileAccess.Read))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(UserModuleConfigInfo));
                return (UserModuleConfigInfo)serializer.Deserialize(stream);
            }
        }
        /// <summary>
        /// 更新配置文件
        /// </summary>
        /// <param name="config"></param>
        public void Update(UserModuleConfigInfo config)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(UserModuleConfigInfo));
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
        /// <summary>
        /// 
        /// </summary>
        public static JobsConfig JobsConfig
        {
            get
            {
                return ConfigInfo().JobsConfig;
            }
        }
    }
}
