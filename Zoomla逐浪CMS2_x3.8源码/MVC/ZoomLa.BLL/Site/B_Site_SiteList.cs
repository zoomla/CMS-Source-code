using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using ZoomLa.Model;
using ZoomLa.SQLDAL;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLa.BLL
{
    public class B_Site_SiteList
    {
        private M_Site_SiteList model = new M_Site_SiteList();
        public string strTableName = "";
        public string PK = "";
        public DataTable dt = null;

        public B_Site_SiteList() 
        {
            strTableName = model.TbName;
            PK = model.PK;
        }
        //-----------------Retrieve
        /// <summary>
        /// 根据UserID，获取其组权限模型
        /// </summary>
        public M_Site_SiteList SelBySiteID(string id)
        {
            // strSql = "SELECT * FROM " + strTableName + " " + strWhere;
            SqlParameter[] sp = new SqlParameter[] {new SqlParameter("id",id) };
            using (SqlDataReader reader = Sql.SelReturnReader(strTableName, "Where SiteID = @id", sp))
            {
                if (reader.Read())
                {
                    return model.GetModelFromReader(reader);
                }
                else return null;
            }
        }
        /// <summary>
        /// 找到第一个返回模型
        /// </summary>
        public M_Site_SiteList SelByUserID(string id)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("id", id) };
            using (SqlDataReader reader = Sql.SelReturnReader(strTableName, "Where SiteManager = @id", sp))
            {
                if (reader.Read())
                {
                    return model.GetModelFromReader(reader);
                }
                else return null;
            }
        }
        public DataTable SelAllByUserID(string id)
        {
            SqlParameter[] sp = new SqlParameter[] {new SqlParameter("id",id) };
            dt=SqlHelper.ExecuteTable(CommandType.Text,"Select * From "+strTableName+" Where SiteManager = @id",sp);
            return dt;
        }
        public DataTable SelAll() 
        {
            dt = SqlHelper.ExecuteTable(CommandType.Text, "Select * From " + strTableName);
            return dt;
        }
        public PageSetting SelPage(int cpage, int psize)
        {
            PageSetting setting = PageSetting.Single(cpage, psize, strTableName, PK, "");
            DBCenter.SelPage(setting);
            return setting;
        }
        /// <summary>
        /// EndDate(到期时间)>计算机时间,为空不检测
        /// </summary>
        public DataTable SelAllExpire() 
        {
          DataTable dt=SqlHelper.ExecuteTable(CommandType.Text,"Select * From "+strTableName+" Where EndDate<getdate();");
          return dt;
        }
        /// <summary>
        /// 检测用户是否有该站点ID的管理权
        /// </summary>
        public bool AuthCheck(string siteID,string userID) 
        {
            //检查该用户是否有指定的站点的权限
            SqlParameter[] sp = new SqlParameter[] {
            new SqlParameter("siteID",siteID),
            new SqlParameter("userID",userID)};
            dt = SqlHelper.ExecuteTable(CommandType.Text,"select * from "+strTableName+" Where SiteID=@siteID and SiteManager = @userID",sp);
            return (dt != null && dt.Rows.Count > 0);
        }
        //-----------------Insert
        public int Insert(M_Site_SiteList model)
        {
            return Sql.insert(strTableName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        /// <summary>
        /// 参数为内存表,批量插入
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public void BatInsert(DataTable dt)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                SqlHelper.ExecuteNonQuery(CommandType.Text, "Insert", null);
            }
           
        }
        //-----------------Update
        /// <summary>
        /// 更新信息
        /// </summary>
        public bool UpdateModel(M_Site_SiteList model)
        {
            return Sql.UpdateByIDs(strTableName, PK, model.ID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
        public M_Site_SiteList SelectByName(string sitename)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("sitename", sitename) };
            using (SqlDataReader reader = Sql.SelReturnReader(strTableName, "Where SiteName = @sitename", sp))
            {
                if (reader.Read())
                {
                    return model.GetModelFromReader(reader);
                }
                else
                    return null;
            }
        }
        public DataTable CheckOrderNum(string ordernum)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("ordernum", "%" + ordernum + "%") };
            return SqlHelper.ExecuteTable(CommandType.Text, "Select * From " + strTableName + " Where OrderNum like @ordernum",sp);
        }
        public DataTable CheckOrderNum(string sitename,string ordernum)
        {
            SqlParameter[] sp = new SqlParameter[] { 
                new SqlParameter("ordernum", "%" + ordernum + "%"),
                new SqlParameter("sitename",sitename)
            };
            return SqlHelper.ExecuteTable(CommandType.Text, "Select * From " + strTableName + " Where OrderNum like @ordernum and SiteName=@sitename", sp);
        }
    }
}
