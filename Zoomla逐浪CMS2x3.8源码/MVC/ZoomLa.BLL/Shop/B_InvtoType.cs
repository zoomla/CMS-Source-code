namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using ZoomLa.SQLDAL;
    using System.Data.SqlClient;
    using SQLDAL.SQL;
    public class B_InvtoType
    {
        private string strTableName, PK;
        private M_InvtoType initMod = new M_InvtoType();
        public B_InvtoType()
        {
            strTableName = initMod.TbName;
            PK = initMod.PK;
        }
        public DataTable Sel(int ID)
        {
            return Sql.Sel(strTableName, PK, ID);
        }
        public M_InvtoType SelReturnModel(int ID)
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
        private M_InvtoType SelReturnModel(string strWhere)
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
        public PageSetting SelPage(int cpage, int psize)
        {
            PageSetting setting = PageSetting.Single(cpage, psize, strTableName, PK, "");
            DBCenter.SelPage(setting);
            return setting;
        }
        /// <summary>
        /// 根据ID更新
        /// </summary>
        public bool UpdateByID(M_InvtoType model)
        {
            return Sql.UpdateByIDs(strTableName, PK, model.ID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
        public bool Del(int ID)
        {
            return Sql.Del(strTableName, ID);
        }
        public int insert(M_InvtoType model)
        {
            return Sql.insert(strTableName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
		/// <summary>
		///添加记录
		/// </summary>
		/// <param name="InvtoType"></param>
		/// <returns></returns>
        public int GetInsert(M_InvtoType model)
        {
            //return dal.GetInsert(invtoType);
            string sqlStr = "INSERT INTO [dbo].[ZL_InvtoType] ([InvtoType],[Invto],[Addtime],[AdminID],[Remark]) VALUES (@InvtoType,@Invto,@Addtime,@AdminID,@Remark);select @@IDENTITY";
            SqlParameter[] cmdParams = model.GetParameters();
            return SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.Text, sqlStr, cmdParams));
        }

		/// <summary>
		///更新记录
		/// </summary>
		/// <param name="InvtoType"></param>
		/// <returns></returns>
        public bool GetUpdate(M_InvtoType model)
        {
            //return dal.GetUpdate(invtoType);
            string sqlStr = "UPDATE [dbo].[ZL_InvtoType] SET [InvtoType] = @InvtoType,[Invto] = @Invto,[Addtime] = @Addtime,[AdminID] = @AdminID,[Remark]=@Remark WHERE [ID] = @ID";
            SqlParameter[] cmdParams = model.GetParameters();
            return SqlHelper.ExecuteSql(sqlStr, cmdParams);
        }

		/// <summary>
		///删除记录
		/// </summary>
		/// <param name="InvtoType"></param>
		/// <returns></returns>
		public bool DeleteByGroupID(int InvtoTypeID)
        {
            //return dal.DeleteByGroupID(InvtoTypeID);
            string sqlStr = "DELETE FROM [dbo].[ZL_InvtoType] WHERE [ID] = @ID";
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@ID", SqlDbType.Int, 4);
            cmdParams[0].Value = InvtoTypeID;
            return SqlHelper.ExecuteSql(sqlStr, cmdParams);
        }

		/// <summary>
		///查找一条记录
		/// </summary>
		/// <param name="InvtoType"></param>
		/// <returns></returns>
		public M_InvtoType GetSelect(int InvtoTypeID)
        {
            //return dal.GetSelect(InvtoTypeID);
            string sqlStr = "SELECT [ID],[InvtoType],[Invto],[Addtime],[AdminID],[Remark] FROM [dbo].[ZL_InvtoType] WHERE [ID] = @ID";
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@ID", SqlDbType.Int, 4);
            cmdParams[0].Value = InvtoTypeID;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sqlStr, cmdParams))
            {
                if (reader.Read())
                {
                    return initMod.GetModelFromReader(reader);
                }
                else
                {
                    return new M_InvtoType();
                }
            }
        }

		/// <summary>
		///返回所有记录
		/// </summary>
		/// <returns></returns>
		public DataTable Select_All()
        {
            //return dal.Select_All();
            string sqlStr = "SELECT [ID],[InvtoType],[Invto],[Addtime],[AdminID],[Remark] FROM [dbo].[ZL_InvtoType]";
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, null);
        }

	}
}
