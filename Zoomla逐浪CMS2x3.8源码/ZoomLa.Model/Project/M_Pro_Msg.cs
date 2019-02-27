using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ZoomLa.Model.Project
{
    public class M_Pro_Msg : M_Base
    {
        public int ID { get; set; }
        /// <summary>
        /// 步骤ID
        /// </summary>
        public int StepID { get; set; }
        /// <summary>
        /// 步骤内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 创建人ID
        /// </summary>
        public int CUser { get; set; }
        /// <summary>
        /// 创建人用户名
        /// </summary>
        public string CUName { get; set; }
        public DateTime CDate { get; set; }
        /// <summary>
        /// 回信的信息ID
        /// </summary>
        public int ReplyMsgID { get; set; }
        /// <summary>
        /// 回复人
        /// </summary>
        public int RCUser { get; set; }
        /// <summary>
        /// 回复人用户名
        /// </summary>
        public string RCUName { get; set; }
        public M_Pro_Msg() 
        {
            CDate = DateTime.Now;
        }
        public override string TbName { get { return "ZL_Pro_Msg"; } }
        public override string PK { get { return "ID"; } }

        public override string[,] FieldList()
        {
            string[,] Tablelist = {
        		        		{"ID","Int","4"},
        		        		{"StepID","Int","4"},
        		        		{"Content","NText","50000"},
        		        		{"CUser","Int","4"},
                                {"CUName","NVarChar","200"},
                                {"CDate","DateTime","8"},
        		        		{"ReplyMsgID","Int","4"},
                                {"RCUser","Int","4"},
                                {"RCUName","NVarChar","200"}
        };
            return Tablelist;
        }

        public override SqlParameter[] GetParameters()
        {
            M_Pro_Msg model=this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.StepID;
            sp[2].Value = model.Content;
            sp[3].Value = model.CUser;
            sp[4].Value = model.CUName;
            sp[5].Value = model.CDate;
            sp[6].Value = model.ReplyMsgID;
            sp[7].Value = model.RCUser;
            sp[8].Value = model.RCUName;
            return sp;
        }
        public M_Pro_Msg GetModelFromReader(SqlDataReader rdr)
        {
            M_Pro_Msg model = new M_Pro_Msg();
            model.ID = ConvertToInt(rdr["ID"]);
            model.StepID = ConvertToInt(rdr["StepID"]);
            model.Content = ConverToStr(rdr["Content"]);
            model.CUser = ConvertToInt(rdr["CUser"]);
            model.CUName = ConverToStr(rdr["CUName"]);
            model.CDate = ConvertToDate(rdr["CDate"]);
            model.ReplyMsgID = ConvertToInt(rdr["ReplyMsgID"]);
            model.RCUser = ConvertToInt(rdr["RCUser"]);
            model.RCUName = ConverToStr(rdr["RCUName"]);
            rdr.Close();
            return model;
        }
    }
}
