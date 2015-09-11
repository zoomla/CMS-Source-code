using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ZoomLa.Model;
using System.Data.SqlClient;

/// <summary>
/// ID_Role 的摘要说明
/// </summary>
public interface ID_Role
{
    //添加添加角色
    bool Add(M_RoleInfo RoleInfo);
    bool DeleteByRoleId(int roleId);
    M_RoleInfo GetRoleByID(int roleId);
    M_RoleInfo GetRoleByName(string roleName);
    bool Update(M_RoleInfo roleInfo);
    bool IsExist(string roleName);
    bool IsExist(int roleId);
    //查询角色名及ID
    DataTable SelectRoleName();
    //查询角色所有信息
    DataTable SeachRoleAll();
    bool SavePower(int roleID, string power);//保存选中权限
    void DelPower(int roleID, string power);
    bool IsExistPower(int roleID, string power);
    string GetPowerInfo(string OperateCode);//根据权限获取具有该权限的角色ID数组
    string GetPowerInfo(int RoleID);//根据角色获取对应权限
}
