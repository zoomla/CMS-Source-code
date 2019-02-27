using System;
using System.Collections.Generic;
using System.Text;

namespace BDUModel
{
    /// <summary>
    ///UserLog业务实体
    /// </summary>
    [Serializable]
    public class UserLog
    {
        #region 字段定义

        ///<summary>
        ///
        ///</summary>
        private Guid iD = Guid.Empty;

        ///<summary>
        ///用户编号
        ///</summary>
        private int userID ;

        ///<summary>
        ///日志内容
        ///</summary>
        private string logContext = String.Empty;

        ///<summary>
        ///日志创建时间
        ///</summary>
        private DateTime creatDate;

        ///<summary>
        ///最后修改时间
        ///</summary>
        private DateTime updateLogDate;

        ///<summary>
        ///所属类别
        ///</summary>
        private Guid logTypeID = Guid.Empty;

        private int logState;

        private string logTitle;

        private int readCount = 0;

        private int criticismCount = 0;

        

        #endregion

        #region 构造函数

        ///<summary>
        ///
        ///</summary>
        public UserLog()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public UserLog
        (
            Guid iD,
            int userID,
            string logContext,
            DateTime creatDate,
            DateTime updateLogDate,
            Guid logTypeID
        )
        {
            this.iD = iD;
            this.userID = userID;
            this.logContext = logContext;
            this.creatDate = creatDate;
            this.updateLogDate = updateLogDate;
            this.logTypeID = logTypeID;

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
        ///用户编号
        ///</summary>
        public int UserID
        {
            get { return userID; }
            set { userID = value; }
        }

        ///<summary>
        ///日志内容
        ///</summary>
        public string LogContext
        {
            get { return logContext; }
            set { logContext = value; }
        }

        ///<summary>
        ///日志创建时间
        ///</summary>
        public DateTime CreatDate
        {
            get { return creatDate; }
            set { creatDate = value; }
        }

        ///<summary>
        ///最后修改时间
        ///</summary>
        public DateTime UpdateLogDate
        {
            get { return updateLogDate; }
            set { updateLogDate = value; }
        }

        ///<summary>
        ///所属类别
        ///</summary>
        public Guid LogTypeID
        {
            get { return logTypeID; }
            set { logTypeID = value; }
        }

        /// <summary>
        /// 日志状态(0草稿1已经发布）
        /// </summary>
        public int LogState
        {
            get { return logState; }
            set { logState = value; }
        }

        /// <summary>
        /// 日志标题
        /// </summary>
        public string LogTitle
        {
            get { return logTitle; }
            set { logTitle = value; }
        }

        /// <summary>
        /// 阅读数
        /// </summary>
        public int ReadCount
        {
            get { return readCount; }
            set { readCount = value; }
        }

        /// <summary>
        /// 评论数
        /// </summary>
        public int CriticismCount
        {
            get { return criticismCount; }
            set { criticismCount = value; }
        }
        /// <summary>
        /// 地点
        /// </summary>
        public string Site { get; set; }
        /// <summary>
        /// 天气
        /// </summary>
        public int Weather { get; set; }
        /// <summary>
        /// 心情
        /// </summary>
        public int Mood { get; set; }
        #endregion

    }
}
