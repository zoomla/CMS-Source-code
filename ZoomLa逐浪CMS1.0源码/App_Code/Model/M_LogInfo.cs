namespace ZoomLa.Model
{
    using System;
    using ZoomLa.ZLEnum;

    /// <summary>
    /// 日志信息
    /// </summary>
    public class M_LogInfo
    {
        private LogCategory m_Category = LogCategory.None;
        private int m_LogId;
        private string m_Message;
        private string m_PostString;
        private LogPriority m_Priority = LogPriority.Normal;
        private string m_ScriptName;
        private string m_Source;
        private DateTime m_Timestamp = DateTime.MaxValue;
        private string m_Title;
        private string m_UserIP;
        private string m_UserName;
        public M_LogInfo()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        public LogCategory Category
        {
            get
            {
                return this.m_Category;
            }
            set
            {
                this.m_Category = value;
            }
        }

        public int LogId
        {
            get
            {
                return this.m_LogId;
            }
            set
            {
                this.m_LogId = value;
            }
        }

        public string Message
        {
            get
            {
                return this.m_Message;
            }
            set
            {
                this.m_Message = value;
            }
        }

        public string PostString
        {
            get
            {
                return this.m_PostString;
            }
            set
            {
                this.m_PostString = value;
            }
        }

        public LogPriority Priority
        {
            get
            {
                return this.m_Priority;
            }
            set
            {
                this.m_Priority = value;
            }
        }

        public string ScriptName
        {
            get
            {
                return this.m_ScriptName;
            }
            set
            {
                this.m_ScriptName = value;
            }
        }

        public string Source
        {
            get
            {
                return this.m_Source;
            }
            set
            {
                this.m_Source = value;
            }
        }

        public DateTime Timestamp
        {
            get
            {
                return this.m_Timestamp;
            }
            set
            {
                this.m_Timestamp = value;
            }
        }

        public string Title
        {
            get
            {
                return this.m_Title;
            }
            set
            {
                this.m_Title = value;
            }
        }

        public string UserIP
        {
            get
            {
                return this.m_UserIP;
            }
            set
            {
                this.m_UserIP = value;
            }
        }

        public string UserName
        {
            get
            {
                return this.m_UserName;
            }
            set
            {
                this.m_UserName = value;
            }
        }
    }
}