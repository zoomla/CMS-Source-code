using System;
using System.Collections.Generic;
using System.Text;
using BDUModel;
using System.Data.SqlClient;
using System.Data;
using ZoomLa.SQLDAL;

namespace BDULogic
{
    public class StockLogic
    {
        #region 创建一个股票
        /// <summary>
        /// 创建一个股票
        /// </summary>
        /// <param name="ss"></param>
        /// <returns></returns>
        public static Guid CreatStock(SystemStock ss)
        {
            try
            {
                ss.ID = Guid.NewGuid();
                string sql = @"INSERT INTO [SystemStock] (
	                            [ID],
	                            [StockCode],
	                            [StockName],
	                            [StockPrice]
                            ) VALUES (
	                            @ID,
	                            @StockCode,
	                            @StockName,
	                            @StockPrice
                            )
                            ";
                SqlParameter[] parameter =
                    {
                        new SqlParameter("ID",ss.ID),
                         new SqlParameter("StockCode",ss.StockCode),
                         new SqlParameter("StockName",ss.StockName),
                         new SqlParameter("StockPrice",ss.StockPrice)
                    };
                SqlHelper.ExecuteNonQuery(CommandType.Text, sql, parameter);
                return ss.ID;
            }
            catch
            {
                throw;
            }

        }
        #endregion

        #region 获取所有股票
        /// <summary>
        /// 获取所有股票
        /// </summary>
        /// <returns></returns>
        public static DataTable GetSystemStockList()
        {
            try
            {
                string sql = @"select * from SystemStock";
                SqlParameter[] parameter =
                    {
                        new SqlParameter("ID","")
                    };
                return SqlHelper.ExecuteTable(CommandType.Text, sql, parameter);

            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 修改股票价格
        /// <summary>
        ///修改股票价格 
        /// </summary>
        /// <param name="stockID"></param>
        /// <param name="stockPrice"></param>
        public static void UpdataStockPrice(Guid stockID, decimal stockPrice)
        {
            try
            {
                string sql = @"UPDATE [SystemStock] SET
	                            [StockPrice] = @StockPrice
                            WHERE
	                            [ID] = @ID";
                SqlParameter[] parameter =
                    {
                        new SqlParameter("ID",stockID),
                         new SqlParameter("StockPrice",stockPrice)
                    };
                 SqlHelper.ExecuteTable(CommandType.Text, sql, parameter);
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 购买股票
        /// <summary>
        /// 购买股票
        /// </summary>
        /// <param name="us"></param>
        /// <returns></returns>
        public static Guid UserBuyStock(UserStock us)
        {
            try
            {
                us.ID = Guid.NewGuid();
                string sql = @"INSERT INTO [UserStock] (
	                                [ID],
	                                [StockID],
	                                [UserID],
	                                [BuyPrice],
	                                [CreatTime],
	                                [BuyCount]
                                ) VALUES (
	                                @ID,
	                                @StockID,
	                                @UserID,
	                                @BuyPrice,
	                                @CreatTime,
	                                @BuyCount
                                )
                                ";
                    SqlParameter[] parameter =
                    {
                        new SqlParameter("ID",us.ID),
                         new SqlParameter("StockID",us.StockID),
                        new SqlParameter("UserID",us.UserID),
                        new SqlParameter("BuyPrice",us.BuyPrice),
                        new SqlParameter("CreatTime",DateTime.Now),
                        new SqlParameter("BuyCount",us.BuyCount)
                    };
                    SqlHelper.ExecuteNonQuery(CommandType.Text, sql, parameter);
                    return us.ID;

            }

            catch
            {
                throw;
            }
        }
        #endregion

        #region 获取用户股票
        /// <summary>
        ///修改股票价格 
        /// </summary>
        /// <param name="stockID"></param>
        /// <param name="stockPrice"></param>
        public static DataTable GetUserStock(Guid userID)
        {
            try
            {
                string sql = @"
                             Delete  UserStock where BuyCount=0 or BuyCount<0;
                            SELECT UserStock.*, SystemStock.StockPrice, SystemStock.StockName, 
                                  SystemStock.StockCode
                            FROM UserStock INNER JOIN
                                  SystemStock ON UserStock.StockID = SystemStock.ID where UserID=@UserID
                              ";
                SqlParameter[] parameter =
                    {
                        new SqlParameter("UserID",userID)
                    };
               return  SqlHelper.ExecuteTable(CommandType.Text, sql, parameter);
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 卖股票
        /// <summary>
        /// 卖股票
        /// </summary>
        /// <param name="id"></param>
        public static void SellStock(Guid id,int count)
        {
            try
            {
                string sql = @"UPDATE [UserStock] SET
	                            [BuyCount] =BuyCount-@BuyCount
                            WHERE
	                            [ID] = @ID";
                SqlParameter[] parameter =
                    {
                        new SqlParameter("ID",id),
                         new SqlParameter("BuyCount",count)
                    };
                SqlHelper.ExecuteNonQuery(CommandType.Text, sql, parameter);
            }
            catch
            {
                throw;
            }
        }
        #endregion
    }
}
