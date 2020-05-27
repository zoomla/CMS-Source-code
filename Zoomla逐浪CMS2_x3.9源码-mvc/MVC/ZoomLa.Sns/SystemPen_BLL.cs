using System;
using System.Collections.Generic;
using System.Text;
using BDUModel;
using BDULogic;

namespace BDUBLL
{
    public class SystemPen_BLL
    {
        #region ��ӳ���
        /// <summary>
        /// ��ӳ���
        /// </summary>
        /// <param name="pen"></param>
        public void Add(SystemPen pen)
        {
            SystemPen_Logic.Add(pen);
        }
        #endregion

        #region �޸ĳ�����Ϣ
        /// <summary>
        /// �޸ĳ�����Ϣ
        /// </summary>
        /// <param name="pen"></param>
        public  void Update(SystemPen pen)
        {
            SystemPen_Logic.Update(pen);
        }
        #endregion

        #region ������ѯ������Ϣ
        /// <summary>
        /// ������ѯ������Ϣ
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SystemPen GetPen(Guid id)
        {
            return SystemPen_Logic.GetPen(id);
        }
        #endregion

        #region ��ѯ������Ϣ�б�
        /// <summary>
        /// ��ѯ������Ϣ�б�
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public List<SystemPen> GetAllSystemPen(int state,PagePagination page)
        {
            return SystemPen_Logic.GetAllSystemPen(state,page);
        }
        #endregion
    }
}
