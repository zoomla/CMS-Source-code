namespace ZoomLa.Model
{
    using System;
    using System.Xml.Serialization;    
    /// <summary>
    /// 管理员工作台配置
    /// </summary>
    [Serializable]
    public class M_AdminProfile
    {
        private string m_AdminName;
        private string m_QuickLinksConfig;

        public M_AdminProfile()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        [XmlIgnore]
        public string AdminName
        {
            get
            {
                return this.m_AdminName;
            }
            set
            {
                this.m_AdminName = value;
            }
        }
        public string QuickLinksConfig
        {
            get
            {
                return this.m_QuickLinksConfig;
            }
            set
            {
                this.m_QuickLinksConfig = value;
            }
        }
    }
}