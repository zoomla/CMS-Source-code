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
/// M_PayPlat 的摘要说明
/// </summary>

namespace ZoomLa.Model
{

    public class M_PayPlat
    {
        private int m_PayPlatformID; //平台ID
        private string m_PayPlatformName; //平台名称
        private string m_AccountsID; //帐号
        private string m_MD5; //MD5密钥
        private float m_Rate; //手续费
        private int m_OrderID; //排序ID
        private bool m_IsDisabled;//是否启用
        private bool m_IsDefault;//是否默认

        public M_PayPlat()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        public int PayPlatformID
        {
            get { return this.m_PayPlatformID; }
            set { this.m_PayPlatformID = value; }
        }

        public string PayPlatformName
        {
            get { return this.m_PayPlatformName; }
            set { this.m_PayPlatformName = value; }
        }
        public string AccountsID
        {
            get { return this.m_AccountsID; }
            set { this.m_AccountsID = value; }
        }
        public string MD5
        {
            get { return this.m_MD5; }
            set { this.m_MD5 = value; }
        }
        public float Rate
        {
            get { return this.m_Rate; }
            set { this.m_Rate = value; }
        }
        public int OrderID
        {
            get { return this.m_OrderID; }
            set { this.m_OrderID = value; }
        }
        public bool IsDisabled
        {
            get { return this.m_IsDisabled; }
            set { this.m_IsDisabled = value; }
        }
        public bool IsDefault
        {
            get { return this.m_IsDefault; }
            set { this.m_IsDefault = value; }
        }
    }
}
