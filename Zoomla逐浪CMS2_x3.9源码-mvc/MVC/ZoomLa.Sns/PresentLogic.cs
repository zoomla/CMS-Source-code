using System;
using System.Collections.Generic;
using System.Text;
using BDUModel;
using System.Data;
using System.Data.SqlClient;
using ZoomLa.SQLDAL;

namespace BDULogic
{
    public class PresentLogic
    {
        #region 获取系统礼物
        /// <summary>
        /// 获取系统礼物
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static List<SystemPresent> GetQuestionByUserID(int isFancy, PagePagination page)
        {
            try
            {
                List<SystemPresent> list = new List<SystemPresent>();
                string sql = @"select * from ZL_Sns_SystemPresent where IsFancy=@IsFancy";
                if (page != null)
                {
                    sql = page.PaginationSql(sql);
                }
                SqlParameter[] parameter ={
                new SqlParameter("@IsFancy",isFancy)};
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sql, parameter))
                {
                    while (dr.Read())
                    {
                        SystemPresent qu = new SystemPresent();
                        ReadSystemPresent(dr, qu);
                        list.Add(qu);
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

        #region 赠送礼物
        /// <summary>
        /// 赠送礼物
        /// </summary>
        /// <param name="sp"></param>
        /// <returns></returns>
        public static Guid LargessPresent(UserPresent sp)
        {
            try
            {
                sp.ID = Guid.NewGuid();
                string sql = @"INSERT INTO [ZL_Sns_UserPresent] (
	                            [ID],
	                            [HostID],
	                            [FriendID],
	                            [PresentID],
	                            [CreatTime],
	                            [IsAnonymity],
                                [LeaveWord]
                            ) VALUES (
	                            @ID,
	                            @HostID,
	                            @FriendID,
	                            @PresentID,
	                            @CreatTime,
	                            @IsAnonymity,
                                @LeaveWord
                            )";
                SqlParameter[] parameter ={
                new SqlParameter("@ID",sp.ID ),
                new SqlParameter("@HostID",sp.HostID ),
                new SqlParameter("@FriendID",sp.FriendID ),
                new SqlParameter("@PresentID",sp.PresentID ),
                new SqlParameter("@CreatTime",DateTime.Now ),
                new SqlParameter("@IsAnonymity",sp.IsAnonymity),
                new SqlParameter("@LeaveWord",sp.LeaveWord)
               };
                SqlHelper.ExecuteNonQuery(CommandType.Text, sql, parameter);
                return sp.ID;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 获取我的礼物
        /// <summary>
        ///获取我的礼物 
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static DataTable GetUserPresentListByUserID(Guid userID)
        {
            try
            {
                string sql = @"SELECT ZL_Sns_UserPresent.*, ZL_Sns_SystemPresent.PresentPic, ZL_Sns_SystemPresent.PresentPrice, 
                                  ZL_Sns_SystemPresent.IsFancy,ZL_Sns_UserTable.UserName
                            FROM ZL_Sns_UserPresent INNER JOIN
                                  ZL_Sns_SystemPresent ON ZL_Sns_UserPresent.PresentID = ZL_Sns_SystemPresent.ID 
                                 INNER JOIN
                                      ZL_Sns_UserTable ON ZL_Sns_UserTable.UserID = ZL_Sns_UserPresent.HostID where FriendID=@HostID";
                SqlParameter[] parameter ={
                new SqlParameter("@HostID",userID)};
                DataTable dt = SqlHelper.ExecuteTable(CommandType.Text, sql, parameter);
                return dt;
            }
            catch
            {
                throw;
            }
        }
        #endregion


        public static void ReadSystemPresent(SqlDataReader dr, SystemPresent sp)
        {
            sp.ID = (Guid)dr["ID"];
            sp.IsFancy = int.Parse(dr["IsFancy"].ToString());
            sp.PresentPic = dr["PresentPic"].ToString();
            sp.PresentPrice = dr["PresentPrice"] is DBNull ? 0.00M : decimal.Parse(dr["PresentPrice"].ToString());
        }
    }
}
