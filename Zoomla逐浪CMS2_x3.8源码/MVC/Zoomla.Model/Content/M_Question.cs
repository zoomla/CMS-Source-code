using System;
using System.Data.SqlClient;
using System.Data;


namespace ZoomLa.Model
{
    public class M_Question:M_Base
    {
        public M_Question() { }
        public M_Question(bool value)
        {
            this.IsNull = value;
        }
        public int QuestionID
        {
            get;
            set;
        }
        /// <summary>
        /// //问卷的ID
        /// </summary>
        public int SurveyID
        {
            get;
            set;
        }
        /// <summary>
        /// 问题类型ID
        /// </summary>
        public int TypeID
        {
            get;
            set;
        }
        /// <summary>
        /// 问题题目
        /// </summary>
        public string QuestionTitle
        {
            get;
            set;
        }
        /// <summary>
        /// 问题设定内容
        /// </summary>
        public string QuestionContent
        {
            get;
            set;
        }
        public int OrderID
        {
            get;
            set;
        }
        public DateTime QuestionCreateTime
        {
            get;
            set;
        }
        //该问题的设定与规则,用于前端输出
        public string Qoption { get; set; }
        public bool IsNull
        {
            get;
            set;
        }
        public override string PK { get { return "QID"; } }
        public override string TbName { get { return "ZL_QUESTION"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"QID","Int","4"},
                                  {"SurveyID","Int","4"},
                                  {"TypeID","Int","4"},
                                  {"QTitle","NVarChar","500"},
                                  {"QContent","NVarChar","1000"},
                                  {"Orderid","Int","4"},
                                  {"QuestionCreateTime","DateTime","8"},
                                  {"IsNull","Bit","1"},
                                  {"Qoption","NVarChar","4000"}
                                 };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_Question model = this;
            if (model.QuestionCreateTime <= DateTime.MinValue) { model.QuestionCreateTime = DateTime.Now; }
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.QuestionID;
            sp[1].Value = model.SurveyID;
            sp[2].Value = model.TypeID;
            sp[3].Value = model.QuestionTitle;
            sp[4].Value = model.QuestionContent;
            sp[5].Value = model.OrderID; 
            sp[6].Value = model.QuestionCreateTime;
            sp[7].Value = model.IsNull;
            sp[8].Value = model.Qoption;
            return sp;
        }
        public M_Question GetQuestionFromReader(SqlDataReader rdr)
        {
            M_Question info = new M_Question();
            info.QuestionID = Convert.ToInt32(rdr["QID"].ToString());
            info.SurveyID = Convert.ToInt32(rdr["SurveyID"].ToString());
            info.TypeID = Convert.ToInt32(rdr["TypeID"].ToString());
            info.QuestionTitle = rdr["QTitle"].ToString();
            info.QuestionContent = rdr["QContent"].ToString();
            info.OrderID = ConvertToInt(rdr["OrderID"]);
            info.QuestionCreateTime = ConvertToDate(rdr["QuestionCreateTime"]);
            info.IsNull = ConverToBool(rdr["IsNull"].ToString());
            info.Qoption = ConverToStr(rdr["Qoption"]);
            return info;
        }
    }
    public class M_QuestOption
    {
        public string Sel_Type_Rad_Str { get { switch (sel_type_rad) { 
            case "single":return "单选"; 
            case "multi":return "多选"; 
            case "select":default: return "选择"; } 
        } }
        public string Text_Str_DP_Str
        {
            get
            {
                switch (text_str_dp)
                {
                    case "none": return "不限制";
                    case "date": return "日期";
                    case "num": return "数字";
                    case "email":
                    default: return "邮件";
                }
            }
        }
        //选择问题区
        public string sel_type_rad = "";//radio,checkbox,select
        public string sel_num_dp = "";//每行显示数
        public string sel_op_body = "";//选项区
        //---------
        public string text_type_rad = "";//text,textarea
        public string text_str_dp = "";//none,date,num,email
        public void FillByForm(System.Web.HttpRequest req)
        {
            sel_type_rad = req["sel_type_rad"];
            sel_num_dp = req["sel_num_dp"];
            sel_op_body = req["sel_text"];
            text_type_rad = req["text_type_rad"];
            text_str_dp = req["text_str_dp"];
        }
    }
}