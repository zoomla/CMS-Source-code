namespace ZoomLa.Components
{
    using System;
    using System.Collections.ObjectModel;
    using System.Xml.Serialization;

     [Serializable]
    public class BankComm
    { 
        //验证服务器地址
        private string m_DemoUrl;
        public string BankcommUrl
        {
            get
            {
                return this.m_DemoUrl;
            }
            set
            {
                this.m_DemoUrl = value;
            }
        }
        
    }
    
}  
