/*---------------------------------------------------------------
// Copyright (C) 2008  
// 版权所有
//
// 文件名： ActiveJoin.cs
// 文件功能描述：定义数据表ActiveJoin的业务实体
//
// 创建标识：Owner(2008-10-17) 
//
// 修改标识：
// 修改描述：
----------------------------------------------------------------*/
using System;


namespace BDUModel
{
    /// <summary>
    ///ActiveJoin业务实体
    /// </summary>
    [Serializable]
    public class ActiveJoin
    {
        #region 字段定义

        ///<summary>
        ///编号
        ///</summary>
        private Guid iD = Guid.Empty;

        ///<summary>
        ///活动ID
        ///</summary>
        private Guid activeID = Guid.Empty;

        ///<summary>
        ///参与人ID
        ///</summary>
        private Guid userID = Guid.Empty;

        ///<summary>
        ///参与状态
        ///</summary>
        private int joinState;

        ///<summary>
        ///参与时间
        ///</summary>
        private DateTime joinAddtime;


        #endregion

        #region 构造函数

        ///<summary>
        ///
        ///</summary>
        public ActiveJoin()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public ActiveJoin
        (
            Guid iD,
            Guid activeID,
            Guid userID,
            int joinState,
            DateTime joinAddtime
        )
        {
            this.iD = iD;
            this.activeID = activeID;
            this.userID = userID;
            this.joinState = joinState;
            this.joinAddtime = joinAddtime;

        }

        #endregion

        #region 属性定义

        ///<summary>
        ///编号
        ///</summary>
        public Guid ID
        {
            get { return iD; }
            set { iD = value; }
        }

        ///<summary>
        ///活动ID
        ///</summary>
        public Guid ActiveID
        {
            get { return activeID; }
            set { activeID = value; }
        }

        ///<summary>
        ///参与人ID
        ///</summary>
        public Guid UserID
        {
            get { return userID; }
            set { userID = value; }
        }

        ///<summary>
        ///参与状态
        ///</summary>
        public int JoinState
        {
            get { return joinState; }
            set { joinState = value; }
        }

        ///<summary>
        ///参与时间
        ///</summary>
        public DateTime JoinAddtime
        {
            get { return joinAddtime; }
            set { joinAddtime = value; }
        }

        #endregion

    }
}
