using System;
using System.Collections.Generic;
using System.Text;
using BDUModel;
using BDULogic;

namespace BDUBLL
{
    public class I4GiveU_BLL
    {
        #region ��������Ϣ
        /// <summary>
        /// ��������Ϣ
        /// </summary>
        /// <param name="give"></param>
        public Guid  Add(I4GiveU give)
        {
            return I4GiveU_Logic.Add(give);
        }
        #endregion

        #region ��ѯ���������Ϣ
        /// <summary>
        /// ��ѯ���������Ϣ
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public I4GiveU GetOne(Guid id)
        {
            return I4GiveU_Logic.GetOne(id);
        }
        #endregion

        #region ��ѯ�û�������¼�
        /// <summary>
        /// ��ѯ�û�������¼�
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<I4GiveU> GetI4GiveUList(Guid id, PagePagination page)
        {
            return I4GiveU_Logic.GetI4GiveUList(id,page);
        }
        #endregion

        #region �����û���ѯ���������������
        /// <summary>
        /// �����û���ѯ���������������
        /// </summary>
        /// <param name="id"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public List<UserTable> GetConFess(Guid id, PagePagination page)
        {
            return I4GiveU_Logic.GetConFess(id, page);
        }
        #endregion

         #region �û��ĺ�����������ڵ���
        /// <summary>
        /// �û��ĺ�����������ڵ���
        /// </summary>
        /// <param name="id"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public List<UserTable> GetFriendNewConfess(Guid id, PagePagination page)
        {
            return I4GiveU_Logic.GetFriendNewConfess(id, page);
        }
        #endregion
    }
}
