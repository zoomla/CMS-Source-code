using System;
using System.Collections.Generic;
using System.Text;
using BDULogic;
using BDUModel;
using System.Data;

namespace BDUBLL
{
    public class LogManageBLL
    {
        #region 创建日志类别
        /// <summary>
        /// 创建日志类别
        /// </summary>
        /// <param name="logType"></param>
        /// <returns></returns>
        public Guid CreatLogType(UserLogType logType)
        {
            return LogManageLogic.CreatLogType(logType);
        }
        #endregion

        #region 用户编号获取日志类型
        /// <summary>
        /// 用户编号获取日志类型
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<UserLogType> GetLogTypeByUserID(int userID)
        {
            return LogManageLogic.GetLogTypeByUserID(userID);
        }
        #endregion

        #region 删除日志类型
        /// <summary>
        /// 删除日志类型
        /// </summary>
        /// <param name="id"></param>
        public void DeleteLogType(Guid id)
        {
            LogManageLogic.DeleteLogType(id);
        }
        #endregion

        #region 修改日志类型
        /// <summary>
        /// 修改日志类型
        /// </summary>
        /// <param name="logType"></param>
        public void UpdateLogType(UserLogType logType)
        {
            LogManageLogic.UpdateLogType(logType);
        }
        #endregion

        #region 通过日志类型编号获取日志
        /// <summary>
        /// 通过日志类型编号获取日志
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UserLogType GetLogTypeByID(Guid id)
        {
            return LogManageLogic.GetLogTypeByID(id);
        }
        #endregion

        #region 创建一篇日志
        /// <summary>
        /// 创建一篇日志
        /// </summary>
        /// <param name="userLog"></param>
        /// <returns></returns>
        public Guid CreatLog(UserLog userLog)
        {
            return LogManageLogic.CreatLog(userLog);
        }
        #endregion

        #region 通过用户名获取自己的日志
        /// <summary>
        ///通过用户名获取自己的日志 
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<UserLog> GetSelfUserLogByUserID(int userID, int state, Guid logTypeID, DateTime creatDate, BDUModel.PagePagination page)
        {
            return LogManageLogic.GetSelfUserLogByUserID(userID, state, logTypeID, creatDate, page);
        }
        #endregion
        /// <summary>
        /// 通过用户ID获取用户日志
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public List<UserLog> GetLogByUserID(int UserID) {
            return LogManageLogic.GetLogByUserID(UserID);
        }
        #region 读取所有日志
        /// <summary>
        /// 读取所有日志
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public List<UserLog> GetAllLog(BDUModel.PagePagination page)
        {
            return LogManageLogic.GetAllLog(page);
        }
        #endregion
        /// <summary>
        /// 根据UserID读取日志
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public DataTable GetAllLogs(int UserID) {
            return LogManageLogic.GetAllLogs(UserID);
        }

        #region 通过日志类别获取日志
        /// <summary>
        /// 通过日志类别获取日志
        /// </summary>
        /// <param name="logTypeID"></param>
        /// <returns></returns>
        public List<UserLog> GetUserLogByLogTypeID(Guid logTypeID,int UserID)
        {
            return LogManageLogic.GetUserLogByLogTypeID(logTypeID, UserID);
        }
        #endregion

        #region 通过日期获取日志
        /// <summary>
        /// 通过日期获取日志
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<UserLog> GetUserLogByData(int userID)
        {
            return LogManageLogic.GetUserLogByData(userID);
        }
        #endregion

        #region 通过日志编号获取日志
        /// <summary>
        ///通过日志编号获取日志 
        /// </summary>
        /// <param name="logID"></param>
        /// <returns></returns>
        public UserLog GetUserLogByID(Guid logID)
        {
            return LogManageLogic.GetUserLogByID(logID);
        }
        #endregion

        #region 修改日志
        /// <summary>
        /// 修改日志
        /// </summary>
        /// <param name="userLog"></param>
        public void UpdataLog(UserLog userLog)
        {
            LogManageLogic.UpdataLog(userLog);
        }
        public void UpdataReadCount(Guid logid, int readCount)
        {
            LogManageLogic.UpdataReadCount(logid, readCount);
        }
        #endregion

        #region 通过日志编号删除日志
        /// <summary>
        /// 通过日志编号删除日志
        /// </summary>
        /// <param name="logID"></param>
        public void DeleteLogByID(Guid logID)
        {
            LogManageLogic.DeleteLogByID(logID);
        }
        #endregion

        #region 获取日志的评论
        /// <summary>
        ///获取日志的评论 
        /// </summary>
        /// <param name="logID"></param>
        /// <returns></returns>
        public List<LogCriticism> GetLogCriticismByLogID(Guid logID, PagePagination page)
        {
           return LogManageLogic.GetLogCriticismByLogID(logID, page);
        }
        #endregion

        #region 创建评论
        /// <summary>
        /// 创建评论
        /// </summary>
        /// <param name="logCriticism"></param>
        /// <returns></returns>
        public Guid CreatLogCriticism(LogCriticism logCriticism)
        {
            return LogManageLogic.CreatLogCriticism(logCriticism);
        }
        #endregion
        
        #region 删除评论
        /// <summary>
        /// 删除评论
        /// </summary>
        /// <param name="criticimeID"></param>
        public  void DeleteCriticism(Guid criticimeID)
        {
            LogManageLogic.DeleteCriticism(criticimeID);
        }
        #endregion

        #region 通过用户自定义条件搜索日志
        /// <summary>
        /// 通过用户自定义条件搜索日志
        /// </summary>
        /// <param name="whereEx"></param>
        /// <returns></returns>
        public DataTable GetLogMessageByc(string whereEx)
        {
            return LogManageLogic.GetLogMessageByc(whereEx);
        }
        #endregion
        #region 通过用户名搜索日志
        /// <summary>
        /// 通过用户名搜索日志
        /// </summary>
        /// <param name="whereEx"></param>
        /// <returns></returns>
        public DataTable GetLogMessageByN(string whereEx)
        {
            return LogManageLogic.GetLogMessageByN(whereEx);
        }

        #endregion
    }
}
