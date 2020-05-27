using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ZoomLa.SQLDAL;

namespace ZoomLa.Sns
{
   public class BlogStyleTableLogic
   {
       #region 添加空间模板
       /// <summary>
       /// 添加空间模板
       /// </summary>
       /// <param name="bst"></param>
       public static void InsertBlogStyle(BlogStyleTable bst)
       {
           try
           {
               string sql = @"INSERT INTO [ZL_Sns_BlogStyleTable] (
	[StyleName],
	[Author],
	[StylePic],
	[Addtime],
	[StyleState],
	[UserIndexStyle],
	[LogStyle],
	[LogShowStyle],
	[PhotoStyle],
	[PhotoShowStyle],
	[PicShowStyle],
	[GroupStyle],
	[GroupShowStyle],
	[GroupTopicShow]
) VALUES (
	@StyleName,
	@Author,
	@StylePic,
	@Addtime,
	@StyleState,
	@UserIndexStyle,
	@LogStyle,
	@LogShowStyle,
	@PhotoStyle,
	@PhotoShowStyle,
	@PicShowStyle,
	@GroupStyle,
	@GroupShowStyle,
	@GroupTopicShow
)";
               SqlParameter[] parameter ={ 
                    new SqlParameter("StyleName",bst.StyleName),
                    new SqlParameter("Author",bst.Author),
                    new SqlParameter("StylePic",bst.StylePic),
                    new SqlParameter("UserIndexStyle",bst.UserIndexStyle),
                    new SqlParameter("LogStyle",bst.LogStyle),
                    new SqlParameter("StyleState",bst.StyleState),
                    new SqlParameter("LogShowStyle",bst.LogShowStyle),
                    new SqlParameter("PhotoStyle",bst.PhotoStyle),
                    new SqlParameter("PhotoShowStyle",bst.PhotoShowStyle),
                    new SqlParameter("PicShowStyle",bst.PicShowStyle),
                    new SqlParameter("GroupStyle",bst.GroupStyle),
                    new SqlParameter("GroupShowStyle",bst.GroupShowStyle),
                    new SqlParameter("GroupTopicShow",bst.GroupTopicShow),
                    new SqlParameter("Addtime",DateTime.Now)
                };
               SqlHelper.ExecuteNonQuery(CommandType.Text, sql, parameter);

           }
           catch
           {
           }
       }
       #endregion

       #region 修改空间模板
       /// <summary>
       /// 修改空间模板
       /// </summary>
       /// <param name="bst"></param>
       public static void UpdateBlogStyle(BlogStyleTable bst)
       {
           try
           {
               string sql = @"UPDATE [ZL_Sns_BlogStyleTable] SET
	[StyleName] = @StyleName,
	[Author] = @Author,
	[StylePic] = @StylePic,
	[StyleState] = @StyleState,
	[UserIndexStyle] = @UserIndexStyle,
	[LogStyle] = @LogStyle,
	[LogShowStyle] = @LogShowStyle,
	[PhotoStyle] = @PhotoStyle,
	[PhotoShowStyle] = @PhotoShowStyle,
	[PicShowStyle] = @PicShowStyle,
	[GroupStyle] = @GroupStyle,
	[GroupShowStyle] = @GroupShowStyle,
	[GroupTopicShow] = @GroupTopicShow
WHERE
	[ID] = @ID";
               SqlParameter[] parameter ={ 
                    new SqlParameter("ID",bst.ID),
                    new SqlParameter("StyleName",bst.StyleName),
                    new SqlParameter("Author",bst.Author),
                    new SqlParameter("StylePic",bst.StylePic),
                    new SqlParameter("UserIndexStyle",bst.UserIndexStyle),
                    new SqlParameter("LogStyle",bst.LogStyle),
                    new SqlParameter("StyleState",bst.StyleState),
                    new SqlParameter("LogShowStyle",bst.LogShowStyle),
                    new SqlParameter("PhotoStyle",bst.PhotoStyle),
                    new SqlParameter("PhotoShowStyle",bst.PhotoShowStyle),
                    new SqlParameter("GroupStyle",bst.GroupStyle),
                    new SqlParameter("PicShowStyle",bst.PicShowStyle),
                    new SqlParameter("GroupTopicShow",bst.GroupTopicShow),
                    new SqlParameter("GroupShowStyle",bst.GroupShowStyle)
                };
               SqlHelper.ExecuteNonQuery(CommandType.Text, sql, parameter);
           }
           catch { }
       }
       #endregion

       #region 删除空间模板
       /// <summary>
       /// 删除空间模板
       /// </summary>
       /// <param name="list"></param>
       public static void DelBlogStyle(string list)
       {
           try
           {
               string sql = @"delete from ZL_Sns_BlogStyleTable where (ID in ("+list+"))";

               SqlHelper.ExecuteNonQuery(CommandType.Text, sql, null);
           }
           catch { }
       }
       #endregion

       #region 根据ID读取模板信息
       /// <summary>
       /// 根据ID读取模板信息
       /// </summary>
       /// <param name="ID"></param>
       /// <returns></returns>
       public static BlogStyleTable GetBlogStyleByID(int ID)
       {
           try
           {
               string sql = @"select * from ZL_Sns_BlogStyleTable where ID=@ID";
               SqlParameter[] parameter ={ 
                    new SqlParameter("ID",ID)
                };
               BlogStyleTable bst = new BlogStyleTable();
               using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sql, parameter))
               {
                   if (dr.Read())
                   {
                       ReadBlogStyle(dr, bst);
                   }
               }
               return bst;
           }
           catch
           {
               throw;
           }
       }
       #endregion

       #region 根据状态读取模板
       /// <summary>
       /// 根据状态读取模板
       /// </summary>
       /// <param name="state">0为停用 1为启用 2为不限</param>
       /// <returns></returns>
       public static DataTable GetBlogStyleByState(int state)
       {
           string sql = "select * from ZL_Sns_BlogStyleTable where StyleState=" + state;
           return SqlHelper.ExecuteTable(CommandType.Text, sql);
       }
       #endregion

       #region 批量修改状态
       /// <summary>
       /// 批量修改状态
       /// </summary>
       /// <param name="state"></param>
       /// <param name="list"></param>
       public static void UpdateBatchState(int state, string list)
       {
           string[] listArr = list.Split(',');
           string sqlstr = "";
           SqlParameter[] sp = new SqlParameter[listArr.Length + 1];

           for (int i = 0; i < listArr.Length; i++)
           {
               sp[i] = new SqlParameter("list" + i, listArr[i]);
               sqlstr += "@" + sp[i].ParameterName + ",";
           }
           sqlstr = sqlstr.TrimEnd(',');
           sp[listArr.Length] = new SqlParameter("state", state);
           try
           {
               string sql = @"Update ZL_Sns_BlogStyleTable set StyleState=@state where (ID in (" + sqlstr + "))";
               //SqlParameter[] pararmeter ={ new SqlParameter("state", state) };
               SqlHelper.ExecuteNonQuery(CommandType.Text, sql, sp);
           }
           catch { }
       }
       #endregion

       #region 读取模板信息
       /// <summary>
       /// 读取模板信息
       /// </summary>
       /// <param name="dr"></param>
       /// <param name="bst"></param>
       public static void ReadBlogStyle(SqlDataReader dr, BlogStyleTable bst)
       {
           bst.ID = int.Parse(dr["ID"].ToString());
           bst.StyleName = dr["StyleName"].ToString();
           bst.StylePic = dr["StylePic"].ToString();
           
           bst.Author = dr["Author"].ToString();
           bst.StyleState = int.Parse(dr["StyleState"].ToString());
           bst.Addtime = DateTime.Parse(dr["Addtime"].ToString());
           bst.UserIndexStyle = dr["UserIndexStyle"].ToString();
           bst.LogStyle = dr["LogStyle"].ToString();
           bst.LogShowStyle = dr["LogShowStyle"].ToString();
           bst.PhotoStyle = dr["PhotoStyle"].ToString();
           bst.PhotoShowStyle = dr["PhotoShowStyle"].ToString();
           bst.GroupStyle = dr["GroupStyle"].ToString();
           bst.GroupShowStyle = dr["GroupShowStyle"].ToString();
           bst.PicShowStyle = dr["PicShowStyle"].ToString();
           bst.GroupShowStyle = dr["GroupShowStyle"].ToString();
           bst.GroupTopicShow = dr["GroupTopicShow"].ToString();
       }
       #endregion
   }
}
