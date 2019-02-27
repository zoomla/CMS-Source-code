using System;
using System.Collections.Generic;
using System.Text;
using BDUModel;
using BDULogic;

namespace BDUBLL
{
    public class CommendTable_BLL
    {
        #region 添加推荐信息
        /// <summary>
        /// 添加推荐信息
        /// </summary>
        /// <param name="ct"></param>
        public void Add(CommendTable ct)
        {
            CommendTable_Logic.Add(ct);
        }
        #endregion

        #region 删除推荐信息
        /// <summary>
        /// 删除推荐信息
        /// </summary>
        /// <param name="id"></param>
        public void Del(Guid id)
        {
            CommendTable_Logic.Del(id);
        }
        #endregion

        #region 根据用户ID查询推荐信息
        /// <summary>
        /// 根据用户ID查询推荐信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<CommendTable> GetUserCommendTable(Guid id, PagePagination page,CommendTableType ctt)
        {
            return CommendTable_Logic.GetUserCommendTable(id, page,ctt);
        }
        #endregion

        #region 根据ID查询单条推荐信息
        /// <summary>
        /// 根据ID查询单条推荐信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CommendTable GetCommendTable(Guid id)
        {
            return CommendTable_Logic.GetCommendTable(id);
        }
        #endregion
    }
}
