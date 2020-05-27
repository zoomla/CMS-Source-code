/*---------------------------------------------------------------
// Copyright (C) 2008  
// 版权所有
//
// 文件名： ActiveType.cs
// 文件功能描述：定义数据表ActiveType的业务实体
//
// 创建标识：Owner(2008-10-17) 
//
// 修改标识：
// 修改描述：
----------------------------------------------------------------*/
using System;


namespace BDUModel
{
    /// <summary>
    ///ActiveType业务实体
    /// </summary>
    [Serializable]
    public class ActiveType
    {
        #region 字段定义

        ///<summary>
        ///编号
        ///</summary>
        private Guid iD = Guid.Empty;

        ///<summary>
        ///类型名称
        ///</summary>
        private string typeName = String.Empty;


        #endregion

        #region 构造函数

        ///<summary>
        ///
        ///</summary>
        public ActiveType()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public ActiveType
        (
            Guid iD,
            string typeName
        )
        {
            this.iD = iD;
            this.typeName = typeName;

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
        ///类型名称
        ///</summary>
        public string TypeName
        {
            get { return typeName; }
            set { typeName = value; }
        }

        #endregion

    }
}
