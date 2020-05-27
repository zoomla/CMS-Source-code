using System;
using System.Collections.Generic;
using System.Text;
using BDUModel;
using BDULogic;

namespace BDUBLL
{
    public class SystemBooDouChance_BLL
    {
        #region 添加布兜运气事件
        /// <summary>
        /// 添加布兜运气事件
        /// </summary>
        /// <param name="boodou"></param>
        public void Add(SystemBooDouChance boodou)
        {
            SystemBooDouChance_Logic.Add(boodou);
        }
        #endregion

        #region 修改布兜运气事件
        /// <summary>
        /// 修改布兜运气事件
        /// </summary>
        /// <param name="boodou"></param>
        public void Update(SystemBooDouChance boodou)
        {
            SystemBooDouChance_Logic.Update(boodou);
        }
        #endregion

        #region 删除布兜运气事件
        /// <summary>
        /// 删除布兜事件
        /// </summary>
        /// <param name="id"></param>
        public void Del(Guid id)
        {
            SystemBooDouChance_Logic.Del(id);
        }
        #endregion

        #region 查询单条布兜运气事件
        /// <summary>
        /// 查询单条布兜运气事件
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SystemBooDouChance GetBooDouChance(Guid id)
        {
            return SystemBooDouChance_Logic.GetBooDouChance(id);
        }
        #endregion

        #region 查询所有布兜运气事件
        /// <summary>
        /// 查询所有布兜运气事件
        /// </summary>
        /// <returns></returns>
        public List<SystemBooDouChance> GetAllBooDouChance()
        {
            return SystemBooDouChance_Logic.GetAllBooDouChance();
        }
        #endregion
    }
}
