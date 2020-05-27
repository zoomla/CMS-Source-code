using System;
using System.Collections.Generic;
using System.Text;
using BDUModel;
using BDULogic;

namespace BDUBLL
{
    public class I4GiveUForgive_BLL
    {
        #region ���ԭ����Ϣ
        /// <summary>
        /// ���ԭ����Ϣ
        /// </summary>
        /// <param name="forgive"></param>
        public void Add(I4GiveUForgive forgive)
        {
            I4GiveUForgive_Logic.Add(forgive);
        }
        #endregion

        #region ��ѯ�û�ԭ�µĴ���
        /// <summary>
        /// ��ѯ�û�ԭ�µĴ���
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int GetForgiveCount(Guid id)
        {
            return I4GiveUForgive_Logic.GetForgiveCount(id);
        }
        #endregion

        #region ��ѯ�û��ĺ�������ݵ���
        /// <summary>
        /// ��ѯ�û��ĺ�������ݵ���
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<UserTable> GetFriendForgiveOrder(Guid id,PagePagination page)
        {
            return I4GiveUForgive_Logic.GetFriendForgiveOrder(id,page);
        }
        #endregion
    }
}
