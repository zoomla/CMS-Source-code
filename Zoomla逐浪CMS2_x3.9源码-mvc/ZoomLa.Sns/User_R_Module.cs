/*---------------------------------------------------------------
// Copyright (C) 2008  
// 版权所有
//
// 文件名： User_R_Module.cs
// 文件功能描述：定义数据表User_R_Module的业务实体
//
// 创建标识：Owner(2008-10-13) 
//
// 修改标识：
// 修改描述：
----------------------------------------------------------------*/
using System;


namespace BDUModel
{
    /// <summary>
    ///User_R_Module业务实体
    /// </summary>
    [Serializable]
    public class User_R_Module : SystemBDModule
    {
        #region 字段定义

        ///<summary>
        ///
        ///</summary>
        private Guid iD;

        ///<summary>
        ///
        ///</summary>
        private Guid userID = Guid.Empty;

        ///<summary>
        ///可见程度(0任何人可见1好友可见2隐藏）
        ///</summary>
        private int visibleDegree;

        ///<summary>
        ///模块编号
        ///</summary>
        private Guid moduleID = Guid.Empty;

        ///<summary>
        ///排序
        ///</summary>
        private int taix;


        #endregion

        #region 构造函数

        ///<summary>
        ///
        ///</summary>
        public User_R_Module()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public User_R_Module
        (
            Guid iD,
            Guid userID,
            int visibleDegree,
            Guid moduleID,
            int taix
        )
        {
            this.iD = iD;
            this.userID = userID;
            this.visibleDegree = visibleDegree;
            this.moduleID = moduleID;
            this.taix = taix;

        }

        #endregion

        #region 属性定义

        ///<summary>
        ///
        ///</summary>
        public new Guid ID
        {
            get { return iD; }
            set { iD = value; }
        }

        ///<summary>
        ///
        ///</summary>
        public Guid UserID
        {
            get { return userID; }
            set { userID = value; }
        }

        ///<summary>
        ///可见程度(0任何人可见1好友可见2隐藏）
        ///</summary>
        public int VisibleDegree
        {
            get { return visibleDegree; }
            set { visibleDegree = value; }
        }

        ///<summary>
        ///模块编号
        ///</summary>
        public Guid ModuleID
        {
            get { return moduleID; }
            set { moduleID = value; }
        }

        ///<summary>
        ///排序
        ///</summary>
        public int Taix
        {
            get { return taix; }
            set { taix = value; }
        }

        #endregion

    }
}
