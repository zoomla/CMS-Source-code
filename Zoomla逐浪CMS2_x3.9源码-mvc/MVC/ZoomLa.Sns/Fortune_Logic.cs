using System;
using System.Collections.Generic;
using System.Text;
using BDUModel;
using System.Data.SqlClient;
using System.Data;
using ZoomLa.SQLDAL;
namespace BDULogic
{
    public class Fortune_Logic
    {
        #region ����û��Ƹ���Ϣ
        /// <summary>
        /// ����û��Ƹ���Ϣ
        /// </summary>
        /// <param name="fortune"></param>
        public static void Add(Guid id)
        {
            string SQLstr = @"INSERT INTO ZL_Sns_Fortune([UserID],[BooDouMoney],[Cash],[EnergyMoney],[ConsumeMoney])
     select @UserID,a=max(case when ID='5' then BDValue else 0 end),b=max(case when ID='6' then BDValue else 0 end),
c=max(case when ID='7' then BDValue else 0 end),0 from BooDouSystem ";

            SqlParameter[] sp ={
            new SqlParameter("@UserID",id)
            };

            SqlHelper.ExecuteNonQuery(CommandType.Text, SQLstr, sp);

        }
        #endregion

        #region �޸ĲƸ���Ϣ
        /// <summary>
        /// �޸ĲƸ���Ϣ
        /// </summary>
        /// <param name="fortune"></param>
        public static void UpdateMoney(Fortune fortune)
        {
            string SQLstr = @"update ZL_Sns_Fortune set ConsumeMoney=@ConsumeMoney,EnergyMoney=@EnergyMoney,BooDouMoney=@BooDouMoney,Cash=@Cash where UserID=@UserID";

            SqlParameter[] sp ={ 
                new SqlParameter("@ConsumeMoney",fortune.ConsumeMoney),
                new SqlParameter("@EnergyMoney",fortune.EnergyMoney),
                new SqlParameter("@BooDouMoney",fortune.BooDouMoney),
                new SqlParameter("@Cash",fortune.Cash),
                new SqlParameter("@UserID",fortune.UserID) 
            };
            SqlHelper.ExecuteNonQuery(CommandType.Text, SQLstr, sp);

        }
        #endregion

        #region �޸����ľ���Ƹ�
        /// <summary>
        /// �޸����ľ���Ƹ�
        /// </summary>
        /// <param name="Userid">�û�ID</param>
        /// <param name="money">�ֽ�</param>
        public static void UpdateConsumeMoney(Guid Userid, decimal money)
        {
            string SQLstr = @"update ZL_Sns_Fortune set ConsumeMoney=@ConsumeMoney,EnergyMoney=@EnergyMoney,BooDouMoney=@BooDouMoney,Cash=@Cash where UserID=@UserID";

            SqlParameter[] sp ={ new SqlParameter("@ConsumeMoney", money), new SqlParameter("@UserID", Userid) };
            SqlHelper.ExecuteNonQuery(CommandType.Text, SQLstr, sp);
        }
        #endregion

        #region �޸ľ���Ƹ�
        /// <summary>
        /// �޸ľ���Ƹ�
        /// </summary>
        /// <param name="Userid">�û�ID</param>
        /// <param name="money">�ֽ�</param>
        public static void UpdateEnergyMoney(Guid Userid, decimal money)
        {
            string SQLstr = @"update ZL_Sns_Fortune set EnergyMoney=@EnergyMoney where UserID=@UserID";

            SqlParameter[] sp ={ new SqlParameter("@EnergyMoney", money), new SqlParameter("@UserID", Userid) };
            SqlHelper.ExecuteNonQuery(CommandType.Text, SQLstr, sp);
        }
        #endregion

        #region �޸��ֽ�
        /// <summary>
        /// �޸��ֽ�
        /// </summary>
        /// <param name="Userid">�û�ID</param>
        /// <param name="money">�ֽ�</param>
        public static void UpdateCash(Guid Userid, decimal money)
        {
            string SQLstr = @"update ZL_Sns_Fortune set Cash=@Cash where UserID=@UserID";

            SqlParameter[] sp ={ new SqlParameter("@Cash", money), new SqlParameter("@UserID", Userid) };
            SqlHelper.ExecuteNonQuery(CommandType.Text, SQLstr, sp);
        }
        #endregion

        #region �޸Ķ���
        /// <summary>
        /// �޸Ķ���
        /// </summary>
        /// <param name="Userid">�û�ID</param>
        /// <param name="money">����</param>
        public static void UpdateBooDouMoney(Guid Userid, decimal money)
        {
            string SQLstr = @"update ZL_Sns_Fortune set BooDouMoney=@BooDouMoney where UserID=@UserID";

            SqlParameter[] sp ={ new SqlParameter("@BooDouMoney", money), new SqlParameter("@UserID", Userid) };
            SqlHelper.ExecuteNonQuery(CommandType.Text, SQLstr, sp);
        }
        #endregion

        #region �����û�ID��ѯ�Ƹ����
        /// <summary>
        /// �����û�ID��ѯ�Ƹ����
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static Fortune GetFortune(Guid userid)
        {
            try
            {
                string SQLstr = @"select Top 1 * from [ZL_Sns_Fortune] where UserID=@UserID";
                SqlParameter[] sp ={
                new SqlParameter("@UserID",userid)};
                Fortune fortune = new Fortune();
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, SQLstr, sp))
                {
                    if (dr.Read())
                    {
                        ReadFortune(dr, fortune);
                    }
                }
                return fortune;

            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region ��ȡ�Ƹ�������
        /// <summary>
        /// ��ȡ�Ƹ�������
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="ut"></param>
        public static void ReadFortune(SqlDataReader dr, Fortune fortune)
        {
            fortune.UserID = (Guid)dr["UserID"];
            fortune.BooDouMoney = Convert.ToInt32(dr["BooDouMoney"].ToString());
            fortune.Cash = Convert.ToInt32(dr["Cash"].ToString());
            fortune.EnergyMoney = Convert.ToInt32(dr["EnergyMoney"].ToString());
            fortune.ConsumeMoney = Convert.ToInt32(dr["ConsumeMoney"].ToString());
        }
        #endregion

        #region �����û��Ƹ����а�
        /// <summary>
        /// �����û��Ƹ����а�
        /// </summary>
        /// <param name="fortunetype"></param>
        /// <returns></returns>
        public static List<UserTable> GetAllFortuneOrder(int fortunetype, PagePagination page)
        {
            try
            {
                string str = "";
                switch (fortunetype)
                {
                    case 1:
                        str = "BooDouMoney+(Cash*10)+(EnergyMoney-ConsumeMoney)*100";
                        break;
                    case 2:
                        str = "BooDouMoney";
                        break;
                    case 3:
                        str = "BooDouMoney+(Cash*10)";
                        break;
                    case 4:
                        str = "(Cash*10)+(EnergyMoney-ConsumeMoney)*100";
                        break;
                }
                string SQLstr = @"select a.aa,b.UserName from 
(select sum(" + str + @") as aa,UserID from ZL_Sns_Fortune group by UserID ) 
as a inner join ZL_Sns_UserTable b on a.UserID=b.UserID";
                if (page != null)
                {
                    SQLstr = page.PaginationSql(SQLstr);
                }
                List<UserTable> list = new List<UserTable>();
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, SQLstr))
                {
                    while (dr.Read())
                    {
                        UserTable ut = new UserTable();
                        ut.UserName = dr["UserName"].ToString();
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

        #region ��ѯ�����û������а�
        /// <summary>
        /// ��ѯ�����û������а�
        /// </summary>
        /// <param name="fortunetype"></param>
        /// <param name="hostid"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public static List<UserTable> GetAllFortuneOrder(int fortunetype, Guid hostid, PagePagination page)
        {
            try
            {
                string str = "";
                switch (fortunetype)
                {
                    case 1:
                        str = "BooDouMoney+(Cash*10)+(EnergyMoney-ConsumeMoney)*100";
                        break;
                    case 2:
                        str = "BooDouMoney";
                        break;
                    case 3:
                        str = "BooDouMoney+(Cash*10)";
                        break;
                    case 4:
                        str = "(Cash*10)+(EnergyMoney-ConsumeMoney)*100";
                        break;
                }
                string SQLstr = @"select a.aa,b.UserName from (select sum(" + str + @") as aa,UserID from 
ZL_Sns_Fortune group by UserID ) as a inner join ZL_Sns_UserTable b on a.UserID=b.UserID where a.ConfessID in 
(select FriendID from ZL_Sns_Userfriend where HostID=@HostID) ";
                if (page != null)
                {
                    SQLstr = page.PaginationSql(SQLstr);
                }
                SqlParameter[] sp ={ new SqlParameter("@HostID", hostid) };
                List<UserTable> list = new List<UserTable>();
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, SQLstr, sp))
                {
                    while (dr.Read())
                    {
                        UserTable ut = new UserTable();
                        ut.UserName = dr["UserName"].ToString();
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
    }
}
