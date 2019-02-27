using System;
using System.Data.SqlClient;
using System.Data;
namespace ZoomLa.Model
{
	[Serializable]
	public class M_Courseware:M_Base
	{
		#region 定义字段
		/// <summary>
        /// 
        /// </summary>
        public int ID { get; set; }
		/// <summary>
        /// 课件主题
        /// </summary>
        public string Courseware { get; set; }
		/// <summary>
        /// 课件次序
        /// </summary>
        public int CoursNum { get; set; }
		/// <summary>
        /// 可否试听：0为否,1为是
        /// </summary>
        public int Listen { get; set; }
		/// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
		/// <summary>
        /// 试听地址
        /// </summary>
        public string FileUrl { get; set; }
		/// <summary>
        /// 课程ID
        /// </summary>
        public int CourseID { get; set; }
        /// <summary>
        /// 主讲人
        /// </summary>
        public string Speaker { get; set; }
        /// <summary>
        /// 设计者
        /// </summary>
        public string SJName { get; set; }
        /// <summary>
        /// 状态:0为可用;1为不可用
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 简介
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 分类
        /// </summary>
        public string Classification { get; set; }
        /// <summary>
        /// 课件类型:0为外部课件;1为SCORM标准课件 
        /// </summary>
        public int CoursType { get; set; }
        /// <summary>
        /// 课件创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }
		#endregion
		
		#region 构造函数
		public M_Courseware()
		{
		}
		
		public M_Courseware
		(
			int ID,
			string Courseware,
			int CoursNum,
			int Listen,
			DateTime UpdateTime,
			string FileUrl,
			int CourseID,
            string Speaker,
            string SJName,
            int Status,
            string Description,
            string Classification,
            int CoursType,
            DateTime CreationTime
		)
		{
			this.ID = ID;
			this.Courseware = Courseware;
			this.CoursNum = CoursNum;
			this.Listen = Listen;
			this.UpdateTime = UpdateTime;
			this.FileUrl = FileUrl;
			this.CourseID = CourseID;
            this.Speaker = Speaker;
            this.SJName = SJName;
            this.Status = Status;
            this.Description = Description;
            this.Classification = Classification;
            this.CoursType = CoursType;
            this.CreationTime = CreationTime;
		}
		/// <summary>
		/// 返回实体列表数组
		/// </summary>
		/// <returns>String[]</returns>
		public string[] CoursewareList()
		{
            string[] Tablelist = { "ID", "Courseware", "CoursNum", "Listen", "UpdateTime", "FileUrl", "CourseID", "Speaker", "SJName", "Status", "Description", "Classification", "CoursType", "CreationTime" };
			return Tablelist;
		}
		#endregion

        public override string TbName { get { return "ZL_Courseware"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"ID","Int","4"},
                                  {"Courseware","NVarChar","50"},
                                  {"CoursNum","Int","4"}, 
                                  {"Listen","Int","4"},
                                  {"UpdateTime","DateTime","8"},
                                  {"FileUrl","NVarChar","255"},
                                  {"CourseID","Int","4"},
                                  {"Speaker","NVarChar","50"}, 
                                  {"SJName","NVarChar","255"},
                                  {"Status","Int","4"},
                                  {"Description","NVarChar","255"},
                                  {"Classification","NVarChar","255"},
                                  {"CoursType","Int","4"}, 
                                  {"CreationTime","DateTime","8"}, 
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

        public override SqlParameter[] GetParameters()
        {
            M_Courseware model = this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.Courseware;
            sp[2].Value = model.CoursNum;
            sp[3].Value = model.Listen;
            sp[4].Value = model.UpdateTime;
            sp[5].Value = model.FileUrl;
            sp[6].Value = model.CourseID;
            sp[7].Value = model.Speaker;
            sp[8].Value = model.SJName;
            sp[9].Value = model.Status;
            sp[10].Value = model.Description;
            sp[11].Value = model.Classification;
            sp[12].Value = model.CoursType;
            sp[13].Value = model.CreationTime;
            return sp;
        }

        public  M_Courseware GetModelFromReader(SqlDataReader rdr)
        {
            M_Courseware model = new M_Courseware();
            model.ID = ConvertToInt(rdr["ID"]);
            model.Courseware = ConverToStr(rdr["Courseware"]);
            model.CoursNum = ConvertToInt(rdr["CoursNum"]);
            model.Listen = ConvertToInt(rdr["Listen"]);
            model.UpdateTime = ConvertToDate(rdr["UpdateTime"]);
            model.FileUrl = ConverToStr(rdr["FileUrl"]);
            model.CourseID = ConvertToInt(rdr["CourseID"]);
            model.Speaker = ConverToStr(rdr["Speaker"]);
            model.SJName = ConverToStr(rdr["SJName"]);
            model.Status = ConvertToInt(rdr["Status"]);
            model.Description = ConverToStr(rdr["Description"]);
            model.Classification = ConverToStr(rdr["Classification"]);
            model.CoursType = ConvertToInt(rdr["CoursType"]);
            model.CreationTime = ConvertToDate(rdr["CreationTime"]);
            rdr.Close();
            rdr.Dispose();
            return model;
        }
    }
	
}


