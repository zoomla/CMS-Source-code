using System;
using System.Data.SqlClient;
using System.Data;
namespace ZoomLa.Model
{
    [Serializable]
    public class M_Accountinfo : M_Base
    {
        #region 属性定义
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 支付ID
        /// </summary>
        public int PayId { get; set; }
        /// <summary>
        /// 开户行及支行全称
        /// </summary>
        public string BankOfDeposit { get; set; }
        /// <summary>
        /// 开户人真实姓名
        /// </summary>
        public string NameOfDeposit { get; set; }
        /// <summary>
        /// 开户帐号
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 会员真实姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 会员身份证号码
        /// </summary>
        public string CardID { get; set; }
        /// <summary>
        /// 是否绑定用户真实姓名:0为未绑定,1为已绑定
        /// </summary>
        public int Lock { get; set; }
        /// <summary>
        /// 所属用户ID
        /// </summary>
        public int UserId { get; set; }
        #endregion

        #region 构造函数
        public M_Accountinfo()
        {
            this.BankOfDeposit = string.Empty;
            this.NameOfDeposit = string.Empty;
            this.Account = string.Empty;
            this.Name = string.Empty;
            this.CardID = string.Empty;
        }
        public M_Accountinfo
        (
            int id,
            string BankOfDeposit,
            string NameOfDeposit,
            string Account,
            string Name,
            string CardID,
            int Lock,
            int UserId,
            int payid
        )
        {
            this.id = id;
            this.BankOfDeposit = BankOfDeposit;
            this.NameOfDeposit = NameOfDeposit;
            this.Account = Account;
            this.Name = Name;
            this.CardID = CardID;
            this.Lock = Lock;
            this.UserId = UserId;
            this.PayId = payid;
        }
        /// <summary>
        /// 返回实体列表数组
        /// </summary>
        /// <returns>String[]</returns>
        public string[] AccountinfoList()
        {
            string[] Tablelist = { "id", "BankOfDeposit", "NameOfDeposit", "Account", "Name", "CardID", "Lock", "UserId", "PayId" };
            return Tablelist;
        }
        #endregion
        public override string TbName { get { return "ZL_Accountinfo"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"id","Int","4"},
                                  {"BankOfDeposit","NVarChar","50"},
                                  {"NameOfDeposit","NVarChar","50"},
                                  {"Account","NVarChar","50"},
                                  {"Name","NVarChar","50"},
                                  {"CardID","NVarChar","50"},
                                  {"Lock","Int","4"},
                                  {"UserId","Int","4"},
                                  {"PayId","Int","4"} 
                                 };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_Accountinfo model = this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.id;
            sp[1].Value = model.BankOfDeposit;
            sp[2].Value = model.NameOfDeposit;
            sp[3].Value = model.Account;
            sp[4].Value = model.Name;
            sp[5].Value = model.CardID;
            sp[6].Value = model.Lock;
            sp[7].Value = model.UserId;
            sp[8].Value = model.PayId;
            return sp;
        }

        public M_Accountinfo GetModelFromReader(SqlDataReader rdr)
        {
            M_Accountinfo model = new M_Accountinfo();
            model.id = Convert.ToInt32(rdr["id"]);
            model.BankOfDeposit = ConverToStr(rdr["BankOfDeposit"]); ;
            model.NameOfDeposit = ConverToStr(rdr["NameOfDeposit"]);
            model.Account = ConverToStr(rdr["Account"]);
            model.Name = ConverToStr(rdr["Name"]);
            model.CardID = ConverToStr(rdr["CardID"]); ;
            model.Lock = ConvertToInt(rdr["Lock"]);
            model.UserId = ConvertToInt(rdr["UserId"]);
            model.PayId = ConvertToInt(rdr["PayId"]);
            rdr.Close();
            return model;
        }
    }
}
