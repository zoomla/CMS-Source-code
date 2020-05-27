using System;
using System.Collections.Generic;
using System.Text;
using BDUModel;
using System.Data.SqlClient;
using System.Data;
using ZoomLa.SQLDAL;
namespace BDULogic
{
    public class BooDouSystem_Logic
    {

        #region ���ֵ
        /// <summary>
        /// ���ֵ
        /// </summary>
        /// <param name="boodou"></param>
        public static void  Add(int id)
        {
            string SQLstr = "insert into ZL_BooDouSystem (ID) values (@ID)";
            SqlParameter[] sp ={
            new SqlParameter("@ID",id)
            };
            SqlHelper.ExecuteNonQuery(CommandType.Text, SQLstr, sp);
        }
        #endregion

        #region �޸�ֵ
        /// <summary>
        /// �޸�ֵ
        /// </summary>
        /// <param name="boodou"></param>
        public static void Update(BooDouSystem boodou)
        {
            string SQLstr = "update ZL_BooDouSystem set BDValue=@BDValue,BDKey=@BDKey where ID=@ID";
            SqlParameter[] sp={
            new SqlParameter("@BDValue",boodou.BDValue),
            new SqlParameter("@BDKey",boodou.BDKey),
            new SqlParameter("@ID",boodou.ID)
            };
            SqlHelper.ExecuteNonQuery(CommandType.Text, SQLstr, sp);
        }
        #endregion

        #region ��ѯ����ϵͳ����
        /// <summary>
        /// ��ѯ����ϵͳ����
        /// </summary>
        /// <returns></returns>
        public static List<BooDouSystem> GetSystem(int minindex,int maxindex)
        {
            try
            {
                string sql = @"select * from ZL_BooDouSystem where ID>@MinIndex and ID<@MaxIndex and BDKey is not null ";
                SqlParameter[] sp ={ new SqlParameter("@MinIndex", minindex), new SqlParameter("@MaxIndex",maxindex) };
                List<BooDouSystem> list = new List<BooDouSystem>();
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sql,sp))
                {
                    while (dr.Read())
                    {
                        BooDouSystem uf = new BooDouSystem();
                        ReadSystemTable(dr, uf);
                        list.Add(uf);
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

        #region ����ID��ѯ
        public static BooDouSystem GetBooDou(int id)
        {
            try
            {
                BooDouSystem uf = new BooDouSystem();
                string sql = "select * from ZL_BooDouSystem where ID=@ID ";
                SqlParameter[] sp ={new SqlParameter("@ID", id) };
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sql, sp))
                {
                    while (dr.Read())
                    {
                        ReadSystemTable(dr, uf);
                    }
                }
                return uf;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region ����ָ���ļ���ID��ѯϵͳ����
        /// <summary>
        /// ����ָ���ļ���ID��ѯϵͳ����
        /// </summary>
        /// <returns></returns>
        public static List<BooDouSystem> GetSystem(string id)
        {
            try
            {
                string[] arry = id.Split(',');
                string sql = "select * from ZL_BooDouSystem where ID in (";
                SqlParameter[] parameter = new SqlParameter[arry.Length];
                for (int i = 0; i < arry.Length; i++)
                {
                    sql = sql + "@UserID" + i.ToString() + ",";
                    parameter[i] = new SqlParameter("@UserID" + i.ToString(), arry[i]);
                }
                if (sql.EndsWith(","))
                    sql = sql.Substring(0, sql.Length - 1) + ")";
                else
                    sql = sql + ")";
                List<BooDouSystem> list = new List<BooDouSystem>();
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sql))
                {
                    while (dr.Read())
                    {
                        BooDouSystem uf = new BooDouSystem();
                        ReadSystemTable(dr, uf);
                        list.Add(uf);
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

        #region ��ȡϵͳ������
        /// <summary>
        /// ��ȡϵͳ������
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="ut"></param>
        private static void ReadSystemTable(SqlDataReader dr, BooDouSystem boodou)
        {
            boodou.ID = int.Parse(dr["ID"].ToString());
            boodou.BDKey = dr["BDKey"].ToString();
            boodou.BDValue = dr["BDValue"].ToString();
        }
        #endregion
    }
}
