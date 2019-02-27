using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;

namespace ZoomLa.Model
{
    public class M_Baike:M_Base
    {
        public int ID { get; set; }
        /// <summary>
        /// 流水号,同一个内容不同的版本,流水号相同,拥有同一流水号(从最初的地方拷贝)
        /// </summary>
        public string Flow { get; set; }
        /// <summary>
        /// 原为正在编辑的原始对象(Disuse)
        /// </summary>
        public int EditID { get; set; }
        /// <summary>
        /// 指向上一版本的BaikeID(Disuse)
        /// </summary>
        public int OldID { get; set; }
        /// <summary>
        /// 词条
        /// </summary>
        public string Tittle { get; set; }

        /// <summary>
        /// 创建人ID
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime AddTime { get; set; }

        /// <summary>
        /// 状态：0未审核，1：通过
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Contents { get; set; }

        /// <summary>
        /// 参考资料(Json类型,存索引网站的资料)
        /// </summary>
        public string Reference { get; set; }

        /// <summary>
        /// 词条标签(,号隔开可多个)
        /// </summary>
        public string Btype { get; set; }

        /// <summary>
        /// 基本信息,Json类型
        /// </summary>
        public string Extend { get; set; }

        /// <summary>
        /// 推荐 1：推荐 2：特色
        /// </summary>
        public int Elite { get; set; }

        /// <summary>
        /// 概述图片
        /// </summary>
        public string BriefImg { get; set; }
        /// <summary>
        /// 概述内容
        /// </summary>
        public string Brief { get; set; }

        /// <summary>
        /// 词条分类(存名称,仅一个)
        /// </summary>
        public string Classification { get; set; }

        /// <summary>
        /// 编辑次数
        /// </summary>
        public int Editnumb { get; set; }
        /// <summary>
        /// 分类联动IDS
        /// </summary>
        public string GradeIDS { get; set; }
        /// <summary>
        /// 当前所用的版本
        /// </summary>
        public string VerStr { get; set; }
        private string _tbname = "ZL_Baike";
        public override string TbName { get { return _tbname; } set { _tbname = value; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"ID","Int","4"}, 
                                  {"Tittle","NVarChar","100"}, 
                                  {"UserId","Int","4"}, 
                                  {"UserName","NVarChar","255"}, 
                                  {"AddTime","DateTime","8"}, 
                                  {"Status","Int","4"}, 
                                  {"UpdateTime","DateTime","8"}, 
                                  {"Contents","NText","80000"}, 
                                  {"Reference","NText","20000"}, 
                                  {"Btype","NVarChar","255"}, 
                                  {"Extend","NText","20000"}, 
                                  {"Elite","Int","4"},
                                  {"Brief","NText","20000"},
                                  {"Classification","NVarChar","50"},
                                  {"Editnumb","Int","4"},
                                  {"GradeIDS","VarChar","1000"},
                                  {"BriefImg","NVarChar","500"},
                                  {"Flow","NVarChar","500"},
                                  {"OldID","Int","4"},
                                  {"VerStr","NVarChar","500"}
                                 };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_Baike model = this;
            if (AddTime <= DateTime.MinValue) { AddTime = DateTime.Now; }
            if (UpdateTime <= DateTime.MinValue) { UpdateTime = DateTime.Now; }
            if (string.IsNullOrEmpty(Flow)) { Flow = DateTime.Now.ToString("yyyyMMddHHmmssfff"); }
            if (string.IsNullOrEmpty(VerStr)) { VerStr = DateTime.Now.ToString("yyyyMMddHHmmssfff"); }
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.Tittle;
            sp[2].Value = model.UserId;
            sp[3].Value = model.UserName;
            sp[4].Value = model.AddTime;
            sp[5].Value = model.Status;
            sp[6].Value = model.UpdateTime;
            sp[7].Value = model.Contents;
            sp[8].Value = model.Reference;
            sp[9].Value = model.Btype;
            sp[10].Value = model.Extend;
            sp[11].Value = model.Elite;
            sp[12].Value = model.Brief;
            sp[13].Value = model.Classification;
            sp[14].Value = model.Editnumb;
            sp[15].Value = model.GradeIDS;
            sp[16].Value = model.BriefImg;
            sp[17].Value = model.Flow;
            sp[18].Value = model.OldID;
            sp[19].Value = model.VerStr;
            return sp;
        }
        public M_Baike GetModelFromReader(DbDataReader rdr)
        {
            M_Baike model = new M_Baike();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.Tittle = ConverToStr(rdr["Tittle"]);
            model.UserId = ConvertToInt(rdr["UserId"]);
            model.UserName = ConverToStr(rdr["UserName"]);
            model.AddTime = ConvertToDate(rdr["AddTime"]);
            model.Status = ConvertToInt(rdr["Status"]);
            model.UpdateTime = ConvertToDate(rdr["UpdateTime"]);
            model.Contents = ConverToStr(rdr["Contents"]);
            model.Reference = ConverToStr(rdr["Reference"]);
            model.Btype = ConverToStr(rdr["Btype"]);
            model.Extend = ConverToStr(rdr["Extend"]);
            model.Elite = ConvertToInt(rdr["Elite"]);
            model.Brief = ConverToStr(rdr["Brief"]);
            model.BriefImg = ConverToStr(rdr["BriefImg"]);
            model.Classification = ConverToStr(rdr["Classification"]);
            model.Editnumb = ConvertToInt(rdr["Editnumb"]);
            model.GradeIDS = ConverToStr(rdr["GradeIDS"]);
            model.Flow = ConverToStr(rdr["Flow"]);
            model.OldID = ConvertToInt(rdr["OldID"]);
            model.VerStr = ConverToStr(rdr["VerStr"]);
            rdr.Close();
            return model;
        }
    }
}
