using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using BDUModel;
using ZoomLa.SQLDAL;

namespace BDULogic
{
    public class I4GiveUForgive_Logic
    {
        #region 添加原谅信息
        /// <summary>
        /// 添加原谅信息
        /// </summary>
        /// <param name="forgive"></param>
        public static void Add(I4GiveUForgive forgive)
        {
            string SQLstr = @"INSERT INTO ZL_Sns_I4GiveUForgive([ConfessID],[ForgiveID],[ForgiveTime])
                    VALUES (@ConfessID,@ForgiveID,@ForgiveTime)";
            SqlParameter[] sp ={
                new SqlParameter("@ConfessID",forgive.ConfessID),
                new SqlParameter("@ForgiveID",forgive.ForgiveID),
                new SqlParameter("@ForgiveTime",forgive.ForgiveTime)
            };
            SqlHelper.ExecuteNonQuery(CommandType.Text, SQLstr, sp);
        }
        #endregion

        #region 查询用户原谅的次数
        /// <summary>
        /// 查询用户原谅的次数
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int GetForgiveCount(Guid id)
        {
            try
            {
                int Fcount = 0;
                string SQLstr = @"select distinct count(*) from ZL_Sns_I4GiveUForgive where ForgiveID=@ForgiveID";
                SqlParameter[] parameter ={
                new SqlParameter("@ForgiveID",id)};
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, SQLstr, parameter))
                {
                    while (dr.Read())
                    {
                        Fcount = Convert.ToInt32(dr[0].ToString());
                    }
                }
                return Fcount;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 查询用户原谅过的事情
        //public static List<string> GetForgiveCount(Guid id)
        //{

        //}
        #endregion

        #region 查询用户的好友最宽容的人
        /// <summary>
        /// 查询用户的好友最宽容的人
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<UserTable> GetFriendForgiveOrder(Guid id, PagePagination page)
        {
            try
            {
                string SQLstr = @"select a.aa,b.UserID,b.UserName,b.Userpic from 
(select count(*) as aa,ForgiveID from ZL_Sns_I4GiveUForgive group by ForgiveID ) as a
 inner join ZL_Sns_UserTable b on a.ForgiveID=b.UserID where a.ForgiveID in (select FriendID from ZL_Sns_Userfriend where 
HostID=@HostID) ";
                if (page != null)
                {
                    SQLstr = page.PaginationSql(SQLstr);
                }
                SqlParameter[] sp ={ new SqlParameter("@HostID", id) };
                List<UserTable> list = new List<UserTable>();
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, SQLstr, sp))
                {
                    while (dr.Read())
                    {
                        UserTable ut = new UserTable();
                        ut.UserID = (Guid)dr["UserID"];
                        ut.UserName = dr["UserName"].ToString();
                        ut.Userpic = dr["Userpic"].ToString();
                        list.Add(ut);
                    }
                }
                return list;

            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region

        #endregion

        #region

        #endregion
    }
}
