namespace ZoomLa.Model
{
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
    /// M_Source 的摘要说明
    /// </summary>
    public class M_Source
    {
        private int m_SourceID;//ID 自动列

        private string m_Type;//来源类型
        private string m_Name;//来源名
        private bool m_Passed;//是否通过审核
        private bool m_onTop;//是否置顶
        private bool m_IsElite;//是否推荐
        private int m_Hits;//点击数
        private DateTime m_LastUseTime;//最近登录时间
        private string m_Photo;//来源图片
        private string m_Intro;//简介
        private string m_Address;//地址
        private string m_Tel;//电话
        private string m_Fax;//传真
        private string m_Mail;//通信地址
        private string m_Email;//电子邮件
        private int m_ZipCode;//邮政编码
        private string m_HomePage;//个人网站
        private string m_Im;//即时聊天号码
        private string m_Contacter;//联系人名字
        public M_Source()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        public int SourceID
        {
            get
            {
                return this.m_SourceID;
            }
            set
            {
                this.m_SourceID = value;
            }
        }
        public string Type
        {
            get { return this.m_Type; }
            set { this.m_Type = value; }
        }
        public string Name
        {
            get { return this.m_Name; }
            set { this.m_Name = value; }
        }
        public bool Passed
        {
            get { return this.m_Passed; }
            set { this.m_Passed = value; }
        }
        public bool onTop
        {
            get { return this.m_onTop; }
            set { this.m_onTop = value; }
        }
        public bool IsElite
        {
            get { return this.m_IsElite; }
            set { this.m_IsElite = value; }
        }
        public int Hits
        {
            get
            {
                return this.m_Hits;
            }
            set
            {
                this.m_Hits = value;
            }
        }
        public DateTime LastUseTime
        {
            get
            {
                return this.m_LastUseTime;
            }
            set
            {
                this.m_LastUseTime = value;
            }
        }     
        public string Photo
        {
            get
            {
                return this.m_Photo;
            }
            set
            {
                this.m_Photo = value;
            }
        }
        public string Intro
        {
            get
            {
                return this.m_Intro;
            }
            set
            {
                this.m_Intro = value;
            }
        }
        public string Address
        {
            get
            {
                return this.m_Address;
            }
            set
            {
                this.m_Address = value;
            }
        }
        public string Tel
        {
            get
            {
                return this.m_Tel;
            }
            set
            {
                this.m_Tel = value;
            }
        }
        public string Fax
        {
            get
            {
                return this.m_Fax;
            }
            set
            {
                this.m_Fax = value;
            }
        }
        public string Mail
        {
            get
            {
                return this.m_Mail;
            }
            set
            {
                this.m_Mail = value;
            }
        }
        public string Email
        {
            get
            {
                return this.m_Email;
            }
            set
            {
                this.m_Email = value;
            }
        }
        //------------ZipCode
        public int ZipCode
        {
            get
            {
                return this.m_ZipCode;
            }
            set
            {
                this.m_ZipCode = value;
            }
        }
        public string HomePage
        {
            get
            {
                return this.m_HomePage;
            }
            set
            {
                this.m_HomePage = value;
            }
        }
        public string Im
        {
            get
            {
                return this.m_Im;
            }
            set
            {
                this.m_Im = value;
            }
        }
        public string Contacter
        {
            get
            {
                return this.m_Contacter;
            }
            set
            {
                this.m_Contacter = value;
            }
        }
    }
}
