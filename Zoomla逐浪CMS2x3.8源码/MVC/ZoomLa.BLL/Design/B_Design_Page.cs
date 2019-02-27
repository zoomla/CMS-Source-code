using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ZoomLa.Model.Design;
using ZoomLa.SQLDAL;
using PageEnum = ZoomLa.Model.Design.M_Design_Page.PageEnum;

namespace ZoomLa.BLL.Design
{
    public class B_Design_Page : ZL_Bll_InterFace<M_Design_Page>
    {
        private string PK, TbName = "";
        private M_Design_Page initMod = new M_Design_Page();
        public B_Design_Page()
        {
            PK = initMod.PK;
            TbName = initMod.TbName;
        }
        public int Insert(M_Design_Page model)
        {
            return DBCenter.Insert(model);
        }
        public bool UpdateByID(M_Design_Page model)
        {
            return DBCenter.UpdateByID(model, model.ID);
        }
        public bool Del(int ID)
        {
            return DBCenter.Del(TbName,PK,ID);
        }
        public DataTable Sel()
        {
            return DBCenter.Sel(TbName, "", PK + " DESC");
        }
        public M_Design_Page SelReturnModel(int ID)
        {
            return SelModelByWhere(TbName, PK + "=" + ID, null);
        }
        public M_Design_Page SelModelByGuid(string guid)
        {
            List<SqlParameter> sp = new List<SqlParameter>() { new SqlParameter("guid", guid) }; ;
            string where = "Guid=@guid";
            return SelModelByWhere(TbName, where, sp);
        }
        public M_Design_Page SelModelByPath(string domain, string path)
        {
            List<SqlParameter> sp = new List<SqlParameter>() { new SqlParameter("domain", domain), new SqlParameter("path", path) };
            string table = "(SELECT A.*,B.DomName FROM " + TbName + " A LEFT JOIN ZL_IDC_DomainList B ON A.UserID=B.UserID) AS A";
            string where = " A.DomName=@domain AND A.Path=@path ";
            return SelModelByWhere(table, where, sp);
        }
        /// <summary>
        /// 根据站点ID与路径取页面,path为空则取默认主页
        /// </summary>
        public M_Design_Page SelModelBySiteID(int siteid, string path)
        {
            path = (path ?? "").Replace(" ", "");
            List<SqlParameter> sp = new List<SqlParameter>() { new SqlParameter("path", path) };
            string where = "SiteID=" + siteid + " AND Path=@path";
            if (string.IsNullOrEmpty(path))
            {
                where = "SiteID=" + siteid + " AND (Path='' OR Path='/' OR Path='/index' OR Path='index')"; ;
            }
            return SelModelByWhere(TbName, where, sp);
        }
        private M_Design_Page SelModelByWhere(string tbname, string where, List<SqlParameter> sp)
        {
            using (DbDataReader reader = DBCenter.SelReturnReader(tbname, where, "", sp))
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
        /// 融合全局组件
        /// </summary>
        public M_Design_Page MergeGlobal(M_Design_Page pageMod,string source="")
        {
            string where = "";
            if (pageMod.UserID == 0) { source = "tlp"; }
            switch (source.ToLower())
            {
                case "tlp"://模板修改
                    where = "ZType=" + (int)PageEnum.Global + " AND TlpID=" + pageMod.TlpID + " AND ID!=" + pageMod.ID;
                    break;
                default:
                    where = "ZType=" + (int)PageEnum.Global + " AND SiteID=" + pageMod.SiteID + " AND ID!=" + pageMod.ID;
                    break;
            }
            //将组件数据融合
            DataTable dt = DBCenter.SelWithField(TbName, "path,comp", where);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                if (string.IsNullOrEmpty(dr["comp"].ToString())) { continue; }
                pageMod.comp_global.Add(new M_Page_GlobalComp(dr));
            }
            return pageMod;
        }
        public bool IsExist(int siteID, string path)
        {
            return (SelModelBySiteID(siteID, path) != null);
        }
        //-----------------------------------------------------------------
        public DataTable U_Sel(int uid, int siteid,PageEnum ztype)
        {
            string where = "UserID=" + uid;
            if (siteid > 0) { where += " AND SiteID=" + siteid; }
            if (ztype != PageEnum.All) { where += " AND ZType="+(int)ztype; }
            return DBCenter.Sel(TbName, where);
        }
        public void U_Del(int uid, int id) { DBCenter.DelByWhere(TbName, PK + "=" + id + " AND UserID=" + uid); }
    }
}
