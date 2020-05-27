using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ZoomLa.Model.Project
{
    /// <summary>
    /// 项目流程Model类
    /// </summary>
    public class M_Pro_Flow : M_Base
    {
        //mprojectwork.WorkName = TxtWorkName.Text.Trim();
        //mprojectwork.WorkIntro = TxtWorkIntro.Text.Trim();
        //mprojectwork.ProjectID = 0;
        //mprojectwork.Approving = 0;//默认值
        //mprojectwork.Status = 1;
        //mprojectwork.EndDate = DateTime.Now;
        public int ID { get; set; }
        public string WorkName { get; set; }
        public string WorkIntro { get; set; }
        public int ProjectID { get; set; }
        public int Status { get; set; }
        public int Approving { get; set; }
        public DateTime BDate { get; set; }
        public DateTime EDate { get; set; }
        public override string TbName { get { return "ZL_Pro_Flow"; } }
        public override string PK { get { return "ID"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                {"ID","Int","4"},
                                {"WorkName","NVarChar","50"},
                                {"WorkIntro","NVarChar","500"},
                                {"ProjectID","Int","4"},
                                {"Status","Int","4"},
                                {"Approving","Int","4"},
                                {"BDate","DateTime","8"},
                                {"EDate","DateTime","8"}
            };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_Pro_Flow model = this;
            if (BDate <= DateTime.MinValue) { BDate = DateTime.Now; }
            if (EDate <= DateTime.MinValue) { EDate = DateTime.Now; }
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.WorkName;
            sp[2].Value = model.WorkIntro;
            sp[3].Value = model.ProjectID;
            sp[4].Value = model.Status;
            sp[5].Value = model.Approving;
            sp[6].Value = model.BDate;
            sp[7].Value = model.EDate;
            return sp;
        }
        public M_Pro_Flow GetModelFromReader(DbDataReader rdr)
        {
            M_Pro_Flow model = new M_Pro_Flow();
            model.ID = ConvertToInt(rdr["ID"]);
            model.WorkName = ConverToStr(rdr["WorkName"]);
            model.WorkIntro = ConverToStr(rdr["WorkIntro"]);
            model.ProjectID = ConvertToInt(rdr["ProjectID"]);
            model.Status = ConvertToInt(rdr["Status"]);
            model.Approving = ConvertToInt(rdr["Approving"]);
            model.BDate = ConvertToDate(rdr["BDate"]);
            model.EDate = ConvertToDate(rdr["EDate"]);
            rdr.Close();
            return model;
        }
    }
}
