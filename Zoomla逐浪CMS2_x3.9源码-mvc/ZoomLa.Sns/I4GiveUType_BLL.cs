using System;
using System.Collections.Generic;
using System.Text;
using BDULogic;
using BDUModel;

namespace BDUBLL
{
    public class I4GiveUType_BLL
    {
        #region ���������
        /// <summary>
        /// ���������
        /// </summary>
        /// <param name="give"></param>
        public void Add(I4GiveUType give)
        {
            I4GiveUType_Logic.Add(give);

        }
        #endregion

        #region �޸�������
        /// <summary>
        /// �޸�������
        /// </summary>
        /// <param name="give"></param>
        public void Update(I4GiveUType give)
        {
            I4GiveUType_Logic.Update(give);
        }
        #endregion

        #region ɾ��������
        /// <summary>
        /// ɾ��������
        /// </summary>
        /// <param name="give"></param>
        public void Del(Guid id)
        {
            I4GiveUType_Logic.Del(id);
        }
        #endregion
    }
}
