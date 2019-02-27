using System;
using System.Collections.Generic;
using System.Text;
using BDUModel;
using System.Data.SqlClient;
using System.Data;
using ZoomLa.SQLDAL;

namespace BDULogic
{
    public class SystemLogLogic
    {
        #region 创建日志
        /// <summary>
        /// 创建日志
        /// </summary>
        /// <param name="fortune"></param>
        public static Guid  AddSystemLog(SystemLog log)
        {
            try
            {
                log.ID = Guid.NewGuid();
                string SQLstr = @"INSERT INTO [SystemLog] (
	                            [ID],[UserID],[ContentID],[LogType],[ModelUrl],[CreateTime],[LogContent],[LogTitle]
                            ) VALUES (
	                            @ID,@UserID,@ContentID, @LogType,@ModelUrl,@CreateTime,@LogContent,@LogTitle
                            )";

                SqlParameter[] parameter ={ 
                new SqlParameter("@ID",log.ID), 
                new SqlParameter("@UserID",log.UserID), 
                new SqlParameter("@ContentID",log.ContentID), 
                new SqlParameter("@LogType",log.LogType), 
                new SqlParameter("@ModelUrl",log.ModelUrl), 
                new SqlParameter("@CreateTime",DateTime.Now),
                new SqlParameter("@LogContent",log.LogContent),
                new SqlParameter("@LogTitle",log.LogTitle)
            };
                SqlHelper.ExecuteNonQuery(CommandType.Text, SQLstr, parameter);
                return log.ID;
            }
            catch
            {
                throw;
            }

        }
        #endregion

        #region 获取用户动态
        /// <summary>
        /// 获取用户动态
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static List<SystemLog> GetUserDynamic(Guid userID, PagePagination page)
        {
            try
            {
                List<SystemLog> list = new List<SystemLog>();
                string SQLstr = @"SELECT * FROM SystemLog WHERE UserID=@UserID";
                if (page != null)
                {
                    SQLstr = page.PaginationSql(SQLstr);
                }
                SqlParameter[] parameter ={ 
                new SqlParameter("@UserID",userID)};
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, SQLstr, parameter))
                {
                    while (dr.Read())
                    {
                        SystemLog log = new SystemLog();
                        ReadSystemLog(dr, log);
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

        #region 获取好友动态
        /// <summary>
        /// 获取好友动态
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static List<SystemLog> GetFriendDynamic(Guid userID,string topNum)
        {
            try
            {
                List<SystemLog> list = new List<SystemLog>();
                string sqlGetFriendDynamic;
                if(topNum=="")
                    sqlGetFriendDynamic = @"SELECT * FROM SystemLog WHERE UserID IN(SELECT  FriendID FROM Userfriend WHERE HostID=@UserID) AND UserID<>@UserID";
                else
                    sqlGetFriendDynamic = "SELECT top " + topNum + @" * FROM SystemLog WHERE UserID IN(SELECT  FriendID FROM Userfriend WHERE HostID=@UserID) AND UserID<>@UserID";
                SqlParameter[] parameter ={ 
                new SqlParameter("@UserID",userID)};
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sqlGetFriendDynamic, parameter))
                {
                    while (dr.Read())
                    {
                        SystemLog log = new SystemLog();
                        ReadSystemLog(dr, log);
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

        #region 获取流量日志
        /// <summary>
        /// 获取流量日志
        /// </summary>
        /// <param name="logtype">流量类型</param>
        /// <param name="page">显示的数量</param>
        /// <returns></returns>
        public static List<SystemLog> GetFluxLog(SystemLogType logtype, PagePagination page)
        {
            try
            {
                string SQLstr = String.Empty;
                if (logtype == SystemLogType.All)
                {
                    SQLstr = @"select * from SystemLog where ID in (select GolID from MoneyFlux)";

                }
                else
                {
                    SQLstr = @"select * from SystemLog where LogType=@LogType ";

                }
                if (page != null)
                {
                    SQLstr = page.PaginationSql(SQLstr);
                }
                SqlParameter[] sp ={
                    new SqlParameter("@LogType",logtype)
                    };
                List<SystemLog> list = new List<SystemLog>();
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, SQLstr, sp))
                {
                    while (dr.Read())
                    {
                        SystemLog log = new SystemLog();
                        ReadSystemLog(dr, log);
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

        #region 读取系统日志
        public static void ReadSystemLog(SqlDataReader dr, SystemLog log)
        {
            log.ID = (Guid)dr["ID"];
            log.ContentID = dr["ContentID"] is DBNull ? Guid.Empty : (Guid)dr["ContentID"];
            log.CreateTime = DateTime.Parse(dr["CreateTime"].ToString());
            log.LogType = dr["LogType"].ToString();
            log.ModelUrl = dr["ModelUrl"].ToString();
            log.UserID = (Guid)dr["UserID"];
            log.LogContent = dr["LogContent"].ToString();
            log.LogTitle = dr["LogTitle"].ToString();
        }
        #endregion

        
    }
}
