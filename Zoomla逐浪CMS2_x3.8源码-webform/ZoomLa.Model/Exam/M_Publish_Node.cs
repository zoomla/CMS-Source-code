using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ZoomLa.Model.Exam
{
    public class M_Publish_Node:M_Base
    {
        public int ID { get; set; }
        /// <summary>
        /// 父节点id
        /// </summary>
        public int Pid { get; set; }
        public string NodeName { get; set; }
        /// <summary>
        /// 节点信息
        /// </summary>
        public string Describe { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CDate { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public int CAdmin { get; set; }
        /// <summary>
        /// 节点权限
        /// </summary>
        public string Auth { get; set; }

        public override string TbName { get { return "ZL_Publish_Node"; } }
        public override string[,] FieldList()
        {
            string[,] tablelist ={
                                    {"ID","Int","4"},
                                    {"Pid","Int","4"},
                                    {"NodeName","NVarChar","200"},
                                    {"Describe","NVarChar","500"},
                                    {"CDate","DateTime","8"},
                                    {"CAdmin","Int","4"},
                                    {"Auth","NVarChar","500"}
                                };
            return tablelist;
        }

        public override SqlParameter[] GetParameters()
        {
            M_Publish_Node model = this;
            if (CDate.Year < 1901) CDate = DateTime.Now;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.Pid;
            sp[2].Value = model.NodeName;
            sp[3].Value = model.Describe;
            sp[4].Value = model.CDate;
            sp[5].Value = model.CAdmin;
            sp[6].Value = model.Auth;
            return sp;
        }
        public M_Publish_Node GetModelFromReader(SqlDataReader sdr)
        {
            M_Publish_Node model = new M_Publish_Node();
            model.ID = Convert.ToInt32(sdr["ID"]);
            model.Pid = ConvertToInt(sdr["Pid"]);
            model.NodeName = ConverToStr(sdr["NodeName"]);
            model.Describe = ConverToStr(sdr["Describe"]);
            model.CDate = ConvertToDate(sdr["CDate"]);
            model.CAdmin = ConvertToInt(sdr["CAdmin"]);
            model.Auth = ConverToStr(sdr["Auth"]);
            return model;
        }
    }
}
