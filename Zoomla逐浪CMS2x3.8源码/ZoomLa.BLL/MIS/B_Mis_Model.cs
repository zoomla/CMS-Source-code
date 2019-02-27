using System;
using System.Data;
using ZoomLa.Model;
using ZoomLa.Common;
using ZoomLa.SQLDAL;
using System.Data.SqlClient;

namespace ZoomLa.BLL
{
    public class B_Mis_Model
    {
        public M_Mis_Model model = new M_Mis_Model();
        public string strTableName = "";
        public string PK = "";
        public DataTable dt = null;
        public B_Mis_Model()
        {
            strTableName = model.TbName;
            PK = model.PK;
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
        public M_Mis_Model SelReturnModel(int ID)
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
        public M_Mis_Model SelReturnModel(string strWhere)
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
        public DataTable SelWordHead() 
        {
            string sql = "SELECT * FROM " + strTableName + " WHERE WordPath IS NOT NULL AND WordPath !=''";
            return SqlHelper.ExecuteTable(CommandType.Text,sql);
        }
        public DataTable SelByDocType(int docType) 
        {
            string sql = "Select * From " + strTableName + " Where DocType=";
            if (docType == 0)
            {
                sql += docType+" OR DocType is Null";
            }
            else { sql += docType; }
            
            return SqlHelper.ExecuteTable(CommandType.Text,sql);
        }
        /// <summary>
        /// 插入数据
        /// </summary>
        public int insert(M_Mis_Model model)
        {
            return Sql.insert(strTableName, model.GetParameters(), model.GetParas(), model.GetFields());
        }
        /// <summary>
        /// 跟新数据
        /// </summary>
        public bool UpdateByID(M_Mis_Model model)
        {
            return Sql.UpdateByIDs(strTableName, PK, model.ID.ToString(), model.GetFieldAndPara(), model.GetParameters());
        }
        public bool DelByModelID(int id)
        {
            SqlParameter[] sp = new SqlParameter[]{ new SqlParameter("id",id)};
            return Sql.Del(strTableName, "ID=@id", sp);
        }
    }
}
