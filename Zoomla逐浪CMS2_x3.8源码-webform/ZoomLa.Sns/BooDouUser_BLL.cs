using System;
using System.Collections.Generic;
using System.Text;
using BDUModel;
using BDULogic;

namespace BDUBLL
{
    public class BooDouUser_BLL
    {
        #region ����û����������¼���Ϣ
        /// <summary>
        /// ����û����������¼���Ϣ
        /// </summary>
        /// <param name="bduser"></param>
        public void Add(BooDouUser bduser)
        {
            BooDouUser��Logic.Add(bduser);
        }
        #endregion

        #region �����û�ID��ѯ����û����һ��ʹ�ò���������ʱ��
        /// <summary>
        /// �����û�ID��ѯ����û����һ��ʹ�ò���������ʱ��
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DateTime GetTime(Guid id)
        {
            return BooDouUser��Logic.GetTime(id);
        }
        #endregion
    }
}
