using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ZoomLa.BLL.Helper;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;
using ZoomLa.Model.Site;
using ZoomLa.SQLDAL;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLa.BLL.Site
{
    public class B_IDC_DomainList
    {
        private string strTableName = "ZL_IDC_DomainList";
        private string PK = "ID";
        private M_IDC_DomainList initMod = new M_IDC_DomainList();
        public int Insert(M_IDC_DomainList model)
        {
            return Sql.insertID(strTableName, model.GetParameters(model), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        public bool UpdateByID(M_IDC_DomainList model)
        {
            return Sql.UpdateByIDs(strTableName, PK, model.ID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters(model));
        }
        public M_IDC_DomainList SelReturnModel(int id)
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
        public M_IDC_DomainList SelModelByDomain(string domain)
        {
            domain = domain.Replace(" ", "");
            if (string.IsNullOrEmpty(domain)) { return null; }
            SqlParameter[] sp = new SqlParameter[] {new SqlParameter("domain",domain) };
            using (SqlDataReader reader = Sql.SelReturnReader(strTableName, " WHERE DomName=@domain", sp))
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
        public M_IDC_DomainList SelModelByUid(int uid)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(strTableName, " WHERE UserID=" + uid))
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
        //-----------------Retrieve
        public DataTable Sel()
        {
            return SqlHelper.ExecuteTable(CommandType.Text, "Select * From " + strTableName + " WHERE SType>0");
        }
        public PageSetting SelPage(int cpage, int psize)
        {
            PageSetting setting = PageSetting.Single(cpage, psize, strTableName, PK, "");
            DBCenter.SelPage(setting);
            return setting;
        }
        /// <summary>
        /// 获取全部内容，并且附带用户信息
        /// </summary>
        public DataTable SelWithUser()
        {
            return SqlHelper.JoinQuery("A.*,B.UserName", strTableName, "ZL_User", "A.UserID=B.UserID");
        }
        /// <summary>
        /// 查询指定用户的信息
        /// </summary>
        public DataTable SelWithUserByID(int uid)
        {
            return SqlHelper.JoinQuery("A.*,B.UserName", strTableName, "ZL_User", "A.UserID=B.UserID", "A.UserID=" + uid);
        }
        public DataTable SelByID(string id)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("ID", id) };
            return SqlHelper.ExecuteTable(CommandType.Text, "Select * From " + strTableName + " Where ID=@id", sp);
        }
        //-----------------Insert
        public int Insert(string DomName, DateTime CreatDate, DateTime EndDate, int UserID, string regInfo, int Year)
        {
        //    SqlParameter[] sp = new SqlParameter[] 
        //{
        //    new SqlParameter("DomName",DomName),
        //    new SqlParameter("CreatDate",CreatDate),
        //    new SqlParameter("EndDate",EndDate),
        //    new SqlParameter("UserID",UserID),
        //    new SqlParameter("regInfo",regInfo),
        //    new SqlParameter("Year",Year)
        //};
        //    return SqlHelper.ExecuteNonQuery(CommandType.Text, "Insert Into " + strTableName + " (DomName,EndDate,UserID,RegInfo,Year,CreatDate)Values(@DomName,@EndDate,@UserID,@regInfo,@Year,@CreatDate);select @@IDENTITY;", sp);
            return 0;
        }
        //-----------------Update
        public void UpdateByID(string DomName, DateTime CreatDate, DateTime EndDate, int UserID, string RegInfo, int Year, string id)
        {
            //SqlParameter[] sp = new SqlParameter[] 
            //{
            //    new SqlParameter("DomName",DomName),
            //    new SqlParameter("CreatDate",CreatDate),
            //    new SqlParameter("EndDate",EndDate),
            //    new SqlParameter("UserID",UserID),
            //    new SqlParameter("RegInfo",RegInfo),
            //    new SqlParameter("id",id),
            //    new SqlParameter("Year",Year)
            //};
            //SqlHelper.ExecuteNonQuery(CommandType.Text, "Update " + strTableName + " Set DomName=@DomName,EndDate=@EndDate,UserID=@UserID,RegInfo=@RegInfo,Year=@Year,CreatDate=@CreatDate Where ID=@id", sp);
        }
        //-----------------Delete
        public void Del(string ids)
        {
            SafeSC.CheckIDSEx(ids);
            SqlHelper.ExecuteSql("DELETE FROM " + strTableName + " WHERE ID IN (" + ids + ")");
        }
        //-----------------Tool
        /// <summary>
        /// 存在返回目标ID,不存在则为0
        /// </summary>
        /// <returns></returns>
        public int isExist(string domName) 
        {
            int result=0;
            SqlParameter[] sp=new SqlParameter[]{new SqlParameter("domName",domName)};
            try
            {
                result = Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.Text, "Select " + PK + " From " + strTableName + " Where DomName=@domName", sp));
            }
            catch { }
            return result;
        }
        /// <summary>
        /// 返回以/结尾的域名
        /// </summary>
        public static string GetMyDomain(int domainID, string protocol = "http")
        {
            B_IDC_DomainList domBll = new B_IDC_DomainList();
            M_IDC_DomainList domMod = domBll.SelReturnModel(domainID);
            if (domMod == null) { throw new Exception("指定的域名[" + domainID + "]不存在"); }
            return protocol + "://" + domMod.DomName.TrimEnd('/') + "/";
        }
        //-----------------Design
        /// <summary>
        /// 序号+"."+用户ID(小写拼音)+".demo.com"
        /// </summary>
        public string GetNewDomain(string uname)
        {
            //http://admin.site.2013.hx008.com/
             uname = StringHelper.ChineseToPY(uname).ToLower();
             return uname + "." + SiteConfig.SiteOption.DesignDomain;
        }
        public void ChangeDomain(string newdomain)
        {
            DataTable dt = DBCenter.JoinQuery("A.ID,B.UserName,B.UserID", strTableName, "ZL_User", "A.UserID=B.UserID", "SType=2");
            foreach (DataRow dr in dt.Rows)
            {
                //admin.site.2013.hx008.com
                //用户名.新域名
                List<SqlParameter> sp = new List<SqlParameter>() { new SqlParameter("domain", dr["UserID"] + "." + newdomain) };
                DBCenter.UpdateSQL(strTableName, "DomName=@domain", "ID=" + dr["ID"], sp);
            }
        }
    }
}
