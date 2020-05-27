
namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using ZoomLa.SQLDAL;
    using System.Data.SqlClient;

    public class B_Manufacturers
    {
        private string TbName, PK;
        private M_Manufacturers initMod = new M_Manufacturers();
        public B_Manufacturers() 
        {
            TbName = initMod.TbName;
            PK = initMod.PK;
        }
        public DataTable Sel(int ID)
        {
            return Sql.Sel(TbName, PK, ID);
        }
        public M_Manufacturers SelReturnModel(int ID)
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
        private M_Manufacturers SelReturnModel(string strWhere)
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
        public bool UpdateByID(M_Manufacturers model)
        {
            return Sql.UpdateByIDs(TbName, PK, model.id.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters(model));
        }
        public bool Del(int ID)
        {
            return Sql.Del(TbName, ID);
        }
        public int insert(M_Manufacturers model)
        {
            return Sql.insertID(TbName, model.GetParameters(model), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        public bool Add(M_Manufacturers model)
        {
             Sql.insertID(TbName, model.GetParameters(model), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
             return true;
        }
        public bool DeleteByID(int ID)
        {
            return Sql.Del(TbName, ID);
        }
        public bool DeleteBylist(string ids)
        {
            SafeSC.CheckIDSEx(ids);
            string sqlStr = "delete from ZL_Manufacturers where (id in (" + ids + "))";
            return SqlHelper.ExecuteSql(sqlStr, null);
        }
        public bool Update(M_Manufacturers model)
        {
            return Sql.UpdateByIDs(TbName, PK, model.id.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters(model));
        }
        public M_Manufacturers GetManufacturersByid(int ID)
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

        public DataTable GetManufacturersAll(string where,SqlParameter[] sp=null)
        {
            string strSql = "select * from ZL_Manufacturers" + where;
            return SqlHelper.ExecuteTable(CommandType.Text, strSql, sp);
        }
        public bool Upotherdata(string str, string id)
        {
            //return dal.Upotherdata(str,id);
            int sid = DataConverter.CLng(id);
            string sqlStr = "";
            string strclass = str;
            switch (strclass)
            {
                case "1":
                    sqlStr = "update ZL_Manufacturers set Disable=1 where id=" + sid + "";
                    break;
                case "2":
                    sqlStr = "update ZL_Manufacturers set Istop=1 where id=" + sid + "";
                    break;
                case "3":
                    sqlStr = "update ZL_Manufacturers set Isbest=1 where id=" + sid + "";
                    break;
                case "4":
                    sqlStr = "update ZL_Manufacturers set Disable=0 where id=" + sid + "";
                    break;
                case "5":
                    sqlStr = "update ZL_Manufacturers set Istop=0 where id=" + sid + "";
                    break;
                case "6":
                    sqlStr = "update ZL_Manufacturers set Isbest=0 where id=" + sid + "";
                    break;
                default:
                    sqlStr = "update ZL_Manufacturers set Producername=Producername where id=" + sid + "";
                    break;
            }
            return SqlHelper.ExecuteSql(sqlStr, null);
        }
    }
}
