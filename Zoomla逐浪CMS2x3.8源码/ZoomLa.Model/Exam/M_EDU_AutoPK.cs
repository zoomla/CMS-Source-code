using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ZoomLa.Model.Exam
{
    public class M_EDU_AutoPK:M_Base
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        //详细配置
        public string Config { get; set; }
        //所属班级
        public int Ownclass { get; set; }
        //授课老师的IDS
        public string TechIDS { get; set; }
        public DateTime CDate { get; set; }
        public DateTime SDate { get; set; }
        public DateTime EDate { get; set; }

        public override string TbName { get { return "ZL_EDU_AutoPK"; } }
        public M_EDU_AutoPK()
        {
            this.SDate = DateTime.MinValue;
            this.EDate = DateTime.MinValue;
        }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
        		        		{"ID","Int","4"},
                                {"UserID","Int","4"},
        		        		{"Config","NText","10000"},
        		        		{"Ownclass","Int","4"},
        		        		{"TechIDS","NVarChar","500"},
        		        		{"CDate","DateTime","8"},
                                {"SDate","DateTime","8"},
                                {"EDate","DateTime","8"}
        };
            return Tablelist;
        }

        public override SqlParameter[] GetParameters()
        {
            if (CDate <= DateTime.MinValue) CDate = DateTime.Now;
            M_EDU_AutoPK model = this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.UserID;
            sp[2].Value = model.Config;
            sp[3].Value = model.Ownclass;
            sp[4].Value = model.TechIDS;
            sp[5].Value = model.CDate;
            sp[6].Value = model.SDate;
            sp[7].Value = model.EDate;
            return sp;
        }
        public M_EDU_AutoPK GetModelFromReader(SqlDataReader rdr)
        {
            M_EDU_AutoPK model = new M_EDU_AutoPK();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.Config = ConverToStr(rdr["Config"]);
            model.Ownclass = ConvertToInt(rdr["Ownclass"]);
            model.TechIDS = ConverToStr(rdr["TechIDS"]);
            model.CDate = ConvertToDate(rdr["CDate"]);
            model.SDate = ConvertToDate(rdr["SDate"]);
            model.EDate = ConvertToDate(rdr["EDate"]);
            model.UserID = ConvertToInt(rdr["UserID"]);
            rdr.Close();
            return model;
        }
    }
}
