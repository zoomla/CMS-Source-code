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
    /// B_ExTeacher 的摘要说明
    /// </summary>
    public class B_ExTeacher
    {
        public B_ExTeacher()
        {
            strTableName = initmod.TbName;
            PK = initmod.PK;
        }
        public string PK, strTableName;
        private M_ExTeacher initmod = new M_ExTeacher();
		/// <summary>
		///添加记录
		/// </summary>
		/// <param name="ExTeacher"></param>
		/// <returns></returns>
		public int GetInsert(M_ExTeacher model)
        {
          return DBCenter.Insert(model);
        }

		/// <summary>
		///更新记录
		/// </summary>
		/// <param name="ExTeacher"></param>
		/// <returns></returns>
		public bool GetUpdate(M_ExTeacher model)
        {
           return DBCenter.UpdateByID(model,model.ID);
        }

		/// <summary>
		///删除记录
		/// </summary>
		/// <param name="ExTeacher"></param>
		/// <returns></returns>
		public bool DeleteByGroupID(int ExTeacherID)
        {
            return Sql.Del(strTableName, ExTeacherID);
        }

		/// <summary>
		///查找一条记录
		/// </summary>
		/// <param name="ExTeacher"></param>
		/// <returns></returns>
		public M_ExTeacher GetSelect(int ExTeacherID)
        {
            string sqlStr = "SELECT [ID],[TClsss],[TName],[Post],[Teach],[FileUpload],[Remark],[CreatTime],[AddUser] FROM [dbo].[ZL_ExTeacher] WHERE [ID] = @ID";
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@ID", SqlDbType.Int, 4);
            cmdParams[0].Value = ExTeacherID;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sqlStr, cmdParams))
            {
                if (reader.Read())
                {
                    return GetInfoFromReader(reader);
                }
                else
                {
                    return new M_ExTeacher();
                }
            }
        }
        private static M_ExTeacher GetInfoFromReader(SqlDataReader rdr)
        {
            M_ExTeacher info = new M_ExTeacher();
            info.ID = DataConverter.CLng(rdr["ID"].ToString());
            info.TClsss = DataConverter.CLng(rdr["TClsss"].ToString());
            info.TName = rdr["TName"].ToString();
            info.Post = rdr["Post"].ToString();
            info.Teach = rdr["Teach"].ToString();
            info.FileUpload = rdr["FileUpload"].ToString();
            info.Remark = rdr["Remark"].ToString();
            info.CreatTime = DataConverter.CDate(rdr["CreatTime"].ToString());
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
        public DataTable Sel_All()
        {
            string sql = "SELECT A.*,B.TypeName FROM "+strTableName+ " A LEFT JOIN ZL_Exam_PaperNode B ON A.TClsss=B.ID";
            return SqlHelper.ExecuteTable(sql);
        }

        public DataTable SelByIDS(string ids)
        {
            SafeSC.CheckIDSEx(ids);
            string sql = "SELECT * FROM " + strTableName + " WHERE ID IN ("+ids+")";
            return SqlHelper.ExecuteTable(CommandType.Text,sql);
        }

	}
}
