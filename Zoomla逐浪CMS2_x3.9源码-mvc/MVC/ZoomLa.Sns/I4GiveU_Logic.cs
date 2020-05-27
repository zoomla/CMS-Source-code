using System;
using System.Collections.Generic;
using System.Text;
using BDUModel;
using System.Data;
using System.Data.SqlClient;
using ZoomLa.SQLDAL;

namespace BDULogic
{
    public class I4GiveU_Logic
    {
        #region ��������Ϣ
        /// <summary>
        /// ��������Ϣ
        /// </summary>
        /// <param name="give"></param>
        public static Guid Add(I4GiveU give)
        {
            give.ID = Guid.NewGuid();
            string SQLstr = @"INSERT INTO ZL_Sns_I4GiveU ([ID],[ConfessID],[ByConfessID],[ConfessType],[ConfessContext],[ConfessTime],[ConfessTitle])
     VALUES (@ID,@ConfessID,@ByConfessID,@ConfessType,@ConfessContext,@ConfessTime,@ConfessTitle)";
            SqlParameter[] sp ={
                new SqlParameter("@ID",give.ID),
                new SqlParameter("@ConfessID",give.ConfessID),
                new SqlParameter("@ByConfessID",give.ByConfessID),
                new SqlParameter("@ConfessType",give.ConfessType),
                new SqlParameter("@ConfessContext",give.ConfessContext),
                new SqlParameter("@ConfessTime",give.ConfessTime),
                new SqlParameter("@ConfessTitle",give.ConfessTitle)
            };
            SqlHelper.ExecuteNonQuery(CommandType.Text, SQLstr, sp);
            return give.ID;
        }
        #endregion


        #region ��ѯ���������Ϣ
        /// <summary>
        /// ��ѯ���������Ϣ
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static I4GiveU GetOne(Guid id)
        {
            try
            {
                I4GiveU give = new I4GiveU();
                string SQLstr = @"SELECT *  FROM ZL_Sns_I4GiveU where ID=@ID";

                SqlParameter[] sp ={ new SqlParameter("@ID", id) };

                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, SQLstr, sp))
                {
                    if (dr.Read())
                    {
                        ReadI4GiveU(dr, give);
                    }
                }
                return give;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region ��ѯ�û�������¼�
        /// <summary>
        /// ��ѯ�û�������¼�
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<I4GiveU> GetI4GiveUList(Guid id, PagePagination page)
        {
            try
            {
                string SQLstr = @"SELECT *  FROM ZL_Sns_I4GiveU where ConfessID=@ID ";
                if (page != null)
                {
                    SQLstr = page.PaginationSql(SQLstr);
                }
                SqlParameter[] sp ={ new SqlParameter("@ID", id) };
                List<I4GiveU> list = new List<I4GiveU>();
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, SQLstr, sp))
                {
                    while (dr.Read())
                    {
                        I4GiveU give = new I4GiveU();
                        ReadI4GiveU(dr, give);
                        list.Add(give);
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

        #region �����û���ѯ���������������
        /// <summary>
        /// �����û���ѯ���������������
        /// </summary>
        /// <param name="id"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public static List<UserTable> GetConFess(Guid id, PagePagination page)
        {
            try
            {
                string SQLstr = @"select a.aa, b.UserID,b.UserName,b.Userpic from 
(select count(*) as aa,ConfessID from ZL_Sns_I4GiveU group by ConfessID ) as a
 inner join ZL_Sns_UserTable b on a.ConfessID=b.UserID
where a.ConfessID in (select FriendID from ZL_Sns_Userfriend where 
HostID=@HostID)  ";
                if (page != null)
                {
                    SQLstr = page.PaginationSql(SQLstr);
                }
                SqlParameter[] sp ={ new SqlParameter("@HostID", id) };
                List<UserTable> list = new List<UserTable>();
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, SQLstr, sp))
                {
                    while (dr.Read())
                    {
                        UserTable ut = new UserTable();
                        ut.Userpic = dr["Userpic"].ToString();
                        ut.UserName = dr["UserName"].ToString();
                        ut.UserID = (Guid)dr["UserID"];
                        list.Add(ut);
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

        #region �û��ĺ�����������ڵ���
        /// <summary>
        /// �û��ĺ�����������ڵ���
        /// </summary>
        /// <param name="id"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public static List<UserTable> GetFriendNewConfess(Guid id, PagePagination page)
        {
            try
            {
                string SQLstr = @"select a.ConfessTime,b.UserID,b.UserName,b.Userpic from ZL_Sns_UserTable b inner join ZL_Sns_I4GiveU a 
on a.ConfessID=b.UserID where a.ConfessID in (select FriendID from ZL_Sns_Userfriend where 
HostID=@HostID)  ";
                if (page != null)
                {
                    SQLstr = page.PaginationSql(SQLstr);
                }
                SqlParameter[] sp ={ new SqlParameter("@HostID", id) };
                List<UserTable> list = new List<UserTable>();
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, SQLstr, sp))
                {
                    while (dr.Read())
                    {
                        UserTable ut = new UserTable();
                        ut.Userpic = dr["Userpic"].ToString();
                        ut.UserName = dr["UserName"].ToString();
                        ut.UserID = (Guid)dr["UserID"];
                        list.Add(ut);
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

        #region ��ȡ�����Ϣ
        /// <summary>
        /// ��ȡ�����Ϣ
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="piccritique"></param>
        public static void ReadI4GiveU(SqlDataReader dr, I4GiveU give)
        {
            give.ID = (Guid)dr["ID"];
            give.ByConfessID = dr["ByConfessID"].ToString();
            give.ConfessTitle = dr["ConfessTitle"].ToString();
            give.ConfessContext = dr["ConfessContext"].ToString();
            give.ConfessID = (Guid)dr["ConfessID"];
            give.ConfessTime = dr["ConfessTime"] is DBNull ? new DateTime() : DateTime.Parse(dr["ConfessTime"].ToString());
            give.ConfessType = Convert.ToInt32(dr["ConfessType"].ToString());
        }
        #endregion

    }
}
