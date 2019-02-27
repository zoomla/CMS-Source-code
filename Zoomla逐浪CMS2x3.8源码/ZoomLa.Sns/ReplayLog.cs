using System;
using System.Collections.Generic;
using System.Text;

namespace BDUModel
{
    /// <summary>
    ///ReplayLog业务实体
    /// </summary>
    [Serializable]
    public class ReplayLog
    {
        #region 字段定义

        ///<summary>
        ///
        ///</summary>
        private Guid iD = Guid.Empty;

        ///<summary>
        ///回复的内容
        ///</summary>
        private string replayLpgContext = String.Empty;

        ///<summary>
        ///日志编号
        ///</summary>
        private Guid logID = Guid.Empty;

        ///<summary>
        ///回复者
        ///</summary>
        private Guid replayUserID = Guid.Empty;

        ///<summary>
        ///回复时间
        ///</summary>
        private DateTime replayLogTime;

        ///<summary>
        ///回复是否被看过
        ///</summary>
        private bool replayFlag;


        #endregion

        #region 构造函数

        ///<summary>
        ///
        ///</summary>
        public ReplayLog()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public ReplayLog
        (
            Guid iD,
            string replayLpgContext,
            Guid logID,
            Guid replayUserID,
            DateTime replayLogTime,
            bool replayFlag
        )
        {
            this.iD = iD;
            this.replayLpgContext = replayLpgContext;
            this.logID = logID;
            this.replayUserID = replayUserID;
            this.replayLogTime = replayLogTime;
            this.replayFlag = replayFlag;

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
        ///回复的内容
        ///</summary>
        public string ReplayLpgContext
        {
            get { return replayLpgContext; }
            set { replayLpgContext = value; }
        }

        ///<summary>
        ///日志编号
        ///</summary>
        public Guid LogID
        {
            get { return logID; }
            set { logID = value; }
        }

        ///<summary>
        ///回复者
        ///</summary>
        public Guid ReplayUserID
        {
            get { return replayUserID; }
            set { replayUserID = value; }
        }

        ///<summary>
        ///回复时间
        ///</summary>
        public DateTime ReplayLogTime
        {
            get { return replayLogTime; }
            set { replayLogTime = value; }
        }

        ///<summary>
        ///回复是否被看过
        ///</summary>
        public bool ReplayFlag
        {
            get { return replayFlag; }
            set { replayFlag = value; }
        }

        #endregion

    }
}
