namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Collections.Generic;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using ZoomLa.SQLDAL;
    using SQLDAL.SQL;
    public class B_ArticlePromotion
    {
        private string strTableName, PK;
        private M_ArticlePromotion initMod = new M_ArticlePromotion();
        public B_ArticlePromotion()
        {

            strTableName = initMod.TbName;
            PK = initMod.PK;
        }
        public DataTable Sel(int ID)
        {
            return Sql.Sel(strTableName, PK, ID);
        }
        public M_ArticlePromotion GetSelect(int ID)
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
        public M_ArticlePromotion SelReturnModel(string strWhere)
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
            return Sql.Sel(strTableName, null,"AddTime");
        }
        public PageSetting SelPage(int cpage, int psize)
        {
            PageSetting setting = PageSetting.Single(cpage, psize, strTableName, PK, "");
            DBCenter.SelPage(setting);
            return setting;
        }
        public DataTable SelWithCart()
        {
            string mtable = "(SELECT A.*,B.UserName FROM " + strTableName + " A LEFT JOIN ZL_User B ON A.PromotionUserID=B.UserID)";
            return DBCenter.JoinQuery("A.*,B.Proname,B.Shijia,B.Pronum,B.AllMoney", mtable, "ZL_CartPro", "A.CartProID=B.ID", "", PK + " DESC");
        }
        public bool GetUpdate(M_ArticlePromotion model)
        {
            return Sql.UpdateByIDs(strTableName, PK, model.Id.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
        public bool Update(string strSet, string strWhere)
        {
            return Sql.Update(strTableName, strSet, strWhere, null);
        }
        public bool DeleteByGroupID(int ID)
        {
            return Sql.Del(strTableName, ID);
        }
        public int insert(M_ArticlePromotion model)
        {
            return Sql.insert(strTableName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
		public int GetInsert(M_ArticlePromotion articlePromotion)
        {
            string sqlStr = "INSERT INTO [dbo].[ZL_ArticlePromotion] ([PromotionUserId],[CartProId],[PromotionUrl],[IsBalance],[IsEnable],[AddTime],[BalanceTime],[RebatesId]) VALUES (@PromotionUserId,@CartProId,@PromotionUrl,@IsBalance,@IsEnable,@AddTime,@BalanceTime,@RebatesId);SET @Id = SCOPE_IDENTITY()";
            SqlParameter[] cmdParams = articlePromotion.GetParameters();
            return SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.Text, sqlStr, cmdParams));
        }
		public M_ArticlePromotion GetSelectBySqlParams(string strSql,SqlParameter[] param)
        {
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, strSql, param))
            {
                if (reader.Read())
                {
                    return initMod.GetModelFromReader(reader);
                }
                else
                {
                    return new M_ArticlePromotion();
                }
            }
        }
        /// <summary>
        /// 记录总价
        /// </summary>
        /// <returns></returns>
        public object SetCount(int count)
        {
            string sqlStr = "select sum(allmoney) from ZL_CartPro where id in (select cartproid from ZL_ArticlePromotion where PromotionUserId=@PromotionUserId and isenable=1 and isbalance=0)";
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@PromotionUserId", SqlDbType.Int, 4);
            cmdParams[0].Value = count;
            return SqlHelper.ExecuteScalar(CommandType.Text, sqlStr, cmdParams);
        }

}
}
