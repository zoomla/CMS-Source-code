using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ZoomLa.SQLDAL;
using ZoomLa.Model;

namespace ZoomLa.BLL
{
    public class B_OA_BC
    {
        public M_OA_BC model = new M_OA_BC();
        public string strTableName = "";
        public string PK = "";
        public DataTable dt = null;
        public B_OA_BC() 
        {
            strTableName = model.TbName;
            PK = model.PK;
        }
        public int Insert(M_OA_BC model)
        {
            return Sql.insert(strTableName, model.GetParameters(model), model.GetParas(), model.GetFields());
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
        public M_OA_BC SelReturnModel(int ID)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(strTableName, PK, ID))
            {
                if (reader.Read())
                {
                    return model.GetModelFromReader(reader);
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 根据条件查询一条记录
        /// </summary>
        public M_OA_BC SelReturnModel(string strWhere)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(strTableName, strWhere))
            {
                if (reader.Read())
                {
                    return model.GetModelFromReader(reader);
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
        /// 删除记录
        /// </summary>
        public bool Del(int ID)
        {
            return Sql.Del(strTableName, ID);
        }
        /// <summary>
        /// 更新记录
        /// </summary>
        public bool UpdateByID(M_OA_BC model)
        {
            return Sql.UpdateByIDs(strTableName, PK, model.ID.ToString(), model.GetFieldAndPara(), model.GetParameters(model));
        }
    }
}
