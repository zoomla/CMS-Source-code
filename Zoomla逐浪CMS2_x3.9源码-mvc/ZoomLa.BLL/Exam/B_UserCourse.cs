namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using ZoomLa.SQLDAL;
    using System.Data.SqlClient;
	
    public class B_UserCourse
    {
        public B_UserCourse()
        {
            strTableName = initmod.TbName;
            PK = initmod.PK;
        }
        public string PK, strTableName;
        private M_UserCourse initmod = new M_UserCourse();

		///添加记录
		/// </summary>
		/// <param name="UserCourse"></param>
		/// <returns></returns>
		public int GetInsert(M_UserCourse userCourse)
        {
            string sqlStr = "INSERT INTO [dbo].[ZL_UserCourse] ([CourseID],[ClassID],[UserID],[CurrCoureHour],[State],[AddTime],[PayMent],[OrderID],[Aunit],[Remark]) VALUES (@CourseID,@ClassID,@UserID,@CurrCoureHour,@State,@AddTime,@PayMent,@OrderID,@Aunit,@Remark);select @@IDENTITY";
            SqlParameter[] cmdParams = initmod.GetParameters(userCourse);
            return SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.Text, sqlStr, cmdParams));
        }

		/// <summary>
		///更新记录
		/// </summary>
		/// <param name="UserCourse"></param>
		/// <returns></returns>
		public bool GetUpdate(M_UserCourse userCourse)
        {
            string sqlStr = "UPDATE [dbo].[ZL_UserCourse] SET [CourseID] = @CourseID,[ClassID] = @ClassID,[UserID] = @UserID,[CurrCoureHour] = @CurrCoureHour,[State] = @State,[AddTime] = @AddTime,[PayMent] = @PayMent,[OrderID] = @OrderID,[Aunit] = @Aunit,[Remark]=@Remark WHERE [ID] = @ID";
            SqlParameter[] cmdParams = initmod.GetParameters(userCourse);
            return SqlHelper.ExecuteSql(sqlStr, cmdParams);
        }

		/// <summary>
		///删除记录
		/// </summary>
		/// <param name="UserCourse"></param>
		/// <returns></returns>
		public bool DeleteByGroupID(int UserCourseID)
        {
            return Sql.Del(strTableName, UserCourseID);
        }

        public DataTable SelectByUidCoidClid(int uid, int coid, int clid)
        {
            string sqlStr = "SELECT * FROM [dbo].[ZL_UserCourse]  WHERE 1=1";
            if (uid > 0)
            {
                sqlStr += " AND UserID=" + uid;
            }
            if (coid >= 0)
            {
                sqlStr += " AND (Aunit=" + coid + " or state=1)";
            }
            if (clid > 0)
            {
                sqlStr += " AND CourseID IN ( SELECT id FROM zl_course WHERE CoureseClass =" + clid + ")";
            }
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, null);
        }

		/// <summary>
		///查找一条记录
		/// </summary>
		/// <param name="UserCourse"></param>
		/// <returns></returns>
		public M_UserCourse GetSelect(int UserCourseID)
        {
            string sqlStr = "SELECT * FROM [dbo].[ZL_UserCourse] WHERE [ID] = @UserCourseID";
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@UserCourseID", SqlDbType.Int, 4);
            cmdParams[0].Value = UserCourseID;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sqlStr, cmdParams))
            {
                if (reader.Read())
                {
                    return GetInfoFromReader(reader);
                }
                else
                {
                    return new M_UserCourse();
                }
            }
        }
        private static M_UserCourse GetInfoFromReader(SqlDataReader rdr)
        {
            M_UserCourse info = new M_UserCourse();
            info.ID = DataConverter.CLng(rdr["ID"].ToString());
            info.CourseID = DataConverter.CLng(rdr["CourseID"].ToString());
            info.ClassID = DataConverter.CLng(rdr["ClassID"].ToString());
            info.UserID = DataConverter.CLng(rdr["UserID"].ToString());
            info.CurrCoureHour = DataConverter.CDouble(rdr["CurrCoureHour"].ToString());
            info.State = DataConverter.CLng(rdr["State"].ToString());
            info.AddTime = DataConverter.CDate(rdr["AddTime"].ToString());
            info.PayMent = DataConverter.CLng(rdr["PayMent"].ToString());
            info.OrderID = rdr["OrderID"].ToString();
            info.Aunit = DataConverter.CLng(rdr["Aunit"].ToString());
            info.Remark = rdr["Remark"].ToString();
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
            return Sql.Sel(strTableName, null, " Aunit,AddTime DESC");
        }

        /// <summary>
        /// 用户查询课程
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <param name="Aunit">是否审核</param>
        /// <returns></returns>
        public DataTable SelectByUserId(int userid, int Aunit)
        {
            string sqlStr = "SELECT * FROM [dbo].[ZL_UserCourse] WHERE UserID=@UserID ";
            if (Aunit >= 0)
            {
                sqlStr += " AND Aunit=" + Aunit;
            }
            sqlStr += " ORDER BY AddTime DESC";
            SqlParameter[] para = new SqlParameter[]{
                new SqlParameter("@UserID",userid)
            };
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, para);
        }

        /// <summary>
        /// 通过用户ID查询
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <param name="aunit">审核</param>
        /// <param name="Coureseclassid">课程分类</param>
        /// <returns></returns>
        public DataTable SelectByUserid(int userid, int aunit, int Coureseclassid)
        {
            string sqlStr = "SELECT * FROM [dbo].[ZL_UserCourse]  WHERE 1=1";
            if (userid > 0)
            {
                sqlStr += " AND UserID=" + userid;
            }
            if (aunit >= 0)
            {
                sqlStr += " AND (Aunit=" + aunit + " or state=1)";
            }
            if (Coureseclassid > 0)
            {
                sqlStr += " AND CourseID IN ( SELECT id FROM zl_course WHERE CoureseClass =" + Coureseclassid + ")";
            }
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, null);
        }
        /// <summary>
        /// 班级和课程ID查询
        /// </summary>
        /// <param name="classID">班级ID</param>
        /// <param name="CourseID">课程ID</param>
        /// <returns></returns>
        public M_UserCourse Select_ClassID(int classID, int CourseID, int userid)
        {
            string sqlStr = "SELECT * FROM ZL_UserCourse WHERE classID=@classID AND CourseID=@CourseID AND userid=@userid";
            SqlParameter[] para = new SqlParameter[]{
                new SqlParameter("@classID",classID),
                new SqlParameter("@CourseID",CourseID),
                new SqlParameter("@userid",userid)
            };
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sqlStr, para))
            {
                if (reader.Read())
                {
                    return GetInfoFromReader(reader);
                }
                else
                {
                    return new M_UserCourse();
                }
            }
        }

         /// <summary>
        /// 批量审核
        /// </summary>
        /// <param name="courseIDs">课程iD</param>
        /// <param name="anuit">审核</param>
        /// <returns></returns>
        public bool UpdateAunit(string courseIDs, int anuit)
        {
            string sqlStr = "UPDATE ZL_UserCourse  SET AddTime=getdate(),Aunit =@Aunit WHERE  ID IN (" + courseIDs + ")";
            SqlParameter[] para = new SqlParameter[]{
                new SqlParameter("@Aunit",anuit)
            };
            return SqlHelper.ExecuteSql(sqlStr, para);
        }

        /// <summary>
        /// 根据班级ID查询学员列表
        /// </summary>
        /// <param name="ClassID"></param>
        /// <returns></returns>
        public DataTable SelectUinfoClass(int ClassID)
        {
            string sqlstr = "select ZL_User.*, ZL_UserCourse.*, ZL_ExClassgroup.* from ZL_User left join ZL_UserCourse on ZL_User.UserID=ZL_UserCourse.UserID INNER JOIN ZL_ExClassgroup ON ZL_UserCourse.ClassID = ZL_ExClassgroup.GroupID where ZL_UserCourse.ClassID=@ClassID";

            SqlParameter[] para = new SqlParameter[]{
                new SqlParameter("@ClassID",SqlDbType.Int)
            };
            para[0].Value = ClassID;
            return SqlHelper.ExecuteTable(CommandType.Text, sqlstr, para);
        }
	}
}
