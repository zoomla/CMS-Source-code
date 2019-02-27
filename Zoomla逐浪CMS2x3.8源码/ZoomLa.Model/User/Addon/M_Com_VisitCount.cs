using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ZoomLa.Model.User
{
    public class M_Com_VisitCount : M_Base
    {
        public int ID { get; set; }
        /// <summary>
        /// 操作系统
        /// </summary>
        public string OSVersion { get; set; }
        /// <summary>
        /// 访问者的用户ID，若为0则表示是用户未登陆或是游客访问
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// 访问源
        /// </summary>
        public string Source { get; set; }
        /// <summary>
        ///浏览器版本
        /// </summary>
        public string BrowerVersion { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        public DateTime CDate { get; set; }
        /// <summary>
        /// 设备
        /// </summary>
        public string Device { get; set; }
        /// <summary>
        /// 访问者IP
        /// </summary>
        public string IP { get; set; }
        /// <summary>
        /// 访问者地区
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 浏览量
        /// </summary>
        public int DVisitNum { get; set; }
        public string ZType { get; set; }
        /// <summary>
        /// 内容ID,可以是id或Guid,需要通过页面传参
        /// </summary>
        public string InfoID { get; set; }
        public string InfoTitle { get; set; }
        /// <summary>
        /// 内容或其他备注
        /// </summary>
        public string Remind { get; set; }
        public override string TbName { get { return "ZL_Com_VisitCount"; } }
        public override string[,] FieldList()
        {
            string[,] TableList = {
                                  {"ID","Int","4"},
                                  {"UserID","Int","4"},
                                  {"IP","VarChar","500"},
                                  {"OSVersion","NVarChar","500"},
                                  {"Device","NVarChar","500"},
                                  {"Remind","NVarChar","500"},
                                  {"BrowerVersion","NVarChar","500"},
                                  {"Source","NVarChar","500"},
                                  {"Address","NVarChar","500"},
                                  {"DVisitNum","Int","4"},
                                  {"CDate","DateTime","8"},
                                  {"ZType","NVarChar","100"},
                                  {"InfoID","NVarChar","200"},
                                  {"InfoTitle","NVarChar","200"}
                                  };
            return TableList;
        }

        public override SqlParameter[] GetParameters()
        {
            M_Com_VisitCount model = this;
            if (model.CDate <= DateTime.MinValue) { model.CDate = DateTime.Now; }
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.UserID;
            sp[2].Value = model.IP;
            sp[3].Value = model.OSVersion;
            sp[4].Value = model.Device;
            sp[5].Value = model.Remind;
            sp[6].Value = model.BrowerVersion;
            sp[7].Value = model.Source;
            sp[8].Value = model.Address;
            sp[9].Value = model.DVisitNum;
            sp[10].Value = model.CDate;
            sp[11].Value = model.ZType;
            sp[12].Value = model.InfoID;
            sp[13].Value = model.InfoTitle;
            return sp;
        }
        public M_Com_VisitCount GetModelFromReader(SqlDataReader rdr)
        {
            M_Com_VisitCount model = new M_Com_VisitCount();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.UserID = ConvertToInt(rdr["UserID"]);
            model.IP = ConverToStr(rdr["IP"]);
            model.OSVersion = ConverToStr(rdr["OSVersion"]);
            model.Device = ConverToStr(rdr["Device"]);
            model.Remind = ConverToStr(rdr["Remind"]);
            model.BrowerVersion = ConverToStr(rdr["BrowerVersion"]);
            model.Source = ConverToStr(rdr["Source"]);
            model.Address = ConverToStr(rdr["Address"]);
            model.DVisitNum = ConvertToInt(rdr["DVisitNum"]);
            model.CDate = ConvertToDate(rdr["CDate"]);
            model.ZType = ConverToStr(rdr["ZType"]);
            model.InfoID = ConverToStr(rdr["InfoID"]);
            model.InfoTitle = ConverToStr(rdr["InfoTitle"]);
            rdr.Close();
            return model;
        }
        
    }
}
