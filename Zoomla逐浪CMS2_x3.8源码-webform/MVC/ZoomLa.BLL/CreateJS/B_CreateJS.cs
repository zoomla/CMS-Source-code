namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using ZoomLa.SQLDAL;
    using System.Data.SqlClient;
    using SQLDAL.SQL;
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
        public int GetInsert(M_CreateJS model)
        {
          return DBCenter.Insert(model);
        }

        /// <summary>
        ///更新记录
        /// </summary>
        /// <param name="CreateJS"></param>
        /// <returns></returns>
        public bool GetUpdate(M_CreateJS model)
        {
          return DBCenter.UpdateByID(model,model.id);
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
        public PageSetting SelPage(int cpage, int psize)
        {
            PageSetting setting = PageSetting.Single(cpage, psize, strTableName, PK, "");
            DBCenter.SelPage(setting);
            return setting;
        }
    }
}