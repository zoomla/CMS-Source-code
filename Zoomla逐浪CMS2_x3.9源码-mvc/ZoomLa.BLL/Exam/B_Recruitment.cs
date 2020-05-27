namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using ZoomLa.SQLDAL;
    using System.Data.SqlClient;

    /// <summary>
    /// B_Recruitment 的摘要说明
    /// </summary>
    public class B_Recruitment
    {
        public B_Recruitment()
        {
            strTableName = initmod.TbName;
            PK = initmod.PK;
        }
        public string PK, strTableName;
        private M_Recruitment initmod = new M_Recruitment();
        /// <summary>
        /// 查询所有招聘人员信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetRecruintmentallTech()
        {
            string sqlStr = "SELECT ZL_User.*, ZL_UserBase.* FROM ZL_User INNER JOIN ZL_Group ON ZL_User.GroupID = ZL_Group.GroupID INNER JOIN ZL_UserBase ON ZL_User.UserID = ZL_UserBase.UserID WHERE ZL_Group.Enroll=1";
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, null);
        }

        /// <summary>
        /// 查询所有招聘信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetRecruintmentall()
        {
            string sqlStr = "SELECT ZL_EnrollList.*, ZL_User.*, ZL_UserBase.* FROM ZL_EnrollList INNER JOIN ZL_User ON ZL_EnrollList.UesrID = ZL_User.UserID INNER JOIN ZL_UserBase ON ZL_User.UserID = ZL_UserBase.UserID";
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, null);
        }

        /// <summary>
        /// 根据用户名查询招聘人员信息
        /// </summary>
        /// <param name="EnrollUserID"></param>
        /// <returns></returns>
        public DataTable GetRecruintment(int EnrollUserID)
        {
            string sqlStr = "SELECT ZL_EnrollList.*, ZL_Recruitment.*,ZL_Recruitment.id as ssid FROM ZL_EnrollList INNER JOIN ZL_Recruitment ON ZL_EnrollList.id = ZL_Recruitment.EnrolllistID where ZL_EnrollList.id=@EnrolllistID";
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@EnrolllistID", SqlDbType.Int, 4);
            cmdParams[0].Value = EnrollUserID;
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, cmdParams);
        }
        /// <summary>
        ///添加记录
        /// </summary>
        /// <param name="Recruitment"></param>
        /// <returns></returns>
        public int GetInsert(M_Recruitment recruitment)
        {
            string sqlStr = "INSERT INTO [dbo].[ZL_Recruitment] ([Studioname],[PriorUserName],[LogPassWord],[TechID],[CourseID],[ClassID],[Remark],[Tel],[Addinfo],[CradNo],[WriteTime],[AddTime],[EnrolllistID]) VALUES (@Studioname,@PriorUserName,@LogPassWord,@TechID,@CourseID,@ClassID,@Remark,@Tel,@Addinfo,@CradNo,@WriteTime,@AddTime,@EnrolllistID);SET @id = SCOPE_IDENTITY()";
            SqlParameter[] cmdParams = initmod.GetParameters(recruitment);
            return SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.Text, sqlStr, cmdParams));
        }

        /// <summary>
        ///更新记录
        /// </summary>
        /// <param name="Recruitment"></param>
        /// <returns></returns>
        public bool GetUpdate(M_Recruitment recruitment)
        {
            string sqlStr = "UPDATE [dbo].[ZL_Recruitment] SET [Studioname] = @Studioname,[PriorUserName] = @PriorUserName,[LogPassWord] = @LogPassWord,[TechID] = @TechID,[CourseID] = @CourseID,[ClassID] = @ClassID,[Remark] = @Remark,[Tel] = @Tel,[Addinfo] = @Addinfo,[CradNo] = @CradNo,[WriteTime] = @WriteTime,[AddTime] = @AddTime,[EnrolllistID] = @EnrolllistID WHERE [id] = @id";
            SqlParameter[] cmdParams = initmod.GetParameters(recruitment);
            return SqlHelper.ExecuteSql(sqlStr, cmdParams);
        }

        /// <summary>
        ///删除记录
        /// </summary>
        /// <param name="Recruitment"></param>
        /// <returns></returns>
        public bool DeleteByGroupID(int RecruitmentID)
        {
            return Sql.Del(strTableName, RecruitmentID);
        }

        /// <summary>
        ///查找一条记录
        /// </summary>
        /// <param name="Recruitment"></param>
        /// <returns></returns>
        public M_Recruitment GetSelect(int RecruitmentID)
        {
            string sqlStr = "SELECT [id],[Studioname],[PriorUserName],[LogPassWord],[TechID],[CourseID],[ClassID],[Remark],[Tel],[Addinfo],[CradNo],[WriteTime],[AddTime],[EnrolllistID] FROM [dbo].[ZL_Recruitment] WHERE [id] = @id";
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@id", SqlDbType.Int, 4);
            cmdParams[0].Value = RecruitmentID;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sqlStr, cmdParams))
            {
                if (reader.Read())
                {
                    return GetInfoFromReader(reader);
                }
                else
                {
                    return new M_Recruitment();
                }
            }
        }
        private static M_Recruitment GetInfoFromReader(SqlDataReader rdr)
        {
            M_Recruitment info = new M_Recruitment();
            info.id = DataConverter.CLng(rdr["id"].ToString());
            info.Studioname = rdr["Studioname"].ToString();
            info.PriorUserName = rdr["PriorUserName"].ToString();
            info.LogPassWord = rdr["LogPassWord"].ToString();
            info.TechID = DataConverter.CLng(rdr["TechID"].ToString());
            info.CourseID = DataConverter.CLng(rdr["CourseID"].ToString());
            info.ClassID = DataConverter.CLng(rdr["ClassID"].ToString());
            info.Remark = rdr["Remark"].ToString();
            info.Tel = rdr["Tel"].ToString();
            info.Addinfo = rdr["Addinfo"].ToString();
            info.CradNo = rdr["CradNo"].ToString();
            info.WriteTime = DataConverter.CDate(rdr["WriteTime"].ToString());
            info.AddTime = DataConverter.CDate(rdr["AddTime"].ToString());
            info.EnrolllistID = DataConverter.CLng(rdr["EnrolllistID"].ToString());
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

        public DataTable GetRencount(int id)
        {
            string sql = "select * from ZL_Recruitment where EnrolllistID=" + id + "";
            return SqlHelper.ExecuteTable(CommandType.Text, sql, null);
        }
    }
}
