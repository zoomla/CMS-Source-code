using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using BDUModel;
using ZoomLa.SQLDAL;

namespace BDULogic
{
    public class LotTestLogic
    {
        #region 添加缘分配对记录
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ln"></param>
        /// <returns></returns>
        public static Guid InsertLot(LotNote ln)
        {
            try
            {
                ln.ID = Guid.NewGuid();
                string sql = @"INSERT INTO [LotNote] (
	[ID],
	[StartID],
	[EndID],
	[MessageID],
	[NoteType]
) VALUES (
	@ID,
	@StartID,
	@EndID,
	@MessageID,
	@NoteType
)";
                SqlParameter[] parameter ={ 
                new SqlParameter("ID",ln.ID),
                    new SqlParameter("StartID",ln.StartID),
                    new SqlParameter("EndID",ln.EndID),
                    new SqlParameter("MessageID",ln.MessageID),
                    new SqlParameter("NoteType",ln.NoteType),
                };
                SqlHelper.ExecuteNonQuery(CommandType.Text, sql, parameter);
                return ln.ID;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 查询记录
        /// <summary>
        /// 查询记录
        /// </summary>
        /// <param name="sID"></param>
        /// <param name="eID"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        public static LotNote CheckLot(Guid sID, Guid eID, int i)
        {
            try
            {
                string sql = @"select * from LotNote where StartID=@sID and EndID=@eID and NoteType=@i";
                SqlParameter[] parameter ={ 
                    new SqlParameter("sID",sID),
                    new SqlParameter("eID",eID),
                    new SqlParameter("i",i)
                };
                LotNote ln = new LotNote();
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sql, parameter))
                {
                    if (dr.Read())
                    {
                        ReadLot(dr, ln);
                    }
                    else
                    {
                        ln.ID = Guid.Empty;
                    }
                }
                return ln ;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 读取表
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="ln"></param>
        public static void ReadLot(SqlDataReader dr, LotNote ln)
        {
            ln.ID = (Guid)dr["ID"];
            ln.StartID = (Guid)dr["StartID"];
            ln.EndID = (Guid)dr["EndID"];
            ln.MessageID = (Guid)dr["MessageID"];
            ln.NoteType = (int)dr["NoteType"];
        }
        #endregion

        #region 读取信息表
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static Guid GetMessageID()
        {
            try
            {
                string sql = @"select top 1 * from LotMessage order by newid()";
                SqlParameter[] parmater ={ };
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sql, parmater))
                {
                    if (dr.Read())
                        return new Guid(dr["ID"].ToString());
                    else
                        return Guid.Empty;
                }
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 读取信息表内容
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static string GetMessage(Guid ID)
        {
            try
            {
                string sql = @"select * from LotMessage where ID=@ID";
                SqlParameter[] parameter ={ new SqlParameter("ID",ID)};
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sql,parameter))
                {
                    if (dr.Read())
                        return dr["MessageContent"].ToString();
                    else
                        return "";
                }
            }
            catch
            {
                throw;
            }
        }
        #endregion
    }
}
