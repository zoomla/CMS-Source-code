using System;
using System.Collections.Generic;
using System.Net.Mail;

namespace ZoomLa.Components
{
    /// <summary>
    /// 邮件信息
    /// </summary>
    public class MailInfo
    {
        //发件人名
        private string m_FromName;
        //邮件内容是否Html格式
        private bool m_IsBodyHtml;
        //邮件内容
        private string m_MailBody;
        //邮件优先级
        private MailPriority m_Priority;
        //邮件回复地址
        private MailAddress m_ReplyTo;
        //邮件标题
        private string m_Subject;        
        //收件人地址
        private MailAddress m_ToAddress;
        //是否空对象
        private bool m_IsNull;
        //邮件发送状态信息
        private string m_Msg;

        public MailInfo()
        {
            
        }
        public MailInfo(bool value)
        {
            this.m_IsNull = value;
        }
        /// <summary>
        /// 发件人名(该值必须有,且不能为邮箱名,否则有可能会被过滤掉)
        /// </summary>
        public string FromName
        {
            get { return this.m_FromName; }
            set { this.m_FromName = value; }
        }
        /// <summary>
        /// 邮件标题
        /// </summary>
        public string Subject
        {
            get { return this.m_Subject; }
            set { this.m_Subject = value; }
        }
        /// <summary>
        /// 邮件内容
        /// </summary>
        public string MailBody
        {
            get { return this.m_MailBody; }
            set { this.m_MailBody = value; }
        }
        /// <summary>
        /// 邮件内容是否Html格式
        /// </summary>
        public bool IsBodyHtml
        {
            get { return this.m_IsBodyHtml; }
            set { this.m_IsBodyHtml = value; }
        }
        /// <summary>
        /// 邮件优先级
        /// </summary>
        public MailPriority Priority
        {
            get { return this.m_Priority; }
            set { this.m_Priority = value; }
        }
        /// <summary>
        /// 邮件回复地址
        /// </summary>
        public MailAddress ReplyTo
        {
            get { return this.m_ReplyTo; }
            set { this.m_ReplyTo = value; }
        }
        /// <summary>
        /// 收件人地址
        /// </summary>
        public MailAddress ToAddress
        {
            get { return this.m_ToAddress; }
            set { this.m_ToAddress = value; }
        }
        /// <summary>
        /// 是否空对象
        /// </summary>
        public bool IsNull
        {
            get { return this.m_IsNull; }            
        }
        /// <summary>
        /// 邮件发送返回状态信息
        /// </summary>
        public string Msg
        {
            get { return this.m_Msg; }
            set { this.m_Msg = value; }
        }
    }
}