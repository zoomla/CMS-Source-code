using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ZoomLa.Model.Site
{
    public class M_IDC_DBList:M_Base
    {

        public int ID { get; set; }
        /// <summary>
        /// 所属站点ID
        /// </summary>
        public int SiteID { get; set; }
        /// <summary>
        /// 站点名称
        /// </summary>
        public string SiteName { get; set; }
        /// <summary>
        /// 数据库名
        /// </summary>
        public string DBName { get; set; }
        /// <summary>
        /// 数据库用户名
        /// </summary>
        public string DBUser { get; set; }
        /// <summary>
        /// 数据库初始密码
        /// </summary>
        public string DBInitPwd { get; set; }
        /// <summary>
        /// 所属用户ID
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// 所属用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remind { get; set; }
        /// <summary>
        /// 数据库状态
        /// </summary>
        public int Status { get; set; }
        public M_IDC_DBList()
        {

        }

        public override string TbName { get { return "ZL_IDC_DBList"; } }

        public override string[,] FieldList()
        {
            string[,] Tablelist = {
        	            {"ID","Int","4"},            
                        {"SiteID","Int","4"},            
                        {"SiteName","NVarChar","50"},            
                        {"DBName","NVarChar","50"},            
                        {"DBUser","NVarChar","50"},            
                        {"DBInitPwd","NVarChar","50"},            
                        {"UserID","Int","4"},            
                        {"UserName","NVarChar","50"},            
                        {"CreateTime","DateTime","8"},            
                        {"Remind","NVarChar","50"},            
                        {"Status","Int","4"}            
              
        };
            return Tablelist;
        }
        public SqlParameter[] GetParameters(M_IDC_DBList model)
        {
            string[,] strArr = FieldList();
            if(model.CreateTime <= DateTime.MinValue) { model.CreateTime = DateTime.Now; }
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.SiteID;
            sp[2].Value = model.SiteName;
            sp[3].Value = model.DBName;
            sp[4].Value = model.DBUser;
            sp[5].Value = model.DBInitPwd;
            sp[6].Value = model.UserID;
            sp[7].Value = model.UserName;
            sp[8].Value = model.CreateTime;
            sp[9].Value = model.Remind;
            sp[10].Value = model.Status;
            return sp;
        }
        public M_IDC_DBList GetModelFromReader(SqlDataReader rdr)
        {
            M_IDC_DBList model = new M_IDC_DBList();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.SiteID = ConvertToInt(rdr["SiteID"]);
            model.SiteName = ConverToStr(rdr["SiteName"]);
            model.DBName = ConverToStr(rdr["DBName"]);
            model.DBUser = ConverToStr(rdr["DBUser"]);
            model.DBInitPwd = ConverToStr(rdr["DBInitPwd"]);
            model.UserID = Convert.ToInt32(rdr["UserID"]);
            model.UserName = ConverToStr(rdr["UserName"]);
            model.CreateTime = ConvertToDate(rdr["CreateTime"]);
            model.Remind = ConverToStr(rdr["Remind"]);
            model.Status = Convert.ToInt32(rdr["Status"]);
            rdr.Close();
            return model;
        }
    }
}
