using System;
using System.Collections.Generic;
using System.Text;
using BDUModel;
using BDULogic;

namespace BDUBLL
{
    public class SystemBooDouChance_BLL
    {
        #region ��Ӳ��������¼�
        /// <summary>
        /// ��Ӳ��������¼�
        /// </summary>
        /// <param name="boodou"></param>
        public void Add(SystemBooDouChance boodou)
        {
            SystemBooDouChance_Logic.Add(boodou);
        }
        #endregion

        #region �޸Ĳ��������¼�
        /// <summary>
        /// �޸Ĳ��������¼�
        /// </summary>
        /// <param name="boodou"></param>
        public void Update(SystemBooDouChance boodou)
        {
            SystemBooDouChance_Logic.Update(boodou);
        }
        #endregion

        #region ɾ�����������¼�
        /// <summary>
        /// ɾ�������¼�
        /// </summary>
        /// <param name="id"></param>
        public void Del(Guid id)
        {
            SystemBooDouChance_Logic.Del(id);
        }
        #endregion

        #region ��ѯ�������������¼�
        /// <summary>
        /// ��ѯ�������������¼�
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SystemBooDouChance GetBooDouChance(Guid id)
        {
            return SystemBooDouChance_Logic.GetBooDouChance(id);
        }
        #endregion

        #region ��ѯ���в��������¼�
        /// <summary>
        /// ��ѯ���в��������¼�
        /// </summary>
        /// <returns></returns>
        public List<SystemBooDouChance> GetAllBooDouChance()
        {
            return SystemBooDouChance_Logic.GetAllBooDouChance();
        }
        #endregion
    }
}
