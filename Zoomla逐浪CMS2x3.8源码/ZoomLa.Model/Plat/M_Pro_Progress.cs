using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ZoomLa.Model.Plat
{
    public class M_Pro_Progress : M_Base
    {
        public int ID { get; set; }
        /// <summary>
        /// 所属项目
        /// </summary>
        public int ProID { get; set; }
        /// <summary>
        /// 进度标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 进度内容
        /// </summary>
        public string Content { get; set; }
        public DateTime CDate { get; set; }
        public int ZStatus { get; set; }
        public int OrderID { get; set; }
        public M_Pro_Progress() { CDate = DateTime.Now; }

        public override string TbName { get { return "ZL_Pro_Progress"; } }
        public override string PK { get { return "ID"; } }

        public override string[,] FieldList()
        {
            string[,] Tablelist = {
        		        		{"ID","Int","4"},
        		        		{"ProID","Int","4"},
        		        		{"Title","NVarChar","200"},
        		        		{"Content","NVarChar","4000"},
        		        		{"CDate","DateTime","8"},
        		        		{"ZStatus","Int","4"},
                                {"OrderID","Int","4"}
        };
            return Tablelist;
        }

        public override SqlParameter[] GetParameters()
        {
            M_Pro_Progress model=this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.ProID;
            sp[2].Value = model.Title;
            sp[3].Value = model.Content;
            sp[4].Value = model.CDate;
            sp[5].Value = model.ZStatus;
            sp[6].Value = model.OrderID;
            return sp;
        }
        public M_Pro_Progress GetModelFromReader(SqlDataReader rdr)
        {
            M_Pro_Progress model = new M_Pro_Progress();
            model.ID = ConvertToInt(rdr["ID"]);
            model.ProID = ConvertToInt(rdr["ProID"]);
            model.Title = ConverToStr(rdr["Title"]);
            model.Content = ConverToStr(rdr["Content"]);
            model.CDate = ConvertToDate(rdr["CDate"]);
            model.ZStatus = ConvertToInt(rdr["ZStatus"]);
            model.OrderID = ConvertToInt(rdr["OrderID"]);
            rdr.Close();
            return model;
        }
    }
}
