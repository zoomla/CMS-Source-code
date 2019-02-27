namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using ZoomLa.SQLDAL;
    using System.Data.SqlClient;
    using SQLDAL.SQL;
    /// <summary>
    /// B_BossInfo 的摘要说明
    /// </summary>
    public class B_BossInfo
    {

        public string strTableName ,PK;
        private M_BossInfo initMod = new M_BossInfo();
        public B_BossInfo()
        {
            strTableName = initMod.TbName;
            PK = initMod.PK;
        }
        /// <summary>
        /// 根据ID查询一条记录
        /// </summary>
        public DataTable Sel(int ID)
        {
            return Sql.Sel(strTableName, PK, ID);
        }

        /// <summary>
        /// 根据ID查询一条记录
        /// </summary>
        public M_BossInfo SelReturnModel(int ID)
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
        /// <summary>
        /// 查询所有记录
        /// </summary>
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
        /// <summary>
        /// 根据ID更新
        /// </summary>
        public bool UpdateByID(M_BossInfo model)
        {
            return Sql.UpdateByIDs(strTableName, PK, model.nodeid.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters(model));
        }

        public bool Update(string strSet, string strWhere)
        {
            return Sql.Update(strTableName, strSet, strWhere, null);
        }

        public bool Del(int ID)
        {
            return Sql.Del(strTableName, ID);
        }
        public bool DelByNodeId(int nid)
        {
            string sql = "DELETE FROM " + strTableName + " WHERE nodeid="+nid;
            return SqlHelper.ExecuteSql(sql);
        }
        public M_BossInfo GetSelectUserid(int userid)
        {
            return SelReturnModel(" Where UserID=" + userid);
        }
        private M_BossInfo SelReturnModel(string strWhere)
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
        public int insert(M_BossInfo model)
        {
            return Sql.insert(strTableName, model.GetParameters(model), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        /// <summary>
        ///添加记录
        /// </summary>
        /// <param name="BossInfo"></param>
        /// <returns></returns>
        public int GetInsert(M_BossInfo model)
        {
            return insert(model);
        }
        public M_BossInfo GetSelectName(string Cname)
        {
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@Cname", SqlDbType.NVarChar, 255);
            cmdParams[0].Value = Cname;
            string strSql = "select top 1 * from ZL_bossinfo where Cname LIKE '%" + Cname + "%'";
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, strSql, cmdParams))
            {
                if (reader.Read())
                {
                    return initMod.GetModelFromReader(reader);
                }
                else
                {
                    return new M_BossInfo();
                }
            }
        }
        public bool GetUpdateChild(int nodeid)
        {
            string sqlStr = "update ZL_BossInfo set Childs=Childs+1 where nodeid=@nodeid";
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@nodeid", SqlDbType.Int, 4);
            cmdParams[0].Value = nodeid;
            if (SqlHelper.ExecuteNonQuery(CommandType.Text, sqlStr, cmdParams) > 0)
                return true;
            else
                return false;
        }
        /// <summary>
        ///更新记录
        /// </summary>
        /// <param name="BossInfo"></param>
        /// <returns></returns>
        public bool GetUpdate(M_BossInfo model)
        {
            return UpdateByID(model);
        }

        /// <summary>
        ///不存在则添加否则更新
        /// </summary>
        /// <param name="BossInfo"></param>
        /// <returns></returns>
        public bool InsertUpdate(M_BossInfo model)
        {
            if (model.nodeid > 0)
                UpdateByID(model);
            else
                insert(model);
            return true;
        }

        /// <summary>
        ///删除记录
        /// </summary>
        /// <param name="BossInfo"></param>
        /// <returns></returns>
        public bool GetDelete(int ID)
        {
            return Sql.Del(strTableName, PK + "=" + ID);
        }

        public bool DeleteByList(string str)
        {
            string sqlStr = "delete from ZL_BossInfo where (nodeid in (" + str + "))";
            return SqlHelper.ExecuteSql(sqlStr, null);
        }

        /// <summary>
        ///查找一条记录
        /// </summary>
        /// <param name="BossInfo"></param>
        /// <returns></returns>
        public M_BossInfo GetSelect(int ID)
        {
            return SelReturnModel(ID);
        }
        /// <summary>
        ///返回所有记录
        /// </summary>
        /// <returns></returns>
        public DataTable Select_All()
        {
            return Sel();
        }

    }
}
