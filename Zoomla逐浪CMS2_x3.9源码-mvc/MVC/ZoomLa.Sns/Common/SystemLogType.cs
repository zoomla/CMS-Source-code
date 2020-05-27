using System;
using System.Data;
using System.Configuration;



namespace BDUModel
{
    public enum SystemLogType
    {
        /// <summary>
        /// 好友访问友类型
        /// </summary>
        CallFriend,
        /// <summary>
        /// 好友发表新的日志
        /// </summary>
        FriendAppearLog,

        /// <summary>
        /// 提问
        /// </summary>
        PutQuestion,

        /// <summary>
        /// 添加好友
        /// </summary>
        AddFriend,

        /// <summary>
        /// 流入
        /// </summary>
        Afflux,
        /// <summary>
        /// 流出
        /// </summary>
        Pour,
        /// <summary>
        /// 兑换
        /// </summary>
        Change,
        /// <summary>
        /// 所有
        /// </summary>
        All

    }
}
