using System;
using System.Collections.Generic;
using System.Text;
using BDULogic;
using BDUModel;
using System.Data;

namespace BDUBLL
{
    public class ChitchatBLL
    {
        #region 创建房间
        /// <summary>
        /// 创建房间
        /// </summary>
        /// <param name="cr"></param>
        /// <returns></returns>
        public Guid CreatRoom(ChitchatRoom cr)
        {
            return ChitchatLogic.CreatRoom(cr);
        }
        #endregion

        #region 获取所有房间
        /// <summary>
        /// 获取所有房间
        /// </summary>
        /// <returns></returns>
        public DataTable GetRoom()
        {
            return ChitchatLogic.GetRoom();
        }
        #endregion

        #region 房间密码是否正确
        /// <summary>
        /// 房间密码是否正确
        /// </summary>
        /// <param name="roomID"></param>
        /// <param name="wp"></param>
        /// <returns></returns>
        public bool GetRoomPassWord(Guid roomID, string wp)
        {
            return ChitchatLogic.GetRoomPassWord(roomID, wp);
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
        public Guid JoinRoom(Guid userID, Guid roomID, string ccten, string userName)
        {
            return ChitchatLogic.JoinRoom(userID, roomID, ccten, userName);
        }
        #endregion

        #region 获取房间消息
        /// <summary>
        /// 获取房间消息
        /// </summary>
        /// <param name="roomID"></param>
        /// <returns></returns>
        public DataTable GetRoomNote(Guid roomID)
        {
            return ChitchatLogic.GetRoomNote(roomID);
        }
        #endregion

        #region 获取房间
        /// <summary>
        /// 获取房间
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ChitchatRoom GetChitchatRoomByID(Guid id)
        {
            return ChitchatLogic.GetChitchatRoomByID(id);
        }
        #endregion
    }
}
