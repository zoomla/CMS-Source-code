using System;
using System.Data.SqlClient;
using System.Data;
namespace ZoomLa.Model
{
	[Serializable]
	public class M_ExamPoint:M_Base
	{
		#region 定义字段
		/// <summary>
        /// 考点ID
        /// </summary>
        public int ID { get; set; }
		/// <summary>
        /// 考点名称
        /// </summary>
        public string TestPoint { get; set; }
		/// <summary>
        /// 上级考点
        /// </summary>
        public int TID { get; set; }
		/// <summary>
        /// 排序
        /// </summary>
        public int OrderBy { get; set; }
		/// <summary>
        /// 管理员
        /// </summary>
        public int AddUser { get; set; }
		/// <summary>
        /// 添加时间
        /// </summary>
        public DateTime AddTime { get; set; }
		#endregion
		
		#region 构造函数
		public M_ExamPoint()
		{
            this.AddTime = DateTime.Now;
            this.TestPoint = string.Empty;
		}
		
		public M_ExamPoint
		(
			int ID,
			string TestPoint,
			int TID,
			int OrderBy,
			int AddUser,
			DateTime AddTime
		)
		{
			this.ID = ID;
			this.TestPoint = TestPoint;
			this.TID = TID;
			this.OrderBy = OrderBy;
			this.AddUser = AddUser;
			this.AddTime = AddTime;
		}
		/// <summary>
		/// 返回实体列表数组
		/// </summary>
		/// <returns>String[]</returns>
		public string[] ExamPointList()
		{
			string[] Tablelist = {"ID","TestPoint","TID","OrderBy","AddUser","AddTime"};
			return Tablelist;
		}
		#endregion
        public override string TbName { get { return "ZL_ExamPoint"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"ID","Int","4"},
                                  {"TestPoint","NVarChar","50"},
                                  {"TID","Int","4"},
                                  {"OrderBy","Int","4"},
                                  {"AddUser","Int","4"},
                                  {"AddTime","DateTime","8"},

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

        public  SqlParameter[] GetParameters(M_ExamPoint model)
        {
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.TestPoint;
            sp[2].Value = model.TID;
            sp[3].Value = model.OrderBy;
            sp[4].Value = model.AddUser;
            sp[5].Value = model.AddTime;
            return sp;
        }

        public  M_ExamPoint GetModelFromReader(SqlDataReader rdr)
        {
            M_ExamPoint model = new M_ExamPoint();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.TestPoint = rdr["TestPoint"].ToString();
            model.TID = Convert.ToInt32(rdr["TID"]);
            model.OrderBy = Convert.ToInt32(rdr["OrderBy"]);
            model.AddUser = Convert.ToInt32(rdr["AddUser"]);
            model.AddTime = Convert.ToDateTime(rdr["AddTime"]);
            rdr.Close();
            rdr.Dispose();
            return model;
        }
    }
}


