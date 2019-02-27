using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ZoomLa.Model.Design
{
    public class M_Design_Ask : M_Base
    {
        public int ID { get; set; }
        public string Title { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public int CUser { get; set; }
        public string Remind { get; set; }
        /// <summary>
        /// 问卷类型
        /// </summary>
        public int ZType { get; set; }
        /// <summary>
        /// 问卷状态
        /// </summary>
        public int ZStatus { get; set; }
        public DateTime CDate { get; set; }
        /// <summary>
        /// 终止日期
        /// </summary>
        public DateTime EndDate { get; set; }
        /// <summary>
        /// 预览图用于微信分享
        /// </summary>
        public string PreViewImg { get; set; }
        public M_Design_Ask() { PreViewImg = ""; ZStatus = 0; }
        public override string TbName { get { return "ZL_Design_Ask"; } }
        public override string PK { get { return "ID"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
        		        		{"ID","Int","4"},
        		        		{"Title","NVarChar","200"},
        		        		{"CUser","Int","4"},
        		        		{"Remind","NVarChar","500"},
        		        		{"ZType","Int","4"},
        		        		{"ZStatus","Int","4"},
        		        		{"CDate","DateTime","8"},
                                {"PreViewImg","NVarChar","1000"},
                                {"EndDate","DateTime","8"}
        };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_Design_Ask model = this;
            if (model.CDate <= DateTime.MinValue) { model.CDate = DateTime.Now; }
            if (model.EndDate <= DateTime.MinValue) { model.EndDate = DateTime.Now.AddYears(1); }
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.Title;
            sp[2].Value = model.CUser;
            sp[3].Value = model.Remind;
            sp[4].Value = model.ZType;
            sp[5].Value = model.ZStatus;
            sp[6].Value = model.CDate;
            sp[7].Value = model.PreViewImg;
            sp[8].Value = model.EndDate;
            return sp;
        }
        public M_Design_Ask GetModelFromReader(DbDataReader rdr)
        {
            M_Design_Ask model = new M_Design_Ask();
            model.ID = ConvertToInt(rdr["ID"]);
            model.Title = ConverToStr(rdr["Title"]);
            model.CUser = ConvertToInt(rdr["CUser"]);
            model.Remind = ConverToStr(rdr["Remind"]);
            model.ZType = ConvertToInt(rdr["ZType"]);
            model.ZStatus = ConvertToInt(rdr["ZStatus"]);
            model.CDate = ConvertToDate(rdr["CDate"]);
            model.PreViewImg = ConverToStr(rdr["PreViewImg"]);
            model.EndDate = ConvertToDate(rdr["EndDate"]);
            rdr.Close();
            return model;
        }
    }
}
