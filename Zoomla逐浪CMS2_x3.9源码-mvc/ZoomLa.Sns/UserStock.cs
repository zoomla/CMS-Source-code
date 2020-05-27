/*---------------------------------------------------------------
// Copyright (C) 2008  
// 版权所有
//
// 文件名： UserStock.cs
// 文件功能描述：定义数据表UserStock的业务实体
//
// 创建标识：Owner(2008-10-25) 
//
// 修改标识：
// 修改描述：
----------------------------------------------------------------*/
using System;


namespace BDUModel
{
    /// <summary>
    ///UserStock业务实体
    /// </summary>
    [Serializable]
    public class UserStock
    {
        #region 字段定义

        ///<summary>
        ///
        ///</summary>
        private Guid iD;

        ///<summary>
        ///股票编号
        ///</summary>
        private Guid stockID = Guid.Empty;

        ///<summary>
        ///用户编号
        ///</summary>
        private Guid userID = Guid.Empty;

        ///<summary>
        ///购买时价格
        ///</summary>
        private decimal buyPrice;

        ///<summary>
        ///购买时间
        ///</summary>
        private DateTime creatTime;

        private int buyCount = 0;

        public int BuyCount
        {
            get { return buyCount; }
            set { buyCount = value; }
        }

        #endregion

        #region 构造函数

        ///<summary>
        ///
        ///</summary>
        public UserStock()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public UserStock
        (
            Guid iD,
            Guid stockID,
            Guid userID,
            decimal buyPrice,
            DateTime creatTime
        )
        {
            this.iD = iD;
            this.stockID = stockID;
            this.userID = userID;
            this.buyPrice = buyPrice;
            this.creatTime = creatTime;

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
        ///股票编号
        ///</summary>
        public Guid StockID
        {
            get { return stockID; }
            set { stockID = value; }
        }

        ///<summary>
        ///用户编号
        ///</summary>
        public Guid UserID
        {
            get { return userID; }
            set { userID = value; }
        }

        ///<summary>
        ///购买时价格
        ///</summary>
        public decimal BuyPrice
        {
            get { return buyPrice; }
            set { buyPrice = value; }
        }

        ///<summary>
        ///购买时间
        ///</summary>
        public DateTime CreatTime
        {
            get { return creatTime; }
            set { creatTime = value; }
        }

        #endregion

    }
}
