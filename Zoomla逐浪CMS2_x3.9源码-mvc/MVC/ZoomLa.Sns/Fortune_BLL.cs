using System;
using System.Collections.Generic;
using System.Text;
using BDUModel;
using BDULogic;

namespace BDUBLL
{
    public class Fortune_BLL
    {
        #region ����û��Ƹ���Ϣ
        /// <summary>
        /// ����û��Ƹ���Ϣ
        /// </summary>
        /// <param name="fortune"></param>
        public void Add(Guid id)
        {
            Fortune_Logic.Add(id);
            MoneyFlux_BLL fluxbll = new MoneyFlux_BLL();
            MoneyFlux flux = new MoneyFlux();
            Fortune fortune = new Fortune();
            fortune=Fortune_Logic.GetFortune(id);
            flux.UserID = id;
            flux.Occurtype = 1;
            flux.OccurTime = DateTime.Now;
            //flux.
            
        }
        #endregion

        #region �޸����ľ���Ƹ�
        /// <summary>
        /// �޸����ľ���Ƹ�
        /// </summary>
        /// <param name="Userid">�û�ID</param>
        /// <param name="money">�ֽ�</param>
        public void UpdateConsumeMoney(Guid Userid, decimal money)
        {
            Fortune_Logic.UpdateConsumeMoney(Userid, money);
        }
        #endregion

        #region �޸ľ���Ƹ�
        /// <summary>
        /// �޸ľ���Ƹ�
        /// </summary>
        /// <param name="Userid">�û�ID</param>
        /// <param name="money">�ֽ�</param>
        public void UpdateEnergyMoney(Guid Userid, decimal money)
        {
            Fortune_Logic.UpdateEnergyMoney(Userid, money);
        }
        #endregion

        #region �޸��ֽ�
        /// <summary>
        /// �޸��ֽ�
        /// </summary>
        /// <param name="Userid">�û�ID</param>
        /// <param name="money">�ֽ�</param>
        public void UpdateCash(Guid Userid, decimal money)
        {
            Fortune_Logic.UpdateCash(Userid, money);
        }
        #endregion

        #region �޸Ķ���
        /// <summary>
        /// �޸Ķ���
        /// </summary>
        /// <param name="Userid">�û�ID</param>
        /// <param name="money">����</param>
        public void UpdateBooDouMoney(Guid Userid, decimal money)
        {
            Fortune_Logic.UpdateBooDouMoney(Userid, money);
        }
        #endregion

        #region �����û�ID��ѯ�Ƹ����
        /// <summary>
        /// �����û�ID��ѯ�Ƹ����
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public  Fortune GetFortune(Guid userid)
        {
            return Fortune_Logic.GetFortune(userid);
        }
        #endregion

        #region �޸ĲƸ���Ϣ
        /// <summary>
        /// �޸ĲƸ���Ϣ
        /// </summary>
        /// <param name="fortune"></param>
        public void UpdateMoney(Fortune fortune)
        {
            Fortune_Logic.UpdateMoney(fortune);
        }
        #endregion

        #region �����û��Ƹ����а�
        /// <summary>
        /// �����û��Ƹ����а�
        /// </summary>
        /// <param name="fortunetype"></param>
        /// <returns></returns>
        public List<UserTable> GetAllFortuneOrder(int fortunetype, PagePagination page)
        {
            return Fortune_Logic.GetAllFortuneOrder(fortunetype, page);
        }
        #endregion

        #region ��ѯ�����û������а�
        /// <summary>
        /// ��ѯ�����û������а�
        /// </summary>
        /// <param name="fortunetype"></param>
        /// <param name="hostid"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public List<UserTable> GetAllFortuneOrder(int fortunetype, Guid hostid, PagePagination page)
        {
            return Fortune_Logic.GetAllFortuneOrder(fortunetype,hostid, page);
        }
        #endregion
    }
}
