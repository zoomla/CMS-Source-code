using System;
using System.Collections.Generic;
using System.Text;
using BDUModel;
using System.Data.SqlClient;
using System.Data;
using ZoomLa.SQLDAL;

namespace BDULogic
{
    public class SystemBooDouChance_Logic
    {
        #region 添加布兜运气事件
        /// <summary>
        /// 添加布兜运气事件
        /// </summary>
        /// <param name="boodou"></param>
        public static void Add(SystemBooDouChance boodou)
        {
            boodou.ID = Guid.NewGuid();
            string SQLstr = @"INSERT INTO SystemBooDouChance ([ID],[BooDouType],[BooDouNum],[BooDouText],[EncouragementType])
     VALUES (@ID,@BooDouType,@BooDouNum,@BooDouText,@EncouragementType)";
            SqlParameter[] sp ={ 
                new SqlParameter("@ID", boodou.ID),
                new SqlParameter("@BooDouType",boodou.BooDouType),
                new SqlParameter("@BooDouNum",boodou.BooDouNum),
                new SqlParameter("@BooDouText",boodou.BooDouText),
                new SqlParameter("@EncouragementType",boodou.EncouragementType)
            };
            SqlHelper.ExecuteNonQuery(CommandType.Text, SQLstr, sp);
        }
        #endregion

        #region 修改布兜运气事件
        /// <summary>
        /// 修改布兜运气事件
        /// </summary>
        /// <param name="boodou"></param>
        public static void Update(SystemBooDouChance boodou)
        {
            string SQLstr = @"UPDATE SystemBooDouChance SET [BooDouType] = @BooDouType,[BooDouNum] = @BooDouNum,[BooDouText] = @BooDouText,[EncouragementType] = @EncouragementType WHERE ID=@ID";
            SqlParameter[] sp ={ 
                new SqlParameter("@ID", boodou.ID),
                new SqlParameter("@BooDouType",boodou.BooDouType),
                new SqlParameter("@BooDouNum",boodou.BooDouNum),
                new SqlParameter("@BooDouText",boodou.BooDouText),
                new SqlParameter("@EncouragementType",boodou.EncouragementType)
            };
            SqlHelper.ExecuteNonQuery(CommandType.Text, SQLstr, sp);
        }
        #endregion

        #region 删除布兜运气事件
        /// <summary>
        /// 删除布兜事件
        /// </summary>
        /// <param name="id"></param>
        public static void Del(Guid id)
        {
            string SQLstr = @"DELETE FROM SystemBooDouChance WHERE ID=@ID";
            SqlParameter[] sp ={new SqlParameter("@ID",id) };
            SqlHelper.ExecuteNonQuery(CommandType.Text, SQLstr, sp);
            
        }
        #endregion

        #region 查询单条布兜运气事件
        /// <summary>
        /// 查询单条布兜运气事件
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static SystemBooDouChance GetBooDouChance(Guid id)
        {
            try
            {
                SystemBooDouChance boodou = new SystemBooDouChance();
                string SQLstr = @"SELECT *  FROM SystemBooDouChance WHERE ID=@ID";

                SqlParameter[] sp ={ new SqlParameter("@ID", id) };

                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, SQLstr, sp))
                {
                    if (dr.Read())
                    {
                        ReadBooDouChance(dr, boodou);
                    }
                }
                return boodou;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 查询所有布兜运气事件
        /// <summary>
        /// 查询所有布兜运气事件
        /// </summary>
        /// <returns></returns>
        public static List<SystemBooDouChance> GetAllBooDouChance()
        {
            try
            {
                string SQLstr = @"SELECT *  FROM SystemBooDouChance";
                
                List<SystemBooDouChance> list = new List<SystemBooDouChance>();
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, SQLstr))
                {
                    while (dr.Read())
                    {
                        SystemBooDouChance boodou = new SystemBooDouChance();
                        ReadBooDouChance(dr, boodou);
                        list.Add(boodou);
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

        #region 读取布兜运气事件信息
        /// <summary>
        /// 读取布兜运气事件信息
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="piccritique"></param>
        public static void ReadBooDouChance(SqlDataReader dr, SystemBooDouChance boodou)
        {
            boodou.ID =(Guid)dr["ID"];
            boodou.BooDouNum = Convert.ToInt32(dr["BooDouNum"]);
            boodou.BooDouText = dr["BooDouText"].ToString();
            boodou.BooDouType = Convert.ToInt32(dr["BooDouType"]);
            boodou.EncouragementType = Convert.ToInt32(dr["EncouragementType"]);
        }
        #endregion
    }
}
