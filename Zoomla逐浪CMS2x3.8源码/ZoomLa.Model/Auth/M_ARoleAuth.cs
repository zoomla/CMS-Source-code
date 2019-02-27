using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ZoomLa.Model.Auth
{
    public class M_ARoleAuth : M_Base
    {
        public int ID { get; set; }
        public int Rid { get; set; }
        public string model { get; set; }
        public string content { get; set; }
        public string crm { get; set; }
        public string label { get; set; }
        public string shop { get; set; }
        public string store { get; set; }
        public string page { get; set; }
        public string user { get; set; }
        public string other { get; set; }
        public string edu { get; set; }
        public string extend { get; set; }
        public string system { get; set; }
        public string oa { get; set; }
        public string mobile { get; set; }
        public string bar { get; set; }
        public string site { get; set; }
        public string pub { get; set; }
        public override string TbName
        {
            get { return "ZL_ARoleAuth"; }
        }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
        		        		{"ID","Int","4"},
        		        		{"Rid","Int","4"},
        		        		{"model","VarChar","3000"},
        		        		{"content","VarChar","3000"},
        		        		{"crm","VarChar","3000"},
        		        		{"label","VarChar","3000"},
        		        		{"shop","VarChar","3000"},
        		        		{"store","VarChar","3000"},
        		        		{"page","VarChar","3000"},
        		        		{"user","VarChar","3000"},
        		        		{"other","VarChar","3000"},
        		        		{"edu","VarChar","3000"},
        		        		{"extend","VarChar","3000"},
        		        		{"system","VarChar","3000"},
        		        		{"oa","VarChar","3000"},
        		        		{"mobile","VarChar","3000"},
        		        		{"bar","VarChar","3000"},
        		        		{"site","VarChar","3000"},
                                {"pub","VarChar","3000" }
        };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_ARoleAuth model=this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.Rid;
            sp[2].Value = model.model;
            sp[3].Value = model.content;
            sp[4].Value = model.crm;
            sp[5].Value = model.label;
            sp[6].Value = model.shop;
            sp[7].Value = model.store;
            sp[8].Value = model.page;
            sp[9].Value = model.user;
            sp[10].Value = model.other;
            sp[11].Value = model.edu;
            sp[12].Value = model.extend;
            sp[13].Value = model.system;
            sp[14].Value = model.oa;
            sp[15].Value = model.mobile;
            sp[16].Value = model.bar;
            sp[17].Value = model.site;
            sp[18].Value = model.pub;
            return sp;
        }
        public M_ARoleAuth GetModelFromReader(SqlDataReader rdr)
        {
            M_ARoleAuth model = new M_ARoleAuth();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.Rid = ConvertToInt(rdr["Rid"]);
            model.model = ConverToStr(rdr["model"]);
            model.content = ConverToStr(rdr["content"]);
            model.crm = ConverToStr(rdr["crm"]);
            model.label = ConverToStr(rdr["label"]);
            model.shop = ConverToStr(rdr["shop"]);
            model.store = ConverToStr(rdr["store"]);
            model.page = ConverToStr(rdr["page"]);
            model.user = ConverToStr(rdr["user"]);
            model.other = ConverToStr(rdr["other"]);
            model.edu = ConverToStr(rdr["edu"]);
            model.extend = ConverToStr(rdr["extend"]);
            model.system = ConverToStr(rdr["system"]);
            model.oa = ConverToStr(rdr["oa"]);
            model.mobile = ConverToStr(rdr["mobile"]);
            model.bar = ConverToStr(rdr["bar"]);
            model.site = ConverToStr(rdr["site"]);
            model.pub = ConverToStr(rdr["pub"]);
            rdr.Close();
            return model;
        }
    }
}
