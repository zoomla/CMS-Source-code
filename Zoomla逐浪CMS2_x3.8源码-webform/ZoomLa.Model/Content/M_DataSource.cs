using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace ZoomLa.Model.Content
{
    [Serializable]
    public class M_DataSource : M_Base
    {
        public M_DataSource()
        {
            Status = "1";
        }
        /// <summary>
        /// 连接类型，有些地方直接用了字符串判断，改动大小写需要注意
        /// </summary>
        public static string[] DataSourceType = { "LocalSQL", "External SQL", "MySQL", "Oracle", "Access", "XML", "Excel" };
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 数据源名称
        /// </summary>
        public string DSName { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnectionString { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateMan { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Remind { get; set; }
        public override string TbName { get { return "ZL_DataSource"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"ID","Int","4"},
                                  {"DSName","VarChar","20"},
                                  {"Type","NVarChar","20"},
                                  {"ConnectionString","NVarChar","100"},
                                  {"CreateTime","DateTime","10"},
                                  {"CreateMan","NVarChar","20"},
                                  {"Status","Char","10"},
                                  {"Remind","NVarChar","100"}
                                 };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()//用于插入数据,将模型作为参数传入
        {
            M_DataSource model = this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.DSName;
            sp[2].Value = model.Type;
            sp[3].Value = model.ConnectionString;
            sp[4].Value = model.CreateTime;
            sp[5].Value = model.CreateMan;
            sp[6].Value = model.Status;
            sp[7].Value = model.Remind;
            return sp;
        }

        public M_DataSource GetModelFromReader(SqlDataReader rdr)
        {
            M_DataSource model = new M_DataSource();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.DSName = ConverToStr(rdr["DSName"]);
            model.Type = ConverToStr(rdr["Type"]);
            model.ConnectionString = ConverToStr(rdr["ConnectionString"]);
            model.CreateTime = ConvertToDate(rdr["CreateTime"]);
            model.CreateMan = ConverToStr(rdr["CreateMan"]);
            model.Status = ConverToStr(rdr["Status"]);
            model.Remind = ConverToStr(rdr["Remind"]);
            rdr.Close();
            rdr.Dispose();
            return model;
        }

        public bool IsXml
        {
            get
            {
                if (string.IsNullOrEmpty(this.Type)) return false;
                return M_DataSource.DataSourceType[Convert.ToInt32(this.Type)] == M_DataSource.DataSourceType[5];
            }
        }
        /// <summary>
        /// Column_Name,返回Xml列名字符串
        /// </summary>
        /// <returns></returns>
        //public DataTable GetXmlColumns(string path) 
        //{
        //    DataTable dt = new DataTable();//存列名
        //    dt.Columns.Add("Column_Name", typeof(string));
        //    if (!IsXml) { return dt; }
        //    DataSet ds = new DataSet();
        //    ds.ReadXml(path);
        //    foreach (DataColumn c in ds.Tables[0].Columns)
        //    {
        //        DataRow dr = dt.NewRow();
        //        dr["Column_Name"] = c.ColumnName;
        //        dt.Rows.Add(dr);
        //    }
        //    return dt;
        //}
        /// <summary>
        /// 根据表名
        /// </summary>
        public DataTable GetXmlColumns(string path, string tableName)
        {
            DataTable dt = new DataTable();//存列名
            dt.Columns.Add("Column_Name", typeof(string));
            if (!IsXml) { return dt; }
            DataSet ds = new DataSet();
            ds.ReadXml(path);
            foreach (DataColumn c in ds.Tables[tableName].Columns)
            {
                DataRow dr = dt.NewRow();
                dr["Column_Name"] = c.ColumnName;
                dt.Rows.Add(dr);
            }
            return dt;
        }

        /// <summary>
        /// Column_Name,返回Xml列名字符串
        /// </summary>
        /// <returns></returns>
        public DataTable GetXmlColumns(string path)
        {
            DataTable dt = new DataTable();//存列名
            dt.Columns.Add("Column_Name", typeof(string));
            if (!IsXml) { return dt; }
            foreach (DataColumn c in MergeDataTable(path).Columns)
            {
                DataRow dr = dt.NewRow();
                dr["Column_Name"] = c.ColumnName;
                dt.Rows.Add(dr);
            }
            return dt;
        }

        /// <summary>
        /// 将XML中的表融合,属性与父节下父节点等支持，支持简单表
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public DataTable MergeDataTable(string path)
        {

            DataSet ds = new DataSet();
            try
            {
                ds.ReadXml(path);
                if (!ds.Tables[0].Columns.Contains("Table_Id")) { return ds.Tables[0]; }//如果是简单表，直接返回
                for (int b = 1; b < ds.Tables.Count; b++)//从表数
                {
                    foreach (DataColumn dc in ds.Tables[b].Columns)
                    {
                        if (!ds.Tables[0].Columns.Contains(dc.ColumnName))//如果不存在该列，则加上
                        {
                            ds.Tables[0].Columns.Add(dc.ColumnName, typeof(string));
                        }
                    }
                    //列创建完成，开始将数据移入,根据Table_ID取对应的值
                    for (int r = 0; r < ds.Tables[0].Rows.Count; r++)//主表行
                    {
                        string id = ds.Tables[0].Rows[r]["Table_Id"].ToString();
                        if (!string.IsNullOrEmpty(id))
                        {
                            ds.Tables[b].DefaultView.RowFilter = "Table_Id =" + id;
                            for (int c = 0; c < ds.Tables[b].DefaultView.ToTable().Columns.Count; c++)
                            {
                                string columnName = ds.Tables[b].DefaultView.ToTable().Columns[c].ColumnName;
                                if (columnName == "Table_Id") continue;
                                try  //避免行中无数据报错
                                {
                                    ds.Tables[0].Rows[r][columnName] = ds.Tables[b].DefaultView.ToTable().Rows[0][columnName];
                                }
                                catch { }
                            }
                        }
                    }
                }
            }
            catch
            {
                return new DataTable(); //如果数据文件被删除，或其他原因，返回一个空的数据表
            }
            return ds.Tables[0];
        }
    }//M_DataSource End;
}
