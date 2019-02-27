namespace ZoomLa.Model
{
    using System;
    using System.Data.SqlClient;
    using System.Data;
    /// <summary>
    /// Summary description for M_IP_Para
    /// </summary>
    public class M_IP_para:M_Base
    {

        #region 返回、设置属性
        /// <summary>
        /// 此IP段标识
        /// </summary>
        public int IP_ID { get; set; }
        /// <summary>
        /// 此IP段起始IP
        /// </summary>
        public long startIP { get; set; }
        /// <summary>
        /// 此IP段结束IP
        /// </summary>
        public long endIP { get; set; }
        /// <summary>
        /// 此IP段所属省级名
        /// </summary>
        public string pro_name { get; set; }
        /// <summary>
        /// 此IP段所属城市名
        /// </summary>
        public string city_name { get; set; }
        /// <summary>
        /// 此IP段所属分类ID
        /// </summary>
        public int class_ID { get; set; }
        #endregion
        public override string PK { get { return "IP_ID"; } }
        public override string TbName { get { return "ZL_IPpara"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"IP_ID","Int","4"},
                                  {"class_ID","Int","4"},
                                  {"pro_name","NVarChar","255"},
                                  {"city_name","NVarChar","255"}, 
                                  {"startIP","Int","4"}, 
                                  {"endIp","Int","4000"}
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

        public override SqlParameter[] GetParameters()
        {
            M_IP_para model = this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.IP_ID;
            sp[1].Value = model.class_ID;
            sp[2].Value = model.pro_name;
            sp[3].Value = model.city_name;
            sp[4].Value = model.startIP;
            sp[4].Value = model.endIP;
            return sp;
        }

        public  M_IP_para GetModelFromReader(SqlDataReader rdr)
        {
            M_IP_para model = new M_IP_para();
            model.IP_ID = Convert.ToInt32(rdr["IP_ID"]);
            model.class_ID = Convert.ToInt32(rdr["class_ID"]);
            model.pro_name = rdr["pro_name"].ToString();
            model.city_name = rdr["city_name"].ToString();
            model.startIP = Convert.ToInt32(rdr["startIP"]);
            model.endIP = Convert.ToInt32(rdr["endIp"]);
            rdr.Close();
            rdr.Dispose();
            return model;
        }
    }
}
