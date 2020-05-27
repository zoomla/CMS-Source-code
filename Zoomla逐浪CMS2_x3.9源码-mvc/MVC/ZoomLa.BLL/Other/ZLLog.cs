using System;
using System.IO;
using System.Web;
using System.Data;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Text;
using log4net;
using log4net.Appender;
using Newtonsoft.Json;
using ZoomLa.BLL.Helper;
using ZoomLa.Common;
using ZoomLa.SQLDAL;
using ZoomLa.Model;
using System.Data.SqlServerCe;

namespace ZoomLa.BLL
{
    /*
     * 何人(uname,ip,aname),何时(CDate:datetime)从何处(Source:Request.Rawurl),产生何种动作(action,type),动作的详情(message:文件名,保存路径)
     * 
     * 用途:
     * 1,安全:如记录上传文件信息,可以方便的追朔上传人,上传页面,上传时间,快速发现漏洞
     * 2,问题诊断:根据日志文件,记录并分析出异常产生的原因(如标签,全局异常)
     * 3,操作日志:用于记录管理员的常用操作,用于备忘
     * 4,将日志与主数据库SQL分离,避免大量的读写操作造成数据库压力
     * 
     * 每个数据库只有一张表,便于维护管理
     * 增量删除功能(DB,文本)
     */
    public class ZLLog
    {
        public static string constr = "";
        static ZLLog() { constr = SqlCEHelper.GetConStr(function.VToP("/Log/log.sdf"),"zoomlacms"); }
        public static void L(string msg)
        {
            L(ZLEnum.Log.exception, msg);
        }
        public static void L(Model.ZLEnum.Log type, string msg)
        {
            M_Log model = new M_Log() { Message = msg };
            L(type, model);
        }
        /// <summary>
        /// 写入至文本或XML
        /// </summary>
        public static void L(Model.ZLEnum.Log type, M_Log model)
        {
            EmptyDeal(type, model);
            ILog logs = LogManager.GetLogger(type.ToString());
            logs.Info(model.ToString());
        }
        /// <summary>
        /// 写入至SQLite
        /// </summary>
        /// <returns>插入的ID</returns>
        public static int ToDB(Model.ZLEnum.Log type, M_Log model)
        {
            try
            {
                //EmptyDeal(type, model);
                //string dbfile = GetDBFile(type);
                //int id = SQLiteHelper.Insert(dbfile, GetTableName(type), model.ToDic());
                //if (id % 100 == 0)
                //{
                //    //如果数据大于3万条,则删除1万条
                //    string sql = "DELETE FROM ZL_ConLog WHERE (SELECT count(ID) FROM ZL_ConLog)> 30000 AND ID in (SELECT ID FROM ZL_ConLog ORDER BY CDate DESC";
                //    sql += " LIMIT (SELECT count(ID) FROM ZL_ConLog) offset 20000 ) ";
                //    SQLiteHelper.ExecuteSql(dbfile, sql);
                //}
                //return id;
                return 1;
            }
            catch { return 1; }
        }
        public static void ToCE(Model.ZLEnum.Log type, M_Log model)
        {
            try
            {
                string tbname = "ZL_Log4";
                EmptyDeal(type, model);
                SqlCEHelper.InsertID(constr, tbname, BLLCommon.GetFields(model), BLLCommon.GetParas(model), GetCESP(model));
                int ran = new Random().Next(0, 151);
                if (ran == 150)
                {
                    int count = Convert.ToInt32(SqlCEHelper.ExecuteTable(constr, "SELECT COUNT(*) FROM " + tbname).Rows[0][0]);
                    if (count > 30000)//如果数据大于3万条,则删除1万条
                    {
                        string midsql = "SELECT * FROM " + tbname + " ORDER BY CDate DESC OFFSET 20000 ROWS FETCH NEXT 1 ROWS ONLY;";
                        int id = Convert.ToInt32(SqlCEHelper.ExecuteTable(constr, midsql).Rows[0][0]);
                        string delsql = "DELETE FROM " + tbname + " WHERE ID<" + id;
                        SqlCEHelper.ExecuteSql(constr, delsql);
                    }
                }
            }
            catch { }
        }
        //FOR SQLCE
        private static SqlCeParameter[] GetCESP(M_Log model)
        {
            if (model.CDate <= DateTime.MinValue) { model.CDate = DateTime.Now; }
            string[,] strArr = model.FieldList();
            SqlCeParameter[] sp = new SqlCeParameter[strArr.GetLength(0)];
            for (int i = 0; i < strArr.GetLength(0); i++)
            {
                sp[i] = new SqlCeParameter("@" + strArr[i, 0], (SqlDbType)Enum.Parse(typeof(SqlDbType), strArr[i, 1]), Convert.ToInt32(strArr[i, 2]));
            }
            sp[0].Value = model.ID;
            sp[1].Value = model.UName;
            sp[2].Value = model.IP;
            sp[3].Value = model.AName;
            sp[4].Value = model.CDate;
            sp[5].Value = model.Source;
            sp[6].Value = model.Action;
            sp[7].Value = model.Message;
            sp[8].Value = model.Level;
            return sp;
        }
        //写入至SQL
        public static int ToSQL(Model.ZLEnum.Log type, M_Log model)
        {
            EmptyDeal(type, model);
            try
            {
                int id = Sql.insertID(model.TbName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
                if (id % 200 == 0)
                {
                    string sql = "DELETE FROM " + model.TbName + " WHERE (SELECT COUNT(ID) FROM " + model.TbName + ")> 30000 AND ID IN (SELECT TOP 10000 ID FROM " + model.TbName + " ORDER BY CDate ASC)";
                    SqlHelper.ExecuteSql(sql);
                }
                return id;
            }
            catch { return 0; }
        }
        private static M_Log EmptyDeal(Model.ZLEnum.Log type, M_Log model)
        {
            if (string.IsNullOrEmpty(model.Action)) { model.Action = type.ToString(); }
            if (model.CDate <= DateTime.MinValue) { model.CDate = DateTime.Now; }
            try
            {
                if (string.IsNullOrEmpty(model.IP)) { model.IP = IPScaner.GetUserIP(); }
                if (string.IsNullOrEmpty(model.Source)) { model.Source = HttpContext.Current.Request.RawUrl; }
            }
            catch { }
            if (type == Model.ZLEnum.Log.fileup || type == Model.ZLEnum.Log.safe)
            {
                try
                {
                    if (string.IsNullOrEmpty(model.UName)) { M_UserInfo mu = new B_User().GetLogin(); if (mu != null && mu.UserID != 0) { model.UName = mu.UserName; } }
                    if (string.IsNullOrEmpty(model.AName)) { M_AdminInfo adminMod = B_Admin.GetLogin(); if (adminMod != null && adminMod.AdminId > 0) { model.AName = adminMod.AdminName; } }
                }
                catch { }
            }
            return model;
        }
        #region  SQLite(Disuse)
        //private static string GetDBFile(Model.ZLEnum.Log type)
        //{
        //    switch (type)
        //    {
        //        case Model.ZLEnum.Log.content:
        //            return function.VToP("/Log/ConLog.db");
        //        case Model.ZLEnum.Log.alogin:
        //            return function.VToP("/Log/AdminLogin.db");
        //        default:
        //            throw new Exception("无对应数据库");
        //    }
        //}
        //private static string GetTableName(Model.ZLEnum.Log type)
        //{
        //    return "ZL_ConLog";//用此名,按数据库分
        //}
        #endregion
        /*---------------------------------------------*/
        //内容管理日志
        public DataTable SelLog(Model.ZLEnum.Log type)
        {
            //string dbfile = GetDBFile(type);
            //try
            //{
            //    return SQLiteHelper.ExecuteTable(dbfile, "SELECT * FROM ZL_ConLog");
            //}
            //catch { return null; }
            switch (type)
            {
                case Model.ZLEnum.Log.alogin:
                    //return SqlHelper.ExecuteTable(CommandType.Text,"SELECT * FROM ZL_Log4");
                    return SqlCEHelper.ExecuteTable(constr, "SELECT * FROM ZL_Log4 ORDER BY CDate DESC");
                case Model.ZLEnum.Log.content:
                    return null;
                default:
                    return null;
            }
        }
    }
}
