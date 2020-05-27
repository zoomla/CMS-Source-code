using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;

namespace ZoomLa.Model.Plat
{
    public class M_Blog_Sdl : M_Base
    {
        /// <summary>
        /// 日程名称
        /// </summary>
        public int ID
        {
            get;
            set;
        }
        /// <summary>
        /// 日程名称
        /// </summary>
        public string Name
        {
            get;
            set;
        }
        /// <summary>
        /// 日程地址
        /// </summary>
        public string Place
        {
            get;
            set;
        }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartDate
        {
            get;
            set;
        }
        /// <summary>
        /// 结束时间  
        /// </summary>
        public DateTime EndDate
        {
            get;
            set;
        }
        /// <summary>
        /// 全天事件
        /// </summary>
        public int AllDay
        {
            get;
            set;
        }
        /// <summary>
        /// 重复
        /// </summary>
        public int Repeat
        {
            get;
            set;
        }
        private string _leaderIDS, _parterIDS;
        /// <summary>
        /// 负责人
        /// </summary>
        public string LeaderIDS
        {
            get 
            {
                if (!string.IsNullOrEmpty(_leaderIDS))
                {
                    _leaderIDS = ","+_leaderIDS.Trim(',')+",";
                }
                return _leaderIDS;
            }
            set { _leaderIDS = value; }
        }
        /// <summary>
        /// 参与人
        /// </summary>
        public string ParterIDS
        {
            get
            {
                if (!string.IsNullOrEmpty(_parterIDS))
                {
                    _parterIDS = "," + _parterIDS.Trim(',') + ",";
                }
                return _leaderIDS;
            }
            set { _parterIDS = value; }
        }
        /// <summary>
        /// 日程描述
        /// </summary>
        public string Describe
        {
            get;
            set;
        }
        /// <summary>
        /// 0:个人日程,1:公开日程
        /// </summary>
        public int TaskType
        {
            get;
            set;
        }
        public int UserID { get; set; }
        public override string TbName { get { return "ZL_Plat_Schedule"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
        		        		{"ID","Int","4"},
        		        		{"Name","NVarChar","50"},
        		        		{"StartDate","DateTime","8"},
                                {"EndDate","DateTime","8"},
                                {"AllDay","Int","4"},
                                {"Repeat","Int","4"},
                                {"Place","NVarChar","50"},
                                {"Describe","NVarChar","200"},
                                {"TaskType","NVarChar","50"},
                                {"LeaderIDS","NText","50"},
                                {"ParterIDS","NText","50"},
                                {"UserID","Int","4"}
        };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_Blog_Sdl model = this;
            if (model.StartDate <= DateTime.MinValue) { model.StartDate = DateTime.Now; }
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.Name;
            sp[2].Value = model.StartDate;
            sp[3].Value = model.EndDate;
            sp[4].Value = model.AllDay;
            sp[5].Value = model.Repeat;
            sp[6].Value = model.Place;
            sp[7].Value = model.Describe;
            sp[8].Value = model.TaskType;
            sp[9].Value = model.LeaderIDS;
            sp[10].Value = model.ParterIDS;
            sp[11].Value = model.UserID;
            return sp;
        }
        public M_Blog_Sdl GetModelFromReader(DbDataReader rdr)
        {
            M_Blog_Sdl model = new M_Blog_Sdl();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.Name = ConverToStr(rdr["Name"]);
            model.StartDate = ConvertToDate(rdr["StartDate"]);
            model.EndDate = ConvertToDate(rdr["EndDate"]);
            model.AllDay = ConvertToInt(rdr["AllDay"]);
            model.Repeat = ConvertToInt(rdr["Repeat"]);
            model.Place = ConverToStr(rdr["Place"]);
            model.Describe = ConverToStr(rdr["Describe"]);
            model.TaskType = ConvertToInt(rdr["TaskType"]);
            model.LeaderIDS = ConverToStr(rdr["LeaderIDS"]);
            model.ParterIDS = ConverToStr(rdr["ParterIDS"]);
            model.UserID = ConvertToInt(rdr["UserID"]);
            return model;
        }
    }
}