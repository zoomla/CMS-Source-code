using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ZoomLa.Model.Exam
{
    public class M_EDU_Subject:M_Base
    {

        public int ID { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// 学科id
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// 同一时段最大安排数
        /// </summary>
        public int MaxCount { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Flag { get; set; }

        public override string TbName { get { return "ZL_EDU_Subject"; } }

        public override string[,] FieldList()
        {
            string[,] fields ={
                                 {"ID","Int","4"},
                                 {"Name","NVarChar","50"},
                                 {"Subject","NVarChar","100"},
                                 {"MaxCount","Int","4"},
                                 {"Flag","NVarChar","200"}
                             };
            return fields;
        }
        public override SqlParameter[] GetParameters()
        {
            M_EDU_Subject model = this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.Name;
            sp[2].Value = model.Subject;
            sp[3].Value = model.MaxCount;
            sp[4].Value = model.Flag;
            return sp;
        }

        public M_EDU_Subject GetModelFromReader(SqlDataReader rdr)
        {
            M_EDU_Subject model = new M_EDU_Subject();
            model.ID = ConvertToInt(rdr["ID"]);
            model.Name = rdr["Name"].ToString();
            model.Subject = rdr["Subject"].ToString();
            model.MaxCount = ConvertToInt(rdr["MaxCount"]);
            model.Flag = rdr["Flag"].ToString();
            rdr.Close();
            return model;
        }
    }
}
