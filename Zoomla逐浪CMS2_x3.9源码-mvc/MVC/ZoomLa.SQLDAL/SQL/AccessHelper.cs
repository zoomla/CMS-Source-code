using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data.Common;
using ZoomLa.Model;

namespace ZoomLa.SQLDAL.SQL
{
    public class AccessHelper : SqlBase
    {
        private OleDbConnection GetConnection()
        {
            OleDbConnection conn = new OleDbConnection(ConnectionString);
            try
            {
                conn.Open(); return conn;
            }
            catch (Exception ex) { conn.Dispose(); ExceptionDeal(ex, "打开ACCESS连接失败"); }
            return null;
        }
        private void PreCommand(OleDbCommand command, List<SqlParameter> spList)
        {
            if (spList == null || spList.Count < 1) { return; }
            foreach (SqlParameter sp in spList)
            {
                if (sp == null) continue;
                if (sp.Value == DBNull.Value || sp.Value == null) { sp.Value = ""; }
                OleDbParameter mysp = new OleDbParameter();
                mysp.ParameterName = sp.ParameterName;
                mysp.Value = sp.Value;
                command.Parameters.Add(sp);
            }
        }
        public bool ExecuteSql(string sql, List<SqlParameter> spList = null)
        {
            using (OleDbConnection connection = GetConnection())
            {
                using (OleDbCommand command = new OleDbCommand(sql, connection))
                {
                    try
                    {
                        PreCommand(command, spList);
                        return command.ExecuteNonQuery() > 0;
                    }
                    catch (OleDbException ex)
                    {
                        ExceptionDeal(ex, command.CommandText, spList);
                        return false;
                    }
                    finally { connection.Dispose(); }
                }
            }
        }
        public override string DBType
        {
            get { return "access"; }
        }
        public override string GetSQL(SqlModel model)
        {
            if (!string.IsNullOrEmpty(model.sql)) { return model.sql; }
            string sql = "";
            GetWhereSql(model);
            switch (model.cmd)
            {
                case SqlModel.SqlCmd.Insert:
                    //insert into table1(field1,field2) values(value1,value2)
                    sql = "INSERT INTO " + model.tbName + " ({0}) VALUES({1});SELECT @@IDENTITY;";
                    sql = string.Format(sql, model.fields, model.values);
                    break;
                case SqlModel.SqlCmd.Delete:
                    //delete * from table1 
                    sql = "DELETE * FROM " + model.tbName;
                    if (!string.IsNullOrEmpty(model.where))
                    {
                        sql += " WHERE " + model.where;
                    }
                    break;
                case SqlModel.SqlCmd.Update:
                    //update table1 set field1=value1 
                    sql = "UPDATE " + model.tbName + " SET " + model.set + " ";
                    if (!string.IsNullOrEmpty(model.where))
                    {
                        sql += " WHERE " + model.where;
                    }
                    break;
                case SqlModel.SqlCmd.Select:
                    //SELECT * FROM {tbName} WHERE {where} ORDER BY {order}
                    if (model.size > 0) { model.fields = " TOP " + model.size + " " + model.fields; }
                    sql = "SELECT " + model.fields + " FROM " + model.tbName;
                    if (!string.IsNullOrEmpty(model.where))
                    {
                        sql += " WHERE " + model.where;
                    }
                    if (!string.IsNullOrEmpty(model.order))
                    {
                        sql += " ORDER BY " + model.order;
                    }
                    break;
            }
            return sql;
        }

        public override void ExecuteNonQuery(SqlModel model)
        {
            string sql = GetSQL(model);
            using (OleDbConnection conn = GetConnection())
            {
                OleDbCommand command = new OleDbCommand(sql, conn);
                try
                {
                    PreCommand(command, model.spList);
                    command.ExecuteNonQuery();
                }
                catch (Exception ex) { ExceptionDeal(ex, sql, model.spList); }
                finally { conn.Dispose(); }
            }
        }

        public override DataTable ExecuteTable(SqlModel model)
        {
            DataTable result = new DataTable();
            using (OleDbConnection connection = GetConnection())
            {
                using (OleDbCommand command = new OleDbCommand(GetSQL(model), connection))
                {
                    PreCommand(command, model.spList);
                    try
                    {
                        OleDbDataAdapter adapter = new OleDbDataAdapter();
                        adapter.SelectCommand = command;
                        DataSet dataSet = new DataSet();
                        adapter.Fill(dataSet, "Result");
                        command.Parameters.Clear();
                        if (dataSet.Tables.Count > 0)
                        {
                            return dataSet.Tables["Result"];
                        }
                        return null;
                    }
                    catch (OleDbException ex)
                    {
                        ExceptionDeal(ex, command.CommandText, model.spList);
                        return null;
                    }
                    finally { connection.Dispose(); }
                }
            }
        }

        public override DbDataReader ExecuteReader(SqlModel model)
        {
            string sql = GetSQL(model);
            OleDbConnection conn = GetConnection();
            OleDbCommand command = new OleDbCommand(sql, conn);
            try
            {
                PreCommand(command, model.spList);
                DbDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                return reader;
            }
            catch (Exception ex) { conn.Dispose(); ExceptionDeal(ex, sql, model.spList); return null; }
        }
        /// <summary>
        /// 用于执行Insert,返回ID
        /// </summary>
        public override object ExecuteScala(SqlModel model)
        {
            string sql = GetSQL(model);
            using (OleDbConnection conn = GetConnection())
            {
                try
                {
                    OleDbCommand command = new OleDbCommand(sql, conn);
                    PreCommand(command, model.spList);
                    return command.ExecuteScalar();
                }
                catch (Exception ex) { conn.Dispose(); ExceptionDeal(ex, sql, model.spList.ToArray()); return 0; }
            }
        }

        public override int InsertID(SqlModel model)
        {
            return DataConvert.CLng(ExecuteScala(model));
        }

        //--------------分页与连结
        public override DataTable SelPage(PageSetting config)
        {
            if (!string.IsNullOrEmpty(config.t2))
            {
                config.sql = "SELECT TOP " + config.psize + config.fields + " FROM " + config.t1 + " A  " + config.join + " " + config.t2 + " B ON " + config.on + " WHERE ";
                if (config.cursize > 0) { config.sql += config.pk + " Not In(SELECT TOP " + config.cursize + " " + config.pk + " FROM " + config.t1 + " A LEFT JOIN " + config.t2 + " B ON " + config.on + " WHERE " + config.where + config.order + ") AND "; }
                config.sql += config.where + config.order;
                config.countsql = "SELECT COUNT(*) FROM " + config.t1 + " A LEFT JOIN " + config.t2 + " B ON " + config.on + " WHERE " + config.where;
            }
            else
            {
                config.sql = "SELECT TOP " + config.psize + config.fields + " FROM " + config.t1 + " A WHERE ";
                if (config.cursize > 0) { config.sql += config.pk + " Not In(Select Top " + config.cursize + " " + config.pk + " FROM " + config.t1 + " WHERE " + config.where + config.order + ") And "; }
                config.sql += config.where + config.order;
                config.countsql = "SELECT COUNT(*) FROM " + config.t1 + " WHERE " + config.where;
            }
            config.DealWithAlias();
            SqlModel sp = new SqlModel();
            config.itemCount = DataConvert.CLng(ExecuteScala(new SqlModel(config.countsql, config.sp)));
            config.pageCount = GetPageCount(config.itemCount, config.psize);
            return ExecuteTable(new SqlModel(config.sql, config.sp));
        }
        /// <summary>
        /// 联接查询,统一使用左连接
        /// </summary>
        public override DataTable JoinQuery(PageSetting config)
        {
            string sql = "SELECT {0} FROM {1} A LEFT JOIN {2} B ON {3} ";
            if (!string.IsNullOrEmpty(config.where))
            {
                sql += " WHERE " + config.where;
            }
            if (!string.IsNullOrEmpty(config.order))
            {
                if (!config.order.ToUpper().Contains("ORDER BY ")) { config.order = " ORDER BY " + config.order; }
                sql += config.order;
            }
            sql = string.Format(sql, config.fields, config.t1, config.t2, config.on);
            return ExecuteTable(new SqlModel(sql, config.sp));
        }

        public override DataTable SelTop(PageSetting config)
        {
            if (string.IsNullOrEmpty(config.t2))//单表
            {
                config.sql = "SELECT TOP " + config.psize + config.fields + " FROM " + config.t1;
            }
            else
            {
                config.sql = "SELECT TOP " + config.psize + config.fields + "  FROM {0} A LEFT JOIN {1} B ON {2} ";
                config.sql = string.Format(config.sql, config.t1, config.t2, config.on);
            }

            if (!string.IsNullOrEmpty(config.where)) { config.sql += " WHERE " + config.where; }
            if (!string.IsNullOrEmpty(config.order)) { config.sql += config.order; }
            return ExecuteTable(new SqlModel() { sql = config.sql });
        }

        public override string GetWhereSql(SqlModel model)
        {
            string sql = "";
            if (model.whereList == null || model.whereList.Count < 1) { return sql; }
            foreach (Sql_Where where in model.whereList)
            {
                switch (where.type)
                {
                    case "date":
                        sql += GetDateSql(where);
                        break;
                    default:
                        break;
                }
            }
            model.where += sql;
            return sql;
        }

        public override string GetDateSql(Sql_Where where)
        {
            string datesql = "";
            if (!string.IsNullOrEmpty(where.stime)) { datesql += where.field + ">'" + where.stime + "'"; }
            if (!string.IsNullOrEmpty(where.etime))
            {
                if (!string.IsNullOrEmpty(where.stime)) { datesql += " AND "; }
                datesql += where.field + "<#" + where.etime + "#";
            }
            datesql = " " + where.join + " (" + datesql + ")";
            return datesql;
        }

        public override string GetFields(M_Base model)
        {
            string str = string.Empty, PK = model.PK.ToLower();
            string[,] strArr = model.FieldList();
            for (int i = 0; i < strArr.GetLength(0); i++)
            {
                if (strArr[i, 0].ToLower() != PK)
                {
                    str += "" + strArr[i, 0] + ",";
                }
            }
            return str.Substring(0, str.Length - 1);
        }

        public override string GetParams(M_Base model)
        {
            string str = string.Empty, PK = model.PK.ToLower();
            string[,] strArr = model.FieldList();
            for (int i = 0; i < strArr.GetLength(0); i++)
            {
                if (strArr[i, 0].ToLower() != PK)
                {
                    str += "@" + strArr[i, 0] + ",";
                }
            }
            return str.Substring(0, str.Length - 1);
        }

        public override string GetFieldAndParam(M_Base model)
        {
            string str = string.Empty, PK = model.PK.ToLower();
            string[,] strArr = model.FieldList();
            for (int i = 0; i < strArr.GetLength(0); i++)
            {
                if (strArr[i, 0].ToLower() != PK)
                {
                    str += "[" + strArr[i, 0] + "]=@" + strArr[i, 0] + ",";
                }
            }
            return str.Substring(0, str.Length - 1);
        }

        public override bool Table_Exist(string name)
        {
            //string sql = "SELECT * FROM " + name;
            //try
            //{
            //    DataTable dt = ExecuteTable(new SqlModel() { sql = sql });
            //    return true;
            //}
            //catch (Exception ex)
            //{
            //    return false;
            //}
            using (OleDbConnection conn = GetConnection())
            {
                DataTable dt = GetConnection().GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new Object[] { null, null, name, "Table" });
                return dt.Rows.Count > 0;
            }
        }

        public override bool Table_Add(string table, M_SQL_Field field)
        {
            throw new NotImplementedException();
        }

        public override bool Table_Remove(string name)
        {
            string sql = "DROP TABLE " + name;
            return ExecuteSql(sql);
        }

        public override bool Table_Clear(string name)
        {
            if (Table_Exist(name))
            {
                string sql = "DELETE TABLE FROM " + name;
                return ExecuteSql(sql);
            }
            return false;
        }

        public override DataTable Table_List()
        {
            using (OleDbConnection conn = GetConnection())
            {
                DataTable dt = GetConnection().GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new Object[] { null, null, null, "Table" });
                dt.Columns["TABLE_NAME"].ColumnName = "Name";
                return dt;
            }
        }

        public override bool View_Exist(string name)
        {
            using (OleDbConnection conn = GetConnection())
            {
                DataTable dt = GetConnection().GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new Object[] { null, null, name, "View" });
                return dt.Rows.Count > 0;
            }
        }

        public override bool View_Drop(string name)
        {
            string sql = "DROP VIEW " + name;
            return ExecuteSql(sql);
        }

        public override bool Field_Add(string table, M_SQL_Field field)
        {
            throw new NotImplementedException();
        }

        public override bool Field_Remove(string table, string field)
        {
            string sql = "ALERT TABLE " + table + " DROP COLUMN " + field;
            return ExecuteSql(sql);
        }

        public override DataTable Field_List(string table)
        {
            using (OleDbConnection conn = GetConnection())
            {
                DataTable dt = GetConnection().GetOleDbSchemaTable(OleDbSchemaGuid.Columns, new Object[] { null, null, table, null });
                dt.Columns["COLUMN_NAME"].ColumnName = "Name";
                return dt;
            }
        }

        public override bool DB_Create(string dbname)
        {
            throw new NotImplementedException();
        }

        public override bool DB_Remove(string dbname)
        {
            throw new NotImplementedException();
        }

        public override bool DB_Exist(string dbName)
        {
            throw new NotImplementedException();
        }

        public override bool DB_Attach(string mdfSource, string logSource, string dbName)
        {
            throw new NotImplementedException();
        }
    }
}
