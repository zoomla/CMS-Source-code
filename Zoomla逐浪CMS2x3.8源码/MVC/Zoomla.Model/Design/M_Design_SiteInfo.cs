using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ZoomLa.Model.Design
{
    public class M_Design_SiteInfo : M_Base
    {
        public int ID { get; set; }
        /// <summary>
        /// 站点名称
        /// </summary>
        public string SiteName { get; set; }
        /// <summary>
        /// 用户ID,需要支持游客,ID=0的只有管理员可修改
        /// </summary>
        public string UserID { get; set; }
        public string UserName { get; set; }
        /// <summary>
        /// 站点备注
        /// </summary>
        public string Remind { get; set; }
        public int ZStatus { get; set; }
        public DateTime CDate { get; set; }
        public string SiteFlag { get; set; }
        /// <summary>
        /// 绑定的域名ID
        /// </summary>
        public int DomainID { get; set; }
        public string Logo { get; set; }
        /// <summary>
        /// 评分，0-5.0
        /// </summary>
        public double Score { get; set; }

        //--------------------------------No Save
        /// <summary>
        /// 以/结尾的路径
        /// </summary>
        public string SiteDir { get { return "/Site/" + ID + "/"; } }

        public override string TbName { get { return "ZL_Design_SiteInfo"; } }
        public override string PK { get { return "ID"; } }

        public override string[,] FieldList()
        {
            string[,] Tablelist = {
        		        		{"ID","Int","4"},
        		        		{"SiteName","NVarChar","100"},
        		        		{"UserID","NVarChar","500"},
        		        		{"UserName","NVarChar","500"},
        		        		{"Remind","NVarChar","500"},
        		        		{"ZStatus","Int","4"},
        		        		{"CDate","DateTime","8"},
                                {"SiteFlag","NVarChar","100"},
                                {"DomainID","NVarChar","200"},
                                {"Logo","NVarChar","1000"},
                                {"Score","Float","8" }
        };
            return Tablelist;
        }

        public override SqlParameter[] GetParameters()
        {
            M_Design_SiteInfo model = this;
            if (model.CDate <= DateTime.MinValue) { model.CDate = DateTime.Now; }
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.SiteName;
            sp[2].Value = model.UserID;
            sp[3].Value = model.UserName;
            sp[4].Value = model.Remind;
            sp[5].Value = model.ZStatus;
            sp[6].Value = model.CDate;
            sp[7].Value = model.SiteFlag;
            sp[8].Value = model.DomainID;
            sp[9].Value = model.Logo;
            sp[10].Value = model.Score;
            return sp;
        }
        public M_Design_SiteInfo GetModelFromReader(DbDataReader rdr)
        {
            M_Design_SiteInfo model = new M_Design_SiteInfo();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.SiteName = ConverToStr(rdr["SiteName"]);
            model.UserID = ConverToStr(rdr["UserID"]);
            model.UserName = ConverToStr(rdr["UserName"]);
            model.Remind = ConverToStr(rdr["Remind"]);
            model.ZStatus = ConvertToInt(rdr["ZStatus"]);
            model.CDate = ConvertToDate(rdr["CDate"]);
            model.SiteFlag = ConverToStr(rdr["SiteFlag"]);
            model.DomainID = ConvertToInt(rdr["DomainID"]);
            model.Logo = ConverToStr(rdr["Logo"]);
            model.Score = ConverToDouble(rdr["Score"]);
            rdr.Close();
            return model;
        }
        public M_Design_SiteInfo GetModelFromReader(DataRow rdr)
        {
            M_Design_SiteInfo model = new M_Design_SiteInfo();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.SiteName = ConverToStr(rdr["SiteName"]);
            model.UserID = ConverToStr(rdr["UserID"]);
            model.UserName = ConverToStr(rdr["UserName"]);
            model.Remind = ConverToStr(rdr["Remind"]);
            model.ZStatus = ConvertToInt(rdr["ZStatus"]);
            model.CDate = ConvertToDate(rdr["CDate"]);
            model.SiteFlag = ConverToStr(rdr["SiteFlag"]);
            model.DomainID = ConvertToInt(rdr["DomainID"]);
            model.Logo = ConverToStr(rdr["Logo"]);
            model.Score = ConverToDouble(rdr["Score"]);
            return model;
        }
    }
}
