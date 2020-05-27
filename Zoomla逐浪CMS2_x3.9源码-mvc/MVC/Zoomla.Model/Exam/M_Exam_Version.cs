using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ZoomLa.Model.Exam
{
    public class M_Exam_Version : M_Base
    {
        public int ID { get; set; }
        /// <summary>
        /// 所属的父教材目录
        /// </summary>
        public int Pid { get; set; }
        /// <summary>
        /// 科目
        /// </summary>
        public int NodeID { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int OrderID { get; set; }
        /// <summary>
        /// 年级
        /// </summary>
        public int Grade { get; set; }
        /// <summary>
        /// 版本名称
        /// </summary>
        public string VersionName { get; set; }
        /// <summary>
        /// 版本时间
        /// </summary>
        public string VersionTime { get; set; }
        public string Alias { get; set; }
        /// <summary>
        /// 册序
        /// </summary>
        public string Volume { get; set; }
        /// <summary>
        /// 管理员ID
        /// </summary>
        public int AdminID { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID { get; set; }
        public DateTime CDate { get; set; }
        public string Remind { get; set; }
        /// <summary>
        /// 添加人,可自定义
        /// </summary>
        public string Inputer { get; set; }
        /// <summary>
        /// 章元名称,章-->节-->课
        /// </summary>
        public string Chapter { get; set; }
        /// <summary>
        /// 节名称
        /// </summary>
        public string SectionName { get; set; }
        /// <summary>
        /// 课名称
        /// </summary>
        public string CourseName { get; set; }
        /// <summary>
        /// 知识点IDS
        /// </summary>
        public string Knows { get; set; }
        /// <summary>
        /// 售价
        /// </summary>
        public double Price { get; set; }
        public override string TbName { get { return "ZL_Exam_Version"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                {"ID","Int","4"},
                                {"NodeID","Int","4"},
                                {"Grade","Int","4"},
                                {"VersionName","NVarChar","200"},
                                {"VersionTime","NVarChar","500"},
                                {"Alias","NVarChar","200"},
                                {"Volume","NVarChar","200"},
                                {"AdminID","Int","4"},
                                {"CDate","DateTime","8"},
                                {"Remind","NVarChar","200"},
                                {"Inputer","NVarChar","200"},
                                {"SectionName","NVarChar","200"},
                                {"CourseName","NVarChar","200"},
                                {"Knows","NVarChar","4000"},
                                {"Price","Money","16"},
                                {"UserID","Int","4"},
                                {"Pid","Int","4"},
                                {"Chapter","NVarChar","200"},
                                {"OrderID","Int","4" }
        };
            return Tablelist;
        }
        public SqlParameter[] GetParameters(M_Exam_Version model)
        {
            if (model.CDate <= DateTime.MinValue) { model.CDate = DateTime.Now; }
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.NodeID;
            sp[2].Value = model.Grade;
            sp[3].Value = model.VersionName;
            sp[4].Value = model.VersionTime;
            sp[5].Value = model.Alias;
            sp[6].Value = model.Volume;
            sp[7].Value = model.AdminID;
            sp[8].Value = model.CDate;
            sp[9].Value = model.Remind;
            sp[10].Value = model.Inputer;
            sp[11].Value = model.SectionName;
            sp[12].Value = model.CourseName;
            sp[13].Value = model.Knows;
            sp[14].Value = model.Price;
            sp[15].Value = model.UserID;
            sp[16].Value = model.Pid;
            sp[17].Value = model.Chapter;
            sp[18].Value = model.OrderID;
            return sp;
        }
        public M_Exam_Version GetModelFromReader(SqlDataReader rdr)
        {
            M_Exam_Version model = new M_Exam_Version();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.NodeID = ConvertToInt(rdr["NodeID"]);
            model.Grade = ConvertToInt(rdr["Grade"]);
            model.VersionName = ConverToStr(rdr["VersionName"]);
            model.VersionTime = ConverToStr(rdr["VersionTime"]);
            model.Alias = ConverToStr(rdr["Alias"]);
            model.Volume = ConverToStr(rdr["Volume"]);
            model.AdminID = ConvertToInt(rdr["AdminID"]);
            model.CDate = ConvertToDate(rdr["CDate"]);
            model.Remind = ConverToStr(rdr["Remind"]);
            model.Inputer = ConverToStr(rdr["Inputer"]);
            model.SectionName = ConverToStr(rdr["SectionName"]);
            model.CourseName = ConverToStr(rdr["CourseName"]);
            model.Knows = ConverToStr(rdr["Knows"]);
            model.Price = ConverToDouble(rdr["Price"]);
            model.UserID = ConvertToInt(rdr["UserID"]);
            model.Pid = ConvertToInt(rdr["Pid"]);
            model.Chapter = ConverToStr(rdr["Chapter"]);
            model.OrderID = ConvertToInt(rdr["OrderID"]);
            rdr.Close();
            return model;
        }
    }
}
