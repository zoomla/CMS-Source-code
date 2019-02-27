using System;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace ZoomLa.Components
{
    [Serializable, XmlRoot("WaterModuleConfig")]
    public class WaterModuleConfigInfo
    {
        private WaterConfig m_WaterConfig;

        /// <summary>
        /// 构造函数
        /// </summary>
        public WaterModuleConfigInfo()
        {
            if (this.m_WaterConfig == null)
            {
                this.m_WaterConfig = new WaterConfig();
            }
        }
        /// <summary>
        /// 人才招聘模块配置
        /// </summary>
        public WaterConfig WaterConfig
        {
            get
            {
                return this.m_WaterConfig;
            }
            set
            {
                this.m_WaterConfig = value;
            }
        }
    }
}
