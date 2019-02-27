namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using System.Configuration;
    using ZoomLa.IDAL;
    using ZoomLa.Model;
    using ZoomLa.Components;
    using ZoomLa.Common;
    using System.Web;
    using ZoomLa.DALFactory;
    using System.Globalization;

    /// <summary>
    /// 有关Admin的业务逻辑实现
    /// </summary>
    public class B_Admin
    {
        private static readonly ID_Admin dal = IDal.CreateAdmin();

        public B_Admin()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        public static bool Add(M_AdminInfo adminInfo)
        {
            return dal.Add(adminInfo);
        }
        public static bool Update2(M_AdminInfo adminInfo)
        {
            return dal.Update2(adminInfo);
        }
        /// <summary>
        /// 管理员登录操作
        /// </summary>
        /// <param name="AdminName">管理员名</param>
        /// <param name="Password">管理员密码</param>
        /// <returns></returns>
        public static M_AdminInfo AuthenticateAdmin(string AdminName, string Password)
        {
            M_AdminInfo adminByAdminName = GetAdminByAdminName(AdminName);
            if (!adminByAdminName.IsNull)
            {
                string str = StringHelper.MD5(Password);
                if (!StringHelper.ValidateMD5(adminByAdminName.AdminPassword, str))
                {
                    return new M_AdminInfo(true);
                }
                adminByAdminName.LastLoginIP = HttpContext.Current.Request.UserHostAddress;
                adminByAdminName.LastLoginTime = DateTime.Now;
                adminByAdminName.LoginTimes++;
                adminByAdminName.RandNumber = DataSecurity.RandomNum(10);
                Update(adminByAdminName);
            }
            return adminByAdminName;
        }

        public static M_AdminInfo GetAdminByAdminId(int adminId)
        {
            if (adminId <= 0)
            {
                return new M_AdminInfo(true);
            }
            return dal.GetAdminByID(adminId);
        }

        public static M_AdminInfo GetAdminByAdminName(string adminName)
        {
            if (string.IsNullOrEmpty(adminName))
            {
                return new M_AdminInfo(true);
            }
            return dal.GetAdminByName(adminName);
        }

        public static bool IsExist(string adminName)
        {
            return dal.IsExist(adminName);
        }
        public static bool IsExist(int adminId)
        {
            return dal.IsExist(adminId);
        }
        public static bool DelAdminById(int adminId)
        {
            if (IsSuperManage(adminId))
                return false;

            return dal.Delete(adminId);
        }

        //public static bool Delete(int adminId)
        //{
        //    int aid = MYContext.Current.AdminPrincipal.AdminInfo.AdminId;
        //    if (aid == adminId || IsSuperManage(adminId))
        //    {
        //        return false;
        //    }
        //    //RoleMembers.RemoveMemberFromAllRoles(adminId);
        //    return dal.Delete(adminId);
        //}

        public static bool Update(M_AdminInfo adminInfo)
        {
            return dal.Update(adminInfo);
        }

        public static bool IsSuperManage(int adminId)
        {
            M_AdminInfo info = dal.GetAdminByID(adminId);
            string[] AdminRole = info.RoleList.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            return IsInRole("0", AdminRole);
        }
        /// <summary>
        /// 判断管理员权限
        /// </summary>
        /// <param name="OperCode"></param>
        /// <returns></returns>
        public bool ChkPermissions(string OperCode)
        {
            bool re = false;
            if (HttpContext.Current.Request.Cookies["ManageState"] == null)
                re = false;
            else
            {
                string loginName = HttpContext.Current.Request.Cookies["ManageState"]["LoginName"];
                string password = HttpContext.Current.Request.Cookies["ManageState"]["Password"];
                M_AdminInfo info = dal.GetLoginAdmin(loginName, password);
                if (info.IsNull)
                    re = false;
                else
                {
                    if (info.IsSuperAdmin)
                        re = true;
                    else
                    {
                        string roleids = B_Role.GetPowerInfo(OperCode);
                        if(info.IsInRole(roleids))
                            re=true;
                        else
                            re=false;
                    }
                }
            }
            return re;
        }
        /// <summary>
        /// 检测是否登录了
        /// </summary>
        public void CheckIsLogin()
        {
            string managePath = GetManagePath();
            if (HttpContext.Current.Request.Cookies["ManageState"] == null)
            {
                HttpContext.Current.Response.Redirect("~/"+managePath+"/Login.aspx");
            }
            else
            {
                string loginName = HttpContext.Current.Request.Cookies["ManageState"]["LoginName"];
                string password = HttpContext.Current.Request.Cookies["ManageState"]["Password"];
                if (this.GetLoginAdmin(loginName, password).IsNull)
                {
                    HttpContext.Current.Response.Redirect("~/" + managePath + "/Login.aspx");
                }
            }
        }
        private M_AdminInfo GetLoginAdmin(string loginName, string password)
        {
            return dal.GetLoginAdmin(loginName, password);
        }
        private M_AdminInfo GetModel(string loginName, string password)
        {
            return dal.GetModel(loginName, password);
        }
        private static string GetManagePath()
        {
            return SiteConfig.SiteOption.ManageDir.ToLower(CultureInfo.CurrentCulture);
        }
        /// <summary>
        /// 检测重复登录
        /// </summary>
        public void CheckMulitLogin()
        {
            this.CheckIsLogin();
            string str = string.Empty;
            if (HttpContext.Current.Request.Cookies["ManageState"]["randNum"] != null)
            {
                str = HttpContext.Current.Request.Cookies["ManageState"]["randNum"].ToString();
            }
            string userName = HttpContext.Current.Request.Cookies["ManageState"]["LoginName"].ToString();
            M_AdminInfo model = GetAdminByAdminName(userName);
            string managePath = GetManagePath();
            if (!model.EnableMultiLogin && (str != model.RandNumber))
            {
                ZoomLa.Web.Utility.WriteErrMsg("该管理员用户不能重复登录，请以别的管理员帐号重新登录", "../Login.aspx");
            }
        }
        /// <summary>
        /// 清除Cookie
        /// </summary>
        public static void ClearCookie()
        {
            HttpContext.Current.Response.Cookies["ManageState"].Expires = DateTime.Now.AddDays(-1.0);
        }
        /// <summary>
        /// 设定登录状态
        /// </summary>
        /// <param name="model"></param>
        public static void SetLoginState(M_AdminInfo model)
        {
            HttpContext.Current.Response.Cookies["ManageState"]["ManageId"] = model.AdminId.ToString();
            HttpContext.Current.Response.Cookies["ManageState"]["LoginName"] = model.AdminName;
            
            HttpContext.Current.Response.Cookies["UserState"]["LoginName"] = model.UserName;
            HttpContext.Current.Response.Cookies["UserState"]["Password"] = model.AdminPassword;
            HttpContext.Current.Response.Cookies["ManageState"]["Password"] = model.AdminPassword;
            HttpContext.Current.Response.Cookies["ManageState"]["Role"] = model.RoleList;
            //HttpContext.Current.Response.Cookies["ManageState"].Expires = DateTime.Now.AddDays(30);//30天有效期
            HttpContext.Current.Response.Redirect("~/Manage/Index.aspx");            
        }
        public static bool IsInRole(string role, string[] m_roles)
        {
            if ((role != null) && (m_roles != null))
            {
                string[] strArray = role.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string str in strArray)
                {
                    for (int i = 0; i < m_roles.Length; i++)
                    {
                        if (!(string.IsNullOrEmpty(m_roles[i]) || (string.Compare(m_roles[i], str, StringComparison.OrdinalIgnoreCase) != 0)))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        /// <summary>
        /// 判断管理员是否有操作权限
        /// </summary>
        /// <param name="openrole">有该项操作权限的角色ID数组</param>
        /// <param name="m_roles">管理员所属角色数组</param>
        /// <returns></returns>
        public bool HasRole(string openrole, string[] m_roles)
        {
            string[] role = openrole.Split(new char[] { ',' });
            bool flag = false;
            for (int i = 0; i < role.Length; i++)
            {
                if (IsInRole(role[i], m_roles))
                {
                    flag = true;
                    break;
                }
            }
            return flag;
        }
        public static DataView GetAdminInfo()
        {
            DataView dv = dal.SelectAdminInfo().Tables[0].DefaultView;
            return dv;
        }
    }
}