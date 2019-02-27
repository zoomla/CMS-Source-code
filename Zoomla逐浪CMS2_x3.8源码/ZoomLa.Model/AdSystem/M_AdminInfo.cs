namespace ZoomLa.Model
{
    using System;
    using System.Data.SqlClient;
    using System.Data;
    using System.Data.Common; 
    public class M_AdminInfo:M_Base
    {
        #region 成员列表

        //角色组ID数组字符串        
        public string m_RoleList;
        //角色组ID数组
        private string[] m_roles;

        /// <summary>
        /// 管理员信息模型 构造函数
        /// </summary>
        public M_AdminInfo()
        {
            this.IsNull = false;
        }
        /// <summary>
        /// 管理员信息模型空对象 构造函数
        /// </summary>
        public M_AdminInfo(bool value)
        {
            this.IsNull = value;
        }
        /// <summary>
        /// 管理员真实姓名
        /// </summary>
        public string AdminTrueName
        {
            get;
            set;
        }
        /// <summary>
        /// 管理员ID
        /// </summary>
        public int PubRole { get; set; }
        public int NodeRole { get; set; }
        public int AdminId { get; set; }
        /// <summary>
        /// 管理员名
        /// </summary>
        public string AdminName { get; set; }
        /// <summary>
        /// 管理员密码
        /// </summary>
        public string AdminPassword { get; set; }
        /// <summary>
        /// 管理员能否更改密码标识
        /// </summary>
        public bool EnableModifyPassword { get; set; }
        /// <summary>
        /// 管理员是否能重复登录
        /// </summary>
        public bool EnableMultiLogin { get; set; }
        /// <summary>
        /// 管理员是否被锁定
        /// </summary>
        public bool IsLock { get; set; }
        /// <summary>
        /// 管理员是否启用个性桌面
        /// </summary>
        public bool IsTable { get; set; }
        /// <summary>
        /// 最近登录IP
        /// </summary>
        public string LastLoginIP { get; set; }
        /// <summary>
        /// 最近登录时间
        /// </summary>
        public DateTime LastLoginTime { get; set; }
        /// <summary>
        /// 最近退出登录时间
        /// </summary>
        public DateTime LastLogoutTime { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CDate { get; set; }
        /// <summary>
        /// 最近修改密码时间
        /// </summary>
        public DateTime LastModifyPasswordTime { get; set; }
        /// <summary>
        /// 登录次数
        /// </summary>
        public int LoginTimes { get; set; }
        /// <summary>
        /// 角色ID字符串（用","分隔）
        /// </summary>
        public string RoleList
        {
            get
            {
                m_RoleList = string.IsNullOrEmpty(m_RoleList) ? "" : "," + (m_RoleList.Trim(',')) + ",";
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
        public string UserName { get; set; }
        /// <summary>
        /// 管理员后台页面主题
        /// </summary>
        public string Theme { get; set; }
        /// <summary>
        /// 管理员对象是否空对象
        /// </summary>
        public bool IsNull { get; private set; }
        /// <summary>
        /// 登录随机密码用于验证是否重复登录
        /// </summary>
        public string RandNumber { get; set; }
        /// <summary>
        /// 内容添加默认权限
        /// </summary>
        public int DefaultStart { get; set; }
        /// <summary>
        /// 判断是否属于某些角色
        /// </summary>
        /// <param Name="role">角色ID字符串</param>
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
        /// 当前用户是否超级管理员
        /// </summary>
        /// <returns></returns>
        public bool IsSuperAdmin()
        {
            string rolelist = "," + RoleList + ",";
            if (rolelist.IndexOf(",1,") > -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 是否是超级管理员
        /// </summary>
        public bool IsSuperAdmin(string rolelist)
        {
            rolelist = "," + rolelist + ",";
            if (rolelist.IndexOf(",1,") > -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 管理节点,当AdminType为2站点管理员时,该字段记录管理的站点ID
        /// </summary>
        public string ManageNode { get; set; }
        /// <summary>
        /// 管理员类型  1超级管理员
        /// </summary>
        public int AdminType { get; set; }
        /// <summary>
        /// 该管理员账号,绑定的用户ID(暂未启用)
        /// </summary>
        public int AddUserID { get; set; }

        /// <summary>
        /// 场景模块名
        /// </summary>
        public string StructureID { get; set; }
        #endregion
        public override string PK { get { return "AdminID"; } }
        public override string TbName { get { return "ZL_Manager"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"AdminID","Int","4"},
                                  {"AdminName","NVarChar","50"},
                                  {"AdminPassword","NVarChar","100"},
                                  {"UserName","NVarChar","255"},
                                  {"EnableMultiLogin","Bit","5"},
                                  {"LoginTimes","Int","4"},
                                  {"LastLoginIP","NVarChar","50"},
                                  {"LastLoginTime","DateTime","8"},
                                  {"LastLogoutTime","DateTime","8"},
                                  {"LastModifyPwdTime","DateTime","8"},
                                  {"IsLock","Bit","10"},    
                                  {"EnableModifyPassword","Bit","8"},
                                  {"AdminRole","NText","4000"},
                                  {"Theme","NVarChar","4000"},
                                  {"RandNumber","NVarChar","50"},
                                  {"NodeRole","Int","4"},
                                  {"PubRole","Int","4"},
                                  {"DefaultStart","Int","4"},
                                  {"AdminType","Int","4"},
                                  {"ManageNode","NVarChar","500"},
                                  {"AddUserID","Int","4"},
                                  {"AdminTrueName","NVarChar","50"},
                                  {"IsTable","Bit","10"},
                                  {"StructureID","NVarChar","255"},
                                  {"CDate","DateTime","8"}
                                 };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_AdminInfo model = this;
            EmptyDeal(model);
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.AdminId;
            sp[1].Value =  SafeStr(model.AdminName);
            sp[2].Value = model.AdminPassword;
            sp[3].Value = model.UserName; 
            sp[4].Value = model.EnableMultiLogin;
            sp[5].Value = model.LoginTimes;
            sp[6].Value = model.LastLoginIP;
            sp[7].Value = model.LastLoginTime;
            sp[8].Value = model.LastLogoutTime;
            sp[9].Value = model.LastModifyPasswordTime;  
            sp[10].Value = model.IsLock;
            sp[11].Value = model.EnableModifyPassword;
            sp[12].Value = model.RoleList;
            sp[13].Value = model.Theme;
            sp[14].Value = model.RandNumber;
            sp[15].Value = model.NodeRole;
            sp[16].Value = model.PubRole;
            sp[17].Value = model.DefaultStart;
            sp[18].Value = model.AdminType;
            sp[19].Value = model.ManageNode;
            sp[20].Value = model.AddUserID;
            sp[21].Value = SafeStr(model.AdminTrueName);
            sp[22].Value = model.IsTable;
            sp[23].Value = model.StructureID;
            sp[24].Value = model.CDate;
            return sp;
        }
        public void EmptyDeal(M_AdminInfo model)
        {
            if (model.LastLoginTime.Year < 1910) model.LastLoginTime = DateTime.Now;
            if (model.LastLogoutTime.Year < 1910) model.LastLogoutTime = DateTime.Now;
            if (model.LastModifyPasswordTime.Year < 1910) model.LastModifyPasswordTime = DateTime.Now;
            if (model.CDate <= DateTime.MinValue) { model.CDate = DateTime.Now; }
        }
        public M_AdminInfo GetModelFromReader(DbDataReader rdr)
        {
            M_AdminInfo model = new M_AdminInfo();
            model.AdminId = Convert.ToInt32(rdr["AdminID"]);
            model.AdminName = rdr["AdminName"].ToString();
            model.AdminPassword = rdr["AdminPassword"].ToString();
            model.UserName = ConverToStr(rdr["UserName"]);
            model.EnableMultiLogin = ConverToBool(rdr["EnableMultiLogin"]);
            model.LoginTimes = ConvertToInt(rdr["LoginTimes"]);
            model.LastLoginIP = ConverToStr(rdr["LastLoginIP"]);
            model.LastLoginTime = ConvertToDate(rdr["LastLoginTime"]);
            model.LastLogoutTime = ConvertToDate(rdr["LastLogoutTime"]);
            model.LastModifyPasswordTime = ConvertToDate(rdr["LastModifyPwdTime"]);
            model.IsLock = ConverToBool(rdr["IsLock"]);
            model.EnableModifyPassword = ConverToBool(rdr["EnableModifyPassword"]);
            model.RoleList = ConverToStr(rdr["AdminRole"]);
            model.Theme = ConverToStr(rdr["Theme"]);
            model.RandNumber = ConverToStr(rdr["RandNumber"]);
            model.NodeRole = ConvertToInt(rdr["NodeRole"]);
            model.PubRole = ConvertToInt(rdr["PubRole"]);
            model.DefaultStart = ConvertToInt(rdr["DefaultStart"]);
            model.AdminType = ConvertToInt(rdr["AdminType"]);
            model.ManageNode = ConverToStr(rdr["ManageNode"]);
            model.AddUserID = ConvertToInt(rdr["AddUserID"]);
            model.AdminTrueName = ConverToStr(rdr["AdminTrueName"]);
            model.IsTable = ConverToBool(rdr["IsTable"]);
            model.StructureID = ConverToStr(rdr["StructureID"]);
            model.CDate = ConvertToDate(rdr["CDate"]);
            rdr.Close();
            return model;
        }
    }
}