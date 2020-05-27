using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using BDUModel;
using System.Data;
using ZoomLa.SQLDAL;
namespace BDULogic
{
    public class MoneyFlux_Logic
    {
        #region 添加现金流量信息
        /// <summary>
        /// 添加现金流量信息
        /// </summary>
        /// <param name="flux"></param>
        public static void Add(MoneyFlux flux)
        {

            string SQLstr = @"INSERT INTO MoneyFlux ([UserID],[Occurtype],[MoneyQuantity],[OccurTime],[GolID])
     VALUES (@UserID,@Occurtype,@MoneyQuantity,@OccurTime,@GolID)";
            SqlParameter[] sp ={
            new SqlParameter("@UserID",flux.UserID),
            new SqlParameter("@Occurtype",flux.Occurtype),
            new SqlParameter("@MoneyQuantity",flux.MoneyQuantity),
            new SqlParameter("@OccurTime",flux.OccurTime),
            new SqlParameter("@GolID",flux.GolID)
            };

            SqlHelper.ExecuteNonQuery(CommandType.Text, SQLstr, sp);
        }
        #endregion

        #region 根据用户ID查询用户现金流量
        /// <summary>
        /// 根据用户ID查询用户现金流量
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static List<MoneyFlux> GetAllMoneyFlux(Guid userid,BDUModel.PagePagination page)
        {
            try
            {
                string sql = @"select * from MoneyFlux where UserID=@UserID";
                if (page != null)
                {
                    sql = page.PaginationSql(sql);
                }
                SqlParameter[] parameter ={
                new SqlParameter("@UserID",userid)};
                List<MoneyFlux> list = new List<MoneyFlux>();
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sql, parameter))
                {
                    while (dr.Read())
                    {
                        MoneyFlux uf = new MoneyFlux();
                        ReadMoneyFluxTable(dr, uf);
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

        #region 读取现金流量表数据
        /// <summary>
        /// 读取现金流量表数据
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="mf"></param>
        public static void ReadMoneyFluxTable(SqlDataReader dr, MoneyFlux mf)
        {
            mf.UserID = (Guid)dr["UserID"];
            mf.Occurtype =Convert.ToInt32(dr["Occurtype"].ToString());
            mf.MoneyQuantity =Convert.ToDecimal(dr["MoneyQuantity"].ToString());
            mf.GolID = (Guid)dr["GolID"];
            //mf.Note=dr[""].ToString();
            mf.OccurTime = dr["OccurTime"] is DBNull ? new DateTime() : DateTime.Parse(dr["OccurTime"].ToString());
        }
        #endregion

        #region 根据用户ID查询流入流出兑换信息
        /// <summary>
        ///  根据用户ID查询流入流出兑换信息
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static string[] GetAll(Guid userid)
        {
            string SQLstr = @"Select sum(case when Occurtype=@Afflux then MoneyQuantity else 0 end),
                sum(case when Occurtype=@Pour then MoneyQuantity else 0 end),
                sum(case when Occurtype=@DBChange then MoneyQuantity else 0 end) 
                From MoneyFlux where Userid=@Userid";

            SqlParameter[] sp ={
                new SqlParameter("@Userid",userid),
                new SqlParameter("@Afflux",MoneyFluxType.Afflux),
                new SqlParameter("@Pour",MoneyFluxType.Pour),
                new SqlParameter("@DBChange",MoneyFluxType.DBChange)
            };
            string[] str=new string [3];
            using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, SQLstr, sp))
            {
                while (dr.Read())
                {
                    str[0] = dr[0].ToString();
                    str[1] = dr[1].ToString();
                    str[2] = dr[2].ToString();
                }
            }
            return str;
        }
        #endregion

        #region 根据用户编号和类型编号查询总值
        /// <summary>
        /// 根据用户编号和类型编号查询总值
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="typeid"></param>
        /// <returns></returns>
        public static int GetTypeMoney(Guid userid, MoneyFluxType typeid)
        {
            string SQLstr = @"select sum(MoneyQuantity) from MoneyFlux where Userid=@Userid and Occurtype=@Occurtype";
            SqlParameter[] sp ={
            new SqlParameter("@Userid",userid),
            new SqlParameter("@Occurtype",typeid)
            };
            int money=0;
            using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, SQLstr, sp))
            {
                while (dr.Read())
                {
                    money=Convert.ToInt32(dr[0].ToString());
                }
            }
            return money;
        }

        #endregion

        #region 根据用户编号,类型编号和日期查询总值
        /// <summary>
        /// 根据用户编号,类型编号和日期查询总值
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="typeid"></param>
        /// <returns></returns>
        public static int GetTypeMoney(Guid userid, MoneyFluxType typeid, DateTime starttime, DateTime endtime)
        {
            string SQLstr = @"select sum(MoneyQuantity) from MoneyFlux where Userid=@Userid and Occurtype=@Occurtype and OccurTime>@StartTime and OccurTime<@endTime";
            SqlParameter[] sp ={
            new SqlParameter("@Userid",userid),
            new SqlParameter("@Occurtype",typeid),
            new SqlParameter("@StartTime",starttime),
            new SqlParameter("@endTime",endtime)
            };
            int money=0;
            using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, SQLstr, sp))
            {
                while (dr.Read())
                {
                    money = Convert.ToInt32(dr[0].ToString());
                }
            }
            return money;
        }

        #endregion
    }
}
