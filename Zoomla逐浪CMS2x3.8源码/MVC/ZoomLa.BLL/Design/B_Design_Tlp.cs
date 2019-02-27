using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ZoomLa.Model;
using ZoomLa.Model.Design;
using ZoomLa.SQLDAL;

namespace ZoomLa.BLL.Design
{
    public class B_Design_Tlp
    {
        private string PK, TbName = "", pageTbName = "ZL_Design_Page";
        private M_Design_Tlp initMod = new M_Design_Tlp();
        public B_Design_Tlp()
        {
            PK = initMod.PK;
            TbName = initMod.TbName;
        }
        public int Insert(M_Design_Tlp model)
        {
            return DBCenter.Insert(model);
        }
        public bool UpdateByID(M_Design_Tlp model)
        {
            return DBCenter.UpdateByID(model, model.ID);
        }
        public bool Del(int ID)
        {
            return DBCenter.Del(TbName, PK, ID);
        }
        public M_Design_Tlp SelReturnModel(int ID)
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
        /// 获取默认模板
        /// </summary>
        public M_Design_Tlp SelModelByDef(int ztype)
        {
            string where = "IsDef=1 AND ZType=" + ztype;
            using (DbDataReader reader = DBCenter.SelReturnReader(TbName, where))
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
        public M_Design_Tlp SelModelByDef(string defby)
        {
            string where = "DefBy=@defby";
            List<SqlParameter> sp = new List<SqlParameter>() { new SqlParameter("defby", defby) };
            using (DbDataReader reader = DBCenter.SelReturnReader(TbName, where,sp.ToArray()))
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
        public DataTable SelWith(int classID = 0, int ztype = 0, int psize = 0, string skey = "", int status = -100)
        {
            string where = " 1=1 ";
            string fields = "A.*,B.Name AS ClassName";
            List<SqlParameter> sp = new List<SqlParameter>();
            if (classID > 0) { where += " AND ClassID=" + classID; }
            if (ztype != -100) { where += " AND ZType=" + ztype; }
            if (psize > 0) { fields = " TOP " + psize + " " + fields; }
            if (!string.IsNullOrEmpty(skey))
            {
                where += " AND TlpName LIKE @skey";
                sp.Add(new SqlParameter("skey", "%" + skey + "%"));
            }
            if (status == -100) { where += " AND ZStatus !=" + (int)ZLEnum.ConStatus.Recycle; }
            else{ where += " AND ZStatus = " + status; }
            return DBCenter.JoinQuery(fields, TbName, "ZL_Design_TlpClass", "A.ClassID=B.ID", where, "ZStatus DESC,ID DESC", sp.ToArray());
        }
        //---------------------------Logic
        /// <summary>
        /// 增加一套新模板,默认创建模板与组件页
        /// </summary>
        public void AddNewTlp(M_Design_Tlp model)
        {
            model.ID = Insert(model);
            M_Design_Page pageMod = new M_Design_Page();
            //pageMod.TbName = pageTbName;
            pageMod.TlpID = model.ID;
            pageMod.ZType = (int)M_Design_Page.PageEnum.Page;
            //---------------------
            pageMod.guid = Guid.NewGuid().ToString();
            pageMod.Title = "首页";
            pageMod.Path = "/index";
            P_Insert(pageMod);
            pageMod.guid = Guid.NewGuid().ToString();
            pageMod.Title = "列表页";
            pageMod.Path = "/list";
            P_Insert(pageMod);
            pageMod.guid = Guid.NewGuid().ToString();
            pageMod.Title = "内容页";
            pageMod.Path = "/content";
            P_Insert(pageMod);
            pageMod.guid = Guid.NewGuid().ToString();
            pageMod.Title = "全局组件";
            pageMod.Path = "/nav";
            pageMod.ZType = (int)M_Design_Page.PageEnum.Global;
            P_Insert(pageMod);
        }
        public void SetDef(int tlpid, int ztype)
        {
            DBCenter.UpdateSQL(TbName, "IsDef=0", "ZType=" + ztype);
            DBCenter.UpdateSQL(TbName, "IsDef=1", "ID=" + tlpid + " AND ZType=" + ztype);
        }
        //---------------------------模板页面操作
        public void P_Del(int id)
        {
            DBCenter.Del(pageTbName, "ID", id);
        }
        public void P_Insert(M_Design_Page pageMod)
        {
            pageMod.TbName = pageTbName;
            DBCenter.Insert(pageMod);
        }
        public DataTable SelByTlpID(int tlpID)
        {
            string where = "TlpID=" + tlpID + " AND UserID=0";
            return DBCenter.Sel(pageTbName, where);
        }
        /// <summary>
        /// 清空目标站点页面,拷贝模板,并返回新加入的模板index页面的guid
        /// </summary>
        public string CopyTlp(int tlpID, M_Design_SiteInfo sfMod)
        {
            if (tlpID < 1 || sfMod == null || sfMod.ID < 1) { return ""; }
            //后期扩展为只覆盖指定页??
            DBCenter.DelByWhere("ZL_Design_Page", "SiteID=" + sfMod.ID);
            string guid = "";
            DataTable dt = SelByTlpID(tlpID);
            dt.TableName = pageTbName;
            dt.Columns.Remove("ID");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                dr["Guid"] = System.Guid.NewGuid().ToString();
                dr["UserID"] = sfMod.UserID;
                dr["SiteID"] = sfMod.ID;
                //dr["Comp"] = dr["Comp"].ToString().Replace("/UploadFiles/", B_Design_SiteInfo.GetSiteUpDir(sfMod.ID));
                string path = dt.Rows[i]["Path"].ToString().ToLower().Replace(" ", "");
                if (path.Equals("/") || path.Equals("/index")) { guid = dr["Guid"].ToString(); }
            }
            SqlHelper.Insert_Bat(dt, SqlHelper.ConnectionString);
            return guid;
        }
    }
}
