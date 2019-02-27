using System;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// M_Survey 的摘要说明
/// </summary>

namespace ZoomLa.Model
{
    public class M_Survey:M_Base
    {
        public M_Survey()
        {
            this.SurveyID = 0;
            this.SurveyName = "";
            this.Description = "";
            this.CreateDate = DateTime.Now;
            this.StartTime = DateTime.Now;
            this.EndTime = DateTime.Now;
            this.IsOpen = false;
            this.NeedLogin = false;
            this.SurType = 1;
            this.IsNull = false;
            this.IsStatus = false;
            this.IsCheck = false;

        }

        public M_Survey(bool value):this()
        {
            this.IsNull = value;
        }
        /// <summary>
        /// 调查问卷ID
        /// </summary>
        public int SurveyID { get; set; }
        /// <summary>
        /// 调查问卷名称
        /// </summary>
        public string SurveyName { get; set; }
        /// <summary>
        /// 调查问卷描述 
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// 开始日期
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 结束日期
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// IP 提交次数限制,为0不限
        /// </summary>
        public int IPRepeat { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsOpen { get; set; }
        /// <summary>
        /// 是否审核
        /// </summary>
        public bool IsStatus { get; set; }
        /// <summary>
        /// 是否登录后才能参与问卷调查
        /// </summary>
        public bool NeedLogin { get; set; }
        /// <summary>
        /// 问券调查类型 1-投票 2-问券 3-报名系统
        /// </summary>
        public int SurType { get; set; }
        /// <summary>
        /// 问券调查类型 1-投票 2-问券 3-报名系统
        /// </summary>
        public bool IsCheck { get; set; }

        /// <summary>
        /// 是否空实例
        /// </summary>
        public bool IsNull { get; private set; }
        /// <summary>
        /// 0:不显示，1：显示
        /// </summary>
        public int IsShow
        {
            get;
            set;
        }
        public override string TbName { get { return "ZL_Survey"; } }
        public override string PK { get { return "Surveyid"; } }
        /// <summary>
        /// 返回实体列表数组
        /// </summary>
        /// <returns>String[]</returns>
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"Surveyid","Int","4"},
                                  {"Surveyname","NVarChar","255"},
                                  {"Description","Text","400"},
                                  {"Createdate","DateTime","8"}, 
                                  {"StartTime","DateTime","8"}, 
                                  {"Endtime","DateTime","8"}, 
                                  {"Isopen","Bit","1"}, 
                                  {"NeedLogin","Bit","1"}, 
                                  {"Surtype","Int","4"}, 
                                  {"Iprepeat","Int","4"},
                                  {"IsStatus","Bit","1"},
                                  {"IsCheck","Bit","1"},
                                  {"IsShow","Int","4"}
                                 };
            return Tablelist; 
        }
        public SqlParameter[] GetParameters(M_Survey model)
        {
            string[,] strArr = FieldList();
            SqlParameter[] sp = new SqlParameter[strArr.Length];

            for (int i = 0; i < strArr.GetLength(0); i++)
            {
                sp[i] = new SqlParameter("@" + strArr[i, 0], (SqlDbType)Enum.Parse(typeof(SqlDbType), strArr[i, 1]), Convert.ToInt32(strArr[i, 2]));
            }
            sp[0].Value = model.SurveyID;
            sp[1].Value = model.SurveyName;
            sp[2].Value = model.Description;
            sp[3].Value = model.CreateDate;
            sp[4].Value = model.StartTime;
            sp[5].Value = model.EndTime;
            sp[6].Value = model.IsOpen;
            sp[7].Value = model.NeedLogin;
            sp[8].Value = model.SurType;
            sp[9].Value = model.IPRepeat;
            sp[10].Value = model.IsStatus;
            sp[11].Value = model.IsCheck;
            sp[12].Value = model.IsShow;
            return sp;
        }
        public M_Survey GetModelFromReader(SqlDataReader rdr)
        {
            M_Survey model = new M_Survey();
            model.SurveyID = ConvertToInt(rdr["Surveyid"]);
            model.SurveyName = rdr["Surveyname"].ToString();
            model.Description = rdr["Description"].ToString();
            model.CreateDate = ConvertToDate(rdr["CreateDate"]);
            model.StartTime = ConvertToDate(rdr["StartTime"]);
            model.EndTime = ConvertToDate(rdr["Endtime"]);
            model.IsOpen = ConverToBool(rdr["IsOpen"].ToString());
            model.NeedLogin = ConverToBool(rdr["NeedLogin"].ToString());
            model.SurType = ConvertToInt(rdr["Surtype"]);
            model.IPRepeat = ConvertToInt(rdr["Iprepeat"]);
            model.IsStatus = ConverToBool(rdr["IsStatus"].ToString());
            model.IsCheck = ConverToBool(rdr["IsCheck"].ToString());
            model.IsShow = ConvertToInt(rdr["IsShow"]);
            rdr.Dispose();
            return model;
        }
    }
}

