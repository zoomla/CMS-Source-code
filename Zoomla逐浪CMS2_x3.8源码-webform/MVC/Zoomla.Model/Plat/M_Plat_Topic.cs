using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ZoomLa.Model.Plat
{
    public class M_Plat_Topic:M_Base
    {
        public M_Plat_Topic() { CDate = DateTime.Now; }
        public int ID { get; set; }
        public string TName { get; set; }
        public int Count { get; set; }
        public DateTime CDate { get; set; }
        /// <summary>
        /// 最后一条信息ID
        /// </summary>
        public int LastMsgID { get; set; }
        public int LastUserID { get; set; }
        public int IsStar { get; set; }
        public int IsSystem { get; set; }
        public int CompID { get; set; }
        public override string TbName { get { return "ZL_Plat_Topic"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
        		        		{"ID","Int","4"},
        		        		{"TName","NVarChar","200"},
        		        		{"Count","Int","4"},
        		        		{"CDate","DateTime","8"},
                                {"LastMsgID","Int","4"},
                                {"LastUserID","Int","4"},
                                {"IsStar","Int","4"},
                                {"IsSystem","Int","4"},
                                {"CompID","Int","4"}
        };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_Plat_Topic model = this;
            if (model.CDate <= DateTime.MinValue) { model.CDate = DateTime.Now; }
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.TName;
            sp[2].Value = model.Count;
            sp[3].Value = model.CDate;
            sp[4].Value = model.LastMsgID;
            sp[5].Value = model.LastUserID;
            sp[6].Value = model.IsStar;
            sp[7].Value = model.IsSystem;
            sp[8].Value = model.CompID;
            return sp;
        }
        public M_Plat_Topic GetModelFromReader(DbDataReader rdr)
        {
            M_Plat_Topic model = new M_Plat_Topic();
            model.ID = Convert.ToInt32(rdr["id"]);
            model.TName = ConverToStr(rdr["TName"]);
            model.Count = ConvertToInt(rdr["Count"]);
            model.CDate = ConvertToDate(rdr["CDate"]);
            model.LastMsgID = ConvertToInt(rdr["LastMsgID"]);
            model.LastUserID = ConvertToInt(rdr["LastUserID"]);
            model.IsStar = ConvertToInt(rdr["IsStar"]);
            model.IsSystem = ConvertToInt(rdr["IsSystem"]);
            model.CompID = ConvertToInt(rdr["CompID"]);
            rdr.Close();
            return model;
        }
    }
}
