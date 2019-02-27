using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ZoomLa.Model.Content
{
    public class M_Content_CR : M_Base
    {
        public int ID { get; set; }
        /// <summary>
        /// 作品ID
        /// </summary>
        public string WorksID { get; set; }
        /// <summary>
        /// 内容表ID
        /// </summary>
        public int GeneralID { get; set; }
        /// <summary>
        /// 关键字
        /// </summary>
        public string KeyWords { get; set; }
        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 应用对应类型，如weibo,blog
        /// </summary>
        public string FromType { get; set; }
        /// <summary>
        /// 文章访问地址
        /// </summary>
        public string FromUrl { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// 添加状态，0为失败，1为成功
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 转载价格
        /// </summary>
        public double RepPrice { get; set; }
        /// <summary>
        /// 素材价格
        /// </summary>
        public double MatPrice { get; set; }
        public override string TbName { get { return "ZL_Content_CR"; } }
        public override string[,] FieldList()
        {
            string[,] fields = {
                                  {"ID","Int","4"},
                                  {"WorksID","NVarChar","50"},
                                  {"GeneralID","Int","4"},
                                  {"KeyWords","NVarChar","50"},
                                  {"Author","NVarChar","50"},
                                  {"Title","NVarChar","50"},
                                  {"Comment","NVarChar","1000"},
                                  {"Type","NVarChar","50"},
                                  {"FromType","NVarChar","50"},
                                  {"FromUrl","NVarChar","50"},
                                  {"CreateDate","DateTime","8"},
                                  {"Status","Int","4"},
                                  {"RepPrice","Money","32"},
                                  {"MatPrice","Money","32"}
                               };
            return fields;
        }
        public override SqlParameter[] GetParameters()
        {
            M_Content_CR model = this;
            if (CreateDate <= DateTime.MinValue) { CreateDate = DateTime.Now; }
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.WorksID;
            sp[2].Value = model.GeneralID;
            sp[3].Value = model.KeyWords;
            sp[4].Value = model.Author;
            sp[5].Value = model.Title;
            sp[6].Value = model.Content;
            sp[7].Value = model.Type;
            sp[8].Value = model.FromType;
            sp[9].Value = model.FromUrl;
            sp[10].Value = model.CreateDate;
            sp[11].Value = model.Status;
            sp[12].Value = model.RepPrice;
            sp[13].Value = model.MatPrice;
            return sp;
        }
        public M_Content_CR GetModelFromReader(DbDataReader rdr)
        {
            M_Content_CR model = new M_Content_CR();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.WorksID = ConverToStr(rdr["WorksID"]);
            model.GeneralID = ConvertToInt("GeneralID");
            model.KeyWords = ConverToStr(rdr["KeyWords"]);
            model.Author = ConverToStr(rdr["Author"]);
            model.Title = ConverToStr(rdr["Title"]);
            model.Content = ConverToStr(rdr["Comment"]);
            model.Type = ConverToStr(rdr["Type"]);
            model.FromType = ConverToStr(rdr["FromType"]);
            model.FromUrl = ConverToStr(rdr["FromUrl"]);
            model.CreateDate = ConvertToDate(rdr["CreateDate"]);
            model.Status = ConvertToInt(rdr["Status"]);
            model.RepPrice = ConverToDouble(rdr["RepPrice"]);
            model.MatPrice = ConverToDouble(rdr["MatPrice"]);
            rdr.Close();
            return model;
        }
    }
}
