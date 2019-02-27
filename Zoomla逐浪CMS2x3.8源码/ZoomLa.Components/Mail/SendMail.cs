using CDO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Sockets;
using System.Runtime.Remoting.Messaging;
using System.Text;


namespace ZoomLa.Components
{
    public sealed class SendMail
    {
        public enum AuthenticationType
        {
            None,
            Basic,
            Ntlm
        }
        public enum MailState
        {
            AttachmentSizeLimit = 10,
            ConfigFileIsWriteOnly = 6,
            FileNotFind = 3,
            MailConfigIsNullOrEmpty = 4,
            MustIssueStartTlsFirst = 11,
            NoMailToAddress = 1,
            None = 0,
            NonsupportSsl = 12,
            NoSubject = 2,
            Ok = 0x63,
            PortError = 13,
            SaveFailure = 7,
            SendFailure = 5,
            SmtpServerNotFind = 8,
            UserNameOrPasswordError = 9
        }
        public static string GetMailStateInfo(MailState mailcode)
        {
            switch (mailcode)
            {
                case MailState.NoMailToAddress:
                    return "缺少收件人地址";

                case MailState.NoSubject:
                    return "缺少邮件标题";

                case MailState.FileNotFind:
                    return "附件文件没找到";

                case MailState.MailConfigIsNullOrEmpty:
                    return "邮件参数配置不全";

                case MailState.SendFailure:
                    return "邮件发送失败";

                case MailState.ConfigFileIsWriteOnly:
                    return "无法读取邮件参数配置文件";

                case MailState.SaveFailure:
                    return "文件保存失败";

                case MailState.SmtpServerNotFind:
                    return "邮件服务器不存在";

                case MailState.UserNameOrPasswordError:
                    return "邮件服务器用户名和密码验证没通过";

                case MailState.AttachmentSizeLimit:
                    return "附件大小超出限制";

                case MailState.MustIssueStartTlsFirst:
                    return "邮件服务器仅接受TLS连接，可以设置支持SSL加密解决";

                case MailState.NonsupportSsl:
                    return "邮件服务器不支持SSL加密";

                case MailState.PortError:
                    return "邮件服务器端口错误";

                case MailState.Ok:
                    return "邮件发送成功";
            }
            return "没有返回信息";
        }
        /// <summary>
        /// 发送邮件(仅用于SMTP25或Explicit SSL(587端口)发送,Implicit SSL使用:SendSSLEmailL)
        /// 该方法仅用于测试,使用请用异步调用
        /// </summary>
        /// <param name="account">邮箱名</param>
        /// <param name="passwd">邮箱密码</param>
        /// <param name="server">SMTP服务器域名</param>
        /// <param name="port">SMTP为25,SSL:465,如果使用IMAP则需另设</param>
        /// <param name="mailInfo">邮件内容</param>
        /// <param name="attach">附件物理地址</param>
        /// <returns></returns>
        public static MailState SendEmail(string account, string passwd, string server, MailInfo mailInfo, string[] attach)
        {
            SmtpClient client = new SmtpClient(server, 25);
            NetworkCredential credential = new NetworkCredential(account, passwd);
            client.UseDefaultCredentials = true;
            client.EnableSsl = false;
            client.Credentials = credential.GetCredential(server, 25, "Basic");
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            //--------------------------------------------------
            MailMessage mailMessage = GetMailMessage(mailInfo, account);
            mailMessage.BodyEncoding = Encoding.UTF8;
            mailMessage.IsBodyHtml = true;
            foreach (string file in attach)//物理路径
            {
                if (string.IsNullOrEmpty(file)) continue;
                Attachment item = new Attachment(file);
                mailMessage.Attachments.Add(item);
            }
            client.Send(mailMessage);
            MailState state = MailState.Ok;
            return state;
        }
        public static MailState SendSSLEmail(string account, string passwd, string server, MailInfo mailInfo, string[] attach)
        {
            Message mailMessage = new Message();
            //CDO.IConfiguration iConfg; 
            //iConfg = oMsg.Configuration;
            //ADODB.Fields oFields;
            //oFields = iConfg.Fields;
            Configuration conf = new ConfigurationClass();
            conf.Fields[CdoConfiguration.cdoSendUsingMethod].Value = CdoSendUsing.cdoSendUsingPort;
            conf.Fields[CdoConfiguration.cdoSMTPAuthenticate].Value = CdoProtocolsAuthentication.cdoBasic;
            conf.Fields[CdoConfiguration.cdoSMTPUseSSL].Value = true;
            conf.Fields[CdoConfiguration.cdoSMTPServer].Value = server;//必填，而且要真实可用   
            conf.Fields[CdoConfiguration.cdoSMTPServerPort].Value = 465;
            conf.Fields[CdoConfiguration.cdoSendEmailAddress].Value = account;
            conf.Fields[CdoConfiguration.cdoSendUserName].Value = account;//真实的邮件地址   
            conf.Fields[CdoConfiguration.cdoSendPassword].Value = passwd;   //为邮箱密码，必须真实   


            conf.Fields.Update();
            mailMessage.Subject = mailInfo.Subject;
            mailMessage.Configuration = conf;
            //oMsg.TextBody = "Hello, how are you doing?";
            mailMessage.HTMLBody = mailInfo.MailBody;
            //TODO: Replace with your preferred Web page
            //oMsg.CreateMHTMLBody("http://www.microsoft.com",CDO.CdoMHTMLFlags.cdoSuppressNone, "", "");                    
            mailMessage.From = account;
            mailMessage.To = mailInfo.ToAddress.Address;
            //oMsg.AddAttachment("C:\Hello.txt", "", "");
            foreach (string file in attach)//物理路径
            {
                if (string.IsNullOrEmpty(file)) continue;
                mailMessage.AddAttachment(file, "", "");
            }
            mailMessage.Send();
            return MailState.Ok;
        }
        private static MailMessage GetMailMessage(MailInfo mailInfo, string fromEmail)
        {
            MailMessage message = new MailMessage();
            message.To.Add(mailInfo.ToAddress);
            if (mailInfo.ReplyTo != null)
            {
                message.ReplyTo = mailInfo.ReplyTo;
            }
            if (!string.IsNullOrEmpty(mailInfo.FromName))
            {
                message.From = new MailAddress(fromEmail, mailInfo.FromName);
            }
            else
            {
                message.From = new MailAddress(fromEmail);
            }
            message.Subject = mailInfo.Subject;
            message.SubjectEncoding = Encoding.UTF8;
            message.Body = mailInfo.MailBody;
            message.BodyEncoding = Encoding.UTF8;
            message.Priority = mailInfo.Priority;
            message.IsBodyHtml = mailInfo.IsBodyHtml;
            return message;
        }
        /// <summary>
        /// 使用系统邮件模块配置的信息,异步发送邮件(信息配置错误可能导致VS报错)
        /// </summary>
        public static MailState Send(MailInfo mailInfo)
        {
            MailConfig cfg = SiteConfig.MailConfig;
            SendHandler sender = null;
            if (cfg.Port == 25)
            {
                sender = SendEmail;
            }
            else if (cfg.Port == 465)
            {
                sender = SendSSLEmail;
            }
            else { throw new Exception("邮件端口[" + cfg.Port + "]配置不正确"); }
            if (string.IsNullOrEmpty(cfg.MailServerUserName)) { throw new Exception("未配置发送邮箱,取消发送"); }
            if (string.IsNullOrEmpty(cfg.MailServerPassWord)) { throw new Exception("未配置发送邮箱密码,取消发送"); }
            sender.BeginInvoke(cfg.MailServerUserName, cfg.MailServerPassWord, cfg.MailServer, mailInfo, "".Split(','), SendCallBack, null);
            return MailState.Ok;
        }
        //------------------Tools
        public static MailInfo GetMailInfo(string email, string from, string subject, string content)
        {
            MailAddress address = new MailAddress(email);
            MailInfo mailInfo = new MailInfo();
            mailInfo.Subject = subject;
            mailInfo.IsBodyHtml = true;
            mailInfo.FromName = from;
            mailInfo.ToAddress = address;
            mailInfo.MailBody = content;
            return mailInfo;
        }
        //------------------ASync
        private delegate MailState SendHandler(string account, string passwd, string server, MailInfo mailInfo, string[] attach);
        private static void SendCallBack(IAsyncResult result)
        {
            AsyncResult resultObj = (AsyncResult)result;
            SendHandler sender = (SendHandler)resultObj.AsyncDelegate;//获取原来的委托
            sender.EndInvoke(resultObj);
        }
    }
}