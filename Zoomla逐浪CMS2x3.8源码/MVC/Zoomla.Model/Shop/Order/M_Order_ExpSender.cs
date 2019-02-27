using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ZoomLa.Model.Shop.Order
{
    public class M_Order_ExpSender : M_Base
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string CompName { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }
        public int AdminID { get; set; }
        public DateTime CDate { get; set; }
        public string Remind { get; set; }
        public int IsDefault { get; set; }
        public override string TbName { get { return "ZL_Order_ExpSender"; } }
        public override string PK { get { return "ID"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                {"ID","Int","4"},
                                {"Name","NVarChar","100"},
                                {"CompName","NVarChar","100"},
                                {"Address","NVarChar","500"},
                                {"Mobile","NVarChar","100"},
                                {"AdminID","Int","4"},
                                {"CDate","DateTime","8"},
                                {"Remind","NVarChar","500"},
                                {"IsDefault","Int","4"}
        };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_Order_ExpSender model = this;
            if (model.CDate <= DateTime.MinValue) { model.CDate = DateTime.Now; }
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.Name;
            sp[2].Value = model.CompName;
            sp[3].Value = model.Address;
            sp[4].Value = model.Mobile;
            sp[5].Value = model.AdminID;
            sp[6].Value = model.CDate;
            sp[7].Value = model.Remind;
            sp[8].Value = model.IsDefault;
            return sp;
        }
        public M_Order_ExpSender GetModelFromReader(DbDataReader rdr)
        {
            M_Order_ExpSender model = new M_Order_ExpSender();
            model.ID = ConvertToInt(rdr["ID"]);
            model.Name = ConverToStr(rdr["Name"]);
            model.CompName = ConverToStr(rdr["CompName"]);
            model.Address = ConverToStr(rdr["Address"]);
            model.Mobile = ConverToStr(rdr["Mobile"]);
            model.AdminID = ConvertToInt(rdr["AdminID"]);
            model.CDate = ConvertToDate(rdr["CDate"]);
            model.Remind = ConverToStr(rdr["Remind"]);
            model.IsDefault = ConvertToInt(rdr["IsDefault"]);
            rdr.Close();
            return model;
        }

    }
}
