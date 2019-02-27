namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using ZoomLa.SQLDAL;
    using System.Data.SqlClient;
    using SQLDAL.SQL;
    public class B_ServiceSeat : ZoomLa.BLL.ZL_Bll_InterFace<M_ServiceSeat>
    {
        private string strTableName, PK;
        private M_ServiceSeat initMod = new M_ServiceSeat();
        public B_ServiceSeat()
        {
            strTableName = initMod.TbName;
            PK = initMod.PK;
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
        public DataTable Sel(int ID)
        {
            return Sql.Sel(strTableName, PK, ID);
        }
        public M_ServiceSeat SelReturnModel(int ID)
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
        public DataTable SelBySUid(int uid)
        {
            string sql = "SELECT * FROM " + strTableName + " WHERE S_AdminID=" + uid;
            return SqlHelper.ExecuteTable(CommandType.Text, sql);
        }
        public bool UpdateByID(M_ServiceSeat model)
        {
            return Sql.UpdateByIDs(strTableName, PK, model.S_ID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters(model));
        }
        public bool Update(string strSet, string strWhere)
        {
            return Sql.Update(strTableName, strSet, strWhere, null);
        }
        public bool Del(int ID)
        {
            return Sql.Del(strTableName, ID);
        }
        public void DelByIDS(string ids)
        {
            SafeSC.CheckIDSEx(ids);
            string sql = "DELETE FROM " + strTableName + " WHERE S_ID IN (" + ids + ")";
            SqlHelper.ExecuteSql(sql);
        }
        public int Insert(M_ServiceSeat model)
        {
            return Sql.insert(strTableName, model.GetParameters(model), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        public int GetInsert(M_ServiceSeat model)
        {
            return Insert(model);
        }
        public bool GetUpdate(M_ServiceSeat model)
        {
            return UpdateByID(model);
        }
        public M_ServiceSeat GetSelectByaid(int adminid)
        {
            string sqlStr = "SELECT * FROM ZL_ServiceSeat WHERE S_AdminID=" + adminid;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sqlStr))
            {
                if (reader.Read())
                {
                    return initMod.GetModelFromReader(reader);
                }
                else
                {
                    return new M_ServiceSeat();
                }
            }
        }
        public bool InsertUpdate(M_ServiceSeat model)
        {
            if (model.S_ID > 0)
                UpdateByID(model);
            else Insert(model);
            return true;
        }
        public bool GetDelete(int ID)
        {
            return Sql.Del(strTableName, PK + "=" + ID);
        }
        public M_ServiceSeat GetSelect(int ID)
        {
            return SelReturnModel(ID);
        }
        public DataTable Select_All()
        {
            return Sel();
        }
        public bool InsertUpdateByAdminid(M_ServiceSeat model)
        {
            if (model.S_AdminID > 0)
                UpdateByID(model);
            else
                Insert(model);
            return true;
        }
        public bool UpdateDefault(int defaultids)
        {
            string sqlStr = "update ZL_ServiceSeat set S_Default=2 where S_Default=1";
            return SqlHelper.ExecuteSql(sqlStr, null);
        }
    }
}
