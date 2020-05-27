namespace ZoomLa.Model
{
    using System;
    using System.Data.SqlClient;
    using System.Data;

    /// <summary>
    /// Summary description for M_IP_class
    /// </summary>
    public class M_IP_class:M_Base
    {
        #region 获得、设置属性
        /// <summary>
        /// 此分类标识
        /// </summary>
        public int class_ID { get; set; }
        /// <summary>
        /// 此分类名称
        /// </summary>
        public string class_name { get; set; }
        /// <summary>
        /// 此分类所属分类ID
        /// </summary>
        public int leadto_ID { get; set; }
        #endregion
        public override string PK { get { return "class_ID"; } }
        public override string TbName { get { return "ZL_IPclass"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"class_ID","Int","4"},
                                  {"leadto_ID","Int","4"},
                                  {"class_name","NVarChar","4"}
                                 };
            return Tablelist;
        }
        public  override SqlParameter[] GetParameters()
        {
            M_IP_class model=this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.class_ID;
            sp[1].Value = model.leadto_ID;
            sp[2].Value = model.class_name;
            return sp;
        }

        public  M_IP_class GetModelFromReader(SqlDataReader rdr)
        {
            M_IP_class model = new M_IP_class();
            model.class_ID = Convert.ToInt32(rdr["class_ID"]);
            model.leadto_ID = Convert.ToInt32(rdr["leadto_ID"]);
            model.class_name = rdr["class_name"].ToString();
            rdr.Close();
            rdr.Dispose();
            return model;
        }
    }
}