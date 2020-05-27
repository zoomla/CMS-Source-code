using System;
using System.Collections.Generic;
using System.Text;

namespace BDUModel
{
    /// <summary>
    ///LogTypePWD业务实体
    /// </summary>
    [Serializable]
    public class LogTypePWD
    {
        #region 字段定义

        ///<summary>
        ///
        ///</summary>
        private Guid iD = Guid.Empty;

        ///<summary>
        ///密码
        ///</summary>
        private string pWD = String.Empty;

        ///<summary>
        ///日志类别编号
        ///</summary>
        private string logTypeID = String.Empty;


        #endregion

        #region 构造函数

        ///<summary>
        ///
        ///</summary>
        public LogTypePWD()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public LogTypePWD
        (
            Guid iD,
            string pWD,
            string logTypeID
        )
        {
            this.iD = iD;
            this.pWD = pWD;
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
        ///密码
        ///</summary>
        public string PWD
        {
            get { return pWD; }
            set { pWD = value; }
        }

        ///<summary>
        ///日志类别编号
        ///</summary>
        public string LogTypeID
        {
            get { return logTypeID; }
            set { logTypeID = value; }
        }

        #endregion

    }
}
