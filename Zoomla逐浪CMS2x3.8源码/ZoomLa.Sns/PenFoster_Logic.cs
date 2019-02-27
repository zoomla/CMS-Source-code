using System;
using System.Collections.Generic;
using System.Text;
using BDUModel;
using System.Data.SqlClient;
using System.Data;
using ZoomLa.SQLDAL;

namespace BDULogic
{
    public class PenFoster_Logic
    {
        #region 添加宠物项目信息
        /// <summary>
        /// 添加宠物项目信息
        /// </summary>
        /// <param name="foster"></param>
        public static void Add(PenFoster foster)
        {
            foster.ID = Guid.NewGuid();
            string SQLstr = @"INSERT INTO [PenFoster]([ID],[FosterItem],[FosterSynopsis],[FosterMoney],[FosterExperience],[FosterImage]
,[FosterTime],[FostertPayment])VALUES (@ID,@FosterItem,@FosterSynopsis,@FosterMoney,@FosterExperience,@FosterImage
,@FosterTime,@FostertPayment)";

            SqlParameter[] sp ={
                new SqlParameter("@ID",foster.ID),
                new SqlParameter("@FosterItem",foster.FosterItem),
                new SqlParameter("@FosterSynopsis",foster.FosterSynopsis),
                new SqlParameter("@FosterMoney",foster.FosterMoney),
                new SqlParameter("@FosterExperience",foster.FosterExperience),
                new SqlParameter("@FosterImage",foster.FosterImage),
                new SqlParameter("@FosterTime",foster.FosterTime),
                new SqlParameter("@FostertPayment",foster.FostertPayment)
            };
            SqlHelper.ExecuteNonQuery(CommandType.Text, SQLstr, sp);
        }
        #endregion

        #region 修改宠物项目信息
        /// <summary>
        /// 修改宠物项目信息
        /// </summary>
        /// <param name="foster"></param>
        public static void Update(PenFoster foster)
        {
            string SQLstr = @"UPDATE PenFoster SET [FosterSynopsis] = @FosterSynopsis,[FosterMoney] = @FosterMoney,[FosterExperience] = @FosterExperience,[FosterImage] = @FosterImage,[FosterTime] = @FosterTime,[FostertPayment]=@FostertPayment
 WHERE [ID] = @ID";

            SqlParameter[] sp ={
                new SqlParameter("@ID",foster.ID),
                new SqlParameter("@FosterSynopsis",foster.FosterSynopsis),
                new SqlParameter("@FosterMoney",foster.FosterMoney),
                new SqlParameter("@FosterExperience",foster.FosterExperience),
                new SqlParameter("@FosterImage",foster.FosterImage),
                new SqlParameter("@FosterTime",foster.FosterTime),
                new SqlParameter("@FostertPayment",foster.FostertPayment)
            };
            SqlHelper.ExecuteNonQuery(CommandType.Text, SQLstr, sp);
        }
        #endregion

        #region 删除项目信息
        /// <summary>
        /// 删除项目信息
        /// </summary>
        /// <param name="id"></param>
        public static void Del(Guid id)
        {
            string SQLstr = @"DELETE FROM PenFoster WHERE [ID] = @ID";

            SqlParameter[] sp ={
                new SqlParameter("@ID",id)};
            SqlHelper.ExecuteNonQuery(CommandType.Text, SQLstr, sp);
        }
        #endregion

        #region 根据ID查找项目信息
        /// <summary>
        /// 根据ID查找项目信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static PenFoster GetPenFoster(Guid id)
        {
            try
            {
                PenFoster pen = new PenFoster();
                string SQLstr = @"SELECT *  FROM PenFoster where ID=@ID";

                SqlParameter[] sp ={ new SqlParameter("@ID", id) };

                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, SQLstr, sp))
                {
                    if (dr.Read())
                    {
                        ReadPenFoster(dr, pen);
                    }
                }
                return pen;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 查询所有项目
        /// <summary>
        /// 查询所有项目
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public static List<PenFoster> GetAllPenFoster(PagePagination page)
        {
            try
            {
                string SQLstr = @"SELECT *  FROM PenFoster ";
                if (page != null)
                {
                    SQLstr = page.PaginationSql(SQLstr);
                }
                List<PenFoster> list = new List<PenFoster>();
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, SQLstr))
                {
                    while (dr.Read())
                    {
                        PenFoster pen = new PenFoster();
                        ReadPenFoster(dr, pen);
                        list.Add(pen);
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

        #region 读取宠物项目信息
        /// <summary>
        /// 读取宠物项目信息
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="piccritique"></param>
        public static void ReadPenFoster(SqlDataReader dr, PenFoster foster)
        {
            foster.ID = (Guid)dr["ID"];
            foster.FosterItem = dr["FosterItem"].ToString();
            foster.FosterSynopsis = dr["FosterSynopsis"].ToString();
            foster.FosterImage = dr["FosterImage"].ToString();
            foster.FosterExperience = Convert.ToInt32(dr["FosterExperience"]);
            foster.FosterMoney = Convert.ToInt32(dr["FosterMoney"]);
            foster.FosterTime = Convert.ToInt32(dr["FosterTime"]);
            foster.FostertPayment = Convert.ToInt32(dr["FostertPayment"]);
        }
        #endregion
    }
}
