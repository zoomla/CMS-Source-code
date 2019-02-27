namespace ZoomLa.SQLDAL
{
    using System;
    using System.Collections;
    using System.Data;
    using System.Data.SqlClient;
    using System.Configuration;

    /// <summary>
    /// Sql Server操作类
    /// </summary>
    public class SqlHelper
    {
        //数据库连接字符串
        private static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["Connection String"].ConnectionString;
        //参数哈希表
        private static Hashtable parmCache = Hashtable.Synchronized(new Hashtable());

        public SqlHelper()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        public static void CacheParameters(string cacheKey, SqlParameter[] commandParameters)
        {
            parmCache[cacheKey] = commandParameters;
        }
        /// <summary>
        /// 执行查询并将查询结果填充到DataSet
        /// </summary>
        /// <param name="cmdType">命令类型 分sql查询语句或存储过程</param>
        /// <param name="cmdText">命令字符串</param>
        /// <param name="commandParameters">参数</param>
        /// <returns>查询结果DataSet</returns>
        public static DataSet ExecuteDataSet(CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            return ExecuteDataSet(ConnectionString, cmdType, cmdText, commandParameters);
        }
        /// <summary>
        /// 执行查询并将查询结果填充到DataSet
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="cmdType">命令类型</param>
        /// <param name="cmdText">命令语句</param>
        /// <param name="commandParameters">参数</param>
        /// <returns>查询结果</returns>
        public static DataSet ExecuteDataSet(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                PrepareCommand(cmd, connection, cmdType, cmdText, commandParameters);
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);
                cmd.Parameters.Clear();
                adapter.Dispose();
                return dataSet;
            }
        }
        /// <summary>
        /// 执行SQL操作
        /// </summary>
        /// <param name="cmdType">命令类型</param>
        /// <param name="cmdText">命令语句</param>
        /// <param name="commandParameters">参数</param>
        /// <returns></returns>
        public static int ExecuteNonQuery(CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            return ExecuteNonQuery(ConnectionString, cmdType, cmdText, commandParameters);
        }

        public static int ExecuteNonQuery(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandTimeout = 60;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                PrepareCommand(cmd, connection, cmdType, cmdText, commandParameters);
                int num = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return num;
            }
        }
        /// <summary>
        /// 返回命令执行成功状态 不带参数
        /// </summary>
        /// <param name="strSql">执行语句</param>
        /// <returns>执行状态 成功true 否则false</returns>
        public static bool ExecuteSql(string strSql)
        {
            return (ExecuteNonQuery(CommandType.Text,strSql, null) > 0);
        }
        
        /// <summary>
        /// 返回命令执行成功状态 带参数
        /// </summary>
        /// <param name="strSql">执行语句</param>
        /// <param name="cmdParams">参数</param>
        /// <returns>执行状态 成功true 否则false</returns>
        public static bool ExecuteProc(string strSql, params SqlParameter[] cmdParams)
        {
            return (ExecuteNonQuery(CommandType.StoredProcedure,strSql, cmdParams) > 0);
        }
        /// <summary>
        /// 返回命令执行成功状态 不带参数
        /// </summary>
        /// <param name="strSql">执行语句</param>
        /// <returns>执行状态 成功true 否则false</returns>
        public static bool ExecuteProc(string strSql)
        {
            return (ExecuteNonQuery(CommandType.StoredProcedure, strSql, null) > 0);
        }
        /// <summary>
        /// 返回命令执行成功状态 带参数
        /// </summary>
        /// <param name="strSql">执行语句</param>
        /// <param name="cmdParams">参数</param>
        /// <returns>执行状态 成功true 否则false</returns>
        public static bool ExecuteSql(string strSql, params SqlParameter[] cmdParams)
        {
            return (ExecuteNonQuery(CommandType.Text, strSql, cmdParams) > 0);
        }

        public static SqlDataReader ExecuteReader(CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            return ExecuteReader(ConnectionString, cmdType, cmdText, commandParameters);
        }

        public static SqlDataReader ExecuteReader(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlDataReader reader2;
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                PrepareCommand(cmd, conn, cmdType, cmdText, commandParameters);
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                reader2 = reader;
            }
            catch
            {                
                throw;
            }            
            return reader2;
        }

        public static object ExecuteScalar(CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            return ExecuteScalar(ConnectionString, cmdType, cmdText, commandParameters);
        }

        public static object ExecuteScalar(SqlConnection connection, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, connection, cmdType, cmdText, commandParameters);
            object obj2 = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            return obj2;
        }

        public static object ExecuteScalar(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                PrepareCommand(cmd, connection, cmdType, cmdText, commandParameters);
                object obj2 = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return obj2;
            }
        }

        public static DataTable ExecuteTable(CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            return ExecuteTable(ConnectionString, cmdType, cmdText, commandParameters);
        }

        public static DataTable ExecuteTable(SqlConnection connection, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, connection, cmdType, cmdText, commandParameters);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet, "Result");
            cmd.Parameters.Clear();
            return dataSet.Tables["Result"];
        }

        public static DataTable ExecuteTable(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                PrepareCommand(cmd, connection, cmdType, cmdText, commandParameters);
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet, "Result");
                cmd.Parameters.Clear();
                if (dataSet.Tables.Count > 0)
                {
                    return dataSet.Tables["Result"];
                }
                return null;
            }
        }        

        public static SqlParameter[] GetCachedParameters(string cacheKey)
        {
            SqlParameter[] parameterArray = (SqlParameter[])parmCache[cacheKey];
            if (parameterArray == null)
            {
                return null;
            }
            SqlParameter[] parameterArray2 = new SqlParameter[parameterArray.Length];
            int index = 0;
            int length = parameterArray.Length;
            while (index < length)
            {
                parameterArray2[index] = (SqlParameter)((ICloneable)parameterArray[index]).Clone();
                index++;
            }
            return parameterArray2;
        }
        /// <summary>
        /// 查询记录是否存在
        /// </summary>
        /// <param name="strCommand">查询命令</param>
        /// <param name="cmdParams">查询参数</param>
        /// <returns>存在返回true 否则false</returns>
        public static bool Exists(CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {

            return (ObjectToInt32(ExecuteScalar(cmdType,cmdText,commandParameters)) > 0);
        }

        public static bool ExistsSql(string strSql)
        {
            return Exists(CommandType.Text, strSql,null);
        }

        public static bool ExistsSql(string strSql, params SqlParameter[] cmdParams)
        {
            return Exists(CommandType.Text,strSql, cmdParams);
        }
        /// <summary>
        /// 获取表里某字段的最大值
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="fieldName">字段名</param>
        /// <returns>该字段在表里的最大值</returns>
        public static int GetMaxId(string tableName, string fieldName)
        {
            string query = "select max(" + fieldName + ") from " + tableName;
            return ObjectToInt32(ExecuteScalar(CommandType.Text,query,null));
        }
        /// <summary>
        /// 将数据对象转换成整形
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int ObjectToInt32(object obj)
        {
            int result = 0;
            if (!(object.Equals(obj, null) || object.Equals(obj, DBNull.Value)))
            {
                int.TryParse(obj.ToString(), out result);
            }
            return result;
        }
        /// <summary>
        /// 读取sql server数据库结构 输出表名
        /// </summary>
        /// <param name="sType">结构类型 Table表 Views视图</param>
        /// <returns></returns>
        public static DataTable GetSchemaTable()
        {
            DataTable dt;
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                try
                {
                    dt = conn.GetSchema("TABLES");
                }
                catch
                {
                    dt = null;
                }
                finally
                {
                    conn.Close();
                }
            }
            return dt;
        }
        public static DataTable GetTableColumn(string TableName)
        {            
            string strSql = "select c.name as fieldname,t.name fieldtype,m.text as fielddef from";
            strSql = strSql + " syscolumns c inner join systypes t on c.xusertype=t.xusertype ";
            strSql = strSql + "left join syscomments m on c.cdefault=m.id where ";
            strSql = strSql + "objectproperty(c.id,'IsUserTable')=1 and (object_name(c.id)='"+TableName+"') order by c.colorder";
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = strSql;
                cmd.CommandTimeout = 60;
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet, "Result");
                adapter.Dispose();
                //cmd.Parameters.Clear();
                if (dataSet.Tables.Count > 0)
                {
                    return dataSet.Tables["Result"];
                }
                return null;
            }
        }
        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, CommandType cmdType, string cmdText, SqlParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            cmd.CommandTimeout = 60;            
            cmd.CommandType = cmdType;
            if (cmdParms != null)
            {
                foreach (SqlParameter parameter in cmdParms)
                {
                    cmd.Parameters.Add(parameter);
                }
            }
        }
    }
}