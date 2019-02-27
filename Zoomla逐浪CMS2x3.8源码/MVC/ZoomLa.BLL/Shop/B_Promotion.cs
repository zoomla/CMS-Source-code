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
    /// B_Promotion 的摘要说明
    /// </summary>
    public class B_Promotion
    {
        public string strTableName, PK;
        private M_Promotion initMod = new M_Promotion();
        public B_Promotion()
        {
            strTableName = initMod.TbName;
            PK = initMod.PK;
        }

        /// <summary>
        /// 根据ID查询一条记录
        /// </summary>
        public DataTable Sel(int ID)
        {
            return Sql.Sel(strTableName, PK, ID);
        }

        /// <summary>
        /// 根据ID查询一条记录
        /// </summary>
        public M_Promotion SelReturnModel(int ID)
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
        /// <summary>
        /// 查询所有记录
        /// </summary>
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
        /// <summary>
        /// 根据ID更新
        /// </summary>
        public bool UpdateByID(M_Promotion model)
        {
            return Sql.UpdateByIDs(strTableName, PK, model.id.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }

        public bool Update(string strSet, string strWhere)
        {
            return Sql.Update(strTableName, strSet, strWhere, null);
        }

        public bool Del(int ID)
        {
            return Sql.Del(strTableName, ID);
        }
        public int insert(M_Promotion model)
        {
            return Sql.insert(strTableName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        //private static readonly ID_Promotion dal = IDal.CreatePromotion();
		/// <summary>
		///添加记录
		/// </summary>
		/// <param name="Promotion"></param>
		/// <returns></returns>
		public int GetInsert(M_Promotion promotion)
        {
            string sqlStr = "INSERT INTO [dbo].[ZL_Promotion] ([userId],[LinkUrl],[PromoUrl],[type],[AllinaceUrl]) VALUES (@userId,@LinkUrl,@PromoUrl,@type,@AllinaceUrl);select @@IDENTITY";
            SqlParameter[] cmdParams = promotion.GetParameters();
            return SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.Text, sqlStr, cmdParams));
        }

		/// <summary>
		///更新记录
		/// </summary>
		/// <param name="Promotion"></param>
		/// <returns></returns>
		public bool GetUpdate(M_Promotion promotion)
        {
            string sqlStr = "UPDATE [dbo].[ZL_Promotion] SET [userId] = @userId,[LinkUrl] = @LinkUrl,[PromoUrl] = @PromoUrl,[type] = @type,[AllinaceUrl]=@AllinaceUrl WHERE [id] = @id";
            SqlParameter[] cmdParams = promotion.GetParameters();
            return SqlHelper.ExecuteSql(sqlStr, cmdParams);
        }

		/// <summary>
		///删除记录
		/// </summary>
		/// <param name="Promotion"></param>
		/// <returns></returns>
		public bool DeleteByGroupID(int PromotionID)
        {
            string sqlStr = "DELETE FROM [dbo].[ZL_Promotion] WHERE [id] = @id";
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@id", SqlDbType.Int, 4);
            cmdParams[0].Value = PromotionID;
            return SqlHelper.ExecuteSql(sqlStr, cmdParams);
        }

		/// <summary>
		///查找一条记录
		/// </summary>
		/// <param name="Promotion"></param>
		/// <returns></returns>
		public M_Promotion GetSelect(int PromotionID)
        {
            string sqlStr = "SELECT [id],[userId],[LinkUrl],[PromoUrl],[type],[AllinaceUrl] FROM [dbo].[ZL_Promotion] WHERE [id] = @id";
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@id", SqlDbType.Int, 4);
            cmdParams[0].Value = PromotionID;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sqlStr, cmdParams))
            {
                if (reader.Read())
                {
                    return initMod.GetModelFromReader(reader);
                }
                else
                {
                    return new M_Promotion();
                }
            }
        }

		/// <summary>
		///返回所有记录
		/// </summary>
		/// <returns></returns>
		public DataTable Select_All()
        {
      		return Sel();
        }

	}
}
