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
using ZoomLa.SQLDAL;
using ZoomLa.Common;

/// <summary>
/// SD_Role 的摘要说明
/// </summary>
public class SD_Role : ID_Role
{
	public SD_Role()
	{
    }
    /// <summary>
    /// 增加新的角色到数据库中
    /// </summary>
    /// <param name="RoleInfo">角色</param>
    /// <returns>增加结果状态 成功为true 反之 false</returns>
    public bool Add(M_RoleInfo RoleInfo)
    {
        string strSql = "INSERT INTO ZL_Role(RoleName,Description)VALUES(@RoleName,@Description)";
        SqlParameter[] parameter = new SqlParameter[2]; 
        parameter[0] = new SqlParameter("@RoleName", SqlDbType.NVarChar, 20);
        parameter[0].Value = RoleInfo.RoleName;
        parameter[1] = new SqlParameter("@Description", SqlDbType.NVarChar, 255);
        parameter[1].Value = RoleInfo.Description;
        
        return SqlHelper.ExecuteSql(strSql, parameter);
    }
    /// <summary>
    /// 删除角色
    /// </summary>
    /// <param name="roleId"></param>
    public bool DeleteByRoleId(int roleId)
    {
        string strSql = "DELETE FROM ZL_Role WHERE RoleID=@RoleId;Delete From ZL_RolePermissions Where RoleID=@RoleId";
        SqlParameter[] cmdParams = new SqlParameter[1];
        cmdParams[0] = new SqlParameter("@RoleId", SqlDbType.Int, 4);
        cmdParams[0].Value = roleId;
        return SqlHelper.ExecuteSql(strSql, cmdParams);
    }
    /// <summary>
    /// 根据角色ID，名称读取角色信息
    /// </summary>
    /// <param name="adminId">角色ID</param>
    /// <param name="adminName">角色名</param>
    /// <returns>角色信息对象 RoleInfo</returns>
    public M_RoleInfo GetRoleByID(int roleId)
    {
        string strSql = "SELECT * FROM ZL_Role WHERE 1=1 ";
        SqlParameter[] cmdParams = new SqlParameter[] { new SqlParameter("@RoleId", SqlDbType.Int, 4) };
        if (roleId > 0)
        {
            strSql = strSql + " AND RoleID=@RoleId ";
            cmdParams[0].Value = roleId;
        }
        else
        {
            return new M_RoleInfo();
        }
        using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, strSql, cmdParams))
        {
            if (reader.Read())
            {
                return GetRoleInfoFromReader(reader);
            }
            else
                return new M_RoleInfo();
        }
    }
    /// <summary>
    /// 根据角色名称读取角色信息
    /// </summary>
    /// <param name="roleId"></param>
    /// <returns></returns>
    public M_RoleInfo GetRoleByName(string roleName)
    {
        string strSql = "SELECT * FROM ZL_Role WHERE 1=1 ";
        SqlParameter[] cmdParams = new SqlParameter[] { new SqlParameter("@RoleName", SqlDbType.Int, 4) };
        if (!string.IsNullOrEmpty(roleName))
        {
            strSql = strSql + " AND RoleName=@RoleName ";
            cmdParams[0].Value = roleName;
        }
        else
        {
            return new M_RoleInfo();
        }
        using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, strSql, cmdParams))
        {
            if (reader.Read())
            {
                return GetRoleInfoFromReader(reader);
            }
            else
                return new M_RoleInfo();
        }
    }
    /// <summary>
    /// 从DataReader中读取管理员记录
    /// </summary>
    /// <param name="rdr">DataReader</param>
    /// <returns>M_RoleInfo 角色</returns>
    private static M_RoleInfo GetRoleInfoFromReader(SqlDataReader rdr)
    {
        M_RoleInfo info = new M_RoleInfo();
        info.RoleID = Convert.ToInt32(rdr["RoleID"]);
        info.RoleName = rdr["RoleName"].ToString();
        info.Description = rdr["Description"].ToString();
        rdr.Close();
        return info;
    }

    /// <summary>
    /// 查询角色
    /// </summary>
    /// <returns>返回datatable供复选框应用</returns>
    public DataTable SelectRoleName()
    {
        string sqlStr = "select RoleID,RoleName from ZL_Role";
        return SqlHelper.ExecuteTable(CommandType.Text,sqlStr,null);
    }
    /// <summary>
    /// 判断是否更新
    /// </summary>
    /// <returns></returns>
    public bool Update(M_RoleInfo roleInfo)
    {
        string sqlStr = "UPDATE ZL_Role SET RoleName=@RoleName,Description=@Description WHERE RoleID=@RoleId";
        SqlParameter[] Params = GetParameters(roleInfo);
        return SqlHelper.ExecuteSql(sqlStr, Params);
    }
    /// <summary>
    /// 传递参数
    /// </summary>
    /// <param name="adminInfo"></param>
    /// <returns></returns>
    private static SqlParameter[] GetParameters(M_RoleInfo roleInfo)
    {
        SqlParameter[] parameter = new SqlParameter[3];
        parameter[0] = new SqlParameter("@RoleID", SqlDbType.Int, 4);
        parameter[0].Value = roleInfo.RoleID;
        parameter[1] = new SqlParameter("@RoleName", SqlDbType.NVarChar, 50);
        parameter[1].Value = roleInfo.RoleName;
        parameter[2] = new SqlParameter("@Description", SqlDbType.NVarChar, 255);
        parameter[2].Value = roleInfo.Description;
        return parameter;
    }
    /// <summary>
    /// 判断某角色的角色名是否已存在
    /// </summary>
    /// <param name="adminName">要检索的角色名</param>
    /// <returns>存在状态</returns>
    public bool IsExist(string roleName)
    {
        string strSql = "SELECT COUNT(*) FROM ZL_Role WHERE RoleName=@RoleName";
        SqlParameter[] cmdParams = new SqlParameter[] { new SqlParameter("@RoleName", SqlDbType.NVarChar, 20) };
        cmdParams[0].Value = roleName;
        return SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.Text, strSql, cmdParams)) > 0;
    }
    /// <summary>
    /// 判断某角色是否已存在
    /// </summary>
    /// <param name="adminName">要检索的角色ID</param>
    /// <returns>存在状态</returns>
    public bool IsExist(int roleId)
    {
        string strSql = "SELECT SELECT COUNT(*) FROM ZL_Role WHERE RoleID=@RoleId";
        SqlParameter[] cmdParams = new SqlParameter[] { new SqlParameter("@RoleId", SqlDbType.Int) };
        cmdParams[0].Value = roleId;
        return SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.Text, strSql, cmdParams)) > 0;
    }
    /// <summary>
    /// 查询所有角色信息
    /// </summary>
    /// <returns>返回DataTable</returns>
    public DataTable SeachRoleAll()
    {
        string sqlStr = "select RoleID,RoleName,Description from ZL_Role";
        DataSet ds = SqlHelper.ExecuteDataSet(CommandType.Text, sqlStr, null);
        DataTable dt = ds.Tables[0];
        return dt;
    }    
    /// <summary>
    /// 保存选中权限
    /// </summary>
    /// <param name="roleID">角色ＩＤ</param>
    /// <param name="power">权限操作代码</param>
    /// <returns></returns>
    public bool SavePower(int roleID,string power)
    {
        string sqlStr = "insert into ZL_RolePermissions (RoleID,OperateCode) values (@RoleID,@power)";
        SqlParameter[] parameter = new SqlParameter[2];
        parameter[0] = new SqlParameter("@RoleID", SqlDbType.Int,4);
        parameter[0].Value = roleID;
        parameter[1] = new SqlParameter("@power", SqlDbType.NVarChar, 50);
        parameter[1].Value = power;
        return SqlHelper.ExecuteSql(sqlStr, parameter);
    }
    public void DelPower(int roleID, string power)
    {
        string sqlStr = "Delete from ZL_RolePermissions where RoleID=@RoleID And OperateCode=@power";
        SqlParameter[] parameter = new SqlParameter[2];
        parameter[0] = new SqlParameter("@RoleID", SqlDbType.Int, 4);
        parameter[0].Value = roleID;
        parameter[1] = new SqlParameter("@power", SqlDbType.NVarChar, 50);
        parameter[1].Value = power;
        SqlHelper.ExecuteSql(sqlStr, parameter);
    }
    public bool IsExistPower(int roleID, string power)
    {
        string sqlStr = "select Count(*) from ZL_RolePermissions where RoleID=@RoleID And OperateCode=@power";
        SqlParameter[] parameter = new SqlParameter[2];
        parameter[0] = new SqlParameter("@RoleID", SqlDbType.Int, 4);
        parameter[0].Value = roleID;
        parameter[1] = new SqlParameter("@power", SqlDbType.NVarChar, 50);
        parameter[1].Value = power;
        return (SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.Text,sqlStr, parameter)) > 0);
    }
    //获取权限角色ID信息
    public string GetPowerInfo(string OperateCode)
    {
        string sqlStr = "select RoleID from  ZL_RolePermissions where OperateCode =@OperateCode";
        SqlParameter[] cmdParams = new SqlParameter[1];
        cmdParams[0] = new SqlParameter("@OperateCode", SqlDbType.NVarChar);
        cmdParams[0].Value = OperateCode;
        string result = "";
        using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sqlStr, cmdParams))
        {

            while (reader.Read())
            {
                if (string.IsNullOrEmpty(result))
                {
                    result = reader["RoleID"].ToString();
                }
                else
                {
                    result = result + "," + reader["RoleID"].ToString();
                }
            }            
        }
        return result;
    }
    public string GetPowerInfo(int RoleID)
    {
        string sqlStr = "select OperateCode from  ZL_RolePermissions where RoleID =@RoleID";
        SqlParameter[] cmdParams = new SqlParameter[1];
        cmdParams[0] = new SqlParameter("@RoleID", SqlDbType.Int);
        cmdParams[0].Value = RoleID;
        string result = "";
        using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sqlStr, cmdParams))
        {
            
            while (reader.Read())
            {
                if (string.IsNullOrEmpty(result))
                {
                    result = reader["OperateCode"].ToString();
                }
                else
                {
                    result = result + "," + reader["OperateCode"].ToString();
                }
            }            
        }
        return result;
    }
}
