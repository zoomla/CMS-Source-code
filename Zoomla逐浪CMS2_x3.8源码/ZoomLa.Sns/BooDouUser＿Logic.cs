using System;
using System.Collections.Generic;
using System.Text;
using BDUModel;
using System.Data;
using System.Data.SqlClient;
using ZoomLa.SQLDAL;

namespace BDULogic
{
    public class BooDouUser＿Logic
    {
        #region 添加用户参与运气事件信息
        /// <summary>
        /// 添加用户参与运气事件信息
        /// </summary>
        /// <param name="bduser"></param>
        public static void Add(BooDouUser bduser)
        {
            string SQLstr = @"INSERT INTO ZL_Sns_BooDouUser ([UserID],[BooDouTime])
     VALUES(@UserID,@BooDouTime)";

            SqlParameter[] sp ={
                new SqlParameter("@UserID",bduser.UserID),
                new SqlParameter("@BooDouTime",bduser.BooDouTime)
            };

            SqlHelper.ExecuteNonQuery(CommandType.Text, SQLstr, sp);
        }
        #endregion

        #region 根据用户ID查询这个用户最近一次使用布兜运气的时间
        /// <summary>
        /// 根据用户ID查询这个用户最近一次使用布兜运气的时间
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static DateTime GetTime(Guid id)
        {
            try
            {
                string SQLstr = @"SELECT Top 1 BooDouTime FROM ZL_Sns_BooDouUser WHERE UserID=@ID order by BooDouTime desc";

                SqlParameter[] sp ={ new SqlParameter("@ID", id) };

                DateTime dt = new DateTime();

                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, SQLstr, sp))
                {
                    if (dr.Read())
                    {
                        dt = dr["BooDouTime"] is DBNull ? new DateTime() : DateTime.Parse(dr["BooDouTime"].ToString());
                    }
                }
                return dt;
            }
            catch
            {
                throw;
            }
        }
        #endregion
    }
}
