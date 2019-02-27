using System;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace ZoomLa.Components
{
    [Serializable, XmlRoot("UserModuleConfig")]
    public class UserModuleConfigInfo
    {
        private JobsConfig m_JobsConfig;

        /// <summary>
        /// 构造函数
        /// </summary>
        public UserModuleConfigInfo()
        {
            if (this.m_JobsConfig == null)
            {
                this.m_JobsConfig = new JobsConfig();
            }
        }
        /// <summary>
        /// 人才招聘模块配置
        /// </summary>
        public JobsConfig JobsConfig
        {
            get
            {
                return this.m_JobsConfig;
            }
            set
            {
                this.m_JobsConfig = value;
            }
        }
    }
}
