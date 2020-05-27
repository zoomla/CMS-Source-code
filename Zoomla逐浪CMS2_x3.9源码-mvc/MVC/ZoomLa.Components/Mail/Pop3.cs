using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZoomLa.Components.Mail
{
    public abstract class Pop3
    {
        #region 窗体变量

        /// <summary>
        /// 是否存在错误
        /// </summary>
        public abstract Boolean ExitsError { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public abstract String ErrorMessage { get; set; }
        /// <summary>
        /// POP3端口号
        /// </summary>
        public abstract Int32 Pop3Port { set; get; }
        /// <summary>
        /// POP3地址
        /// </summary>
        public abstract String Pop3Address { set; get; }
        /// <summary>
        /// 邮箱地址
        /// </summary>
        public abstract String EmailAddress { set; get; }
        /// <summary>
        /// 邮箱密码
        /// </summary>
        public abstract String EmailPassword { set; get; }

        #endregion

        #region 链接至服务器并读取邮件集合
        /// <summary>
        /// 链接至服务器并读取邮件集合
        /// </summary>
        public abstract Boolean Authenticate();

        #endregion

        #region 获取邮件数量
        /// <summary>
        /// 获取邮件数量
        /// </summary>
        /// <returns></returns>
        public abstract Int32 GetMailCount();

        #endregion

        #region 获取发件人
        /// <summary>
        /// 获取发件人
        /// </summary>
        /// <param name="mailIndex"></param>
        /// <returns></returns>
        public abstract String GetSendMialAddress(Int32 mailIndex);

        #endregion

        #region 获取邮件的主题
        /// <summary>
        /// 获取邮件的主题
        /// </summary>
        /// <param name="mailIndex"></param>
        /// <returns></returns>
        public abstract String GetMailUID(Int32 mailIndex);

        #endregion

        #region 取邮件的UID
        /// <summary>
        /// 获取邮件的UID
        /// </summary>
        /// <param name="mailIndex"></param>
        /// <returns></returns>
        public abstract String GetMailSubject(Int32 mailIndex);
        #endregion

        #region 获取邮件正文
        /// <summary>
        /// 获取邮件正文
        /// </summary>
        /// <param name="mailIndex">邮件顺序</param>
        /// <returns></returns>
        public abstract String GetMailBodyAsText(Int32 mailIndex);
        #endregion

        #region 获取邮件的附件
        public abstract Boolean GetMailAttachment(Int32 mailIndex, String receiveBackpath);

        #endregion

        #region 删除邮件
        /// <summary>
        /// 删除邮件
        /// </summary>
        /// <param name="mailIndex"></param>
        public abstract void DeleteMail(Int32 mailIndex);
        #endregion

        #region 关闭邮件服务器
        public abstract void Pop3Close();
        #endregion
    }
}
