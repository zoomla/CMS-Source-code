using System;
using System.Collections.Generic;
using System.Text;
using BDUModel;
using System.Data;
using System.Data.SqlClient;
using ZoomLa.SQLDAL;

namespace BDULogic
{
    public class SystemPen_Logic
    {
        #region 添加宠物
        /// <summary>
        /// 添加宠物
        /// </summary>
        /// <param name="pen"></param>
        public static void Add(SystemPen pen)
        {
            pen.ID = Guid.NewGuid();
            string SQLstr = @"
                        INSERT INTO [SystemPen] (
	                        [ID],
	                        [PenName],
	                        [PenImage],
	                        [PenInitialization],
	                        [PenPrice],
	                        [PenContext],
	                        [PenState],
	                        [Marker]
                        ) VALUES (
	                        @ID,
	                        @PenName,
	                        @PenImage,
	                        @PenInitialization,
	                        @PenPrice,
	                        @PenContext,
	                        @PenState,
	                        @Marker
                        )
                            ";

            SqlParameter[] sp ={
            new SqlParameter("@ID",pen.ID),
                new SqlParameter("@PenName",pen.PenName),
                new SqlParameter("@PenImage",pen.PenImage),
                new SqlParameter("@PenInitialization",pen.PenInitialization),
                new SqlParameter("@PenPrice",pen.PenPrice),
                new SqlParameter("@PenContext",pen.PenContext),
                new SqlParameter("@PenState",pen.PenState ),
                new SqlParameter("@Marker",pen.Marker )
            };
            SqlHelper.ExecuteNonQuery(CommandType.Text, SQLstr, sp);

        }
        #endregion

        #region 修改宠物信息
        /// <summary>
        /// 修改宠物信息
        /// </summary>
        /// <param name="pen"></param>
        public static void Update(SystemPen pen)
        {
            string SQLstr = @"UPDATE SystemPen SET [PenImage] = @PenImage,
[PenInitialization] = @PenInitialization,[PenPrice] = @PenPrice,
[PenContext] = @PenContext,[PenState]=@PenState WHERE [ID] = @ID";

            SqlParameter[] sp ={
            new SqlParameter("@ID",pen.ID),
                new SqlParameter("@PenImage",pen.PenImage),
                new SqlParameter("@PenInitialization",pen.PenInitialization),
                new SqlParameter("@PenPrice",pen.PenPrice),
                new SqlParameter("@PenContext",pen.PenContext),
                new SqlParameter("@PenState",pen.PenState )
            };
            SqlHelper.ExecuteNonQuery(CommandType.Text, SQLstr, sp);

        }
        #endregion

        #region 单条查询宠物信息
        /// <summary>
        /// 单条查询宠物信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static SystemPen GetPen(Guid id)
        {
            try
            {
                SystemPen pen = new SystemPen();
                string SQLstr = @"SELECT *  FROM SystemPen where ID=@ID";

                SqlParameter[] sp ={ new SqlParameter("@ID", id) };

                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, SQLstr, sp))
                {
                    if (dr.Read())
                    {
                        ReadSystemPen(dr, pen);
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

        #region 查询宠物信息列表
        /// <summary>
        /// 查询宠物信息列表
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public static List<SystemPen> GetAllSystemPen(int state, PagePagination page)
        {
            try
            {
                string SQLstr = @"SELECT *  FROM SystemPen ";
                switch (state)
                {
                    case 0:
                        SQLstr = SQLstr + " where PenState=0";
                        break;
                    case 1:
                        SQLstr = SQLstr + " where PenState=1";
                        break;
                    default :
                        break;
                }
                if (page != null)
                {
                    SQLstr = page.PaginationSql(SQLstr);
                }
                List<SystemPen> list = new List<SystemPen>();
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, SQLstr))
                {
                    while (dr.Read())
                    {
                        SystemPen pen = new SystemPen();
                        ReadSystemPen(dr, pen);
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

        #region 读取宠物信息
        /// <summary>
        /// 读取宠物信息
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="piccritique"></param>
        public static void ReadSystemPen(SqlDataReader dr, SystemPen pen)
        {
            pen.ID = (Guid)dr["ID"];
            pen.PenName = dr["PenName"].ToString();
            pen.PenImage = dr["PenImage"].ToString();
            pen.PenContext = dr["PenContext"].ToString();
            pen.PenInitialization = dr["PenInitialization"] is DBNull ? 0 : Convert.ToInt32(dr["PenInitialization"]);
            pen.PenPrice =dr["PenPrice"] is DBNull?0: Convert.ToInt32(dr["PenPrice"]);
            pen.PenState =dr["PenState"] is DBNull? 0: Convert.ToInt32(dr["PenState"]);
            pen.Marker = dr["Marker"].ToString();
        }
        #endregion
    }
}
