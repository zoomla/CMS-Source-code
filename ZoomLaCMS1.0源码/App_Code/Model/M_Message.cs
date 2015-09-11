namespace ZoomLa.Model
{
    using System;
    using System.Data;
    using System.Configuration;
    using System.Web;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Web.UI.WebControls.WebParts;
    using System.Web.UI.HtmlControls;

    /// <summary>
    /// M_Message 的摘要说明
    /// </summary>
    public class M_Message
    {
        private int m_MsgID;
        private string m_Title;
        private string m_Content;
        private string m_Sender;
        private string m_Incept;
        private int m_status;
        private DateTime m_PostDate;


        public int MsgID
        {
            get
            {
                return this.m_MsgID;
            }
            set
            {
                this.m_MsgID = value;
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
        public string Content
        {
            get
            {
                return this.m_Content;
            }
            set
            {
                this.m_Content = value;
            }
        }
        public string Sender
        {
            get
            {
                return this.m_Sender;
            }
            set
            {
                this.m_Sender = value;
            }
        }
        public string Incept
        {
            get
            {
                return this.m_Incept;
            }
            set
            {
                this.m_Incept = value;
            }
        }
        public DateTime PostDate
        {
            get
            {
                return this.m_PostDate;
            }
            set
            {
                this.m_PostDate = value;
            }
        }
        private bool m_IsNull = false;
         public M_Message(bool value)
        {
            this.m_IsNull = value;
        }
        public bool IsNull
        {
            get { return this.m_IsNull; }
        }
        public int status
        {
            get
            {
                return this.m_status;
            }
            set
            {
                this.m_status = value;
            }
        }

        public M_Message()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
    }
}