using System;
using System.Data.SqlClient;
using System.Data;
namespace ZoomLa.Model
{
    public class M_ClassRoom:M_Base
    {
        public M_ClassRoom()
        {
            Creation = DateTime.Now;
        }
        #region 属性定义
        /// <summary>
        /// 班级ID
        /// </summary>
        public int RoomID
        {
            get;
            set;
        }
        /// <summary>
        /// 班级名称
        /// </summary>
        public string RoomName
        {
            get;
            set;
        }
        /// <summary>
        /// 学校ID
        /// </summary>
        public int SchoolID
        {
            get;
            set;
        }

        public string ClassName
        {
            get;
            set;
        }

        public string Room
        {
            get;
            set;
        }
        /// <summary>
        /// 担任老师
        /// </summary>
        public string Teacher
        {
            get;
            set;
        }
        /// <summary>
        /// 副管理员
        /// </summary>
        public string Adviser
        {
            get;
            set;
        }
        /// <summary>
        /// 班微标
        /// </summary>
        public string Monitor
        {
            get;
            set;
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime Creation
        {
            get;
            set;
        }
        /// <summary>
        /// 创建人(管理员)
        /// </summary>
        public int CreateUser
        {
            get;
            set;
        }
        /// <summary>
        /// 人数(Disuse,应该动态查询)
        /// </summary>
        public int Integral
        {
            get;
            set;
        }
        /// <summary>
        /// 班级介绍
        /// </summary>
        public string Classinfo
        {
            get;
            set;
        }
        /// <summary>
        /// 年级(disuse,应该改名称)
        /// </summary>
        public int Grade
        {
            get;
            set;
        }
        /// <summary>
        /// 审核
        /// </summary>
        public int IsTrue
        {
            get;
            set;
        }
        /// <summary>
        /// 班级星级
        /// </summary>
        public int ClassStar { get; set; }
        /// <summary>
        /// 是否毕业
        /// </summary>
        public int IsDone { get; set; }
        #endregion
        public override string PK { get { return "RoomID"; } }
        public override string TbName { get { return "ZL_Exam_ClassRoom"; } }
        public override string[,] FieldList()
        {

            string[,] Tablelist = {
                                  {"RoomID","Int","4"},
                                  {"RoomName","NVarChar","255"},
                                  {"SchoolID","Int","4"}, 
                                  {"ClassName","NVarChar","50"},
                                  {"Room","NVarChar","10"},
                                  {"Teacher","NVarChar","255"}, 
                                  {"Adviser","NVarChar","255"},
                                  {"Monitor","NVarChar","255"},
                                  {"Creation","DateTime","8"}, 
                                  {"CreateUser","Int","8"},
                                  {"Integral","Int","4"},
                                  {"Classinfo","NText","400"}, 
                                  {"Grade","Int","4"},
                                  {"IsTrue","Int","4"},
                                  {"ClassStar","Int","4"},
                                  {"IsDone","Int","4" }
                                 };

            return Tablelist;

        }
        public override SqlParameter[] GetParameters()
        {
            M_ClassRoom model = this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.RoomID;
            sp[1].Value = model.RoomName;
            sp[2].Value = model.SchoolID;
            sp[3].Value = model.ClassName;
            sp[4].Value = model.Room;
            sp[5].Value = model.Teacher;
            sp[6].Value = model.Adviser;
            sp[7].Value = model.Monitor;
            sp[8].Value = model.Creation;
            sp[9].Value = model.CreateUser;
            sp[10].Value = model.Integral;
            sp[11].Value = model.Classinfo;
            sp[12].Value = model.Grade;
            sp[13].Value = model.IsTrue;
            sp[14].Value = model.ClassStar;
            sp[15].Value = model.IsDone;
            return sp;
        }
        public M_ClassRoom GetModelFromReader(SqlDataReader rdr)
        {
            M_ClassRoom model = new M_ClassRoom();
            model.RoomID = Convert.ToInt32(rdr["RoomID"]);
            model.RoomName = ConverToStr(rdr["RoomName"]);
            model.SchoolID = ConvertToInt(rdr["SchoolID"]);
            model.ClassName = ConverToStr(rdr["ClassName"]);
            model.Room =  ConverToStr(rdr["Room"]);
            model.Teacher = ConverToStr(rdr["Teacher"]);
            model.Adviser =  ConverToStr(rdr["Adviser"]);
            model.Monitor =  ConverToStr(rdr["Monitor"]);
            model.Creation =ConvertToDate(rdr["Creation"]);
            model.CreateUser =ConvertToInt( rdr["CreateUser"]);
            model.Integral = ConvertToInt(rdr["Integral"]);
            model.Classinfo = ConverToStr(rdr["Classinfo"]);
            model.Grade = ConvertToInt(rdr["Grade"]);
            model.IsTrue = ConvertToInt(rdr["IsTrue"]);
            model.ClassStar = ConvertToInt(rdr["ClassStar"]);
            model.IsDone = ConvertToInt(rdr["IsDone"]);
            rdr.Dispose();
            return model;
        }
    }
}






