using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ZoomLa.Model
{
    [Serializable]
    //ZL_Order_PayLog
    public class M_Order_PayLog:M_Base
    {

        public int ID { get; set; }
        /// <summary>
        /// 订单ID
        /// </summary>	
        public int OrderID { get; set; }
        /// <summary>
        /// 支付方式:1为余额，2为银币，3为其他；
        /// </summary>
        public int PayMethod { get; set; }
        public enum PayMethodEnum
        {
            Purse=1,Silver=2,Score=4,Other=3
        }
        /// <summary>
        /// 支付金额
        /// </summary>	
        public double PayMoney
        { get; set; }
        /// <summary>
        /// 支付平台ID  0:用银币或余额支付(详见PayMethodEnum),其他详见支付平台对应记录
        /// </summary>	
        public int PayPlatID { get; set; }
        public int UserID { get; set; }
        public DateTime AddTime { get; set; }
        public string Remind { get; set; }
        public M_Order_PayLog()
        {
            AddTime = DateTime.Now;
        }
        public override string TbName { get { return "ZL_Order_PayLog"; } }

        public override string[,] FieldList()
        {
            string[,] Tablelist = {
        	            {"ID","Int","4"},            
                        {"OrderID","Int","4"},            
                        {"PayMethod","Int","4"},            
                        {"PayMoney","Decimal","9"},            
                        {"PayPlatID","Int","4"},            
                        {"UserID","Int","4"},            
                        {"AddTime","DateTime","8"},            
                        {"Remind","NVarChar","255"}            
              
        };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_Order_PayLog model = this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.OrderID;
            sp[2].Value = model.PayMethod;
            sp[3].Value = model.PayMoney;
            sp[4].Value = model.PayPlatID;
            sp[5].Value = model.UserID;
            sp[6].Value = model.AddTime;
            sp[7].Value = model.Remind;
            return sp;
        }
        public M_Order_PayLog GetModelFromReader(SqlDataReader rdr)
        {
            M_Order_PayLog model = new M_Order_PayLog();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.OrderID = ConvertToInt(rdr["OrderID"]);
            model.PayMethod = ConvertToInt(rdr["PayMethod"]);
            model.PayMoney = ConverToDouble(rdr["PayMoney"]);
            model.PayPlatID = ConvertToInt(rdr["PayPlatID"]);
            model.UserID = Convert.ToInt32(rdr["UserID"]);
            model.AddTime = ConvertToDate(rdr["AddTime"]);
            model.Remind = ConverToStr(rdr["Remind"]);
            rdr.Close();
            return model;
        }
    }
}