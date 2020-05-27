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

        /// <summary>
        /// 获取字段窜
        /// </summary>
        public  string GetFields()
        {
            string str = string.Empty;
            string[,] strArr = FieldList();
            for (int i = 0; i < strArr.GetLength(0); i++)
            {
                if (strArr[i, 0] != PK)
                {
                    str += "[" + strArr[i, 0] + "],";
                }
            }
            return str.Substring(0, str.Length - 1);
        }

        /// <summary>
        /// 获取参数串
        /// </summary>
        public  string GetParas()
        {
            string str = string.Empty;
            string[,] strArr = FieldList();
            for (int i = 0; i < strArr.GetLength(0); i++)
            {
                if (strArr[i, 0] != PK)
                {
                    str += "@" + strArr[i, 0] + ",";
                }
            }
            return str.Substring(0, str.Length - 1);
        }

        /// <summary>
        /// 获取字段=参数
        /// </summary>
        public  string GetFieldAndPara()
        {
            string str = string.Empty;
            string[,] strArr = FieldList();
            for (int i = 0; i < strArr.GetLength(0); i++)
            {
                if (strArr[i, 0] != PK)
                {
                    str += "[" + strArr[i, 0] + "]=@" + strArr[i, 0] + ",";
                }
            }
            return str.Substring(0, str.Length - 1);
        }

        public  SqlParameter[] GetParameters(M_IP_class model)
        {
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