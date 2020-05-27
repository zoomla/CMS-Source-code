using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace ZoomLa.Model
{
    [Serializable]
    public class M_Process : M_Base
    {
        #region 构造方法
        public M_Process()
        {

        }
        #endregion
        #region 属性、字段

        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 步骤名称
        /// </summary>
        public string PName { get; set; }

        /// <summary>
        /// 步骤描述
        /// </summary>
        public string PDepcit { get; set; }

        /// <summary>
        /// 可执行的角色id
        /// </summary>
        public string PRole { get; set; }

        /// <summary>
        /// 流程码
        /// </summary>
        public int PCode { get; set; }
        //private string pMayWork;

        //public string PMayWork
        //{
        //    get { return pMayWork; }
        //    set { pMayWork = value; }
        //}

        /// <summary>
        /// 通过审核名称
        /// </summary>
        public string PPassName { get; set; }

        /// <summary>
        /// 通过审核码
        /// </summary>
        public int PPassCode { get; set; }

        /// <summary>
        /// 未通过审核名称
        /// </summary>
        public string PNoPassName { get; set; }

        /// <summary>
        /// 未通过审核码
        /// </summary>
        public int PNoPassCode { get; set; }

        /// <summary>
        /// 流程编号
        /// </summary>
        public int PFlowId { get; set; }
        public int NeedCode { get; set; }
        #endregion
        public override string TbName { get { return "ZL_Process"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"id","Int","4"},
                                  {"PName","VarChar","200"},
                                  {"PDepcit","VarChar","5000"},
                                  {"PRole","VarChar","60"}, 
                                  {"PCode","Int","4"}, 
                                  {"PPassName","VarChar","200"}, 
                                  {"PPassCode","Int","4"}, 
                                  {"PNoPassName","VarChar","200"}, 
                                  {"PNoPassCode","Int","4"}, 
                                  {"PFlowId","Int","4"},
                                  {"NeedCode","Int","4"}
                                 };
            return Tablelist;
        }
        public SqlParameter[] GetParameters(M_Process model)
        {
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.Id;
            sp[1].Value = model.PName;
            sp[2].Value = model.PDepcit;
            sp[3].Value = model.PRole;
            sp[4].Value = model.PCode;
            sp[5].Value = model.PPassName;
            sp[6].Value = model.PPassCode;
            sp[7].Value = model.PNoPassName;
            sp[8].Value = model.PNoPassCode;
            sp[9].Value = model.PFlowId;
            sp[10].Value = model.NeedCode;
            return sp;
        }

        public M_Process GetModelFromReader(SqlDataReader rdr)
        {
            M_Process model = new M_Process();
            model.Id = Convert.ToInt32(rdr["id"]);
            model.PName = rdr["PName"].ToString();
            model.PDepcit = rdr["PDepcit"].ToString();
            model.PRole = rdr["PRole"].ToString();
            model.PCode = Convert.ToInt32(rdr["PCode"]);
            model.PPassName = rdr["PPassName"].ToString();
            model.PPassCode = Convert.ToInt32(rdr["PPassCode"]);
            model.PNoPassName = rdr["PNoPassName"].ToString();
            model.PNoPassCode = Convert.ToInt32(rdr["PNoPassCode"]);
            model.PFlowId = Convert.ToInt32(rdr["PFlowId"]);
            model.NeedCode = Convert.ToInt32(rdr["NeedCode"] == DBNull.Value ? "0" : rdr["NeedCode"]);
            rdr.Close();
            return model;
        }
    }
}
