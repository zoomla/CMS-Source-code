using System;
using System.Collections.Generic;
using System.Text;
using ZoomLa.SQLDAL;
using System.Data;
using System.Data.SqlClient;

namespace ZoomLa.Sns
{
   public class ChatLogLogic
   {
       #region 添加聊天记录
       /// <summary>
       /// 添加聊天记录
       /// </summary>
       /// <param name="cl"></param>
       public static void InsertLog(ChatLog cl)
       {
           try
           {
               string sql = @"INSERT INTO [dbo].[ZL_Sns_ChatLog] (
	[Name],
	[Sex],
	[ChatContent],
	[Addtime],
	[ByID],
	[ChatType]
) VALUES (
	@Name,
	@Sex,
	@ChatContent,
	@Addtime,
	@ByID,
	@ChatType
)";
               SqlParameter[] parameter =
                   { 
                       new SqlParameter("Name",cl.Name),
                       new SqlParameter("Sex",cl.Sex),
                       new SqlParameter("ChatContent",cl.ChatContent),
                       new SqlParameter("ChatType",cl.ChatType),
                       new SqlParameter("Addtime",DateTime.Now),
                       new SqlParameter("ByID",cl.ByID)
                   };
               SqlHelper.ExecuteNonQuery(CommandType.Text, sql, parameter);
               
           }
           catch
           {
           }
       }

       #endregion

       #region 读取聊天记录
       /// <summary>
       /// 读取聊天记录
       /// </summary>
       /// <param name="Byid"></param>
       /// <returns></returns>
       public static List<ChatLog> GetCharLog(int Byid)
       {
           try
           {
               string sql = @"select * from ZL_Sns_ChatLog where ByID=@Byid order by Addtime asc";
               SqlParameter[] parameter ={ new SqlParameter("Byid", Byid) };
               List<ChatLog> list = new List<ChatLog>();
               using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sql, parameter))
               {
                   while (dr.Read())
                   {
                       ChatLog cl = new ChatLog();
                       cl.ID = int.Parse(dr["ID"].ToString());
                       cl.Name = dr["Name"].ToString();
                       cl.Sex = dr["Sex"].ToString();
                       cl.Addtime = DateTime.Parse(dr["Addtime"].ToString());
                       cl.ByID = int.Parse(dr["ByID"].ToString());
                       cl.ChatContent = dr["ChatContent"].ToString();
                       cl.ChatType = dr["ChatType"].ToString();
                       list.Add(cl);
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

       #region 清空聊天记录
       /// <summary>
       /// 清空聊天记录
       /// </summary>
       public static void DelChat()
       {
           try
           {
               string sql = @"delete from ZL_Sns_ChatLog";
               SqlHelper.ExecuteNonQuery(CommandType.Text, sql, null);
           }
           catch
           {
           }
       }
       #endregion
   }
}
