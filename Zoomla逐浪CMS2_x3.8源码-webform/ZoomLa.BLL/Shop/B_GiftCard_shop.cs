namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using ZoomLa.SQLDAL;
    using System.Data.SqlClient;
    public class B_GiftCard_shop
    {
        private string strTableName, PK;
        private M_GiftCard_shop initMod = new M_GiftCard_shop();
        public B_GiftCard_shop() 
        {
            strTableName = initMod.TbName;
            PK = initMod.PK;
        }
        public DataTable Sel(int ID)
        {
            return Sql.Sel(strTableName, PK, ID);
        }
        public M_GiftCard_shop SelReturnModel(int ID)
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
        private M_GiftCard_shop SelReturnModel(string strWhere)
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
        public bool UpdateByID(M_GiftCard_shop model)
        {
            return Sql.UpdateByIDs(strTableName, PK, model.id.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
        public bool Del(int ID)
        {
            return Sql.Del(strTableName, ID);
        }
        public int insert(M_GiftCard_shop model)
        {
            return Sql.insert(strTableName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        public int GetInsert(M_GiftCard_shop model)
        {

            string sqlStr = "INSERT INTO [dbo].[ZL_GiftCard_shop] ([Period],[Cardinfo],[remark],[Points],[rebateVal],[Days],[Num],[ShopId],[PicUrl]) VALUES (@Period,@Cardinfo,@remark,@Points,@rebateVal,@Days,@Num,@ShopId,@PicUrl);SELECT @@IDENTITY;";
            SqlParameter[] cmdParams = model.GetParameters();
            return SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.Text, sqlStr, cmdParams));
        }
        public bool GetUpdate(M_GiftCard_shop model)
        {
            string sqlStr = "UPDATE [dbo].[ZL_GiftCard_shop] SET [Period] = @Period,[Cardinfo] = @Cardinfo,[remark] = @remark,[Points] = @Points,[rebateVal] = @rebateVal,[Days] = @Days,[Num] = @Num,[ShopId]=@ShopId,[PicUrl]=@PicUrl WHERE [id] = @id";
            SqlParameter[] cmdParams = model.GetParameters();
            return SqlHelper.ExecuteSql(sqlStr, cmdParams);
        }
		public bool DeleteByGroupID(int GiftCard_shopID)
        {
            string sqlStr = "DELETE FROM [dbo].[ZL_GiftCard_shop] WHERE [id] = @id";
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@id", SqlDbType.Int, 4);
            cmdParams[0].Value = GiftCard_shopID;
            return SqlHelper.ExecuteSql(sqlStr, cmdParams);
        }
		public M_GiftCard_shop GetSelect(int GiftCard_shopID)
        {
            string sqlStr = "SELECT [id],[Period],[Cardinfo],[remark],[Points],[rebateVal],[Days],[Num],[ShopId],[PicUrl] FROM [dbo].[ZL_GiftCard_shop] WHERE [id] = @id";
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@id", SqlDbType.Int, 4);
            cmdParams[0].Value = GiftCard_shopID;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sqlStr, cmdParams))
            {
                if (reader.Read())
                {
                    return initMod.GetModelFromReader(reader);
                }
                else
                {
                    return new M_GiftCard_shop();
                }
            }
        }
		public DataTable Select_All()
        {
            string sqlStr = "SELECT [id],[Period],[Cardinfo],[remark],[Points],[rebateVal],[Days],[Num],[ShopId],[PicUrl] FROM [dbo].[ZL_GiftCard_shop] ORDER BY ID DESC";
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, null);
        }
        /// <summary>
        /// 根据返利值查询
        /// </summary>
        /// <param name="RevateVal"></param>
        /// <returns></returns>
        public DataTable GetSelectByRevateVal(double RevateVal)
        {
            string sqlStr = "SELECT [id],[Period],[Cardinfo],[remark],[Points],[rebateVal],[Days],[Num],[ShopId],[PicUrl] FROM [dbo].[ZL_GiftCard_shop] WHERE [rebateVal]<=@RevateVal AND [Num]>0  ORDER BY ID DESC";
            SqlParameter[] para = new SqlParameter[]{
                 new SqlParameter("@RevateVal",RevateVal)
            };
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, para);
        }
         /// <summary>
        /// 更新数量
        /// </summary>
        /// <param name="id"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public bool GetUpdate(int id, int num)
        {
            string sqlStr = "UPDATE [dbo].[ZL_GiftCard_shop] SET [Num] = @Num WHERE [id] = @id";
            SqlParameter[] cmdParams = new SqlParameter[]{
                new SqlParameter("@Num",num),
                new SqlParameter("@id",id)
            };
            return SqlHelper.ExecuteSql(sqlStr, cmdParams);
        }
	}
}
