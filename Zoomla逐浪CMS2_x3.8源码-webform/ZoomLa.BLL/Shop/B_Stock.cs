namespace ZoomLa.BLL
{

    using System;
    using System.Data;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using ZoomLa.SQLDAL;
    using System.Data.SqlClient;
    public class B_Stock
    {
        private string TbName, PK;
        private M_Stock initMod = new M_Stock();
        public B_Stock()
        {
            TbName = initMod.TbName;
            PK = initMod.PK;
        }
        public DataTable Sel(int ID)
        {
            return Sql.Sel(TbName, PK, ID);
        }
        public M_Stock SelReturnModel(int ID)
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
        private M_Stock SelReturnModel(string strWhere)
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
        public bool UpdateByID(M_Stock model)
        {
            return Sql.UpdateByIDs(TbName, PK, model.id.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
        public bool Del(int ID)
        {
            return Sql.Del(TbName, ID);
        }
        public int insert(M_Stock model)
        {
            return Sql.insertID(TbName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        public DataTable AllStock()
        {
            string strSql = "select * from ZL_Stock order by(ID) desc";
            return SqlHelper.ExecuteTable(CommandType.Text, strSql, null);
        }
        public bool AddStock(M_Stock model)
        {
            string sqlStrs = "";
            if (model.stocktype == 0)
            {
                sqlStrs = "Update ZL_Commodities set Stock=Stock+" + model.Pronum + " where ID=" + model.proid;
            }
            else
            {
                sqlStrs = "Update ZL_Commodities set Stock=Stock-" + model.Pronum + " where ID=" + model.proid;

            }
            SqlHelper.ExecuteSql(sqlStrs);
            return Sql.insertID(TbName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model)) > 0;
        }
        public bool EditStock(M_Stock model)
        {
            return Sql.UpdateByIDs(TbName, PK, model.id.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
        public bool DeleteByID(int ID)
        {
            return Sql.Del(TbName, ID);
        }
        public DataTable SelectTypeByID(int typename)
        {
            string strSql = "select * from ZL_Stock where stocktype=" + typename + " order by(ID) desc";
            return SqlHelper.ExecuteTable(CommandType.Text, strSql, null);
        }
        public bool delstock(string ids)
        {
            SafeSC.CheckIDSEx(ids);
            return SqlHelper.ExecuteSql("delete from ZL_Stock where id in (" + ids + ")");
        }
        public M_Stock GetStockByid(int ID)
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
    }
}