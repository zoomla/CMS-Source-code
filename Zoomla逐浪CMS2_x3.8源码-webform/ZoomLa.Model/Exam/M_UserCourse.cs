using System;
using System.Data.SqlClient;
using System.Data;
namespace ZoomLa.Model
{
	[Serializable]
	public class M_UserCourse:M_Base
	{
		#region 定义字段
		/// <summary>
        /// 
        /// </summary>
        public int ID { get; set; }
		/// <summary>
        /// 课程ID
        /// </summary>
        public int CourseID { get; set; }
		/// <summary>
        /// 班级ID
        /// </summary>
        public int ClassID { get; set; }
		/// <summary>
        /// 用户ID
        /// </summary>
        public int UserID { get; set; }
		/// <summary>
        /// 当前课时
        /// </summary>
        public double CurrCoureHour { get; set; }
		/// <summary>
        /// 开放状态
        /// </summary>
        public int State { get; set; }
		/// <summary>
        /// 申请时间
        /// </summary>
        public DateTime AddTime { get; set; }
		/// <summary>
        /// 是否付款
        /// </summary>
        public int PayMent { get; set; }
		/// <summary>
        /// 订单号
        /// </summary>
        public string OrderID { get; set; }
		/// <summary>
        /// 是否审核:0为否,1为是
        /// </summary>
        public int Aunit { get; set; }

        /// <summary>
        /// 申请原因
        /// </summary>
        public string Remark { get; set; }
		#endregion
		
		#region 构造函数
		public M_UserCourse()
		{
		}
		
		public M_UserCourse
		(
			int ID,
			int CourseID,
			int ClassID,
			int UserID,
			double CurrCoureHour,
			int State,
			DateTime AddTime,
			int PayMent,
			string OrderID,
			int Aunit,
            string Remark
		)
		{
			this.ID = ID;
			this.CourseID = CourseID;
			this.ClassID = ClassID;
			this.UserID = UserID;
			this.CurrCoureHour = CurrCoureHour;
			this.State = State;
			this.AddTime = AddTime;
			this.PayMent = PayMent;
			this.OrderID = OrderID;
			this.Aunit = Aunit;
            this.Remark = Remark;
		}
		/// <summary>
		/// 返回实体列表数组
		/// </summary>
		/// <returns>String[]</returns>
		public string[] UserCourseList()
		{
            string[] Tablelist = { "ID", "CourseID", "ClassID", "UserID", "CurrCoureHour", "State", "AddTime", "PayMent", "OrderID", "Aunit", "Remark" };
			return Tablelist;
		}
		#endregion

        public override string TbName { get { return "ZL_UserCourse"; } }
        /// <summary>
        /// 返回实体列表数组
        /// </summary>
        /// <returns>String[]</returns>
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"ID","Int","4"},
                                  {"CourseID","Int","4"},
                                  {"ClassID","Int","4"},
                                  {"UserID","Int","4"},
                                  {"CurrCoureHour","Float","8"},
                                  {"State","Int","4"},
                                  {"AddTime","DateTime","8"},
                                  {"PayMent","Int","4"},
                                  {"OrderID","NVarChar","50"},
                                  {"Aunit","Int","4"},
                                  {"Remark","NVarChar","1000"},
                                 };
            return Tablelist;
        }

        public  override SqlParameter[] GetParameters()
        {
            M_UserCourse model=this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.CourseID;
            sp[2].Value = model.ClassID;
            sp[3].Value = model.UserID;
            sp[4].Value = model.CurrCoureHour;
            sp[5].Value = model.State;
            sp[6].Value = model.AddTime;
            sp[7].Value = model.PayMent;
            sp[8].Value = model.OrderID;
            sp[9].Value = model.Aunit;
            sp[10].Value = model.Remark; 
            return sp;
        }

        public  M_UserCourse GetModelFromReader(SqlDataReader rdr)
        {
            M_UserCourse model = new M_UserCourse();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.CourseID = ConvertToInt(rdr["CourseID"]);
            model.ClassID = ConvertToInt(rdr["ClassID"]);
            model.UserID = ConvertToInt(rdr["UserID"]);
            model.CurrCoureHour = Convert.ToInt64(rdr["CurrCoureHour"]);
            model.State = ConvertToInt(rdr["State"]);
            model.AddTime = ConvertToDate(rdr["AddTime"]);
            model.PayMent = ConvertToInt(rdr["PayMent"]);
            model.OrderID = ConverToStr(rdr["OrderID"]);
            model.Aunit = ConvertToInt(rdr["Aunit"]);
            model.Remark = ConverToStr(rdr["Remark"]);
            rdr.Close();
            rdr.Dispose();
            return model;
        }
    }
	
}


