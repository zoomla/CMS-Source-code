/*---------------------------------------------------------------
// Copyright (C) 2008  
// 版权所有
//
// 文件名： Memo.cs
// 文件功能描述：定义数据表Memo的业务实体
//
// 创建标识：Owner(2008-10-16) 
//
// 修改标识：
// 修改描述：
----------------------------------------------------------------*/
using System;


namespace BDUModel
{
    /// <summary>
    ///Memo业务实体
    /// </summary>
    [Serializable]
    public class UserMemo
    {
        #region 字段定义

        private Guid id = Guid.Empty;
        ///<summary>
        ///用户编号
        ///</summary>
        private int userID ;

        ///<summary>
        ///备忘录标题
        ///</summary>
        private string memoTitle = String.Empty;

        ///<summary>
        ///备忘录内容
        ///</summary>
        private string memoContext = String.Empty;

        ///<summary>
        ///备忘时间
        ///</summary>
        private DateTime memoTime;


        #endregion
        
        #region 构造函数

        ///<summary>
        ///
        ///</summary>
        public UserMemo()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public UserMemo
        (
            Guid id,
            int userID,
            string memoTitle,
            string memoContext,
            DateTime memoTime
        )
        {
            this.id = id;
            this.userID = userID;
            this.memoTitle = memoTitle;
            this.memoContext = memoContext;
            this.memoTime = memoTime;

        }

        #endregion

        #region 属性定义

        public Guid ID
        {
            get { return id; }
            set { id = value; }
        }

        ///<summary>
        ///用户编号
        ///</summary>
        public int UserID
        {
            get { return userID; }
            set { userID = value; }
        }

        ///<summary>
        ///备忘录标题
        ///</summary>
        public string MemoTitle
        {
            get { return memoTitle; }
            set { memoTitle = value; }
        }

        ///<summary>
        ///备忘录内容
        ///</summary>
        public string MemoContext
        {
            get { return memoContext; }
            set { memoContext = value; }
        }

        ///<summary>
        ///备忘时间
        ///</summary>
        public DateTime MemoTime
        {
            get { return memoTime; }
            set { memoTime = value; }
        }

        #endregion

    }
}
