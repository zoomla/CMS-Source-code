using System;
using System.Data.SqlClient;
using System.Data;
namespace ZoomLa.Model
{
    [Serializable]
    public class M_GiftCard_User : M_Base
    {
        #region 构造函数
        public M_GiftCard_User()
        {
            this.CardNO = string.Empty;
            this.CardPass = string.Empty;
            this.password = string.Empty;
            this.OrderData = DateTime.Now;
            this.Remark = string.Empty;
            this.confirmData = DateTime.Now;
        }
        public M_GiftCard_User
        (
            int id,
            int ShopCardId,
            string CardNO,
            string CardPass,
            string password,
            int CardType,
            int UserId,
            DateTime OrderData,
            DateTime confirmData,
            int State,
            int confirmState,
            string remark
        )
        {
            this.id = id;
            this.ShopCardId = ShopCardId;
            this.CardNO = CardNO;
            this.CardPass = CardPass;
            this.password = password;
            this.CardType = CardType;
            this.UserId = UserId;
            this.OrderData = OrderData;
            this.confirmData = confirmData;
            this.State = State;
            this.confirmState = confirmState;
            this.Remark = remark;
        }
        /// <summary>
        /// 返回实体列表数组
        /// </summary>
        /// <returns>String[]</returns>
        public string[] GiftCard_UserList()
        {
            string[] Tablelist = { "id", "ShopCardId", "CardNO", "CardPass", "password", "CardType", "UserId", "OrderData", "confirmData", "State", "confirmState", "remark" };
            return Tablelist;
        }
        #endregion

        #region 属性定义
        public int id { get; set; }
        /// <summary>
        /// 商城礼品ID
        /// </summary>
        public int ShopCardId { get; set; }
        /// <summary>
        /// 卡号
        /// </summary>
        public string CardNO { get; set; }
        /// <summary>
        /// 卡号密码
        /// </summary>
        public string CardPass { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 确认密码
        /// </summary>
        public string password { get; set; }
        /// <summary>
        /// 卡类别:1为积分兑换,2为返利金额兑换
        /// </summary>
        public int CardType { get; set; }
        /// <summary>
        /// 所属用户ID
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 申请时间
        /// </summary>
        public DateTime OrderData { get; set; }
        /// <summary>
        /// 确认日期
        /// </summary>
        public DateTime confirmData { get; set; }
        /// <summary>
        /// 兑现状态:0为未兑现,1为已兑现
        /// </summary>
        public int State { get; set; }
        /// <summary>
        /// 确认状态
        /// </summary>
        public int confirmState { get; set; }
        #endregion
        public override string TbName { get { return "ZL_GiftCard_User"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"id","Int","4"},
                                  {"ShopCardId","Int","4"},
                                  {"CardNO","NVarChar","4"},
                                  {"CardPass","NVarChar","4"}, 
                                  {"password","NVarChar","4"}, 
                                  {"remark","NText","4"}, 
                                  {"CardType","Int","4"}, 
                                  {"UserId","Int","4"}, 
                                  {"OrderData","DateTime","4"}, 
                                  {"confirmData","DateTime","4"}, 
                                  {"State","Int","4"}, 
                                  {"confirmState","Int","4"}
                                 };
            return Tablelist;
        }

        public override SqlParameter[] GetParameters()
        {
            M_GiftCard_User model = this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.id;
            sp[1].Value = model.ShopCardId;
            sp[2].Value = model.CardNO;
            sp[3].Value = model.CardPass;
            sp[4].Value = model.password;
            sp[4].Value = model.Remark;
            sp[4].Value = model.CardType;
            sp[4].Value = model.UserId;
            sp[4].Value = model.OrderData;
            sp[4].Value = model.confirmData;
            sp[4].Value = model.State;
            sp[4].Value = model.confirmState;
            return sp;
        }

        public M_GiftCard_User GetModelFromReader(SqlDataReader rdr)
        {
            M_GiftCard_User model = new M_GiftCard_User();
            model.id = Convert.ToInt32(rdr["id"]);
            model.ShopCardId = ConvertToInt(rdr["ShopCardId"]);
            model.CardNO = ConverToStr(rdr["CardNO"]);
            model.CardPass = ConverToStr(rdr["CardPass"]);
            model.password = ConverToStr(rdr["password"]);
            model.Remark = ConverToStr(rdr["remark"]);
            model.CardType = ConvertToInt(rdr["CardType"]);
            model.UserId = ConvertToInt(rdr["UserId"]);
            model.OrderData = ConvertToDate(rdr["OrderData"]);
            model.confirmData = ConvertToDate(rdr["confirmData"]);
            model.State = ConvertToInt(rdr["State"]);
            model.confirmState = ConvertToInt(rdr["confirmState"]);
            rdr.Close();
            return model;
        }
    }
}