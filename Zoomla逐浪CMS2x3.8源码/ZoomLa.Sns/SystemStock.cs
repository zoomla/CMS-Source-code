/*---------------------------------------------------------------
// Copyright (C) 2008  
// 版权所有
//
// 文件名： SystemStock.cs
// 文件功能描述：定义数据表SystemStock的业务实体
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
    ///SystemStock业务实体
    /// </summary>
    [Serializable]
    public class SystemStock
    {
        #region 字段定义

        ///<summary>
        ///
        ///</summary>
        private Guid iD;

        ///<summary>
        ///股票代码
        ///</summary>
        private string stockCode = String.Empty;

        ///<summary>
        ///股票名称
        ///</summary>
        private string stockName = String.Empty;

        ///<summary>
        ///股票价格
        ///</summary>
        private decimal stockPrice;


        #endregion

        #region 构造函数

        ///<summary>
        ///
        ///</summary>
        public SystemStock()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public SystemStock
        (
            Guid iD,
            string stockCode,
            string stockName,
            decimal stockPrice
        )
        {
            this.iD = iD;
            this.stockCode = stockCode;
            this.stockName = stockName;
            this.stockPrice = stockPrice;

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
        ///股票代码
        ///</summary>
        public string StockCode
        {
            get { return stockCode; }
            set { stockCode = value; }
        }

        ///<summary>
        ///股票名称
        ///</summary>
        public string StockName
        {
            get { return stockName; }
            set { stockName = value; }
        }

        ///<summary>
        ///股票价格
        ///</summary>
        public decimal StockPrice
        {
            get { return stockPrice; }
            set { stockPrice = value; }
        }

        #endregion

    }
}
