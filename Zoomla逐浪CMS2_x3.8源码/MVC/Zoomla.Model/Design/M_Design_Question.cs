using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ZoomLa.Model.Design
{
    public class M_Design_Question : M_Base
    {
        public int ID { get; set; }
        /// <summary>
        /// 所属问卷ID
        /// </summary>
        public int AskID { get; set; }
        /// <summary>
        /// 问题标题
        /// </summary>
        public string QTitle { get; set; }
        /// <summary>
        /// 问题内容(选填)
        /// </summary>
        public string QContent { get; set; }
        /// <summary>
        /// 问题选项(单选|多选|排序)
        /// </summary>
        public string QOption { get; set; }
        /// <summary>
        /// 问题类型
        /// </summary>
        public string QType { get; set; }
        /// <summary>
        /// 问题标识,用于类型下再区分(日期|生日|性别)
        /// </summary>
        public string QFlag { get; set; }
        /// <summary>
        /// 是否必填
        /// </summary>
        public bool Required { get; set; }
        public DateTime CDate { get; set; }
        /// <summary>
        /// 从1开始,ASC 
        /// </summary>
        public int OrderID { get; set; }
        public int CUser { get; set; }
        public override string TbName { get { return "ZL_Design_Question"; } }
        public override string PK { get { return "ID"; } }
        public M_Design_Question() 
        {
            Required = false;
            QFlag = "";
            QContent = "";
        }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
        		        		{"ID","Int","4"},
        		        		{"AskID","Int","4"},
        		        		{"QTitle","NVarChar","2000"},
        		        		{"QContent","NVarChar","4000"},
        		        		{"QOption","NText","6000"},
        		        		{"QType","NVarChar","200"},
        		        		{"QFlag","NVarChar","200"},
        		        		{"Required","Int","4"},
        		        		{"CDate","DateTime","8"},
                                {"OrderID","Int","4"},
                                {"CUser","Int","4"}
        };
            return Tablelist;
        }

        public override SqlParameter[] GetParameters()
        {
            M_Design_Question model = this;
            if (model.CDate <= DateTime.MinValue) { model.CDate = DateTime.Now; }
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.AskID;
            sp[2].Value = model.QTitle;
            sp[3].Value = model.QContent;
            sp[4].Value = model.QOption;
            sp[5].Value = model.QType;
            sp[6].Value = model.QFlag;
            sp[7].Value = model.Required ? 1 : 0;
            sp[8].Value = model.CDate;
            sp[9].Value = model.OrderID;
            sp[10].Value = model.CUser;
            return sp;
        }
        public M_Design_Question GetModelFromReader(DbDataReader rdr)
        {
            M_Design_Question model = new M_Design_Question();
            model.ID = ConvertToInt(rdr["ID"]);
            model.AskID = ConvertToInt(rdr["AskID"]);
            model.QTitle = ConverToStr(rdr["QTitle"]);
            model.QContent = ConverToStr(rdr["QContent"]);
            model.QOption = ConverToStr(rdr["QOption"]);
            model.QType = ConverToStr(rdr["QType"]);
            model.QFlag = ConverToStr(rdr["QFlag"]);
            model.Required = ConverToBool(rdr["Required"]);
            model.CDate = ConvertToDate(rdr["CDate"]);
            model.OrderID = ConvertToInt(rdr["OrderID"]);
            model.CUser = ConvertToInt(rdr["CUser"]);
            rdr.Close();
            return model;
        }
    }
}
