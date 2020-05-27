using System;
using System.Collections.Generic;
using System.Text;
using BDUModel;
using BDULogic;

namespace BDUBLL
{
    public class BooDouSystem_BLL
    {

        #region ���ֵ
        /// <summary>
        /// ���ֵ
        /// </summary>
        /// <param name="boodou"></param>
        public int Add(int minindex, int maxindex)
        {
            int i = BooDouSystem_Logic.GetSystem(minindex, maxindex).Count;
            i = minindex + i + 1;
            BooDouSystem_Logic.Add(i);
            return i;
        }
        #endregion

        #region �޸�ֵ
        /// <summary>
        /// �޸�ֵ
        /// </summary>
        /// <param name="boodou"></param>
        public void Update(BooDouSystem boodou)
        {
            BooDouSystem_Logic.Update(boodou);
        }
        #endregion

        #region ��ѯ����ϵͳ����
        /// <summary>
        /// ��ѯ����ϵͳ����
        /// </summary>
        /// <returns></returns>
        public List<BooDouSystem> GetSystem(int minindex, int maxindex)
        {
            return BooDouSystem_Logic.GetSystem(minindex,maxindex);
        }
        #endregion
        
        #region ����ָ���ļ���ID��ѯϵͳ����
        /// <summary>
        /// ����ָ���ļ���ID��ѯϵͳ����
        /// </summary>
        /// <returns></returns>
        public List<BooDouSystem> GetSystem(string id)
        {
            return BooDouSystem_Logic.GetSystem(id);
        }
        #endregion

        #region ����ID��ѯ
        /// <summary>
        /// ����ID��ѯ
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BooDouSystem GetBooDou(int id)
        {
            return BooDouSystem_Logic.GetBooDou(id);
        }
        #endregion
    }
}
