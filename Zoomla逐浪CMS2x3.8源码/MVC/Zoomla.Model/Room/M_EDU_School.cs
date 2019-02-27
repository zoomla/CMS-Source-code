using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ZoomLa.Model.Room
{
    /// <summary>
    ///  用于存储排课信息
    /// </summary>
    public class M_EDU_School : M_Base
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        /// <summary>
        /// 早读
        /// </summary>
        public int premoning { get; set; }
        /// <summary>
        /// 上午几节
        /// </summary>
        public int moring { get; set; }
        /// <summary>
        /// 下午几节
        /// </summary>
        public int afternoon { get; set; }
        /// <summary>
        /// 晚上几节
        /// </summary>
        public int evening { get; set; }
        /// <summary>
        /// 每周几天课
        /// </summary>
        public int weekday { get; set; }
        //private List<M_EDU_Item> _items = new List<M_EDU_Item>();
        ///// <summary>
        ///// 课数基本配置
        ///// </summary>
        //public List<M_EDU_Item> items { get { return _items; } set { _items = value; } }
        public string items { get; set; }
        /// <summary>
        /// 学校名
        /// </summary>
        public string SchoolName { get; set; }
        /// <summary>
        /// 学期名|课程表名称
        /// </summary>
        public string TermName { get; set; }
        public string SchoolType { get; set; }
        /// <summary>
        /// 是否有早晚自习
        /// </summary>
        public int StudySelf { get; set; }
        /// <summary>
        /// 是否有自选课程
        /// </summary>
        public int SelectCourse { get; set; }
        /// <summary>
        /// 每节课的时间
        /// </summary>
        public string CourseTime { get; set; }

        /// <summary>
        /// 所属班级IDS,为空则为私有
        /// </summary>
        public string ClassIDS { get; set; }
        public DateTime CDate { get; set; }

        public override string TbName { get { return "ZL_EDU_School"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
        		        		{"ID","Int","4"},
                                {"UserID","Int","4"},
                                {"premoning","Int","4"},
        		        		{"moring","Int","4"},
        		        		{"afternoon","Int","4"},
        		        		{"evening","Int","4"},
        		        		{"weekday","Int","4"},
        		        		{"items","NText","50000"},
        		        		{"SchoolName","NVarChar","200"},
        		        		{"TermName","NVarChar","200"},
        		        		{"SchoolType","NVarChar","50"},
        		        		{"StudySelf","Int","4"},
        		        		{"SelectCourse","Int","4"},
        		        		{"CourseTime","NVarChar","4000"},
                                {"ClassIDS","VarChar","8000"},
                                {"CDate","DateTime","8"}

        };
            return Tablelist;
        }

        public override SqlParameter[] GetParameters()
        {
            M_EDU_School model = this;
            if (model.CDate <= DateTime.MinValue) { CDate = DateTime.Now; }
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.UserID;
            sp[2].Value = model.premoning;
            sp[3].Value = model.moring;
            sp[4].Value = model.afternoon;
            sp[5].Value = model.evening;
            sp[6].Value = model.weekday;
            sp[7].Value = model.items;
            sp[8].Value = model.SchoolName;
            sp[9].Value = model.TermName;
            sp[10].Value = model.SchoolType;
            sp[11].Value = model.StudySelf;
            sp[12].Value = model.SelectCourse;
            sp[13].Value = model.CourseTime;
            sp[14].Value = model.ClassIDS;
            sp[15].Value = model.CDate;
            return sp;
        }
        public M_EDU_School GetModelFromReader(SqlDataReader rdr)
        {
            M_EDU_School model = new M_EDU_School();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.premoning = ConvertToInt(rdr["premoning"]);
            model.moring = ConvertToInt(rdr["moring"]);
            model.afternoon = ConvertToInt(rdr["afternoon"]);
            model.evening = ConvertToInt(rdr["evening"]);
            model.weekday = ConvertToInt(rdr["weekday"]);
            model.items =  ConverToStr(rdr["items"]);
            model.SchoolName = ConverToStr(rdr["SchoolName"]);
            model.TermName = ConverToStr(rdr["TermName"]);
            model.SchoolType = ConverToStr(rdr["SchoolType"]);
            model.StudySelf = ConvertToInt(rdr["StudySelf"]);
            model.SelectCourse = ConvertToInt(rdr["SelectCourse"]);
            model.CourseTime = ConverToStr(rdr["CourseTime"]);
            model.UserID = ConvertToInt(rdr["UserID"]);
            model.ClassIDS = ConverToStr(rdr["ClassIDS"]);
            model.CDate = ConvertToDate(rdr["CDate"]);
            rdr.Close();
            return model;
        }
    }
    //一节课的模型
    public class M_EDU_Item
    {
        //moring,afternoon,evening
        public string time { get; set; }
        //第几日,从1开始
        public int day { get; set; }
        //第几节课,课数从1开始
        public int num { get; set; }
        public bool disabled { get; set; }
        public string text { get; set; }
    }
}
