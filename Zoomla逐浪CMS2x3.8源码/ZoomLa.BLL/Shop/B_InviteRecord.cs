namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using ZoomLa.SQLDAL;
    using System.Data.SqlClient;
    public class B_InviteRecord
    {
        private string strTableName, PK;
        private M_InviteRecord initMod = new M_InviteRecord();
        public B_InviteRecord() 
        {
            strTableName = initMod.TbName;
            PK = initMod.PK;
        }
        public DataTable Sel(int ID)
        {
            return Sql.Sel(strTableName, PK, ID);
        }
        public M_InviteRecord SelReturnModel(int ID)
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
        private M_InviteRecord SelReturnModel(string strWhere)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(strTableName, strWhere))
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
        public bool UpdateByID(M_InviteRecord model)
        {
            return Sql.UpdateByIDs(strTableName, PK, model.id.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
        public bool Del(int ID)
        {
            return Sql.Del(strTableName, ID);
        }
        public int insert(M_InviteRecord model)
        {
            return Sql.insert(strTableName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
		/// <summary>
		///添加记录
		/// </summary>
		/// <param name="InviteRecord"></param>
		/// <returns></returns>
        public int GetInsert(M_InviteRecord model)
        {
            string sqlStr = "INSERT INTO [dbo].[ZL_InviteRecord] ([userid],[RecommUserId],[RegData],[isValid],[isReset]) VALUES (@userid,@RecommUserId,@RegData,@isValid,@isReset);select @@IDENTITY";
            SqlParameter[] cmdParams = model.GetParameters();
            return SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.Text, sqlStr, cmdParams));
        }

		/// <summary>
		///更新记录
		/// </summary>
		/// <param name="InviteRecord"></param>
		/// <returns></returns>
        public bool GetUpdate(M_InviteRecord model)
        {
            string sqlStr = "UPDATE [dbo].[ZL_InviteRecord] SET [userid] = @userid,[RecommUserId] = @RecommUserId,[RegData] = @RegData,[isValid] = @isValid,[isReset]=@isReset WHERE [id] = @id";
            SqlParameter[] cmdParams = model.GetParameters();
            return SqlHelper.ExecuteSql(sqlStr, cmdParams);
        }

		/// <summary>
		///删除记录
		/// </summary>
		/// <param name="InviteRecord"></param>
		/// <returns></returns>
		public bool DeleteByGroupID(int InviteRecordID)
        {
            string sqlStr = "DELETE FROM [dbo].[ZL_InviteRecord] WHERE [id] = @id";
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@id", SqlDbType.Int, 4);
            cmdParams[0].Value = InviteRecordID;
            return SqlHelper.ExecuteSql(sqlStr, cmdParams);
        }

		/// <summary>
		///查找一条记录
		/// </summary>
		/// <param name="InviteRecord"></param>
		/// <returns></returns>
		public M_InviteRecord GetSelect(int InviteRecordID)
        {
            string sqlStr = "SELECT [id],[userid],[RecommUserId],[RegData],[isValid],[isReset] FROM [dbo].[ZL_InviteRecord] WHERE [id] = @id";
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@id", SqlDbType.Int, 4);
            cmdParams[0].Value = InviteRecordID;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sqlStr, cmdParams))
            {
                if (reader.Read())
                {
                    return initMod.GetModelFromReader(reader);
                }
                else
                {
                    return new M_InviteRecord();
                }
            }
        }
        /// <summary>
        /// 通过用户查询
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public DataTable GetSelectByUserid(int userId)
        {
            string sqlStr = "SELECT [id],[userid],[RecommUserId],[RegData],[isValid],[isReset] FROM [dbo].[ZL_InviteRecord] WHERE userid=@userid ORDER BY [RegData] DESC";
            SqlParameter[] para = new SqlParameter[]{
                new SqlParameter("@userid",userId)
            };
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, para);
        }

        /// <summary>
        /// 获取总数
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int GetCountNum(int userId)
        {
            string sqlStr = "SELECT COUNT(1) FROM [dbo].[ZL_InviteRecord] WHERE userid=@userid AND isReset=0";
            SqlParameter[] para = new SqlParameter[]{
                new SqlParameter("@userid",userId)
            };
            return SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.Text, sqlStr, para));
        }

        /// <summary>
        /// 获取有效用户总数
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int GetCountIsVal(int userId)
        {
            string sqlStr = "SELECT COUNT(1) FROM [dbo].[ZL_InviteRecord] WHERE userid=@userid AND [isValid]=1 AND isReset=0";
            SqlParameter[] para = new SqlParameter[]{
                new SqlParameter("@userid",userId)
            };
            return SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.Text, sqlStr, para));
        }

        /// <summary>
        /// 更新是否为有效用户
        /// </summary>
        /// <param name="isVali"></param>
        /// <returns></returns>
        public bool GetUpdateVali(int isVali,int id)
        {
            string sqlStr = "UPDATE [dbo].[ZL_InviteRecord] SET [isValid] = @isValid WHERE [id] = @id";
            SqlParameter[] cmdParams = new SqlParameter[]{
                new SqlParameter("@isValid",isVali),
                new SqlParameter("@id",id)
            };
            return SqlHelper.ExecuteSql(sqlStr, cmdParams);
        }

        /// <summary>
        /// 更新是否重置
        /// </summary>
        /// <returns></returns>
        public bool GetUpdateSet(int userid, int reset)
        {
            string sqlStr = "UPDATE [dbo].[ZL_InviteRecord] SET [isReset] = @isReset WHERE [userid] = @userid";
            SqlParameter[] cmdParams = new SqlParameter[]{
                new SqlParameter("@isReset",reset),
                new SqlParameter("@userid",userid)
            };
            return SqlHelper.ExecuteSql(sqlStr, cmdParams);
        }

        /// <summary>
        /// 是否重置和用户查询
        /// </summary>
        /// <param name="isCount">是否重置</param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public DataTable GetSelecByResetAndUser(int isReset, int userId)
        {
            string sqlStr = "SELECT [id],[userid],[RecommUserId],[RegData],[isValid],[isReset] FROM [dbo].[ZL_InviteRecord] WHERE userId=@userId AND isReset=@isReset ORDER BY [RegData] DESC";
            SqlParameter[] para = new SqlParameter[]{
                new SqlParameter("@userId",userId),
                new SqlParameter("@isReset",isReset)
            };
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, para);
        }

         /// <summary>
        /// 是否重置和用户查询
        /// </summary>
        /// <param name="isCount">是否重置</param>
        /// <param name="userId">用户ID</param>
        /// <param name="Vali">是否有效</param>
        /// <returns></returns>
        public DataTable GetSelecByResetAndUser(int isReset, int userId,int Vali)
        {
            string sqlStr = "SELECT [id],[userid],[RecommUserId],[RegData],[isValid],[isReset] FROM [dbo].[ZL_InviteRecord] WHERE userId=@userId AND isReset=@isReset AND isValid=@isValid ORDER BY [RegData] DESC";
            SqlParameter[] para = new SqlParameter[]{
                new SqlParameter("@userId",userId),
                new SqlParameter("@isReset",isReset),
                new SqlParameter("@isValid",Vali)
            };
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, para);
        }

         /// <summary>
        /// 根据推广注册用户查询
        /// </summary>
        /// <param name="RecommUserId"></param>
        /// <returns></returns>
        public M_InviteRecord GetSelByRuid(int RecommUserId)
        {
            string sqlStr = "SELECT [id],[userid],[RecommUserId],[RegData],[isValid],[isReset] FROM [dbo].[ZL_InviteRecord] WHERE [RecommUserId] = @RecommUserId";
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@RecommUserId", SqlDbType.Int, 4);
            cmdParams[0].Value = RecommUserId;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sqlStr, cmdParams))
            {
                if (reader.Read())
                {
                    return initMod.GetModelFromReader(reader);
                }
                else
                {
                    return new M_InviteRecord();
                }
            }
        }
	}
}
