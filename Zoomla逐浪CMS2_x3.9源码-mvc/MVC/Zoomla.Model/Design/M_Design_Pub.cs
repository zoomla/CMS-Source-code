using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ZoomLa.Model.Design
{
    public class M_Design_Pub : M_Base
    {
        public int ID { get; set; }
        /// <summary>
        /// 场景模型Guid
        /// </summary>
        public string H5ID { get; set; }
        /// <summary>
        /// 表单名|标识(预留)
        /// </summary>
        public string FormName { get; set; }
        /// <summary>
        /// 用户提交的表单内容(json格式)
        /// </summary>
        public string FormContent { get; set; }
        /// <summary>
        /// 提交人用户ID|游客ID
        /// </summary>
        public string UserID { get; set; }
        /// <summary>
        /// 提交人名称|别名
        /// </summary>
        public string UserName { get; set; }
        public DateTime CDate { get; set; }
        public string IP { get; set; }
        public override string TbName { get { return "ZL_Design_Pub"; } }
        public override string PK { get { return "ID"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
        		        		{"ID","Int","4"},
        		        		{"H5ID","NVarChar","500"},
        		        		{"FormName","NVarChar","500"},
        		        		{"FormContent","NText","6000"},
        		        		{"UserID","VarChar","50"},
        		        		{"UserName","VarChar","100"},
        		        		{"CDate","DateTime","8"},
                                {"IP","NVarChar","200"}
        };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_Design_Pub model = this;
            if (model.CDate <= DateTime.MinValue) { model.CDate = DateTime.Now; }
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.H5ID;
            sp[2].Value = model.FormName;
            sp[3].Value = model.FormContent;
            sp[4].Value = model.UserID;
            sp[5].Value = model.UserName;
            sp[6].Value = model.CDate;
            sp[7].Value = model.IP;
            return sp;
        }
        public M_Design_Pub GetModelFromReader(DbDataReader rdr)
        {
            M_Design_Pub model = new M_Design_Pub();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.H5ID = ConverToStr(rdr["H5ID"]);
            model.FormName = ConverToStr(rdr["FormName"]);
            model.FormContent = ConverToStr(rdr["FormContent"]);
            model.UserID = ConverToStr(rdr["UserID"]);
            model.UserName = ConverToStr(rdr["UserName"]);
            model.CDate = ConvertToDate(rdr["CDate"]);
            model.IP = ConverToStr(rdr["IP"]);
            rdr.Close();
            return model;
        }
    }
}
