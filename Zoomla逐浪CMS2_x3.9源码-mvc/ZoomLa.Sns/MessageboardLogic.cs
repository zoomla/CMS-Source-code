using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using BDUModel;
using ZoomLa.SQLDAL;

namespace BDULogic
{
    public class MessageboardLogic
    {
        #region 添加留言
        /// <summary>
        /// 添加留言
        /// </summary>
        /// <param name="me"></param>
        /// <returns></returns>
        public static Guid InsertMessage(Messageboard me)
        {
            try
            {
                me.ID = Guid.NewGuid();
                me.State = 0;
                string sql = @"insert into ZL_Sns_Messageboard(ID,SendID,InceptID,Mcontent,Addtime,RestoreID,State)
values(@ID,@SendID,@InceptID,@Mcontent,@Addtime,@RestoreID,@State)";
                SqlParameter[] parameter =
                    { 
                        new SqlParameter("ID",me.ID),
                        new SqlParameter("SendID",me.SendID),
                        new SqlParameter("InceptID",me.InceptID),
                        new SqlParameter("Mcontent",me.Mcontent),
                        new SqlParameter("Addtime",DateTime.Now),
                        new SqlParameter("RestoreID",me.RestoreID),
                        new SqlParameter("State",me.State),
                    };
                SqlHelper.ExecuteNonQuery(CommandType.Text, sql, parameter);
                return me.ID;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 读取用户留言信息
        /// <summary>
        /// 读取用户留言信息
        /// </summary>
        /// <param name="InceptID"></param>
        /// <returns></returns>
        public static List<Messageboard> GetMessageByInceptID(int InceptID)
        {
            try
            {
                Guid RestoreID = Guid.Empty;
                string sql = @"select * from ZL_Sns_Messageboard where InceptID=@InceptID and RestoreID=@RestoreID and State=0 order by addtime desc";
                SqlParameter[] parameter ={ new SqlParameter("InceptID", InceptID),
                new SqlParameter("RestoreID",RestoreID)
                };
                List<Messageboard> list = new List<Messageboard>();
                using(SqlDataReader dr=SqlHelper.ExecuteReader(CommandType.Text,sql,parameter))
                {
                    while (dr.Read())
                    {
                        Messageboard me = new Messageboard();
                        ReadMessage(dr, me);
                        list.Add(me);
                    }
                }
                return list;
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// 读取用户自己留言或回复信息
        /// </summary>
        /// <param name="InceptID"></param>
        /// <returns></returns>
        public static List<Messageboard> GetMessageBySendID(int SendID,Guid RestoreID)
        {
            try
            {
                string sql = @"select * from ZL_Sns_Messageboard where SendID=@SendID and RestoreID=@RestoreID and State=0 order by addtime desc";
                SqlParameter[] parameter ={ new SqlParameter("SendID", SendID),
                new SqlParameter("RestoreID",RestoreID)
                };
                List<Messageboard> list = new List<Messageboard>();
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sql, parameter))
                {
                    while (dr.Read())
                    {
                        Messageboard me = new Messageboard();
                        ReadMessage(dr, me);
                        list.Add(me);
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

        #region 读取留言回复信息
        /// <summary>
        /// 读取留言回复信息
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static List<Messageboard> GetRestoreMessageByID(Guid ID)
        {
            try
            {
                string sql = @"select * from ZL_Sns_Messageboard where RestoreID=@ID and State=0";
                SqlParameter[] parameter ={ new SqlParameter("ID", ID) };
                List<Messageboard> list = new List<Messageboard>();
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sql, parameter))
                {
                    while (dr.Read())
                    {
                        Messageboard me = new Messageboard();
                        ReadMessage(dr, me);
                        list.Add(me);
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

        #region 删除留言
        public static void DelMessage(Guid ID)
        {
            string sql = @"update ZL_Sns_Messageboard set  state=1 where ID=@ID";
            SqlParameter[] parameter ={ new SqlParameter("ID",ID)};
            SqlHelper.ExecuteNonQuery(CommandType.Text, sql, parameter);

        }
        #endregion

        #region 读取留言表
        /// <summary>
        /// 读取留言表
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="me"></param>
        public static void ReadMessage(SqlDataReader dr, Messageboard me)
        {
            me.ID = (Guid)dr["ID"];
            me.SendID = int.Parse(dr["SendID"].ToString());
            me.InceptID = int.Parse(dr["InceptID"].ToString());
            me.Mcontent = dr["Mcontent"].ToString();
            me.Addtime = DateTime.Parse(dr["Addtime"].ToString());
            me.RestoreID = (Guid)dr["RestoreID"];
            me.State = (int)dr["State"];
        }
        #endregion
    }
}
