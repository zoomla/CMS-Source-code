using System;
using System.Data.SqlClient;
using System.Data;
namespace ZoomLa.Model
{
	[Serializable]
	public class M_Course:M_Base
	{
		#region 定义字段
		/// <summary>
        /// 
        /// </summary>
		public int id{get;set;}
		/// <summary>
        /// 课程名称
        /// </summary>
		public string CourseName {get;set;}
		/// <summary>
        /// 课程缩写
        /// </summary>
		public string CoureseThrun {get;set;}
		/// <summary>
        /// 课程代码
        /// </summary>
		public string CoureseCode {get;set;}
		/// <summary>
        /// 学分
        /// </summary>
		public double CoureseCredit{get;set;}
		/// <summary>
        /// 课程简介
        /// </summary>
		public string CoureseRemark {get;set;}
		/// <summary>
        /// 是否热门:0为否,1为是
        /// </summary>
		public int Hot{get;set;}
		/// <summary>
        /// 课程分类ID
        /// </summary>
		public int CoureseClass{get;set;}
		/// <summary>
        /// 添加时间
        /// </summary>
		public DateTime AddTime{get;set;}
		/// <summary>
        /// 添加用户
        /// </summary>
		public int AddUser{get;set;}
		#endregion
		
		#region 构造函数
		public M_Course()
		{
		}
		
		public M_Course
		(
			int id,
			string CourseName,
			string CoureseThrun,
			string CoureseCode,
			double CoureseCredit,
			string CoureseRemark,
			int Hot,
			int CoureseClass,
			DateTime AddTime,
			int AddUser
		)
		{
			this.id = id;
			this.CourseName = CourseName;
			this.CoureseThrun = CoureseThrun;
			this.CoureseCode = CoureseCode;
			this.CoureseCredit = CoureseCredit;
			this.CoureseRemark = CoureseRemark;
			this.Hot = Hot;
			this.CoureseClass = CoureseClass;
			this.AddTime = AddTime;
			this.AddUser = AddUser;
		}
		/// <summary>
		/// 返回实体列表数组
		/// </summary>
		/// <returns>String[]</returns>
		public string[] CourseList()
		{
			string[] Tablelist = {"id","CourseName","CoureseThrun","CoureseCode","CoureseCredit","CoureseRemark","Hot","CoureseClass","AddTime","AddUser"};
			return Tablelist;
		}
		#endregion

        public override string TbName { get { return "ZL_Course"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"id","Int","4"},
                                  {"CourseName","NVarChar","500"},
                                  {"CoureseThrun","NVarChar","50"}, 
                                  {"CoureseCode","NVarChar","50"},
                                  {"CoureseCredit","Float","8"},
                                  {"CoureseRemark","NVarChar","1000"}, 
                                  {"Hot","Int","4"},
                                  {"CoureseClass","Int","4"},
                                  {"AddTime","DateTime","8"}, 
                                  {"AddUser","Int","4"},
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

        public  SqlParameter[] GetParameters(M_Course model)
        {
            string[,] strArr = FieldList();
            SqlParameter[] sp = new SqlParameter[strArr.Length];

            for (int i = 0; i < strArr.GetLength(0); i++)
            {
                sp[i] = new SqlParameter("@" + strArr[i, 0], (SqlDbType)Enum.Parse(typeof(SqlDbType), strArr[i, 1]), Convert.ToInt32(strArr[i, 2]));
            }
            sp[0].Value = model.id;
            sp[1].Value = model.CourseName;
            sp[2].Value = model.CoureseThrun;
            sp[3].Value = model.CoureseCode;
            sp[4].Value = model.CoureseCredit;
            sp[5].Value = model.CoureseRemark;
            sp[6].Value = model.Hot;
            sp[7].Value = model.CoureseClass;
            sp[8].Value = model.AddTime;
            sp[9].Value = model.AddUser;    
            return sp;
        }

        public  M_Course GetModelFromReader(SqlDataReader rdr)
        {
            M_Course model = new M_Course();
            model.id = Convert.ToInt32(rdr["id"]);
            model.CourseName = ConverToStr(rdr["CourseName"]);
            model.CoureseThrun = ConverToStr(rdr["CoureseThrun"]);
            model.CoureseCode = ConverToStr(rdr["CoureseCode"]);
            model.CoureseCredit = Convert.ToInt64(rdr["CoureseCredit"]);
            model.CoureseRemark = ConverToStr(rdr["CoureseRemark"]);
            model.Hot = ConvertToInt(rdr["Hot"]);
            model.CoureseClass = ConvertToInt(rdr["CoureseClass"]);
            model.AddTime =  ConvertToDate(rdr["AddTime"]);
            model.AddUser = ConvertToInt(rdr["AddUser"]);
            rdr.Close();
            rdr.Dispose();
            return model;
        }
    }
	
}


