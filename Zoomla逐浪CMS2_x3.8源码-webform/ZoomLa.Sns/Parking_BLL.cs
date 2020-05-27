using System;
using System.Collections.Generic;
using System.Text;

namespace ZoomLa.Sns
{
    public class Parking_BLL
    {
        #region 添加车辆信息
        /// <summary>
        /// 添加车辆信息
        /// </summary>
        /// <param name="cl"></param>
        public void AddCarList(P_CarList cl)
        {
            Parking_Logic.AddCarList(cl);
        }
        #endregion

        #region 更新车辆列表信息
        /// <summary>
        /// 更新车辆列表信息
        /// </summary>
        /// <param name="cl"></param>
        public void UpdateCarList(P_CarList cl)
        {
            Parking_Logic.UpdateCarList(cl);
        }
        #endregion

        #region 设置车辆是否出售
        /// <summary>
        /// 设置车辆是否出售
        /// </summary>
        /// <param name="id"></param>
        /// <param name="check"></param>
        public void UpdateCarListCheck(int id, int check)
        {
            Parking_Logic.UpdateCarListCheck(id, check);
        }
        #endregion

        #region 查询系统内所有车辆
        /// <summary>
        /// 查询系统内所有车辆
        /// </summary>
        /// <returns></returns>
        public List<P_CarList> GetCarList()
        {
            return Parking_Logic.GetCarList();
        }
        #endregion

        #region 查询单个车辆信息
        /// <summary>
        /// 查询单个车辆信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public P_CarList GetCar(int id)
        {
            return Parking_Logic.GetCar(id);
        }
        #endregion

        #region 购买车辆
        /// <summary>
        /// 购买车辆
        /// </summary>
        /// <param name="mcar"></param>
        public void AddMyCar(ZL_Sns_MyCar mcar)
        {
            Parking_Logic.AddMyCar(mcar);
        }
        #endregion

        #region 更新用户车辆信息
        /// <summary>
        /// 更新用户车辆信息
        /// </summary>
        /// <param name="mc"></param>
        public void UpdataMyCar(ZL_Sns_MyCar mc)
        {
            Parking_Logic.UpdataMyCar(mc);
        }
        #endregion

        #region 根据用户ID查询用户拥有车辆
        /// <summary>
        /// 根据用户ID查询用户拥有车辆
        /// </summary>
        /// <param name="P_uid"></param>
        /// <returns></returns>
        public List<ZL_Sns_MyCar> GetMyCarList(int P_uid)
        {
            return Parking_Logic.GetMyCarList(P_uid);
        }
        #endregion

        #region 根据编号ID查询用户拥有车辆信息
        /// <summary>
        /// 根据编号ID查询用户拥有车辆信息
        /// </summary>
        /// <param name="pmid"></param>
        /// <returns></returns>
        public ZL_Sns_MyCar GetMyCar(int pmid)
        {
            return Parking_Logic.GetMyCar(pmid);
        }
        #endregion

        #region 根据用户ID，车辆ID查询用户单个车辆的信息
        /// <summary>
        /// 根据用户ID，车辆ID查询用户单个车辆的信息
       /// </summary>
       /// <param name="P_uid">用户ID</param>
       /// <param name="Pid">车辆ID</param>
       /// <returns></returns>
        public ZL_Sns_MyCar GetMyCar(int P_uid, int Pid)
        {
            return Parking_Logic.GetMyCar(P_uid, Pid);
        }
        #endregion

        #region 检查用户拥有的车辆
        /// <summary>
        /// 检查用户拥有的车辆
        /// </summary>
        /// <param name="id">车辆ID</param>
        /// <param name="userid">用户ID</param>
        /// <returns>返回True该用户未拥有这种车，返回false该用户已经拥有这种车</returns>
        public bool CheckCar(int id, int userid)
        {
            return Parking_Logic.CheckCar(id, userid);
        }
        #endregion

        #region 添加用户车辆日志
        /// <summary>
        /// 添加用户车辆日志
        /// </summary>
        /// <param name="zscarlog"></param>
        public void AddCarLog(ZL_Sns_CarLog zscarlog)
        {
            Parking_Logic.AddCarLog(zscarlog);
        }
        #endregion

        #region 根据用户ID查询车辆日志
        /// <summary>
        /// 根据用户ID查询车辆日志
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<ZL_Sns_CarLog> GetUserIDCarLog(int id)
        {
            return Parking_Logic.GetUserIDCarLog(id);
        }
        #endregion

        #region 初始化用户车位信息
        /// <summary>
        /// 初始化用户车位信息
        /// </summary>
        /// <param name="id"></param>
        public void AddMyPose(int id)
        {
            //检查用户车位
            if (!CheckUserPose(id))
            {
                Parking_Logic.AddMyPose(id);
            }
        }
        #endregion

        #region 更新用户车位信息
        /// <summary>
        /// 更新用户车位信息
        /// </summary>
        /// <param name="mp"></param>
        public void UpdateMyPose(ZL_Sns_MyPose mp)
        {
            Parking_Logic.UpdateMyPose(mp);
        }
        #endregion

        #region 检查用户车位
        /// <summary>
        /// 检查用户车位
        /// </summary>
        /// <param name="userid"></param>
        /// <returns>返回true 该用户已经拥有车位，返回false 该用户没有车位</returns>
        public bool CheckUserPose(int userid)
        {
            return Parking_Logic.CheckUserPose(userid);
        }
        #endregion

        #region 根据用户ID查询用户车位
        /// <summary>
        /// 根据用户ID查询用户车位
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public ZL_Sns_MyPose GetMyPose(int userid)
        {
            return Parking_Logic.GetMyPose(userid);
        }
        #endregion

        #region 添加停放信息
        /// <summary>
        /// 添加停放信息
        /// </summary>
        /// <param name="zsr"></param>
        public int AddReport(ZL_Sns_Report zsr)
        {
            return Parking_Logic.AddReport(zsr);
        }
        #endregion

        #region 更新车辆停放信息
        /// <summary>
        /// 更新车辆停放信息
        /// </summary>
        /// <param name="zsr"></param>
        public void UpdateReport(int id)
        {
            Parking_Logic.UpdateReport(id);

        }
        #endregion

        #region 根据ID查询车辆停放信息
        /// <summary>
        /// 根据ID查询车辆停放信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public  ZL_Sns_Report GetCarReport(int id)
        {
            return Parking_Logic.GetCarReport(id);
        }
        #endregion

        #region 修改抢车位规则
        /// <summary>
        /// 修改抢车位规则
        /// </summary>
        /// <param name="cc"></param>
        public void UpdateCarConfig(string cvalue, int id)
        {
            Parking_Logic.UpdateCarConfig(cvalue, id);
        }
        #endregion

        #region 查询抢车位规则
        /// <summary>
        /// 查询抢车位规则
        /// </summary>
        /// <returns></returns>
        public List<ZL_Sns_CarConfig> GetCarConfig()
        {
            return Parking_Logic.GetCarConfig();
        }
        #endregion
    }
}
