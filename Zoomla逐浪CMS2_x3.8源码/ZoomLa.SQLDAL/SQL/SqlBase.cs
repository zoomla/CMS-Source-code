using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ZoomLa.Model;

namespace ZoomLa.SQLDAL.SQL
{
    public abstract class SqlBase
    {
        public abstract string DBType { get; }
        public string ConnectionString = "";
        /// <summary>
        /// 根据模型,生成SQL,为性能考虑,勿调用多次
        /// </summary>
        public abstract string GetSQL(SqlModel model);
        public abstract void ExecuteNonQuery(SqlModel model);
        public abstract DataTable ExecuteTable(SqlModel model);
        public abstract DbDataReader ExecuteReader(SqlModel model);
        public abstract object ExecuteScala(SqlModel model);
        public abstract int InsertID(SqlModel model);
        //--------------------分页与联结查询
        /// <summary>
        /// 支持单表和多表[四张联接查询]
        /// </summary>
        public abstract DataTable SelPage(PageSetting config);
        public abstract DataTable JoinQuery(PageSetting config);
        /// <summary>
        /// 可不需要指定Order与主键,取前几条数据,主用于标签
        /// </summary>
        public abstract DataTable SelTop(PageSetting config);
        //--------------------Model方法(字段,参数,字段=参数)
        public abstract string GetWhereSql(SqlModel model);
        public abstract string GetDateSql(Sql_Where where);
        public abstract string GetFields(M_Base model);
        public abstract string GetParams(M_Base model);
        public abstract string GetFieldAndParam(M_Base model);
        //--------------------表与数据库操作
        public abstract bool Table_Exist(string name);
        public abstract bool Table_Add(string table,M_SQL_Field field);
        public abstract bool Table_Remove(string name);
        public abstract bool Table_Clear(string name);
        public abstract DataTable Table_List();
        public abstract bool View_Exist(string name);
        public abstract bool View_Drop(string name);
        public abstract bool Field_Add(string table, M_SQL_Field field);
        public abstract bool Field_Remove(string table, string field);
        public abstract DataTable Field_List(string table);
        public abstract bool DB_Create(string dbname);
        public abstract bool DB_Remove(string dbname);
        public abstract bool DB_Exist(string dbName);
        public abstract bool DB_Attach(string mdfSource, string logSource, string dbName);
        //--------------------公用方法
        public static SqlBase CreateHelper(string dbtype)
        {
            switch (dbtype.ToLower())
            {
                case "oracle":
                    return new OracleHelper();
                case "mysql":
                    return new MySqlHelper();
                case "localdb":
                case "mssql":
                    return new MSSqlHelper();
                case "access":
                    return new AccessHelper();
                case "excel":
                    return new ExcelHelper();
                //case "xml":
                //    return null;
                //case "sqlite":
                //    break;
                //case "sqlce":
                //    break;
                default:
                    throw new Exception("数据库类型[" + dbtype + "]不存在");
            }
        }
        public static void ExceptionDeal(Exception ex, string sql, List<SqlParameter> splist)
        {
            SqlParameter[] sp = null;
            if (splist != null) { sp = splist.ToArray(); }
            ExceptionDeal(ex, sql, sp);
        }
        public static void ExceptionDeal(Exception ex, string sql, SqlParameter[] sp = null)
        {
            string values = "";
            if (sp != null && sp.Length > 0)
            {
                foreach (SqlParameter param in sp)
                {
                    if (sp == null) { continue; }
                    if (param.Value == null || param.Value == DBNull.Value) { param.Value = ""; }
                    values += param.ParameterName + "|" + param.Value + ",";
                }
            }
            string error = "";
            if (ex != null) { error += "Message:" + ex.Message + ","; }
            error += "SQL:" + sql + ",SP:" + values;
            throw new Exception(error);
        }
        /// <summary>
        /// 书写时以MSSql为准,其余将其替换为自己的占位符,Oracle为:
        /// </summary>
        public static string ReplaceParamPreFix(string value, string prefix = ":")
        {
            value = value.Trim();
            if (string.IsNullOrEmpty(value)) { return value; }
            if (value.IndexOf("@") == 0) { value = prefix + value.Substring(1, value.Length - 1); }
            return value;
        }
        /// <summary>
        /// 获取当前页
        /// </summary>
        public static int GetCPage(int cpage, int min, int max)
        {
            if (cpage > max) { cpage = max; }
            if (cpage < min) { cpage = min; }
            if (cpage < 0) { cpage = min; }
            return cpage;
        }
        /// <summary>
        /// 计算分页数量
        /// </summary>
        /// <param name="itemCount">数据条数</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns>页面数</returns>
        public static int GetPageCount(int itemCount, int pageSize) { return itemCount / pageSize + ((itemCount % pageSize > 0) ? 1 : 0); }
    }
    //SqlMain main = new MSSqlHelper();
    //main.ConnectionString = SqlHelper.ConnectionString;
    //DataTable dt = main.ExecuteTable(new SqlModel()
    //{
    //    cmd = SqlModel.SqlCmd.Select,
    //    tbName = "ZL_User"
    //});
}
