using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ZoomLa.Model.Other
{
    public class M_WX_RedDetail : M_Base
    {
        public int ID { get; set; }
        /// <summary>
        /// 所属红包
        /// </summary>
        public int MainID { get; set; }
        /// <summary>
        /// 红包码
        /// </summary>
        public string RedCode { get; set; }
        /// <summary>
        /// 红包金额
        /// </summary>
        public double Amount { get; set; }
        /// <summary>
        /// 1:正常,99:已使用,-1:已有人领取,但未成功,0:禁用
        /// </summary>
        public int ZStatus { get; set; }
        /// <summary>
        /// 领取时间
        /// </summary>
        public string UseTime { get; set; }
        /// <summary>
        /// 领取备注
        /// </summary>
        public string Remind { get; set; }
        /// <summary>
        /// 领取人ID
        /// </summary>
        public int UserID { get; set; }
        public string UserName { get; set; }
        public DateTime CDate { get; set; }
        public override string TbName { get { return "ZL_WX_RedDetail"; } }
        public override string PK { get { return "ID"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
        		        		{"ID","Int","4"},
        		        		{"MainID","Int","4"},
        		        		{"RedCode","NVarChar","100"},
        		        		{"Amount","Decimal","8"},
        		        		{"ZStatus","Int","4"},
        		        		{"UseTime","NVarChar","500"},
        		        		{"Remind","NVarChar","500"},
        		        		{"UserID","Int","4"},
        		        		{"UserName","NVarChar","500"},
        		        		{"CDate","DateTime","8"}
        };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_WX_RedDetail model = this;
            if (model.CDate <= DateTime.MinValue) {model.CDate=DateTime.Now; }
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.MainID;
            sp[2].Value = model.RedCode;
            sp[3].Value = model.Amount;
            sp[4].Value = model.ZStatus;
            sp[5].Value = model.UseTime;
            sp[6].Value = model.Remind;
            sp[7].Value = model.UserID;
            sp[8].Value = model.UserName;
            sp[9].Value = model.CDate;
            return sp;
        }
        public M_WX_RedDetail GetModelFromReader(DbDataReader rdr)
        {
            M_WX_RedDetail model = new M_WX_RedDetail();
            model.ID = ConvertToInt(rdr["ID"]);
            model.MainID = ConvertToInt(rdr["MainID"]);
            model.RedCode = ConverToStr(rdr["RedCode"]);
            model.Amount = ConverToDouble(rdr["Amount"]);
            model.ZStatus = ConvertToInt(rdr["ZStatus"]);
            model.UseTime = ConverToStr(rdr["UseTime"]);
            model.Remind = ConverToStr(rdr["Remind"]);
            model.UserID = ConvertToInt(rdr["UserID"]);
            model.UserName = ConverToStr(rdr["UserName"]);
            model.CDate = ConvertToDate(rdr["CDate"]);
            rdr.Close();
            return model;
        }
    }
}
