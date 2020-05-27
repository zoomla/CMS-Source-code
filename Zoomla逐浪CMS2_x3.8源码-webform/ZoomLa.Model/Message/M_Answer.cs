using System;
using System.Data.SqlClient;
using System.Data;

/// <summary>
///用于问卷调查
/// </summary>

namespace ZoomLa.Model
{
    public class M_Answer:M_Base
    {
        public int AnswerID { get; set; } //答案ID
        public int SurveyID { get; set; } //问卷ID
        public int QuestionID { get; set; }//问题ID        
        public string AnswerContent { get; set; }//答案内容
        public int UserID { get; set; }//用户ID
        public string SubmitIP { get; set; }
        public DateTime SubmitDate { get; set; } //提交答案时间
        public bool IsNull { get; set; }
        public int AnswerScore { get; set; }//提交答案得分
        public M_Answer()
        {
            this.AnswerID = 0;
            this.SurveyID = 0;
            this.QuestionID = 0;
            this.AnswerContent = "";
            this.UserID = 0;
            this.SubmitDate = DateTime.Now;
            this.IsNull = false;
            this.AnswerScore = 0;
        }
        public M_Answer(bool value)
        {
            this.IsNull = value;
        }
        public override string PK { get { return "AnswerID"; } }
        public override string TbName { get { return "ZL_Answer"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"Aid","Int","4"},
                                  {"SurveyID","Int","4"},
                                  {"Qid","Int","4"},
                                  {"AnswerContent","Text","1000"},
                                  {"UserID","Int","4"},
                                  {"SubmitDate","DateTime","8"}, 
                                  {"SubmitIP","NVarChar","1000"},
                                  {"AnswerScore","Int","4"},
                                 };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_Answer model = this;
            if (SubmitDate <= DateTime.MinValue) { SubmitDate = DateTime.Now; }
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.AnswerID;
            sp[1].Value = model.SurveyID;
            sp[2].Value = model.QuestionID;
            sp[3].Value = model.AnswerContent;
            sp[4].Value = model.UserID;
            sp[5].Value = model.SubmitDate;
            sp[6].Value = model.SubmitIP;
            sp[7].Value = model.AnswerScore;
            //sp[5].Value = model.SubmitIP;
            //sp[7].Value = model.IsNull;
            return sp;
        }
        public M_Answer GetModelFromReader(SqlDataReader rdr)
        {
            M_Answer model = new M_Answer();
            model.AnswerID = Convert.ToInt32(rdr["Aid"]);
            model.SurveyID = ConvertToInt(rdr["SurveyID"]);
            model.QuestionID = ConvertToInt(rdr["Qid"]);
            model.AnswerContent =ConverToStr(rdr["AnswerContent"]);
            model.UserID = ConvertToInt(rdr["UserID"]);
            model.SubmitIP = ConverToStr(rdr["SubmitIP"]);
            model.SubmitDate = ConvertToDate(rdr["SubmitDate"]);
            model.AnswerScore = ConvertToInt(rdr["AnswerScore"]);
            rdr.Close();
            rdr.Dispose();
            return model;
        }
    }
}