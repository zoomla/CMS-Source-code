using System;
using System.Collections.Generic;
using System.Text;
using BDUModel;
using System.Data.SqlClient;
using System.Data;
using ZoomLa.SQLDAL;

namespace BDULogic
{
    public class MovieBookLogic
    {

        #region 查询电影书籍
        /// <summary>
        /// 查询电影书籍
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static List<SystemMovieBook> GetMovieBookSearch(string name, PagePagination page)
        {
            try
            {
                string sqlGetMovieBookSearch = @"select * from ZL_Sns_SystemMovieBook where 1=1 ";
                if (name != "")
                    sqlGetMovieBookSearch += @" and BookMovieName like @name+'%'";
                if (page != null)
                    sqlGetMovieBookSearch = page.PaginationSql(sqlGetMovieBookSearch);
                SqlParameter[] parameter ={
                new SqlParameter("name",name)};
                List<SystemMovieBook> list = new List<SystemMovieBook>();
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sqlGetMovieBookSearch, parameter))
                {
                    while (dr.Read())
                    {
                        SystemMovieBook gs = new SystemMovieBook();
                        ReadMovieBook(dr, gs);
                        list.Add(gs);
                    }
                }
                return list;

            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 读取电影数据
        public static void ReadMovieBook(SqlDataReader dr, SystemMovieBook smb)
        {
            smb.AuthorSynopsis = dr["AuthorSynopsis"].ToString();
            smb.BookMovieDirect = dr["BookMovieDirect"].ToString();
            smb.BookMovieName = dr["BookMovieName"].ToString();
            smb.BookMovieStager = dr["BookMovieStager"].ToString();
            smb.Describe = dr["Describe"].ToString();
            smb.Falg = int.Parse(dr["Falg"].ToString());
            smb.ID = (Guid)dr["ID"];
            smb.MBURL = dr["MBURL"].ToString();
            smb.Nameplate = dr["Nameplate"].ToString();
            smb.PublisherFilmCompany = dr["PublisherFilmCompany"].ToString();
            smb.LookUrl = dr["LookUrl"].ToString();
        }
        #endregion
    }
}
