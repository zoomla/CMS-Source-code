using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using ZoomLa.SQLDAL;

namespace ZoomLa.Sns
{
   public class BlogStyleTableBLL
    {
       #region 添加空间模板
       /// <summary>
       /// 添加空间模板
       /// </summary>
       /// <param name="bst"></param>
       public void InsertBlogStyle(BlogStyleTable bst)
       {
           BlogStyleTableLogic.InsertBlogStyle(bst);
       }
       #endregion

       #region 修改空间模板
       /// <summary>
       /// 修改空间模板
       /// </summary>
       /// <param name="bst"></param>
       public void UpdateBlogStyle(BlogStyleTable bst)
       {
           BlogStyleTableLogic.UpdateBlogStyle(bst);
       }
       #endregion

       #region 删除空间模板
       /// <summary>
       /// 删除空间模板
       /// </summary>
       /// <param name="list"></param>
       public void DelBlogStyle(string list)
       {
           BlogStyleTableLogic.DelBlogStyle(list);
       }
       #endregion

       #region 根据ID读取模板信息
       /// <summary>
       /// 根据ID读取模板信息
       /// </summary>
       /// <param name="ID"></param>
       /// <returns></returns>
       public BlogStyleTable GetBlogStyleByID(int ID)
       {
          return BlogStyleTableLogic.GetBlogStyleByID(ID);
       }
       #endregion

       public DataTable SelByState(int state)
       {
           string sql = @"select * from ZL_Sns_BlogStyleTable where 1=1";
           if (state != 2)
           {
               sql += " and StyleState=@state";
           }
           SqlParameter[] parameter = { new SqlParameter("state", state) };
           return SqlHelper.ExecuteTable(CommandType.Text, sql, parameter);
       }
       #region 批量修改状态
       /// <summary>
       /// 批量修改状态
       /// </summary>
       /// <param name="state"></param>
       /// <param name="list"></param>
       public void UpdateBatchState(int state, string list)
       {
           BlogStyleTableLogic.UpdateBatchState(state, list);
       }
       #endregion
   }
}
