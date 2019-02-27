namespace ZoomLa.BLL
{
    using System;
    using System.Configuration;
    using System.Data.SqlClient;
    using System.Data;
    using System.Web;
    using System.Globalization;
    using ZoomLa.Common;
    using ZoomLa.Components;
    using ZoomLa.Model;
    using ZoomLa.SQLDAL;
    using ZoomLa.BLL.Helper;
    using System.Data.Common;
    using System.Collections.Generic;

    public class B_Admin : B_Base<M_AdminInfo>
    {
        private M_AdminInfo initMod = new M_AdminInfo();
        private static string tbName = "ZL_Manager";
        private static string PK = "AdminID";
        //---------------------Ignore
        /// <summary>
        /// 检测二级密码
        /// </summary>
        public bool CheckSPwd(string pwd)
        {
            M_AdminInfo admin = B_Admin.GetLogin();
            return CheckSPwd(admin, pwd);
        }
        public bool CheckSPwd(M_AdminInfo admin, string pwd)
        {
            if (string.IsNullOrEmpty(pwd)) return false;
            return admin.RandNumber.Equals(pwd);
        }
        public static void ClearLogin()
        {
            HttpContext.Current.Session["M_LoginName"] = null;
            HttpContext.Current.Session["M_Password"] = null;
            if (HttpContext.Current.Request.Cookies["ManageState"] != null)
            {
                HttpContext.Current.Response.Cookies.Clear();
                HttpContext.Current.Response.Cookies["ManageState"].Expires = DateTime.Now.AddDays(-1.0);
            }
        }
        /// <summary>
        /// 管理员或用户任一登录均可
        /// </summary>
        public static bool CheckLogByAU()
        {
            return (new B_Admin().CheckLogin() || new B_User().CheckLogin());
        }
        /// <summary>
        /// (不推荐),使用静态方法CheckIsLogged
        /// </summary>
        public void CheckIsLogin(string url = "")
        {
            if (HttpContext.Current.Session["M_LoginName"] != null && HttpContext.Current.Session["M_Password"] != null)
                return;
            if (HttpContext.Current.Request.Cookies["ManageState"] == null)
            {
                HttpContext.Current.Response.Redirect("~/" + SiteConfig.SiteOption.ManageDir + "/Login.aspx?r=" + url);
            }
            else
            {
                string loginName = HttpContext.Current.Request.Cookies["ManageState"]["LoginName"];

                if (!string.IsNullOrEmpty(loginName))
                {
                    loginName = StringHelper.Base64StringDecode(loginName);

                }
                string password = HttpContext.Current.Request.Cookies["ManageState"]["Password"];
                if (this.GetLoginAdmin(loginName, password) == null)
                {
                    HttpContext.Current.Response.Redirect("~/" + SiteConfig.SiteOption.ManageDir + "/Login.aspx?r=" + url);
                }
                else
                {
                    HttpContext.Current.Session["M_LoginName"] = loginName;
                    HttpContext.Current.Session["M_Password"] = password;
                }
            }
        }
        public static void CheckIsLogged(string url = "")
        {
            new B_Admin().CheckIsLogin(url);
        }
        /// <summary>
        /// 检测后台是否登录了,有返回值
        /// </summary>
        public bool CheckLogin()
        {
            if (HttpContext.Current.Request.Cookies["ManageState"] == null) { return false; }
            string loginName = HttpContext.Current.Request.Cookies["ManageState"]["LoginName"];
            if (!string.IsNullOrEmpty(loginName))
            {
                loginName = StringHelper.Base64StringDecode(loginName);
            }
            string password = HttpContext.Current.Request.Cookies["ManageState"]["Password"];
            if (this.GetLoginAdmin(loginName, password) == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// 设定登录状态
        /// </summary>
        /// <param Name="model"></param>
        public void SetLoginState(M_AdminInfo model)
        {
            HttpContext.Current.Response.Cookies["ManageState"].Path = "/";
            HttpContext.Current.Response.Cookies["ManageState"]["ManageId"] = model.AdminId.ToString();
            HttpContext.Current.Response.Cookies["ManageState"]["LoginName"] = StringHelper.Base64StringEncode(model.AdminName);
            HttpContext.Current.Response.Cookies["ManageState"]["TrueName"] = StringHelper.Base64StringEncode(model.AdminTrueName);
            HttpContext.Current.Response.Cookies["ManageState"]["Password"] = model.AdminPassword;
            HttpContext.Current.Response.Cookies["ManageState"].Expires = DateTime.Now.AddDays(1);
            HttpContext.Current.Session["M_LoginName"] = model.AdminName;
            HttpContext.Current.Session["M_Password"] = model.AdminPassword;
            HttpContext.Current.Response.Cookies["UserState"]["LoginName"] = StringHelper.Base64StringEncode(model.UserName);
            HttpContext.Current.Response.Cookies["UserState"]["Password"] = model.AdminPassword;
        }
        public static bool Add(M_AdminInfo model)
        {
            model.AdminName = model.AdminName.Replace(" ", "");
            if (!SafeSC.CheckUName(model.AdminName)) { throw new Exception("用户名含有非法字符!!"); }
            DBCenter.Insert(model);
            return true;
        }
        public static bool Update(M_AdminInfo model)
        {
            DBCenter.UpdateByID(model, model.AdminId);
            return true;
        }
        public static bool DelAdminById(int adminId)
        {
            DBCenter.Del(tbName, PK, adminId); return true;
        }
        public DataTable Sel()
        {
            return DBCenter.Sel(initMod.TbName);
        }
        public DataTable SelByIds(string ids, int islock = -1)
        {
            SafeSC.CheckDataEx(ids);
            string where = "AdminID IN (" + ids + ")";
            where += islock > -1 ? " AND IsLock=" + islock : "";
            return DBCenter.Sel(tbName, where, "CDATE DESC");
        }
        public DataTable SelByRole(int rid)
        {
            //SelectGroupAdminInfo
            return DBCenter.Sel(tbName, "AdminRole LIKE '%," + rid + ",%'");
        }
        public void UpdatePwdByIDS(string ids, string pwd)
        {
            pwd = StringHelper.MD5(pwd);
            List<SqlParameter> sp = new List<SqlParameter>() { new SqlParameter("pwd", pwd) };
            DBCenter.UpdateSQL(tbName, "AdminPassword=@pwd", "AdminID IN(" + ids + ") AND AdminID!=1", sp);
        }
        public static bool IsExist(string adminName)
        {
            adminName = adminName.Trim();
            List<SqlParameter> sp = new List<SqlParameter>() { new SqlParameter("AdminName", adminName) };
            return DBCenter.Sel(tbName, "AdminName=@AdminName", "", sp).Rows.Count > 0;
        }
        public static bool IsExist(int adminID)
        {
            return DBCenter.Sel(tbName, "AdminID=" + adminID).Rows.Count > 0;
        }
        /// <summary>
        /// 根据管理员名和密码
        /// </summary>
        /// <param name="loginName"></param>
        /// <param name="password">MD5后的密码</param>
        /// <returns></returns>
        private M_AdminInfo GetLoginAdmin(string loginName, string password)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("AdminName", loginName), new SqlParameter("AdminPass", password) };
            using (DbDataReader reader = DBCenter.SelReturnReader(tbName, "AdminName=@AdminName AND AdminPassword=@AdminPass", sp))
            {
                if (reader.Read())
                    return new M_AdminInfo().GetModelFromReader(reader);
                else
                    return null;
            }
        }
        public M_AdminInfo GetAdminLogin()
        {
            this.CheckIsLogin();
            return B_Admin.GetLogin();
        }
        /// <summary>
        /// 获取当前登录用户,40毫秒每次
        /// </summary>
        public static M_AdminInfo GetLogin()
        {
            if (HttpContext.Current == null || HttpContext.Current.Session["M_LoginName"] == null)
            {
                //从Cookies中重读
                if (HttpContext.Current.Request.Cookies["ManageState"] == null) { return null; }
                string name = HttpContext.Current.Request.Cookies["ManageState"]["LoginName"] as string;
                string pwd = HttpContext.Current.Request.Cookies["ManageState"]["PassWord"] as string;
                if (StrHelper.StrNullCheck(name, pwd)) { return null; }
                return new B_Admin().GetLoginAdmin(StringHelper.Base64StringDecode(name), pwd);
            }
            else
            {
                string userName = HttpContext.Current.Session["M_LoginName"].ToString();
                return GetAdminByAdminName(userName);
            }
        }
        public static M_AdminInfo GetAdminByID(int adminId)
        {
            return GetAdminByAdminId(adminId);
        }
        //-------
        public static M_AdminInfo GetAdminByAdminId(int adminId)
        {
            if (adminId <= 0) { return null; }
            using (DbDataReader reader = DBCenter.SelReturnReader(tbName, PK, adminId))
            {
                if (reader.Read())
                {
                    return new M_AdminInfo().GetModelFromReader(reader);
                }
                else
                { return null; }
            }
        }
        public static M_AdminInfo GetAdminByAdminName(string adminName)
        {
            if (string.IsNullOrEmpty(adminName)) { return null; }
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("adminName", adminName) };
            using (DbDataReader reader = DBCenter.SelReturnReader(tbName, "adminName=@adminName", sp))
            {
                if (reader.Read())
                {
                    return new M_AdminInfo().GetModelFromReader(reader);
                }
                else
                { return null; }
            }
        }
        /// <summary>
        /// 检测是否超管,返回True
        /// </summary>
        public static bool IsSuperManage(int adminId)
        {
            M_AdminInfo info = GetAdminByID(adminId);
            string[] AdminRole = info.RoleList.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string role in AdminRole)
            {
                if (role.Equals("1")) { return true; }
            }
            return false;
        }
        /// <summary>
        /// 如非超管,则跳转
        /// </summary>
        public static void IsSuperManage()
        {
            M_AdminInfo model = B_Admin.GetLogin();
            if (model == null || model.AdminId < 1 || !IsSuperManage(model.AdminId))
            {
                function.WriteErrMsg("非超级管理员，无权访问该页面");
            }
        }
        public static M_AdminInfo AuthenticateAdmin(string AdminName, string Password)
        {
            if (string.IsNullOrEmpty(AdminName) || string.IsNullOrEmpty(Password)) { return null; }
            M_AdminInfo adminMod = null;
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("AdminName", AdminName), new SqlParameter("AdminPassword", StringHelper.MD5(Password)) };
            using (DbDataReader reader = DBCenter.SelReturnReader(tbName, "AdminName=@AdminName AND AdminPassword=@AdminPassword", sp))
            {
                if (reader.Read())
                {
                    adminMod = new M_AdminInfo().GetModelFromReader(reader);
                }
                else
                { return null; }
            }
            adminMod.LastLoginIP = IPScaner.GetUserIP();
            adminMod.LastLoginTime = DateTime.Now;
            adminMod.LoginTimes++;
            Update(adminMod);
            return adminMod;
        }
        /// <summary>
        /// 仅系统调用
        /// </summary>
        public static void UpdateField(string field, string value, int adminID)
        {
            DBCenter.UpdateSQL(tbName, field + "=@value", "AdminID=" + adminID, null);
        }
        public static void UpdateField(string field, string value, string adminName)
        {
            List<SqlParameter> sp = new List<SqlParameter>() { new SqlParameter("value", value), new SqlParameter("adminName", adminName) };
            DBCenter.UpdateSQL("ZL_Manager", field + " =@value", "AdminName=@adminName", sp);
        }
        /// <summary>
        /// 更改管理员锁定状态(不包含超管)
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="islock">true:1锁定</param>
        /// <returns></returns>
        public bool LockAdmin(string ids, bool islock)
        {
            SafeSC.CheckIDSEx(ids);
            int isLocked = islock ? 1 : 0;
            DBCenter.UpdateSQL(tbName, "IsLock=" + isLocked, "AdminID IN(" + ids + ") AND AdminID!=1", null);
            return true;
        }
        /// <summary>
        /// 获取当前登录用户在节点中所拥有的权限列表，(ZL_NodeRole)RID:角色,NID:节点,look:查看
        /// </summary>
        /// <returns></returns>
        public DataTable GetNodeAuthList(string auth = "")
        {
            SafeSC.CheckDataEx(auth);
            M_AdminInfo m = B_Admin.GetLogin();
            if (m == null || m.AdminId < 1 || string.IsNullOrEmpty(m.RoleList.Replace(",", ""))) return null;
            string where = "Rid in (" + m.RoleList.Trim(',') + ")";
            if (!string.IsNullOrEmpty(auth))
            {
                where += " And " + auth + " =1";
            }
            return DBCenter.Sel("ZL_NodeRole", where);
        }
        /// <summary>
        /// 用于NodeTree.ascx,当前用户，有阅读权限的节点
        /// </summary>
        public string GetNodeAuthStr()
        {
            DataTable dt = GetNodeAuthList("look");
            string nodeids = "";
            foreach (DataRow dr in dt.Rows)
            {
                nodeids += dr["NID"] + ",";
            }
            nodeids = nodeids.TrimEnd(',');
            return nodeids;
        }
    }
}