using System;
using System.Collections.Generic;
using System.Text;
using BDUModel;
using BDULogic;

namespace BDUBLL
{
    public class SystemUser_BLL
    {
        #region 添加系统管理账户
        /// <summary>
        /// 添加系统管理账户
        /// </summary>
        /// <param name="user"></param>
        public void Add(SystemUser user)
        {
            SystemUser_Logic.Add(user);
        }
        #endregion

        #region 修改密码
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="user"></param>
        public void Update(SystemUser user)
        {
            SystemUser_Logic.Update(user);
        }
        #endregion

        #region 根据用户名密码查询用户
        /// <summary>
        /// 根据用户名密码查询用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SystemUser GetUser(SystemUser user)
        {
            return SystemUser_Logic.GetUser(user);
        }
        #endregion

    }
}
