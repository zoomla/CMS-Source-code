using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace ZoomLa.Model
{
    [Serializable]
    public class M_Questions_Knowledge : M_Base
    {
        public int K_id
        {
            get;
            set;
        }
        /// <summary>
        /// 知识点名称
        /// </summary>
        public string K_name
        {
            get;
            set;
        }
        /// <summary>
        /// 科目
        /// </summary>
        public int K_class_id
        {
            get;
            set;
        }
        /// <summary>
        /// 排序
        /// </summary>
        public int K_OrderBy
        {
            get;
            set;
        }
        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 年级
        /// </summary>
        public int Grade { get; set; }
        /// <summary>
        /// 是否为系统知识点
        /// </summary>
        public int IsSys { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CDate { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public int CUser { get; set; }
        /// <summary>
        /// 父节点id
        /// </summary>
        public int Pid { get; set; }
        public M_Questions_Knowledge()
        {
            CDate = DateTime.Now;
        }
        public override string PK { get { return "k_id"; } }
        public override string TbName { get { return "ZL_Questions_Knowledge"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"k_id","Int","4"},
                                  {"k_name","NVarChar","200"},
                                  {"k_Class_id","Int","4"},
                                   {"k_OrderBy","Int","4"},
                                   {"Status","Int","4" },
                                   {"Grade","Int","4" },
                                   {"IsSys","Int","4" },
                                   {"CDate","DateTime","8" },
                                   {"CUser","Int","4" },
                                   {"Pid","Int","4"}
                              };
            return Tablelist;
        }
        public SqlParameter[] GetParameters(M_Questions_Knowledge model)
        {
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.K_id;
            sp[1].Value = model.K_name;
            sp[2].Value = model.K_class_id;
            sp[3].Value = model.K_OrderBy;
            sp[4].Value = model.Status;
            sp[5].Value = model.Grade;
            sp[6].Value = model.IsSys;
            sp[7].Value = model.CDate;
            sp[8].Value = model.CUser;
            sp[9].Value = model.Pid;

            return sp;
        }
        public M_Questions_Knowledge GetModelFromReader(SqlDataReader rdr)
        {
            M_Questions_Knowledge model = new M_Questions_Knowledge();
            model.K_id = Convert.ToInt32(rdr["k_id"]);
            model.K_name = ConverToStr(rdr["k_name"]);
            model.K_class_id = ConvertToInt(rdr["k_Class_id"]);
            model.K_OrderBy = ConvertToInt(rdr["k_OrderBy"]);
            model.Status = ConvertToInt(rdr["Status"]);
            model.Grade = ConvertToInt(rdr["Grade"]);
            model.IsSys = ConvertToInt(rdr["IsSys"]);
            model.CDate = ConvertToDate(rdr["CDate"]);
            model.CUser = ConvertToInt(rdr["CUser"]);
            model.Pid = ConvertToInt(rdr["Pid"]);
            rdr.Close();
            rdr.Dispose();
            return model;
        }
    }
}
