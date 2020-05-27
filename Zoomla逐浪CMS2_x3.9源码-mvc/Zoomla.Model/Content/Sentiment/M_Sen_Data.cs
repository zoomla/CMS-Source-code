using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
//采集到的数据模型

namespace ZoomLa.Model.Sentiment
{
    public class M_Sen_Data:M_Base
    {
        public int ID { get; set; }
        public string Title { get; set; }
        /// <summary>
        /// 内容来源(新闻,微博,微信)
        /// </summary>
        public string Source { get; set; }
        /// <summary>
        /// 内容作者
        /// </summary>
        public string Author { get; set; }
        /// <summary>
        /// 内容链接
        /// </summary>
        public string Link { get; set; }
        /// <summary>
        /// 文章发布日期
        /// </summary>
        public DateTime CDate { get; set; }
        /// <summary>
        /// 一月当中的第几天
        /// </summary>
        public int Day { get; set; }
        /// <summary>
        /// 采集日期
        /// </summary>
        public DateTime CollDate = DateTime.Now;
        /// <summary>
        /// 所属任务ID
        /// </summary>
        public int TaskID { get; set; }
        /// <summary>
        /// 所属任务信息
        /// </summary>
        public string TaskInfo { get; set; }

        public override string TbName { get { return "ZL_Sen_Data"; } }

        public override string[,] FieldList()
        {
            string[,] Tablelist = {
        		        		{"ID","Int","4"},
        		        		{"Title","NVarChar","400"},
        		        		{"Source","NVarChar","200"},
        		        		{"Author","NVarChar","200"},
        		        		{"Link","NVarChar","2000"},
        		        		{"CDate","DateTime","4"},
        		        		{"Day","Int","4"},
        		        		{"CollDate","DateTime","8"},
        		        		{"TaskID","Int","4"},
                                {"TaskInfo","NVarChar","2000"}
        };
            return Tablelist;
        }

        public SqlParameter[] GetParameters(M_Sen_Data model)
        {
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.Title;
            sp[2].Value = model.Source;
            sp[3].Value = model.Author;
            sp[4].Value = model.Link;
            sp[5].Value = model.CDate;
            sp[6].Value = model.Day;
            sp[7].Value = model.CollDate;
            sp[8].Value = model.TaskID;
            sp[9].Value = model.TaskInfo;
            return sp;
        }
        public M_Sen_Data GetModelFromReader(SqlDataReader rdr)
        {
            M_Sen_Data model = new M_Sen_Data();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.Title = rdr["Title"].ToString();
            model.Source = rdr["Source"].ToString();
            model.Author = rdr["Author"].ToString();
            model.Link = rdr["Link"].ToString();
            model.CDate = ConvertToDate(rdr["CDate"]);
            model.Day = ConvertToInt(rdr["Day"]);
            model.CollDate = ConvertToDate(rdr["CollDate"]);
            model.TaskID = ConvertToInt(rdr["TaskID"]);
            model.TaskInfo = ConverToStr(rdr["TaskInfo"]);
            rdr.Close();
            return model;
        }
    }
}
