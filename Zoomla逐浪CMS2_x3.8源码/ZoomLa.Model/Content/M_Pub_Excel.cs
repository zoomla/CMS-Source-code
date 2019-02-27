using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ZoomLa.Model.Content
{
    public class M_Pub_Excel : M_Base
    {
        public int ID { get; set; }
        /// <summary>
        /// 表名
        /// </summary>
        public string TableName { get; set; }
        /// <summary>
        /// 列名,与Fields对应,无则使用Fields
        /// </summary>
        public string CNames { get; set; }
        /// <summary>
        /// 需要导出的字段名
        /// </summary>
        public string Fields { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public int UserID { get; set; }
        public DateTime CreateTime { get; set; }
        public override string TbName { get { return "ZL_Pub_Excel"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
        		        		{"ID","Int","4"},
        		        		{"TableName","NVarChar","50"},
                                {"CNames","NText","4000"},
        		        		{"Fields","NText","4000"},
        		        		{"UserID","Int","4"},
                                {"CreateTime","DateTime","8"}
        };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_Pub_Excel model = this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.TableName;
            sp[2].Value = model.CNames;
            sp[3].Value = model.Fields;
            sp[4].Value = model.UserID;
            sp[5].Value = model.CreateTime;
            return sp;
        }
        public M_Pub_Excel GetModelFromReader(SqlDataReader rdr)
        {
            M_Pub_Excel model = new M_Pub_Excel();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.TableName = rdr["TableName"].ToString();
            model.CNames = rdr["CNames"].ToString();
            model.Fields = rdr["Fields"].ToString();
            model.UserID = Convert.ToInt32(rdr["UserID"]);
            model.CreateTime = Convert.ToDateTime(rdr["CreateTime"]);
            rdr.Close();
            return model;
        }
    }
}
