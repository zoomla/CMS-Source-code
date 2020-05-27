using System;
using System.Data.SqlClient;
using System.Data;
namespace ZoomLa.Model
{
	[Serializable]
	public class M_ExLecturer:M_Base
	{
		#region 定义字段
		/// <summary>
        /// 
        /// </summary>
        public int ID { get; set; }
		/// <summary>
        /// 教师名称
        /// </summary>
        public string TechName { get; set; }
		/// <summary>
        /// 教师类型：内训,外请
        /// </summary>
		public string TechType { get; set; }
		/// <summary>
        /// 教师性别:0为男,1为女
        /// </summary>
        public int TechSex { get; set; }
		/// <summary>
        /// 职称：初级,中级，副高,正高
        /// </summary>
		public string TechTitle { get; set; }
		/// <summary>
        /// 教师电话
        /// </summary>
		public string TechPhone{ get; set; }
		/// <summary>
        /// 创建时间
        /// </summary>
		public DateTime CreateTime{ get; set; }
		/// <summary>
        /// 特长
        /// </summary>
        public string TechSpecialty { get; set; }
		/// <summary>
        /// 爱好
        /// </summary>
        public string TechHobby { get; set; }
		/// <summary>
        /// 
        /// </summary>
        public string TechIntrodu { get; set; }
		/// <summary>
        /// 教师级别：一级，二级，三级
        /// </summary>
        public string TechLevel { get; set; }
		/// <summary>
        /// 部门
        /// </summary>
        public int TechDepart { get; set; }
		/// <summary>
        /// 专业类别：英语,计算机
        /// </summary>
        public int TechClass { get; set; }
		/// <summary>
        /// 推荐讲师
        /// </summary>
        public int TechRecom { get; set; }
		/// <summary>
        /// 人气
        /// </summary>
        public int Popularity { get; set; }
		/// <summary>
        /// 获奖情况
        /// </summary>
        public string Awardsinfo { get; set; }
		/// <summary>
        /// 上传照片
        /// </summary>
        public string FileUpload { get; set; }
		/// <summary>
        /// 添加用户
        /// </summary>
        public int AddUser { get; set; }
		/// <summary>
        /// 专业ID
        /// </summary>
        public int Professional { get; set; }
		#endregion
		
		#region 构造函数
		public M_ExLecturer()
		{
		}
		
		public M_ExLecturer
		(
			int ID,
			string TechName,
			string TechType,
			int TechSex,
			string TechTitle,
			string TechPhone,
			DateTime CreateTime,
			string TechSpecialty,
			string TechHobby,
			string TechIntrodu,
			string TechLevel,
			int TechDepart,
			int TechClass,
			int TechRecom,
			int Popularity,
			string Awardsinfo,
			string FileUpload,
			int AddUser,
			int Professional
		)
		{
			this.ID = ID;
			this.TechName = TechName;
			this.TechType = TechType;
			this.TechSex = TechSex;
			this.TechTitle = TechTitle;
			this.TechPhone = TechPhone;
			this.CreateTime = CreateTime;
			this.TechSpecialty = TechSpecialty;
			this.TechHobby = TechHobby;
			this.TechIntrodu = TechIntrodu;
			this.TechLevel = TechLevel;
			this.TechDepart = TechDepart;
			this.TechClass = TechClass;
			this.TechRecom = TechRecom;
			this.Popularity = Popularity;
			this.Awardsinfo = Awardsinfo;
			this.FileUpload = FileUpload;
			this.AddUser = AddUser;
			this.Professional = Professional;
		}
		/// <summary>
		/// 返回实体列表数组
		/// </summary>
		/// <returns>String[]</returns>
		public string[] ExLecturerList()
		{
            string[] Tablelist = { "ID", "TechName", "TechType", "TechSex", "TechTitle", "TechPhone", "CreateTime", "TechSpecialty", "TechHobby", "TechIntrodu", "TechLevel", "TechDepart", "TechClass", "TechRecom", "Popularity", "Awardsinfo", "FileUpload", "AddUser", "Professional" };
			return Tablelist;
		}
		#endregion

        public override string TbName { get { return "ZL_ExLecturer"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"ID","Int","4"},
                                  {"TechName","NVarChar","50"},
                                  {"TechType","NVarChar","50"},
                                  {"TechSex","Int","4"},
                                  {"TechTitle","NVarChar","50"},
                                  {"TechPhone","NVarChar","50"},
                                  {"CreateTime","DateTime","8"},
                                  {"TechSpecialty","NVarChar","1000"},
                                  {"TechHobby","NVarChar","1000"} ,
                                  {"TechIntrodu","NVarChar","1000"},
                                  {"TechLevel","NVarChar","50"},
                                  {"TechDepart","Int","4"},
                                  {"TechClass","Int","4"},
                                  {"TechRecom","Int","4"},
                                  {"Popularity","Int","4"} ,
                                  {"Awardsinfo","NVarChar","1000"},
                                  {"FileUpload","NVarChar","1000"},
                                  {"AddUser","Int","4"} ,
                                  {"Professional","Int","4"} ,
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

        public  SqlParameter[] GetParameters(M_ExLecturer model)
        {
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.TechName;
            sp[2].Value = model.TechType;
            sp[3].Value = model.TechSex;
            sp[4].Value = model.TechTitle;
            sp[5].Value = model.TechPhone;
            sp[6].Value = model.CreateTime;
            sp[7].Value = model.TechSpecialty;
            sp[8].Value = model.TechHobby;
            sp[9].Value = model.TechIntrodu;
            sp[10].Value = model.TechLevel;
            sp[11].Value = model.TechDepart;
            sp[12].Value = model.TechClass;
            sp[13].Value = model.TechRecom;
            sp[14].Value = model.Popularity;
            sp[15].Value = model.Awardsinfo;
            sp[16].Value = model.FileUpload;
            sp[17].Value = model.AddUser;
            sp[18].Value = model.Professional;
            return sp;
        }

        public  M_ExLecturer GetModelFromReader(SqlDataReader rdr)
        {
            M_ExLecturer model = new M_ExLecturer();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.TechName = rdr["TechName"].ToString();
            model.TechType = rdr["TechType"].ToString();
            model.TechSex = Convert.ToInt32(rdr["TechSex"]);
            model.TechTitle = rdr["TechTitle"].ToString();
            model.TechPhone = rdr["TechPhone"].ToString();
            model.CreateTime = Convert.ToDateTime(rdr["CreateTime"]);
            model.TechSpecialty = rdr["TechSpecialty"].ToString();
            model.TechHobby = rdr["TechHobby"].ToString();
            model.TechIntrodu = rdr["TechIntrodu"].ToString();
            model.TechLevel = rdr["TechLevel"].ToString();
            model.TechDepart = Convert.ToInt32(rdr["TechDepart"]);
            model.TechClass = Convert.ToInt32(rdr["TechClass"]);
            model.TechRecom = Convert.ToInt32(rdr["TechRecom"]);
            model.Popularity = Convert.ToInt32(rdr["Popularity"]);
            model.Awardsinfo = rdr["Awardsinfo"].ToString();
            model.FileUpload = rdr["FileUpload"].ToString();
            model.AddUser = Convert.ToInt32(rdr["AddUser"]);
            model.Professional = Convert.ToInt32(rdr["Professional"]); 
            rdr.Close();
            rdr.Dispose();
            return model;
        }
    }
}


