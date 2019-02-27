using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ZoomLa.Model.Sentiment
{
    public class M_Sen_Task:M_Base
    {
        public int ID { get; set; }
        /// <summary>
        /// 所属栏目
        /// </summary>
        public int Pid { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 搜索方式
        /// </summary>
        public string SType { get; set; }
        /// <summary>
        /// 来源,如新网,网站
        /// </summary>
        public string Source { get; set; }
        /// <summary>
        /// 搜索条件
        /// </summary>
        public string Condition { get; set; }
        /// <summary>
        /// 匹配词
        /// </summary>
        public string SuitKey { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CDate = DateTime.Now;
        /// <summary>
        /// 状态 0:禁用,1:启用
        /// </summary>
        public int Status { get; set; }

        public override string TbName { get { return "ZL_Sen_Task"; } }

        public override string[,] FieldList()
        {
            string[,] Tablelist ={
                  {"ID","Int","4"},
                  {"Pid","Int","4"},
                  {"Title","NVarChar","200"},
                  {"Source","NVarChar","200"},
                  {"SType","VarChar","20"},
                  {"Condition","NVarChar","1000"},
                  {"SuitKey","NVarChar","200"},
                  {"CDate","DateTime","8"},
                  {"Status","Int","4"},
            };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_Sen_Task model=this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.Pid;
            sp[2].Value = model.Title;
            sp[3].Value = model.Source;
            sp[4].Value = model.SType;
            sp[5].Value = model.Condition;
            sp[6].Value = model.SuitKey;
            sp[7].Value = model.CDate;
            sp[8].Value = model.Status;
            return sp;
        }
        public M_Sen_Task GetModelFromReader(SqlDataReader rdr)
        {
            M_Sen_Task model = new M_Sen_Task();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.Pid = ConvertToInt(rdr["Pid"]);
            model.Title = rdr["Title"].ToString();
            model.Source = rdr["Source"].ToString();
            model.SType = ConverToStr(rdr["SType"]);
            model.Condition = ConverToStr(rdr["Condition"]);
            model.SuitKey = ConverToStr(rdr["SuitKey"]);
            model.CDate = ConvertToDate(rdr["CDate"]);
            model.Status = ConvertToInt(rdr["Status"]);
            rdr.Close();
            return model;
        }    
    }
}
