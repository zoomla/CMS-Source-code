using System;
using System.Data.SqlClient;
using System.Data;
namespace ZoomLa.Model
{
    [Serializable]
    public class M_ExStudytime : M_Base
    {
        #region 构造函数
        public M_ExStudytime()
        {
        }

        public M_ExStudytime
        (
            int StudyID,
            int Stuid,
            int Course,
            string Studytime,
            string Resttime,
            int Schedule,
            string Reviews
        )
        {
            this.StudyID = StudyID;
            this.Stuid = Stuid;
            this.Course = Course;
            this.Studytime = Studytime;
            this.Resttime = Resttime;
            this.Schedule = Schedule;
            this.Reviews = Reviews;
        }
        /// <summary>
        /// 返回实体列表数组
        /// </summary>
        /// <returns>String[]</returns>
        public string[] ExStudytimeList()
        {
            string[] Tablelist = { "StudyID", "Stuid", "Course", "Studytime", "Resttime", "Schedule", "Reviews" };
            return Tablelist;
        }
        #endregion

        #region 属性定义
        /// <summary>
        /// 培训ID
        /// </summary>
        public int StudyID { get; set; }
        /// <summary>
        /// 学员ID
        /// </summary>
        public int Stuid { get; set; }
        /// <summary>
        /// 课程
        /// </summary>
        public int Course { get; set; }
        /// <summary>
        /// 学习时间
        /// </summary>
        public string Studytime { get; set; }
        /// <summary>
        /// 每日学习时间安排
        /// </summary>
        public string Resttime { get; set; }
        /// <summary>
        /// 学员当前进度
        /// </summary>
        public int Schedule { get; set; }
        /// <summary>
        /// 评语
        /// </summary>
        public string Reviews { get; set; }
        #endregion
        public override string PK { get { return "StudyID"; } }
        public override string TbName { get { return "ZL_ExStudytime"; } }

        /// <summary>
        /// 返回实体列表数组
        /// </summary>
        /// <returns>String[]</returns>
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"StudyID","Int","4"},
                                  {"Stuid","Int","4"},
                                  {"Course","Int","4"},
                                  {"Studytime","NChar","2000"},
                                  {"Resttime","NText","400"},
                                  {"Schedule","Int","4"},
                                  {"Reviews","NChar","1000"}
                             };

            return Tablelist;

        }

        /// <summary>
        /// 获取字段窜
        /// </summary>
        public string GetFields()
        {
            string str = string.Empty;
            string[,] strArr = FieldList();
            for (int i = 0; i < strArr.GetLength(0); i++)
            {
                if (strArr[i, 0] != PK)
                {
                    str += "[" + strArr[i, 0] + "],";
                }
            }
            return str.Substring(0, str.Length - 1);
        }

        /// <summary>
        /// 获取参数串
        /// </summary>
        public string GetParas()
        {
            string str = string.Empty;
            string[,] strArr = FieldList();
            for (int i = 0; i < strArr.GetLength(0); i++)
            {
                if (strArr[i, 0] != PK)
                {
                    str += "@" + strArr[i, 0] + ",";
                }
            }
            return str.Substring(0, str.Length - 1);
        }

        /// <summary>
        /// 获取字段=参数
        /// </summary>
        public string GetFieldAndPara()
        {
            string str = string.Empty;
            string[,] strArr = FieldList();
            for (int i = 0; i < strArr.GetLength(0); i++)
            {
                if (strArr[i, 0] != PK)
                {
                    str += "[" + strArr[i, 0] + "]=@" + strArr[i, 0] + ",";
                }
            }
            return str.Substring(0, str.Length - 1);
        }

        public SqlParameter[] GetParameters(M_ExStudytime model)
        {
            string[,] strArr = FieldList();
            SqlParameter[] sp = new SqlParameter[strArr.Length];

            for (int i = 0; i < strArr.GetLength(0); i++)
            {
                sp[i] = new SqlParameter("@" + strArr[i, 0], (SqlDbType)Enum.Parse(typeof(SqlDbType), strArr[i, 1]), Convert.ToInt32(strArr[i, 2]));
            }
            sp[0].Value = model.StudyID;
            sp[1].Value = model.Stuid;
            sp[2].Value = model.Course;
            sp[3].Value = model.Studytime;
            sp[4].Value = model.Resttime;
            sp[5].Value = model.Schedule;
            sp[6].Value = model.Reviews;

            return sp;
        }
        public M_ExStudytime GetModelFromReader(SqlDataReader rdr)
        {
            M_ExStudytime model = new M_ExStudytime();
            model.StudyID = Convert.ToInt32(rdr["StudyID"]);
            model.Stuid = ConvertToInt(rdr["Stuid"]);
            model.Stuid = ConvertToInt(rdr["Stuid"]);
            model.Course = ConvertToInt(rdr["Course"]);
            model.Studytime = ConverToStr(rdr["Studytime"]);
            model.Resttime = ConverToStr(rdr["Resttime"]);
            model.Schedule = ConvertToInt(rdr["Schedule"]);
            model.Reviews = ConverToStr(rdr["Reviews"]);
            rdr.Close();
            rdr.Dispose();
            return model;
        }
    }
}