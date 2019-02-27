using System;
using System.Data;
using System.Data.SqlClient;
namespace ZoomLa.Model
{
    [Serializable]
    public class M_Rebates : M_Base
    {
        #region 构造函数
        public M_Rebates()
        {
        }

        public M_Rebates
        (
            int ID,
            int UserID,
            double BalanceMoney,
            double Scale,
            double Money,
            int ShopCount,
            string OrderID
        )
        {
            this.ID = ID;
            this.UserID = UserID;
            this.BalanceMoney = BalanceMoney;
            this.Scale = Scale;
            this.Money = Money;
            this.ShopCount = ShopCount;
            this.OrderID = OrderID;
        }
        /// <summary>
        /// 返回实体列表数组
        /// </summary>
        /// <returns>String[]</returns>
        public string[] RebatesList()
        {
            string[] Tablelist = { "ID", "UserID", "BalanceMoney", "Scale", "Money", "ShopCount", "OrderID" };
            return Tablelist;
        }
        #endregion

        #region 属性定义
        /// <summary>
        /// 
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 推广人ID
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// 结算金额
        /// </summary>
        public double BalanceMoney { get; set; }
        /// <summary>
        /// 返利比例
        /// </summary>
        public double Scale { get; set; }
        /// <summary>
        /// 返利金额
        /// </summary>
        public double Money { get; set; }
        /// <summary>
        /// 总商品件数
        /// </summary>
        public int ShopCount { get; set; }
        /// <summary>
        /// 订单号ID
        /// </summary>
        public string OrderID { get; set; }
        #endregion
        public override string TbName { get { return "ZL_Rebates"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"ID","Int","4"},
                                  {"UserID","Int","4"},
                                  {"BalanceMoney","Money","8"},
                                  {"Scale","Float","8"}, 
                                  {"Money","Money","8"},
                                  {"ShopCount","Int","4"}, 
                                  {"OrderID","NVarChar","50"}
                                 };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_Rebates model = this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.UserID;
            sp[2].Value = model.BalanceMoney;
            sp[3].Value = model.Scale;
            sp[4].Value = model.Money;
            sp[5].Value = model.ShopCount;
            sp[6].Value = model.OrderID;
            return sp;
        }

        public M_Rebates GetModelFromReader(SqlDataReader rdr)
        {
            M_Rebates model = new M_Rebates();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.UserID = Convert.ToInt32(rdr["UserID"]);
            model.BalanceMoney = Convert.ToDouble(rdr["BalanceMoney"]);
            model.Scale = Convert.ToDouble(rdr["Scale"]);
            model.Money = Convert.ToDouble(rdr["Money"]);
            model.ShopCount = ConvertToInt(rdr["ShopCount"]);
            model.OrderID = ConverToStr(rdr["OrderID"]);
            rdr.Close();
            return model;
        }
    }
}