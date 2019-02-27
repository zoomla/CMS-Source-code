namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using ZoomLa.SQLDAL;
    using System.Data.SqlClient;
    using SQLDAL.SQL;
    public class B_CardType
    {
        public string strTableName ,PK;
        private M_CardType initMod = new M_CardType();
        public B_CardType()
        {
            strTableName = initMod.TbName;
            PK = initMod.PK;
        }
        public DataTable Sel(int ID)
        {
            return Sql.Sel(strTableName, PK, ID);
        }
        public PageSetting SelPage(int cpage, int psize)
        {
            PageSetting setting = PageSetting.Single(cpage, psize, strTableName, PK, "");
            DBCenter.SelPage(setting);
            return setting;
        }
        public M_CardType SelReturnModel(int ID)
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
        public DataTable SelectAll()
        {
            return Sql.Sel(strTableName);
        }
        public bool UpdateByID(M_CardType model)
        {
            return Sql.UpdateByIDs(strTableName, PK, model.c_id.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters(model));
        }
        public bool Del(int ID)
        {
            return Sql.Del(strTableName, ID);
        }
        public int insert(M_CardType model)
        {
            return Sql.insert(strTableName, model.GetParameters(model), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        #region ID_CardType 成员
        public bool Add(M_CardType card)
        {
            string strSql = "insert into ZL_CardType (typename,iscount) values (@typename,@iscount)";
            SqlParameter[] sp = new SqlParameter[] {
                new SqlParameter("@typename", SqlDbType.NVarChar,100),
                new SqlParameter("@iscount", SqlDbType.Decimal)
              
            };
            sp[0].Value = card.typename;
            sp[1].Value = card.iscount;
            return SqlHelper.ExecuteSql(strSql, sp);
        }
      
        public bool UpCardType(M_CardType c)
        {
            string strSql = "Update zl_cardtype set typename=@typename,iscount=@iscount Where c_id=@c_id";
            SqlParameter[] cmdParams = new SqlParameter[] {
                new SqlParameter("@typename", SqlDbType.NVarChar),
                new SqlParameter("@iscount", SqlDbType.Decimal),  
                new SqlParameter("@c_id", SqlDbType.Int)
            };
            cmdParams[0].Value = c.typename;
            cmdParams[1].Value = c.iscount;
            cmdParams[2].Value = c.c_id;
            return SqlHelper.ExecuteSql(strSql, cmdParams);
        }
        public M_CardType SelectType(int cid)
        {
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@cid", SqlDbType.Int);
            cmdParams[0].Value = cid;
            string strsql = "select * from zl_cardtype where c_id=@cid";
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, strsql, cmdParams))
            {
                if (reader.Read())
                {
                    return initMod.GetModelFromReader(reader);
                }
                else
                {
                    return new M_CardType();
                }
            }
        }
        public bool Delall(string c_id)
        {
            string sqlStr = "DELETE FROM zl_cardtype WHERE c_id in(" + c_id + ")";
            //SqlParameter[] cmdParams = new SqlParameter[1];
            //cmdParams[0] = new SqlParameter("@c_id", SqlDbType.NVarChar);
            //cmdParams[0].Value = c_id;

            return SqlHelper.ExecuteSql(sqlStr, null);
        }
        public bool delid(int id)
        {
            string strSql = "DELETE FROM zl_cardtype WHERE c_id=@id";

            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@id", SqlDbType.Int, 4);
            cmdParams[0].Value = id;

            return SqlHelper.ExecuteSql(strSql, cmdParams);
        }
   
        #endregion
    }
}
