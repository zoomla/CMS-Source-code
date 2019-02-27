using System;
using System.Data.SqlClient;
using System.Data;
namespace ZoomLa.Model
{
	[Serializable]
	public class M_Paper_Questions:M_Base
	{
		#region 定义字段
		/// <summary>
        /// 
        /// </summary>
		public int ID{get; set;}
		/// <summary>
        /// 大题标题
        /// </summary>
        public string QuestionTitle { get; set; }
		/// <summary>
        /// 试卷ID
        /// </summary>
        public int PaperID { get; set; }
		/// <summary>
        /// 
        /// </summary>
        public int OrderBy { get; set; }
		/// <summary>
        /// 题型
        /// </summary>
        public int QuestionType { get; set; }
        /// 副标题
        /// </summary>
        public string Subtitle { get; set; }
        /// 每小题分数
        /// </summary>
        public double Course { get; set; }
		/// <summary>
        /// 包含题数
        /// </summary>
        public int QuesNum { get; set; }
		/// <summary>
        /// 答题说明
        /// </summary>
        public string Remark { get; set; }
		/// <summary>
        /// 试题ID(多个以逗号隔开)
        /// </summary>
        public string QuestIDs { get; set; }
		/// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
		/// <summary>
        /// 添加用户
        /// </summary>
        public int AddUser { get; set; }
		#endregion
		
		#region 构造函数
		public M_Paper_Questions()
		{
		}
		
		public M_Paper_Questions
		(
			int ID,
			string QuestionTitle,
			int PaperID,
			int OrderBy,
			int QuestionType,
			string Subtitle,
			double Course,
			int QuesNum,
			string Remark,
			string QuestIDs,
			DateTime CreateTime,
			int AddUser
		)
		{
			this.ID = ID;
			this.QuestionTitle = QuestionTitle;
			this.PaperID = PaperID;
			this.OrderBy = OrderBy;
			this.QuestionType = QuestionType;
			this.Subtitle = Subtitle;
			this.Course = Course;
			this.QuesNum = QuesNum;
			this.Remark = Remark;
			this.QuestIDs = QuestIDs;
			this.CreateTime = CreateTime;
			this.AddUser = AddUser;
		}
		/// <summary>
		/// 返回实体列表数组
		/// </summary>
		/// <returns>String[]</returns>
		public string[] Paper_QuestionsList()
		{
			string[] Tablelist = {"ID","QuestionTitle","PaperID","OrderBy","QuestionType","Subtitle","Course","QuesNum","Remark","QuestIDs","CreateTime","AddUser"};
			return Tablelist;
		}
		#endregion
        public override string TbName { get { return "ZL_Paper_Questions"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"ID","Int","4"},
                                  {"QuestionTitle","NVarChar","50"},
                                  {"PaperID","Int","4"},
                                  {"OrderBy","Int","4"},
                                  {"QuestionType","Int","4"},
                                  {"Subtitle","NVarChar","500"},
                                  {"Course","Float","8"},
                                  {"QuesNum","Int","4"},
                                  {"Remark","NVarChar","500"} ,
                                  {"QuestIDs","NVarChar","500"},
                                  {"CreateTime","DateTime","8"},
                                  {"AddUser","Int","4"} 

                                 };
            return Tablelist;
        }

        public override SqlParameter[] GetParameters()
        {
            M_Paper_Questions model = this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.QuestionTitle;
            sp[2].Value = model.PaperID;
            sp[3].Value = model.OrderBy;
            sp[4].Value = model.QuestionType;
            sp[5].Value = model.Subtitle;
            sp[6].Value = model.Course;
            sp[7].Value = model.QuesNum;
            sp[8].Value = model.Remark;
            sp[9].Value = model.QuestIDs;
            sp[10].Value = model.CreateTime;
            sp[11].Value = model.AddUser;
            return sp;
        }

        public  M_Paper_Questions GetModelFromReader(SqlDataReader rdr)
        {
            M_Paper_Questions model = new M_Paper_Questions();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.QuestionTitle = ConverToStr(rdr["QuestionTitle"]); ;
            model.PaperID = ConvertToInt(rdr["PaperID"]);
            model.OrderBy = ConvertToInt(rdr["OrderBy"]);
            model.QuestionType = ConvertToInt(rdr["QuestionType"]);
            model.Subtitle = ConverToStr(rdr["Subtitle"]);
            model.Course = Convert.ToInt64(rdr["Course"]);
            model.QuesNum = ConvertToInt(rdr["QuesNum"]);
            model.Remark = ConverToStr(rdr["Remark"]);
            model.QuestIDs = ConverToStr(rdr["QuestIDs"]);
            model.CreateTime = ConvertToDate(rdr["CreateTime"]);
            model.AddUser = ConvertToInt(rdr["AddUser"]); 
            rdr.Close();
            rdr.Dispose();
            return model;
        }
    }
	
}


