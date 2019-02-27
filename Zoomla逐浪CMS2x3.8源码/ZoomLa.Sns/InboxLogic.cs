using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using BDUModel;
using ZoomLa.SQLDAL;

namespace BDULogic
{
    public class InboxLogic
    {
        #region 添加邮件
        /// <summary>
        /// 添加邮件
        /// </summary>
        /// <param name="it"></param>
        /// <returns></returns>
        public static Guid InsertInbox(InboxTable it)
        {
            try
            {
                it.ID = Guid.NewGuid();
                it.InboxState = 0;
                string sql = @"insert into ZL_Sns_InboxTable(ID,SenderID,InceptID,InboxTitle,InboxContent,Addtime,InboxState,InboxType) values
(@ID,@SenderID,@InceptID,@InboxTitle,@InboxContent,@Addtime,@InboxState,@InboxType)";
                SqlParameter[] parameter =
                   {
                       new SqlParameter("ID",it.ID),
                       new SqlParameter("SenderID",it.SenderID),
                       new SqlParameter("InceptID",it.InceptID),
                       new SqlParameter("InboxTitle",it.InboxTitle),
                       new SqlParameter("InboxContent",it.InboxContent),
                       new SqlParameter("Addtime",DateTime.Now),
                       new SqlParameter("InboxState",it.InboxState),
                       new SqlParameter("InboxType",it.InboxType)
                   };
                SqlHelper.ExecuteNonQuery(CommandType.Text, sql, parameter);
                return it.ID;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 读取某用户收件箱
        /// <summary>
        /// 
        /// </summary>
        /// <param name="InceptID"></param>
        /// <returns></returns>
        public static List<InboxTable> Getinbox(Guid InceptID)
        {
            try
            {
                string sql = @"select * from ZL_Sns_inboxtable where InceptID=@InceptID order by addtime desc";
                SqlParameter[] parameter ={ new SqlParameter("InceptID", InceptID) };
                List<InboxTable> list = new List<InboxTable>();
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sql, parameter))
                {
                    while (dr.Read())
                    {
                        InboxTable it = new InboxTable();
                        ReadInbox(dr, it);
                        list.Add(it);
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

        #region 读取某用户发件箱
        /// <summary>
        /// 读取某用户发件箱
        /// </summary>
        /// <param name="InceptID"></param>
        /// <returns></returns>
        public static List<InboxTable> Getinboxsend(Guid InceptID)
        {
            try
            {
                string sql = @"select * from ZL_Sns_inboxtable where SenderID=@InceptID order by addtime desc";
                SqlParameter[] parameter ={ new SqlParameter("InceptID", InceptID) };
                List<InboxTable> list = new List<InboxTable>();
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sql, parameter))
                {
                    while (dr.Read())
                    {
                        InboxTable it = new InboxTable();
                        ReadInbox(dr, it);
                        list.Add(it);
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

        #region 修改邮件的阅读状态
        /// <summary>
        /// 修改邮件的阅读状态
        /// </summary>
        /// <param name="ID"></param>
        public static void UpdateState(Guid ID)
        {
            try
            {
                string sql = @"Update ZL_Sns_InboxTable set InboxState=1 where ID=@ID";
                SqlParameter[] parameter ={ new SqlParameter("ID", ID) };
                SqlHelper.ExecuteNonQuery(CommandType.Text, sql, parameter);
            }
            catch { }
        }
        #endregion

        #region 根据ID读取数据
        /// <summary>
        /// 根据ID读取数据
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static InboxTable GetInboxbyID(Guid ID)
        {
            try
            {
                string sql = @"select * from ZL_Sns_InboxTable where ID=@ID";
                SqlParameter[] parameter ={ new SqlParameter("ID", ID) };
                InboxTable it = new InboxTable();
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sql, parameter))
                {
                    if (dr.Read())
                    {
                        ReadInbox(dr, it);
                    }
                }
                return it;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 读取新邮件
        /// <summary>
        /// 读取新邮件
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public static int GetInboxCount(Guid UserID, int state)
        {
            try
            {
                string sql = @"select * from ZL_Sns_InboxTable where InceptID=@UserID and InboxType=@state and InboxState=1";
                SqlParameter[] parameter ={ 
               
                new SqlParameter("UserID",UserID),
                   new SqlParameter("state",state)};
                //int s;
                // using(SqlDataReader dr=SqlHelper.ExecuteReader(CommandType.Text,sql,parameter))
                // {
                //     if (dr.Read())
                //     {
                //         s = dr.co
                //     }
                // }
                // return s;
                if (SqlHelper.ExecuteScalar(CommandType.Text, sql, parameter) != null)
                    return SqlHelper.ExecuteNonQuery(CommandType.Text, sql, parameter);
                else return 0;

            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 根据ID删除记录
        /// <summary>
        /// 根据ID删除记录
        /// </summary>
        /// <param name="ID"></param>
        public static void DelinboxByid(Guid ID)
        {
            try
            {
                string sql = @"delete from ZL_Sns_inboxtable where ID=@ID";
                SqlParameter[] parameter ={ new SqlParameter("ID", ID) };
                SqlHelper.ExecuteNonQuery(CommandType.Text, sql, parameter);
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 读取表
        /// <summary>
        /// 读取表
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="it"></param>
        public static void ReadInbox(SqlDataReader dr, InboxTable it)
        {
            it.ID = (Guid)dr["ID"];
            it.SenderID = (Guid)dr["SenderID"];
            it.InceptID = (Guid)dr["InceptID"];
            it.InboxTitle = dr["InboxTitle"].ToString();
            it.InboxContent = dr["InboxContent"].ToString();
            it.Addtime = DateTime.Parse(dr["Addtime"].ToString());
            it.InboxState = (int)dr["InboxState"];
            it.InboxType = (int)dr["InboxType"];
        }
        #endregion
    }
}
