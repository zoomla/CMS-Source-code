/*---------------------------------------------------------------
// Copyright (C) 2008  
// 版权所有
//
// 文件名： SystemLog.cs
// 文件功能描述：定义数据表SystemLog的业务实体
//
// 创建标识：Owner(2008-10-21) 
//
// 修改标识：
// 修改描述：
----------------------------------------------------------------*/
using System;


namespace BDUModel
{
    /// <summary>
    ///SystemLog业务实体
    /// </summary>
    [Serializable]
    public class SystemLog
    {
        #region 字段定义

        ///<summary>
        ///
        ///</summary>
        private Guid iD;

        ///<summary>
        ///用户编号
        ///</summary>
        private Guid userID = Guid.Empty;


        private string userName = string.Empty;

        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        ///<summary>
        ///内容编号
        ///</summary>
        private Guid contentID = Guid.Empty;


        private string contentName = string.Empty;

        /// <summary>
        /// 内容名称 
        /// </summary>
        public string ContentName
        {
            get { return contentName; }
            set { contentName = value; }
        }

        ///<summary>
        ///日志类型
        ///</summary>
        private string logType = String.Empty;

        ///<summary>
        ///链接地址
        ///</summary>
        private string modelUrl = String.Empty;

        ///<summary>
        ///创建时间
        ///</summary>
        private DateTime createTime;

        private string logContent = string.Empty;

    


        #endregion

        #region 构造函数

        ///<summary>
        ///
        ///</summary>
        public SystemLog()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public SystemLog
        (
            Guid iD,
            Guid userID,
            Guid contentID,
            string logType,
            string modelUrl,
            DateTime createTime
        )
        {
            this.iD = iD;
            this.userID = userID;
            this.contentID = contentID;
            this.logType = logType;
            this.modelUrl = modelUrl;
            this.createTime = createTime;

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
        public Guid UserID
        {
            get { return userID; }
            set { userID = value; }
        }

        ///<summary>
        ///内容编号
        ///</summary>
        public Guid ContentID
        {
            get { return contentID; }
            set { contentID = value; }
        }

        ///<summary>
        ///日志类型
        ///</summary>
        public string LogType
        {
            get { return logType; }
            set { logType = value; }
        }

        ///<summary>
        ///链接地址
        ///</summary>
        public string ModelUrl
        {
            get { return modelUrl; }
            set { modelUrl = value; }
        }

        ///<summary>
        ///创建时间
        ///</summary>
        public DateTime CreateTime
        {
            get { return createTime; }
            set { createTime = value; }
        }

        /// <summary>
        /// 日志内容
        /// </summary>
        public string LogContent
        {
            get { return logContent; }
            set { logContent = value; }
        }

        private string logTitle = string.Empty;


        /// <summary>
        /// 日志标题
        /// </summary>
        public string LogTitle
        {
            get { return logTitle; }
            set { logTitle = value; }
        }

        #endregion

    }
}
