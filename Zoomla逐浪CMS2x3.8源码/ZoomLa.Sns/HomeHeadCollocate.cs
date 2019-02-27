/*---------------------------------------------------------------
// Copyright (C) 2008  
// 版权所有
//
// 文件名： HomeHeadCollocate.cs
// 文件功能描述：定义数据表HomeHeadCollocate的业务实体
//
// 创建标识：Owner(2008-11-13) 
//
// 修改标识：
// 修改描述：
----------------------------------------------------------------*/
using System;


namespace FHModel
{
    /// <summary>
    ///HomeHeadCollocate业务实体
    /// </summary>
    [Serializable]
    public class HomeHeadCollocate
    {
        #region 字段定义

        ///<summary>
        ///用户ID
        ///</summary>
        private int userID;

        ///<summary>
        ///头像图片
        ///</summary>
        private string userHeadPic = String.Empty;

        ///<summary>
        ///左边
        ///</summary>
        private int userLeft;

        ///<summary>
        ///层数
        ///</summary>
        private int userIndexZ;

        ///<summary>
        ///上边
        ///</summary>
        private int userTop;

        ///<summary>
        ///同居ID
        ///</summary>
        private Guid cohabitID = Guid.Empty;

        ///<summary>
        ///同居头像图片
        ///</summary>
        private string cohabitHeadPic = String.Empty;

        ///<summary>
        ///同居左边
        ///</summary>
        private int cohabitLeft;

        ///<summary>
        ///同居层数
        ///</summary>
        private int cohabitIndexZ;

        ///<summary>
        ///同居上边
        ///</summary>
        private int cohabitTop;


        #endregion

        #region 构造函数

        ///<summary>
        ///
        ///</summary>
        public HomeHeadCollocate()
        {
        }

        

        #endregion

        #region 属性定义

        ///<summary>
        ///用户ID
        ///</summary>
        public int UserID
        {
            get { return userID; }
            set { userID = value; }
        }

        ///<summary>
        ///头像图片
        ///</summary>
        public string UserHeadPic
        {
            get { return userHeadPic; }
            set { userHeadPic = value; }
        }

        ///<summary>
        ///左边
        ///</summary>
        public int UserLeft
        {
            get { return userLeft; }
            set { userLeft = value; }
        }

        ///<summary>
        ///层数
        ///</summary>
        public int UserIndexZ
        {
            get { return userIndexZ; }
            set { userIndexZ = value; }
        }

        ///<summary>
        ///上边
        ///</summary>
        public int UserTop
        {
            get { return userTop; }
            set { userTop = value; }
        }

        ///<summary>
        ///同居ID
        ///</summary>
        public Guid CohabitID
        {
            get { return cohabitID; }
            set { cohabitID = value; }
        }

        ///<summary>
        ///同居头像图片
        ///</summary>
        public string CohabitHeadPic
        {
            get { return cohabitHeadPic; }
            set { cohabitHeadPic = value; }
        }

        ///<summary>
        ///同居左边
        ///</summary>
        public int CohabitLeft
        {
            get { return cohabitLeft; }
            set { cohabitLeft = value; }
        }

        ///<summary>
        ///同居层数
        ///</summary>
        public int CohabitIndexZ
        {
            get { return cohabitIndexZ; }
            set { cohabitIndexZ = value; }
        }

        ///<summary>
        ///同居上边
        ///</summary>
        public int CohabitTop
        {
            get { return cohabitTop; }
            set { cohabitTop = value; }
        }

        #endregion

    }
}
