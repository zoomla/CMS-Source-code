using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace ZoomLa.Model
{
    public class M_Content_ScheTask:M_Base
    {
        public int ID
        {
            get;
            set;
        }
        public string TaskName
        {
            get;
            set;
        }        
        public string TaskContent
        {
            get;
            set;
        }        
        /// <summary>
        /// 任务类型，详见枚举,根据类型加载不同的任务
        /// </summary>
        public int TaskType
        {
            get;
            set;
        }
        /// <summary>
        /// (disuse)
        /// </summary>
        public int IsLoop { get; set; }
        public int ExecuteType { get; set; }
        /// <summary>
        /// 循环检测时间(30),或日期格式(仅一次),或时间格式(每日定时)
        /// </summary>
        public string ExecuteTime
        {
            get;
            set;
        }
        public int Interval { get; set; }
        /// <summary>
        /// 最近一次执行时间
        /// </summary>
        public string LastTime
        {
            get;
            set;
        }
        /// <summary>
        /// 0:未执行,100任务完结,-1已停用,将不会再进入TaskCenter执行
        /// </summary>
        public int Status
        {
            get;
            set;
        }        
        public string Remind
        {
            get;
            set;
        }
        public DateTime CDate { get; set; }
        public int AdminID { get; set; }
        //----------------------------------------不存储
        /// <summary>
        /// 执行时间,日期格式
        /// </summary>
        public DateTime ExecuteTime2
        {
            get { return Convert.ToDateTime(ExecuteTime); }
        }
        public DateTime LastTime2 { get { return Convert.ToDateTime(LastTime); } }
        /// <summary>
        /// 仅一次,每日定时,每隔多少时间(分),被动,每月的某几日执行
        /// </summary>
        public enum ExecuteTypeEnum
        {
            JustOnce = 0, EveryDay = 1, Interval = 2, Passive = 3, EveryMonth = 4
        }
        /// <summary>
        /// 大分类,下再接字符串进行小分类
        /// </summary>
        public enum TaskTypeEnum : int
        {
            ExecuteSQL = 1, Release = 2, Content = 3
        };
        public override string TbName { get { return "ZL_Content_ScheTask"; } }
        public override string[,] FieldList()
        {
        string[,] Tablelist = {
                                  {"ID","Int","4"},
                                  {"TaskName","NVarChar","100"},
                                  {"TaskContent","NVarChar","4000"},
                                  {"TaskType","Int","4"}, 
                                  {"ExecuteType","Int","4"}, 
                                  {"ExecuteTime","NVarChar","255"}, 
                                  {"LastTime","NVarChar","255"}, 
                                  {"Status","Int","4"}, 
                                  {"Remind","NVarChar","255"},
                                  {"IsLoop","Int","4"} ,
                                  {"Interval","Int","4"} ,
                                  {"CDate","DateTime","8"},
                                  {"AdminID","Int","4"}
                                 };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters() 
        {
            M_Content_ScheTask model = this;
            if (model.CDate <= DateTime.MinValue) { model.CDate = DateTime.Now; }
            SqlParameter[] sp = GetSP(); ;
            sp[0].Value = model.ID;
            sp[1].Value = model.TaskName;
            sp[2].Value = model.TaskContent;
            sp[3].Value = model.TaskType;
            sp[4].Value = model.ExecuteType;
            sp[5].Value = model.ExecuteTime;
            sp[6].Value = model.LastTime;//LastTime
            sp[7].Value = model.Status;
            sp[8].Value = model.Remind;
            sp[9].Value = model.IsLoop;
            sp[10].Value = model.Interval;
            sp[11].Value = model.CDate;
            sp[12].Value = model.AdminID;
            return sp;
        }
        public M_Content_ScheTask GetModelFromReader(SqlDataReader rdr) 
        {
            M_Content_ScheTask model = new M_Content_ScheTask();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.TaskName = ConverToStr(rdr["TaskName"]);
            model.TaskContent = ConverToStr(rdr["TaskContent"]);
            model.TaskType = ConvertToInt(rdr["TaskType"]);
            model.IsLoop = ConvertToInt(rdr["IsLoop"]);
            model.ExecuteType = ConvertToInt(rdr["ExecuteType"]);
            model.ExecuteTime =ConverToStr(rdr["ExecuteTime"]);
            model.LastTime = ConverToStr(rdr["LastTime"]);
            model.Status = ConvertToInt(rdr["Status"]);
            model.Remind = ConverToStr(rdr["Remind"]);
            model.Interval = ConvertToInt(rdr["Interval"]);
            model.CDate = ConvertToDate(rdr["CDate"]);
            model.AdminID = ConvertToInt(rdr["AdminID"]);
            rdr.Close();
            return model;
        }
        public M_Content_ScheTask GetModelFromDR(DataRow rdr)
        {
            M_Content_ScheTask model = new M_Content_ScheTask();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.TaskName = ConverToStr(rdr["TaskName"]);
            model.TaskContent = ConverToStr(rdr["TaskContent"]);
            model.TaskType = ConvertToInt(rdr["TaskType"]);
            model.IsLoop = ConvertToInt(rdr["IsLoop"]);
            model.ExecuteType = ConvertToInt(rdr["ExecuteType"]);
            model.ExecuteTime = ConverToStr(rdr["ExecuteTime"]);
            model.LastTime = ConverToStr(rdr["LastTime"]);
            model.Status = ConvertToInt(rdr["Status"]);
            model.Remind = ConverToStr(rdr["Remind"]);
            model.Interval = ConvertToInt(rdr["Interval"]);
            model.CDate = ConvertToDate(rdr["CDate"]);
            model.AdminID = ConvertToInt(rdr["AdminID"]);
            return model;
        }
    }
}
