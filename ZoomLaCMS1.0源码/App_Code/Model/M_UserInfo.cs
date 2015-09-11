namespace ZoomLa.Model
{
    using ZoomLa.Common;
    using ZoomLa.Model;
    using System;
    using ZoomLa.ZLEnum;

    
    public class M_UserInfo
    {
        //会员名
        private string m_UserName;
        //会员密码
        private string m_UserPwd;
        //会员ID
        private int m_UserID;
        //会员组
        private int m_GroupID;        
        //注册Email
        private string m_Email;
        //密码提示问题
        private string m_Question;
        //密码提示答案
        private string m_Answer;
        //头像
        private string m_UserFace;
        //注册时间
        private DateTime m_RegTime;
        //登录次数
        private int m_LoginTimes;
        //最近登录时间
        private DateTime m_LastLoginTime;
        //最近登录IP
        private string m_LastLoginIP;
        //最近修改密码时间
        private DateTime m_LastPwdChangeTime;
        //最近被锁定时间
        private DateTime m_LastLockTime;
        //会员状态
        private int m_Status;
        //邮件验证码
        private string m_CheckNum;
        //头像宽度
        private int m_FaceWidth;
        //头像高度
        private int m_FaceHeight;
        //签名
        private string m_Sign;
        //隐私设置
        private int m_PrivacySetting;
        //是否空对象
        private bool m_IsNull;

        public M_UserInfo()
        {

        }

        public M_UserInfo(bool value)
        {
            this.m_IsNull = value;
        } 
        /// <summary>
        /// 
        /// </summary>
        public string UserName
        {
            get
            {
                return this.m_UserName;
            }
            set
            {
                this.m_UserName = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UserPwd
        {
            get
            {
                return this.m_UserPwd;
            }
            set
            {
                this.m_UserPwd=value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public int UserID
        {
            get
            {
                return this.m_UserID;
            }
            set
            {
                this.m_UserID = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public int GroupID
        {
            get
            {
                return this.m_GroupID;
            }
            set
            {
                this.m_GroupID = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
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
        /// <summary>
        /// 
        /// </summary>
        public string Question
        {
            get
            {
                return this.m_Question;
            }
            set
            {
                this.m_Question = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Answer
        {
            get
            {
                return this.m_Answer;
            }
            set
            {
                this.m_Answer = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UserFace
        {
            get
            {
                return this.m_UserFace;
            }
            set
            {
                this.m_UserFace = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime RegTime
        {
            get
            {
                return this.m_RegTime;
            }
            set
            {
                this.m_RegTime = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public int LoginTimes
        {
            get
            {
                return this.m_LoginTimes;
            }
            set
            {
                this.m_LoginTimes = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime LastLoginTimes
        {
            get
            {
                return this.m_LastLoginTime;
            }
            set
            {
                this.m_LastLoginTime = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string LastLoginIP
        {
            get
            {
                return this.m_LastLoginIP;
            }
            set
            {
                this.m_LastLoginIP = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime LastPwdChangeTime
        {
            get
            {
                return this.m_LastPwdChangeTime;
            }
            set
            {
                this.m_LastPwdChangeTime = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime LastLockTime
        {
            get
            {
                return this.m_LastLockTime;
            }
            set
            {
                this.m_LastLockTime = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Status
        {
            get
            {
                return this.m_Status;
            }
            set
            {
                this.m_Status = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CheckNum
        {
            get
            {
                return this.m_CheckNum;
            }
            set
            {
                this.m_CheckNum = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public int FaceHeight
        {
            get
            {
                return this.m_FaceHeight;
            }
            set
            {
                this.m_FaceHeight = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public int FaceWidth
        {
            get
            {
                return this.m_FaceWidth;
            }
            set
            {
                this.m_FaceWidth = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Sign
        {
            get
            {
                return this.m_Sign;
            }
            set
            {
                this.m_Sign = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public int PrivacySetting
        {
            get
            {
                return this.m_PrivacySetting;
            }
            set
            {
                this.m_PrivacySetting = value;
            }
        }
        public bool IsNull
        {
            get { return this.m_IsNull; }
        }
    }
}