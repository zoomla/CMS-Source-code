using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ZoomLa.Model
{
    [Serializable]
    public class M_MoneyManage : M_Base
    {
        public int Flow
        {
            get;
            set;
        }
        /// <summary>
        /// Money_code
        /// </summary>	
        public string Money_code
        {
            get;
            set;
        }
        /// <summary>
        /// 货币代码
        /// </summary>	
        public string Money_descp
        {
            get;
            set;
        }
        /// <summary>
        /// 货处符号
        /// </summary>	
        public string Money_sign
        {
            get;
            set;
        }
        /// <summary>
        /// 当前汇率
        /// </summary>	
        public decimal Money_rate
        {
            get;
            set;
        }
        /// <summary>
        /// 默认货币 1:默认
        /// </summary>	
        public string Is_flag
        {
            get;
            set;
        }
        public M_MoneyManage()
        {

        }
        public override string PK { get { return "Flow"; } }
        public override string TbName { get { return "ZL_MoneyManage"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                        {"Money_code","VarChar","10"},
                        {"Money_descp","VarChar","20"},
                        {"Money_sign","VarChar","4"},
                        {"Money_rate","Decimal","9"},
                        {"Is_flag","Char","1"}

        };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_MoneyManage model = this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.Money_code;
            sp[1].Value = model.Money_descp;
            sp[2].Value = model.Money_sign;
            sp[3].Value = model.Money_rate;
            sp[4].Value = model.Is_flag;
            return sp;
        }
        public M_MoneyManage GetModelFromReader(SqlDataReader rdr)
        {
            M_MoneyManage model = new M_MoneyManage();
            model.Flow = Convert.ToInt32(rdr["Flow"]);
            model.Money_code = rdr["Money_code"].ToString();
            model.Money_descp = rdr["Money_descp"].ToString();
            model.Money_sign = rdr["Money_sign"].ToString();
            model.Money_rate = ConverToDec(rdr["Money_rate"]);
            model.Is_flag = rdr["Is_flag"].ToString();
            rdr.Close();
            rdr.Dispose();
            return model;
        }
    }
}