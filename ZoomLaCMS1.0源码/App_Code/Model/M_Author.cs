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
    /// M_Author 的摘要说明
    /// </summary>
    public class M_Author
    {
        private int m_ID;//自动增加
        private int m_AuthorID;//用户ID-UserID
        private string m_AuthorType;//作者类型
        private string m_AuthorName;//作者名
        private bool m_AuthorPassed;//是否通过审核 默认 0
        private bool m_AuthoronTop;//是否置顶-0 
        private bool m_AuthorIsElite;//是否推荐 -0
        private int m_AuthorHits;//点击数
        private DateTime m_AuthorLastUseTime;//最近登录时间
        private int m_AuthorTemplateID;//作者模板
        private string m_AuthorPhoto;//作者头像
        private string m_AuthorIntro;//作者简介

        private string m_AuthorAddress;//作者地址
        private string m_AuthorTel;//电话
        private string m_AuthorFax;//传真
        private string m_AuthorMail;//通信地址
        private string m_AuthorEmail;//电子邮件
        private int m_AuthorZipCode;//邮政编码

        private string m_AuthorHomePage;//个人网站
        private string m_AuthorIm;//Imeeting，即时聊天号码
        private int m_AuthorSex;//性别 0-男 1-女

        private DateTime m_AuthorBirthDay;//生日
        private string m_AuthorCompany;//公司名
        private string m_AuthorDepartment;//部门
       
        public M_Author()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        public int ID
        {
            get
            {
                return this.m_ID;
            }
            set
            {
                this.m_ID= value;
            }
        }
        public int AuthorID
        {
            get
            {
                return this.m_AuthorID;
            }
            set
            {
                this.m_AuthorID= value;
            }
        }
        public string AuthorType
        {
            get
            {
                return this.m_AuthorType;
            }
            set
            {
                this.m_AuthorType = value;
            }
        }
        public string AuthorName
        {
            get
            {
                return this.m_AuthorName;
            }
            set
            {
                this.m_AuthorName = value;
            }
        }
        public bool AuthorPassed
        {
            get
            {
                return this.m_AuthorPassed;

            }
            set
            {
                this.m_AuthorPassed = value;
            }
        }
        public bool AuthoronTop
        {
            get
            {
                return this.m_AuthoronTop;

            }
            set
            {
                this.m_AuthoronTop= value;
            }
        }
        public bool AuthorIsElite
        {
            get
            {
                return this.m_AuthorIsElite;

            }
            set
            {
                this.m_AuthorIsElite = value;
            }
        }
        public int AuthorHits
        {
            get
            {
                return this.m_AuthorHits;
            }
            set
            {
                this.m_AuthorHits = value;
            }
        }
        public DateTime AuthorLastUseTime
        {
            get
            {
                return this.m_AuthorLastUseTime;

            }
            set
            {
                this.m_AuthorLastUseTime = value;
            }
        }
        public int AuthorTemplateID
        {
            get
            {
                return this.m_AuthorTemplateID;
            }
            set
            {
                this.m_AuthorTemplateID= value;
            }
        }
        public string AuthorPhoto
        {
            get
            {
                return this.m_AuthorPhoto;
            }
            set
            {
                this.m_AuthorPhoto = value;
            }
        }
        public string AuthorIntro
        {
            get
            {
                return this.m_AuthorIntro;
            }
            set
            {
                this.m_AuthorIntro = value;
            }
        }
        public string AuthorAddress
        {
            get
            {
                return this.m_AuthorAddress;
            }
            set
            {
                this.m_AuthorAddress = value;
            }
        }
        public string AuthorTel
        {
            get
            {
                return this.m_AuthorTel;
            }
            set
            {
                this.m_AuthorTel= value;
            }
        }
        public string AuthorFax
        {
            get
            {
                return this.m_AuthorFax;
            }
            set
            {
                this.m_AuthorFax = value;
            }
        }
        public string AuthorMail
        {
            get
            {
                return this.m_AuthorMail;
            }
            set
            {
                this.m_AuthorMail= value;
            }
        }
        public string AuthorEmail
        {
            get
            {
                return this.m_AuthorEmail;
            }
            set
            {
                this.m_AuthorEmail= value;
            }
        }
        public int AuthorZipCode
        {
            get
            {
                return this.m_AuthorZipCode;
            }
            set
            {
                this.m_AuthorZipCode = value;
            }
        }
        public string AuthorHomePage
        {
            get
            {
                return this.m_AuthorHomePage;
            }
            set
            {
                this.m_AuthorHomePage = value;
            }
        }
        public string AuthorIm
        {
            get
            {
                return this.m_AuthorIm;
            }
            set
            {
                this.m_AuthorIm = value;
            }
        }
        public int AuthorSex
        {
            get
            {
                return this.m_AuthorSex;
            }
            set
            {
                this.m_AuthorSex = value;
            }
        }
        public DateTime AuthorBirthDay
        {
            get
            {
                return this.m_AuthorBirthDay;

            }
            set
            {
                this.m_AuthorBirthDay = value;
            }
        }  
        public string AuthorCompany
        {
            get
            {
                return this.m_AuthorCompany;
            }
            set
            {
                this.m_AuthorCompany = value;
            }
        }
        public string AuthorDepartment
        {
            get
            {
                return this.m_AuthorDepartment;
            }
            set
            {
                this.m_AuthorDepartment= value;
            }
        }
    }
}
