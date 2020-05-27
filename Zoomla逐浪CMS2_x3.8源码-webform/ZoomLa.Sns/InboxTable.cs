/*---------------------------------------------------------------
// Copyright (C) 2008  
// 版权所有
//
// 文件名： InboxTable.cs
// 文件功能描述：定义数据表InboxTable的业务实体
//
// 创建标识：Owner(2008-10-21) 
//
// 修改标识：
// 修改描述：
----------------------------------------------------------------*/
using System;


namespace BDUModel
{
    /// <summary>
    ///InboxTable业务实体
    /// </summary>
    [Serializable]
    public class InboxTable
    {
        #region 字段定义

        ///<summary>
        ///编号
        ///</summary>
        private Guid iD = Guid.Empty;

        ///<summary>
        ///发件人ID
        ///</summary>
        private Guid senderID = Guid.Empty;

        ///<summary>
        ///收件人ID
        ///</summary>
        private Guid inceptID = Guid.Empty;

        ///<summary>
        ///标题
        ///</summary>
        private string inboxTitle = String.Empty;

        ///<summary>
        ///内容
        ///</summary>
        private string inboxContent = String.Empty;

        ///<summary>
        ///发送时间
        ///</summary>
        private DateTime addtime;

        ///<summary>
        ///阅读状态(0为未阅读 1为阅读)
        ///</summary>
        private int inboxState;

        ///<summary>
        ///0为普通邮件 1为系统消息
        ///</summary>
        private int inboxType;


        #endregion

        #region 构造函数

        ///<summary>
        ///
        ///</summary>
        public InboxTable()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public InboxTable
        (
            Guid iD,
            Guid senderID,
            Guid inceptID,
            string inboxTitle,
            string inboxContent,
            DateTime addtime,
            int inboxState,
            int inboxType
        )
        {
            this.iD = iD;
            this.senderID = senderID;
            this.inceptID = inceptID;
            this.inboxTitle = inboxTitle;
            this.inboxContent = inboxContent;
            this.addtime = addtime;
            this.inboxState = inboxState;
            this.inboxType = inboxType;

        }

        #endregion

        #region 属性定义

        ///<summary>
        ///编号
        ///</summary>
        public Guid ID
        {
            get { return iD; }
            set { iD = value; }
        }

        ///<summary>
        ///发件人ID
        ///</summary>
        public Guid SenderID
        {
            get { return senderID; }
            set { senderID = value; }
        }

        ///<summary>
        ///收件人ID
        ///</summary>
        public Guid InceptID
        {
            get { return inceptID; }
            set { inceptID = value; }
        }

        ///<summary>
        ///标题
        ///</summary>
        public string InboxTitle
        {
            get { return inboxTitle; }
            set { inboxTitle = value; }
        }

        ///<summary>
        ///内容
        ///</summary>
        public string InboxContent
        {
            get { return inboxContent; }
            set { inboxContent = value; }
        }

        ///<summary>
        ///发送时间
        ///</summary>
        public DateTime Addtime
        {
            get { return addtime; }
            set { addtime = value; }
        }

        ///<summary>
        ///阅读状态(0为未阅读 1为阅读)
        ///</summary>
        public int InboxState
        {
            get { return inboxState; }
            set { inboxState = value; }
        }

        ///<summary>
        ///0为普通邮件 1为系统消息
        ///</summary>
        public int InboxType
        {
            get { return inboxType; }
            set { inboxType = value; }
        }

        #endregion

    }
}
