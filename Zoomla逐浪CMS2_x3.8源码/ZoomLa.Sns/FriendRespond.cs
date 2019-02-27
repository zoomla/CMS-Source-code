/*---------------------------------------------------------------
// Copyright (C) 2008  
// 版权所有
//
// 文件名： FriendRespond.cs
// 文件功能描述：定义数据表FriendRespond的业务实体
//
// 创建标识：Owner(2008-10-22) 
//
// 修改标识：
// 修改描述：
----------------------------------------------------------------*/
using System;


namespace BDUModel
{
    /// <summary>
    ///FriendRespond业务实体
    /// </summary>
    [Serializable]
    public class FriendRespond
    {
        #region 字段定义

        ///<summary>
        ///
        ///</summary>
        private Guid iD;

        ///<summary>
        ///问题编号
        ///</summary>
        private Guid qusetionID = Guid.Empty;

        ///<summary>
        ///答案编号
        ///</summary>
        private Guid answerID = Guid.Empty;

        ///<summary>
        ///回答时间
        ///</summary>
        private DateTime creatTime;

        ///<summary>
        ///回答的人
        ///</summary>
        private Guid friendID = Guid.Empty;


        #endregion

        #region 构造函数

        ///<summary>
        ///
        ///</summary>
        public FriendRespond()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public FriendRespond
        (
            Guid iD,
            Guid qusetionID,
            Guid answerID,
            DateTime creatTime,
            Guid friendID
        )
        {
            this.iD = iD;
            this.qusetionID = qusetionID;
            this.answerID = answerID;
            this.creatTime = creatTime;
            this.friendID = friendID;

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
        ///问题编号
        ///</summary>
        public Guid QusetionID
        {
            get { return qusetionID; }
            set { qusetionID = value; }
        }

        ///<summary>
        ///答案编号
        ///</summary>
        public Guid AnswerID
        {
            get { return answerID; }
            set { answerID = value; }
        }

        ///<summary>
        ///回答时间
        ///</summary>
        public DateTime CreatTime
        {
            get { return creatTime; }
            set { creatTime = value; }
        }

        ///<summary>
        ///回答的人
        ///</summary>
        public Guid FriendID
        {
            get { return friendID; }
            set { friendID = value; }
        }

        #endregion

    }
}
