using System;
using System.Collections.Generic;
using System.Text;
using BDULogic;
using BDUModel;

namespace BDUBLL
{
    public class Memo_BLL
    {
        #region ��ӱ���¼����
        /// <summary>
        /// ��ӱ���¼����
        /// </summary>
        /// <param name="memo"></param>
        public void Add(UserMemo memo)
        {
            Memo_Logic.Add(memo);
        }
        #endregion

        #region �޸ı���¼
        /// <summary>
        /// �޸ı���¼
        /// </summary>
        /// <param name="memo"></param>
        public void Update(UserMemo memo)
        {
            Memo_Logic.Update(memo);
        }
        #endregion

        #region �����û�ID��ѯ���б���¼
        /// <summary>
        ///  �����û�ID��ѯ���б���¼
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<UserMemo> GetMemoList(int id, PagePagination page)
        {
            return Memo_Logic.GetMemoList(id, page);
        }
        #endregion

        #region ����ID��ѯ��������¼
        /// <summary>
        /// ����ID��ѯ��������¼
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UserMemo GetMemo(Guid id)
        {
            return Memo_Logic.GetMemo(id);
        }
        #endregion

        #region ɾ������¼
        /// <summary>
        /// ɾ������¼
        /// </summary>
        /// <param name="id"></param>
        public  void del(Guid id)
        {
            Memo_Logic.del(id);
        }
        #endregion

    }
}
