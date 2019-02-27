namespace ZoomLa.Model
{
    using System;

    /// <summary>
    /// 管理员信息
    /// </summary>
    public class M_AdminInfo
    {
        //管理员ID
        private int m_AdminId;
        //管理员名
        private string m_AdminName;
        //管理员密码
        private string m_AdminPassword;
        //管理员能否更改密码标识
        private bool m_EnableModifyPassword;
        //管理员是否能多人同时登录标识
        private bool m_EnableMultiLogin;
        //管理员是否被锁定标识
        private bool m_IsLock;
        //最近登录IP
        private string m_LastLoginIP;
        //最近登录时间
        private DateTime m_LastLoginTime;
        //最近退出登录时间
        private DateTime m_LastLogoutTime;
        //最近更改密码时间
        private DateTime m_LastModifyPasswordTime;
        //登录次数
        private int m_LoginTimes;
        //角色组ID数组字符串        
        private string m_RoleList;
        //角色组ID数组
        private string[] m_roles;
        //管理员在前台的用户名
        private string m_UserName;
        //后台管理页面主题
        private string m_Theme;
        //是否是空对象
        private bool m_IsNull=false;
        //登录随机数字
        private string m_RandNumber;      
        /// <summary>
        /// 管理员信息模型 构造函数
        /// </summary>
        public M_AdminInfo()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        /// <summary>
        /// 管理员信息模型空对象 构造函数
        /// </summary>
        public M_AdminInfo(bool value)
        {
            this.m_IsNull = value;
        }
        /// <summary>
        /// 管理员ID
        /// </summary>
        public int AdminId
        {
            get
            {
                return this.m_AdminId;
            }
            set
            {
                this.m_AdminId = value;
            }
        }
        /// <summary>
        /// 管理员名
        /// </summary>
        public string AdminName
        {
            get
            {
                return this.m_AdminName;
            }
            set
            {
                this.m_AdminName = value;
            }
        }
        /// <summary>
        /// 管理员密码
        /// </summary>
        public string AdminPassword
        {
            get
            {
                return this.m_AdminPassword;
            }
            set
            {
                this.m_AdminPassword = value;
            }
        }
        /// <summary>
        /// 管理员能否更改密码标识
        /// </summary>
        public bool EnableModifyPassword
        {
            get
            {
                return this.m_EnableModifyPassword;
            }
            set
            {
                this.m_EnableModifyPassword = value;
            }
        }
        /// <summary>
        /// 管理员是否能重复登录
        /// </summary>
        public bool EnableMultiLogin
        {
            get
            {
                return this.m_EnableMultiLogin;
            }
            set
            {
                this.m_EnableMultiLogin = value;
            }
        }
        /// <summary>
        /// 管理员是否被锁定
        /// </summary>
        public bool IsLock
        {
            get
            {
                return this.m_IsLock;
            }
            set
            {
                this.m_IsLock = value;
            }
        }
        /// <summary>
        /// 最近登录IP
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
        /// 最近登录时间
        /// </summary>
        public DateTime LastLoginTime
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
        /// 最近退出登录时间
        /// </summary>
        public DateTime LastLogoutTime
        {
            get
            {
                return this.m_LastLogoutTime;
            }
            set
            {
                this.m_LastLogoutTime = value;
            }
        }
        /// <summary>
        /// 最近修改密码时间
        /// </summary>
        public DateTime LastModifyPasswordTime
        {
            get
            {
                return this.m_LastModifyPasswordTime;
            }
            set
            {
                this.m_LastModifyPasswordTime = value;
            }
        }
        /// <summary>
        /// 登录次数
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
        /// 角色ID字符串（用","分隔）
        /// </summary>
        public string RoleList
        {
            get
            {
                return this.m_RoleList;
            }
            set
            {
                this.m_RoleList = value;
                if (!string.IsNullOrEmpty(value))
                {
                    this.m_roles = value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                }
            }
        }
        /// <summary>
        /// 管理员前台用户名
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
        /// 管理员后台页面主题
        /// </summary>
        public string Theme
        {
            get
            {
                return this.m_Theme;
            }
            set
            {
                this.m_Theme = value;
            }
        }
        /// <summary>
        /// 管理员对象是否空对象
        /// </summary>
        public bool IsNull
        {
            get
            {
                return this.m_IsNull;
            }
        }
        /// <summary>
        /// 登录随机密码用于验证是否重复登录
        /// </summary>
        public string RandNumber
        {
            get
            {
                return this.m_RandNumber;
            }
            set
            {
                this.m_RandNumber = value;
            }
        }
        /// <summary>
        /// 判断是否属于某些角色
        /// </summary>
        /// <param name="role">角色ID字符串</param>
        /// <returns>如果属于角色组中某角色返回true,反之返回false</returns>
        public bool IsInRole(string role)
        {
            if ((role != null) && (this.m_roles != null))
            {
                string[] strArray = role.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string str in strArray)
                {
                    for (int i = 0; i < this.m_roles.Length; i++)
                    {
                        if (!(string.IsNullOrEmpty(this.m_roles[i]) || (string.Compare(this.m_roles[i], str, StringComparison.OrdinalIgnoreCase) != 0)))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        /// <summary>
        /// 是否是超级管理员
        /// </summary>
        public bool IsSuperAdmin
        {
            get
            {
                return this.IsInRole("0");
            }
        }
    }
}