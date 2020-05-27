using System;
using System.Collections.Generic;
using System.Text;
using BDUModel;
using System.Data.SqlClient;
using System.Data;
using ZoomLa.SQLDAL;
namespace BDULogic
{
    public class SystemUser_Logic
    {
        #region 添加系统管理账户
        /// <summary>
        /// 添加系统管理账户
        /// </summary>
        /// <param name="user"></param>
        public static void Add(SystemUser user)
        {
            user.ID = Guid.NewGuid();
            string SQLstr = "insert into SystemUser (ID,UserName,UserPwd) values (@ID,@UserName,@UserPwd)";

            SqlParameter[] sp ={
            new SqlParameter("@ID",user.ID ),
            new SqlParameter("@UserName",user.UserName),
            new SqlParameter("@UserPwd",user.UserPwd)
            };

            SqlHelper.ExecuteNonQuery(CommandType.Text, SQLstr, sp);

        }
        #endregion

        #region 修改密码
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="user"></param>
        public static void Update(SystemUser user)
        {
            string SQLstr = "update SystemUser set UserPwd=@UserPwd where ID=@ID";
            SqlParameter[] sp ={
            new SqlParameter("@ID",user.ID ),
            new SqlParameter("@UserPwd",user.UserPwd)
            };
            SqlHelper.ExecuteNonQuery(CommandType.Text, SQLstr, sp);

        }
        #endregion

        #region 根据用户名密码查询用户
        /// <summary>
        /// 根据用户名密码查询用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static SystemUser GetUser(SystemUser user)
        {
            string SQLstr = "select * from SystemUser where UserName=@UserName and UserPwd=@UserPwd";
            SqlParameter[] sp ={ 
                new SqlParameter("@UserName", user.UserName), 
                new SqlParameter("@UserPwd", user.UserPwd) 
            };
            SystemUser userVO=new SystemUser ();
            try{
            using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, SQLstr, sp))
                {
                    while (dr.Read())
                    {
                        ReadSystemTable(dr, userVO);
                    }
                }
                return userVO;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 读取系统表数据
        /// <summary>
        /// 读取系统表数据
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="user"></param>
        private static void ReadSystemTable(SqlDataReader dr, SystemUser user)
        {
            user.ID = (Guid)dr["ID"];
            user.UserName = dr["UserName"].ToString();
            user.UserPwd = dr["UserPwd"].ToString();
        }
        #endregion
    }
}
