using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ZoomLa.Model.Shop
{
    public class M_Shop_FareTlp : M_Base
    {
        public int ID { get; set; }
        public string TlpName { get; set; }
        /// <summary>
        /// 是否包邮
        /// </summary>
        public int IsFree { get; set; }
        /// <summary>
        /// 计费模式
        /// </summary>
        public int PriceMode { get; set; }
        /// <summary>
        /// 开启区域限制
        /// </summary>
        public int RegionBan { get; set; }
        /// <summary>
        /// 费用Json
        /// </summary>
        public string Express { get; set; }
        /// <summary>
        /// EMS计价JSON
        /// </summary>
        public string EMS { get; set; }
        /// <summary>
        /// 平邮计价JSON
        /// </summary>
        public string Mail { get; set; }
        public int UserID { get; set; }
        public int AdminID { get; set; }
        public DateTime CDate { get; set; }
        /// <summary>
        /// 备注,买家可见
        /// </summary>
        public string Remind { get; set; }
        /// <summary>
        /// 备注,仅卖家可见
        /// </summary>
        public string Remind2 { get; set; }

        public override string TbName { get { return "ZL_Shop_FareTlp"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
        		        		{"ID","Int","4"},
        		        		{"TlpName","NVarChar","200"},
        		        		{"IsFree","Int","4"},
        		        		{"PriceMode","Int","4"},
        		        		{"RegionBan","Int","4"},
        		        		{"Express","NVarChar","3000"},
        		        		{"EMS","NVarChar","3000"},
        		        		{"Mail","NVarChar","3000"},
        		        		{"AdminID","Int","4"},
        		        		{"CDate","DateTime","8"},
                                {"Remind","NVarChar","500"},
                                {"Remind2","NVarChar","500"},
                                {"UserID","Int","4" }
        };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_Shop_FareTlp model = this;
            if (model.CDate <= DateTime.MinValue) { model.CDate = DateTime.Now; }
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.TlpName;
            sp[2].Value = model.IsFree;
            sp[3].Value = model.PriceMode;
            sp[4].Value = model.RegionBan;
            sp[5].Value = model.Express;
            sp[6].Value = model.EMS;
            sp[7].Value = model.Mail;
            sp[8].Value = model.AdminID;
            sp[9].Value = model.CDate;
            sp[10].Value = model.Remind;
            sp[11].Value = model.Remind2;
            sp[12].Value = model.UserID;
            return sp;
        }
        public M_Shop_FareTlp GetModelFromReader(SqlDataReader rdr)
        {
            M_Shop_FareTlp model = new M_Shop_FareTlp();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.TlpName = ConverToStr(rdr["TlpName"]);
            model.IsFree = ConvertToInt(rdr["IsFree"]);
            model.PriceMode = ConvertToInt(rdr["PriceMode"]);
            model.RegionBan = ConvertToInt(rdr["RegionBan"]);
            model.Express = ConverToStr(rdr["Express"]);
            model.EMS = ConverToStr(rdr["EMS"]);
            model.Mail = ConverToStr(rdr["Mail"]);
            model.AdminID = ConvertToInt(rdr["AdminID"]);
            model.CDate = ConvertToDate(rdr["CDate"]);
            model.Remind = ConverToStr(rdr["Remind"]);
            model.Remind2 = ConverToStr(rdr["Remind2"]);
            model.UserID = ConvertToInt(rdr["UserID"]);
            rdr.Close();
            return model;
        }
    }
    public class M_Shop_Fare:M_Base
    {
        //快递名,是否启用,计价方式,初始价,续价,包邮方式,多少件包邮,多少钱包邮
        public string name { get; set; }
        public string Alias
        {
            get
            {
                switch (name)
                {
                    case "exp":
                        return "快递";
                    case "ems":
                        return "EMS";
                    case "mail":
                        return "平邮";
                    default:
                        return "未知";
                }
            }
        }
        public string free_num = "";
        public string free_money = "";
        public bool enabled { get; set; }
        //1,计件,2计重
        public int mode { get; set; }
        public string price = "";
        //基础运费
        public double Price { get { return ConverToDouble(price); } }
        public string plus = "";
        //每件续费
        public double Plus { get { return ConverToDouble(plus); } }
        /// <summary>
        /// 0:不包邮,1:计件包邮,2:金额包邮,3计件+金额包邮(任一)
        /// </summary>
        public int free_sel { get; set; }
        public int Free_num { get { return ConvertToInt(free_num); } }
        public double Free_Money { get { return ConverToDouble(free_money); } }
        public override string TbName { get { return ""; } }
        public override string PK { get { return ""; } }
        public override string[,] FieldList()
        {
            return null;
        }
    }
}
