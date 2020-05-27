namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using ZoomLa.SQLDAL;
    using System.Data.SqlClient;
    using SQLDAL.SQL;
    public class B_Sns_Kiss
    {
        private string strTableName ,PK;
        private M_Sns_Kiss initMod = new M_Sns_Kiss();
        public B_Sns_Kiss()
        {
            strTableName = initMod.TbName;
            PK = initMod.PK;
        }
        public DataTable Sel(int ID)
        {
            return Sql.Sel(strTableName, PK, ID);
        }
        public M_Sns_Kiss SelReturnModel(int ID)
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
        public PageSetting SelPage(int cpage, int psize)
        {
            PageSetting setting = PageSetting.Single(cpage, psize, strTableName, PK, "");
            DBCenter.SelPage(setting);
            return setting;
        }
        public bool UpdateByID(M_Sns_Kiss model)
        {
            return Sql.UpdateByIDs(strTableName, PK, model.IsRead.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters(model));
        }

        public bool Del(int ID)
        {
            return Sql.Del(strTableName, ID);
        }
        public int insert(M_Sns_Kiss model)
        {
            return Sql.insert(strTableName, model.GetParameters(model), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        public int GetInsert(M_Sns_Kiss snsKiss)
        {
            string sqlStr = "INSERT INTO [dbo].[ZL_Sns_Kiss] ([Title],[Inputer],[Sendto],[Otherdel],[Content],[InputerID],[SendtoID],[SendTime],[ReadTime],[IsRead]) VALUES (@Title,@Inputer,@Sendto,@Otherdel,@Content,@InputerID,@SendtoID,@SendTime,@ReadTime,@IsRead);select @@IDENTITY";
            SqlParameter[] cmdParams = snsKiss.GetParameters(snsKiss);
            return SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.Text, sqlStr, cmdParams));
        }
        public bool GetUpdate(M_Sns_Kiss snsKiss)
        {
            string sqlStr = "UPDATE [dbo].[ZL_Sns_Kiss] SET [Title] = @Title,[Inputer] = @Inputer,[Sendto] = @Sendto,[Otherdel] = @Otherdel,[Content] = @Content,[InputerID] = @InputerID,[SendtoID] = @SendtoID,[SendTime] = @SendTime,[ReadTime] = @ReadTime,[IsRead] = @IsRead WHERE [id] = @id";
            SqlParameter[] cmdParams = snsKiss.GetParameters(snsKiss);
            return SqlHelper.ExecuteSql(sqlStr, cmdParams);
        }
        public bool DeleteByGroupID(int Sns_KissID)
        {
            string sqlStr = "DELETE FROM [dbo].[ZL_Sns_Kiss] WHERE [id] = @id";
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@id", SqlDbType.Int, 4);
            cmdParams[0].Value = Sns_KissID;
            return SqlHelper.ExecuteSql(sqlStr, cmdParams);
        }
        public M_Sns_Kiss GetSelect(int Sns_KissID)
        {
            string sqlStr = "SELECT [id],[Title],[Inputer],[Sendto],[Otherdel],[Content],[InputerID],[SendtoID],[SendTime],[ReadTime],[IsRead] FROM [dbo].[ZL_Sns_Kiss] WHERE [id] = @id";
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@id", SqlDbType.Int, 4);
            cmdParams[0].Value = Sns_KissID;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sqlStr, cmdParams))
            {
                if (reader.Read())
                {
                    return initMod.GetModelFromReader(reader);
                }
                else
                {
                    return new M_Sns_Kiss();
                }
            }
        }
        public DataTable SelectTable(int Sns_KissID)
        {
            string sqlStr = "SELECT [id],[Title],[Inputer],[Sendto],[Otherdel],[Content],[InputerID],[SendtoID],[SendTime],[ReadTime],[IsRead] FROM [dbo].[ZL_Sns_Kiss] WHERE [id] = @id";
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@id", SqlDbType.Int, 4);
            cmdParams[0].Value = Sns_KissID;
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, cmdParams);
        }
        public DataTable Select_All()
        {
            string sqlStr = "SELECT [id],[Title],[Inputer],[Sendto],[Otherdel],[Content],[InputerID],[SendtoID],[SendTime],[ReadTime],[IsRead] FROM [dbo].[ZL_Sns_Kiss]";
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, null);
        }
        public DataTable Select_All(int userid)
        {
            string sqlStr = "SELECT [id],[Title],[Inputer],[Sendto],[Otherdel],[Content],[InputerID],[SendtoID],[SendTime],[ReadTime],[IsRead] FROM [dbo].[ZL_Sns_Kiss] where InputerID=@id";
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@id", SqlDbType.Int, 4);
            cmdParams[0].Value = userid;
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, cmdParams);
        }
    }
}
