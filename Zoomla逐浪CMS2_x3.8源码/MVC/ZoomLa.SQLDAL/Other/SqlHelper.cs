using System;
using System.Data;
using System.Xml;
using System.Data.SqlClient;
using System.Collections;
using System.Configuration;
using System.Web;
using System.Collections.Generic;
using System.Diagnostics;
using ZoomLa.SQLDAL.SQL;
using ZoomLa.Safe;

namespace ZoomLa.SQLDAL
{
    public sealed class SqlHelper
    {
        //数据库连接字符串
        public static readonly string ConnectionString = SafeC.Cert_Decry(ConfigurationManager.ConnectionStrings["Connection String"].ConnectionString);
        public static readonly string PlugConnectionString = "";
        #region 私有构造函数和方法
        private SqlHelper() {}

        /// <summary>
        /// 将SqlParameter参数数组(参数值)分配给SqlCommand命令.
        /// 这个方法将给任何一个参数分配DBNull.Value;
        /// 该操作将阻止默认值的使用.
        /// </summary>
        /// <param name="command">命令名</param>
        /// <param name="commandParameters">SqlParameters数组</param>
        private static void AttachParameters(SqlCommand command, SqlParameter[] commandParameters)
        {
            if (command == null) throw new ArgumentNullException("command");
            if (commandParameters != null)
            {
                foreach (SqlParameter p in commandParameters)
                {
                    if (p != null)
                    {
                        // 检查未分配值的输出参数,将其分配以DBNull.Value.
                        if ((p.Direction == ParameterDirection.InputOutput ||
                            p.Direction == ParameterDirection.Input) &&
                            (p.Value == null))
                        {
                            p.Value = DBNull.Value;
                        }
                        command.Parameters.Add(p);
                    }
                }
            }
        }

        /// <summary>
        /// 将DataRow类型的列值分配到SqlParameter参数数组.
        /// </summary>
        /// <param name="commandParameters">要分配值的SqlParameter参数数组</param>
        /// <param name="dataRow">将要分配给存储过程参数的DataRow</param>
        private static void AssignParameterValues(SqlParameter[] commandParameters, DataRow dataRow)
        {
            if ((commandParameters == null) || (dataRow == null))
            {
                // Do nothing if we get no data
                return;
            }

            int i = 0;
            // 设置参数值
            foreach (SqlParameter commandParameter in commandParameters)
            {
                // 创建参数名称,如果不存在,只抛出一个异常.
                if (commandParameter.ParameterName == null ||
                    commandParameter.ParameterName.Length <= 1)
                    throw new Exception(
                        string.Format(
                            "请提供参数{0}一个有效的名称{1}.",
                            i, commandParameter.ParameterName));
                // 从dataRow的表中获取为参数数组中数组名称的列的索引.
                // 如果存在和参数名称相同的列,则将列值赋给当前名称的参数.
                if (dataRow.Table.Columns.IndexOf(commandParameter.ParameterName.Substring(1)) != -1)
                    commandParameter.Value = dataRow[commandParameter.ParameterName.Substring(1)];
                i++;
            }
        }

        /// <summary>
        /// 将一个对象数组分配给SqlParameter参数数组.
        /// </summary>
        /// <param name="commandParameters">要分配值的SqlParameter参数数组</param>
        /// <param name="parameterValues">将要分配给存储过程参数的对象数组</param>
        private static void AssignParameterValues(SqlParameter[] commandParameters, object[] parameterValues)
        {
            if ((commandParameters == null) || (parameterValues == null))
            {
                // 如果得不到任何数据,什么也不做
                return;
            }

            // 确保对象数组个数与参数个数匹配,如果不匹配,抛出一个异常.
            if (commandParameters.Length != parameterValues.Length)
            {
                throw new ArgumentException("参数列数与参数值列数不匹配。");
            }

            // 给参数赋值
            for (int i = 0, j = commandParameters.Length; i < j; i++)
            {
                // If the current array value derives from IDbDataParameter, then assign its Value property
                if (parameterValues[i] is IDbDataParameter)
                {
                    IDbDataParameter paramInstance = (IDbDataParameter)parameterValues[i];
                    if (paramInstance.Value == null)
                    {
                        commandParameters[i].Value = DBNull.Value;
                    }
                    else
                    {
                        commandParameters[i].Value = paramInstance.Value;
                    }
                }
                else if (parameterValues[i] == null)
                {
                    commandParameters[i].Value = DBNull.Value;
                }
                else
                {
                    commandParameters[i].Value = parameterValues[i];
                }
            }
        }

        /// <summary>
        /// 预处理用户提供的命令,数据库连接/事务/命令类型/参数
        /// </summary>
        /// <param name="command">要处理的SqlCommand</param>
        /// <param name="connection">数据库连接</param>
        /// <param name="transaction">一个有效的事务或者是null值</param>
        /// <param name="commandType">命令类型 (存储过程,命令文本, 其它.)</param>
        /// <param name="commandText">存储过程名或都T-SQL命令文本</param>
        /// <param name="commandParameters">和命令相关联的SqlParameter参数数组,如果没有参数为'null'</param>
        /// <param name="mustCloseConnection"><c>true</c> 如果连接是打开的,则为true,其它情况下为false.</param>
        private static void PrepareCommand(SqlCommand command, SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText, SqlParameter[] commandParameters, out bool mustCloseConnection)
        {
            if (command == null) throw new ArgumentNullException("command");
            if (commandText == null || commandText.Length == 0) throw new ArgumentNullException("commandText");
            
            // If the provided connection is not open, we will open it
            if (connection.State != ConnectionState.Open)
            {
                mustCloseConnection = true;
                connection.Open();
            }
            else
            {
                mustCloseConnection = false;
            }

            // 给命令分配一个数据库连接.
            command.Connection = connection;

            // 设置命令文本(存储过程名或SQL语句)
            command.CommandText = commandText;

            // 分配事务
            if (transaction != null)
            {
               transaction=connection.BeginTransaction();
                if (transaction.Connection == null) throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
                command.Transaction = transaction;

            }

            // 设置命令类型.
            command.CommandType = commandType;

            // 分配命令参数
            if (commandParameters != null)
            {
                AttachParameters(command, commandParameters);
            }
            return;
        }
        /// <summary>
        /// 预处理用户提供的命令,数据库连接/事务/命令类型/参数
        /// </summary>
        /// <param name="command">要处理的SqlCommand</param>
        /// <param name="connection">数据库连接</param>
        /// <param name="commandType">命令类型 (存储过程,命令文本, 其它.)</param>
        /// <param name="commandText">存储过程名或都T-SQL命令文本</param>
        /// <param name="commandParameters">和命令相关联的SqlParameter参数数组,如果没有参数为'null'</param>
        /// <param name="mustCloseConnection"><c>true</c> 如果连接是打开的,则为true,其它情况下为false.</param>
        private static void PrepareCommand(SqlCommand command, SqlConnection connection, CommandType commandType, string commandText, SqlParameter[] commandParameters)
        {
            if (command == null) throw new ArgumentNullException("command");
            if (commandText == null || commandText.Length == 0) throw new ArgumentNullException("commandText");

            // If the provided connection is not open, we will open it
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            // 给命令分配一个数据库连接.
            command.Connection = connection;

            // 设置命令文本(存储过程名或SQL语句)
            command.CommandText = commandText;

            // 设置命令类型.
            command.CommandType = commandType;

            // 分配命令参数
            if (commandParameters != null)
            {
                AttachParameters(command, commandParameters);
            }
            return;
        }

        #endregion 私有构造函数和方法结束
        #region ZoomLa专用方法
        /// <summary>
        /// 返回命令执行成功状态 带参数
        /// </summary>
        /// <param name="strSql">执行语句</param>
        /// <param name="cmdParams">参数</param>
        /// <returns>执行状态 成功true 否则false</returns>
        public static bool ExecuteProc(string strSql, SqlParameter[] cmdParams = null)
        {

            return (ExecuteNonQuery(CommandType.StoredProcedure, strSql, cmdParams) > 0);
        }
        /// <summary>
        /// 返回命令执行成功状态 带参数
        /// </summary>
        /// <param name="strSql">执行语句</param>
        /// <param name="cmdParams">参数</param>
        /// <returns>执行状态 成功true 否则false</returns>
        public static bool ExecuteSql(string strSql, SqlParameter[] cmdParams = null)
        {
            return (ExecuteNonQuery(CommandType.Text, strSql, cmdParams) > 0);
        }
        public static DataTable ExecuteTable(string sql, SqlParameter[] sp = null)
        {
            return ExecuteTable(CommandType.Text, sql, sp);
        }
        public static DataTable ExecuteTable(CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            return ExecuteTable(ConnectionString, cmdType, cmdText, commandParameters);
        }
        /// <summary>
        /// 查询记录是否存在
        /// </summary>
        /// <param name="strCommand">查询命令</param>
        /// <param name="cmdParams">查询参数</param>
        /// <returns>存在返回true 否则false</returns>
        public static bool Exists(CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            return (DataConvert.CLng(ExecuteScalar(cmdType, cmdText, commandParameters)) > 0);
        }
        public static bool ExistsSql(string strSql, SqlParameter[] cmdParams = null)
        {
            return Exists(CommandType.Text, strSql, cmdParams);
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
            return DataConvert.CLng(ExecuteScalar(CommandType.Text, query, null));
        }
        /// <summary>
        /// 将数据对象转换成整形
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int ObjectToInt32(object obj)
        {
            return DataConvert.CLng(obj);
        }
        public static DataTable GetSchemaTable(string ConnectionString)
        {
           return GetSchemaTable(ConnectionString,"dbo");
        }
        /// <summary>
        /// 读取sql server数据库结构 输出表名
        /// </summary>
        /// <param name="sType">结构类型 Table表 Views视图</param>
        /// <param name="sType">架构名称,默认为dbo</param>
        /// <returns></returns>
        public static DataTable GetSchemaTable(string ConnectionString,string schema)
        {
            if (string.IsNullOrEmpty(schema)) schema = "dbo";
            schema = schema.Trim();
            DataTable dt;
            DataTable newtable = new DataTable();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                try
                {
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }
                    dt = conn.GetSchema("TABLES");
                    dt.DefaultView.Sort = "TABLE_NAME asc";
                    newtable.Columns.Add("TABLE_NAME");
                    newtable.Columns.Add("TABLENAME");
                    string[] connect1 = ConnectionString.Split(new string[] { "Initial Catalog=" }, StringSplitOptions.RemoveEmptyEntries);
                    string[] connect = connect1[1].Split(new string[] { ";User ID=" }, StringSplitOptions.RemoveEmptyEntries);
                    string dataname = connect[0];

                    foreach (DataRow row in dt.Rows)
                    {
                        newtable.Rows.Add(dataname + "."+schema+"." + row["TABLE_NAME"], row["TABLE_NAME"]);
                    }
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
            //TABLE_NAME
            return newtable;
        }
        /// <summary>
        /// 判断连接是否有效，外部库
        /// </summary>
        public static bool ConnIsOK(string ConnectionString)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                try
                {
                        conn.Open();
                }
                catch
                {
                    return false;
                }
                finally
                {
                    conn.Close();
                }
            }
            return true;
        }
        public static DataTable GetTable(string strSql, string ConnStr)
        {
            return ExecuteTable(ConnStr, CommandType.Text, strSql, null);
        }
        /// <summary>
        /// 读取sql server数据库结构 输出表名
        /// </summary>
        /// <param name="sType">结构类型 Table表 Views视图</param>
        /// <returns></returns>
        public static DataTable GetSchemaTable2(string ConnectionString)
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
            dt.DefaultView.Sort = "TABLE_NAME asc";
            //TABLE_NAME
            DataTable newtable = new DataTable();
            newtable.Columns.Add("TABLE_NAME");
            newtable.Columns.Add("TABLENAME");
            string[] connect1 = ConnectionString.Split(new string[] { "Initial Catalog=" }, StringSplitOptions.RemoveEmptyEntries);
            string[] connect = connect1[1].Split(new string[] { ";User ID=" }, StringSplitOptions.RemoveEmptyEntries);
            string dataname = connect[0];
            foreach (DataRow row in dt.Rows)
            {
                newtable.Rows.Add(row["TABLE_NAME"], row["TABLE_NAME"]);
            }
            return newtable;
        }
        public static string GetTablecolumn(string tablename, string columnname)
        {

            string[] connect1 = ConnectionString.Split(new string[] { "Initial Catalog=" }, StringSplitOptions.RemoveEmptyEntries);
            string[] connect = connect1[1].Split(new string[] { ";User ID=" }, StringSplitOptions.RemoveEmptyEntries);
            string dataname = connect[0];

            string truetablename = "";
            string truedataname = "";
            string connstr = "";

            if (tablename.IndexOf(".") > -1)
            {
                string[] columnlist = tablename.Split(new string[] { "." }, StringSplitOptions.None);
                if (columnlist.Length == 3)
                {
                    truetablename = columnlist[columnlist.Length - 1];
                    truedataname = columnlist[0];
                }
                if (truedataname == dataname)
                {
                    connstr = ConnectionString;
                }
            }
            else
            {
                connstr = ConnectionString;
            }

            string Columtxt = "";
            string strSql = "SELECT value FROM ::fn_listextendedproperty ('MS_Description','user','dbo','table','" + truetablename + "','column','" + columnname + "')";
            using (SqlConnection conn = new SqlConnection(connstr))
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
                Columtxt = (string)cmd.ExecuteScalar();
            }
            return Columtxt;
        }
        /// <summary>
        /// 查询数据库表字段的信息，字段名，字段数据类型，不包含说明，但所需权限小，用于标签--外部数据库
        /// </summary>
        public static DataTable GetTableColumn2(string TableName, object dbConnectionString)
        {

            DataTable viewDT = new DataTable();
            viewDT.Columns.Add("FieldName", typeof(string));
            viewDT.Columns.Add("FieldType", typeof(string));
            viewDT.Columns.Add("FieldDef", typeof(string));
            viewDT.Columns.Add("IsNotNull", typeof(string));
            viewDT.Columns.Add("FieldAlias", typeof(string));//说明
            if (string.IsNullOrEmpty(TableName)) return viewDT;
            string strSql2 = "Select Top 0 * From " + TableName + "";
            DataTable dt = ExecuteTable(dbConnectionString.ToString(), CommandType.Text, strSql2);
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                DataRow dr = viewDT.NewRow();
                dr[0] = dt.Columns[i].ColumnName;
                dr[1] = dt.Columns[i].DataType.FullName.Replace("System.", "");
                dr[2] = dt.Columns[i].DefaultValue;
                dr[3] = dt.Columns[i].AllowDBNull.ToString();
                dr[4] = "";
                viewDT.Rows.Add(dr);
            }
            return viewDT;
        }
        /// <summary>
        /// 获取数据列的信息，用于用户模块等,信息较丰富,包含说明等
        /// </summary>
        public static DataTable GetTableColumn(string TableName, object dbConnectionString)
        {
            //sys.extended_properties
            string strSql = "";
            if (TableName.IndexOf(".") > -1)
            {
                string[] temps = TableName.Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);
                TableName = temps[temps.Length - 1];
            }

            using (SqlConnection conn = new SqlConnection(dbConnectionString.ToString()))
            {

                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                if (conn.ServerVersion.IndexOf("08.00") > -1)//SQL2000数据库使用
                {
                    strSql = "select c.name as fieldname,t.name fieldtype,m.text as fielddef,p.[value] AS FieldAlias,c.isnullable as IsNotNull from";
                    strSql = strSql + " syscolumns c inner join systypes t on c.xusertype=t.xusertype ";
                    strSql = strSql + "left join syscomments m on c.cdefault=m.id left join sysproperties p on c.id=p.id AND p.smallid = c.colid where ";
                    strSql = strSql + "objectproperty(c.id,'IsUserTable')=1 and (object_name(c.id)='" + TableName + "') order by c.colorder";
                }
                else
                {

                    strSql = "select c.name as fieldname,t.name fieldtype,m.text as fielddef,p.[value] AS FieldAlias,c.isnullable as IsNotNull from";
                    strSql = strSql + " syscolumns c inner join systypes t on c.xusertype=t.xusertype ";
                    strSql = strSql + "left join syscomments m on c.cdefault=m.id left join sys.extended_properties p on c.id=p.major_id AND p.minor_id = c.colid where ";
                    strSql = strSql + "objectproperty(c.id,'IsUserTable')=1 and (object_name(c.id)='" + TableName + "') order by c.colorder";
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
                    if (dataSet.Tables["Result"].Rows.Count > 0)
                    {
                        return dataSet.Tables["Result"];
                    }
                    else
                    {

                        string strSql2 = "select * from ZL_View where ViewName='" + TableName + "'";

                        SqlCommand cmd2 = new SqlCommand();
                        cmd2.Connection = conn;
                        cmd2.CommandText = strSql2;
                        cmd2.CommandTimeout = 60;
                        cmd2.CommandType = CommandType.Text;
                        SqlDataAdapter adapter2 = new SqlDataAdapter();
                        adapter2.SelectCommand = cmd2;
                        DataSet dataSet2 = new DataSet();
                        adapter2.Fill(dataSet2, "Result");
                        adapter2.Dispose();

                        if (dataSet2.Tables.Count > 0)
                        {
                            return dataSet2.Tables["Result"];
                        }
                    }
                }
                return null;
            }
        }
        /// <summary>
        /// 查询所有数据库
        /// </summary>
        /// <returns>数据库列表</returns>
        public static DataTable GetDataList(string connStr, string strSql)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
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
        #endregion
        #region ExecuteDataSet方法
        public static DataSet ExecuteDataSet(CommandType cmdType,string commandText, SqlParameter[] commandParameters = null)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                return ExecuteDataSet(connection, cmdType, commandText, commandParameters);
            }
        }
        public static DataSet ExecuteDataSet(string commandText, SqlParameter[] commandParameters = null)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                return ExecuteDataSet(connection, CommandType.Text, commandText, commandParameters);
            }
        }
        /// <summary>
        /// 执行指定数据库连接对象的命令,指定存储过程参数,返回DataSet.
        /// </summary>
        /// <remarks>
        /// 示例:  
        ///  DataSet ds = ExecuteDataSet(conn, CommandType.StoredProcedure, "GetOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connection">一个有效的数据库连接对象</param>
        /// <param name="commandType">命令类型 (存储过程,命令文本或其它)</param>
        /// <param name="commandText">存储过程名或T-SQL语句</param>
        /// <param name="commandParameters">SqlParamter参数数组</param>
        /// <returns>返回一个包含结果集的DataSet</returns>
        public static DataSet ExecuteDataSet(SqlConnection connection, CommandType commandType, string commandText, SqlParameter[] commandParameters)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            if (connection.State != ConnectionState.Open) { connection.Open(); }
            // 预处理
            SqlCommand cmd = new SqlCommand();
            bool mustCloseConnection = false;
            PrepareCommand(cmd, connection, (SqlTransaction)null, commandType, commandText, commandParameters, out mustCloseConnection);
            // 创建SqlDataAdapter和DataSet.
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                DataSet ds = new DataSet();
                da.Fill(ds);
                cmd.Parameters.Clear();

                if (mustCloseConnection)
                    connection.Close();

                // Return the dataset
                return ds;
            }
        }
        #endregion ExecuteDataSet数据集命令结束
        public static SqlDataReader ExecuteReader(CommandType commandType, string commandText, SqlParameter[] commandParameters = null)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            return ExecuteReader(connection, commandType, commandText, commandParameters);
        }
        /// <summary>
        /// [调用者方式]执行指定数据库连接对象的数据阅读器,指定参数.
        /// </summary>
        public static SqlDataReader ExecuteReader(SqlConnection connection, CommandType commandType, string commandText, SqlParameter[] commandParameters)
        {
            return ExecuteReader(connection, (SqlTransaction)null, commandType, commandText, commandParameters);
        }
        /// <summary>
        /// 执行指定数据库连接对象的数据阅读器.
        /// </summary>
        /// <remarks>
        /// 如果是SqlHelper打开连接,当连接关闭DataReader也将关闭.
        /// 如果是调用都打开连接,DataReader由调用都管理.
        /// </remarks>
        private static SqlDataReader ExecuteReader(SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText, SqlParameter[] commandParameters)
        {
            bool mustCloseConnection = false;
            SqlCommand cmd = new SqlCommand();
            try
            {
                PrepareCommand(cmd, connection, transaction, commandType, commandText, commandParameters, out mustCloseConnection);
                SqlDataReader dataReader;
                dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                // 清除参数,以便再次使用..
                bool canClear = true;
                foreach (SqlParameter commandParameter in cmd.Parameters)
                {
                    if (commandParameter.Direction != ParameterDirection.Input)
                        canClear = false;
                }
                if (canClear)
                {
                    cmd.Parameters.Clear();
                }
                return dataReader;
            }
            catch (Exception ex)
            {
                connection.Dispose();
                ExceptionDeal(ex, commandText, null); return null;
            }
        }
        public static object ExecuteScalar(CommandType cmdType, string commandText, SqlParameter[] commandParameters = null)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                return ExecuteScalar(connection, cmdType, commandText, commandParameters);
            }
        }
        public static object ExecuteScalar(string commandText, SqlParameter[] commandParameters = null)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                return ExecuteScalar(connection, CommandType.Text, commandText, commandParameters);
            }
        }
        public static object ExecuteScalar(SqlConnection connection, CommandType commandType, string commandText, SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            bool mustCloseConnection = false;
            try
            {
                PrepareCommand(cmd, connection, (SqlTransaction)null, commandType, commandText, commandParameters, out mustCloseConnection);
                // 执行SqlCommand命令,并返回结果.
                object retval = cmd.ExecuteScalar();
                // 清除参数,以便再次使用.
                cmd.Parameters.Clear();
                return retval;
            }
            catch (Exception ex) { ExceptionDeal(ex, commandText, commandParameters); return null; }
            finally { if (mustCloseConnection) { connection.Close(); } cmd.Dispose(); }
        }
        /// <summary>
        /// 创建SqlCommand命令,指定数据库连接对象,存储过程名和参数.
        /// </summary>
        /// <remarks>
        /// 示例:  
        ///  SqlCommand command = CreateCommand(conn, "AddCustomer", "CustomerID", "CustomerName");
        /// </remarks>
        /// <param name="connection">一个有效的数据库连接对象</param>
        /// <param name="spName">存储过程名称</param>
        /// <param name="sourceColumns">源表的列名称数组</param>
        /// <returns>返回SqlCommand命令</returns>
        public static SqlCommand CreateCommand(SqlConnection connection, string spName, params string[] sourceColumns)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

            // 创建命令
            SqlCommand cmd = new SqlCommand(spName, connection);
            cmd.CommandType = CommandType.StoredProcedure;

            // 如果有参数值
            if ((sourceColumns != null) && (sourceColumns.Length > 0))
            {
                // 从缓存中加载存储过程参数,如果缓存中不存在则从数据库中检索参数信息并加载到缓存中. 
                SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(connection, spName);

                // 将源表的列到映射到DataSet命令中.
                for (int index = 0; index < sourceColumns.Length; index++)
                    commandParameters[index].SourceColumn = sourceColumns[index];

                // Attach the discovered parameters to the SqlCommand object
                AttachParameters(cmd, commandParameters);
            }

            return cmd;
        }
        /// <summary>
        /// 联接查询,统一使用左连接
        /// </summary>
        /// <param name="fields">字段名,主表为A,次表为B</param>
        /// <param name="mtable">主表名</param>
        /// <param name="stable">次表名</param>
        public static DataTable JoinQuery(string fields, string mtable, string stable, string on, string where = "", string order = "", SqlParameter[] sp = null)
        {
            string sql = "SELECT {0} FROM {1} A LEFT JOIN {2} B ON {3} ";
            if (!string.IsNullOrEmpty(where))
            {
                sql += " WHERE " + where;
            }
            if (!string.IsNullOrEmpty(order))
            {
                if (!order.ToUpper().Contains("ORDER BY ")) { order = " ORDER BY " + order; }
                sql += order;
            }
            sql = string.Format(sql, fields, mtable, stable, on);
            return ExecuteTable(CommandType.Text, sql, sp);
        }
        /// <summary>
        /// 批量插入,必须为dt指定表名,请注意列名的大小写
        /// </summary>
        public static void Insert_Bat(DataTable dt, string connStr)
        {
            Insert_Bat(dt, connStr, SqlBulkCopyOptions.Default);
        }
        public static void Insert_Bat(DataTable dt, string connStr, SqlBulkCopyOptions option)
        {
            if (string.IsNullOrEmpty(dt.TableName)) { throw new Exception("表名不能为空"); }
            using (SqlBulkCopy bulk = new SqlBulkCopy(connStr, option))//保留数据表中的主键ID信息
            {
                bulk.DestinationTableName = dt.TableName;

                foreach (DataColumn dc in dt.Columns)
                {
                    bulk.ColumnMappings.Add(dc.ColumnName, dc.ColumnName);
                }

                bulk.WriteToServer(dt);
            }
        }
        //[main]
        public static DataTable ExecuteTable(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            //Debug_Start();
            SqlCommand cmd = new SqlCommand();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    PrepareCommand(cmd, connection, cmdType, cmdText, commandParameters);
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = cmd;
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet, "Result");
                    cmd.Parameters.Clear();
                    //Debug_End();
                    if (dataSet.Tables.Count > 0)
                    {
                        return dataSet.Tables["Result"];
                    }
                    return null;
                }
                catch (Exception ex)
                {
                    ExceptionDeal(ex, cmdText, commandParameters); return null;
                }
                finally { connection.Dispose(); }
            }
        }
        /// <summary>
        /// [main]执行指定数据库连接对象的命令
        /// </summary>
        /// <remarks>
        /// 示例:  
        ///  int result = ExecuteNonQuery(conn, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connection">一个有效的数据库连接对象</param>
        /// <param name="commandType">命令类型(存储过程,命令文本或其它.)</param>
        /// <param name="commandText">T存储过程名称或T-SQL语句</param>
        /// <param name="commandParameters">SqlParamter参数数组</param>
        /// <returns>返回影响的行数</returns>
        public static int ExecuteNonQuery(SqlConnection connection, CommandType commandType, string commandText, SqlParameter[] commandParameters)
        {
            //Debug_Start();
            try
            {
                SqlCommand cmd = new SqlCommand();
                bool mustCloseConnection = false;
                PrepareCommand(cmd, connection, (SqlTransaction)null, commandType, commandText, commandParameters, out mustCloseConnection);
                int retval = cmd.ExecuteNonQuery();
                // 清除参数,以便再次使用.
                cmd.Parameters.Clear();
                if (mustCloseConnection) { connection.Close(); }
                //Debug_End();
                return retval;
            }
            catch (Exception ex)
            {
                ExceptionDeal(ex, commandText, commandParameters); return 0;
            }
        }
        public static int ExecuteNonQuery(CommandType commandType, string commandText, SqlParameter[] commandParameters=null)
        {
            using (SqlConnection connection = new SqlConnection(SqlHelper.ConnectionString))
            {
                return ExecuteNonQuery(connection, commandType, commandText, commandParameters);
            }
        }
        public static int ExecuteNonQuery(string connstr, CommandType commandType, string commandText, SqlParameter[] commandParameters)
        {
            using (SqlConnection connection = new SqlConnection(connstr))
            {
                return ExecuteNonQuery(connection, commandType, commandText, commandParameters);
            }
        }
        //抛出异常
        private static void ExceptionDeal(Exception ex, string sql, SqlParameter[] sp)
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
    }
    /// <summary>
    /// SqlHelperParameterCache提供缓存存储过程参数,并能够在运行时从存储过程中探索参数.
    /// </summary>
    public sealed class SqlHelperParameterCache
    {
        #region 私有方法,字段,构造函数
        // 私有构造函数,妨止类被实例化.
        //instances from being created with "new SqlHelperParameterCache()"
        private SqlHelperParameterCache() { }

        // 这个方法要注意
        private static Hashtable paramCache = Hashtable.Synchronized(new Hashtable());

        /// <summary>
        /// 探索运行时的存储过程,返回SqlParameter参数数组.
        /// 初始化参数值为 DBNull.Value.
        /// </summary>
        /// <param name="connection">一个有效的数据库连接</param>
        /// <param name="spName">存储过程名称</param>
        /// <param name="includeReturnValueParameter">是否包含返回值参数</param>
        /// <returns>返回SqlParameter参数数组</returns>
        private static SqlParameter[] DiscoverSpParameterSet(SqlConnection connection, string spName, bool includeReturnValueParameter)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

            SqlCommand cmd = new SqlCommand(spName, connection);
            cmd.CommandType = CommandType.StoredProcedure;

            connection.Open();
            // 检索cmd指定的存储过程的参数信息,并填充到cmd的Parameters参数集中.
            SqlCommandBuilder.DeriveParameters(cmd);
            connection.Close();
            // 如果不包含返回值参数,将参数集中的每一个参数删除.
            if (!includeReturnValueParameter)
            {
                cmd.Parameters.RemoveAt(0);
            }
            // 创建参数数组
            SqlParameter[] discoveredParameters = new SqlParameter[cmd.Parameters.Count];
            // 将cmd的Parameters参数集复制到discoveredParameters数组.
            cmd.Parameters.CopyTo(discoveredParameters, 0);

            // 初始化参数值为 DBNull.Value.
            foreach (SqlParameter discoveredParameter in discoveredParameters)
            {
                discoveredParameter.Value = DBNull.Value;
            }
            return discoveredParameters;
        }

        /// <summary>
        /// SqlParameter参数数组的深层拷贝.
        /// </summary>
        /// <param name="originalParameters">原始参数数组</param>
        /// <returns>返回一个同样的参数数组</returns>
        private static SqlParameter[] CloneParameters(SqlParameter[] originalParameters)
        {
            SqlParameter[] clonedParameters = new SqlParameter[originalParameters.Length];

            for (int i = 0, j = originalParameters.Length; i < j; i++)
            {
                clonedParameters[i] = (SqlParameter)((ICloneable)originalParameters[i]).Clone();
            }

            return clonedParameters;
        }

        #endregion 私有方法,字段,构造函数结束

        #region 缓存方法

        /// <summary>
        /// 追加参数数组到缓存.
        /// </summary>
        /// <param name="connectionString">一个有效的数据库连接字符串</param>
        /// <param name="commandText">存储过程名或SQL语句</param>
        /// <param name="commandParameters">要缓存的参数数组</param>
        public static void CacheParameterSet(string connectionString, string commandText, params SqlParameter[] commandParameters)
        {
            if (connectionString == null || connectionString.Length == 0) throw new ArgumentNullException("connectionString");
            if (commandText == null || commandText.Length == 0) throw new ArgumentNullException("commandText");

            string hashKey = connectionString + ":" + commandText;

            paramCache[hashKey] = commandParameters;
        }

        /// <summary>
        /// 从缓存中获取参数数组.
        /// </summary>
        /// <param name="connectionString">一个有效的数据库连接字符</param>
        /// <param name="commandText">存储过程名或SQL语句</param>
        /// <returns>参数数组</returns>
        public static SqlParameter[] GetCachedParameterSet(string connectionString, string commandText)
        {
            if (connectionString == null || connectionString.Length == 0) throw new ArgumentNullException("connectionString");
            if (commandText == null || commandText.Length == 0) throw new ArgumentNullException("commandText");

            string hashKey = connectionString + ":" + commandText;

            SqlParameter[] cachedParameters = paramCache[hashKey] as SqlParameter[];
            if (cachedParameters == null)
            {
                return null;
            }
            else
            {
                return CloneParameters(cachedParameters);
            }
        }

        #endregion 缓存方法结束

        #region 检索指定的存储过程的参数集

        /// <summary>
        /// 返回指定的存储过程的参数集
        /// </summary>
        /// <remarks>
        /// 这个方法将查询数据库,并将信息存储到缓存.
        /// </remarks>
        /// <param name="connectionString">一个有效的数据库连接字符</param>
        /// <param name="spName">存储过程名</param>
        /// <returns>返回SqlParameter参数数组</returns>
        public static SqlParameter[] GetSpParameterSet(string connectionString, string spName)
        {
            return GetSpParameterSet(connectionString, spName, false);
        }

        /// <summary>
        /// 返回指定的存储过程的参数集
        /// </summary>
        /// <remarks>
        /// 这个方法将查询数据库,并将信息存储到缓存.
        /// </remarks>
        /// <param name="connectionString">一个有效的数据库连接字符.</param>
        /// <param name="spName">存储过程名</param>
        /// <param name="includeReturnValueParameter">是否包含返回值参数</param>
        /// <returns>返回SqlParameter参数数组</returns>
        public static SqlParameter[] GetSpParameterSet(string connectionString, string spName, bool includeReturnValueParameter)
        {
            if (connectionString == null || connectionString.Length == 0) throw new ArgumentNullException("connectionString");
            if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                return GetSpParameterSetInternal(connection, spName, includeReturnValueParameter);
            }
        }

        /// <summary>
        /// [内部]返回指定的存储过程的参数集(使用连接对象).
        /// </summary>
        /// <remarks>
        /// 这个方法将查询数据库,并将信息存储到缓存.
        /// </remarks>
        /// <param name="connection">一个有效的数据库连接字符</param>
        /// <param name="spName">存储过程名</param>
        /// <returns>返回SqlParameter参数数组</returns>
        internal static SqlParameter[] GetSpParameterSet(SqlConnection connection, string spName)
        {
            return GetSpParameterSet(connection, spName, false);
        }

        /// <summary>
        /// [内部]返回指定的存储过程的参数集(使用连接对象)
        /// </summary>
        /// <remarks>
        /// 这个方法将查询数据库,并将信息存储到缓存.
        /// </remarks>
        /// <param name="connection">一个有效的数据库连接对象</param>
        /// <param name="spName">存储过程名</param>
        /// <param name="includeReturnValueParameter">
        /// 是否包含返回值参数
        /// </param>
        /// <returns>返回SqlParameter参数数组</returns>
        internal static SqlParameter[] GetSpParameterSet(SqlConnection connection, string spName, bool includeReturnValueParameter)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            using (SqlConnection clonedConnection = (SqlConnection)((ICloneable)connection).Clone())
            {
                return GetSpParameterSetInternal(clonedConnection, spName, includeReturnValueParameter);
            }
        }

        /// <summary>
        /// [私有]返回指定的存储过程的参数集(使用连接对象)
        /// </summary>
        /// <param name="connection">一个有效的数据库连接对象</param>
        /// <param name="spName">存储过程名</param>
        /// <param name="includeReturnValueParameter">是否包含返回值参数</param>
        /// <returns>返回SqlParameter参数数组</returns>
        private static SqlParameter[] GetSpParameterSetInternal(SqlConnection connection, string spName, bool includeReturnValueParameter)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

            string hashKey = connection.ConnectionString + ":" + spName + (includeReturnValueParameter ? ":include ReturnValue Parameter" : "");

            SqlParameter[] cachedParameters;

            cachedParameters = paramCache[hashKey] as SqlParameter[];
            if (cachedParameters == null)
            {
                SqlParameter[] spParameters = DiscoverSpParameterSet(connection, spName, includeReturnValueParameter);
                paramCache[hashKey] = spParameters;
                cachedParameters = spParameters;
            }

            return CloneParameters(cachedParameters);
        }

        #endregion 参数集检索结束

    }
}