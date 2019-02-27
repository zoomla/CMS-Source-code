using System;
using System.Collections.Generic;
using System.Text;
using BDULogic;
using BDUModel;

namespace BDUBLL
{
    public class Memo_BLL
    {
        #region 添加备忘录数据
        /// <summary>
        /// 添加备忘录数据
        /// </summary>
        /// <param name="memo"></param>
        public void Add(UserMemo memo)
        {
            Memo_Logic.Add(memo);
        }
        #endregion

        #region 修改备忘录
        /// <summary>
        /// 修改备忘录
        /// </summary>
        /// <param name="memo"></param>
        public void Update(UserMemo memo)
        {
            Memo_Logic.Update(memo);
        }
        #endregion

        #region 根据用户ID查询所有备忘录
        /// <summary>
        ///  根据用户ID查询所有备忘录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<UserMemo> GetMemoList(int id, PagePagination page)
        {
            return Memo_Logic.GetMemoList(id, page);
        }
        #endregion

        #region 根据ID查询单条备忘录
        /// <summary>
        /// 根据ID查询单条备忘录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UserMemo GetMemo(Guid id)
        {
            return Memo_Logic.GetMemo(id);
        }
        #endregion

        #region 删除备忘录
        /// <summary>
        /// 删除备忘录
        /// </summary>
        /// <param name="id"></param>
        public  void del(Guid id)
        {
            Memo_Logic.del(id);
        }
        #endregion

    }
}
