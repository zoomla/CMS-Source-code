using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ZoomLa.SQLDAL;

namespace ZoomLa.Sns
{
   public class blogTableLogic
   {
       #region 申请开通空间
       /// <summary>
       /// 申请开通空间
       /// </summary>
       /// <param name="bt"></param>
       public static void InsertBlog(blogTable bt)
       {
           try
           {
               bt.BlogHits = 0;
               string sql = @"INSERT INTO [dbo].[ZL_Sns_blogTable] (
	[UserID],
	[BlogName],
	[BlogContent],
	[BlogState],
	[Addtime],
	[CommendState],
	[StyleID],
	[BlogHits]
) VALUES (
	@UserID,
	@BlogName,
	@BlogContent,
	@BlogState,
	@Addtime,
	@CommendState,
	@StyleID,
	@BlogHits
)";
               SqlParameter[] parameter ={ 
                    new SqlParameter("UserID",bt.UserID),
                    new SqlParameter("BlogName",bt.BlogName),
                    new SqlParameter("BlogContent",bt.BlogContent),
                    new SqlParameter("BlogState",bt.BlogState),
                    new SqlParameter("CommendState",bt.CommendState),
                    new SqlParameter("StyleID",bt.StyleID),
                    new SqlParameter("AddTime",DateTime.Now),
                    new SqlParameter("BlogHits",bt.BlogHits)
                };
               SqlHelper.ExecuteNonQuery(CommandType.Text, sql, parameter);
               
           }
           catch
           {
           }
           
       }
       #endregion

       #region 修改空间
       /// <summary>
       /// 修改空间
       /// </summary>
       /// <param name="bt"></param>
       public static void UpdateBlogtable(blogTable bt)
       { 
               string sql = @"UPDATE [ZL_Sns_blogTable] SET
	[BlogName] = @BlogName,
	[BlogContent] = @BlogContent,
	[BlogState] = @BlogState,
	[CommendState] = @CommendState,
    [StyleID]=@StyleID
WHERE
	[ID] = @ID";
               SqlParameter[] parameter ={ 
                    new SqlParameter("ID",bt.ID),
                    new SqlParameter("BlogName",bt.BlogName),
                    new SqlParameter("BlogContent",bt.BlogContent),
                    new SqlParameter("BlogState",bt.BlogState),
                    new SqlParameter("StyleID",bt.StyleID),
                    new SqlParameter("CommendState",bt.CommendState)
                };
               SqlHelper.ExecuteNonQuery(CommandType.Text, sql, parameter);
          
       }
       #endregion

       #region 删除空间
       /// <summary>
       /// 删除空间
       /// </summary>
       /// <param name="ID"></param>
       public static void DelBlogtable(int ID)
       {
           try
           {
               string sql = @"DELETE FROM [ZL_Sns_blogTable]
WHERE
	[ID] = @ID";
               SqlParameter[] parameter ={ 
                    new SqlParameter("ID",ID)
                };
               SqlHelper.ExecuteNonQuery(CommandType.Text, sql, parameter);
           }
           catch { }
       }
       #endregion

       #region 读取用户空间信息
       /// <summary>
       /// 读取用户空间信息
       /// </summary>
       /// <param name="UserID"></param>
       /// <returns></returns>
       public static blogTable GetUserBlog(int UserID)
       {
           try
           {
               string sql = @"select * from ZL_Sns_blogTable where UserID=@UserID";
               SqlParameter[] parameter ={ 
                    new SqlParameter("UserID",UserID)
                };
               blogTable bt = new blogTable();
               using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sql, parameter))
               {
                   if (dr.Read())
                   {
                       ReadBlogTable(dr, bt);
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

       #region 根据类型读取空间列表
       /// <summary>
       /// 根据类型读取空间列表
       /// </summary>
       /// <param name="state">0待审核 1通过 2为关闭</param>
       /// <returns></returns>
       public static List<blogTable> GetBlogTableByState(int state)
       {
           try
           {
               string sql = @"select * from ZL_Sns_blogTable where 1=1";
               if (state != 1)
               {
                   sql += " and BlogState=@state";
               }
               else
               {
                   sql += " and BlogState>0";
               }
               SqlParameter[] parameter ={ 
                    new SqlParameter("state",state)
               };
               List<blogTable> list = new List<blogTable>();
               using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sql, parameter))
               {
                   while (dr.Read())
                   {
                       blogTable bt = new blogTable();
                       ReadBlogTable(dr, bt);
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

       #region 批量修改状态
       /// <summary>
       /// 批量修改状态
       /// </summary>
       /// <param name="list"></param>
       /// <param name="state"></param>
       /// <param name="type">C为推荐,关闭状态 D为删除 A为审核</param>
       public static void BatchUpdateState(string list,int state,string type)
       {
           string sql = string.Empty;
           string[] listArr = list.Split(',');
           string sqlstr = "";
           SqlParameter[] sp = new SqlParameter[listArr.Length + 1];

           for (int i = 0; i < listArr.Length; i++)
           {
               sp[i] = new SqlParameter("list" + i, listArr[i]);
               sqlstr += "@" + sp[i].ParameterName + ",";
           }
           sqlstr = sqlstr.TrimEnd(',');
           if (type == "C")
           {
               sql = "update ZL_Sns_blogTable set CommendState=@state where (id in (" + sqlstr + "))";
           }
           else if (type == "D")
           {
               sql = "delete from ZL_Sns_blogTable where (id in (" + sqlstr + "))";
           }
           else
           {
               sql = "update ZL_Sns_blogTable set BlogState=1 where (id in (" + sqlstr + "))";
           }
           sp[listArr.Length] = new SqlParameter("state", state);
           //SqlParameter[] parameter ={ new SqlParameter("state", state) };
           SqlHelper.ExecuteNonQuery(CommandType.Text, sql, sp);
       }
       #endregion

       #region 修改点击数
       /// <summary>
       /// 修改点击数
       /// </summary>
       /// <param name="ID"></param>
       public static void UpdateHits(int ID)
       {
           try
           {
               string sql = @"update ZL_Sns_blogTable set BlogHits=BlogHits+1 where ID=@ID";
               SqlParameter[] parameter ={ new SqlParameter("ID", ID) };
               SqlHelper.ExecuteNonQuery(CommandType.Text, sql, parameter);
           }
           catch { }
       }
       #endregion

       #region 读取空间表
       public static void ReadBlogTable(SqlDataReader dr, blogTable bt)
       {
           bt.ID = int.Parse(dr["ID"].ToString());
           bt.UserID = int.Parse(dr["UserID"].ToString());
           bt.BlogName = dr["BlogName"].ToString();
           bt.BlogContent = dr["BlogContent"].ToString();
           bt.BlogState = int.Parse(dr["BlogState"].ToString());
           bt.CommendState = int.Parse(dr["CommendState"].ToString());
           bt.Addtime =DateTime.Parse(dr["AddTime"].ToString());
           bt.StyleID = int.Parse(dr["StyleID"].ToString());
           bt.BlogHits = int.Parse(dr["BlogHits"].ToString());
       }
       #endregion
   }
}
