using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OracleClient;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ZoomLa.Model;

namespace ZoomLa.SQLDAL.SQL
{
    public class OracleHelper : SqlBase
    {
        //4.0以后不再提供对Oracle的支持,根据需要可替换为ODP.NET
        private OracleConnection GetConnection(SqlModel model)
        {
            OracleConnection conn = new OracleConnection(ConnectionString);
            try
            {
                conn.Open(); return conn;
            }
            catch (Exception ex) { conn.Dispose(); ExceptionDeal(ex, "打开Oracle连接失败"); }
            return null;
        }
        private void PreCommand(OracleCommand command, List<SqlParameter> spList)
        {
            //oracle的参数不能多于命令中声明的参数
            if (spList == null || spList.Count < 1) { return; }
            foreach (SqlParameter sp in spList)
            {
                if (sp == null) { continue; }
                if (sp.Value == DBNull.Value || sp.Value == null) { sp.Value = ""; }
                OracleParameter mysp = new OracleParameter();
                mysp.ParameterName = sp.ParameterName.Replace("@", ":");//必须有此句
                mysp.Value = sp.Value;
                command.Parameters.Add(mysp);
            }
        }
        //--------------
        public override string DBType
        {
            get { return "oracle"; }
        }
        public override string GetSQL(SqlModel model)
        {
            if (!string.IsNullOrEmpty(model.sql)) { return model.sql; }
            if (!string.IsNullOrEmpty(model.where)) { model.where = model.where.Replace("@", ":"); }
            string sql = "";
            GetWhereSql(model);
            switch (model.cmd)
            {
                case SqlModel.SqlCmd.Insert:
                    //先查询出自动加的值,再将其交与上层使用(必须使用自动增长,但导出的SQL语句如何处理)
                    //insert into table1(field1,field2) values(value1,value2)
                    sql = "INSERT INTO " + model.tbName + " ({0}) VALUES({1})";
                    //model.fields = model.fields.Split(',')[0];
                    //model.values = model.values.Split(',')[0];
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
                    model.set = SqlToOracle(model.set);
                    sql = "UPDATE " + model.tbName + " SET " + model.set + " ";
                    //foreach (SqlParam param in model.paramList)
                    //{
                    //    param.value = ReplaceParamPreFix(param.value);
                    //    sql += param.key + "=" + param.value + ",";
                    //}
                    //sql = sql.TrimEnd(',');
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
            string sql = GetSQL(model);
            OracleConnection conn = GetConnection(model);
            try
            {
                OracleCommand command = new OracleCommand(sql, conn);
                PreCommand(command, model.spList);
                command.ExecuteNonQuery(); 
            }
            catch (Exception ex) { ExceptionDeal(ex, sql, model.spList); }
            finally { conn.Dispose(); }
        }
        public override object ExecuteScala(SqlModel model)
        {
            string sql = GetSQL(model);
            OracleConnection conn = GetConnection(model);
            try
            {
                OracleCommand command = new OracleCommand(sql, conn);
                PreCommand(command, model.spList);
                return command.ExecuteScalar();
            }
            catch (Exception ex) { ExceptionDeal(ex, sql, model.spList); return 0; }
            finally { conn.Dispose(); }
        }
        public override DataTable ExecuteTable(SqlModel model)
        {
            DataTable result = new DataTable();
            OracleConnection conn = GetConnection(model);
            string sql = GetSQL(model);
            try
            {
                OracleCommand command = new OracleCommand(sql, conn);
                PreCommand(command, model.spList);
                result.Load(command.ExecuteReader());
            }
            catch (Exception ex) { ExceptionDeal(ex, sql, model.spList); }
            finally { conn.Dispose(); }
            return result;
        }
        public override DbDataReader ExecuteReader(SqlModel model)
        {
            string sql = GetSQL(model);
            OracleConnection conn = GetConnection(model);
            try
            {
                OracleCommand command = new OracleCommand(sql, conn);
                PreCommand(command, model.spList);
                return command.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex) { ExceptionDeal(ex, sql, model.spList); conn.Dispose(); return null; }
        }
        public override int InsertID(SqlModel model)
        {
            //Oracle是否要考虑为附加式,即ID不自动增长,而是手动生成后再插入??(减小脚本,查询准确)
            ExecuteNonQuery(model);
            if (string.IsNullOrEmpty(model.pk)) { model.pk = "ID"; }
            return DataConvert.CLng(ExecuteScala(new SqlModel() { sql = "SELECT MAX(" + model.pk + ") FROM " + model.tbName }));
        }
        //--------------
        public override DataTable SelPage(PageSetting config)
        {
            SqlToOracle(config);
            config.sql = "SELECT T.*,ROWNUM FROM ({0}) T WHERE ROWNUM>" + (config.cpage - 1) * config.psize + " AND ROWNUM<=" + config.cpage * config.psize;
            string innersql = "";
            if (!string.IsNullOrEmpty(config.t2))//双表查询
            {
                innersql = "SELECT " + config.fields + " FROM " + config.t1 + " A LEFT JOIN " + config.t2 + " B ON " + config.on;
                config.countsql = "SELECT COUNT(*) FROM " + config.t1 + " A LEFT JOIN " + config.t2 + " B ON " + config.on;
            }
            else//单表查询
            {
                innersql = "SELECT " + config.fields + " FROM " + config.t1 + " A ";
                config.countsql = "SELECT COUNT(*) FROM " + config.t1;
            }
            if (!string.IsNullOrEmpty(config.where)) { innersql += " WHERE " + config.where; config.countsql += " WHERE " + config.where; }
            if (!string.IsNullOrEmpty(config.order)) { innersql += config.order; }
            config.sql = string.Format(config.sql, innersql);
            config.DealWithAlias();
            //------------------
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
            SqlToOracle(config);
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
            SqlToOracle(config);
            config.sql = "SELECT T.*,ROWNUM FROM ({0}) T WHERE  ROWNUM<=" + config.psize;
            string innersql = "";
            if (string.IsNullOrEmpty(config.t2))//单表
            {
                innersql = "SELECT " + config.fields + " FROM " + config.t1 + " A ";
            }
            else
            {
                innersql = "SELECT " + config.fields + " FROM " + config.t1 + " A LEFT JOIN " + config.t2 + " B ON " + config.on;
            }
            if (!string.IsNullOrEmpty(config.where)) { innersql += " WHERE " + config.where; }
            if (!string.IsNullOrEmpty(config.order)) { innersql += config.order; }
            config.sql = string.Format(config.sql, innersql);
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
            //SELECT * FROM ZL_USER WHERE (LASTLOGINTIME > to_date('2016-02-17 00:00:00','yyyy-mm-dd hh24:mi:ss') and LASTLOGINTIME <to_date('2016-02-18 23:59:59','yyyy-mm-dd hh24:mi:ss'))
            string datesql = "";
            if (!string.IsNullOrEmpty(where.stime)) { datesql += where.field + "> to_date('" + where.stime + "','yyyy-mm-dd hh24:mi:ss')"; }
            if (!string.IsNullOrEmpty(where.etime))
            {
                if (!string.IsNullOrEmpty(where.stime)) { datesql += " AND "; }
                datesql += where.field + "< to_date('" + where.etime + "','yyyy-mm-dd hh24:mi:ss')";
            }
            datesql = " " + where.join +" (" + datesql + ")";
            return datesql;
        }
        //--------------Model方法
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
        //------------私有
        /// <summary>
        /// 将SQL语法转为Oracle,用于set,where等地方
        /// </summary>
        private void SqlToOracle(PageSetting setting)
        {
            setting.where = SqlToOracle(setting.where);
        }
        private string SqlToOracle(string sql)
        {
            if (string.IsNullOrEmpty(sql)) { return sql; }
            sql = sql.Replace("@", ":");
            //sql = sql.Replace("+","||"); //可能会有加法运算存在
            return sql;
        }
        //--------------------------------------------------------
        //扩展
        public override bool Table_Exist(string name)
        {
            List<SqlParameter> splist = new List<SqlParameter>() { new SqlParameter("name", name.ToUpper()) };
            string sql = "SELECT COUNT(*) AS TableCount FROM user_tables WHERE table_name = :name";
            DataTable dt = ExecuteTable(new SqlModel() { sql = sql, spList = splist });
            return DataConvert.CLng(dt.Rows[0]["TableCount"]) > 0;
        }
        public override bool Table_Add(string table,M_SQL_Field field)
        {
            string sql = "CREATE TABLE " + table + " ("+field.fieldName+" "+field.fieldType+" ("+field.fieldLen+") "+field.defval+")";
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
            //获取当前用户所有表
            string sql = "SELECT A.TABLE_NAME,B.COMMENTS  FROM user_tables a,user_tab_comments b  WHERE a.TABLE_NAME=b.TABLE_NAME ORDER BY TABLE_NAME";
            DataTable dt= ExecuteTable(new SqlModel() { sql = sql });
            dt.Columns["TABLE_NAME"].ColumnName = "Name";
            return dt;
        }
        public override bool Field_Add(string tbname, M_SQL_Field field)
        {
            string sql = "ALTER TABLE " + tbname + " ADD (" + field.fieldName + " " + field.fieldType + " (" + field.fieldLen + ") " + field.defval + ")";
            ExecuteNonQuery(new SqlModel() { sql = sql });
            return true;
        }
        public override bool Field_Remove(string table, string field)
        {
            string sql = "ALTER TABLE " + table + " DROP (" + field + ")";
            ExecuteNonQuery(new SqlModel() { sql = sql });
            return true;
        }
        public override DataTable Field_List(string table)
        {
            List<SqlParameter> splist = new List<SqlParameter>() { new SqlParameter("tbname", table.ToUpper()) };
            string sql = "SELECT COLUMN_NAME,DATA_TYPE,DATA_LENGTH,DATA_DEFAULT FROM user_tab_columns WHERE TABLE_NAME=:tbname";
            DataTable dt = ExecuteTable(new SqlModel() { sql = sql, spList = splist });
            dt.Columns["COLUMN_NAME"].ColumnName = "Name";
            return dt;
        }
        public override bool View_Exist(string name)
        {
            List<SqlParameter> splist = new List<SqlParameter>() { new SqlParameter("viewname", name) };
            string sql = "SELECT COUNT(1) AS viewExist FROM user_views WHERE VIEW_NAME=:viewname";
            DataTable dt = ExecuteTable(new SqlModel() { sql = sql, spList = splist });
            return DataConvert.CLng(dt.Rows[0]["viewExist"]) > 0;
        }
        public override bool View_Drop(string name)
        {
            string sql = "DROP VIEW " + name;
            ExecuteNonQuery(new SqlModel() { sql = sql });
            return true;
        }
        public override bool DB_Exist(string dbName)
        {
            throw new NotImplementedException();
        }
        public override bool DB_Create(string dbname)
        {
            throw new NotImplementedException();
        }
        public override bool DB_Remove(string dbname)
        {
            throw new NotImplementedException();
        }
        public override bool DB_Attach(string mdfSource, string logSource, string dbName)
        {
            throw new NotImplementedException();
        }
    }
}
