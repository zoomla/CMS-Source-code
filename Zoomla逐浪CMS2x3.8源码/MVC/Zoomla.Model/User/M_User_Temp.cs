using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ZoomLa.Model
{
    public class M_User_Temp:M_Base
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public DateTime CDate { get; set; }
        public string Describe { get; set; }
        public string Str1 { get; set; }
        public string Str2 { get; set; }
        public string Str3 { get; set; }
        public string Str4 { get; set; }
        public string Str5 { get; set; }
        public string Str6 { get; set; }
        public int UseType { get; set; }
        public override string TbName { get { return "ZL_Temp"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
        		        		{"ID","Int","4"},
        		        		{"UserID","Int","4"},
        		        		{"CDate","DateTime","8"},
        		        		{"Describe","NVarChar","500"},
        		        		{"Str1","NVarChar","500"},
                                {"Str2","NVarChar","500"},
                                {"Str3","NVarChar","500"},
                                {"Str4","NVarChar","500"},
                                {"Str5","NVarChar","500"},
                                {"Str6","NVarChar","500"},
                                {"UseType","Int","4"}
             };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_User_Temp model = this;
            if (model.CDate <= DateTime.MinValue) { model.CDate = DateTime.Now; }
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.UserID;
            sp[2].Value = model.CDate;
            sp[3].Value = model.Describe;
            sp[4].Value = model.Str1;
            sp[5].Value = model.Str2;
            sp[6].Value = model.Str3;
            sp[7].Value = model.Str4;
            sp[8].Value = model.Str5;
            sp[9].Value = model.Str6;
            sp[10].Value = model.UseType;
            return sp;
        }
        public M_User_Temp GetModelFromReader(SqlDataReader rdr)
        {
            M_User_Temp model = new M_User_Temp();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.UserID = Convert.ToInt32(rdr["UserID"]);
            model.CDate = ConvertToDate(rdr["CDate"]);
            model.Describe = ConverToStr(rdr["Describe"]);
            model.Str1 = ConverToStr(rdr["Str1"]);
            model.Str2 = ConverToStr(rdr["Str2"]);
            model.Str3 = ConverToStr(rdr["Str3"]);
            model.Str4 = ConverToStr(rdr["Str4"]);
            model.Str5 = ConverToStr(rdr["Str5"]);
            model.Str6 = ConverToStr(rdr["Str6"]);
            model.UseType = ConvertToInt(rdr["UseType"]);
            rdr.Close();
            return model;
        }
    }
}
