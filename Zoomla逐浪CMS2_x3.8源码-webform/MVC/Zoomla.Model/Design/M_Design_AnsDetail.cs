using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ZoomLa.Model.Design
{
    public class M_Design_AnsDetail : M_Base
    {
        public int ID { get; set; }
        /// <summary>
        /// 对应问卷
        /// </summary>
        public int AskID { get; set; }
        /// <summary>
        /// 所属回答
        /// </summary>
        public int AnsID { get; set; }
        /// <summary>
        /// 对应问题
        /// </summary>
        public int Qid { get; set; }
        /// <summary>
        /// 回答
        /// </summary>
        public string Answer { get; set; }
        /// <summary>
        /// 提交人
        /// </summary>
        public int UserID { get; set; }
        public DateTime CDate { get; set; }
        public override string TbName { get { return "ZL_Design_AnsDetail"; } }
        public override string PK { get { return "ID"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
        		        		{"ID","Int","4"},
        		        		{"AskID","Int","4"},
        		        		{"AnsID","Int","4"},
        		        		{"Qid","Int","4"},
        		        		{"Answer","NVarChar","4000"},
        		        		{"UserID","Int","4"},
        		        		{"CDate","DateTime","8"}
        };
            return Tablelist;
        }

        public override SqlParameter[] GetParameters()
        {
            M_Design_AnsDetail model = this;
            if (model.CDate <= DateTime.MinValue) { model.CDate = DateTime.Now; }
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.AskID;
            sp[2].Value = model.AnsID;
            sp[3].Value = model.Qid;
            sp[4].Value = model.Answer;
            sp[5].Value = model.UserID;
            sp[6].Value = model.CDate;
            return sp;
        }
        public M_Design_AnsDetail GetModelFromReader(DbDataReader rdr)
        {
            M_Design_AnsDetail model = new M_Design_AnsDetail();
            model.ID = ConvertToInt(rdr["ID"]);
            model.AskID = ConvertToInt(rdr["AskID"]);
            model.AnsID = ConvertToInt(rdr["AnsID"]);
            model.Qid = ConvertToInt(rdr["Qid"]);
            model.Answer = ConverToStr(rdr["Answer"]);
            model.UserID = ConvertToInt(rdr["UserID"]);
            model.CDate = ConvertToDate(rdr["CDate"]);
            rdr.Close();
            return model;
        }
    }
}
