using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace ZoomLa.Model
{
    public class M_Site_SiteList:M_Base
    {
        /// <summary>
        /// 站群--分配管理员或在CMS中新建站点加入
        /// </summary>		
        public int ID { get; set; }
        /// <summary>
        /// SiteID
        /// </summary>		
        public int SiteID { get; set; }
        /// <summary>
        /// SiteName
        /// </summary>		
        public string SiteName { get; set; }
        /// <summary>
        /// SiteManager
        /// </summary>		
        public string SiteManager { get; set; }
        /// <summary>
        /// Remind
        /// </summary>		
        public string Remind { get; set; }
        /// <summary>
        /// CreateDate
        /// </summary>		
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// 到期关闭时间
        /// </summary>
        public DateTime EndDate { get; set; }
        /// <summary>
        /// CreateMan
        /// </summary>		
        public string CreateMan { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNum { get; set; }

        public override string TbName { get { return "ZL_IDC_SiteList"; } }

        public override string[,] FieldList() 
        {
            string[,] Tablelist = {
                                  {"ID","Int","4"},
                                  {"SiteID","Int","4"},
                                  {"SiteName","NVarChar","50"},
                                  {"SiteManager","NVarChar","50"}, 
                                  {"Remind","NVarChar","50"}, 
                                  {"CreateDate","DateTime","20"}, 
                                  {"EndDate","DateTime","20"}, 
                                  {"CreateMan","NVarChar","50"},
                                  {"OrderNum","NText","5000"}
                                 };
            return Tablelist;
        }

        public override SqlParameter[] GetParameters() 
        {
            M_Site_SiteList model = this;
            if(model.CreateDate <= DateTime.MinValue) { model.CreateDate = DateTime.Now; }
            if(model.EndDate<= DateTime.MinValue) { model.EndDate = DateTime.Now; }
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.SiteID;
            sp[2].Value = model.SiteName;
            sp[3].Value = model.SiteManager;
            sp[4].Value = model.Remind;
            sp[5].Value = model.CreateDate;
            sp[6].Value = model.EndDate;
            sp[7].Value = model.CreateMan;
            sp[8].Value = model.OrderNum;
            return sp;
        }
        public  M_Site_SiteList GetModelFromReader(SqlDataReader rdr) 
        {
            M_Site_SiteList model = new M_Site_SiteList();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.SiteID = ConvertToInt(rdr["SiteID"]);
            model.SiteName = ConverToStr(rdr["SiteName"]);
            model.SiteManager = ConverToStr(rdr["SiteManager"]);
            model.Remind = ConverToStr(rdr["Remind"]);
            model.CreateDate = ConvertToDate(rdr["CreateDate"]);
            model.EndDate = ConvertToDate(rdr["EndDate"]);
            model.CreateMan = ConverToStr(rdr["CreateMan"]);
            model.OrderNum = ConverToStr(rdr["OrderNum"]);
            rdr.Close();
            return model;
        }
    }
}
