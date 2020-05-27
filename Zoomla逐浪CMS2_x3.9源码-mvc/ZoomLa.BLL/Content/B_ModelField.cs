namespace ZoomLa.BLL
{
    using SQLDAL.SQL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Text.RegularExpressions;
    using ZoomLa.BLL.Helper;
    using ZoomLa.Common;
    using ZoomLa.Components;
    using ZoomLa.Model;
    using ZoomLa.SQLDAL;
    public class B_ModelField
    {
        public B_ModelField()
        {
            strTableName = initMod.TbName;
            PK = initMod.PK;
        }
        public string PK, strTableName;
        private M_ModelField initMod = new M_ModelField();
        #region 整理区
        /// <summary>
        /// 更改顺序,只能移动非系统字段,数字越小越前
        /// </summary>
        //public void OrderChange(M_ModelField model, string move)
        //{
        //    //switch (move)
        //    //{
        //    //    case "up":
        //    //        string sql = "SELECT TOP 1 FieldID,OrderID FROM " + strTableName + " WHERE ORDERID<" + model.OrderID + " ORDER BY OrderID ASC";
        //    //        DataTable dt = SqlHelper.ExecuteTable(sql);
        //    //        if (dt.Rows.Count < 1) { return; }
        //    //        //直接加1,避免OrderID相同
        //    //        string upsql = "UPDATE " + strTableName + " SET OrderID=OrderID+1 WHERE FieldID=" + dt.Rows[0]["FieldID"] + ";";
        //    //        upsql += "UPDATE " + strTableName + " SET OrderID=" + dt.Rows[0]["OrderID"] + " WHERE FieldID=" + model.FieldID + ";";
        //    //        break;
        //    //    case "down":
        //    //        string sql = "SELECT TOP 1 FieldID,OrderID FROM " + strTableName + " WHERE ORDERID>" + model.OrderID + " ORDER BY OrderID ASC";
        //    //        break;
        //    //}
        //}
        public DataTable Sel()
        {
            return Sql.Sel(strTableName);
        }
        public DataTable Sel(int ID)
        {
            return Sql.Sel(strTableName, PK, ID);
        }
        /// <summary>
        /// 获取指定模型的字段,默认不含系统字段,包含禁用
        /// </summary>
        /// <param name="sys">是否包含系统字段</param>
        /// <param name="baned">是否包含禁用字段</param>
        public DataTable SelByModelID(int modelID, bool sys = false, bool baned = true)
        {
            string where = "ModelID=" + modelID;
            if (sys == false)
            {
                where += " AND sys_type=0 ";
            }
            if (baned == false)
            {
                where += " AND IsCopy!='-1' ";
            }
            return DBCenter.Sel(strTableName, where, "OrderID ASC");
        }
        private M_ModelField SelModelByWhere(string where, string order = "", List<SqlParameter> sp = null)
        {
            using (DbDataReader reader = DBCenter.SelReturnReader(strTableName, where, order, sp))
            {
                if (reader.Read())
                {
                    return initMod.GetModelFromReader(reader);
                }
                else
                {
                    return new M_ModelField(true);
                }
            }
        }
        #endregion
        #region 兼容区
        public DataSet GetModelFieldAllList(int modelid)
        {
            DataSet ds = new DataSet();
            DataTable dt = SelByModelID(modelid, false);
            dt.TableName = "Table";
            ds.Tables.Add(dt);
            return ds;
        }
        public M_ModelField GetModelByID(string ModelID, int FieldID)
        {
            return SelModelByWhere("ModelID=" + Convert.ToInt32(ModelID) + " AND FieldID=" + FieldID);
        }
        public M_ModelField GetModelByFieldName(int ModelID, string FieldName)
        {
            List<SqlParameter> sp = new List<SqlParameter>() { new SqlParameter("FieldName", FieldName) };
            return SelModelByWhere("ModelID=" + ModelID + " AND FieldName=@FieldName", "", sp);
        }
        /// <summary>
        /// 根据ID,从数据库中返回模型
        /// </summary>
        public M_ModelField GetModelByIDXML(int ID)
        {
            return SelReturnModel(ID);
        }
        public M_ModelField SelReturnModel(int id)
        {
            if (id < 1) { return null; }
            using (SqlDataReader reader = Sql.SelReturnReader(strTableName, PK, id))
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
        #endregion
        /// 按表名（或者modelid）与字段名查询
        /// </summary>
        /// <param name="tbname"></param>
        /// <param name="fieldname"></param>
        /// <returns></returns>
        public DataTable GetTableByFieldName(string fieldname, string tbname, int modelid = 0)
        {
            SafeSC.CheckDataEx(tbname);
            string where = " FieldName=@name ";
            if (modelid > 0 && !tbname.Equals("ZL_UserBaseField", StringComparison.CurrentCultureIgnoreCase))
            {
                where += " AND ModelId=" + modelid;
            }
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("name", fieldname) };
            string sql = "SELECT * FROM " + tbname + " WHERE  " + where;
            return SqlHelper.ExecuteTable(sql, sp);
        }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="cpage"></param>
        /// <param name="psize"></param>
        /// <param name="ModelID"></param>
        /// <param name="fieldname">字段名</param>
        /// <param name="sys">是否包含系统字段</param>
        /// <param name="baned">是否包含禁用字段</param>
        /// <returns></returns>
        public PageSetting SelPage(int cpage, int psize, int ModelID = 0, string fieldname = "", bool sys = false, bool baned = true)
        {
            string where = " 1=1";
            List<SqlParameter> sp = new List<SqlParameter>();
            if (ModelID > 0) { where += " AND ModelID=" + ModelID; }
            if (!string.IsNullOrEmpty(fieldname)) { where += " AND FieldName=@fieldname"; sp.Add(new SqlParameter("fieldname", fieldname)); }
            if (!sys) { where += " AND sys_type=0 "; }
            if (!baned) { where += " AND IsCopy!='-1' "; }
            PageSetting setting = PageSetting.Single(cpage, psize, strTableName, PK, where, "OrderID ASC", sp);
            DBCenter.SelPage(setting);
            return setting;
        }
        /// <summary>
        /// 根据ID,从Xml中获取模型的第一个PicType的字段
        /// </summary>
        public M_ModelField GetFieldNameByMID(int ModelID)
        {
            string where = "[ModelID]=" + ModelID + " AND FieldType='PicType' AND IsShow=1";
            return SelModelByWhere(where);
        }
        /// <summary>
        /// 从数据库中读取模型的字段,注意数字库中只保存了自定义字段
        /// </summary>
        public DataTable DB_SelByModel(int modelid)
        {
            return SqlHelper.ExecuteTable("SELECT * FROM " + strTableName + " WHERE ModelID=" + modelid);
        }
        /// <summary>
        /// 系统模型分类
        /// </summary>
        public string GetModelType(int type)
        {
            return new B_Model().GetModelType(type);
        }
        /// <summary>
        /// 返回系统字段列表
        /// </summary>
        public DataTable GetSysteFieldList()
        {
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("FieldName", typeof(string)));
            table.Columns.Add(new DataColumn("FieldAlias", typeof(string)));
            table.Columns.Add(new DataColumn("FieldType", typeof(string)));
            table.Columns.Add(new DataColumn("IsNotNull", typeof(string)));
            string[] strArray = "GeneralID|内容ID|数字|1,Title|标题|标题|1,NodeID|所属节点|节点|1,SpecialID|专题|专题|0,Hits|点击数|数字|0,EliteLevel|推荐级别|数字|1,CreateTime|生成时间|日期时间|1,UpDateTime|更新时间|日期时间|1,Status|状态|数字|0".Split(new char[] { ',' });
            for (int i = 0; i < strArray.Length; i++)
            {
                string[] strArray2 = strArray[i].Split(new char[] { '|' });
                DataRow row = table.NewRow();
                if (strArray2 != null && strArray2.Length > 0)
                {
                    row[0] = strArray2[0];
                }
                if (strArray2 != null && strArray2.Length > 1)
                {
                    row[1] = strArray2[1];
                }
                if (strArray2 != null && strArray2.Length > 2)
                {
                    row[2] = strArray2[2];
                }
                if (strArray2 != null && strArray2.Length > 3)
                {
                    row[3] = strArray2[3];
                }
                table.Rows.Add(row);
            }
            return table;
        }
        /// <summary>
        /// 采集信息系统字段
        /// </summary>
        public DataTable GetCollFieldList()
        {
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("FieldName", typeof(string)));
            table.Columns.Add(new DataColumn("FieldAlias", typeof(string)));
            table.Columns.Add(new DataColumn("FieldType", typeof(string)));
            table.Columns.Add(new DataColumn("Content", typeof(string)));
            table.Columns.Add(new DataColumn("Description", typeof(string)));
            table.Columns.Add(new DataColumn("ModelID", typeof(string)));
            string[] strArray = "Title|标题|TextType|,=,=,=|$Hits|点击数|NumType|,=,=|$EliteLevel|推荐级别|NumType|,=,=|$CreateTime|生成时间|DateType||$UpDateTime|更新时间|DateType||$Status|状态|NumType|,=,=|".Split(new char[] { '$' });
            for (int i = 0; i < strArray.Length; i++)
            {
                string[] strArray2 = strArray[i].Split('|');
                DataRow row = table.NewRow();
                if (strArray2 != null && strArray2.Length > 0)
                {
                    row["FieldName"] = strArray2[0];
                }
                if (strArray2 != null && strArray2.Length > 1)
                {
                    row["FieldAlias"] = strArray2[1];
                }
                if (strArray2 != null && strArray2.Length > 2)
                {
                    row["FieldType"] = strArray2[2];
                }
                if (strArray2 != null && strArray2.Length > 3)
                {
                    row["Content"] = strArray2[3];
                }
                row["ModelID"] = "0";
                table.Rows.Add(row);
            }
            return table;
        }
        /// <summary>
        /// 互动模型系统字段
        /// </summary>
        public DataTable GetPubFieldList()
        {
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("FieldName", typeof(string)));
            table.Columns.Add(new DataColumn("FieldAlias", typeof(string)));
            table.Columns.Add(new DataColumn("FieldType", typeof(string)));
            table.Columns.Add(new DataColumn("IsNotNull", typeof(string)));
            string[] strArray = "ID|模块ID|数字|1,Pubupid|互动ID|数字|1,PubTitle|模块标题|字符|0,PubUserName|参与用户名|字符|0,PubUserID|参与用户ID|数字|0,PubContentid|信息ID|数字|0,Parentid|所属ID|数字|0,PubIP|IP地址|字符|1,Pubnum|参与人数|日期时间|1,Pubstart|是否审核|数字|1,PubContent|模块内容|备注|0,PubAddTime|添加时间|日期时间|1,Optimal|最佳答案|数字|1,cookflag|cookie验证|字符|0".Split(new char[] { ',' });
            for (int i = 0; i < strArray.Length; i++)
            {
                string[] strArray2 = strArray[i].Split(new char[] { '|' });
                DataRow row = table.NewRow();
                if (strArray2 != null && strArray2.Length > 0)
                {
                    row[0] = strArray2[0];
                }
                if (strArray2 != null && strArray2.Length > 1)
                {
                    row[1] = strArray2[1];
                }
                if (strArray2 != null && strArray2.Length > 2)
                {
                    row[2] = strArray2[2];
                }
                if (strArray2 != null && strArray2.Length > 3)
                {
                    row[3] = strArray2[3];
                }
                table.Rows.Add(row);
            }
            return table;
        }
        /// <summary>
        /// 获得用户模型的系统字段
        /// </summary>
        public DataTable GetSysUserField()
        {
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("FieldName", typeof(string)));
            table.Columns.Add(new DataColumn("FieldAlias", typeof(string)));
            table.Columns.Add(new DataColumn("FieldType", typeof(string)));
            table.Columns.Add(new DataColumn("IsNotNull", typeof(string)));
            string[] strArray = "ID|系统ID|数字|1,UserID|会员ID|数字|1,UserName|会员名|字符型|1,Styleid|样式ID|数字|1,PageSkins_ID|风格ID|数字".Split(new char[] { ',' });
            for (int i = 0; i < strArray.Length; i++)
            {
                string[] strArray2 = strArray[i].Split(new char[] { '|' });
                DataRow row = table.NewRow();
                if (strArray2 != null && strArray2.Length > 0)
                {
                    row[0] = strArray2[0];
                }
                if (strArray2 != null && strArray2.Length > 1)
                {
                    row[1] = strArray2[1];
                }
                if (strArray2 != null && strArray2.Length > 2)
                {
                    row[2] = strArray2[2];
                }
                if (strArray2 != null && strArray2.Length > 3)
                {
                    row[3] = strArray2[3];
                }
                if (strArray2 != null && strArray2.Length > 4)
                {
                    row[4] = strArray2[4];
                }
                table.Rows.Add(row);
            }
            return table;
        }
        /// <summary>
        /// 获取功能模型的系统字段
        /// </summary>
        public DataTable GetSysFunField()
        {
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("FieldName", typeof(string)));
            table.Columns.Add(new DataColumn("FieldAlias", typeof(string)));
            table.Columns.Add(new DataColumn("FieldType", typeof(string)));
            table.Columns.Add(new DataColumn("IsNotNull", typeof(string)));
            string[] strArray = "ID|系统ID|数字|1,Uid|会员ID|数字|1,Title|标题|字符型|1,Url|网站链接|字符型|1,PositionTop|位置(上)|数字|1,PositionLeft|位置(左)|数字|1,PositionWidth|宽度|数字|1,PositionHeight|高度|数字|1,IsVisble|是否显示|数字|1,UpdateTime|修改时间|时间类型|1,PageId|页面ID|数字|1,MenuID|页签ID|数字|1,LevelCut|横级|数字|0,VerticalCut|纵级|数字|0,status|状态|数字|0".Split(new char[] { ',' });
            for (int i = 0; i < strArray.Length; i++)
            {
                string[] strArray2 = strArray[i].Split(new char[] { '|' });
                DataRow row = table.NewRow();
                if (strArray2 != null && strArray2.Length > 0)
                {
                    row[0] = strArray2[0];
                }
                if (strArray2 != null && strArray2.Length > 1)
                {
                    row[1] = strArray2[1];
                }
                if (strArray2 != null && strArray2.Length > 2)
                {
                    row[2] = strArray2[2];
                }
                if (strArray2 != null && strArray2.Length > 3)
                {
                    row[3] = strArray2[3];
                }
                table.Rows.Add(row);
            }
            return table;
        }
        /// <summary>
        /// 返回店铺模型的系统字段
        /// </summary>
        public DataTable GetSysStore()
        {
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("FieldName", typeof(string)));
            table.Columns.Add(new DataColumn("FieldAlias", typeof(string)));
            table.Columns.Add(new DataColumn("FieldType", typeof(string)));
            table.Columns.Add(new DataColumn("IsNotNull", typeof(string)));
            string[] strArray = "ID|系统ID|数字|1,UserID|会员ID|数字|1,UserName|会员名|字符型|1,StoreName|商铺名|字符型|1,StoreCredit|信用积分|数字|1,StoreCommendState|推荐状态|数字|1,StoreState|商铺状态|数字|1,StoreModelID|店铺模型ID|数字|1,StoreStyleID|店铺模板ID|数字|1,AddTime|添加时间|日期|1".Split(new char[] { ',' });
            for (int i = 0; i < strArray.Length; i++)
            {
                string[] strArray2 = strArray[i].Split(new char[] { '|' });
                DataRow row = table.NewRow();
                if (strArray2 != null && strArray2.Length > 0)
                {
                    row[0] = strArray2[0];
                }
                if (strArray2 != null && strArray2.Length > 1)
                {
                    row[1] = strArray2[1];
                }
                if (strArray2 != null && strArray2.Length > 2)
                {
                    row[2] = strArray2[2];
                }
                if (strArray2 != null && strArray2.Length > 3)
                {
                    row[3] = strArray2[3];
                }
                table.Rows.Add(row);
            }
            return table;
        }
        /// <summary>
        /// 返回项目管理模型系统字段
        /// </summary>
        public DataTable GetProjectsStore()
        {
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("FieldName", typeof(string)));
            table.Columns.Add(new DataColumn("FieldAlias", typeof(string)));
            table.Columns.Add(new DataColumn("FieldType", typeof(string)));
            table.Columns.Add(new DataColumn("IsNotNull", typeof(string)));
            string[] strArray = "ID|系统ID|数字|1,Name|项目名称|字符型|1,TypeID|所属类型ID|数字|0,OrderID|关联订单ID|数字|0,UserID|关联用户ID|数字|0,UserInfo|甲方信息|字符型|0,Requirements|项目需求|字符|0,Price|价格|数字|0,BeginTime|启动时间|日期|0,Leader|项目经理|字符|0,WebCoding|技术负责人|字符|0,AuditStatus|审核状态|数字|0,ProStatus|项目状态|数字|0,Progress|项目进程|数字|0,CompletionTime|完成时间|日期|0,Rating|评分|数字|0,Evaluation|项目评价|字符型|0,ApplicationTime|申请时间|日期|0".Split(new char[] { ',' });
            for (int i = 0; i < strArray.Length; i++)
            {
                string[] strArray2 = strArray[i].Split(new char[] { '|' });
                DataRow row = table.NewRow();
                if (strArray2 != null && strArray2.Length > 0)
                {
                    row[0] = strArray2[0];
                }
                if (strArray2 != null && strArray2.Length > 1)
                {
                    row[1] = strArray2[1];
                }
                if (strArray2 != null && strArray2.Length > 2)
                {
                    row[2] = strArray2[2];
                }
                if (strArray2 != null && strArray2.Length > 3)
                {
                    row[3] = strArray2[3];
                }
                table.Rows.Add(row);
            }
            return table;
        }
        /// <summary>
        /// 根据标识类型返回系统数据类型
        /// </summary>
        public SqlDbType GetPara(string FieldName, string FieldType)
        {
            SqlDbType result = SqlDbType.NVarChar;
            switch (FieldType)
            {
                case "TextType":
                    result = SqlDbType.NVarChar;
                    break;
                case "OptionType":
                    result = SqlDbType.NVarChar;
                    break;
                case "GradeOptionType":
                    result = SqlDbType.NVarChar;
                    break;
                case "ListBoxType":
                    result = SqlDbType.NText;
                    break;
                case "DateType":
                    result = SqlDbType.DateTime;
                    break;
                case "MultipleHtmlType":
                    result = SqlDbType.NText;
                    break;
                case "MultipleTextType":
                    result = SqlDbType.NText;
                    break;
                case "SmallFileType":
                    result = SqlDbType.NVarChar;
                    break;
                case "PullFileType":
                    result = SqlDbType.NVarChar;
                    break;
                case "FileType":
                    result = SqlDbType.NText;
                    break;
                case "PicType":
                    result = SqlDbType.NVarChar;
                    break;

                case "FIT_SqlType":
                    result = SqlDbType.NVarChar;
                    break;
                case "SqlType":
                    result = SqlDbType.Image;
                    break;
                case "SqlFile":
                    result = SqlDbType.Image;
                    break;
                case "FileSize":
                    result = SqlDbType.NVarChar;
                    break;
                case "ThumbField":
                    result = SqlDbType.NVarChar;
                    break;
                case "MultiPicType":

                    result = SqlDbType.NText;
                    break;
                case "OperatingType":
                    result = SqlDbType.NVarChar;
                    break;
                case "SuperLinkType":
                    result = SqlDbType.NVarChar;
                    break;
                case "MoneyType":
                    result = SqlDbType.Money;
                    break;
                case "BoolType":
                    result = SqlDbType.Bit;
                    break;
                case "int":
                    result = SqlDbType.Int;
                    break;
                case "float":
                    result = SqlDbType.Float;
                    break;
                case "money":
                    result = SqlDbType.Money;
                    break;
                case "Upload":
                    result = SqlDbType.NVarChar;
                    break;
                case "api_qq_mvs":
                    result = SqlDbType.NVarChar;
                    break;
            }
            return result;
        }
        M_ModelField GetModelByFieldNames(int ModelID, string FieldName)
        {
            string strSql = "select * from ZL_ModelField where ModelId=@ModelId and FieldName=@FieldName";
            SqlParameter[] cmdParams = new SqlParameter[] {
                new SqlParameter("@ModelId",SqlDbType.Int,4),
                new SqlParameter("@FieldName",SqlDbType.NVarChar)};
            cmdParams[0].Value = ModelID;
            cmdParams[1].Value = FieldName;
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(CommandType.Text, strSql, cmdParams))
            {
                if (sdr.Read())
                {
                    return initMod.GetModelFromReader(sdr);
                }
                else
                {
                    return new M_ModelField(true);
                }
            }
        }
        //-----------------------INSERT
        private int InsertID(M_ModelField model)
        {
            model.FieldName = model.FieldName.Replace(" ", "");
            if (string.IsNullOrEmpty(model.FieldName)) { throw new Exception("字段名不能为空"); }
            if (model.ModelID < 1) { throw new Exception("模型ID不能为空"); }
            if (IsExists(model.ModelID, model.FieldName)) { throw new Exception("[" + model.FieldName + "]名称重复"); }
            return Sql.insertID(strTableName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        private void Add(M_ModelField model)
        {
            InsertID(model);
        }
        /// <summary>
        /// 请勿直接调用,仅用于UserBase这些拥有独立数据表的模型使用,其他请调AddModelField()
        /// </summary>
        /// <param name="tablename">表名</param>
        /// <param name="fieldname">字段名</param>
        /// <param name="fieldtype">数据库字段的类型</param>
        /// <param name="defaultvalue">默认值</param>
        public void AddField(string tablename, string fieldname, string fieldtype, string defaultvalue)
        {
            SafeSC.CheckDataEx(tablename, defaultvalue, fieldname);
            if (StrHelper.StrNullCheck(tablename, fieldname)) { throw new Exception("表名与字段名不能为空"); }
            if (!DBHelper.Table_IsExist(tablename)) { throw new Exception("数据表[" + tablename + "]不存在"); }
            fieldname = fieldname.Replace(" ", "");
            string sqlstr = "";
            switch (fieldtype.ToLower())
            {
                case "nvarchar":
                    sqlstr = "ALTER TABLE [" + tablename + "] ADD [" + fieldname + "] nvarchar(255) DEFAULT ('" + defaultvalue + "')";
                    break;
                case "varchar":
                    sqlstr = "ALTER TABLE [" + tablename + "] ADD [" + fieldname + "] varchar(255) DEFAULT ('" + defaultvalue + "')";
                    break;
                case "ntext":
                    sqlstr = "ALTER TABLE [" + tablename + "] ADD [" + fieldname + "] ntext DEFAULT ('" + defaultvalue + "')";
                    break;
                case "int":
                    sqlstr = "ALTER TABLE [" + tablename + "] ADD [" + fieldname + "] int DEFAULT (0)";
                    break;
                case "bit":
                    sqlstr = "ALTER TABLE [" + tablename + "] ADD [" + fieldname + "] bit DEFAULT (0)";
                    break;
                case "char":
                    sqlstr = "ALTER TABLE [" + tablename + "] ADD [" + fieldname + "] char(10)";
                    break;
                case "datetime":
                    sqlstr = "ALTER TABLE [" + tablename + "] ADD [" + fieldname + "] datetime DEFAULT (Null)";
                    break;
                case "decimal":
                    sqlstr = "ALTER TABLE [" + tablename + "] ADD [" + fieldname + "] decimal(8)";
                    break;
                case "money":
                    sqlstr = "ALTER TABLE [" + tablename + "] ADD [" + fieldname + "] money";
                    break;
                case "image":
                    sqlstr = "ALTER TABLE [" + tablename + "] ADD [" + fieldname + "] image";
                    break;
                case "Binary":
                    sqlstr = "ALTER TABLE [" + tablename + "] ADD [" + fieldname + "] Binary";
                    break;
                case "nchar":
                    sqlstr = "ALTER TABLE [" + tablename + "] ADD [" + fieldname + "] nchar(10)";
                    break;
                case "text":
                    sqlstr = "ALTER TABLE [" + tablename + "] ADD [" + fieldname + "] text";
                    break;
                case "float":
                    sqlstr = "ALTER TABLE [" + tablename + "] ADD [" + fieldname + "] float(8)";
                    break;
                case "":
                    throw new Exception(fieldname + "的字段类型为空");
                default:
                    throw new Exception(fieldname + ":" + fieldtype + "不是有效的类型");
            }
            Sql.ExeSql(sqlstr);
        }
        /// <summary>
        /// [main]添加字段记录进入数据库与指定表
        /// </summary>
        public void AddModelField(M_ModelInfo model, M_ModelField fieldMod)
        {
            if (StrHelper.StrNullCheck(fieldMod.FieldType)) { throw new Exception("表名:[" + model.TableName + "],[" + fieldMod.FieldName + "]字段类型不能为空"); }
            if (StrHelper.StrNullCheck(fieldMod.FieldName)) { throw new Exception("表名:[" + model.TableName + "],字段名不能为空"); }
            if (IsExists(model.ModelID, fieldMod.FieldName)) { throw new Exception("表名:[" + model.TableName + "],[" + fieldMod.FieldName + "]名称重复"); }
            if (fieldMod.OrderID < 1) { fieldMod.OrderID = (GetMaxOrder(model.ModelID) + 1); }
            // string fName, string fAlias, string fType, string dataType
            fieldMod.ModelID = model.ModelID;
            //fieldMod.FieldName = fName;//字段名
            //fieldMod.FieldAlias = fAlias;//别名
            //fieldMod.Sys_type = false;//标识身份为系统字段,不可删
            //fieldMod.FieldType = fType;// "TextType";
            //fieldMod.IsCopy = 1;
            //fieldMod.IsChain = false;
            AddField(model.TableName, fieldMod.FieldName, GetTableFieldType(fieldMod), "");//加入表中  text
            fieldMod.FieldID = InsertID(fieldMod);//其ID与添加入的ID一致
        }
        //-----------------------UPDATE
        /// <summary>
        /// [main]更新模型信息,但不再更新数据表
        /// </summary>
        /// <param name="model"></param>
        public void Update(M_ModelField model)
        {
            UpdateByID(model);
        }
        private bool UpdateByID(M_ModelField model)
        {
            return Sql.UpdateByIDs(strTableName, PK, model.FieldID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
        public void UpdateOrder(M_ModelField first, M_ModelField second)
        {
            string sql = "UPDATE " + strTableName + " SET OrderID=" + first.OrderID + " WHERE FieldID=" + first.FieldID + ";";
            sql += "UPDATE " + strTableName + " SET OrderID=" + second.OrderID + " WHERE FieldID=" + second.FieldID + ";";
            SqlHelper.ExecuteSql(sql);
        }
        public void UpdateOrder(M_ModelField model)
        {
            UpdateField("OrderID", model.OrderID.ToString(), model.FieldID);
        }
        private void UpdateField(string field, string value, int fieldid)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("value", value) };
            string sql = "UPDATE " + strTableName + " SET " + field + "=@value WHERE FieldID=" + fieldid;
            SqlHelper.ExecuteSql(sql, sp);
        }
        public void UpdateShowList(M_ModelField model)
        {
            UpdateField("ShowList", model.ShowList.ToString(), model.FieldID);
        }
        //-----------------------DEL
        /// <summary>
        /// 删除模型时调用
        /// </summary>
        public void DelByModel(int modelid)
        {
            string sql = "DELETE FROM " + strTableName + " WHERE ModelID=" + modelid;
        }
        public void DelByFieldID(string ids)
        {
            foreach (string id in ids.Split(','))
            {
                DelByFieldID(DataConvert.CLng(id));
            }
        }
        /// <summary>
        /// [main]删除字段,并从对应的数据表中移除
        /// </summary>
        /// <param name="id"></param>
        public void DelByFieldID(int id)
        {
            M_ModelField fieldMod = SelReturnModel(id);
            if (fieldMod == null) { return; }
            M_ModelInfo modelMod = new B_Model().SelReturnModel(fieldMod.ModelID);
            Sql.Del(strTableName, PK + "=" + id);
            if (DBHelper.Table_IsExist(modelMod.TableName))
            {
                DelField(modelMod.TableName, fieldMod.FieldName);
            }
        }
        /// <summary>
        /// 删除字段定义,用于某些拥有固定表的模型
        /// </summary>
        public bool DelField(string TableName, string FieldName)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("@tablename", TableName), new SqlParameter("@fieldname", FieldName) };
            string sql = "SELECT b.name FROM syscolumns a,sysobjects b WHERE a.id=object_id(@tablename) AND b.id=a.cdefault AND a.name=@fieldname AND b.name LIKE 'DF%'";
            DataTable dt = SqlHelper.ExecuteTable(sql, sp);
            string name = dt.Rows[0]["name"].ToString();
            sql = "ALTER TABLE " + TableName + " DROP constraint " + name;
            SqlHelper.ExecuteSql(sql);
            dt = SqlHelper.ExecuteTable("select * from syscolumns where id=object_id(@tablename) and name = @fieldname", sp);
            if (dt.Rows.Count > 0) { SqlHelper.ExecuteSql("ALTER TABLE " + TableName + " DROP COLUMN [" + FieldName.Trim() + "]"); }
            return true;
            //SqlParameter[] cmdParams = new SqlParameter[] {
            //    new SqlParameter("@TableName", SqlDbType.NVarChar,50),
            //    new SqlParameter("@FieldName", SqlDbType.NVarChar,50)
            //};
            //cmdParams[0].Value = TableName;
            //cmdParams[1].Value = FieldName;
            //return SqlHelper.ExecuteProc("PR_DelTableField", cmdParams);
        }
        //-----------------------OTHER
        /// <summary>
        /// 根据模型ID,取到XML中的所有字段(除已禁用字段和系统字段)
        /// IsCopy:改为存是否禁用,-1:禁用
        /// </summary>
        public DataTable GetModelFieldListall(int ModelID)
        {
            string sql = "SELECT * FROM " + strTableName + " WHERE ModelID=" + ModelID + " AND  Sys_type=0 AND IsCopy>-1 ORDER BY OrderID";
            return SqlHelper.ExecuteTable(sql);
        }
        /// <summary>
        /// 取所有的字段
        /// </summary>
        public DataTable GetModelFieldListallTT(int ModelID)
        {
            return Sel();
        }
        public DataTable GetModelFieldList_all()
        {
            return Sel();
        }
        /// <summary>
        /// 获取指定模型的字段数据,只包含自定字段
        /// </summary>
        public DataTable GetModelFieldListnone(int ModelID)
        {
            string sql = "SELECT * FROM " + strTableName + " WHERE IsShow=0 AND Sys_type=0";
            return SqlHelper.ExecuteTable(sql);
        }
        /// <summary>
        /// 用于用户中心,输出模型字段列表
        /// </summary>
        public DataTable GetModelFieldList(int ModelID)
        {
            string sql = "SELECT * FROM " + strTableName + " WHERE ModelID=" + ModelID + " AND IsShow=1 AND Sys_type=0 AND IsCopy >-1 ";
            DataTable newtable = SqlHelper.ExecuteTable(sql);
            if (newtable == null || newtable.Rows.Count < 1) { return newtable; }
            try
            {
                DataColumn dc = new DataColumn("OrderIDs", typeof(int));
                bool b = true;
                string str = " | ";
                foreach (DataColumn column in newtable.Columns)
                {
                    str += column.ColumnName;
                    str += " | ";
                    if (column.ColumnName == dc.ColumnName)
                    {
                        b = false;
                        break;
                    }
                }
                if (b)
                {
                    newtable.Columns.Add("OrderIDs", typeof(int));
                }
            }
            catch (Exception ex)
            {
                throw new Exception("OrderIDs：" + ex.Message);
            }
            for (int i = 0; i < newtable.Rows.Count; i++)
            {
                newtable.Rows[i]["OrderIDs"] = DataConverter.CLng(newtable.Rows[i]["OrderID"].ToString());
            }
            newtable.DefaultView.Sort = "OrderIDs";
            newtable = newtable.DefaultView.ToTable();
            return newtable;
        }
        /// <summary>
        /// 返回所有自定义字段
        /// </summary>
        public DataTable GetModelFieldListAll(int ModelID)
        {
            string sql = "SELECT * FROM " + strTableName + " WHERE ModelID=" + ModelID + " AND Sys_type=0";
            DataTable newtable = SqlHelper.ExecuteTable(sql);
            if (newtable == null || newtable.Rows.Count < 1) { return newtable; }
            try
            {
                DataColumn dc = new DataColumn("OrderIDs", typeof(int));
                bool b = true;
                string str = " | ";
                foreach (DataColumn column in newtable.Columns)
                {
                    str += column.ColumnName;
                    str += " | ";
                    if (column.ColumnName == dc.ColumnName)
                    {
                        b = false;
                        break;
                    }
                }
                if (b)
                {
                    newtable.Columns.Add("OrderIDs", typeof(int));
                }
            }
            catch (Exception ex)
            {
                throw new Exception("OrderIDs：" + ex.Message);
            }
            for (int i = 0; i < newtable.Rows.Count; i++)
            {
                newtable.Rows[i]["OrderIDs"] = DataConverter.CLng(newtable.Rows[i]["OrderID"].ToString());
            }
            newtable.DefaultView.Sort = "OrderIDs";
            newtable = newtable.DefaultView.ToTable();
            return newtable;
        }
        /// <summary>
        /// 从数据库中获取指定模型的字段列表
        /// </summary>
        public DataSet GetModelFieldAllListXML(int ModelID)
        {
            string sql = "select * from " + strTableName + " where ModelID=" + ModelID + " ORDER BY OrderID";
            return SqlHelper.ExecuteDataSet(CommandType.Text, sql);
        }
        /// <summary>
        /// 返回可显示的字段(不包含系统字段)
        /// </summary>
        DataSet GetModelFieldLists(int ModelID)
        {
            string strSql = "select * from ZL_ModelField where ModelID=" + ModelID + " and IsShow=1 and fieldname not in(select fieldname from zl_modelfield where modelid=0) order by OrderID";
            return SqlHelper.ExecuteDataSet(CommandType.Text, strSql);
        }
        /// <summary>
        /// 仅用于黄页,支持标题搜索
        /// </summary>
        public DataTable SelAllPage(string title, int type, string flag, string order, int parentid = 0)
        {
            string where = " 1=1 ";
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("title", "%" + title + "%"), new SqlParameter("title2", "%" + title + "%"), new SqlParameter("ParentID", parentid) };
            if (!string.IsNullOrEmpty(title))
            {
                switch (type)
                {
                    case 0:
                        where = where + " and  Title like @title ";
                        break;
                    case 1:
                        where = where + " and  GeneralID=" + DataConverter.CLng(title);
                        break;
                    case 2:
                        where = where + " and  Inputer like @title2 ";
                        break;
                    default:
                        break;
                }
            }
            if (!string.IsNullOrEmpty(flag))
            {
                switch (flag)
                {
                    case "Audit":
                        where = where + " and Status=99";
                        break;
                    case "UnAudit":
                        where = where + " and Status=0";
                        break;
                    case "Elite":
                        where = where + " and EliteLevel=1";
                        break;
                }
            }
            if (parentid > 0)
                where += " AND NodeID=@ParentID";
            string sql = "SELECT A.*,B.TemplateName AS TempName FROM ZL_CommonModel A LEFT JOIN ZL_PageTemplate B ON A.NodeID=B.TemplateID WHERE " + where + " And TableName like 'ZL_Page_%' ORDER BY " + order;

            return SqlHelper.ExecuteTable(CommandType.Text, sql, sp);
        }
        /// <summary>
        /// 从指定表中获取数据
        /// </summary>
        public DataTable SelectTableName(string Tablename, string Where, SqlParameter[] sp = null)
        {
            SafeSC.CheckDataEx(Tablename);
            List<SqlParameter> splist = new List<SqlParameter>();
            if (sp != null) { splist.AddRange(sp); }
            return DBCenter.Sel(Tablename, Where, "", splist);
        }
        /// <summary>
        /// 获取前一个Order的字段模型
        /// </summary>
        public M_ModelField GetPreField(int ModelID, int CurrentID)
        {
            return SelModelByWhere("ModelID=" + ModelID + " AND OrderID<" + CurrentID, "OrderID DESC");
        }
        /// <summary>
        /// 获取下一个Order的字段模型
        /// </summary>
        public M_ModelField GetNextField(int ModelID, int CurrentID)
        {
            return SelModelByWhere("ModelID=" + ModelID + " AND OrderID>" + CurrentID);
        }
        public string GetFieldContent(string Content, int PlaceId, int TypeId)
        {

            string[] arr = Content.Split(new char[] { ',' });
            string re = "";
            if (arr.Length > PlaceId)
            {
                string[] sub = arr[PlaceId].Split(new char[] { '=' });
                if (sub.Length > TypeId)
                    re = sub[TypeId].ToString();
                else
                    re = "";
            }
            return re;
        }
        /// <summary>
        /// 互动显示回复
        /// </summary>
        [Obsolete]
        public string GetPubModelHtmlall(int ModelID, DataRow UserData)
        {
            StringBuilder builder = new StringBuilder();
            DataTable newtable = SelByModelID(ModelID);
            if (newtable.Rows.Count < 1) { return ""; }
            if (newtable.Columns.Contains("Sys_type"))
            { newtable.DefaultView.RowFilter = "Sys_type=0"; }
            DataTable dt = newtable.DefaultView.ToTable();
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (DataConverter.CLng(dt.Rows[i]["RestoreField"].ToString()) == 1)
                    {
                        builder.Append(GetUserShowStyle(dt.Rows[i]["FieldAlias"].ToString(), dt.Rows[i]["FieldName"].ToString(), DataConverter.CBool(dt.Rows[i]["IsNotNull"].ToString()), dt.Rows[i]["FieldType"].ToString(), dt.Rows[i]["Content"].ToString(), dt.Rows[i]["Description"].ToString(), ModelID, UserData));
                    }
                }
            }
            return builder.ToString();
        }
        /// <summary>
        /// 给用户模型的某个字段生成编辑代码
        /// </summary>
        /// <param name="Alias">字段别名</param>
        /// <param name="Name">字段名称</param>
        /// <param name="IsNotNull">是否必填</param>
        /// <param name="Type">字段类型</param>
        /// <param name="Content">字段设定内容</param>
        /// <param name="Description">字段描述</param>
        /// <param name="ModelID">模型ID</param>
        /// <param name="UserData">用户模型信息表的一行记录</param>
        /// <returns>编辑页面html代码</returns>
        [Obsolete]
        private string GetUserShowStyle(string Alias, string Name, bool IsNotNull, string Type, string Content, string Description, int ModelID, DataRow UserData)
        {
            B_UserModelShow bshow = new B_UserModelShow();
            return bshow.ShowStyleField(Alias, Name, IsNotNull, Type, Content, Description, ModelID, UserData);
        }
        [Obsolete]
        public string GetInputHtml(int ModelID, int NodeID)
        {
            DataTable ddt = SelByModelID(ModelID);
            ddt.DefaultView.RowFilter = "Sys_type=0";
            ddt = ddt.DefaultView.ToTable();
            DataTable dt = ddt.DefaultView.ToTable();
            StringBuilder builder = new StringBuilder();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    builder.Append(GetShowStyle(dt.Rows[i]["FieldAlias"].ToString(), dt.Rows[i]["FieldName"].ToString(), DataConverter.CBool(dt.Rows[i]["IsNotNull"].ToString()), dt.Rows[i]["FieldType"].ToString(), dt.Rows[i]["Content"].ToString(), dt.Rows[i]["Description"].ToString(), ModelID, NodeID));
                }
            }
            return builder.ToString();
        }
        /// <summary>
        /// 为模型的自定义字段生成Html输入界面代码,用于前后台添加内容
        /// </summary>
        [Obsolete]
        public string GetInputallHtml(int modelID, int nodeID)
        {
            DataTable dt = this.GetModelFieldListall(modelID);
            StringBuilder builder = new StringBuilder();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    builder.Append(GetShowStyle(dr["FieldAlias"].ToString(), dr["FieldName"].ToString(), DataConverter.CBool(dr["IsNotNull"].ToString()), dr["FieldType"].ToString(), dr["Content"].ToString(), dr["Description"].ToString(), modelID, nodeID));
                }
            }
            return builder.ToString();
        }
        [Obsolete]
        public string GetUpdateHtml(int ModelID, int NodeID, DataTable dt1)
        {
            DataTable dt = this.GetModelFieldList(ModelID);
            StringBuilder builder = new StringBuilder();
            if (dt1.Rows.Count > 0)
            {
                DataRow dr = dt1.Rows.Count > 0 ? dt1.Rows[0] : null;//避免保存空表单时不解析
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        builder.Append(GetShowStyle(dt.Rows[i]["FieldAlias"].ToString(), dt.Rows[i]["FieldName"].ToString(), DataConverter.CBool(dt.Rows[i]["IsNotNull"].ToString()), dt.Rows[i]["FieldType"].ToString(), dt.Rows[i]["Content"].ToString(), dt.Rows[i]["Description"].ToString(), ModelID, NodeID, dr));
                    }
                }
            }
            return builder.ToString();
        }
        /// <summary>
        /// 为模型的自定义字段生成Html输入界面代码,用于修改内容
        /// </summary>
        /// <param name="dt1">已有的内容值</param>
        /// <returns>解析后的Html</returns>
        [Obsolete]
        public string GetUpdateAllHtml(int ModelID, int NodeID, DataTable dt1)
        {
            StringBuilder builder = new StringBuilder();
            B_ShowField showBll = new B_ShowField();
            DataTable dt = GetModelFieldListall(ModelID);
            if (dt1.Rows.Count > 0 && dt.Rows.Count > 0)
            {
                DataRow dr = dt1.Rows[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    bool isChain = dt.Columns.Contains("IsChain") ? DataConvert.CBool(dt.Rows[i]["IsChain"].ToString()) : false;
                    FieldModel fieldMod = FieldModel.LoadFromDR(dt.Rows[i], NodeID);
                    fieldMod.ModelID = ModelID;
                    fieldMod.dr = dr;
                    builder.Append(showBll.ShowStyleField(fieldMod));
                    //builder.Append(showBll.ShowStyleField(dt.Rows[i]["FieldAlias"].ToString(), dt.Rows[i]["FieldName"].ToString(), DataConverter.CBool(dt.Rows[i]["IsNotNull"].ToString()), dt.Rows[i]["FieldType"].ToString(), dt.Rows[i]["Content"].ToString(), dt.Rows[i]["Description"].ToString(), ModelID, NodeID, dr, isChain));
                }
            }
            return builder.ToString();
        }
        public string GetShowStyle(string value, string Alias, string Name, bool IsNotNull, string Content, string Description, DataRow dr)
        {
            B_ShowField bshow = new B_ShowField();
            return bshow.GetLable(value, Alias, Name, IsNotNull, Content, Description, dr);
        }
        /// <summary>
        /// content不能为空
        /// </summary>
        public string GetShowStyle(string Alias, string Name, bool IsNotNull, string Type, string Content, string Description, int ModelID, int NodeID, DataRow dr, bool ischain = false)
        {
            B_ShowField bshow = new B_ShowField();
            return bshow.ShowStyleField(Alias, Name, IsNotNull, Type, Content, Description, ModelID, NodeID, dr, ischain);
        }
        /// <summary>
        /// 用户注册信息修改
        /// </summary>
        public string GetUserShowStyle(string Alias, string Name, bool IsNotNull, string Type, string Content, string Description, int ModelID, int NodeID, DataRow dr)
        {
            B_ShowField bshow = new B_ShowField();
            bshow.config.source = ModelConfig.SType.UserBase;
            return bshow.ShowStyleField(Alias, Name, IsNotNull, Type, Content, Description, ModelID, NodeID, dr);
        }
        public string GetShowStyle(string Alias, string Name, bool IsNotNull, string Type, string Content, string Description, int ModelID, int NodeID)
        {
            return new B_ShowField().ShowStyleField(Alias, Name, IsNotNull, Type, Content, Description, ModelID, NodeID, null);
        }
        /// <summary>
        /// 为模型的自定义字段生成Html输入界面代码
        /// </summary>
        public string GetInputHtmlUser(int ModelID, int NodeID)
        {
            DataTable dt = this.GetModelFieldListall(ModelID);
            StringBuilder builder = new StringBuilder();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    builder.Append(GetShowStyleUser(dt.Rows[i]["FieldID"].ToString(), dt.Rows[i]["FieldAlias"].ToString(), dt.Rows[i]["FieldName"].ToString(), DataConverter.CBool(dt.Rows[i]["IsNotNull"].ToString()), dt.Rows[i]["FieldType"].ToString(), dt.Rows[i]["Content"].ToString(), dt.Rows[i]["Description"].ToString(), ModelID, NodeID));
                }
            }
            return builder.ToString();
        }
        /// <summary>
        /// 为模型的自定义字段生成Html输入界面代码
        /// </summary>
        /// <param name="ModelID">模型ID</param>
        /// <param name="NodeID">节点ID</param>
        /// <param name="ismust">是否必填 1-真 0-假</param>
        [Obsolete]
        public string GetInputHtmlUser(int ModelID, int NodeID, int IsNotNull)
        {
            DataTable dt = this.GetModelFieldListall(ModelID);
            StringBuilder builder = new StringBuilder();
            if (dt != null && dt.Rows.Count > 0)
            {
                dt = dt.DefaultView.ToTable();

                if (IsNotNull == 1)
                {
                    DataRow[] dtrow = dt.Select("IsNotNull=true");

                    if (dtrow.Length > 0)
                    {
                        for (int i = 0; i < dtrow.Length; i++)
                        {
                            builder.Append(GetShowStyleUser(dtrow[i]["FieldID"].ToString(), dtrow[i]["FieldAlias"].ToString(), dtrow[i]["FieldName"].ToString(), DataConverter.CBool(dtrow[i]["IsNotNull"].ToString()), dtrow[i]["FieldType"].ToString(), dtrow[i]["Content"].ToString(), dtrow[i]["Description"].ToString(), ModelID, NodeID));
                        }
                    }
                }
                else
                {
                    DataRow[] dtrow = dt.Select("IsNotNull=false");
                    if (dtrow.Length > 0)
                    {
                        for (int i = 0; i < dtrow.Length; i++)
                        {
                            builder.Append(GetShowStyleUser(dtrow[i]["FieldID"].ToString(), dtrow[i]["FieldAlias"].ToString(), dtrow[i]["FieldName"].ToString(), DataConverter.CBool(dtrow[i]["IsNotNull"].ToString()), dtrow[i]["FieldType"].ToString(), dtrow[i]["Content"].ToString(), dtrow[i]["Description"].ToString(), ModelID, NodeID));
                        }
                    }
                }
            }
            return builder.ToString();
        }
        public string GetShowStyleUser(string FieldID, string Alias, string Name, bool IsNotNull, string Type, string Content, string Description, int ModelID, int NodeID, DataRow dr = null)
        {
            B_ContentField bshow = new B_ContentField();
            return bshow.ShowStyleField(FieldID, Alias, Name, IsNotNull, Type, Content, Description, ModelID, NodeID, dr);
        }
        #region 解析字段
        /// <summary>
        /// 显示隐藏自定义字段生成Html输入界面代码()
        /// </summary>
        [Obsolete]
        public string GetInputHtmlUsernone(int ModelID, int NodeID)
        {
            DataTable dt = this.GetModelFieldListall(ModelID);
            StringBuilder builder = new StringBuilder();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    builder.Append(GetShowStyleUser(dt.Rows[i]["FieldID"].ToString(), dt.Rows[i]["FieldAlias"].ToString(), dt.Rows[i]["FieldName"].ToString(), DataConverter.CBool(dt.Rows[i]["IsNotNull"].ToString()), dt.Rows[i]["FieldType"].ToString(), dt.Rows[i]["Content"].ToString(), dt.Rows[i]["Description"].ToString(), ModelID, NodeID));
                }
            }
            return builder.ToString();
        }
        /// <summary>
        /// 将模型字段,转化为html返回
        /// </summary>
        /// <param name="ModelID">模型ID</param>
        /// <param name="NodeID">节点ID</param>
        /// <param name="dtable">字段表</param>
        /// <returns>字段html</returns>
        [Obsolete]
        public string GetUpdateHtmlUser(int ModelID, int NodeID, DataTable dtable)
        {
            DataTable dt = this.GetModelFieldListall(ModelID);
            StringBuilder builder = new StringBuilder();
            if (dtable != null && dtable.Rows.Count > 0)
            {
                DataRow dr = dtable.Rows[0];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        builder.Append(GetShowStyleUser(dt.Rows[i]["FieldID"].ToString(), dt.Rows[i]["FieldAlias"].ToString(), dt.Rows[i]["FieldName"].ToString(), DataConverter.CBool(dt.Rows[i]["IsNotNull"].ToString()), dt.Rows[i]["FieldType"].ToString(), dt.Rows[i]["Content"].ToString(), dt.Rows[i]["Description"].ToString(), ModelID, NodeID, dr));
                    }
                }
            }
            return builder.ToString();
        }
        /// <summary>
        /// [main]用户中心--添加内容,注册等地方均调用
        /// </summary>
        public string InputallHtml(int modelID, int nodeID, ModelConfig config)
        {
            StringBuilder builder = new StringBuilder();
            B_ShowField showBll = new B_ShowField();
            showBll.config.source = config.Source;
            DataTable dt = new DataTable();
            switch (config.Source)
            {
                case ModelConfig.SType.UserRegister:
                    {
                        if (string.IsNullOrEmpty(config.Fields)) { return ""; }
                        string tbname = ModelConfig.GetTableName(config.Source);
                        string paramStr = "";
                        SqlParameter[] sp = StrHelper.GetStrSP(config.Fields, out paramStr);
                        dt = SqlHelper.ExecuteTable("SELECT *,ModelID=0,IsShow='True' FROM " + tbname + " WHERE FieldName IN (" + paramStr + ")", sp);
                    }
                    break;
                default:
                    {
                        dt = SelByModelID(modelID, false, false);
                        #region 字段过滤
                        if (!string.IsNullOrEmpty(config.Fields))
                        {
                            string fields = "";
                            foreach (string field in config.Fields.Split(','))
                            {
                                if (string.IsNullOrEmpty(field)) continue;
                                fields += "'" + field + "',";
                            }
                            fields = fields.TrimEnd(',');
                            dt.DefaultView.RowFilter = " FieldName IN(" + fields + ")";
                            dt = dt.DefaultView.ToTable();
                        }
                        #endregion
                    }
                    break;
            }
            //解析字段为Html
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                FieldModel fieldMod = FieldModel.LoadFromDR(dt.Rows[i], nodeID);
                if (config.Source != ModelConfig.SType.Admin && !fieldMod.IsShow)//true:false
                {
                    continue;
                }
                fieldMod.ModelID = modelID;
                fieldMod.dr = config.ValueDR;
                fieldMod.config.source = config.Source;
                fieldMod.config.mode = config.Mode;
                builder.Append(showBll.ShowStyleField(fieldMod));
                //builder.Append(GetShowStyle(dr["FieldAlias"].ToString(), dr["FieldName"].ToString(), DataConverter.CBool(dr["IsNotNull"].ToString()), dr["FieldType"].ToString(), dr["Content"].ToString(), dr["Description"].ToString(), modelID, nodeID));
            }
            return builder.ToString();
        }
        #endregion
        //-----------------------Img用处不大
        /// <summary>
        /// 从数据中获取所有img的url
        /// </summary>
        public List<string> GetImgUrl(string Value)
        {
            List<string> remoteImageList = new List<string>();
            string pattern = "(?<content><img.*?/>)";
            Regex regexImage = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            MatchCollection matchCollection = regexImage.Matches(Value);
            //循环所有img标签
            foreach (Match match in matchCollection)
            {
                string imgbq = match.Groups[0].Value;
                string patterns = "(src=)('|\")()(.*?)('|\")";
                Regex ImgUrl = new Regex(patterns, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                MatchCollection matchList = ImgUrl.Matches(imgbq);
                //循环img标签中url的值
                foreach (Match Mit in matchList)
                {
                    remoteImageList.Add(string.Format("{0}{1}", Mit.Groups[3].Value, Mit.Groups[4].Value));
                }
            }
            return remoteImageList;
        }
        /// <summary>
        /// 会产生graphics异常的PixelFormat
        /// </summary>
        private static PixelFormat[] indexedPixelFormats = {
                                                               PixelFormat.Undefined,
                                                               PixelFormat.DontCare,
                                                               PixelFormat.Format16bppArgb1555,
                                                               PixelFormat.Format1bppIndexed,
                                                               PixelFormat.Format4bppIndexed,
                                                               PixelFormat.Format8bppIndexed
                                                           };

        /// <summary>
        /// 判断图片的PixelFormat 是否在 引发异常的 PixelFormat 之中
        /// </summary>
        /// <param name="imgPixelFormat">原图片的PixelFormat</param>
        private static bool IsPixelFormatIndexed(PixelFormat imgPixelFormat)
        {
            foreach (PixelFormat pf in indexedPixelFormats)
            {
                if (pf.Equals(imgPixelFormat)) return true;
            }
            return false;
        }
        /// <summary>
        /// 下载远程图片保存到本地
        /// </summary>
        /// <param name="savedir">保存路径</param>
        /// <param name="imgpath">远程图片文件</param>
        /// <param name="NimgName">新图片名称</param>
        public string downRemoteImg(string savedir, string imgpath, string NimgName, string savePath, string imgUrl)
        {
            if (string.IsNullOrEmpty(imgpath))
            {
                return string.Empty;
            }
            else
            {
                string imgName = string.Empty;
                string imgExt = string.Empty;
                string saveFilePath = string.Empty;

                imgExt = imgpath.Substring(imgpath.LastIndexOf("."), imgpath.Length - imgpath.LastIndexOf(".")).ToLower();

                saveFilePath = savedir;
                if (!Directory.Exists(saveFilePath))
                    Directory.CreateDirectory(saveFilePath);

                //不是指定后缀的图片
                if (imgExt.Length > 4 || imgExt.Length < 3)
                {
                    WebRequest wreq = WebRequest.Create(imgpath);
                    wreq.Timeout = 10000;
                    HttpWebResponse wresp = (HttpWebResponse)wreq.GetResponse();
                    Stream s = wresp.GetResponseStream();
                    System.Drawing.Image img = System.Drawing.Image.FromStream(s);
                    img.Save(saveFilePath + NimgName + ".jpg", ImageFormat.Jpeg);
                    img.Dispose();
                    s.Dispose();
                    if (WaterModuleConfig.WaterConfig.IsUsed && (imgExt == ".jpg" || imgExt == ".png"))
                    {
                        //string Testfoldername = "";
                        WaterImages dd = new WaterImages();
                        if (WaterModuleConfig.WaterConfig.WaterClass == "1")
                        {
                            string imgurl = imgUrl;
                            int post = DataConverter.CLng(WaterModuleConfig.WaterConfig.lopostion);
                            imgurl = imgurl.Replace("/", @"\");
                            imgurl = imgurl.Replace(@"\/", @"\");
                            imgurl = imgurl.Replace(@"\\", @"\");
                        }
                        return savePath + "Water" + NimgName + ".jpg";
                    }
                    else
                    {
                        return savePath + NimgName + ".jpg";
                    }
                }
                //过滤服务图片
                if (imgpath.Contains("UploadFiles").ToString().ToLower() == "false" && imgpath.Substring(1, 1) != ":")
                {
                    WebRequest wreq = WebRequest.Create(imgpath);
                    wreq.Timeout = 10000;
                    HttpWebResponse wresp = (HttpWebResponse)wreq.GetResponse();
                    Stream s = wresp.GetResponseStream();
                    System.Drawing.Image img = System.Drawing.Image.FromStream(s);
                    //判断png异常处理
                    if (IsPixelFormatIndexed(img.PixelFormat))
                    {
                        Bitmap bmp = new Bitmap(img.Width, img.Height, PixelFormat.Format32bppArgb);
                        using (Graphics g = Graphics.FromImage(bmp))
                        {
                            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                            g.DrawImage(img, 0, 0);
                        }
                        imgName = NimgName + imgExt;
                        switch (imgExt.ToLower())
                        {
                            case ".png":
                                bmp.Save(saveFilePath + imgName, ImageFormat.Png);
                                break;
                            default:
                                bmp.Save(saveFilePath + NimgName + ".jpg", ImageFormat.Jpeg);
                                break;
                        }
                        bmp.Dispose();
                        s.Dispose();
                        if (WaterModuleConfig.WaterConfig.IsUsed && (imgExt == ".jpg" || imgExt == ".png"))
                        {
                            //string Testfoldername = "";

                            WaterImages dd = new WaterImages();
                            if (WaterModuleConfig.WaterConfig.WaterClass == "1")
                            {
                                string imgurl = imgUrl;
                                int post = DataConverter.CLng(WaterModuleConfig.WaterConfig.lopostion);
                                imgurl = imgurl.Replace("/", @"\");
                                imgurl = imgurl.Replace(@"\/", @"\");
                                imgurl = imgurl.Replace(@"\\", @"\");
                            }
                            else
                            {
                                string waterword = WaterModuleConfig.WaterConfig.WaterWord;
                                string ziti = WaterModuleConfig.WaterConfig.WaterWordType;
                            }
                            return savePath + "Water" + NimgName + imgExt;
                        }
                        else
                        {
                            return savePath + imgName;
                        }
                    }
                    else
                    {
                        imgName = NimgName + imgExt;
                        switch (imgExt.ToLower())
                        {
                            case ".gif":
                                img.Save(saveFilePath + imgName, ImageFormat.Gif);
                                break;
                            case ".jpg":
                                img.Save(saveFilePath + imgName, ImageFormat.Jpeg);
                                break;
                            case ".jpeg":
                                img.Save(saveFilePath + imgName, ImageFormat.Jpeg);
                                break;
                            case ".png":
                                img.Save(saveFilePath + imgName, ImageFormat.Png);
                                break;
                            case ".icon":
                                img.Save(saveFilePath + imgName, ImageFormat.Icon);
                                break;
                            case ".bmp":
                                img.Save(saveFilePath + imgName, ImageFormat.Bmp);
                                break;
                            default:
                                if (imgExt.ToLower().Contains("png"))
                                {
                                    img.Save(saveFilePath + NimgName + ".png", ImageFormat.Jpeg);
                                }
                                else if (imgExt.ToLower().Contains("jpg"))
                                {
                                    img.Save(saveFilePath + NimgName + ".jpg", ImageFormat.Jpeg);
                                }
                                break;
                        }
                        img.Dispose();
                        s.Dispose();
                        if (WaterModuleConfig.WaterConfig.IsUsed && (imgExt == ".jpg" || imgExt == ".png"))
                        {
                            //string Testfoldername = "";
                            WaterImages dd = new WaterImages();
                            if (WaterModuleConfig.WaterConfig.WaterClass == "1")
                            {
                                string imgurl = imgUrl;
                                int post = DataConverter.CLng(WaterModuleConfig.WaterConfig.lopostion);
                                imgurl = imgurl.Replace("/", @"\");
                                imgurl = imgurl.Replace(@"\/", @"\");
                                imgurl = imgurl.Replace(@"\\", @"\");
                            }
                            return savePath + "Water" + NimgName + imgExt;
                        }
                        else
                        {
                            return savePath + imgName;
                        }
                    }
                }
                else //编辑器上传的文件
                {
                    if (!Path.GetFileNameWithoutExtension(imgpath).ToLower().Contains("water"))  //表示没有加水印
                    {
                        if (WaterModuleConfig.WaterConfig.IsUsed && (imgExt == ".jpg" || imgExt == ".png" || imgExt == ".gif"))
                        {
                            //string Testfoldername = "";
                            WaterImages dd = new WaterImages();
                            if (WaterModuleConfig.WaterConfig.WaterClass == "1")
                            {
                                string imgurl = imgUrl;
                                int post = DataConverter.CLng(WaterModuleConfig.WaterConfig.lopostion);
                                imgurl = imgurl.Replace("/", @"\");
                                imgurl = imgurl.Replace(@"\/", @"\");
                                imgurl = imgurl.Replace(@"\\", @"\");
                            }
                            return "\\" + saveFilePath.Substring(saveFilePath.IndexOf("UploadFiles"), saveFilePath.Length - saveFilePath.IndexOf("UploadFiles")) + "Water" + NimgName + imgExt;
                        }
                        else
                        {
                            if (imgpath.Contains("\\UploadFiles\\images"))
                            {
                                File.Copy(imgpath, saveFilePath + NimgName + imgExt);
                                File.Delete(imgpath);
                                return "\\" + saveFilePath.Substring(saveFilePath.IndexOf("UploadFiles"), saveFilePath.Length - saveFilePath.IndexOf("UploadFiles")) + NimgName + imgExt;
                            }
                            else
                            {
                                return "\\" + imgpath.Substring(imgpath.IndexOf("UploadFiles"), imgpath.Length - imgpath.IndexOf("UploadFiles"));
                            }
                        }
                    }
                    else
                    {
                        return "\\" + imgpath.Substring(imgpath.IndexOf("UploadFiles"), imgpath.Length - imgpath.IndexOf("UploadFiles"));
                    }
                }

            }
        }
        public void SaveGz(string str, out string newImgname, out string Patha, string node)
        {
            string ty = str.Substring(str.LastIndexOf("/"), str.Length - str.LastIndexOf("/")).ToLower();
            string imgExt = str.Substring(str.LastIndexOf("."), str.Length - str.LastIndexOf(".")).ToLower();
            if (imgExt.Length > 5 || imgExt.Length < 3)
            {
                if (ty.Contains("png"))
                {
                    imgExt = ".png";
                }
                else
                {
                    imgExt = ".jpg";
                }
            }
            newImgname = "";
            int Gz = SiteConfig.SiteOption.FileN;
            if (Gz == 0)
            {

                Random Sj = new Random();
                newImgname = Sj.Next(999999).ToString() + "" + Sj.Next(999999).ToString() + "" + Sj.Next(999999).ToString();
            }
            else if (Gz == 1)
            {
                newImgname = DateTime.Now.ToString("yyyyMMddhhmmssff");
            }
            else if (Gz == 2)
            {
                newImgname = DateTime.Now.ToString("hhmmssff");
            }
            else if (Gz == 3)
            {
                newImgname = Path.GetFileNameWithoutExtension(str);
            }

            Patha = "";
            //保存路径
            int Rj = SiteConfig.SiteOption.FileRj;
            if (Rj == 1)
            {
                Patha = "/UploadFiles/" + imgExt.Substring(1, 3) + "/" + DateTime.Now.ToString("yyyyMM") + "/";
            }
            else if (Rj == 2)
            {
                Patha = "/UploadFiles/" + imgExt.Substring(1, 3) + "/" + DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString() + "/";
            }
            else if (Rj == 3)
            {
                Patha = "/UploadFiles/" + imgExt.Substring(1, 3) + "/" + node + "/" + DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString() + "/";
            }
            else if (Rj == 4)
            {
                Patha = "/UploadFiles/" + node + "/" + DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString() + "/";
            }
            else if (Rj == 0)
            {
                Patha = "/UploadFiles/" + node + "/" + imgExt.Substring(1, 3) + "/";
            }
        }
        /// <summary>
        /// 根据模型ID获取需要内链的字段，ischain为1：支持内链
        /// </summary>
        public string GetIsChain(int modelID, int ischain)
        {
            string result = "";
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("modelID", modelID), new SqlParameter("ischain", ischain) };
            string str = "Select * From " + strTableName + " Where ModelID=@modelID And IsChain=1";
            DataTable dt = SqlHelper.ExecuteTable(CommandType.Text, str, sp);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                result += dt.Rows[i]["FieldName"].ToString() + ",";
            }
            result = result.Trim(',');
            return result;
        }
        //----------Tools
        public string GetFieldType(string FieldType)
        {
            switch (FieldType.Trim())
            {
                case "TextType":
                    return "单行文本";
                case "OptionType":
                    return "单选项";
                case "ListBoxType":
                    return "多选项";
                case "GradeOptionType":
                    return "多级选项";
                case "DateType":
                    return "日期时间";
                case "SuperLinkType":
                    return "超链接";
                case "MultipleHtmlType":
                    return "多行文本(支持Html)";
                case "MultipleTextType":
                    return "多行文本(不支持Html)";
                case "SmallFileType":
                    return "文件";
                case "PullFileType":
                    return "下拉文件";
                case "FileType":
                    return "多文件";
                case "PicType":
                    return "图片";
                case "SqlType":
                    return "图片入库";
                case "SqlFile":
                    return "文件入库";
                case "MoneyType":
                    return "货币";
                case "MoneyType2":
                    return "货币类型";
                case "BoolType":
                    return "是/否";
                case "NumType":
                    return "数字";
                case "MultiPicType":
                    return "多图片";
                case "ColorType":
                    return "颜色代码";
                case "DoubleDateType":
                    return "双时间类型";
                case "Upload":
                    return "在线浏览";
                case "MobileSMS":
                    return "手机短信";
                case "MapType":
                    return "地图";
                case "Charts":
                    return "报表";
                case "SwfFileUpload":
                    return "智能多文件上传";
                case "RemoteFile":
                    return "远程文件";
                case "TableField":
                    return "库选字段";
                case "Random":
                    return "随机数";
                case "Images":
                    return "组图";
                case "CameraType":
                    return "拍照字段";
                case "OperatingType":
                    return "运行平台";
                case "autothumb":
                    return "压图传入";
                case "api_qq_mvs":
                    return "微视频";
                default:
                    return "未知类型:" + FieldType;
            }
        }
        /// <summary>
        /// 传入控件类型,返回数据表字段类型
        /// </summary>
        /// <param name="fieldType"></param>
        /// <returns></returns>
        private string GetTableFieldType(M_ModelField fieldMod)
        {
            string fieldType = "";
            switch (fieldMod.FieldType)
            {
                //单行文本
                case "TextType":
                    fieldType = "nvarchar";
                    break;
                //多行文本(不支持Html)
                case "MultipleTextType":
                    fieldType = "ntext";
                    break;
                //多行文本(支持Html)
                case "MultipleHtmlType":
                    fieldType = "ntext";
                    break;
                //单选项
                case "OptionType":
                    fieldType = "nvarchar";
                    break;
                //多选项
                case "ListBoxType":
                    fieldType = "ntext";
                    break;
                //数字
                case "NumType":
                    //int numstyle = 1;
                    //if (Content.IndexOf(".") > -1)
                    //{
                    //    string[] contentarr = Content.Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);
                    //    for (int i = 0; i < contentarr.Length; i++)
                    //    {
                    //        if (contentarr[i].IndexOf("NumberType=") > -1)
                    //        {
                    //            string[] iiarr = contentarr[i].Split(new string[] { "NumberType=" }, StringSplitOptions.None);
                    //            numstyle = DataConverter.CLng(iiarr[1]);
                    //        }
                    //    }
                    //}
                    //else
                    //{
                    //    if (Content.IndexOf("NumberType=") > -1)
                    //    {
                    //        string[] iiarr = Content.Split(new string[] { "NumberType=" }, StringSplitOptions.None);
                    //        numstyle = DataConverter.CLng(iiarr[1]);
                    //    }
                    //}
                    fieldType = "float";
                    break;
                //日期时间
                case "DateType":
                    fieldType = "datetime";
                    break;
                //图片
                case "PicType":
                    fieldType = "nvarchar";
                    break;
                //图片入库
                case "SqlType":
                    fieldType = "ntext";
                    //AddFieldCopy(tablename, "FIT_" + tablename, "TextType", "", "");
                    break;
                //文件入库
                case "SqlFile":
                    fieldType = "ntext";
                    //AddFieldCopy(tablename, "FIT_" + tablename, "TextType", "", "");
                    break;
                //多图片
                case "MultiPicType":
                    fieldType = "ntext";
                    //bool ChkThumb = DataConverter.CBool(strArray[0].Split(new char[] { '=' })[1]);
                    //string ThumbField = strArray[1].Split(new char[] { '=' })[1];
                    //if (ChkThumb && !string.IsNullOrEmpty(ThumbField))
                    //{
                    //    AddFieldCopy(tablename, ThumbField, "TextType", "", "");
                    //}
                    break;
                //文件
                case "SmallFileType":
                    fieldType = "nvarchar";
                    break;
                //下拉文件
                case "PullFileType":
                    fieldType = "nvarchar";
                    break;
                //多文件
                case "FileType":
                case "SwfFileUpload":
                    fieldType = "ntext";
                    //bool ChkFileSize = DataConverter.CBool(strArray[0].Split(new char[] { '=' })[1]);
                    //string FileSizeField = strArray[1].Split(new char[] { '=' })[1];
                    //if (ChkFileSize && !string.IsNullOrEmpty(FileSizeField))
                    //{
                    //    AddFieldCopy(tablename, FileSizeField, "TextType", "", "");
                    //}
                    break;
                //运行平台
                case "OperatingType":
                    fieldType = "nvarchar";
                    break;
                //超链接
                case "SuperLinkType":
                    fieldType = "nvarchar";
                    break;
                case "GradeOptionType":
                    fieldType = "nvarchar";
                    break;
                //颜色字段
                case "ColorType":
                    fieldType = "nvarchar";
                    break;
                //货币字段
                case "MoneyType":
                case "MoneyType2": //货币字段
                    fieldType = "money";
                    break;
                case "CameraType"://拍照
                case "Upload":
                case "TableField":
                case "Random":
                case "autothumb":
                case "api_qq_mvs":
                    fieldType = "nvarchar";
                    break;
                case "DoubleDateType"://双时间字段
                case "MobileSMS":     //手机短信
                case "Images"://组图
                case "MapType":
                    fieldType = "ntext";
                    break;
                default:
                    break;
            }
            return fieldType;
        }
        /// <summary>
        /// 指定模型是否存在该字段
        /// </summary>
        public bool IsExists(int ModelID, string fieldname)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("FieldName", fieldname) };
            string sql = "SELECT TOP 1 FieldID FROM " + strTableName + " WHERE ModelID=" + ModelID + " AND FieldName=@FieldName";
            return SqlHelper.ExecuteTable(sql, sp).Rows.Count > 0;
        }
        private void ConverPath(DataTable dt)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["FileUrl"] = function.VToP(dt.Rows[i]["FileUrl"].ToString());
            }
        }
        public int GetMaxOrder(int ModelID)
        {
            string sql = "SELECT MAX(OrderId) FROM " + strTableName + " WHERE ModelId=" + ModelID;
            DataTable dt = SqlHelper.ExecuteTable(sql);
            if (dt.Rows.Count < 1) { return 0; }
            return DataConvert.CLng(dt.Rows[0][0]);
        }
        public int GetMinOrder(int ModelID)
        {
            string sql = "SELECT MIN(OrderId) FROM " + strTableName + " WHERE ModelId=" + ModelID;
            DataTable dt = SqlHelper.ExecuteTable(sql);
            if (dt.Rows.Count < 1) { return 0; }
            return DataConvert.CLng(dt.Rows[0][0]);
        }

        /// <summary>
        /// 复制系统字段到模型中,仅给B_Model调用
        /// </summary>
        public bool AddFroms(int ID)
        {
            string sql = "SELECT * FROM " + strTableName + " WHERE modelid=0 ORDER BY orderid";
            DataTable dt = SqlHelper.ExecuteTable(sql);
            foreach (DataRow dr in dt.Rows)
            {
                M_ModelField model = new M_ModelField();
                model = model.GetModelFromReader(dr);
                model.ModelID = ID;
                InsertID(model);
            }
            return true;
            //string strSql = "PR_ModelField_AddFrom";
            //SqlParameter[] cmdParams = new SqlParameter[] { new SqlParameter("@modelid", SqlDbType.Int, 4) };
            //cmdParams[0].Value = ID;
            //return SqlHelper.ExecuteProc(strSql, cmdParams);
        }
    }
    //解析配置层-->字段解析配置层
    public class ModelConfig
    {
        public ModelConfig()
        {
            Source = SType.Admin;
            Mode = SMode.Edit;
        }
        /// <summary>
        /// 解析请求的来源,会做一些特别处理(例如从不同的地方读模型数据)
        /// </summary>
        public enum SType { Admin = 0, UserContent, UserBase, UserRegister, OAForm };
        /// <summary>
        /// 模式:编辑模式,预览模式
        /// </summary>
        public enum SMode { Edit = 0, PreView };
        public SMode Mode { get; set; }
        public SType Source { get; set; }
        /// <summary>
        /// 需要解析的字段,主用于注册页面
        /// </summary>
        public string Fields { get; set; }
        /// <summary>
        /// 存储所有字段的值,用于修改字段
        /// </summary>
        public DataTable ValueDT { set { if (value != null && value.Rows.Count > 0) { _valueDR = value.Rows[0]; } } }
        private DataRow _valueDR = null;
        public DataRow ValueDR
        {
            get
            {
                return _valueDR;
            }
            set { _valueDR = value; }
        }
        /// <summary>
        /// 根据不同的类型,返回对应的表名
        /// </summary>
        public static string GetTableName(SType type)
        {
            switch (type)
            {
                case SType.UserBase:
                case SType.UserRegister:
                    return "ZL_UserBaseField";
                default:
                    return "ZL_ModelField";
            }
        }
    }
}