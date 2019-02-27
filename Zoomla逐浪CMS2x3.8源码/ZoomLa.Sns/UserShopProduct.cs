/*---------------------------------------------------------------
// Copyright (C) 2008  
// 版权所有
//
// 文件名： UserShopProduct.cs
// 文件功能描述：定义数据表UserShopProduct的业务实体
//
// 创建标识：Owner(2008-11-11) 
//
// 修改标识：
// 修改描述：
----------------------------------------------------------------*/
using System;


namespace FHModel
{
    /// <summary>
    ///UserShopProduct业务实体
    /// </summary>
    [Serializable]
    public class UserShopProduct
    {
        #region 字段定义

        ///<summary>
        ///ID
        ///</summary>
        private Guid iD = Guid.Empty;

        ///<summary>
        ///用户ID
        ///</summary>
        private int userID ;

        ///<summary>
        ///商品ID
        ///</summary>
        private Guid productID = Guid.Empty;

        ///<summary>
        ///商品使用天数
        ///</summary>
        private int shopDay;

        ///<summary>
        ///商品购买时间
        ///</summary>
        private DateTime addtime;

        ///<summary>
        ///赠送人ID
        ///</summary>
        private int largessID ;

        ///<summary>
        ///赠送留言
        ///</summary>
        private string largessContent = String.Empty;

        ///<summary>
        ///名称
        ///</summary>
        private string productName = String.Empty;

        ///<summary>
        ///产品图片
        ///</summary>
        private string productPic = String.Empty;



        #endregion

        #region 构造函数

        ///<summary>
        ///
        ///</summary>
        public UserShopProduct()
        {
        }

        ///<summary>
        ///
        ///</summary>
        

        #endregion

        #region 属性定义

        ///<summary>
        ///ID
        ///</summary>
        public Guid ID
        {
            get { return iD; }
            set { iD = value; }
        }

        ///<summary>
        ///用户ID
        ///</summary>
        public int UserID
        {
            get { return userID; }
            set { userID = value; }
        }

        ///<summary>
        ///商品ID
        ///</summary>
        public Guid ProductID
        {
            get { return productID; }
            set { productID = value; }
        }

        ///<summary>
        ///商品使用天数
        ///</summary>
        public int ShopDay
        {
            get { return shopDay; }
            set { shopDay = value; }
        }

        ///<summary>
        ///商品购买时间
        ///</summary>
        public DateTime Addtime
        {
            get { return addtime; }
            set { addtime = value; }
        }

        ///<summary>
        ///赠送人ID
        ///</summary>
        public int LargessID
        {
            get { return largessID; }
            set { largessID = value; }
        }

        ///<summary>
        ///赠送留言
        ///</summary>
        public string LargessContent
        {
            get { return largessContent; }
            set { largessContent = value; }
        }

        ///<summary>
        ///名称
        ///</summary>
        public string ProductName
        {
            get { return productName; }
            set { productName = value; }
        }

        ///<summary>
        ///产品图片
        ///</summary>
        public string ProductPic
        {
            get { return productPic; }
            set { productPic = value; }
        }

        #endregion

    }
}
