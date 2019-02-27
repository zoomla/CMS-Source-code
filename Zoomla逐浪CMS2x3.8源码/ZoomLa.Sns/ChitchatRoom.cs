/*---------------------------------------------------------------
// Copyright (C) 2008  
// 版权所有
//
// 文件名： ChitchatRoom.cs
// 文件功能描述：定义数据表ChitchatRoom的业务实体
//
// 创建标识：Owner(2008-10-24) 
//
// 修改标识：
// 修改描述：
----------------------------------------------------------------*/
using System;


namespace BDUModel
{
    /// <summary>
    ///ChitchatRoom业务实体
    /// </summary>
    [Serializable]
    public class ChitchatRoom
    {
        #region 字段定义

        ///<summary>
        ///
        ///</summary>
        private Guid iD;

        ///<summary>
        ///房间名称
        ///</summary>
        private string roomName = String.Empty;

        ///<summary>
        ///创建人
        ///</summary>
        private Guid userID = Guid.Empty;

        ///<summary>
        ///密码
        ///</summary>
        private string passWord = String.Empty;

        ///<summary>
        ///聊天话题
        ///</summary>
        private string titlle = String.Empty;


        #endregion

        #region 构造函数

        ///<summary>
        ///
        ///</summary>
        public ChitchatRoom()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public ChitchatRoom
        (
            Guid iD,
            string roomName,
            Guid userID,
            string passWord,
            string titlle
        )
        {
            this.iD = iD;
            this.roomName = roomName;
            this.userID = userID;
            this.passWord = passWord;
            this.titlle = titlle;

        }

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
        ///房间名称
        ///</summary>
        public string RoomName
        {
            get { return roomName; }
            set { roomName = value; }
        }

        ///<summary>
        ///创建人
        ///</summary>
        public Guid UserID
        {
            get { return userID; }
            set { userID = value; }
        }

        ///<summary>
        ///密码
        ///</summary>
        public string PassWord
        {
            get { return passWord; }
            set { passWord = value; }
        }

        ///<summary>
        ///聊天话题
        ///</summary>
        public string Titlle
        {
            get { return titlle; }
            set { titlle = value; }
        }

        #endregion

    }
}
