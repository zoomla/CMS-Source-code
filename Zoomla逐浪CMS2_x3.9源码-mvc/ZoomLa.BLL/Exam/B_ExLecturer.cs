namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using ZoomLa.SQLDAL;
    using System.Data.SqlClient;
	
    public class B_ExLecturer
    {
        public B_ExLecturer()
        {
            strTableName = initmod.TbName;
            PK = initmod.PK;
        }
        public string PK, strTableName;
        private M_ExLecturer initmod = new M_ExLecturer();
		/// <summary>
		///添加记录
		/// </summary>
		/// <param name="ExLecturer"></param>
		/// <returns></returns>
		public int GetInsert(M_ExLecturer exLecturer)
        {
            string sqlStr = "INSERT INTO [dbo].[ZL_ExLecturer] ([TechName],[TechType],[TechSex],[TechTitle],[TechPhone],[CreateTime],[TechSpecialty],[TechHobby],[TechIntrodu],[TechLevel],[TechDepart],[TechClass],[TechRecom],[Popularity],[Awardsinfo],[FileUpload],[AddUser],[Professional]) VALUES (@TechName,@TechType,@TechSex,@TechTitle,@TechPhone,@CreateTime,@TechSpecialty,@TechHobby,@TechIntrodu,@TechLevel,@TechDepart,@TechClass,@TechRecom,@Popularity,@Awardsinfo,@FileUpload,@AddUser,@Professional);select @@IDENTITY";
            SqlParameter[] cmdParams = initmod.GetParameters(exLecturer);
            return SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.Text, sqlStr, cmdParams));
        }

		/// <summary>
		///更新记录
		/// </summary>
		/// <param name="ExLecturer"></param>
		/// <returns></returns>
		public bool GetUpdate(M_ExLecturer exLecturer)
        {
            string sqlStr = "UPDATE [dbo].[ZL_ExLecturer] SET [TechName] = @TechName,[TechType] = @TechType,[TechSex] = @TechSex,[TechTitle] = @TechTitle,[TechPhone] = @TechPhone,[CreateTime] = @CreateTime,[TechSpecialty] = @TechSpecialty,[TechHobby] = @TechHobby,[TechIntrodu] = @TechIntrodu,[TechLevel] = @TechLevel,[TechDepart] = @TechDepart,[TechClass] = @TechClass,[TechRecom] = @TechRecom,[Popularity] = @Popularity,[Awardsinfo] = @Awardsinfo,[FileUpload] = @FileUpload,[AddUser] = @AddUser,[Professional] = @Professional WHERE [ID] = @ID";
            SqlParameter[] cmdParams = initmod.GetParameters(exLecturer);
            return SqlHelper.ExecuteSql(sqlStr, cmdParams);
        }

		/// <summary>
		///删除记录
		/// </summary>
		/// <param name="ExLecturer"></param>
		/// <returns></returns>
		public bool DeleteByGroupID(int ExLecturerID)
        {
            return Sql.Del(strTableName, ExLecturerID);
        }

		/// <summary>
		///查找一条记录
		/// </summary>
		/// <param name="ExLecturer"></param>
		/// <returns></returns>
		public M_ExLecturer GetSelect(int ExLecturerID)
        {
            string sqlStr = "SELECT [ID],[TechName],[TechType],[TechSex],[TechTitle],[TechPhone],[CreateTime],[TechSpecialty],[TechHobby],[TechIntrodu],[TechLevel],[TechDepart],[TechClass],[TechRecom],[Popularity],[Awardsinfo],[FileUpload],[AddUser],[Professional] FROM [dbo].[ZL_ExLecturer] WHERE [ID] = @ID";
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@ID", SqlDbType.Int, 4);
            cmdParams[0].Value = ExLecturerID;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sqlStr, cmdParams))
            {
                if (reader.Read())
                {
                    return GetInfoFromReader(reader);
                }
                else
                {
                    return new M_ExLecturer();
                }
            }
        }
        private static M_ExLecturer GetInfoFromReader(SqlDataReader rdr)
        {
            M_ExLecturer info = new M_ExLecturer();
            info.ID = DataConverter.CLng(rdr["ID"].ToString());
            info.TechName = rdr["TechName"].ToString();
            info.TechType = rdr["TechType"].ToString();
            info.TechSex = DataConverter.CLng(rdr["TechSex"].ToString());
            info.TechTitle = rdr["TechTitle"].ToString();
            info.TechPhone = rdr["TechPhone"].ToString();
            info.CreateTime = DataConverter.CDate(rdr["CreateTime"].ToString());
            info.TechSpecialty = rdr["TechSpecialty"].ToString();
            info.TechHobby = rdr["TechHobby"].ToString();
            info.TechIntrodu = rdr["TechIntrodu"].ToString();
            info.TechLevel = rdr["TechLevel"].ToString();
            info.TechDepart = DataConverter.CLng(rdr["TechDepart"].ToString());
            info.TechClass = DataConverter.CLng(rdr["TechClass"].ToString());
            info.TechRecom = DataConverter.CLng(rdr["TechRecom"].ToString());
            info.Popularity = DataConverter.CLng(rdr["Popularity"].ToString());
            info.Awardsinfo = rdr["Awardsinfo"].ToString();
            info.FileUpload = rdr["FileUpload"].ToString();
            info.AddUser = DataConverter.CLng(rdr["AddUser"].ToString());
            info.Professional = DataConverter.CLng(rdr["Professional"].ToString());
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
