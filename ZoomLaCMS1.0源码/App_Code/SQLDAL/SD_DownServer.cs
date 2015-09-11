using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ZoomLa.IDAL;
using ZoomLa.Model;
using System.Data.SqlClient;
using ZoomLa.Common;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

/// <summary>
/// SD_DownServer 的摘要说明
/// </summary>
namespace ZoomLa.SQLDAL
{
    public class SD_DownServer : ID_DownServer
    {
        public SD_DownServer()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        private SqlParameter[] GetParameters(M_DownServer downserverInfo)
        {
            SqlParameter[] parameter = new SqlParameter[] {
                new SqlParameter("@ServerID", SqlDbType.Int),
                new SqlParameter("@ServerName", SqlDbType.NVarChar,50),
                new SqlParameter("@ServerUrl", SqlDbType.NVarChar,50),
                new SqlParameter("@ServerLogo", SqlDbType.NVarChar,255),
                new SqlParameter("@OrderID", SqlDbType.Int),  
                new SqlParameter("@ShowType",SqlDbType.Int)
            };
            parameter[0].Value = downserverInfo.ServerID;
            parameter[1].Value = downserverInfo.ServerName;
            parameter[2].Value = downserverInfo.ServerUrl;
            parameter[3].Value = downserverInfo.ServerLogo;
            parameter[4].Value = downserverInfo.OrderID;
            parameter[5].Value = downserverInfo.ShowType;
            return parameter;
        }
        public bool Add(M_DownServer DownServerInfo)
        {
            string strSql = "PR_DownServer_Add";
            SqlParameter[] parameter = GetParameters(DownServerInfo);
            return SqlHelper.ExecuteProc(strSql, parameter);
        }
        public bool DeleteByID(string downserverId)
        {

            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@ServerID", SqlDbType.NVarChar, 4000);
            cmdParams[0].Value = downserverId;
            return SqlHelper.ExecuteProc("PR_DownServer_Delete", cmdParams);
        }
        public bool Update(M_DownServer downserverInfo)
        {

            SqlParameter[] cmdParams = GetParameters(downserverInfo); ;
            return SqlHelper.ExecuteProc("PR_DownServer_Update", cmdParams);
        }
        public M_DownServer GetDownServerByid(int dId)
        {
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@ServerID", SqlDbType.Int, 4);
            cmdParams[0].Value = dId;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "PR_DownServer_GetById", cmdParams))
            {
                if (reader.Read())
                {
                    return GetDownServerInfoFromReader(reader);
                }
                else
                    return new M_DownServer();
            }
        }
        private M_DownServer GetDownServerInfoFromReader(SqlDataReader reader)
        {
            M_DownServer info = new M_DownServer();
            info.ServerID = DataConverter.CLng(reader["ServerID"]);
            info.ServerName = reader["ServerName"].ToString();
            info.ServerUrl = reader["ServerUrl"].ToString();
            info.ServerLogo= reader["ServerLogo"].ToString();
            info.OrderID = DataConverter.CLng(reader["OrderID"]);
            info.ShowType=DataConverter.CLng(reader["ShowType"]);
            reader.Close();
            return info;
        }
        public DataTable GetDownServerAll()
        {
            return SqlHelper.ExecuteTable(CommandType.StoredProcedure,"PR_DownServer_GetList", null);

        }
        public int Max()
        {
            string strsql = "SELECT TOP 1 ServerID FROM [ZL_DownServer] ORDER BY ServerID DESC";
            return SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.Text, strsql, null));

        }
        
    }
}
