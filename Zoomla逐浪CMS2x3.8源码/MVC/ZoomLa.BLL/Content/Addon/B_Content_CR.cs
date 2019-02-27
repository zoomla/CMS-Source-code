using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ZoomLa.Components;
using ZoomLa.Model;
using ZoomLa.Model.Content;
using ZoomLa.SQLDAL;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLa.BLL.Content
{
    public class B_Content_CR
    {
        private M_Content_CR initMod = new M_Content_CR();
        private string TbName, PK;
        public B_Content_CR()
        {
            TbName = initMod.TbName;
            PK = initMod.PK;
        }
        public bool Del(int ID)
        {
            return DBCenter.Del(TbName, PK, ID);
        }
        public int Insert(M_Content_CR model)
        {
            return DBCenter.Insert(model);
        }
        public DataTable Sel()
        {
            return DBCenter.Sel(TbName);
        }
        public M_Content_CR SelReturnModel(int ID)
        {
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
        public bool UpdateByID(M_Content_CR model)
        {
            return DBCenter.UpdateByID(model, model.ID);
        }
        public bool DelByIDS(string ids)
        {
            SafeSC.CheckIDSEx(ids);
            return DBCenter.DelByIDS(TbName, PK, ids);
        }
        public DataTable Search(string skey)
        {
            List<SqlParameter> sp = new List<SqlParameter>();
            sp.Add(new SqlParameter("Title", "%" + skey + "%"));
            return DBCenter.Sel(TbName, "Title LIKE @Title", PK + " DESC", sp);
        }
        public PageSetting SelPage(int cpage, int psize, string title = "")
        {
            string where = "1=1 ";
            List<SqlParameter> sp = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(title)) { where += " AND Title LIKE @title"; sp.Add(new SqlParameter("title", "%" + title + "%")); }
            PageSetting setting = PageSetting.Single(cpage, psize, TbName, PK, where, PK + " DESC", sp);
            DBCenter.SelPage(setting);
            return setting;
        }
        public M_Content_CR CreateFromContent(M_CommonData conMod, string content, double repPrice, double matPrice)
        {
            M_Content_CR model = new M_Content_CR();
            model.FromUrl = SiteConfig.SiteInfo.SiteUrl + "/Item/" + conMod.GeneralID + ".aspx";
            model.Type = "";
            model.FromType = SiteConfig.SiteInfo.SiteName;
            model.GeneralID = conMod.GeneralID;
            model.Title = conMod.Title;
            model.Author = conMod.Inputer;
            model.KeyWords = conMod.TagKey;
            model.Content = content;
            model.RepPrice = repPrice;
            model.MatPrice = matPrice;
            return model;
        }
        public M_Content_CR SelByGid(int gid)
        {
            if (gid < 1) { return null; }
            string where = "GeneralID = " + gid + "AND Status = 1";
            using (DbDataReader reader = DBCenter.SelReturnReader(TbName, where, null))
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
        public DataTable SelByGidToDT(int gid)
        {
            return DBCenter.SelTop(1, "ID", "*", TbName, "GeneralID=" + gid, "ID DESC");
        }
        public bool DelByGid(int gid)
        {
            return Sql.Del(TbName, "GeneralID = " + gid);
        }
    }
}
