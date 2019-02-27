using System;
using System.Collections.Generic;
using System.Text;
using BDUModel;
using System.Data.SqlClient;
using System.Data;
using ZoomLa.SQLDAL;

namespace BDULogic
{
    public class UserfriendLogic
    {
        #region 添加好友
        /// <summary>
        /// 添加好友
        /// </summary>
        /// <param name="uf"></param>
        /// <returns></returns>
        public static Guid InsertUserfriend(Userfriend uf)
        {
            try
            {
                uf.ID = Guid.NewGuid();
                string sqlInsertUserfriend = @"INSERT INTO [Userfriend]
           ([ID],[HostID],[FriendID],[GroupID],[CreateDate],[BlackList])  VALUES (@ID,@HostID,@FriendID,@GroupID,@CreateDate,@BlackList)";
                SqlParameter[] parameter ={
                new SqlParameter("ID",uf.ID),
                new SqlParameter("HostID",uf.HostID),
                new SqlParameter("FriendID",uf.FriendID),
                new SqlParameter("GroupID",uf.GroupID),
                new SqlParameter("CreateDate",DateTime.Now),
                new SqlParameter("BlackList",uf.BlackList)
            };
                SqlHelper.ExecuteNonQuery(CommandType.Text, sqlInsertUserfriend, parameter);
                return uf.ID;
            }
            catch
            {
                throw;
            }

        }
        #endregion

        #region 修改好友
        /// <summary>
        /// 修改好友
        /// </summary>
        /// <param name="uf"></param>
        /// <returns></returns>
        public static void UpdateUserfriend(Userfriend uf)
        {
            try
            {
                string sqlUpdateUserfriend = @"Update [Userfriend] set [GroupID]=@GroupID,[BlackList]=0 where [ID]=@ID";
                SqlParameter[] parameter ={
                new SqlParameter("ID",uf.ID),
                new SqlParameter("GroupID",uf.GroupID)
            };
                SqlHelper.ExecuteNonQuery(CommandType.Text, sqlUpdateUserfriend, parameter);
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 删除好友
        /// <summary>
        /// 删除好友
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static void DelUserfriend(Guid ID)
        {
            try
            {
                string sqlDelUserfriend = @"Delete from [Userfriend] where [ID]=@ID";
                SqlParameter[] parameter ={
                new SqlParameter("ID",ID)
            };
                SqlHelper.ExecuteNonQuery(CommandType.Text, sqlDelUserfriend, parameter);
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 修改黑名单
        /// <summary>
        /// 修改黑名单
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="type">0为出黑名单,1为进黑名单</param>
        /// <returns></returns>
        public static void UpdateFriToBlack(Guid ID)
        {
            try
            {
                string sqlUpdateFriToBlack = @"Update [Userfriend] set [BlackList]=1 where [ID]=@ID";
                SqlParameter[] parameter ={
                new SqlParameter("ID",ID)
            };
                SqlHelper.ExecuteNonQuery(CommandType.Text, sqlUpdateFriToBlack, parameter);
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 通过HostID查询黑名单
        /// <summary>
        /// 通过HostID查询黑名单
        /// </summary>
        /// <param name="HostID"></param>
        /// <param name="page">分页</param>
        /// <returns></returns>
        public static List<Userfriend> GetUserBlackfriendByHostID(Guid HostID, PagePagination page)
        {
            try
            {
                string sqlGetUserfriendByGroupID = @"SELECT FriendGroup.GroupName AS Expr1, Userfriend.*, UserTable.UserName AS Expr2, 
      UserTable.UserRealname AS Expr3, UserTable.Usermail AS Expr4
FROM FriendGroup INNER JOIN
      Userfriend ON FriendGroup.ID = Userfriend.GroupID INNER JOIN
      UserTable ON Userfriend.FriendID = UserTable.UserID
WHERE (Userfriend.BlackList = 1 and Userfriend.HostID=@HostID)";
                if (page != null)
                {
                    sqlGetUserfriendByGroupID = page.PaginationSql(sqlGetUserfriendByGroupID);
                }
                SqlParameter[] parameter ={
                new SqlParameter("HostID",HostID)};
                List<Userfriend> list = new List<Userfriend>();
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sqlGetUserfriendByGroupID, parameter))
                {
                    while (dr.Read())
                    {

                        Userfriend uf = new Userfriend();
                        uf.Mail = dr["Expr4"].ToString();
                        uf.Realname = dr["Expr3"].ToString();
                        uf.Username = dr["Expr2"].ToString();
                        uf.Groupname = "黑名单";
                        ReadUserfriend(dr, uf);
                        list.Add(uf);
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


        #region 通过HostID查询信息
        /// <summary>
        /// 通过HostID查询信息
        /// </summary>
        /// <param name="HostID"></param>
        /// <param name="page">分页</param>
        /// <returns></returns>
        public static List<Userfriend> GetUserfriendByHostID(Guid HostID, PagePagination page)
        {
            try
            {
                string sqlGetUserfriendByGroupID = @"SELECT FriendGroup.GroupName AS Expr1, Userfriend.*, UserTable.UserName AS Expr2, 
                                                      UserTable.UserRealname AS Expr3, UserTable.Usermail AS Expr4
                                                FROM FriendGroup INNER JOIN
                                                      Userfriend ON FriendGroup.ID = Userfriend.GroupID INNER JOIN
                                                      UserTable ON Userfriend.FriendID = UserTable.UserID
                                                WHERE (Userfriend.BlackList = 0 and Userfriend.HostID=@HostID)";
                if (page != null)
                {
                    sqlGetUserfriendByGroupID = page.PaginationSql(sqlGetUserfriendByGroupID);
                }
                SqlParameter[] parameter ={
                new SqlParameter("HostID",HostID)};
                List<Userfriend> list = new List<Userfriend>();
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sqlGetUserfriendByGroupID, parameter))
                {
                    while (dr.Read())
                    {

                        Userfriend uf = new Userfriend();
                        uf.Mail = dr["Expr4"].ToString();
                        uf.Realname = dr["Expr3"].ToString();
                        uf.Username = dr["Expr2"].ToString();
                        uf.Groupname = dr["Expr1"].ToString();
                        ReadUserfriend(dr, uf);
                        list.Add(uf);
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

        #region 通过GroupID查询信息
        /// <summary>
        /// 通过GroupID查询信息
        /// </summary>
        /// <param name="HostID"></param>
        /// <param name="page">分页</param>
        /// <returns></returns>
        public static List<Userfriend> GetUserfriendByGroupID(Guid GroupID, Guid userID, PagePagination page)
        {
            try
            {
                string sqlGetUserfriendByGroupID = @"SELECT FriendGroup.GroupName AS Expr1, Userfriend.*,UserTable.Userpic, UserTable.UserName AS Expr2, 
                                                      UserTable.UserRealname AS Expr3, UserTable.Usermail AS Expr4
                                                FROM FriendGroup INNER JOIN
                                                      Userfriend ON FriendGroup.ID = Userfriend.GroupID INNER JOIN
                                                      UserTable ON Userfriend.FriendID = UserTable.UserID
                                                WHERE  Userfriend.HostID=@UserID ";
                if (GroupID != Guid.Empty)
                    sqlGetUserfriendByGroupID += " and Userfriend.GroupID=@GroupID";
                if (page != null)
                {
                    sqlGetUserfriendByGroupID = page.PaginationSql(sqlGetUserfriendByGroupID);
                }
                SqlParameter[] parameter ={
                new SqlParameter("GroupID",GroupID)
                ,new SqlParameter("UserID",userID)};
                List<Userfriend> list = new List<Userfriend>();
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sqlGetUserfriendByGroupID, parameter))
                {
                    while (dr.Read())
                    {

                        Userfriend uf = new Userfriend();
                        uf.CreateDate = DateTime.Parse(dr["CreateDate"].ToString());
                        uf.GroupID = (Guid)dr["GroupID"];
                        uf.HostID = (Guid)dr["HostID"];
                        uf.ID = (Guid)dr["ID"];
                        uf.Mail = dr["Expr4"].ToString();
                        uf.Realname = dr["Expr3"].ToString();
                        uf.Username = dr["Expr2"].ToString();
                        uf.Groupname = dr["Expr1"].ToString();
                        uf.Userpic = dr["Userpic"].ToString();
                        uf.FriendID = (Guid)dr["FriendID"];
                        ReadUserfriend(dr, uf);
                        if (list.Count != 0)
                        {
                            bool ishas = false;
                            foreach (Userfriend uf1 in list)
                            {
                                if (uf1.FriendID == uf.FriendID)
                                {
                                    ishas = true;
                                }
                            }
                            if (!ishas)
                            {
                                list.Add(uf);
                            }
                        }
                        else
                        {
                            list.Add(uf);

                        }
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


        #region 判断2个ID是否是好友
        /// <summary>
        ///  判断2个ID是否是好友
        /// </summary>
        /// <param name="HostID"></param>
        /// <param name="FriendID"></param>
        /// <returns></returns>
        public static bool CheckUserfriendByIDandID(int HostID, int FriendID)
        {
            try
            {
                string sqlCheckUserfriendByIDandID = @"SELECT ZL_UserFriendTable.*
FROM ZL_UserFriendGroup INNER JOIN
      ZL_UserFriendTable ON ZL_UserFriendGroup.ID = ZL_UserFriendTable.GroupID where ZL_UserFriendGroup.GroupName <> '黑名单' and ZL_UserFriendTable.HostID=@HostID and ZL_UserFriendTable.FriendID=@FriendID";
                SqlParameter[] parameter ={
                new SqlParameter("HostID",HostID),
                new SqlParameter("FriendID",FriendID)
                };
                if (SqlHelper.ExecuteScalar(CommandType.Text, sqlCheckUserfriendByIDandID, parameter) == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }

            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 判断2个ID是否在黑名单
        /// <summary>
        /// 判断2个ID是否在黑名单(不在返回true)
        /// </summary>
        /// <param name="HostID"></param>
        /// <param name="FriendID"></param>
        /// <returns></returns>
        public static bool CheckBlackList(Guid HostID, Guid FriendID)
        {
            try
            {
                string sqlCheckUserfriendByIDandID = @"SELECT ZL_UserFriendTable.*
FROM FriendGroup INNER JOIN
      ZL_UserFriendTable ON FriendGroup.ID = ZL_UserFriendTable.GroupID where FriendGroup.GroupName = '黑名单' and ZL_UserFriendTable.HostID=@HostID and ZL_UserFriendTable.FriendID=@FriendID";
                SqlParameter[] parameter ={
                new SqlParameter("HostID",HostID),
                new SqlParameter("FriendID",FriendID)
                };
                if (SqlHelper.ExecuteScalar(CommandType.Text, sqlCheckUserfriendByIDandID, parameter) == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 读取最新访问的好友
        /// <summary>
        /// 读取最新访问的好友
        /// </summary>
        /// <param name="HostID"></param>
        /// <returns></returns>
        public static List<Userfriend> GetNewAttentFriend(Guid HostID)
        {
            try
            {
                string sql = @"SELECT UserTable.UserName, UserTable.Userpic,UserTable.UserRealname,Userfriend.*
FROM ZL_UserFriendTable INNER JOIN
      SystemLog ON ZL_UserFriendTable.FriendID = SystemLog.UserID INNER JOIN
      UserTable ON SystemLog.UserID = UserTable.UserID where SystemLog.ContentID=@HostID and SystemLog.LogType='Attent'";
                SqlParameter[] parameter ={ new SqlParameter("HostID", HostID) };
                List<Userfriend> list = new List<Userfriend>();
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sql, parameter))
                {
                    while (dr.Read())
                    {

                        Userfriend uf = new Userfriend();
                        uf.Realname = dr["UserRealname"].ToString();
                        uf.Username = dr["UserName"].ToString();
                        uf.Userpic = dr["Userpic"].ToString();
                        ReadUserfriend(dr, uf);
                        if (list.Count != 0)
                        {
                            bool ishas = false;
                            foreach (Userfriend uf1 in list)
                            {
                                if (uf1.FriendID == uf.FriendID)
                                {
                                    ishas = true;
                                }
                            }
                            if (!ishas)
                            {
                                list.Add(uf);
                            }
                        }
                        else
                        {
                            list.Add(uf);

                        }
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

        #region 判断2个ID是否存在列表中
        /// <summary>
        ///  判断2个ID是否存在列表中(包括黑名单)
        /// </summary>
        /// <param name="HostID"></param>
        /// <param name="FriendID"></param>
        /// <returns></returns>
        public static bool CheckUserfriendblackByIDandID(Guid HostID, Guid FriendID)
        {
            try
            {
                string sqlCheckUserfriendByIDandID = @"select * from ZL_UserFriendTable where HostID=@HostID and FriendID=@FriendID";
                SqlParameter[] parameter ={
                new SqlParameter("HostID",HostID),
                new SqlParameter("FriendID",FriendID)
                };
                if (SqlHelper.ExecuteScalar(CommandType.Text, sqlCheckUserfriendByIDandID, parameter) == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }

            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 根据ID查询用户
        /// <summary>
        /// 根据ID查询用户
        /// </summary>
        /// <param name="UserEmail"></param>
        /// <returns></returns>
        public static Userfriend GetUserfriendByID(Guid ID)
        {
            try
            {
                string sqlGetUserfriendByID = @"SELECT FriendGroup.GroupName AS Expr1, Userfriend.*, UserTable.UserName AS Expr2, 
      UserTable.UserRealname AS Expr3, UserTable.Usermail AS Expr4
FROM FriendGroup INNER JOIN
      Userfriend ON FriendGroup.ID = Userfriend.GroupID INNER JOIN
      UserTable ON Userfriend.FriendID = UserTable.UserID
WHERE (Userfriend.ID=@ID)";
                SqlParameter[] parameter ={
                new SqlParameter("ID",ID)};
                Userfriend uf = new Userfriend();
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sqlGetUserfriendByID, parameter))
                {
                    if (dr.Read())
                    {
                        uf.Mail = dr["Expr4"].ToString();
                        uf.Realname = dr["Expr3"].ToString();
                        uf.Username = dr["Expr2"].ToString();
                        uf.Groupname = dr["Expr1"].ToString();
                        ReadUserfriend(dr, uf);
                    }
                }
                return uf;

            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region 通过好友编号和用户编号获取好友所在的组
        /// <summary>
        ///  通过好友编号和用户编号获取好友所在的组
        /// </summary>
        /// <param name="hostID"></param>
        /// <param name="fID"></param>
        /// <returns></returns>
        public static List<Guid> GetFriendGroup(Guid hostID, Guid fID)
        {
            try
            {
                List<Guid> list = new List<Guid>();
                string sqlGetFriendGroup = @"SELECT GroupID FROM ZL_UserFriendTable WHERE HostID=@HostID AND FriendID=@FriendID";
                SqlParameter[] parameter ={
                new SqlParameter("HostID",hostID),
                new SqlParameter("FriendID",fID)};
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sqlGetFriendGroup, parameter))
                {
                    while (dr.Read())
                    {
                        list.Add(new Guid(dr["GroupID"].ToString()));
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

        #region 通过好友编号和用户编号删除好友所在的组
        /// <summary>
        ///  通过好友编号和用户编号删除好友所在的组
        /// </summary>
        /// <param name="hostID"></param>
        /// <param name="fID"></param>
        /// <returns></returns>
        public static void DeleteFriendGroup(Guid hostID, Guid fID)
        {
            try
            {
                List<Guid> list = new List<Guid>();
                string sqlDeleteFriendGroup = @"delete ZL_UserFriendTable WHERE HostID=@HostID AND FriendID=@FriendID";
                SqlParameter[] parameter ={
                new SqlParameter("HostID",hostID),
                new SqlParameter("FriendID",fID)};
                SqlHelper.ExecuteNonQuery(CommandType.Text, sqlDeleteFriendGroup, parameter);
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 读取好友表数据
        /// <summary>
        /// 读取好友表数据
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="uf"></param>
        public static void ReadUserfriend(SqlDataReader dr, Userfriend uf)
        {
            uf.ID = (Guid)dr["ID"];
            uf.HostID = (Guid)dr["HostID"];
            uf.FriendID = (Guid)dr["FriendID"];
            uf.GroupID = (Guid)dr["GroupID"];
            uf.BlackList = (int)dr["BlackList"];
            uf.CreateDate = dr["CreateDate"] is DBNull ? new DateTime() : DateTime.Parse(dr["CreateDate"].ToString());
        }
        #endregion

        #region 读取好友表数据
        /// <summary>
        /// 读取好友表数据
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="uf"></param>
        public static void ReadUserfriends(SqlDataReader dr, Userfriend uf)
        {
            uf.ID = (Guid)dr["Userfriend.ID"];
            uf.HostID = (Guid)dr["Userfriend.HostID"];
            uf.FriendID = (Guid)dr["Userfriend.FriendID"];
            uf.GroupID = (Guid)dr["Userfriend.GroupID"];
            uf.BlackList = (int)dr["Userfriend.BlackList"];
            uf.CreateDate = dr["Userfriend.CreateDate"] is DBNull ? new DateTime() : DateTime.Parse(dr["Userfriend.CreateDate"].ToString());
            uf.Mail = dr["Expr4"].ToString();
            uf.Realname = dr["Expr3"].ToString();
            uf.Username = dr["Expr2"].ToString();
            uf.Groupname = dr["Expr1"].ToString();

        }
        #endregion

        #region 通过昵称,邮箱查询好友信息
        /// <summary>
        /// 通过昵称,邮箱查询好友信息
        /// </summary>
        /// <param name="username"></param>
        /// <param name="email"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static List<Userfriend> GetUserFriendLike(string username, string email, Guid userid)
        {
            try
            {
                string sql = @"SELECT Userfriend.*,UserTable.UserName,UserTable.Userpic
FROM UserTable INNER JOIN
      Userfriend ON UserTable.UserID = Userfriend.FriendID INNER JOIN
      FriendGroup ON Userfriend.GroupID = FriendGroup.ID
WHERE FriendGroup.GroupName <> '黑名单' and Userfriend.HostID=@userid ";
                if (username != "")
                {
                    sql += " and UserTable.UserName like '%'+@username+'%'";
                }
                SqlParameter[] parameter=
                    {
                        new SqlParameter("username",username),
                        new SqlParameter("userid",userid)
                    };
                List<Userfriend> list = new List<Userfriend>();
                using(SqlDataReader dr=SqlHelper.ExecuteReader(CommandType.Text,sql,parameter))
                {
                    while(dr.Read())
                    {
                        Userfriend uf = new Userfriend();
                        ReadUserfriend(dr, uf);
                        uf.Username = dr["UserName"].ToString();
                        uf.Userpic = dr["Userpic"].ToString();
                        if (list.Count != 0)
                        {
                            bool ishas = false;
                            foreach (Userfriend uf1 in list)
                            {
                                if (uf1.FriendID == uf.FriendID)
                                {
                                    ishas = true;
                                }
                            }
                            if (!ishas)
                            {
                                list.Add(uf);
                            }
                        }
                        else
                        {
                            list.Add(uf);

                        }
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
