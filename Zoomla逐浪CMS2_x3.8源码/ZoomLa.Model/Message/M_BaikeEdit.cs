using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;
namespace ZoomLa.Model
{
    public class M_BaikeEdit :M_Baike
    {
        /// <summary>
        /// 审核的管理员ID
        /// </summary>
        public int AdminID { get; set; }
        /// <summary>
        /// 用户备注
        /// </summary>
        public string UserRemind { get; set; }
        /// <summary>
        /// 管理员备注
        /// </summary>
        public string AdminRemind { get; set; }
        /// <summary>
        /// 审核日期
        /// </summary>
        public DateTime AuditDate { get; set; }
        private string _tbname = "ZL_Baike_Edit";
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
                                  {"EditID","Int","4"},
                                  {"VerStr","NVarChar","500"},
                                  {"AdminID","Int","4"},
                                  {"UserRemind","NVarChar","1000"},
                                  {"AdminRemind","NVarChar","1000"},
                                  {"AuditDate","DateTime","8"},
                                 };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_BaikeEdit model = this;
            if (AddTime <= DateTime.MinValue) { AddTime = DateTime.Now; }
            if (UpdateTime <= DateTime.MinValue) { UpdateTime = DateTime.Now; }
            if (AuditDate <= DateTime.MinValue) { AuditDate = DateTime.Now; }
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
            sp[19].Value = model.EditID;
            sp[20].Value = model.VerStr;
            sp[21].Value = model.AdminID;
            sp[22].Value = model.UserRemind;
            sp[23].Value = model.AdminRemind;
            sp[24].Value = model.AuditDate;
            return sp;
        }
        public new M_BaikeEdit GetModelFromReader(DbDataReader rdr)
        {
            M_BaikeEdit model = new M_BaikeEdit();
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
            model.EditID = ConvertToInt(rdr["EditID"]);
            model.VerStr = ConverToStr(rdr["VerStr"]);
            model.AdminID = ConvertToInt(rdr["AdminID"]);
            model.UserRemind = ConverToStr(rdr["UserRemind"]);
            model.AdminRemind = ConverToStr(rdr["AdminRemind"]);
            model.AuditDate = ConvertToDate(rdr["AuditDate"]);
            rdr.Close();
            return model;
        }
    }
}
