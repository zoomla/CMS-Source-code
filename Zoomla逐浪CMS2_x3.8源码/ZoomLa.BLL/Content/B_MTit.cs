namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using ZoomLa.SQLDAL;
    using System.Data.SqlClient;
    using SQLDAL.SQL;
    public class B_MTit
    {
        private string TbName, PK;
        private M_MTit initMod = new M_MTit();
        public B_MTit()
        {
            TbName = initMod.TbName;
            PK = initMod.PK;
        }
        public DataTable Sel(int ID)
        {
            return Sql.Sel(TbName, PK, ID);
        }
        public M_MTit SelReturnModel(int ID)
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
        private M_MTit SelReturnModel(string strWhere)
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
        public PageSetting SelPage(int cpage, int psize)
        {
            PageSetting setting = PageSetting.Single(cpage, psize, TbName, PK, "");
            DBCenter.SelPage(setting);
            return setting;
        }
        public bool UpdateByID(M_MTit model)
        {
            return Sql.UpdateByIDs(TbName, PK, model.I_id.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
        public bool Del(int ID)
        {
            return Sql.Del(TbName, ID);
        }
        public int insert(M_MTit model)
        {
            return Sql.insertID(TbName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        public int GetInsert(M_MTit model)
        {
            return Sql.insertID(TbName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        public int MaxOrMinOrderID(int I_id, int type)
        {
            string strsql = "";
            if (type == 1)
                strsql = "SELECT TOP 1 I_id FROM ZL_Mtit WHERE orderid>(SELECT TOP 1 orderid FROM ZL_Mtit WHERE I_id=@I_id) ORDER BY orderid";
            else
                strsql = "SELECT TOP 1 I_id FROM ZL_Mtit WHERE orderid<(SELECT TOP 1 orderid FROM ZL_Mtit WHERE I_id=@I_id) ORDER BY orderid desc";
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@I_id", SqlDbType.Int, 4);
            cmdParams[0].Value = I_id;
            return DataConverter.CLng(SqlHelper.ExecuteScalar(CommandType.Text, strsql, cmdParams));
        }
        public DataTable Select_All(int type)
        {
            string sqlStr = "";
            switch (type)
            {
                case 1:
                    sqlStr = "select * from ZL_MTit where Itype=1 order by orderID desc,I_id desc";
                    break;
                case 2:
                    sqlStr = "select * from ZL_MTit where Stype=1 order by orderID desc,I_id desc";
                    break;
                case 3:
                    sqlStr = "select * from ZL_MTit order by orderID desc,I_id desc";
                    break;
                case 4:
                    sqlStr = "select * from ZL_MTit order by orderID desc,I_id desc";
                    break;
                default:
                    sqlStr = "select * from ZL_MTit where Itype=1";
                    break;
            }
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, null);
        }
        public int MaxOrderID()
        {
            string strsql = "select max(orderid) from ZL_Mtit";
            return DataConverter.CLng(SqlHelper.ExecuteScalar(CommandType.Text, strsql, null));
        }
        public int UpOrderID(int I_id, int OrderID)
        {
            string strsql = "Update ZL_Mtit set OrderID=@OrderID where I_id=@I_id";
            SqlParameter[] cmdParams = new SqlParameter[2];
            cmdParams[0] = new SqlParameter("@I_id", SqlDbType.Int, 4);
            cmdParams[0].Value = I_id;
            cmdParams[1] = new SqlParameter("@OrderID", SqlDbType.Int, 4);
            cmdParams[1].Value = OrderID;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strsql, cmdParams);
        }
        public bool GetUpdate(M_MTit model)
        {
            return Sql.UpdateByIDs(TbName, PK, model.I_id.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
        public bool InsertUpdate(M_MTit model)
        {
            if (model.I_id > 0)
                GetUpdate(model);
            else
                GetInsert(model);
            return true;
        }
        public bool GetDelete(int ID)
        {
            return Sql.Del(TbName, PK + "=" + ID);
        }
        public M_MTit GetSelect(int ID)
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
        public DataTable Select_All()
        {
            return Sql.Sel(TbName, "", "");
        }
    }
}