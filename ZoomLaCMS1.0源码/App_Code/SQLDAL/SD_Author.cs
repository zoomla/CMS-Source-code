
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
    using System.Collections;
    using ZoomLa.Common;

    /// <summary>
    /// SD_Author 的摘要说明
    /// </summary>
    public class SD_Author : ID_Author
    {
        private static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["Connection String"].ConnectionString;
        public SD_Author()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        /// <returns></returns>
        public bool Add(M_Author Authorfo)
        {
            string strSql = "PR_Author_Add";
            SqlParameter[] parameter = GetParameters(Authorfo);
            return SqlHelper.ExecuteProc(strSql, parameter);
        }
        /// <summary>
        /// 删除操作:批量和单个
        /// </summary>
        /// <param name="authorId"></param>
        /// <returns></returns>
        public bool DeleteByID(string authorId)
        {
           
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@ID", SqlDbType.NVarChar,100);
            cmdParams[0].Value = authorId;
            return SqlHelper.ExecuteProc("PR_Author_Delete",cmdParams);
        }
        public bool Update(M_Author Authorfo)
        {
          
            SqlParameter[] cmdParams = GetParameters(Authorfo); ;
            return SqlHelper.ExecuteProc("PR_Author_Update", cmdParams);
        }
        /// <summary>
        /// 从Sqldatareader读取作者信息
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private M_Author GetAuthorInfoFromReader(SqlDataReader reader)
        {
            M_Author info = new M_Author();
            info.AuthorID = DataConverter.CLng(reader["UserID"].ToString());
            info.AuthorType = reader["Type"].ToString();
            info.AuthorName = reader["Name"].ToString();
            info.AuthorPassed = DataConverter.CBool(reader["Passed"].ToString());
            info.AuthoronTop = DataConverter.CBool(reader["onTop"].ToString());
            info.AuthorIsElite =DataConverter.CBool(reader["IsElite"].ToString());
            info.AuthorHits = DataConverter.CLng(reader["Hits"].ToString());
            info.AuthorLastUseTime = DataConverter.CDate(reader["LastUseTime"].ToString());
            info.AuthorTemplateID = DataConverter.CLng(reader["TemplateID"].ToString());
            info.AuthorPhoto = reader["Photo"].ToString();
            info.AuthorIntro = reader["Intro"].ToString();
            info.AuthorAddress =reader["Address"].ToString();
            info.AuthorTel =reader["Tel"].ToString();
            info.AuthorFax= reader["Fax"].ToString();
            info.AuthorMail = reader["Mail"].ToString();
            info.AuthorEmail= reader["Email"].ToString();
            info.AuthorZipCode = DataConverter.CLng(reader["ZipCode"].ToString());
            info.AuthorHomePage = reader["HomePage"].ToString();
            info.AuthorIm= reader["Im"].ToString();
            info.AuthorSex =Convert.ToInt16(reader["Sex"].ToString());//smaallint
            info.AuthorBirthDay = DataConverter.CDate(reader["BirthDay"].ToString());
            info.AuthorCompany =reader["Company"].ToString();
            info.AuthorDepartment =reader["Department"].ToString();
            reader.Close();
            return info;
        }
        public M_Author GetAuthorByid(int authorId)
        {
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@ID", SqlDbType.Int,4);
            cmdParams[0].Value = authorId;
          using (SqlDataReader reader =SqlHelper.ExecuteReader(CommandType.StoredProcedure,"PR_Author_GetAuthorInfoByID", cmdParams))
          {
              if (reader.Read())
                {
                    return GetAuthorInfoFromReader(reader);
                }
                else
                    return new M_Author();
          }         
        }
        private SqlParameter[] GetParameters(M_Author authorInfo)
        {
            SqlParameter[] parameter = new SqlParameter[] {
                new SqlParameter("@UserId", SqlDbType.Int),
                //new SqlParameter("@ID", SqlDbType.Int),
                new SqlParameter("@Name", SqlDbType.VarChar,50),
                new SqlParameter("@Type", SqlDbType.VarChar,50),
                new SqlParameter("@Passed", SqlDbType.Bit),
                new SqlParameter("@onTop", SqlDbType.Bit),
                new SqlParameter("@IsElite", SqlDbType.Bit),
                new SqlParameter("@Hits", SqlDbType.Int,4),
                new SqlParameter("@LastUseTime", SqlDbType.DateTime,8),
                new SqlParameter("@TemplateID", SqlDbType.Int),
                new SqlParameter("@Photo", SqlDbType.VarChar,255),
                new SqlParameter("@Intro", SqlDbType.NVarChar,255),
                new SqlParameter("@Address", SqlDbType.VarChar,50),
                new SqlParameter("@Tel", SqlDbType.VarChar,50),
                new SqlParameter("@Fax", SqlDbType.VarChar,50),
                new SqlParameter("@Mail", SqlDbType.VarChar,50),
                new SqlParameter("@Email", SqlDbType.VarChar,50),
                new SqlParameter("@ZipCode", SqlDbType.Int),
                new SqlParameter("@HomePage", SqlDbType.VarChar,50),
                new SqlParameter("@Im", SqlDbType.VarChar,50),
                new SqlParameter("@Sex",SqlDbType.SmallInt),
                new SqlParameter("@BirthDay",SqlDbType.DateTime,8),
                new SqlParameter("@Company",SqlDbType.VarChar,50),
                new SqlParameter("@Department",SqlDbType.VarChar,50),
                new SqlParameter("@ID", SqlDbType.Int)
            };
            parameter[0].Value = authorInfo.AuthorID;//?
            parameter[1].Value = authorInfo.AuthorName;
            parameter[2].Value = authorInfo.AuthorType;
            parameter[3].Value = authorInfo.AuthorPassed;
            parameter[4].Value = authorInfo.AuthoronTop;
            parameter[5].Value = authorInfo.AuthorIsElite;
            parameter[6].Value = authorInfo.AuthorHits;
            parameter[7].Value = authorInfo.AuthorLastUseTime;//?
            parameter[8].Value = authorInfo.AuthorTemplateID;
            parameter[9].Value = authorInfo.AuthorPhoto;
            parameter[10].Value = authorInfo.AuthorIntro;
            parameter[11].Value = authorInfo.AuthorAddress;
            parameter[12].Value = authorInfo.AuthorTel;
            parameter[13].Value = authorInfo.AuthorFax;
            parameter[14].Value = authorInfo.AuthorMail;
            parameter[15].Value = authorInfo.AuthorEmail;
            parameter[16].Value = authorInfo.AuthorZipCode;
            parameter[17].Value = authorInfo.AuthorHomePage;
            parameter[18].Value = authorInfo.AuthorIm;
            parameter[19].Value = authorInfo.AuthorSex;
            parameter[20].Value = authorInfo.AuthorBirthDay;
            parameter[21].Value = authorInfo.AuthorCompany;
            parameter[22].Value = authorInfo.AuthorDepartment;
            parameter[23].Value = authorInfo.ID;//?
            return parameter;
        }
        public DataTable GetAuthorPage(int authorid, int Cpage, int PageSize)
        {
            string strSql = "up_Page2005";
            SqlParameter[] sp = new SqlParameter[]{
                new SqlParameter("@TableName",SqlDbType.NVarChar),
                new SqlParameter("@Fields",SqlDbType.NVarChar),
                new SqlParameter("@OrderField",SqlDbType.NVarChar),
                new SqlParameter("@sqlWhere",SqlDbType.NVarChar),
                new SqlParameter("@pageSize",SqlDbType.Int),
                new SqlParameter("@pageIndex",SqlDbType.Int),
                new SqlParameter("@TotalPage",SqlDbType.Int)
            };
            sp[0].Value = "ZL_Author";
            sp[1].Value = "*";
            sp[2].Value = "ID";
            sp[3].Value = "";
            sp[4].Value = PageSize;
            sp[5].Value = Cpage;
            sp[6].Value = 0;
            sp[6].Direction = ParameterDirection.Output;
            return SqlHelper.ExecuteTable(CommandType.StoredProcedure, strSql, sp);
        }
        public DataTable GetAuthorPage(int startindex, int pageSize)
        {
            string strSql = "select * from ZL_Author order by(ID) asc";
            //string strSql = "select * from ZL_Author where posttopic=@topicid order by(postid) asc";
            //SqlParameter[] sp = new SqlParameter[] {
            //    new SqlParameter("@topicid",SqlDbType.Int)
            //};
            //sp[0].Value = topicid;
            return ExecuteTable(CommandType.Text, strSql, startindex, pageSize,null);
        }
        //分页数据查询
        public static DataTable ExecuteTable(CommandType cmdType, string cmdText, int startindex, int pageSize, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            int i = startindex;
            int j = pageSize;
            //startindex = 2;
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                PrepareCommand(cmd, connection, cmdType, cmdText, commandParameters);
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet, startindex, pageSize, "Result");
                cmd.Parameters.Clear();
                int y = dataSet.Tables["Result"].Rows.Count;
                if (dataSet.Tables.Count > 0)
                {
                    return dataSet.Tables["Result"];
                }
                return null;
            }
        }
        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, CommandType cmdType, string cmdText, SqlParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            cmd.CommandTimeout = 60;            
            cmd.CommandType = cmdType;
            if (cmdParms != null)
            {
                foreach (SqlParameter parameter in cmdParms)
                {
                    cmd.Parameters.Add(parameter);
                }
            }
        }
        public DataTable GetSourceAll()
        {
            string strSql = "select * from ZL_Author order by(ID) asc";
            return SqlHelper.ExecuteTable(CommandType.Text, strSql, null);
        }
    }
}
