namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using ZoomLa.SQLDAL;
    using System.Data.SqlClient;

    /// <summary>
    /// B_CreateJS 的摘要说明
    /// </summary>
    public class B_CreateJS
    {
        public B_CreateJS()
        {
            strTableName = initmod.TbName;
            PK = initmod.PK;
        }
        public string PK, strTableName;
        private M_CreateJS initmod = new M_CreateJS();
        /// <summary>
        ///添加记录
        /// </summary>
        /// <param name="CreateJS"></param>
        /// <returns></returns>
        public int GetInsert(M_CreateJS createJS)
        {
            string sqlStr = "INSERT INTO [dbo].[ZL_CreateJS] ([Jsname],[JsReadme],[ContentType],[JsFileName],[JsType],[JsXmlContent]) VALUES (@Jsname,@JsReadme,@ContentType,@JsFileName,@JsType,@JsXmlContent);select @@IDENTITY";
            SqlParameter[] cmdParams = initmod.GetParameters(createJS);
            return SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.Text, sqlStr, cmdParams));
        }

        /// <summary>
        ///更新记录
        /// </summary>
        /// <param name="CreateJS"></param>
        /// <returns></returns>
        public bool GetUpdate(M_CreateJS createJS)
        {
            string sqlStr = "UPDATE [dbo].[ZL_CreateJS] SET [Jsname] = @Jsname,[JsReadme] = @JsReadme,[ContentType] = @ContentType,[JsFileName] = @JsFileName,[JsType] = @JsType,[JsXmlContent] = @JsXmlContent WHERE [id] = @id";
            SqlParameter[] cmdParams = initmod.GetParameters(createJS);
            return SqlHelper.ExecuteSql(sqlStr, cmdParams);
        }

        /// <summary>
        ///删除记录
        /// </summary>
        /// <param name="CreateJS"></param>
        /// <returns></returns>
        public bool DeleteByGroupID(int CreateJSID)
        {
            return Sql.Del(strTableName, CreateJSID);
        }

        /// <summary>
        ///查找一条记录
        /// </summary>
        /// <param name="CreateJS"></param>
        /// <returns></returns>
        public M_CreateJS GetSelect(int CreateJSID)
        {
            string sqlStr = "SELECT [id],[Jsname],[JsReadme],[ContentType],[JsFileName],[JsType],[JsXmlContent] FROM [dbo].[ZL_CreateJS] WHERE [id] = @id";
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@id", SqlDbType.Int, 4);
            cmdParams[0].Value = CreateJSID;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sqlStr, cmdParams))
            {
                if (reader.Read())
                {
                    return GetInfoFromReader(reader);
                }
                else
                {
                    return new M_CreateJS();
                }
            }
        }
        private static M_CreateJS GetInfoFromReader(SqlDataReader rdr)
        {
            M_CreateJS info = new M_CreateJS();
            info.id = DataConverter.CLng(rdr["id"].ToString());
            info.Jsname = rdr["Jsname"].ToString();
            info.JsReadme = rdr["JsReadme"].ToString();
            info.ContentType = DataConverter.CLng(rdr["ContentType"].ToString());
            info.JsFileName = rdr["JsFileName"].ToString();
            info.JsType = DataConverter.CLng(rdr["JsType"].ToString());
            info.JsXmlContent = rdr["JsXmlContent"].ToString();
            rdr.Close();
            rdr.Dispose();
            return info;
        }
        /// <summary>
        ///返回所有记录
        /// </summary>
        /// <returns></returns>
        public DataTable Select_All()
        {
            return Sql.Sel(strTableName);
        }
    }
}