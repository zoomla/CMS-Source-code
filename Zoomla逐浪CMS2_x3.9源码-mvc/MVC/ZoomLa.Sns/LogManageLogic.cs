using System;
using System.Collections.Generic;
using System.Text;
using BDUModel;
using System.Data.SqlClient;
using System.Data;
using ZoomLa.SQLDAL;

namespace BDULogic
{
    public class LogManageLogic
    {
        #region 创建日志类别
        /// <summary>
        /// 创建日志类别
        /// </summary>
        /// <param name="logType"></param>
        /// <returns></returns>
        public static Guid CreatLogType(UserLogType logType)
        {
            try
            {
                logType.ID = Guid.NewGuid();
                string sqlCreatLogType = @"INSERT INTO [ZL_Sns_UserLogType]
           ([ID],[LogTypeName],[CreateTime],[LogArea],[UserID],[LogTypePWD])  VALUES (@ID,@LogTypeName,@CreateTime,@LogArea,@UserID,@LogTypePWD)";
                SqlParameter[] parameter ={
                new SqlParameter("ID",logType.ID),
                new SqlParameter("LogTypeName",logType.LogTypeName),
                new SqlParameter("CreateTime",DateTime.Now),
                new SqlParameter("LogArea",logType.LogArea),
                new SqlParameter("UserID",logType.UserID),
                new SqlParameter("LogTypePWD",logType.LogTypePWD==null ? string.Empty:logType.LogTypePWD)
            };
                SqlHelper.ExecuteNonQuery(CommandType.Text, sqlCreatLogType, parameter);
                return logType.ID;
            }
            catch
            {
                throw;
            }

        }
        #endregion

        #region 用户编号获取日志类型
        /// <summary>
        /// 用户编号获取日志类型
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static List<UserLogType> GetLogTypeByUserID(int userID)
        {
            try
            {
                string sqlGetLogTypeByUserID = @"select * from ZL_Sns_UserLogType where UserID=@UserID";
                SqlParameter[] parameter ={
                new SqlParameter("UserID",userID)};
                List<UserLogType> list = new List<UserLogType>();
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sqlGetLogTypeByUserID, parameter))
                {
                    while (dr.Read())
                    {
                        UserLogType logType = new UserLogType();
                        ReadUserType(dr, logType);
                        list.Add(logType);
                    }
                }
                return list;

            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 删除日志类型
        /// <summary>
        /// 删除日志类型
        /// </summary>
        /// <param name="id"></param>
        public static void DeleteLogType(Guid id)
        {
            try
            {
                string sqlDeleteLogType = @"delete ZL_Sns_UserLogType where ID=@ID";
                SqlParameter[] parameter ={
                new SqlParameter("ID",id)};
                SqlHelper.ExecuteNonQuery(CommandType.Text, sqlDeleteLogType, parameter);
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 通过日志类型编号获取日志类型
        /// <summary>
        /// 通过日志类型编号获取日志类型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static UserLogType GetLogTypeByID(Guid id)
        {
            try
            {
                string sqlGetLogTypeByID = @"select * from ZL_Sns_UserLogType where ID=@ID";
                UserLogType logType = new UserLogType();
                SqlParameter[] parameter ={ new SqlParameter("ID", id) };
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sqlGetLogTypeByID, parameter))
                {
                    if (dr.Read())
                    {
                        ReadUserType(dr, logType);
                    }
                }
                return logType;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 修改日志类型
        /// <summary>
        /// 修改日志类型
        /// </summary>
        /// <param name="logType"></param>
        public static void UpdateLogType(UserLogType logType)
        {
            try
            {
                string sqlUpdateLogType = @"UPDATE [ZL_Sns_UserLogType]  SET LogTypeName=@LogTypeName,[LogArea]=@LogArea,[LogTypePWD]=@LogTypePWD where ID=@ID";
                SqlParameter[] parameter ={
                        new SqlParameter("ID",logType.ID),
                        new SqlParameter("LogTypeName",logType.LogTypeName),
                        new SqlParameter("LogArea",logType.LogArea),
                        new SqlParameter("LogTypePWD",logType.LogTypePWD==null ? string.Empty:logType.LogTypePWD)
                    };
                SqlHelper.ExecuteNonQuery(CommandType.Text, sqlUpdateLogType, parameter);
            }
            catch
            { throw; }
        }
        #endregion

        #region 创建一篇日志
        /// <summary>
        /// 创建一篇日志
        /// </summary>
        /// <param name="userLog"></param>
        /// <returns></returns>
        public static Guid CreatLog(UserLog userLog)
        {
            try
            {
                userLog.ID = Guid.NewGuid();
                string sqlCreatLog = @"INSERT INTO [ZL_Sns_UserLog]([ID],[UserID],[LogContext],[CreatDate],[LogTypeID],[LogState],[LogTitle])  VALUES (@ID,@UserID,@LogContext,@CreatDate,@LogTypeID,@LogState,@LogTitle)";
                //System.Web.HttpContext.Current.Response.Write(sqlCreatLog);
                //System.Web.HttpContext.Current.Response.End();
                SqlParameter[] parameter ={
                new SqlParameter("ID",userLog.ID),
                new SqlParameter("UserID",userLog.UserID),
                new SqlParameter("LogContext",userLog.LogContext),
                new SqlParameter("CreatDate",DateTime.Now),
                new SqlParameter("LogTypeID",userLog.LogTypeID),
                new SqlParameter("LogState",userLog.LogState),
                new SqlParameter("LogTitle",userLog.LogTitle)
            };
               SqlHelper.ExecuteNonQuery(CommandType.Text, sqlCreatLog, parameter);
                return userLog.ID;
            }
            catch
            {
                throw;
            }
            
        }
        #endregion

        #region 读取所有日志
        /// <summary>
        /// 读取所有日志
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public static List<UserLog> GetAllLog(BDUModel.PagePagination page)
        {
            try
            {
                string sql = @"select * from ZL_Sns_UserLog where LogState=1";
                if (page != null)
                {
                    sql = page.PaginationSql(sql);
                }

                List<UserLog> list = new List<UserLog>();
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sql, null))
                {
                    while (dr.Read())
                    {
                        UserLog log = new UserLog();
                        ReadUserLog(dr, log);
                        list.Add(log);
                    }
                }
                return list;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        public static DataTable GetAllLogs(int UserID)
        {
            string sqlStr = "select * from ZL_Sns_UserLog where UserID=@UserID";
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@UserName", SqlDbType.Int, 4);
            cmdParams[0].Value = UserID;
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, cmdParams);
        }

        #region 通过用户名获取自己的日志
        /// <summary>
        ///通过用户名获取自己的日志 
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static List<UserLog> GetSelfUserLogByUserID(int userID, int state, Guid logTypeID, DateTime creatDate, BDUModel.PagePagination page)
        {
            try
            {
                string sqlGetSelfUserLogByUserID = @"select * from ZL_Sns_UserLog where UserID=@UserID and LogState=@LogState ";
                if (logTypeID != new Guid("ff503f4b-7972-4cd2-8fc8-08bdcfff7115"))
                {
                    sqlGetSelfUserLogByUserID += @" and LogTypeID=@LogTypeID";
                }
                if (creatDate != new DateTime())
                {
                    sqlGetSelfUserLogByUserID += @"  and CONVERT(varchar(12) , CreatDate,111)=@CreatDate ";
                }
                sqlGetSelfUserLogByUserID += @" order by CreatDate desc";
                if (page != null)
                    sqlGetSelfUserLogByUserID = page.PaginationSql(sqlGetSelfUserLogByUserID);
                SqlParameter[] parameter ={
                                new SqlParameter("UserID",userID),
                                new SqlParameter("LogState",state),
                                new SqlParameter("LogTypeID",logTypeID),
                                new SqlParameter("CreatDate",creatDate.ToString()==new DateTime().ToString()?DateTime.Now.ToString():creatDate.ToString("yyyy/MM/dd").Replace('-','/'))
                };
                List<UserLog> list = new List<UserLog>();
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sqlGetSelfUserLogByUserID, parameter))
                {
                    while (dr.Read())
                    {
                        UserLog log = new UserLog();
                        ReadUserLog(dr, log);
                        list.Add(log);
                    }
                }
                return list;

            }
            catch
            {
                throw;
            }

        }
        /// <summary>
        /// 通过用户ID获取用户日志
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public static List<UserLog> GetLogByUserID(int UserID) {
            string sqlStr = "select * from ZL_Sns_UserLog where UserID=@UserID";
            SqlParameter[] parameter ={
                                      new SqlParameter("UserID",UserID)
                                      };
            List<UserLog> list = new List<UserLog>();
            using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sqlStr, parameter))
                {
                    while (dr.Read())
                    {
                        UserLog log = new UserLog();
                        ReadUserLog(dr, log);
                        list.Add(log);
                    }
                    
                }
                return list;
                
        }
        #endregion

        #region 通过日志类别获取日志
        /// <summary>
        /// 通过日志类别获取日志
        /// </summary>
        /// <param name="logTypeID"></param>
        /// <returns></returns>
        public static List<UserLog> GetUserLogByLogTypeID(Guid logTypeID,int UserID)
        {
            try
            {
                List<UserLog> list = new List<UserLog>();
                string sqlGetUserLogByLogTypeID = @"select * from ZL_Sns_UserLog where LogTypeID=@LogTypeID and LogState=1";
                if (UserID >0)
                {
                    sqlGetUserLogByLogTypeID += " and UserID=" + UserID;
                }
                SqlParameter[] parameter ={ new SqlParameter("LogTypeID", logTypeID) };
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sqlGetUserLogByLogTypeID, parameter))
                {
                    while (dr.Read())
                    {
                        UserLog userLog = new UserLog();
                        ReadUserLog(dr, userLog);
                        list.Add(userLog);
                    }
                }
                return list;

            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 通过日期获取日志
        /// <summary>
        /// 通过日期获取日志
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static List<UserLog> GetUserLogByData(int userID)
        {
            try
            {
                List<UserLog> list = new List<UserLog>();
                string sqlGetUserLogByData = @"SELECT COUNT(*) AS LogCount,CONVERT(varchar(12) , CreatDate,111) AS LogDate FROM ZL_Sns_UserLog WHERE  UserID=@UserID and LogState=1  GROUP BY CONVERT(varchar(12) , CreatDate,111) ORDER BY  LogDate desc";
                SqlParameter[] parameter ={ new SqlParameter("UserID", userID) };
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sqlGetUserLogByData, parameter))
                {
                    while (dr.Read())
                    {
                        UserLog userLog = new UserLog();
                        userLog.CreatDate = DateTime.Parse(dr["LogDate"].ToString());
                        userLog.ReadCount = int.Parse(dr["LogCount"].ToString());
                        list.Add(userLog);
                    }
                }
                return list;

            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 通过日志编号获取日志
        /// <summary>
        ///通过日志编号获取日志 
        /// </summary>
        /// <param name="logID"></param>
        /// <returns></returns>
        public static UserLog GetUserLogByID(Guid logID)
        {
            try
            {
                UserLog userLog = new UserLog();
                string sqlGetUserLogByID = @"select * from ZL_Sns_UserLog where ID=@ID";
                SqlParameter[] parameter ={ new SqlParameter("ID", logID) };
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sqlGetUserLogByID, parameter))
                {
                    if (dr.Read())
                    {
                        ReadUserLog(dr, userLog);
                    }
                }
                return userLog;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 修改日志
        /// <summary>
        /// 修改日志
        /// </summary>
        /// <param name="userLog"></param>
        public static void UpdataLog(UserLog userLog)
        {
            try
            {
                string sqlUpdataLog = @"UPDATE [ZL_Sns_UserLog]  SET LogContext=@LogContext,[UpdateLogDate]=@UpdateLogDate,[LogTypeID]=@LogTypeID,[LogState]=@LogState,[LogTitle]=@LogTitle where ID=@ID";
                SqlParameter[] parameter ={
                        new SqlParameter("ID",userLog.ID),
                        new SqlParameter("LogContext",userLog.LogContext),
                        new SqlParameter("UpdateLogDate",DateTime.Now),
                        new SqlParameter("LogTypeID",userLog.LogTypeID),
                        new SqlParameter("LogState",userLog.LogState),
                        new SqlParameter("LogTitle",userLog.LogTitle)
                    };
                SqlHelper.ExecuteNonQuery(CommandType.Text, sqlUpdataLog, parameter);
            }
            catch
            {
                throw;
            }
        }

        public static void UpdataReadCount(Guid logid, int readCount)
        {
            string sqlUpdataReadCount = @"UPDATE [ZL_Sns_UserLog]  SET ReadCount=@ReadCount WHERE ID=@ID";
            SqlParameter[] parameter ={
                        new SqlParameter("@ID",logid),
                        new SqlParameter("@ReadCount",readCount),
                      };
            SqlHelper.ExecuteNonQuery(CommandType.Text, sqlUpdataReadCount, parameter);
        }
        #endregion

        #region 通过日志编号删除日志
        /// <summary>
        /// 通过日志编号删除日志
        /// </summary>
        /// <param name="logID"></param>
        public static void DeleteLogByID(Guid logID)
        {
            try
            {
                string sqlDeleteLogType = @"delete ZL_Sns_UserLog where ID=@ID";
                SqlParameter[] parameter ={
                new SqlParameter("ID",logID)};
                SqlHelper.ExecuteNonQuery(CommandType.Text, sqlDeleteLogType, parameter);
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 获取日志的评论
        /// <summary>
        ///获取日志的评论 
        /// </summary>
        /// <param name="logID"></param>
        /// <returns></returns>
        public static List<LogCriticism> GetLogCriticismByLogID(Guid logID, PagePagination page)
        {
            try
            {
                //string sqlGetLogCriticismByLogID = @"SELECT ZL_Sns_LogCriticism.UserID, ZL_User.UserName,ZL_Sns_LogCriticism.ID, ZL_Sns_LogCriticism.LogID, 
                //                                      ZL_Sns_LogCriticism.CriticismConten, ZL_Sns_LogCriticism.CreatTime FROM ZL_Sns_LogCriticism INNER JOIN ZL_User ON ZL_Sns_LogCriticism.UserID = ZL_User.UserID where ZL_Sns_LogCriticism.LogID=@LogID";
                //if (page != null)
                //    sqlGetLogCriticismByLogID = page.PaginationSql(sqlGetLogCriticismByLogID);
                //SqlParameter[] parameter ={
                //new SqlParameter("LogID",logID)};
                //List<LogCriticism> list = new List<LogCriticism>();
                //using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sqlGetLogCriticismByLogID, parameter))
                //{
                //    while (dr.Read())
                //    {
                //        LogCriticism logCriticism = new LogCriticism();
                //        ReadLogCriticism(dr, logCriticism);
                //        logCriticism.UserName = dr["UserName"].ToString();
                //        //logCriticism.Userpic = dr["Userpic"].ToString();
                //        list.Add(logCriticism);
                //    }
                //}
                //return list;
                return null;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 创建评论
        /// <summary>
        /// 创建评论
        /// </summary>
        /// <param name="logCriticism"></param>
        /// <returns></returns>
        public static Guid CreatLogCriticism(LogCriticism logCriticism)
        {
            try
            {
                //logCriticism.ID = Guid.NewGuid();
                //string sqlCreatLogCriticism = @"INSERT INTO [ZL_Sns_LogCriticism]([ID],[LogID],[UserID] ,[CriticismConten],[CreatTime]) VALUES(@ID,@LogID,@UserID,@CriticismConten,@CreatTime);";
                //sqlCreatLogCriticism += @"  UPDATE ZL_Sns_UserLog SET CriticismCount= (CASE WHEN CriticismCount IS NULL THEN 1 ELSE CriticismCount+1 END) where ID=@LogID ";

                //SqlParameter[] parameter ={
                //    new SqlParameter("ID",logCriticism.ID ),
                //    new SqlParameter("LogID",logCriticism.LogID ),
                //    new SqlParameter("UserID",logCriticism.UserID ),
                //    new SqlParameter("CriticismConten",logCriticism.CriticismConten ),
                //    new SqlParameter("CreatTime",DateTime.Now)
                //};
                //SqlHelper.ExecuteNonQuery(CommandType.Text, sqlCreatLogCriticism, parameter);
                //return logCriticism.ID;
                return new Guid();
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 删除评论
        /// <summary>
        /// 删除评论
        /// </summary>
        /// <param name="criticimeID"></param>
        public static void DeleteCriticism(Guid criticimeID)
        {
            //try
            //{
            //    string sqlDeleteCriticism = @"delete ZL_Sns_LogCriticism where ID=@ID";
            //    SqlParameter[] parameter ={
            //    new SqlParameter("ID",criticimeID)};
            //    SqlHelper.ExecuteNonQuery(CommandType.Text, sqlDeleteCriticism, parameter);
            //}
            //catch
            //{
            //    throw;
            //}
        }
        #endregion

        #region 读取日志类别
        /// <summary>
        /// 读取日志类别
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="logType"></param>
        public static void ReadUserType(SqlDataReader dr, UserLogType logType)
        {
            logType.ID = (Guid)dr["ID"];
            logType.CreateTime = dr["CreateTime"] is DBNull ? new DateTime() : DateTime.Parse(dr["CreateTime"].ToString());
            logType.LogArea = dr["LogArea"] is DBNull ? 0 : int.Parse(dr["LogArea"].ToString());
            logType.LogTypeName = dr["LogTypeName"].ToString();
            logType.UserID = Convert.ToInt32 (dr["UserID"].ToString());
            logType.LogTypePWD = dr["LogTypePWD"].ToString();
        }
        #endregion

        #region 读取日志
        /// <summary>
        ///读取日志 
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="userLog"></param>
        public static void ReadUserLog(SqlDataReader dr, UserLog userLog)
        {
            userLog.CreatDate = dr["CreatDate"] is DBNull ? new DateTime() : DateTime.Parse(dr["CreatDate"].ToString());
            userLog.CriticismCount = dr["CriticismCount"] is DBNull ? 0 : int.Parse(dr["CriticismCount"].ToString());
            userLog.ID = (Guid)dr["ID"];
            userLog.LogContext = dr["LogContext"].ToString();
            userLog.LogState = dr["LogState"] is DBNull ? 0 : int.Parse(dr["LogState"].ToString());
            userLog.LogTitle = dr["LogTitle"].ToString();
            userLog.LogTypeID = (Guid)dr["LogTypeID"];
            userLog.ReadCount = dr["ReadCount"] is DBNull ? 0 : int.Parse(dr["ReadCount"].ToString());
            userLog.UpdateLogDate = dr["UpdateLogDate"] is DBNull ? new DateTime() : DateTime.Parse(dr["UpdateLogDate"].ToString());
            userLog.UserID = Convert.ToInt32 (dr["UserID"].ToString ());
        }
        #endregion

        #region 读取评论
        /// <summary>
        /// 读取评论
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="logCriticism"></param>
        public static void ReadLogCriticism(SqlDataReader dr, LogCriticism logCriticism)
        {
            logCriticism.CreatTime = DateTime.Parse(dr["CreatTime"].ToString());
            logCriticism.CriticismConten = dr["CriticismConten"].ToString();
            logCriticism.ID = (Guid)dr["ID"];
            logCriticism.LogID = (Guid)dr["LogID"];
            logCriticism.UserID = Convert.ToInt32 (dr["UserID"].ToString ());
        }
        #endregion

        #region 高级搜索日志
        /// <summary>
        /// 通过用户自定义条件搜索日志
        /// </summary>
        /// <param name="whereEx"></param>
        /// <returns></returns>
        public static DataTable GetLogMessageByc(string whereEx)
        {
            string cmd = "select ZL_Sns_UserLog.* from zl_userbase,ZL_Sns_UserLog where zl_userbase.userid=ZL_Sns_UserLog.userid"+whereEx;
            return SqlHelper.ExecuteTable(CommandType.Text,cmd,null);
        }
        #endregion
        #region 通过用户名搜索日志
        /// <summary>
        /// 通过用户名搜索日志
        /// </summary>
        /// <param name="whereEx"></param>
        /// <returns></returns>
        public static DataTable GetLogMessageByN(string whereEx)
        {
            string cmd = "";
            return SqlHelper.ExecuteTable(CommandType.Text,cmd,null);
        
        }
        #endregion
    }
}
