using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ZoomLa.Model.Other
{
    public class M_WX_RedPacket : M_Base
    {
        public int ID { get; set; }
        /// <summary>
        /// 批次号|匹配号
        /// </summary>
        public string Flow {get;set; }
        public string Remind { get; set; }
        public DateTime CDate { get; set; }
        /// <summary>
        /// 生效日期
        /// </summary>
        public DateTime SDate { get; set; }
        /// <summary>
        /// 结束日期
        /// </summary>
        public DateTime EDate { get; set; }
        /// <summary>
        /// 初始红包数量(实际数量动态计算)
        /// </summary>
        public int RedNum { get; set; }
        public int AdminID { get; set; }
        /// <summary>
        /// 状态开头,暂时不用
        /// </summary>
        public int ZStatus { get; set; }
        /// <summary>
        /// 红包名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 祝福语
        /// </summary>
        public string Wishing { get; set; }
        /// <summary>
        /// 金额范围示例:1 或1-10
        /// </summary>
        public string AmountRange { get; set; }
        /// <summary>
        /// 红包码格式示例:RD{000000AA}
        /// </summary>
        public string CodeFormat { get; set; }
        /// <summary>
        /// 红包所属公众号APPID
        /// </summary>
        public int AppID { get; set; }
        public override string TbName { get { return "ZL_WX_RedPacket"; } }
        public override string PK { get { return "ID"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
        		        		{"ID","Int","4"},
        		        		{"Remind","NVarChar","500"},
        		        		{"CDate","DateTime","8"},
        		        		{"SDate","DateTime","8"},
        		        		{"EDate","DateTime","8"},
        		        		{"RedNum","Int","4"},
        		        		{"AdminID","Int","4"},
        		        		{"ZStatus","Int","4"},
        		        		{"Name","NVarChar","100"},
        		        		{"Wishing","NVarChar","500"},
        		        		{"AmountRange","NVarChar","100"},
        		        		{"CodeFormat","NVarChar","100"},
        		        		{"AppID","Int","4"},
                                {"Flow","NVarChar","100"},
        };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_WX_RedPacket model = this;
            if (model.CDate <= DateTime.MinValue) {model.CDate=DateTime.Now; }
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.Remind;
            sp[2].Value = model.CDate;
            sp[3].Value = model.SDate;
            sp[4].Value = model.EDate;
            sp[5].Value = model.RedNum;
            sp[6].Value = model.AdminID;
            sp[7].Value = model.ZStatus;
            sp[8].Value = model.Name;
            sp[9].Value = model.Wishing;
            sp[10].Value = model.AmountRange;
            sp[11].Value = model.CodeFormat;
            sp[12].Value = model.AppID;
            sp[13].Value=model.Flow;
            return sp;
        }
        public M_WX_RedPacket GetModelFromReader(DbDataReader rdr)
        {
            M_WX_RedPacket model = new M_WX_RedPacket();
            model.ID = ConvertToInt(rdr["ID"]);
            model.Remind = ConverToStr(rdr["Remind"]);
            model.CDate = ConvertToDate(rdr["CDate"]);
            model.SDate = ConvertToDate(rdr["SDate"]);
            model.EDate = ConvertToDate(rdr["EDate"]);
            model.RedNum = ConvertToInt(rdr["RedNum"]);
            model.AdminID = ConvertToInt(rdr["AdminID"]);
            model.ZStatus = ConvertToInt(rdr["ZStatus"]);
            model.Name = ConverToStr(rdr["Name"]);
            model.Wishing = ConverToStr(rdr["Wishing"]);
            model.AmountRange = ConverToStr(rdr["AmountRange"]);
            model.CodeFormat = ConverToStr(rdr["CodeFormat"]);
            model.AppID = ConvertToInt(rdr["AppID"]);
            model.Flow = ConverToStr(rdr["Flow"]);
            rdr.Close();
            return model;
        }
    }
}
