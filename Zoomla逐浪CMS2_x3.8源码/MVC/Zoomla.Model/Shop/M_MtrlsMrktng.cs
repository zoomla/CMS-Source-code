using System;
namespace ZoomLa.Model
{
    [Serializable]
    public class M_MtrlsMrktng
    {
        #region 构造函数
        public M_MtrlsMrktng()
        {
        }

        public M_MtrlsMrktng
        (
            int ID,
            int ShopID,
            int UserID,
            string Type,
            int Count,
            double commission
        )
        {
            this.ID = ID;
            this.ShopID = ShopID;
            this.UserID = UserID;
            this.Type = Type;
            this.Count = Count;
            this.commission = commission;
        }
        /// <summary>
        /// 返回实体列表数组
        /// </summary>
        /// <returns>String[]</returns>
        public string[] MtrlsMrktngList()
        {
            string[] Tablelist = { "ID", "ShopID", "UserID", "Type", "Count", "commission" };
            return Tablelist;
        }
        #endregion

        #region 属性定义
        /// <summary>
        /// 
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 商品ID
        /// </summary>
        public int ShopID { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// 佣金类型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 总计商品
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// 提成
        /// </summary>
        public double commission { get; set; }
        #endregion
    }
}


