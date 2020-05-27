using System;
using System.Collections.Generic;
using System.Text;
using ZoomLa.Model;
using ZoomLa.SQLDAL;
using System.Data;
using System.Data.SqlClient;

namespace ZoomLa.BLL
{
    public class B_RoomMessage
    {
        private string strTableName,PK;
        private M_RoomMessage initMod = new M_RoomMessage();
        public B_RoomMessage()
        {
            strTableName = initMod.TbName;
            PK = initMod.PK;
        }
        public DataTable Sel(int ID)
        {
            return Sql.Sel(strTableName, PK, ID);
        }
        public M_RoomMessage SelReturnModel(int ID)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(strTableName, PK, ID))
            {
                if (reader.Read())
                {
                    return initMod.GetModelFromReader(reader);
                }
                else
                {
                    return null;
                }
            }
        }
        public DataTable Sel()
        {
            return Sql.Sel(strTableName);
        }
        public bool UpdateByID(M_RoomMessage model)
        {
            return Sql.UpdateByIDs(strTableName, PK, model.ID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
        public bool Del(int ID)
        {
            return Sql.Del(strTableName, ID);
        }
        public int insert(M_RoomMessage model)
        {
            return Sql.insert(strTableName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        /// <summary>
        /// 添加留言
        /// </summary>
        /// <param name="me"></param>
        /// <returns></returns>
        public void InsertMessage(M_RoomMessage me)
        {
            try
            {
                me.State = 0;
                string sql = @"insert into ZL_RoomMessage(SendID,InceptID,Mcontent,Addtime,RestoreID,State)
values(@SendID,@InceptID,@Mcontent,@Addtime,@RestoreID,@State)";
                SqlParameter[] parameter =
                    { 
                        new SqlParameter("SendID",me.SendID),
                        new SqlParameter("InceptID",me.InceptID),
                        new SqlParameter("Mcontent",me.Mcontent),
                        new SqlParameter("Addtime",DateTime.Now),
                        new SqlParameter("RestoreID",me.RestoreID),
                        new SqlParameter("State",me.State)
                    };
                SqlHelper.ExecuteSql(sql, parameter);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 读取用户留言信息
        /// </summary>
        /// <param name="InceptID"></param>
        /// <returns></returns>
        public List<M_RoomMessage> GetMessageByInceptID(int InceptID)
        {
            try
            {
                int RestoreID = 0;
                string sql = @"select * from ZL_RoomMessage where InceptID=@InceptID and RestoreID=@RestoreID and State=0 order by addtime desc";
                SqlParameter[] parameter ={ new SqlParameter("InceptID", InceptID),
                new SqlParameter("RestoreID",RestoreID)
                };
                List<M_RoomMessage> list = new List<M_RoomMessage>();
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sql, parameter))
                {
                    while (dr.Read())
                    {
                        M_RoomMessage me = new M_RoomMessage();
                        ReadMessage(dr, me);
                        list.Add(me);
                    }
                    dr.Close();
                    dr.Dispose();
                }
                return list;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 读取留言回复信息
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        /// 
        public static void ReadMessage(SqlDataReader dr, M_RoomMessage me)
        {
            me.ID = int.Parse(dr["ID"].ToString());
            me.SendID = int.Parse(dr["SendID"].ToString());
            me.InceptID = int.Parse(dr["InceptID"].ToString());
            me.Mcontent = dr["Mcontent"].ToString();
            me.Addtime = DateTime.Parse(dr["Addtime"].ToString());
            me.RestoreID = int.Parse(dr["RestoreID"].ToString());
            me.State = (int)dr["State"];
            //dr.Close();
        }
        public List<M_RoomMessage> GetRestoreMessageByID(int ID)
        {
            try
            {
                string sql = @"select * from ZL_RoomMessage where RestoreID=@ID and State=0";
                SqlParameter[] parameter = { new SqlParameter("ID", ID) };
                List<M_RoomMessage> list = new List<M_RoomMessage>();
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sql, parameter))
                {
                    while (dr.Read())
                    {
                        M_RoomMessage me = new M_RoomMessage();
                        ReadMessage(dr, me);
                        list.Add(me);
                    }
                    dr.Close();
                    dr.Dispose();
                }
                return list;
            }
            catch
            {
                throw;
            }
        }
        public void DelMessage(int ID)
        {
            string sql = @"update ZL_RoomMessage set  state=1 where ID=@ID";
            SqlParameter[] parameter = { new SqlParameter("ID", ID) };
            SqlHelper.ExecuteSql(sql, parameter);

        }
    }
}
