
namespace ZoomLa.SQLDAL
{
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

    /// <summary>
    /// SD_ProjectWork 的摘要说明
    /// </summary>
    public class SD_ProjectWork :ID_ProjectWork
    {
        public SD_ProjectWork()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        public bool AddProjectWork(M_ProjectWork m_projwork)       
        {
            string strSql = "PR_ProjectWork_Add";
            SqlParameter[] cmdParams = GetParameters(m_projwork);
            return SqlHelper.ExecuteProc(strSql, cmdParams);
        }

        public bool DelProWorByID(int workId)
        {
            SqlParameter[] cmdParams = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParams[0].Value = workId;
            return (SqlHelper.ExecuteProc("PR_ProjectWork_Del", cmdParams));
        }
        public bool DelProWorByPID(int projectId)
        {
            string strSql = "delete from ZL_ProjectWork where ProjectID=@PID";
            SqlParameter[] sp = new SqlParameter[] {
                new SqlParameter("@PID",SqlDbType.Int)
            };
            sp[0].Value = projectId;
            return (SqlHelper.ExecuteSql(strSql,sp));
        }
        public DataTable SelectWorkByPID(int projectId)
        {
            SqlParameter[] cmdParams = new SqlParameter[] { new SqlParameter("@ProjectID", SqlDbType.Int, 4) };
            cmdParams[0].Value =projectId;
            return SqlHelper.ExecuteTable(CommandType.StoredProcedure,"PR_ProjectWork_SelByPID",cmdParams);
        }
        private static SqlParameter[] GetParameters(M_ProjectWork m_projwork)
        {
            SqlParameter[] parameter = new SqlParameter[] {
                new SqlParameter("@WorkID", SqlDbType.Int, 4),
                new SqlParameter("@WorkName",SqlDbType.NVarChar,255),
                new SqlParameter("@WorkIntro", SqlDbType.NText),
                new SqlParameter("@ProjectID", SqlDbType.Int, 4),
                new SqlParameter("@Approving", SqlDbType.Int, 4),
                new SqlParameter("@Status", SqlDbType.Int, 4),
                new SqlParameter("@EndDate", SqlDbType.DateTime,8)
            };
            parameter[0].Value = m_projwork.WorkID;
            parameter[1].Value = m_projwork.WorkName;
            parameter[2].Value = m_projwork.WorkIntro;
            parameter[3].Value = m_projwork.ProjectID;
            parameter[4].Value = m_projwork.Approving;
            parameter[5].Value = m_projwork.Status;
            parameter[6].Value = m_projwork.EndDate;
            return parameter;
         }
        public bool UpdateProjectWork(M_ProjectWork m_projwork)
        {
            string strSql = "PR_ProjectWork_Update";
            SqlParameter[] cmdParams = GetParameters(m_projwork);
            return SqlHelper.ExecuteProc(strSql, cmdParams);
        }
        public M_ProjectWork SelectWorkByWID(int WId)
        {
            SqlParameter[] cmdParams = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParams[0].Value =WId;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "PR_ProjectWork_SelByWID", cmdParams))
            {
                if (reader.Read())
                {
                    return GetProjectWorkInfoFromReader(reader);
                }
                else
                    return new M_ProjectWork();
            }   
        }
        private M_ProjectWork GetProjectWorkInfoFromReader(SqlDataReader reader)
        {
            M_ProjectWork info = new M_ProjectWork();
            info.WorkID = DataConverter.CLng(reader["WorkID"]);
            info.WorkName =reader["WorkName"].ToString();
            info.WorkIntro = reader["WorkIntro"].ToString();
            info.ProjectID =DataConverter.CLng(reader["ProjectID"]);
            info.Approving =DataConverter.CLng(reader["Approving"].ToString());
            info.Status = DataConverter.CLng(reader["Status"].ToString().Trim());
            info.EndDate =DataConverter.CDate(reader["EndDate"].ToString().Trim());
            return info;
        }
        public int CountWork(int projectid)
        {
            string strSql = "select count(*) from ZL_ProjectWork where ProjectID=@PID";
            SqlParameter[] sp = new SqlParameter[] {
                new SqlParameter("@PID",SqlDbType.Int)
            };
            sp[0].Value = projectid;
            return SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.Text,strSql, sp));
        }
        public int CountFinishWork(int projectid)
        {
            string strSql = "select count(*) from ZL_ProjectWork where ProjectID=@PID and status=1";
            SqlParameter[] sp = new SqlParameter[] {
                new SqlParameter("@PID",SqlDbType.Int)
            };
            sp[0].Value = projectid;
            return SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.Text, strSql, sp));
        }
        public int GetMaxWorkID(int projectid)
        {
            string strSql = "select top 1 WorkID from ZL_ProjectWork where ProjectID=@PID order by WorkID desc";
            SqlParameter[] sp = new SqlParameter[] {
                new SqlParameter("@PID",SqlDbType.Int)
            };
            sp[0].Value = projectid;
            return SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.Text, strSql, sp));
        }
        public DataTable GetProjectWorkAll()
        {
            string strSql = "select * from ZL_ProjectWork order by(WorkID) desc";
            return SqlHelper.ExecuteTable(CommandType.Text, strSql, null);
        }
    }
}
