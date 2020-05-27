using System;
using System.Collections.Generic;
using System.Text;

namespace BDUModel
{
    /// <summary>
    ///Logҵ��ʵ��
    /// </summary>
    [Serializable]
    public class Log
    {
        #region �ֶζ���

        ///<summary>
        ///
        ///</summary>
        private Guid iD;

        ///<summary>
        ///��־����
        ///</summary>
        private int logType;

        ///<summary>
        ///��־����
        ///</summary>
        private string logContext = String.Empty;

        ///<summary>
        ///��¼��־ʱ��
        ///</summary>
        private DateTime logTime;

        ///<summary>
        ///�û�ID
        ///</summary>
        private Guid userID = Guid.Empty;


        #endregion

        #region ���캯��

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
        ///��־����
        ///</summary>
        public int LogType
        {
            get { return logType; }
            set { logType = value; }
        }

        ///<summary>
        ///��־����
        ///</summary>
        public string LogContext
        {
            get { return logContext; }
            set { logContext = value; }
        }

        ///<summary>
        ///��¼��־ʱ��
        ///</summary>
        public DateTime LogTime
        {
            get { return logTime; }
            set { logTime = value; }
        }

        ///<summary>
        ///�û�ID
        ///</summary>
        public Guid UserID
        {
            get { return userID; }
            set { userID = value; }
        }

        #endregion

    }
}
