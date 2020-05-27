using System;
using System.Collections.Generic;
using System.Text;
using BDULogic;
using BDUModel;

namespace BDUBLL
{
    public class MoneyFlux_BLL
    {

        #region ����ֽ�������Ϣ
        /// <summary>
        /// ����ֽ�������Ϣ
        /// </summary>
        /// <param name="flux"></param>
        public void Add(MoneyFlux flux)
        {
            MoneyFlux_Logic.Add(flux);
        }
        #endregion

        #region �����û�ID��ѯ�û��ֽ�����
        /// <summary>
        /// �����û�ID��ѯ�û��ֽ�����
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public List<MoneyFlux> GetAllMoneyFlux(Guid userid, PagePagination page)
        {
                return MoneyFlux_Logic.GetAllMoneyFlux(userid,page);

        }
        #endregion

        #region �����û�ID��ѯ���������һ���Ϣ
        /// <summary>
        ///  �����û�ID��ѯ���������һ���Ϣ
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public string[] GetAll(Guid userid)
        {
            return MoneyFlux_Logic.GetAll(userid);
        }
        #endregion

        #region �����û���ź����ͱ�Ų�ѯ��ֵ
        /// <summary>
        /// �����û���ź����ͱ�Ų�ѯ��ֵ
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="typeid"></param>
        /// <returns></returns>
        public int GetTypeMoney(Guid userid, MoneyFluxType typeid)
        {
            return MoneyFlux_Logic.GetTypeMoney(userid,typeid );
        }

        #endregion

        #region �����û����,���ͱ�ź����ڲ�ѯ��ֵ
        /// <summary>
        /// �����û����,���ͱ�ź����ڲ�ѯ��ֵ
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="typeid"></param>
        /// <returns></returns>
        public int GetTypeMoney(Guid userid, MoneyFluxType typeid, DateTime starttime, DateTime endtime)
        {
            return MoneyFlux_Logic.GetTypeMoney(userid,typeid,starttime,endtime );
        }

        #endregion
    }
}
