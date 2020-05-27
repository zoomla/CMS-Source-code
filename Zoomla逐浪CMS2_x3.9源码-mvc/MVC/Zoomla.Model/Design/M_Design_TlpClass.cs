using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ZoomLa.Model.Design
{
    public class M_Design_TlpClass : M_Base
    {

        public int ID { get; set; }
        public string Name { get; set; }
        public int Pid { get; set; }
        /// <summary>
        /// 深度,插入时计算
        /// </summary>
        public int Depth { get; set; }
        public int OrderID { get; set; }
        public int AdminID { get; set; }
        public string Remind { get; set; }
        public DateTime CDate { get; set; }

        public override string TbName { get { return "ZL_Design_TlpClass"; } }
        public override string PK { get { return "ID"; } }

        public override string[,] FieldList()
        {
            string[,] Tablelist = {
        		        		{"ID","Int","4"},
        		        		{"Name","NVarChar","100"},
        		        		{"Pid","Int","4"},
        		        		{"OrderID","Int","4"},
        		        		{"AdminID","Int","4"},
        		        		{"Remind","NVarChar","500"},
        		        		{"CDate","DateTime","8"},
                                {"Depth","Int","4"}
        };
            return Tablelist;
        }

        public override SqlParameter[] GetParameters()
        {
            M_Design_TlpClass model = this;
            if (CDate <= DateTime.MinValue) { CDate = DateTime.Now; }
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.Name;
            sp[2].Value = model.Pid;
            sp[3].Value = model.OrderID;
            sp[4].Value = model.AdminID;
            sp[5].Value = model.Remind;
            sp[6].Value = model.CDate;
            sp[7].Value = model.Depth;
            return sp;
        }
        public M_Design_TlpClass GetModelFromReader(DbDataReader rdr)
        {
            M_Design_TlpClass model = new M_Design_TlpClass();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.Name = ConverToStr(rdr["Name"]);
            model.Pid = ConvertToInt(rdr["Pid"]);
            model.OrderID = ConvertToInt(rdr["OrderID"]);
            model.AdminID = ConvertToInt(rdr["AdminID"]);
            model.Remind = ConverToStr(rdr["Remind"]);
            model.CDate = ConvertToDate(rdr["CDate"]);
            model.Depth = ConvertToInt(rdr["Depth"]);
            rdr.Close();
            return model;
        }
    }
}
