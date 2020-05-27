using System;
using System.Data.SqlClient;
using System.Data;
namespace ZoomLa.Model
{
	[Serializable]
	public class M_ExTeacher:M_Base
	{
		#region 定义字段
		/// <summary>
        /// 
        /// </summary>
        public int ID { get; set; }
		/// <summary>
        /// 老师分类
        /// </summary>
        public int TClsss { get; set; }
		/// <summary>
        /// 老师名称
        /// </summary>
        public string TName { get; set; }
		/// <summary>
        /// 职位
        /// </summary>
		public string Post { get; set; }
		/// <summary>
        /// 授课
        /// </summary>
        public string Teach { get; set; }
		/// <summary>
        /// 图片
        /// </summary>
        public string FileUpload { get; set; }
		/// <summary>
        /// 老师信息
        /// </summary>
        public string Remark { get; set; }
		/// <summary>
        /// 
        /// </summary>
        public DateTime CreatTime { get; set; }
		/// <summary>
        /// 添加用户
        /// </summary>
        public int AddUser { get; set; }
		#endregion
		
		#region 构造函数
		public M_ExTeacher()
		{
		}
		
		public M_ExTeacher
		(
			int ID,
			int TClsss,
			string TName,
			string Post,
			string Teach,
			string FileUpload,
			string Remark,
			DateTime CreatTime,
			int AddUser
		)
		{
			this.ID = ID;
			this.TClsss = TClsss;
			this.TName = TName;
			this.Post = Post;
			this.Teach = Teach;
			this.FileUpload = FileUpload;
			this.Remark = Remark;
			this.CreatTime = CreatTime;
			this.AddUser = AddUser;
		}
		/// <summary>
		/// 返回实体列表数组
		/// </summary>
		/// <returns>String[]</returns>
		public string[] ExTeacherList()
		{
			string[] Tablelist = {"ID","TClsss","TName","Post","Teach","FileUpload","Remark","CreatTime","AddUser"};
			return Tablelist;
		}
		#endregion
        public override string TbName { get { return "ZL_ExTeacher"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"ID","Int","4"},
                                  {"TClsss","Int","4"},
                                  {"TName","NVarChar","50"},
                                  {"Post","NVarChar","50"},
                                  {"Teach","NVarChar","50"},
                                  {"FileUpload","NVarChar","255"},
                                  {"Remark","NVarChar","1000"},
                                  {"CreatTime","DateTime","8"},
                                  {"AddUser","Int","4"} ,

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

        public  SqlParameter[] GetParameters(M_ExTeacher model)
        {
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.TClsss;
            sp[2].Value = model.TName;
            sp[3].Value = model.Post;
            sp[4].Value = model.Teach;
            sp[5].Value = model.FileUpload;
            sp[6].Value = model.Remark;
            sp[7].Value = model.CreatTime;
            sp[8].Value = model.AddUser;
            return sp;
        }

        public  M_ExTeacher GetModelFromReader(SqlDataReader rdr)
        {
            M_ExTeacher model = new M_ExTeacher();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.TClsss = Convert.ToInt32(rdr["TClsss"]);
            model.TName = rdr["TName"].ToString();
            model.Post = rdr["Post"].ToString();
            model.Teach = rdr["Teach"].ToString();
            model.FileUpload = rdr["FileUpload"].ToString();
            model.Remark = rdr["Remark"].ToString();
            model.CreatTime = Convert.ToDateTime(rdr["CreatTime"]);
            model.AddUser = Convert.ToInt32(rdr["AddUser"]); 
            rdr.Close();
            rdr.Dispose();
            return model;
        }
    }
	
}



