using System;


namespace BDUModel
{
    /// <summary>
    ///LogCriticism业务实体
    /// </summary>
    [Serializable]
    public class LogCriticism
    {
        #region 字段定义

        ///<summary>
        ///
        ///</summary>
        private Guid iD;

        ///<summary>
        ///被评论的日志编号
        ///</summary>
        private Guid logID = Guid.Empty;

        ///<summary>
        ///发评论用户
        ///</summary>
        private int userID ;


        private string userName = string.Empty;

        ///<summary>
        ///用户头相
        ///</summary>
        private string userpic = String.Empty;

      

        ///<summary>
        ///评论内容
        ///</summary>
        private string criticismConten = String.Empty;

        ///<summary>
        ///评论时间
        ///</summary>
        private DateTime creatTime;


        #endregion

        #region 构造函数

        ///<summary>
        ///
        ///</summary>
        public LogCriticism()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public LogCriticism
        (
            Guid iD,
            Guid logID,
            int userID,
            string criticismConten,
            DateTime creatTime
        )
        {
            this.iD = iD;
            this.logID = logID;
            this.userID = userID;
            this.criticismConten = criticismConten;
            this.creatTime = creatTime;

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
        ///被评论的日志编号
        ///</summary>
        public Guid LogID
        {
            get { return logID; }
            set { logID = value; }
        }

        ///<summary>
        ///发评论用户
        ///</summary>
        public int UserID
        {
            get { return userID; }
            set { userID = value; }
        }

        ///<summary>
        ///评论内容
        ///</summary>
        public string CriticismConten
        {
            get { return criticismConten; }
            set { criticismConten = value; }
        }

        ///<summary>
        ///评论时间
        ///</summary>
        public DateTime CreatTime
        {
            get { return creatTime; }
            set { creatTime = value; }
        }

        /// <summary>
        /// 发评论的用户名称
        /// </summary>
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }


        ///<summary>
        ///用户头相
        ///</summary>
        public string Userpic
        {
            get { return userpic == string.Empty ? @"~\App_Themes\DefaultTheme\images\head.jpg" : userpic; }
            set { userpic = value; }
        }

        #endregion

    }
}
