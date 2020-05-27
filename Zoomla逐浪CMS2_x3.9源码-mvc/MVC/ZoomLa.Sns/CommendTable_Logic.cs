using System;
using System.Collections.Generic;
using System.Text;
using BDUModel;
using System.Data;
using System.Data.SqlClient;
using ZoomLa.SQLDAL;

namespace BDULogic
{
    public class CommendTable_Logic
    {
        #region ����Ƽ���Ϣ
        /// <summary>
        /// ����Ƽ���Ϣ
        /// </summary>
        /// <param name="ct"></param>
        public static void Add(CommendTable ct)
        {
            ct.ID = Guid.NewGuid();
            string SQLstr = @"INSERT INTO [ZL_Sns_CommendTable] ([ID],[CommendTitle],[CommendContext],[CommendUrl],[CommendUserID],[CommendImage],[CommendType],[CommendTime]) 
                    VALUES (@ID,@CommendTitle,@CommendContext,@CommendUrl,@CommendUserID,@CommendImage,@CommendType,@CommendTime)";
            SqlParameter[] sp ={   
                new SqlParameter("@ID",ct.ID),
                new SqlParameter("@CommendTitle",ct.CommendTitle),
                new SqlParameter("@CommendContext",ct.CommendContext),
                new SqlParameter("@CommendUrl",ct.CommendUrl),
                new SqlParameter("@CommendUserID",ct.CommendUserID),
                new SqlParameter("@CommendImage",ct.CommendImage),
                new SqlParameter("@CommendType",ct.CommendType),
                new SqlParameter("@CommendTime",ct.CommendTime)
            };
            SqlHelper.ExecuteNonQuery(CommandType.Text, SQLstr, sp);
        }
        #endregion

        #region ɾ���Ƽ���Ϣ
        /// <summary>
        /// ɾ���Ƽ���Ϣ
        /// </summary>
        /// <param name="id"></param>
        public static void Del(Guid id)
        {
            string SQLstr = @"DELETE FROM [ZL_Sns_CommendTable] WHERE	[ID] = @ID";

            SqlParameter[] sp ={   
                new SqlParameter("@ID",id)
            };

            SqlHelper.ExecuteNonQuery(CommandType.Text, SQLstr, sp);
        }
        #endregion

        #region �����û�ID��ѯ�Ƽ���Ϣ
        /// <summary>
        /// �����û�ID��ѯ�Ƽ���Ϣ
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<CommendTable> GetUserCommendTable(Guid id, PagePagination page, CommendTableType ctt)
        {
            try
            {
                string SQLstr = @"select a.*,b.UserName from ZL_Sns_CommendTable a JOIN ZL_Sns_UserTable b ON a.CommendUserID=b.UserID
 where a.CommendUserID=@CommendUserID ";
                if (ctt != CommendTableType.NullType)
                {
                    SQLstr = SQLstr + " and a.CommendType=@CommendType";
                }
                if (page != null)
                {
                    SQLstr = page.PaginationSql(SQLstr);
                }
                SqlParameter[] parameter ={
                new SqlParameter("@CommendUserID",id),
                    new SqlParameter("@CommendType",ctt)
                };
                List<CommendTable> list = new List<CommendTable>();
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, SQLstr, parameter))
                {
                    while (dr.Read())
                    {
                        CommendTable ct = new CommendTable();
                        ReadCommendTable(dr, ct);
                        ct.UserName = dr["UserName"].ToString();
                        list.Add(ct);
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

        #region ����ID��ѯ�����Ƽ���Ϣ
        /// <summary>
        /// ����ID��ѯ�����Ƽ���Ϣ
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static CommendTable GetCommendTable(Guid id)
        {
            try
            {
                CommendTable commtable = new CommendTable();
                string SQLstr = @"select * from ZL_Sns_CommendTable where ID=@ID";
                SqlParameter[] parameter ={
                new SqlParameter("@ID",id)};
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, SQLstr, parameter))
                {
                    while (dr.Read())
                    {
                        ReadCommendTable(dr, commtable);
                    }
                }
                return commtable;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region ��ȡ�Ƽ���Ϣ
        /// <summary>
        /// ��ȡ�Ƽ���Ϣ
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="piccateg"></param>
        public static void ReadCommendTable(SqlDataReader dr, CommendTable ct)
        {
            ct.ID = (Guid)dr["ID"];
            ct.CommendContext = dr["CommendContext"].ToString();
            ct.CommendImage = dr["CommendImage"].ToString();
            ct.CommendTitle = dr["CommendTitle"].ToString();
            ct.CommendType = Convert.ToInt32(dr["CommendType"].ToString());
            ct.CommendUrl = dr["CommendUrl"].ToString();
            ct.CommendUserID = (Guid)dr["ID"];
            ct.CommendTime = dr["CommendTime"] is DBNull ? new DateTime() : DateTime.Parse(dr["CommendTime"].ToString());

        }
        #endregion
    }
}
