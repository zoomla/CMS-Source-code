using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ZoomLa.SQLDAL;

namespace ZoomLa.BLL.Site
{
   public class B_IDC_DomainLog
    {
        // private M_Site_Log model = new M_Site_Log();
        public string strTableName = "";
        public string PK = "ID";
        public DataTable dt = null;

        public B_IDC_DomainLog() 
        {
            strTableName = "ZL_IDC_DomainLog";
            //strTableName = model.TbName;
            //PK = model.PK;
        }
        //-----------------Insert
        public int Insert(string CartID, string DomName, OPType Type, string OwnUserID, string Detail) 
        {
            SqlParameter[] sp = new SqlParameter[] 
            {
                new SqlParameter("CartID",CartID),
                 new SqlParameter("DomName",DomName),//用户更改插入此值
                  new SqlParameter("Type",Type),//管理员更改插入此值
                  new SqlParameter("OwnUserID",OwnUserID),
                   new SqlParameter("Detail",Detail),
            };
          return SqlHelper.ExecuteNonQuery(CommandType.Text, "Insert Into " + strTableName + " (CartID,DomName,Type,OwnUserID,Detail)Values(@CartID,@DomName,@Type,@OwnUserID,@Detail);select @@IDENTITY;", sp);
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
        public DataTable SelAllByUserID(int userID, UserType uType)
        {
            SqlParameter[] sp = new SqlParameter[] 
            {
                new SqlParameter("userID",userID),
            };
            return SqlHelper.ExecuteTable(CommandType.Text, "Select * From " + strTableName + " Where " + uType.ToString() + " = @userID", sp);
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
        //-----------------Tool
       //续费，更改日期,购买域名,管理员增加域名,管理员删除域名,
        public enum OPType{Renewal,BuyDomain,AddDomain,DelDomain};
        public enum UserType {OwnUserID,OwnAdminID};//用户,管理员,全部
        public string GetType(string s) 
        {
            string result = "";
            switch (s)
            {
                case "0":
                    result = "续费";
                    break;
                case "1":
                    result = "购买域名";
                    break;
                case "2":
                    result = "管理员加域名";
                    break;
                case "3":
                    result = "管理员删域名";
                    break;
                default:
                    result = "未知";
                    break;
            }
            return result;
        }
    }
}
