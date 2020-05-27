using System;
using System.Collections.Generic;
using System.Text;
using ZoomLa.SQLDAL;
using ZoomLa.Model.FTP;
using System.Data;
using System.Data.SqlClient;

namespace ZoomLa.BLL.FTP
{
    public class B_FTP
    {
        public B_FTP()
        {
            strTableName = initmod.TbName;
            PK = initmod.PK;
        }
        public string PK, strTableName;
        private M_FtpConfig initmod = new M_FtpConfig();
        /// <summary>
        /// 插入配置信息
        /// </summary>
        public int AddFTPFile(M_FtpConfig mf)
        {
            string sqlStr = "insert into ZL_FTPConfig values(@FtpServer,@FtpPort,@FtpUsername,@FtpPassword,@OutTime,@SavePath,@Alias,@Url)";
            SqlParameter[] cmdParams = initmod.GetParameters(mf);
            return SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.Text, sqlStr, cmdParams));
        }

        /// <summary>
        /// 查询配置记录
        /// </summary>
        /// <returns></returns>
        public DataTable SelectFtp_All()
        {
            return Sql.Sel(strTableName);
        }

        /// <summary>
        /// 根据ID查询配置记录
        /// </summary>
        public M_FtpConfig SeleteIDByAll(int id)
        {
            string sqlStr = "select * from ZL_FTPConfig where ID=" + id;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sqlStr))
            {
                if (reader.Read())
                {
                    M_FtpConfig mf =GetFtpFile(reader);
                    return mf;
                }
                else
                {
                    return new M_FtpConfig();
                }
            }
        }
        private M_FtpConfig GetFtpFile(SqlDataReader reader)
        {
            M_FtpConfig mf = new M_FtpConfig();
            mf.ID = Convert.ToInt32(reader["ID"]);
            mf.FtpServer = reader["FtpServer"].ToString();
            mf.FtpPort = reader["FtpPort"].ToString();
            mf.FtpUsername = reader["FtpUsername"].ToString();
            mf.FtpPassword = reader["FtpPassword"].ToString();
            mf.OutTime = reader["OutTime"].ToString();
            mf.SavePath = reader["SavePath"].ToString();
            mf.Alias = reader["Alias"].ToString();
            mf.Url = reader["Url"].ToString();
            reader.Close();
            reader.Dispose();
            return mf;
        }
        /// <summary>
        /// 修改配置信息
        /// </summary>
        public bool UpdateFtpFile(int id, M_FtpConfig mf)
        {
            string sqlStr = "update ZL_FTPConfig set FtpServer=@FtpServer,FtpPort=@FtpPort,FtpUsername=@FtpUsername,FtpPassword=@FtpPassword,OutTime=@OutTime,SavePath=@SavePath,Alias=@Alias,Url=@Url where ID=" + id;
            SqlParameter[] cmdParams = initmod.GetParameters(mf);
            return SqlHelper.ExecuteSql(sqlStr, cmdParams);
        }

        /// <summary>
        /// 删除配置信息
        /// </summary>
        public bool DeleteFtpFile(int id)
        {
            return Sql.Del(strTableName, id);
        }
        public DataTable SelByAlias(string alias) 
        {
            SqlParameter[] sp = new SqlParameter[] {new SqlParameter("Alias",alias) };
            string sql = "Select * From "+strTableName+" Where Alias =@Alias";
            return SqlHelper.ExecuteTable(CommandType.Text,sql,sp);
        }
        public M_FtpConfig SelFirstModel()
        {
            string sqlStr = "select Top 1 * from ZL_FTPConfig";
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sqlStr))
            {
                if (reader.Read())
                {
                    M_FtpConfig mf = GetFtpFile(reader);
                    return mf;
                }
                else
                {
                    return new M_FtpConfig();
                }
            }
        }
    }
}