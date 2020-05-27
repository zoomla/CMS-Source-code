namespace ZoomLa.Model
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Data.SqlClient;
    using System.Data;

    public class M_DataList : M_Base
    {
        public M_DataList()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        public int ID
        {
            get;
            set;
        }
        public string TableName
        {
            get;
            set;
        }
        public int Type
        {
            get;
            set;
        }
        public string Explain
        {
            get;
            set;
        }
        public string SecShort
        {
            get;
            set;
        }

        public string ThrShort
        {
            get;
            set;
        }
        public override string TbName { get { return "ZL_DataList"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"ID","Int","4"},
                                  {"TableName","NVarChar","50"},
                                  {"Type","Int","4"},
                                  {"Explain","NVarChar","255"},
                                  {"SecShort","NVarChar","2"},
                                  {"ThrShort","NVarChar","3"}
              };
            return Tablelist;
        }
        public SqlParameter[] GetParameters(M_DataList model)
        {
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.TableName;
            sp[2].Value = model.Type;
            sp[3].Value = model.Explain;
            sp[4].Value = model.SecShort;
            sp[5].Value = model.ThrShort;
            return sp;
        }
        public M_DataList GetModelFromReader(SqlDataReader rdr)
        {
            M_DataList model = new M_DataList();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.TableName = rdr["TableName"].ToString();
            model.Type = Convert.ToInt32(rdr["Type"]);
            model.Explain = rdr["Explain"].ToString();
            model.SecShort = rdr["SecShort"].ToString();
            model.ThrShort = rdr["ThrShort"].ToString();
            rdr.Close();
            return model;
        }
    }
}