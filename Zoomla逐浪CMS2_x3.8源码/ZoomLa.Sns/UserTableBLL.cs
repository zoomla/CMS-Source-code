using System;
using System.Collections.Generic;
using System.Text;
using ZoomLa.Model;
using ZoomLa.Sns.Logic;
using ZoomLa.Sns.Model;
using ZoomLa.BLL;
using System.Data;

namespace ZoomLa.Sns.BLL
{
    public class UserTableBLL
    {

        #region 初始化用户详细表信息
        /// <summary>
        /// 初始化用户详细表信息
        /// </summary>
        /// <param name="id"></param>
        public void AddMoreinfo(int id)
        {
            UserTableLogic.AddMoreinfo(id);
        }
        #endregion

        #region 根据ID查询用户详细表
        /// <summary>
        /// 根据ID查询用户详细表
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public UserMoreinfo GetMoreinfoByUserid(int UserID)
        {
            return UserTableLogic.GetMoreinfoByUserid(UserID);
        }
        #endregion

        #region 修改用户详细表
        /// <summary>
        /// 修改用户详细表
        /// </summary>
        /// <param name="um"></param>
        public bool UpdateMoreinfo(UserMoreinfo um)
        {
            B_User ut = new B_User();
            M_Uinfo uinfo = ut.GetUserBaseByuserid(um.UserID);
            uinfo.UserId = um.UserID;
            uinfo.UserFace = um.UserPic;
            ut.UpdateBase(uinfo);


            return UserTableLogic.UpdateMoreinfo(um);
        }
        #endregion

        #region 查找用户2
        /// <summary>
        /// 查找用户2
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="age1"></param>
        /// <param name="age2"></param>
        /// <param name="sex"></param>
        /// <param name="pro"></param>
        /// <param name="city"></param>
        /// <param name="marry"></param>
        /// <param name="monthin"></param>
        /// <param name="home"></param>
        /// <returns></returns>
        public List<UserMoreinfo> GetSearch2(int UserID, string age1, string age2, string sex, string pro, string city, string marry)
        {
            return UserTableLogic.GetSearch2(UserID, age1, age2, sex, pro, city, marry);
        }

        public List<UserMoreinfo> GetSearch3(int UserID, string username)
        {
            return UserTableLogic.GetSearch3(UserID, username);
        }


        #endregion

        #region 查询职业收入
        /// <summary>
        /// 查询职业收入
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="age1"></param>
        /// <param name="age2"></param>
        /// <param name="sex"></param>
        /// <param name="pro"></param>
        /// <param name="city"></param>
        /// <param name="work"></param>
        /// <param name="bachelor"></param>
        /// <param name="monthIn"></param>
        /// <returns></returns>
        public List<UserMoreinfo> GetWorkMoney(int userid, string age1, string age2, string sex, string pro, string city, string work, string bachelor, string monthIn)
        {
            return UserTableLogic.GetWorkMoney(userid, age1, age2, sex, pro, city, work, bachelor, monthIn);
        }
        #endregion

        #region 查询星座血型
        /// <summary>
        /// 查询星座血型
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="age1"></param>
        /// <param name="age2"></param>
        /// <param name="sex"></param>
        /// <param name="pro"></param>
        /// <param name="city"></param>
        /// <param name="blood"></param>
        /// <param name="constellation"></param>
        /// <returns></returns>
        public List<UserMoreinfo> GetConstellation(int userid, string age1, string age2, string sex, string pro, string city, string blood, string constellation)
        {
            return UserTableLogic.GetConstellation(userid, age1, age2, sex, pro, city, blood, constellation);
        }

        #endregion

        #region 学校搜索
        /// <summary>
        /// 学校搜索
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="sex"></param>
        /// <param name="pro"></param>
        /// <param name="city"></param>
        /// <returns></returns>
        public List<UserMoreinfo> GetSchool(int userid, string sex, string pro, string city, string school)
        {
            return UserTableLogic.GetSchool(userid, sex, pro, city, school);
        }
        #endregion

        /// <summary>
        /// WAP根据学校搜索好友
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="sex"></param>
        /// <param name="city"></param>
        /// <param name="school"></param>
        /// <returns></returns>
        public List<UserMoreinfo> GetSchools(int userid, string sex, string city, string school) {
            
            return UserTableLogic.GetSchools(userid, sex, city, school);
        }

        #region 高级搜索
        /// <summary>
        /// 在高级搜索中满足条件的用户部份信息
        /// </summary>
        /// <param name="wherex">查询条件</param>
        /// <returns></returns>
        public DataTable GetSeachByCondition(string wherex)
        {
            return UserTableLogic.GetSeachByCondition(wherex);
        }
        #endregion
        #region 搜索出好友显示
        /// <summary>
        /// 搜索出好友显示
        /// </summary>
        /// <param name="wherex"></param>
        /// <returns></returns>
        public static DataTable GetUsersInfo(string wherex)
        {
            return UserTableLogic.GetUsersInfo(wherex);
        }
        #endregion
        /// <summary>
        /// 改头像的方法
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <param name="UserFace">头像地址</param>
        /// <returns></returns>
        public bool UpdateUserFace(int UserID, string UserFace) 
        { 
            B_User ut = new B_User();
            return false;
        }
    }
}
