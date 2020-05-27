using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLa.SQLDAL
{
    public static class DBHelper
    {
        //--------------------------------Table AND View
        public static bool View_IsExist(string name)
        {
            try
            {
                string sql = "SELECT 1 FROM sys.views WHERE name='" + name + "'";
                DataTable dt = SqlHelper.ExecuteTable(CommandType.Text, sql);
                return (dt.Rows.Count > 0);
            }
            catch { return false; }
        }
        public static DataTable View_List(string connstr)
        {
            //name,crdate,type=v
            string sql = "SELECT * FROM sys.views";
            return SqlHelper.ExecuteTable(sql);
        }
        public static bool Table_IsExist(string tbname)
        {
            return Table_IsExist(new M_SQL_Connection()
            {
                constr = SqlHelper.ConnectionString,
                tbname = tbname
            });
        }
        public static bool Table_IsExist(M_SQL_Connection model)
        {
            if (string.IsNullOrEmpty(model.tbname)) { return true; }
            try
            {
                string sql = "SELECT * FROM dbo.SysObjects WHERE ID = object_id(N'[" + model.tbname + "]') AND OBJECTPROPERTY(ID, 'IsTable') = 1";
                return SqlHelper.ExecuteTable(sql).Rows.Count > 0;
            }
            catch { return false; }
        }
        public static void Table_Del(string tbname)
        {
            if (Table_IsExist(tbname))
            {
                string sql = "DROP TABLE " + tbname;
                SqlHelper.ExecuteSql(sql);
            }
        }
        public static DataTable Table_List(string connstr)
        {
            string sql = "SELECT name,xtype,crdate FROM sysobjects WHERE xtype in('U')";//,'S'
            return SqlHelper.ExecuteTable(connstr, CommandType.Text, sql, null);
        }
        public static void Table_Add(M_SQL_Connection model)
        {

        }
        /// <summary>
        /// �����ֶν������ݿ�
        /// </summary>
        /// <param name="model">����ģ��</param>
        /// <param name="field">�ֶ�ģ��</param>
        public static void Table_AddField(M_SQL_Connection model, M_SQL_Field field)
        {
            string sql = "ALTER TABLE [" + model.tbname + "] ADD [" + field.fieldName + "] ";
            switch (field.fieldType.ToLower())
            {
                case "int":
                case "money":
                case "ntext":
                case "text":
                case "bit":
                case "datetime":
                    sql += field.fieldType;
                    break;
                default:
                    sql += field.fieldType + "(" + field.fieldLen + ") ";
                    break;
            }
            if (!string.IsNullOrEmpty(field.defval)) { sql += " DEFAULT ('" + field.defval + "')"; }
            if (string.IsNullOrEmpty(model.constr)) { model.constr = SqlHelper.ConnectionString; }
            DBHelper.ExecuteSQL(model.constr, sql);
        }
        //------------------------------------DB
        public static bool DB_Exist(string connectString, string dbName)
        {
            bool flag = false;
            DataTable dt = DB_GetList(connectString);
            dt.DefaultView.RowFilter = "name in ('" + dbName + "')";
            if (dt.DefaultView.ToTable().Rows.Count > 0)
            {
                flag = true;
            }
            return flag;
        }
        /// <summary>
        /// �������ݿ�
        /// </summary>
        public static void DB_Create(string connstr, string dbName)
        {
            //string connstr1 = "Data Source=" + datasourcesa + ";Initial Catalog=" + datanamesa + ";User ID=" + usernamesa + ";Password=" + userpwdsa;
            // SqlConnection connsa1 = Install.Connection(connstr1);
            //�������ݿ�
            //string connectionString = string.Format(@"Data Source={0};User ID={1};Password={2};Initial Catalog={3};Pooling=false", source, userID, passWD, "master");
            string commandText = string.Format("CREATE DATABASE [{0}]", dbName);
            ExecuteSQL(connstr, commandText);
        }
        public static void DB_Remove(string connectString, string dbName)
        {
            string sql = "Drop DataBase " + dbName;
            SqlConnection cn = new SqlConnection(connectString);
            SqlCommand cmd = new SqlCommand(sql, cn);
            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex) { throw (new Exception(dbName + "����ʧ�ܣ�" + ex.Message)); }
            //catch { throw (new Exception("����ʧ��,��ΪSQL�û�����sysadmin��ɫ")); }
            finally
            {
                cn.Close();
                cmd.Dispose();
            }
        }
        /// <summary>
        /// �������ݿ�
        /// </summary>
        public static void DB_Attach(string mdfSource, string logSource, string dbName)
        {
            string sql = "exec sp_attach_db @dbname='" + dbName + "',@filename1='" + mdfSource + "',@filename2='" + logSource + "'";
            string strcon = "Server=(local);Integrated Security=SSPI;Initial Catalog=master";
            SqlConnection cn = new SqlConnection(strcon);
            SqlCommand cmd = new SqlCommand(sql, cn);
            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
        }
        /// <summary>
        /// ���ݿ���Ϣ,����ΪdataTable����
        /// </summary>
        private static DataTable DB_GetList(string connectString)
        {
            DataTable dt = new DataTable();
            String connectionString = connectString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand("SELECT name,database_id FROM sys.databases ORDER BY Name", conn))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {

                        dt.Load(reader);
                    }
                }
                return dt;
            }
        }
        /// <summary>
        /// ��ǰ���ݿ��Ƿ񱾵�
        /// </summary>
        /// <param name="ip">�����IP��ַ(Request.ServerVariables["LOCAl_ADDR"];)</param>
        /// <returns>True:����</returns>
        public static bool IsLocalDB(string serverip, string domain)
        {
            serverip = serverip.ToLower().Trim();
            domain = domain.ToLower().Trim();
            string dbip = DBHelper.GetAttrByStr(SqlHelper.ConnectionString, "Data Source").ToLower().TrimStart('(').TrimEnd(')').Trim();
            bool flag = false;
            switch (dbip)
            {
                case "local":
                case ".":
                case "127.0.0.1":
                    flag = true;
                    break;
                default:
                    if (dbip.Equals(serverip) || dbip.Equals(domain)) { flag = true; }
                    break;
            }
            return flag;
        }
        /// <summary>
        /// ������ݿ��е����,��ͼ,��,�洢����
        /// </summary>
        public static void DB_Clear(string connstr)
        {
            string dbname = GetAttrByStr(connstr, "Initial Catalog").Replace(" ", "").ToLower();
            if (string.IsNullOrEmpty(dbname)) { throw new Exception("���ݿⲻ��Ϊ��"); }
            string[] banDB = "master,model,msdb.tempdb".Split(',');
            foreach (string db in banDB) { if (banDB.Equals(dbname)) { throw new Exception("ȡ������,ԭ��:���ݿ�[" + dbname + "]"); } }
            var delContrast = "DECLARE c1 cursor for "
                    + "select 'alter table ['+ object_name(parent_obj) + '] drop constraint ['+name+']; ' "
                    + "from sysobjects "
                    + "where xtype = 'F' "
                    + "open c1 "
                    + "declare @c1 varchar(8000) "
                    + "fetch next from c1 into @c1 "
                    + "while(@@fetch_status=0) "
                    + "begin "
                    + "exec(@c1) "
                    + "fetch next from c1 into @c1 "
                    + "end "
                    + "close c1 "
                    + "deallocate c1 ";
            DBHelper.ExecuteSQL(connstr, delContrast);
            DataTable dt = Table_List(connstr);
            foreach (DataRow dr in dt.Rows)
            {
                string sql = "DROP TABLE " + dr["name"];
                DBHelper.ExecuteSQL(connstr, sql);
            }
            DataTable viewdt = View_List(connstr);
            foreach (DataRow dr in viewdt.Rows)
            {
                string sql = "DROP TABLE " + dr["name"];
                DBHelper.ExecuteSQL(connstr, sql);
            }
            //�Ƴ��洢����
            var delProce = "declare @procName varchar(500) "
             + "declare cur cursor "
             + "for select [name] from sys.objects where type = 'p' "
             + "open cur "
             + "fetch next from cur into @procName "
             + "while @@fetch_status = 0 "
             + "begin "
             + "if @procName <> 'DeleteAllProcedures' "
             + "exec('drop procedure ' + @procName) "
             + "fetch next from cur into @procName "
             + "end "
             + "close cur "
             + "deallocate cur ";
            DBHelper.ExecuteSQL(connstr, delProce);
        }
        //------------------------------------Common
        /// <summary>
        /// ִ�����ݽű�,������GO��ͷ,���нű�������GO�и�
        /// </summary>
        /// <param name="fileName">�ű�����·��</param>
        public static bool ExecuteSqlScript(string connectString, string fileName)
        {
            SqlConnection connection = new SqlConnection(connectString);
            SqlCommand command = new SqlCommand();
            connection.Open();
            command.Connection = connection;
            using (StreamReader reader = new StreamReader(fileName, Encoding.UTF8))
            {
                try
                {
                    while (!reader.EndOfStream)
                    {
                        StringBuilder builder = new StringBuilder();
                        while (!reader.EndOfStream)
                        {
                            string str = reader.ReadLine();
                            if (!string.IsNullOrEmpty(str) && str.ToUpper().Trim().Equals("GO"))
                            {
                                break;
                            }
                            builder.AppendLine(str);
                        }
                        command.CommandType = CommandType.Text;
                        command.CommandText = builder.ToString();
                        command.CommandTimeout = 300;
                        command.ExecuteNonQuery();

                    }
                }
                catch (SqlException ex)//����ʱ�׳��쳣
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                    connection.Dispose();
                }
            }
            command.Dispose();
            connection.Close();
            connection.Dispose();
            return true;
        }
        public static string ExecuteSQL(string connectionString, string sql, SqlParameter[] sp = null)
        {
            string result = "";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            try
            {
                PrepareCommand(cmd, connection, (SqlTransaction)null, CommandType.Text, sql, sp);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex) { result = ex.Message; }
            finally { cmd.Parameters.Clear(); cmd.Dispose(); connection.Close(); }
            return result;
        }
        private static void PrepareCommand(SqlCommand command, SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText, SqlParameter[] commandParameters)
        {
            if (command == null) throw new ArgumentNullException("command");
            if (commandText == null || commandText.Length == 0) throw new ArgumentNullException("commandText");
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            // ���������һ�����ݿ�����.
            command.Connection = connection;
            // ���������ı�(�洢��������SQL���)
            command.CommandText = commandText;
            // ��������
            if (transaction != null)
            {
                transaction = connection.BeginTransaction();
                if (transaction.Connection == null) throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
                command.Transaction = transaction;

            }

            // ������������.
            command.CommandType = commandType;

            // �����������
            if (commandParameters != null)
            {
                AttachParameters(command, commandParameters);
            }
            return;
        }
        private static void AttachParameters(SqlCommand command, SqlParameter[] commandParameters)
        {
            if (commandParameters != null)
            {
                foreach (SqlParameter p in commandParameters)
                {
                    if (p != null)
                    {
                        // ���δ����ֵ���������,���������DBNull.Value.
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
        //------------------------------------Tools
        /// <summary>
        /// ��ConnectionString��ʽ���ַ�������,Initial Catalog|Data Source
        /// </summary>
        /// <param name="constr">�����ַ���</param>
        /// <param name="name">������,ʾ��:Data Source</param>
        /// <returns>=���ұߵ�ֵ</returns>
        public static string GetAttrByStr(string constr, string name)
        {
            try
            {
                if (string.IsNullOrEmpty(constr) || string.IsNullOrEmpty(name)) { return ""; }
                name = name.Trim();
                string[] arr = constr.Split(';');
                foreach (string attr in arr)
                {
                    if (string.IsNullOrEmpty(attr) || !attr.Contains("=")) continue;
                    string n = attr.Split('=')[0].Trim();
                    string v = attr.Split('=')[1];
                    if (n.Equals(name, StringComparison.OrdinalIgnoreCase)) { return v; }
                }
                return "";
            }
            catch (Exception ex) { throw new Exception("GetAttrByStr��" + constr + "," + name + "ԭ��:" + ex.Message); }
        }
        /// <summary>
        /// ����Ƿ�Ϊ�ؼ���,���ڼ���ֶ���
        /// </summary>
        public static bool IsKeyWord(string field)
        {
            field = field.Replace(" ", "");
            string[] keys = "select,update,del,delete,table,file,lock,account,interval,public,package,option,system,user,check,size,type,level,content,group,update,resource,admin,add,uid,number,count,audit,time,scale,file,modify,columns,extend,rename,initial,comment,desc,successful,name".Split(',');
            foreach (string key in keys)
            {
                if (key.Equals(field, StringComparison.CurrentCultureIgnoreCase)) { return true; }
            }
            return false;
        }
    }
    public class M_SQL_Connection
    {
        public string constr = "";
        public string tbname = ""; 
    }
    public class M_SQL_Field 
    {
        //�ֶ���,����,����,Ĭ��ֵ,�Ƿ����������Ƿ�ɿ�
        public string fieldName = "";
        public string fieldType = "";
        public int fieldLen = 4;
        public string defval = "";
        public bool ispk = false;
        public bool isnull = true;
    }
}
