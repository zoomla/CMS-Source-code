using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Configuration;
using System.Collections;
using ZoomLa.SQLDAL;
using System.Data.SqlClient;

namespace ZoomLa.BLL
{
    public class B_Data
    {
        //数据库连接字符串
        //private static readonly ID_Data idd = IDal.CreateData();

        /// <summary>
        /// 查询SQL Server里的数据库名
        /// </summary>
        /// <returns></returns>
        public DataTable GetDataList()
        {
            string strSql = "select * from sysdatabases order by name";
            return SqlHelper.GetDataList("", strSql);
        }
        /// <summary>
        /// 查询数据库中的所有表名，视图名
        /// </summary>
        /// <param name="data">数据库名</param>
        /// <returns></returns>
        public DataTable GetTableList(string data)
        {
            string conn = GetConn(data);
            string strSql = "SELECT * FROM sysobjects WHERE type='U' OR type='V' order by name";
            return SqlHelper.GetDataList(conn, strSql);
        }
        /// <summary>
        /// 查询表里的所有字段属性
        /// </summary>
        /// <param name="table">表名</param>
        /// <param name="data">数据库名</param>
        /// <returns></returns>
        public DataTable GetColumn(string table, string data)
        {
            string conn = GetConn(data);
            return SqlHelper.GetTableColumn(table, conn);
        }
        /// <summary>
        /// 查询表里的记录
        /// </summary>
        /// <param name="strSql">SQL 语句</param>
        /// <param name="data">数据库名</param>
        /// <returns></returns>
        public DataTable GetTable(string strSql, string data)
        {
            string conn = GetConn(data);
          try
            {
                return SqlHelper.GetTable(strSql, conn);
            }
            catch (SqlException)
            {
                return null;
            }
        }
        /// <summary>
        /// 查询表里的记录
        /// </summary>
        /// <param name="strSql">SQL 语句</param>
        /// <param name="data">数据库名</param>
        /// <param name="conn">数据库链接语句</param>
        /// <returns></returns>
        public DataTable GetTable(string strSql, string data,string conn)
        {
            try
            {
                return SqlHelper.GetTable(strSql, conn);
            }
            catch (SqlException)
            {
                return null;
            }
        }

    
        public string GetConn(string data)
        {
            string[] str = SqlHelper.ConnectionString.Split(new char[] { ';' });
            String strServerName = str[0].Replace("Data Source=", " ").TrimStart();

            String strUserID = str[2].Replace("User ID=", " ").TrimStart();

            String strPSW = str[3].Replace("Password=", " ").TrimStart();
            
            
            string connstr = "";
            if (string.IsNullOrEmpty(data))
            {
                connstr = String.Format("Data Source={0};User ID={1};PWD={2};", strServerName, strUserID, strPSW);
            }
            else
            {
                connstr = String.Format("Data Source={0};User ID={1};PWD={2};Initial Catalog={3}", strServerName, strUserID, strPSW, data);
            }
            return connstr;
        }

        /// <summary>
        /// 检测查询语句是否成功
        /// </summary>
        /// <param name="strSql">SQL 语句</param>
        /// <param name="data">数据库名</param>
        /// <param name="conn">数据库链接语句</param>
        /// <param name="type">做什么类型的语句检测</param>
        /// <returns>成功返回OK，否则返回异常内容</returns>
        public string CheckSQL(string strSql, string data, int type)
        {
            //return idd.CheckSQL(strSql, data, GetConn(data), type);
                switch (type)
                {
                    case 1:
                        break;
                    case 2:
                        break;
                    case 4:
                        GetTable(strSql, data, GetConn(data));
                        break;
                }
                return "OK";
        }

        /// <summary>
        /// 检测表在数据库中是否存在
        /// </summary>
        /// <param name="tab"></param>
        /// <returns></returns>
        public bool CheckTable(string tab)
        {
            string sql = "select *from sysobjects where type='U' and name='" + tab + "'";
            DataSet myhisDs = SqlHelper.ExecuteDataSet(CommandType.Text, sql, null);
            if (myhisDs.Tables[0].Rows.Count == 0) return false; else return true;
        }

        /// <summary>
        /// 批量替换数据
        /// </summary>
        /// <param name="TargetField"></param>
        /// <param name="ShiftField"></param>
        /// <param name="Database"></param>
        /// <param name="DbTable"></param>
        /// <param name="DbFieldlist"></param>
        /// <returns></returns>
        public bool UpdateData(string TargetField, string ShiftField,  string DbTable, ArrayList DbFieldlist)
        {
            bool boo = false;
            string sql = "exec sp_help " + DbTable;
            DataSet ds = SqlHelper.ExecuteDataSet(CommandType.Text, sql);
            DataTable dt = ds.Tables[1];
            foreach (DataRow row in dt.Rows)
            {
                for (int i = 0; i < DbFieldlist.Count; i++)
                {
                    if (row[0].ToString() == DbFieldlist[i].ToString())
                    {
                        if (row[1].ToString() != "nvarchar" && row["Type"].ToString() != "varchar" && row["Type"].ToString() != "ntext" && row["Type"].ToString() != "text")
                        {
                            DbFieldlist.Remove(DbFieldlist[i]);
                        }
                    }
                }
            }
            for (int i = 0; i < DbFieldlist.Count; i++)
            {
                string sqlStr = "UPDATE  " + DbTable + " SET " + DbFieldlist[i].ToString() + "='" + ShiftField + "' where " + DbFieldlist[i].ToString() + "='" + TargetField + "'";
                try
                {
                    boo = SqlHelper.ExecuteSql(sqlStr);
                }
                catch
                {
                    throw;
                }
            }
            return boo;
        }
    }
}
