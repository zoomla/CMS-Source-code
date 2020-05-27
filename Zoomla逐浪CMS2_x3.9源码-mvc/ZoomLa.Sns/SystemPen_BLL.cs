using System;
using System.Collections.Generic;
using System.Text;
using BDUModel;
using BDULogic;

namespace BDUBLL
{
    public class SystemPen_BLL
    {
        #region 添加宠物
        /// <summary>
        /// 添加宠物
        /// </summary>
        /// <param name="pen"></param>
        public void Add(SystemPen pen)
        {
            SystemPen_Logic.Add(pen);
        }
        #endregion

        #region 修改宠物信息
        /// <summary>
        /// 修改宠物信息
        /// </summary>
        /// <param name="pen"></param>
        public  void Update(SystemPen pen)
        {
            SystemPen_Logic.Update(pen);
        }
        #endregion

        #region 单条查询宠物信息
        /// <summary>
        /// 单条查询宠物信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SystemPen GetPen(Guid id)
        {
            return SystemPen_Logic.GetPen(id);
        }
        #endregion

        #region 查询宠物信息列表
        /// <summary>
        /// 查询宠物信息列表
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
