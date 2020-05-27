using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ZoomLa.SQLDAL
{
    public class Sql
    {
        /// <summary>
        /// 根据表名查询所有记录
        /// </summary>
        /// <param name="strTableName">表名</param>
        public static DataTable Sel(string strTableName)
        {
            string strSql = "SELECT * FROM " + strTableName;
            return SqlHelper.ExecuteTable(CommandType.Text, strSql);
        }
        public static DataTable Sels(string strTableName)
        {
            string strSql = "SELECT max(ID) as ID  FROM " + strTableName;
            return SqlHelper.ExecuteTable(CommandType.Text, strSql);
        }
        /// <summary>
        /// 根据表名、ID查询一条记录+20
        /// </summary>
        /// <param name="strTableName">表名</param>
        /// <param name="ID">标识ID</param>
        /// <returns>返回一条记录</returns>
        public static DataTable Sel(string strTableName, string PK, int ID)
        {
            string strSql = "SELECT * FROM " + strTableName + " WHERE [" + PK + "]=" + ID;
            return SqlHelper.ExecuteTable(CommandType.Text, strSql);
        }
        /// <summary>
        /// sel...order by
        /// </summary>
        public static DataTable Sel(string strTableName, string strWhere, string strOrderby)
        {
            string strSql = "SELECT * FROM " + strTableName;
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql += " WHERE " + strWhere;
            }
            if (!string.IsNullOrEmpty(strOrderby))
            {
                strSql += " ORDER BY " + strOrderby;
            }
            return SqlHelper.ExecuteTable(CommandType.Text, strSql);
        }
        /// <summary>
        /// 防注入sel...order by
        /// </summary>
        public static DataTable Sel(string strTableName, string strWhere, string strOrderby, SqlParameter[] sp)
        {
            string strSql = "SELECT * FROM " + strTableName;
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql += " WHERE " + strWhere;
            }
            if (!string.IsNullOrEmpty(strOrderby))
            {
                strSql += " ORDER BY " + strOrderby;
            }
            return SqlHelper.ExecuteTable(CommandType.Text, strSql, sp);
        }
        public static DataTable Select(string strTableName, SqlParameter[] sp)
        {
            string strsql = "SELECT * FROM " + strTableName; 
            return SqlHelper.ExecuteTable(CommandType.Text, strsql, sp);
        }
        public static DataTable Select(string strTableName, string strWhere, string result)
        {
            string sqlStr = "SELECT ";
            if (result != "")
            {
                sqlStr += result;
            }
            else
            {
                sqlStr += "*";
            }
            sqlStr += " FROM " + strTableName;
            if (strWhere != "")
            {
                sqlStr += " WHERE " + strWhere;
            }
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr);
        }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="strTableName">表名</param>
        /// <param name="PK">标识列</param>
        /// <param name="strWhere">查询条件</param>
        /// <param name="strOrderby">排序方式</param>
        /// <param name="pageNum">页码</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns>查询结果</returns>
        public static DataTable SelPage(string strTableName, string PK, string strWhere, string strOrderby, int pageNum, int pageSize)
        {
            int oldSize = (pageNum - 1) * pageSize;
            oldSize=oldSize < 0 ? 0 : oldSize;

            if (string.IsNullOrEmpty(strWhere))
            {
                strWhere = "1=1";
            }
            if (!string.IsNullOrEmpty(strOrderby))
            {
                strOrderby = " ORDER BY " + strOrderby;
            }
            string strSql = "SELECT TOP " + pageSize + " * FROM [" + strTableName + "] WHERE " + strWhere + " AND [" + PK + "] NOT IN(SELECT TOP " + oldSize + " [" + PK + "] FROM [" + strTableName + "] WHERE " + strWhere + strOrderby + ")" + strOrderby; 
            return SqlHelper.ExecuteTable(CommandType.Text, strSql);
        }
        public static SqlDataReader SelReturnReader(string strTableName, string PK, int ID)
        {
            string strSql = "";
            try
            {
                strSql = "SELECT * FROM " + strTableName + " WHERE [" + PK + "]=" + ID;
                return SqlHelper.ExecuteReader(CommandType.Text, strSql);
            }
            catch(Exception ex) { throw new Exception(ex.Message+":"+strSql); }
        }
        public static SqlDataReader SelReturnReader(string strTableName, string strWhere, SqlParameter[] sp = null)
        {
            string strSql = "";
            try
            {
                strSql = "SELECT Top 1 * FROM " + strTableName + " " + strWhere;
                return SqlHelper.ExecuteReader(CommandType.Text, strSql, sp);
            }
            catch (Exception ex) { throw new Exception(ex.Message + ":" + strSql); }
        }
        /// <summary>
        /// 根据表名、ID删除一条记录
        /// </summary>
        /// <param name="strTableName">表名</param>
        /// <param name="ID">标识</param>
        /// <returns>返回sql语句是否成功执行的状态</returns>
        public static bool Del(string strTableName, int ID)
        {
            string strSql = "DELETE FROM " + strTableName + " WHERE [ID]=" + ID;
            return SqlHelper.ExecuteSql(strSql);
        }
        /// <summary>
        /// 自定义删除操作
        /// </summary>
        public static bool Del(string strTableName, string strWhere)
        {
            string strSql = "DELETE FROM " + strTableName + " WHERE " + strWhere;
            return SqlHelper.ExecuteSql(strSql);
        }
        public static bool Del(string strTableName, string strWhere, SqlParameter[] sp)
        {
            string strSql = "DELETE FROM " + strTableName + " WHERE " + strWhere;
            return SqlHelper.ExecuteSql(strSql,sp);
        }
        /// <summary>
        /// 自定义删除操作
        /// </summary>
        public static int Delint(string strTableName, string strWhere)
        {
            string strSql = "DELETE FROM " + strTableName + " WHERE " + strWhere;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSql);
        }
        /// <summary>
        /// 自定义删除操作
        /// </summary>
        public static int DelLabel(string strTableName, string strWhere, SqlParameter[] sp)
        {
            string strSql = "DELETE FROM " + strTableName + " WHERE " + strWhere;
            return SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.Text, strSql, sp));
        }
        /// <summary>
        /// [has]批量删除
        /// </summary>
        public static bool Delall(string strTableName, string PK, string ID)
        {
            string sqlStr = "DELETE FROM " + strTableName + " WHERE " + PK + " in(" + ID + ")";
            return SqlHelper.ExecuteSql(sqlStr, null);
        }

        /// <summary>
        /// 按条件更新
        /// </summary>
        /// <param name="strTableName">表名</param>
        /// <param name="strSet">赋值</param>
        /// <param name="strWhere">查询条件</param>
        public static bool Update(string strTableName, string strSet, string strWhere, SqlParameter[] sp)
        {
            string strSql = "UPDATE " + strTableName + " SET " + strSet;
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql += " WHERE " + strWhere;
            }
            return SqlHelper.ExecuteSql(strSql, sp);
        }
        /// <summary>
        /// [has little]按条件更新
        /// </summary>
        /// <param name="strTableName">表名</param>
        /// <param name="strSet">赋值</param>
        /// <param name="strWhere">查询条件</param>
        public static bool UpBool(string strTableName, string strSet, string strWhere)
        {
            string strSql = "UPDATE " + strTableName + " SET " + strSet;
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql += " WHERE " + strWhere;
            }
            return SqlHelper.ExecuteSql(strSql);
        }
        /// <summary>
        /// 按条件更新
        /// </summary>
        /// <param name="strTableName">表名</param>
        /// <param name="strSet">赋值</param>
        /// <param name="strWhere">查询条件</param>
        public static int UpInt(string strTableName, string strSet, string strWhere)
        {
            string strSql = "UPDATE " + strTableName + " SET " + strSet;
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql += " WHERE " + strWhere;
            }
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSql);
        }
        /// <summary>
        /// 按条件更新
        /// </summary>
        /// <param name="strTableName">表名</param>
        /// <param name="strSet">赋值</param>
        /// <param name="strWhere">查询条件</param>
        public static int UpLabel(string strTableName, string strSet, string strWhere, SqlParameter[] sp)
        {
            string strSql = "UPDATE " + strTableName + " SET " + strSet;
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql += " WHERE " + strWhere;
            } 
            return SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.Text, strSql, sp)); 
        }
        /// <summary>
        /// 根据标识ID更新
        /// </summary>
        /// <param name="strTableName">表名</param>
        /// <param name="PK">标识列</param>
        /// <param name="ID">标识列的值</param>
        /// <param name="strSet">赋值</param>
        public static bool UpdateByID(string strTableName, string PK, int ID, string strSet, SqlParameter[] sp)
        {
            string strSql = "UPDATE " + strTableName + " SET " + strSet + " WHERE " + PK + "=" + ID;
            return SqlHelper.ExecuteSql(strSql, sp);
        }
        public static bool UpdateByIDs(string strTableName, string PK, string IDs, string strSet, SqlParameter[] sp)
        {
            return UpdateByID(strTableName, PK, Convert.ToInt32(IDs), strSet, sp);
        }
        /// <summary>
        /// 返回值是命令影响的行数,非ID
        /// </summary>
        public static int insert(string tableName, SqlParameter[] mf, string paras, string filed)
        {
            string sqlStr = "INSERT INTO " + tableName + "(" + filed + ")" + " VALUES(" + paras + ")";
             SqlParameter[] cmdParams = mf;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, sqlStr, cmdParams);
        }
        public static int insertID(string tableName, SqlParameter[] mf, string paras, string filed)
        {
            string sqlStr = "INSERT INTO " + tableName + "(" + filed + ")" + " VALUES(" + paras + ");SELECT @@identity;";
            try
            {
                return Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.Text, sqlStr, mf));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ":" + sqlStr);
            }
        }
        /// <summary>
        /// 添加,允许插入主键的值
        /// </summary>
        /// <returns></returns>
        public static int InsertKey(string tableName, SqlParameter[] mf, string paras, string filed) 
        {
            string sqlStr = "SET IDENTITY_INSERT [" + tableName + "] ON insert into " + tableName + "(" + filed + ")" + " values(" + paras + ") SET IDENTITY_INSERT [" + tableName + "] OFF;select @@identity";
            return Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.Text, sqlStr, mf)); 
        }
        /// <summary>
        /// 执行一段Sql语句
        /// </summary>
        public static bool insert(string sqlStr,SqlParameter[] mf)
        {
            return SqlHelper.ExecuteSql(sqlStr, mf);
        }
        /// <summary>
        /// disuse
        /// </summary>
        public static DataTable Fonddateno(string strTableName, string strWhere)
        {
            return null;
        }
        public static int getnum(string strTableName)
        {
            string strSql = "select count(*) from " + strTableName;
            return Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.Text, strSql, null));
        }
        public static bool IsExist(string strTableName, string strWhere)
        {
            string strSql = "select count(*) from " + strTableName;
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql += " WHERE " + strWhere;
            }
            return SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.Text, strSql)) > 0; 
        }
        public static bool IsExist(string strTableName, string strWhere,SqlParameter[] sp)
        {
            string strSql = "select count(*) from " + strTableName;
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql += " WHERE " + strWhere;
            }
            return SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.Text, strSql,sp)) > 0;
        }
        public static int IsExistInt(string strTableName, string strWhere)
        {
            string strSql = "select count(*) from " + strTableName;
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql += " WHERE " + strWhere;
            }
            return SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.Text, strSql, null)); 
        }
        public static string GetFieldValue(DataRow dr)
        {
            string FieldType = dr["FieldType"].ToString();
            string result = "";
            switch (FieldType)
            {
                case "TextType":
                    result = "" + dr["FieldValue"].ToString() + "";
                    break;
                case "OptionType":
                    result = "" + dr["FieldValue"].ToString() + "";
                    break;
                case "ListBoxType":
                    result = "" + dr["FieldValue"].ToString() + "";
                    break;
                case "DateType":
                    result = "" + dr["FieldValue"].ToString() + "";
                    break;
                case "MultipleHtmlType":
                    result = "" + dr["FieldValue"].ToString() + "";
                    break;
                case "MultipleTextType":
                    result = "" + dr["FieldValue"].ToString() + "";
                    break;
                case "FileType":
                    result = "" + dr["FieldValue"].ToString() + "";
                    break;
                case "PicType":
                    result = "" + dr["FieldValue"].ToString() + "";
                    break;
                case "FileSize":
                    result = "" + dr["FieldValue"].ToString() + "";
                    break;
                case "ThumbField":
                    result = "" + dr["FieldValue"].ToString() + "";
                    break;
                case "MultiPicType":
                    result = "" + dr["FieldValue"].ToString() + "";
                    break;
                case "OperatingType":
                    result = "" + dr["FieldValue"].ToString() + "";
                    break;
                case "SmallFileType":
                    result = "" + dr["FieldValue"].ToString() + "";
                    break;
                case "SuperLinkType":
                    result = "" + dr["FieldValue"].ToString() + "";
                    break;
                case "MoneyType":
                    result = dr["FieldValue"].ToString();
                    break;
                case "BoolType":
                    result = dr["FieldValue"].ToString();
                    break;
                case "NumType":
                    if (dr["FieldValue"].ToString() == "")
                    {
                        result = "";
                    }
                    else
                    {
                        result = dr["FieldValue"].ToString();
                    }
                    break;
                case "GradeOptionType":
                    result = "" + dr["FieldValue"].ToString() + "";
                    break;
                default:
                    result = "" + dr["FieldValue"].ToString() + "";
                    break;
            }
            return result;
        }
        public static SqlParameter[] ContentPara(DataTable DTContent)
        {
            SqlParameter[] sp = new SqlParameter[DTContent.Rows.Count];
            for (int i = 0; i < DTContent.Rows.Count; i++)
            {
                sp[i] = GetPara(DTContent.Rows[i]);
            }
            return sp;
        }
        private static SqlParameter GetPara(DataRow dr)
        {
            string FieldType = dr["FieldType"].ToString();
            SqlParameter result = new SqlParameter();
            switch (FieldType)
            {
                case "TextType":
                    result = new SqlParameter(dr["FieldName"].ToString(), SqlDbType.NVarChar, 255);
                    result.Value = dr["FieldValue"].ToString();
                    break;
                case "OptionType":
                    result = new SqlParameter(dr["FieldName"].ToString(), SqlDbType.NVarChar, 255);
                    result.Value = dr["FieldValue"].ToString();
                    break;
                case "GradeOptionType":
                    result = new SqlParameter(dr["FieldName"].ToString(), SqlDbType.NVarChar, 255);
                    result.Value = dr["FieldValue"].ToString();
                    break;
                case "ListBoxType":
                    result = new SqlParameter(dr["FieldName"].ToString(), SqlDbType.NText);
                    result.Value = dr["FieldValue"].ToString();
                    break;
                case "DateType":
                    result = new SqlParameter(dr["FieldName"].ToString(), SqlDbType.DateTime);
                    result.Value = DataConvert.CDate(dr["FieldValue"].ToString());
                    break;
                case "MultipleHtmlType":
                    result = new SqlParameter(dr["FieldName"].ToString(), SqlDbType.NText);
                    result.Value = dr["FieldValue"].ToString();
                    break;
                case "MultipleTextType":
                    result = new SqlParameter(dr["FieldName"].ToString(), SqlDbType.NText);
                    result.Value = dr["FieldValue"].ToString();
                    break;
                case "FileType":
                    result = new SqlParameter(dr["FieldName"].ToString(), SqlDbType.NText);
                    result.Value = dr["FieldValue"].ToString();
                    break;
                case "PicType":
                    result = new SqlParameter(dr["FieldName"].ToString(), SqlDbType.NVarChar, 255);
                    result.Value = dr["FieldValue"].ToString();
                    break;
                case "FileSize":
                    result = new SqlParameter(dr["FieldName"].ToString(), SqlDbType.NVarChar, 255);
                    result.Value = dr["FieldValue"].ToString();
                    break;
                case "ThumbField":
                    result = new SqlParameter(dr["FieldName"].ToString(), SqlDbType.NVarChar, 255);
                    result.Value = dr["FieldValue"].ToString();
                    break;
                case "MultiPicType":
                    result = new SqlParameter(dr["FieldName"].ToString(), SqlDbType.NText);
                    result.Value = dr["FieldValue"].ToString();
                    break;
                case "OperatingType":
                    result = new SqlParameter(dr["FieldName"].ToString(), SqlDbType.NVarChar, 255);
                    result.Value = dr["FieldValue"].ToString();
                    break;
                case "SuperLinkType":
                    result = new SqlParameter(dr["FieldName"].ToString(), SqlDbType.NVarChar, 255);
                    result.Value = dr["FieldValue"].ToString();
                    break;
                case "BoolType":
                    result = new SqlParameter(dr["FieldName"].ToString(), SqlDbType.Bit, 1);
                    result.Value = dr["FieldValue"].ToString();
                    break;
                case "int":
                    result = new SqlParameter(dr["FieldName"].ToString(), SqlDbType.Int);
                    result.Value = DataConvert.CLng(dr["FieldValue"].ToString());
                    break;
                case "NumType":
                    result = new SqlParameter(dr["FieldName"].ToString(), SqlDbType.Float);
                    result.Value = DataConvert.CFloat(dr["FieldValue"].ToString());
                    break;
                case "float":
                    result = new SqlParameter(dr["FieldName"].ToString(), SqlDbType.Float);
                    result.Value = DataConvert.CFloat(dr["FieldValue"].ToString());
                    break;
                case "money":
                    result = new SqlParameter(dr["FieldName"].ToString(), SqlDbType.Money);
                    result.Value = DataConvert.CFloat(dr["FieldValue"].ToString());
                    break;
                default:
                    result = new SqlParameter(dr["FieldName"].ToString(), SqlDbType.NVarChar, 255);
                    result.Value = dr["FieldValue"].ToString();
                    break;
            }
            return result;
        }
        /*无存储过程*/
        public static bool ExeSql(string sqlstr)
        {
            try
            {
                string strSql = "PR_TSQL";
                SqlParameter[] parameter = new SqlParameter[] {       
               new SqlParameter("@tsql", SqlDbType.VarChar, 1000),             
            };
                parameter[0].Value = sqlstr;
                return SqlHelper.ExecuteProc(strSql, parameter);
            }
            catch (Exception ex) { throw new Exception(ex.Message + ":" + sqlstr); }
        }
        /// <summary>
        /// [has little]
        /// </summary>
        /// <param name="DTContent"></param>
        /// <returns></returns>
        public static string UpdateSql(DataTable DTContent)
        {
            string strSql = "";
            foreach (DataRow dr in DTContent.Rows)
            {
                if (string.IsNullOrEmpty(strSql))
                    strSql = dr["FieldName"].ToString() + "=@" + dr["FieldName"].ToString();
                else
                    strSql = strSql + "," + dr["FieldName"].ToString() + "=@" + dr["FieldName"].ToString();
            }
            return strSql;
        }
        public static string InsertSql(DataTable DTContent)
        {
            string strSql = "";
            string strValue = "";
            foreach (DataRow dr in DTContent.Rows)
            {
                if (string.IsNullOrEmpty(strSql))
                    strSql = " (" + dr["FieldName"].ToString();
                else
                    strSql = strSql + "," + dr["FieldName"].ToString();
                if (string.IsNullOrEmpty(strValue))
                    strValue = " values(@" + dr["FieldName"].ToString();
                else
                    strValue = strValue + ",@" + dr["FieldName"].ToString();
            }
            return strSql + ")" + strValue + ")";
        }
        //全局搜索
        public static DataTable SelectAll(string strs)
        {
            string sqlStr = "declare @str varchar(100) set @str='" +  strs + "' declare @s varchar(8000) declare tb cursor local for select s='if exists(select 1 from ['+b.name+'] where ['+a.name+'] like ''%'+@str+'%'') print '' ['+b.name+'].['+a.name+']''' from syscolumns a join sysobjects b on a.id=b.id where b.xtype='U' and a.status>=0 and a.xusertype in(175,239,231,167) open tb fetch next from tb into @s while @@fetch_status=0 begin exec(@s) fetch next from tb into @s end close tb deallocate tb";
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr); 
        }
    }
}