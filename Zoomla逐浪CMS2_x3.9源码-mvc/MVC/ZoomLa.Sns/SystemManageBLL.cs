using System;
using System.Collections.Generic;
using System.Text;
using BDULogic;
using BDUModel;

namespace BDUBLL
{
    public class SystemManageBLL
    {
        #region 创建布兜世界的模块
        /// <summary>
        /// 创建布兜世界的模块
        /// </summary>
        /// <param name="bdModule"></param>
        /// <returns></returns>
        public Guid CreatSystemBDModule(SystemBDModule bdModule)
        {
            return SystemManageLogic.CreatSystemBDModule(bdModule);
        }
        #endregion

        #region 获取所有模块
        /// <summary>
        /// 获取所有模块
        /// </summary>
        /// <returns></returns>
        public List<SystemBDModule> GetSystemBDModule()
        {
            return SystemManageLogic.GetSystemBDModule();
        }
        #endregion

        #region 用户添加组建
        /// <summary>
        /// 用户添加组建
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="mondelID"></param>
        public Guid AddMondel(Guid userID, Guid mondelID)
        {
            return SystemManageLogic.AddMondel(userID, mondelID);
        }
        #endregion

        #region 通过用户编号和模块编号判断用户是否已经添加了模块
        /// <summary>
        /// 通过用户编号和模块编号判断用户是否已经添加了模块
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="mondelID"></param>
        /// <returns></returns>
        public bool ExitUserHasMondel(Guid userID, Guid mondelID)
        {
            return SystemManageLogic.ExitUserHasMondel(userID, mondelID);
        }
        #endregion

        #region 通过模块编号获取模块
        /// <summary>
        /// 通过模块编号获取模块
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SystemBDModule GetSystemBDModuleByID(Guid id)
        {
            return SystemManageLogic.GetSystemBDModuleByID(id);
        }
        #endregion

        #region  通过用户获取现有模块
        /// <summary>
        /// 通过用户获取现有模块
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<User_R_Module> GetUserModuleByUserID(Guid userID)
        {
            return SystemManageLogic.GetUserModuleByUserID(userID);
        }
        #endregion

        #region 通过用户模块编号获取模块
        /// <summary>
        /// 通过用户模块编号获取模块
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public User_R_Module GetUserRModuleByID(Guid id)
        {
            return SystemManageLogic.GetUserRModuleByID(id);
        }
        #endregion

           #region 删除用户选择的模块
        /// <summary>
        /// 删除用户选择的模块
        /// </summary>
        /// <param name="id"></param>
        public  void DeleteUserMondelByID(Guid id)
        {
            SystemManageLogic.DeleteUserMondelByID(id);
        }
           #endregion
    }
}
