using System;
using System.Collections.Generic;
using System.Text;
using BDUModel;
using BDULogic;

namespace BDUBLL
{
    public class I4GiveUForgive_BLL
    {
        #region 添加原谅信息
        /// <summary>
        /// 添加原谅信息
        /// </summary>
        /// <param name="forgive"></param>
        public void Add(I4GiveUForgive forgive)
        {
            I4GiveUForgive_Logic.Add(forgive);
        }
        #endregion

        #region 查询用户原谅的次数
        /// <summary>
        /// 查询用户原谅的次数
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int GetForgiveCount(Guid id)
        {
            return I4GiveUForgive_Logic.GetForgiveCount(id);
        }
        #endregion

        #region 查询用户的好友最宽容的人
        /// <summary>
        /// 查询用户的好友最宽容的人
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<UserTable> GetFriendForgiveOrder(Guid id,PagePagination page)
        {
            return I4GiveUForgive_Logic.GetFriendForgiveOrder(id,page);
        }
        #endregion
    }
}
