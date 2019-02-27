using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace ZoomLa.Model.Site
{
    public class M_IDC_DNSTable:M_Base
    {
        public override string TbName { get { return "ZL_IDC_DnsTable"; } }

        public int ID { get; set; }
        public string Domain { get; set; }
        public string IP { get; set; }
        /// <summary>
        /// 邮件域名
        /// </summary>
        public string MX { get; set; }
        /// <summary>
        /// 最大子域名数
        /// </summary>
        public int Max_sub_domain { get; set; }
        /// <summary>
        /// 最大跳转数
        /// </summary>
        public int Max_url_forward { get; set; }
        /// <summary>
        /// 1为启用0为不启用
        /// </summary>
        public int State { get; set; }
        /// <summary>
        /// 站点ID
        /// </summary>
        public int Site_ID { get; set; }
        public int SiteProperty_ID { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public int User_ID { get; set; }
        public int Server_ID { get; set; }
        public int Closed_By { get; set; }
        public int MultiByte { get; set; }
        public int MultiByte_Domain { get; set; }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"ID","Int","4"},
                                  {"Domain","NVarChar","50"},
                                  {"IP","NVarChar","50"},
                                  {"MX","NVarChar","50"}, 
                                  {"Max_sub_domain","NVarChar","50"}, 
                                  {"Max_url_forward","NVarChar","50"},
                                  {"State","Int","4"},
                                  {"Site_ID","Int","4"},
                                  {"SiteProperty_ID","Int","4"},
                                  {"User_ID","Int","4"},
                                  {"Server_ID","Int","4"},
                                  {"Closed_By","Int","4"},
                                  {"MultiByte","Int","4"},
                                  {"MultiByte_Domain","Int","4"}
                                 };
            return Tablelist;
        }
        
        public override SqlParameter[] GetParameters()
        {
            M_IDC_DNSTable model = this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.Domain;
            sp[2].Value = model.IP;
            sp[3].Value = model.MX;
            sp[4].Value = model.Max_sub_domain;
            sp[5].Value = model.Max_url_forward;
            sp[6].Value = model.State;
            sp[7].Value = model.Site_ID;
            sp[8].Value = model.SiteProperty_ID;
            sp[9].Value = model.User_ID;
            sp[10].Value = model.Server_ID;
            sp[11].Value = model.Closed_By;
            sp[12].Value = model.MultiByte;
            sp[13].Value = model.MultiByte_Domain;
            return sp;
        }
        public M_IDC_DNSTable GetModelFromReader(SqlDataReader rdr)
        {
            M_IDC_DNSTable model = new M_IDC_DNSTable();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.Domain = ConverToStr(rdr["Domain"]);
            model.IP = ConverToStr(rdr["IP"]);
            model.MX = ConverToStr(rdr["MX"]);
            model.Max_sub_domain = ConvertToInt(rdr["Max_sub_domain"]);
            model.Max_url_forward = ConvertToInt(rdr["Max_url_forward"]);
            model.State = Convert.ToInt32(rdr["State"]);
            model.Site_ID = ConvertToInt(rdr["Site_ID"]);
            model.SiteProperty_ID = ConvertToInt(rdr["SiteProperty_ID"]);
            model.User_ID = Convert.ToInt32(rdr["User_ID"]);
            model.Server_ID = ConvertToInt(rdr["Server_ID"]);
            model.Closed_By = ConvertToInt(rdr["Closed_By"]);
            model.MultiByte = ConvertToInt(rdr["MultiByte"]);
            model.MultiByte_Domain = ConvertToInt(rdr["MultiByte_Domain"]);
            rdr.Close();
            return model;
        }
    }
}
