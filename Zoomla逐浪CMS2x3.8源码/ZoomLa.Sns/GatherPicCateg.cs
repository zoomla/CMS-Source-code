/*---------------------------------------------------------------
// Copyright (C) 2008  
// 版权所有
//
// 文件名： GatherPicCateg.cs
// 文件功能描述：定义数据表GatherPicCateg的业务实体
//
// 创建标识：Owner(2008-10-11) 
//
// 修改标识：
// 修改描述：
----------------------------------------------------------------*/
using System;


namespace BDUModel
{
    /// <summary>
    ///GatherPicCateg业务实体
    /// </summary>
    [Serializable]
    public class GatherPicCateg
    {
        #region 字段定义

        ///<summary>
        ///
        ///</summary>
        private Guid gatherID = Guid.Empty;

        ///<summary>
        ///
        ///</summary>
        private Guid categID = Guid.Empty;


        #endregion

        #region 构造函数

        ///<summary>
        ///
        ///</summary>
        public GatherPicCateg()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public GatherPicCateg
        (
            Guid gatherID,
            Guid categID
        )
        {
            this.gatherID = gatherID;
            this.categID = categID;

        }

        #endregion

        #region 属性定义

        ///<summary>
        ///
        ///</summary>
        public Guid GatherID
        {
            get { return gatherID; }
            set { gatherID = value; }
        }

        ///<summary>
        ///
        ///</summary>
        public Guid CategID
        {
            get { return categID; }
            set { categID = value; }
        }

        #endregion

    }
}
