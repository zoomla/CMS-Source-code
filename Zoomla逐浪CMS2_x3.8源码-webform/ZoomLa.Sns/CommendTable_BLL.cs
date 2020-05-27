using System;
using System.Collections.Generic;
using System.Text;
using BDUModel;
using BDULogic;

namespace BDUBLL
{
    public class CommendTable_BLL
    {
        #region ����Ƽ���Ϣ
        /// <summary>
        /// ����Ƽ���Ϣ
        /// </summary>
        /// <param name="ct"></param>
        public void Add(CommendTable ct)
        {
            CommendTable_Logic.Add(ct);
        }
        #endregion

        #region ɾ���Ƽ���Ϣ
        /// <summary>
        /// ɾ���Ƽ���Ϣ
        /// </summary>
        /// <param name="id"></param>
        public void Del(Guid id)
        {
            CommendTable_Logic.Del(id);
        }
        #endregion

        #region �����û�ID��ѯ�Ƽ���Ϣ
        /// <summary>
        /// �����û�ID��ѯ�Ƽ���Ϣ
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<CommendTable> GetUserCommendTable(Guid id, PagePagination page,CommendTableType ctt)
        {
            return CommendTable_Logic.GetUserCommendTable(id, page,ctt);
        }
        #endregion

        #region ����ID��ѯ�����Ƽ���Ϣ
        /// <summary>
        /// ����ID��ѯ�����Ƽ���Ϣ
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CommendTable GetCommendTable(Guid id)
        {
            return CommendTable_Logic.GetCommendTable(id);
        }
        #endregion
    }
}
