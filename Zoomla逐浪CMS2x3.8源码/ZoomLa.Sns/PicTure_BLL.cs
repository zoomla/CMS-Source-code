using System;
using System.Collections.Generic;
using System.Text;
using BDUModel;
using BDULogic;

namespace BDUBLL
{
    /// <summary>
    /// PicTure
    /// 相片逻辑层
    /// </summary>
    public class PicTure_BLL
    {
        #region 添加相片
        /// <summary>
        /// 添加相片
        /// </summary>
        /// <param name="ture"></param>
        /// <returns></returns>
        public Guid Add(PicTure ture)
        {
            if (Convert.ToInt32(GetCount(ture.PicCategID)) < 50)
            {
                return PicTure_Logic.Add(ture);
            }
            else
                return new Guid ();
        }
        #endregion

        #region 修改相片信息
        /// <summary>
        /// 修改相片信息
        /// </summary>
        /// <param name="picture"></param>
        /// <returns></returns>
        public Guid Update(PicTure picture)
        {
            return PicTure_Logic.Update(picture);
        }
        #endregion

        #region 按相片ID删除相片
        /// <summary>
        /// 按相片ID删除相片
        /// </summary>
        /// <param name="id">相片ID</param>
        public void DelPic(Guid id)
        {
             PicTure_Logic.DelPic(id);
        }
        #endregion

        #region 按相册ID删除相片
        /// <summary>
        /// 按相册ID删除相片
        /// </summary>
        /// <param name="id">相册ID</param>
        public void Del(Guid id)
        {
            PicTure_Logic.Del(id);
        }
        #endregion

        #region 相片列表
        /// <summary>
        /// 相片列表
        /// </summary>
        /// <param name="CategID"></param>
        /// <param name="page">分页</param>
        /// <returns></returns>
        public List<PicTure> GetPicTureList(Guid CategID,PagePagination page)
        {
            return PicTure_Logic.GetPicTureList(CategID,page);
        }
        #endregion

        #region 查看相片
        /// <summary>
        /// 查看单张相片
        /// </summary>
        /// <param name="picid">相片编号</param>
        /// <returns></returns>
        public PicTure GetPic(Guid picid)
        {
            return PicTure_Logic.GetPic(picid);
        }
        #endregion

        #region 根据相册ID查询相片总数
        /// <summary>
        /// 根据相册ID查询相片总数
        /// </summary>
        /// <param name="Categid"></param>
        /// <returns></returns>
        public string GetCount(Guid Categid)
        {
            return PicTure_Logic.GetCount(Categid);
        }
        #endregion
    }
}
