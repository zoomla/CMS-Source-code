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
    /// B_MtrlsMrktng 的摘要说明
    /// </summary>
    public class B_MtrlsMrktng
    {
        public B_MtrlsMrktng()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        /// <summary>
        ///添加记录
        /// </summary>
        /// <param name="MtrlsMrktng"></param>
        /// <returns></returns>
        public int GetInsert(M_MtrlsMrktng mtrlsMrktng)
        {
            string sqlStr = "INSERT INTO [dbo].[ZL_MtrlsMrktng] ([ShopID],[UserID],[Type],[Count],[commission]) VALUES (@ShopID,@UserID,@Type,@Count,@commission);SET @ID = SCOPE_IDENTITY()";
            SqlParameter[] cmdParams = GetParameters(mtrlsMrktng);
            return SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.Text, sqlStr, cmdParams));
        }

        /// <summary>
        ///更新记录
        /// </summary>
        /// <param name="MtrlsMrktng"></param>
        /// <returns></returns>
        public bool GetUpdate(M_MtrlsMrktng mtrlsMrktng)
        {
            string sqlStr = "UPDATE [dbo].[ZL_MtrlsMrktng] SET [ShopID] = @ShopID,[UserID] = @UserID,[Type] = @Type,[Count] = @Count,[commission] = @commission WHERE [ID] = @ID";
            SqlParameter[] cmdParams = GetParameters(mtrlsMrktng);
            return SqlHelper.ExecuteSql(sqlStr, cmdParams);
        }
        private static SqlParameter[] GetParameters(M_MtrlsMrktng MtrlsMrktnginfo)
        {
            SqlParameter[] parameter = new SqlParameter[6];
            parameter[0] = new SqlParameter("@ID", SqlDbType.Int, 4);
            parameter[0].Value = MtrlsMrktnginfo.ID;
            parameter[1] = new SqlParameter("@ShopID", SqlDbType.Int, 4);
            parameter[1].Value = MtrlsMrktnginfo.ShopID;
            parameter[2] = new SqlParameter("@UserID", SqlDbType.Int, 4);
            parameter[2].Value = MtrlsMrktnginfo.UserID;
            parameter[3] = new SqlParameter("@Type", SqlDbType.NVarChar, 50);
            parameter[3].Value = MtrlsMrktnginfo.Type;
            parameter[4] = new SqlParameter("@Count", SqlDbType.Int, 4);
            parameter[4].Value = MtrlsMrktnginfo.Count;
            parameter[5] = new SqlParameter("@commission", SqlDbType.Money);
            parameter[5].Value = MtrlsMrktnginfo.commission;
            return parameter;
        }
        /// <summary>
        ///删除记录
        /// </summary>
        /// <param name="MtrlsMrktng"></param>
        /// <returns></returns>
        public bool DeleteByGroupID(int MtrlsMrktngID)
        {
            string sqlStr = "DELETE FROM [dbo].[ZL_MtrlsMrktng] WHERE [ID] = @ID";
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@ID", SqlDbType.Int, 4);
            cmdParams[0].Value = MtrlsMrktngID;
            return SqlHelper.ExecuteSql(sqlStr, cmdParams);
        }

        /// <summary>
        ///查找一条记录
        /// </summary>
        /// <param name="MtrlsMrktng"></param>
        /// <returns></returns>
        public M_MtrlsMrktng GetSelect(int MtrlsMrktngID)
        {
            string sqlStr = "SELECT [ID],[ShopID],[UserID],[Type],[Count],[commission] FROM [dbo].[ZL_MtrlsMrktng] WHERE [ID] = @ID";
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@ID", SqlDbType.Int, 4);
            cmdParams[0].Value = MtrlsMrktngID;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sqlStr, cmdParams))
            {
                if (reader.Read())
                {
                    return GetInfoFromReader(reader);
                }
                else
                {
                    return new M_MtrlsMrktng();
                }
            }
        }
        private static M_MtrlsMrktng GetInfoFromReader(SqlDataReader rdr)
        {
            M_MtrlsMrktng info = new M_MtrlsMrktng();
            info.ID = DataConverter.CLng(rdr["ID"].ToString());
            info.ShopID = DataConverter.CLng(rdr["ShopID"].ToString());
            info.UserID = DataConverter.CLng(rdr["UserID"].ToString());
            info.Type = rdr["Type"].ToString();
            info.Count = DataConverter.CLng(rdr["Count"].ToString());
            info.commission = DataConverter.CDouble(rdr["commission"].ToString());
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
            string sqlStr = "SELECT [ID],[ShopID],[UserID],[Type],[Count],[commission] FROM [dbo].[ZL_MtrlsMrktng]";
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, null);
        }
        public PageSetting SelPage(int cpage, int psize)
        {
            PageSetting setting = PageSetting.Single(cpage, psize, "ZL_MtrlsMrktng", "ID", "");
            setting.fields = "[ID],[ShopID],[UserID],[Type],[Count],[commission]";
            DBCenter.SelPage(setting);
            return setting;
        }
    }
}
