using System;
using System.Collections.Generic;
using System.Text;
using BDUModel;
using BDULogic;

namespace BDUBLL
{
    /// <summary>
    /// PicCateg 
    /// 相册逻辑层
    /// </summary>
    public class PicCateg_BLL
    {
        #region 创建相册
        /// <summary>
        /// 创建相册
        /// </summary>
        /// <param name="pc"></param>
        /// <returns></returns>
        public Guid Add(PicCateg pc, int userid, int category)
        {
            if (category == 0)
            {
                if (pc.PicCategUserID.CompareTo(userid) == 0)
                    return PicCateg_Logic.Add(pc);
                else
                    return Guid.Empty;
            }
            else
            {
                return Guid.Empty;
            }
        }
        #endregion

        #region 所有相册
        /// <summary>
        /// 所有相册
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public List<PicCateg> GetAllPic(PagePagination page)
        {
            return PicCateg_Logic.GetAllPic(page);
        }
        #endregion

        #region 修改相册信息
        /// <summary>
        /// 修改相册信息
        /// </summary>
        /// <param name="pc"></param>
        /// <returns></returns>
        public Guid Update(PicCateg pc)
        {
            return PicCateg_Logic.Update(pc);
        }
        #endregion

        #region 删除相册
        /// <summary>
        /// 删除相册
        /// </summary>
        /// <param name="id"></param>
        public void Del(Guid id)
        {
            PicTure_BLL ture = new PicTure_BLL();
            ture.Del(id);
            PicCateg_Logic.Del(id);
        }
        #endregion

        #region 族群相册列表
        /// <summary>
        /// 族群ID
        /// </summary>
        /// <param name="gropid"></param>
        /// <returns></returns>
        public List<PicCateg> GetGropPicCategList(int visitorID, Guid gropid)
        {
            return null;

        }
        #endregion

        #region 相册列表
        /// </summary>
       /// 相册列表
       /// </summary>
       /// <param name="visitorID">访问者ID</param>
       /// <param name="intervieweeID">受访问用户ID</param>
        /// <param name="page">分页</param>
       /// <returns>返回相册列表</returns>
        public List<PicCateg> GetPicCategList(int visitorID,int intervieweeID,PagePagination page)
        {
            if (visitorID == intervieweeID)
                return PicCateg_Logic.GetMyPicCategList(intervieweeID, page);
            else
            {
                UserfriendBLL user = new UserfriendBLL();
                if (user.CheckUserfriendByIDandID(intervieweeID, visitorID))
                    return PicCateg_Logic.GetPicCategList(2, intervieweeID, page);
                else
                    return PicCateg_Logic.GetPicCategList(1, intervieweeID, page);
            }
        }
        #endregion

        #region 设置相册首页相片
        /// <summary>
        /// 设置相册首页相片
        /// </summary>
        /// <param name="PicID">相片ID</param>
        /// <param name="CategID">相册ID</param>
        public void CategFirstPic(Guid PicID, Guid CategID)
        {
            PicCateg_Logic.CategFirstPic(PicID, CategID);
        }
        #endregion

        #region 查询单个相册信息
        /// <summary>
        /// 查询单个相册信息
        /// </summary>
        /// <param name="categid">相册ID</param>
        /// <returns></returns>
        public PicCateg GetPicCateg(Guid categid)
        {
            return PicCateg_Logic.GetPicCateg(categid);
        }
        #endregion


    }
}
