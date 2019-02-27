/*---------------------------------------------------------------
// Copyright (C) 2008  
// 版权所有
//
// 文件名： Messageboard.cs
// 文件功能描述：定义数据表Messageboard的业务实体
//
// 创建标识：Owner(2008-10-22) 
//
// 修改标识：
// 修改描述：
----------------------------------------------------------------*/
using System;


namespace BDUModel
{
    /// <summary>
    ///Messageboard业务实体
    /// </summary>
    [Serializable]
    public class Messageboard
    {
        #region 字段定义

        ///<summary>
        ///编号
        ///</summary>
        private Guid iD = Guid.Empty;

        ///<summary>
        ///留言者
        ///</summary>
        private int sendID ;

        ///<summary>
        ///被留言者
        ///</summary>
        private int inceptID ;

        ///<summary>
        ///内容
        ///</summary>
        private string mcontent = String.Empty;

        ///<summary>
        ///时间
        ///</summary>
        private DateTime addtime;

        ///<summary>
        ///回复ID
        ///</summary>
        private Guid restoreID = Guid.Empty;

        ///<summary>
        ///0为正常 1为删除
        ///</summary>
        private int state;


        #endregion

        #region 构造函数

        ///<summary>
        ///
        ///</summary>
        public Messageboard()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public Messageboard
        (
            Guid iD,
            int sendID,
            int inceptID,
            string mcontent,
            DateTime addtime,
            Guid restoreID,
            int state
        )
        {
            this.iD = iD;
            this.sendID = sendID;
            this.inceptID = inceptID;
            this.mcontent = mcontent;
            this.addtime = addtime;
            this.restoreID = restoreID;
            this.state = state;

        }

        #endregion

        #region 属性定义

        ///<summary>
        ///编号
        ///</summary>
        public Guid ID
        {
            get { return iD; }
            set { iD = value; }
        }

        ///<summary>
        ///留言者
        ///</summary>
        public int SendID
        {
            get { return sendID; }
            set { sendID = value; }
        }

        ///<summary>
        ///被留言者
        ///</summary>
        public int InceptID
        {
            get { return inceptID; }
            set { inceptID = value; }
        }

        ///<summary>
        ///内容
        ///</summary>
        public string Mcontent
        {
            get { return mcontent; }
            set { mcontent = value; }
        }

        ///<summary>
        ///时间
        ///</summary>
        public DateTime Addtime
        {
            get { return addtime; }
            set { addtime = value; }
        }

        ///<summary>
        ///回复ID
        ///</summary>
        public Guid RestoreID
        {
            get { return restoreID; }
            set { restoreID = value; }
        }

        ///<summary>
        ///0为正常 1为删除
        ///</summary>
        public int State
        {
            get { return state; }
            set { state = value; }
        }

        #endregion

    }
}
