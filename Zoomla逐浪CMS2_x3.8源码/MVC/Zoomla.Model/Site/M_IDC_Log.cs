using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ZoomLa.Model
{
    /*
     * 同时作为计划任务的日志页
     */
    public class M_IDC_Log : M_Base
    {
        public int ID { get; set; }
        /// <summary>
        /// 创建该条目的用户,无则为0
        /// </summary>
        public int OwnUserID { get; set; }
        /// <summary>
        /// 创建该条目的管理员,无则为0
        /// </summary>
        public int OwnAdminID { get; set; }
        /// <summary>
        /// 对应的站点ID,无则为0
        /// </summary>
        public int SiteID { get; set; }
        public int LogType { get; set; }
        public string LogTypeStr { get; set; }
        /// <summary>
        /// 日志主体
        /// </summary>
        public string Remind { get; set; }
        public DateTime CreateDate { get; set; }
        public M_IDC_Log() { }

        public override string TbName { get { return "ZL_IDC_Log"; } }
        public override string[,] FieldList()
        {
            string[,] TableList ={
                {"ID","Int","4"},
                {"OwnUserID","Int","4"},
                {"OwnAdminID","Int","4"},
                {"SiteID","Int","4"},
                {"Type","Int","4"},
                {"LogTypeStr","NVarChar","50"},
                {"Remind","NVarChar","500"},
                {"CreateDate","DateTime","8"}
            };
            return TableList;
        }
        public override SqlParameter[] GetParameters()
        {
            M_IDC_Log Model = this;
            if(Model.CreateDate <= DateTime.MinValue) { Model.CreateDate = DateTime.Now; }
            SqlParameter[] sp = GetSP();
            sp[0].Value = Model.ID;
            sp[1].Value = Model.OwnUserID;
            sp[2].Value = Model.OwnAdminID;
            sp[3].Value = Model.SiteID;
            sp[4].Value = Model.LogType;
            sp[5].Value = Model.LogTypeStr;
            sp[6].Value = Model.Remind ?? "";
            sp[7].Value = Model.CreateDate;
            return sp;
        }
        public M_IDC_Log GetModelFromReader(SqlDataReader rdr)
        {
            M_IDC_Log model = new M_IDC_Log();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.OwnUserID = ConvertToInt(rdr["OwnUserID"]);
            model.OwnAdminID = ConvertToInt(rdr["OwnAdminID"]);
            model.SiteID = ConvertToInt(rdr["SiteID"]);
            model.LogType = ConvertToInt(rdr["Type"]);
            model.LogTypeStr = ConverToStr(rdr["LogTypeStr"]);
            model.Remind = ConverToStr(rdr["Remind"]);
            model.CreateDate = ConvertToDate(rdr["CreateDate"]);
            rdr.Close();
            return model;
        }
    }
}
