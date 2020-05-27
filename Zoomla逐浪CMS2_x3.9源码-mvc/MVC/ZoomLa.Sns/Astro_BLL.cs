using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using BDULogic;

namespace BDUBLL
{
    public class Astro_BLL
    {
        #region ���������˳�
        /// <summary>
        /// ���������˳�
        /// </summary>
        /// <param name="stat">����0������1</param>
        /// <param name="astro">��������</param>
        /// <returns></returns>
        public DataTable GetDay(int stat, string astro)
        {
            return Astro_Logic.GetDay(stat, astro);
        }
        #endregion

        #region ���˳�
        /// <summary>
        /// ���˳�
        /// </summary>
        /// <param name="astro">��������</param>
        /// <returns></returns>
        public DataTable GetWeek(string astro)
        {
            return Astro_Logic.GetWeek(astro);
        }
        #endregion

        #region ���˳�
        /// <summary>
        /// ���˳�
        /// </summary>
        /// <param name="astro">��������</param>
        /// <returns></returns>
        public DataTable GetMonth(string astro)
        {
            return Astro_Logic.GetMonth(astro);
        }
        #endregion

        #region ���˳�
        /// <summary>
        /// ���˳�
        /// </summary>
        /// <param name="astro">��������</param>
        /// <returns></returns>
        public DataTable GetYear(string astro)
        {
            return Astro_Logic.GetYear(astro);
        }
        #endregion
    }
}
