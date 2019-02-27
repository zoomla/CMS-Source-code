using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using BDUModel;
using ZoomLa.SQLDAL;

namespace BDULogic
{
    public class CommendCommentOn_Logic
    {
        #region 添加推荐评论
        /// <summary>
        /// 添加推荐评论
        /// </summary>
        /// <param name="ccon"></param>
        public static void Add(CommendCommentOn ccon)
        {
            ccon.ID = Guid.NewGuid();
            string SQLstr = @"INSERT INTO [ZL_Sns_CommendCommentOn] ([ID],[CommentOnUserID],[CommentOnContext],[CommentOnTime],[CommentID]) 
VALUES (@ID,@CommentOnUserID,@CommentOnContext,@CommentOnTime,@CommentID)";

            SqlParameter[] sp ={ 
                new SqlParameter("@ID",ccon.ID),
                new SqlParameter("@CommentOnUserID",ccon.CommentOnUserID),
                new SqlParameter("@CommentOnContext",ccon.CommentOnContext),
                new SqlParameter("@CommentOnTime",ccon.CommentOnTime),
                new SqlParameter("@CommentID",ccon.CommentID)
            };

            SqlHelper.ExecuteNonQuery(CommandType.Text, SQLstr, sp);
        }
        #endregion

        #region 删除推荐评论
        /// <summary>
        /// 删除推荐评论
        /// </summary>
        /// <param name="id"></param>
        public static void Del(Guid id)
        {
            string SQLstr = @"DELETE FROM [ZL_Sns_CommendCommentOn] WHERE [ID] = @ID";

            SqlParameter[] sp ={ new SqlParameter("@ID", id) };

            SqlHelper.ExecuteNonQuery(CommandType.Text, SQLstr, sp);
        }
        #endregion

        #region 根据推荐ID查询评论
        /// <summary>
        /// 根据推荐ID查询评论
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<CommendCommentOn> GetCommendCommentOn(Guid id, PagePagination page)
        {
            try
            {
                string SQLstr = @"select a.*,b.UserName from ZL_Sns_CommendCommentOn as a INNER JOIN ZL_Sns_UserTable AS  b ON
a.CommentOnUserID=b.UserID  where a.CommentID=@CommentID ";
                if (page != null)
                {
                    SQLstr = page.PaginationSql(SQLstr);
                }
                SqlParameter[] parameter ={
                    new SqlParameter("@CommentID",id)
                };
                List<CommendCommentOn> list = new List<CommendCommentOn>();
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, SQLstr, parameter))
                {
                    while (dr.Read())
                    {
                        CommendCommentOn ct = new CommendCommentOn();
                        ReadCommendTable(dr, ct);
                        ct.UserName = dr["UserName"].ToString();
                        list.Add(ct);
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

        #region

        #endregion

        #region 读取推荐评论信息
        /// <summary>
        /// 读取推荐评论信息
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="piccateg"></param>
        public static void ReadCommendTable(SqlDataReader dr, CommendCommentOn ct)
        {
            ct.ID = (Guid)dr["ID"];
            ct.CommentOnContext = dr["CommentOnContext"].ToString();
            ct.CommentOnUserID = (Guid)dr["CommentOnUserID"];
            ct.CommentOnTime = dr["CommentOnTime"] is DBNull ? new DateTime() : DateTime.Parse(dr["CommentOnTime"].ToString());
            ct.CommentID = (Guid)dr["CommentID"];
        }
        #endregion
    }
}
