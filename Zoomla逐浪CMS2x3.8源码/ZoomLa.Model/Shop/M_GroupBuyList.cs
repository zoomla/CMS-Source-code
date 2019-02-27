using System;
using System.Data.SqlClient;
using System.Data;
namespace ZoomLa.Model
{
    [Serializable]
    public class M_GroupBuyList : M_Base
    {
        #region 构造函数
        public M_GroupBuyList()
        {
            this.DepositTime = DateTime.Now;
            this.buytime = DateTime.Now;
        }
        public M_GroupBuyList
        (
            int id,
            int UserID,
            int ProID,
            double Deposit,
            DateTime DepositTime,
            int isbuy,
            DateTime buytime,
            double buymoney,
            DateTime Btime,
            int PayID,
            int DepositPayID,
            int Snum,
            int OrderID
        )
        {
            this.id = id;
            this.UserID = UserID;
            this.ProID = ProID;
            this.Deposit = Deposit;
            this.DepositTime = DepositTime;
            this.isbuy = isbuy;
            this.buytime = buytime;
            this.buymoney = buymoney;
            this.Btime = Btime;
            this.PayID = PayID;
            this.DepositPayID = DepositPayID;
            this.Snum = Snum;
            this.OrderID = OrderID;
        }
        /// <summary>
        /// 返回实体列表数组
        /// </summary>
        /// <returns>String[]</returns>
        public string[] GroupBuyListList()
        {
            string[] Tablelist = { "id", "UserID", "ProID", "Deposit", "DepositTime", "isbuy", "buytime", "buymoney", "Btime", "PayID", "DepositPayID", "Snum", "OrderID" };
            return Tablelist;
        }
        #endregion

        #region 属性定义
        /// <summary>
        /// 是否收货
        /// </summary>
        public int IsReceipt { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// 商品ID
        /// </summary>
        public int ProID { get; set; }
        /// <summary>
        /// 支付订金
        /// </summary>
        public double Deposit { get; set; }
        /// <summary>
        /// 支付订金时间
        /// </summary>
        public DateTime DepositTime { get; set; }
        /// <summary>
        /// 是否购买
        /// </summary>
        public int isbuy { get; set; }
        /// <summary>
        /// 支付时间
        /// </summary>
        public DateTime buytime { get; set; }
        /// <summary>
        /// 购买价格
        /// </summary>
        public double buymoney { get; set; }
        /// <summary>
        /// 团购时间
        /// </summary>
        public DateTime Btime { get; set; }
        /// <summary>
        /// 预订支付ID
        /// </summary>
        public int DepositPayID { get; set; }
        /// <summary>
        /// 支付ID
        /// </summary>
        public int PayID { get; set; }
        /// <summary>
        /// 报名人数
        /// </summary>
        public int Snum { get; set; }
        /// <summary>
        /// 订单ID
        /// </summary>
        public int OrderID { get; set; }
        #endregion
        public override string TbName { get { return "ZL_GroupBuyList"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"id","Int","4"},
                                  {"UserID","Int","4"},
                                  {"ProID","Int","4"},
                                  {"Deposit","Money","10"}, 
                                  {"DepositTime","DateTime","8"}, 
                                  {"isbuy","Int","4"}, 
                                  {"buytime","DateTime","8"}, 
                                  {"buymoney","Money","10"}, 
                                  {"Btime","DateTime","8"}, 
                                  {"PayID","Int","4"}, 
                                  {"DepositPayID","Int","4"}, 
                                  {"Snum","Int","4"}, 
                                  {"OrderID","Int","4"}, 
                                  {"IsReceipt","Int","4"}
                                 };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_GroupBuyList model = this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.id;
            sp[1].Value = model.UserID;
            sp[2].Value = model.ProID;
            sp[3].Value = model.Deposit;
            sp[4].Value = model.DepositTime;
            sp[5].Value = model.isbuy;
            sp[6].Value = model.buytime;
            sp[7].Value = model.buymoney;
            sp[8].Value = model.Btime;
            sp[9].Value = model.PayID;
            sp[10].Value = model.DepositPayID;
            sp[11].Value = model.Snum;
            sp[12].Value = model.OrderID;
            sp[13].Value = model.IsReceipt;
            return sp;
        }

        public M_GroupBuyList GetModelFromReader(SqlDataReader rdr)
        {
            M_GroupBuyList model = new M_GroupBuyList();
            model.id = Convert.ToInt32(rdr["id"]);
            model.UserID = Convert.ToInt32(rdr["UserID"]);
            model.ProID = Convert.ToInt32(rdr["ProID"]);
            model.Deposit = ConverToDouble(rdr["Deposit"]);
            model.DepositTime = ConvertToDate(rdr["DepositTime"]);
            model.isbuy = ConvertToInt(rdr["isbuy"]);
            model.buytime = ConvertToDate(rdr["buytime"]);
            model.buymoney = ConverToDouble(rdr["buymoney"]);
            model.Btime = ConvertToDate(rdr["Btime"]);
            model.PayID = Convert.ToInt32(rdr["PayID"]);
            model.DepositPayID = ConvertToInt(rdr["DepositPayI"]);
            model.Snum = ConvertToInt(rdr["Snum"]);
            model.OrderID = ConvertToInt(rdr["OrderID"]);
            model.IsReceipt = ConvertToInt(rdr["IsReceipt"]);
            rdr.Close();
            return model;
        }


    }
}