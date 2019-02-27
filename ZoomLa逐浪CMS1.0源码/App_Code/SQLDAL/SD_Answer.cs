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
/// SD_Answer 的摘要说明
/// </summary>

namespace ZoomLa.SQLDAL
{
    public class SD_Answer:ID_Answer
    {
        public SD_Answer()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        public bool AddAnswer(M_Answer m_Answer)
        {
            string strSql = "PR_Answer_Add";
            SqlParameter[] parameter = GetParameters(m_Answer);
            return SqlHelper.ExecuteProc(strSql, parameter);
        }
        public bool UpdateAnswer(M_Answer m_Answer)
        {
            SqlParameter[] cmdParams = GetParameters(m_Answer); ;
            return SqlHelper.ExecuteProc("PR_Answer_Update", cmdParams);
        }
        public M_Answer GetAnswerByAid(int AnswerID)
        {
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@ID", SqlDbType.Int, 4);
            cmdParams[0].Value = AnswerID;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "PR_Answer_GetAnswerByAid", cmdParams))
            {
                if (reader.Read())
                {
                    return GetAnswerInfoFromReader(reader);
                }
                else
                    return new M_Answer();
            }
        }
        private M_Answer GetAnswerInfoFromReader(SqlDataReader reader)
        {
            M_Answer info = new M_Answer();
            info.AnswerID = DataConverter.CLng(reader["AnswerID"].ToString());
            info.SurveryID = DataConverter.CLng(reader["SurveryID"].ToString());
            info.QuestionID = DataConverter.CLng(reader["QuestionID"].ToString());
            info.OptionID = DataConverter.CLng(reader["OptionID"].ToString());
            info.AnswerContent = reader["AnswerContent"].ToString();
            info.UserID = DataConverter.CLng(reader["UserID"]);         
            return info;
        }
        public bool DelAnswerByAid(int AnswerID)
        {
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@ID", SqlDbType.Int, 4);
            cmdParams[0].Value = AnswerID;
            return SqlHelper.ExecuteProc("PR_Answer_DeleteAid", cmdParams);
        }
        private SqlParameter[] GetParameters(M_Answer m_Answer)
        {
            SqlParameter[] parameter = new SqlParameter[] {
                new SqlParameter("@AnswerID", SqlDbType.BigInt),
                new SqlParameter("@SurveryID", SqlDbType.Int,4),
                new SqlParameter("@QuestionID", SqlDbType.Int,4),              
                new SqlParameter("@OptionID", SqlDbType.Int,4),
                new SqlParameter("@AnswerContent", SqlDbType.Text),
                new SqlParameter("@UserID", SqlDbType.Int,4)
                   
            };
            parameter[0].Value = m_Answer.AnswerID;
            parameter[1].Value = m_Answer.SurveryID;
            parameter[2].Value = m_Answer.QuestionID;
            parameter[3].Value = m_Answer.OptionID;
            parameter[4].Value = m_Answer.AnswerContent;
            parameter[5].Value = m_Answer.UserID;           
            return parameter;
        }
    }
}
