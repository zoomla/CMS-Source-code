using System;
using System.Data;
using System.Configuration;
using System.Globalization;
using ZoomLa.Model;
using ZoomLa.Components;
using ZoomLa.Common;
using System.Web;

using ZoomLa.SQLDAL;
namespace ZoomLa.BLL
{
    public class B_SiteMap
    {
       

        public B_SiteMap()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        public DataTable SelectSiteMap(int num)
        {
            string strSql = "SELECT TOP " + num + " * FROM ZL_CommonModel ORDER BY generalid desc";  //天下最经典的SQL查询语句
            return SqlHelper.ExecuteTable(CommandType.Text, strSql, null);
        }

        public DataTable SelectSIteMap(int num)
        {
            string strSql = "SELECT TOP " + num + " * FROM ZL_CommonModel ORDER BY generalid desc";  //天下最经典的SQL查询语句
            //SqlParameter[] cmdParams = new SqlParameter[1];
            //cmdParams[0] = new SqlParameter("@ID", SqlDbType.Int, 4);
            //cmdParams[0].Value = id;
            return SqlHelper.ExecuteTable(CommandType.Text, strSql, null);
        }


    }
}
