using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlServerCe;
using System.Data;

namespace ZoomLa.SQLDAL
{
    public class SqlCEHelper
    {
        //string dbfile = Server.MapPath("MyDatabase#1.sdf");
        //
        public static DataTable ExecuteTable(string constr, string sql,SqlCeParameter[] sp=null)
        {
            DataTable dt = new DataTable();
            SqlCeConnection conn = null;
            SqlCeCommand cmd = null;
            try
            {
                conn = GetConn(constr);
                cmd = GetCommand(conn, sql, sp);
                SqlCeDataReader reader = cmd.ExecuteReader();
                dt.Load(reader);
                return dt;
            }
            catch (Exception ex) { throw new Exception(ex.Message + ":" + sql); }
            finally { if (conn != null) { conn.Dispose(); } if (cmd != null) { cmd.Dispose(); } }
        }
        public static void InsertID(string constr, string tbname, string fields, string values, SqlCeParameter[] sp)
        {
            string sql = "INSERT INTO " + tbname + " (" + fields + ") VALUES(" + values + ");";
            SqlCeConnection conn = null;SqlCeCommand cmd = null;
            try
            {
                conn = GetConn(constr);
                cmd = GetCommand(conn, sql,sp);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex) { throw new Exception(ex.Message + ":" + sql); }//+":"+GetSPEx(sp)
            finally {if (conn != null) { conn.Dispose(); } if (cmd != null) { cmd.Dispose();  }}
        }
        /// <summary>
        /// 执行SQL,有行变更则表示执行成功
        /// </summary>
        public static bool ExecuteSql(string constr, string sql)
        {
            SqlCeConnection conn = null; SqlCeCommand cmd = null;
            try
            {
                conn = GetConn(constr);
                cmd = GetCommand(conn, sql);
                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex) { throw new Exception(ex.Message + ":" + sql); }
            finally { if (conn != null) { conn.Dispose(); } if (cmd != null) { cmd.Dispose(); } }
        }
        public static DataTable SelPage(string constr, int pageSize, int curPage, out int itemCount, string pk, string fields, string tbname, string where = "", string order = "", SqlCeParameter[] sp = null)
        {
            if (!string.IsNullOrEmpty(order))
            {
                order = " ORDER BY " + order;
            }
            if (!string.IsNullOrEmpty(where))
            {
                where = " WHERE " + where;
            }
            fields = " " + fields;
            pageSize = pageSize > 0 ? pageSize : 10;
            curPage = curPage < 1 ? 1 : curPage; curPage--;
            int curSize = (pageSize * curPage);
            string countsql = "Select COUNT(" + pk + ") From " + tbname + where;
            string sql = "SELECT " + fields + " FROM " + tbname + where + order + " OFFSET " + curSize + " ROWS FETCH NEXT " + pageSize + " ROWS ONLY";
            itemCount = DataConvert.CLng(ExecuteTable(constr, countsql).Rows[0][0]);
            return ExecuteTable(constr, sql);
        }
        //----------Tools
        /// <summary>
        /// 返回数据库连接字符串
        /// </summary>
        /// <param name="dbfile">物理路径</param>
        /// <param name="pwd">密码,CE不支持用户名</param>
        public static string GetConStr(string dbfile, string pwd = "")
        {
            string constr = "Data Source=" + dbfile + ";Persist Security Info=False;";
            if (!string.IsNullOrEmpty(pwd)) { constr += "Password=" + pwd + ";"; }
            return constr;
        }
        private static SqlCeConnection GetConn(string constr)
        {
            SqlCeConnection conn = new SqlCeConnection(constr);
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            return conn;
        }
        private static SqlCeCommand GetCommand(SqlCeConnection conn, string sql, SqlCeParameter[] sp = null)
        {
            SqlCeCommand cmd = new SqlCeCommand(sql, conn);
            if (sp != null)
            {
                foreach (SqlCeParameter param in sp)
                {
                    if (param == null||param.Value==null) {
                        param.Value = DBNull.Value;
                    }
                    cmd.Parameters.Add(param); 
                }
            }
            return cmd;
        }
        private static string GetSPEx(SqlCeParameter[] sp)
        {
            string result = "";
            if (sp == null) return result;
            foreach (SqlCeParameter param in sp)
            {
                result += param.ParameterName + ":" + param.Value + ",";
            }
            return result;
        }
    }
}
