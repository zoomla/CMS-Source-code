using System;
using System.Data;
using System.Configuration;
using ZoomLa.Model;
using ZoomLa.Common;
using ZoomLa.SQLDAL;
using System.Data.SqlClient;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLa.BLL
{
    /// <summary>
    /// B_DataList 的摘要说明
    /// </summary>
    public class B_DataList
    {
        public string strTableName ,PK;
        private M_DataList initMod = new M_DataList();
        public B_DataList()
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
        public M_DataList SelReturnModel(int ID)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(strTableName, PK, ID))
            {
                if (reader.Read())
                {
                    M_DataList model= initMod.GetModelFromReader(reader);
                    return model;
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
        public bool UpdateByID(M_DataList model)
        {
            return Sql.UpdateByIDs(strTableName, PK, model.ID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters(model));
        }
        public bool UpdateByField(string field,string value,int id) 
        {
            SafeSC.CheckDataEx(field);
            string sql = "Update "+strTableName+" Set "+field+"=@value Where ID="+id;
            SqlParameter[] sp = new SqlParameter[] {new SqlParameter("value",value) };
            SqlHelper.ExecuteNonQuery(CommandType.Text,sql,sp);
            return true;
        }
        public bool Update(string strSet, string strWhere)
        {
            return Sql.Update(strTableName, strSet, strWhere, null);
        }

        public bool Del(int ID)
        {
            return Sql.Del(strTableName, ID);
        }
        public int insert(M_DataList model)
        {
            return Sql.insert(strTableName, model.GetParameters(model), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        public bool Update_Author_ByID(M_DataList model)
        {
            return UpdateByID(model);
        }
    }
}