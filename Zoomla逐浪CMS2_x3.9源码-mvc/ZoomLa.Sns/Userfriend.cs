using System;
using System.Collections.Generic;
using System.Text;

namespace BDUModel
{
    /// <summary>
    ///Userfriend业务实体
    /// </summary>
    [Serializable]
    public class Userfriend
    {
        #region 字段定义

        ///<summary>
        ///
        ///</summary>
        private Guid iD;

        ///<summary>
        ///主人编号
        ///</summary>
        private Guid hostID = Guid.Empty;

        ///<summary>
        ///好友编号
        ///</summary>
        private Guid friendID = Guid.Empty;

        ///<summary>
        ///所属组编号
        ///</summary>
        private Guid groupID = Guid.Empty;

        ///<summary>
        ///添加时间
        ///</summary>
        private DateTime createDate;

        ///<summary>
        ///黑名单
        ///</summary>
        private int blackList;

        ///<summary>
        ///好友mail
        ///</summary>
        private string mail;

        ///<summary>
        ///好友真实姓名
        ///</summary>
        private string realname;

        ///<summary>
        ///好友昵称
        ///</summary>
        private string username;

        ///<summary>
        ///好友分组名称
        ///</summary>
        private string groupname;


        #endregion

        #region 构造函数

        ///<summary>
        ///
        ///</summary>
        public Userfriend()
        {
        }

        ///<summary>
        ///
        ///</summary>


        #endregion

        #region 属性定义

        ///<summary>
        ///
        ///</summary>
        public Guid ID
        {
            get { return iD; }
            set { iD = value; }
        }

        ///<summary>
        ///主人编号
        ///</summary>
        public Guid HostID
        {
            get { return hostID; }
            set { hostID = value; }
        }

        ///<summary>
        ///好友编号
        ///</summary>
        public Guid FriendID
        {
            get { return friendID; }
            set { friendID = value; }
        }

        ///<summary>
        ///所属组编号
        ///</summary>
        public Guid GroupID
        {
            get { return groupID; }
            set { groupID = value; }
        }

        ///<summary>
        ///添加时间
        ///</summary>
        public DateTime CreateDate
        {
            get { return createDate; }
            set { createDate = value; }
        }

        ///<summary>
        ///黑名单
        ///</summary>
        public int BlackList
        {
            get { return blackList; }
            set { blackList = value; }
        }

        ///<summary>
        ///好友mail
        ///</summary>
        public string Mail
        {
            get { return mail; }
            set { mail = value; }
        }

        ///<summary>
        ///好友真实姓名
        ///</summary>
        public string Realname
        {
            get { return realname; }
            set { realname = value; }
        }

        ///<summary>
        ///好友昵称
        ///</summary>
        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        ///<summary>
        ///好友分组名称
        ///</summary>
        public string Groupname
        {
            get { return groupname; }
            set { groupname = value; }
        }

        private string userpic;


        /// <summary>
        /// 好友的图片
        /// </summary>
        public string Userpic
        {
            get { return userpic == string.Empty ? @"~\App_Themes\DefaultTheme\images\head.jpg" : userpic; }
            set { userpic = value; }
        }

        #endregion

    }
}
