using System;
using System.Collections.Generic;
using System.Text;
using BDUModel;
using BDULogic;

namespace BDUBLL
{
    public class PenFoster_BLL
    {
        #region ��ӳ�����Ŀ��Ϣ
        /// <summary>
        /// ��ӳ�����Ŀ��Ϣ
        /// </summary>
        /// <param name="foster"></param>
        public void Add(PenFoster foster)
        {
            PenFoster_Logic.Add(foster);
        }
        #endregion

        #region �޸ĳ�����Ŀ��Ϣ
        /// <summary>
        /// �޸ĳ�����Ŀ��Ϣ
        /// </summary>
        /// <param name="foster"></param>
        public void Update(PenFoster foster)
        {
            PenFoster_Logic.Update(foster);
        }
        #endregion

        #region ɾ����Ŀ��Ϣ
        /// <summary>
        /// ɾ����Ŀ��Ϣ
        /// </summary>
        /// <param name="id"></param>
        public void Del(Guid id)
        {
            PenFoster_Logic.Del(id);
        }
        #endregion

        #region ����ID������Ŀ��Ϣ
        /// <summary>
        /// ����ID������Ŀ��Ϣ
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PenFoster GetPenFoster(Guid id)
        {
            return PenFoster_Logic.GetPenFoster(id);
        }
        #endregion

        #region ��ѯ������Ŀ
        /// <summary>
        /// ��ѯ������Ŀ
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public List<PenFoster> GetAllPenFoster(PagePagination page)
        {
            return PenFoster_Logic.GetAllPenFoster(page);
        }
        #endregion
    }
}
