using System;
using System.Collections.Generic;
using System.Text;
using BDUModel;
using System.Data.SqlClient;
using System.Data;
using ZoomLa.SQLDAL;

namespace BDULogic
{
    public class I4GiveUType_Logic
    {
        #region 添加忏悔类别
        /// <summary>
        /// 添加忏悔类别
        /// </summary>
        /// <param name="give"></param>
        public static void Add(I4GiveUType give)
        {
            give.ID = Guid.NewGuid();
            string SQLstr = @"INSERT INTO ZL_Sns_I4GiveUType ([ID],[TyepName])VALUES(@ID,@TyepName)";
            SqlParameter[] sp ={
                new SqlParameter("@ID",give.ID),
                new SqlParameter("@TyepName",give.TyepName)
            };
            SqlHelper.ExecuteNonQuery(CommandType.Text, SQLstr, sp);
        }
        #endregion

        #region 修改忏悔类别
        /// <summary>
        /// 修改忏悔类别
        /// </summary>
        /// <param name="give"></param>
        public static void Update(I4GiveUType give)
        {
            give.ID = Guid.NewGuid();
            string SQLstr = @"UPDATE [ZL_Sns_I4GiveUType] SET [TyepName] = @TyepName WHERE [ID] = @ID";
            SqlParameter[] sp ={
                new SqlParameter("@ID",give.ID),
                new SqlParameter("@TyepName",give.TyepName)
            };
            SqlHelper.ExecuteNonQuery(CommandType.Text, SQLstr, sp);
        }
        #endregion

        #region 删除忏悔类别
        /// <summary>
        /// 删除忏悔类别
        /// </summary>
        /// <param name="give"></param>
        public static void Del(Guid id)
        {
            string SQLstr = @"DELETE FROM [ZL_Sns_I4GiveUType] WHERE [ID] = @ID";
            SqlParameter[] sp ={
                new SqlParameter("@ID",id)
            };
            SqlHelper.ExecuteNonQuery(CommandType.Text, SQLstr, sp);
        }
        #endregion

        #region
        public static List<I4GiveUType> GetType(Guid id)
        {
            try
            {
                string SQLstr = @"select * from  I4GiveUType where ID=@ID";

                SqlParameter[] sp ={ new SqlParameter("@ID", id) };
                List<I4GiveUType> list = new List<I4GiveUType>();
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, SQLstr, sp))
                {
                    while (dr.Read())
                    {
                        I4GiveUType ut = new I4GiveUType();
                        //ut.ID = dr["Userpic"].ToString();
                        //ut.UserName = dr["UserName"].ToString();
                        //ut.UserID = (Guid)dr["UserID"];
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
