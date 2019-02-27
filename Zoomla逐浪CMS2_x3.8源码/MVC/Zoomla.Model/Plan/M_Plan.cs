namespace ZoomLa.Model
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Data.SqlClient;
    using System.Data;

    public class M_Plan : M_Base
    {
        public int ID { get; set; }
        /// <summary>
        /// 计划名称
        /// </summary>
        public string PlanName { get; set; }
        /// <summary>
        /// 执行规则
        /// </summary>
        public string PlanRule { get; set; }
        /// <summary>
        /// 操作的数据库
        /// </summary>
        public string DataSet { get; set; }
        /// <summary>
        /// 执行时间
        /// </summary>
        public DateTime ExecutionTime { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 次数
        /// </summary>
        public string Step { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }

        public M_Plan()
        {
            this.CreationTime = DateTime.Now;
        }

        public override string TbName { get { return "ZL_Plan"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"ID","Int","4"},
                                  {"PlanName","NVarChar","4000"},
                                  {"PlanRule","NVarChar","400"},
                                  {"DataSet","NVarChar","50"},
                                  {"ExecutionTime","DateTime","8"},
                                  {"Description","NText","400"},
                                  {"Step","NVarChar","4000"},
                                  {"CreationTime","DateTime","8"}
                                 };
            return Tablelist;
        }


        public override string GetPK()
        {
            return PK;
        }
        public override SqlParameter[] GetParameters()
        {
            M_Plan model=this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.PlanName;
            sp[2].Value = model.PlanRule;
            sp[3].Value = model.DataSet;
            sp[4].Value = model.ExecutionTime;
            sp[5].Value = model.Description;
            sp[6].Value = model.Step;
            sp[7].Value = model.CreationTime;
            return sp;
        }

        public M_Plan GetModelFromReader(SqlDataReader rdr)
        {
            M_Plan model = new M_Plan();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.PlanName = rdr["PlanName"].ToString();
            model.PlanRule = rdr["PlanRule"].ToString();
            model.DataSet = rdr["DataSet"].ToString();
            model.ExecutionTime = Convert.ToDateTime(rdr["ExecutionTime"]);
            model.Description = rdr["Description"].ToString();
            model.Step = rdr["Step"].ToString();
            model.CreationTime = Convert.ToDateTime(rdr["CreationTime"]);
            rdr.Close();
            return model;
        }
    }
}