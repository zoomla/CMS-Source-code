using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.Data.Common;
using ZoomLa.Model;

namespace ZoomLa.SQLDAL.SQL
{

    public class MySqlHelper : SqlBase
    {
        //--------------Tools
        private MySqlConnection GetConnection(SqlModel model)
        {
            MySqlConnection conn = new MySqlConnection(ConnectionString);
            try
            {
                conn.Open();
                return conn;
            }
            catch (Exception ex) { conn.Dispose(); throw new Exception(ex.Message); }
        }
        private void PreCommand(MySqlCommand command, List<SqlParameter> spList)
        {
            if (spList == null || spList.Count < 1) { return; }
            foreach (SqlParameter sp in spList)
            {
                MySqlParameter mysp = new MySqlParameter();
                mysp.ParameterName = sp.ParameterName;
                mysp.Value = sp.Value;
                command.Parameters.Add(mysp);
            }
        }
        //--------------基础
        public override string DBType
        {
            get
            {
                return "mysql";
            }
        }

        ////--------------
        ///// <summary>
        ///// 根据参数,返回对应格式的sql语句
        ///// </summary>
        public override string GetSQL(SqlModel model)
        {
            if (!string.IsNullOrEmpty(model.sql)) { return model.sql; }
            string sql = "";
            GetWhereSql(model);
            switch (model.cmd)
            {
                case SqlModel.SqlCmd.Insert:
                    //insert into table1(field1,field2) values(value1,value2)
                    sql = "INSERT INTO " + model.tbName + " ({0}) VALUES({1})";
                    sql = string.Format(sql, model.fields, model.values);
                    break;
                case SqlModel.SqlCmd.Delete:
                    //delete from table1 
                    sql = "DELETE FROM " + model.tbName;
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
            MySqlConnection conn = GetConnection(model);
            try
            {
                MySqlCommand command = new MySqlCommand(GetSQL(model), conn);
                PreCommand(command, model.spList);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally { conn.Dispose(); }

        }
        /// <summary>
        /// 用于执行Insert,返回ID
        /// </summary>
        public override object ExecuteScala(SqlModel model)
        {
            string sql = GetSQL(model);
            using (MySqlConnection conn = GetConnection(model))
            {
                MySqlCommand command = new MySqlCommand(sql, conn);
                PreCommand(command, model.spList);
                return command.ExecuteScalar();
            }
            //return 0;
        }
        public override DataTable ExecuteTable(SqlModel model)
        {
            DataTable result = new DataTable();
            using (MySqlConnection conn = GetConnection(model))
            {
                MySqlCommand command = new MySqlCommand(GetSQL(model), conn);
                PreCommand(command, model.spList);
                MySqlDataAdapter adapter = new MySqlDataAdapter();
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

        }
        public override DbDataReader ExecuteReader(SqlModel model)
        {
            DataTable dt = ExecuteTable(model);
            DataTableReader reader = new DataTableReader(dt);
            return reader;

        }
        public override int InsertID(SqlModel model)
        {
            return DataConvert.CLng(ExecuteScala(model));
        }
        //--------------分页与连结
        public override DataTable SelPage(PageSetting config)
        {
            int offset = config.psize * (config.cpage - 1);
            if (string.IsNullOrEmpty(config.t2))
            {
                config.sql = "SELECT " + config.fields + " FROM " + config.t1 + " WHERE " + config.where + " LIMIT " + offset + "," + config.psize;
                config.countsql = "SELECT COUNT(*) FROM " + config.t1 + " WHERE " + config.where;
            }
            else
            {
                config.sql = "SELECT " + config.fields + " FROM " + config.t1 + " A LEFT JOIN " + config.t2 + " B ON " + config.on + " WHERE " + config.where + " LIMIT " + offset + "," + config.psize;
                config.countsql = "SELECT COUNT(*) FROM " + config.t1 + " A LEFT JOIN " + config.t2 + " B ON " + config.on + " WHERE " + config.where;
            }
            config.DealWithAlias();
            SqlModel countMod = new SqlModel() { sql = config.countsql };
            countMod.AddSpToList(config.sp);
            SqlModel model = new SqlModel() { sql = config.sql };
            model.AddSpToList(config.sp);
            config.itemCount = DataConvert.CLng(ExecuteScala(countMod));
            config.pageCount = GetPageCount(config.itemCount, config.psize);
            return ExecuteTable(model);
        }
        public override DataTable JoinQuery(PageSetting config)
        {
            config.sql = "SELECT {0} FROM {1} A LEFT JOIN {2} B ON {3} ";
            config.sql = string.Format(config.sql, config.fields, config.t1, config.t2, config.on);
            if (!string.IsNullOrEmpty(config.where)) { config.sql += " WHERE " + config.where; }
            if (!string.IsNullOrEmpty(config.order)) { config.sql += config.order; }
            config.DealWithAlias();
            SqlModel model = new SqlModel() { sql = config.sql, };
            model.AddSpToList(config.sp);
            return ExecuteTable(model);
        }
        public override DataTable SelTop(PageSetting config)
        {
            if (string.IsNullOrEmpty(config.t2))//单表
            {
                config.sql = "SELECT " + config.fields + " FROM " + config.t1 + " LIMIT " + config.psize;
            }
            else
            {
                config.sql = "SELECT " + config.fields + "  FROM {0} A LEFT JOIN {1} B ON {2} LIMIT" + config.psize;
                config.sql = string.Format(config.sql, config.t1, config.t2, config.on);
            }

            if (!string.IsNullOrEmpty(config.where)) { config.sql += " WHERE " + config.where; }
            if (!string.IsNullOrEmpty(config.order)) { config.sql += config.order; }
            config.DealWithAlias();
            return ExecuteTable(new SqlModel() { sql = config.sql });
        }
        //--------------Where模型
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
        //专用于处理date
        public override string GetDateSql(Sql_Where where)
        {
            string datesql = "";
            if (!string.IsNullOrEmpty(where.stime)) { datesql += where.field + "> str_to_date('" + where.stime + "','%Y-%m-%d %k:%i:%s')"; }
            if (!string.IsNullOrEmpty(where.etime))
            {
                if (!string.IsNullOrEmpty(where.stime)) { datesql += " AND "; }
                datesql += where.field + "< str_to_date('" + where.etime + "','%Y-%m-%d %k:%i:%s')";
            }
            datesql = " " + where.join + " (" + datesql + ")";
            return datesql;
        }
        //--------------Model方法(字段,参数,字段=参数)
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
                    str += ":" + strArr[i, 0] + ",";
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
                    str += "" + strArr[i, 0] + "=:" + strArr[i, 0] + ",";
                }
            }
            return str.Substring(0, str.Length - 1);
        }

        public override bool Table_Exist(string name)
        {
            string sql = "SHOW TABLES;";
            DataTable dt = ExecuteTable(new SqlModel() { sql = sql });
            //因为得到的列名是动态显示的(Tables_in_数据库名),所以遍历查询
            foreach (DataRow dr in dt.Rows)
            {
                if (dr[0].Equals(name)) { return true; }
            }
            return false;
        }
        public override bool Table_Add(string table, M_SQL_Field field)
        {
            string sql = "CREATE TABLE " + table + "(" + field.fieldName + " " + field.fieldType + "(" + field.fieldLen + "))";
            ExecuteNonQuery(new SqlModel() { sql = sql });
            return true;
        }
        public override bool Table_Remove(string name)
        {
            string sql = "DROP TABLE " + name;
            ExecuteNonQuery(new SqlModel() { sql = sql });
            return true;
        }
        public override bool Table_Clear(string name)
        {
            string sql = "TRUNCATE TABLE " + name;
            ExecuteNonQuery(new SqlModel() { sql = sql });
            return true;
        }
        public override DataTable Table_List()
        {
            string sql = "SHOW TABLES;";
            return ExecuteTable(new SqlModel() { sql = sql });
        }
        public override bool View_Exist(string name)
        {
            string sql = "SHOW FULL TABLES;";
            DataTable dt = ExecuteTable(new SqlModel() { sql = sql });
            foreach (DataRow dr in dt.Rows)
            {
                if (dr[0].Equals(name) && dr["table_type"].Equals("VIEW")) { return true; }
            }
            return false;
        }
        public override bool View_Drop(string name)
        {
            string sql = "DROP VIEW " + name;
            ExecuteNonQuery(new SqlModel() { sql = sql });
            return true;
        }
        public override bool Field_Add(string table, M_SQL_Field field)
        {
            string sql = "ALTER TABLE " + table + " ADD COLUMN " + field.fieldName + " " + field.fieldType + "(" + field.fieldLen + ") DEFAULT NULL";
            ExecuteNonQuery(new SqlModel() { sql = sql });
            return true;
        }
        public override bool Field_Remove(string table, string field)
        {
            string sql = "ALTER TABLE " + table + " DROP COLUMN " + field;
            ExecuteNonQuery(new SqlModel() { sql = sql });
            return true;
        }
        public override DataTable Field_List(string table)
        {
            string sql = "DESCRIBE " + table;
            return ExecuteTable(new SqlModel() { sql = sql });
        }
        public override bool DB_Create(string dbname)
        {
            string sql = "CREATE DATABASE " + dbname;
            ExecuteNonQuery(new SqlModel() { sql = sql });
            return true;
        }
        public override bool DB_Remove(string dbname)
        {
            string sql = "DROP DATABASE " + dbname;
            ExecuteNonQuery(new SqlModel() { sql = sql });
            return true;
        }
        public override bool DB_Exist(string dbName)
        {
            string sql = "SHOW DATABASES;";
            DataTable dt = ExecuteTable(new SqlModel() { sql = sql });
            return dt.Select("Database='" + dbName + "'").Length > 0;
        }
        public override bool DB_Attach(string mdfSource, string logSource, string dbName)
        {
            throw new NotImplementedException();
        }

    }
}
