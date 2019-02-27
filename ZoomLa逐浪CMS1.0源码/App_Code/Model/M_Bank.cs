using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// M_Bank 的摘要说明
/// </summary>

namespace ZoomLa.Model
{

    public class M_Bank
    {
        private int m_BankID; //银行帐户ID
        private string m_BankShortName; //帐户名称
        private string m_BankName; //开户行
        private string m_Accounts; //帐号
        private string m_CardNum; //卡号
        private string m_HolderName; //户名
        private string m_BankIntro;//帐户说明
        private string m_BankPic;//银行图标
        private int m_OrderID;//排序ID
        private bool m_IsDefault;//是否为默认银行帐户
        private bool m_IsDisabled;//是否禁用

        public M_Bank()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        public int BankID
        {
            get { return this.m_BankID; }
            set { this.m_BankID = value; }
        }
        public string BankShortName
        {
            get { return this.m_BankShortName; }
            set { this.m_BankShortName= value; }
        }
        public string BankName
        {
            get { return this.m_BankName; }
            set { this.m_BankName = value; }
        }
        public string Accounts
        {
            get { return this.m_Accounts; }
            set { this.m_Accounts = value; }
        }
        public string CardNum
        {
            get { return this.m_CardNum; }
            set { this.m_CardNum = value; }
        }
        public string HolderName
        {
            get { return this.m_HolderName; }
            set { this.m_HolderName = value; }
        }
        public string BankIntro
        {
            get { return this.m_BankIntro; }
            set { this.m_BankIntro = value; }
        }
        public string BankPic
        {
            get { return this.m_BankPic; }
            set { this.m_BankPic = value; }
        }
        public int OrderID
        {
            get { return this.m_OrderID; }
            set { this.m_OrderID = value; }
        }
        public bool IsDefault
        {
            get { return this.m_IsDefault; }
            set { this.m_IsDefault = value; }
        }
        public bool IsDisabled
        {
            get { return this.m_IsDisabled; }
            set { this.m_IsDisabled = value; }
        }
    }
}
