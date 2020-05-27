/*---------------------------------------------------------------
// Copyright (C) 2008  
// 版权所有
//
// 文件名： SystemUser.cs
// 文件功能描述：定义数据表SystemUser的业务实体
//
// 创建标识：Owner(2008-10-15) 
//
// 修改标识：
// 修改描述：
----------------------------------------------------------------*/
using System;


namespace BDUModel
{
    /// <summary>
    ///SystemUser业务实体
    /// </summary>
    [Serializable]
    public class SystemUser
    {
        #region 字段定义

        ///<summary>
        ///
        ///</summary>
        private Guid iD;

        ///<summary>
        ///用户名
        ///</summary>
        private string userName = String.Empty;

        ///<summary>
        ///密码
        ///</summary>
        private string userPwd = String.Empty;


        #endregion

        #region 构造函数

        ///<summary>
        ///
        ///</summary>
        public SystemUser()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public SystemUser
        (
            Guid iD,
            string userName,
            string userPwd
        )
        {
            this.iD = iD;
            this.userName = userName;
            this.userPwd = userPwd;

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
        ///用户名
        ///</summary>
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        ///<summary>
        ///密码
        ///</summary>
        public string UserPwd
        {
            get { return userPwd; }
            set { userPwd = value; }
        }

        #endregion

    }
}
