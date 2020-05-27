using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient; 

namespace ZoomLa.Model
{
    public class M_Ucenter:M_Base
    {
        public int ID { get; set; }
        /// <summary>
        /// 来源网站
        /// </summary>
        public string WebSite { get; set; }
        /// <summary>
        /// 用户,问答,内容,其他权限字符串
        /// </summary>
        public string UserAuth { get; set; }
        public string AskAuth { get; set; }
        public string ConAuth { get; set; }
        public string OtherAuth { get;set; }
        public DateTime AddTime { get; set; }
        public string DBUName { get; set; }
        public string DBPwd { get; set; }
        public string Key { get; set; }
        /// <summary>
        /// 1:正常,0:禁用
        /// </summary>
        public int Status { get; set; }
        public string Alias { get; set; }
        public string Desc { get; set; }
        public override string TbName { get { return "ZL_Ucenter"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"ID","Int","4"},
                                  {"WebSite","NVarChar","255"},
                                  {"UserAuth","VarChar","1000"},
                                  {"AskAuth","VarChar","1000"},
                                  {"ConAuth","VarChar","1000"}, 
                                  {"OtherAuth","VarChar","1000"}, 
                                  {"AddTime","DateTime","8"},
                                  {"Key","NVarChar","500"},
                                  {"Status","Int","4"},
                                  {"DBUName","NVarChar","200"},
                                  {"DBPwd","NVarChar","200"},
                                  {"Alias","NVarChar","200"},
                                  {"Desc","NVarChar","200"}
                                 };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_Ucenter model = this;
            if (AddTime <= DateTime.MinValue) { AddTime = DateTime.Now; }
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.WebSite;
            sp[2].Value = model.UserAuth;
            sp[3].Value = model.AskAuth;
            sp[4].Value = model.ConAuth;
            sp[5].Value = model.OtherAuth;
            sp[6].Value = model.AddTime;
            sp[7].Value = model.Key;
            sp[8].Value = model.Status;
            sp[9].Value = model.DBUName;
            sp[10].Value = model.DBPwd;
            sp[11].Value = model.Alias;
            sp[12].Value = model.Desc;
            return sp;
        }
        public M_Ucenter GetModelFromReader(SqlDataReader rdr)
        {
            M_Ucenter model = new M_Ucenter();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.WebSite = rdr["WebSite"].ToString();
            model.UserAuth = ConverToStr(rdr["UserAuth"]);
            model.AskAuth = ConverToStr(rdr["AskAuth"]);
            model.ConAuth = ConverToStr(rdr["ConAuth"]);
            model.OtherAuth = ConverToStr(rdr["OtherAuth"]);
            model.AddTime = ConvertToDate(rdr["AddTime"]);
            model.Key = ConverToStr(rdr["Key"]);
            model.Status = ConvertToInt(rdr["Status"]);
            model.DBUName = ConverToStr(rdr["DBUName"]);
            model.DBPwd = ConverToStr(rdr["DBPwd"]);
            model.Alias = ConverToStr(rdr["Alias"]);
            model.Desc = ConverToStr(rdr["Desc"]);
            rdr.Close();
            return model;
        }
    }
}
