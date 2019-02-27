/*---------------------------------------------------------------
// Copyright (C) 2008  
// 版权所有
//
// 文件名： ProductTypetable.cs
// 文件功能描述：定义数据表ProductTypetable的业务实体
//
// 创建标识：Owner(2008-11-10) 
//
// 修改标识：
// 修改描述：
----------------------------------------------------------------*/
using System;


namespace FHModel
{
    /// <summary>
    ///ProductTypetable业务实体
    /// </summary>
    [Serializable]
    public class ProductTypetable
    {
        #region 字段定义

        ///<summary>
        ///
        ///</summary>
        private Guid iD = Guid.Empty;

        ///<summary>
        ///类型名称
        ///</summary>
        private string name = String.Empty;


        #endregion

        #region 构造函数

        ///<summary>
        ///
        ///</summary>
        public ProductTypetable()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public ProductTypetable
        (
            Guid iD,
            string name
        )
        {
            this.iD = iD;
            this.name = name;

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
        ///类型名称
        ///</summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        #endregion

    }
}
