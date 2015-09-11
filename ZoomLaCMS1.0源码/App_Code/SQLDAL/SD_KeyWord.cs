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

namespace ZoomLa.SQLDAL
{

    /// <summary>
    /// SD_KeyWord 的摘要说明
    /// </summary>
    public class SD_KeyWord :ID_KeyWord
    {
        public SD_KeyWord()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        private SqlParameter[] GetParameters(M_KeyWord sourceInfo)
        {
            SqlParameter[] parameter = new SqlParameter[] {
                new SqlParameter("@KeywordText", SqlDbType.NVarChar,200),
                new SqlParameter("@KeywordType", SqlDbType.Int),
                new SqlParameter("@Priority", SqlDbType.Int),
                new SqlParameter("@Hits", SqlDbType.Int),
                new SqlParameter("@LastUseTime", SqlDbType.DateTime,8),
                new SqlParameter("@ArrayGeneralId",SqlDbType.NText),
                new SqlParameter("@QuoteTimes", SqlDbType.Int),
                new SqlParameter("@KeywordID", SqlDbType.Int) 
            };
            parameter[0].Value = sourceInfo.KeywordText;
            parameter[1].Value = sourceInfo.KeywordType;
            parameter[2].Value = sourceInfo.Priority;
            parameter[3].Value = sourceInfo.Hits;
            parameter[4].Value = sourceInfo.LastUseTime;
            parameter[5].Value = sourceInfo.ArrGeneralID;
            parameter[6].Value = sourceInfo.QuoteTimes;
            parameter[7].Value = sourceInfo.KeyWordID;   
            return parameter;
        }
        public bool Add(M_KeyWord KeyWordInfo)
        {
            string strSql = "PR_Keywords_Add";
            SqlParameter[] parameter = GetParameters(KeyWordInfo);
            return SqlHelper.ExecuteProc(strSql, parameter);
        }
        public bool DeleteByID(string keywordId)
        {

            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@KeywordId", SqlDbType.NVarChar,4000);
            cmdParams[0].Value = keywordId;
            return SqlHelper.ExecuteProc("PR_Keywords_Delete", cmdParams);
        }
        public bool Update(M_KeyWord keywordInfo)
        {
            SqlParameter[] cmdParams = GetParameters(keywordInfo); ;
            return SqlHelper.ExecuteProc("PR_Keywords_Update", cmdParams);
        }
        public M_KeyWord GetKeyWordByid(int kId)
        {
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@KeywordID", SqlDbType.Int, 4);
            cmdParams[0].Value = kId;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "PR_Keywords_GetById", cmdParams))
            {
                if (reader.Read())
                {
                    return GetKeyWordInfoFromReader(reader);
                }
                else
                    return new M_KeyWord();
            }
        }
        private M_KeyWord GetKeyWordInfoFromReader(SqlDataReader reader)
        {
            M_KeyWord info = new M_KeyWord();
            info.KeywordText = reader["KeywordText"].ToString();
            info.KeywordType = DataConverter.CLng(reader["KeywordType"].ToString());
            info.Priority = DataConverter.CLng(reader["Priority"].ToString());
            info.Hits = DataConverter.CLng(reader["Hits"].ToString());
            info.LastUseTime = DataConverter.CDate(reader["LastUseTime"]);
            info.ArrGeneralID = reader["ArrGeneralID"].ToString();
            info.QuoteTimes = DataConverter.CLng(reader["QuoteTimes"]);
            reader.Close();
            return info;
        }
        public DataTable GetKeyWordAll()
        {
            return SqlHelper.ExecuteTable(CommandType.StoredProcedure, "PR_KeyWords_GetKeyWordnfo", null);

        }
    }
}
