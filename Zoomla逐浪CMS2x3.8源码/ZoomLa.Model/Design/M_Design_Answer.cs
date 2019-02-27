using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ZoomLa.Model.Design
{
    public class M_Design_Answer : M_Base
    {
        public int ID { get; set; }
        /// <summary>
        /// 所回复的问卷
        /// </summary>
        public int AskID { get; set; }
        /// <summary>
        /// 用复回复,json类型
        /// </summary>
        public string Answer { get; set; }
        public string Remind { get; set; }
        /// <summary>
        /// 回复状态
        /// </summary>
        public int ZType { get; set; }
        /// <summary>
        /// 回复状态
        /// </summary>
        public int ZStatus { get; set; }
        /// <summary>
        /// 用户,支持游客
        /// </summary>
        public int UserID { get; set; }
        public DateTime CDate { get; set; }
        /// <summary>
        /// 来源IP
        /// </summary>
        public string IP { get; set; }
        /// <summary>
        /// 来源渠道,如微信|PC
        /// </summary>
        public string Source { get; set; }
        public override string TbName { get { return "ZL_Design_Answer"; } }
        public override string PK { get { return "ID"; } }
        public M_Design_Answer() { ZStatus = 0; ZType = 0; Remind = ""; }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
        		        		{"ID","Int","4"},
        		        		{"AskID","Int","4"},
        		        		{"Answer","NText","6000"},
        		        		{"Remind","NVarChar","500"},
        		        		{"ZType","Int","4"},
        		        		{"ZStatus","Int","4"},
        		        		{"UserID","Int","4"},
        		        		{"CDate","DateTime","8"},
                                {"IP","NVarChar","200"},
                                {"Source","NVarChar","500"}
        };
            return Tablelist;
        }

        public override SqlParameter[] GetParameters()
        {
            M_Design_Answer model = this;
            if (model.CDate <= DateTime.MinValue) { CDate = DateTime.Now; }
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.AskID;
            sp[2].Value = model.Answer;
            sp[3].Value = model.Remind;
            sp[4].Value = model.ZType;
            sp[5].Value = model.ZStatus;
            sp[6].Value = model.UserID;
            sp[7].Value = model.CDate;
            sp[8].Value = model.IP;
            sp[9].Value = model.Source;
            return sp;
        }
        public M_Design_Answer GetModelFromReader(DbDataReader rdr)
        {
            M_Design_Answer model = new M_Design_Answer();
            model.ID = ConvertToInt(rdr["ID"]);
            model.AskID = ConvertToInt(rdr["AskID"]);
            model.Answer = ConverToStr(rdr["Answer"]);
            model.Remind = ConverToStr(rdr["Remind"]);
            model.ZType = ConvertToInt(rdr["ZType"]);
            model.ZStatus = ConvertToInt(rdr["ZStatus"]);
            model.UserID = ConvertToInt(rdr["UserID"]);
            model.CDate = ConvertToDate(rdr["CDate"]);
            model.IP = ConverToStr(rdr["IP"]);
            model.Source = ConverToStr(rdr["Source"]);
            rdr.Close();
            return model;
        }
    }
}
