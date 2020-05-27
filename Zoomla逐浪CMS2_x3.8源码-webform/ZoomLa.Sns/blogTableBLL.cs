using System;
using System.Collections.Generic;
using System.Text;

namespace ZoomLa.Sns
{
   public class blogTableBLL
    {
       #region 申请开通空间
       /// <summary>
       /// 申请开通空间
       /// </summary>
       /// <param name="bt"></param>
       public void InsertBlog(blogTable bt)
       {
           blogTableLogic.InsertBlog(bt);
       }
       #endregion

       #region 修改空间
       /// <summary>
       /// 修改空间
       /// </summary>
       /// <param name="bt"></param>
       public void UpdateBlogtable(blogTable bt)
       {
           blogTableLogic.UpdateBlogtable(bt);
       }
       #endregion

       #region 删除空间
       /// <summary>
       /// 删除空间
       /// </summary>
       /// <param name="ID"></param>
       public void DelBlogtable(int ID)
       {
           blogTableLogic.DelBlogtable(ID);
       }
       #endregion

       #region 读取用户空间信息
       /// <summary>
       /// 读取用户空间信息
       /// </summary>
       /// <param name="UserID"></param>
       /// <returns></returns>
       public blogTable GetUserBlog(int UserID)
       {
          return blogTableLogic.GetUserBlog(UserID);
       }
       #endregion

       #region 批量修改状态
       /// <summary>
       /// 批量修改状态
       /// </summary>
       /// <param name="list"></param>
       /// <param name="state"></param>
       /// <param name="type">C为推荐,关闭状态 D为删除 A为审核</param>
       public void BatchUpdateState(string list,int state,string type)
       {
           blogTableLogic.BatchUpdateState(list, state, type);
       }
       #endregion

       #region 根据类型读取空间列表
       /// <summary>
       /// 根据类型读取空间列表
       /// </summary>
       /// <param name="state">0待审核 1通过 2为关闭</param>
       /// <returns></returns>
       public List<blogTable> GetBlogTableByState(int state)
       {
           return blogTableLogic.GetBlogTableByState(state);
       }
       #endregion

       #region 修改点击数
       /// <summary>
       /// 修改点击数
       /// </summary>
       /// <param name="ID"></param>
       public void UpdateHits(int ID)
       {
           blogTableLogic.UpdateHits(ID);
       }
       #endregion
   }
}
