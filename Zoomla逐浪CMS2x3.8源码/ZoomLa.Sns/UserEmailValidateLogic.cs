using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using BDUModel;
using ZoomLa.SQLDAL;

namespace BDULogic
{
    public class UserEmailValidateLogic
    {


        #region 添加验证用户
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="validate"></param>
        /// <returns></returns>
        public static Guid InsertUserEmailValidate(UserEmailValidate validate)
        {
            try
            {
                validate.ID = Guid.NewGuid();
                string sqlInsertUserEmailValidate = @"INSERT INTO [UserEmailValidate]
           ([ID],[UserEmail],[UserPWD],[UserName],[Validate],[CreatTime])  VALUES (@ID,@UserEmail,@UserPWD,@UserName,@Validate,@CreatTime)";
                SqlParameter[] parameter ={
                new SqlParameter("ID",validate.ID),
                new SqlParameter("UserEmail",validate.UserEmail),
                new SqlParameter("UserPWD",validate.Userpwd),
                new SqlParameter("UserName",validate.UserName),
                new SqlParameter("Validate",validate.Validate),
                new SqlParameter("CreatTime",DateTime.Now)
            };
                SqlHelper.ExecuteNonQuery(CommandType.Text, sqlInsertUserEmailValidate, parameter);
                return validate.ID;
            }
            catch
            {
                throw;
            }

        }
        #endregion

        #region 验证激活号码是否存在
        /// <summary>
        /// 验证激活号码是否存在,存在返回TRUE,不存在返回FLASE
        /// </summary>
        /// <param name="Validate"></param>
        /// <returns></returns>
        public static bool CheckUserEmailValidateByValidate(string Validate)
        {
            try
            {
                string sqlCheckUserEmailValidateByValidate = @"select * from [UserEmailValidate] where Validate=@Validate";
                SqlParameter[] parameter ={
                new SqlParameter("Validate",Validate)};

                if (SqlHelper.ExecuteScalar(CommandType.Text, sqlCheckUserEmailValidateByValidate, parameter) == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }

            }
            catch
            {
                throw;
            }
        }
        #endregion


        #region 根据激活码读取临时表数据
        /// <summary>
        /// 根据激活码读取临时表数据
        /// </summary>
        /// <param name="Validate"></param>
        /// <returns></returns>
        public static UserEmailValidate GetUserEmailValidateByValidate(string Validate)
        {
            try
            {
                string sqlGetUserEmailValidateByValidate = @"select Top 1 * from [UserEmailValidate] where Validate=@Validate";
                SqlParameter[] parameter ={
                new SqlParameter("Validate",Validate)};
                UserEmailValidate uv = new UserEmailValidate();
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sqlGetUserEmailValidateByValidate, parameter))
                {
                    if(dr.Read())
                    {
                        ReadUserEmailValidate(dr, uv);    
                    }
                }
                return uv;

            }
            catch
            {
                throw;
            }
        }
        #endregion


        #region 根据ID删除相应临时表数据
        /// <summary>
        /// 根据ID删除相应临时表数据
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static bool DelUserEmailValidateById(Guid ID)
        {
            try
            {
                string sqlDelUserEmailValidateById = @"delete from UserEmailValidate where ID=@ID";
                SqlParameter[] parameter ={
                new SqlParameter("ID",ID)};

                return (SqlHelper.ExecuteNonQuery(CommandType.Text, sqlDelUserEmailValidateById, parameter) > 0);
            }
                
            catch
            {
                throw;
            }

        }
        #endregion

        #region 读取用户临时表数据
        /// <summary>
        /// 读取用户临时表数据
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="uv"></param>
        public static void ReadUserEmailValidate(SqlDataReader dr, UserEmailValidate uv)
        {
            uv.ID = (Guid)dr["ID"];
            uv.CreatTime = dr["CreatTime"] is DBNull ? new DateTime() : DateTime.Parse(dr["CreatTime"].ToString());
            uv.UserName = dr["UserName"].ToString();
            uv.Userpwd = dr["Userpwd"].ToString();
            uv.UserEmail = dr["UserEmail"].ToString();
        }
        #endregion
    }
}
