namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using ZoomLa.SQLDAL;
    using System.Data.SqlClient;
    using SQLDAL.SQL;
    public class B_Page
    {
        private string strTableName = "ZL_Page", PK = "ID";
        private M_Page initMod = new M_Page();
        public DataTable Sel(int ID)
        {
            return Sql.Sel(strTableName, PK, ID);
        }
        public M_Page SelReturnModel(int ID)
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
        public bool UpdateByID(M_Page model)
        {
            return Sql.UpdateByIDs(strTableName, PK, model.ID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
        public bool Del(int ID)
        {
            return Sql.Del(strTableName, ID);
        }
        public int insert(M_Page model)
        {
            return Sql.insert(strTableName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        public B_Page()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        public int GetInsert(M_Page page)
        {
            string sqlStr = "SET IDENTITY_INSERT [ZL_Page] ON;INSERT INTO [dbo].[ZL_Page] ([ID],[Proname],[PageTitle],[KeyWords],[Description],[LOGO],[Domain],[Style],[HeadHeight],[HeadColor],[CommonModelID],[TableName],[Hits],[InfoID],[Best],[Status],[UserID],[UserName],[ParentPageID],[ParentUserID],[PageInfo],[TopWords],[BottonWords],[CreateTime],[NodeStyle],[HeadBackGround]) VALUES (@ID,@Proname,@PageTitle,@KeyWords,@Description,@LOGO,@Domain,@Style,@HeadHeight,@HeadColor,@CommonModelID,@TableName,@Hits,@InfoID,@Best,@Status,@UserID,@UserName,@ParentPageID,@ParentUserID,@PageInfo,@TopWords,@BottonWords,@CreateTime,@NodeStyle,@HeadBackGround);select @@IDENTITY;SET IDENTITY_INSERT [ZL_Page] OFF;";
            SqlParameter[] cmdParams = page.GetParameters();
            return SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.Text, sqlStr, cmdParams));
        }
        public bool GetUpdate(M_Page page)
        {
            string sqlStr = "UPDATE [dbo].[ZL_Page] SET [Proname]=@Proname,[PageTitle] = @PageTitle,[KeyWords] = @KeyWords,[Description] = @Description,[LOGO] = @LOGO,[Domain] = @Domain,[Style] = @Style,[HeadHeight] = @HeadHeight,[HeadColor] = @HeadColor,[CommonModelID] = @CommonModelID,[TableName] = @TableName,[Hits] = @Hits,[InfoID] = @InfoID,[Best] = @Best,[Status] = @Status,[UserID] = @UserID,[UserName] = @UserName,[ParentPageID] = @ParentPageID,[ParentUserID] = @ParentUserID,[PageInfo] = @PageInfo,[TopWords] = @TopWords,[BottonWords] = @BottonWords,[CreateTime] = @CreateTime,[NodeStyle]=@NodeStyle,[HeadBackGround]=@HeadBackGround WHERE [ID] = @ID";
            SqlParameter[] cmdParams = page.GetParameters();
            return SqlHelper.ExecuteSql(sqlStr, cmdParams);
        }
        public bool DeleteByGroupID(int PageID)
        {
            string sqlStr = "DELETE FROM [dbo].[ZL_Page] WHERE [ID] = @ID";
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@ID", SqlDbType.Int, 4);
            cmdParams[0].Value = PageID;
            return SqlHelper.ExecuteSql(sqlStr, cmdParams);
        }
        public M_Page GetSelect(int PageID)
        {

            string sqlStr = "SELECT [ID],[Proname],[PageTitle],[KeyWords],[Description],[LOGO],[Domain],[Style],[HeadHeight],[HeadColor],[CommonModelID],[TableName],[Hits],[InfoID],[Best],[Status],[UserID],[UserName],[ParentPageID],[ParentUserID],[PageInfo],[TopWords],[BottonWords],[CreateTime],[NodeStyle],[HeadBackGround] FROM [dbo].[ZL_Page] WHERE [ID] = @ID";
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@ID", SqlDbType.Int, 4);
            cmdParams[0].Value = PageID;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sqlStr, cmdParams))
            {
                if (reader.Read())
                {
                    return initMod.GetModelFromReader(reader);
                }
                else
                {
                    return new M_Page();
                }
            }
        }
        public M_Page GetSelectByCommonModelID(int CommonModelID) {
            string sqlStr = "SELECT * FROM [ZL_Page] WHERE [CommonModelID] = @CommonModelID";
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@CommonModelID", SqlDbType.Int, 4);
            cmdParams[0].Value = CommonModelID;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sqlStr, cmdParams))
            {
                if (reader.Read())
                {
                    return initMod.GetModelFromReader(reader);
                }
                else
                {
                    return new M_Page();
                }
            }
        }
        public DataTable Select_All()
        {
            string sqlStr = "SELECT [ID],[Proname],[PageTitle],[KeyWords],[Description],[LOGO],[Domain],[Style],[HeadHeight],[HeadColor],[CommonModelID],[TableName],[Hits],[InfoID],[Best],[Status],[UserID],[UserName],[ParentPageID],[ParentUserID],[PageInfo],[TopWords],[BottonWords],[CreateTime],[NodeStyle],[HeadBackGround] FROM [dbo].[ZL_Page]";
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, null);
        }
        public M_Page GetSelectByUserName(string UserName)
        {
            string sqlStr = "SELECT [ID],[Proname],[PageTitle],[KeyWords],[Description],[LOGO],[Domain],[Style],[HeadHeight],[HeadColor],[CommonModelID],[TableName],[Hits],[InfoID],[Best],[Status],[UserID],[UserName],[ParentPageID],[ParentUserID],[PageInfo],[TopWords],[BottonWords],[CreateTime],[NodeStyle],[HeadBackGround] FROM [dbo].[ZL_Page] WHERE [UserName] = @UserName";
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@UserName", SqlDbType.NVarChar, 255);
            cmdParams[0].Value = UserName;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sqlStr, cmdParams))
            {
                if (reader.Read())
                {
                    return initMod.GetModelFromReader(reader);
                }
                else
                {
                    return new M_Page();
                }
            }
        }
        public M_Page GetSelectByUserID(int UserID)
        {
            string sqlStr = "SELECT [ID],[Proname],[PageTitle],[KeyWords],[Description],[LOGO],[Domain],[Style],[HeadHeight],[HeadColor],[CommonModelID],[TableName],[Hits],[InfoID],[Best],[Status],[UserID],[UserName],[ParentPageID],[ParentUserID],[PageInfo],[TopWords],[BottonWords],[CreateTime],[NodeStyle],[HeadBackGround] FROM [dbo].[ZL_Page] WHERE [UserID] = @UserID";
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@UserID", SqlDbType.Int, 4);
            cmdParams[0].Value = UserID;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sqlStr, cmdParams))
            {
                if (reader.Read())
                {
                    return initMod.GetModelFromReader(reader);
                }
                else
                {
                    return new M_Page();
                }
            }
        }
        public bool GetUpdateByUserID(M_Page page)
        {
            string sqlStr = "UPDATE [dbo].[ZL_Page] SET [Proname]=@Proname,[PageTitle] = @PageTitle,[KeyWords] = @KeyWords,[Description] = @Description,[LOGO] = @LOGO,[Domain] = @Domain,[Style] = @Style,[HeadHeight] = @HeadHeight,[HeadColor] = @HeadColor,[CommonModelID] = @CommonModelID,[TableName] = @TableName,[Hits] = @Hits,[InfoID] = @InfoID,[Best] = @Best,[Status] = @Status,[UserName] = @UserName,[ParentPageID] = @ParentPageID,[ParentUserID] = @ParentUserID,[PageInfo] = @PageInfo,[TopWords] = @TopWords,[BottonWords] = @BottonWords,[CreateTime] = @CreateTime,[NodeStyle]=@NodeStyle,[HeadBackGround]=@HeadBackGround WHERE [UserID] = @UserID";
            SqlParameter[] cmdParams = initMod.GetParameters();
            return SqlHelper.ExecuteSql(sqlStr, cmdParams);
        }
        public bool GetUpdateByUserName(M_Page page)
        {
            string sqlStr = "UPDATE [dbo].[ZL_Page] SET [Proname]=@Proname,[PageTitle] = @PageTitle,[KeyWords] = @KeyWords,[Description] = @Description,[LOGO] = @LOGO,[Domain] = @Domain,[Style] = @Style,[HeadHeight] = @HeadHeight,[HeadColor] = @HeadColor,[CommonModelID] = @CommonModelID,[TableName] = @TableName,[Hits] = @Hits,[InfoID] = @InfoID,[Best] = @Best,[Status] = @Status,[UserName] = @UserName,[ParentPageID] = @ParentPageID,[ParentUserID] = @ParentUserID,[PageInfo] = @PageInfo,[TopWords] = @TopWords,[BottonWords] = @BottonWords,[CreateTime] = @CreateTime,[NodeStyle]=@NodeStyle,[HeadBackGround]=@HeadBackGround WHERE [UserName] = @UserName";
            SqlParameter[] cmdParams = page.GetParameters();
            return SqlHelper.ExecuteSql(sqlStr, cmdParams);
        }
        public M_Page GetSelectByGID(int Gid)
        {
            string sqlStr = "SELECT [ID],[Proname],[PageTitle],[KeyWords],[Description],[LOGO],[Domain],[Style],[HeadHeight],[HeadColor],[CommonModelID],[TableName],[Hits],[InfoID],[Best],[Status],[UserID],[UserName],[ParentPageID],[ParentUserID],[PageInfo],[TopWords],[BottonWords],[CreateTime],[NodeStyle],[HeadBackGround] FROM [dbo].[ZL_Page] WHERE [CommonModelID] = @CommonModelID";
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@CommonModelID", SqlDbType.Int, 4);
            cmdParams[0].Value = Gid;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sqlStr, cmdParams))
            {
                if (reader.Read())
                {
                    return initMod.GetModelFromReader(reader);
                }
                else
                {
                    return new M_Page();
                }
            }
        }

         /// <summary>
        /// 根据上一级用户查询
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public DataTable GetSelectByparentUserid(int userid, int day, DateTime datatime)
        {
            string sqlStr = "SELECT * FROM [dbo].[ZL_Page] WHERE [ParentUserID]=@ParentUserID";
            if (day == -1)  //全年
            {
                sqlStr += " AND CreateTime BETWEEN '" + datatime + "' AND DATEADD(Year,1,'" + datatime + "')";
            }
            if (day == -2)
            {
                sqlStr += " AND CreateTime BETWEEN '" + datatime + "' AND DATEADD(month,1,'" + datatime + "')";
            }
            if (day > 0)
            {
                sqlStr += " AND CreateTime BETWEEN '" + datatime + "' AND DATEADD(DAY," + day + ",'" + datatime + "')";
            }
            SqlParameter[] para = new SqlParameter[]{
                new SqlParameter("@ParentUserID",userid)
            };
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, para);
        }

        /// <summary>
        /// 根据二级域名查询commonmodelID
        /// </summary>
        /// <param name="domain">二级域名</param>
        /// <returns></returns>
        public static string GetCommByDomain(string domain)
        {
            try
            {
                string sqlStr = "SELECT CommonModelID FROM [ZL_Page] WHERE [Domain]=@domain";
                SqlParameter[] sp = new SqlParameter[] { new SqlParameter("domain", domain) };
                return SqlHelper.ExecuteScalar(CommandType.Text, sqlStr,sp).ToString();
            }
            catch
            {
                return "";
            }
        }
    }
}
