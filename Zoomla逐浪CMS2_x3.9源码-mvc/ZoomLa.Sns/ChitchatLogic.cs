using System;
using System.Collections.Generic;
using System.Text;
using BDUModel;
using System.Data.SqlClient;
using System.Data;
using ZoomLa.SQLDAL;

namespace BDULogic
{
    public class ChitchatLogic
    {
        #region 创建房间
        /// <summary>
        /// 创建房间
        /// </summary>
        /// <param name="cr"></param>
        /// <returns></returns>
        public static Guid CreatRoom(ChitchatRoom cr)
        {
            cr.ID = Guid.NewGuid();
            string sql = @"INSERT INTO [ZL_Sns_ChitchatRoom] (
	                    [ID],
	                    [RoomName],
	                    [UserID],
	                    [PassWord],
	                    [Titlle]
                    ) VALUES (
	                    @ID,
	                    @RoomName,
	                    @UserID,
	                    @PassWord,
	                    @Titlle
                    )";
            SqlParameter[] parameter ={
              new SqlParameter("@ID",cr.ID),
              new SqlParameter("@RoomName",cr.RoomName),
              new SqlParameter("@UserID",cr.UserID),
              new SqlParameter("@PassWord",cr.PassWord),
              new SqlParameter("@Titlle",cr.Titlle)
          };
            SqlHelper.ExecuteNonQuery(CommandType.Text, sql, parameter);
            return cr.ID;
        }
        #endregion

        #region 获取所有房间
        /// <summary>
        /// 获取所有房间
        /// </summary>
        /// <returns></returns>
        public static DataTable GetRoom()
        {
            try
            {
                string sql = @"SELECT ZL_Sns_ChitchatRoom.*, ZL_Sns_UserTable.UserName, ZL_Sns_UserTable.Userpic
                            FROM ZL_Sns_ChitchatRoom INNER JOIN
                                  ZL_Sns_UserTable ON ChitchatRoom.UserID = UserTable.UserID";
                SqlParameter[] parameter ={
              new SqlParameter("@ID","")};
                return SqlHelper.ExecuteTable(CommandType.Text, sql, parameter);

            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 获取房间
        /// <summary>
        /// 获取房间
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ChitchatRoom GetChitchatRoomByID(Guid id)
        {
            ChitchatRoom cr = new ChitchatRoom();
            string sql = @"select * from ZL_Sns_ChitchatRoom where ID=@ID";
            SqlParameter[] parameter ={
              new SqlParameter("@ID",id)};
            using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sql, parameter))
            {
                if (dr.Read())
                {
                    cr.ID = (Guid)dr["ID"];
                    cr.PassWord = dr["PassWord"].ToString();
                    cr.RoomName = dr["RoomName"].ToString();
                    cr.Titlle = dr["Titlle"].ToString();
                    cr.UserID = (Guid)dr["UserID"];
                }
            }
            return cr;

        }
        #endregion

        #region 房间密码是否正确
        /// <summary>
        /// 房间密码是否正确
        /// </summary>
        /// <param name="roomID"></param>
        /// <param name="wp"></param>
        /// <returns></returns>
        public static bool GetRoomPassWord(Guid roomID, string wp)
        {
            try
            {
                string sql = @"select PassWord from ZL_Sns_ChitchatRoom where ID=@ID";
                SqlParameter[] parameter ={
              new SqlParameter("@ID",roomID)};
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sql, parameter))
                {
                    if (dr.Read())
                    {
                        if (dr["PassWord"].ToString() == wp)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 加入到房间
        /// <summary>
        ///  加入到房间
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="roomID"></param>
        /// <param name="ccten"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static Guid JoinRoom(Guid userID, Guid roomID, string ccten, string userName)
        {
            try
            {
                Guid ID = Guid.NewGuid();
                string sql = @"INSERT INTO [dbo].[ZL_Sns_ChitchatNote] (
	                        [ID],
	                        [RoomID],
	                        [UserID],
	                        [CName],
	                        [CContent],
	                        [CreatTime]
                        ) VALUES (
	                        @ID,
	                        @RoomID,
	                        @UserID,
	                        @CName,
	                        @CContent,
	                        @CreatTime
                        )";
                SqlParameter[] parameter ={
              new SqlParameter("@ID",ID),
               new SqlParameter("@UserID",userID),
               new SqlParameter("@RoomID",roomID),
               new SqlParameter("@CName",userName),
               new SqlParameter("@CContent",ccten),
               new SqlParameter("@CreatTime",DateTime.Now)};
                SqlHelper.ExecuteNonQuery(CommandType.Text, sql, parameter);
                return ID;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 获取房间消息
        /// <summary>
        /// 获取房间消息
        /// </summary>
        /// <param name="roomID"></param>
        /// <returns></returns>
        public static DataTable GetRoomNote(Guid roomID)
        {
            try
            {
                string sql = @"select * from ZL_Sns_ChitchatNote where RoomID=@RoomID order by CreatTime DESC";
                SqlParameter[] parameter ={
              new SqlParameter("@RoomID",roomID)};
                return SqlHelper.ExecuteTable(CommandType.Text, sql, parameter);

            }
            catch
            {
                throw;
            }
        }
        #endregion
    }
}
