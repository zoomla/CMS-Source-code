using System;
using System.Data.SqlClient;
using System.Data;
namespace ZoomLa.Model
{
    [Serializable]
    public class M_RedEnvelope : M_Base
    {
        #region 定义字段
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 申请时间
        /// </summary>
        public DateTime OrderData { get; set; }
        /// <summary>
        /// 申请状态
        /// </summary>
        public int OrderState { get; set; }
        /// <summary>
        /// 扣除手续费
        /// </summary>
        public double DeducFee { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 红包
        /// </summary>
        public string RedEnvelope { get; set; }
        /// <summary>
        /// 类型:0为现金，1为礼品卡
        /// </summary>
        public int type { get; set; }
        /// <summary>
        /// 所属用户
        /// </summary>
        public int Userid { get; set; }

        public int RebateId { get; set; }

        /// <summary>
        /// 负责人
        /// </summary>
        public int AdminId { get; set; }

        /// <summary>
        /// 支付日期
        /// </summary>
        public DateTime PayDate { get; set; }
        #endregion

        #region 构造函数
        public M_RedEnvelope()
        {
        }

        public M_RedEnvelope
        (
            int id,
            DateTime OrderData,
            int OrderState,
            double DeducFee,
            string Remark,
            string RedEnvelope,
            int type,
            int Userid,
            int adminid,
            DateTime payData
        )
        {
            this.id = id;
            this.OrderData = OrderData;
            this.OrderState = OrderState;
            this.DeducFee = DeducFee;
            this.Remark = Remark;
            this.RedEnvelope = RedEnvelope;
            this.type = type;
            this.Userid = Userid;
            this.AdminId = adminid;
            this.PayDate = payData;
        }
        /// <summary>
        /// 返回实体列表数组
        /// </summary>
        /// <returns>String[]</returns>
        public string[] RedEnvelopeList()
        {
            string[] Tablelist = { "id", "OrderData", "OrderState", "DeducFee", "Remark", "RedEnvelope", "type", "Userid", "RebateId", "AdminId", "PayDate" };
            return Tablelist;
        }
        #endregion
        public override string TbName { get { return "ZL_RedEnvelope"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"id","Int","4"},
                                  {"OrderData","DateTime","8"},
                                  {"OrderState","Int","4"},
                                  {"DeducFee","Money","8"}, 
                                  {"RebateId","Int","4"}, 
                                  {"Remark","NText","400"},
                                  {"RedEnvelope","NVarChar","50"},
                                  {"type","Int","4"},
                                  {"Userid","Int","4"}, 
                                  {"PayDate","DateTime","8"},
                                  {"AdminId","Int","4"}
                                 };

            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_RedEnvelope model = this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.id;
            sp[1].Value = model.OrderData;
            sp[2].Value = model.OrderState;
            sp[3].Value = model.DeducFee;
            sp[4].Value = model.RebateId;
            sp[5].Value = model.Remark;
            sp[6].Value = model.RedEnvelope;
            sp[7].Value = model.type;
            sp[8].Value = model.Userid;
            sp[9].Value = model.PayDate;
            sp[10].Value = model.AdminId;
            return sp;
        }

        public M_RedEnvelope GetModelFromReader(SqlDataReader rdr)
        {
            M_RedEnvelope model = new M_RedEnvelope();
            model.id = Convert.ToInt32(rdr["id"]);
            model.OrderData = ConvertToDate(rdr["OrderData"]);
            model.OrderState = ConvertToInt(rdr["OrderState"]);
            model.DeducFee = ConvertToInt(rdr["DeducFee"]);
            model.RebateId = ConvertToInt(rdr["RebateId"]);
            model.Remark = ConverToStr(rdr["Remark"]);
            model.RedEnvelope = ConverToStr(rdr["RedEnvelope"]);
            model.type = ConvertToInt(rdr["type"]);
            model.Userid = Convert.ToInt32(rdr["Userid"]);
            model.PayDate = ConvertToDate(rdr["PayDate"]);
            model.AdminId = ConvertToInt(rdr["PayDate"]);
            rdr.Close();
            return model;
        }
    }
}


