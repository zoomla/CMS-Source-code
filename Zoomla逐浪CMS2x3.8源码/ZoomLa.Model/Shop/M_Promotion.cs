using System;
using System.Data.SqlClient;
using System.Data;
namespace ZoomLa.Model
{
    [Serializable]
    public class M_Promotion:M_Base
    {
        #region 构造函数
        public M_Promotion()
        {
        }

        public M_Promotion
        (
            int id,
            int userId,
            string LinkUrl,
            string PromoUrl,
            string type,
            string allianceurl
        )
        {
            this.id = id;
            this.userId = userId;
            this.LinkUrl = LinkUrl;
            this.PromoUrl = PromoUrl;
            this.type = type;
            this.AllinaceUrl = allianceurl;
        }
        /// <summary>
        /// 返回实体列表数组
        /// </summary>
        /// <returns>String[]</returns>
        public string[] PromotionList()
        {
            string[] Tablelist = { "id", "userId", "LinkUrl", "PromoUrl", "type", "AllinaceUrl" };
            return Tablelist;
        }
        #endregion

        #region 属性定义
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 推广用户ID
        /// </summary>
        public int userId { get; set; }
        /// <summary>
        /// 联盟推广地址(实际链接地址)
        /// </summary>
        public string AllinaceUrl { get; set; }
        /// <summary>
        /// 原始Url
        /// </summary>
        public string LinkUrl { get; set; }
        /// <summary>
        /// 推广URl
        /// </summary>
        public string PromoUrl { get; set; }
        /// <summary>
        /// 链接类型:1为链接注册推广,2为红包涵注册,3为商品,4为商城,5为活动
        /// </summary>
        public string type { get; set; }
        #endregion
        public override string TbName { get { return "ZL_Promotion"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"id","Int","4"},
                                  {"userId","Int","4"},
                                  {"LinkUrl","NText","400"},
                                  {"PromoUrl","NText","400"}, 
                                  {"type","Char","10"}, 
                                  {"AllinaceUrl","NText","400"}
                                 };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_Promotion model = this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.id;
            sp[1].Value = model.userId;
            sp[2].Value = model.LinkUrl;
            sp[3].Value = model.PromoUrl;
            sp[4].Value = model.type;
            sp[5].Value = model.AllinaceUrl;
            return sp;
        }

        public  M_Promotion GetModelFromReader(SqlDataReader rdr)
        {
            M_Promotion model = new M_Promotion();
            model.id = Convert.ToInt32(rdr["id"]);
            model.userId = Convert.ToInt32(rdr["userId"]);
            model.LinkUrl = ConverToStr(rdr["LinkUrl"]);
            model.PromoUrl = ConverToStr(rdr["PromoUrl"]);
            model.type = ConverToStr(rdr["type"]);
            model.AllinaceUrl = ConverToStr(rdr["AllinaceUrl"]);
            rdr.Close();
            return model;
        }
    }
}


