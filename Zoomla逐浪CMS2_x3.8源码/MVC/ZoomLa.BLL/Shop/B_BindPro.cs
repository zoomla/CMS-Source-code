namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using ZoomLa.SQLDAL;
    using System.Data.SqlClient;
    using SQLDAL.SQL;
    public class B_BindPro
    {
        private string TbName, PK;
        private M_BindPro initMod = new M_BindPro();
        public B_BindPro()
        {
            TbName = initMod.TbName;
            PK = initMod.PK;
        }
        public DataTable Sel(int ID)
        {
            return Sql.Sel(TbName, PK, ID);
        }
        public M_BindPro SelReturnModel(int ID)
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
        private M_BindPro SelReturnModel(string strWhere)
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
        public DataTable SelByProID(int proid, int type)
        {
            string strwhere = type == 1 ? "BindProID=" + proid : "Proid="+proid;
            string sql = "SELECT A.*,B.Thumbnails FROM " + TbName+ " A LEFT JOIN ZL_Commodities B ON A.ProID=B.ID WHERE " + strwhere;
            return SqlHelper.ExecuteTable(CommandType.Text, sql);
        }
        public bool DelByProID(int proid, int type)
        {
            string strwhere = type == 1 ? "BindProID=" + proid : "Proid=" + proid;
            string sql = "DELETE FROM "+TbName+" WHERE "+strwhere;
            return SqlHelper.ExecuteSql(sql);
        }
        public DataTable Sel()
        {
            return Sql.Sel(TbName);
        }
        public bool UpdateByID(M_BindPro model)
        {
            return Sql.UpdateByIDs(TbName, PK, model.ID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
        public bool Del(int ID)
        {
            return Sql.Del(TbName, ID);
        }
        public int insert(M_BindPro model)
        {
            return Sql.insertID(TbName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        public bool GetInsert(M_BindPro model)
        {
            Sql.insertID(TbName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
            return true;
        }
        public bool GetUpdate(M_BindPro model)
        {
            return Sql.UpdateByIDs(TbName, PK, model.ID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
        public bool InsertUpdate(M_BindPro model)
        {
            if (model.ID > 0)
                GetUpdate(model);
            else
                GetInsert(model);
            return true;
        }
        public bool GetDelete(int ID)
        {
            return Sql.Del(TbName, ID);
        }
        public M_BindPro GetSelect(int ID)
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
        public DataTable SelByProID(int id, string prolist)
        {
            SafeSC.CheckIDSEx(prolist);
            string sql = "Select * From " + TbName + " Where BindProID =" + id + " And ProID in (" + prolist + ")";
            return SqlHelper.ExecuteTable(CommandType.Text, sql);
        }
        public DataTable Select_All()
        {
            return Sql.Sel(TbName, "", "");
        }
        public PageSetting SelPage(int cpage, int psize)
        {
            PageSetting setting = PageSetting.Single(cpage, psize, TbName, PK, "");
            DBCenter.SelPage(setting);
            return setting;
        }

    }
}
