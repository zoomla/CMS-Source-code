using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ZoomLa.SQLDAL;

namespace ZoomLa.BLL.Site
{
   public class B_IDC_DomainTemp
    {
        public string strTableName = "ZL_IDC_DomainTemp";
        public string PK = "ID";
        public DataTable dt = null;
        //-----------------Retrieve
        public DataTable Sel()
        {
            return SqlHelper.ExecuteTable(CommandType.Text,"Select * From "+strTableName);
        }
       /// <summary>
       /// 从数据库中获取ID对应的模板数据
       /// </summary>
        public DataTable SelByID(string id) 
        {
                SqlParameter[] sp = new SqlParameter[] { new SqlParameter("ID", id) };
                return SqlHelper.ExecuteTable(CommandType.Text, "Select * From " + strTableName + " Where ID=" + id, sp);
        }
       /// <summary>
       /// 从内存表中获取ID对应的模板,用于购物车
       /// </summary>
        public DataTable SelFromDT(DataTable dt,string id) 
        {
            dt.DefaultView.RowFilter = "ID = "+id;
            return dt.DefaultView.ToTable();
        }
        public DataTable SelByUserID(int userID) 
        {
           return SqlHelper.ExecuteTable(CommandType.Text,"Select * From "+strTableName+" Where OwnUserID = "+userID);
        }
       /// <summary>
       ///模板ID与模板名,供前台切割使用,格式:key:value,key2,value2
       /// </summary>
        public string SelTemp(DataTable dt=null)
        {
            string result = "";
            if(dt==null) dt = Sel();
            foreach (DataRow dr in dt.Rows)
            {
                result += dr["ID"] + ":" + dr["TempName"] + ",";
            }
            result = result.TrimEnd(','); 
            return result;
        }

        public string SelTempByUserID(int userID)
        {
            string result = "";
            DataTable dt = SelByUserID(userID);
            foreach (DataRow dr in dt.Rows)
            {
                result += dr["ID"] + ":" + dr["TempName"] + ",";
            }
            result = result.TrimEnd(',');
            return result;
        }
       //------------------New
       /// <summary>
       /// 默认模板信息和最近十次的购买
       /// </summary>
        public DataTable SelTempWithHistory(int userID) 
        {
            DataTable dt = SelByUserID(userID);
            dt.Columns.Add(new DataColumn("Index", typeof(int)));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["Index"] = i;
            }
            dt.Merge(SelHistoryByID(userID,dt.Rows.Count));
            return dt;
        }
       /// <summary>
       /// 最近十次的购买记录,用其作为模板
       /// </summary>
        public DataTable SelHistoryByID(int userID,int index=0) 
        {
            string sql = "Select Top 10 b.ProName as TempName,b.Attribute as TempValue From ZL_OrderInfo as a Left Join ZL_CartPro as b On a.id=b.orderlistid Where a.UserID="+userID+" And OrderType=5 Order By b.AddTime Desc";
            DataTable dt = SqlHelper.ExecuteTable(CommandType.Text,sql);
            dt.Columns.Add(new DataColumn("Index", typeof(int)));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["Index"] = index+i;
            }
            return dt;
        }
       /// <summary>
       /// 返回默认值
       /// </summary>
        public string SelValueByUserID(int userID) 
        {
            string result="";
            string sql = "Select Top 1 * From " + strTableName + " Where OwnUserID=" + userID;
            DataTable dt = SqlHelper.ExecuteTable(CommandType.Text,sql);
            if(dt.Rows.Count>0)
            {
                result=dt.Rows[0]["TempValue"] as string;
            }
            return result;
        }
        //-----------------Insert
        public int Insert(string tempName, string value,int ownUserID)
        {
            SqlParameter[] sp = new SqlParameter[] 
        {
            new SqlParameter("TempName",tempName),
            new SqlParameter("TempValue",value),
            new SqlParameter("OwnUserID",ownUserID)
        };
            return SqlHelper.ExecuteNonQuery(CommandType.Text, "Insert Into " + strTableName + " (TempName,TempValue,OwnUserID)Values(@TempName,@TempValue,@OwnUserID);Select @@IDENTITY;", sp);
        }
        //-----------------Update
        //允许更改UserID
        public void UpdateByID(string tempName, string value,string id) 
        {
            SqlParameter[] sp = new SqlParameter[] 
            {
                new SqlParameter("TempName",tempName),
                new SqlParameter("TempValue",value),
                new SqlParameter("id",id)
            };
            SqlHelper.ExecuteNonQuery(CommandType.Text, "Update " + strTableName + " Set TempName=@TempName,TempValue=@TempValue Where ID=@id",sp);
        }
        public bool DelByID(string id)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("id", id) };
            return SqlHelper.ExecuteSql("Delete From " + strTableName + " Where ID = @id", sp);
        }
    }
}
