using System;
using System.Data;
using System.Configuration;
using ZoomLa.Model;
using ZoomLa.Components;
using ZoomLa.Common;
using System.Web;
using System.Globalization;
using ZoomLa.SQLDAL;
using System.Data.SqlClient;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLa.BLL
{
    public class B_StoreStyleTable
    {
        public string strTableName, PK;
        private M_StoreStyleTable initMod = new M_StoreStyleTable();
        public B_StoreStyleTable()
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
        public M_StoreStyleTable GetStyleByID(int ID)
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
        public bool UpdateByID(M_StoreStyleTable model)
        {
            return Sql.UpdateByIDs(strTableName, PK, model.ID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }

        public bool Update(string strSet, string strWhere)
        {
            return Sql.Update(strTableName, strSet, strWhere, null);
        }

        public bool Del(int ID)
        {
            return Sql.Del(strTableName, ID);
        }
        #region 添加店铺模板
        /// <summary>
        /// 添加店铺模板
        /// </summary>
        /// <param name="sst"></param>
        public void InsertStoreStyle(M_StoreStyleTable model)
        {
            Sql.insert(strTableName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        #endregion

        #region 修改店铺模板
        /// <summary>
        /// 修改店铺模板
        /// </summary>
        /// <param name="sst"></param>
        public void UpdateStoreStyle(M_StoreStyleTable model)
        {
            Sql.UpdateByIDs(strTableName, PK, model.ID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
        #endregion

        #region 查询所有模板
        /// <summary>
        /// 查询所有模板
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllStyle()
        {

            string sql = @"select * from ZL_StoreStyleTable order by id desc";
            return SqlHelper.ExecuteTable(CommandType.Text, sql, null);
        }
        #endregion

        #region 查询模型下的模板
        /// <summary>
        /// 查询模型下的模板
        /// </summary>
        /// <param name="modelid"></param>
        /// <param name="type">1启动 0停用 2没限制</param>
        /// <returns></returns>
        public DataTable GetStyleByModel(int modelid, int type)
        {
            try
            {
                string sql = "select * from ZL_StoreStyleTable where ModelID=" + modelid + "";
                if (type != 2)
                {
                    sql += "  and IsTrue=" + type + " order by id desc";
                }
                return SqlHelper.ExecuteTable(CommandType.Text, sql, null);
            }
            catch
            {
                throw;
            }
        }
        #endregion


        #region 删除店铺模板
        /// <summary>
        /// 删除店铺模板
        /// </summary>
        /// <param name="list"></param>
        public void DelStoreStyle(string list)
        {
            try
            {
                string sql = "delete from ZL_StoreStyleTable where (id in(" + list + "))";
                SqlHelper.ExecuteNonQuery(CommandType.Text, sql, null);
            }
            catch { }
        }
        #endregion

        #region 批量修改状态
        /// <summary>
        /// 批量修改状态
        /// </summary>
        /// <param name="list"></param>

        public void UpdateStoreState(string list, int state)
        {
            try
            {
                string[] listArr = list.Split(',');
                string sql = "";
                SqlParameter[] sp = new SqlParameter[listArr.Length];
                for (int i = 0; i < listArr.Length; i++)
                {
                    sp[i] = new SqlParameter("list" + i, listArr[i]);
                    sql += "@" + sp[i].ParameterName + ",";
                }
                sql = sql.TrimEnd(',');
                SqlHelper.ExecuteNonQuery(CommandType.Text, "update ZL_StoreStyleTable set  IsTrue=" + state + " where (id in(" + sql + "))", sp);
            }
            catch { }
        }
        #endregion

        #region 取某模型的一个模板
        /// <summary>
        /// 取某模型的一个模板
        /// </summary>
        /// <param name="modelid"></param>
        /// <returns></returns>

        public M_StoreStyleTable GetNewStyle(int modelid)
        {
            try
            {
                string sql = "select top 1 * from ZL_StoreStyleTable where ModelID=" + modelid + " and IsTrue=1";
                M_StoreStyleTable sst = new M_StoreStyleTable();
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sql, null))
                {
                    if (dr.Read())
                    {
                        ReadStoreStyle(dr, sst);
                    }
                }
                return sst;
            }
            catch
            {
                throw;
            }
        }
        #endregion
        #region 读取模板表
        /// <summary>
        /// 读取模板表
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="sst"></param>
        public void ReadStoreStyle(SqlDataReader dr, M_StoreStyleTable sst)
        {
            sst.ID = int.Parse(dr["ID"].ToString());
            sst.StyleName = dr["StyleName"].ToString();
            sst.StylePic = dr["StylePic"].ToString();
            sst.StyleUrl = dr["StyleUrl"].ToString();
            sst.ListStyle = dr["ListStyle"].ToString();
            sst.ContentStyle = dr["ContentStyle"].ToString();
            sst.ModelID = int.Parse(dr["ModelID"].ToString());
            sst.IsTrue = int.Parse(dr["IsTrue"].ToString());
            dr.Close();
            dr.Dispose();
        }
        #endregion
    }
}