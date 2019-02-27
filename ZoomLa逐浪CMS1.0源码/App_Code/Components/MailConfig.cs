namespace ZoomLa.Components
{
    using ZoomLa.ZLEnum;
    using System;

    [Serializable]
    public class MailConfig
    {
        private AuthenticationType m_AuthenticationType;
        private bool m_EnabledSsl;
        private bool m_IsNull;
        private string m_MailFrom;
        private string m_MailServer;
        private string m_MailServerPassWord;
        private string m_MailServerUserName;
        private int m_Port;

        public MailConfig()
        {
        }

        public MailConfig(bool value)
        {
            this.m_IsNull = value;
        }

        public AuthenticationType AuthenticationType
        {
            get
            {
                return this.m_AuthenticationType;
            }
            set
            {
                this.m_AuthenticationType = value;
            }
        }

        public bool EnabledSsl
        {
            get
            {
                return this.m_EnabledSsl;
            }
            set
            {
                this.m_EnabledSsl = value;
            }
        }

        public bool IsNull
        {
            get
            {
                return this.m_IsNull;
            }
        }

        public string MailFrom
        {
            get
            {
                return this.m_MailFrom;
            }
            set
            {
                this.m_MailFrom = value;
            }
        }

        public string MailServer
        {
            get
            {
                return this.m_MailServer;
            }
            set
            {
                this.m_MailServer = value;
            }
        }

        public string MailServerPassWord
        {
            get
            {
                return this.m_MailServerPassWord;
            }
            set
            {
                this.m_MailServerPassWord = value;
            }
        }

        public string MailServerUserName
        {
            get
            {
                return this.m_MailServerUserName;
            }
            set
            {
                this.m_MailServerUserName = value;
            }
        }

        public int Port
        {
            get
            {
                return this.m_Port;
            }
            set
            {
                this.m_Port = value;
            }
        }
    }
}