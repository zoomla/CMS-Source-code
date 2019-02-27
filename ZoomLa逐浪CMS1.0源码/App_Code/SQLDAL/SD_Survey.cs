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
/// SD_Survey 的摘要说明
/// </summary>

namespace ZoomLa.SQLDAL
{
    public class SD_Survey:ID_Survey
    {
        public SD_Survey()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        public bool AddSurvey(M_Survey m_Survey)
        {
            string strSql = "PR_Survey_Add";
            SqlParameter[] parameter = GetParameters(m_Survey);
            return SqlHelper.ExecuteProc(strSql, parameter);
        }
        public bool UpdateSurvey(M_Survey m_Survey)
        {

            SqlParameter[] cmdParams = GetParameters(m_Survey); ;
            return SqlHelper.ExecuteProc("PR_Survey_Update", cmdParams);
        }
        public M_Survey GetSurveyBySid(int SurveyID)
        {
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@ID", SqlDbType.Int, 4);
            cmdParams[0].Value = SurveyID;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "PR_Survey_GetSurveyByID", cmdParams))
            {
                if (reader.Read())
                {
                    return GetSurveyInfoFromReader(reader);
                }
                else
                    return new M_Survey();
            }
        }
        private M_Survey  GetSurveyInfoFromReader(SqlDataReader reader)
        {
            M_Survey info = new M_Survey();
            info.SurveyID = DataConverter.CLng(reader["SurveyID"].ToString());
            info.SurveyName = reader["SurveyName"].ToString();
            info.Description = reader["Description"].ToString();
            info.IPRepeat = DataConverter.CLng(reader["IPRepeat"].ToString());
            info.CreateDate = DataConverter.CDate(reader["CreateDate"].ToString());
            info.EndTime = DataConverter.CDate(reader["EndTime"]);
            info.IsOpen = DataConverter.CLng(reader["IsOpen"]);
            info.NeedLogin = DataConverter.CLng(reader["NeedLogin"]);
            info.Template = reader["Template"].ToString();
            return info;
        }
        public bool DelSurveyBySid(int SurveyID)
        {
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@ID", SqlDbType.Int, 4);
            cmdParams[0].Value = SurveyID;
            return SqlHelper.ExecuteProc("PR_Survey_Delete", cmdParams);
        }
        private SqlParameter[] GetParameters(M_Survey m_Survey)
        {
            SqlParameter[] parameter = new SqlParameter[] {
                new SqlParameter("@SurveyID", SqlDbType.Int),
                new SqlParameter("@SurveyName", SqlDbType.NVarChar,50),
                new SqlParameter("@Description", SqlDbType.NText),              
                new SqlParameter("@IPRepeat", SqlDbType.Int,4),
                new SqlParameter("@CreateDate", SqlDbType.DateTime,8),
                new SqlParameter("@EndTime", SqlDbType.DateTime,8),
                new SqlParameter("@IsOpen", SqlDbType.Int,4),
                new SqlParameter("@NeedLogin", SqlDbType.Int,4),
                new SqlParameter("@Template", SqlDbType.NVarChar,255)
                
            };
            parameter[0].Value = m_Survey.SurveyID;
            parameter[1].Value = m_Survey.SurveyName;
            parameter[2].Value = m_Survey.Description;
            parameter[3].Value = m_Survey.IPRepeat;
            parameter[4].Value = m_Survey.CreateDate;
            parameter[5].Value = m_Survey.EndTime;
            parameter[6].Value = m_Survey.IsOpen;
            parameter[7].Value = m_Survey.NeedLogin;
            parameter[8].Value = m_Survey.Template;
            return parameter;
        }
    }
}
