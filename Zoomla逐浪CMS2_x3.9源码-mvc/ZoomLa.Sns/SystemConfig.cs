/*---------------------------------------------------------------
// Copyright (C) 2008  
// 版权所有
//
// 文件名： SystemConfig.cs
// 文件功能描述：定义数据表SystemConfig的业务实体
//
// 创建标识：Owner(2008-11-8) 
//
// 修改标识：
// 修改描述：
----------------------------------------------------------------*/
using System;


namespace ZoomLa.Sns.Model
{
    /// <summary>
    ///SystemConfig业务实体
    /// </summary>
    [Serializable]
    public class SystemConfig
    {
        #region 字段定义

        ///<summary>
        ///排序
        ///</summary>
        private int iD;

        ///<summary>
        ///关键字
        ///</summary>
        private string iDKey = String.Empty;

        ///<summary>
        ///说明
        ///</summary>
        private string iDtext = String.Empty;

        ///<summary>
        ///值
        ///</summary>
        private string iDvalue = String.Empty;


        #endregion

        #region 构造函数

        ///<summary>
        ///
        ///</summary>
        public SystemConfig()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public SystemConfig
        (
            int iD,
            string iDKey,
            string iDtext,
            string iDvalue
        )
        {
            this.iD = iD;
            this.iDKey = iDKey;
            this.iDtext = iDtext;
            this.iDvalue = iDvalue;

        }

        #endregion

        #region 属性定义

        ///<summary>
        ///排序
        ///</summary>
        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }

        ///<summary>
        ///关键字
        ///</summary>
        public string IDKey
        {
            get { return iDKey; }
            set { iDKey = value; }
        }

        ///<summary>
        ///说明
        ///</summary>
        public string IDtext
        {
            get { return iDtext; }
            set { iDtext = value; }
        }

        ///<summary>
        ///值
        ///</summary>
        public string IDvalue
        {
            get { return iDvalue; }
            set { iDvalue = value; }
        }

        #endregion

    }
}
