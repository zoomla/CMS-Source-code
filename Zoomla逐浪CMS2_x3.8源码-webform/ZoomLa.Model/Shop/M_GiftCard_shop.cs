using System;
using System.Data;
using System.Data.SqlClient;
namespace ZoomLa.Model
{
    [Serializable]
    public class M_GiftCard_shop : M_Base
    {
        #region 构造函数
        public M_GiftCard_shop()
        {
            this.Cardinfo = string.Empty;
            this.remark = string.Empty;
            this.PicUrl = string.Empty;
            this.Period = DateTime.Now;
        }
        public M_GiftCard_shop
        (
            int id,
            DateTime Period,
            string Cardinfo,
            string remark,
            int Points,
            double rebateVal,
            int Days,
            int Num,
            int shopid,
            string picUrl
        )
        {
            this.id = id;
            this.Period = Period;
            this.Cardinfo = Cardinfo;
            this.remark = remark;
            this.Points = Points;
            this.rebateVal = rebateVal;
            this.Days = Days;
            this.Num = Num;
            this.ShopId = shopid;
            this.PicUrl = picUrl;
        }
        /// <summary>
        /// 返回实体列表数组
        /// </summary>
        /// <returns>String[]</returns>
        public string[] GiftCard_shopList()
        {
            string[] Tablelist = { "id", "Period", "Cardinfo", "remark", "Points", "rebateVal", "Days", "Num", "ShopId", "PicUrl" };
            return Tablelist;
        }
        #endregion

        #region 属性定义
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 图片地址
        /// </summary>
        public string PicUrl { get; set; }
        /// <summary>
        /// 商家ID
        /// </summary>
        public int ShopId { get; set; }
        /// <summary>
        /// 有效期
        /// </summary>
        public DateTime Period { get; set; }
        /// <summary>
        /// 卡信息
        /// </summary>
        public string Cardinfo { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }
        /// <summary>
        /// 积分数
        /// </summary>
        public int Points { get; set; }
        /// <summary>
        /// 返利金额
        /// </summary>
        public double rebateVal { get; set; }
        /// <summary>
        /// 限制:几天可兑换一次
        /// </summary>
        public int Days { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Num { get; set; }
        #endregion

        public override string TbName { get { return "ZL_GiftCard_shop"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"Cgtype","Int","4"},
                                  {"Period","DateTime","4"},
                                  {"Cardinfo","NVarChar","4"},
                                  {"remark","NText","4"}, 
                                  {"Points","Int","4"}, 
                                  {"shopId","Int","4"}, 
                                  {"rebateVal","Money","4"}, 
                                  {"Days","Int","4"}, 
                                  {"Num","Int","4"}, 
                                  {"PicUrl","NVarChar","4"} 
                                 };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_GiftCard_shop model = this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.id;
            sp[1].Value = model.Period;
            sp[2].Value = model.Cardinfo;
            sp[3].Value = model.remark;
            sp[4].Value = model.Points;
            sp[5].Value = model.ShopId;
            sp[6].Value = model.rebateVal;
            sp[7].Value = model.Days;
            sp[8].Value = model.Num;
            sp[9].Value = model.PicUrl;
            return sp;
        }

        public M_GiftCard_shop GetModelFromReader(SqlDataReader rdr)
        {
            M_GiftCard_shop model = new M_GiftCard_shop();
            model.id = Convert.ToInt32(rdr["Cgtype"]);
            model.Period = ConvertToDate(rdr["Period"]);
            model.Cardinfo = ConverToStr(rdr["Cardinfo"]);
            model.remark = ConverToStr(rdr["remark"]);
            model.Points = ConvertToInt(rdr["Points"]);
            model.ShopId = ConvertToInt(rdr["shopId"]);
            model.rebateVal = ConvertToInt(rdr["rebateVal"]);
            model.Days = ConvertToInt(rdr["Days"]);
            model.Num = ConvertToInt(rdr["Num"]);
            model.PicUrl = ConverToStr(rdr["PicUrl"]);
            rdr.Close();
            return model;
        }
    }
}