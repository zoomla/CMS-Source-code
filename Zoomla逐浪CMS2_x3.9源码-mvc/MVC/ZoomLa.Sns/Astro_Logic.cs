using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ZoomLa.SQLDAL;

namespace BDULogic
{
    public class Astro_Logic
    {
        #region 今日明日运程
        /// <summary>
        /// 今日明日运程
        /// </summary>
        /// <param name="stat">今天0，明天1</param>
        /// <param name="astro">星座名称</param>
        /// <returns></returns>
        public static DataTable GetDay(int stat, string astro)
        {
            try
            {
                string SQLstr = @"select * from ZL_Sns_GuessLuck where Xzmc=@Xzmc and Stat=@Stat";

                SqlParameter[] sp ={ new SqlParameter("@Xzmc", astro), new SqlParameter("@Stat", stat) };

                return SqlHelper.ExecuteTable(CommandType.Text, SQLstr, sp);
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 周运程
        /// <summary>
        /// 周运程
        /// </summary>
        /// <param name="astro">星座名称</param>
        /// <returns></returns>
        public static DataTable GetWeek(string astro)
        {
            try
            {
                string SQLstr = @"select * from ZL_Sns_GuessLuckWeek where Xzmc=@Xzmc ";

                SqlParameter[] sp ={ new SqlParameter("@Xzmc", astro) };

                return SqlHelper.ExecuteTable(CommandType.Text, SQLstr, sp);
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 月运程
        /// <summary>
        /// 月运程
        /// </summary>
        /// <param name="astro">星座名称</param>
        /// <returns></returns>
        public static DataTable GetMonth(string astro)
        {
            try
            {
                string SQLstr = @"select * from ZL_Sns_GuessLuckMonth where Xzmc=@Xzmc ";

                SqlParameter[] sp ={ new SqlParameter("@Xzmc", astro) };

                return SqlHelper.ExecuteTable(CommandType.Text, SQLstr, sp);
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 年运程
        /// <summary>
        /// 年运程
        /// </summary>
        /// <param name="astro">星座名称</param>
        /// <returns></returns>
        public static DataTable GetYear(string astro)
        {
            try
            {
                string SQLstr = @"select * from ZL_Sns_GuessLuckYear where Xzmc=@Xzmc ";

                SqlParameter[] sp ={ new SqlParameter("@Xzmc", astro) };

                return SqlHelper.ExecuteTable(CommandType.Text, SQLstr, sp);
            }
            catch
            {
                throw;
            }
        }
        #endregion
    }
}
