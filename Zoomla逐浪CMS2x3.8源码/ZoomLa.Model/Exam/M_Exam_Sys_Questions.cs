using System;
using System.Data.SqlClient;
using System.Data;
namespace ZoomLa.Model
{
    [Serializable]
    public class M_Exam_Sys_Questions:M_Base
    {
        public void M_Exam_Sys_Papers()
        {
            IsBig = 0;
            IsShare = 1;
            IsSystem = 0;
            IsSmall = 0;
        }
        public int p_id { get; set; }
        /// <summary>
        /// 试题标题
        /// </summary>
        public string p_title { get; set; }
        /// <summary>
        /// 基础(0.91-1.00)容易(0.81-0.90)中等(0.51-0.80)偏难(0.31-0.50)极难(0.01-0.30)
        /// </summary>
        public double p_Difficulty { get; set; }
        public static string GetTypeStr(int type)
        {
            switch (type)
            {
                case 0:
                    return "单选题";
                case 1:
                    return "多选题";
                case 2:
                    return "填空题";
                case 3:
                    return "选择题";
                case 4:
                    return "完形填空";
                case 10:
                    return "大题";
                default:
                    return "单选题";
            }
        }
        
        /// <summary>
        /// 试题所属(节点|分类)
        /// </summary>
        public int p_Class { get; set; }
        /// <summary>
        /// 所属年级
        /// </summary>
        public int p_Views { get; set; }
        /// <summary>
        /// 作者
        /// </summary>
        public string p_Inputer { get; set; }
        public int p_Type { get; set; }
        /// <summary>
        /// 考点名称(Disuse)
        /// </summary>
        public int p_Knowledge { get; set; }
        /// <summary>
        /// 答案
        /// </summary>
        public string p_Answer { get; set; }
        /// <summary>
        /// 试题内容,如果是大题,则存ids
        /// </summary>
        public string p_Content { get; set; }
        /// <summary>
        /// 试题附带的json信息,如大题,里面存了排序等
        /// </summary>
        public string Qinfo { get; set; }
        public DateTime p_CreateTime { get; set; }
        /// <summary>
        /// 所属试卷ID
        /// </summary>
        public int Paper_Id { get; set; }
        /// <summary>
        /// 试卷排序
        /// </summary>
        public int p_Order { get; set; }
        /// <summary>
        /// 暂未知
        /// </summary>
        public string p_Shipin { get; set; }
        /// <summary>
        /// 选项Json信息
        /// </summary>
        public string p_Optioninfo { get; set; }
        /// <summary>
        /// 选项个数
        /// </summary>
        public int p_ChoseNum { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        public string p_shuming { get; set; }
        /// <summary>
        /// 缺省分数
        /// </summary>
        public float p_defaultScores { get; set; }
        /// <summary>
        /// 0:小题,1:大题(用于包含小题)
        /// </summary>
        public int IsBig { get; set; }
        /// <summary>
        /// 父题ID(组合题所属小题)
        /// </summary>
        public int parentId { get; set; }
        /// <summary>
        /// 题目解析
        /// </summary>
        public string Jiexi{ get;set;}
        public QType MyQType { get { return (QType)p_Type; } }
        /// <summary>
        /// 关键词=知识点
        /// </summary>
        public string Tagkey { get; set; }
        /// <summary>
        /// 后台添加的试题,UserID=0
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// 是否开放共享 1:开放,0:仅自己(主用于用户中心)
        /// </summary>
        public int IsShare { get; set; }
        /// <summary>
        /// 是否系统添加的试题1:是,0:否
        /// (是否后台添加,用于可能的试题篮只允许系统试题)
        /// </summary>
        public int IsSystem { get; set; }
        /// <summary>
        /// 所属教材版本,其值在数据字典中定义
        /// </summary>
        public int Version { get; set; }
        /// <summary>
        /// 是否小题(1:小题)
        /// </summary>
        public int IsSmall { get; set; }
        public string LargeContent { get; set; }

        /// <summary>
        /// 单选,多选,填空,解答,完形填空,大题
        /// </summary>
        public enum QType { Radio = 0, Multi = 1, FillBlank = 2, Answer = 3,FillTextBlank=4,Big=10 }
        public override string PK { get { return "p_id"; } }
        public override string TbName { get { return "ZL_Exam_Sys_Questions"; } }
        public static string OptionDir = "/Log/Storage/Exam/Options/";//id.opt
        public string GetOPDir() { return OptionDir + p_id + ".opt"; }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"p_id","Int","4"},
                                  {"p_title","NVarChar","255"},
                                  {"p_Difficulty","Money","20"},
                                  {"p_Class","Int","4"},
                                  {"p_Views","Int","4"},
                                  {"p_Inputer","NVarChar","255"},
                                  {"p_Type","Int","4"},
                                  {"p_Knowledge","Int","4"},
                                  {"p_Answer","NText","3000"},
                                  {"p_Content","NText","40000"},
                                  {"p_CreateTime","DateTime","8"},
                                  {"Paper_Id","Int","4"},
                                  {"p_Order","Int","4"},
                                  {"p_Shipin","NVarChar","255"},
                                  {"p_Optioninfo","NText","20000"},
                                  {"p_ChoseNum","Int","4"},
                                  {"p_shuming","NText","30000"},
                                  {"p_defaultScores","Float","8"},
                                  {"IsToShare","Int","4"},
                                  {"parentId","Int","4"},
                                  {"Jiexi","NText","30000"},
                                  {"Tagkey","NVarChar","1000" },
                                  {"UserID","Int","4"},
                                  {"QInfo","NVarChar","4000"},
                                  {"IsShare","Int","4"},
                                  {"IsSystem","Int","4"},
                                  {"Version","Int","4"},
                                  {"LargeContent","NText","40000"},
                                  {"IsSmall","Int","4"}  
                                 };

            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_Exam_Sys_Questions model = this;
            if (p_CreateTime <= DateTime.MinValue) { p_CreateTime = DateTime.Now; }
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.p_id;
            sp[1].Value = model.p_title;
            sp[2].Value = model.p_Difficulty;
            sp[3].Value = model.p_Class;
            sp[4].Value = model.p_Views;
            sp[5].Value = model.p_Inputer;
            sp[6].Value = model.p_Type;
            sp[7].Value = model.p_Knowledge;
            sp[8].Value = model.p_Answer;
            sp[9].Value = model.p_Content;
            sp[10].Value = model.p_CreateTime;
            sp[11].Value = model.Paper_Id;
            sp[12].Value = model.p_Order;
            sp[13].Value = model.p_Shipin;
            sp[14].Value = model.p_Optioninfo;
            sp[15].Value = model.p_ChoseNum;
            sp[16].Value = model.p_shuming;
            sp[17].Value = model.p_defaultScores;
            sp[18].Value = model.IsBig;
            sp[19].Value = model.parentId;
            sp[20].Value = model.Jiexi;
            sp[21].Value = model.Tagkey;
            sp[22].Value = model.UserID;
            sp[23].Value = model.Qinfo;
            sp[24].Value = model.IsShare;
            sp[25].Value = model.IsSystem;
            sp[26].Value = model.Version;
            sp[27].Value = model.LargeContent;
            sp[28].Value = model.IsSmall;
            return sp;
        }
        public M_Exam_Sys_Questions GetModelFromReader(SqlDataReader rdr)
        {
            M_Exam_Sys_Questions model = new M_Exam_Sys_Questions();
            model.p_id = ConvertToInt(rdr["p_id"]);
            model.p_title = ConverToStr(rdr["p_title"]);
            model.p_Difficulty = ConverToDouble(rdr["p_Difficulty"]);
            model.p_Class = ConvertToInt(rdr["p_Class"]);
            model.p_Views = ConvertToInt(rdr["p_Views"]);
            model.p_Inputer = ConverToStr(rdr["p_Inputer"]);
            model.p_Type = ConvertToInt(rdr["p_Type"]);
            model.p_Knowledge = ConvertToInt(rdr["p_Knowledge"]);
            model.p_Answer = ConverToStr(rdr["p_Answer"]);
            model.p_Content = ConverToStr(rdr["p_Content"]);
            model.p_CreateTime = ConvertToDate(rdr["p_CreateTime"]);
            model.Paper_Id = ConvertToInt(rdr["Paper_Id"]);
            model.p_Order = ConvertToInt(rdr["p_Order"]);
            model.p_Shipin = ConverToStr(rdr["p_Shipin"]);
            model.p_Optioninfo = ConverToStr(rdr["p_Optioninfo"]);
            model.p_ChoseNum = ConvertToInt(rdr["p_ChoseNum"]);
            model.p_shuming = ConverToStr(rdr["p_shuming"]);
            model.p_defaultScores = ConvertToInt(rdr["p_defaultScores"]);
            model.IsBig = ConvertToInt(rdr["IsToShare"]);
            model.parentId = ConvertToInt(rdr["parentId"]);
            model.Jiexi = ConverToStr(rdr["Jiexi"]);
            model.Tagkey = ConverToStr(rdr["Tagkey"]);
            model.UserID = ConvertToInt(rdr["UserID"]);
            model.Qinfo = ConverToStr(rdr["Qinfo"]);
            model.IsShare = ConvertToInt(rdr["IsShare"]);
            model.IsSystem = ConvertToInt(rdr["IsSystem"]);
            model.Version = ConvertToInt(rdr["Version"]);
            model.LargeContent = ConverToStr(rdr["LargeContent"]);
            model.IsSmall = ConvertToInt(rdr["IsSmall"]);
            rdr.Close();
            return model;
        }
    }
}



