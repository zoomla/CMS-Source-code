using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using BDULogic;

namespace BDUBLL
{
    public class Astro_BLL
    {
        #region 今日明日运程
        /// <summary>
        /// 今日明日运程
        /// </summary>
        /// <param name="stat">今天0，明天1</param>
        /// <param name="astro">星座名称</param>
        /// <returns></returns>
        public DataTable GetDay(int stat, string astro)
        {
            return Astro_Logic.GetDay(stat, astro);
        }
        #endregion

        #region 周运程
        /// <summary>
        /// 周运程
        /// </summary>
        /// <param name="astro">星座名称</param>
        /// <returns></returns>
        public DataTable GetWeek(string astro)
        {
            return Astro_Logic.GetWeek(astro);
        }
        #endregion

        #region 月运程
        /// <summary>
        /// 月运程
        /// </summary>
        /// <param name="astro">星座名称</param>
        /// <returns></returns>
        public DataTable GetMonth(string astro)
        {
            return Astro_Logic.GetMonth(astro);
        }
        #endregion

        #region 年运程
        /// <summary>
        /// 年运程
        /// </summary>
        /// <param name="astro">星座名称</param>
        /// <returns></returns>
        public DataTable GetYear(string astro)
        {
            return Astro_Logic.GetYear(astro);
        }
        #endregion
    }
}
