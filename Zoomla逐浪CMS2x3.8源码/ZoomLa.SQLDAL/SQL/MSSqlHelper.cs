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
    public class MSSqlHelper : SqlBase
    {
        //--------------Tools
        private SqlConnection GetConnection(SqlModel model)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            try
            {
                conn.Open(); return conn;
            }
            catch (Exception ex) { conn.Dispose(); ExceptionDeal(ex, "打开MSSQL连接失败"); }
            return null;
        }
        private void PreCommand(SqlCommand command, List<SqlParameter> spList)
        {
            if (spList == null || spList.Count < 1) { return; }
            foreach (SqlParameter sp in spList)
            {
                if (sp == null) continue;
                if (sp.Value == DBNull.Value || sp.Value == null) { sp.Value = ""; }
                command.Parameters.Add(sp);
            }
        }
        //--------------基础
        public override string DBType
        {
            get { return "mssql"; }
        }
        /// <summary>
        /// 根据参数,返回对应格式的sql语句
        /// </summary>
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
                    //string fields = "", values = "";
                    //foreach (SqlParam param in model.paramList)
                    //{
                    //    fields += param.key + ",";
                    //    values += param.value + ",";
                    //}
                    //fields = fields.TrimEnd(',');
                    //values = values.TrimEnd(',');
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
                    sql = "UPDATE " + model.tbName + " SET "+model.set+" ";
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
            using (SqlConnection conn = GetConnection(model))
            {
                SqlCommand command = new SqlCommand(sql, conn);
                try
                {
                    PreCommand(command, model.spList);
                    command.ExecuteNonQuery();
                }
                catch (Exception ex) { ExceptionDeal(ex, sql, model.spList); }
                finally { conn.Dispose(); }
            }
        }
        /// <summary>
        /// 用于执行Insert,返回ID
        /// </summary>
        public override object ExecuteScala(SqlModel model)
        {
            string sql = GetSQL(model);
            using (SqlConnection conn = GetConnection(model))
            {
                try
                {
                    SqlCommand command = new SqlCommand(sql, conn);
                    PreCommand(command, model.spList);
                    return command.ExecuteScalar();
                }
                catch (Exception ex) { conn.Dispose(); ExceptionDeal(ex, sql, model.spList.ToArray()); return 0; }
            }
        }
        public override DataTable ExecuteTable(SqlModel model)
        {
            DataTable result = new DataTable();
            using (SqlConnection conn = GetConnection(model))
            {
                SqlCommand command = new SqlCommand(GetSQL(model), conn);
                PreCommand(command, model.spList);
                try
                {
                    //result.Load(command.ExecuteReader());
                    //return result;
                    //性能优于直接加载result返回
                    SqlDataAdapter adapter = new SqlDataAdapter();
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
                catch (Exception ex) { ExceptionDeal(ex, command.CommandText, model.spList); return null; }
                finally { conn.Dispose();  }
            }
        }
        public override DbDataReader ExecuteReader(SqlModel model)
        {
            string sql = GetSQL(model);
            SqlConnection conn = GetConnection(model);
            SqlCommand command = new SqlCommand(sql, conn);
            try
            {
                PreCommand(command, model.spList);
                DbDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                return reader;
            }
            catch (Exception ex) { conn.Dispose(); ExceptionDeal(ex, sql, model.spList); return null; }
        }
        public override int InsertID(SqlModel model)
        {
            return DataConvert.CLng(ExecuteScala(model));
        }
        //--------------分页与连结
        public override DataTable SelPage(PageSetting config)
        {
            if (config.pageMethod.Equals("row")) { return SelPageByRow(config); }
            if (!string.IsNullOrEmpty(config.t2))
            {
                config.sql = "SELECT TOP " + config.psize + config.fields + " FROM " + config.t1 + " A  "+config.join+" " + config.t2 + " B ON " + config.on + " WHERE " + config.pk + " Not In(SELECT TOP " + config.cursize + " " + config.pk + " FROM " + config.t1 + " A LEFT JOIN " + config.t2 + " B ON " + config.on + " WHERE " + config.where + config.order + ") AND " + config.where + config.order;
                config.countsql = "SELECT COUNT(*) FROM " + config.t1 + " A LEFT JOIN " + config.t2 + " B ON " + config.on + " WHERE " + config.where;
            }
            else
            {
                config.sql = "SELECT TOP " + config.psize + config.fields + " FROM " + config.t1 + " A WHERE " + config.pk + " Not In(Select Top " + config.cursize + " " + config.pk + " FROM " + config.t1 + " WHERE " + config.where + config.order + ") And " + config.where + config.order;
                config.countsql = "SELECT COUNT(*) FROM " + config.t1 + " WHERE " + config.where;
            }
            config.DealWithAlias();
            config.itemCount = DataConvert.CLng(SqlHelper.ExecuteScalar(CommandType.Text, config.countsql, config.sp));
            config.pageCount = GetPageCount(config.itemCount, config.psize);
            if (config.debug) { throw new Exception(config.sql); }
            return SqlHelper.ExecuteTable(this.ConnectionString, CommandType.Text, config.sql, config.sp);
        }
        public DataTable SelPageByRow(PageSetting config)
        {
            //select top 10 * from (select row_number() over(order by UserID) as rownumber,* from ZL_User where UserID>1) Awhere rownumber > 40
            if (string.IsNullOrEmpty(config.order)) { throw new Exception("行号分页,必须输入排序条件"); }
            int preSize = config.psize * (config.cpage - 1); if (preSize < 0) { preSize = 0; }
            string orderField = config.order.Replace("A.", "").Replace("B.", "");//排序
            string alias = string.IsNullOrEmpty(config.T1Alias) ? config.t1 : config.T1Alias;//用于判断标签
            //不需要再用Order条件
            if (!string.IsNullOrEmpty(config.t2) && !string.IsNullOrEmpty(config.t1))//双表
            {
                config.sql = "SELECT TOP " + config.psize + " * FROM (SELECT ROW_Number() OVER (" + config.order + ") RowNum," + config.fields + " FROM " + config.t1 + " A " + config.join + " " + config.t2 + " B ON " + config.on + " WHERE " + config.where + ") " + alias + " WHERE RowNum>" + preSize;
                config.countsql = "SELECT COUNT(*) FROM " + config.t1 + " A " + config.join + " " + config.t2 + " B ON " + config.on + " WHERE " + config.where;
            }
            else
            {
                config.sql = "SELECT TOP " + config.psize + " * FROM (SELECT ROW_Number() OVER(" + config.order + ") RowNum," + config.fields + " FROM " + config.t1 + " WHERE " + config.where + ") " + alias + " WHERE RowNum>" + preSize;
                config.countsql = "SELECT COUNT(*) From " + config.t1 + " WHERE " + config.where;
            }
            config.DealWithAlias();
            config.itemCount = DataConvert.CLng(ExecuteScala(new SqlModel(config.countsql, config.sp)));
            config.pageCount = GetPageCount(config.itemCount, config.psize);
            if (config.debug) { throw new Exception(config.sql); }
            return SqlHelper.ExecuteTable(this.ConnectionString,CommandType.Text,config.sql);
        }
        public override DataTable JoinQuery(PageSetting config)
        {
            return SqlHelper.JoinQuery(config.fields, config.t1, config.t2, config.on, config.where, config.order, config.sp);
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
            if (!string.IsNullOrEmpty(where.stime)) { datesql += where.field + ">'" + where.stime+"'"; }
            if (!string.IsNullOrEmpty(where.etime))
            {
                if (!string.IsNullOrEmpty(where.stime)) { datesql += " AND "; }
                datesql += where.field + "<'" + where.etime + "'";
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
                    str += "[" + strArr[i, 0] + "],";
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
            return DBHelper.Table_IsExist(new M_SQL_Connection() { constr = ConnectionString, tbname = name });
        }
        public override bool Table_Add(string table, M_SQL_Field field)
        {
            string sql = "CREATE TABLE "+table+" ({0})";
            string fieldstr = field.fieldName+" ";
            switch (field.fieldType.ToLower())
            {
                case "int":
                case "money":
                case "ntext":
                case "bit":
                case "datetime":
                    fieldstr += field.fieldType;
                    break;
                default:
                    fieldstr += field.fieldType + "(" + field.fieldLen + ") ";
                    break;
            }
            if (!string.IsNullOrEmpty(field.defval)) { fieldstr += " DEFAULT ('" + field.defval + "')"; }
            return SqlHelper.ExecuteSql(string.Format(sql, fieldstr));
        }

        public override bool Table_Remove(string name)
        {
            if (Table_Exist(name))
            {
                string sql = "DROP TABLE " + name;
                SqlHelper.ExecuteSql(sql);
                return true;
            }
            return false;
        }

        public override bool Table_Clear(string name)
        {
            string sql = "TRUNCATE TABLE "+name;
            return SqlHelper.ExecuteSql(sql);
        }

        public override DataTable Table_List()
        {
            return DBHelper.Table_List(ConnectionString);
        }

        public override bool View_Exist(string name)
        {
            return DBHelper.View_IsExist(name);
        }

        public override bool View_Drop(string name)
        {
            string sql = "DROP VIEW "+name;
            return SqlHelper.ExecuteSql(sql);
        }

        public override bool Field_Add(string table, M_SQL_Field field)
        {
            DBHelper.Table_AddField(new M_SQL_Connection() {constr=ConnectionString,tbname=table }, field);
            return true;
        }

        public override bool Field_Remove(string table, string field)
        {
            string sql = "ALTER TABLE "+table+" DROP COLUMN "+field;
            return SqlHelper.ExecuteSql(sql);
        }
        public override DataTable Field_List(string table)
        {
            string sql = "SELECT TABLE_CATALOG,TABLE_SCHEMA,COLUMN_NAME AS Name,DATA_TYPE,CHARACTER_MAXIMUM_LENGTH FROM INFORMATION_SCHEMA.columns WHERE TABLE_NAME='" + table + "'";
            DataTable dt = ExecuteTable(new SqlModel() { sql = sql });
            return dt;
        }
        public override bool DB_Create(string dbname)
        {
            DBHelper.DB_Create(ConnectionString, dbname);
            return true;
        }

        public override bool DB_Remove(string dbname)
        {
            DBHelper.DB_Remove(ConnectionString, dbname);
            return true;
        }

        public override bool DB_Exist(string dbName)
        {
            return DBHelper.DB_Exist(ConnectionString, dbName);
        }

        public override bool DB_Attach(string mdfSource, string logSource, string dbName)
        {
            DBHelper.DB_Attach(mdfSource, logSource, dbName);
            return true;
        }
    }
}
