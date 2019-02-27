using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;

namespace ZoomLa.SQLDAL.SQL
{
    /*
     *1,注意表名$sheet1,OleDB.SelectTables(path).Rows[0]["Table_Name"] 
     *2,不支持连接查询
     *3,分页查询,必须有一列ID来作为主键(且不可重复) 
     */
    public class ExcelHelper : SqlBase
    {       
        //--------------Tools
        private string VToP(string vpath)
        {
            if (vpath.Contains(":"))
                return vpath;
            else
                return (AppDomain.CurrentDomain.BaseDirectory + vpath.Replace("/", "\\")).Replace(@"\\", "\\");
        }
        private OleDbConnection GetConnection()
        {
            string path = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + VToP(this.ConnectionString) + ";" + "Extended Properties=Excel 8.0;";
            OleDbConnection conn = new OleDbConnection(path);
            try
            {
                conn.Open(); return conn;
            }
            catch (Exception ex) { conn.Dispose(); ExceptionDeal(ex, "打开" + this.ConnectionString + ":" + DBType + "连接失败"); }
            return null;
        }
        public override string DBType { get { return "excel"; } }
        public override string GetSQL(SqlModel model)
        {
            if (!string.IsNullOrEmpty(model.sql)) { return model.sql; }
            string sql = "";
            GetWhereSql(model);
            switch (model.cmd)
            {
                case SqlModel.SqlCmd.Insert:
                    //string sql = string.Format("insert into [Sheet1$] values('{0}','{1}','{2}')", "123", "226", "775");
                    throw new Exception("Insert Limited");
                case SqlModel.SqlCmd.Delete:
                    throw new Exception("Delete Limited");
                case SqlModel.SqlCmd.Update:
                    throw new Exception("UPDATE Limited");
                case SqlModel.SqlCmd.Select:
                    //SELECT TOP 2 * FROM [Sheet1$] WHERE ID>1 ORDER BY ID DESC
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
            throw new NotImplementedException();
        }
        public override DataTable ExecuteTable(SqlModel model)
        {
            OleDbDataAdapter command = new OleDbDataAdapter();
            OleDbConnection conn = GetConnection();
            string sql = GetSQL(model);
            try
            {
                DataTable dt = new DataTable();
                command = new OleDbDataAdapter(sql,conn);
                command.Fill(dt);
                return dt;
            }
            catch (Exception ex) { ExceptionDeal(ex, "查询失败"); return null; }
            finally
            {
                conn.Dispose();
                command.Dispose();
            }
        }

        public override DbDataReader ExecuteReader(SqlModel model)
        {
            string sql = GetSQL(model);
            OleDbConnection conn = GetConnection();
            OleDbCommand command = new OleDbCommand(sql, conn);
            try
            {
                //PreCommand(command, model.spList);
                DbDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                return reader;
            }
            catch (Exception ex) { conn.Dispose(); ExceptionDeal(ex, sql, model.spList); return null; }
        }

        public override object ExecuteScala(SqlModel model)
        {
            OleDbDataAdapter command = new OleDbDataAdapter();
            string sql = GetSQL(model);
            try
            {
                DataTable dt = new DataTable();
                command = new OleDbDataAdapter(sql, GetConnection());
                command.Fill(dt);
                if (dt.Rows.Count > 0) { return dt.Rows[0][0]; }
                else { return ""; }
            }
            catch (Exception ex) { ExceptionDeal(ex, "查询失败"); return null; }
            finally
            {
                command.Dispose();
            }
        }

        public override int InsertID(SqlModel model)
        {
            throw new NotImplementedException();
        }

        public override DataTable SelPage(PageSetting config)
        {
            //excel需要加判断为0时不生成子查询语句
            config.sql = "SELECT TOP " + config.psize + " * FROM " + config.t1 + " WHERE ";
            if (config.cursize > 0)
            {
                config.sql += config.pk + " Not In (Select Top " + config.cursize + " " + config.pk + " FROM " + config.t1 + " WHERE " + config.where + config.order + ") ";
            }
            else { config.sql += " 1=1 "; }
            config.sql += " AND " + config.where + config.order;
            DataTable dt = ExecuteTable(new SqlModel(config.sql, null));
            config.itemCount = dt.Rows.Count;
            config.pageCount = GetPageCount(config.itemCount, config.psize);
            return dt;
        }

        public override DataTable JoinQuery(PageSetting config)
        {
            throw new NotImplementedException();
        }

        public override DataTable SelTop(PageSetting config)
        {
            config.sql = "SELECT TOP " + config.psize + " * FROM " + config.t1;
            if (!string.IsNullOrEmpty(config.where)) { config.sql += " WHERE " + config.where; }
            if (!string.IsNullOrEmpty(config.order)) { config.sql += config.order; }
            return ExecuteTable(new SqlModel() { sql = config.sql });
        }
        public override string GetWhereSql(SqlModel model)
        {
            throw new NotImplementedException();
        }

        public override string GetDateSql(Sql_Where where)
        {
            throw new NotImplementedException();
        }

        public override string GetFields(Model.M_Base model)
        {
            throw new NotImplementedException();
        }

        public override string GetParams(Model.M_Base model)
        {
            throw new NotImplementedException();
        }

        public override string GetFieldAndParam(Model.M_Base model)
        {
            throw new NotImplementedException();
        }

        public override bool Table_Exist(string name)
        {
            DataTable dt = Table_List();
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["name"].ToString().Equals(name)) { return true; }
            }
            return false;
        }

        public override bool Table_Add(string table, M_SQL_Field field)
        {
            throw new NotImplementedException();
        }

        public override bool Table_Remove(string name)
        {
            throw new NotImplementedException();
        }

        public override bool Table_Clear(string name)
        {
            throw new NotImplementedException();
        }

        public override DataTable Table_List()
        {
            OleDbConnection conn = GetConnection();
            DataTable dt = new DataTable();
            try
            {
                dt = conn.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, null);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["Table_Name"] = "[" + dt.Rows[i]["Table_Name"] + "]";
                }
                dt.Columns["Table_Name"].ColumnName = "name";
                return dt;
            }
            catch { return null; }
            finally { conn.Close(); }
        }

        public override bool View_Exist(string name)
        {
            throw new NotImplementedException();
        }

        public override bool View_Drop(string name)
        {
            throw new NotImplementedException();
        }

        public override bool Field_Add(string table, M_SQL_Field field)
        {
            throw new NotImplementedException();
        }

        public override bool Field_Remove(string table, string field)
        {
            throw new NotImplementedException();
        }

        public override DataTable Field_List(string table)
        {
            DataTable fieldDT = new DataTable();
            DataTable dt = ExecuteTable(new SqlModel("SELECT * FROM " + table + " WHERE 1=2", null));
            fieldDT.Columns.Add(new DataColumn("name", typeof(string)));
            foreach (DataColumn dc in dt.Columns)
            {
                DataRow dr = fieldDT.NewRow();
                dr["name"] = dc.ColumnName;
                fieldDT.Rows.Add(dr);
            }
            return fieldDT;
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
