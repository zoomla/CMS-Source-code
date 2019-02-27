namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using System.Configuration;
    using ZoomLa.Model;
    using ZoomLa.Components;
    using ZoomLa.Common;
    using System.Web;
    using System.Globalization;
    using System.Data.SqlClient;
    using System.Collections.Generic;
    using ZoomLa.SQLDAL;
    using SQLDAL.SQL;
    /// <summary>
    /// B_Role 的摘要说明
    /// </summary>
    public class B_Role
    {
        public B_Role()
        {
            PK = initmod.PK;
            strTableName = initmod.TbName;
        }
        private string PK, strTableName;
        private M_RoleInfo initmod = new M_RoleInfo();
        public DataTable Sel(int ID)
        {
            return Sql.Sel(strTableName, PK, ID);
        }
        public M_RoleInfo SelReturnModel(int ID)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(strTableName, PK, ID))
            {
                if (reader.Read())
                {
                    return initmod.GetModelFromReader(reader);
                }
                else
                {
                    return null;
                }
            }
        }
        private M_RoleInfo SelReturnModel(string strWhere)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(strTableName, strWhere))
            {
                if (reader.Read())
                {
                    return initmod.GetModelFromReader(reader);
                }
                else
                {
                    return null;
                }
            }
        }
        public DataTable GetRoleAll()
        {
            return Sql.Sel(strTableName);
        }
        public PageSetting SelPage(int cpage, int psize)
        {
            PageSetting setting = PageSetting.Single(cpage, psize, strTableName, PK, "");
            DBCenter.SelPage(setting);
            return setting;
        }

        /// <summary>
        /// 根据ID更新
        /// </summary>
        public bool UpdateByID(M_RoleInfo model)
        {
            return Sql.UpdateByIDs(strTableName, PK, model.RoleID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
        public bool Del(int ID)
        {
            return Sql.Del(strTableName, ID);
        }
        public int insert(M_RoleInfo model)
        {
            return Sql.insert(strTableName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        /// <summary>
        /// 查询非roleId的角色信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetRoleIdByNotRoleId(IList<int> list)
        {
            string strSql = null;
            if (list.Count > 0)
            {
                strSql = "select * from ZL_Role where RoleID not in(" + list[0];
                for (int i = 1; i < list.Count; i++)
                {
                    strSql += "," + list[i];
                }
                strSql += ")";

            }
            else
            {
                strSql = "select * from ZL_Role";
            }
            return SqlHelper.ExecuteTable(CommandType.Text, strSql, null);
        }
        /// <summary>
        /// 获取角色信息
        /// </summary>
        /// <returns></returns>
        public static DataTable GetRoleName()
        {
            string sqlStr = "select RoleID,RoleName from ZL_Role";
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, null);
        }
        /// <summary>
        /// 根据管理员角色ID,获取其所拥有的节点权限
        /// </summary>
        public DataTable SelectNodeRoleName(int rid)
        {
            string sql = "select ZL_node.nodeid,ZL_node.NodeName,ZL_NodeRole.* from ZL_node inner join ZL_NodeRole on ZL_node.nodeid=ZL_NodeRole.nid where ZL_NodeRole.rid=" + rid;
            return SqlHelper.ExecuteTable(CommandType.Text, sql);
        }
        public DataTable SelectNodeRoleName()
        {
            string sqlStr = "select ZL_node.nodeid,ZL_NodeRole.* from ZL_node inner join ZL_NodeRole on ZL_node.nodeid=ZL_NodeRole.nid ";
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, null);
        }
        public static DataTable SelectNodeRoleNode(int nodeid)
        {
            string sqlStr = "select RoleID,RoleName,ZL_NodeRole.* from ZL_Role left outer join ZL_NodeRole on ZL_Role.RoleID=ZL_NodeRole.RID and ZL_NodeRole.nid=@nodeid";
            SqlParameter[] parameter = new SqlParameter[1];
            parameter[0] = new SqlParameter("@nodeid", SqlDbType.Int, 4);
            parameter[0].Value = nodeid;
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, parameter);
        }
        //public static DataTable SelectNodeRoleName(int nodeid)
        //{
        //    return roleMethod.SelectNodeRoleName( nodeid);
        //}
        /// <summary>
        /// 根据ID获取角色信息
        /// </summary>
        public static  M_RoleInfo GetRoleById(int roleId)
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
                    return new M_RoleInfo().GetModelFromReader(reader);
                }
                else
                    return new M_RoleInfo();
            }
        }
        // 增加角色
        public static bool Add(M_RoleInfo RoleInfo)
        {
            string strSql = "INSERT INTO ZL_Role(RoleName,Description,NodeID)VALUES(@RoleName,@Description,@NodeID)";
            SqlParameter[] parameter = new SqlParameter[3];
            parameter[0] = new SqlParameter("@RoleName", SqlDbType.NVarChar, 20);
            parameter[0].Value = RoleInfo.RoleName;
            parameter[1] = new SqlParameter("@Description", SqlDbType.NVarChar, 255);
            parameter[1].Value = RoleInfo.Description;
            parameter[2] = new SqlParameter("@NodeID", SqlDbType.Int, 255);
            parameter[2].Value = RoleInfo.NodeID;
            return SqlHelper.ExecuteSql(strSql, parameter);
        }
        // 更新角色
        public  bool Update(M_RoleInfo roleInfo)
        {
            string sqlStr = "UPDATE ZL_Role SET RoleName=@RoleName,Description=@Description WHERE RoleID=@RoleId";
            SqlParameter[] Params = roleInfo.GetParameters();
            return SqlHelper.ExecuteSql(sqlStr, Params);
        }

        //根据角色名判断角色是否存在
        public static bool IsExit(string roleName)
        {
            string strSql = "SELECT COUNT(*) FROM ZL_Role WHERE RoleName=@RoleName";
            SqlParameter[] cmdParams = new SqlParameter[] { new SqlParameter("@RoleName", SqlDbType.NVarChar, 20) };
            cmdParams[0].Value = roleName;
            return SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.Text, strSql, cmdParams)) > 0;
        }
        //根据角ID判断角色是否存在
        public static bool IsExit(int roleID)
        {
            string strSql = "SELECT COUNT(*) FROM ZL_Role WHERE RoleID=@RoleId";
            SqlParameter[] cmdParams = new SqlParameter[] { new SqlParameter("@RoleId", SqlDbType.Int) };
            cmdParams[0].Value = roleID;
            return SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.Text, strSql, cmdParams)) > 0;
        }
        //获取所有角色信息
        public static DataView GetRoleInfo()
        {
            string sqlStr = "select RoleID,RoleName,Description from ZL_Role order by RoleID asc";
            DataSet ds = SqlHelper.ExecuteDataSet(CommandType.Text, sqlStr, null);
            DataTable dt = ds.Tables[0];
            return dt.DefaultView;
        }
        //根据ＩＤ删除角色
        public static bool DelRoleByID(int roleID)
        {
            string strSql = "DELETE FROM ZL_Role WHERE RoleID=@RoleId;Delete From ZL_RolePermissions Where RoleID=@RoleId";
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@RoleId", SqlDbType.Int, 4);
            cmdParams[0].Value = roleID;
            return SqlHelper.ExecuteSql(strSql, cmdParams);
        }
        public static bool SavePower(int roleID, string str)
        {
            string sqlStr = "insert into ZL_RolePermissions (RoleID,OperateCode) values (@RoleID,@power)";
            SqlParameter[] parameter = new SqlParameter[2];
            parameter[0] = new SqlParameter("@RoleID", SqlDbType.Int, 4);
            parameter[0].Value = roleID;
            parameter[1] = new SqlParameter("@power", SqlDbType.NVarChar, 50);
            parameter[1].Value = str;
            return SqlHelper.ExecuteSql(sqlStr, parameter);
        }
        public static bool SavePower(int roleID, string str, int NodeID)
        {
            string sqlStr = "insert into ZL_RolePermissions (RoleID,OperateCode,NodeID) values (@RoleID,@power,@NodeID)";
            SqlParameter[] parameter = new SqlParameter[3];
            parameter[0] = new SqlParameter("@RoleID", SqlDbType.Int, 4);
            parameter[0].Value = roleID;
            parameter[1] = new SqlParameter("@power", SqlDbType.NVarChar, 50);
            parameter[1].Value = str;
            parameter[2] = new SqlParameter("@NodeID", SqlDbType.Int, 4);
            parameter[2].Value = NodeID;
            return SqlHelper.ExecuteSql(sqlStr, parameter);
        }
        public static void DelPower(string power)
        {
            string sqlStr = "Delete From ZL_RolePermissions where OperateCode=@power";
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("power", power) };
            SqlHelper.ExecuteSql(sqlStr, sp);
        }
        public static void DelPower(int roleID, string str)
        {
            string sqlStr = "Delete from ZL_RolePermissions where RoleID=@RoleID And OperateCode=@power";
            SqlParameter[] parameter = new SqlParameter[2];
            parameter[0] = new SqlParameter("@RoleID", SqlDbType.Int, 4);
            parameter[0].Value = roleID;
            parameter[1] = new SqlParameter("@power", SqlDbType.NVarChar, 50);
            parameter[1].Value = str;
            SqlHelper.ExecuteSql(sqlStr, parameter);
        }
        public static void DelPower(int roleID, string str, int NodeID)
        {
            string sqlStr = "Delete from ZL_RolePermissions where RoleID=@RoleID And OperateCode=@power and NodeID=@NodeID";
            SqlParameter[] parameter = new SqlParameter[3];
            parameter[0] = new SqlParameter("@RoleID", SqlDbType.Int, 4);
            parameter[0].Value = roleID;
            parameter[1] = new SqlParameter("@power", SqlDbType.NVarChar, 50);
            parameter[1].Value = str;
            parameter[2] = new SqlParameter("@NodeID", SqlDbType.Int, 4);
            parameter[2].Value = NodeID;
            SqlHelper.ExecuteSql(sqlStr, parameter);
        }
        public static bool IsExistPower(int roleID, string power)
        {
            string sqlStr = "select Count(*) from ZL_RolePermissions where RoleID=@RoleID And OperateCode=@power";
            SqlParameter[] parameter = new SqlParameter[2];
            parameter[0] = new SqlParameter("@RoleID", SqlDbType.Int, 4);
            parameter[0].Value = roleID;
            parameter[1] = new SqlParameter("@power", SqlDbType.NVarChar, 50);
            parameter[1].Value = power;
            return (SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.Text, sqlStr, parameter)) > 0);
        }
        public static bool IsExistPower(int roleID, string power, int NodeID)
        {
            string sqlStr = "select Count(*) from ZL_RolePermissions where RoleID=@RoleID And OperateCode=@power and NodeID=@NodeID";
            SqlParameter[] parameter = new SqlParameter[3];
            parameter[0] = new SqlParameter("@RoleID", SqlDbType.Int, 4);
            parameter[0].Value = roleID;
            parameter[1] = new SqlParameter("@power", SqlDbType.NVarChar, 50);
            parameter[1].Value = power;
            parameter[2] = new SqlParameter("@NodeID", SqlDbType.Int, 4);
            parameter[2].Value = NodeID;
            return (SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.Text, sqlStr, parameter)) > 0);
        }
        //获取权限信息
        public static string GetPowerInfo(string OperateCode)
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
                reader.Close();
                reader.Dispose();
            }
            return result;
        }
        public static string GetPowerInfoByIDs(string ids)
        {
            ids = ids.Trim(',');
            SafeSC.CheckIDSEx(ids);
            string sqlStr = "select OperateCode from  ZL_RolePermissions where RoleID in (" + ids + ")";
            string result = "";
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sqlStr))
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
                reader.Close();
                reader.Dispose();
            }
            return "," + result + ",";
        }
        public static string GetPowerInfo(int RoleID)
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
                reader.Close();
                reader.Dispose();
            }
            return result;
        }
        /// <summary>
        ///按条件查找记录
        /// </summary>
        /// <param name="strSQL">查询条件</param>
        /// <param name="strSelect">查询结果</param>
        /// <param name="Orderby"></param>
        /// <returns></returns>
        public DataTable Select_Where(string strSQL, string strSelect, string Orderby)
        {
            string sqlStr = "select " + strSelect + " from ZL_RolePermissions where " + strSQL + Orderby;
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, null);
        }
        //权限类型
        public enum AT {OnlyMe }
    }
}