using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ZoomLa.Model.Project
{
    public class M_Pro_Type : M_Base
    {
        public int ID { get; set; }
        public string TName { get; set; }
        public string Remind { get; set; }
        public override string TbName { get { return "ZL_Pro_Type"; } }
        public override string PK { get { return "ID"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                {"ID","Int","4" },
                                {"TName","NVarChar","50" },
                                {"Remind","NVarChar","200" }
            };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_Pro_Type model = this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.TName;
            sp[2].Value = model.Remind;
            return sp;
        }
        public M_Pro_Type GetModelFromReader(DbDataReader rdr)
        {
            M_Pro_Type model = new M_Pro_Type();
            model.ID = ConvertToInt(rdr["ID"]);
            model.TName = ConverToStr(rdr["TName"]);
            model.Remind = ConverToStr(rdr["Remind"]);
            rdr.Close();
            return model;
        }
    }
}
