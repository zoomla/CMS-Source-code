using System;
using System.Collections.Generic;
using System.Text;
using BDULogic;
using BDUModel;


namespace BDUBLL
{
    public class FriendGroupBLL
    {
        #region 创建用户组
        /// <summary>
        /// 创建用户组
        /// </summary>
        /// <param name="fg"></param>
        /// <returns></returns>
        public Guid InsertFriendGroup(FriendGroup fg)
        {
            return FriendGroupLogic.InsertFriendGroup(fg);
        }
        #endregion

        #region 修改好友组
        /// <summary>
        /// 修改好友组
        /// </summary>
        /// <param name="fg"></param>
        /// <returns></returns>
        public void UpdateFriendGroup(FriendGroup fg)
        {
             FriendGroupLogic.UpdateFriendGroup(fg);
        }
        #endregion

        #region 删除好友分组
        /// <summary>
        /// 删除好友分组
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public void DelFriendGroup(Guid ID)
        {
             FriendGroupLogic.DelFriendGroup(ID);
        }
        #endregion

        #region 查询分组名称重复
        /// <summary>
        /// 查询分组名称重复
        /// </summary>
        /// <param name="HostID"></param>
        /// <param name="GroupName"></param>
        /// <returns></returns>
        public bool CheckGroupNameByHostID(Guid HostID, string GroupName)
        {
            return FriendGroupLogic.CheckGroupNameByHostID(HostID,GroupName);
        }
        #endregion

        #region 通过HostID查询信息
        /// <summary>
        /// 通过HostID查询信息
        /// </summary>
        /// <param name="HostID"></param>
        /// <returns></returns>
        public List<FriendGroup> GetFriendGroupByHostID(Guid HostID, PagePagination page)
        {
            return FriendGroupLogic.GetFriendGroupByHostID(HostID,page);
        }
        #endregion

        #region 通过ID查询
        /// <summary>
        /// 通过ID查询信息
        /// </summary>
        /// <param name="HostID"></param>
        /// <returns></returns>
        public  FriendGroup GetFriendGroupOneByID(Guid ID)
        {
            return FriendGroupLogic.GetFriendGroupOneByID(ID);
        }
        #endregion

    }
}
