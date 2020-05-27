using System;
using System.Collections.Generic;
using System.Text;
using BDUModel;
using BDULogic;

namespace BDUBLL
{
    public class SystemUser_BLL
    {
        #region ���ϵͳ�����˻�
        /// <summary>
        /// ���ϵͳ�����˻�
        /// </summary>
        /// <param name="user"></param>
        public void Add(SystemUser user)
        {
            SystemUser_Logic.Add(user);
        }
        #endregion

        #region �޸�����
        /// <summary>
        /// �޸�����
        /// </summary>
        /// <param name="user"></param>
        public void Update(SystemUser user)
        {
            SystemUser_Logic.Update(user);
        }
        #endregion

        #region �����û��������ѯ�û�
        /// <summary>
        /// �����û��������ѯ�û�
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SystemUser GetUser(SystemUser user)
        {
            return SystemUser_Logic.GetUser(user);
        }
        #endregion

    }
}
