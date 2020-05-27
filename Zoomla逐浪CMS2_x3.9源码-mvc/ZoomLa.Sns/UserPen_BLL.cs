using System;
using System.Collections.Generic;
using System.Text;
using BDULogic;
using BDUModel;

namespace BDUBLL
{
    public class UserPen_BLL
    {
        #region ����û�������Ϣ
        /// <summary>
        /// ����û�������Ϣ
        /// </summary>
        /// <param name="pen"></param>
        public void Add(UserPen pen)
        {
            UserPen_Logic.Add(pen);
        }
        #endregion

        #region �޸ĳ�������
        /// <summary>
        /// �޸ĳ�������
        /// </summary>
        /// <param name="id">����ID</param>
        /// <param name="name">�޸ĵ�����</param>
        public void UpdateName(Guid id, string name)
        {
            UserPen_Logic.UpdateName(id, name);
        }
        #endregion

        #region �޸ĳ���ȼ�
        /// <summary>
        /// �޸ĳ���ȼ�
        /// </summary>
        /// <param name="pen"></param>
        public void UpdateFoster(UserPen pen)
        {
            UserPen_Logic.UpdateFoster(pen);
        }
        #endregion

        #region ת�ó���
        /// <summary>
        /// ת�ó���
        /// </summary>
        /// <param name="pen"></param>
        public void UpdateUserPen(UserPen pen, Guid BuyUserID)
        {
            UserPen_Logic.UpdateUserPen(pen, BuyUserID);
        }

        #endregion

        #region ��������ID��ѯ������Ϣ
        /// <summary>
        /// ��������ID��ѯ������Ϣ
        /// </summary>
        /// <param name="id">����ID</param>
        /// <returns></returns>
        public UserPen GetUserPen(Guid id)
        {
            return UserPen_Logic.GetUserPen(id);
        }
        #endregion

        #region ��ȡ�ҵ�����
        /// <summary>
        ///��ȡ�ҵ����� 
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public System.Data.DataTable GetCarByUserID(Guid userID)
        {
            return UserPen_Logic.GetCarByUserID(userID);
        }
        #endregion

        #region ����ID��ѯ������Ϣ
        /// <summary>
        /// ����ID��ѯ������Ϣ
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        public  UserPen GetUserCar(Guid id)
        {
            return UserPen_Logic.GetUserCar(id);
        }
        #endregion
    }

}
