using System;
using System.Data.SqlClient;
using System.Data;
namespace ZoomLa.Model
{
    [Serializable]
    public class M_ExStudent:M_Base
    {
        #region 构造函数
        public M_ExStudent()
        {
        }

        public M_ExStudent
        (
            int Stuid,
            string Stuname,
            string Stupassword,
            string Stucardno,
            int Userid,
            DateTime Exptime,
            int Lognum,
            int Examnum,
            int Logtimeout,
            int Stugroup,
            int Regulation,
            string strCompetence,
            string Course,
            int Qualified,
            DateTime Addtime
        )
        {
            this.Stuid = Stuid;
            this.Stuname = Stuname;
            this.Stupassword = Stupassword;
            this.Stucardno = Stucardno;
            this.Userid = Userid;
            this.Exptime = Exptime;
            this.Lognum = Lognum;
            this.Examnum = Examnum;
            this.Logtimeout = Logtimeout;
            this.Stugroup = Stugroup;
            this.Regulation = Regulation;
            this.strCompetence = strCompetence;
            this.Course = Course;
            this.Qualified = Qualified;
            this.Addtime = Addtime;
        }
        /// <summary>
        /// 返回实体列表数组
        /// </summary>
        /// <returns>String[]</returns>
        public string[] ExStudentList()
        {
            string[] Tablelist = { "Stuid", "Stuname", "Stupassword", "Stucardno", "Userid", "Exptime", "Lognum", "Examnum", "Logtimeout", "Stugroup", "Regulation", "strCompetence", "Course", "Qualified", "Addtime" };
            return Tablelist;
        }
        #endregion

        #region 属性定义
        /// <summary>
        /// 学员ID
        /// </summary>
        public int Stuid { get; set; }
        /// <summary>
        /// 考生姓名
        /// </summary>
        public string Stuname { get; set; }
        /// <summary>
        /// 登录密码(二重密码)
        /// </summary>
        public string Stupassword { get; set; }
        /// <summary>
        /// 考生考号(卡)
        /// </summary>
        public string Stucardno { get; set; }
        /// <summary>
        /// 捆绑用户ID
        /// </summary>
        public int Userid { get; set; }
        /// <summary>
        /// 到期有效时间
        /// </summary>
        public DateTime Exptime { get; set; }
        /// <summary>
        /// 登录次数
        /// </summary>
        public int Lognum { get; set; }
        /// <summary>
        /// 考试次数
        /// </summary>
        public int Examnum { get; set; }
        /// <summary>
        /// 登录时长
        /// </summary>
        public int Logtimeout { get; set; }
        /// <summary>
        /// 所在组别
        /// </summary>
        public int Stugroup { get; set; }
        /// <summary>
        /// 监管人
        /// </summary>
        public int Regulation { get; set; }
        /// <summary>
        /// 考生权限
        /// </summary>
        public string strCompetence { get; set; }
        /// <summary>
        /// 学习课程
        /// </summary>
        public string Course { get; set; }
        /// <summary>
        /// 是否合格
        /// </summary>
        public int Qualified { get; set; }
        /// <summary>
        /// 启用时间
        /// </summary>
        public DateTime Addtime { get; set; }
        #endregion
        public override string PK { get { return "Stuid"; } }
        public override string TbName { get { return "ZL_ExStudent"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"Stuid","Int","4"},
                                  {"Stuname","NChar","255"},
                                  {"Stupassword","NChar","255"}, 
                                  {"Stucardno","NChar","255"},
                                  {"Userid","Int","4"},
                                  {"Exptime","DateTime","8"}, 
                                  {"Lognum","Int","4"},
                                  {"Examnum","Int","4"},
                                  {"Logtimeout","Int","4"},
                                  {"Stugroup","Int","4"},
                                  {"Regulation","Int","4"}, 
                                  {"strCompetence","NText","400"}, 
                                  {"Course","NChar","500"}, 
                                  {"Qualified","Int","4"}, 
                                  {"Addtime","DateTime","8"}
                              };

            return Tablelist;

        }

        /// <summary>
        /// 获取字段窜
        /// </summary>
        public  string GetFields()
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
        public  string GetParas()
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
        public  string GetFieldAndPara()
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

        public  SqlParameter[] GetParameters(M_ExStudent model)
        {
            string[,] strArr = FieldList();
            SqlParameter[] sp = new SqlParameter[strArr.Length];

            for (int i = 0; i < strArr.GetLength(0); i++)
            {
                sp[i] = new SqlParameter("@" + strArr[i, 0], (SqlDbType)Enum.Parse(typeof(SqlDbType), strArr[i, 1]), Convert.ToInt32(strArr[i, 2]));
            }
            sp[0].Value = model.Stuid;
            sp[1].Value = model.Stuname;
            sp[2].Value = model.Stupassword;
            sp[3].Value = model.Stucardno;
            sp[4].Value = model.Userid;
            sp[5].Value = model.Exptime;
            sp[6].Value = model.Lognum;
            sp[7].Value = model.Examnum;
            sp[8].Value = model.Logtimeout;
            sp[9].Value = model.Stugroup;
            sp[10].Value = model.Regulation;
            sp[11].Value = model.strCompetence;
            sp[12].Value = model.Course;
            sp[13].Value = model.Qualified;
            sp[14].Value = model.Addtime;
            return sp;
        }
        public  M_ExStudent GetModelFromReader(SqlDataReader rdr)
        {
            M_ExStudent model = new M_ExStudent();
            model.Stuid = Convert.ToInt32(rdr["Stuid"]);
            model.Stuname = ConverToStr(rdr["Stuname"]);
            model.Stupassword = ConverToStr(rdr["Stupassword"]);
            model.Stucardno = ConverToStr(rdr["Stucardno"]);
            model.Userid = ConvertToInt(rdr["Userid"]);
            model.Exptime = ConvertToDate(rdr["Exptime"]);
            model.Lognum = ConvertToInt(rdr["Lognum"]);
            model.Examnum = ConvertToInt(rdr["Examnum"]);
            model.Logtimeout = ConvertToInt(rdr["Logtimeout"]);
            model.Stugroup = ConvertToInt(rdr["Stugroup"]);
            model.Regulation = ConvertToInt(rdr["Regulation"]);
            model.strCompetence = ConverToStr(rdr["strCompetence"]);
            model.Course = ConverToStr(rdr["Course"]);
            model.Qualified = ConvertToInt(rdr["Qualified"]);
            model.Addtime = ConvertToDate(rdr["Addtime"]);
            rdr.Close();
            rdr.Dispose();
            return model;
        }
    }
}