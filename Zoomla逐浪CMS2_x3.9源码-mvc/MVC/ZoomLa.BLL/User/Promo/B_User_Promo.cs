using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ZoomLa.SQLDAL;

namespace ZoomLa.BLL.User
{
    //用户推广中心
    public class B_User_Promo
    {
        /// <summary>
        /// 按用户汇总数据和时间汇总数据
        /// </summary>
        /// <param name="year">年份</param>
        /// <param name="month">月份(先指定年份,该参数才有效,否则忽略)</param>
        /// <returns></returns>
        public DataTable SelByFilter(int year, int month, string uname = "", int top = 0)
        {
            //YEAR(B.RegTime)AS Year,MONTH(B.RegTime)AS Month,DAY(B.RegTime)AS Day 
            string where = " A.PCount>0 ";
            string subWhere = " ParentUserID>0 ";
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("year", year), new SqlParameter("month", month),new SqlParameter("uname","%"+uname+"%") };
            if (!string.IsNullOrEmpty(uname))
            {
                where += " AND UserName LIKE @uname";
            }
            if (year > 0)
            {
                subWhere += "AND YEAR(RegTime)=@year";
                if (month > 0)
                {
                    subWhere += " AND MONTH(RegTime)=@month";
                }
            }
            string fields = "ROW_NUMBER() OVER(ORDER BY PCount DESC) AS RowNum,";
            fields += "A.*,B.*";
            if (top > 0) { fields = " TOP " + top + " " + fields; }
            string mtable = "(SELECT COUNT(*)AS PCount,ParentUserID FROM ZL_User WHERE " + subWhere + " Group BY ParentUserID)";
            DataTable dt = SqlHelper.JoinQuery(fields, mtable, "ZL_User", "A.ParentUserID=B.UserID", where, "PCount DESC", sp);
            return dt;
        }
        //按年月日汇总数据
        public DataTable SelByTime(string group = "month")
        {
            string sql = "";
            //按月统计
            string monthsql = "SELECT COUNT(*)AS PCount,* FROM (SELECT YEAR(RegTime)AS Year,MONTH(RegTime)AS Month FROM ZL_User WHERE ParentUserID>0) AS A GROUP BY [Year],[Month]";
            //按天统计
            string daysql = "SELECT COUNT(*)AS PCount,* FROM (SELECT YEAR(RegTime)AS Year,MONTH(RegTime)AS Month,DAY(RegTime)AS Day FROM ZL_User WHERE ParentUserID>0) AS A GROUP BY [Year],[Month],[Day]";
            switch (group)
            {
                case "year":
                    sql = "";
                    break;
                case "month":
                    sql = monthsql;
                    break;
                case "day":
                    sql = daysql;
                    break;
                default:
                    throw new Exception("选项错误[" + group + "]");
            }
            return SqlHelper.ExecuteTable(sql);
        }
    }
}
