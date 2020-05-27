using System;
using System.Collections.Generic;
using System.Text;
using BDUModel;
using BDULogic;

namespace BDUBLL
{
    public class CommendCommentOn_BLL
    {
        #region ����Ƽ�����
        /// <summary>
        /// ����Ƽ�����
        /// </summary>
        /// <param name="ccon"></param>
        public void Add(CommendCommentOn ccon)
        {
            CommendCommentOn_Logic.Add(ccon);
        }
        #endregion

        #region ɾ���Ƽ�����
        /// <summary>
        /// ɾ���Ƽ�����
        /// </summary>
        /// <param name="id"></param>
        public void Del(Guid id)
        {
            CommendCommentOn_Logic.Del(id);
        }
        #endregion

        #region �����Ƽ�ID��ѯ����
        /// <summary>
        /// �����Ƽ�ID��ѯ����
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<CommendCommentOn> GetCommendCommentOn(Guid id,PagePagination page)
        {
            return CommendCommentOn_Logic.GetCommendCommentOn(id,page );
        }
        #endregion
    }
}
