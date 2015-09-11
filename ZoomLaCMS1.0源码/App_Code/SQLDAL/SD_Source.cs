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
    /// SD_Source 的摘要说明
    /// </summary>
    public class SD_Source : ID_Source
    {
        public SD_Source()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        private SqlParameter[] GetParameters(M_Source sourceInfo)
        {
            SqlParameter[] parameter = new SqlParameter[] {
                new SqlParameter("@Type", SqlDbType.VarChar,50),
                new SqlParameter("@Name", SqlDbType.VarChar,50),
                new SqlParameter("@Passed", SqlDbType.Bit),
                new SqlParameter("@onTop", SqlDbType.Bit),
                new SqlParameter("@IsElite", SqlDbType.Bit),
                new SqlParameter("@Hits", SqlDbType.Int,4),
                new SqlParameter("@LastUseTime", SqlDbType.DateTime),
                new SqlParameter("@Photo", SqlDbType.VarChar,50),
                new SqlParameter("@Intro", SqlDbType.NVarChar,255),
                new SqlParameter("@Address", SqlDbType.VarChar,50),
                new SqlParameter("@Tel", SqlDbType.VarChar,50),
                new SqlParameter("@Fax", SqlDbType.VarChar,50),
                new SqlParameter("@Mail", SqlDbType.VarChar,50),
                new SqlParameter("@Email", SqlDbType.VarChar,50),
                new SqlParameter("@ZipCode", SqlDbType.Int),
                new SqlParameter("@HomePage", SqlDbType.VarChar,50),
                new SqlParameter("@Im", SqlDbType.VarChar,50),
                new SqlParameter("@ContacterName", SqlDbType.VarChar,50), 
                new SqlParameter("@ID", SqlDbType.Int,4),
            };
            parameter[0].Value = sourceInfo.Type;
            parameter[1].Value = sourceInfo.Name;
            parameter[2].Value = sourceInfo.Passed;
            parameter[3].Value = sourceInfo.onTop;
            parameter[4].Value = sourceInfo.IsElite;
            parameter[5].Value = sourceInfo.Hits;
            parameter[6].Value = sourceInfo.LastUseTime;
            parameter[7].Value = sourceInfo.Photo;
            parameter[8].Value = sourceInfo.Intro;
            parameter[9].Value = sourceInfo.Address;
            parameter[10].Value = sourceInfo.Tel;
            parameter[11].Value = sourceInfo.Fax;
            parameter[12].Value = sourceInfo.Mail;
            parameter[13].Value = sourceInfo.Email;
            parameter[14].Value = sourceInfo.ZipCode;
            parameter[15].Value = sourceInfo.HomePage;
            parameter[16].Value = sourceInfo.Im;
            parameter[17].Value = sourceInfo.Contacter;
            parameter[18].Value = sourceInfo.SourceID;
            return parameter;
        }
        public bool Add(M_Source SourceInfo)
        {
            string strSql = "PR_Source_Insert";
            SqlParameter[] parameter = GetParameters(SourceInfo);
            return SqlHelper.ExecuteProc(strSql, parameter);
        }
        public bool DeleteByID(string sourceId)
        {

            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@ID", SqlDbType.NVarChar, 100);
            cmdParams[0].Value = sourceId;
            return SqlHelper.ExecuteProc("PR_Source_Delete", cmdParams);
        }
        public bool Update(M_Source SourceInfo)
        {

            SqlParameter[] cmdParams = GetParameters(SourceInfo); ;
            return SqlHelper.ExecuteProc("PR_Source_Update", cmdParams);
        }
        public M_Source GetSourceByid(int sId)
        {
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@ID", SqlDbType.Int,4);
            cmdParams[0].Value = sId;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "PR_Source_GetSourceInfoByID", cmdParams))
          {
              if (reader.Read())
                {
                    return GetSourceInfoFromReader(reader);
                }
                else
                    return new M_Source();
          }         
        }
        private M_Source GetSourceInfoFromReader(SqlDataReader reader)
        {
            M_Source info = new M_Source();
            info.Type = reader["Type"].ToString();
            info.Name = reader["Name"].ToString();
            info.Passed = DataConverter.CBool(reader["Passed"].ToString());
            info.onTop = DataConverter.CBool(reader["onTop"].ToString());
            info.IsElite = DataConverter.CBool(reader["IsElite"].ToString());
            info.Hits = DataConverter.CLng(reader["Hits"].ToString());
            info.LastUseTime = DataConverter.CDate(reader["LastUseTime"].ToString());
            info.Photo = reader["Photo"].ToString();
            info.Intro = reader["Intro"].ToString();
            info.Address = reader["Address"].ToString();
            info.Tel = reader["Tel"].ToString();
            info.Fax = reader["Fax"].ToString();
            info.Mail = reader["Mail"].ToString();
            info.Email = reader["Email"].ToString();
            info.ZipCode = DataConverter.CLng(reader["ZipCode"].ToString());
            info.HomePage = reader["HomePage"].ToString();
            info.Im =reader["Im"].ToString();
            info.Contacter = reader["Contacter"].ToString();
            reader.Close();
            return info;
        }
        public DataTable GetSourceAll()
        {
            return SqlHelper.ExecuteTable(CommandType.StoredProcedure, "PR_Source_GetSourceInfo", null);
        }
        public DataTable SearchSource(string sourcekey)
        {
            string str = "select * from [ZL_Source] where Name like '%" + sourcekey + "%'";
            return SqlHelper.ExecuteTable(CommandType.Text, str, null);
        }
     }
}
