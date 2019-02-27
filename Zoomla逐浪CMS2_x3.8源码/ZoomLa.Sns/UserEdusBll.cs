using System;
using System.Collections.Generic;
using System.Text;
using BDULogic;
using BDUModel;

namespace BDUBLL
{
    public class UserEdusBll
    {
        #region 添加信息
        /// <summary>
        /// 添加信息
        /// </summary>
        /// <param name="ue"></param>
        /// <returns></returns>
        public Guid InsertUseredu(UserEducationWork ue)
        {
            return UserEdusLogic.InsertUseredu(ue);
            
        }
        #endregion

        #region 修改信息
        /// <summary>
        /// 添加信息
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public void Updateedu(UserEducationWork ue)
        {
             UserEdusLogic.Updateedu(ue);
        }
        #endregion

        #region 删除信息
        /// <summary>
        /// 添加信息
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public void Deledu(Guid ID)
        {
            UserEdusLogic.Deledu(ID);
        }
        #endregion

        #region 根据用户ID查询信息
        /// <summary>
        /// 根据用户ID查询信息
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="Flage"></param>
        /// <returns></returns>
        public List<UserEducationWork> GetUsereduByUserID(Guid UserID, int Flage)
        {
            return UserEdusLogic.GetUsereduByUserID(UserID, Flage);
        }
        #endregion
    }
}
