using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Model.Design;
using ZoomLa.SQLDAL;

namespace ZoomLa.BLL.Design
{
    public class B_Design_SiteInfo
    {
        private string PK, TbName = "";
        private M_Design_SiteInfo initMod = new M_Design_SiteInfo();
        public B_Design_SiteInfo()
        {
            PK = initMod.PK;
            TbName = initMod.TbName;
        }
        public int Insert(M_Design_SiteInfo model)
        {
            return DBCenter.Insert(model);
        }
        public bool UpdateByID(M_Design_SiteInfo model)
        {
            return DBCenter.UpdateByID(model, model.ID);
        }
        public bool Del(int ID)
        {
            return DBCenter.Del(TbName, PK, ID);
        }
        public M_Design_SiteInfo SelReturnModel(int ID)
        {
            if (ID < 1) { return null; }
            using (DbDataReader reader = DBCenter.SelReturnReader(TbName, PK, ID))
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
        /// <summary>
        /// 默认返回第一个站点
        /// </summary>
        public M_Design_SiteInfo SelModelByUid(int uid)
        {
            if (uid < 1) { return null; }
            using (DbDataReader reader = DBCenter.SelReturnReader(TbName, "UserID=" + uid))
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
        public DataTable Sel()
        {
            return DBCenter.Sel(TbName, "", PK + " DESC");
        }
        public DataTable SelWith(string uname = "")
        {
            string where = "";
            List<SqlParameter> sp = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(uname))
            {
                where += " (UserName LIKE @uname OR SiteName LIKE @uname)";
                sp.Add(new SqlParameter("uname", "%" + uname + "%"));
            }
            return DBCenter.JoinQuery("A.*,B.DomName", TbName, "ZL_IDC_DomainList", "A.DomainID=B.ID", where, PK + " DESC", sp.ToArray());
        }
        public DataTable U_Sel(int uid)
        {
            return DBCenter.Sel(TbName, "UserID=" + uid, PK + " DESC");
        }
        //-------------------------------Tools
        /// <summary>
        /// 将站点信息整合为配置给页面调用
        /// </summary>
        public string ToSiteCfg(M_Design_SiteInfo model)
        {
            if (model == null || model.ID < 1) { return "{}"; }
            JObject cfg = new JObject();
            cfg.Add(new JProperty("siteid", model.ID));
            cfg.Add(new JProperty("api", "/design/design.ashx"));
            string json = JsonConvert.SerializeObject(cfg);
            return json;
        }
        /// <summary>
        /// 用户是否有管理该站点的权限
        /// </summary>
        public void CheckAuthEx(M_Design_SiteInfo sfMod, M_UserInfo mu)
        {
            if (sfMod == null || mu == null || mu.IsNull || !sfMod.UserID.Equals(mu.UserID.ToString())) { function.WriteErrMsg("你没有管理该站点的权限"); }
        }
        /// <summary>
        /// 获取当前站点的上传文件目录
        /// </summary>
        public static string GetSiteUpDir(int siteID)
        {
            return "/Site/" + siteID + "/UploadFiles/";
        }
    }
}
