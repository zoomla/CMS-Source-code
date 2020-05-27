using System;
using System.Data.SqlClient;
using System.Data;
namespace ZoomLa.Model
{
    [Serializable]
    public class M_ScoreStatics:M_Base
    {
        #region 定义字段
        /// <summary>
        /// 
        /// </summary>
        public int ID{get; set;}
        /// <summary>
        /// 课程ID
        /// </summary>
        public int CourseID{get; set;}
        /// <summary>
        /// 试卷ID
        /// </summary>
        public int PID{get; set;}
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID{get; set;}
        /// <summary>
        /// 分数
        /// </summary>
        public double Fraction{get; set;}
        /// <summary>
        /// 考试时间
        /// </summary>
        public DateTime ExamTime{get; set;}
        /// <summary>
        /// 错误ID
        /// </summary>
        public int ErrorID{get; set;}
        /// <summary>
        /// 试题ID(包含的所有试题)
        /// </summary>
        public string QuestionIDs{get; set;}
        /// <summary>
        /// 用户答案(id+"|||"+答案)
        /// </summary>
        public string Answer {get; set;}
        /// <summary>
        /// 实际分数
        /// </summary>
        public double ProfileScore{get; set;}
        /// <summary>
        /// 评卷老师
        /// </summary>
        public string GraderWill {get; set;}
        #endregion

        #region 构造函数
        public M_ScoreStatics()
        {
        }

        public M_ScoreStatics
        (
            int ID,
            int CourseID,
            int PID,
            int UserID,
            double Fraction,
            DateTime ExamTime,
            int ErrorID,
            string QuestionIDs,
            string Answer,
            double ProfileScore,
            string GraderWill
        )
        {
            this.ID = ID;
            this.CourseID = CourseID;
            this.PID = PID;
            this.UserID = UserID;
            this.Fraction = Fraction;
            this.ExamTime = ExamTime;
            this.ErrorID = ErrorID;
            this.QuestionIDs = QuestionIDs;
            this.Answer = Answer;
            this.ProfileScore = ProfileScore;
            this.GraderWill = GraderWill;
        }
        /// <summary>
        /// 返回实体列表数组
        /// </summary>
        /// <returns>String[]</returns>
        public string[] ScoreStaticsList()
        {
            string[] Tablelist = { "ID", "CourseID", "PID", "UserID", "Fraction", "ExamTime", "ErrorID", "QuestionIDs", "Answer", "ProfileScore", "GraderWill" };
            return Tablelist;
        }
        #endregion
        public override string TbName { get { return "ZL_ScoreStatics"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"ID","Int","4"},
                                  {"CourseID","Int","4"},
                                  {"PID","Int","4"},
                                  {"UserID","Int","4"},
                                  {"Fraction","Float","8"},
                                  {"ExamTime","DateTime","8"},
                                  {"ErrorID","Int","4"},
                                  {"QuestionIDs","NVarChar","250"},
                                  {"Answer","NVarChar","500"},
                                  {"ProfileScore","Float","8"},
                                  {"GraderWill","NVarChar","50"},
                                 };
            return Tablelist;
        }

        public  override SqlParameter[] GetParameters()
        {
            M_ScoreStatics model=this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.CourseID;
            sp[2].Value = model.PID;
            sp[3].Value = model.UserID;
            sp[4].Value = model.Fraction;
            sp[5].Value = model.ExamTime;
            sp[6].Value = model.ErrorID;
            sp[7].Value = model.QuestionIDs;
            sp[8].Value = model.Answer;
            sp[9].Value = model.ProfileScore;
            sp[10].Value = model.GraderWill; 
            return sp;
        }

        public  M_ScoreStatics GetModelFromReader(SqlDataReader rdr)
        {
            M_ScoreStatics model = new M_ScoreStatics();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.CourseID = Convert.ToInt32(rdr["CourseID"]);
            model.PID = Convert.ToInt32(rdr["PID"]);
            model.UserID = Convert.ToInt32(rdr["UserID"]);
            model.Fraction = Convert.ToInt64(rdr["Fraction"]);
            model.ExamTime =  Convert.ToDateTime(rdr["ExamTime"]);
            model.ErrorID = Convert.ToInt32(rdr["ErrorID"]);
            model.QuestionIDs = rdr["QuestionIDs"].ToString();
            model.Answer = rdr["Answer"].ToString();
            model.ProfileScore = Convert.ToInt64(rdr["ProfileScore"]);
            model.GraderWill = rdr["GraderWill"].ToString();
            rdr.Close();
            rdr.Dispose();
            return model;
        }

        public override string GetPK()
        {
            return PK;
        }
    }
	
}


