    using System;
using System.Data.SqlClient;
using System.Data;
namespace ZoomLa.Model
{
    [Serializable]
    public class M_ArticlePromotion : M_Base
    {
        #region 构造函数
        public M_ArticlePromotion()
        {
            this.PromotionUrl = string.Empty;
            this.AddTime = DateTime.Now;
            this.BalanceTime = DateTime.Now;
        }
        public M_ArticlePromotion
        (
            int Id,
            int PromotionUserId,
            int CartProId,
            string PromotionUrl,
            bool IsBalance,
            bool IsEnable,
            DateTime AddTime,
            DateTime BalanceTime,
            int RebatesId
        )
        {
            this.Id = Id;
            this.PromotionUserId = PromotionUserId;
            this.CartProId = CartProId;
            this.PromotionUrl = PromotionUrl;
            this.IsBalance = IsBalance;
            this.IsEnable = IsEnable;
            this.AddTime = AddTime;
            this.BalanceTime = BalanceTime;
            this.RebatesId = RebatesId;
        }
        /// <summary>
        /// 返回实体列表数组
        /// </summary>
        /// <returns>String[]</returns>
        public string[] ArticlePromotionList()
        {
            string[] Tablelist = { "Id", "PromotionUserId", "CartProId", "PromotionUrl", "IsBalance", "IsEnable", "AddTime", "BalanceTime", "RebatesId" };
            return Tablelist;
        }
        #endregion

        #region 属性定义
        /// <summary>
        /// 主键Id，自增
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 推广用户ID
        /// </summary>
        public int PromotionUserId { get; set; }
        /// <summary>
        /// 推广商品ID
        /// </summary>
        public int CartProId { get; set; }
        /// <summary>
        /// 推广地址
        /// </summary>
        public string PromotionUrl { get; set; }
        /// <summary>
        /// 是否已结算
        /// </summary>
        public bool IsBalance { get; set; }
        /// <summary>
        /// 是否禁用、是否删除
        /// </summary>
        public bool IsEnable { get; set; }
        /// <summary>
        /// 增加时间
        /// </summary>
        public DateTime AddTime { get; set; }
        /// <summary>
        /// 结算时间
        /// </summary>
        public DateTime BalanceTime { get; set; }
        /// <summary>
        /// 结算信息ID
        /// </summary>
        public int RebatesId { get; set; }
        #endregion
        public override string TbName { get { return "ZL_ArticlePromotion"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"Id","Int","4"},
                                  {"PromotionUserId","Int","4"},
                                  {"CartProId","Int","4"},
                                  {"PromotionUrl","NVarChar","255"}, 
                                  {"IsBalance","Bit","1"},
                                  {"IsEnable","Bit","1"},
                                  {"AddTime","DateTime","8"},
                                  {"BalanceTime","DateTime","8"},
                                  {"RebatesId","Int","4"}, 
                                  };

            return Tablelist;
        }

        public override SqlParameter[] GetParameters()
        {
            M_ArticlePromotion model = this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.Id;
            sp[1].Value = model.PromotionUserId;
            sp[2].Value = model.CartProId;
            sp[3].Value = model.PromotionUrl;
            sp[4].Value = model.IsBalance;
            sp[5].Value = model.IsEnable;
            sp[6].Value = model.AddTime;
            sp[7].Value = model.BalanceTime;
            sp[8].Value = model.RebatesId;
            return sp;
        }

        public M_ArticlePromotion GetModelFromReader(SqlDataReader rdr)
        {
            M_ArticlePromotion model = new M_ArticlePromotion();
            model.Id = Convert.ToInt32(rdr["Id"]);
            model.PromotionUserId = ConvertToInt(rdr["PromotionUserId"]);
            model.CartProId = ConvertToInt(rdr["CartProId"]);
            model.PromotionUrl = ConverToStr(rdr["PromotionUrl"]);
            model.IsBalance = ConverToBool(rdr["IsBalance"]);
            model.IsEnable = ConverToBool(rdr["IsEnable"]);
            model.AddTime =ConvertToDate(rdr["AddTime"]);
            model.BalanceTime = ConvertToDate(rdr["BalanceTime"]);
            model.RebatesId = ConvertToInt(rdr["RebatesId"]);
            rdr.Close();
            return model;
        }


    }
}