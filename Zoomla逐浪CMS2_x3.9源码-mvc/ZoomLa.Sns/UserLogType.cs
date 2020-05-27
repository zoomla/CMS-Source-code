using System;
using System.Collections.Generic;
using System.Text;

namespace BDUModel
{
    /// <summary>
    ///UserLogType业务实体
    /// </summary>
    [Serializable]
    public class UserLogType
    {
        #region 字段定义

        ///<summary>
        ///
        ///</summary>
        private Guid iD = Guid.Empty;

        ///<summary>
        ///日志类别名称
        ///</summary>
        private string logTypeName = String.Empty;

        ///<summary>
        ///创建时间
        ///</summary>
        private DateTime createTime;

        ///<summary>
        ///日志范围(0 所有人1好友2私有的3凭密码
        ///</summary>
        private int logArea;

        private int userID;


        private string logTypePWD;


        private int logCount;


        #endregion

        #region 构造函数

        ///<summary>
        ///
        ///</summary>
        public UserLogType()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public UserLogType
        (
            Guid iD,
            string logTypeName,
            DateTime createTime,
            int logArea
        )
        {
            this.iD = iD;
            this.logTypeName = logTypeName;
            this.createTime = createTime;
            this.logArea = logArea;

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
        ///日志类别名称
        ///</summary>
        public string LogTypeName
        {
            get { return logTypeName; }
            set { logTypeName = value; }
        }

        ///<summary>
        ///创建时间
        ///</summary>
        public DateTime CreateTime
        {
            get { return createTime; }
            set { createTime = value; }
        }

        ///<summary>
        ///日志范围(0 所有人1好友2私有的3凭密码
        ///</summary>
        public int LogArea
        {
            get { return logArea; }
            set { logArea = value; }
        }

        ///<summary>
        ///用户编号
        ///</summary>
        public int UserID
        {
            get { return userID; }
            set { userID = value; }
        }

        /// <summary>
        /// 日志密码
        /// </summary>
        public string LogTypePWD
        {
            get { return logTypePWD; }
            set { logTypePWD = value; }
        }



        /// <summary>
        /// 日志数
        /// </summary>
        public int LogCount
        {
            get { return logCount; }
            set { logCount = value; }
        }

        #endregion

    }
}
