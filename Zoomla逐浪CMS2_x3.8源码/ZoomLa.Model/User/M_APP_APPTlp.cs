using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ZoomLa.Model
{
    public class M_APP_APPTlp: M_Base
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public string TlpUrl { get; set; }
        public DateTime CDate { get; set; }
        public string Alias { get; set; }
        public override string PK { get { return "ID"; } }
        public override string TbName { get { return "ZL_APP_APPTlp"; } }
        public override string[,] FieldList()
        {
            string[,] fieldlist = {
                                    {"ID","Int","4"},
                                    { "UserID","Int","4"},
                                    { "TlpUrl","NVarChar","1000"},
                                    { "CDate","DateTime","8"},
                                    { "Alias","NVarChar","500"}
                                 };
            return fieldlist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_APP_APPTlp model = this;
            if (model.CDate <= DateTime.MinValue) { model.CDate = DateTime.Now; }
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.UserID;
            sp[2].Value = model.TlpUrl;
            sp[3].Value = model.CDate;
            sp[4].Value = model.Alias;
            return sp;
        }
        public M_APP_APPTlp GetModelFromReader(SqlDataReader rdr)
        {
            M_APP_APPTlp model = new M_APP_APPTlp();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.UserID = ConvertToInt(rdr["UserID"]);
            model.TlpUrl = ConverToStr(rdr["TlpUrl"]);
            model.CDate = ConvertToDate(rdr["CDate"]);
            model.Alias = ConverToStr(rdr["Alias"]);
            rdr.Close();
            return model;
        }
    }
}
