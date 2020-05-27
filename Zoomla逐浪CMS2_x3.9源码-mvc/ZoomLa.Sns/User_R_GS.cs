/*---------------------------------------------------------------
// Copyright (C) 2008  
// 版权所有
//
// 文件名： User_R_GS.cs
// 文件功能描述：定义数据表User_R_GS的业务实体
//
// 创建标识：Owner(2008-10-11) 
//
// 修改标识：
// 修改描述：
----------------------------------------------------------------*/
using System;


namespace BDUModel
{
    /// <summary>
    ///User_R_GS业务实体
    /// </summary>
    [Serializable]
    public class User_R_GS
    {
        #region 字段定义

        ///<summary>
        ///
        ///</summary>
        private Guid iD;

        ///<summary>
        ///用户编号
        ///</summary>
        private int userID ;

        ///<summary>
        ///所属群族编号
        ///</summary>
        private Guid gSID = Guid.Empty;

        ///<summary>
        ///加入时间
        ///</summary>
        private DateTime createTime;

        private string userName = string.Empty;

        ///<summary>
        ///用户头相
        ///</summary>
        private string userpic = String.Empty;


        private int  isCreator;


       
        #endregion

        #region 构造函数

        ///<summary>
        ///
        ///</summary>
        public User_R_GS()
        {
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
        public int UserID
        {
            get { return userID; }
            set { userID = value; }
        }

        ///<summary>
        ///所属群族编号
        ///</summary>
        public Guid GSID
        {
            get { return gSID; }
            set { gSID = value; }
        }

        ///<summary>
        ///加入时间
        ///</summary>
        public DateTime CreateTime
        {
            get { return createTime; }
            set { createTime = value; }
        }

        /// <summary>
        /// 用户名称
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
            get { return string.IsNullOrEmpty(userpic) ? @"/Images/userface/noface.png" : userpic; }
            set { userpic = value; }
        }

        /// <summary>
        /// 是否是群主
        /// </summary>
        public int  IsCreator
        {
            get { 
                
                return isCreator; 
            }
            set { isCreator = value; }
        }

        private DateTime lastCallTime;

        /// <summary>
        /// 最近访问时间
        /// </summary>
        public DateTime LastCallTime
        {
            get
            {
                return lastCallTime;
            }
            set
            {
                lastCallTime = value;
            }
        }

        private string useraddr;
        ///<summary>
        ///地址
        ///</summary>
        public string Useraddr
        {
            get { return useraddr; }
            set { useraddr = value; }
        }


        public string  IsMy
        {
            get
            {
                if (IsCreator == UserID)
                    return "创建者";
                else return string.Empty;
            }
        }

        #endregion

    }
}
