using System;
using System.Collections.Generic;
using System.Text;

namespace BDUModel
{
    #region CommendTable枚举类型

    public enum CommendTableType
    {
        /// <summary>
        /// 其他
        /// </summary>
        ElseType,
        /// <summary>
        /// 书籍
        /// </summary>
        Book,
        /// <summary>
        /// 电影
        /// </summary>
        Cinema,
        /// <summary>
        /// 话题
        /// </summary>
        Topic,
        /// <summary>
        /// 族群
        /// </summary>
        Group,
        /// <summary>
        /// 活动
        /// </summary>
        Activity,
        /// <summary>
        /// 空类型
        /// </summary>
        NullType
    }
    #endregion
    

    /// <summary>
    ///CommendTable业务实体
    /// </summary>
    [Serializable]
    public class CommendTable
    {
        #region 字段定义

        ///<summary>
        ///
        ///</summary>
        private Guid iD;

        ///<summary>
        ///推荐标题
        ///</summary>
        private string commendTitle = String.Empty;

        ///<summary>
        ///推荐内容
        ///</summary>
        private string commendContext = String.Empty;

        ///<summary>
        ///推荐地址
        ///</summary>
        private string commendUrl = String.Empty;

        ///<summary>
        ///推荐用户ID
        ///</summary>
        private Guid commendUserID = Guid.Empty;

        ///<summary>
        ///推荐图片
        ///</summary>
        private string commendImage = String.Empty;

        ///<summary>
        ///推荐类型
        ///</summary>
        private int commendType;

        ///<summary>
        ///推荐时间
        ///</summary>
        private DateTime commendTime;

        ///<summary>
        ///用户名
        ///</summary>
        private string userName = String.Empty;


        #endregion

        #region 构造函数

        ///<summary>
        ///
        ///</summary>
        public CommendTable()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public CommendTable
        (
            Guid iD,
            string commendTitle,
            string commendContext,
            string commendUrl,
            Guid commendUserID,
            string commendImage,
            int commendType,
            DateTime commendTime,
            string userName
        )
        {
            this.iD = iD;
            this.commendTitle = commendTitle;
            this.commendContext = commendContext;
            this.commendUrl = commendUrl;
            this.commendUserID = commendUserID;
            this.commendImage = commendImage;
            this.commendType = commendType;
            this.commendTime = commendTime;
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
        ///推荐标题
        ///</summary>
        public string CommendTitle
        {
            get { return commendTitle; }
            set { commendTitle = value; }
        }

        ///<summary>
        ///推荐内容
        ///</summary>
        public string CommendContext
        {
            get { return commendContext; }
            set { commendContext = value; }
        }

        ///<summary>
        ///推荐地址
        ///</summary>
        public string CommendUrl
        {
            get { return commendUrl; }
            set { commendUrl = value; }
        }

        ///<summary>
        ///推荐用户ID
        ///</summary>
        public Guid CommendUserID
        {
            get { return commendUserID; }
            set { commendUserID = value; }
        }

        ///<summary>
        ///推荐图片
        ///</summary>
        public string CommendImage
        {
            get { return commendImage == string.Empty ? "" : "<img src='" + commendImage + "' />"; }
            set { commendImage = value; }
        }

        ///<summary>
        ///推荐类型
        ///</summary>
        public int CommendType
        {
            get { return commendType; }
            set { commendType = value; }
        }

        ///<summary>
        ///推荐时间
        ///</summary>
        public DateTime CommendTime
        {
            get { return commendTime; }
            set { commendTime = value; }
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
