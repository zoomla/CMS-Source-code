namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using ZoomLa.SQLDAL;
    using System.Data.SqlClient;
    using SQLDAL.SQL;
    public class B_NodeRole
    {
        public B_NodeRole()
        {
            strTableName = initmod.TbName;
            PK = initmod.PK;
        }
        private string PK, strTableName;
        private M_NodeRole initmod = new M_NodeRole();
        public DataTable Sel(int ID)
        {
            return Sql.Sel(strTableName, PK, ID);
        }
        public M_NodeRole SelReturnModel(int ID)
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
        public M_NodeRole SelModelByRidAndNid(int nid, int rid)
        {
            return SelReturnModel("WHERE RID=" + rid + " AND NID=" + nid);
        }
        private M_NodeRole SelReturnModel(string strWhere)
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
        public PageSetting SelPage(int cpage, int psize, int nodeid = 0, string roleids = "")
        {
            string where = " 1=1";
            if (nodeid > 0) { where += " AND NID=" + nodeid; }
            if (!string.IsNullOrEmpty(roleids)) { SafeSC.CheckIDSEx(roleids); where += " AND RID IN (" + roleids + ")"; }
            PageSetting setting = PageSetting.Single(cpage, psize, strTableName, PK, where);
            DBCenter.SelPage(setting);
            return setting;
        }
        public bool UpdateByID(M_NodeRole model)
        {
            return Sql.UpdateByIDs(strTableName, PK, model.RN_ID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
        public bool Del(int ID)
        {
            return Sql.Del(strTableName, ID);
        }
        public int insert(M_NodeRole model)
        {
            return Sql.insert(strTableName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        public int GetInsert(M_NodeRole model)
        {
            return insert(model);
        }
        public DataTable GetSelectNodeANDRid(int Nodeid, string roleIDS)
        {
            SafeSC.CheckIDSEx(roleIDS);
            string sqlstr = "select * from ZL_NodeRole where NID=" + Nodeid + " and RID in (" + roleIDS + ")";
            return SqlHelper.ExecuteTable(CommandType.Text, sqlstr);
        }
        public bool GetUpdate(M_NodeRole model)
        {
            return UpdateByID(model);
        }
        public bool InsertUpdate(M_NodeRole model)
        {
            if (model.RN_ID > 0)
                UpdateByID(model);
            else
                insert(model);
            return true;
        }
        public bool GetDelete(int ID)
        {
            return Sql.Del(strTableName, PK + "=" + ID);
        }
        public M_NodeRole GetSelect(int ID)
        {
            return SelReturnModel(ID);
        }
        public DataTable Select_All()
        {
            return Sel();
        }
    }
}