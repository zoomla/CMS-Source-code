using System;
using System.Data.SqlClient;
using System.Data;
namespace ZoomLa.Model
{
    [Serializable]
    public class M_PromoCount:M_Base
    {
        #region 构造函数
        public M_PromoCount()
        {
        }

        public M_PromoCount
        (
            int id,
            string PromotionUrl,
            int sid,
            int linkCount
        )
        {
            this.id = id;
            this.PromotionUrl = PromotionUrl;
            this.sid = sid;
            this.linkCount = linkCount;
        }
        /// <summary>
        /// 返回实体列表数组
        /// </summary>
        /// <returns>String[]</returns>
        public string[] PromoCountList()
        {
            string[] Tablelist = { "id", "PromotionUrl", "sid", "linkCount" };
            return Tablelist;
        }
        #endregion

        #region 属性定义
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 推广Url
        /// </summary>
        public string PromotionUrl { get; set; }
        /// <summary>
        /// 商家id
        /// </summary>
        public int sid { get; set; }
        /// <summary>
        /// 点击链接次数
        /// </summary>
        public int linkCount { get; set; }
        #endregion
        public override string TbName { get { return "ZL_PromoCount"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"id","Int","4"},
                                  {"PromotionUrl","Int","4"},
                                  {"sid","Int","4"},
                                  {"linkCount","Int","4"} 
                                 };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_PromoCount model = this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.id;
            sp[1].Value = model.PromotionUrl;
            sp[2].Value = model.sid;
            sp[3].Value = model.linkCount;
            return sp;
        }

        public  M_PromoCount GetModelFromReader(SqlDataReader rdr)
        {
            M_PromoCount model = new M_PromoCount();
            model.id = Convert.ToInt32(rdr["id"]);
            model.PromotionUrl = ConverToStr(rdr["PromotionUrl"]);
            model.sid = ConvertToInt(rdr["sid"]);
            model.linkCount = ConvertToInt(rdr["linkCount"]);
            rdr.Close();
            return model;
        }
    }
}


