/*---------------------------------------------------------------
// Copyright (C) 2008  
// 版权所有
//
// 文件名： SystemPresent.cs
// 文件功能描述：定义数据表SystemPresent的业务实体
//
// 创建标识：Owner(2008-10-23) 
//
// 修改标识：
// 修改描述：
----------------------------------------------------------------*/
using System;


namespace BDUModel
{
    /// <summary>
    ///SystemPresent业务实体
    /// </summary>
    [Serializable]
    public class SystemPresent
    {
        #region 字段定义

        ///<summary>
        ///
        ///</summary>
        private Guid iD;

        ///<summary>
        ///礼物图片
        ///</summary>
        private string presentPic = String.Empty;

        ///<summary>
        ///礼物价格
        ///</summary>
        private decimal presentPrice;

        ///<summary>
        ///
        ///</summary>
        private int isFancy;


        #endregion

        #region 构造函数

        ///<summary>
        ///
        ///</summary>
        public SystemPresent()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public SystemPresent
        (
            Guid iD,
            string presentPic,
            decimal presentPrice,
            int isFancy
        )
        {
            this.iD = iD;
            this.presentPic = presentPic;
            this.presentPrice = presentPrice;
            this.isFancy = isFancy;

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
        ///礼物图片
        ///</summary>
        public string PresentPic
        {
            get { return presentPic; }
            set { presentPic = value; }
        }

        ///<summary>
        ///礼物价格
        ///</summary>
        public decimal PresentPrice
        {
            get { return presentPrice; }
            set { presentPrice = value; }
        }

        ///<summary>
        ///
        ///</summary>
        public int IsFancy
        {
            get { return isFancy; }
            set { isFancy = value; }
        }

        #endregion

    }
}
