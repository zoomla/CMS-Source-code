using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ZoomLa.Sns.Model;
using ZoomLa.SQLDAL;

namespace ZoomLa.Sns.Logic
{
    public class commentlogic
    {
        #region 添加评论
        /// <summary>
        /// 添加评论
        /// </summary>
        /// <param name="ca"></param>
        /// <returns></returns>
        public static Guid Insertcomment(CommentAll ca)
        {
            try
            {
                ca.ID = Guid.NewGuid();
                string sql = @"insert into ZL_Sns_CommentAll(ID,Ctitle,UserID,Ccontent,CbyID,Caddtime,CbyType) values(
@ID,@Ctitle,@UserID,@Ccontent,@CbyID,@Caddtime,@CbyType)";
                SqlParameter[] parameter =
                    {
                        new SqlParameter("ID",ca.ID),
                        new SqlParameter("Ctitle",ca.Ctitle),
                        new SqlParameter("UserID",ca.UserID),
                        new SqlParameter("Ccontent",ca.Ccontent),
                        new SqlParameter("CbyID",ca.CbyID),
                        new SqlParameter("Caddtime",DateTime.Now),
                        new SqlParameter("CbyType",ca.CbyType),
                    };
                SqlHelper.ExecuteNonQuery(CommandType.Text, sql, parameter);
                return ca.ID;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 查询某类型的评论
        /// <summary>
        /// 查询某类型的评论
        /// </summary>
        /// <param name="cID"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public static List<CommentAll> GetCommentBycbyID(Guid cID)
        {
            try
            {
                string sql = @"SELECT * FROM ZL_Sns_CommentAll  where CbyID=@cID";
                
                SqlParameter[] parameter =
                    {
                        new SqlParameter("cID",cID)
                    };

                List<CommentAll> list = new List<CommentAll>();
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sql, parameter))
                {
                    while (dr.Read())
                    {
                        CommentAll ca = new CommentAll();
                        ReadComment(dr, ca);
                        list.Add(ca);
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

        #region 读取用户的评论
        /// <summary>
        /// 读取用户的评论
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public static List<CommentAll> GetcommandByUserID(Guid UserID)
        {
            try
            {
                string sql = @"SELECT * FROM ZL_Sns_CommentAll  where UserID=@UserID";
               
                SqlParameter[] parameter =
                    {
                        new SqlParameter("UserID",UserID)
                    };

                List<CommentAll> list = new List<CommentAll>();
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sql, parameter))
                {
                    while (dr.Read())
                    {
                        CommentAll ca = new CommentAll();
                        ReadComment(dr, ca);
                        list.Add(ca);
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

        #region 删除评论
        /// <summary>
        /// 删除评论
        /// </summary>
        /// <param name="ID"></param>
        public static void Delcommant(Guid ID)
        {
            try
            {
                string sql = @"delete from ZL_Sns_CommentAll where ID=@ID";
                SqlParameter[] parameter ={ new SqlParameter("ID", ID) };
                SqlHelper.ExecuteNonQuery(CommandType.Text, sql, parameter);
            }
            catch
            {
            }
        }
        #endregion

        #region 读取评论表
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="ca"></param>
        public static void ReadComment(SqlDataReader dr, CommentAll ca)
        {
            ca.ID = (Guid)dr["ID"];
            ca.Ctitle = dr["Ctitle"].ToString();
            ca.UserID = (int)dr["UserID"];
            ca.Ccontent = dr["Ccontent"].ToString();
            ca.CbyID = (Guid)dr["CbyID"];
            ca.Caddtime = DateTime.Parse(dr["Caddtime"].ToString());
            ca.CbyType = (int)dr["CbyType"];
        }
        #endregion

        #region 查询最新几条评论
        /// <summary>
        /// 查询最新几条评论
        /// </summary>
        /// <param name="topx"></param>
        /// <returns></returns>
        public static List<CommentAll> GetTopcomment(int topx)
        {
            try
            {
                string sql = @"select top " + @topx + " * from ZL_Sns_CommentAll order by Caddtime desc";
                SqlParameter[] parameter ={ new SqlParameter("topx", topx) };
                List<CommentAll> list = new List<CommentAll>();
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sql, parameter))
                {
                    while (dr.Read())
                    {
                        CommentAll ca = new CommentAll();
                        ReadComment(dr, ca);
                        list.Add(ca);
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
    }
}
