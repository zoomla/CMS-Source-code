using System;
using System.Collections.Generic;
using System.Text;
using ZoomLa.Model;
using ZoomLa.Model.User;
using System.Data;
using ZoomLa.SQLDAL;
using System.Data.SqlClient;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLa.BLL.User
{
    public class B_UserMagazine
    {
        private string strTableName, PK;
        private M_Usermagazine initMod = new Model.User.M_Usermagazine();
        public B_UserMagazine()
        {
            strTableName = initMod.TbName;
            PK = initMod.PK;
        }
        public DataTable Sel(int ID)
        {
            return Sql.Sel(strTableName, PK, ID);
        }
        public DataTable SelByField(string field, string value)
        {
            SafeSC.CheckDataEx(field);
            string sql = "Select * From " + strTableName + " Where " + field + " =@value";
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("value", value) };
            return SqlHelper.ExecuteTable(CommandType.Text, sql, sp);
        }
        public DataTable SelByFieldL(string field, string value)
        {
            SafeSC.CheckDataEx(field);
            string sql = "Select * From " + strTableName + " Where " + field + " Like @value";
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("value", "%" + value + "%") };
            return SqlHelper.ExecuteTable(CommandType.Text, sql, sp);
        }
        public M_Usermagazine GetSelectById(int ID)
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
        private M_Usermagazine SelReturnModel(string strWhere)
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

        public DataTable SelByUid(int uid)
        {
            return Sql.Sel(strTableName, "UserId=" + uid, PK + " DESC", null);
        }
        public bool GetUpdata(M_Usermagazine model)
        {
            return Sql.UpdateByIDs(strTableName, PK, model.ID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
        public bool GetDelete(int ID)
        {
            return Sql.Del(strTableName, ID);
        }
        public int GetInsert(M_Usermagazine model)
        {
            return Sql.insert(strTableName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
    }
}
