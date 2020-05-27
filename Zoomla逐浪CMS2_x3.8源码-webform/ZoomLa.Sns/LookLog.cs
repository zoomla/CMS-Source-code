using System;
namespace ZoomLa.Model
{
    [Serializable]
    public class M_Sns_LookLog
    {
        #region 定义字段
        /// <summary>
        /// 
        /// </summary>
        private int m_id;
        /// <summary>
        /// 浏览用户ID
        /// </summary>
        private int m_userid;
        /// <summary>
        /// 被浏览的用户ID
        /// </summary>
        private int m_lookid;
        /// <summary>
        /// 浏览时间
        /// </summary>
        private DateTime m_looktime;
        /// <summary>
        /// 浏览者Ip
        /// </summary>
        private string m_lookip = string.Empty;
        #endregion

        #region 构造函数
        public M_Sns_LookLog()
        {
        }

        public M_Sns_LookLog
        (
            int id,
            int userid,
            int lookid,
            DateTime looktime,
            string lookip
        )
        {
            this.m_id = id;
            this.m_userid = userid;
            this.m_lookid = lookid;
            this.m_looktime = looktime;
            this.m_lookip = lookip;
        }
        /// <summary>
        /// 返回实体列表数组
        /// </summary>
        /// <returns>String[]</returns>
        public string[] Sns_LookLogList()
        {
            string[] Tablelist = { "id", "userid", "lookid", "looktime", "lookip" };
            return Tablelist;
        }
        #endregion

        #region 属性定义
        /// <summary>
        /// 
        /// </summary>
        public int id
        {
            get { return this.m_id; }
            set { this.m_id = value; }
        }
        /// <summary>
        /// 浏览用户ID
        /// </summary>
        public int userid
        {
            get { return this.m_userid; }
            set { this.m_userid = value; }
        }
        /// <summary>
        /// 被浏览的用户ID
        /// </summary>
        public int lookid
        {
            get { return this.m_lookid; }
            set { this.m_lookid = value; }
        }
        /// <summary>
        /// 浏览时间
        /// </summary>
        public DateTime looktime
        {
            get { return this.m_looktime; }
            set { this.m_looktime = value; }
        }
        /// <summary>
        /// 浏览者Ip
        /// </summary>
        public string lookip
        {
            get { return this.m_lookip; }
            set { this.m_lookip = value; }
        }
        #endregion
    }
}


namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using ZoomLa.Model;
    using ZoomLa.SQLDAL;

    /// <summary>
    /// B_Sns_LookLog 的摘要说明
    /// </summary>
    public class B_Sns_LookLog
    {
        public B_Sns_LookLog()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        /// <summary>
        ///添加记录
        /// </summary>
        /// <param name="Sns_LookLog"></param>
        /// <returns></returns>
        public int GetInsert(M_Sns_LookLog snsLooklog)
        {
            return SD_Sns_LookLog.GetInsert(snsLooklog);
        }
        public static bool GetInsertOrUpdate(M_Sns_LookLog snslooklog)
        {
            return SD_Sns_LookLog.GetInsertOrUpdate(snslooklog);
        }
        /// <summary>
        ///更新记录
        /// </summary>
        /// <param name="Sns_LookLog"></param>
        /// <returns></returns>
        public bool GetUpdate(M_Sns_LookLog snsLooklog)
        {
            return SD_Sns_LookLog.GetUpdate(snsLooklog);
        }

        /// <summary>
        ///删除记录
        /// </summary>
        /// <param name="Sns_LookLog"></param>
        /// <returns></returns>
        public bool DeleteByGroupID(int Sns_LookLogID)
        {
            return SD_Sns_LookLog.DeleteByGroupID(Sns_LookLogID);
        }

        /// <summary>
        ///查找一条记录
        /// </summary>
        /// <param name="Sns_LookLog"></param>
        /// <returns></returns>
        public M_Sns_LookLog GetSelect(int Sns_LookLogID)
        {
            return SD_Sns_LookLog.GetSelect(Sns_LookLogID);
        }

        /// <summary>
        ///返回所有记录
        /// </summary>
        /// <returns></returns>
        public DataTable Select_All()
        {
            return SD_Sns_LookLog.Select_All();
        }

    }
}

namespace ZoomLa.SQLDAL
{
    using System;
    using System.Data;
    using ZoomLa.Model;
    using System.Data.SqlClient;
    using ZoomLa.Common;

    /// <summary>
    /// 
    /// </summary>
    public static class SD_Sns_LookLog 
    {
        /// <summary>
        ///添加记录
        /// </summary>
        /// <param name="snslooklog"></param>
        /// <returns></returns>
        public static int GetInsert(M_Sns_LookLog snslooklog)
        {
            string sqlStr = "INSERT INTO [dbo].[ZL_Sns_LookLog] ([userid],[lookid],[looktime],[lookip]) VALUES (@userid,@lookid,@looktime,@lookip);select @@IDENTITY";
            SqlParameter[] cmdParams = GetParameters(snslooklog);
            return SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.Text, sqlStr, cmdParams));
        }
        /// <summary>
        /// 不存在则添加否则更新
        /// </summary>
        /// <param name="snslooklog"></param>
        /// <returns></returns>
        public static bool GetInsertOrUpdate(M_Sns_LookLog snslooklog)
        {
            string sqlStr = "SELECT [id],[userid],[lookid],[looktime],[lookip] FROM [dbo].[ZL_Sns_LookLog] where userid=@userid and lookid=@lookid";
            SqlParameter[] cmdParams = new SqlParameter[2];
            cmdParams[0] = new SqlParameter("@userid", SqlDbType.Int, 4);
            cmdParams[0].Value = snslooklog.userid;
            cmdParams[1] = new SqlParameter("@lookid", SqlDbType.NVarChar, 50);
            cmdParams[1].Value = snslooklog.lookid;
            DataTable selectlist = SqlHelper.ExecuteTable(CommandType.Text, sqlStr, cmdParams);
            if (selectlist.Rows.Count == 0)
            {
                string sqlStrc = "INSERT INTO [dbo].[ZL_Sns_LookLog] ([userid],[lookid],[looktime],[lookip]) VALUES (@userid,@lookid,@looktime,@lookip);select @@IDENTITY";
                SqlParameter[] cmdParamsc = GetParameters(snslooklog);
                SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.Text, sqlStrc, cmdParamsc));
            }
            else
            {
                string sqlStrd = "UPDATE [dbo].[ZL_Sns_LookLog] SET [userid] = @userid,[lookid] = @lookid,[looktime] = @looktime,[lookip] = @lookip WHERE [id] = @id";
                SqlParameter[] cmdParamsd = GetParameters(snslooklog);
                SqlHelper.ExecuteSql(sqlStrd, cmdParamsd);
            }
            return true;
        }

        /// <summary>
        ///更新记录
        /// </summary>
        /// <param name="snslooklog"></param>
        /// <returns></returns>
        public static bool GetUpdate(M_Sns_LookLog snslooklog)
        {
            string sqlStr = "UPDATE [dbo].[ZL_Sns_LookLog] SET [userid] = @userid,[lookid] = @lookid,[looktime] = @looktime,[lookip] = @lookip WHERE [id] = @id";
            SqlParameter[] cmdParams = GetParameters(snslooklog);
            return SqlHelper.ExecuteSql(sqlStr, cmdParams);
        }

        /// <summary>
        ///按标识删除记录
        /// </summary>
        /// <param name="strSQL"></param>
        /// <returns></returns>
        public static bool DeleteByGroupID(int id)
        {
            string sqlStr = "DELETE FROM [dbo].[ZL_Sns_LookLog] WHERE [id] = @id";
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@id", SqlDbType.Int, 4);
            cmdParams[0].Value = id;
            return SqlHelper.ExecuteSql(sqlStr, cmdParams);
        }

        /// <summary>
        ///查找一条记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static M_Sns_LookLog GetSelect(int id)
        {
            string sqlStr = "SELECT [id],[userid],[lookid],[looktime],[lookip] FROM [dbo].[ZL_Sns_LookLog] WHERE [id] = @id";
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@id", SqlDbType.Int, 4);
            cmdParams[0].Value = id;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sqlStr, cmdParams))
            {
                if (reader.Read())
                {
                    return GetInfoFromReader(reader);
                }
                else
                {
                    return new M_Sns_LookLog();
                }
            }
        }

        /// <summary>
        ///返回所有记录
        /// </summary>
        /// <returns></returns>
        public static DataTable Select_All()
        {
            string sqlStr = "SELECT [id],[userid],[lookid],[looktime],[lookip] FROM [dbo].[ZL_Sns_LookLog]";
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, null);
        }

        #region 方法定义
        /// <summary>
        /// GetParameters
        /// </summary>
        /// <param name="Sns_LookLoginfo"></param>
        /// <returns>SqlParameter[]</returns>
        private static SqlParameter[] GetParameters(M_Sns_LookLog Sns_LookLoginfo)
        {
            SqlParameter[] parameter = new SqlParameter[5];
            parameter[0] = new SqlParameter("@id", SqlDbType.Int, 4);
            parameter[0].Value = Sns_LookLoginfo.id;
            parameter[1] = new SqlParameter("@userid", SqlDbType.Int, 4);
            parameter[1].Value = Sns_LookLoginfo.userid;
            parameter[2] = new SqlParameter("@lookid", SqlDbType.Int, 4);
            parameter[2].Value = Sns_LookLoginfo.lookid;
            parameter[3] = new SqlParameter("@looktime", SqlDbType.DateTime, 8);
            parameter[3].Value = Sns_LookLoginfo.looktime;
            parameter[4] = new SqlParameter("@lookip", SqlDbType.NVarChar, 50);
            parameter[4].Value = Sns_LookLoginfo.lookip;
            return parameter;
        }

        /// <summary>
        /// GetInfoFromReader
        /// </summary>
        /// <param name="rdr">SqlDataReader</param>
        /// <returns></returns>
        private static M_Sns_LookLog GetInfoFromReader(SqlDataReader rdr)
        {
            M_Sns_LookLog info = new M_Sns_LookLog();
            info.id = DataConverter.CLng(rdr["id"].ToString());
            info.userid = DataConverter.CLng(rdr["userid"].ToString());
            info.lookid = DataConverter.CLng(rdr["lookid"].ToString());
            info.looktime = DataConverter.CDate(rdr["looktime"].ToString());
            info.lookip = rdr["lookip"].ToString();
            rdr.Close();
            return info;
        }

        /// <summary>
        /// GetInfoFromDataTable
        /// </summary>
        /// <param name="Rowsinfo">DataTable</param>
        /// <returns></returns>
        private static M_Sns_LookLog GetInfoFromDataTable(DataTable Rowsinfo)
        {
            M_Sns_LookLog info = new M_Sns_LookLog();
            if (Rowsinfo.Rows.Count > 0)
            {
                info.id = DataConverter.CLng(Rowsinfo.Rows[0]["id"].ToString());
                info.userid = DataConverter.CLng(Rowsinfo.Rows[0]["userid"].ToString());
                info.lookid = DataConverter.CLng(Rowsinfo.Rows[0]["lookid"].ToString());
                info.looktime = DataConverter.CDate(Rowsinfo.Rows[0]["looktime"].ToString());
                info.lookip = Rowsinfo.Rows[0]["lookip"].ToString();
            }
            return info;
        }
        #endregion

    }
}




