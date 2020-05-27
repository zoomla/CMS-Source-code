using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ZoomLa.Model.Shop
{
    public class M_Shop_PrintTlp:M_Base
    {
        public int ID { get; set; }
        /// <summary>
        /// 别名
        /// </summary>
        public string Alias { get; set; }
        /// <summary>
        /// 模板内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remind { get; set; }
        public int AdminID { get; set; }
        public DateTime CDate { get; set; }
        public override string TbName { get { return "ZL_Shop_PrintTlp"; } }
        public override string[,] FieldList()
        {
            string[,] fields = {
                                  {"ID","Int","4" },
                                  {"Alias","NVarChar","50" },
                                  {"Content","NVarChar","1000" },
                                  {"Remind","NVarChar","50" },
                                  {"AdminID","Int","4" },
                                  {"CDate","DateTime","8" }
                               };
            return fields;
        }
        public override SqlParameter[] GetParameters()
        {
            M_Shop_PrintTlp model = this;
            if (CDate <= DateTime.MinValue) { CDate = DateTime.Now; }
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.Alias;
            sp[2].Value = model.Content;
            sp[3].Value = model.Remind;
            sp[4].Value = model.AdminID;
            sp[5].Value = model.CDate;
            return sp;
        }
        public M_Shop_PrintTlp GetModelFromReader(DbDataReader rdr)
        {
            M_Shop_PrintTlp model = new M_Shop_PrintTlp();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.Alias = ConverToStr(rdr["Alias"]);
            model.Content = ConverToStr(rdr["Content"]);
            model.Remind = ConverToStr(rdr["Remind"]);
            model.AdminID = Convert.ToInt32(rdr["AdminID"]);
            model.CDate = ConvertToDate(rdr["CDate"]);
            return model;
        }
    }
}
