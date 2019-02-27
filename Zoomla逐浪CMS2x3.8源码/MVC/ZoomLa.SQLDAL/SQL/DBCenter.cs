using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ZoomLa.Model;
using ZoomLa.Safe;
using ZoomLa.SQLDAL.SQL;
using SqlCmd = ZoomLa.SQLDAL.SQL.SqlModel.SqlCmd;
/*
 * 1,将模型转化为SqlModel需要的参数
 * 2,方便BLL层调用,减少代码量
 * 3,用于多数据库的汇聚层
 */
namespace ZoomLa.SQLDAL
{
    public static class DBCenter
    {
        public static SqlBase DB = null;
        static DBCenter()
        {
            //根据类型,初始化不同的辅助类,和读取连接字符串
            string dbtype = ConfigurationManager.AppSettings["DBType"];
            if (string.IsNullOrEmpty(dbtype)) { dbtype = "mssql"; }
            DB = SqlBase.CreateHelper(dbtype);
            switch (dbtype)//为多数据库读取
            {
                case "oracle":
                    DB.ConnectionString = ConfigurationManager.ConnectionStrings["OraConnection String"].ConnectionString;
                    break;
                default:
                    DB.ConnectionString = SafeC.Cert_Decry(ConfigurationManager.ConnectionStrings["Connection String"].ConnectionString);
                    break;
            }
        }
        #region 兼容区
        /// <summary>
        /// 依据ID和主键删除
        /// </summary>
        public static bool Del(string tbname, string pk, int id)
        {
            SqlModel model = new SqlModel() { cmd = SqlCmd.Delete, tbName = tbname, };
            model.where = "" + pk + "=" + id;
            DB.ExecuteNonQuery(model);
            return true;
        }
        /// <summary>
        /// 依据IDS和主键删除,上层检测其传值是否合法
        /// </summary>
        public static bool DelByIDS(string tbname, string pk, string ids)
        {
            SqlModel model = new SqlModel() { cmd = SqlCmd.Delete, tbName = tbname, };
            model.where = "" + pk + " IN (" + ids + ")";
            DB.ExecuteNonQuery(model);
            return true;
        }
        public static bool DelByWhere(string tbname, string where, List<SqlParameter> splist=null)
        {
            SqlModel model = new SqlModel() { cmd = SqlCmd.Delete, tbName = tbname, };
            model.where = where;
            model.spList = splist;
            DB.ExecuteNonQuery(model);
            return true;
        }
        public static DataTable Sel(string tbname, string where = "", string order = "", List<SqlParameter> sp= null)
        {
            return SelWithField(tbname, "*", where, order, sp);
        }
        /// <summary>
        /// 支持自定义字段单表查询(可带语句和指定字段值)
        /// </summary>
        public static DataTable SelWithField(string tbname, string fields, string where = "", string order = "", List<SqlParameter> sp = null)
        {
            tbname = tbname + " A ";
            SqlModel model = new SqlModel() { cmd = SqlCmd.Select, fields = fields, tbName = tbname };
            if (!string.IsNullOrEmpty(where)) { model.where = where; }
            if (!string.IsNullOrEmpty(order)) { model.order = order; }
            model.spList = sp;
            return DB.ExecuteTable(model);
        }
        public static DataTable SelTop(int psize, string pk, string fields, string tbname, string where, string order, List<SqlParameter> sp = null)
        {
            PageSetting config = new PageSetting() { cpage = 1, psize = psize, pk = pk,t1= tbname, fields = fields, where = where, order = order, spList = sp };
            return DB.SelTop(config);
        }
        public static DataTable SelTop(PageSetting config)
        {
            return DB.SelTop(config);
        }
        public static DataTable SelPage(int psize, int cpage, out int itemCount, string pk, string fields, string tbname, string where = "", string order = "", SqlParameter[] sp = null)
        {
            PageSetting config = new PageSetting();
            config.cpage = cpage;
            config.psize = psize;
            config.pk = pk;
            config.fields = fields;
            config.t1 = tbname;
            config.where = where;
            config.order = order;
            config.sp = sp;
            DataTable dt = DB.SelPage(config);
            itemCount = config.itemCount;
            return dt;
        }
        public static DataTable SelPage(int psize, int cpage, out int itemCount, string pk, string fields, string t1, string t2, string on, string where, string order, SqlParameter[] sp = null)
        {
            PageSetting config = new PageSetting();
            config.cpage = cpage;
            config.psize = psize;
            config.pk = pk;
            config.fields = fields;
            config.t1 = t1;
            config.t2 = t2;
            config.on = on;
            config.where = where;
            config.order = order;
            config.sp = sp;
            DataTable dt = DB.SelPage(config);
            itemCount = config.itemCount;
            return dt;
        }
        public static DataTable SelPage(PageSetting config) { config.dt = DB.SelPage(config); return config.dt; }
        public static DataTable JoinQuery(string fields, string mtable, string stable, string on, string where = "", string order = "", SqlParameter[] sp = null)
        {
            PageSetting setting = new PageSetting();
            setting.fields = fields;
            setting.t1 = mtable;
            setting.t2 = stable;
            setting.on = on;
            setting.where = where;
            setting.order = order;
            setting.sp = sp;
            return DB.JoinQuery(setting);
        }
        public static DataTable JoinQuery(PageSetting setting) { return DB.JoinQuery(setting); }
        #endregion
        /// <summary>
        /// 用于一些无模型的数据写入(ZL_GroupModel)
        /// </summary>
        public static int Insert(string tbname, string fields, string values, SqlParameter[] sp = null)
        {
            SqlModel model = new SqlModel() { cmd = SqlCmd.Insert, tbName = tbname };
            model.fields = fields;
            model.values = values;
            model.AddSpToList(sp);
            return DB.InsertID(model);
        }
        public static int Insert(M_Base data)
        {
            SqlModel model = new SqlModel() { cmd = SqlCmd.Insert, tbName = data.TbName, pk = data.PK };
            model.AddSpToList(data.GetParameters());
            model.fields = DB.GetFields(data);
            model.values = DB.GetParams(data);
            PreSPList(model);
            return DB.InsertID(model);
        }
        public static bool UpdateByID(M_Base data, int id)
        {
            SqlModel model = new SqlModel()
            {
                cmd = SqlCmd.Update,
                tbName = data.TbName,
                pk = data.PK
            };
            model.AddSpToList(data.GetParameters());
            model.sql = "UPDATE " + data.TbName + " SET " + DB.GetFieldAndParam(data) + " WHERE " + data.PK + "=" + id;
            PreSPList(model);
            DB.ExecuteNonQuery(model);
            return true;
        }
        /// <summary>
        /// 用于执行更新语句
        /// </summary>
        public static bool UpdateSQL(string tbname, string sets, string where, List<SqlParameter> sp = null)
        {
            if (string.IsNullOrEmpty(sets)) { throw new Exception("更新字段不能为空"); }
            SqlModel model = new SqlModel();
            model.cmd = SqlCmd.Update;
            model.tbName = tbname;
            model.where = where;
            model.set = sets;
            //List<SqlParam> paramList = new List<SqlParam>();
            ////这里的的按,号切割,造成Replace等切的过多报错
            //foreach (string set in sets.Split(','))
            //{
            //    SqlParam paramMod = new SqlParam();
            //    paramMod.key = set.Split('=')[0];
            //    paramMod.value = set.Split('=')[1];
            //    paramList.Add(paramMod);
            //}
            //model.paramList = paramList;
            model.spList = sp;
            DB.ExecuteNonQuery(model);
            return true;
        }
        public static DbDataReader SelReturnReader(string tbname, string pk, int id)
        {
            return SelReturnReader(tbname, pk + "=" + id, null);
        }
        /// <summary>
        /// [base]
        /// </summary>
        public static DbDataReader SelReturnReader(string tbname, string where, SqlParameter[] sp = null)
        {
            SqlModel model = new SqlModel();
            model.size = 1;
            model.cmd = SqlCmd.Select;
            model.tbName = tbname;
            model.where = where;
            model.AddSpToList(sp);
            return DB.ExecuteReader(model);
        }
        public static DbDataReader SelReturnReader(string tbname, string where, string order, List<SqlParameter> sp = null)
        {
            SqlModel model = new SqlModel();
            model.cmd = SqlCmd.Select;
            model.tbName = tbname;
            model.where = where;
            model.order = order;
            model.spList = sp;
            return DB.ExecuteReader(model);
        }
        /// <summary>
        /// true:存在
        /// </summary>
        public static bool IsExist(string tbname, string where, List<SqlParameter> sp = null)
        {
            return Count(tbname, where, sp, null) > 0;
        }
        public static int Count(string tbname, string where, List<SqlParameter> sp = null)
        {
            return Count(tbname, where, sp, null);
        }
        public static int Count(string tbname, string where, List<SqlParameter> sp, params Sql_Where[] whereList)
        {
            SqlModel model = new SqlModel();
            model.fields = "Count(*)";
            model.tbName = tbname;
            model.where = where;
            if (whereList != null) { model.whereList.AddRange(whereList); }
            model.spList = sp;
            return DataConvert.CLng(DB.ExecuteScala(model));
        }
        //-------------------
        public static object ExecuteScala(string tbname, string fields, string where, string order = "", List<SqlParameter> sp = null)
        {
            SqlModel model = new SqlModel();
            model.tbName = tbname;
            model.fields = fields;
            model.where = where;
            model.order = order;
            model.spList = sp;
            object obj = DB.ExecuteScala(model);
            if (obj == DBNull.Value || obj == null) { obj = ""; }
            return obj;
        }
        //仅给予标签层调用,其他层不要调
        public static DataTable ExecuteTable(string sql, List<SqlParameter> sp = null)
        {
            SqlModel model = new SqlModel();
            model.sql = sql;
            model.spList = sp;
            return DB.ExecuteTable(model);
        }
        //public static void ExecuteNonQuery(string sql, List<SqlParameter> sp = null)
        //{
        //    SqlModel model = new SqlModel();
        //    model.sql = sql;
        //    model.spList = sp;
        //    DB.ExecuteNonQuery(model);
        //}
        #region Tools
        /// <summary>
        /// 根据数据库,返回对应的语句
        /// </summary>
        public static string GetSqlByDB(string sql, string oracle)
        {
            switch (DBCenter.DB.DBType)
            {
                case "oracle":
                    return oracle;
                default:
                    return sql;
            }
        }
        /// <summary>
        /// 生成 between语句
        /// </summary>
        public static string GetDateSql(string field,string stime, string etime)
        {
            Sql_Where whereMod = new Sql_Where() { join = "", field = field, type = "date", stime = stime, etime = etime };
            return DB.GetDateSql(whereMod);
        }
        /// <summary>
        /// 如果是Oracle则对splist进行检测,剔除其中的多余参数
        /// </summary>
        /// <param name="model"></param>
        private static void PreSPList(SqlModel model)
        {
            if (model.spList == null || model.spList.Count < 1) { return; }
            //剔除主键
            if (!string.IsNullOrEmpty(model.pk))
            {
                for (int i = 0; i < model.spList.Count; i++)
                {
                    if (model.spList[i].ParameterName.Equals("@" + model.pk, StringComparison.CurrentCultureIgnoreCase))
                    { model.spList.Remove(model.spList[i]); }
                }
            }
        }
        ///// <summary>
        ///// 对字段进行处理,去除语法糖,使其兼容多数据库
        ///// </summary>
        //public string Conver_Field(string fields)
        //{
        //    if (fields.Equals("*")) { return fields; }
        //    //将child=(select)用法换为(select) AS child
          
        //}
        #endregion
    }
}
