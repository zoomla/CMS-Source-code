using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ZoomLa.Model.Project
{
    public class M_Pro_Step : M_Base
    {
        public int ID { get; set; }
        /// <summary>
        /// 项目ID
        /// </summary>
        public int ProID { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        public int OrderID { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 元素列表
        /// </summary>
        public string comlist { get; set; }
        /// <summary>
        /// 背景mp3链接
        /// </summary>
        public string mp3 { get; set; }
        public int CUser { get; set; }
        public string CUName { get; set; }
        public DateTime CDate { get; set; }
        /// <summary>
        /// 项目状态
        /// </summary>
        public int ZStatus { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remind { get; set; }
        /// <summary>
        /// 封面图片
        /// </summary>
        public string topimg { get; set; }
        public M_Pro_Step() { CDate = DateTime.Now; }
        public override string TbName { get { return "ZL_Pro_Step"; } }
        public override string PK { get { return "ID"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
        		        		{"ID","Int","4"},
        		        		{"ProID","Int","4"},
        		        		{"OrderID","Int","4"},
        		        		{"Title","NVarChar","200"},
        		        		{"comlist","NText","50000"},
        		        		{"mp3","NVarChar","500"},
        		        		{"CUser","Int","4"},
        		        		{"CUName","NVarChar","100"},
        		        		{"CDate","DateTime","8"},
        		        		{"ZStatus","Int","4"},
        		        		{"Remind","NVarChar","200"},
                                {"topimg","NVarChar","1000"}
        };
            return Tablelist;
        }
        public SqlParameter[] GetParameters(M_Pro_Step model)
        {
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.ProID;
            sp[2].Value = model.OrderID;
            sp[3].Value = model.Title;
            sp[4].Value = model.comlist;
            sp[5].Value = model.mp3;
            sp[6].Value = model.CUser;
            sp[7].Value = model.CUName;
            sp[8].Value = model.CDate;
            sp[9].Value = model.ZStatus;
            sp[10].Value = model.Remind;
            sp[11].Value = model.topimg;
            return sp;
        }
        public M_Pro_Step GetModelFromReader(SqlDataReader rdr)
        {
            M_Pro_Step model = new M_Pro_Step();
            model.ID = ConvertToInt(rdr["ID"]);
            model.ProID = ConvertToInt(rdr["ProID"]);
            model.OrderID = ConvertToInt(rdr["OrderID"]);
            model.Title = ConverToStr(rdr["Title"]);
            model.comlist = ConverToStr(rdr["comlist"]);
            model.mp3 = ConverToStr(rdr["mp3"]);
            model.CUser = ConvertToInt(rdr["CUser"]);
            model.CUName = ConverToStr(rdr["CUName"]);
            model.CDate = ConvertToDate(rdr["CDate"]);
            model.ZStatus = ConvertToInt(rdr["ZStatus"]);
            model.Remind = ConverToStr(rdr["Remind"]);
            model.topimg = ConverToStr(rdr["topimg"]);
            rdr.Close();
            return model;
        }
    }
}
