namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using ZoomLa.SQLDAL;
    using System.Data.SqlClient;
    public class B_Promotions
    {
        private string TbName, PK;
        private M_Promotions initMod = new M_Promotions();
        public B_Promotions() 
        {
            TbName = initMod.TbName;
            PK = initMod.PK;
        }
        public DataTable Sel(int ID)
        {
            return Sql.Sel(TbName, PK, ID);
        }
        public M_Promotions SelReturnModel(int ID)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(TbName, PK, ID))
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
        private M_Promotions SelReturnModel(string strWhere)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(TbName, strWhere))
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
            return Sql.Sel(TbName);
        }
        /// <summary>
        /// 根据ID更新
        /// </summary>
        public bool UpdateByID(M_Promotions model)
        {
            return Sql.UpdateByIDs(TbName, PK, model.Id.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters(model));
        }
        public bool Del(int ID)
        {
            return Sql.Del(TbName, ID);
        }
        public int insert(M_Promotions model)
        {
            return Sql.insertID(TbName, model.GetParameters(model), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        public bool Add(M_Promotions model)
        {
            Sql.insertID(TbName, model.GetParameters(model), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
            return true;
        }
        public bool DeleteByID(int ID)
        {
            return Sql.Del(TbName, ID);
        }
        public bool Update(M_Promotions model)
        {
            return Sql.UpdateByIDs(TbName, PK, model.Id.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters(model));
        }
        public DataTable GetPromotionsAll()
        {
            string strSql = "select * from ZL_Promotions order by(ID) desc";
            return SqlHelper.ExecuteTable(CommandType.Text, strSql, null);
        }
        public M_Promotions GetPromotionsByid(int ID)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(TbName, PK, ID))
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
        public DataTable GetPromotionsByUid(int id)
        {
            string sql = "Select * From " + TbName + " Where ID=" + id;
            return SqlHelper.ExecuteTable(CommandType.Text, sql);
        }
        public bool PromotionsColumnAdd(string strsql) 
        {
            return SqlHelper.ExecuteSql(strsql);
        }

        public DataTable PromotionsSearch(string key)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("key", "%" + key + "%") };
            string strSql = "select * from ZL_Promotions where Promoname like @key  order by(ID) desc";
            return SqlHelper.ExecuteTable(CommandType.Text, strSql, sp);
        }
         public DataTable PromotionsSearch(string select, string where, string order)
        {
            string strSql = "select ";
            if (!string.IsNullOrEmpty(select))
            {
                strSql += select + " from ";
            }
            strSql += "ZL_Promotions";
            if (!string.IsNullOrEmpty(where))
            {
                strSql += " where " + where;
            }
            if (!string.IsNullOrEmpty(order))
            {
                strSql += " Order by " + order;
            }
            return SqlHelper.ExecuteTable(CommandType.Text, strSql, null);
        }

    }
}
