namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using ZoomLa.SQLDAL;
    using System.Data.SqlClient;
    using SQLDAL.SQL;
    public class B_PointGrounp
    {
        private string strTableName, PK;
        private M_PointGrounp initMod = new M_PointGrounp();
        public B_PointGrounp()
        {
            strTableName = initMod.TbName;
            PK = initMod.PK;
        }
        public DataTable Sel(int ID)
        {
            return Sql.Sel(strTableName, PK, ID);
        }
        public M_PointGrounp SelReturnModel(int ID)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(strTableName, PK, ID))
            {
                if (reader.Read())
                {
                    return initMod.GetModelFromReader(reader);
                }
                else
                {
                    return null;
                }
            }
        }
        private M_PointGrounp SelReturnModel(string strWhere)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(strTableName, strWhere))
            {
                if (reader.Read())
                {
                    return initMod.GetModelFromReader(reader);
                }
                else
                {
                    return null;
                }
            }
        }
        public DataTable Sel()
        {
            return Sql.Sel(strTableName);
        }
        public PageSetting SelPage(int cpage, int psize)
        {
            PageSetting setting = PageSetting.Single(cpage, psize, strTableName, PK, "");
            DBCenter.SelPage(setting);
            return setting;
        }
        public bool UpdateByID(M_PointGrounp model)
        {
            return Sql.UpdateByIDs(strTableName, PK, model.ID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
        public bool Del(int ID)
        {
            return Sql.Del(strTableName, ID);
        }
        public int insert(M_PointGrounp model)
        {
            return Sql.insert(strTableName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        public int GetInsert(M_PointGrounp model)
        {
            string sqlStr = "INSERT INTO [dbo].[ZL_PointGrounp] ([PointVal],[Remark],[AddTime],[GroupName],[ImgUrl]) VALUES (@PointVal,@Remark,@AddTime,@GroupName,@ImgUrl);select @@IDENTITY";
            SqlParameter[] cmdParams = model.GetParameters();
            return SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.Text, sqlStr, cmdParams));
        }
        public bool GetUpdate(M_PointGrounp model)
        {
            string sqlStr = "UPDATE [dbo].[ZL_PointGrounp] SET [PointVal] = @PointVal,[Remark] = @Remark,[AddTime] = @AddTime,[GroupName]=@GroupName,[ImgUrl]=@ImgUrl WHERE [ID] = @ID";
            SqlParameter[] cmdParams = model.GetParameters();
            return SqlHelper.ExecuteSql(sqlStr, cmdParams);
        }
		public bool DeleteByGroupID(int PointGrounpID)
        {
            string sqlStr = "DELETE FROM [dbo].[ZL_PointGrounp] WHERE [ID] = @ID";
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@ID", SqlDbType.Int, 4);
            cmdParams[0].Value = PointGrounpID;
            return SqlHelper.ExecuteSql(sqlStr, cmdParams);
        }
		public M_PointGrounp GetSelect(int PointGrounpID)
        {
            string sqlStr = "SELECT [ID],[PointVal],[Remark],[AddTime],[GroupName],[ImgUrl] FROM [dbo].[ZL_PointGrounp] WHERE [ID] = @ID";
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@ID", SqlDbType.Int, 4);
            cmdParams[0].Value = PointGrounpID;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sqlStr, cmdParams))
            {
                if (reader.Read())
                {
                    return initMod.GetModelFromReader(reader);
                }
                else
                {
                    return new M_PointGrounp();
                }
            }
        }
		public DataTable Select_All()
        {
      		return Sel();
        }
         /// <summary>
        /// 通过用户积分查询积分等级
        /// </summary>
        /// <param name="pointVal">用户积分</param>
        /// <returns></returns>
        public M_PointGrounp SelectPintGroup(double pointVal)
        {
            string sqlStr = "select TOP 1 * from ZL_PointGrounp where PointVal<=" + pointVal + " order by pointVal DESC";
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sqlStr, null))
            {
                if (reader.Read())
                {
                    return initMod.GetModelFromReader(reader);
                }
                else
                {
                    return new M_PointGrounp();
                }
            }
        }
	}
}
