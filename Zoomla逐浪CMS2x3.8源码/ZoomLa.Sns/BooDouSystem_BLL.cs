using System;
using System.Collections.Generic;
using System.Text;
using BDUModel;
using BDULogic;

namespace BDUBLL
{
    public class BooDouSystem_BLL
    {

        #region 添加值
        /// <summary>
        /// 添加值
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

        #region 修改值
        /// <summary>
        /// 修改值
        /// </summary>
        /// <param name="boodou"></param>
        public void Update(BooDouSystem boodou)
        {
            BooDouSystem_Logic.Update(boodou);
        }
        #endregion

        #region 查询所有系统设置
        /// <summary>
        /// 查询所有系统设置
        /// </summary>
        /// <returns></returns>
        public List<BooDouSystem> GetSystem(int minindex, int maxindex)
        {
            return BooDouSystem_Logic.GetSystem(minindex,maxindex);
        }
        #endregion
        
        #region 根据指定的几个ID查询系统设置
        /// <summary>
        /// 根据指定的几个ID查询系统设置
        /// </summary>
        /// <returns></returns>
        public List<BooDouSystem> GetSystem(string id)
        {
            return BooDouSystem_Logic.GetSystem(id);
        }
        #endregion

        #region 根据ID查询
        /// <summary>
        /// 根据ID查询
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
