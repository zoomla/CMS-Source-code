/*---------------------------------------------------------------
// Copyright (C) 2008  
// 版权所有
//
// 文件名： LotNote.cs
// 文件功能描述：定义数据表LotNote的业务实体
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
    ///LotNote业务实体
    /// </summary>
    [Serializable]
    public class LotNote
    {
        #region 字段定义

        ///<summary>
        ///
        ///</summary>
        private Guid iD = Guid.Empty;

        ///<summary>
        ///
        ///</summary>
        private Guid startID = Guid.Empty;

        ///<summary>
        ///
        ///</summary>
        private Guid endID = Guid.Empty;

        ///<summary>
        ///
        ///</summary>
        private Guid messageID = Guid.Empty;

        ///<summary>
        ///
        ///</summary>
        private int noteType;


        #endregion

        #region 构造函数

        ///<summary>
        ///
        ///</summary>
        public LotNote()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public LotNote
        (
            Guid iD,
            Guid startID,
            Guid endID,
            Guid messageID,
            int noteType
        )
        {
            this.iD = iD;
            this.startID = startID;
            this.endID = endID;
            this.messageID = messageID;
            this.noteType = noteType;

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
        ///
        ///</summary>
        public Guid StartID
        {
            get { return startID; }
            set { startID = value; }
        }

        ///<summary>
        ///
        ///</summary>
        public Guid EndID
        {
            get { return endID; }
            set { endID = value; }
        }

        ///<summary>
        ///
        ///</summary>
        public Guid MessageID
        {
            get { return messageID; }
            set { messageID = value; }
        }

        ///<summary>
        ///
        ///</summary>
        public int NoteType
        {
            get { return noteType; }
            set { noteType = value; }
        }

        #endregion

    }
}
