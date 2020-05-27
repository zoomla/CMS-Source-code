using OpenPop.Mime;
using OpenPop.Mime.Header;
using OpenPop.Pop3;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ZoomLa.Components.Mail
{
    public class OpenPopPop3 : Pop3
    {

        #region 窗体变量

        /// <summary>
        /// 是否存在错误
        /// </summary>
        public override Boolean ExitsError { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public override String ErrorMessage { get; set; }
        /// <summary>
        /// POP3端口号
        /// </summary>
        public override Int32 Pop3Port { set; get; }
        /// <summary>
        /// POP3地址
        /// </summary>
        public override String Pop3Address { set; get; }
        /// <summary>
        /// 邮箱地址
        /// </summary>
        public override String EmailAddress { set; get; }
        /// <summary>
        /// 邮箱密码
        /// </summary>
        public override String EmailPassword { set; get; }

        #endregion

        #region 私有变量
        private Pop3Client pop3Client;

        // private List<POP3_ClientMessage> pop3MessageList = new List<POP3_ClientMessage>();

        private Int32 mailTotalCount;
        #endregion

        #region 构造函数
        public OpenPopPop3() { }
        #endregion

        #region 链接至服务器并读取邮件集合
        /// <summary>
        /// 链接至服务器并读取邮件集合
        /// </summary>
        public override Boolean Authenticate()
        {
            try
            {
                pop3Client = new Pop3Client();
                if (pop3Client.Connected)
                    pop3Client.Disconnect();
                pop3Client.Connect(Pop3Address, Pop3Port, false);
                pop3Client.Authenticate(EmailAddress, EmailPassword, AuthenticationMethod.UsernameAndPassword);
                mailTotalCount = pop3Client.GetMessageCount();

                return ExitsError = true;
            }
            catch (Exception ex) { ErrorMessage = ex.Message; return ExitsError = false; }
        }
        #endregion

        #region 获取邮件数量
        /// <summary>
        /// 获取邮件数量
        /// </summary>
        /// <returns></returns>
        public override Int32 GetMailCount()
        {
            return mailTotalCount;
        }
        #endregion

        #region 获取发件人
        /// <summary>
        /// 获取发件人
        /// </summary>
        /// <param name="mailIndex"></param>
        /// <returns></returns>
        public override String GetSendMialAddress(Int32 mailIndex)
        {
            RfcMailAddress address = pop3Client.GetMessageHeaders(mailIndex).From;
            return address.Address;
        }
        public DateTime GetMailDate(int mailIndex) 
        {
            return pop3Client.GetMessageHeaders(mailIndex).DateSent;
        }
        #endregion

        #region 获取邮件的主题
        /// <summary>
        /// 获取邮件的主题
        /// </summary>
        /// <param name="mailIndex"></param>
        /// <returns></returns>
        public override String GetMailUID(Int32 mailIndex)
        {
            return pop3Client.GetMessageUid(mailIndex);

        }
        #endregion

        #region 获取邮件的UID
        /// <summary>
        /// 获取邮件的UID
        /// </summary>
        /// <param name="mailIndex"></param>
        /// <returns></returns>
        public override String GetMailSubject(Int32 mailIndex)
        {
            return pop3Client.GetMessageHeaders(mailIndex).Subject;
        }
        #endregion

        #region 获取邮件正文
        /// <summary>
        /// 获取邮件正文
        /// </summary>
        /// <param name="mailIndex">邮件顺序</param>
        /// <returns></returns>
        public override String GetMailBodyAsText(Int32 mailIndex)
        {
            Message message = pop3Client.GetMessage(mailIndex);
            MessagePart selectedMessagePart = message.MessagePart;
            string body = " ";
            if (selectedMessagePart.IsText)
            {
                body = selectedMessagePart.GetBodyAsText(); 
            }
            else if (selectedMessagePart.IsMultiPart)
            {
                //MessagePart plainTextPart = message.FindFirstPlainTextVersion();
                MessagePart plainTextPart = message.FindFirstHtmlVersion();
                if (plainTextPart != null)
                {
                    body = plainTextPart.GetBodyAsText();
                }
                else
                {
                    List<MessagePart> textVersions = message.FindAllTextVersions();
                    if (textVersions.Count >= 1)
                        body = textVersions[0].GetBodyAsText();
                    else
                        body = "<<OpenPop>> Cannot find a text version body in this message.";
                }
               
            }
            return body;
        }
        #endregion

        #region 获取邮件的附件
        public override bool GetMailAttachment(Int32 mailIndex, String receiveBackpath)
        {
            return string.IsNullOrEmpty(GetMailAttach(mailIndex, receiveBackpath));
        }
        public string GetMailAttach(Int32 mailIndex, String receiveBackpath)
        {
            string filepath = "";
            if (mailIndex == 0)
                return filepath;
            else if (mailIndex > mailTotalCount)
                return filepath;
            else
            {
                try
                {
                    Message message = pop3Client.GetMessage(mailIndex);
                    //邮件的全部附件.
                    List<MessagePart> attachments = message.FindAllAttachments();
                    foreach (MessagePart attachment in attachments)
                    {
                        string fileName = attachment.FileName;
                        string fileFullName = receiveBackpath + "\\" + fileName;
                        FileInfo fileInfo = new FileInfo(fileFullName);
                        if (fileInfo.Exists) fileInfo.Delete();
                        attachment.SaveToFile(fileInfo);
                        filepath += PToV(fileFullName)+"|";
                    }
                    //pop3Client.DeleteMessage(mailIndex);
                    return filepath;
                }
                catch (Exception ex)
                {
                    ErrorMessage = ex.Message;
                    return "";
                }
            }
        }
        #endregion
        public string PToV(string ppath)
        {
            ppath = ppath.Replace(@"\\", "\\");
            ppath = ppath.Replace(AppDomain.CurrentDomain.BaseDirectory, "");
            ppath = ppath.Replace(@"\", "/");
            return ("/" + ppath).Replace("//", "/");//避免有些带/有些不带
        }
        #region 删除邮件
        /// <summary>
        /// 删除邮件
        /// </summary>
        /// <param name="mailIndex"></param>
        public override void DeleteMail(Int32 mailIndex)
        {
            pop3Client.DeleteMessage(mailIndex);
        }
        #endregion

        #region 关闭邮件服务器
        public override void Pop3Close()
        {
            pop3Client.Disconnect();
            pop3Client.Dispose();
        }
        #endregion
    }
}
