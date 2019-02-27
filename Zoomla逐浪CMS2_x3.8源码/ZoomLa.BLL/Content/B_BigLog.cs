namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using ZoomLa.SQLDAL;
    using System.Data.SqlClient;
    using SQLDAL.SQL;
    public class B_BigLog
    {
        public B_BigLog()
        {
            PK = initmod.PK;
            strTableName = initmod.TbName;
        }
        private string PK, strTableName;
        private M_BigLog initmod = new M_BigLog();
        public DataTable Sel(int ID)
        {
            return Sql.Sel(strTableName, PK, ID);
        }
        public M_BigLog GetSelect(int ID)
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
        private M_BigLog SelReturnModel(string strWhere)
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
            PageSetting setting = PageSetting.Single(cpage, psize, strTableName, PK, "");
            DBCenter.SelPage(setting);
            return setting;
        }
        public bool GetUpdate(M_BigLog model)
        {
            return Sql.UpdateByIDs(strTableName, PK, model.id.ToString(), model.GetFieldAndPara(), model.GetParameters());
        }
        public bool DeleteByGroupID(int ID)
        {
            return Sql.Del(strTableName, ID);
        }

        public int GetInsert(M_BigLog model)
        {
            return Sql.insert(strTableName, model.GetParameters(), model.GetParas(), model.GetFields());
        }
        public DataTable SelectBysearch(int search)
        {
            string sqlStr = "SELECT [id],[title],[type],[num],[Cid],[Ctime],[userID] FROM [dbo].[ZL_BigLog]";
            switch (search)
            {
                case 30:
                    sqlStr += " where datediff(m,[Ctime],getdate())=0 order by Ctime desc";
                    break;
                case 7:
                    sqlStr += " where datediff(ww,[Ctime],getdate())=0 order by Ctime desc";
                    break;
                case 0:
                    sqlStr += " where datediff(day,[Ctime],getdate())=0 order by Ctime desc";
                    break;
                case -1:
                    sqlStr += " where datediff(day,[Ctime],getdate())=1 order by Ctime desc";
                    break;
                case -2:
                    sqlStr += " where datediff(day,[Ctime],getdate())=2 order by Ctime desc";
                    break;
                case -7:
                    sqlStr += " where datediff(ww,[Ctime],getdate())=1 order by Ctime desc";
                    break;
                case -30:
                    sqlStr += " where datediff(m,[Ctime],getdate())=1 order by Ctime desc";
                    break;
                case -365:
                    sqlStr += " where datediff(yyyy,[Ctime],getdate())=1 order by Ctime desc";
                    break;
            }
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, null);
        }

        public DataTable GetInfoByUserName(string name)
        {
            string sqlStr = "SELECT [dbo].[ZL_BigLog].[id],[dbo].[ZL_BigLog].[title],[dbo].[ZL_BigLog].[type],[dbo].[ZL_BigLog].[num],[dbo].[ZL_BigLog].[Cid],[dbo].[ZL_BigLog].[Ctime],[dbo].[ZL_BigLog].[userID] FROM [dbo].[ZL_BigLog],[dbo].[ZL_User] where [dbo].[ZL_User].UserID=[dbo].[ZL_BigLog].UserID and [dbo].[ZL_User].UserName=@name";
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("name", name) };
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, sp);
        }
    }
}