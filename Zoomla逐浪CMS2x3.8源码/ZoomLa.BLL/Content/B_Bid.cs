namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using ZoomLa.SQLDAL;
    using System.Data.SqlClient;
    using SQLDAL.SQL;
    using System.Collections.Generic;
    public class B_Bid
    {
        public B_Bid()
        {
            PK = initmod.PK;
            strTableName = initmod.TbName;
        }
        private string PK, strTableName;
        private M_Bid initmod = new M_Bid();
        public DataTable Sel(int ID)
        {
            return Sql.Sel(strTableName, PK, ID);
        }
        public M_Bid GetSelect(int ID)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(strTableName, PK, ID))
            {
                if (reader.Read())
                {
                    return initmod.GetModelFromReader(reader);
                }
                else
                {
                    return null;
                }
            }
        }
        private M_Bid SelReturnModel(string strWhere)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(strTableName, strWhere))
            {
                if (reader.Read())
                {
                    return initmod.GetModelFromReader(reader);
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
        public PageSetting SelPage(int cpage, int psize)
        {
            string where = "1=1 ";
            PageSetting setting = PageSetting.Single(cpage, psize, strTableName, PK, where);
            DBCenter.SelPage(setting);
            return setting;
        }
        public bool GetUpdate(M_Bid model)
        {
            return Sql.UpdateByIDs(strTableName, PK, model.Id.ToString(), model.GetFieldAndPara(), model.GetParameters());
        }
        public bool DeleteByGroupID(int ID)
        {
            return Sql.Del(strTableName, ID);
        }
        public int GetInsert(M_Bid model)
        {
            return Sql.insert(strTableName, model.GetParameters(), model.GetParas(), model.GetFields());
        }
        public void GetInsertOrUpdate(M_Bid bid)
        {
            string sqlStr = "SELECT [id],[Participation],[Compliance],[Successful],[Website] FROM [dbo].[ZL_Bid]";
            DataTable talist = SqlHelper.ExecuteTable(CommandType.Text, sqlStr, null);

            if (talist.Rows.Count == 0)
            {
                string sqlStrs = "INSERT INTO [dbo].[ZL_Bid] ([Participation],[Compliance],[Successful],[Website]) VALUES (@Participation,@Compliance,@Successful,@Website);select @@IDENTITY";
                SqlParameter[] cmdParams = bid.GetParameters();
                SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.Text, sqlStrs, cmdParams));
            }
            else
            {
                bid.Id = DataConverter.CLng(talist.Rows[0]["id"]);
                string sqlStrd = "UPDATE [dbo].[ZL_Bid] SET [Participation] = @Participation,[Compliance] = @Compliance,[Successful] = @Successful,[Website] = @Website WHERE [id] = @id";
                SqlParameter[] cmdParams = bid.GetParameters();
                SqlHelper.ExecuteSql(sqlStrd, cmdParams);
            }
        }
    }
}