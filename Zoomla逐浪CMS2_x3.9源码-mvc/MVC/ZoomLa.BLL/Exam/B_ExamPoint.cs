namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using ZoomLa.SQLDAL;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    /// <summary>
    /// B_ExamPoint 的摘要说明
    /// </summary>
    public class B_ExamPoint
    {
        public B_ExamPoint()
        {
            strTableName = initmod.TbName;
            PK = initmod.PK;
        }
        public string PK, strTableName;
        private M_ExamPoint initmod = new M_ExamPoint();
        /// <summary>
        ///添加记录
        /// </summary>
        /// <param name="ExamPoint"></param>
        /// <returns></returns>
        public int GetInsert(M_ExamPoint examPoint)
        {
            string sqlStr = "INSERT INTO [dbo].[ZL_ExamPoint] ([TestPoint],[TID],[OrderBy],[AddUser],[AddTime]) VALUES (@TestPoint,@TID,@OrderBy,@AddUser,@AddTime);select @@IDENTITY";
            SqlParameter[] cmdParams = initmod.GetParameters(examPoint);
            return SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.Text, sqlStr, cmdParams));
        }

        /// <summary>
        ///更新记录
        /// </summary>
        /// <param name="ExamPoint"></param>
        /// <returns></returns>
        public bool GetUpdate(M_ExamPoint examPoint)
        {
            string sqlStr = "UPDATE [dbo].[ZL_ExamPoint] SET [TestPoint] = @TestPoint,[TID] = @TID,[OrderBy] = @OrderBy,[AddUser] = @AddUser,[AddTime] = @AddTime WHERE [ID] = @ID";
            SqlParameter[] cmdParams = initmod.GetParameters(examPoint);
            return SqlHelper.ExecuteSql(sqlStr, cmdParams);
        }

        /// <summary>
        ///删除记录
        /// </summary>
        /// <param name="ExamPoint"></param>
        /// <returns></returns>
        public bool DeleteByGroupID(int ExamPointID)
        {
            return Sql.Del(strTableName, ExamPointID);
        }

        /// <summary>
        ///查找一条记录
        /// </summary>
        /// <param name="ExamPoint"></param>
        /// <returns></returns>
        public M_ExamPoint GetSelect(int ExamPointID)
        {
            string sqlStr = "SELECT [ID],[TestPoint],[TID],[OrderBy],[AddUser],[AddTime] FROM [dbo].[ZL_ExamPoint] WHERE [ID] = @ID";
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@ID", SqlDbType.Int, 4);
            cmdParams[0].Value = ExamPointID;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sqlStr, cmdParams))
            {
                if (reader.Read())
                {
                    return GetInfoFromReader(reader);
                }
                else
                {
                    return new M_ExamPoint();
                }
            }
        }
        private static M_ExamPoint GetInfoFromReader(SqlDataReader rdr)
        {
            M_ExamPoint info = new M_ExamPoint();
            info.ID = DataConverter.CLng(rdr["ID"].ToString());
            info.TestPoint = rdr["TestPoint"].ToString();
            info.TID = DataConverter.CLng(rdr["TID"].ToString());
            info.OrderBy = DataConverter.CLng(rdr["OrderBy"].ToString());
            info.AddUser = DataConverter.CLng(rdr["AddUser"].ToString());
            info.AddTime = DataConverter.CDate(rdr["AddTime"].ToString());
            rdr.Close();
            rdr.Dispose();
            return info;
        }
        /// <summary>
        ///查找一条记录
        /// </summary>
        /// <param name="ExamPoint"></param>
        /// <returns></returns>
        public M_ExamPoint GetSelectByPointName(string ExamPointName)
        {
            string sqlStr = "SELECT [ID],[TestPoint],[TID],[OrderBy],[AddUser],[AddTime] FROM [dbo].[ZL_ExamPoint] WHERE [TestPoint] = @TestPoint";
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@TestPoint", SqlDbType.NVarChar, 255);
            cmdParams[0].Value = ExamPointName;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sqlStr, cmdParams))
            {
                if (reader.Read())
                {
                    return GetInfoFromReader(reader);
                }
                else
                {
                    return new M_ExamPoint();
                }
            }
        }


        /// <summary>
        ///返回所有记录
        /// </summary>
        /// <returns></returns>
        public DataTable Select_All()
        {
            return Sql.Sel(strTableName);
        }
        public DataTable SelByCid(int cid)
        {
            string sql = "Select * From " + strTableName + " Where  WHERE [ID]="+cid;
            return SqlHelper.ExecuteTable(CommandType.Text,sql);
        }
        /// <summary>
        /// 根据分类ID查询所有知识点
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public List<M_ExamPoint> GetSelectByCid(int cid)
        {
            string sql = "SELECT [ID],[TestPoint],[TID],[OrderBy],[AddUser],[AddTime] FROM ZL_ExamPoint WHERE [ID]=" + cid;
            DataSet ds = SqlHelper.ExecuteDataSet(CommandType.Text, sql);
            List<M_ExamPoint> mqks = GetKByds(ds);
            if (mqks != null && mqks.Count > 0)
            {
                return mqks;
            }
            else
            {
                return new List<M_ExamPoint>();
            }
        }
        private List<M_ExamPoint> GetKByds(DataSet ds)
        {
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                List<M_ExamPoint> mqks = new List<M_ExamPoint>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    M_ExamPoint mep = new M_ExamPoint();
                    mep.ID = DataConverter.CLng(dr["id"]);
                    mep.TestPoint = (dr["TestPoint"]).ToString();
                    mep.OrderBy = DataConverter.CLng(dr["OrderBy"]);
                    mep.TID = DataConverter.CLng(dr["TID"]);
                    mep.AddUser = DataConverter.CLng(dr["AddUser"]);
                    mep.AddTime = DataConverter.CDate(dr["AddTime"]);
                    mqks.Add(mep);
                }
                return mqks;
            }
            return new List<M_ExamPoint>();
        }
        /// <summary>
        /// 通过名称和分类ID查询知识点
        /// </summary>
        /// <param name="name"></param>
        /// <param name="cid"></param>
        /// <returns></returns>
        public M_ExamPoint GetSelectByNameAndCid(string name, int cid)
        {
            string sql = "SELECT  [ID],[TestPoint],[TID],[OrderBy],[AddUser],[AddTime] FROM ZL_ExamPoint WHERE TestPoint='" + name + "' AND ID=" + cid;
            DataSet ds = SqlHelper.ExecuteDataSet(CommandType.Text, sql);
            List<M_ExamPoint> mqks = GetKByds(ds);
            if (mqks != null && mqks.Count > 0)
            {
                return mqks[0];
            }
            else
            {
                return new M_ExamPoint();
            }
        }


    }
}
