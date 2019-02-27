using System;

/// <summary>
/// M_Bank 的摘要说明
/// </summary>

namespace ZoomLa.Model
{

    public class M_Bank
    {

        public M_Bank()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        public int BankID { get; set; }
        public string BankShortName { get; set; }
        public string BankName { get; set; }
        public string Accounts { get; set; }
        public string CardNum { get; set; }
        public string HolderName { get; set; }
        public string BankIntro { get; set; }
        public string BankPic { get; set; }
        public int OrderID { get; set; }
        public bool IsDefault { get; set; }
        public bool IsDisabled { get; set; }
    }
}
