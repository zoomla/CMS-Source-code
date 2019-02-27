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
using ZoomLa.Common;
using System.Data.SqlClient;
/// <summary>
/// SD_Question 的摘要说明
/// </summary>

namespace ZoomLa.SQLDAL
{
    public class SD_Question:ID_Question
    {
        public SD_Question()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        public bool AddQuestiony(M_Question m_Question)
        {
            string strSql = "PR_Questiony_Add";
            SqlParameter[] parameter = GetParameters(m_Question);
            return SqlHelper.ExecuteProc(strSql, parameter);
        }
        public bool UpdateQuestion(M_Question m_Question)
        {
            SqlParameter[] cmdParams = GetParameters(m_Question); ;
            return SqlHelper.ExecuteProc("PR_Questiony_Update", cmdParams);
        }
        public M_Question GetQuestionByQid(int QuestionID)
        {
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@ID", SqlDbType.Int, 4);
            cmdParams[0].Value = QuestionID;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "PR_Question_GetQuestionByID", cmdParams))
            {
                if (reader.Read())
                {
                    return GetQuestionInfoFromReader(reader);
                }
                else
                    return new M_Question();
            }
        }
        private M_Question GetQuestionInfoFromReader(SqlDataReader reader)
        {
            M_Question info = new M_Question();
            info.QuestionID = DataConverter.CLng(reader["QuestionID"].ToString());
            info.SurveryID = DataConverter.CLng(reader["SurveryID"].ToString());
            info.TypeID = DataConverter.CLng(reader["TypeID"].ToString());
            info.QuestionTitle = reader["QuestionTitle"].ToString();
            info.QuestionContent =reader["QuestionContent"].ToString();
            info.QuestionCreateTime = DataConverter.CDate(reader["QuestionCreateTime"]);
            info.ManagerID = DataConverter.CLng(reader["ManagerID"]);         
            return info;
        }
        public bool DelQuestionByQid(int QuestionID)
        {
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@ID", SqlDbType.Int, 4);
            cmdParams[0].Value = QuestionID;
            return SqlHelper.ExecuteProc("PR_Question_Delete", cmdParams);
        }
        private SqlParameter[] GetParameters(M_Question m_Question)
        {
            SqlParameter[] parameter = new SqlParameter[] {
                new SqlParameter("@QuestionID", SqlDbType.Int,4),
                new SqlParameter("@SurveryID", SqlDbType.Int,4),
                new SqlParameter("@TypeID", SqlDbType.Int,4),              
                new SqlParameter("@QuestionTitle", SqlDbType.NVarChar,255),
                new SqlParameter("@QuestionContent", SqlDbType.Text),
                new SqlParameter("@QuestionCreateTime", SqlDbType.DateTime,8),
                new SqlParameter("@ManagerID", SqlDbType.Int,4)                
            };
            parameter[0].Value = m_Question.QuestionID;
            parameter[1].Value = m_Question.SurveryID;
            parameter[2].Value = m_Question.TypeID;
            parameter[3].Value = m_Question.QuestionTitle;
            parameter[4].Value = m_Question.QuestionContent;
            parameter[5].Value = m_Question.QuestionCreateTime;
            parameter[6].Value = m_Question.ManagerID;         
            return parameter;
        }
    }
}
