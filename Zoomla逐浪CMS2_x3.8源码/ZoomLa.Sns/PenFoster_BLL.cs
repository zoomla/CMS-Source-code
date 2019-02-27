using System;
using System.Collections.Generic;
using System.Text;
using BDUModel;
using BDULogic;

namespace BDUBLL
{
    public class PenFoster_BLL
    {
        #region 添加宠物项目信息
        /// <summary>
        /// 添加宠物项目信息
        /// </summary>
        /// <param name="foster"></param>
        public void Add(PenFoster foster)
        {
            PenFoster_Logic.Add(foster);
        }
        #endregion

        #region 修改宠物项目信息
        /// <summary>
        /// 修改宠物项目信息
        /// </summary>
        /// <param name="foster"></param>
        public void Update(PenFoster foster)
        {
            PenFoster_Logic.Update(foster);
        }
        #endregion

        #region 删除项目信息
        /// <summary>
        /// 删除项目信息
        /// </summary>
        /// <param name="id"></param>
        public void Del(Guid id)
        {
            PenFoster_Logic.Del(id);
        }
        #endregion

        #region 根据ID查找项目信息
        /// <summary>
        /// 根据ID查找项目信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PenFoster GetPenFoster(Guid id)
        {
            return PenFoster_Logic.GetPenFoster(id);
        }
        #endregion

        #region 查询所有项目
        /// <summary>
        /// 查询所有项目
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
