using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ZoomLa.Model.Exam
{
    public class M_Exam_Answer:M_Base
    {
        public int ID { get; set; }
        /// <summary>
        /// 流水号
        /// </summary>
        public string FlowID { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        /// <summary>
        /// 问题ID
        /// </summary>
        public int QID { get; set; }
        public string QTitle { get; set; }
        /// <summary>
        /// 问题类型
        /// </summary>
        public string QType { get; set; }
        public string Answer { get; set; }
        /// <summary>
        /// 开始答题时间
        /// </summary>
        public DateTime StartDate { get; set; }

        public DateTime CDate { get; set; }
        /// <summary>
        /// 试卷日期
        /// </summary>
        public int PaperID { get; set; }
        /// <summary>
        /// 试卷名
        /// </summary>
        public string PaperName { get; set; }
        //--------------------------------
        /// <summary>
        /// 该回答所获得的分数
        /// </summary>
        public int Score { get; set; }
        ///// <summary>
        ///// 自定义问题分数,或也使用score
        ///// </summary>
        //public int cscore { get; set; }
        /// <summary>
        /// 总分,仅存于主卷上
        /// </summary>
        public int TotalScore { get; set; }
        /// <summary>
        /// 教师ID
        /// </summary>
        public int TechID { get; set; }
        /// <summary>
        /// 教师名
        /// </summary>
        public string TechName { get; set; }
        /// <summary>
        /// 0尚未批复,1正确,2错误
        /// </summary>
        public int IsRight { get; set; }
        /// <summary>
        /// 教师对该题的批注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 教师批注日期
        /// </summary>
        public DateTime RDate { get; set; }
        /// <summary>
        /// 备注:为1则作为主记录
        /// </summary>
        public string Remind { get; set; }
        /// <summary>
        /// 所属大题
        /// </summary>
        public int pid { get; set; }
        /// <summary>
        /// 我的学校和班级,考试时手动输入
        /// </summary>
        public string MySchool { get; set; }
        public string MyClass { get; set; }
        public override string TbName { get { return "ZL_Exam_Answer"; } }

        public override string[,] FieldList()
        {
            string[,] Tablelist ={
                                     {"ID","Int","4"},
                                     {"UserID","Int","4"},
                                     {"UserName","NVarChar","200"},
                                     {"QID","Int","4"},
                                     {"QTitle","NVarChar","200"},
                                     {"QType","Int","4"},
                                     {"Answer","NText","10000"},
                                     {"CDate","DateTime","8"},
                                     {"FlowID","VarChar","50"},
                                     {"PaperID","Int","4"},
                                     {"PaperName","NVarChar","200"},
                                     {"Score","Int","4"},
                                     {"TechID","Int","4"},
                                     {"TechName","NVarChar","200"},
                                     {"IsRight","Int","4"},
                                     {"Remark","NVarChar","2000"},
                                     {"RDate","DateTime","8"},
                                     {"Remind","VarChar","50"},
                                     {"TotalScore","Int","4"},
                                     {"pid","Int","4"},
                                     {"StartDate","DateTime","8"},
                                     {"MySchool","NVarChar","50"},
                                     {"MyClass","NVarChar","50"}
                                };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_Exam_Answer model = this;
            if (CDate <= DateTime.MinValue) CDate = DateTime.Now;
            if (RDate <= DateTime.MinValue) RDate = DateTime.Now;
            if (StartDate <= DateTime.MinValue) StartDate = DateTime.Now;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.UserID;
            sp[2].Value = model.UserName;
            sp[3].Value = model.QID;
            sp[4].Value = model.QTitle;
            sp[5].Value = model.QType;
            sp[6].Value = model.Answer;
            sp[7].Value = model.CDate;
            sp[8].Value = model.FlowID;
            sp[9].Value = model.PaperID;
            sp[10].Value = model.PaperName;
            sp[11].Value = model.Score;
            sp[12].Value = model.TechID;
            sp[13].Value = model.TechName;
            sp[14].Value = model.IsRight;
            sp[15].Value = model.Remark;
            sp[16].Value = model.RDate;
            sp[17].Value = model.Remind;
            sp[18].Value = model.TotalScore;
            sp[19].Value = model.pid;
            sp[20].Value = model.StartDate;
            sp[21].Value = model.MySchool;
            sp[22].Value = model.MyClass;
            return sp;
        }
        public M_Exam_Answer GetModelFromReader(SqlDataReader rdr)
        {
            M_Exam_Answer model = new M_Exam_Answer();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.UserID = Convert.ToInt32(rdr["UserID"]);
            model.UserName = rdr["UserName"].ToString();
            model.QID = ConvertToInt(rdr["QID"]);
            model.QTitle = rdr["QTitle"].ToString();
            model.QType = rdr["QType"].ToString();
            model.Answer = rdr["Answer"].ToString();
            model.CDate = ConvertToDate(rdr["CDate"]);
            model.FlowID = ConverToStr(rdr["FlowID"]);
            model.PaperID = ConvertToInt(rdr["PaperID"]);
            model.PaperName = ConverToStr(rdr["PaperName"]);
            model.Score = ConvertToInt(rdr["Score"]);
            model.TechID = ConvertToInt(rdr["TechID"]);
            model.TechName = ConverToStr(rdr["TechName"]);
            model.IsRight = ConvertToInt(rdr["IsRight"]);
            model.Remark = ConverToStr(rdr["Remark"]);
            model.RDate = ConvertToDate(rdr["RDate"]);
            model.Remind = ConverToStr(rdr["Remind"]);
            model.TotalScore = ConvertToInt(rdr["TotalScore"]);
            model.pid = ConvertToInt(rdr["pid"]);
            model.StartDate = ConvertToDate(rdr["StartDate"]);
            model.MySchool = ConverToStr(rdr["MySchool"]);
            model.MyClass = ConverToStr(rdr["MyClass"]);
            rdr.Close();
            return model;
        }
    }
}
