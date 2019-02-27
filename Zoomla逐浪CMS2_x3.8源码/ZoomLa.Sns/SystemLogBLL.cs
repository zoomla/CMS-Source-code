using System;
using System.Collections.Generic;
using System.Text;
using BDULogic;
using BDUModel;

namespace BDUBLL
{
    public class SystemLogBLL
    {
        #region 创建日志
        /// <summary>
        /// 创建日志
        /// </summary>
        /// <param name="fortune"></param>
        public Guid AddSystemLog(SystemLog log)
        {
            return SystemLogLogic.AddSystemLog(log);
        }
        #endregion

        #region 获取用户动态
        /// <summary>
        /// 获取用户动态
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<SystemLog> GetUserDynamic(Guid userID, PagePagination page)
        {
            return SystemLogLogic.GetUserDynamic(userID, page);
        }
        #endregion

        #region 获取好友动态
        /// <summary>
        /// 获取好友动态
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<SystemLog> GetFriendDynamic(Guid userID, string topnum)
        {
            return SystemLogLogic.GetFriendDynamic(userID,topnum);
        }
        #endregion

        #region 获取流量日志
        /// <summary>
        /// 获取流量日志
        /// </summary>
        /// <param name="logtype">流量类型</param>
        /// <param name="page">显示的数量</param>
        /// <returns></returns>
        public List<SystemLog> GetFluxLog(SystemLogType logtype, PagePagination page)
        {
            return SystemLogLogic.GetFluxLog(logtype, page);
        }
        #endregion

    }
}
