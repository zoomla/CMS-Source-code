using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;


namespace ZoomLa.Model
{
    public class M_Deposit : M_Base
    {
        public M_Deposit() 
        {
            CDate = DateTime.Now;
            LastCDate = DateTime.Now;
            DayInWeek = (int)DateTime.Now.DayOfWeek;
        }
        public int ID { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public double Money { get; set; }
        /// <summary>
        /// 分红比率
        /// </summary>
        public double BonusPer { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        /// <summary>
        /// 次额转入时间,上次分红时间的话,是否动态读取表中?
        /// </summary>
        public DateTime CDate { get; set; }
        /// <summary>
        /// 资金类型,1:资金,2:赠金
        /// </summary>
        public int MyType { get; set; }
        /// <summary>
        /// 赠金绑定的资金入账ID
        /// </summary>
        public int BindID { get; set; }
        public int MyState { get; set; }
        /// <summary>
        /// 备注,存用户可见信息
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 备注,存仅管理员可见信息
        /// </summary>
        public string Remark2 { get; set; }
        /// <summary>
        /// 最近一次分红时间
        /// </summary>
        public DateTime LastCDate { get; set; }
        public enum SType { Valid = 0, InValid = -1 };
        /// <summary>
        /// 所属周的第几天
        /// </summary>
        public int DayInWeek { get; set; }
        public override string TbName { get { return "ZL_Deposit"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                      {"ID","Int","4"},
                                      {"Money","Money","8"},
                                      {"BonusPer","Decimal","8"},
                                      {"UserID","Int","4"},
                                      {"UserName","NVarChar","100"},
                                      {"CDate","DateTime","8"},
                                      {"MyType","Int","4"},
                                      {"MyState","Int","4"},
                                      {"Remark","NVarChar","500"},
                                      {"Remark2","NVarChar","500"},
                                      {"BindID","Int","4"},
                                      {"LastCDate","DateTime","8"},
                                      {"DayInWeek","Int","4"}
                                  };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_Deposit model = this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.Money;
            sp[2].Value = model.BonusPer;
            sp[3].Value = model.UserID;
            sp[4].Value = model.UserName;
            sp[5].Value = model.CDate;
            sp[6].Value = model.MyType;
            sp[7].Value = model.MyState;
            sp[8].Value = model.Remark;
            sp[9].Value = model.Remark2;
            sp[10].Value = model.BindID;
            sp[11].Value = model.LastCDate;
            sp[12].Value = model.DayInWeek;
            return sp;
        }
        public M_Deposit GetModelFromReader(SqlDataReader rdr)
        {
            M_Deposit model = new M_Deposit();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.Money = ConverToDouble(rdr["Money"]);
            model.BonusPer = ConverToDouble(rdr["BonusPer"]);
            model.UserID = ConvertToInt(rdr["UserID"]);
            model.UserName = ConverToStr(rdr["UserName"]);
            model.CDate = ConvertToDate(rdr["CDate"]);
            model.MyType = ConvertToInt(rdr["MyType"]);
            model.MyState = ConvertToInt(rdr["MyState"]);
            model.Remark = ConverToStr(rdr["Remark"]);
            model.Remark2 = ConverToStr(rdr["Remark2"]);
            model.BindID = ConvertToInt(rdr["BindID"]);
            model.LastCDate = ConvertToDate(LastCDate);
            model.DayInWeek = ConvertToInt(rdr["DayInWeek"]);
            rdr.Close();
            return model;
        }
        public M_Deposit GetModelFromReader(DataRow rdr)
        {
            M_Deposit model = new M_Deposit();
            model.ID = ConvertToInt(rdr["ID"]);
            model.Money = ConverToDouble(rdr["Money"]);
            model.BonusPer = ConverToDouble(rdr["BonusPer"]);
            model.UserID = ConvertToInt(rdr["UserID"]);
            model.UserName = rdr["UserName"].ToString();
            model.CDate = ConvertToDate(rdr["CDate"]);
            model.MyType = ConvertToInt(rdr["MyType"]);
            model.MyState = ConvertToInt(rdr["MyState"]);
            model.Remark = ConverToStr(rdr["Remark"]);
            model.Remark2 = ConverToStr(rdr["Remark2"]);
            model.BindID = ConvertToInt(rdr["BindID"]);
            model.LastCDate = ConvertToDate(LastCDate);
            model.DayInWeek = ConvertToInt(rdr["DayInWeek"]);
            return model;
        }
        //仅用于推广佣金结算
        public M_Deposit GetModelFromUnit(DataRow rdr)
        {
            M_Deposit model = new M_Deposit();
            model.Money = ConverToDouble(rdr["Amount"]);
            model.UserID = ConvertToInt(rdr["UserID"]);
            model.MyType = ConvertToInt(rdr["MyType"]);
            return model;
        }

    }
}
