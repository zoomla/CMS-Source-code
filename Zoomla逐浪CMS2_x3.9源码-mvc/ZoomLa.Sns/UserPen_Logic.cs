using System;
using System.Collections.Generic;
using System.Text;
using BDUModel;
using System.Data;
using System.Data.SqlClient;
using ZoomLa.SQLDAL;

namespace BDULogic
{
    public class UserPen_Logic
    {
        #region ����û�������Ϣ
        /// <summary>
        /// ����û�������Ϣ
        /// </summary>
        /// <param name="pen"></param>
        public static void Add(UserPen pen)
        {
            pen.ID = Guid.NewGuid();
            string SQLstr = @"INSERT INTO UserPen ([ID],[UserID],[PenID],[PenName],[BuyDate],[PenGrade],[BuyPrice])
     VALUES (@ID,@UserID,@PenID,@PenName,@BuyDate,@PenGrade,@BuyPrice)";

            SqlParameter[] sp ={ 
                 new SqlParameter("@ID", pen.ID),
                new SqlParameter("@UserID", pen.UserID),
                new SqlParameter("@PenID", pen.PenID), 
                new SqlParameter("@PenName", pen.PenName),
                new SqlParameter("@BuyDate", DateTime.Now),
                new SqlParameter("@PenGrade", pen.PenGrade),
                new SqlParameter("@BuyPrice", pen.BuyPrice),
            };

            SqlHelper.ExecuteNonQuery(CommandType.Text, SQLstr, sp);
        }
        #endregion

        #region �޸ĳ�������
        /// <summary>
        /// �޸ĳ�������
        /// </summary>
        /// <param name="id">����ID</param>
        /// <param name="name">�޸ĵ�����</param>
        public static void UpdateName(Guid id, string name)
        {
            string SQLstr = @"UPDATE UserPen SET [PenName] = @PenName WHERE [UserID]= @UserID";

            SqlParameter[] sp ={ new SqlParameter("@PenName", name), new SqlParameter("@UserID",id ) };

            SqlHelper.ExecuteNonQuery(CommandType.Text, SQLstr, sp);
        
        }
        #endregion

        #region �޸ĳ���ȼ�
        /// <summary>
        /// �޸ĳ���ȼ�
        /// </summary>
        /// <param name="pen"></param>
        public static void UpdateFoster(UserPen pen)
        {
            string SQLstr = @"UPDATE UserPen SET [PenGrade] = @PenGrade,[FosterID] = @FosterID,[ForsterStart] = @ForsterStart WHERE [UserID]= @UserID";

            SqlParameter[] sp ={ 
                new SqlParameter("@PenGrade", pen.PenGrade), 
                new SqlParameter("@FosterID", pen.FosterID),
                new SqlParameter("@ForsterStart", pen.ForsterStart),
                new SqlParameter("@UserID", pen.UserID)
            };

            SqlHelper.ExecuteNonQuery(CommandType.Text, SQLstr, sp);
        }
        #endregion

        #region ת�ó���
        /// <summary>
        /// ת�ó���
        /// </summary>
        /// <param name="pen"></param>
        public static void UpdateUserPen(UserPen pen,Guid BuyUserID)
        {
            string SQLstr = @"UPDATE UserPen SET [UserID]= @BuyUserID,[BuyDate] = @BuyDate,[BuyPrice] = @BuyPrice WHERE [UserID]= @UserID and [PenID]=@PenID";

            SqlParameter[] sp ={ 
                new SqlParameter("@BuyDate", pen.BuyDate), 
                new SqlParameter("@BuyPrice", pen.BuyPrice),
                new SqlParameter("@PenID", pen.PenID),
                new SqlParameter("@UserID", pen.UserID),
                new SqlParameter("@BuyUserID",BuyUserID)
            };

            SqlHelper.ExecuteNonQuery(CommandType.Text, SQLstr, sp);
        }

        #endregion

        #region ��������ID��ѯ������Ϣ
        /// <summary>
        /// ��������ID��ѯ������Ϣ
        /// </summary>
        /// <param name="id">����ID</param>
        /// <returns></returns>
        public static UserPen GetUserPen(Guid id)
        {
            try
            {
                UserPen pen = new UserPen();
                string SQLstr = @"select a.UserID,a.PenID,a.PenName,a.BuyDate,a.PenGrade,a.BuyPrice,a.FosterID,a.ForsterStart
,b.PenImage from UserPen a join SystemPen b on a.PenID=b.ID where a.UserID=@ID and a.PenGrade<>-1";

                SqlParameter[] sp ={ new SqlParameter("@ID", id) };

                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, SQLstr, sp))
                {
                    if (dr.Read())
                    {
                        ReadUserPen(dr, pen);
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


        #region ����ID��ѯ������Ϣ
        /// <summary>
        /// ��������ID��ѯv��Ϣ
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        public static UserPen GetUserCar(Guid id)
        {
            try
            {
                UserPen pen = new UserPen();
                string SQLstr = @"SELECT * FROM UserPen WHERE ID=@ID";

                SqlParameter[] sp ={ new SqlParameter("@ID", id) };

                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, SQLstr, sp))
                {
                    if (dr.Read())
                    {

                        pen.UserID = (Guid)dr["UserID"];
                        pen.ID = (Guid)dr["ID"];
                        pen.PenID = (Guid)dr["PenID"];
                        pen.BuyPrice = Convert.ToInt32(dr["BuyPrice"]);
                        pen.BuyDate = dr["BuyDate"] is DBNull ? new DateTime() : DateTime.Parse(dr["BuyDate"].ToString());
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

        #region ��ȡ�ҵ�����
        /// <summary>
        ///��ȡ�ҵ����� 
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static DataTable GetCarByUserID(Guid userID)
        {
            try
            {
                string sql = @"SELECT UserPen.ID, UserPen.PenID, SystemPen.PenName, SystemPen.PenImage, 
                                  SystemPen.PenPrice, SystemPen.Marker, UserPen.BuyDate
                            FROM SystemPen INNER JOIN
                                  UserPen ON SystemPen.ID = UserPen.PenID where UserPen.UserID=@UserID and  UserPen.PenGrade=-1";
                SqlParameter[] sp ={ new SqlParameter("@UserID", userID) };
                DataTable dt = new DataTable();
                dt = SqlHelper.ExecuteTable(CommandType.Text, sql, sp);
                return dt;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region ��ȡ�û�������Ϣ
        /// <summary>
        /// ��ȡ�û�������Ϣ
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="piccritique"></param>
        public static void ReadUserPen(SqlDataReader dr, UserPen pen)
        {
            pen.UserID = (Guid)dr["UserID"];
            pen.PenName = dr["PenName"].ToString();
            pen.PenImage = dr["PenImage"].ToString();
            pen.PenID = (Guid)dr["PenID"];
            pen.PenGrade = Convert.ToInt32(dr["PenGrade"]);
            if(dr["FosterID"].ToString ()!="")
            pen.FosterID = (Guid)dr["FosterID"];
            pen.BuyPrice = Convert.ToInt32 (dr["BuyPrice"]);
            pen.BuyDate = dr["BuyDate"] is DBNull ? new DateTime() : DateTime.Parse(dr["BuyDate"].ToString());
            pen.ForsterStart = dr["ForsterStart"] is DBNull ? new DateTime() : DateTime.Parse(dr["ForsterStart"].ToString());
        }
        #endregion
    }
}
