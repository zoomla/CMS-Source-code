namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using ZoomLa.SQLDAL;
    using System.Data.SqlClient;
    using SQLDAL.SQL;
    using System.Collections.Generic;
    public class B_Client_Service
    {
        public B_Client_Service()
        {
            PK = initmod.PK;
            strTableName = initmod.TbName;
        }
        private string PK, strTableName;
        private M_Client_Service initmod = new M_Client_Service();
        public DataTable Sel(int ID)
        {
            return Sql.Sel(strTableName, PK, ID);
        }
        public M_Client_Service SelReturnModel(int ID)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(strTableName, PK, ID))
            {
                if (reader.Read())
                {
                    return initmod.GetModelFromReader(reader);
                }
                else
                {
                    return null;
                }
            }
        }
        private M_Client_Service SelReturnModel(string strWhere)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(strTableName, strWhere))
            {
                if (reader.Read())
                {
                    return initmod.GetModelFromReader(reader);
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
            string where = "1=1 ";
            List<SqlParameter> sp = new List<SqlParameter>();
            PageSetting setting = PageSetting.Single(cpage, psize, strTableName, PK, where, PK + " DESC", sp);
            DBCenter.SelPage(setting);
            return setting;
        }
        public bool UpdateByID(M_Client_Service model)
        {
            return Sql.UpdateByIDs(strTableName, PK, model.Flow.ToString(), model.GetFieldAndPara(), model.GetParameters(model));
        }
        public bool GetDelete(int Client_ServiceID)
        {
            return Sql.Del(strTableName, Client_ServiceID);
        }
        public int insert(M_Client_Service model)
        {
            return Sql.insert(strTableName, model.GetParameters(model), model.GetParas(), model.GetFields());
        }
        public int GetInsert(M_Client_Service model)
        {
            return insert(model); 
        }
        public bool GetUpdate(M_Client_Service model)
        {
            return UpdateByID(model);
        }
        public bool InsertUpdate(M_Client_Service model)
        {
            if (model.Flow > 0)
                UpdateByID(model);
            else
                insert(model);
            return true;
        }
        public M_Client_Service GetSelect(int ID)
        {
            return SelReturnModel(ID);
        }
        public DataTable Select_All()
        {
            return Sel();
        }
    }
}

