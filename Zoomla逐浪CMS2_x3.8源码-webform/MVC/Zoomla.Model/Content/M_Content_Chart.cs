using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ZoomLa.Model
{
    public class M_Content_Chart : M_Base
    {
        public int ID { get; set; }
        public string TName { get; set; }
        /// <summary>
        /// ChartTitle的序列化字符串
        /// </summary>
        public string ChartTitle { get; set; }
        /// <summary>
        /// 工具条配置
        /// </summary>
        public string ToolBox { get; set; }
        /// <summary>
        /// 数据包
        /// </summary>
        public string Package { get; set; }
        /// <summary>
        /// 生成的option
        /// </summary>
        public string option { get; set; }
        public string SType { get; set; }
        public string GetTypeStr(string stype = "")
        {
            if (string.IsNullOrEmpty(stype)) stype = SType;
            switch (stype.ToLower())
            {
                case "bar":
                    return "柱图";
                case "funnel":
                    return "漏斗图";
                case "line":
                    return "折线图";
                case "pie":
                    return "饼图";
                case "dash":
                    return "仪表盘";
                case "scatter":
                    return "散点图";
                case "circle":
                    return "气泡图";
                case "map":
                    return "地图";
                default:
                    return stype;
            }
        }
        public int AdminID { get; set; }
        public int UserID { get; set; }
        //标识图标的显示方式
        public string Tag { get; set; }
        public DateTime CDate = DateTime.Now;
        public override string TbName { get { return "ZL_Content_Chart"; } }
        public override string[,] FieldList()
        {
            string[,] field = {
                                  {"ID","Int","4"},
                                  {"TName","NVarChar","100"},
                                  {"SType","NVarChar","100"},
                                  {"option","NText","10000"},
                                  {"AdminID","Int","4"},
                                  {"UserID","Int","4"},
                                  {"CDate","DateTime","10000"},
                                  {"ChartTitle","NText","10000"},
                                  {"ToolBox","NVarChar","1000"},
                                  {"Package","NText","10000"},
                                  {"Tag","NVarChar","100"}
                              };
            return field;
        }
        public override SqlParameter[] GetParameters()
        {
            M_Content_Chart model = this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.TName;
            sp[2].Value = model.SType;
            sp[3].Value = model.option;
            sp[4].Value = model.AdminID;
            sp[5].Value = model.UserID;
            sp[6].Value = model.CDate;
            sp[7].Value = model.ChartTitle;
            sp[8].Value = model.ToolBox;
            sp[9].Value = model.Package;
            sp[10].Value = model.Tag;
            return sp;
        }
        public M_Content_Chart GetModelFromReader(SqlDataReader rdr)
        {
            M_Content_Chart model = new M_Content_Chart();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.TName = ConverToStr(rdr["TName"]);
            model.SType = ConverToStr(rdr["SType"]);
            model.option = ConverToStr(rdr["option"]);
            model.AdminID = ConvertToInt(rdr["AdminID"]);
            model.UserID = ConvertToInt(rdr["UserID"]);
            model.CDate = ConvertToDate(rdr["CDate"]);
            model.ChartTitle = ConverToStr(rdr["ChartTitle"]);
            model.ToolBox = ConverToStr(rdr["ToolBox"]);
            model.Package = ConverToStr(rdr["Package"]);
            model.Tag = ConverToStr(rdr["Tag"]);
            rdr.Close();
            return model;
        }
    }
}
