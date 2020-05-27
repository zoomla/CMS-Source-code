using System;
using System.Collections.Generic;
using System.Text;
using BDUModel;
using BDULogic;

namespace BDUBLL
{
    public class I4GiveU_BLL
    {
        #region 添加忏悔信息
        /// <summary>
        /// 添加忏悔信息
        /// </summary>
        /// <param name="give"></param>
        public Guid  Add(I4GiveU give)
        {
            return I4GiveU_Logic.Add(give);
        }
        #endregion

        #region 查询单条忏悔信息
        /// <summary>
        /// 查询单条忏悔信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public I4GiveU GetOne(Guid id)
        {
            return I4GiveU_Logic.GetOne(id);
        }
        #endregion

        #region 查询用户的忏悔事件
        /// <summary>
        /// 查询用户的忏悔事件
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<I4GiveU> GetI4GiveUList(Guid id, PagePagination page)
        {
            return I4GiveU_Logic.GetI4GiveUList(id,page);
        }
        #endregion

        #region 根据用户查询好友里忏悔最多的人
        /// <summary>
        /// 根据用户查询好友里忏悔最多的人
        /// </summary>
        /// <param name="id"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public List<UserTable> GetConFess(Guid id, PagePagination page)
        {
            return I4GiveU_Logic.GetConFess(id, page);
        }
        #endregion

         #region 用户的好友里最新忏悔的人
        /// <summary>
        /// 用户的好友里最新忏悔的人
        /// </summary>
        /// <param name="id"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public List<UserTable> GetFriendNewConfess(Guid id, PagePagination page)
        {
            return I4GiveU_Logic.GetFriendNewConfess(id, page);
        }
        #endregion
    }
}
