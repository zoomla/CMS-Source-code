namespace ZoomLa.DZNT
{
    using System;

    /// <summary>
    /// Discuzz!NT2.5 论坛会员整合接口 会员模型
    /// </summary>
    public class UserInfo
    {
        private int m_uid;
        private string m_username;        
        private string m_password;
        private int m_gender;        
        private int m_groupid;
        private string m_regip;
        private string m_lastip;
        private string m_email;
        private bool m_IsNull;

        public UserInfo()
        {
            this.m_uid = 0;
            this.m_username = "";            
            this.m_password = "";
            this.m_gender = 0;            
            this.m_groupid = 10;
            this.m_regip = "";
            this.m_lastip = "";
            this.m_email = "";
            this.m_IsNull = false;
        }
        public UserInfo(bool value)
        {
            this.m_IsNull = value;
        }
        /// <summary>
        /// 会员id
        /// </summary>
        public int uid
        {
            get { return this.m_uid; }
            set { this.m_uid = value; }
        }
        /// <summary>
        /// 会员名
        /// </summary>
        public string username
        {
            get { return this.m_username; }
            set { this.m_username = value; }
        } 
        /// <summary>
        /// 会员密码
        /// </summary>
        public string password
        {
            get { return this.m_password; }
            set { this.m_password = value; }
        }
        /// <summary>
        /// 性别 男=0 女=1
        /// </summary>
        public int gender
        {
            get { return this.m_gender; }
            set { this.m_gender = value; }
        }
        /// <summary>
        /// 用户组 10=新手上路
        /// </summary>
        public int groupid
        {
            get { return this.m_groupid; }
            set { this.m_groupid = value; }
        }
        /// <summary>
        /// 注册ip
        /// </summary>
        public string regip
        {
            get { return this.m_regip; }
            set { this.m_regip = value; }
        }
        /// <summary>
        /// 最后登陆ip
        /// </summary>
        public string lastip
        {
            get { return this.m_lastip; }
            set { this.m_lastip = value; }
        }
        /// <summary>
        /// 会员邮箱
        /// </summary>
        public string email
        {
            get { return this.m_email; }
            set { this.m_email = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsNull
        {
            get { return this.m_IsNull; }            
        }
    }
}