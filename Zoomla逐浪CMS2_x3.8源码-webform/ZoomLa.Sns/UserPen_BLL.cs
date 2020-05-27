using System;
using System.Collections.Generic;
using System.Text;
using BDULogic;
using BDUModel;

namespace BDUBLL
{
    public class UserPen_BLL
    {
        #region 添加用户宠物信息
        /// <summary>
        /// 添加用户宠物信息
        /// </summary>
        /// <param name="pen"></param>
        public void Add(UserPen pen)
        {
            UserPen_Logic.Add(pen);
        }
        #endregion

        #region 修改宠物名字
        /// <summary>
        /// 修改宠物名字
        /// </summary>
        /// <param name="id">主人ID</param>
        /// <param name="name">修改的名字</param>
        public void UpdateName(Guid id, string name)
        {
            UserPen_Logic.UpdateName(id, name);
        }
        #endregion

        #region 修改宠物等级
        /// <summary>
        /// 修改宠物等级
        /// </summary>
        /// <param name="pen"></param>
        public void UpdateFoster(UserPen pen)
        {
            UserPen_Logic.UpdateFoster(pen);
        }
        #endregion

        #region 转让宠物
        /// <summary>
        /// 转让宠物
        /// </summary>
        /// <param name="pen"></param>
        public void UpdateUserPen(UserPen pen, Guid BuyUserID)
        {
            UserPen_Logic.UpdateUserPen(pen, BuyUserID);
        }

        #endregion

        #region 根据主人ID查询宠物信息
        /// <summary>
        /// 根据主人ID查询宠物信息
        /// </summary>
        /// <param name="id">主人ID</param>
        /// <returns></returns>
        public UserPen GetUserPen(Guid id)
        {
            return UserPen_Logic.GetUserPen(id);
        }
        #endregion

        #region 获取我的汽车
        /// <summary>
        ///获取我的汽车 
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public System.Data.DataTable GetCarByUserID(Guid userID)
        {
            return UserPen_Logic.GetCarByUserID(userID);
        }
        #endregion

        #region 根据ID查询汽车信息
        /// <summary>
        /// 根据ID查询汽车信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        public  UserPen GetUserCar(Guid id)
        {
            return UserPen_Logic.GetUserCar(id);
        }
        #endregion
    }

}
