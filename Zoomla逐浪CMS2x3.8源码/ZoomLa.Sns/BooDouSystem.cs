/*---------------------------------------------------------------
// Copyright (C) 2008  
// 版权所有
//
// 文件名： BooDouSystem.cs
// 文件功能描述：定义数据表BooDouSystem的业务实体
//
// 创建标识：Owner(2008-10-15) 
//
// 修改标识：
// 修改描述：
----------------------------------------------------------------*/
using System;


namespace BDUModel
{
    /// <summary>
    ///BooDouSystem业务实体
    /// </summary>
    [Serializable]
    public class BooDouSystem
    {
        #region 字段定义

        ///<summary>
        ///
        ///</summary>
        private int iD;

        ///<summary>
        ///初始化名称
        ///</summary>
        private string bDKey = String.Empty;

        ///<summary>
        ///初始化值
        ///</summary>
        private string bDValue = String.Empty;


        #endregion

        #region 构造函数

        ///<summary>
        ///
        ///</summary>
        public BooDouSystem()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public BooDouSystem
        (
            int iD,
            string bDKey,
            string bDValue
        )
        {
            this.iD = iD;
            this.bDKey = bDKey;
            this.bDValue = bDValue;

        }

        #endregion

        #region 属性定义

        ///<summary>
        ///
        ///</summary>
        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }

        ///<summary>
        ///初始化名称
        ///</summary>
        public string BDKey
        {
            get { return bDKey; }
            set { bDKey = value; }
        }

        ///<summary>
        ///初始化值
        ///</summary>
        public string BDValue
        {
            get { return bDValue; }
            set { bDValue = value; }
        }

        #endregion

    }
}
