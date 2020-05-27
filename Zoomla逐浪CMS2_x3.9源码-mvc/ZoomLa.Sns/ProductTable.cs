/*---------------------------------------------------------------
// Copyright (C) 2008  
// 版权所有
//
// 文件名： ProductTable.cs
// 文件功能描述：定义数据表ProductTable的业务实体
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
    ///ProductTable业务实体
    /// </summary>
    [Serializable]
    public class ProductTable
    {
        #region 字段定义

        ///<summary>
        ///编号
        ///</summary>
        private Guid iD = Guid.Empty;

        ///<summary>
        ///名称
        ///</summary>
        private string productName = String.Empty;

        ///<summary>
        ///类型ID
        ///</summary>
        private Guid typeID = Guid.Empty;

        ///<summary>
        ///内容简介
        ///</summary>
        private string productContent = String.Empty;

        ///<summary>
        ///普通价格
        ///</summary>
        private int price;

        ///<summary>
        ///Vip价格
        ///</summary>
        private int vipPrice;

        ///<summary>
        ///产品图片
        ///</summary>
        private string productPic = String.Empty;

        ///<summary>
        ///添加时间
        ///</summary>
        private DateTime addtime;

        ///<summary>
        ///类型名称
        ///</summary>
        private string name = String.Empty;


        #endregion

        #region 构造函数

        ///<summary>
        ///
        ///</summary>
        public ProductTable()
        {
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
        ///名称
        ///</summary>
        public string ProductName
        {
            get { return productName; }
            set { productName = value; }
        }

        ///<summary>
        ///类型ID
        ///</summary>
        public Guid TypeID
        {
            get { return typeID; }
            set { typeID = value; }
        }

        ///<summary>
        ///内容简介
        ///</summary>
        public string ProductContent
        {
            get { return productContent; }
            set { productContent = value; }
        }

        ///<summary>
        ///普通价格
        ///</summary>
        public int Price
        {
            get { return price; }
            set { price = value; }
        }

        ///<summary>
        ///Vip价格
        ///</summary>
        public int VipPrice
        {
            get { return vipPrice; }
            set { vipPrice = value; }
        }

        ///<summary>
        ///产品图片
        ///</summary>
        public string ProductPic
        {
            get { return productPic; }
            set { productPic = value; }
        }

        ///<summary>
        ///添加时间
        ///</summary>
        public DateTime Addtime
        {
            get { return addtime; }
            set { addtime = value; }
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
