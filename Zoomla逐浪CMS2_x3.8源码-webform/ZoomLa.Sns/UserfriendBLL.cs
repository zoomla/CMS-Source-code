using System;
using System.Collections.Generic;
using System.Text;
using BDULogic;
using BDUModel;

namespace BDUBLL
{
    public class UserfriendBLL
    {
        #region 添加好友
        /// <summary>
        /// 添加好友
        /// </summary>
        /// <param name="uf"></param>
        /// <returns></returns>
        public Guid InsertUserfriend(Userfriend uf)
        {
            return UserfriendLogic.InsertUserfriend(uf);
        }
        #endregion

        #region 修改好友
        /// <summary>
        /// 修改好友
        /// </summary>
        /// <param name="uf"></param>
        /// <returns></returns>
        public void UpdateUserfriend(Userfriend uf)
        {
            UserfriendLogic.UpdateUserfriend(uf);
        }
        #endregion

        #region 删除好友
        /// <summary>
        /// 删除好友
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public void DelUserfriend(Guid ID)
        {
            UserfriendLogic.DelUserfriend(ID);
        }
        #endregion

        #region 修改黑名单
        /// <summary>
        /// 修改黑名单
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public void UpdateFriToBlack(Guid ID)
        {
            UserfriendLogic.UpdateFriToBlack(ID);
        }
        #endregion

        #region 通过HostID查询黑名单
        /// <summary>
        /// 通过HostID查询黑名单
        /// </summary>
        /// <param name="HostID"></param>
        /// <param name="page">分页</param>
        /// <returns></returns>
        public List<Userfriend> GetUserBlackfriendByHostID(Guid HostID, BDUModel.PagePagination page)
        {
            return UserfriendLogic.GetUserBlackfriendByHostID(HostID, page);
        }
        #endregion

        #region 通过GroupID查询信息
        /// <summary>
        /// 通过GroupID查询信息
        /// </summary>
        /// <param name="HostID"></param>
        /// <param name="page">分页</param>
        /// <returns></returns>
        public List<Userfriend> GetUserfriendByGroupID(Guid GroupID, Guid userID, BDUModel.PagePagination page)
        {
            return UserfriendLogic.GetUserfriendByGroupID(GroupID, userID, page);
        }
        #endregion

        #region 读取最新访问的好友
        /// <summary>
        /// 读取最新访问的好友
        /// </summary>
        /// <param name="HostID"></param>
        /// <returns></returns>
        public List<Userfriend> GetNewAttentFriend(Guid HostID)
        {
            return UserfriendLogic.GetNewAttentFriend(HostID);
        }
        #endregion

        #region 通过HostID查询信息
        /// <summary>
        /// 通过HostID查询信息
        /// </summary>
        /// <param name="HostID"></param>
        /// <returns></returns>
        public List<Userfriend> GetUserfriendByHostID(Guid HostID, BDUModel.PagePagination page)
        {
            return UserfriendLogic.GetUserfriendByHostID(HostID, page);
        }
        #endregion

        #region 根据ID查询用户
        /// <summary>
        /// 根据ID查询用户
        /// </summary>
        /// <param name="UserEmail"></param>
        /// <returns></returns>
        public Userfriend GetUserfriendByID(Guid ID)
        {
            return UserfriendLogic.GetUserfriendByID(ID);
        }
        #endregion

        #region 判断2个ID是否在黑名单
        /// <summary>
        /// 判断2个ID是否在黑名单(不在返回true)
        /// </summary>
        /// <param name="HostID"></param>
        /// <param name="FriendID"></param>
        /// <returns></returns>
        public bool CheckBlackList(Guid HostID, Guid FriendID)
        {
            return UserfriendLogic.CheckBlackList(HostID, FriendID);
        }
        #endregion

        #region 判断2个ID是否是好友
        /// <summary>
        ///  判断2个ID是否是好友
        /// </summary>
        /// <param name="HostID"></param>
        /// <param name="FriendID"></param>
        /// <returns></returns>
        public bool CheckUserfriendByIDandID(int HostID, int FriendID)
        {
            return UserfriendLogic.CheckUserfriendByIDandID(HostID, FriendID);
        }
        #endregion

        #region 判断2个ID是否存在列表中
        /// <summary>
        ///  判断2个ID是否存在列表中(包括黑名单)返回false表示不存在
        /// </summary>
        /// <param name="HostID"></param>
        /// <param name="FriendID"></param>
        /// <returns></returns>
        public bool CheckUserfriendblackByIDandID(Guid HostID, Guid FriendID)
        {
            return UserfriendLogic.CheckUserfriendblackByIDandID(HostID, FriendID);
        }
        #endregion

        #region 通过好友编号和用户编号获取好友所在的组
        /// <summary>
        ///  通过好友编号和用户编号获取好友所在的组
        /// </summary>
        /// <param name="hostID"></param>
        /// <param name="fID"></param>
        /// <returns></returns>
        public List<Guid> GetFriendGroup(Guid hostID, Guid fID)
        {
            return UserfriendLogic.GetFriendGroup(hostID, fID);
        }
        #endregion

        #region 通过好友编号和用户编号删除好友所在的组
        /// <summary>
        ///  通过好友编号和用户编号删除好友所在的组
        /// </summary>
        /// <param name="hostID"></param>
        /// <param name="fID"></param>
        /// <returns></returns>
        public void DeleteFriendGroup(Guid hostID, Guid fID)
        {
            UserfriendLogic.DeleteFriendGroup(hostID, fID);
        }
        #endregion

        #region 通过昵称,邮箱查询好友信息
        /// <summary>
        /// 通过昵称,邮箱查询好友信息
        /// </summary>
        /// <param name="username"></param>
        /// <param name="email"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public List<Userfriend> GetUserFriendLike(string username, string email, Guid userid)
        {
            return UserfriendLogic.GetUserFriendLike(username, email, userid);
        }
        #endregion
    }

}
