using System;
using System.Collections.Generic;
using System.Text;
using BDUModel;
using System.Data.SqlClient;
using System.Data;
using ZoomLa.SQLDAL;
namespace BDULogic
{
    /// <summary>
    /// PicTure
    /// ��Ƭ���ݷ��ʲ�
    /// </summary>
    public class PicTure_Logic
    {
        #region �����Ƭ
        /// <summary>
        /// �����Ƭ
        /// </summary>
        /// <param name="picture"></param>
        /// <returns></returns>
        public static Guid Add(PicTure picture)
        {
            try
            {
                picture.ID = Guid.NewGuid();
                picture.PicUpTime = System.DateTime.Now;
                string SQLstr = @"INSERT INTO [ZL_Sns_PicTure]([ID],[PicName],[PicUrl],[Remark],[PicCategID],[PicSize],[PicUpTime])
     VALUES (@ID,@PicName,@PicUrl,@Remark,@PicCategID,@PicSize,@PicUpTime)";
                SqlParameter[] sp = new SqlParameter[7];
                sp[0] = new SqlParameter("@ID", picture.ID);
                sp[1] = new SqlParameter("@PicName", picture.PicName);
                sp[2] = new SqlParameter("@PicUrl", picture.PicUrl);
                sp[3] = new SqlParameter("@Remark", picture.Remark);
                sp[4] = new SqlParameter("@PicCategID", picture.PicCategID);
                sp[5] = new SqlParameter("@PicSize", picture.PicSize);
                sp[6] = new SqlParameter("@PicUpTime", picture.PicUpTime);

                SqlHelper.ExecuteNonQuery(CommandType.Text, SQLstr, sp);
                return picture.ID;
            }
            catch
            {
                throw;
            }

        }
        #endregion

        #region �޸���Ƭ��Ϣ
        /// <summary>
        /// �޸���Ƭ��Ϣ
        /// </summary>
        /// <param name="picture"></param>
        /// <returns></returns>
        public static Guid Update(PicTure picture)
        {
            try
            {
                string SQLstr = @"UPDATE [ZL_Sns_PicTure]
                SET [PicName] =@PicName,[Remark] = @Remark
                WHERE [ID] = @ID";

                SqlParameter[] sp ={ new SqlParameter("@PicName", picture.PicName), new SqlParameter("@Remark", picture.Remark), new SqlParameter("@ID", picture.ID) };

                SqlHelper.ExecuteNonQuery(CommandType.Text, SQLstr, sp);

                return picture.ID;

            }
            catch
            {
                throw;
            }

        }
        #endregion

        #region ����ƬIDɾ����Ƭ
        /// <summary>
        /// ����ƬIDɾ����Ƭ
        /// </summary>
        /// <param name="id">��ƬID</param>
        public static void DelPic(Guid id)
        {
            string SQLstr = @"DELETE FROM [ZL_Sns_PicTure]
      WHERE ID=@ID";
            SqlParameter[] sp ={ new SqlParameter("@ID", id) };
            SqlHelper.ExecuteNonQuery(CommandType.Text, SQLstr, sp);

        }
        #endregion

        #region �����IDɾ����Ƭ
        /// <summary>
        /// �����IDɾ����Ƭ
        /// </summary>
        /// <param name="id">���ID</param>
        public static void Del(Guid id)
        {
            string SQLstr = @"DELETE FROM [ZL_Sns_PicTure]
      WHERE PicCategID=@PicCategID";
            SqlParameter[] sp ={ new SqlParameter("@PicCategID", id) };
            SqlHelper.ExecuteNonQuery(CommandType.Text, SQLstr, sp);

        }
        #endregion

        #region ��Ƭ�б�
        /// <summary>
        /// ��Ƭ�б�
        /// </summary>
        /// <param name="CategID"></param>
        /// <param name="page">��ҳ</param>
        /// <returns></returns>[ID],[PicName],[PicUrl],[Remark],[PicCategID]
        public static List<PicTure> GetPicTureList(Guid CategID, PagePagination page)
        {
            try
            {
                string SQLstr = @"SELECT *  FROM ZL_Sns_PicTure where PicCategID=@ID ";
                if (page != null)
                {
                    SQLstr = page.PaginationSql(SQLstr);
                }
                SqlParameter[] sp ={ new SqlParameter("@ID", CategID) };
                List<PicTure> list = new List<PicTure>();
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, SQLstr, sp))
                {
                    while (dr.Read())
                    {
                        PicTure picture = new PicTure();
                        ReadPicTure(dr, picture);
                        list.Add(picture);
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

        #region �鿴��Ƭ
        /// <summary>
        /// �鿴������Ƭ
        /// </summary>
        /// <param name="picid">��Ƭ���</param>
        /// <returns></returns>
        public static PicTure GetPic(Guid picid)
        {
            try
            {
                PicTure picture = new PicTure();
                string SQLstr = @"SELECT *  FROM ZL_Sns_PicTure where ID=@ID";

                SqlParameter[] sp ={ new SqlParameter("@ID", picid) };

                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, SQLstr, sp))
                {
                    if (dr.Read())
                    {
                        ReadPicTure(dr, picture);
                    }
                }
                return picture;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region ��ȡ��Ƭ��Ϣ
        /// <summary>
        /// ��ȡ��Ƭ��Ϣ
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="piccritique"></param>
        public static void ReadPicTure(SqlDataReader dr, PicTure picture)
        {
            picture.ID = (Guid)dr["ID"];
            picture.PicName = dr["PicName"].ToString();
            picture.PicUrl = dr["PicUrl"].ToString();
            picture.Remark = dr["Remark"].ToString();
            picture.PicCategID = (Guid)dr["PicCategID"];
            picture.PicSize = Convert.ToInt32(dr["PicSize"]);
            picture.PicUpTime = dr["PicUpTime"] is DBNull ? new DateTime() : DateTime.Parse(dr["PicUpTime"].ToString());
        }
        #endregion

        #region �������ID��ѯ��Ƭ����
        /// <summary>
        /// �������ID��ѯ��Ƭ����
        /// </summary>
        /// <param name="Categid"></param>
        /// <returns></returns>
        public static string GetCount(Guid Categid)
        {
            string SQLstr = @"select count(*) from ZL_Sns_PicTure where PicCategID=@PicCategID";
            SqlParameter[] sp ={ new SqlParameter("@PicCategID", Categid) };
            return SqlHelper.ExecuteDataSet(CommandType.Text, SQLstr, sp).Tables[0].Rows[0][0].ToString();
        }
        #endregion
    }
}
