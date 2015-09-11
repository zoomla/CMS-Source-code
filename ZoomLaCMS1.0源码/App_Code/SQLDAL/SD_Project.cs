
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
    /// SD_Project 的摘要说明
    /// </summary>
    public class SD_Project : ID_Project
    {
        public SD_Project()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        public bool Add(M_Project m_project)
        {
            string strSql = "PR_Project_Add";
            SqlParameter[] parameter = GetParameters(m_project);
            return SqlHelper.ExecuteProc(strSql, parameter);
        }
        public bool DeleteByID(int projectId)
        {
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@ID", SqlDbType.Int,4);
            cmdParams[0].Value = projectId;
            return SqlHelper.ExecuteProc("PR_Project_Delete", cmdParams);
        }
        public bool Update(M_Project m_project)
        {

            SqlParameter[] cmdParams = GetParameters(m_project); ;
            return SqlHelper.ExecuteProc("PR_Project_Update", cmdParams);
        }
        private SqlParameter[] GetParameters(M_Project m_project)
        {
            SqlParameter[] parameter = new SqlParameter[] {
                new SqlParameter("@ProjectID", SqlDbType.Int),
                new SqlParameter("@ProjectName", SqlDbType.NVarChar,255),
                new SqlParameter("@ProjectIntro", SqlDbType.NText,16),              
                new SqlParameter("@RequireID", SqlDbType.Int,4),
                new SqlParameter("@StartDate", SqlDbType.DateTime,8),
                new SqlParameter("@EndDate", SqlDbType.DateTime,8),
                new SqlParameter("@Status", SqlDbType.Int,4),
                new SqlParameter("@UserID", SqlDbType.Int,4)
                
            };
            parameter[0].Value = m_project.ProjectID;
            parameter[1].Value = m_project.ProjectName;
            parameter[2].Value = m_project.ProjectIntro;
            parameter[3].Value = m_project.RequireID;
            parameter[4].Value = m_project.StartDate;
            parameter[5].Value = m_project.EndDate;
            parameter[6].Value = m_project.Status;
            parameter[7].Value = m_project.Uid;
            return parameter;
        }
        public DataTable GetProjectAll()
        {
            string strSql = "select * from ZL_Project order by(ProjectID) asc";
            return SqlHelper.ExecuteTable(CommandType.Text, strSql, null);
        }
        public M_Project GetProjectByid(int projectId)
        {
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@ID", SqlDbType.Int, 4);
            cmdParams[0].Value = projectId;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "PR_Project_GetProjectByID", cmdParams))
            {
                if (reader.Read())
                {
                    return GetProjectInfoFromReader(reader);
                }
                else
                    return new M_Project();
            }
        }
        private M_Project GetProjectInfoFromReader(SqlDataReader reader)
        {
            M_Project info = new M_Project();
            info.ProjectID = DataConverter.CLng(reader["ProjectID"].ToString());
            info.ProjectName = reader["ProjectName"].ToString();
            info.ProjectIntro = reader["ProjectIntro"].ToString();
            info.RequireID = DataConverter.CLng(reader["RequireID"].ToString());
            info.Uid = DataConverter.CLng(reader["UserID"].ToString());
            info.StartDate = DataConverter.CDate(reader["StartDate"]);
            info.EndDate = DataConverter.CDate(reader["EndDate"]);
            info.Status = DataConverter.CLng(reader["Status"]);
            return info;
        }
        public DataTable GetProjectByUid(int Uid)
        {
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@Uid", SqlDbType.Int, 4);
            cmdParams[0].Value = Uid;
            return SqlHelper.ExecuteTable(CommandType.StoredProcedure,"PR_Project_SelByUid", cmdParams);
        }
        public int CountProjectNumByRid(int rid)
        {
            string sqlStr = "select Count(*) from ZL_Project where RequireID=@RequireID";
            SqlParameter[] parameter = new SqlParameter[1];
            parameter[0] = new SqlParameter("@RequireID", SqlDbType.Int, 4);
            parameter[0].Value =rid;
            return SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.Text, sqlStr, parameter));
        }
        public string GetProjectEndDate(int projectid)
        {
            string sqlStr = "select top 1 EndDate from ZL_ProjectWork where ProjectID=(select ProjectID from ZL_Project where ProjectID=@projectid and Status=1) order by EndDate desc";
            SqlParameter[] parameter = new SqlParameter[1];
            parameter[0] = new SqlParameter("@projectid", SqlDbType.Int, 4);
            parameter[0].Value = projectid;
            object obj = SqlHelper.ExecuteScalar(CommandType.Text, sqlStr, parameter);
            if (obj != null)
                return obj.ToString();
            else
                return string.Empty;
        }

    }
}
