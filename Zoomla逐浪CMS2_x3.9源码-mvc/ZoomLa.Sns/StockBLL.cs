using System;
using System.Collections.Generic;
using System.Text;
using BDUModel;
using BDULogic;
using System.Data;

namespace BDUBLL
{
    public class StockBLL
    {
        #region 创建一个股票
        /// <summary>
        /// 创建一个股票
        /// </summary>
        /// <param name="ss"></param>
        /// <returns></returns>
        public Guid CreatStock(SystemStock ss)
        {
            return StockLogic.CreatStock(ss);
        }
        #endregion

        #region 获取所有股票
        /// <summary>
        /// 获取所有股票
        /// </summary>
        /// <returns></returns>
        public DataTable GetSystemStockList()
        {
            return StockLogic.GetSystemStockList();
        }
        #endregion

        #region 修改股票价格
        /// <summary>
        ///修改股票价格 
        /// </summary>
        /// <param name="stockID"></param>
        /// <param name="stockPrice"></param>
        public void UpdataStockPrice(Guid stockID, decimal stockPrice)
        {
            StockLogic.UpdataStockPrice(stockID, stockPrice);
        }
        #endregion

        #region 购买股票
        /// <summary>
        /// 购买股票
        /// </summary>
        /// <param name="us"></param>
        /// <returns></returns>
        public Guid UserBuyStock(UserStock us)
        {
            return StockLogic.UserBuyStock(us);
        }
        #endregion

        #region 获取用户股票
        /// <summary>
        ///修改股票价格 
        /// </summary>
        /// <param name="stockID"></param>
        /// <param name="stockPrice"></param>
        public DataTable GetUserStock(Guid userID)
        {
            return StockLogic.GetUserStock(userID);
        }
        #endregion

        #region 卖股票
        /// <summary>
        /// 卖股票
        /// </summary>
        /// <param name="id"></param>
        public void SellStock(Guid id, int count)
        {
            StockLogic.SellStock(id, count);
        }
        #endregion
    }
}
