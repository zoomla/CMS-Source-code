using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ZoomLa.Sns.Model;
using ZoomLa.SQLDAL;

namespace ZoomLa.Sns.Logic
{
    public class CollectTableLogic
    {
        #region 读取用户收藏状态
        /// <summary>
        /// 读取用户收藏状态
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="ID"></param>
        /// <returns></returns>

        public static CollectTable GetCollectByID(int UserID, Guid ID)
        {
            try
            {

                string sql = @"select * from ZL_Sns_CollectTable where UserID=@UserID and CbyID=@ID ";

                SqlParameter[] parameter =
                   { 
                    new SqlParameter("UserID",UserID),
                       new SqlParameter("ID",ID)
                   };
                CollectTable ct = new CollectTable();
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sql, parameter))
                {
                    if (dr.Read())
                    {
                        ReadCollect(dr, ct);
                    }

                }
                return ct;

            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 用户收藏信息
        /// <summary>
        /// 用户收藏信息
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="page"></param>
        /// <param name="stype"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public static List<CollectTable> GetcollectByUserID(int UserID,int stype, int state)
        {
            try
            {
                string sql = @"select * from ZL_Sns_CollectTable where UserID=@UserID ";
                if (stype != 100)
                {
                    sql += " and Cbytype=@stype";
                }
                if (state != 100)
                {
                    sql += " and Cstate=@state";
                }
                SqlParameter[] parameter =
                   { 
                    new SqlParameter("UserID",UserID),
                       new SqlParameter("stype",stype),
                       new SqlParameter("state",state)

                   };
                List<CollectTable> list = new List<CollectTable>();
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sql, parameter))
                {
                    while (dr.Read())
                    {
                        CollectTable ct = new CollectTable();
                        ReadCollect(dr, ct);
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

        #region 添加收藏
        /// <summary>
        /// 添加收藏
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>

        public static Guid InsertCollect(CollectTable ct)
        {
            try
            {
                ct.ID = Guid.NewGuid();

                string sql = @"Insert into ZL_Sns_CollectTable(ID,UserID,CbyID,Cbytype,Cstate,CAddtime,LabelName) values
(@ID,@UserID,@CbyID,@Cbytype,@Cstate,@CAddtime,@LabelName)";
                SqlParameter[] parameter =
                   {
                       new SqlParameter("ID",ct.ID),
                       new SqlParameter("UserID",ct.UserID),
                       new SqlParameter("CbyID",ct.CbyID),
                       new SqlParameter("Cbytype",ct.Cbytype),
                       new SqlParameter("Cstate",ct.Cstate),
                       new SqlParameter("LabelName",ct.LabelName),
                       new SqlParameter("CAddtime",DateTime.Now)
                   };
                SqlHelper.ExecuteNonQuery(CommandType.Text, sql, parameter);
                return ct.ID;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 读取评论表
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="ca"></param>
        public static void ReadCollect(SqlDataReader dr, CollectTable ca)
        {
            ca.ID = (Guid)dr["ID"];
            ca.UserID = (int)dr["UserID"];
            ca.CbyID = (Guid)dr["CbyID"];
            ca.CAddtime = DateTime.Parse(dr["CAddtime"].ToString());
            ca.Cbytype = (int)dr["CbyType"];
            ca.Cstate = (int)dr["Cstate"];
            ca.LabelName = dr["LabelName"].ToString();
        }
        #endregion

        #region 删除收藏
        /// <summary>
        /// 删除收藏
        /// </summary>
        /// <param name="ID"></param>
        public static void DelCollect(Guid ID)
        {
            string sql = @"delete from ZL_Sns_CollectTable where ID=@ID";
            SqlParameter[] parameter ={ new SqlParameter("ID", ID) };
            SqlHelper.ExecuteNonQuery(CommandType.Text, sql, parameter);
        }
        #endregion

        #region 根据收藏ID读取标签
        /// <summary>
        /// 根据收藏ID读取标签
        /// </summary>
        /// <param name="Byid"></param>
        /// <returns></returns>
        public static string GetLabelName(Guid Byid)
        {
            try
            {
                string[] labelname = null;
                string name = "";
                string sql = @"select LabelName from ZL_Sns_CollectTable where CbyID=@Byid";
                SqlParameter[] parameter ={ new SqlParameter("Byid", Byid) };
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sql, parameter))
                {
                    while (dr.Read())
                    {
                        labelname = dr["LabelName"].ToString().Split(new Char[] { ' ' });
                        for (int i = 0; i < labelname.Length; i++)
                        {
                            if (name.IndexOf(labelname[i].ToString()) < 0)
                            {
                                name += " " + labelname[i];
                            }
                        }
                    }
                }
                return name;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 读取某类型的所有标签
        /// <summary>
        /// 读取某类型的所有标签
        /// </summary>
        /// <param name="stype"></param>
        /// <returns></returns>
        public static string GetAllLabelName(int stype)
        {
            try
            {
                string[] labelname = null;
                string name = "";
                string sql = @"select LabelName from ZL_Sns_CollectTable where 1=1";
                if (stype != 100)
                {
                    sql += " and Cbytype=@stype";
                }

                SqlParameter[] parameter ={ new SqlParameter("stype", stype) };
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sql, parameter))
                {
                    while (dr.Read())
                    {
                        labelname = dr["LabelName"].ToString().Split(new Char[] { ' ' });
                        for (int i = 0; i < labelname.Length; i++)
                        {
                            if (name.IndexOf(labelname[i].ToString()) < 0)
                            {
                                name += " " + labelname[i];
                            }
                        }
                    }
                }
                return name;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 查询某类型某标签下的记录
        /// <summary>
        /// 查询某类型某标签下的记录
        /// </summary>
        /// <param name="label"></param>
        /// <param name="stype"></param>
        /// <returns></returns>
        public static List<Guid> GetBookBystate(string label, int stype)
        {
            try
            {
                string sql = @"select * from ZL_Sns_CollectTable where Cbytype=@stype and LabelName like '%'+@label+'%'";
                SqlParameter[] parameter ={ new SqlParameter("label", @label), new SqlParameter("stype", @stype) };
                List<Guid> list = new List<Guid>();
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sql, parameter))
                {
                    while (dr.Read())
                    {
                        Guid ID = new Guid(dr["CbyID"].ToString());
                        list.Add(ID);
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

        #region 查询用户标签
        /// <summary>
        /// 查询用户标签
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="stype"></param>
        /// <returns></returns>
        public static string GetUserLabel(int UserID, int stype)
        {
            try
            {
                string[] labelname = null;
                string name = "";
                string sql = @"select LabelName from ZL_Sns_CollectTable where UserID=@UserID";
                if (stype != 100)
                {
                    sql += " and Cbytype=@stype";
                }

                SqlParameter[] parameter ={ new SqlParameter("stype", stype), 
                   new SqlParameter("UserID", UserID) };
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sql, parameter))
                {
                    while (dr.Read())
                    {
                        labelname = dr["LabelName"].ToString().Split(new Char[] { ' ' });
                        for (int i = 0; i < labelname.Length; i++)
                        {
                            if (name.IndexOf(labelname[i].ToString()) < 0)
                            {
                                name += " " + labelname[i];
                            }
                        }
                    }
                }
                return name;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 根据ID查询数据
        /// <summary>
        /// 根据ID查询数据
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static CollectTable GetCollectTableByID(Guid ID)
        {
            try
            {

                string sql = @"select * from ZL_Sns_CollectTable where ID=@ID ";

                SqlParameter[] parameter =
                   { 
                       new SqlParameter("ID",ID)
                   };
                CollectTable ct = new CollectTable();
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sql, parameter))
                {
                    if (dr.Read())
                    {
                        ReadCollect(dr, ct);
                    }

                }
                return ct;

            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 修改收藏
        /// <summary>
        /// 修改收藏
        /// </summary>
        /// <param name="state"></param>
        /// <param name="LabelName"></param>
        /// <param name="ID"></param>
        public static void UpdateCollect(int state, string LabelName, Guid ID)
        {
            try
            {
                string sql = @"update ZL_Sns_CollectTable Set Cstate=@state,LabelName=@LabelName where ID=@ID";
                SqlParameter[] parameter ={ 
                new SqlParameter("state",state),
                   new SqlParameter("LabelName",LabelName),
               new SqlParameter("ID",ID)
               };
                SqlHelper.ExecuteNonQuery(CommandType.Text, sql, parameter);
            }
            catch
            {
                throw;
            }
        }
        #endregion
    }
}
