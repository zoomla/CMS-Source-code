namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using ZoomLa.SQLDAL;
    using System.Data.SqlClient;

    /// <summary>
    /// B_UserPromotions 的摘要说明
    /// </summary>
    public class B_UserPromotions
    {
        public string strTableName = "";
        public string PK = "";
        private M_UserPromotions initMod = new M_UserPromotions();
        public B_UserPromotions()
        {
            strTableName = initMod.TbName;
            PK = initMod.PK;
        }
        /// <summary>
        ///添加记录
        /// </summary>
        /// <param name="UserPromotions"></param>
        /// <returns></returns>
        public int GetInsert(M_UserPromotions model)
        {
            return Sql.insert(strTableName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }

        /// <summary>
        ///更新记录
        /// </summary>
        /// <param name="UserPromotions"></param>
        /// <returns></returns>
        public bool GetUpdate(M_UserPromotions model)
        {
            string sqlStr = "UPDATE [dbo].[ZL_UserPromotions] SET [GroupID] = @GroupID,[NodeID] = @NodeID,[ModelID] = @ModelID,[look] = @look,[addTo] = @addTo,[Modify] = @Modify,[Columns] = @Columns,[Comments] = @Comments,[Deleted]=@Deleted,[quote]=@quote,[down]=@quote WHERE [pid] = @pid";
            SqlParameter[] cmdParams = model.GetParameters();
            return SqlHelper.ExecuteSql(sqlStr, cmdParams);
        }

        /// <summary>
        ///删除记录
        /// </summary>
        /// <param name="UserPromotions"></param>
        /// <returns></returns>
        public bool DeleteByGroupID(int UserPromotionsID)
        {
            string sqlStr = "DELETE FROM [dbo].[ZL_UserPromotions] WHERE [pid] = @pid";
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@pid", SqlDbType.Int, 4);
            cmdParams[0].Value = UserPromotionsID;
            return SqlHelper.ExecuteSql(sqlStr, cmdParams);
        }

        /// <summary>
        ///查找一条记录
        /// </summary>
        public M_UserPromotions GetSelect(int UserPromotionsID)
        {
            string sqlStr = "SELECT [pid],[GroupID],[NodeID],[ModelID],[look],[addTo],[Modify],[Columns],[Comments],[Deleted],[quote],[down] FROM [dbo].[ZL_UserPromotions] WHERE [pid] = @pid";
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@pid", SqlDbType.Int, 4);
            cmdParams[0].Value = UserPromotionsID;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sqlStr, cmdParams))
            {
                if (reader.Read())
                {
                    return initMod.GetModelFromReader(reader);
                }
                else
                {
                    return new M_UserPromotions();
                }
            }
        }

        /// <summary>
        ///返回所有记录
        /// </summary>
        /// <returns></returns>
        public DataTable Select_All()
        {
            return Sql.Sel(strTableName); 
        }

        /// <summary>
        /// 从节点更新
        /// </summary>
        /// <param name="userpromotions"></param>
        /// <returns></returns>
        public bool GetUpdateByNodeGroup(M_UserPromotions userpromotions)
        {
            string sqlStr = "UPDATE [dbo].[ZL_UserPromotions] SET [look] = @look,[addTo] = @addTo,[Modify] = @Modify,[Columns] = @Columns,[Comments] = @Comments,[Deleted]=@Deleted,[quote]=@quote,[down]=@down WHERE [GroupID] = @GroupID and [NodeID] = @NodeID";
            SqlParameter[] cmdParams = userpromotions.GetParameters();
            return SqlHelper.ExecuteSql(sqlStr, cmdParams);
        }

        /// <summary>
        /// 从节点添加
        /// </summary>
        /// <param name="userpromotions"></param>
        /// <returns></returns>
        public int GetInsertByNodeGroup(M_UserPromotions userpromotions)
        {
            string sqlStr = "INSERT INTO [dbo].[ZL_UserPromotions] ([GroupID],[NodeID],[look],[addTo],[Modify],[Columns],[Comments],[Deleted],[quote],[down]) VALUES (@GroupID,@NodeID,@look,@addTo,@Modify,@Columns,@Comments,@Deleted,@quote,@down);select @@IDENTITY";
            SqlParameter[] cmdParams = userpromotions.GetParameters();
            return SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.Text, sqlStr, cmdParams)); 
        }

        /// <summary>
        /// 存在则更新，否则更新
        /// </summary>
        /// <param name="userpromotions"></param>
        /// <returns></returns>
        public bool GetInsertOrUpdate(M_UserPromotions userpromotions)
        {
            string sqls = "Select count(*) from [dbo].[ZL_UserPromotions] where [GroupID] = @GroupID and [NodeID] = @NodeID";
            SqlParameter[] cmdParams = userpromotions.GetParameters();
            int Pcount = SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.Text, sqls, cmdParams));

            if (Pcount > 0)
            {
                string sqlStr = "UPDATE [dbo].[ZL_UserPromotions] SET [look] = @look,[addTo] = @addTo,[Modify] = @Modify,[Columns] = @Columns,[Comments] = @Comments,[Deleted]=@Deleted,[quote]=@quote,[down]=@down  WHERE [GroupID] = @GroupID and [NodeID] = @NodeID";
                return SqlHelper.ExecuteSql(sqlStr, cmdParams);
            }
            else
            {
                string sqlStr = "INSERT INTO [dbo].[ZL_UserPromotions] ([GroupID],[NodeID],[look],[addTo],[Modify],[Columns],[Comments],[Deleted],[quote],[down]) VALUES (@GroupID,@NodeID,@look,@addTo,@Modify,@Columns,@Comments,@Deleted,@quote,@down);select @@IDENTITY";
                return (SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.Text, sqlStr, cmdParams)) > 0);
            }
        }
        /// <summary>
        /// 用户组在指定节点的权限模型
        /// </summary>
        public M_UserPromotions GetSelect(int NodeID, int GroupID)
        {
            string sql = "SELECT TOP 1 * FROM " + strTableName + " WHERE NodeID=" + NodeID + " AND GroupID=" + GroupID;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sql, null))
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
        /// <summary>
        /// 节点中查找一条记录
        /// </summary>
        public DataTable GetSelectbyGroupID(int GroupID)
        {
            string sqlStr = "SELECT [pid],[GroupID],[NodeID],[ModelID],[look],[addTo],[Modify],[Columns],[Comments],[Deleted],[quote],[down] FROM [dbo].[ZL_UserPromotions] WHERE [GroupID]=@GroupID order by NodeID";
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@GroupID", SqlDbType.Int, 4);
            cmdParams[0].Value = GroupID;
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, cmdParams);
        }
        /// <summary>
        /// 获取指定组拥有录入权限的nodeids,用于OA,过滤掉无权限的
        /// </summary>
        /// <returns></returns>
        public string GetNodeIDS(int gid) 
        {
            string ids = "";
            string where = "GroupID=" + gid + " And addTo>0";
            DataTable dt = DBCenter.SelWithField("ZL_UserPromotions", "NodeID", where);
            foreach (DataRow dr in dt.Rows)
            {
                ids += dr["NodeID"] + ",";
            }
            ids = ids.TrimEnd(',');
            return ids;
        }
        public DataTable SelByNid(int nodeid)
        {
            string sql = "SELECT * FROM " + strTableName + " WHERE NodeID=" + nodeid;
            return SqlHelper.ExecuteTable(sql);
        }
    }
}
