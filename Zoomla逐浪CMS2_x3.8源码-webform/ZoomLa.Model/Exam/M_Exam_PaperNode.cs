using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ZoomLa.Model.Exam
{
    public class M_Exam_PaperNode : M_Base
    {
        public int ID { get; set; }
        public string TypeName { get; set; }
        /// <summary>
        /// 父节点ID
        /// </summary>
        public int Pid { get; set; }
        public int OrderID { get; set; }
        public DateTime CDate { get; set; }
        public int AdminID { get; set; }
        public string Remind { get; set; }
        public override string TbName { get { return "ZL_Exam_PaperNode"; } }

        public override string[,] FieldList()
        {
            string[,] fildlist ={
                                    {"ID","Int","4" },
                                    {"TypeName","NVarChar","500"},
                                    {"Pid","Int","4"},
                                    {"OrderID","Int","4"},
                                    {"CDate","DateTime","8"},
                                    {"AdminID","Int","4" },
                                    {"Remind","NVarChar","500"}
                                };
            return fildlist;
        }
  
        public override SqlParameter[] GetParameters()
        {
            M_Exam_PaperNode model = this;
            SqlParameter[] sp = GetSP();
            if (model.CDate <= DateTime.MinValue) { model.CDate = DateTime.Now; }
            sp[0].Value = model.ID;
            sp[1].Value = model.TypeName;
            sp[2].Value = model.Pid;
            sp[3].Value = model.OrderID;
            sp[4].Value = model.CDate;
            sp[5].Value = model.AdminID;
            sp[6].Value = model.Remind;
            return sp;
        }
        public M_Exam_PaperNode GetModelFromReader(SqlDataReader rdr)
        {
            M_Exam_PaperNode model = new M_Exam_PaperNode();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.TypeName = rdr["TypeName"].ToString();
            model.Pid = ConvertToInt(rdr["Pid"]);
            model.OrderID = ConvertToInt(rdr["OrderID"]);
            model.CDate = ConvertToDate(rdr["CDate"]);
            model.AdminID = ConvertToInt(rdr["AdminID"]);
            model.Remind = ConverToStr(rdr["Remind"]);
            rdr.Close();
            return model;
        }
    }
}
