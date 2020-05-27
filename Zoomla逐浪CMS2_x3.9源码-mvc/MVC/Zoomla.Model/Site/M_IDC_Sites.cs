using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ZoomLa.Model.Site
{
    /*
     * 用于子站内容,功能不同于M_IDC_SiteList(子站与主站内容同步)
     */ 
    public class M_IDC_Sites:M_Base
    {
        public int ID {get;set; }
        /// <summary>
        /// 站点名
        /// </summary>
        public string SiteName { get; set; }
        public string SiteKey { get; set; }
        public string Nodes { get; set; }
        /// <summary>
        /// 站点Url 示例：http://demo.z01.com
        /// </summary>
        public string SiteUrl { get; set; }

        public string SiteDesc { get; set; }
        /// <summary>
        /// 最近一次更新时间
        /// </summary>
        public DateTime LastTime { get; set; }

        public DateTime CreateTime { get; set; }

        public int Status { get; set; }
        /// <summary>
        /// 创建人ID
        /// </summary>
        public int AdminID { get; set; }

        public M_IDC_Sites() { }

        public override string TbName { get { return "ZL_IDC_Sites"; } }
        public override string[,] FieldList()
        {
            string[,] TableList ={
                {"ID","Int","4"},
                {"SiteName","NVarChar","50"},
                {"SiteKey","NVarChar","50"},
                {"Nodes","NVarChar","200"},
                {"SiteUrl","NVarChar","200"},
                {"SiteDesc","NVarChar","100"},
                {"LastTime","DateTime","8"},
                {"CreateTime","DateTime","8"},
                {"Status","Int","4"},
                {"AdminID","Int","4"}
            };
            return TableList;
        }
        public override SqlParameter[] GetParameters()
        {
            M_IDC_Sites Model = this;
            if(Model.LastTime <= DateTime.MinValue) { Model.LastTime = DateTime.Now; }
            if(Model.CreateTime <= DateTime.MinValue) { Model.CreateTime = DateTime.Now; }
            SqlParameter[] sp = GetSP();
            sp[0].Value = Model.ID;
            sp[1].Value = Model.SiteName;
            sp[2].Value = Model.SiteKey;
            sp[3].Value = Model.Nodes;
            sp[4].Value = Model.SiteUrl;
            sp[5].Value = Model.SiteDesc;
            sp[6].Value = Model.LastTime;
            sp[7].Value = Model.CreateTime;
            sp[8].Value = Model.Status;
            sp[9].Value = Model.AdminID;
            return sp;
        }

        public M_IDC_Sites GetModelFromReader(SqlDataReader rdr)
        {
            M_IDC_Sites model = new M_IDC_Sites();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.SiteName = ConverToStr(rdr["SiteName"]);
            model.SiteKey = ConverToStr(rdr["SiteKey"]);
            model.Nodes = ConverToStr(rdr["Nodes"]);
            model.SiteUrl = ConverToStr(rdr["SiteUrl"]);
            model.SiteDesc = ConverToStr(rdr["SiteDesc"]);
            model.LastTime = ConvertToDate(rdr["LastTime"]);
            model.CreateTime = ConvertToDate(rdr["CreateTime"]);
            model.Status = Convert.ToInt32(rdr["Status"]);
            model.AdminID = Convert.ToInt32(rdr["AdminID"]);
            rdr.Close();
            return model;
        }

    }

}
