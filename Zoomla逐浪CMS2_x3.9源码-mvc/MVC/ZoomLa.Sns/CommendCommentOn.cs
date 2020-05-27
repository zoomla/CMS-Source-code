using System;
using System.Collections.Generic;
using System.Text;

namespace BDUModel
{
    /// <summary>
    ///CommendCommentOn业务实体
    /// </summary>
    [Serializable]
    public class CommendCommentOn
    {
        #region 字段定义

        ///<summary>
        ///
        ///</summary>
        private Guid iD;

        ///<summary>
        ///评论用户ID
        ///</summary>
        private Guid commentOnUserID = Guid.Empty;

        ///<summary>
        ///评论内容
        ///</summary>
        private string commentOnContext = String.Empty;

        ///<summary>
        ///评论时间
        ///</summary>
        private DateTime commentOnTime;

        ///<summary>
        ///被评论推荐ID
        ///</summary>
        private Guid commentID = Guid.Empty;

        ///<summary>
        ///用户名
        ///</summary>
        private string userName = String.Empty;


        #endregion

        #region 构造函数

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

        #region 属性定义

        ///<summary>
        ///
        ///</summary>
        public Guid ID
        {
            get { return iD; }
            set { iD = value; }
        }

        ///<summary>
        ///评论用户ID
        ///</summary>
        public Guid CommentOnUserID
        {
            get { return commentOnUserID; }
            set { commentOnUserID = value; }
        }

        ///<summary>
        ///评论内容
        ///</summary>
        public string CommentOnContext
        {
            get { return commentOnContext; }
            set { commentOnContext = value; }
        }

        ///<summary>
        ///评论时间
        ///</summary>
        public DateTime CommentOnTime
        {
            get { return commentOnTime; }
            set { commentOnTime = value; }
        }

        ///<summary>
        ///被评论推荐ID
        ///</summary>
        public Guid CommentID
        {
            get { return commentID; }
            set { commentID = value; }
        }

        ///<summary>
        ///用户名
        ///</summary>
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        #endregion

    }
}
