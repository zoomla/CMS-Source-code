/*---------------------------------------------------------------
// Copyright (C) 2008  
// 版权所有
//
// 文件名： UserPresent.cs
// 文件功能描述：定义数据表UserPresent的业务实体
//
// 创建标识：Owner(2008-10-23) 
//
// 修改标识：
// 修改描述：
----------------------------------------------------------------*/
using System;


namespace BDUModel
{
    /// <summary>
    ///UserPresent业务实体
    /// </summary>
    [Serializable]
    public class UserPresent
    {
        #region 字段定义

        ///<summary>
        ///
        ///</summary>
        private Guid iD;

        ///<summary>
        ///送礼的人
        ///</summary>
        private Guid hostID = Guid.Empty;

        ///<summary>
        ///接收礼物的人
        ///</summary>
        private Guid friendID = Guid.Empty;

        ///<summary>
        ///礼物编号
        ///</summary>
        private Guid presentID = Guid.Empty;

        ///<summary>
        ///送礼时间
        ///</summary>
        private DateTime creatTime;

        ///<summary>
        ///是否匿名(0不是匿名1匿名)
        ///</summary>
        private int isAnonymity;

        private string leaveWord;

       
        #endregion

        #region 构造函数

        ///<summary>
        ///
        ///</summary>
        public UserPresent()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public UserPresent
        (
            Guid iD,
            Guid hostID,
            Guid friendID,
            Guid presentID,
            DateTime creatTime,
            int isAnonymity
        )
        {
            this.iD = iD;
            this.hostID = hostID;
            this.friendID = friendID;
            this.presentID = presentID;
            this.creatTime = creatTime;
            this.isAnonymity = isAnonymity;

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
        ///送礼的人
        ///</summary>
        public Guid HostID
        {
            get { return hostID; }
            set { hostID = value; }
        }

        ///<summary>
        ///接收礼物的人
        ///</summary>
        public Guid FriendID
        {
            get { return friendID; }
            set { friendID = value; }
        }

        ///<summary>
        ///礼物编号
        ///</summary>
        public Guid PresentID
        {
            get { return presentID; }
            set { presentID = value; }
        }

        ///<summary>
        ///送礼时间
        ///</summary>
        public DateTime CreatTime
        {
            get { return creatTime; }
            set { creatTime = value; }
        }

        ///<summary>
        ///是否匿名(0不是匿名1匿名)
        ///</summary>
        public int IsAnonymity
        {
            get { return isAnonymity; }
            set { isAnonymity = value; }
        }

        public string LeaveWord
        {
            get { return leaveWord; }
            set { leaveWord = value; }
        }

        #endregion

    }
}
