using System;
using System.Data.SqlClient;
using System.Data;
namespace ZoomLa.Model
{
    [Serializable]
    public class M_ExAnswer:M_Base
    {
        #region 构造函数
        public M_ExAnswer()
        {
        }

        public M_ExAnswer
        (
            int AnswerID,
            int ExaID,
            int Stuid,
            string Answer,
            DateTime AnswerTime,
            int Fraction,
            int Scores,
            string Examiners,
            int RoomID,
            string StuName
        )
        {
            this.AnswerID = AnswerID;
            this.ExaID = ExaID;
            this.Stuid = Stuid;
            this.Answer = Answer;
            this.AnswerTime = AnswerTime;
            this.Fraction = Fraction;
            this.Scores = Scores;
            this.Examiners = Examiners;
            this.RoomID = RoomID;
            this.StuName = StuName;
        }
        /// <summary>
        /// 返回实体列表数组
        /// </summary>
        /// <returns>String[]</returns>
        public string[] ExAnswerList()
        {
            string[] Tablelist = { "AnswerID", "ExaID", "Stuid", "Answer", "AnswerTime", "Fraction", "Scores", "Examiners", "RoomID", "StuName" };
            return Tablelist;
        }
        #endregion

        #region 属性定义
        /// <summary>
        /// 答题ID
        /// </summary>
        public int AnswerID { get; set; }
        /// <summary>
        /// 试卷ID
        /// </summary>
        public int ExaID { get; set; }
        /// <summary>
        /// 考生ID
        /// </summary>
        public int Stuid { get; set; }
        /// <summary>
        /// 考生答案
        /// </summary>
        public string Answer { get; set; }
        /// <summary>
        /// 答题时间
        /// </summary>
        public DateTime AnswerTime { get; set; }
        /// <summary>
        /// 试题分数
        /// </summary>
        public int Fraction { get; set; }
        /// <summary>
        /// 所得分数
        /// </summary>
        public int Scores { get; set; }
        /// <summary>
        /// 阅卷人
        /// </summary>
        public string Examiners { get; set; }
        /// <summary>
        /// 考场ID
        /// </summary>
        public int RoomID { get; set; }
        /// <summary>
        /// 考生姓名
        /// </summary>
        public string StuName { get; set; }
        #endregion

        public override string PK { get { return "AnswerID"; } }
        public override string TbName { get { return "ZL_ExAnswer"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"AnswerID","Int","4"},
                                  {"ExaID","Int","4"},
                                  {"Stuid","Int","4"}, 
                                  {"Answer","NChar","1000"},
                                  {"AnswerTime","DateTime","8"},
                                  {"Fraction","Int","4"}, 
                                  {"Scores","Int","4"},
                                  {"Examiners","NChar","255"},
                                  {"RoomID","Int","4"}, 
                                  {"StuName","NChar","255"}
                             };
            return Tablelist;
        }

        /// <summary>
        /// 获取字段窜
        /// </summary>
        public string GetFields()
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
        public string GetParas()
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
        public string GetFieldAndPara()
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

        public SqlParameter[] GetParameters(M_ExAnswer model)
        {
            string[,] strArr = FieldList();
            SqlParameter[] sp = new SqlParameter[strArr.Length];

            for (int i = 0; i < strArr.GetLength(0); i++)
            {
                sp[i] = new SqlParameter("@" + strArr[i, 0], (SqlDbType)Enum.Parse(typeof(SqlDbType), strArr[i, 1]), ConvertToInt(strArr[i, 2]));
            }
            sp[0].Value = model.AnswerID;
            sp[1].Value = model.ExaID;
            sp[2].Value = model.Stuid;
            sp[3].Value = model.Answer;
            sp[4].Value = model.AnswerTime;
            sp[5].Value = model.Fraction;
            sp[6].Value = model.Scores;
            sp[7].Value = model.RoomID;
            sp[8].Value = model.Examiners;
            sp[9].Value = model.RoomID;
            sp[10].Value = model.StuName;
            return sp;
        }
        public M_ExAnswer GetModelFromReader(SqlDataReader rdr)
        {
            M_ExAnswer model = new M_ExAnswer();
            model.AnswerID = Convert.ToInt32(rdr["AnswerID"]);
            model.ExaID =ConvertToInt( rdr["ExaID"]);
            model.Stuid = ConvertToInt(rdr["Stuid"]);
            model.Answer = ConverToStr(rdr["Answer"]);
            model.AnswerTime = ConvertToDate(rdr["AnswerTime"]);
            model.Fraction = ConvertToInt(rdr["Fraction"]);
            model.Scores = ConvertToInt(rdr["Scores"]);
            model.Examiners = ConverToStr(rdr["Examiners"]);
            model.RoomID =ConvertToInt( rdr["RoomID"]);
            model.StuName = ConverToStr(rdr["StuName"]);
            model.Examiners = ConverToStr(rdr["Examiners"]);
            model.RoomID = ConvertToInt(rdr["RoomID"]);
            rdr.Close();
            rdr.Dispose();
            return model;
        }
    }
}


