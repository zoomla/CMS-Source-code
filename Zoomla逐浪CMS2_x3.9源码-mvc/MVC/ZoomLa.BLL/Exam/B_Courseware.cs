namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using ZoomLa.SQLDAL;
    using System.Data.SqlClient;
	
	/// <summary>
    /// B_Courseware 的摘要说明
    /// </summary>
    public class B_Courseware
    {
        public B_Courseware()
        {
            strTableName = initmod.TbName;
            PK = initmod.PK;
        }
        public string PK, strTableName;
        private M_Courseware initmod = new M_Courseware();
		/// <summary>
		///添加记录
		/// </summary>
		/// <param name="Courseware"></param>
		/// <returns></returns>
		public int GetInsert(M_Courseware courseware)
        {
            string sqlStr = "INSERT INTO [dbo].[ZL_Courseware] ([Courseware],[CoursNum],[Listen],[UpdateTime],[FileUrl],[CourseID],[Speaker],[SJName],[Status],[Description],[Classification],[CoursType],[CreationTime]) VALUES (@Courseware,@CoursNum,@Listen,@UpdateTime,@FileUrl,@CourseID,@Speaker,@SJName,@Status,@Description,@Classification,@CoursType,@CreationTime);select @@IDENTITY";
            SqlParameter[] cmdParams = courseware.GetParameters();
            return SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.Text, sqlStr, cmdParams));
        }

		/// <summary>
		///更新记录
		/// </summary>
		/// <param name="Courseware"></param>
		/// <returns></returns>
		public bool GetUpdate(M_Courseware courseware)
        {
            string sqlStr = "UPDATE [dbo].[ZL_Courseware] SET [Courseware] = @Courseware,[CoursNum] = @CoursNum,[Listen] = @Listen,[UpdateTime] = @UpdateTime,[FileUrl] = @FileUrl,[CourseID] = @CourseID,[Speaker]=@Speaker,[SJName]=@SJName,[Status]=@Status,[Description]=@Description,[Classification]=@Classification,[CoursType]=@CoursType,[CreationTime]=@CreationTime WHERE [ID] = @ID";
            SqlParameter[] cmdParams = courseware.GetParameters();
            return SqlHelper.ExecuteSql(sqlStr, cmdParams);
        }

		/// <summary>
		///删除记录
		/// </summary>
		/// <param name="Courseware"></param>
		/// <returns></returns>
		public bool DeleteByGroupID(int CoursewareID)
        {
            return Sql.Del(strTableName, CoursewareID);
        }

		/// <summary>
		///查找一条记录
		/// </summary>
		/// <param name="Courseware"></param>
		/// <returns></returns>
		public M_Courseware GetSelect(int CoursewareID)
        {
            string sqlStr = "SELECT [ID],[Courseware],[CoursNum],[Listen],[UpdateTime],[FileUrl],[CourseID],[Speaker],[SJName],[Status],[Description],[Classification],[CoursType],[CreationTime] FROM [dbo].[ZL_Courseware] WHERE [ID] = @ID";
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@ID", SqlDbType.Int, 4);
            cmdParams[0].Value = CoursewareID;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sqlStr, cmdParams))
            {
                if (reader.Read())
                {
                    return GetInfoFromReader(reader);
                }
                else
                {
                    return new M_Courseware();
                }
            }
        }
        private static M_Courseware GetInfoFromReader(SqlDataReader rdr)
        {
            M_Courseware info = new M_Courseware();
            info.ID = DataConverter.CLng(rdr["ID"].ToString());
            info.Courseware = rdr["Courseware"].ToString();
            info.CoursNum = DataConverter.CLng(rdr["CoursNum"].ToString());
            info.Listen = DataConverter.CLng(rdr["Listen"].ToString());
            info.UpdateTime = DataConverter.CDate(rdr["UpdateTime"].ToString());
            info.FileUrl = rdr["FileUrl"].ToString();
            info.CourseID = DataConverter.CLng(rdr["CourseID"].ToString());
            info.Speaker = rdr["Speaker"].ToString();
            info.SJName = rdr["SJName"].ToString();
            info.Status = DataConverter.CLng(rdr["Status"].ToString());
            info.Description = rdr["Description"].ToString();
            info.Classification = rdr["Classification"].ToString();
            info.CoursType = DataConverter.CLng(rdr["CoursType"].ToString());
            info.CreationTime = DataConverter.CDate(rdr["CreationTime"].ToString());
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

         /// <summary>
        /// 通过课程ID查询
        /// </summary>
        /// <param name="CourseID">课程ID</param>
        /// <returns></returns>
        public DataTable Select_CourseID(int CourseID)
        {
            return Sql.Sel(strTableName, "CourseID=" + CourseID, null);
        }
	}
}
