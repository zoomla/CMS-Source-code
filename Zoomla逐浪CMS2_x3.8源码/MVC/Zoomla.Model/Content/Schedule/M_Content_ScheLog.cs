using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ZoomLa.Model.Content
{
    public class M_Content_ScheLog : M_Base
    {
        public int ID { get; set; }
        public string TaskName { get; set; }
        public int TaskID { get; set; }
        public int Result { get; set; }
        public string ErrMsg { get; set; }
        public DateTime CDate { get; set; }

        public override string TbName { get { return "ZL_Content_ScheLog"; } }
        public override string PK { get { return "ID"; } }

        public override string[,] FieldList()
        {
            string[,] Tablelist = {
        		        		{"ID","Int","4"},
        		        		{"TaskName","NVarChar","100"},
        		        		{"TaskID","Int","4"},
        		        		{"Result","Int","4"},
        		        		{"ErrMsg","NText","6000"},
        		        		{"CDate","DateTime","8"}
        };
            return Tablelist;
        }

        public override SqlParameter[] GetParameters()
        {
            M_Content_ScheLog model = this;
            if (model.CDate <= DateTime.MinValue) { model.CDate = DateTime.Now; }
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.TaskName;
            sp[2].Value = model.TaskID;
            sp[3].Value = model.Result;
            sp[4].Value = model.ErrMsg;
            sp[5].Value = model.CDate;
            return sp;
        }
        public M_Content_ScheLog GetModelFromReader(DbDataReader rdr)
        {
            M_Content_ScheLog model = new M_Content_ScheLog();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.TaskName = ConverToStr(rdr["TaskName"]);
            model.TaskID = ConvertToInt(rdr["TaskID"]);
            model.Result = ConvertToInt(rdr["Result"]);
            model.ErrMsg = ConverToStr(rdr["ErrMsg"]);
            model.CDate = ConvertToDate(rdr["CDate"]);
            rdr.Close();
            return model;
        }
    }
}
