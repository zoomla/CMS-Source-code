using System;
using System.Collections.Generic;
using System.Text;
using BDULogic;
using BDUModel;

namespace BDUBLL
{
    public class UserEmailValidateBLL
    {
        #region 验证激活号码是否存在
        /// <summary>
        /// 验证激活号码是否存在,存在返回TRUE,不存在返回FLASE
        /// </summary>
        /// <param name="Validate"></param>
        /// <returns></returns>
        public bool CheckUserEmailValidateByValidate(string Validate)
        {
            return UserEmailValidateLogic.CheckUserEmailValidateByValidate(Validate);
        }
        #endregion

        #region 添加验证用户
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="validate"></param>
        /// <returns></returns>
        public Guid InsertUserEmailValidate(UserEmailValidate validate)
        {
            return UserEmailValidateLogic.InsertUserEmailValidate(validate);
        }
        #endregion

        #region 根据激活码读取临时表数据
        /// <summary>
        /// 根据激活码读取临时表数据
        /// </summary>
        /// <param name="Validate"></param>
        /// <returns></returns>
       public UserEmailValidate GetUserEmailValidateByValidate(string Validate)
        {
            return UserEmailValidateLogic.GetUserEmailValidateByValidate(Validate);
        }
        #endregion

        #region 根据ID删除相应临时表数据
        /// <summary>
        /// 根据ID删除相应临时表数据
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public bool DelUserEmailValidateById(Guid ID)
        {
            return UserEmailValidateLogic.DelUserEmailValidateById(ID);
        }
        #endregion
    }
}
