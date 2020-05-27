using System;
using System.Collections.Generic;
using System.Text;
using BDUModel;
using BDULogic;
using System.Data;

namespace BDUBLL
{
    public class PresentBLL
    {
        #region 获取系统礼物
        /// <summary>
        /// 获取系统礼物
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<SystemPresent> GetQuestionByUserID(int isFancy, PagePagination page)
        {
            return PresentLogic.GetQuestionByUserID(isFancy, page);
        }
        #endregion

        #region 赠送礼物
        /// <summary>
        /// 赠送礼物
        /// </summary>
        /// <param name="sp"></param>
        /// <returns></returns>
        public Guid LargessPresent(UserPresent sp)
        {
            return PresentLogic.LargessPresent(sp);
        }
        #endregion

        #region 获取我的礼物
        /// <summary>
        ///获取我的礼物 
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public DataTable GetUserPresentListByUserID(Guid userID)
        {
            return PresentLogic.GetUserPresentListByUserID(userID);
        }
        #endregion

    }
}
