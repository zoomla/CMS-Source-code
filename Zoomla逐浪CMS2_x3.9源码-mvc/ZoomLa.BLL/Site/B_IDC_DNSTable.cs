using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ZoomLa.Model.Site;
using ZoomLa.SQLDAL;

namespace ZoomLa.BLL.Site
{
    public class B_IDC_DNSTable
    {
        private M_IDC_DNSTable model = new M_IDC_DNSTable();
        public string strTableName = "";
        public string PK = "";

        public B_IDC_DNSTable() 
        {
            strTableName = model.TbName;
            PK = model.PK;
        }
        //-----------------Retrieve
        /// <summary>
        /// 根据UserID，获取其组权限模型
        /// </summary>
        public M_IDC_DNSTable SelByID(string id)
        {
            // strSql = "SELECT * FROM " + strTableName + " " + strWhere;
            SqlParameter[] sp = new SqlParameter[] {new SqlParameter("id",id) };
            using (SqlDataReader reader = Sql.SelReturnReader(strTableName, "Where id = @id", sp))
            {
                if (reader.Read())
                {
                    return model.GetModelFromReader(reader);
                }
                else return null;
            }
        }
        /// <summary>
        /// 找到第一个返回模型
        /// </summary>
        public M_IDC_DNSTable SelByUserID(string id)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("id", id) };
            using (SqlDataReader reader = Sql.SelReturnReader(strTableName, "Where SiteManager = @id", sp))
            {
                if (reader.Read())
                {
                    return model.GetModelFromReader(reader);
                }
                else return null;
            }
        }
        public DataTable SelAllByUserID(string id)
        {
            SqlParameter[] sp = new SqlParameter[] {new SqlParameter("id",id) };
            DataTable dt=SqlHelper.ExecuteTable(CommandType.Text,"Select * From "+strTableName+" Where SiteManager = @id",sp);
            return dt;
        }
        public DataTable SelAll() 
        {
            DataTable dt = SqlHelper.ExecuteTable(CommandType.Text, "Select * From " + strTableName);
            return dt;
        }
        /// <summary>
        /// 根据列名与关键字,获取数据,1:IP,2,域名,3用户
        /// </summary>
        /// <param name="colName">列名</param>
        /// <param name="key">关键字</param>
        public DataTable SelAll(string key) 
        {
            SqlParameter[] sp = new SqlParameter[] {new SqlParameter("key","%"+key+"%") };
            DataTable dt = SqlHelper.ExecuteTable(CommandType.Text, "Select * From " + strTableName + " Where IP like @key or Domain like @key", sp);
            return dt;
        }
        //-----------------Insert
        public int Insert(M_IDC_DNSTable model)
        {
            return Sql.insert(strTableName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        //-----------------Update
        /// <summary>
        /// 更新信息
        /// </summary>
        public bool UpdateModel(M_IDC_DNSTable model)
        {
            return Sql.UpdateByIDs(strTableName, PK, model.ID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
        //-----------------Delete
        public bool DeleteByID(string id) 
        {
            SqlParameter[] sp = new SqlParameter[] {new SqlParameter("id",id) };
            return SqlHelper.ExecuteNonQuery(CommandType.Text, "Delete " + strTableName + " Where ID=@id", sp) > 0;
        }
    }
}
