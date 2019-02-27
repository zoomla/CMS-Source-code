using System;
using System.Collections.Generic;
using System.Text;
using BDUModel;
using System.Data.SqlClient;
using System.Data;
using ZoomLa.SQLDAL;

namespace BDULogic
{
    public class Memo_Logic
    {
        #region 添加备忘录数据
        /// <summary>
        /// 添加备忘录数据
        /// </summary>
        /// <param name="memo"></param>
        public static void Add(UserMemo memo)
        {
            memo.ID = Guid.NewGuid();
            string SQLstr = @"INSERT INTO ZL_Sns_Memo ([ID],[UserID],[MemoTitle],[MemoContext],[MemoTime])
     VALUES(@ID,@UserID,@MemoTitle,@MemoContext,@MemoTime)";
            SqlParameter[] sp ={ 
                new SqlParameter("@ID",memo.ID),
                new SqlParameter("@UserID",memo.UserID),
                new SqlParameter("@MemoTitle",memo.MemoTitle),
                new SqlParameter("@MemoContext",memo.MemoContext),
                new SqlParameter("@MemoTime",memo.MemoTime)
            };
            SqlHelper.ExecuteNonQuery(CommandType.Text, SQLstr, sp);

        }
        #endregion

        #region 修改备忘录
        /// <summary>
        /// 修改备忘录
        /// </summary>
        /// <param name="memo"></param>
        public static void Update(UserMemo memo)
        {
            string SQLstr = @"UPDATE ZL_Sns_Memo SET [MemoTitle] = @MemoTitle ,[MemoContext] = @MemoContext,[MemoTime]=@MemoTime
                             WHERE [ID] = @ID";
            SqlParameter[] sp ={ 
                new SqlParameter("@ID",memo.ID),
                new SqlParameter("@MemoTitle",memo.MemoTitle),
                new SqlParameter("@MemoContext",memo.MemoContext),
                new SqlParameter("@MemoTime",memo.MemoTime)
            };
            SqlHelper.ExecuteNonQuery(CommandType.Text, SQLstr, sp);
        }
        #endregion

        #region 根据用户ID查询所有备忘录
        /// <summary>
        ///  根据用户ID查询所有备忘录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<UserMemo> GetMemoList(int id, PagePagination page)
        {
            try
            {
                List<UserMemo> list = new List<UserMemo>();
                string SQLstr = @"select * from ZL_Sns_Memo where UserID=@ID  ";
                if (page != null)
                    SQLstr = page.PaginationSql(SQLstr);
                SqlParameter[] parameter ={
                new SqlParameter("@ID",id)
                };
                
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, SQLstr, parameter))
                {
                    while (dr.Read())
                    {
                        UserMemo memo = new UserMemo();
                        ReadMemo(dr, memo);
                        list.Add(memo);
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

        #region 根据ID查询单条备忘录
        /// <summary>
        /// 根据ID查询单条备忘录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static UserMemo GetMemo(Guid id)
        {
            try
            {
                UserMemo memo = new UserMemo();
                string SQLstr = @"select * from ZL_Sns_Memo where ID=@ID";
                SqlParameter[] parameter ={
                new SqlParameter("@ID",id)
                };
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, SQLstr, parameter))
                {
                    while (dr.Read())
                    {
                        ReadMemo(dr, memo);
                    }
                }
                return memo;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 读取备忘录表数据
        /// <summary>
        /// 读取备忘录表数据
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="mo"></param>
        public static void ReadMemo(SqlDataReader dr, UserMemo mo)
        {
            mo.ID = (Guid)dr["ID"];
            mo.UserID = Convert.ToInt32 (dr["UserID"].ToString ());
            mo.MemoTitle = dr["MemoTitle"].ToString();
            mo.MemoContext = dr["MemoContext"].ToString();
            mo.MemoTime = dr["MemoTime"] is DBNull ? new DateTime() : DateTime.Parse(dr["MemoTime"].ToString());
        }
        #endregion

        #region 删除备忘录
        /// <summary>
        /// 删除备忘录
        /// </summary>
        /// <param name="id"></param>
        public static void del(Guid id)
        {
            string sql = "delete from ZL_Sns_Memo where ID=@id";
            SqlParameter[] sp ={ 
                new SqlParameter("@id",id)
            };
            SqlHelper.ExecuteNonQuery(CommandType.Text, sql, sp);
        }
        #endregion

    }
}
