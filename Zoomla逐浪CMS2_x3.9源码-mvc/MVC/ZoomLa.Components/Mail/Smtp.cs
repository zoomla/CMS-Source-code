using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ZoomLa.Components.Mail
{
    #region mail
    /// <summary>
    /// 无插件版
    /// </summary>
    [Obsolete("过期测试")]
    public class Smtp
    {
        #region 字段
        public int port = 25;
        public String username = null;
        public String password = null;
        #endregion

        #region 构造函数
        /// <summary>
        /// SMTPClient smtp = new SMTPClient();
        /// smtp.Host = "mail.OpenSmtp.com";
        /// smtp.Port = 25;
        /// </summary>
        public Smtp() { }
        /// <summary>
        /// SMTPClient smtp = new SMTPClient("mail.OpenSmtp.com", 25);
        /// </summary>     
        public Smtp(string host, int port) { Host = host; Port = port; }
        /// <summary>
        /// SMTPClient smtp = new SMTPClient("mail.OpenSmtp.com", "login", "pwd");
        /// </summary>     
        public Smtp(string host, string username, string password)
        {
            this.Host = host;
            this.Port = port;
            this.Username = username;
            this.Password = password;
        }
        /// <summary>
        /// SMTPClient smtp = new SMTPClient("mail.OpenSmtp.com", "login", "pwd", 25);
        /// </summary>    
        public Smtp(string host, string username, string password, int port)
        {
            this.Host = host;
            this.Port = port;
            this.Username = username;
            this.Password = password;
        }
        #endregion

        #region 属性
        public String Host { get; set; }
        public Int32 Port
        {
            get { return (this.port); }
            set { this.port = value; }
        }
        public String Username
        {
            get { return this.username; }
            set { username = value; }
        }
        public String Password
        {
            get { return this.password; }
            set { password = value; }
        }
        #endregion

        #region 方法
        /// <summary>
        /// 发送邮件
        /// Smtp smtp = new Smtp("mail.OpenSmtp.com", 25);
        /// smtp.Username="";
        /// smtp.Password="";
        /// smtp.SendMail("support@OpenSmtp.com", "recipient@OpenSmtp.com", "Hi", "Hello Joe Smith");
        /// </summary>
        /// <param name="from">发送邮件地址</param>
        /// <param name="to">接收邮件地址</param>
        /// <param name="subject">邮件主题</param>
        /// <param name="body">邮件内容</param>
        public void SendMail(string from, string to, string subject, string body)
        {
            MailMsg msg = new MailMsg();
            msg.Subject = subject;
            msg.Body = body;
            msg.From = from;
            msg.To = to;
            SendMail(msg);
        }
        /// <summary>
        /// 发送邮件
        /// Smtp smtp = new Smtp("mail.OpenSmtp.com", 25);
        /// smtp.Username="";
        /// smtp.Password="";
        /// smtp.SendMail("support@OpenSmtp.com", "recipient@OpenSmtp.com", "Hi", "Hello Joe Smith",FileInfoList);
        /// </summary>
        /// <param name="from">发送邮件地址</param>
        /// <param name="to">接收邮件地址</param>
        /// <param name="subject">邮件主题</param>
        /// <param name="body">邮件内容</param>
        /// <param name="attList">邮件附件</param>
        public void SendMail(string from, string to, string subject, string body, List<String> attList)
        {
            MailMsg msg = new MailMsg();
            msg.Subject = subject;
            msg.Body = body;
            msg.From = from;
            msg.To = to;
            msg.AttachmentList = attList;
            SendMail(msg);
        }
        /// <summary>
        /// 发送邮件
        /// Smtp smtp = new Smtp();
        /// smtp.SendMail("zy0412326@163.com","zy0412326@sina.com","Test","这是一封测试邮件","FileInfo","smtp.163.com",25,"zy0412326","123456");
        /// </summary>
        /// <param name="from">发送邮件地址</param>
        /// <param name="to">接收邮件地址</param>
        /// <param name="subject">邮件主题</param>
        /// <param name="body">邮件内容</param>
        /// <param name="attList">邮件附件</param>
        /// <param name="host">SMTP服务器</param>
        /// <param name="port">端口号</param>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        public void SendMail(string from, string to, string subject, string body,
            List<String> attList, string host, int port, string username, string password)
        {
            MailMsg msg = new MailMsg();
            msg.Subject = subject;
            msg.Body = body;
            msg.From = from;
            msg.To = to;
            msg.AttachmentList = attList;

            this.Host = host;
            this.Port = port;
            this.Username = username;
            this.Password = password;
            SendMail(msg);
        }
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="msg">邮件对象</param>
        /// <param name="host">SMTP服务器</param>
        /// <param name="port">端口号</param>
        public void SendMail(MailMsg msg, string host, int port)
        {
            this.Host = host;
            this.Port = port;
            SendMail(msg);
        }
        /// <summary>
        /// 发送邮件
        /// MailMsg msg= new MailMsg();
        /// msg.Subject="";...
        /// Smtp smtp = new Smtp("mail.OpenSmtp.com", 25);
        /// smtp.Username="";
        /// smtp.Password="";
        /// smtp.SendMail(msg);
        /// </summary>
        /// <param name="msg"></param>
        public void SendMail(MailMsg msg)
        {
            System.Net.Mail.MailAddress from =
                new System.Net.Mail.MailAddress(msg.From);
            System.Net.Mail.MailAddress to = new System.Net.Mail.MailAddress(msg.To);
            //System.Net.Mail.MailAddress copyTo = new System.Net.Mail.MailAddress(model.CopyTo);
            System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage(from, to);
            message.Subject = msg.Subject;
            message.Body = msg.Body;
            message.SubjectEncoding = System.Text.Encoding.UTF8;
            message.BodyEncoding = System.Text.Encoding.UTF8;
            if (msg.AttachmentList != null && msg.AttachmentList.Count > 0)
            {
                foreach (String attachment in msg.AttachmentList)
                {
                    if (File.Exists(attachment))
                    {
                        System.Net.Mail.Attachment attachFile =
                            new System.Net.Mail.Attachment(attachment);
                        message.Attachments.Add(attachFile);
                    }
                }
            }
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient(this.Host);
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(this.Username, this.Password);
            client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            client.Send(message);
            message.Dispose();
        }
        #endregion
    }
    #endregion

    #region mailmodel
    /// <summary>
    /// 邮件对象
    /// </summary>
    public class MailMsg
    {
        private String subject = "landsea ship system synchronization";
        private String body = "这是一份自动发送的邮件，请勿回复，谢谢！！！";

        public String From { get; set; }
        public String To { get; set; }
        public String CopyTo { get; set; }
        public String Subject { get { return subject; } set { subject = value; } }
        public String Body { get { return body; } set { body = value; } }
        public List<String> AttachmentList { get; set; }
    }

    #endregion
}
