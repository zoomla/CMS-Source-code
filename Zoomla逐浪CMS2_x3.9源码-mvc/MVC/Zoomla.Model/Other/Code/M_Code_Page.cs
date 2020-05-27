using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ZoomLa.Model.Other
{
    public class M_Code_Page : M_Base
    {
        public int ID { get; set; }
        /// <summary>
        /// 页面别名
        /// </summary>
        public string PageAlias { get; set; }
        /// <summary>
        /// 页面名称,根据别名生成
        /// </summary>
        public string PageName { get; set; }
        /// <summary>
        /// 虚拟路径
        /// </summary>
        public string PageUrl { get; set; }
        /// <summary>
        /// 引用模块
        /// </summary>
        public string Models { get; set; }
        /// <summary>
        /// 页面类型
        /// </summary>
        public string PageType { get; set; }
        public int AdminID { get; set; }
        public DateTime CDate { get; set; }
        public string Remind { get; set; }

        public override string TbName { get { return "ZL_Code_Page"; } }
        public M_Code_Page() { CDate = DateTime.Now; }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
        		        		{"ID","Int","4"},
        		        		{"PageAlias","NVarChar","100"},
        		        		{"PageName","NVarChar","100"},
        		        		{"PageUrl","NVarChar","500"},
        		        		{"Models","VarChar","4000"},
        		        		{"PageType","VarChar","4"},
        		        		{"AdminID","Int","4"},
        		        		{"CDate","DateTime","8"},
        		        		{"Remind","NVarChar","1000"}
        };
            return Tablelist;
        }

        public SqlParameter[] GetParameters(M_Code_Page model)
        {
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.PageAlias;
            sp[2].Value = model.PageName;
            sp[3].Value = model.PageUrl;
            sp[4].Value = model.Models;
            sp[5].Value = model.PageType;
            sp[6].Value = model.AdminID;
            sp[7].Value = model.CDate;
            sp[8].Value = model.Remind;
            return sp;
        }
        public M_Code_Page GetModelFromReader(SqlDataReader rdr)
        {
            M_Code_Page model = new M_Code_Page();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.PageAlias = ConverToStr(rdr["PageAlias"]);
            model.PageName = ConverToStr(rdr["PageName"]);
            model.PageUrl = ConverToStr(rdr["PageUrl"]);
            model.Models = ConverToStr(rdr["Models"]);
            model.PageType = ConverToStr(rdr["PageType"]);
            model.AdminID = ConvertToInt(rdr["AdminID"]);
            model.CDate = ConvertToDate(rdr["CDate"]);
            model.Remind = ConverToStr(rdr["Remind"]);
            rdr.Close();
            return model;
        }
    }
}
