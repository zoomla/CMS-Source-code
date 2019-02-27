using System;
using System.Collections.Generic;
using System.Text;
using ZoomLa.Model;
using ZoomLa.Common;
using ZoomLa.SQLDAL;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLa.BLL
{
    public class B_SQL
    {
        public string strTableName, PK;
        private M_SQL initMod = new M_SQL();
        public B_SQL()
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
        public DataTable SelByTName(string tname)
        {
            string sql = "Select * From " + strTableName + " Where TableName=@tname";
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("tname", tname) };
            return SqlHelper.ExecuteTable(CommandType.Text, sql, sp);
        }
        /// <summary>
        /// 根据ID查询一条记录
        /// </summary>
        public M_SQL SelReturnModel(int ID)
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
        public PageSetting SelPage(int cpage, int psize, string tname = "")
        {
            string where = " 1=1";
            List<SqlParameter> sp = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(tname)) { where += " AND TableName=@tname"; sp.Add(new SqlParameter("tname", tname)); }
            PageSetting setting = PageSetting.Single(cpage, psize, strTableName, PK, where, "", sp);
            DBCenter.SelPage(setting);
            return setting;
        }
        /// <summary>
        /// 根据ID更新
        /// </summary>
        public bool UpdateByID(M_SQL model)
        {
            return Sql.UpdateByIDs(strTableName, PK, model.ID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters(model));
        }

        public bool Update(string strSet, string strWhere)
        {
            return Sql.Update(strTableName, strSet, strWhere, null);
        }

        public bool Del(int ID)
        {
            return Sql.Del(strTableName, ID);
        }

        public int insert(M_SQL model)
        {
            return Sql.insert(strTableName, model.GetParameters(model), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
    }
}