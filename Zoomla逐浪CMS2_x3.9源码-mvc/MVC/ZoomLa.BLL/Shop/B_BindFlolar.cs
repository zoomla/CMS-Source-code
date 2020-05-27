namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using ZoomLa.SQLDAL;
    using System.Data.SqlClient;
	
    public class B_BindFlolar
    {
        private string strTableName, PK;
        private M_BindFlolar initMod = new M_BindFlolar();
        public B_BindFlolar()
        {
            strTableName = initMod.TbName;
            PK = initMod.PK;
        }
        public DataTable Sel(int ID)
        {
            return Sql.Sel(strTableName, PK, ID);
        }
        public M_BindFlolar GetSelect(int ID)
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
        private M_BindFlolar SelReturnModel(string strWhere)
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
        public DataTable Select_All()
        {
            return Sql.Sel(strTableName);
        }
        public bool GetUpdate(M_BindFlolar model)
        {
            return Sql.UpdateByIDs(strTableName, PK, model.ID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
        public bool DeleteByGroupID(int ID)
        {
            return Sql.Del(strTableName, ID);
        }
        public int GetInsert(M_BindFlolar model)
        {
            return Sql.insert(strTableName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        /// <summary>
        /// 根据商品ID删除
        /// </summary>
        /// <param name="sid">商品ID</param>
        /// <returns></returns>
        public bool DeleteBySid(int sid)
        {
            string sqlStr = "DELETE FROM [dbo].[ZL_BindFlolar] WHERE [Shopid] = @Shopid";
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@Shopid", SqlDbType.Int, 4);
            cmdParams[0].Value = sid;
            return SqlHelper.ExecuteSql(sqlStr, cmdParams);
        }
         /// <summary>
        /// 根据商品ID查询
        /// </summary>
        /// <param name="sid">商品ID</param>
        /// <returns></returns>
        public DataTable Select(int sid)
        {
            string sqlStr = "SELECT [ID],[Shopid],[BindSid],[Addtime],[Userid],[SNum] FROM [dbo].[ZL_BindFlolar] WHERE [Shopid]=@Shopid AND UserID !=-1";
            SqlParameter[] para = new SqlParameter[]{
                new SqlParameter("@Shopid",sid)
            };
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, para);
        }
         /// <summary>
        /// 根据商品ID查询
        /// </summary>
        /// <param name="sid">商品ID</param>
        /// <param name="userid">用户ID</param>
        /// <returns></returns>
        public DataTable Select(int sid, int userid)
        {
            string sqlStr = "SELECT [ID],[Shopid],[BindSid],[Addtime],[Userid],[SNum] FROM [dbo].[ZL_BindFlolar] WHERE [Shopid]=@Shopid AND UserID =@UserID";
            SqlParameter[] para = new SqlParameter[]{
                new SqlParameter("@Shopid",sid),
                new SqlParameter("@UserID",userid)
            };
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, para);
        }
	}
}
