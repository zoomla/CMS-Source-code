namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using ZoomLa.SQLDAL;
    using System.Data.SqlClient;
    public class B_PromoCount
    {
        private string strTableName, PK;
        private M_PromoCount initMod = new M_PromoCount();
        public B_PromoCount()
        {
            strTableName = initMod.TbName;
            PK = initMod.PK;
        }
        public DataTable Sel(int ID)
        {
            return Sql.Sel(strTableName, PK, ID);
        }
        public M_PromoCount SelReturnModel(int ID)
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
        private M_PromoCount SelReturnModel(string strWhere)
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
        public bool UpdateByID(M_PromoCount model)
        {
            return Sql.UpdateByIDs(strTableName, PK, model.id.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
        public bool Del(int ID)
        {
            return Sql.Del(strTableName, ID);
        }
        public int insert(M_PromoCount model)
        {
            return Sql.insert(strTableName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        public int GetInsert(M_PromoCount model)
        {
            string sqlStr = "INSERT INTO [dbo].[ZL_PromoCount] ([PromotionUrl],[sid],[linkCount]) VALUES (@PromotionUrl,@sid,@linkCount);select @@IDENTITY";
            SqlParameter[] cmdParams = model.GetParameters();
            return SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.Text, sqlStr, cmdParams));
        }
        public bool GetUpdate(M_PromoCount model)
        {
            string sqlStr = "UPDATE [dbo].[ZL_PromoCount] SET [PromotionUrl] = @PromotionUrl,[sid] = @sid,[linkCount] = @linkCount WHERE [id] = @id";
            SqlParameter[] cmdParams = model.GetParameters();
            return SqlHelper.ExecuteSql(sqlStr, cmdParams);
        }
		public bool DeleteByGroupID(int PromoCountID)
        {
            string sqlStr = "DELETE FROM [dbo].[ZL_PromoCount] WHERE [id] = @id";
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@id", SqlDbType.Int, 4);
            cmdParams[0].Value = PromoCountID;
            return SqlHelper.ExecuteSql(sqlStr, cmdParams);
        }
		public M_PromoCount GetSelect(int PromoCountID)
        {
            string sqlStr = "SELECT [id],[PromotionUrl],[sid],[linkCount] FROM [dbo].[ZL_PromoCount] WHERE [id] = @id";
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@id", SqlDbType.Int, 4);
            cmdParams[0].Value = PromoCountID;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sqlStr, cmdParams))
            {
                if (reader.Read())
                {
                    return initMod.GetModelFromReader(reader);
                }
                else
                {
                    return new M_PromoCount();
                }
            }
        }
		public DataTable Select_All()
        {
      		return Sel();
        }
         /// <summary>
        /// 通过商家ID查询
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public M_PromoCount GetSelectBysid(int sid)
        {
            string sqlStr = "SELECT [id],[PromotionUrl],[sid],[linkCount] FROM [dbo].[ZL_PromoCount] WHERE [sid] = @sid";
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@sid", SqlDbType.Int, 4);
            cmdParams[0].Value = sid;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sqlStr, cmdParams))
            {
                if (reader.Read())
                {
                    return initMod.GetModelFromReader(reader);
                }
                else
                {
                    return new M_PromoCount();
                }
            }
        }
	}
}
