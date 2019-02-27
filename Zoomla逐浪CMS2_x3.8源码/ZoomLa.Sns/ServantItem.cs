/*---------------------------------------------------------------
// Copyright (C) 2008  
// 版权所有
//
// 文件名： ServantItem.cs
// 文件功能描述：定义数据表ServantItem的业务实体
//
// 创建标识：Owner(2008-10-14) 
//
// 修改标识：
// 修改描述：
----------------------------------------------------------------*/
using System;


namespace BDUModel
{
    /// <summary>
    ///ServantItem业务实体
    /// </summary>
    [Serializable]
    public class ServantItem
    {
        #region 字段定义

        ///<summary>
        ///
        ///</summary>
        private Guid iD = Guid.Empty;

        ///<summary>
        ///项目名称
        ///</summary>
        private string itemName = String.Empty;

        ///<summary>
        ///项目介绍
        ///</summary>
        private string itemSynopsis = String.Empty;

        ///<summary>
        ///项目开销
        ///</summary>
        private decimal itemMoney;

        ///<summary>
        ///项目图片
        ///</summary>
        private string itemImage = String.Empty;


        #endregion

        #region 构造函数

        ///<summary>
        ///
        ///</summary>
        public ServantItem()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public ServantItem
        (
            Guid iD,
            string itemName,
            string itemSynopsis,
            decimal itemMoney,
            string itemImage
        )
        {
            this.iD = iD;
            this.itemName = itemName;
            this.itemSynopsis = itemSynopsis;
            this.itemMoney = itemMoney;
            this.itemImage = itemImage;

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
        ///项目名称
        ///</summary>
        public string ItemName
        {
            get { return itemName; }
            set { itemName = value; }
        }

        ///<summary>
        ///项目介绍
        ///</summary>
        public string ItemSynopsis
        {
            get { return itemSynopsis; }
            set { itemSynopsis = value; }
        }

        ///<summary>
        ///项目开销
        ///</summary>
        public decimal ItemMoney
        {
            get { return itemMoney; }
            set { itemMoney = value; }
        }

        ///<summary>
        ///项目图片
        ///</summary>
        public string ItemImage
        {
            get { return itemImage; }
            set { itemImage = value; }
        }

        #endregion

    }
}
