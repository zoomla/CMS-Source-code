/*---------------------------------------------------------------
// Copyright (C) 2008  
// 版权所有
//
// 文件名： GSType.cs
// 文件功能描述：定义数据表GSType的业务实体
//
// 创建标识：Owner(2008-10-10) 
//
// 修改标识：
// 修改描述：
----------------------------------------------------------------*/
using System;


namespace BDUModel
{
    /// <summary>
    ///GSType业务实体
    /// </summary>
    [Serializable]
    public class GSType
    {
        #region 字段定义

        ///<summary>
        ///
        ///</summary>
        private Guid iD = Guid.Empty;

        ///<summary>
        ///群族类别名称
        ///</summary>
        private string gSTypeName = String.Empty;


        #endregion

        #region 构造函数

        ///<summary>
        ///
        ///</summary>
        public GSType()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public GSType
        (
            Guid iD,
            string name
        )
        {
            this.iD = iD;
            this.gSTypeName = name;

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
        ///群族类别名称
        ///</summary>
        public string GSTypeName
        {
            get { return gSTypeName; }
            set { gSTypeName = value; }
        }

        #endregion

    }
}
