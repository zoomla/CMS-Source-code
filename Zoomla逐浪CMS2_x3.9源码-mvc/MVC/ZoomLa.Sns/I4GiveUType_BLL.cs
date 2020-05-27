using System;
using System.Collections.Generic;
using System.Text;
using BDULogic;
using BDUModel;

namespace BDUBLL
{
    public class I4GiveUType_BLL
    {
        #region 添加忏悔类别
        /// <summary>
        /// 添加忏悔类别
        /// </summary>
        /// <param name="give"></param>
        public void Add(I4GiveUType give)
        {
            I4GiveUType_Logic.Add(give);

        }
        #endregion

        #region 修改忏悔类别
        /// <summary>
        /// 修改忏悔类别
        /// </summary>
        /// <param name="give"></param>
        public void Update(I4GiveUType give)
        {
            I4GiveUType_Logic.Update(give);
        }
        #endregion

        #region 删除忏悔类别
        /// <summary>
        /// 删除忏悔类别
        /// </summary>
        /// <param name="give"></param>
        public void Del(Guid id)
        {
            I4GiveUType_Logic.Del(id);
        }
        #endregion
    }
}
