using System;
using System.Collections.Generic;
using System.Text;
using BDUModel;
using BDULogic;

namespace BDUBLL
{
    /// <summary>
    /// PicTure
    /// ��Ƭ�߼���
    /// </summary>
    public class PicTure_BLL
    {
        #region �����Ƭ
        /// <summary>
        /// �����Ƭ
        /// </summary>
        /// <param name="ture"></param>
        /// <returns></returns>
        public Guid Add(PicTure ture)
        {
            if (Convert.ToInt32(GetCount(ture.PicCategID)) < 50)
            {
                return PicTure_Logic.Add(ture);
            }
            else
                return new Guid ();
        }
        #endregion

        #region �޸���Ƭ��Ϣ
        /// <summary>
        /// �޸���Ƭ��Ϣ
        /// </summary>
        /// <param name="picture"></param>
        /// <returns></returns>
        public Guid Update(PicTure picture)
        {
            return PicTure_Logic.Update(picture);
        }
        #endregion

        #region ����ƬIDɾ����Ƭ
        /// <summary>
        /// ����ƬIDɾ����Ƭ
        /// </summary>
        /// <param name="id">��ƬID</param>
        public void DelPic(Guid id)
        {
             PicTure_Logic.DelPic(id);
        }
        #endregion

        #region �����IDɾ����Ƭ
        /// <summary>
        /// �����IDɾ����Ƭ
        /// </summary>
        /// <param name="id">���ID</param>
        public void Del(Guid id)
        {
            PicTure_Logic.Del(id);
        }
        #endregion

        #region ��Ƭ�б�
        /// <summary>
        /// ��Ƭ�б�
        /// </summary>
        /// <param name="CategID"></param>
        /// <param name="page">��ҳ</param>
        /// <returns></returns>
        public List<PicTure> GetPicTureList(Guid CategID,PagePagination page)
        {
            return PicTure_Logic.GetPicTureList(CategID,page);
        }
        #endregion

        #region �鿴��Ƭ
        /// <summary>
        /// �鿴������Ƭ
        /// </summary>
        /// <param name="picid">��Ƭ���</param>
        /// <returns></returns>
        public PicTure GetPic(Guid picid)
        {
            return PicTure_Logic.GetPic(picid);
        }
        #endregion

        #region �������ID��ѯ��Ƭ����
        /// <summary>
        /// �������ID��ѯ��Ƭ����
        /// </summary>
        /// <param name="Categid"></param>
        /// <returns></returns>
        public string GetCount(Guid Categid)
        {
            return PicTure_Logic.GetCount(Categid);
        }
        #endregion
    }
}
