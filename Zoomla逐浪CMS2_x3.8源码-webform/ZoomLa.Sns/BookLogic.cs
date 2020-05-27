using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using BDUModel;
using ZoomLa.Sns.Model;
using ZoomLa.SQLDAL;
namespace ZoomLa.Sns.Logic
{
    public class BookLogic
    {
        #region 添加书籍
        /// <summary>
        /// 添加书籍
        /// </summary>
        /// <param name="bt"></param>
        /// <returns></returns>
        public static Guid InsertBooktable(BookTable bt)
        {
            try
            {
                bt.ID = Guid.NewGuid();
                string sql = @"insert into ZL_Sns_BookTable(ID,BookTitle,BookOtherTitle,BookAnthor,BookIsbn,BookPrice,BookConcerm,BookYear,Bookurl,BookContent
,BookState,BookAddtime,Uid) values(@ID,@BookTitle,@BookOtherTitle,@BookAnthor,@BookIsbn,@BookPrice,@BookConcerm,@BookYear,@Bookurl,@BookContent,@BookState,@BookAddtime,@Uid)";
                SqlParameter[] parameter ={ 
                    new SqlParameter("ID",bt.ID),
                    new SqlParameter("BookTitle",bt.BookTitle),
                    new SqlParameter("BookOtherTitle",bt.BookOtherTitle),
                    new SqlParameter("BookAnthor",bt.BookAnthor),
                    new SqlParameter("BookIsbn",bt.BookIsbn),
                    new SqlParameter("BookPrice",bt.BookPrice),
                    new SqlParameter("BookConcerm",bt.BookConcerm),
                    new SqlParameter("BookYear",bt.BookYear),
                    new SqlParameter("Bookurl",bt.Bookurl),
                    new SqlParameter("BookContent",bt.BookContent),
                    new SqlParameter("BookState",bt.BookState),
                    new SqlParameter("BookAddtime",DateTime.Now),
                    new SqlParameter("Uid",bt.Uid)
                };
                SqlHelper.ExecuteScalar(CommandType.Text, sql, parameter);
                return bt.ID;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 显示所有书籍
        /// <summary>
        /// 显示所有
        /// </summary>
        /// <param name="page"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public static List<BookTable> GetBooktable(int state)
        {
            try
            {
                string sql = @"select * from ZL_Sns_BookTable where BookState=@BookState order by bookAddtime desc";

                SqlParameter[] parameter ={ new SqlParameter("BookState", state) };
                List<BookTable> list = new List<BookTable>();
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sql, parameter))
                {
                    while (dr.Read())
                    {
                        BookTable bt = new BookTable();
                        ReadBookTable(dr, bt);
                        list.Add(bt);
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

        #region 按名称搜索
        /// <summary>
        /// 按名称搜索
        /// </summary>
        /// <param name="page"></param>
        /// <param name="title"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public static List<BookTable> GetBookLike(string title, int state)
        {
            try
            {
                string sql = @"select * from ZL_Sns_BookTable where 1=1 ";
                if (title != null)
                {
                    sql += "and BookTitle like '%'+@title+'%'";
                }
                else
                {
                    title = "1";
                }
                if (state != 100)
                {
                    sql += " and BookState=@BookState";
                }
                SqlParameter[] parameter ={ new SqlParameter("BookState", state),
                new SqlParameter("title",title)
                };
                List<BookTable> list = new List<BookTable>();
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sql, parameter))
                {
                    while (dr.Read())
                    {
                        BookTable bt = new BookTable();
                        ReadBookTable(dr, bt);
                        list.Add(bt);
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

        #region 显示Top数书籍
        /// <summary>
        /// 显示Top数书籍
        /// </summary>
        /// <param name="Topx"></param>
        /// <param name="page"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public static List<BookTable> GetBooktableTop(int Topx,int state)
        {
            try
            {
                string sql = @"select top " + @Topx + " * from ZL_Sns_BookTable where BookState=@BookState";
                
                SqlParameter[] parameter ={ 
                new SqlParameter("Topx",Topx),
                    new SqlParameter("BookState", state)
                };
                List<BookTable> list = new List<BookTable>();
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sql, parameter))
                {
                    while (dr.Read())
                    {
                        BookTable bt = new BookTable();
                        ReadBookTable(dr, bt);
                        list.Add(bt);
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

        #region 读取书籍
        /// <summary>
        /// 读取书籍
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="bt"></param>
        public static void ReadBookTable(SqlDataReader dr, BookTable bt)
        {
            bt.ID = (Guid)dr["ID"];
            bt.BookTitle = dr["BookTitle"].ToString();
            bt.BookOtherTitle = dr["BookOtherTitle"].ToString();
            bt.BookAnthor = dr["BookAnthor"].ToString();
            bt.BookIsbn = dr["BookIsbn"].ToString();
            bt.BookPrice = dr["BookPrice"] is DBNull ? decimal.Parse("0.00") : decimal.Parse(dr["BookPrice"].ToString());
            bt.BookConcerm = dr["BookConcerm"].ToString();
            bt.BookYear = dr["BookYear"].ToString();
            bt.Bookurl = dr["Bookurl"].ToString();
            bt.BookContent = dr["BookContent"].ToString();
            bt.BookState = (int)dr["BookState"];
            bt.Uid = (int)dr["Uid"];
            bt.BookAddtime = DateTime.Parse(dr["BookAddtime"].ToString());
        }
        #endregion

        #region 根据ID查询表
        /// <summary>
        /// 根据ID查询表
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static BookTable GetBooktableByID(Guid ID)
        {
            try
            {
                string sql = @"select * from ZL_Sns_BookTable where ID=@ID";
                SqlParameter[] parameter ={ new SqlParameter("ID", ID) };
                BookTable bt = new BookTable();
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sql, parameter))
                {
                    if (dr.Read())
                    {
                        ReadBookTable(dr, bt);
                    }
                }
                return bt;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 删除书籍
        /// <summary>
        /// 删除书籍
        /// </summary>
        /// <param name="id"></param>
        public static void del(Guid id)
        {
            string sql = "delete from ZL_Sns_BookTable where ID=@ID";
            SqlParameter[] parameter ={ 
                    new SqlParameter("ID",id)};
            SqlHelper.ExecuteScalar(CommandType.Text, sql, parameter);
        }
        #endregion
    }
}
