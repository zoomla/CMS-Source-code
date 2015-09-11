namespace ZoomLa.SQLDAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Text;
    using ZoomLa.IDAL;
    using ZoomLa.Model;
    using System.Data.SqlClient;
    using ZoomLa.Common;    

    /// <summary>
    /// 对Admin表的Sql Server数据访问
    /// </summary>
    public class SD_Admin : ID_Admin
    {
        public SD_Admin()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #region ID_Admin 成员
        /// <summary>
        /// 增加新的管理员到数据库中
        /// </summary>
        /// <param name="administratorInfo">管理员数据模型</param>
        /// <returns>增加结果状态 成功为true 反之 false</returns>
        public bool Add(M_AdminInfo adminInfo)
        {
            string strSql = "PR_Manage_Add";
            SqlParameter[] parameter = new SqlParameter[10];            
            parameter[0] = new SqlParameter("@AdminName", SqlDbType.NVarChar, 20);
            parameter[0].Value = adminInfo.AdminName;
            parameter[1] = new SqlParameter("@AdminPassword", SqlDbType.NVarChar, 255);
            parameter[1].Value = adminInfo.AdminPassword;
            parameter[2] = new SqlParameter("@UserName", SqlDbType.NVarChar, 20);
            parameter[2].Value = adminInfo.UserName;
            parameter[3] = new SqlParameter("@EnableMultiLogin", SqlDbType.Bit, 1);
            parameter[3].Value = adminInfo.EnableMultiLogin;            
            parameter[4] = new SqlParameter("@LastLoginIp", SqlDbType.NVarChar, 50);
            parameter[4].Value = adminInfo.LastLoginIP;
            parameter[5] = new SqlParameter("@IsLock", SqlDbType.Bit, 1);
            parameter[5].Value = adminInfo.IsLock;
            parameter[6] = new SqlParameter("@EnableModifyPassword", SqlDbType.Bit, 1);
            parameter[6].Value = adminInfo.EnableModifyPassword;
            parameter[7] = new SqlParameter("@AdminRole", SqlDbType.NVarChar, 255);
            parameter[7].Value = adminInfo.RoleList;
            parameter[8] = new SqlParameter("@Theme", SqlDbType.NVarChar, 50);
            parameter[8].Value = adminInfo.Theme;
            parameter[9] = new SqlParameter("@RndPassword", SqlDbType.NVarChar, 10);
            parameter[9].Value = adminInfo.RandNumber;
            return SqlHelper.ExecuteProc(strSql, parameter);
        }
        /// <summary>
        /// 删除管理员
        /// </summary>
        /// <param name="adminId"></param>
        /// <returns></returns>
        public bool Delete(int adminId)
        {
            string strSql = "DELETE FROM ZL_Manager WHERE AdminId=@AdminId";
            SqlParameter[] cmdParams=new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@AdminId", SqlDbType.Int, 4);
            cmdParams[0].Value=adminId;
            return SqlHelper.ExecuteSql(strSql, cmdParams);
        }
        /// <summary>
        /// 根据管理员ID，名称或前台用户名读取管理员信息
        /// </summary>
        /// <param name="adminId">管理员ID</param>
        /// <param name="adminName">管理员名</param>
        /// <param name="userName">前台用户名</param>
        /// <returns>管理员信息对象 AdministratorInfo</returns>
        public M_AdminInfo GetAdminByID(int adminId)
        {
            string strSql = "SELECT * FROM ZL_Manager WHERE 1=1 ";
            SqlParameter[] cmdParams = new SqlParameter[] { new SqlParameter("@AdminId", SqlDbType.Int, 4) };
            if (adminId > 0)
            {
                strSql = strSql + " AND AdminId=@AdminId ";
                cmdParams[0].Value = adminId;
            }
            else
            {
                return new M_AdminInfo(true);
            }
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text,strSql, cmdParams))
            {
                if (reader.Read())
                {
                    return GetAdminInfoFromReader(reader);
                }
                else
                    return new M_AdminInfo(true);
            }
        }
        /// <summary>
        /// 根据管理员ID，名称或前台用户名读取管理员信息
        /// </summary>
        /// <param name="adminId">管理员ID</param>
        /// <param name="adminName">管理员名</param>
        /// <param name="userName">前台用户名</param>
        /// <returns>管理员信息对象 AdministratorInfo</returns>
        public M_AdminInfo GetAdminByName(string adminname)
        {
            string strSql = "SELECT * FROM ZL_Manager WHERE 1=1 ";
            SqlParameter[] cmdParams = new SqlParameter[] { new SqlParameter("@AdminName", SqlDbType.NVarChar, 20) };
            if (!string.IsNullOrEmpty(adminname))
            {
                strSql = strSql + " AND AdminName=@AdminName ";
                cmdParams[0].Value = adminname;
            }
            else
            {
                return new M_AdminInfo(true);
            }
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, strSql, cmdParams))
            {
                if (reader.Read())
                {
                    return GetAdminInfoFromReader(reader);
                }
                else
                    return new M_AdminInfo(true);
            }
        }
        /// <summary>
        /// 判断某管理员名的管理员是否已存在
        /// </summary>
        /// <param name="adminName">要检索的管理员名</param>
        /// <returns>存在状态</returns>
        public bool IsExist(string adminName)
        {
            string strSql = "SELECT COUNT(*) FROM ZL_Manager WHERE AdminName=@AdminName";
            SqlParameter[] cmdParams = new SqlParameter[] { new SqlParameter("@AdminName", SqlDbType.NVarChar, 20) };
            cmdParams[0].Value = adminName;
            return SqlHelper.ExistsSql(strSql, cmdParams);
        }
        /// <summary>
        /// 判断某管理员是否已存在
        /// </summary>
        /// <param name="adminName">要检索的管理员ID</param>
        /// <returns>存在状态</returns>
        public bool IsExist(int adminID)
        {
            string strSql = "SELECT AdminID FROM ZL_Manager WHERE AdminID=@adminID";
            SqlParameter[] cmdParams = new SqlParameter[] { new SqlParameter("@adminID", SqlDbType.Int) };
            cmdParams[0].Value = adminID;
            return SqlHelper.ExistsSql(strSql, cmdParams);
        }
        /// <summary>
        /// 将管理员信息更新到数据库中
        /// </summary>
        /// <param name="administratorInfo">M_AdminInfo 管理员信息</param>
        /// <returns></returns>
        public bool Update(M_AdminInfo administratorInfo)
        {
            string strSql = "PR_Manage_Update";
            SqlParameter[] cmdParams = GetParameters(administratorInfo);
            return SqlHelper.ExecuteProc(strSql, cmdParams);
        }
        public bool Update2(M_AdminInfo adminInfo)
        {
            string strSql = "UPDATE ZL_Manager SET AdminName=@AdminName,AdminPassword=@AdminPassword,UserName=@UserName,EnableMultilogin=@EnableMultilogin,";
            strSql += "RoleList=@RoleList,IsLock=@IsLock,EnableModifyPassword=@EnableModifyPassword WHERE AdminId=@AdminId";
            SqlParameter[] cmdParams = GetParameters(adminInfo);
            return SqlHelper.ExecuteSql(strSql, cmdParams);
        }
        #endregion
        /// <summary>
        /// 将管理员信息的各属性值传递到参数中
        /// </summary>
        /// <param name="administratorInfo"></param>
        /// <returns></returns>
        private static SqlParameter[] GetParameters(M_AdminInfo adminInfo)
        {
            SqlParameter[] parameter=new SqlParameter[14];
            parameter[0] = new SqlParameter("@AdminId", SqlDbType.Int, 4);
            parameter[0].Value = adminInfo.AdminId;                        
            parameter[1]=new SqlParameter("@AdminName", SqlDbType.NVarChar, 50);
            parameter[1].Value = adminInfo.AdminName;            
            parameter[2]=new SqlParameter("@AdminPassword", SqlDbType.NVarChar, 100);
            parameter[2].Value = adminInfo.AdminPassword;
            parameter[3]=new SqlParameter("@UserName", SqlDbType.NVarChar, 50);
            parameter[3].Value = adminInfo.UserName;
            parameter[4]=new SqlParameter("@EnableMultiLogin", SqlDbType.Bit, 1);
            parameter[4].Value = adminInfo.EnableMultiLogin;
            parameter[5]=new SqlParameter("@LoginTimes", SqlDbType.Int, 4);
            parameter[5].Value = adminInfo.LoginTimes;
            parameter[6]=new SqlParameter("@LastLoginIp", SqlDbType.NVarChar, 50);
            parameter[6].Value = adminInfo.LastLoginIP;
            parameter[7]=new SqlParameter("@LastLoginTime", SqlDbType.DateTime);
            parameter[7].Value = adminInfo.LastLoginTime;
            parameter[8]=new SqlParameter("@LastLogoutTime", SqlDbType.DateTime);
            parameter[8].Value = adminInfo.LastLogoutTime;
            parameter[9]=new SqlParameter("@LastModifyPasswordTime", SqlDbType.DateTime);
            parameter[9].Value = adminInfo.LastModifyPasswordTime;
            parameter[10]=new SqlParameter("@IsLock", SqlDbType.Bit, 1);
            parameter[10].Value = adminInfo.IsLock;
            parameter[11]=new SqlParameter("@EnableModifyPassword", SqlDbType.Bit, 1);
            parameter[11].Value = adminInfo.EnableModifyPassword;
            parameter[12] = new SqlParameter("@RoleList", SqlDbType.NText);
            parameter[12].Value = adminInfo.RoleList;
            parameter[13]=new SqlParameter("@Theme", SqlDbType.NVarChar, 50);
            parameter[13].Value = adminInfo.Theme;
            return parameter;
        }
        /// <summary>
        /// 从DataReader中读取管理员记录
        /// </summary>
        /// <param name="rdr">DataReader</param>
        /// <returns>M_AdminInfo 管理员信息</returns>
        private static M_AdminInfo GetAdminInfoFromReader(SqlDataReader rdr)
        {
            M_AdminInfo info = new M_AdminInfo();
            info.AdminId = DataConverter.CLng(rdr["AdminID"]);
            info.AdminName = rdr["AdminName"].ToString();
            info.AdminPassword = rdr["AdminPassword"].ToString();
            info.UserName = rdr["UserName"].ToString();
            info.EnableMultiLogin = DataConverter.CBool(rdr["EnableMultiLogin"].ToString());
            info.LoginTimes = DataConverter.CLng(rdr["LoginTimes"]);
            info.LastLoginIP = rdr["LastLoginIP"].ToString();
            info.LastLoginTime = DataConverter.CDate(rdr["LastLoginTime"]);
            info.LastLogoutTime = DataConverter.CDate(rdr["LastLogoutTime"]);
            info.LastModifyPasswordTime = DataConverter.CDate(rdr["LastModifyPwdTime"]);
            info.IsLock = DataConverter.CBool(rdr["IsLock"].ToString());
            info.EnableModifyPassword = DataConverter.CBool(rdr["EnableModifyPassword"].ToString());
            info.RoleList = rdr["AdminRole"].ToString();
            info.Theme = rdr["Theme"].ToString();
            //info.RandNumber = rdr["RandNumber"].ToString();
            rdr.Close();
            return info;
        }
        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <returns>返回dataset</returns>
        public DataSet SelectAdminInfo()
        {
            string sqlStr = "select AdminID,AdminName,UserName,AdminRole,EnableMultiLogin,LastLoginIP,LastLoginTime,LastModifyPwdTime,LoginTimes,IsLock from ZL_Manager";
            DataSet ds = SqlHelper.ExecuteDataSet(CommandType.Text,sqlStr,null);
            return ds;
        }

        #region ID_Admin 成员

        M_AdminInfo ID_Admin.GetLoginAdmin(string loginName, string password)
        {
            string AdminPass = password;
            string strSql = "select * from ZL_Manager where AdminName=@AdminName and AdminPassword=@AdminPass";
            SqlParameter[] cmdParam = new SqlParameter[] {
                new SqlParameter("@AdminName", SqlDbType.NVarChar, 255),
                new SqlParameter("@AdminPass", SqlDbType.NVarChar, 255)
            };
            cmdParam[0].Value = loginName;
            cmdParam[1].Value = AdminPass;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, strSql, cmdParam))
            {
                if (reader.Read())
                    return GetAdminInfoFromReader(reader);
                else
                    return new M_AdminInfo(true);
            }
        }

        M_AdminInfo ID_Admin.GetModel(string loginName, string password)
        {
            string AdminPass = StringHelper.MD5(password);
            string strSql = "select * from ZL_Manager where AdminName=@AdminName and AdminPassword=@AdminPass";
            SqlParameter[] cmdParam = new SqlParameter[] {
                new SqlParameter("@AdminName", SqlDbType.NVarChar, 255),
                new SqlParameter("@AdminPass", SqlDbType.NVarChar, 255)
            };
            cmdParam[0].Value = loginName;
            cmdParam[1].Value = AdminPass;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, strSql, cmdParam))
            {
                if (reader.Read())
                    return GetAdminInfoFromReader(reader);
                else
                    return new M_AdminInfo(true);
            }
        }

        #endregion
        
    }
}