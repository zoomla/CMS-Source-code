using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ZoomLa.SQLDAL;

namespace ZoomLa.BLL.Site
{
    public class B_IDC_DNSSubDom
    {
        public string strTableName = "";
        public string PK = "";
        public B_IDC_DNSSubDom() 
        {
            strTableName = "ZL_IDC_DNSSubDom";
            PK = "ID";
        }
        public DataTable SelAllByID(string id)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("id", id) };
            DataTable dt = SqlHelper.ExecuteTable(CommandType.Text, "Select * From " + strTableName + " Where ID = @id", sp);
            return dt;
        }
        public DataTable SelByMainID(string mainDomID)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("id", mainDomID) };
            return SqlHelper.ExecuteTable(CommandType.Text,"Select * From "+strTableName+" Where MainDomID=@id",sp);
        }
        public DataTable SelAll()
        {
            DataTable dt = SqlHelper.ExecuteTable(CommandType.Text, "Select * From " + strTableName);
            return dt;
        }
        /// <summary>
        /// 根据列名与关键字,获取数据,1:IP,2,域名,3用户
        /// </summary>
        /// <param name="colName">列名</param>
        /// <param name="key">关键字</param>
        public DataTable SelAll(string key)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("key", "%" + key + "%") };
            DataTable dt = SqlHelper.ExecuteTable(CommandType.Text, "Select * From " + strTableName + " Where IP like @key or Domain like @key", sp);
            return dt;
        }
        //------------Insert
        public void Insert(int mainDomID,string subDomain,string subDomData, int adminID)
        {
            SqlParameter[] sp = new SqlParameter[]
            {
                new SqlParameter("MainDomID",mainDomID),
                new SqlParameter("subDomain",subDomain),
                new SqlParameter("SubDomData",subDomData),
                new SqlParameter("CreateBy",adminID)
            };
            SqlHelper.ExecuteNonQuery(CommandType.Text, "Insert Into " + strTableName + " (MainDomID,SubDomain,SubDomData,CreateBy) values(@MainDomID,@subDomain,@SubDomData,@CreateBy)", sp);
       
        }
        /// <summary>
        /// 添加新数据后，默认插入Mail,FTP，POP3的子域名
        /// </summary>
        public void InitInsert(int mainDomID,string subDomData,int adminID) 
        {
            string[] subDom = new string[] {"MAIL","FTP","POP" };
            for (int i = 0; i < subDom.Length; i++)
            {
                Insert(mainDomID,subDom[i],subDomData,adminID);
            }
        }
        //------------Update
        public void UpdateByID(string id,string subDomain,string subDomData) 
        {
            SqlParameter[] sp = new SqlParameter[] 
            {
                new SqlParameter("id",id),
                new SqlParameter("subDomain",subDomain),
                new SqlParameter("subDomData",subDomData)
            };
            SqlHelper.ExecuteNonQuery(CommandType.Text, "Update " + strTableName + " Set subDomain=@subDomain,subDomData=@subDomData Where ID=@id", sp);
        }
        //------------Del
        public bool DelByID(string id)
        {
            SqlParameter[] sp = new SqlParameter[] {new SqlParameter("id",id) };
            return (SqlHelper.ExecuteNonQuery(CommandType.Text, "Delete " + strTableName + " Where id=@id", sp) > 0);
        }
        public bool BatDelByID(string ids) 
        {
            SafeSC.CheckIDSEx(ids);
            return Sql.Del(strTableName, "ID in (" + ids + ")"); 
        }
    }
}
