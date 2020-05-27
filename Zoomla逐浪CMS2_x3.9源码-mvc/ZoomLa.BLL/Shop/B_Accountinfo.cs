namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using ZoomLa.SQLDAL;
    using System.Data.SqlClient;
    public class B_Accountinfo
    {
        private string strTableName, PK;
        private M_Accountinfo initMod = new M_Accountinfo();
        public B_Accountinfo()
        {
            strTableName = initMod.TbName;
            PK = initMod.PK;
        }
        public DataTable Sel(int ID)
        {
            return Sql.Sel(strTableName, PK, ID);
        }
        public M_Accountinfo GetSelect(int ID)
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
        public M_Accountinfo SelReturnModel(string strWhere)
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
        public DataTable Select_All()
        {
            return Sql.Sel(strTableName);
        }
        public bool GetUpdate(M_Accountinfo model)
        {
            return Sql.UpdateByIDs(strTableName, PK, model.id.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
        public bool DeleteByGroupID(int ID)
        {
            return Sql.Del(strTableName, ID);
        }
        public int GetInsert(M_Accountinfo model)
        {
            return Sql.insert(strTableName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        /// <summary>
        /// 更新绑定
        /// </summary>
        /// <param name="id"></param>
        /// <param name="bind"></param>
        /// <returns></returns>
        public bool GetUpdate(int id, int bind)
        {
            string sqlStr = "UPDATE [dbo].[ZL_Accountinfo] SET [Lock] = @Lock WHERE [id] = @id";
            SqlParameter[] cmdParams = new SqlParameter[]{
                new SqlParameter("@Lock",bind),
                new SqlParameter("@id",id)
            };
            return SqlHelper.ExecuteSql(sqlStr, cmdParams);
        }
        /// <summary>
        /// 根据用户查询
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public M_Accountinfo GetSelectByuserId(int userId)
        {
            string sqlStr = "SELECT [id],[BankOfDeposit],[NameOfDeposit],[Account],[Name],[CardID],[Lock],[UserId],[payid] FROM [dbo].[ZL_Accountinfo] WHERE userId=@userId";
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@userId", SqlDbType.Int, 4);
            cmdParams[0].Value = userId;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sqlStr, cmdParams))
            {
                if (reader.Read())
                {
                    return initMod.GetModelFromReader(reader);
                }
                else
                {
                    return new M_Accountinfo();
                }
            }
        }
        /// <summary>
        /// 修改绑定状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="locked"></param>
        /// <returns></returns>
        public bool GetUpdate(int id, int locked, string name)
        {
            string sqlStr = "UPDATE [dbo].[ZL_Accountinfo] SET [Lock] = @Lock,[Name] = @Name WHERE [id] = @id";
            SqlParameter[] cmdParams = new SqlParameter[]{
                new SqlParameter("@Lock",locked),
                new SqlParameter("@Name",name),
                new SqlParameter("@id",id)
            };
            return SqlHelper.ExecuteSql(sqlStr, cmdParams);
        }
        /// <summary>
        /// 用户ID和开户行搜索
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="bank"></param>
        /// <returns></returns>
        public DataTable GetSelectByCondi(int userid, string bank)
        {
            string sqlStr = "SELECT * FROM ZL_Accountinfo WHERE 1=1";
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("bank", "%" + bank + "%") };
            if (userid > 0)
            {
                sqlStr += "AND userId=" + userid;
            }
            if (!string.IsNullOrEmpty(bank))
            {
                sqlStr += "AND BankOfDeposit LIKE @bank";
            }
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, sp);
        }
    }
}