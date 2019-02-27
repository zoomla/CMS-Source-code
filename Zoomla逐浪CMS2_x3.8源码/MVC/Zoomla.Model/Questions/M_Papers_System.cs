using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace ZoomLa.Model
{
    /// <summary>
    /// 系统试卷
    /// </summary>
    [Serializable]
    public class M_Papers_System : M_Base
    {
        #region 定义字段

        public int Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string p_name { get; set; }
        /// <summary>

        public int p_type { get; set; }
        #endregion

        public M_Papers_System() { }

        public M_Papers_System(string name, int type)
        {
            this.p_name = name;
            this.p_type = type;
        }

        public override string TbName { get { return "ZL_Papers_System"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"id","Int","4"},
                                  {"p_name","NChar","255"},
                                  {"p_type","Int","4"}
                                  
                                 
                              };
            return Tablelist;
        }

        public override SqlParameter[] GetParameters()
        {
           M_Papers_System model=this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.Id;
            sp[1].Value = model.p_name;
            sp[2].Value = model.p_type;
            return sp;
        }
        public M_Papers_System GetModelFromReader(SqlDataReader rdr)
        {
            M_Papers_System model = new M_Papers_System();
            model.Id = Convert.ToInt32(rdr["id"]);
            model.p_name = rdr["p_name"].ToString();
            model.p_type = Convert.ToInt32(rdr["p_type"]);
            rdr.Close();
            return model;
        }
    }

}


