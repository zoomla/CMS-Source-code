namespace ZoomLa.Model
{
    using System;
    using System.Data.SqlClient;
    using System.Data;

    [Serializable]
    public class M_UserBank:M_Base
    {
        #region 定义字段
        /// <summary>
        /// 
        /// </summary>
        public int Bank_ID { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// 银行名称
        /// </summary>
        public string BankName { get; set; }
        /// <summary>
        /// 开户名
        /// </summary>
        public string BankUserName { get; set; }
        /// <summary>
        /// 银行卡号
        /// </summary>
        public string BankNumber { get; set; }
        /// <summary>
        /// 卡片类型
        /// </summary>
        public string CardType { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
     
        #endregion

        #region 构造函数
        public M_UserBank()
        {
        }

        public M_UserBank
        (
            int Bank_ID,
            int UserID,
            string BankName,
            string BankUserName,
            string BankNumber,
            string CardType,
            string Remark
        )
        {
            this.Bank_ID = Bank_ID;
            this.UserID = UserID;
            this.BankName = BankName;
            this.BankUserName = BankUserName;
            this.BankNumber = BankNumber;
            this.CardType = CardType;
            this.Remark = Remark;
        }
        /// <summary>
        /// 返回实体列表数组
        /// </summary>
        /// <returns>String[]</returns>
        public string[] UserBankList()
        {
            string[] Tablelist = { "Bank_ID", "UserID", "BankName", "BankUserName", "BankNumber", "CardType", "Remark" };
            return Tablelist;
        }
        #endregion
        public override string TbName { get { return "ZL_UserBank"; } }
        public override string PK { get { return "Bank_ID"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"Bank_ID","Int","4"},
                                  {"UserID","Int","4"},
                                  {"BankName","NVarChar","1000"},
                                  {"BankUserName","NVarChar","1000"}, 
                                  {"BankNumber","NVarChar","1000"},
                                  {"CardType","NVarChar","1000"},
                                  {"Remark","NVarChar","4000"}
                                 };
            return Tablelist;
        }

        public override SqlParameter[] GetParameters()
        {
            M_UserBank model = this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.Bank_ID;
            sp[1].Value = model.UserID;
            sp[2].Value = model.BankName;
            sp[3].Value = model.BankUserName;
            sp[4].Value = model.BankNumber;
            sp[5].Value = model.CardType;
            sp[6].Value = model.Remark;
            return sp;
        }
        public M_UserBank GetModelFromReader(SqlDataReader rdr)
        {
            M_UserBank model = new M_UserBank();
            model.Bank_ID = Convert.ToInt32(rdr["Bank_ID"]);
            model.UserID = Convert.ToInt32(rdr["UserID"]);
            model.BankName = ConverToStr(rdr["BankName"]);
            model.BankUserName = ConverToStr(rdr["BankUserName"]);
            model.BankNumber = ConverToStr(rdr["BankNumber"]);
            model.CardType = ConverToStr(rdr["CardType"]);
            model.Remark = ConverToStr(rdr["Remark"]); 
            rdr.Close();
            rdr.Dispose();
            return model;
        }
    }
}