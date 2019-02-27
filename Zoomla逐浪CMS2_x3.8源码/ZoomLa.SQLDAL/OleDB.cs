using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Text;

namespace ZoomLa.SQLDAL
{
    /*
     * 
     * Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + Path + ";" + "Extended Properties=Excel 8.0;"; 
     * 旧的连接无法访问用Office2013建的文件，所以这里不建
     * 加密文件无法打开，即使你配了密码(Could not decrypt file)
     */
    public class OleDB
    {
        /// <summary>;
        /// 根据路径，创健一个OLEDB连接
        /// </summary>
        public static OleDbConnection CreateConn(string path)
        {
            string strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";" + "Extended Properties=Excel 8.0;";
            OleDbConnection conn = new OleDbConnection(strConn);
            try
            {
                conn.Open();
                return conn;
            }
            catch { conn.Close(); return null; }
        }

        /// <summary>
        /// 执行OLEDB查询，主用于Excel
        /// </summary>
        public static DataTable Select(string Path, string sql)
        {
            //OleDB.Select(path, "select * from" + OleDB.SelectTables(path).Rows[0]["Table_Name"]);
            string strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Path + ";" + "Extended Properties=Excel 8.0;";
            string Sql = sql;
            OleDbConnection conn = new OleDbConnection(strConn); 
            OleDbDataAdapter mycommand = new OleDbDataAdapter();
            try
            {
                DataTable dt = new DataTable();
                conn.Open();
                mycommand = new OleDbDataAdapter(Sql, conn); 
                mycommand.Fill(dt);
                return dt;
            }
            catch { return new DataTable(); }
            finally
            {
                mycommand.Dispose();
                conn.Close();
            }
        }
        /// <summary>
        /// 获取Excel中的表数目，"Table_Name"访问
        /// </summary>
        /// <returns></returns>
        public static DataTable SelectTables(string path)
        {
            OleDbConnection conn = CreateConn(path);
            DataTable dt = new DataTable();
            try
            {
                dt= conn.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, null);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["Table_Name"] = "[" + dt.Rows[i]["Table_Name"] + "]";
                }
                    return dt;
            }
            catch  { ; return null; }
            finally { conn.Close(); }
        }
        /// <summary>
        /// 根据表名，获取表中所有列名,"Column_Name"访问
        /// </summary>
        public static DataTable SelectColumns(string path, string tableName)
        {
            OleDbConnection conn = CreateConn(path);

            tableName = tableName.Replace("[", "").Replace("]", "");//查询语句支持，但这里获取表不支持。
            try
            {
                DataTable dt = conn.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Columns, new object[] { null, null, tableName, null });
                return dt;
            }
            catch { return null; }
            finally { conn.Close(); }
        }
        public static string GetFirstTableName(string path)
        {
            return (OleDB.SelectTables(path).Rows[0]["Table_Name"] as string);
        }
        public static void ExecuteSql(string path,string sql) 
        {
            string strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";" + "Extended Properties=Excel 8.0;";
            OleDbConnection conn = new OleDbConnection(strConn); conn.Open();
            OleDbCommand cmd = new OleDbCommand(sql,conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            cmd.Dispose();
        }
    }
}
