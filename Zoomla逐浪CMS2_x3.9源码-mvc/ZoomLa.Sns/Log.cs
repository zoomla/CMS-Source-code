using System;
using System.Collections.Generic;
using System.Text;

namespace BDUModel
{
    /// <summary>
    ///Log业务实体
    /// </summary>
    [Serializable]
    public class Log
    {
        #region 字段定义

        ///<summary>
        ///
        ///</summary>
        private Guid iD;

        ///<summary>
        ///日志类型
        ///</summary>
        private int logType;

        ///<summary>
        ///日志内容
        ///</summary>
        private string logContext = String.Empty;

        ///<summary>
        ///记录日志时间
        ///</summary>
        private DateTime logTime;

        ///<summary>
        ///用户ID
        ///</summary>
        private Guid userID = Guid.Empty;


        #endregion

        #region 构造函数

        ///<summary>
        ///
        ///</summary>
        public Log()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public Log
        (
            Guid iD,
            int logType,
            string logContext,
            DateTime logTime,
            Guid userID
        )
        {
            this.iD = iD;
            this.logType = logType;
            this.logContext = logContext;
            this.logTime = logTime;
            this.userID = userID;

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
        ///日志类型
        ///</summary>
        public int LogType
        {
            get { return logType; }
            set { logType = value; }
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
        ///记录日志时间
        ///</summary>
        public DateTime LogTime
        {
            get { return logTime; }
            set { logTime = value; }
        }

        ///<summary>
        ///用户ID
        ///</summary>
        public Guid UserID
        {
            get { return userID; }
            set { userID = value; }
        }

        #endregion

    }
}
