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
    /// B_Course 的摘要说明
    /// </summary>
    public class B_Course
    {
        public B_Course()
        {
            strTableName = initmod.TbName;
            PK = initmod.PK;
        }
        public string PK, strTableName;
        private M_Course initmod = new M_Course();
        /// <summary>
        ///添加记录
        /// </summary>
        /// <param name="Course"></param>
        /// <returns></returns>
        public int GetInsert(M_Course model)
        {
           return DBCenter.Insert(model);
        }

        public DataTable Select_classidNotInCouresId(int classId)
        {
            string sqlStr = "SELECT * FROM [dbo].[ZL_Course] WHERE 1=1 ";
            if (classId > 0)
            {
                sqlStr += "and CoureseClass=@CoureseClass ";
            }
            sqlStr += " And  ID not in (select courseid from zl_usercourse where (aunit=1 or state=1) ) ORDER BY AddTime DESC";
            SqlParameter[] para = new SqlParameter[]{
                new SqlParameter("@CoureseClass",classId)
            };
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, para);
        }

        /// <summary>
        ///更新记录
        /// </summary>
        /// <param name="Course"></param>
        /// <returns></returns>
        public bool GetUpdate(M_Course model)
        {
          return DBCenter.UpdateByID(model,model.id);
        }

        /// <summary>
        ///删除记录
        /// </summary>
        /// <param name="Course"></param>
        /// <returns></returns>
        public bool DeleteByGroupID(int CourseID)
        {
            return Sql.Del(strTableName, CourseID);
        }

        /// <summary>
        ///查找一条记录
        /// </summary>
        /// <param name="Course"></param>
        /// <returns></returns>
        public M_Course GetSelect(int CourseID)
        {
            string sqlStr = "SELECT [id],[CourseName],[CoureseThrun],[CoureseCode],[CoureseCredit],[CoureseRemark],[Hot],[CoureseClass],[AddTime],[AddUser] FROM [dbo].[ZL_Course] WHERE [id] = @id";
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@id", SqlDbType.Int, 4);
            cmdParams[0].Value = CourseID;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sqlStr, cmdParams))
            {
                if (reader.Read())
                {
                    return GetInfoFromReader(reader);
                }
                else
                {
                    return new M_Course();
                }
            }
        }
        private static M_Course GetInfoFromReader(SqlDataReader rdr)
        {
            M_Course info = new M_Course();
            info.id = DataConverter.CLng(rdr["id"].ToString());
            info.CourseName = rdr["CourseName"].ToString();
            info.CoureseThrun = rdr["CoureseThrun"].ToString();
            info.CoureseCode = rdr["CoureseCode"].ToString();
            info.CoureseCredit = DataConverter.CDouble(rdr["CoureseCredit"].ToString());
            info.CoureseRemark = rdr["CoureseRemark"].ToString();
            info.Hot = DataConverter.CLng(rdr["Hot"].ToString());
            info.CoureseClass = DataConverter.CLng(rdr["CoureseClass"].ToString());
            info.AddTime = DataConverter.CDate(rdr["AddTime"].ToString());
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
            return Sql.Sel(strTableName, null, "AddTime DESC");
        }
        public PageSetting SelPage(int cpage, int psize)
        {
            PageSetting setting = PageSetting.Single(cpage, psize, strTableName, PK, "", "AddTime DESC");
            DBCenter.SelPage(setting);
            return setting;
        }
        /// <summary>
        /// 通过分类查询
        /// </summary>
        /// <param name="classId">课程分类</param>
        /// <returns></returns>
        public DataTable Select_classid(int classId)
        {
            string sqlStr = "SELECT * FROM [dbo].[ZL_Course] WHERE 1=1 ";
            if (classId > 0)
            {
                sqlStr += "CoureseClass=@CoureseClass ";
            }
            sqlStr += " And  ID  in (select courseid from zl_usercourse where (aunit=1 or state=1) ) ORDER BY AddTime DESC";
            SqlParameter[] para = new SqlParameter[]{
                new SqlParameter("@CoureseClass",classId)
            };
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, para);
        }
    }
}
