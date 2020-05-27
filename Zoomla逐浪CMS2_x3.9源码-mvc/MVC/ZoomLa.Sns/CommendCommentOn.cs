using System;
using System.Collections.Generic;
using System.Text;

namespace BDUModel
{
    /// <summary>
    ///CommendCommentOnҵ��ʵ��
    /// </summary>
    [Serializable]
    public class CommendCommentOn
    {
        #region �ֶζ���

        ///<summary>
        ///
        ///</summary>
        private Guid iD;

        ///<summary>
        ///�����û�ID
        ///</summary>
        private Guid commentOnUserID = Guid.Empty;

        ///<summary>
        ///��������
        ///</summary>
        private string commentOnContext = String.Empty;

        ///<summary>
        ///����ʱ��
        ///</summary>
        private DateTime commentOnTime;

        ///<summary>
        ///�������Ƽ�ID
        ///</summary>
        private Guid commentID = Guid.Empty;

        ///<summary>
        ///�û���
        ///</summary>
        private string userName = String.Empty;


        #endregion

        #region ���캯��

        ///<summary>
        ///
        ///</summary>
        public CommendCommentOn()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public CommendCommentOn
        (
            Guid iD,
            Guid commentOnUserID,
            string commentOnContext,
            DateTime commentOnTime,
            Guid commentID,
            string userName
        )
        {
            this.iD = iD;
            this.commentOnUserID = commentOnUserID;
            this.commentOnContext = commentOnContext;
            this.commentOnTime = commentOnTime;
            this.commentID = commentID;
            this.userName = userName;

        }

        #endregion

        #region ���Զ���

        ///<summary>
        ///
        ///</summary>
        public Guid ID
        {
            get { return iD; }
            set { iD = value; }
        }

        ///<summary>
        ///�����û�ID
        ///</summary>
        public Guid CommentOnUserID
        {
            get { return commentOnUserID; }
            set { commentOnUserID = value; }
        }

        ///<summary>
        ///��������
        ///</summary>
        public string CommentOnContext
        {
            get { return commentOnContext; }
            set { commentOnContext = value; }
        }

        ///<summary>
        ///����ʱ��
        ///</summary>
        public DateTime CommentOnTime
        {
            get { return commentOnTime; }
            set { commentOnTime = value; }
        }

        ///<summary>
        ///�������Ƽ�ID
        ///</summary>
        public Guid CommentID
        {
            get { return commentID; }
            set { commentID = value; }
        }

        ///<summary>
        ///�û���
        ///</summary>
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        #endregion

    }
}
