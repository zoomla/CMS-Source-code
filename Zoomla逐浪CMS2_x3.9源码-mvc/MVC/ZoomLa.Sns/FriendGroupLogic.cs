using System;
using System.Collections.Generic;
using System.Text;
using BDUModel;
using System.Data.SqlClient;
using System.Data;
using ZoomLa.SQLDAL;

namespace BDULogic
{
    public class FriendGroupLogic
    {
        #region 创建用户组
        /// <summary>
        /// 创建用户组
        /// </summary>
        /// <param name="fg"></param>
        /// <returns></returns>
        public static Guid InsertFriendGroup(FriendGroup fg)
        {
            try
            {

                fg.ID = Guid.NewGuid();
                string sqlInsertFriendGroup = @"INSERT INTO [ZL_Sns_FriendGroup]
           ([ID],[HostID],[GroupName],[Taxis])  VALUES (@ID,@HostID,@GroupName,@Taxis)";
                SqlParameter[] parameter ={
                new SqlParameter("ID",fg.ID),
                new SqlParameter("HostID",fg.HostID),
                new SqlParameter("GroupName",fg.GroupName),
                new SqlParameter("Taxis",GetGroupTaxisMax(fg.HostID)+1)
            };
                SqlHelper.ExecuteNonQuery(CommandType.Text, sqlInsertFriendGroup, parameter);
                return fg.ID;
            }
            catch
            {
                throw;
            }

        }

        private static int GetGroupTaxisMax(Guid hostID)
        {
            SqlParameter[] parameter ={
                new SqlParameter("HostID",hostID)};
            object obj = SqlHelper.ExecuteScalar(CommandType.Text, "SELECT (CASE WHEN MAX(Taxis) IS NULL THEN '0' ELSE MAX(Taxis) END) AS Taxis  FROM ZL_Sns_FriendGroup WHERE HOSTID=@HostID AND GroupName <>'黑名单'", parameter);
            if (obj != null)
                return int.Parse(obj.ToString());
            else return 0;
        }
        #endregion

        #region 修改好友组
        /// <summary>
        /// 修改好友组
        /// </summary>
        /// <param name="fg"></param>
        /// <returns></returns>
        public static void UpdateFriendGroup(FriendGroup fg)
        {
            try
            {
                string sqlUpdateFriendGroup = @"Update [FriendGroup] set [GroupName]=@GroupName where [ID]=@ID";
                SqlParameter[] parameter ={
                new SqlParameter("ID",fg.ID),
                new SqlParameter("GroupName",fg.GroupName)
            };
                SqlHelper.ExecuteNonQuery(CommandType.Text, sqlUpdateFriendGroup, parameter);
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 删除好友分组
        /// <summary>
        /// 删除好友分组
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static void DelFriendGroup(Guid ID)
        {
            try
            {
                string sqlDelFriendGroup = @"Delete from [FriendGroup] where [ID]=@ID";
                SqlParameter[] parameter ={
                new SqlParameter("ID",ID)
            };
                SqlHelper.ExecuteNonQuery(CommandType.Text, sqlDelFriendGroup, parameter);
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 查询分子名称重复
        /// <summary>
        /// 查询分子名称重复
        /// </summary>
        /// <param name="HostID"></param>
        /// <param name="GroupName"></param>
        /// <returns></returns>
        public static bool CheckGroupNameByHostID(Guid HostID, string GroupName)
        {
            try
            {
                string sqlCheckGroupNameByHostID = @"select * from [FriendGroup] where HostID=@HostID and GroupName=@GroupName";
                SqlParameter[] parameter ={
                new SqlParameter("HostID",HostID),
                new SqlParameter("GroupName",GroupName)
                };

                if (SqlHelper.ExecuteScalar(CommandType.Text, sqlCheckGroupNameByHostID, parameter) == null)
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

        #region 通过HostID查询信息
        /// <summary>
        /// 通过HostID查询信息
        /// </summary>
        /// <param name="HostID"></param>
        /// <param name="page">分页</param>
        /// <returns></returns>
        public static List<FriendGroup> GetFriendGroupByHostID(Guid HostID, PagePagination page)
        {
            try
            {
                string sqlGetFriendGroupByHostID = @"select * from FriendGroup where HostID=@HostID ORDER BY Taxis";
                if (page != null)
                {
                    sqlGetFriendGroupByHostID = page.PaginationSql(sqlGetFriendGroupByHostID);
                }
                SqlParameter[] parameter ={
                new SqlParameter("HostID",HostID)};

                List<FriendGroup> list = new List<FriendGroup>();
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sqlGetFriendGroupByHostID, parameter))
                {
                    while (dr.Read())
                    {
                        FriendGroup fg = new FriendGroup();
                        ReadFriendGroup(dr, fg);
                        list.Add(fg);
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

        #region 通过ID查询
        /// <summary>
        /// 通过ID查询信息
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static FriendGroup GetFriendGroupOneByID(Guid ID)
        {
            try
            {
                string sqlGetFriendGroupOneByID = @"select Top 1 * from FriendGroup where ID=@ID";
                SqlParameter[] parameter ={
                new SqlParameter("ID",ID)};
                FriendGroup ut = new FriendGroup();
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sqlGetFriendGroupOneByID, parameter))
                {
                    if (dr.Read())
                    {
                        ReadFriendGroup(dr, ut);
                    }
                }
                return ut;

            }
            catch
            {
                throw;
            }
        }
        #endregion


        #region 读取用户组表数据
        /// <summary>
        /// 读取用户组表数据
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="uf"></param>
        public static void ReadFriendGroup(SqlDataReader dr, FriendGroup fg)
        {
            fg.ID = (Guid)dr["ID"];
            fg.HostID = (Guid)dr["HostID"];
            fg.GroupName = dr["GroupName"].ToString();
            fg.Taxis = (int)dr["Taxis"]; ;
        }

        #endregion
    }
}