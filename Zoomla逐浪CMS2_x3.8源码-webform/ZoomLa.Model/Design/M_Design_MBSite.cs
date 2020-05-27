using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ZoomLa.Model.Design
{
    //用户的手机站点信息
    public class M_Design_MBSite : M_Base
    {
        public int ID { get; set; }
        /// <summary>
        /// 站点所用模板ID
        /// </summary>
        public int TlpID { get; set; }
        /// <summary>
        /// 站点所属用户
        /// </summary>
        public int UserID { get; set; }
        public DateTime CDate { get; set; }
        /// <summary>
        /// 所绑定的PC站点信息,因为信息是与PC站点关联(disuse)
        /// </summary>
        public int SiteID { get; set; }
        public string SiteName { get; set; }
        /// <summary>
        /// 站点预览缩图
        /// </summary>
        public string SiteImg { get; set; }
        /// <summary>
        /// 站点配置,如背景色等,json字符串
        /// </summary>
        public string SiteCfg { get; set; }
        public override string TbName { get { return "ZL_Design_MBSite"; } }
        public override string PK { get { return "ID"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
        		        		{"ID","Int","4"},
        		        		{"TlpID","Int","4"},
        		        		{"UserID","Int","4"},
        		        		{"CDate","DateTime","8"},
                                {"SiteID","Int","4"},
                                {"SiteName","NVarChar","200"},
                                {"SiteImg","NVarChar","1000"},
                                {"SiteCfg","NText","10000"}
        };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_Design_MBSite model = this;
            SqlParameter[] sp = GetSP();
            if (model.CDate <= DateTime.MinValue) { model.CDate = DateTime.Now; }
            sp[0].Value = model.ID;
            sp[1].Value = model.TlpID;
            sp[2].Value = model.UserID;
            sp[3].Value = model.CDate;
            sp[4].Value = model.SiteID;
            sp[5].Value = model.SiteName;
            sp[6].Value = model.SiteImg;
            sp[7].Value = model.SiteCfg;
            return sp;
        }
        public M_Design_MBSite GetModelFromReader(DbDataReader rdr)
        {
            M_Design_MBSite model = new M_Design_MBSite();
            model.ID = ConvertToInt(rdr["ID"]);
            model.TlpID = ConvertToInt(rdr["TlpID"]);
            model.UserID = ConvertToInt(rdr["UserID"]);
            model.CDate = ConvertToDate(rdr["CDate"]);
            model.SiteID = ConvertToInt(rdr["SiteID"]);
            model.SiteName = ConverToStr(rdr["SiteName"]);
            model.SiteImg = ConverToStr(rdr["SiteImg"]);
            model.SiteCfg = ConverToStr(rdr["SiteCfg"]);
            rdr.Close();
            return model;
        }
    }
}
