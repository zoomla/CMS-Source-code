namespace ZoomLa.Model
{
    using System;
    /// <summary>
    /// 评论信息
    /// </summary>
    public class M_Comment
    {
        private int m_CommentID;
        private int m_GeneralID;    //对应的文章ID号
        private string m_Title;
        private string m_Contents;
        private bool m_Audited;     //bit 数据类型 对应 bool，例如：M_ModelField.cs
        private int m_UserID;
        private DateTime m_CommentTime;    //发表时间
        private int m_Score;
        private bool m_PK;

        public M_Comment()
        {            
        }
        /// <summary>
        /// 评论ID
        /// </summary>
        public int CommentID
        {
            get
            {
                return this.m_CommentID;
            }
            set
            {
                this.m_CommentID = value;
            }
        }
        /// <summary>
        /// 被评论的内容ID
        /// </summary>
        public int GeneralID
        {
            get
            {
                return this.m_GeneralID;
            }
            set
            {
                this.m_GeneralID = value;
            }
        }
        /// <summary>
        /// 评论标题
        /// </summary>
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
        /// <summary>
        /// 评论内容
        /// </summary>
        public string Contents
        {
            get
            {
                return this.m_Contents;
            }
            set
            {
                this.m_Contents = value;
            }
        }
        /// <summary>
        /// 是否审核通过
        /// </summary>
        public bool Audited
        {
            get
            {
                return this.m_Audited;
            }
            set
            {
                this.m_Audited = value;
            }
        }
        /// <summary>
        /// 发表评论的用户ID
        /// </summary>
        public int UserID
        {
            get
            {
                return this.m_UserID;
            }
            set
            {
                this.m_UserID = value;
            }
        }
        /// <summary>
        /// 评论发表时间
        /// </summary>
        public DateTime CommentTime
        {
            get
            {
                return this.m_CommentTime;
            }
            set
            {
                this.m_CommentTime = value;
            }
        }
        /// <summary>
        /// 评分
        /// </summary>
        public int Score
        {
            get
            {
                return this.m_Score;
            }
            set
            {
                this.m_Score = value;
            }
        }
        /// <summary>
        /// 是否支持方
        /// </summary>
        public bool PK
        {
            get
            {
                return this.m_PK;
            }
            set
            {
                this.m_PK = value;
            }
        }
    }
}
