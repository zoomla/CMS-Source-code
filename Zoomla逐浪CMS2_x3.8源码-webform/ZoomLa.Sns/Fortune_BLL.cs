using System;
using System.Collections.Generic;
using System.Text;
using BDUModel;
using BDULogic;

namespace BDUBLL
{
    public class Fortune_BLL
    {
        #region 添加用户财富信息
        /// <summary>
        /// 添加用户财富信息
        /// </summary>
        /// <param name="fortune"></param>
        public void Add(Guid id)
        {
            Fortune_Logic.Add(id);
            MoneyFlux_BLL fluxbll = new MoneyFlux_BLL();
            MoneyFlux flux = new MoneyFlux();
            Fortune fortune = new Fortune();
            fortune=Fortune_Logic.GetFortune(id);
            flux.UserID = id;
            flux.Occurtype = 1;
            flux.OccurTime = DateTime.Now;
            //flux.
            
        }
        #endregion

        #region 修改消耗精神财富
        /// <summary>
        /// 修改消耗精神财富
        /// </summary>
        /// <param name="Userid">用户ID</param>
        /// <param name="money">现金</param>
        public void UpdateConsumeMoney(Guid Userid, decimal money)
        {
            Fortune_Logic.UpdateConsumeMoney(Userid, money);
        }
        #endregion

        #region 修改精神财富
        /// <summary>
        /// 修改精神财富
        /// </summary>
        /// <param name="Userid">用户ID</param>
        /// <param name="money">现金</param>
        public void UpdateEnergyMoney(Guid Userid, decimal money)
        {
            Fortune_Logic.UpdateEnergyMoney(Userid, money);
        }
        #endregion

        #region 修改现金
        /// <summary>
        /// 修改现金
        /// </summary>
        /// <param name="Userid">用户ID</param>
        /// <param name="money">现金</param>
        public void UpdateCash(Guid Userid, decimal money)
        {
            Fortune_Logic.UpdateCash(Userid, money);
        }
        #endregion

        #region 修改兜币
        /// <summary>
        /// 修改兜币
        /// </summary>
        /// <param name="Userid">用户ID</param>
        /// <param name="money">兜币</param>
        public void UpdateBooDouMoney(Guid Userid, decimal money)
        {
            Fortune_Logic.UpdateBooDouMoney(Userid, money);
        }
        #endregion

        #region 根据用户ID查询财富情况
        /// <summary>
        /// 根据用户ID查询财富情况
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public  Fortune GetFortune(Guid userid)
        {
            return Fortune_Logic.GetFortune(userid);
        }
        #endregion

        #region 修改财富信息
        /// <summary>
        /// 修改财富信息
        /// </summary>
        /// <param name="fortune"></param>
        public void UpdateMoney(Fortune fortune)
        {
            Fortune_Logic.UpdateMoney(fortune);
        }
        #endregion

        #region 所有用户财富排行榜
        /// <summary>
        /// 所有用户财富排行榜
        /// </summary>
        /// <param name="fortunetype"></param>
        /// <returns></returns>
        public List<UserTable> GetAllFortuneOrder(int fortunetype, PagePagination page)
        {
            return Fortune_Logic.GetAllFortuneOrder(fortunetype, page);
        }
        #endregion

        #region 查询好友用户的排行榜
        /// <summary>
        /// 查询好友用户的排行榜
        /// </summary>
        /// <param name="fortunetype"></param>
        /// <param name="hostid"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public List<UserTable> GetAllFortuneOrder(int fortunetype, Guid hostid, PagePagination page)
        {
            return Fortune_Logic.GetAllFortuneOrder(fortunetype,hostid, page);
        }
        #endregion
    }
}
