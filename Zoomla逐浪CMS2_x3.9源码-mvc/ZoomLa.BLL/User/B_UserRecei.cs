namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using ZoomLa.SQLDAL;
    using System.Data.SqlClient;
	
	/// <summary>
    /// B_UserRecei 的摘要说明
    /// </summary>
    public class B_UserRecei
    {
        public string strTableName = "";
        public string PK = "";
        private M_UserRecei initMod = new M_UserRecei();
        public DataTable dt = null; 
        public B_UserRecei()
        {
            strTableName = initMod.TbName;
            PK = initMod.PK;
        }
        public int GetInsert(M_UserRecei model)
        {
            return Sql.insertID(strTableName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
		public bool GetUpdate(M_UserRecei userRecei)
        {
            string sqlStr = "UPDATE [dbo].[ZL_UserRecei] SET [ReceivName] = @ReceivName,[Street] = @Street,[Zipcode] = @Zipcode,[phone] = @phone,[MobileNum] = @MobileNum,[isDefault] = @isDefault,[UserID] = @UserID,[Provinces]=@Provinces,[Email]=@Email WHERE [ID] = @ID";
            SqlParameter[] cmdParams = userRecei.GetParameters();
            return SqlHelper.ExecuteSql(sqlStr, cmdParams);
        }
        public void U_DelByID(int id, int uid)
        {
            string sql = "Delete From " + strTableName + " Where ID=" + id + " And UserID=" + uid;
            SqlHelper.ExecuteNonQuery(CommandType.Text, sql);
        }
		public bool DeleteByGroupID(int UserReceiID)
        {
            string sqlStr = "DELETE FROM [dbo].[ZL_UserRecei] WHERE [ID] = @ID";
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@ID", SqlDbType.Int, 4);
            cmdParams[0].Value = UserReceiID;
            return SqlHelper.ExecuteSql(sqlStr, cmdParams);
        }
		public M_UserRecei GetSelect(int UserReceiID)
        {
            string sqlStr = "SELECT * FROM [dbo].[ZL_UserRecei] WHERE [ID] = @ID";
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@ID", SqlDbType.Int, 4);
            cmdParams[0].Value = UserReceiID;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sqlStr, cmdParams))
            {
                if (reader.Read())
                {
                    return initMod.GetModelFromReader(reader);
                }
                else
                {
                    return new M_UserRecei();
                }
            }
        }
        public M_UserRecei GetSelect(int id, int uid)
        {
            string sql = " Where UserID=" + uid + " And ID=" + id;
            using (SqlDataReader reader = Sql.SelReturnReader(strTableName, sql))
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
		public DataTable Select_All()
        {
            return Sql.Sel(strTableName); 
        }
        public DataTable SelByUID(int uid)
        {
            string sql = "Select * From " + strTableName + " Where UserID=" + uid + " Order By isDefault DESC";
            return SqlHelper.ExecuteTable(CommandType.Text, sql);
        }
        public DataTable Select_userID(int userid, int isDefault)
        {
            string sqlStr = "SELECT * FROM [dbo].[ZL_UserRecei]  WHERE UserID=" + userid;
            if (isDefault >= 0)
            {
                sqlStr += " AND [isDefault]=" + isDefault;
            }
            sqlStr += " ORDER BY ID DESC";
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, null);
        }
        public bool DelByIDS(string ids)
        {
            SafeSC.CheckIDSEx(ids);
            string sql = "Delete From [dbo].[ZL_UserRecei] Where ID IN (" + ids + ")";
            return SqlHelper.ExecuteSql(sql);
        }

        /// <summary>
        /// 修改地址默认值
        /// </summary>
        /// <param name="ids">地址薄ID</param>
        /// <param name="isDefault">是否默认：0为默认,1为否</param>
        /// <param name="type">修改类型:0为修改本ID的数据,1为排除该ID的其他所有数据</param>
        /// <returns></returns>
        public bool GetUpdate(int ids, int isDefault, int type)
        {
            string sqlStr = "";
            if (type == 0)  //修改该ID的数据
            {
                sqlStr = "UPDATE [dbo].[ZL_UserRecei] SET [isDefault]=@isDefault WHERE ID=@ID";
            }
            else
            {
                sqlStr = "UPDATE [dbo].[ZL_UserRecei] SET [isDefault]=@isDefault WHERE ID != @ID";
            }
            SqlParameter[] para = new SqlParameter[]{
                new SqlParameter("@isDefault",isDefault),
                new SqlParameter("@ID",ids)
            };
            return SqlHelper.ExecuteSql(sqlStr, para); 
        }
        /// <summary>
        /// 查询该用户地址薄数量
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <returns></returns>
        public int SelecCount(int userid)
        {
            string sqlstr = "SELECT count(1) FROM [dbo].[ZL_UserRecei] WHERE UserID=@UserID";
            SqlParameter[] para = new SqlParameter[]{
                    new SqlParameter("@UserID",userid)
                };
            return SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.Text, sqlstr, para));
        }
        /// <summary>
        /// 设置为默认地址
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool SetDef(int id)
        {
            SqlHelper.ExecuteSql("UPDATE "+strTableName+" SET isDefault=0");
            return SqlHelper.ExecuteSql("UPDATE " + strTableName + " SET isDefault=1 WHERE ID=" + id);
        }

	}
}
