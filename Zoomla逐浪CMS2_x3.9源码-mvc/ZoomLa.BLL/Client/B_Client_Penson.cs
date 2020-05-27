namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using ZoomLa.SQLDAL;
    using System.Data.SqlClient;
    using SQLDAL.SQL;
    using System.Collections.Generic;    /// <summary>
                                         /// B_Client_Penson 的摘要说明
                                         /// </summary>
    public class B_Client_Penson
    {
        public B_Client_Penson()
        {
            PK = initMod.PK;
            strTableName = initMod.TbName;
        }
        private string PK, strTableName;
        private M_Client_Penson initMod = new M_Client_Penson();
        public DataTable Sel(int ID)
        {
            return Sql.Sel(strTableName, PK, ID);
        }
        public M_Client_Penson SelReturnModel(int ID)
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
        public DataTable SelByCode(string code)
        {
            string sql = "SELECT * FROM " + strTableName + " WHERE Code=@code";
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("@code", code) };
            return SqlHelper.ExecuteTable(CommandType.Text, sql, sp);
        }
        private M_Client_Penson SelReturnModel(string strWhere)
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
        public PageSetting SelPage(int cpage, int psize, string code = "")
        {
            string where = "1=1 ";
            List<SqlParameter> sp = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(code)) { where += " AND code=@code"; sp.Add(new SqlParameter("code", code)); }
            PageSetting setting = PageSetting.Single(cpage, psize, strTableName, PK, where, PK + " DESC", sp);
            DBCenter.SelPage(setting);
            return setting;
        }
        public bool UpdateByID(M_Client_Penson model)
        {
            return Sql.UpdateByIDs(strTableName, PK, model.Flow.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters(model));
        }
        public bool Del(int ID)
        {
            return Sql.Del(strTableName, ID);
        }

        public bool Del(string strWhere)
        {
            return Sql.Del(strTableName, strWhere);
        }

        public int insert(M_Client_Penson model)
        {
            return Sql.insert(strTableName, initMod.GetParameters(model), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        public bool GetInsert(M_Client_Penson model)
        {
            return insert(model) > 0;
        }
        public bool GetUpdate(M_Client_Penson model)
        {
            return UpdateByID(model);
        }
        public bool InsertUpdate(M_Client_Penson model)
        {
            if (model.Flow > 0)
                UpdateByID(model);
            else
                insert(model);
            return true;
        }
        public bool GetDelete(int ID)
        {
            return Sql.Del(strTableName, PK + "=" + ID);
        }
        public M_Client_Penson GetSelect(int ID)
        {
            return SelReturnModel(ID);
        }
        public DataTable Select_All()
        {
            return SqlHelper.JoinQuery("A.Id_Code,A.Birthday,B.*", "ZL_Client_Penson", "ZL_Client_Basic", "A.Code=B.Code");
        }
        public M_Client_Penson GetPenSonByCode(string Code)
        {
            string sqlStr = "Select * From ZL_Client_Penson Where Code=@Code";
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@Code", SqlDbType.NVarChar, 255);
            cmdParams[0].Value = Code;

            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sqlStr, cmdParams))
            {
                if (reader.Read())
                {
                    return initMod.GetModelFromReader(reader);
                }
                else
                {
                    return new M_Client_Penson();
                }
            }
        }
    }
}
