using System;
using System.Collections.Generic;
using System.Text;
using BDULogic;
using BDUModel;

namespace BDUBLL
{
    public class MoneyFlux_BLL
    {

        #region 添加现金流量信息
        /// <summary>
        /// 添加现金流量信息
        /// </summary>
        /// <param name="flux"></param>
        public void Add(MoneyFlux flux)
        {
            MoneyFlux_Logic.Add(flux);
        }
        #endregion

        #region 根据用户ID查询用户现金流量
        /// <summary>
        /// 根据用户ID查询用户现金流量
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public List<MoneyFlux> GetAllMoneyFlux(Guid userid, PagePagination page)
        {
                return MoneyFlux_Logic.GetAllMoneyFlux(userid,page);

        }
        #endregion

        #region 根据用户ID查询流入流出兑换信息
        /// <summary>
        ///  根据用户ID查询流入流出兑换信息
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public string[] GetAll(Guid userid)
        {
            return MoneyFlux_Logic.GetAll(userid);
        }
        #endregion

        #region 根据用户编号和类型编号查询总值
        /// <summary>
        /// 根据用户编号和类型编号查询总值
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="typeid"></param>
        /// <returns></returns>
        public int GetTypeMoney(Guid userid, MoneyFluxType typeid)
        {
            return MoneyFlux_Logic.GetTypeMoney(userid,typeid );
        }

        #endregion

        #region 根据用户编号,类型编号和日期查询总值
        /// <summary>
        /// 根据用户编号,类型编号和日期查询总值
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="typeid"></param>
        /// <returns></returns>
        public int GetTypeMoney(Guid userid, MoneyFluxType typeid, DateTime starttime, DateTime endtime)
        {
            return MoneyFlux_Logic.GetTypeMoney(userid,typeid,starttime,endtime );
        }

        #endregion
    }
}
