/*---------------------------------------------------------------
// Copyright (C) 2008  
// 版权所有
//
// 文件名： UserTable.cs
// 文件功能描述：定义数据表UserTable的业务实体
//
// 创建标识：Owner(2008-10-13) 
//
// 修改标识：
// 修改描述：
----------------------------------------------------------------*/
using System;


namespace BDUModel
{
    /// <summary>
    ///UserTable业务实体
    /// </summary>
    [Serializable]
    public class UserTable
    {
        #region 字段定义

        ///<summary>
        ///ID
        ///</summary>
        private Guid userID;

        ///<summary>
        ///用户名称
        ///</summary>
        private string userName = String.Empty;

        ///<summary>
        ///用户密码
        ///</summary>
        private string userpwd = String.Empty;

        ///<summary>
        ///用户真实姓名
        ///</summary>
        private string userRealname = String.Empty;

        ///<summary>
        ///年龄
        ///</summary>
        private string userage = String.Empty;

        ///<summary>
        ///地址
        ///</summary>
        private string useraddr = String.Empty;

        ///<summary>
        ///邮箱
        ///</summary>
        private string usermail = String.Empty;

        ///<summary>
        ///联系方式
        ///</summary>
        private string userphone = String.Empty;

        ///<summary>
        ///性别
        ///</summary>
        private string usersex = String.Empty;

        ///<summary>
        ///用户头相
        ///</summary>
        private string userpic = String.Empty;

        ///<summary>
        ///兴趣爱好
        ///</summary>
        private string userlike = String.Empty;

        ///<summary>
        ///用户状态
        ///</summary>
        private int userstate;

        ///<summary>
        ///注册时间
        ///</summary>
        private DateTime createDate;

        /// <summary>
        /// 用户生日日期
        /// </summary>
        private DateTime userBirthday;

        /// <summary>
        /// 用户最后一次登录时间
        /// </summary>
        private DateTime lastLogicTime;

        #endregion

        #region 构造函数

        ///<summary>
        ///
        ///</summary>
        public UserTable()
        {
        }

        

        #endregion

        #region 属性定义

        ///<summary>
        ///ID
        ///</summary>
        public Guid UserID
        {
            get { return userID; }
            set { userID = value; }
        }

        ///<summary>
        ///用户名称
        ///</summary>
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        ///<summary>
        ///用户密码
        ///</summary>
        public string Userpwd
        {
            get { return userpwd; }
            set { userpwd = value; }
        }

        ///<summary>
        ///用户真实姓名
        ///</summary>
        public string UserRealname
        {
            get { return userRealname; }
            set { userRealname = value; }
        }

        ///<summary>
        ///年龄
        ///</summary>
        public string Userage
        {
            get { return userage; }
            set { userage = value; }
        }

        ///<summary>
        ///地址
        ///</summary>
        public string Useraddr
        {
            get { return useraddr; }
            set { useraddr = value; }
        }

        ///<summary>
        ///邮箱
        ///</summary>
        public string Usermail
        {
            get { return usermail; }
            set { usermail = value; }
        }

        ///<summary>
        ///联系方式
        ///</summary>
        public string Userphone
        {
            get { return userphone; }
            set { userphone = value; }
        }

        ///<summary>
        ///性别
        ///</summary>
        public string Usersex
        {
            get { return usersex; }
            set { usersex = value; }
        }

        ///<summary>
        ///用户头相
        ///</summary>
        public string Userpic
        {
            get { return string.IsNullOrEmpty(userpic) ? @"~\Images/userface/noface.png" : userpic; }
            set { userpic = value; }
        }

        ///<summary>
        ///兴趣爱好
        ///</summary>
        public string Userlike
        {
            get { return userlike; }
            set { userlike = value; }
        }

        ///<summary>
        ///用户状态
        ///</summary>
        public int Userstate
        {
            get { return userstate; }
            set { userstate = value; }
        }

        ///<summary>
        ///注册时间
        ///</summary>
        public DateTime CreateDate
        {
            get { return createDate; }
            set { createDate = value; }
        }

        /// <summary>
        /// 用户生日日期
        /// </summary>
        public DateTime UserBirthday
        {
            get { return userBirthday; }
            set { userBirthday = value; }
        }


        /// <summary>
        /// 用户最后一次登录时间
        /// </summary>
        public DateTime LastLogicTime
        {
            get { return lastLogicTime; }
            set { lastLogicTime = value; }
        }
        #endregion
    }
}
