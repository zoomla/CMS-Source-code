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
    using System.Data.SqlClient;

    /// <summary>
    /// B_Role 的摘要说明
    /// </summary>
    public class B_Role
    {
        private static readonly ID_Role roleMethod = IDal.CreateRole();
        public B_Role()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        /// <summary>
        /// 获取角色信息
        /// </summary>
        /// <returns></returns>
        public static DataTable GetRoleName()
        {
            return roleMethod.SelectRoleName();
        }
        /// <summary>
        /// 根据ID获取角色信息
        /// </summary>
        public static M_RoleInfo GetRoleById(int roleID)
        {
            //if (roleID <= 0)
            //{
            //    return new M_RoleInfo(true);
            //}
            return roleMethod.GetRoleByID(roleID);
        }
        // 增加角色
        public static bool Add(M_RoleInfo role)
        {
            return roleMethod.Add(role);
        }
        // 更新角色
        public static bool Update(M_RoleInfo role)
        {
            return roleMethod.Update(role);
        }

        //根据角色名判断角色是否存在
        public static bool IsExit(string roleName)
        {
            return roleMethod.IsExist(roleName);
        }
        //获取所有角色信息
        public static DataView GetRoleInfo()
        {
            return roleMethod.SeachRoleAll().DefaultView;
        }
        //根据ＩＤ删除角色
        public static bool DelRoleByID(int roleID)
        {
            return roleMethod.DeleteByRoleId(roleID);
        }
        public static bool SavePower(int roleID, string str)
        {
            return roleMethod.SavePower(roleID,str);
 
        }
        public static void DelPower(int roleID, string str)
        {
            roleMethod.DelPower(roleID, str);
        }
        public static bool IsExistPower(int roleID, string power)
        {
            return roleMethod.IsExistPower(roleID, power);
        }
        //获取权限信息
        public static string GetPowerInfo(string OperateCode)
        {
            return roleMethod.GetPowerInfo(OperateCode);
        }
        public static string GetPowerInfo(int RoleID)
        {
            return roleMethod.GetPowerInfo(RoleID);
        }
    }
}