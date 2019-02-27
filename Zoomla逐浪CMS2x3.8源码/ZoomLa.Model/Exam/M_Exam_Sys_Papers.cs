using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace ZoomLa.Model
{
    [Serializable]
    public class M_Exam_Sys_Papers : M_Base
    {
        public int id { get; set; }
        /// <summary>
        /// 试卷名称
        /// </summary>
        public string p_name { get; set; }
        /// <summary>
        /// 出题方式：手工，随机
        /// </summary>
        public int p_type { get; set; }
        /// <summary>
        /// 科目分类
        /// </summary>
        public int p_class { get; set; }
        /// <summary>
        /// 考试时间
        /// </summary>
        public double p_UseTime { get; set; }
        /// <summary>
        /// 试卷形式:视频(没有时间限制),手工(需要考试时间)
        /// </summary>
        public int p_Modus { get; set; }
        /// <summary>
        /// 有效时间：开始时间
        /// </summary>
        public DateTime p_BeginTime = DateTime.Now;
        /// <summary>
        /// 有效时间： 结束时间
        /// </summary>
        public DateTime p_endTime = DateTime.Now;
        /// <summary>
        /// 试卷类型:1:考试,2:练习,3:作业
        /// </summary>
        public int p_Style { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        public string p_Remark { get; set; }
        /// <summary>
        /// 试题ID
        /// </summary>
        public string p_QuestionID { get; set; }
        /// <summary>
        /// 阅卷方式
        /// </summary>
        public int p_rtype { get; set; }
        public string QIDS { get; set; }
        public int UserID { get; set; }
        /// <summary>
        /// QuestJson,存储临时分数与排序
        /// </summary>
        public string QuestList { get; set; }
        /// <summary>
        /// 0:不共享,1:共享
        /// </summary>
        public int IsShare { get; set; }
        public double Price { get; set; }
        public string TagKey { get; set; }
        public override string TbName { get { return "ZL_Exam_Sys_Papers"; } }
        public M_Exam_Sys_Papers() 
        {
            IsShare = 1;
        }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"id","Int","4"},
                                  {"p_name","NVarChar","255"},
                                  {"p_type","Int","4"},
                                  {"p_class","Int","4"},
                                  {"p_UseTime","Float","8"},
                                  {"p_Modus","Int","4"},
                                  {"p_BeginTime","DateTime","8"},
                                  {"p_endTime","DateTime","8"},
                                  {"p_Style","Int","4"},
                                  {"p_Remark","NVarChar","500"},
                                  {"p_QuestionID","NVarChar","500"},
                                  {"p_rtype","Int","4"},
                                  {"QIDS","VarChar","2000"},
                                  {"UserID","Int","4"},
                                  {"QuestList","NVarChar","4000"},
                                  {"TagKey","NVarChar","1000" },
                                  {"IsShare","Int","1000" },
                                  {"Price","Money","100" }
                                 };

            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_Exam_Sys_Papers model = this;
            if (model.p_BeginTime <= DateTime.MinValue) { model.p_BeginTime = DateTime.Now; }
            if (model.p_endTime <= DateTime.MinValue) { model.p_endTime = DateTime.Now.AddMonths(2); }
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.id;
            sp[1].Value = model.p_name;
            sp[2].Value = model.p_type;
            sp[3].Value = model.p_class;
            sp[4].Value = model.p_UseTime;
            sp[5].Value = model.p_Modus;
            sp[6].Value = model.p_BeginTime;
            sp[7].Value = model.p_endTime;
            sp[8].Value = model.p_Style;
            sp[9].Value = model.p_Remark;
            sp[10].Value = model.p_QuestionID;
            sp[11].Value = model.p_rtype;
            sp[12].Value = model.QIDS;
            sp[13].Value = model.UserID;
            sp[14].Value = model.QuestList;
            sp[15].Value = model.TagKey;
            sp[16].Value = model.IsShare;
            sp[17].Value = model.Price;
            return sp;
        }
        public M_Exam_Sys_Papers GetModelFromReader(SqlDataReader rdr)
        {
            M_Exam_Sys_Papers model = new M_Exam_Sys_Papers();
            model.id = Convert.ToInt32(rdr["id"]);
            model.p_name = ConverToStr(rdr["p_name"]);
            model.p_type = Convert.ToInt32(rdr["p_type"]);
            model.p_class = Convert.ToInt32(rdr["p_class"]);
            model.p_UseTime = Convert.ToInt64(rdr["p_UseTime"]);
            model.p_Modus = Convert.ToInt32(rdr["p_Modus"]);
            model.p_BeginTime = Convert.ToDateTime(rdr["p_BeginTime"]);
            model.p_endTime = Convert.ToDateTime(rdr["p_endTime"]);
            model.p_Style = Convert.ToInt32(rdr["p_Style"]);
            model.p_Remark = rdr["p_Remark"].ToString();
            model.p_QuestionID = rdr["p_QuestionID"].ToString();
            model.p_rtype = Convert.ToInt32(rdr["p_rtype"]);
            model.QIDS = rdr["QIDS"].ToString();
            model.UserID = ConvertToInt(rdr["UserID"]);
            model.QuestList = ConverToStr(rdr["QuestList"]);
            model.TagKey = rdr["TagKey"].ToString();
            model.IsShare = ConvertToInt(rdr["IsShare"],1);
            model.Price = ConverToDouble(rdr["Price"]);
            rdr.Close();
            return model;
        }
    }
    public class M_Exam_TempQuest 
    {
        public int id = 0;
        public int score = 0;
        public int order = 0;
    }
}
    
