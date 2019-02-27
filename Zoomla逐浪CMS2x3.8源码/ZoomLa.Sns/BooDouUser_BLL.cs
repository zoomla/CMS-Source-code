using System;
using System.Collections.Generic;
using System.Text;
using BDUModel;
using BDULogic;

namespace BDUBLL
{
    public class BooDouUser_BLL
    {
        #region 添加用户参与运气事件信息
        /// <summary>
        /// 添加用户参与运气事件信息
        /// </summary>
        /// <param name="bduser"></param>
        public void Add(BooDouUser bduser)
        {
            BooDouUser＿Logic.Add(bduser);
        }
        #endregion

        #region 根据用户ID查询这个用户最近一次使用布兜运气的时间
        /// <summary>
        /// 根据用户ID查询这个用户最近一次使用布兜运气的时间
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DateTime GetTime(Guid id)
        {
            return BooDouUser＿Logic.GetTime(id);
        }
        #endregion
    }
}
