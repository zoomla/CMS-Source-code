namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using ZoomLa.SQLDAL;
    using System.Data.SqlClient;
    using ZoomLa.SQLDAL.SQL;
	
    public class B_Paper_Questions
    {
        public B_Paper_Questions()
        {
            strTableName = initmod.TbName;
            PK = initmod.PK;
        }
        public string PK, strTableName;
        private M_Paper_Questions initmod = new M_Paper_Questions();
		
		/// <summary>
		///添加记录
		/// </summary>
		/// <param name="Paper_Questions"></param>
		/// <returns></returns>
		public int GetInsert(M_Paper_Questions model)
        {
          return DBCenter.Insert(model);
        }

		/// <summary>
		///更新记录
		/// </summary>
		/// <param name="Paper_Questions"></param>
		/// <returns></returns>
		public bool GetUpdate(M_Paper_Questions model)
        {
          return DBCenter.UpdateByID(model,model.ID);
        }

		/// <summary>
		///删除记录
		/// </summary>
		/// <param name="Paper_Questions"></param>
		/// <returns></returns>
		public bool DeleteByGroupID(int Paper_QuestionsID)
        {
            return Sql.Del(strTableName, Paper_QuestionsID);
        }

		/// <summary>
		///查找一条记录
		/// </summary>
		/// <param name="Paper_Questions"></param>
		/// <returns></returns>
		public M_Paper_Questions GetSelect(int Paper_QuestionsID)
        {
            string sqlStr = "SELECT [ID],[QuestionTitle],[PaperID],[OrderBy],[QuestionType],[Subtitle],[Course],[QuesNum],[Remark],[QuestIDs],[CreateTime],[AddUser] FROM [dbo].[ZL_Paper_Questions] WHERE [ID] = @ID";
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@ID", SqlDbType.Int, 4);
            cmdParams[0].Value = Paper_QuestionsID;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sqlStr, cmdParams))
            {
                if (reader.Read())
                {
                    return GetInfoFromReader(reader);
                }
                else
                {
                    return new M_Paper_Questions();
                }
            }
        }
        private static M_Paper_Questions GetInfoFromReader(SqlDataReader rdr)
        {
            M_Paper_Questions info = new M_Paper_Questions();
            info.ID = DataConverter.CLng(rdr["ID"].ToString());
            info.QuestionTitle = rdr["QuestionTitle"].ToString();
            info.PaperID = DataConverter.CLng(rdr["PaperID"].ToString());
            info.OrderBy = DataConverter.CLng(rdr["OrderBy"].ToString());
            info.QuestionType = DataConverter.CLng(rdr["QuestionType"].ToString());
            info.Subtitle = rdr["Subtitle"].ToString();
            info.Course = DataConverter.CDouble(rdr["Course"].ToString());
            info.QuesNum = DataConverter.CLng(rdr["QuesNum"].ToString());
            info.Remark = rdr["Remark"].ToString();
            info.QuestIDs = rdr["QuestIDs"].ToString();
            info.CreateTime = DataConverter.CDate(rdr["CreateTime"].ToString());
            info.AddUser = DataConverter.CLng(rdr["AddUser"].ToString());
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
        /// <summary>
        /// 通过试卷ID查询
        /// </summary>
        /// <param name="paperid">试卷ID</param>
        /// <returns></returns>
        public DataTable Select_PaperId(int paperid)
        {
            return Sql.Sel(strTableName, "PaperID=" + paperid, null);
        }
	}
}
