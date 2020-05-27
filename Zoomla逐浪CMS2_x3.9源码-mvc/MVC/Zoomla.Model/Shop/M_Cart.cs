using System;
using System.Data.SqlClient;
using System.Data;
namespace ZoomLa.Model
{
    public class M_Cart:M_Base
    {
        public int ID { get; set; }
        public string Cartid { get; set; }
        public int userid { get; set; }
        public string Username { get; set; }
        public DateTime Addtime { get; set; }
        public int StoreID { get; set; }
        public int ProID { get; set; }
        public int Pronum { get; set; }
        /// <summary>
        /// 需付金额,但生成订单时需要重新计算
        /// </summary>
        public double AllMoney { get; set; }
        /// <summary>
        /// 未计算过折扣等操作的金额
        /// </summary>
        public double AllIntegral { get; set; }
        /// <summary>
        /// 购买时的IP地址
        /// </summary>
        public string Getip { get; set; }
        /// <summary>
        /// 运费(Disuse)
        /// </summary>
        public string FarePrice { get; set; }
        /// <summary>
        /// 附加信息,用于存储联系人,商品信息等
        /// </summary>
        public string Additional { get; set; }
        /// <summary>
        /// 用于支持虚拟币
        /// </summary>
        public string AllMoney_Json { get; set; }
        /// <summary>
        /// 多价格编号,如不为空,则读取多价格信息
        /// </summary>
        public string code { get; set; }
        public string Proname { get; set; }
        public M_Cart() 
        {
            this.Cartid = string.Empty;
            this.Username = string.Empty;
            this.Addtime = DateTime.Now;
            this.Getip = string.Empty;
            this.FarePrice = string.Empty;
        }
        public override string TbName { get { return "ZL_Cart"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"ID","Int","4"},
                                  {"Cartid","NVarChar","255"},
                                  {"userid","Int","4"},
                                  {"Username","NVarChar","255"}, 
                                  {"Addtime","DateTime","8"},
                                  {"StoreID","Int","4"},
                                  {"ProID","Int","4"},
                                  {"Pronum","Int","4"},
                                  {"AllMoney","Money","8"},
                                  {"Getip","NVarChar","50"},
                                  {"AllIntegral","Money","8"},
                                  {"FarePrice","NVarChar","400"},
                                  {"Additional","NText","4000"},
                                  {"AllMoney_Json","NVarChar","500"},
                                  {"code","VarChar","500"},
                                  {"Proname","NVarChar","500"}
                                 };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_Cart model = this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.Cartid;
            sp[2].Value = model.userid;
            sp[3].Value = model.Username;
            sp[4].Value = model.Addtime;
            sp[5].Value = model.StoreID;
            sp[6].Value = model.ProID;
            sp[7].Value = model.Pronum;
            sp[8].Value = model.AllMoney;
            sp[9].Value = model.Getip;
            sp[10].Value = model.AllIntegral;
            sp[11].Value = model.FarePrice;
            sp[12].Value = model.Additional;
            sp[13].Value = model.AllMoney_Json;
            sp[14].Value = model.code;
            sp[15].Value = model.Proname;
            return sp;
        }
        public M_Cart GetModelFromReader(SqlDataReader rdr)
        {
            M_Cart model = new M_Cart();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.Cartid = rdr["Cartid"].ToString();
            model.userid = Convert.ToInt32(rdr["userid"]);
            model.Username = ConverToStr(rdr["Username"]);
            model.Addtime = Convert.ToDateTime(rdr["Addtime"]);
            model.StoreID = Convert.ToInt32(rdr["StoreID"]);
            model.ProID = Convert.ToInt32(rdr["ProID"]);
            model.Pronum = Convert.ToInt32(rdr["Pronum"]);
            model.AllMoney = Convert.ToDouble(rdr["AllMoney"]);
            model.Getip = rdr["Getip"].ToString();
            model.AllIntegral = ConverToDouble(rdr["AllIntegral"]);
            model.FarePrice = ConverToStr(rdr["FarePrice"]);
            model.Additional = ConverToStr(rdr["Additional"]);
            model.AllMoney_Json = ConverToStr(rdr["AllMoney_Json"]);
            model.code = ConverToStr(rdr["code"]);
            model.Proname = ConverToStr(rdr["Proname"]);
            rdr.Close();
            return model;
        }
    }
}
