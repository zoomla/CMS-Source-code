namespace ZoomLa.BLL
{
    using System.Data;
    using ZoomLa.Model;
    using ZoomLa.SQLDAL;
    using System.Data.SqlClient;
    using System.Collections.Generic;
    using SQLDAL.SQL;    /// <summary>
                         /// B_Client_Basic 的摘要说明
                         /// </summary>
    public class B_Client_Basic
    {
        private string TbName, PK;
        private M_Client_Basic initMod = new M_Client_Basic();
        public B_Client_Basic()
        {
            TbName = initMod.TbName;
            PK = initMod.PK;
        }
        public DataTable Sel(int ID)
        {
            return Sql.Sel(TbName, PK, ID);
        }
        public M_Client_Basic SelReturnModel(int ID)
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
        private M_Client_Basic SelReturnModel(string strWhere)
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
        public DataTable SelByType(int type, string Client_Group = "")
        {
            string wherestr = "1=1";
            if (type < 2)
            { wherestr += " AND Client_Type='" + type + "'"; }
            if (!string.IsNullOrEmpty(Client_Group))
            { wherestr += " AND Client_Group='" + Client_Group + "'"; }
            string sql = "SELECT A.*,B.UserName FROM " + TbName + " A LEFT JOIN ZL_User B ON A.FPManID=B.UserID WHERE " + wherestr;
            return SqlHelper.ExecuteTable(CommandType.Text, sql);
        }
        public PageSetting SelByType_SPage(int cpage, int psize, int userid, int type = -100, string cgroup = "")
        {
            string where = "1=1";
            List<SqlParameter> sp = new List<SqlParameter>();
            if (userid > 0) { where += " AND FPManID=" + userid; }
            if (type != -100) { where += " AND Client_Type=@type"; sp.Add(new SqlParameter("type", type)); }
            if (!string.IsNullOrEmpty(cgroup)) { where += " AND Client_Group=@cgroup"; sp.Add(new SqlParameter("cgroup", cgroup)); }
            PageSetting setting = PageSetting.Double(cpage,psize,TbName,"ZL_User","A.ID", "A.FPManID=B.UserID",where, "Flow DESC",sp, "A.*,B.UserName");
            DBCenter.SelPage(setting);
            return setting;
        }
        public DataTable Sel()
        {
            return Sql.Sel(TbName);
        }
        public PageSetting SelPage(int cpage, int psize, int type = 0, string client_group = "")
        {
            string where = "1=1 ";
            List<SqlParameter> sp = new List<SqlParameter>();
            if (type > 0) { where += " AND Client_Type = '" + type + "'"; }
            if (!string.IsNullOrEmpty(client_group)) { where += " AND Client_Group = @client_group"; sp.Add(new SqlParameter("client_group", client_group)); }
            PageSetting setting = PageSetting.Single(cpage, psize, TbName, PK, where, PK + " DESC", sp);
            setting.fields = "A.*,B.UserName";
            setting.t2 = "ZL_User";
            setting.on = "A.FPManID = B.UserID";
            return setting;
        }
        public bool UpdateByID(M_Client_Basic model)
        {
            return Sql.UpdateByIDs(TbName, "Flow", model.Flow.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters(model));
        }
        public bool GetDelete(int ID)
        {
            return Sql.Del(TbName, ID);
        }

        public void GetDeleteByCode(string code)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("code", code) };
            Sql.DelLabel(TbName, "code=@code", sp);
            Sql.DelLabel(TbName, "code=@code", sp);
            Sql.DelLabel(new M_Client_Penson().TbName, "code=@code", sp);
        }
        public int insert(M_Client_Basic model)
        {
            return Sql.insertID(TbName, model.GetParameters(model), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        public bool GetInsert(M_Client_Basic model)
        {
            return Sql.insert(TbName, model.GetParameters(model), BLLCommon.GetParas(model), BLLCommon.GetFields(model)) > 0;
        }
        public bool GetUpdate(M_Client_Basic model)
        {
            return Sql.UpdateByIDs(TbName, "Flow", model.Flow.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters(model));
        }
        public bool InsertUpdate(M_Client_Basic model)
        {
            if (model.Flow > 0)
                GetUpdate(model);
            else
                GetInsert(model);
            return true;
        }

        public DataTable SelByName(string name)
        {
            string sql = "Select * From " + TbName + " Where P_name=@name";
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("name", name) };
            return SqlHelper.ExecuteTable(CommandType.Text, sql, sp);
        }


        /// <summary>
        ///返回所有记录
        /// </summary>
        /// <returns></returns>
        public DataTable Select_All()
        {
            return Sql.Sel(TbName, "", "");
        }
        public DataTable SelectNew(int count)
        {
            string sqlStr = "SELECT ";
            if (count > 0)
            {
                sqlStr += "top " + count;
            }
            sqlStr += " * FROM ZL_Client_Basic ORDER BY Add_Date DESC";
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, null);
        }
        public M_Client_Basic GetSelect(int ID)
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
        public DataTable SelectCrmNoFPMan()
        {
            string sql = "Select * From " + TbName + " Where FPManID is Null or FPManID = '' or FPManID = 0 order By add_date Desc";
            return SqlHelper.ExecuteTable(CommandType.Text, sql, null);
        }
        /// <summary>
        /// 添加模型数据
        /// </summary>
        public int AddContent(DataTable ContentDT, string tbname)
        {
            int itemid = 0;
            if (!string.IsNullOrEmpty(tbname) && ContentDT.Rows.Count > 0)
            {
                itemid = DBCenter.Insert(tbname, BLLCommon.GetFields(ContentDT), BLLCommon.GetParas(ContentDT), BLLCommon.GetParameters(ContentDT).ToArray());
            }
            return itemid;
        }
        //得到模型数据
        public DataTable GetContent(string tbname, int itemid)
        {
            if (string.IsNullOrEmpty(tbname)) { return null; }
            return DBCenter.Sel(tbname, "ID=" + itemid);
        }
        /// <summary>
        /// 修改模型数据
        /// </summary>
        public void UpdateContent(DataTable ContentDT, string tbname, int itemid)
        {
            //需要重处理
            if (ContentDT != null && ContentDT.Rows.Count > 0)
            {
                List<SqlParameter> splist = new List<SqlParameter>();
                splist.AddRange(BLLCommon.GetParameters(ContentDT));
                if (DBCenter.IsExist(tbname, "ID=" + itemid))
                {
                    DBCenter.UpdateSQL(tbname, BLLCommon.GetFieldAndPara(ContentDT), "ID=" + itemid, splist);
                }
                else
                {
                    DBCenter.Insert(tbname, BLLCommon.GetFields(ContentDT), BLLCommon.GetParas(ContentDT), splist.ToArray());
                }
            }
        }

    }
}
