using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using ZoomLa.Model;
using ZoomLa.SQLDAL;

namespace ZoomLa.BLL
{
   public class B_Site_Log
    {
       // private M_Site_Log model = new M_Site_Log();
        public string strTableName = "";
        public string PK = "";
        public DataTable dt = null;

        public B_Site_Log() 
        {
            strTableName = "ZL_IDC_Log";
            //strTableName = model.TbName;
            //PK = model.PK;
        }
        //-----------------Insert
        public int Insert(string type, string ownUserID, string ownAdminID, string remind, DateTime createDate) 
        {
            SqlParameter[] sp = new SqlParameter[] 
            {
                new SqlParameter("Type",type),
                 new SqlParameter("OwnUserID",ownUserID),//用户更改插入此值
                  new SqlParameter("OwnAdminID",ownAdminID),//管理员更改插入此值
                  new SqlParameter("Remind",remind),
                   new SqlParameter("CreateDate",createDate),
            };
            return SqlHelper.ExecuteNonQuery(CommandType.Text, "Insert Into " + strTableName + " (Type,OwnUserID,Remind,CreateDate)Values(@Type,@OwnUserID,@Remind,@CreateDate);select @@IDENTITY;", sp);
        }
       /// <summary>
       /// -1为系统,即计划任务中执行的工作
       /// </summary>
        public static int Insert(string logTypeStr, string remind, int uid = -1)
        {
            M_IDC_Log model = new M_IDC_Log();
            //如果日志表中数据大于20000,则移除最早的5000条
            string sql = "Select COUNT(ID) From " + model.TbName;
            int count = SqlHelper.ObjectToInt32(sql);
            if (count > 20000)
            {
                sql = "Delete " + model.TbName + " Where ID in(SELECT TOP 5000 ID FROM " + model.TbName + ")";
                SqlHelper.ExecuteSql(sql);
            }
            model.LogType = 0;
            model.OwnAdminID = -1;
            model.LogTypeStr = logTypeStr;
            model.Remind = remind;
            model.CreateDate = DateTime.Now;
            return Insert(model);
        }
        public static int Insert(M_IDC_Log model)
        {
            return Sql.insertID(model.TbName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        //-----------------Retrieve
        public DataTable SelAll() 
        {
            dt = SqlHelper.ExecuteTable(CommandType.Text, "Select * From " + strTableName+" Order By CreateDate Desc");
            return dt;
        }
        // /// <summary>
       ///// 预留，后台使用
       ///// </summary>
       ///// <returns></returns>
       // public DataTable SelAll() { return null; }
        /// <summary>
       /// 后台台管理员使用
       /// </summary>
        public DataTable SelByType(string type)
        {
            SqlParameter[] sp = new SqlParameter[] 
            {
                new SqlParameter("Type",type)
            };
            return SqlHelper.ExecuteTable(CommandType.Text,"Select * From "+strTableName+" Where Type=@Type",sp);
        }
        /// <summary>
       /// 前台用户使用
       /// </summary>
        public DataTable SelByTypeAndID(string type,int ownUserID) 
        {
            SqlParameter[] sp=new SqlParameter[]
            {
                new SqlParameter("type",type),
                 new SqlParameter("OwnUserID",ownUserID)
            };
            return SqlHelper.ExecuteTable(CommandType.Text,"Select * From "+strTableName+" Where Type=@Type And OwnUserID=@OwnUserID",sp);
        }

        public DataTable SelByRemind(string key,string stime,string etime)
        {
            SqlParameter[] sp = new SqlParameter[] { 
                new SqlParameter("@Key","%"+key+"%"),
                new SqlParameter("@stime",stime),
                new SqlParameter("@etime",etime)
            };
            string sql = "Select * From "+strTableName+" Where 1=1 ";
            if (!string.IsNullOrEmpty(key))
            {
                sql += " And Remind Like @Key ";
            }
            if (!string.IsNullOrEmpty(stime))
            {
                sql += "And CreateDate > @stime ";
            }
            if (!string.IsNullOrEmpty(etime))
            {
                sql += "And CreateDate < @etime ";
            }
            return SqlHelper.ExecuteTable(CommandType.Text, sql, sp);
        }
        public DataTable SelAllByUserID(int userID, UserType uType)
        {
            SqlParameter[] sp = new SqlParameter[] 
            {
                new SqlParameter("userID",userID),
            };
            return SqlHelper.ExecuteTable(CommandType.Text, "Select * From " + strTableName + " Where " + uType.ToString() + " = @userID", sp);
        }
        //-----------------Tool
       //续费，更改日期,购买域名,管理员增加域名,管理员删除域名,
        public enum OPType{Renewal,ChangeDate,BuyDomain,AddDomain,DelDomain};
        public enum UserType {OwnUserID,OwnAdminID};//用户,管理员,全部
        public string GetType(string s) 
        {
            string result = "";
            switch (s)
            {
                case "1":
                    result = "续费";
                    break;
                default:
                    result = "未知";
                    break;
            }
            return result;
        }
    }
}
