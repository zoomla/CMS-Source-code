using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ZoomLa.SQLDAL.SQL
{
    public class SqlModel
    {
        public enum SqlCmd { Insert, Delete, Update, Select }
        public string connStr = "";
        public string tbName = "";
        public int itemNum = 0;
        //----查询条件
        /// <summary>
        /// Select,Insert,Update,Delete
        /// </summary>
        public SqlCmd cmd = SqlCmd.Select;
        /// <summary>
        /// 仅用于优化 mssql-->ExecuteReader
        /// </summary>
        public int size = 0;
        public string fields = "*";//CR
        public string pk = "";//C
        public string values = "";
        public string on = "";//R
        public string set = "";//U
        public string where = "";//CRUD
        public string order = "";
        //更新语句,后期是否更为键值对应
        //public List<SqlParam> paramList = new List<SqlParam>();//IU
        public List<Sql_Where> whereList = new List<Sql_Where>();//IU
        //参数化(统一使用SQL,再根据所属数据库转)
        public List<SqlParameter> spList = new List<SqlParameter>();
        //如有此语句,则忽略其他
        public string sql = "";
        //------------------------辅助语法糖
        public void AddSpToList(SqlParameter[] sp)
        {
            if (sp == null || sp.Length < 1) { return; }
            spList.AddRange(sp);
        }
        public SqlModel() { }
        public SqlModel(string sqlStr, SqlParameter[] sp) 
        {
            sql = sqlStr;
            if (sp != null) { spList.AddRange(sp); }
        }
    };
    ///// <summary>
    ///// 用于更新和插入,字段名:值
    ///// </summary>
    //public class SqlParam
    //{
    //    public string key = "";
    //    public string value = "";
    //}

    public class Sql_Where
    {
        public string type = "";//between
        public string join = "";//前缀符
        public string field = "";//字段
        //----Date专用
        public string stime = "";
        public string etime = "";
        //----
    }
}
