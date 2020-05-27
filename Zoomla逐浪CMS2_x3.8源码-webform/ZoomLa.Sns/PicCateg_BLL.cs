using System;
using System.Collections.Generic;
using System.Text;
using BDUModel;
using BDULogic;

namespace BDUBLL
{
    /// <summary>
    /// PicCateg 
    /// ����߼���
    /// </summary>
    public class PicCateg_BLL
    {
        #region �������
        /// <summary>
        /// �������
        /// </summary>
        /// <param name="pc"></param>
        /// <returns></returns>
        public Guid Add(PicCateg pc, int userid, int category)
        {
            if (category == 0)
            {
                if (pc.PicCategUserID.CompareTo(userid) == 0)
                    return PicCateg_Logic.Add(pc);
                else
                    return Guid.Empty;
            }
            else
            {
                return Guid.Empty;
            }
        }
        #endregion

        #region �������
        /// <summary>
        /// �������
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public List<PicCateg> GetAllPic(PagePagination page)
        {
            return PicCateg_Logic.GetAllPic(page);
        }
        #endregion

        #region �޸������Ϣ
        /// <summary>
        /// �޸������Ϣ
        /// </summary>
        /// <param name="pc"></param>
        /// <returns></returns>
        public Guid Update(PicCateg pc)
        {
            return PicCateg_Logic.Update(pc);
        }
        #endregion

        #region ɾ�����
        /// <summary>
        /// ɾ�����
        /// </summary>
        /// <param name="id"></param>
        public void Del(Guid id)
        {
            PicTure_BLL ture = new PicTure_BLL();
            ture.Del(id);
            PicCateg_Logic.Del(id);
        }
        #endregion

        #region ��Ⱥ����б�
        /// <summary>
        /// ��ȺID
        /// </summary>
        /// <param name="gropid"></param>
        /// <returns></returns>
        public List<PicCateg> GetGropPicCategList(int visitorID, Guid gropid)
        {
            return null;

        }
        #endregion

        #region ����б�
        /// </summary>
       /// ����б�
       /// </summary>
       /// <param name="visitorID">������ID</param>
       /// <param name="intervieweeID">�ܷ����û�ID</param>
        /// <param name="page">��ҳ</param>
       /// <returns>��������б�</returns>
        public List<PicCateg> GetPicCategList(int visitorID,int intervieweeID,PagePagination page)
        {
            if (visitorID == intervieweeID)
                return PicCateg_Logic.GetMyPicCategList(intervieweeID, page);
            else
            {
                UserfriendBLL user = new UserfriendBLL();
                if (user.CheckUserfriendByIDandID(intervieweeID, visitorID))
                    return PicCateg_Logic.GetPicCategList(2, intervieweeID, page);
                else
                    return PicCateg_Logic.GetPicCategList(1, intervieweeID, page);
            }
        }
        #endregion

        #region ���������ҳ��Ƭ
        /// <summary>
        /// ���������ҳ��Ƭ
        /// </summary>
        /// <param name="PicID">��ƬID</param>
        /// <param name="CategID">���ID</param>
        public void CategFirstPic(Guid PicID, Guid CategID)
        {
            PicCateg_Logic.CategFirstPic(PicID, CategID);
        }
        #endregion

        #region ��ѯ���������Ϣ
        /// <summary>
        /// ��ѯ���������Ϣ
        /// </summary>
        /// <param name="categid">���ID</param>
        /// <returns></returns>
        public PicCateg GetPicCateg(Guid categid)
        {
            return PicCateg_Logic.GetPicCateg(categid);
        }
        #endregion


    }
}
