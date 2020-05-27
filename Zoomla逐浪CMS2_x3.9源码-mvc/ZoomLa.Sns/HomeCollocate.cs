/*---------------------------------------------------------------
// Copyright (C) 2008  
// 版权所有
//
// 文件名： HomeCollocate.cs
// 文件功能描述：定义数据表HomeCollocate的业务实体
//
// 创建标识：Owner(2008-11-12) 
//
// 修改标识：
// 修改描述：
----------------------------------------------------------------*/
using System;


namespace FHModel
{
    /// <summary>
    ///HomeCollocate业务实体
    /// </summary>
    [Serializable]
    public class HomeCollocate
    {
        #region 字段定义

        ///<summary>
        ///
        ///</summary>
        private Guid iD = Guid.Empty;

        ///<summary>
        ///用户ID
        ///</summary>
        private int userID ;

        ///<summary>
        ///购买ID
        ///</summary>
        private Guid shopID = Guid.Empty;

        ///<summary>
        ///左边距
        ///</summary>
        private int cLeft;

        ///<summary>
        ///层数
        ///</summary>
        private int cIndexZ;

        ///<summary>
        ///上边距
        ///</summary>
        private int cTop;

        ///<summary>
        ///布置时间
        ///</summary>
        private DateTime addtime;

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
        public HomeCollocate()
        {
        }

        ///<summary>
        ///
        ///</summary>
        

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
        ///用户ID
        ///</summary>
        public int UserID
        {
            get { return userID; }
            set { userID = value; }
        }

        ///<summary>
        ///购买ID
        ///</summary>
        public Guid ShopID
        {
            get { return shopID; }
            set { shopID = value; }
        }

        ///<summary>
        ///左边距
        ///</summary>
        public int CLeft
        {
            get { return cLeft; }
            set { cLeft = value; }
        }

        ///<summary>
        ///层数
        ///</summary>
        public int CIndexZ
        {
            get { return cIndexZ; }
            set { cIndexZ = value; }
        }

        ///<summary>
        ///上边距
        ///</summary>
        public int CTop
        {
            get { return cTop; }
            set { cTop = value; }
        }

        ///<summary>
        ///布置时间
        ///</summary>
        public DateTime Addtime
        {
            get { return addtime; }
            set { addtime = value; }
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
