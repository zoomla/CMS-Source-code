/*---------------------------------------------------------------
// Copyright (C) 2008  
// 版权所有
//
// 文件名： SystemBDModule.cs
// 文件功能描述：定义数据表SystemBDModule的业务实体
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
    ///SystemBDModule业务实体
    /// </summary>
    [Serializable]
    public class SystemBDModule
    {
        #region 字段定义

        ///<summary>
        ///
        ///</summary>
        private Guid iD;

        ///<summary>
        ///模块名称
        ///</summary>
        private string moduleName = String.Empty;

        ///<summary>
        ///模块URl
        ///</summary>
        private string moduleURL = String.Empty;

        ///<summary>
        ///模块图标
        ///</summary>
        private string modulePic = String.Empty;

        ///<summary>
        ///模块描述
        ///</summary>
        private string moduleDepict = String.Empty;

        ///<summary>
        ///状态(0停用1正常)
        ///</summary>
        private int state;

        ///<summary>
        ///是否可以删除
        ///</summary>
        private bool canDelete;

        ///<summary>
        ///是否可以隐藏
        ///</summary>
        private bool canConceal;

        ///<summary>
        ///大图标
        ///</summary>
        private string moduleViewPic = String.Empty;


        #endregion

        #region 构造函数

        ///<summary>
        ///
        ///</summary>
        public SystemBDModule()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public SystemBDModule
        (
            Guid iD,
            string moduleName,
            string moduleURL,
            string modulePic,
            string moduleDepict,
            int state,
            bool canDelete,
            bool canConceal,
            string moduleViewPic
        )
        {
            this.iD = iD;
            this.moduleName = moduleName;
            this.moduleURL = moduleURL;
            this.modulePic = modulePic;
            this.moduleDepict = moduleDepict;
            this.state = state;
            this.canDelete = canDelete;
            this.canConceal = canConceal;
            this.moduleViewPic = moduleViewPic;

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
        ///模块名称
        ///</summary>
        public string ModuleName
        {
            get { return moduleName; }
            set { moduleName = value; }
        }

        ///<summary>
        ///模块URl
        ///</summary>
        public string ModuleURL
        {
            get { return moduleURL; }
            set { moduleURL = value; }
        }

        ///<summary>
        ///模块图标
        ///</summary>
        public string ModulePic
        {
            get { return modulePic; }
            set { modulePic = value; }
        }

        ///<summary>
        ///模块描述
        ///</summary>
        public string ModuleDepict
        {
            get { return moduleDepict; }
            set { moduleDepict = value; }
        }

        ///<summary>
        ///状态(0停用1正常)
        ///</summary>
        public int State
        {
            get { return state; }
            set { state = value; }
        }

        ///<summary>
        ///是否可以删除
        ///</summary>
        public bool CanDelete
        {
            get { return canDelete; }
            set { canDelete = value; }
        }

        ///<summary>
        ///是否可以隐藏
        ///</summary>
        public bool CanConceal
        {
            get { return canConceal; }
            set { canConceal = value; }
        }

        ///<summary>
        ///大图标
        ///</summary>
        public string ModuleViewPic
        {
            get { return moduleViewPic; }
            set { moduleViewPic = value; }
        }

        #endregion

    }
}
