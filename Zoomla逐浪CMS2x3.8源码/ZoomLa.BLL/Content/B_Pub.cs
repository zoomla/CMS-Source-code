namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using ZoomLa.SQLDAL;
    using System.Data.SqlClient;
    using System.Collections;
    using SQLDAL.SQL;
    using System.Collections.Generic;
    public class B_Pub
    {
        private M_Pub initMod = new M_Pub();
        public B_Pub()
        {
            strTableName = initMod.TbName;
            PK = initMod.PK;
        }
        private string PK, strTableName;
        public DataTable Sel(int ID)
        {
            return Sql.Sel(strTableName, PK, ID);
        }
        public M_Pub SelReturnModel(int ID)
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
        private M_Pub SelReturnModel(string strWhere)
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
        public bool Getdb(string TableName, int id)
        {
            SafeSC.CheckDataEx(TableName);
            string sql = "update " + TableName + " set Optimal=2 where ID=" + id;
            return SqlHelper.ExecuteSql(sql);
        }
        public bool UpdateByID(M_Pub model)
        {
            return DBCenter.UpdateByID(model,model.Pubid);
        }
        public bool Del(int ID)
        {
            DelTableInfo(ID);
            return Sql.Del(strTableName, "PubID="+ID);
        }
        public bool DelByIDS(string ids) 
        {
            SafeSC.CheckIDSEx(ids);
            string sql = "Delete From "+strTableName+" Where "+PK+" in("+ids+")";
            foreach (string id in ids.Split(','))//删除表
            {
                DelTableInfo(DataConvert.CLng(id));
            }
            return SqlHelper.ExecuteSql(sql);
        }
        private bool DelTableInfo(int id)
        {
            M_Pub pubmod = SelReturnModel(id);
            if (pubmod != null)
            {
                string sql = "DROP TABLE " + pubmod.PubTableName;
                return SqlHelper.ExecuteSql(sql);
            }
            return false;
        }
        public bool RecyleByIDS(string ids)
        {
            SafeSC.CheckIDSEx(ids);
            string sql = "UPDATE " + strTableName + " set PubIsDel = 0 WHERE " + PK + " in (" + ids + ")";
            return SqlHelper.ExecuteSql(sql);
        }
        public bool DelAll() 
        {
            string sql = "Delete From " + strTableName + " Where PubIsDel=1";
            return SqlHelper.ExecuteSql(sql);
        }
        public int insert(M_Pub model)
        {
            return DBCenter.Insert(model);
        }
        public int GetInsert(M_Pub model)
        {
            return insert(model);
        }
        public DataTable SelectNode(int Pubclass)
        {
            string sqlStr = "select * from zl_pub where Pubclass=@Pubclass order by pubid";
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@Pubclass", SqlDbType.Int, 4);
            cmdParams[0].Value = Pubclass;
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, cmdParams);
        }
        public M_Pub GetSelectNode(string nodeid)
        {
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@nodeid", SqlDbType.NVarChar, 255);
            cmdParams[0].Value = nodeid;
            string sql = "select * from ZL_pub where pubnodeid like '%,'+@nodeid+',%' or  pubnodeid like @nodeid+',%' or  pubnodeid like '%,'+@nodeid or pubnodeid=@nodeid";
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sql, cmdParams))
            {
                if (reader.Read())
                {
                    return new M_Pub().GetModelFromReader(reader);
                }
                else
                {
                    return new M_Pub();
                }
            }
        }
        public bool GetUpdate(M_Pub model)
        {
            return DBCenter.UpdateByID(model,model.Pubid);
        }
        /// <summary>
        ///不存在则添加否则更新
        /// </summary>
        public bool InsertUpdate(M_Pub model)
        {
            if (model.Pubid > 0)
                UpdateByID(model);
            else
                insert(model);
            return true;
        }

        /// <summary>
        ///删除记录
        /// </summary>
        /// <param name="Pub"></param>
        /// <returns></returns>
        public bool GetDelete(int ID)
        {
            return Sql.Del(strTableName, PK + "=" + ID);
        }
        /// <summary>
        /// 创建模型和数据表,并增加字段
        /// </summary>
        public int CreateModelInfo(M_Pub pub)
        {
            M_ModelInfo modelinfo = new M_ModelInfo();
            B_Model mll = new B_Model();
            string sqlStr = "";
            string propert1 = "";
            //bool isext = DBHelper.Table_IsExist(pub.PubTableName);
            switch (pub.PubType)
            {
                case 0://评论
                    modelinfo.ModelName = "评论" + pub.PubName + "的模型";
                    modelinfo.Description = "";
                    modelinfo.TableName = pub.PubTableName;
                    modelinfo.ItemName = "评论";
                    modelinfo.ItemUnit = "条";
                    modelinfo.ItemIcon = "GuestBook.gif";
                    modelinfo.ContentModule = "/互动模板/默认评论" + pub.PubName + "模板.html";
                    modelinfo.ModelType = 7;
                    modelinfo.MultiFlag = false;
                    sqlStr = "CREATE TABLE [dbo].[" + pub.PubTableName + "] (  [ID] [int] IDENTITY (1, 1) NOT NULL,  [Pubupid] [int] NULL , [PubUserName] [nvarchar] (255) NULL , [PubUserID] [int] NULL , [PubContentid] [int] NULL ,[PubInputer] [nvarchar] (255) NULL ,[Parentid] [int] NULL DEFAULT (0), [PubIP] [nvarchar] (255) NULL , [Pubnum] [int] NULL DEFAULT (0) , [Pubstart] [int] NULL DEFAULT (0) ,  [PubTitle] [nvarchar] (255) NULL ,[PubContent] [ntext] NULL  , [PubAddTime] [datetime] NULL DEFAULT (getdate()),[Optimal] [int] NULL DEFAULT (0),[cookflag] [nvarchar] (500) NULL) ALTER TABLE [" + pub.PubTableName + "] WITH NOCHECK ADD CONSTRAINT [PK_" + pub.PubTableName + "] PRIMARY KEY  NONCLUSTERED ( [ID] )";
                    mll.AddModel(modelinfo,sqlStr);
                    propert1 = @"sp_addextendedproperty N'MS_Description', N'评论ID', N'user', N'dbo', N'table', N'" + pub.PubTableName + "', N'column', N'ID'";
                    Execute(propert1);
                    propert1 = @"sp_addextendedproperty N'MS_Description', N'互动ID', N'user', N'dbo', N'table', N'" + pub.PubTableName + "', N'column', N'Pubupid'";
                    Execute(propert1);
                    propert1 = @"sp_addextendedproperty N'MS_Description', N'回复用户名', N'user', N'dbo', N'table', N'" + pub.PubTableName + "', N'column', N'PubUserName'";
                    Execute(propert1);
                    propert1 = @"sp_addextendedproperty N'MS_Description', N'回复用户ID', N'user', N'dbo', N'table', N'" + pub.PubTableName + "', N'column', N'PubUserID'";
                    Execute(propert1);
                    propert1 = @"sp_addextendedproperty N'MS_Description', N'内容ID', N'user', N'dbo', N'table', N'" + pub.PubTableName + "', N'column', N'PubContentid'";
                    Execute(propert1);
                    propert1 = @"sp_addextendedproperty N'MS_Description', N'所属ID', N'user', N'dbo', N'table', N'" + pub.PubTableName + "', N'column', N'Parentid'";
                    Execute(propert1);
                    propert1 = @"sp_addextendedproperty N'MS_Description', N'IP地址', N'user', N'dbo', N'table', N'" + pub.PubTableName + "', N'column', N'PubIP'";
                    Execute(propert1);
                    propert1 = @"sp_addextendedproperty N'MS_Description', N'参与人数', N'user', N'dbo', N'table', N'" + pub.PubTableName + "', N'column', N'Pubnum'";
                    Execute(propert1);
                    propert1 = @"sp_addextendedproperty N'MS_Description', N'是否审核', N'user', N'dbo', N'table', N'" + pub.PubTableName + "', N'column', N'Pubstart'";
                    Execute(propert1);
                    propert1 = @"sp_addextendedproperty N'MS_Description', N'评论标题', N'user', N'dbo', N'table', N'" + pub.PubTableName + "', N'column', N'PubTitle'";
                    Execute(propert1);
                    propert1 = @"sp_addextendedproperty N'MS_Description', N'评论回复', N'user', N'dbo', N'table', N'" + pub.PubTableName + "', N'column', N'PubContent'";
                    Execute(propert1);
                    propert1 = @"sp_addextendedproperty N'MS_Description', N'评论时间', N'user', N'dbo', N'table', N'" + pub.PubTableName + "', N'column', N'PubAddTime'";
                    Execute(propert1);
                    propert1 = @"sp_addextendedproperty N'MS_Description', N'设置评论最佳回复', N'user', N'dbo', N'table', N'" + pub.PubTableName + "', N'column', N'Optimal'";
                    Execute(propert1);
                    break;
                case 1://投票
                    modelinfo.ModelName = "投票" + pub.PubName + "的模型";
                    modelinfo.Description = "";
                    modelinfo.TableName = pub.PubTableName;
                    modelinfo.ItemName = "投票";
                    modelinfo.ItemUnit = "条";
                    modelinfo.ItemIcon = "Article.gif";
                    modelinfo.ContentModule = "/互动模板/默认投票" + pub.PubName + "模板.html";
                    modelinfo.ModelType = 7;
                    modelinfo.MultiFlag = false;
                    sqlStr = "CREATE TABLE [dbo].[" + pub.PubTableName + "] (  [ID] [int] IDENTITY (1, 1) NOT NULL ,  [Pubupid] [int] NULL , [PubUserName] [nvarchar] (255) NULL , [PubUserID] [int] NULL ,  [PubContentid] [int] NULL ,[PubInputer] [nvarchar] (255) NULL ,[Parentid] [int] NULL DEFAULT (0), [PubIP] [nvarchar] (255) NULL , [Pubnum] [int] NULL DEFAULT (0) , [Pubstart] [int] NULL DEFAULT (0) , [PubTitle] [nvarchar] (255) NULL , [PubContent] [ntext] NULL  , [PubAddTime] [datetime] NULL DEFAULT (getdate()),[Optimal] [int] NULL DEFAULT (0),[cookflag] [nvarchar] (500) NULL ) ALTER TABLE [" + pub.PubTableName + "] WITH NOCHECK ADD CONSTRAINT [PK_" + pub.PubTableName + "] PRIMARY KEY  NONCLUSTERED ( [ID] )";
                    mll.AddModel(modelinfo, sqlStr);
                    propert1 = @"sp_addextendedproperty N'MS_Description', N'投票ID', N'user', N'dbo', N'table', N'" + pub.PubTableName + "', N'column', N'ID'";
                    Execute(propert1);
                    propert1 = @"sp_addextendedproperty N'MS_Description', N'互动ID', N'user', N'dbo', N'table', N'" + pub.PubTableName + "', N'column', N'Pubupid'";
                    Execute(propert1);
                    propert1 = @"sp_addextendedproperty N'MS_Description', N'回复用户名', N'user', N'dbo', N'table', N'" + pub.PubTableName + "', N'column', N'PubUserName'";
                    Execute(propert1);
                    propert1 = @"sp_addextendedproperty N'MS_Description', N'回复用户ID', N'user', N'dbo', N'table', N'" + pub.PubTableName + "', N'column', N'PubUserID'";
                    Execute(propert1);
                    propert1 = @"sp_addextendedproperty N'MS_Description', N'内容ID', N'user', N'dbo', N'table', N'" + pub.PubTableName + "', N'column', N'PubContentid'";
                    Execute(propert1);
                    propert1 = @"sp_addextendedproperty N'MS_Description', N'所属ID', N'user', N'dbo', N'table', N'" + pub.PubTableName + "', N'column', N'Parentid'";
                    Execute(propert1);
                    propert1 = @"sp_addextendedproperty N'MS_Description', N'IP地址', N'user', N'dbo', N'table', N'" + pub.PubTableName + "', N'column', N'PubIP'";
                    Execute(propert1);
                    propert1 = @"sp_addextendedproperty N'MS_Description', N'参与人数', N'user', N'dbo', N'table', N'" + pub.PubTableName + "', N'column', N'Pubnum'";
                    Execute(propert1);
                    propert1 = @"sp_addextendedproperty N'MS_Description', N'是否审核', N'user', N'dbo', N'table', N'" + pub.PubTableName + "', N'column', N'Pubstart'";
                    Execute(propert1);
                    propert1 = @"sp_addextendedproperty N'MS_Description', N'投票标题', N'user', N'dbo', N'table', N'" + pub.PubTableName + "', N'column', N'PubTitle'";
                    Execute(propert1);
                    propert1 = @"sp_addextendedproperty N'MS_Description', N'投票回复', N'user', N'dbo', N'table', N'" + pub.PubTableName + "', N'column', N'PubContent'";
                    Execute(propert1);
                    propert1 = @"sp_addextendedproperty N'MS_Description', N'投票时间', N'user', N'dbo', N'table', N'" + pub.PubTableName + "', N'column', N'PubAddTime'";
                    Execute(propert1);
                    propert1 = @"sp_addextendedproperty N'MS_Description', N'设置投票最佳回复', N'user', N'dbo', N'table', N'" + pub.PubTableName + "', N'column', N'Optimal'";
                    Execute(propert1);
                    break;
                case 2://活动

                    modelinfo.ModelName = "活动" + pub.PubName + "的模型";
                    modelinfo.Description = "";
                    modelinfo.TableName = pub.PubTableName;
                    modelinfo.ItemName = "活动";
                    modelinfo.ItemUnit = "条";
                    modelinfo.ItemIcon = "Announce.gif";
                    modelinfo.ContentModule = "/互动模板/默认活动" + pub.PubName + "模板.html";
                    modelinfo.ModelType = 7;
                    modelinfo.MultiFlag = false;

                    sqlStr = "CREATE TABLE [dbo].[" + pub.PubTableName + "] (  [ID] [int] IDENTITY (1, 1) NOT NULL ,  [Pubupid] [int] NULL , [PubUserName] [nvarchar] (255) NULL , [PubUserID] [int] NULL , [PubContentid] [int] NULL , [PubInputer] [nvarchar] (255) NULL ,[Parentid] [int] NULL DEFAULT (0),[PubIP] [nvarchar] (255) NULL , [Pubnum] [int] NULL DEFAULT (0) , [Pubstart] [int] NULL DEFAULT (0) , [PubTitle] [nvarchar] (255) NULL , [PubContent] [ntext] NULL  , [PubAddTime] [datetime] NULL DEFAULT (getdate()),[Optimal] [int] NULL DEFAULT (0),[cookflag] [nvarchar] (500) NULL) ALTER TABLE [" + pub.PubTableName + "] WITH NOCHECK ADD CONSTRAINT [PK_" + pub.PubTableName + "] PRIMARY KEY  NONCLUSTERED ( [ID] )";
                    mll.AddModel(modelinfo, sqlStr);
                    propert1 = @"sp_addextendedproperty N'MS_Description', N'活动ID', N'user', N'dbo', N'table', N'" + pub.PubTableName + "', N'column', N'ID'";
                    Execute(propert1);
                    propert1 = @"sp_addextendedproperty N'MS_Description', N'互动ID', N'user', N'dbo', N'table', N'" + pub.PubTableName + "', N'column', N'Pubupid'";
                    Execute(propert1);
                    propert1 = @"sp_addextendedproperty N'MS_Description', N'回复用户名', N'user', N'dbo', N'table', N'" + pub.PubTableName + "', N'column', N'PubUserName'";
                    Execute(propert1);
                    propert1 = @"sp_addextendedproperty N'MS_Description', N'回复用户ID', N'user', N'dbo', N'table', N'" + pub.PubTableName + "', N'column', N'PubUserID'";
                    Execute(propert1);
                    propert1 = @"sp_addextendedproperty N'MS_Description', N'内容ID', N'user', N'dbo', N'table', N'" + pub.PubTableName + "', N'column', N'PubContentid'";
                    Execute(propert1);
                    propert1 = @"sp_addextendedproperty N'MS_Description', N'所属ID', N'user', N'dbo', N'table', N'" + pub.PubTableName + "', N'column', N'Parentid'";
                    Execute(propert1);
                    propert1 = @"sp_addextendedproperty N'MS_Description', N'IP地址', N'user', N'dbo', N'table', N'" + pub.PubTableName + "', N'column', N'PubIP'";
                    Execute(propert1);
                    propert1 = @"sp_addextendedproperty N'MS_Description', N'参与人数', N'user', N'dbo', N'table', N'" + pub.PubTableName + "', N'column', N'Pubnum'";
                    Execute(propert1);
                    propert1 = @"sp_addextendedproperty N'MS_Description', N'是否审核', N'user', N'dbo', N'table', N'" + pub.PubTableName + "', N'column', N'Pubstart'";
                    Execute(propert1);
                    propert1 = @"sp_addextendedproperty N'MS_Description', N'活动标题', N'user', N'dbo', N'table', N'" + pub.PubTableName + "', N'column', N'PubTitle'";
                    Execute(propert1);
                    propert1 = @"sp_addextendedproperty N'MS_Description', N'活动回复', N'user', N'dbo', N'table', N'" + pub.PubTableName + "', N'column', N'PubContent'";
                    Execute(propert1);
                    propert1 = @"sp_addextendedproperty N'MS_Description', N'发动时间', N'user', N'dbo', N'table', N'" + pub.PubTableName + "', N'column', N'PubAddTime'";
                    Execute(propert1);
                    propert1 = @"sp_addextendedproperty N'MS_Description', N'设置活动最佳回复', N'user', N'dbo', N'table', N'" + pub.PubTableName + "', N'column', N'Optimal'";
                    Execute(propert1);
                    break;
                case 3://留言

                    modelinfo.ModelName = "留言" + pub.PubName + "的模型";
                    modelinfo.Description = "";
                    modelinfo.TableName = pub.PubTableName;
                    modelinfo.ItemName = "留言";
                    modelinfo.ItemUnit = "条";
                    modelinfo.ItemIcon = "GuestBook.gif";
                    modelinfo.ContentModule = "/互动模板/默认留言" + pub.PubName + "模板.html";
                    modelinfo.ModelType = 7;
                    modelinfo.MultiFlag = false;

                    sqlStr = "CREATE TABLE [dbo].[" + pub.PubTableName + "] (  [ID] [int] IDENTITY (1, 1) NOT NULL ,  [Pubupid] [int] NULL , [PubUserName] [nvarchar] (255) NULL , [PubUserID] [int] NULL , [PubContentid] [int] NULL , [PubInputer] [nvarchar] (255) NULL ,[Parentid] [int] NULL DEFAULT (0),[PubIP] [nvarchar] (255) NULL , [Pubnum] [int] NULL DEFAULT (0) , [Pubstart] [int] NULL DEFAULT (0) , [PubTitle] [nvarchar] (255) NULL ,[PubContent] [ntext] NULL  , [PubAddTime] [datetime] NULL DEFAULT (getdate()),[Optimal] [int] NULL DEFAULT (0),[cookflag] [nvarchar] (500) NULL) ALTER TABLE [" + pub.PubTableName + "] WITH NOCHECK ADD CONSTRAINT [PK_" + pub.PubTableName + "] PRIMARY KEY  NONCLUSTERED ( [ID] )";
                    mll.AddModel(modelinfo, sqlStr);
                    propert1 = @"sp_addextendedproperty N'MS_Description', N'留言ID', N'user', N'dbo', N'table', N'" + pub.PubTableName + "', N'column', N'ID'";
                    Execute(propert1);
                    propert1 = @"sp_addextendedproperty N'MS_Description', N'互动ID', N'user', N'dbo', N'table', N'" + pub.PubTableName + "', N'column', N'Pubupid'";
                    Execute(propert1);
                    propert1 = @"sp_addextendedproperty N'MS_Description', N'回复用户名', N'user', N'dbo', N'table', N'" + pub.PubTableName + "', N'column', N'PubUserName'";
                    Execute(propert1);
                    propert1 = @"sp_addextendedproperty N'MS_Description', N'回复用户ID', N'user', N'dbo', N'table', N'" + pub.PubTableName + "', N'column', N'PubUserID'";
                    Execute(propert1);
                    propert1 = @"sp_addextendedproperty N'MS_Description', N'内容ID', N'user', N'dbo', N'table', N'" + pub.PubTableName + "', N'column', N'PubContentid'";
                    Execute(propert1);
                    propert1 = @"sp_addextendedproperty N'MS_Description', N'所属ID', N'user', N'dbo', N'table', N'" + pub.PubTableName + "', N'column', N'Parentid'";
                    Execute(propert1);
                    propert1 = @"sp_addextendedproperty N'MS_Description', N'IP地址', N'user', N'dbo', N'table', N'" + pub.PubTableName + "', N'column', N'PubIP'";
                    Execute(propert1);
                    propert1 = @"sp_addextendedproperty N'MS_Description', N'参与人数', N'user', N'dbo', N'table', N'" + pub.PubTableName + "', N'column', N'Pubnum'";
                    Execute(propert1);
                    propert1 = @"sp_addextendedproperty N'MS_Description', N'是否审核', N'user', N'dbo', N'table', N'" + pub.PubTableName + "', N'column', N'Pubstart'";
                    Execute(propert1);
                    propert1 = @"sp_addextendedproperty N'MS_Description', N'留言标题', N'user', N'dbo', N'table', N'" + pub.PubTableName + "', N'column', N'PubTitle'";
                    Execute(propert1);
                    propert1 = @"sp_addextendedproperty N'MS_Description', N'留言回复', N'user', N'dbo', N'table', N'" + pub.PubTableName + "', N'column', N'PubContent'";
                    Execute(propert1);
                    propert1 = @"sp_addextendedproperty N'MS_Description', N'留言时间', N'user', N'dbo', N'table', N'" + pub.PubTableName + "', N'column', N'PubAddTime'";
                    Execute(propert1);
                    propert1 = @"sp_addextendedproperty N'MS_Description', N'设置留言最佳回复', N'user', N'dbo', N'table', N'" + pub.PubTableName + "', N'column', N'Optimal'";
                    Execute(propert1);
                    break;
                case 4://问券

                    modelinfo.ModelName = "问券" + pub.PubName + "的模型";
                    modelinfo.Description = "";
                    modelinfo.TableName = pub.PubTableName;
                    modelinfo.ItemName = "问券";
                    modelinfo.ItemUnit = "条";
                    modelinfo.ItemIcon = "Cosmetic.gif";
                    modelinfo.ContentModule = "/互动模板/默认问券" + pub.PubName + "模板.html";
                    modelinfo.ModelType = 7;
                    modelinfo.MultiFlag = false;
                    //B_Model mll = new B_Model();
                    sqlStr = "CREATE TABLE [dbo].[" + pub.PubTableName + "] (  [ID] [int] IDENTITY (1, 1) NOT NULL ,  [Pubupid] [int] NULL , [PubUserName] [nvarchar] (255) NULL , [PubUserID] [int] NULL , [PubContentid] [int] NULL ,[PubInputer] [nvarchar] (255) NULL ,[Parentid] [int] NULL DEFAULT (0), [PubIP] [nvarchar] (255) NULL , [Pubnum] [int] NULL DEFAULT (0) , [Pubstart] [int] NULL DEFAULT (0) , [PubTitle] [nvarchar] (255) NULL , [PubContent] [ntext] NULL , [PubAddTime] [datetime] NULL DEFAULT (getdate()),[Optimal] [int] NULL DEFAULT (0),[cookflag] [nvarchar] (500) NULL) ALTER TABLE [" + pub.PubTableName + "] WITH NOCHECK ADD CONSTRAINT [PK_" + pub.PubTableName + "] PRIMARY KEY  NONCLUSTERED ( [ID] )";
                    mll.AddModel(modelinfo, sqlStr);
                    propert1 = @"sp_addextendedproperty N'MS_Description', N'问券ID', N'user', N'dbo', N'table', N'" + pub.PubTableName + "', N'column', N'ID'";
                    Execute(propert1);
                    propert1 = @"sp_addextendedproperty N'MS_Description', N'互动ID', N'user', N'dbo', N'table', N'" + pub.PubTableName + "', N'column', N'Pubupid'";
                    Execute(propert1);
                    propert1 = @"sp_addextendedproperty N'MS_Description', N'回复用户名', N'user', N'dbo', N'table', N'" + pub.PubTableName + "', N'column', N'PubUserName'";
                    Execute(propert1);
                    propert1 = @"sp_addextendedproperty N'MS_Description', N'回复用户ID', N'user', N'dbo', N'table', N'" + pub.PubTableName + "', N'column', N'PubUserID'";
                    Execute(propert1);
                    propert1 = @"sp_addextendedproperty N'MS_Description', N'内容ID', N'user', N'dbo', N'table', N'" + pub.PubTableName + "', N'column', N'PubContentid'";
                    Execute(propert1);
                    propert1 = @"sp_addextendedproperty N'MS_Description', N'所属ID', N'user', N'dbo', N'table', N'" + pub.PubTableName + "', N'column', N'Parentid'";
                    Execute(propert1);
                    propert1 = @"sp_addextendedproperty N'MS_Description', N'IP地址', N'user', N'dbo', N'table', N'" + pub.PubTableName + "', N'column', N'PubIP'";
                    Execute(propert1);
                    propert1 = @"sp_addextendedproperty N'MS_Description', N'参与人数', N'user', N'dbo', N'table', N'" + pub.PubTableName + "', N'column', N'Pubnum'";
                    Execute(propert1);
                    propert1 = @"sp_addextendedproperty N'MS_Description', N'是否审核', N'user', N'dbo', N'table', N'" + pub.PubTableName + "', N'column', N'Pubstart'";
                    Execute(propert1);
                    propert1 = @"sp_addextendedproperty N'MS_Description', N'问券标题', N'user', N'dbo', N'table', N'" + pub.PubTableName + "', N'column', N'PubTitle'";
                    Execute(propert1);
                    propert1 = @"sp_addextendedproperty N'MS_Description', N'问券回复', N'user', N'dbo', N'table', N'" + pub.PubTableName + "', N'column', N'PubContent'";
                    Execute(propert1);
                    propert1 = @"sp_addextendedproperty N'MS_Description', N'问券时间', N'user', N'dbo', N'table', N'" + pub.PubTableName + "', N'column', N'PubAddTime'";
                    Execute(propert1);
                    propert1 = @"sp_addextendedproperty N'MS_Description', N'设置问券最佳回复', N'user', N'dbo', N'table', N'" + pub.PubTableName + "', N'column', N'Optimal'";
                    Execute(propert1);
                    break;
                case 5://统计
                    modelinfo.ModelName = "统计" + pub.PubName + "的模型";
                    modelinfo.Description = "";
                    modelinfo.TableName = pub.PubTableName;
                    modelinfo.ItemName = "统计";
                    modelinfo.ItemUnit = "条";
                    modelinfo.ItemIcon = "RedirectLink.gif";
                    modelinfo.ContentModule = "/互动模板/默认统计" + pub.PubName + "模板.html";
                    modelinfo.ModelType = 7;
                    modelinfo.MultiFlag = false;
                    sqlStr = "CREATE TABLE [dbo].[" + pub.PubTableName + "] (  [ID] [int] IDENTITY (1, 1) NOT NULL ,  [Pubupid] [int] NULL , [PubUserName] [nvarchar] (255) NULL , [PubUserID] [int] NULL , [PubContentid] [int] NULL , [PubInputer] [nvarchar] (255) NULL ,[Parentid] [int] NULL DEFAULT (0),  [PubIP] [nvarchar] (255) NULL , [Pubnum] [int] NULL DEFAULT (0) , [Pubstart] [int] NULL DEFAULT (0) , [PubTitle] [nvarchar] (255) NULL , [PubContent] [ntext] NULL , [PubAddTime] [datetime] NULL DEFAULT (getdate()),[Optimal] [int] NULL DEFAULT (0),[cookflag] [nvarchar] (500) NULL) ALTER TABLE [" + pub.PubTableName + "] WITH NOCHECK ADD CONSTRAINT [PK_" + pub.PubTableName + "] PRIMARY KEY  NONCLUSTERED ( [ID] )";
                    mll.AddModel(modelinfo, sqlStr);
                    propert1 = @"sp_addextendedproperty N'MS_Description', N'统计ID', N'user', N'dbo', N'table', N'" + pub.PubTableName + "', N'column', N'ID'";
                    Execute(propert1);
                    propert1 = @"sp_addextendedproperty N'MS_Description', N'互动ID', N'user', N'dbo', N'table', N'" + pub.PubTableName + "', N'column', N'Pubupid'";
                    Execute(propert1);
                    propert1 = @"sp_addextendedproperty N'MS_Description', N'参与用户名', N'user', N'dbo', N'table', N'" + pub.PubTableName + "', N'column', N'PubUserName'";
                    Execute(propert1);
                    propert1 = @"sp_addextendedproperty N'MS_Description', N'参与用户ID', N'user', N'dbo', N'table', N'" + pub.PubTableName + "', N'column', N'PubUserID'";
                    Execute(propert1);
                    propert1 = @"sp_addextendedproperty N'MS_Description', N'内容ID', N'user', N'dbo', N'table', N'" + pub.PubTableName + "', N'column', N'PubContentid'";
                    Execute(propert1);
                    propert1 = @"sp_addextendedproperty N'MS_Description', N'所属ID', N'user', N'dbo', N'table', N'" + pub.PubTableName + "', N'column', N'Parentid'";
                    Execute(propert1);
                    propert1 = @"sp_addextendedproperty N'MS_Description', N'IP地址', N'user', N'dbo', N'table', N'" + pub.PubTableName + "', N'column', N'PubIP'";
                    Execute(propert1);
                    propert1 = @"sp_addextendedproperty N'MS_Description', N'参与人数', N'user', N'dbo', N'table', N'" + pub.PubTableName + "', N'column', N'Pubnum'";
                    Execute(propert1);
                    propert1 = @"sp_addextendedproperty N'MS_Description', N'是否审核', N'user', N'dbo', N'table', N'" + pub.PubTableName + "', N'column', N'Pubstart'";
                    Execute(propert1);
                    propert1 = @"sp_addextendedproperty N'MS_Description', N'统计标题', N'user', N'dbo', N'table', N'" + pub.PubTableName + "', N'column', N'PubTitle'";
                    Execute(propert1);
                    propert1 = @"sp_addextendedproperty N'MS_Description', N'统计备注', N'user', N'dbo', N'table', N'" + pub.PubTableName + "', N'column', N'PubContent'";
                    Execute(propert1);
                    propert1 = @"sp_addextendedproperty N'MS_Description', N'添加时间', N'user', N'dbo', N'table', N'" + pub.PubTableName + "', N'column', N'PubAddTime'";
                    Execute(propert1);
                    propert1 = @"sp_addextendedproperty N'MS_Description', N'设置统计最佳回复', N'user', N'dbo', N'table', N'" + pub.PubTableName + "', N'column', N'Optimal'";
                    Execute(propert1);
                    break;
                case 6://竞标
                    modelinfo.ModelName = "竞标" + pub.PubName + "的模型";
                    modelinfo.Description = "";
                    modelinfo.TableName = pub.PubTableName;
                    modelinfo.ItemName = "竞标";
                    modelinfo.ItemUnit = "条";
                    modelinfo.ItemIcon = "Article.gif";
                    modelinfo.ContentModule = "/互动模板/默认竞标" + pub.PubName + "模板.html";
                    modelinfo.ModelType = 7;
                    modelinfo.MultiFlag = false;
                    sqlStr = "CREATE TABLE [dbo].[" + pub.PubTableName + "] (  [ID] [int] IDENTITY (1, 1) NOT NULL ,  [Pubupid] [int] NULL , [PubUserName] [nvarchar] (255) NULL , [PubUserID] [int] NULL ,  [PubContentid] [int] NULL ,[PubInputer] [nvarchar] (255) NULL ,[Parentid] [int] NULL DEFAULT (0), [PubIP] [nvarchar] (255) NULL , [Pubnum] [int] NULL DEFAULT (0) , [Pubstart] [int] NULL DEFAULT (0) , [PubTitle] [nvarchar] (255) NULL , [PubContent] [ntext] NULL  , [PubAddTime] [datetime] NULL DEFAULT (getdate()),[Optimal] [int] NULL DEFAULT (0),[cookflag] [nvarchar] (500) NULL ) ALTER TABLE [" + pub.PubTableName + "] WITH NOCHECK ADD CONSTRAINT [PK_" + pub.PubTableName + "] PRIMARY KEY  NONCLUSTERED ( [ID] )";
                    mll.AddModel(modelinfo, sqlStr);
                    propert1 = @"sp_addextendedproperty N'MS_Description', N'竞标ID', N'user', N'dbo', N'table', N'" + pub.PubTableName + "', N'column', N'ID'";
                    Execute(propert1);
                    propert1 = @"sp_addextendedproperty N'MS_Description', N'互动ID', N'user', N'dbo', N'table', N'" + pub.PubTableName + "', N'column', N'Pubupid'";
                    Execute(propert1);
                    propert1 = @"sp_addextendedproperty N'MS_Description', N'回复用户名', N'user', N'dbo', N'table', N'" + pub.PubTableName + "', N'column', N'PubUserName'";
                    Execute(propert1);
                    propert1 = @"sp_addextendedproperty N'MS_Description', N'回复用户ID', N'user', N'dbo', N'table', N'" + pub.PubTableName + "', N'column', N'PubUserID'";
                    Execute(propert1);
                    propert1 = @"sp_addextendedproperty N'MS_Description', N'内容ID', N'user', N'dbo', N'table', N'" + pub.PubTableName + "', N'column', N'PubContentid'";
                    Execute(propert1);
                    propert1 = @"sp_addextendedproperty N'MS_Description', N'所属ID', N'user', N'dbo', N'table', N'" + pub.PubTableName + "', N'column', N'Parentid'";
                    Execute(propert1);
                    propert1 = @"sp_addextendedproperty N'MS_Description', N'IP地址', N'user', N'dbo', N'table', N'" + pub.PubTableName + "', N'column', N'PubIP'";
                    Execute(propert1);
                    propert1 = @"sp_addextendedproperty N'MS_Description', N'参与人数', N'user', N'dbo', N'table', N'" + pub.PubTableName + "', N'column', N'Pubnum'";
                    Execute(propert1);
                    propert1 = @"sp_addextendedproperty N'MS_Description', N'是否审核', N'user', N'dbo', N'table', N'" + pub.PubTableName + "', N'column', N'Pubstart'";
                    Execute(propert1);
                    propert1 = @"sp_addextendedproperty N'MS_Description', N'竞标标题', N'user', N'dbo', N'table', N'" + pub.PubTableName + "', N'column', N'PubTitle'";
                    Execute(propert1);
                    propert1 = @"sp_addextendedproperty N'MS_Description', N'竞标回复', N'user', N'dbo', N'table', N'" + pub.PubTableName + "', N'column', N'PubContent'";
                    Execute(propert1);
                    propert1 = @"sp_addextendedproperty N'MS_Description', N'竞标时间', N'user', N'dbo', N'table', N'" + pub.PubTableName + "', N'column', N'PubAddTime'";
                    Execute(propert1);
                    propert1 = @"sp_addextendedproperty N'MS_Description', N'设置竞标最佳回复', N'user', N'dbo', N'table', N'" + pub.PubTableName + "', N'column', N'Optimal'";
                    Execute(propert1);
                    break;
                case 7://评星
                    modelinfo.ModelName = "评星" + pub.PubName + "的模型";
                    modelinfo.Description = "";
                    modelinfo.TableName = pub.PubTableName;
                    modelinfo.ItemName = "评星";
                    modelinfo.ItemUnit = "条";
                    modelinfo.ItemIcon = "Article.gif";
                    modelinfo.ContentModule = "/互动模板/评星" + pub.PubName + "模板.html";
                    modelinfo.ModelType = 7;
                    modelinfo.MultiFlag = false;
                    sqlStr = "CREATE TABLE [dbo].[" + pub.PubTableName + "] (  [ID] [int] IDENTITY (1, 1) NOT NULL ,  [Pubupid] [int] NULL , [PubUserName] [nvarchar] (255) NULL , [PubUserID] [int] NULL ,  [PubContentid] [int] NULL ,[PubInputer] [nvarchar] (255) NULL ,[Parentid] [int] NULL DEFAULT (0), [PubIP] [nvarchar] (255) NULL , [Pubnum] [int] NULL DEFAULT (0) , [Pubstart] [int] NULL DEFAULT (0) , [PubTitle] [nvarchar] (255) NULL , [PubContent] [ntext] NULL  , [PubAddTime] [datetime] NULL DEFAULT (getdate()),[Optimal] [int] NULL DEFAULT (0),[Type] [int] NULL DEFAULT (1),[cookflag] [nvarchar] (500) NULL ) ALTER TABLE [" + pub.PubTableName + "] WITH NOCHECK ADD CONSTRAINT [PK_" + pub.PubTableName + "] PRIMARY KEY  NONCLUSTERED ( [ID] )";
                    mll.AddModel(modelinfo, sqlStr);
                    break;
                case 8://互动表单
                    sqlStr = "CREATE TABLE [dbo].[" + pub.PubTableName + "] ([ID] [int] IDENTITY (1, 1) NOT NULL ,  [Pubupid] [int] NULL , [PubUserName] [nvarchar] (255) NULL , [PubUserID] [int] NULL ,  [PubContentid] [int] NULL ,[PubInputer] [nvarchar] (255) NULL ,[Parentid] [int] NULL DEFAULT (0), [PubIP] [nvarchar] (255) NULL , [Pubnum] [int] NULL DEFAULT (0) , [Pubstart] [int] NULL DEFAULT (0) , [PubTitle] [nvarchar] (255) NULL , [PubContent] [ntext] NULL  , [PubAddTime] [datetime] NULL DEFAULT (getdate()),[Optimal] [int] NULL DEFAULT (0),[cookflag] [nvarchar] (500) NULL ) ALTER TABLE [" + pub.PubTableName + "] WITH NOCHECK ADD CONSTRAINT [PK_" + pub.PubTableName + "] PRIMARY KEY  NONCLUSTERED ( [ID] )";
                    Execute(sqlStr);
                    break;
                default:
                    sqlStr = "";
                    break;
            }
            return modelinfo.ModelID;
        }
        public int GetTypeByName(string name)
        {
            Hashtable ht = new Hashtable();
            ht.Add("评论", 0);
            ht.Add("投票", 1);
            ht.Add("活动", 2);
            ht.Add("留言", 3);
            ht.Add("问券", 4);
            ht.Add("统计", 5);
            ht.Add("竞标", 6);
            return (int)ht[name.Trim()];
        }
        private static void Execute(string sqlStr)
        {
            if (sqlStr != "")
            {
                SqlHelper.ExecuteNonQuery(CommandType.Text, sqlStr, null, true);
            }
        }
   
        /// <summary>
        ///查找一条记录
        /// </summary>
        /// <param name="Pub"></param>
        /// <returns></returns>
        public M_Pub GetSelect(int PubID)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(strTableName, PK, PubID))
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
        public DataTable SelByName(string pname,string pid="") 
        {
            string sql = "Select * From " + strTableName + " Where PubName=@pname ";
            if (!string.IsNullOrEmpty(pid)) sql += " And Pubid<> @pid";
            SqlParameter[] sp = new SqlParameter[] {new SqlParameter("pname",pname),new SqlParameter("pid",pid) };
            return SqlHelper.ExecuteTable(CommandType.Text,sql,sp);
        }
        public DataTable SelByPubTbName(string tbname)
        {
            string sql = "Select * From " + strTableName + " Where PubTableName=@pname";
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("pname", tbname) };
            return SqlHelper.ExecuteTable(CommandType.Text, sql, sp);
        }
        public DataTable SelBy(string load, string input = "", string pid = "0") 
        {
             string sql = "Select * From " + strTableName+" WHERE 1=1 ";
             if (!string.IsNullOrEmpty(load) && !string.IsNullOrEmpty(input))
             {
                 sql += " And (PubLoadstr=@load or PubInputLoadStr=@input)";
             }
             else if (!string.IsNullOrEmpty(load))
             {
                 sql += " And PubLoadstr=@load";
             }
             else 
             {
                 sql += " And PubInputLoadStr=@input";
             }
             if (DataConvert.CLng(pid) > 0)
             {
                 sql += " And Pubid<>@pid";
             }
            SqlParameter[] sp = new SqlParameter[] {new SqlParameter("load",load),new SqlParameter("input",input),new SqlParameter("pid",pid) };
            return SqlHelper.ExecuteTable(CommandType.Text, sql,sp);
        }
        ///// <summary>
        /////按条件查找记录
        ///// </summary>        
        //public DataTable SelWhere(string strSQL, string strSelect)
        //{
        //    SqlParameter[] sp = new SqlParameter[] { new SqlParameter("value", strSelect) };
        //    string sqlStr = "SELECT * FROM " + strTableName + " WHERE " + strSQL + " = @value ORDER BY pubid DESC";
        //    return SqlHelper.ExecuteTable(CommandType.Text, sqlStr,sp);
        //}
        public DataTable SelALLForm()
        {
            string sql = "Select * From " + strTableName + " Where PubType=8";
            return SqlHelper.ExecuteTable(CommandType.Text, sql);
        }
        /// <summary>
        /// 返回所有未存档
        /// </summary>
        /// <returns></returns>
        public DataTable Select_NotDel()
        {
            string sqlStr = "select * from " + strTableName + " where PubIsDel=0 and (PubSiteID is null or PubSiteID='')";
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, null);
        }
        /// <summary>
        /// 更新模型
        /// </summary>
        /// <param name="sqlpara"></param>
        /// <param name="tablename"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public bool UpdateModel(SqlParameter[] sqlpara, string TableName, string where)
        {
            SafeSC.CheckDataEx(TableName);
            if (sqlpara.Length > 0 && TableName != "" && where != "")
            {
                string filename = "";
                string filevalue = "";
                string sqlstr = "";
                foreach (SqlParameter para in sqlpara)
                {
                    if (filename == "") { filename = para.ParameterName; }
                    else { filename = filename + "," + para.ParameterName; }

                    if (filevalue == "")
                    {
                        filevalue = para.ParameterName + "=@" + para.ParameterName;
                    }
                    else
                    {
                        filevalue = filevalue + "," + para.ParameterName + "=" + "@" + para.ParameterName;
                    }
                }
                sqlstr = "update " + TableName + " set " + filevalue + " where " + where;
                return SqlHelper.ExecuteSql(sqlstr, sqlpara);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 添加模型信息
        /// </summary>
        /// <param name="sqlpara"></param>
        /// <param name="tablename"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public bool InsertModel(SqlParameter[] sqlpara, string TableName)
        {
            SafeSC.CheckDataEx(TableName);
            if (sqlpara.Length > 0 && TableName != "")
            {
                string filename = "";
                string filevalue = "";
                string sqlstr = "";

                foreach (SqlParameter para in sqlpara)
                {
                    if (para.ParameterName != "PubID")
                    {
                        if (filename == "") { filename = para.ParameterName; }
                        else { filename = filename + "," + para.ParameterName; }

                        if (filevalue == "")
                        {
                            filevalue = "@" + para.ParameterName;
                        }
                        else
                        {
                            filevalue = filevalue + ",@" + para.ParameterName;
                        }
                    }
                }
                sqlstr = "Insert into " + TableName + " (" + filename + ") values (" + filevalue + ")";
                return SqlHelper.ExecuteSql(sqlstr, sqlpara);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除模型信息
        /// </summary>
        public bool DeleteModel(string TableName, string where)
        {
            SafeSC.CheckDataEx(TableName);
            string sqlstr = "";
            sqlstr = "delete from " + TableName + " where " + where;
            return SqlHelper.ExecuteSql(sqlstr, null);
        }
        /// <summary>
        /// 根据模型id查询
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        public M_Pub GetPubModel(int Model)
        {
            string sql = "select * from ZL_Pub where PubModelID=" + Model;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sql, null))
            {
                if (reader.Read())
                {
                    return initMod.GetModelFromReader(reader);
                }
                else
                {
                    return new M_Pub();
                }
            }
        }

        public DataTable GetPubModelPublic()
        {
            string sql = "select * from ZL_Pub where [Public]=1";
            return SqlHelper.ExecuteTable(CommandType.Text, sql, null);
        }
        /// <summary>
        /// 根据互动id查询定义互动模型
        /// </summary>
        /// <param name="Pubupid"></param>
        /// <returns></returns>
        public DataTable GetModelPubuPIdAll(int id, string name, string TableName)
        {
            SafeSC.CheckDataEx(TableName);
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("name", name) };
            string sql = "select * from " + TableName + " where id  in (select  Parentid from  " + TableName + " ) and PubUserName=@name and Pubupid=" + id;
            return SqlHelper.ExecuteTable(CommandType.Text, sql, sp);
        }
        /// <summary>
        /// 根据互动id查询定义互动模型
        /// </summary>
        /// <param name="Pubupid"></param>
        /// <returns></returns>
        public DataTable GetModelPubuPIdAll(int id, int pcid, string TableName)
        {
            SafeSC.CheckDataEx(TableName);
            string sql = "select * from " + TableName + " where Parentid=" + id + " and PubContentid=" + pcid;
            return SqlHelper.ExecuteTable(CommandType.Text, sql, null);
        }
        /// <summary>
        /// 查询发布者的信息
        /// </summary>
        /// <param name="TableName"></param>
        /// <returns></returns>
        public DataTable GePubModelByPubID(string TableName)
        {
            SafeSC.CheckDataEx(TableName);
            string sql = "select * from " + TableName + " where ID=1";
            return SqlHelper.ExecuteTable(CommandType.Text, sql, null);
        }
        /// <summary>
        /// 根据表名查询所有数据
        /// </summary>
        /// <param name="TableName"></param>
        /// <returns></returns>
        public DataTable GetPubModelPubAll(string TableName, int id)
        {
            SafeSC.CheckDataEx(TableName);
            string sql = "select * from " + TableName + " where id  not in(select  top 1 ID from " + TableName + ") and  Parentid=" + id;
            return SqlHelper.ExecuteTable(CommandType.Text, sql, null);
        }
        /// <summary>
        /// 自定义互动表更新数据为1
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool UpdatePubModelById(string TableName, int id)
        {
            SafeSC.CheckDataEx(TableName);
            string sql = "update " + TableName + " set Optimal=1 where ID=" + id;
            return SqlHelper.ExecuteSql(sql);
        }
        /// <summary>
        /// 自定义互动表更新数据为0
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool UpdatePubModelByOptimal(string TableName, int id)
        {
            SafeSC.CheckDataEx(TableName);
            string sql = "update " + TableName + " set Optimal=0 where ID not in(select ID from " + TableName + " where ID=" + id + " and Optimal=1)";
            return SqlHelper.ExecuteSql(sql);
        }

        /// <summary>
        /// 自定义互动表更新数据为-1
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Getnodb(string TableName, int id)
        {
            string sql = "update " + TableName + " set Optimal=-1 where ID=" + id;
            return SqlHelper.ExecuteSql(sql);
        }

        /// <summary>
        /// 添加自定义表数据
        /// </summary>
        /// <param name="Pubupid"></param>
        /// <param name="PubUserName"></param>
        /// <param name="PubUserID"></param>
        /// <param name="PubContentid"></param>
        /// <param name="Parentid"></param>
        /// <param name="PubIP"></param>
        /// <param name="Pubnum"></param>
        /// <param name="Pubstart"></param>
        /// <param name="PubTitle"></param>
        /// <param name="PubContent"></param>
        /// <param name="PubAddTime"></param>
        /// <param name="Optimal"></param>
        /// <returns></returns>
        public bool AddPubModelCustom(DataTable dt, string TableNam, int Pubupid, string PubUserName, int PubUserID, int PubContentid, int Parentid, string PubIP, int Pubnum, int Pubstart, string PubTitle, string PubContent, DateTime PubAddTime, int Optimal, string PubInputer)
        {
            string strname = "";
            string strvalue = "";
            SqlParameter[] sp;
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    strname = strname + "," + dt.Rows[i]["FieldName"];
                    strvalue = strvalue + ",@" + dt.Rows[i]["FieldName"];
                }
                sp = new SqlParameter[dt.Rows.Count + 13];
            }
            else
            {
                sp = new SqlParameter[13];
            }
            string sql = "insert into " + TableNam + " (Pubupid,PubUserName,PubUserID,PubContentid,Parentid,PubIP,Pubnum,Pubstart,PubTitle,PubContent,PubAddTime,Optimal,PubInputer" + strname + ")values"
                                                     +"(@Pubupid,@PubUserName,@PubUserID,@PubContentid,@Parentid,@PubIP,@Pubnum,@Pubstart,@PubTitle,@PubContent,@PubAddTime,@Optimal,@PubInputer" + strvalue + ")";
            sp[0] = new SqlParameter("@Pubupid", SqlDbType.Int);
            sp[1] = new SqlParameter("@PubUserName", SqlDbType.NVarChar);
            sp[2] = new SqlParameter("@PubUserID", SqlDbType.Int);
            sp[3] = new SqlParameter("@PubContentid", SqlDbType.Int);
            sp[4] = new SqlParameter("@Parentid", SqlDbType.Int);
            sp[5] = new SqlParameter("@PubIP", SqlDbType.NVarChar);
            sp[6] = new SqlParameter("@Pubnum", SqlDbType.Int);
            sp[7] = new SqlParameter("@Pubstart", SqlDbType.Int);
            sp[8] = new SqlParameter("@PubTitle", SqlDbType.NVarChar);
            sp[9] = new SqlParameter("@PubContent", SqlDbType.NVarChar);
            sp[10] = new SqlParameter("@PubAddTime", SqlDbType.DateTime);
            sp[11] = new SqlParameter("@Optimal", SqlDbType.Int);
            sp[12] = new SqlParameter("@PubInputer", SqlDbType.NVarChar);
            sp[0].Value = Pubupid;
            sp[1].Value = PubUserName;
            sp[2].Value = PubUserID;
            sp[3].Value = PubContentid;
            sp[4].Value = Parentid;
            sp[5].Value = PubIP;
            sp[6].Value = Pubnum;
            sp[7].Value = Pubstart;
            sp[8].Value = PubTitle;
            sp[9].Value = PubContent;
            sp[10].Value = PubAddTime;
            sp[11].Value = Optimal;
            sp[12].Value = PubInputer;
            ContentPara(sp, dt);
            return SqlHelper.ExecuteSql(sql, sp);
        }
        /// <summary>
        /// 通过存储模型表字段值的table创建插入语句和更新语句的参数数组
        /// </summary>
        /// <param name="DTContent">存储模型表字段值的table</param>
        /// <returns>Sql参数数组</returns>
        public void ContentPara(SqlParameter[] sp, DataTable DTContent)
        {
            int i = 13;
            foreach (DataRow dr in DTContent.Rows)
            {
                sp[i] = GetPara(dr);
                i++;
            }
        }

        /// <summary>
        /// 从存储模型表字段值的table的一行数据创建一个参数元素
        /// </summary>
        /// <param name="dr">存储模型表字段值的table的一行数据</param>
        /// <returns>Sql参数</returns>
        private SqlParameter GetPara(DataRow dr)
        {
            string FieldType = dr["FieldType"].ToString();
            SqlParameter result = new SqlParameter();
            switch (FieldType)
            {
                case "TextType":
                    result = new SqlParameter("@" + dr["FieldName"].ToString(), SqlDbType.NVarChar, 255);
                    result.Value = dr["FieldValue"].ToString();
                    break;
                case "OptionType":
                    result = new SqlParameter("@" + dr["FieldName"].ToString(), SqlDbType.NVarChar, 255);
                    result.Value = dr["FieldValue"].ToString();
                    break;
                case "GradeOptionType":
                    result = new SqlParameter("@" + dr["FieldName"].ToString(), SqlDbType.NVarChar, 255);
                    result.Value = dr["FieldValue"].ToString();
                    break;
                case "ListBoxType":
                    result = new SqlParameter("@" + dr["FieldName"].ToString(), SqlDbType.NText);
                    result.Value = dr["FieldValue"].ToString();
                    break;
                case "DateType":
                    result = new SqlParameter("@" + dr["FieldName"].ToString(), SqlDbType.DateTime);
                    result.Value = DataConverter.CDate(dr["FieldValue"].ToString());
                    break;
                case "MultipleHtmlType":
                    result = new SqlParameter("@" + dr["FieldName"].ToString(), SqlDbType.NText);
                    result.Value = dr["FieldValue"].ToString();
                    break;
                case "MultipleTextType":
                    result = new SqlParameter("@" + dr["FieldName"].ToString(), SqlDbType.NText);
                    result.Value = dr["FieldValue"].ToString();
                    break;
                case "FileType":
                    result = new SqlParameter("@" + dr["FieldName"].ToString(), SqlDbType.NText);
                    result.Value = dr["FieldValue"].ToString();
                    break;
                case "PicType":
                    result = new SqlParameter("@" + dr["FieldName"].ToString(), SqlDbType.NVarChar, 255);
                    result.Value = dr["FieldValue"].ToString();
                    break;
                case "SmallFileType":
                    result = new SqlParameter("@" + dr["FieldName"].ToString(), SqlDbType.NVarChar, 255);
                    result.Value = dr["FieldValue"].ToString();
                    break;
                case "FileSize":
                    result = new SqlParameter("@" + dr["FieldName"].ToString(), SqlDbType.NVarChar, 255);
                    result.Value = dr["FieldValue"].ToString();
                    break;
                case "ThumbField":
                    result = new SqlParameter("@" + dr["FieldName"].ToString(), SqlDbType.NVarChar, 255);
                    result.Value = dr["FieldValue"].ToString();
                    break;
                case "MultiPicType":
                    result = new SqlParameter("@" + dr["FieldName"].ToString(), SqlDbType.NText);
                    result.Value = dr["FieldValue"].ToString();
                    break;
                case "OperatingType":
                    result = new SqlParameter("@" + dr["FieldName"].ToString(), SqlDbType.NVarChar, 255);
                    result.Value = dr["FieldValue"].ToString();
                    break;
                case "SuperLinkType":
                    result = new SqlParameter("@" + dr["FieldName"].ToString(), SqlDbType.NVarChar, 255);
                    result.Value = dr["FieldValue"].ToString();
                    break;
                case "MoneyType":
                    result = new SqlParameter("@" + dr["FieldName"].ToString(), SqlDbType.Money);
                    result.Value = DataConverter.CFloat(dr["FieldValue"].ToString());
                    break;
                case "BoolType":
                    result = new SqlParameter("@" + dr["FieldName"].ToString(), SqlDbType.Bit, 1);
                    result.Value = dr["FieldValue"].ToString();
                    break;
                case "ColorType":
                    result = new SqlParameter("@" + dr["FieldName"].ToString(), SqlDbType.NVarChar, 255);
                    result.Value = dr["FieldValue"].ToString();
                    break;
                case "int":
                    result = new SqlParameter("@" + dr["FieldName"].ToString(), SqlDbType.Int);
                    result.Value = DataConverter.CLng(dr["FieldValue"].ToString());
                    break;
                case "NumType":
                    result = new SqlParameter("@" + dr["FieldName"].ToString(), SqlDbType.Float);
                    result.Value = DataConverter.CFloat(dr["FieldValue"].ToString());
                    break;
                case "float":
                    result = new SqlParameter("@" + dr["FieldName"].ToString(), SqlDbType.Float);
                    result.Value = DataConverter.CFloat(dr["FieldValue"].ToString());
                    break;
                case "money":
                    result = new SqlParameter("@" + dr["FieldName"].ToString(), SqlDbType.Money);
                    result.Value = DataConverter.CFloat(dr["FieldValue"].ToString());
                    break;
            }
            return result;
        }

        /// <summary>
        /// 根据id查询数据
        /// </summary>
        public DataTable GetPubModeById(int id, string TableName)
        {
            SafeSC.CheckDataEx(TableName);
            string sql = "select * from " + TableName + " where ID=" + id;
            return SqlHelper.ExecuteTable(CommandType.Text, sql, null);
        }
        /// <summary>
        /// 根据id查询回复
        /// </summary>
        public DataTable GetPubModelParentName(int id, string name, string TableName)
        {
            SafeSC.CheckDataEx(TableName);
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("name", name) };
            string sql = "select * from " + TableName + " where Parentid=" + id + " and PubUserName=@name";
            return SqlHelper.ExecuteTable(CommandType.Text, sql, sp);
        }
        public M_Pub GetPubModelById(int Model)
        {
            string sql = "select * from ZL_Pub where Pubid=" + Model;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sql, null))
            {
                if (reader.Read())
                {
                    return initMod.GetModelFromReader(reader);
                }
                else
                {
                    return new M_Pub();
                }
            }
        }

        public bool UpdatePubModelOptimal(string TableName, int id)
        {
            string sql = "update " + TableName + " set Optimal=0 where ID=" + id;
            return SqlHelper.ExecuteSql(sql);
        }

        public DataTable GetPubByUserID(int uid, string TableName, string select)
        {
            SafeSC.CheckDataEx(TableName);
            string sql = "select " + select + " from " + TableName + " where  PubUserID=@UserID";
            SqlParameter[] sp = new SqlParameter[1];
            sp[0] = new SqlParameter("@UserID", SqlDbType.Int);
            sp[0].Value = uid;
            return SqlHelper.ExecuteTable(CommandType.Text, sql, sp);
        }

        public DataTable GetModelPubuPIdAll(int pcid, string Table)
        {
            SafeSC.CheckDataEx(Table);
            string sql = "select * from " + Table + " where  PubContentid=@PubContentid ";
            SqlParameter[] sp = { new SqlParameter("@PubContentid", SqlDbType.Int) };
            sp[0].Value = pcid;
            return SqlHelper.ExecuteTable(CommandType.Text, sql, sp);
        }
        public DataTable GetBidsTable(string PubUserName)
        {
            string sqlStr = "select PubTableName from ZL_Pub where PubType=6";
            DataTable dt = SqlHelper.ExecuteTable(CommandType.Text, sqlStr, null);
            DataTable tt = new DataTable();
            tt = dt.Clone();
            foreach (DataRow row in dt.Rows)
            {
                SqlParameter[] sp = new SqlParameter[] { new SqlParameter("PubUserName", PubUserName) };
                string sql = "SELECT * FROM " + row[0] + " WHERE id IN (SELECT max(id) FROM " + row[0] + " where PubUserName=@PubUserName GROUP BY PubUserName)";
                DataTable t = SqlHelper.ExecuteTable(CommandType.Text, sql, sp);
                if (tt.Rows.Count == 0)
                {
                    tt = t;
                }
                else
                {
                    foreach (DataRow r in t.Rows)
                    {
                        tt.ImportRow(r);
                    }
                }
            }
            return tt;
        }
        public DataTable GetMyBidsTable(string PubUserName)
        {
            string sqlStr = "select PubTableName from ZL_Pub where PubType=6";
            DataTable dt = SqlHelper.ExecuteTable(CommandType.Text, sqlStr, null);
            DataTable tt = new DataTable();
            tt = dt.Clone();
            foreach (DataRow row in dt.Rows)
            {
                SqlParameter[] sp = new SqlParameter[] { new SqlParameter("PubUserName", PubUserName) };
                string sql = "select * from " + row[0] + " where Parentid=0 and PubUserName=@PubUserName";
                DataTable t = SqlHelper.ExecuteTable(CommandType.Text, sql, sp);
                if (tt.Rows.Count == 0)
                {
                    tt = t;
                }
                else
                {
                    foreach (DataRow r in t.Rows)
                    {
                        tt.ImportRow(r);
                    }
                }
            }
            return tt;
        }
        public DataTable GetSuccessful(string PubUserName)
        {
            string sqlStr = "select PubTableName from ZL_Pub where PubType=6";
            DataTable dt = SqlHelper.ExecuteTable(CommandType.Text, sqlStr, null);
            DataTable tt = new DataTable();
            tt = dt.Clone();
            foreach (DataRow row in dt.Rows)
            {
                SqlParameter[] sp = new SqlParameter[] { new SqlParameter("PubUserName", PubUserName) };
                string sql = "SELECT * FROM " + row[0] + " WHERE id IN (SELECT max(id) FROM " + row[0] + " where Optimal=1 and PubUserName=@PubUserName GROUP BY PubUserName)";
                DataTable t = SqlHelper.ExecuteTable(CommandType.Text, sql, sp);
                if (tt.Rows.Count == 0)
                {
                    tt = t;
                }
                else
                {
                    foreach (DataRow r in t.Rows)
                    {
                        tt.ImportRow(r);
                    }
                }
            }
            return tt;
        }
        public DataTable GetCompliance(string PubUserName)
        {
            string sqlStr = "select PubTableName from ZL_Pub where PubType=6";
            DataTable dt = SqlHelper.ExecuteTable(CommandType.Text, sqlStr, null);
            DataTable tt = new DataTable();
            tt = dt.Clone();
            foreach (DataRow row in dt.Rows)
            {
                SqlParameter[] sp = new SqlParameter[] { new SqlParameter("PubUserName", PubUserName) };
                string sql = "SELECT * FROM " + row[0] + " WHERE id IN (SELECT max(id) FROM " + row[0] + " where Optimal=2 and PubUserName=@PubUserName GROUP BY PubUserName)";
                DataTable t = SqlHelper.ExecuteTable(CommandType.Text, sql, sp);
                if (tt.Rows.Count == 0)
                {
                    tt = t;
                }
                else
                {
                    foreach (DataRow r in t.Rows)
                    {
                        tt.ImportRow(r);
                    }
                }
            }
            return tt;
        }
        /// <summary>
        /// 查询互动参与总人数
        /// </summary>
        /// <param name="pubid">互动ID</param>
        /// <returns></returns>
        public int GetPubCount(int pubid)
        {
            string sqlStrs = "select PubTableName from ZL_Pub where Pubid=" + pubid;
            string tablename = SqlHelper.ExecuteScalar(CommandType.Text, sqlStrs, null).ToString();
            string sqlStr = "select Count(distinct PubUserName) from " + tablename + " where pubupid=@pubupid";
            SqlParameter[] para = new SqlParameter[]{
                new SqlParameter("@pubupid",pubid)
            };
            return SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.Text, sqlStr, para));
        }
        /// <summary>
        /// 父互动ID,互动内容ID,用户名,表名查询
        /// </summary>
        /// <param name="id">父互动ID</param>
        /// <param name="pcid">互动内容ID</param>
        /// <param name="username">用户名</param>
        /// <param name="table">表名</param>
        /// <returns></returns>
        public DataTable GetModelPubuUserName(int id, int pcid, string username, string table)
        {
            SafeSC.CheckDataEx(table);
            string sql = "select * from " + table + " where Parentid=@Parentid and PubContentid=@PubContentid and PubUserName=@PubUserName";
            SqlParameter[] para = new SqlParameter[]{
                new SqlParameter("@Parentid",id),
                new SqlParameter("@PubContentid",pcid),
                new SqlParameter("@PubUserName",username)
            };
            return SqlHelper.ExecuteTable(CommandType.Text, sql, para);
        }
        public DataTable GetComments(string TBName,string inputer)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("inputer", inputer) };
            string strSql = "select * from " + TBName + " where PubInputer=@inputer and Parentid=0";
            return SqlHelper.ExecuteTable(CommandType.Text, strSql, sp);
        }
        public DataTable GetCommentT(string TBName,int id)
        {
            SafeSC.CheckDataEx(TBName);
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("id", id) };
            string strSql = "select * from " + TBName + " where id=@id";
            return SqlHelper.ExecuteTable(CommandType.Text, strSql, sp);
        }
        public bool PassComments(string TBName, int ID)
        {
            SafeSC.CheckDataEx(TBName);
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("ID", ID) };
            string sql = "update " + TBName + " set Pubstart=1 where ID=@ID";
            return SqlHelper.ExecuteSql(sql,sp);
        }
        public bool NPassComments(string TBName, int ID)
        {
            SafeSC.CheckDataEx(TBName);
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("ID", ID) };
            string sql = "update " + TBName + " set Pubstart=0 where ID=@ID";
            return SqlHelper.ExecuteSql(sql,sp);
        }
        public bool DelComments(string TBName, int ID)
        {
            SafeSC.CheckDataEx(TBName);
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("ID", ID) };
            string sql = "delete from " + TBName + " where ID=@ID";
            return SqlHelper.ExecuteSql(sql,sp);
        }
        public DataTable SelByType(int type = 1)
        {
            string sql = "SELECT * FROM " + strTableName + " WHERE ";
            switch (type)
            {
                case 1:
                    sql += " PubIsDel=0 And PubType!=8 And (PubSiteID is null or PubSiteID='')";
                    break;
                case 2:
                    sql += " PubIsDel=1";
                    break;
                case 3:
                    sql += "PubNodeID<>'0' and PubNodeID is not null and PubNodeID<>''";
                    break;
                case 4:
                    sql += " PubOpenComment=1";
                    break;
            }
            sql += " ORDER BY PubCreateTime DESC";
            return SqlHelper.ExecuteTable(CommandType.Text, sql);
        }
        public bool UpdateComments(string TBName, int ID, string pubtitle, string pubcontent)
        {
            SafeSC.CheckDataEx(TBName);
            SqlParameter[] sp = new SqlParameter[] {
                new SqlParameter("ID", ID),
                new SqlParameter("pubtitle",pubtitle),
                new SqlParameter("pubcontent",pubcontent)
            };
            string sql = "update " + TBName + " set PubTitle=@pubtitle, PubContent=@pubcontent where ID=@ID";
            return SqlHelper.ExecuteSql(sql, sp);
        }
        //计算当前用户的回复数，一般用于投票互动,parentid=0一样计数
        public int SelMsgCount(M_Pub model, int PubContentid, int Pubid, string ip)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("IP", ip) };
            string sql = "Select Count(ID) From " + model.PubTableName + " WHERE PubContentid=" + PubContentid + " AND Pubupid=" + Pubid + " AND PubIP=@IP";
            if (model.Interval > 0)
            {
                sql += " AND DateDiff(hh,PubAddTime,getDate())<=" + model.Interval;
            }
            return SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.Text,sql,sp));
        }
        //互动表单
        public DataTable SelByTbName(string tbname, int parentid=0)
        {
            SafeSC.CheckDataEx(tbname);
            string sql = "Select * From " + tbname + " Where ParentID=" + parentid;
            return SqlHelper.ExecuteTable(CommandType.Text,sql);
        }
        //--------------------------------------------------------------
        public PageSetting SelInfoPage(int cpage, int psize, string tbname, int parentid, int uid)
        {
            string where = "1=1 ";
            if (parentid != -100) { where += " AND ParentID=" + parentid; }
            if (uid > 0) { where += " AND PubUserID=" + uid; }
            PageSetting setting = PageSetting.Single(cpage, psize, tbname, "ID", where, "", null);
            DBCenter.SelPage(setting);
            return setting;
        }
        public PageSetting SelPage(int cpage, int psize)
        {
            string where = "1=1 ";
            List<SqlParameter> sp = new List<SqlParameter>();
            PageSetting setting =PageSetting.Single(cpage,psize,strTableName,PK,where,"",sp);
            DBCenter.SelPage(setting);
            return setting;
        }
        //public PageSetting SelPage(int cpage, int psize, int id = -100, int pcid = -100, string username = "", string table = "")
        //{
        //    string where = "1=1";
        //    List<SqlParameter> sp = new List<SqlParameter>();
        //    if (id != -100) { where += " AND Parentid=" + id; }
        //    if (pcid != -100) { where += " AND PubContentid=" + pcid; }
        //    if (!string.IsNullOrEmpty(username)) { where += " AND PubUserName=@UserName"; sp.Add(new SqlParameter("UserName", username)); }
        //    if (string.IsNullOrEmpty(table)) { table = strTableName; }
        //    else { SafeSC.CheckDataEx(table); }
        //    PageSetting setting = PageSetting.Single(cpage, psize, table, "ID", where, "", sp);
        //    DBCenter.SelPage(setting);
        //    return setting;
        //}
    }
}