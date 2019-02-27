using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ZoomLa.Model.Plat
{
    public class M_Plat_Task : M_Base
    {
        public int ID { get; set; }
        public string TaskName { get; set; }
        public string TaskContent { get; set; }
        /// <summary>
        /// 任务类型
        /// </summary>
        public int TaskType { get; set; }
        /// <summary>
        /// 任务开始时间,预留，暂时不用
        /// </summary>
        public DateTime BeginTime { get; set; }
        /// <summary>
        /// 任务结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 任务相关文件
        /// </summary>
        public string Attach { get; set; }

        private string _leaderIDS, _partTakeIDS;
        /// <summary>
        /// 负责人UserIDS
        /// </summary>
        public string LeaderIDS
        {
            get
            {
                if (!string.IsNullOrEmpty(_leaderIDS))
                {
                    _leaderIDS = "," + _leaderIDS.Trim(',') + ",";
                }
                return _leaderIDS;
            }
            set { _leaderIDS = value; }
        }
        /// <summary>
        ///参与人IDS
        /// </summary>
        public string PartTakeIDS
        {
            get
            {
                if (!string.IsNullOrEmpty(_partTakeIDS))
                {
                    _partTakeIDS = "," + _partTakeIDS.Trim(',') + ",";
                }
                return _partTakeIDS;
            }
            set { _partTakeIDS = value; }
        }
        /// <summary>
        /// 任务色彩,直接存颜色值
        /// </summary>
        public string Color { get; set; }
        public int Status { get; set; }
        public string Remind { get; set; }
        public int CreateUser { get; set; }
        public string CreateUName { get; set; }
        public DateTime CreateTime { get; set; }
        public int CompID { get; set; }
        public M_Plat_Task()
        {

        }
        public override string TbName { get { return "ZL_Plat_Task"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
        		        		{"ID","Int","4"},
        		        		{"TaskName","NVarChar","50"},
        		        		{"TaskContent","NText","2000"},
                                {"TaskType","Int","4"},
        		        		{"BeginTime","DateTime","8"},
        		        		{"EndTime","DateTime","8"},
        		        		{"LeaderIDS","NText","16"},
        		        		{"PartTakeIDS","NText","16"},
        		        		{"Color","NVarChar","50"},
        		        		{"Status","Int","4"},
        		        		{"Remind","NVarChar","200"},
        		        		{"CreateUser","Int","4"},
        		        		{"CreateUName","NVarChar","50"},
        		        		{"CreateTime","DateTime","8"},
                                {"Attach","NVarChar","500"},
                                {"CompID","Int","4"}
        };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_Plat_Task model=this;
            SqlParameter[] sp = GetSP();
            if (model.CreateTime <= DateTime.MinValue) { model.CreateTime = DateTime.Now; }
            if (model.BeginTime <= DateTime.MinValue) { model.BeginTime = DateTime.Now; }
            sp[0].Value = model.ID;
            sp[1].Value = model.TaskName;
            sp[2].Value = model.TaskContent;
            sp[3].Value = model.TaskType;
            sp[4].Value = model.BeginTime;
            sp[5].Value = model.EndTime;
            sp[6].Value = model.LeaderIDS;
            sp[7].Value = model.PartTakeIDS;
            sp[8].Value = model.Color;
            sp[9].Value = model.Status;
            sp[10].Value = model.Remind;
            sp[11].Value = model.CreateUser;
            sp[12].Value = model.CreateUName;
            sp[13].Value = model.CreateTime;
            sp[14].Value = model.Attach;
            sp[15].Value = model.CompID;
            return sp;
        }
        public M_Plat_Task GetModelFromReader(SqlDataReader rdr)
        {
            M_Plat_Task model = new M_Plat_Task();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.TaskName = ConverToStr(rdr["TaskName"]);
            model.TaskContent = ConverToStr(rdr["TaskContent"]);
            model.TaskType = ConvertToInt(rdr["TaskType"]);
            model.BeginTime = ConvertToDate(rdr["BeginTime"]);
            model.EndTime = ConvertToDate(rdr["EndTime"]);
            model.LeaderIDS = ConverToStr(rdr["LeaderIDS"]);
            model.PartTakeIDS = ConverToStr(rdr["PartTakeIDS"]);
            model.Color = ConverToStr(rdr["Color"]);
            model.Status = ConvertToInt(rdr["Status"]);
            model.Remind = ConverToStr(rdr["Remind"]);
            model.CreateUser = ConvertToInt(rdr["CreateUser"]);
            model.CreateUName = ConverToStr(rdr["CreateUName"]);
            model.CreateTime = ConvertToDate(rdr["CreateTime"]);
            model.Attach = ConverToStr(rdr["Attach"]);
            model.CompID = ConvertToInt(rdr["CompID"]);
            rdr.Close();
            return model;
        }
    }
}