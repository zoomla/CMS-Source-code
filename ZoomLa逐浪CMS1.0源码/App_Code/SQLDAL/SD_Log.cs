namespace ZoomLa.SQLDAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Text;
    using ZoomLa.IDAL;
    using ZoomLa.Model;
    using System.Data.SqlClient;

    /// <summary>
    /// SD_Log 的摘要说明
    /// </summary>
    public class SD_Log : ID_Log
    {
        public SD_Log()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }        
        /// <summary>
        /// 将日志信息的各属性值传递到参数中
        /// </summary>
        /// <param name="Info"></param>
        /// <returns></returns>
        private static SqlParameter[] GetParameters(M_LogInfo Info)
        {
            SqlParameter[] parameters = new SqlParameter[] { 
                new SqlParameter("@LogID",SqlDbType.Int,4),
                new SqlParameter("@Category", SqlDbType.Int, 4),
                new SqlParameter("@Priority", SqlDbType.Int, 4),
                new SqlParameter("@Title", SqlDbType.NVarChar, 255),
                new SqlParameter("@Message", SqlDbType.NText, 16),
                new SqlParameter("@Timestamp", SqlDbType.DateTime, 8),
                new SqlParameter("@UserName", SqlDbType.NVarChar, 20),
                new SqlParameter("@UserIP", SqlDbType.NVarChar, 50),
                new SqlParameter("@Source", SqlDbType.NText, 16),
                new SqlParameter("@ScriptName", SqlDbType.NVarChar, 255),
                new SqlParameter("@PostString", SqlDbType.NText, 16)
            };
            parameters[0].Value = Info.LogId;
            parameters[1].Value=Info.Category;
            parameters[2].Value=Info.Priority;
            parameters[3].Value=Info.Title;
            parameters[4].Value=Info.Message;
            parameters[5].Value=Info.Timestamp;
            parameters[6].Value=Info.UserName;
            parameters[7].Value=Info.UserIP;
            parameters[8].Value=Info.Source;
            parameters[9].Value=Info.ScriptName;
            parameters[10].Value=Info.PostString;
            return parameters;
        } 
        #region ID_Log 成员
        /// <summary>
        /// 添加日志到数据库
        /// </summary>
        /// <param name="info">日志信息</param>
        public bool Add(M_LogInfo Info)
        {
            string strSql = "INSERT INTO ZL_Log(Category,Priority,Title,Message,Timestamp,UserName,UserIP,Source,ScriptName,PostString)";
            strSql+=" VALUES(@Category,@Priority,@Title,@Message,@Timestamp,@UserName,@UserIP,@Source,@ScriptName,@PostString)";
            SqlParameter[] cmdParams = new SqlParameter[] { 
                new SqlParameter("@Category", SqlDbType.Int, 4),
                new SqlParameter("@Priority", SqlDbType.Int, 4),
                new SqlParameter("@Title", SqlDbType.NVarChar, 255),
                new SqlParameter("@Message", SqlDbType.NText, 16),
                new SqlParameter("@Timestamp", SqlDbType.DateTime, 8),
                new SqlParameter("@UserName", SqlDbType.NVarChar, 20),
                new SqlParameter("@UserIP", SqlDbType.NVarChar, 50),
                new SqlParameter("@Source", SqlDbType.NText, 16),
                new SqlParameter("@ScriptName", SqlDbType.NVarChar, 255),
                new SqlParameter("@PostString", SqlDbType.NText, 16)
            };
            cmdParams[0].Value = Info.Category;
            cmdParams[1].Value = Info.Priority;
            cmdParams[2].Value = Info.Title;
            cmdParams[3].Value = Info.Message;
            cmdParams[4].Value = Info.Timestamp;
            cmdParams[5].Value = Info.UserName;
            cmdParams[6].Value = Info.UserIP;
            cmdParams[7].Value = Info.Source;
            cmdParams[8].Value = Info.ScriptName;
            cmdParams[9].Value = Info.PostString;
            return SqlHelper.ExecuteSql(strSql, cmdParams);
        }
        public bool Delete(int id)
        {
            string strSql = "DELETE FROM ZL_Log Where LogID=@LogID";
            SqlParameter[] cmdParams = new SqlParameter[] { new SqlParameter("LogID",SqlDbType.Int,4) };
            cmdParams[0].Value=id;
            return SqlHelper.ExecuteSql(strSql, cmdParams);
        }
        #endregion
    }
}