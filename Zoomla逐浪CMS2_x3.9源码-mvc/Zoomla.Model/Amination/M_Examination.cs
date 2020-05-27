using System;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;
namespace ZoomLa.Model
{
    [Serializable]
    public class M_Examination:M_Base
    {

        #region 构造函数
        public M_Examination()
        {
        }

        public M_Examination
        (
            int ExaID,
            string StuUserName,
            int Stuid,
            DateTime Generate,
            DateTime Starttime,
            DateTime Endtime,
            string Qlist,
            int Total,
            int Score,
            string Reviews,
            string Examiners,
            int RoomID
        )
        {
            this.ExaID = ExaID;
            this.StuUserName = StuUserName;
            this.Stuid = Stuid;
            this.Generate = Generate;
            this.Starttime = Starttime;
            this.Endtime = Endtime;
            this.Qlist = Qlist;
            this.Total = Total;
            this.Score = Score;
            this.Reviews = Reviews;
            this.Examiners = Examiners;
            this.RoomID = RoomID;
        }
        /// <summary>
        /// 返回实体列表数组
        /// </summary>
        /// <returns>String[]</returns>
        public string[] ExaminationList()
        {
            string[] Tablelist = { "ExaID", "StuUserName", "Stuid", "Generate", "Starttime", "Endtime", "Qlist", "Total", "Score", "Reviews", "Examiners", "RoomID" };
            return Tablelist;
        }
        #endregion

        #region 属性定义
        /// <summary>
        /// 试卷ID
        /// </summary>
        public int ExaID { get; set; }
        /// <summary>
        /// 考生姓名
        /// </summary>
        public string StuUserName { get; set; }
        /// <summary>
        /// 考生ID
        /// </summary>
        public int Stuid { get; set; }
        /// <summary>
        /// 生成时间
        /// </summary>
        public DateTime Generate { get; set; }
        /// <summary>
        /// 开始考试时间
        /// </summary>
        public DateTime Starttime { get; set; }
        /// <summary>
        /// 结束考试时间
        /// </summary>
        public DateTime Endtime { get; set; }
        /// <summary>
        /// 试题列表(ID)
        /// </summary>
        public string Qlist { get; set; }
        /// <summary>
        /// 试卷总数
        /// </summary>
        public int Total { get; set; }
        /// <summary>
        /// 考试评分
        /// </summary>
        public int Score { get; set; }
        /// <summary>
        /// 考试评语
        /// </summary>
        public string Reviews { get; set; }
        /// <summary>
        /// 阅卷人
        /// </summary>
        public string Examiners { get; set; }
        /// <summary>
        /// 考场ID
        /// </summary>
        public int RoomID { get; set; }
        #endregion

        public override string PK { get { return "ExaID"; } }
        public override string TbName { get { return "ZL_Examination"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"ExaID","Int","4"},
                                  {"StuUserName","NChar","255"},
                                  {"Stuid","Int","4"}, 
                                  {"Generate","DateTime","8"},
                                  {"Starttime","DateTime","8"},
                                  {"Endtime","DateTime","8"}, 
                                  {"Qlist","NChar","500"},
                                  {"Total","Int","4"},
                                  {"Score","Int","4"}, 
                                  {"Reviews","NChar","1000"},
                                  {"Examiners","NChar","255"},
                                  {"RoomID","Int","4"}
                             };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_Examination model = this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ExaID;
            sp[1].Value = model.StuUserName;
            sp[2].Value = model.Stuid;
            sp[3].Value = model.Generate;
            sp[4].Value = model.Starttime;
            sp[5].Value = model.Endtime;
            sp[6].Value = model.Qlist;
            sp[7].Value = model.Total;
            sp[8].Value = model.Score;
            sp[9].Value = model.Reviews;
            sp[10].Value = model.Examiners;
            sp[11].Value = model.RoomID;
          return sp;
        }
        public M_Examination GetModelFromReader(DbDataReader rdr)
        {
            M_Examination model = new M_Examination();
            model.ExaID = Convert.ToInt32(rdr["ExaID"]);
            model.StuUserName = ConverToStr(rdr["StuUserName"]);
            model.Stuid = ConvertToInt( rdr["Stuid"]);
            model.Generate = ConvertToDate( rdr["Generate"]);
            model.Starttime = ConvertToDate( rdr["Starttime"]);
            model.Endtime = ConvertToDate( rdr["Endtime"]);
            model.Qlist = ConverToStr(rdr["Qlist"]);
            model.Total = ConvertToInt(rdr["Total"]);
            model.Score = ConvertToInt(rdr["Score"]);
            model.Reviews = ConverToStr(rdr["Reviews"]);
            model.Examiners = ConverToStr(rdr["Examiners"]);
            model.RoomID = ConvertToInt( rdr["RoomID"]);
            rdr.Close();
            rdr.Dispose();
            return model;
        }
    }
}



