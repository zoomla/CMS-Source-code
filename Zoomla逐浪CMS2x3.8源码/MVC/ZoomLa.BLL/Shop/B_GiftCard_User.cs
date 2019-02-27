namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using ZoomLa.SQLDAL;
    using System.Data.SqlClient;
    using SQLDAL.SQL;
    public class B_GiftCard_User
    {
        private string strTableName, PK;
        private M_GiftCard_User initMod = new M_GiftCard_User();
        public B_GiftCard_User() 
        {
            strTableName = initMod.TbName;
            PK = initMod.PK;
        }
        public DataTable Sel(int ID)
        {
            return Sql.Sel(strTableName, PK, ID);
        }
        public M_GiftCard_User SelReturnModel(int ID)
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
        private M_GiftCard_User SelReturnModel(string strWhere)
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
        public bool UpdateByID(M_GiftCard_User model)
        {
            return Sql.UpdateByIDs(strTableName, PK, model.id.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
        public bool Del(int ID)
        {
            return Sql.Del(strTableName, ID);
        }
        public int insert(M_GiftCard_User model)
        {
            return Sql.insert(strTableName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        public int GetInsert(M_GiftCard_User model)
        {
            string sqlStr = "INSERT INTO [dbo].[ZL_GiftCard_User] ([ShopCardId],[CardNO],[CardPass],[password],[CardType],[UserId],[OrderData],[confirmData],[State],[confirmState],[remark]) VALUES (@ShopCardId,@CardNO,@CardPass,@password,@CardType,@UserId,@OrderData,@confirmData,@State,@confirmState,@remark);SELECT @@IDENTITY;";
            SqlParameter[] cmdParams = model.GetParameters();
            return SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.Text, sqlStr, cmdParams));
        }
        public bool GetUpdate(M_GiftCard_User model)
        {
            string sqlStr = "UPDATE [dbo].[ZL_GiftCard_User] SET [ShopCardId] = @ShopCardId,[CardNO] = @CardNO,[CardPass] = @CardPass,[password] = @password,[CardType] = @CardType,[UserId] = @UserId,[OrderData] = @OrderData,[confirmData] = @confirmData,[State] = @State,[confirmState] = @confirmState,[remark]=@remark WHERE [id] = @id";
            SqlParameter[] cmdParams = model.GetParameters();
            return SqlHelper.ExecuteSql(sqlStr, cmdParams);
        }
		public bool DeleteByGroupID(int GiftCard_UserID)
        {
            string sqlStr = "DELETE FROM [dbo].[ZL_GiftCard_User] WHERE [id] = @id";
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@id", SqlDbType.Int, 4);
            cmdParams[0].Value = GiftCard_UserID;
            return SqlHelper.ExecuteSql(sqlStr, cmdParams);
        }
		public M_GiftCard_User GetSelect(int GiftCard_UserID)
        {
            string sqlStr = "SELECT [id],[ShopCardId],[CardNO],[CardPass],[password],[CardType],[UserId],[OrderData],[confirmData],[State],[confirmState],[remark] FROM [dbo].[ZL_GiftCard_User] WHERE [id] = @id";
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@id", SqlDbType.Int, 4);
            cmdParams[0].Value = GiftCard_UserID;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sqlStr, cmdParams))
            {
                if (reader.Read())
                {
                    return initMod.GetModelFromReader(reader);
                }
                else
                {
                    return new M_GiftCard_User();
                }
            }
        }
		public DataTable Select_All()
        {
            string sqlStr = "SELECT [id],[ShopCardId],[CardNO],[CardPass],[password],[CardType],[UserId],[OrderData],[confirmData],[State],[confirmState],[remark] FROM [dbo].[ZL_GiftCard_User]";
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, null);
        }
        /// <summary>
        /// 更新确认申请状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="state"></param>
        /// <param name="confData"></param>
        /// <returns></returns>
        public bool GetUpdate(int id, int confstate, DateTime confData)
        {
            string sqlStr = "UPDATE [dbo].[ZL_GiftCard_User] SET [confirmData] = @confirmData,[confirmState] = @confirmState WHERE [id] = @id";
            SqlParameter[] cmdParams = new SqlParameter[]{
                new SqlParameter("@confirmData",confData),
                new SqlParameter("@confirmState",confstate),
                new SqlParameter("@id",id)
            };
            return SqlHelper.ExecuteSql(sqlStr, cmdParams);
        }
        /// <summary>
        /// 更新兑换状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public bool GetUpdate(int id, int state)
        {
            string sqlStr = "UPDATE [dbo].[ZL_GiftCard_User] SET [State] = @State WHERE [id] = @id";
            SqlParameter[] cmdParams = new SqlParameter[]{
                new SqlParameter("@State",state),
                new SqlParameter("@id",id)
            };
            return SqlHelper.ExecuteSql(sqlStr, cmdParams);
        }
        /// <summary>
        /// 通过用户ID查询
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public DataTable GetSelectByUserid(int userid)
        {
            string sqlStr = "SELECT [id],[ShopCardId],[CardNO],[CardPass],[password],[CardType],[UserId],[OrderData],[confirmData],[State],[confirmState],[remark] FROM [dbo].[ZL_GiftCard_User] WHERE [UserId]=@UserId AND [confirmState]=1";
            SqlParameter[] para = new SqlParameter[]{
                new SqlParameter("@UserId",userid)
            };
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, para);
        }
	}
}
