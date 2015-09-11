using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Sockets;
using System.Text;
using ZoomLa.ZLEnum;
namespace ZoomLa.Components
{
    /// <summary>
    /// 发送邮件
    /// </summary>
    public sealed class SendMail
    {
        public SendMail()
        {
            
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
        private static MailMessage GetMailMessage(MailInfo mailInfo, MailConfig mailSettings)
        {
            MailMessage message = new MailMessage();
            message.To.Add(mailInfo.ToAddress);
            
            if (mailInfo.ReplyTo != null)
            {
                message.ReplyTo = mailInfo.ReplyTo;
            }
            if (!string.IsNullOrEmpty(mailInfo.FromName))
            {
                message.From = new MailAddress(mailSettings.MailFrom, mailInfo.FromName);
            }
            else
            {
                message.From = new MailAddress(mailSettings.MailFrom);
            }
            message.Subject = mailInfo.Subject;
            message.SubjectEncoding = Encoding.UTF8;
            message.Body = mailInfo.MailBody;
            message.BodyEncoding = Encoding.UTF8;
            message.Priority = mailInfo.Priority;
            message.IsBodyHtml = mailInfo.IsBodyHtml;
            return message;
        }
        public static MailState Send(MailInfo mailInfo)
        {
            MailConfig mailConfig = SiteConfig.MailConfig;
            MailState mailcode = ValidInfo(mailInfo, mailConfig);
            
            if (mailcode == MailState.None)
            {
                SmtpClient client = new SmtpClient();
                MailMessage mailMessage = GetMailMessage(mailInfo, mailConfig);
                try
                {
                    try
                    {
                        client.Host = mailConfig.MailServer;
                        client.Port = mailConfig.Port;
                        NetworkCredential credential = new NetworkCredential(mailConfig.MailServerUserName, mailConfig.MailServerPassWord);
                        string authenticationType = string.Empty;
                        switch (mailConfig.AuthenticationType)
                        {
                            case AuthenticationType.None:
                                client.UseDefaultCredentials = false;
                                break;

                            case AuthenticationType.Basic:
                                client.UseDefaultCredentials = true;
                                authenticationType = "Basic";
                                break;

                            case AuthenticationType.Ntlm:
                                authenticationType = "NTLM";
                                break;
                        }
                        client.EnableSsl = mailConfig.EnabledSsl;
                        client.Credentials = credential.GetCredential(mailConfig.MailServer, mailConfig.Port, authenticationType);
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        client.Send(mailMessage);
                        mailcode = MailState.Ok;
                    }
                    catch (SmtpException exception)
                    {
                        SmtpStatusCode statusCode = exception.StatusCode;
                        if (statusCode != SmtpStatusCode.GeneralFailure)
                        {
                            if (statusCode == SmtpStatusCode.MustIssueStartTlsFirst)
                            {
                                goto Label_01D3;
                            }
                            if (statusCode == SmtpStatusCode.MailboxNameNotAllowed)
                            {
                                goto Label_01CE;
                            }
                            goto Label_01D8;
                        }
                        if (exception.InnerException is IOException)
                        {
                            mailcode = MailState.AttachmentSizeLimit;
                        }
                        else if (exception.InnerException is WebException)
                        {
                            if (exception.InnerException.InnerException == null)
                            {
                                mailcode = MailState.SmtpServerNotFind;
                            }
                            else if (exception.InnerException.InnerException is SocketException)
                            {
                                mailcode = MailState.PortError;
                            }
                        }
                        else
                        {
                            mailcode = MailState.NonsupportSsl;
                        }
                        goto Label_01FA;
                    Label_01CE:
                        mailcode = MailState.UserNameOrPasswordError;
                        goto Label_01FA;
                    Label_01D3:
                        mailcode = MailState.MustIssueStartTlsFirst;
                        goto Label_01FA;
                    Label_01D8:
                        mailcode = MailState.SendFailure;
                    }
                }
                finally
                {
                    
                }
            }
        Label_01FA:
            mailInfo.Msg = GetMailStateInfo(mailcode);
            return mailcode;
        }

        private static MailState ValidInfo(MailInfo mailInfo, MailConfig mailSettings)
        {
            MailState none = MailState.None;
            if (string.IsNullOrEmpty(mailSettings.MailFrom) || string.IsNullOrEmpty(mailSettings.MailServer))
            {
                return MailState.MailConfigIsNullOrEmpty;
            }
            if (mailInfo.ToAddress == null)
            {
                return MailState.NoMailToAddress;
            }
            if (string.IsNullOrEmpty(mailInfo.Subject))
            {
                return MailState.NoSubject;
            }            
            return none;
        }
    }
}