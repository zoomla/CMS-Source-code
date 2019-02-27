using System;
using System.Data.SqlClient;
using System.Data;
namespace ZoomLa.Model
{
    [Serializable]
    public class M_Arrive:M_Base
    {
        /*
         * 0：未激活
         * 1：已激活,未使用(分配用户,或手动激活)
         * 10:已使用完成,终结
         */
        #region 定义字段
        public int ID { get; set; }
        public string ArriveName { get; set; }
        /// <summary>
        /// 生效时间
        /// </summary>
        public DateTime AgainTime { get; set; }
        /// <summary>
        /// 到期时间
        /// </summary>
        public DateTime EndTime { get; set; }
        public double MinAmount { get; set; }
        public double MaxAmount { get; set; }
        /// <summary>
        /// 优惠金额
        /// </summary>
        public double Amount { get; set; }
        /// <summary>
        /// 所属用户ID
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// 抵用劵编号
        /// </summary>
        public string ArriveNO { get; set; }
        /// <summary>
        /// 抵用劵密码
        /// </summary>
        public string ArrivePwd { get; set; }
        /// <summary>
        /// 状态1:正常,10:已使用
        /// </summary>
        public int State { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public int Type { get; set; }
        //------------------------------新增
        /// <summary>
        /// 优惠券缩略图
        /// </summary>
        public string PreviewImg { get; set; }
        /// <summary>
        /// 流水号,用于区分匹次
        /// </summary>
        public string Flow { get; set; }
        /// <summary>
        /// 优惠券详细描述
        /// </summary>
        public string ArriveDesc { get; set; }
        /// <summary>
        /// 使用时间
        /// </summary>
        public DateTime UseTime { get; set; }
        /// <summary>
        /// 使用备注,记录用途,目标订单号,或唯一标记
        /// </summary>
        public string UseRemind { get; set; }
        #endregion
        #region 构造函数
        public M_Arrive() 
        {
            this.AgainTime = DateTime.Now;
            this.ArriveName = string.Empty;
            this.ArriveNO = string.Empty;
            this.ArrivePwd = string.Empty;
            this.EndTime = DateTime.Now;
            this.UseTime = DateTime.Now;
        }
        #endregion
        public override string TbName { get { return "ZL_Arrive"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"ID","Int","4"},
                                  {"ArriveNO","NVarChar","50"},
                                  {"UserID","Int","4"},
                                  {"MinAmount","Money","4"},
                                  {"MaxAmount","Money","4"}, 
                                  {"Amount","Money","4"}, 
                                  {"AgainTime","DateTime","8"},
                                  {"EndTime","DateTime","8"},
                                  {"State","Int","4"},
                                  {"ArriveName","NVarChar","50"},
                                  {"ArrivePwd","NVarChar","50"}, 
                                  {"UseTime","DateTime","8"},
                                  {"Type","Int","4"},
                                  {"ArriveDesc","NVarChar","500"}, 
                                  {"PreviewImg","NVarChar","500"}, 
                                  {"Flow","NVarChar","100"}, 
                                  {"UseRemind","NVarChar","2000"}
                                  };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_Arrive model = this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.ArriveNO;
            sp[2].Value = model.UserID;
            sp[3].Value = model.MinAmount;
            sp[4].Value = model.MaxAmount;
            sp[5].Value = model.Amount;
            sp[6].Value = model.AgainTime;
            sp[7].Value = model.EndTime;
            sp[8].Value = model.State;
            sp[9].Value = model.ArriveName;
            sp[10].Value = model.ArrivePwd;
            sp[11].Value = model.UseTime;
            sp[12].Value =model.Type;
            sp[13].Value = model.ArriveDesc;
            sp[14].Value = model.PreviewImg;
            sp[15].Value = model.Flow;
            sp[16].Value = model.UseRemind;
            return sp;
        }
        public  M_Arrive GetModelFromReader(SqlDataReader rdr)
        {
            M_Arrive model = new M_Arrive();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.ArriveNO = ConverToStr(rdr["ArriveNO"]);
            model.UserID = Convert.ToInt32(rdr["UserID"]);
            model.MinAmount = ConverToDouble(rdr["MinAmount"]);
            model.MaxAmount = ConverToDouble(rdr["MaxAmount"]);
            model.Amount = Convert.ToInt32(rdr["Amount"]);
            model.AgainTime = ConvertToDate(rdr["AgainTime"]);
            model.EndTime = ConvertToDate(rdr["EndTime"]);
            model.State = Convert.ToInt32(rdr["State"]);
            model.ArriveName = ConverToStr(rdr["ArriveName"]);
            model.ArrivePwd = ConverToStr(rdr["ArrivePwd"]);
            model.UseTime = ConvertToDate(rdr["UseTime"]);
            model.Type = ConvertToInt(rdr["Type"]);
            model.ArriveDesc = ConverToStr(rdr["ArriveDesc"]);
            model.PreviewImg = ConverToStr(rdr["PreviewImg"]);
            model.Flow = ConverToStr(rdr["Flow"]);
            model.UseRemind = ConverToStr(rdr["UseRemind"]);
            rdr.Close();
            return model;
        }
        public M_Arrive GetModelFromReader(DataRow rdr)
        {
            M_Arrive model = new M_Arrive();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.ArriveNO = ConverToStr(rdr["ArriveNO"]);
            model.UserID = Convert.ToInt32(rdr["UserID"]);
            model.MinAmount = ConverToDouble(rdr["MinAmount"]);
            model.MaxAmount = ConverToDouble(rdr["MaxAmount"]);
            model.Amount = Convert.ToInt32(rdr["Amount"]);
            model.AgainTime = ConvertToDate(rdr["AgainTime"]);
            model.EndTime = ConvertToDate(rdr["EndTime"]);
            model.State = Convert.ToInt32(rdr["State"]);
            model.ArriveName = ConverToStr(rdr["ArriveName"]);
            model.ArrivePwd = ConverToStr(rdr["ArrivePwd"]);
            model.UseTime = ConvertToDate(rdr["UseTime"]);
            model.Type = ConvertToInt(rdr["Type"]);
            model.ArriveDesc = ConverToStr(rdr["ArriveDesc"]);
            model.PreviewImg = ConverToStr(rdr["PreviewImg"]);
            model.Flow = ConverToStr(rdr["Flow"]);
            model.UseRemind = ConverToStr(rdr["UseRemind"]);
            return model;
        }
    }
}