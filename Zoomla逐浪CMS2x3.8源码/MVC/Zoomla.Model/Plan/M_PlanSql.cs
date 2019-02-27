using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace ZoomLa.Model.PlanSql
{
    public class M_PlanSql:M_Base
    {
        public int ID { get; set; }
        /// <summary>
        /// ZL_Plan中主键
        /// </summary>
        public int PlanID { get; set; }
        /// <summary>
        /// Sql语句
        /// </summary>
        public string txtSql { get; set; }
        public DateTime CreateTime { get; set; }
        public int statu { get; set; }
        public M_PlanSql() {
            this.CreateTime = DateTime.Now;
        }

        public override string TbName { get { return "ZL_PlanSql"; } }
        /// <summary>
        /// 返回实体列表数组
        /// </summary>
        /// <returns>String[]</returns>
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"ID","Int","4"},
                                  {"PlanID","Int","4"},
                                  {"txtSql","NVarChar","4000"},
                                  {"CreateTime","DateTime","8"},
                                  {"statu","Int","4"}
                                 };
            return Tablelist;
        }
        public override string GetPK()
        {
            return PK;
        }

        public override SqlParameter[] GetParameters()
        {
            M_PlanSql model=this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.PlanID;
            sp[2].Value = model.txtSql;
            sp[3].Value = model.CreateTime;
            sp[4].Value = model.statu;
            return sp;
        }

        public  M_PlanSql GetModelFromReader(SqlDataReader rdr)
        {
            M_PlanSql model = new M_PlanSql();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.PlanID = Convert.ToInt32(rdr["PlanID"]);
            model.txtSql = rdr["txtSql"].ToString();
            model.CreateTime = Convert.ToDateTime(rdr["CreateTime"]);
            model.statu = Convert.ToInt32(rdr["statu"]);
            rdr.Close();
            return model;
        }
    }
}