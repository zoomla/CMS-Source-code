using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using ZoomLa.SQLDAL;
using System.Data.SqlClient;
using ZoomLa.Model.PlanSql;

namespace ZoomLa.BLL.PlanSql
{
    public class B_PlanSql
    {
        public string strTableName,PK;
        private M_PlanSql initMod = new M_PlanSql();
        public B_PlanSql()
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
        public DataTable SelByPlanID(int planid)
        {
            string sql = "SELECT * FROM " + strTableName + " WHERE PlanID="+planid+" AND statu>0";
            return SqlHelper.ExecuteTable(sql);
        }

        /// <summary>
        /// 根据ID查询一条记录
        /// </summary>
        public M_PlanSql SelReturnModel(int ID)
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
        /// <summary>
        /// 根据ID更新
        /// </summary>
        public bool UpdateByID(M_PlanSql model)
        {
            return Sql.UpdateByID(strTableName, PK, model.ID, BLLCommon.GetFieldAndPara(model), model.GetParameters(model));
        }

        public bool Update(string strSet, string strWhere)
        {
            return Sql.Update(strTableName, strSet, strWhere, null);
        }

        public bool Del(int ID)
        {
            return Sql.Del(strTableName, ID);
        }

        public bool Del(string strWhere)
        {
            return Sql.Del(strTableName, strWhere);
        }
        public int insert(M_PlanSql model)
        {
            return Sql.insertID(strTableName, model.GetParameters(model), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
    }
}