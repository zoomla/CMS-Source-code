namespace ZoomLa.Components
{
    using System;
    using System.Xml.Serialization;
    /// <summary>
    /// 前台模板配置键
    /// </summary>
    [Serializable]
    public class FrontTemplate
    {
        private bool m_IsNull;
        private string m_Key;
        private string m_Value;

        public FrontTemplate()
        {
        }

        public FrontTemplate(bool value)
        {
            this.m_IsNull = value;
        }

        public bool IsNull
        {
            get
            {
                return this.m_IsNull;
            }
        }

        [XmlAttribute("Key")]
        public string Key
        {
            get
            {
                return this.m_Key;
            }
            set
            {
                this.m_Key = value;
            }
        }

        [XmlAttribute("Value")]
        public string Value
        {
            get
            {
                return this.m_Value;
            }
            set
            {
                this.m_Value = value;
            }
        }
    }
}