using System;
using System.Collections.Generic;
using System.Text;
using ZoomLa.Sns.Model;
using ZoomLa.Sns.Logic;

namespace ZoomLa.Sns.BLL
{
    public class BookBLL
    {
        #region 添加书籍
        /// <summary>
        /// 添加书籍
        /// </summary>
        /// <param name="bt"></param>
        /// <returns></returns>
        public Guid InsertBooktable(BookTable bt)
        {
            return BookLogic.InsertBooktable(bt);
        }
        #endregion

        #region 显示所有书籍
        /// <summary>
        ///  显示所有书籍
        /// </summary>
        /// <returns></returns>
        public List<BookTable> GetBooktable( int state)
        {
            return BookLogic.GetBooktable( state);
        }
        #endregion

         #region 按名称搜索
        /// <summary>
        /// 按名称搜索
        /// </summary>
        /// <param name="page"></param>
        /// <param name="title"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public List<BookTable> GetBookLike(string title,int state)
        {
            return BookLogic.GetBookLike(title, state);
        }
         #endregion

        #region 显示Top数书籍
        /// <summary>
        /// 显示Top数书籍
        /// </summary>
        /// <param name="Topx"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public List<BookTable> GetBooktableTop(int Topx, int state)
        {
            return BookLogic.GetBooktableTop(Topx, state);
        }
        #endregion

        #region 根据ID查询表
        /// <summary>
        /// 根据ID查询表
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public BookTable GetBooktableByID(Guid ID)
        {
            return BookLogic.GetBooktableByID(ID);
        }
        #endregion

         #region 删除书籍
        /// <summary>
        /// 删除书籍
        /// </summary>
        /// <param name="id"></param>
        public void del(Guid id)
        {
            BookLogic.del(id);
        }
        #endregion
    }
}
