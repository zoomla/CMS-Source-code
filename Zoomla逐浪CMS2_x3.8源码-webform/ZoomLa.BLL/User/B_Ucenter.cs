using System;
using System.Collections.Generic;
using ZoomLa.Model;
using System.Data;
using ZoomLa.SQLDAL;
using System.Data.SqlClient;

namespace ZoomLa.BLL
{
   public class B_Ucenter
    {
        private string PK, strTableName;
        private M_Ucenter initMod = new M_Ucenter();

        public B_Ucenter() 
        {
            PK = initMod.PK;
            strTableName = initMod.TbName;
        }
        public int Insert(M_Ucenter model)
        {
            return Sql.insert(strTableName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        public int Add(M_Ucenter model)
        {
            return Insert(model);
        }
        public bool Update(M_Ucenter model)
        {
            return UpdateMapByMid(model);
        }
        public DataTable Sel()
        {
            return Sql.Sel(strTableName);
        }
        public DataTable SelByField(string field,string value) 
        {
            SafeSC.CheckDataEx(field);
            string sql = "Select * From "+strTableName+" Where "+field+" =@value";
            SqlParameter[] sp = new SqlParameter[] {new SqlParameter("value",value) };
            return SqlHelper.ExecuteTable(CommandType.Text,sql,sp);
        }
        public DataTable Sel(string strWhere, string strOrderby)
        {
            return Sql.Sel(strTableName, strWhere, strOrderby);
        }
        public DataTable Sel(int ID)
        {
            return Sql.Sel(strTableName, PK, ID);
        }
        public int getnum()
        {
            return Sql.getnum(strTableName);
        }
        public int getnum(string strwhere)
        {
            return Sql.IsExistInt(strTableName, strwhere);
        }
        public M_Ucenter Select(int ID)
        {
            string sqlStr = "select * from " + strTableName + " where ID=@ID";
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@ID", SqlDbType.Int, 4);
            cmdParams[0].Value = ID;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sqlStr, cmdParams))
            {
                if (reader.Read())
                {
                    return initMod.GetModelFromReader(reader);
                }
                else
                {
                    return new M_Ucenter();
                }
            }
        }
        public M_Ucenter SelByKey(string key,int status=1)
        {
            string sql = "SELECT * FROM " + strTableName + " WHERE [Key]=@key";
            if (status != -1)
            {
                sql += " AND Status = " + status;
            }
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("Key", key) };
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sql, sp))
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
        public bool UpdateMapByMid(M_Ucenter model)
        {
            return Sql.UpdateByID(strTableName, "ID", model.ID, BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
        public bool Del(int ID)
        {
            return Sql.Del(strTableName, ID);
        }
        public bool Del(string strWhere)
        {
            return Sql.Del(strTableName, strWhere);
        }
    }
}
