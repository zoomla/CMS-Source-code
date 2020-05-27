namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using System.Configuration;
    using System.Web;
    using ZoomLa.Model;
    using System.Collections.Generic;
    using ZoomLa.Components;
    using ZoomLa.Common;
    using System.IO;
    using ZoomLa.SQLDAL;
    using System.Data.SqlClient;
    using ZoomLa.BLL.Helper;
    using System.Data.Common;
    public class B_Model
    {
        //public string dirfile = SiteConfig.SiteMapath() + SiteConfig.SiteOption.TemplateDir.Replace("/", @"\").Substring(1, SiteConfig.SiteOption.TemplateDir.Length - 1) + @"\配置库\模型\Directory.model";
        //public string ContentTemplate = SiteConfig.SiteMapath() + SiteConfig.SiteOption.TemplateDir.Replace("/", @"\").Substring(1, SiteConfig.SiteOption.TemplateDir.Length - 1) + @"\配置库\模型\ContentTemplate.Field";
        private string TbName, PK;
        private M_ModelInfo initMod = new M_ModelInfo();
        public B_Model() 
        {
            TbName = initMod.TbName;
            PK = initMod.PK;
        }
        DataTable ListModel()
        {
            string strSql = "Select * from ZL_Model Where ModelType=1 or ModelType=5 or ModelType=2 order by ModelID ";
            DataTable dt = SqlHelper.ExecuteTable(CommandType.Text, strSql, null);
            return dt;
        }
        public DataTable GetListSQL()
        {
            return ListModel();
        }
        /// <summary>
        /// 内容模型,用于批量设置处与节点处
        /// </summary>
        /// <returns></returns>
        public DataTable GetList()
        {
            return SelByType("1,2,5,9");
        }

        /// <summary>
        /// 功能模型
        /// </summary>
        /// <returns></returns>
        public DataTable GetListFuc()
        {
            return SelByType("8");
        }
        /// <summary>
        /// 会员模型
        /// </summary>
        /// <returns></returns>
        public DataTable GetListUser()
        {
            return SelByType("3");
        }
        /// <summary>
        /// 店铺商品模型
        /// </summary>
        /// <returns></returns>
        public DataTable GetListStore()
        {
            return SelByType("6");
        }
        /// <summary>
        /// 读取商城模型列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetListShop()
        {
            return SelByType("2");
        }
        /// <summary>
        /// 互动模型
        /// </summary>
        /// <returns></returns>
        public DataTable GetListPub()
        {
            return SelByType("7");
        }
        /// <summary>
        /// 读取用户商品模型列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetListUserShop()
        {
            return SelByType("5");
        }

        /// <summary>
        /// 获取黄页模型
        /// </summary>
        /// <returns></returns>
        public DataTable GetListPage()
        {
            string mtype = "'黄页模型'";
            DataTable modeltable = GetModel(mtype, "and TableName like 'ZL_Reg_%'");
            return modeltable;
        }

        /// <summary>
        /// 获取项目模型列表
        /// </summary>
        /// <returns></returns>
        public DataTable ListProjectModel()
        {
            return SelByType("8");
        }
        /// <summary>
        /// 商城模型
        /// </summary>
        /// <returns></returns>
        public DataTable GetShopMode()
        {
            return SelByType("2");
        }

        /// <summary>
        /// 为批量导入数据新建模板时动态添内容和创建表
        /// 新建模板添加内容到zl_model/字段加入zl_modelField/根据字段创建内容表
        /// </summary>
        /// <param name="modelName"></param>
        /// <param name="tableName"></param>
        /// <param name="fileNames"></param>
        /// <param name="fieldAliass"></param>
        /// <returns>返回ModelID</returns>
        public object SetModelFieldSingleNewTable(int NodeID, string modelName, string tableName, string fileNames, string fieldAliass)
        {
            return SetModelFieldSingleNewTable(NodeID, modelName, tableName, fileNames, fieldAliass);
        }

        public object GetModelInfoTableNames()
        {
            throw new NotImplementedException();
        }

        public DataTable ListModel(string p)
        {
            throw new NotImplementedException();
        }
        public DataTable CheckName(string name)
        {

            //string strSql = "select " + select + " from ZL_Model where 1=1" + where;
            return GetWhere(" * ", " and ModelName='" + name + "'", "");
        }
        /****************************************************标记结束**********************************************************/
        public DataTable GetWhere(string select, string where, string order)
        {
            string strSql = "select " + select + " from ZL_Model where 1=1" + where;
            if (!string.IsNullOrEmpty(order))
            {
                strSql = strSql + "order by " + order;
            }
            return SqlHelper.ExecuteTable(CommandType.Text, strSql, null);
        }

        public DataSet ModelSet(M_ModelInfo modeinfo)
        {
            DataSet newdataset = new DataSet("NewDataSet");
            DataTable tables = new DataTable("Table");
            tables.Columns.Add("ModelID", typeof(int));
            tables.Columns.Add("ModelName", typeof(string));
            tables.Columns.Add("Description", typeof(string));
            tables.Columns.Add("TableName", typeof(string));
            tables.Columns.Add("ItemName", typeof(string));
            tables.Columns.Add("ItemUnit", typeof(string));
            tables.Columns.Add("ItemIcon", typeof(string));
            tables.Columns.Add("ModelType", typeof(string));
            tables.Columns.Add("ContentModule", typeof(string));
            tables.Columns.Add("MultiFlag", typeof(bool));
            tables.Columns.Add("IsNull", typeof(bool));
            tables.Columns.Add("NodeID", typeof(int));
            tables.Columns.Add("SysModel", typeof(int));
            tables.Columns.Add("FromModel", typeof(int));
            tables.Columns.Add("Thumbnail", typeof(string));
            tables.Columns.Add("Islotsize", typeof(bool));
            DataRow modelrow = tables.NewRow();
            modelrow["ModelID"] = modeinfo.ModelID;//模型ID
            modelrow["ModelName"] = modeinfo.ModelName;//模型名称
            modelrow["Description"] = modeinfo.Description;//模型描述
            modelrow["TableName"] = modeinfo.TableName;//模型内容存储表名
            modelrow["ItemName"] = modeinfo.ItemName;//项目名称：如文章、新闻
            modelrow["ItemUnit"] = modeinfo.ItemUnit;//项目单位：如篇、条
            modelrow["ItemIcon"] = modeinfo.ItemIcon;//项目图标
            modelrow["ModelType"] = modeinfo.ModelType;/// 模型类别
            modelrow["ContentModule"] = modeinfo.ContentModule;/// 内容模板
            modelrow["MultiFlag"] = modeinfo.MultiFlag;/// 是否多条记录，只对用户模型有效true时允许一个用户输入多条此模型信息
            modelrow["IsNull"] = modeinfo.IsNull;
            modelrow["NodeID"] = modeinfo.NodeID;
            modelrow["SysModel"] = modeinfo.SysModel;/// 识别系统模型字段，1：系统生成模型，2：用户自定义模型
            modelrow["FromModel"] = modeinfo.FromModel;/// 复制来源模型ID
            modelrow["Thumbnail"] = modeinfo.Thumbnail;
            modelrow["Islotsize"] = modeinfo.Islotsize;
            tables.Rows.Add(modelrow);
            newdataset.Tables.Add(tables);

            DataTable ModelFieldTable = new DataTable("ModelFieldTable");
            ModelFieldTable.Columns.Add("FieldID", typeof(int));
            ModelFieldTable.Columns.Add("ModelId", typeof(int));
            ModelFieldTable.Columns.Add("FieldName", typeof(string));
            ModelFieldTable.Columns.Add("FieldAlias", typeof(string));
            ModelFieldTable.Columns.Add("FieldTips", typeof(string));
            ModelFieldTable.Columns.Add("Description", typeof(string));
            ModelFieldTable.Columns.Add("IsNotNull", typeof(bool));
            ModelFieldTable.Columns.Add("IsSearchForm", typeof(bool));
            ModelFieldTable.Columns.Add("FieldType", typeof(string));
            ModelFieldTable.Columns.Add("OrderId", typeof(int));
            ModelFieldTable.Columns.Add("ShowList", typeof(bool));
            ModelFieldTable.Columns.Add("ShowWidth", typeof(int));
            ModelFieldTable.Columns.Add("IsNull", typeof(bool));
            ModelFieldTable.Columns.Add("IsShow", typeof(bool));
            ModelFieldTable.Columns.Add("IsView", typeof(bool));
            ModelFieldTable.Columns.Add("IsDownField", typeof(int));
            ModelFieldTable.Columns.Add("DownServerID", typeof(int));
            ModelFieldTable.Columns.Add("RestoreField", typeof(int));
            ModelFieldTable.Columns.Add("IsCopy", typeof(int));
            ModelFieldTable.Columns.Add("Sys_type", typeof(bool));
            ModelFieldTable.Columns.Add("Unfurl", typeof(int));
            ModelFieldTable.Columns.Add("Islotsize", typeof(bool));
            ModelFieldTable.Columns.Add("RegShow", typeof(int));

            List<M_ModelField> modelist = modeinfo.ModelField;
            for (int i = 0; i < modelist.Count; i++)
            {
                DataRow rows = ModelFieldTable.NewRow();
                rows["FieldID"] = modelist[i].FieldID;
                rows["ModelId"] = modelist[i].ModelID;
                rows["FieldName"] = modelist[i].FieldName;
                rows["FieldAlias"] = modelist[i].FieldAlias;
                rows["FieldTips"] = modelist[i].FieldTips;
                rows["Description"] = modelist[i].Description;
                rows["IsNotNull"] = modelist[i].IsNotNull;
                rows["IsSearchForm"] = modelist[i].IsSearchForm;
                rows["FieldType"] = modelist[i].FieldType;
                rows["Content"] = modelist[i].Content;
                rows["OrderId"] = modelist[i].OrderID;
                rows["ShowList"] = modelist[i].ShowList;
                rows["ShowWidth"] = modelist[i].ShowWidth;
                rows["IsNull"] = modelist[i].IsNull;
                rows["IsShow"] = modelist[i].IsShow;
                rows["IsView"] = modelist[i].IsView;
                rows["IsDownField"] = modelist[i].IsDownField;
                rows["DownServerID"] = modelist[i].DownServerID;
                rows["RestoreField"] = modelist[i].RestoreField;
                rows["IsCopy"] = modelist[i].IsCopy;
                rows["Sys_type"] = modelist[i].Sys_type;
                rows["Unfurl"] = modelist[i].Unfurl;
                rows["Islotsize"] = modelist[i].Islotsize;
                rows["RegShow"] = modelist[i].RegShow;
                ModelFieldTable.Rows.Add(rows);
            }
            newdataset.Tables.Add(ModelFieldTable);
            return newdataset;
        }

        public void ResponseWrite(string content, bool toend)
        {
            System.Web.HttpContext.Current.Response.Write(content);
            if (toend)
            {
                System.Web.HttpContext.Current.Response.End();
            }
        }
        public DataTable ModelFieldSet(int ModelID)
        {
            return new B_ModelField().GetModelFieldListall(ModelID);
        }
        public DataSet ModelFieldSetXML(int ModelID)
        {
            DataSet modeinfo = new B_ModelField().GetModelFieldAllListXML(ModelID);
            return modeinfo;
        }
        /// <summary>
        /// 读取内容模型列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetListXML()
        {
            return ListModel();
        }
        /// <summary>
        /// 读取会员模型列表
        /// </summary>
        /// <returns></returns>
        /// 
        DataTable ListUserModel()
        {
            string strSql = "Select * from ZL_Model Where TableName like 'ZL_U_%' and ModelType=3";
            DataTable dt = SqlHelper.ExecuteTable(CommandType.Text, strSql, null);
            return dt;
        }
        /// <summary>
        /// 读取用户商城模型列表
        /// </summary>
        /// <returns></returns>
        /// 
        DataTable ListStoreModel()
        {
            string strSql = "Select * from ZL_Model Where ModelType=6";
            DataTable dt = SqlHelper.ExecuteTable(CommandType.Text, strSql, null);
            return dt;
        }
        public DataTable GetListStoreXML()
        {
            //string strSql = "Select * from ZL_Model Where ModelType=6";
            return ListStoreModel();
        }
        /// <summary>
        /// 读取商城模型列表
        /// </summary>
        /// <returns></returns>
        /// 
        DataTable ListShopModel()
        {
            string strSql = "Select * from ZL_Model Where ModelType=2 ";
            DataTable dt = SqlHelper.ExecuteTable(CommandType.Text, strSql, null);
            return dt;
        }
        public DataTable GetListShopXML()
        {
            //string strSql = "Select * from ZL_Model Where ModelType=2 ";
            return ListShopModel();
        }
        /// <summary>
        /// 获取互动模型
        /// </summary>
        /// <returns></returns>
        /// 
        DataTable ListPubModel()
        {
            string strSql = "Select * from ZL_Model Where ModelType=7";
            DataTable dt = SqlHelper.ExecuteTable(CommandType.Text, strSql, null);
            return dt;
        }
        public DataTable GetListPubXML()
        {
            //string strSql = "Select * from ZL_Model Where ModelType=7";
            return ListPubModel();
        }
        /// <summary>
        /// 读取用户商城模型列表
        /// </summary>
        /// <returns></returns>
        /// 
        DataTable ListUserShopModel()
        {
            string strSql = "Select * from ZL_Model Where ModelType=5";
            DataTable dt = SqlHelper.ExecuteTable(CommandType.Text, strSql, null);
            return dt;
        }
        public DataTable GetListUserShopXML()
        {
            //string strSql = "Select * from ZL_Model Where ModelType=5";
            return ListUserShopModel();
        }
        /// <summary>
        /// 获取黄页模型
        /// </summary>
        /// <returns></returns>
        /// 
        DataTable ListPageModel()
        {
            string strSql = "Select * from ZL_Model Where TableName like 'ZL_Reg_%' and ModelType=3";
            DataTable dt = SqlHelper.ExecuteTable(CommandType.Text, strSql, null);
            return dt;
        }
        public DataTable GetListPageXML()
        {
            //string strSql = "Select * from ZL_Model Where TableName like 'ZL_Reg_%' and ModelType=3";
            return ListPageModel();
        }
        /// <summary>
        /// 获取项目模型列表
        /// </summary>
        /// <returns></returns>
        public DataTable ListProjectModelXML()
        {
            //string strSql = "Select * from ZL_Model Where ModelType=8";
            return ListProjectModel();
        }
        M_ModelInfo GetModelInfo(int ModelID)
        {
            string strSql = "Select * from ZL_Model Where ModelID=@ModelID";
            SqlParameter[] cmdParams = new SqlParameter[] { new SqlParameter("@ModelID", SqlDbType.Int, 4) };
            cmdParams[0].Value = ModelID;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, strSql, cmdParams))
            {
                if (reader.Read())
                {
                    return initMod.GetModelFromReader(reader);
                }
                else
                    return new M_ModelInfo(true);
            }
        }
        /// <summary>
        /// 获得所有模型列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllMode()
        {
            return Sql.Sel(TbName);
        }

        /// <summary>
        /// 获取ModelID下的所有node节点
        /// </summary>
        /// <param name="modelID"></param>
        public DataTable getNodesByModel(string modelID)
        {
            SqlParameter[] sp = new SqlParameter[] 
            { 
                new SqlParameter("modelID",modelID),
                new SqlParameter("mid","%,"+modelID+"%"),
                new SqlParameter("mid2","%"+modelID+",%")
            };
            string strsql = "select * from ZL_Node where contentModel = @modelID or contentModel like @mid or contentModel like @mid2 ";
            return SqlHelper.ExecuteTable(CommandType.Text, strsql, sp);
        }
        public DataTable SelAllModel()
        {
            string sql = "SELECT * FROM " + TbName;
            return SqlHelper.ExecuteTable(sql);
        }
        /// <summary>
        /// 删除所有模型,附加表,模型字段
        /// </summary>
        public void DeleteAll()
        {
            DataTable dt = SelAllModel();
            foreach (DataRow dr in dt.Rows)
            {
                string tbname = dr["TableName"].ToString().Replace(" ", "");
                if (DBHelper.Table_IsExist(tbname))
                {
                    SqlHelper.ExecuteSql("DROP TABLE " + tbname);
                }
            }
            SqlHelper.ExecuteSql("TRUNCATE TABLE ZL_ModelField");
            SqlHelper.ExecuteSql("TRUNCATE TABLE " + TbName);
        }
        /// <summary>
        /// 更改模板、创建互动模型
        /// </summary>
        /// <param name="tablename"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        /// 
        bool AddPubModelSetId(M_ModelInfo ModelInfo)
        {

            string strSql1 = "SET IDENTITY_INSERT [ZL_Model] ON;INSERT INTO [ZL_Model] ([ModelID],[ModelName],[Description],[TableName],[ItemName],[ItemUnit],[ItemIcon],[ModelType],[ContentTemplate],[MultiFlag],[NodeID],[SysModel],[FromModel],[Thumbnail],[Islotsize])VALUES(@ModelID,@ModelName,@Description,@TableName,@ItemName,@ItemUnit,@ItemIcon,@ModelType,@ContentTemplate,@MultiFlag,@NodeID,@SysModel,@FromModel,@Thumbnail,@Islotsize);SET IDENTITY_INSERT [ZL_Model] OFF";
            SqlParameter[] cmdParams = ModelInfo.GetParameters();

            //string strSql = "CREATE TABLE dbo." + ModelInfo.TableName + " ([ID] [int] IDENTITY (1, 1) PRIMARY Key NOT NULL)";
            return SqlHelper.ExecuteSql(strSql1, cmdParams);
        }
        public M_ModelInfo GetModelInfoTableNameMo(string TableName)
        {
            string sqlStr = "select * from ZL_Model where TableName=@TableName";
            SqlParameter[] cmdParams = new SqlParameter[] { new SqlParameter("@TableName", SqlDbType.NVarChar) };
            cmdParams[0].Value = TableName;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sqlStr, cmdParams))
            {
                if (reader.Read())
                {
                    return initMod.GetModelFromReader(reader);
                }
                else
                    return new M_ModelInfo(true);
            }
        }
        public int CreateModelInfo(M_Pub pub, M_ModelInfo modelinfo)
        {
            B_Model mll = new B_Model();
            string sqlStr = "", selectstr = "";
            selectstr = "select * from " + pub.PubTableName;

            int Modelid = 0;
            string propert1 = "";
            bool isext = false;

            try
            {
                SqlHelper.ExecuteNonQuery(CommandType.Text, selectstr, null, true);
                isext = false;
            }
            catch
            {
                isext = true;
            }

            if (isext == true)
            {
                switch (pub.PubType)
                {
                    case 0://评论

                        AddPubModelSetId(modelinfo);

                        Modelid = mll.GetModelInfoTableName(pub.PubTableName).ModelID;

                        sqlStr = "CREATE TABLE [dbo].[" + pub.PubTableName + "] (  [ID] [int] IDENTITY (1, 1) NOT NULL,  [Pubupid] [int] NULL , [PubUserName] [nvarchar] (255) NULL , [PubUserID] [int] NULL , [PubContentid] [int] NULL ,[Parentid] [int] NULL DEFAULT (0), [PubIP] [nvarchar] (255) NULL , [Pubnum] [int] NULL DEFAULT (0) , [Pubstart] [int] NULL DEFAULT (0) ,  [PubTitle] [nvarchar] (255) NULL ,[PubContent] [ntext] NULL  , [PubAddTime] [datetime] NULL DEFAULT (getdate()),[Optimal] [int] NULL DEFAULT (0)) ALTER TABLE [" + pub.PubTableName + "] WITH NOCHECK ADD CONSTRAINT [PK_" + pub.PubTableName + "] PRIMARY KEY  NONCLUSTERED ( [ID] )";
                        Execute(sqlStr);
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
                        //B_Model mll = new B_Model();
                        AddPubModelSetId(modelinfo);
                        Modelid = mll.GetModelInfoTableName(pub.PubTableName).ModelID;

                        sqlStr = "CREATE TABLE [dbo].[" + pub.PubTableName + "] (  [ID] [int] IDENTITY (1, 1) NOT NULL ,  [Pubupid] [int] NULL , [PubUserName] [nvarchar] (255) NULL , [PubUserID] [int] NULL ,  [PubContentid] [int] NULL ,[Parentid] [int] NULL DEFAULT (0), [PubIP] [nvarchar] (255) NULL , [Pubnum] [int] NULL DEFAULT (0) , [Pubstart] [int] NULL DEFAULT (0) , [PubTitle] [nvarchar] (255) NULL , [PubContent] [ntext] NULL  , [PubAddTime] [datetime] NULL DEFAULT (getdate()),[Optimal] [int] NULL DEFAULT (0) ) ALTER TABLE [" + pub.PubTableName + "] WITH NOCHECK ADD CONSTRAINT [PK_" + pub.PubTableName + "] PRIMARY KEY  NONCLUSTERED ( [ID] )";
                        Execute(sqlStr);
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

                        //B_Model mll = new B_Model();
                        AddPubModelSetId(modelinfo);
                        Modelid = mll.GetModelInfoTableName(pub.PubTableName).ModelID;

                        sqlStr = "CREATE TABLE [dbo].[" + pub.PubTableName + "] (  [ID] [int] IDENTITY (1, 1) NOT NULL ,  [Pubupid] [int] NULL , [PubUserName] [nvarchar] (255) NULL , [PubUserID] [int] NULL , [PubContentid] [int] NULL , [Parentid] [int] NULL DEFAULT (0),[PubIP] [nvarchar] (255) NULL , [Pubnum] [int] NULL DEFAULT (0) , [Pubstart] [int] NULL DEFAULT (0) , [PubTitle] [nvarchar] (255) NULL , [PubContent] [ntext] NULL  , [PubAddTime] [datetime] NULL DEFAULT (getdate()),[Optimal] [int] NULL DEFAULT (0)) ALTER TABLE [" + pub.PubTableName + "] WITH NOCHECK ADD CONSTRAINT [PK_" + pub.PubTableName + "] PRIMARY KEY  NONCLUSTERED ( [ID] )";
                        Execute(sqlStr);
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

                        //B_Model mll = new B_Model();
                        AddPubModelSetId(modelinfo);
                        Modelid = mll.GetModelInfoTableName(pub.PubTableName).ModelID;

                        sqlStr = "CREATE TABLE [dbo].[" + pub.PubTableName + "] (  [ID] [int] IDENTITY (1, 1) NOT NULL ,  [Pubupid] [int] NULL , [PubUserName] [nvarchar] (255) NULL , [PubUserID] [int] NULL , [PubContentid] [int] NULL , [Parentid] [int] NULL DEFAULT (0),[PubIP] [nvarchar] (255) NULL , [Pubnum] [int] NULL DEFAULT (0) , [Pubstart] [int] NULL DEFAULT (0) , [PubTitle] [nvarchar] (255) NULL ,[PubContent] [ntext] NULL  , [PubAddTime] [datetime] NULL DEFAULT (getdate()),[Optimal] [int] NULL DEFAULT (0)) ALTER TABLE [" + pub.PubTableName + "] WITH NOCHECK ADD CONSTRAINT [PK_" + pub.PubTableName + "] PRIMARY KEY  NONCLUSTERED ( [ID] )";
                        Execute(sqlStr);
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

                        //B_Model mll = new B_Model();
                        AddPubModelSetId(modelinfo);
                        Modelid = mll.GetModelInfoTableName(pub.PubTableName).ModelID;

                        sqlStr = "CREATE TABLE [dbo].[" + pub.PubTableName + "] (  [ID] [int] IDENTITY (1, 1) NOT NULL ,  [Pubupid] [int] NULL , [PubUserName] [nvarchar] (255) NULL , [PubUserID] [int] NULL , [PubContentid] [int] NULL ,[Parentid] [int] NULL DEFAULT (0), [PubIP] [nvarchar] (255) NULL , [Pubnum] [int] NULL DEFAULT (0) , [Pubstart] [int] NULL DEFAULT (0) , [PubTitle] [nvarchar] (255) NULL , [PubContent] [ntext] NULL , [PubAddTime] [datetime] NULL DEFAULT (getdate()),[Optimal] [int] NULL DEFAULT (0)) ALTER TABLE [" + pub.PubTableName + "] WITH NOCHECK ADD CONSTRAINT [PK_" + pub.PubTableName + "] PRIMARY KEY  NONCLUSTERED ( [ID] )";
                        Execute(sqlStr);
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

                        //B_Model mll = new B_Model();
                        AddPubModelSetId(modelinfo);
                        Modelid = mll.GetModelInfoTableName(pub.PubTableName).ModelID;

                        sqlStr = "CREATE TABLE [dbo].[" + pub.PubTableName + "] (  [ID] [int] IDENTITY (1, 1) NOT NULL ,  [Pubupid] [int] NULL , [PubUserName] [nvarchar] (255) NULL , [PubUserID] [int] NULL , [PubContentid] [int] NULL , [Parentid] [int] NULL DEFAULT (0),  [PubIP] [nvarchar] (255) NULL , [Pubnum] [int] NULL DEFAULT (0) , [Pubstart] [int] NULL DEFAULT (0) , [PubTitle] [nvarchar] (255) NULL , [PubContent] [ntext] NULL , [PubAddTime] [datetime] NULL DEFAULT (getdate()),[Optimal] [int] NULL DEFAULT (0)) ALTER TABLE [" + pub.PubTableName + "] WITH NOCHECK ADD CONSTRAINT [PK_" + pub.PubTableName + "] PRIMARY KEY  NONCLUSTERED ( [ID] )";
                        Execute(sqlStr);
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

                        //B_Model mll = new B_Model();
                        AddPubModelSetId(modelinfo);
                        Modelid = mll.GetModelInfoTableName(pub.PubTableName).ModelID;

                        sqlStr = "CREATE TABLE [dbo].[" + pub.PubTableName + "] (  [ID] [int] IDENTITY (1, 1) NOT NULL ,  [Pubupid] [int] NULL , [PubUserName] [nvarchar] (255) NULL , [PubUserID] [int] NULL ,  [PubContentid] [int] NULL ,[Parentid] [int] NULL DEFAULT (0), [PubIP] [nvarchar] (255) NULL , [Pubnum] [int] NULL DEFAULT (0) , [Pubstart] [int] NULL DEFAULT (0) , [PubTitle] [nvarchar] (255) NULL , [PubContent] [ntext] NULL  , [PubAddTime] [datetime] NULL DEFAULT (getdate()),[Optimal] [int] NULL DEFAULT (0) ) ALTER TABLE [" + pub.PubTableName + "] WITH NOCHECK ADD CONSTRAINT [PK_" + pub.PubTableName + "] PRIMARY KEY  NONCLUSTERED ( [ID] )";
                        Execute(sqlStr);
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
                    default:
                        sqlStr = "";
                        Modelid = 0;
                        break;
                }
            }

            if (Modelid == 0)
            {
                if (pub.PubTableName != "")
                {
                    Modelid = mll.GetModelInfoTableName(pub.PubTableName).ModelID;
                }
            }
            return Modelid;
        }
        /// <summary>
        /// 根据XML模型更新数据库模型
        /// </summary>
        /// 
        bool AddUserModelSetId(M_ModelInfo ModelInfo)
        {
            string strSql1 = "SET IDENTITY_INSERT [ZL_Model] ON;INSERT INTO [ZL_Model] ([ModelID],[ModelName],[Description],[TableName],[ItemName],[ItemUnit],[ItemIcon],[ModelType],[ContentTemplate],[MultiFlag],[NodeID],[SysModel],[FromModel],[Thumbnail],[Islotsize])VALUES(@ModelID,@ModelName,@Description,@TableName,@ItemName,@ItemUnit,@ItemIcon,@ModelType,@ContentTemplate,@MultiFlag,@NodeID,@SysModel,@FromModel,@Thumbnail,@Islotsize);SET IDENTITY_INSERT [ZL_Model] OFF";
            SqlParameter[] cmdParams = ModelInfo.GetParameters();
            SqlHelper.ExecuteSql(strSql1, cmdParams);


            string strSql = "CREATE TABLE dbo." + ModelInfo.TableName + " ([ID][int] IDENTITY (1, 1)  NOT NULL PRIMARY KEY ,[UserID][int] NOT NULL,[UserName][nvarchar] (255) NULL,[Styleid] [int] NULL DEFAULT (0),[Recycler] [bit] NULL DEFAULT (0),[IsCreate] [bit] NULL DEFAULT (0), [NewTime] [datetime] NULL DEFAULT (getdate()) )";
            return Sql.ExeSql(strSql);
        }
        bool DeleteTable(string TableName)
        {
            string sql = "DROP Table " + TableName;
            Sql.ExeSql(sql);
            return true;
        }
        bool Delete(int ModelID)
        {
            string newstrsql = "DELETE FROM ZL_Model WHERE ModelID=@ModelID";
            SqlParameter[] cmdParams = new SqlParameter[] { new SqlParameter("@ModelID", SqlDbType.Int) };
            cmdParams[0].Value = ModelID;
            return SqlHelper.ExecuteSql(newstrsql, cmdParams);
        }
        /// <summary>
        /// 获取用户关联个性字段信息
        /// </summary>
        public DataTable SelUserModelField(int modelid, int uid)
        {
            M_ModelInfo modelinfo = GetModelById(modelid);
            string sql = "SELECT * FROM " + modelinfo.TableName + " WHERE UserID=" + uid;
            return SqlHelper.ExecuteTable(CommandType.Text, sql);
        }
        #region 兼容区
        public DataTable GetModels(string ids)
        {
            ids = StrHelper.PureIDSForDB(ids);
            if (string.IsNullOrEmpty(ids)) { return null; }
            SafeSC.CheckIDSEx(ids);
            string sql = "SELECT * FROM " + TbName + " WHERE ModelID IN (" + ids + ") ORDER BY ModelID ASC";
            return SqlHelper.ExecuteTable(sql);
        }
        public void UpModel()
        {

        }
        public bool IsExistTemplate(string TableName)
        {
            return isExistTableName(TableName);
        }
        /// <summary>
        /// 更新模型的模板字段
        /// </summary>
        public bool UpdateTemplate(string Template, int ModelID)
        {
            Template = Template.Replace(" ", "").Trim('/');
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("Template", Template) };
            string sql = "UPDATE " + TbName + " SET ContentTemplate=@Template WHERE ModelID=" + ModelID;
            SqlHelper.ExecuteSql(sql, sp);
            return true;
        }
        public M_ModelInfo GetModelById(int id)
        {
            M_ModelInfo model = SelReturnModel(id);
            if (model == null) { model = new M_ModelInfo(true); }
            return model;
        }
        public M_ModelInfo GetModelInfoTableName(string TableName)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("TableName", TableName) };
            return SelModelByWhere("TableName=@TableName", sp);
        }
        public void DeleteAllModelFieldTable()
        {
            DeleteAll();
            SqlHelper.ExecuteSql("TRUNCATE TABLE ZL_ModelField");
        }
        public DataTable GetModel(string mtype)
        {
            return GetModel(mtype, "");
        }
        /// <summary>
        /// 根据模型分类，获取信息示例:GetModel("内容模型","ModelIDS ASC");GetModel("黄页模型","ModelIDS ASC");
        /// </summary>
        public DataTable GetModel(string mtype, string andwhere)
        {
            int modelType = GetModelInt(mtype);
            return SqlHelper.ExecuteTable("SELECT * FROM " + TbName + " WHERE ModelType IN(" + modelType + ") " + andwhere);
        }
        public DataTable SelByType(string ids) 
        {
            SafeSC.CheckIDSEx(ids);
            return SqlHelper.ExecuteTable("SELECT * FROM " + TbName + " WHERE ModelType IN(" + ids + ") ");
        }
        /// <summary>
        /// 更新数据库模型信息
        /// </summary>
        bool Update(M_ModelInfo model)
        {
            if (model.ModelID>0)
            {
                UpdateByID(model);
            }
            else
            {
                Insert(model);
                SqlHelper.ExecuteSql("CREATE TABLE dbo." + model.TbName + " ([ID] [int] IDENTITY (1, 1) PRIMARY Key NOT NULL)");
            }
            //string strSql = "PR_Model_Add";
            //SqlParameter[] cmdParams = initMod.GetParameters(model);
            //return SqlHelper.ExecuteProc(strSql, cmdParams);
            return true;
        }
        public int Insert(M_ModelInfo model)
        {
            return Sql.insertID(TbName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        public bool UpdateByID(M_ModelInfo model)
        {
            return Sql.UpdateByIDs(TbName, PK, model.ModelID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
        public bool UpdateModel(M_ModelInfo info)
        {
            return Update(info);
        }
        #endregion
        #region 新方法区
        public bool isExistTableName(string TableName)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("TableName", TableName) };
            return SqlHelper.ExecuteTable("SELECT Top 1 ModelID FROM " + TbName + " WHERE TableName=@TableName", sp).Rows.Count > 0;
        }
        public M_ModelInfo SelReturnModel(int id)
        {
            if (id < 1) { return null; }
            using (DbDataReader reader = DBCenter.SelReturnReader(TbName, PK, id))
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
        public M_ModelInfo SelModelByTbName(string tbname)
        {
            if (string.IsNullOrEmpty(tbname)) { return null; }
            string where = "TableName=@tbname";
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("tbname", tbname) };
            return SelModelByWhere(where, sp);
        }
        private M_ModelInfo SelModelByWhere(string where, SqlParameter[] sp)
        {
            using (DbDataReader reader = DBCenter.SelReturnReader(TbName, where, sp))
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
        /// 删除模型|对应的数据表|模型字段
        /// </summary>
        public bool DelModel(int id)
        {
            M_ModelInfo model = SelReturnModel(id);
            try { if (DBHelper.Table_IsExist(model.TableName)) { DeleteTable(model.TableName); } }
            catch (Exception ex) { ZLLog.L(ZLEnum.Log.exception, "模型表[" + model.TableName + "]删除失败,原因:" + ex.Message); }
            new B_ModelField().DelByModel(id);
            Sql.Del(TbName, PK + "=" + id);
            return true;
        }
        #endregion
        #region Tools
        private static void Execute(string sqlStr)
        {
            if (!string.IsNullOrEmpty(sqlStr))
            {
                SqlHelper.ExecuteNonQuery(CommandType.Text, sqlStr, null, true);
            }
        }
        public M_ModelInfo GetModelInfoTableName_Info(string TableName)
        {
            string sqlStr = "select * from ZL_Model where TableName=@TableName";
            SqlParameter[] cmdParams = new SqlParameter[] { new SqlParameter("@TableName", SqlDbType.NVarChar) };
            cmdParams[0].Value = TableName;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sqlStr, cmdParams))
            {
                if (reader.Read())
                {
                    return initMod.GetModelFromReader(reader);
                }
                else
                    return new M_ModelInfo(true);
            }
        }
        /// <summary>
        /// 系统模型分类
        /// </summary>
        public string GetModelType(int type)
        {
            string typename = "";
            switch (type)
            {
                case 1:
                    typename = "内容模型";
                    break;
                case 2:
                    typename = "商品模型";
                    break;
                case 3:///黄页申请模型
                    typename = "用户模型";
                    break;
                case 4:
                    typename = "黄页内容模型";
                    break;
                case 5:
                    typename = "店铺商品模型";
                    break;
                case 6:
                    typename = "店铺申请模型";
                    break;
                case 7:
                    typename = "互动模型";
                    break;
                case 8:
                    typename = "功能模型";
                    break;
                case 9:
                    typename = "用户注册";
                    break;
                case 10://黄页申请模型
                    typename = "黄页模型";
                    break;
                case 11://CRM模型
                    typename = "CRM模型";
                    break;
                case 12:
                    typename = "OA办公模型";
                    break;
            }
            return typename;
        }
        public int GetModelInt(string name)
        {
            name = name.Replace(" ", "").Replace("'", "");
            switch (name)
            {
                case "内容模型":
                    return 1;
                case "商品模型":
                    return 2;
                case "用户模型":
                    return 3;
                case "黄页内容模型":
                    return 4;
                case "店铺商品模型":
                    return 5;
                case "店铺申请模型":
                    return 6;
                case "互动模型":
                    return 7;
                case "功能模型":
                    return 8;
                case "用户注册":
                    return 9;
                case "黄页模型":
                    return 10;
                case "CRM模型":
                    return 11;
                case "OA办公模型":
                    return 12;
                default:
                    throw new Exception("[" + name + "]类型不存在");
            }
        }
        #endregion
        #region 添加模型
        /// <summary>
        /// 添加内容模型
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddModel(M_ModelInfo model)
        {
            string strSql = "CREATE TABLE dbo." + model.TableName + " ([ID] [int] IDENTITY (1, 1) PRIMARY Key NOT NULL)";
            return AddModel(model, strSql);
        }
        /// <summary>
        /// [main]创建模型,表,字段
        /// </summary>
        public bool AddModel(M_ModelInfo model, string sql)
        {
            if (string.IsNullOrEmpty(model.ItemIcon)) { model.ItemIcon = "fa fa-file"; }//默认模型图标
            model.TableName = model.TableName.Replace(" ", "");
            if (string.IsNullOrEmpty(model.TableName)) { throw new Exception("模型表名不能为空"); }
            if (DBHelper.Table_IsExist(model.TableName)) { throw new Exception("模型表[" + model.TableName + "]已存在,取消创建"); }
            model.ModelID = Sql.insertID(TbName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
            SqlHelper.ExecuteSql(sql);
            //如果是内容模型
            if (model.TableName.ToLower().Contains("zl_c_"))
            {
                new B_ModelField().AddFroms(model.ModelID);
            }
            return true;
        }
       
        /// <summary>
        ///添加互动模型
        /// </summary>
        public int AddPubModel(M_ModelInfo model)//模型尚未插入数据库
        {
            string strSql = "CREATE TABLE dbo." + model.TableName + " ([ID] [int] IDENTITY (1, 1) PRIMARY Key NOT NULL)";
            AddModel(model, strSql);
            return model.ModelID;
        }
        public bool AddStoreModel(M_ModelInfo model)
        {
            string strSql = "CREATE TABLE dbo." + model.TableName + @" ([ID][int] IDENTITY (1, 1)  NOT NULL PRIMARY KEY ,
                            [UserID][int] NOT NULL,
                            [UserName][nvarchar] (255) NULL,
                            [StoreName][varchar] (50) NULL,
                            [StoreCredit] [int] NULL DEFAULT (0),
                            [StoreCommendState] [int] NULL DEFAULT (0),
                            [StoreState] [int] NULL DEFAULT (0),
                            [StoreStyleID] [int] NULL DEFAULT (0),
                            [StoreModelID] [int] NULL ,
                            [StoreStyle] [int] NULL DEFAULT (0),
                            [AddTime][datetime] NULL DEFAULT (getdate()))";

            return AddModel(model, strSql);
        }
        public bool AddUserModel(M_ModelInfo model)
        {
            string strSql = "CREATE TABLE dbo." + model.TableName + " ([ID][int] IDENTITY (1, 1)  NOT NULL PRIMARY KEY ,[UserID][int] NOT NULL,[UserName][nvarchar] (255) NULL,[Styleid] [int] NULL DEFAULT (0),[Recycler] [bit] NULL DEFAULT (0),[IsCreate] [bit] NULL DEFAULT (0), [NewTime] [datetime] NULL DEFAULT (getdate()) )";
            return AddModel(model, strSql);
        }
        /// <summary>
        /// 添加功能模型
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public bool AddFunModel(M_ModelInfo model)
        {
            string strSql = "CREATE TABLE dbo." + model.TableName + " ([Id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,[Uid] [int] NOT NULL,[Title] [nvarchar](50) NULL, [Url] [nvarchar](50) NOT NULL, [PositionTop] [int] NOT NULL, [PositionLeft] [int] NOT NULL, [PositionWidth] [int] NOT NULL, [PositionHeight] [int] NOT NULL, [IsVisble] [int] NOT NULL, [UpdateTime] [datetime] NOT NULL, [PageId] [int] NOT NULL, [MenuID] [int] NOT NULL, [LevelCut] [int] NULL, [VerticalCut] [int] NULL,[status] [int] NULL)";
            return AddModel(model, strSql);
        }
        public bool AddPageModel(M_ModelInfo model)
        {
            string strSql = "CREATE TABLE dbo." + model.TableName + " ([ID][int] IDENTITY (1, 1)  NOT NULL PRIMARY KEY ,[UserID][int] NOT NULL,[UserName][nvarchar] (255) NULL,[Styleid] [int] NULL DEFAULT (0))";
            return AddModel(model, strSql);
        }
        public bool AddUserModel_Info(M_ModelInfo ModelInfo)
        {
            string strSql1 = "INSERT INTO [ZL_Model] ([ModelName],[Description],[TableName],[ItemName],[ItemUnit],[ItemIcon],[ModelType],[ContentTemplate],[MultiFlag],[NodeID],[SysModel],[FromModel],[Thumbnail],[Islotsize])VALUES(@ModelName,@Description,@TableName,@ItemName,@ItemUnit,@ItemIcon,@ModelType,@ContentTemplate,@MultiFlag,@NodeID,@SysModel,@FromModel,@Thumbnail,@Islotsize)";
            SqlParameter[] cmdParams = ModelInfo.GetParameters();
            SqlHelper.ExecuteSql(strSql1, cmdParams);
            string strSql = "CREATE TABLE dbo." + ModelInfo.TableName + " ([ID][int] IDENTITY (1, 1)  NOT NULL PRIMARY KEY ,[UserID][int] NOT NULL,[UserName][nvarchar] (255) NULL,[Styleid] [int] NULL DEFAULT (0),[Recycler] [bit] NULL DEFAULT (0),[IsCreate] [bit] NULL DEFAULT (0), [NewTime] [datetime] NULL DEFAULT (getdate()) )";
            return Sql.ExeSql(strSql);
        }
        public bool Addinput_Info(M_ModelInfo ModelInfo)
        {
            string strSql1 = "SET IDENTITY_INSERT [ZL_Model] ON;INSERT INTO [ZL_Model] ([ModelID],[ModelName],[Description],[TableName],[ItemName],[ItemUnit],[ItemIcon],[ModelType],[ContentTemplate],[MultiFlag],[NodeID],[SysModel],[FromModel],[Thumbnail],[Islotsize])VALUES(@ModelID,@ModelName,@Description,@TableName,@ItemName,@ItemUnit,@ItemIcon,@ModelType,@ContentTemplate,@MultiFlag,@NodeID,@SysModel,@FromModel,@Thumbnail,@Islotsize);SET IDENTITY_INSERT [ZL_Model] OFF";
            SqlParameter[] cmdParams = ModelInfo.GetParameters();
            SqlHelper.ExecuteSql(strSql1, cmdParams);

            string strSql = "CREATE TABLE dbo." + ModelInfo.TableName + " ([ID] [int] IDENTITY (1, 1) PRIMARY Key NOT NULL)";
            return Sql.ExeSql(strSql);
        }
        /// <summary>
        /// 添加导入模型
        /// </summary>
        public bool Addinput(M_ModelInfo ModelInfo)
        {
            string strSql1 = "SET IDENTITY_INSERT [ZL_Model] ON;INSERT INTO [ZL_Model] ([ModelID],[ModelName],[Description],[TableName],[ItemName],[ItemUnit],[ItemIcon],[ModelType],[ContentTemplate],[MultiFlag],[NodeID],[SysModel],[FromModel],[Thumbnail],[Islotsize])VALUES(@ModelID,@ModelName,@Description,@TableName,@ItemName,@ItemUnit,@ItemIcon,@ModelType,@ContentTemplate,@MultiFlag,@NodeID,@SysModel,@FromModel,@Thumbnail,@Islotsize);SET IDENTITY_INSERT [ZL_Model] OFF";
            SqlParameter[] cmdParams = ModelInfo.GetParameters();
            SqlHelper.ExecuteSql(strSql1, cmdParams);

            string strSql = "CREATE TABLE dbo." + ModelInfo.TableName + " ([ID] [int] IDENTITY (1, 1) PRIMARY Key NOT NULL)";
            return Sql.ExeSql(strSql);
        }
        #endregion
    }
}