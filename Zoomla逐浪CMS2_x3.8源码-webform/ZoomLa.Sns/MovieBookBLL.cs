using System;
using System.Collections.Generic;
using System.Text;
using BDULogic;
using BDUModel;

namespace BDUBLL
{
   public  class MovieBookBLL
    {

       
        #region 查询电影书籍
        /// <summary>
        /// 查询电影书籍
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
       public List<SystemMovieBook> GetMovieBookSearch(string name, PagePagination page)
       {
           return MovieBookLogic.GetMovieBookSearch(name, page);
       }
        #endregion
   }
}
