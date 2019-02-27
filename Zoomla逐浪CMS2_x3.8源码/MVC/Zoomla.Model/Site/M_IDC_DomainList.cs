using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ZoomLa.Model.Site
{
    public class M_IDC_DomainList : M_Base
    {
        public int ID { get; set; }
        /// <summary>
        /// 1:后台域名目录,2:Wix指向
        /// </summary>
        public int SType { get; set; }
        /// <summary>
        /// 指向的路径
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 需要路由的域名
        /// </summary>
        public string DomName { get; set; }
        public string UserID { get; set; }
        public string RegInfo { get; set; }
        public string Remind { get; set; }
        public DateTime CDate { get; set; }
        public DateTime EndDate { get; set; }
        /// <summary>
        /// 1:启用,0:禁用
        /// </summary>
        public int MyStatus { get; set; }
        public M_IDC_DomainList() 
        {
            MyStatus = 1;
        }
        public override string TbName { get { return "ZL_IDC_DomainList"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
        		        		{"ID","Int","4"},
                                {"SType","Int","4"},
        		        		{"DomName","NVarChar","500"},
                                {"Url","NVarChar","500"},
        		        		{"UserID","VarChar","50"},
        		        		{"RegInfo","NVarChar","500"},
        		        		{"Remind","NVarChar","50"},
        		        		{"CDate","DateTime","8"},
                                {"EndDate","DateTime","8"},
                                {"MyStatus","Int","4"}
        };
            return Tablelist;
        }
        public SqlParameter[] GetParameters(M_IDC_DomainList model)
        {
            if (model.EndDate <= DateTime.MinValue) { model.EndDate = DateTime.Now.AddYears(3); }
            if (model.CDate <= DateTime.MinValue) { model.CDate = DateTime.Now; }
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.SType;
            sp[2].Value = model.DomName;
            sp[3].Value = model.Url;
            sp[4].Value = model.UserID;
            sp[5].Value = model.RegInfo;
            sp[6].Value = model.Remind;
            sp[7].Value = model.CDate;
            sp[8].Value = model.EndDate;
            sp[9].Value = model.MyStatus;
            return sp;
        }
        public M_IDC_DomainList GetModelFromReader(SqlDataReader rdr)
        {
            M_IDC_DomainList model = new M_IDC_DomainList();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.SType = ConvertToInt(rdr["SType"]);
            model.DomName = ConverToStr(rdr["DomName"]);
            model.Url = ConverToStr(rdr["Url"]);
            model.UserID = ConverToStr(rdr["UserID"]);
            model.RegInfo = ConverToStr(rdr["RegInfo"]);
            model.Remind = ConverToStr(rdr["Remind"]);
            model.CDate = ConvertToDate(rdr["CDate"]);
            model.EndDate = ConvertToDate(rdr["EndDate"]);
            model.MyStatus = ConvertToInt(rdr["MyStatus"]);
            rdr.Close();
            return model;
        }
        public M_IDC_DomainList GetModelFromReader(DataRow rdr)
        {
            M_IDC_DomainList model = new M_IDC_DomainList();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.SType = ConvertToInt(rdr["SType"]);
            model.DomName = ConverToStr(rdr["DomName"]);
            model.Url = ConverToStr(rdr["Url"]);
            model.UserID = ConverToStr(rdr["UserID"]);
            model.RegInfo = ConverToStr(rdr["RegInfo"]);
            model.Remind = ConverToStr(rdr["Remind"]);
            model.CDate = ConvertToDate(rdr["CDate"]);
            model.EndDate = ConvertToDate(rdr["EndDate"]);
            model.MyStatus = ConvertToInt(rdr["MyStatus"]);
            return model;
        }
    }
}
