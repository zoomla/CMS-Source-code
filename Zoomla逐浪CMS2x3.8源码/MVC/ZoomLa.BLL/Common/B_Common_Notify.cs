using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ZoomLa.Model;
using ZoomLa.SQLDAL;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLa.BLL
{
    public class B_Common_Notify
    {
        private M_Common_Notify initMod = new M_Common_Notify();
        public string TbName, PK;
        public B_Common_Notify()
        {
            TbName = initMod.TbName;
            PK = initMod.PK;
        }
        public bool Del(int ID)
        {
            return DBCenter.Del(TbName, PK, ID);
        }
        public int Insert(M_Common_Notify model)
        {
            return DBCenter.Insert(model);
        }
        public DataTable Sel()
        {
            return DBCenter.Sel(TbName);
        }
        public DataTable Blog_Sel(int infoid, int zstatus)
        {
            return Sel("2", zstatus, infoid);
        }
        /// <summary>
        /// 选择要提示给该用户的短信
        /// </summary>
        public DataTable Blog_SelMy(int uid)//后期是否改为
        {
            string where = "ReceUsers LIKE '%," + uid + ",%' AND ZStatus=0 AND NType=2 AND BeginDate<='" + DateTime.Now.ToString() + "'";
            return DBCenter.Sel(TbName, where, "ID DESC");
        }
        public DataTable Sel(string ntype = "", int zstatus = -100, int infoid = -100, string sdate = "", string title = "")
        {
            List<SqlParameter> sp = new List<SqlParameter>();
            string where = "1=1 ";
            if (!string.IsNullOrEmpty(ntype)) { SafeSC.CheckIDSEx(ntype); where += " AND NType IN(" + ntype + ")"; }
            if (zstatus != -100) { where += " AND ZStatus=" + zstatus; }
            if (infoid != -100) { where += " AND InfoID=" + infoid; }
            if (!string.IsNullOrEmpty(sdate)) { where += " AND BeginDate>=@sdate"; sp.Add(new SqlParameter("sdate", sdate)); }
            if (!string.IsNullOrEmpty(title)) { where += " AND Title LIKE @title"; sp.Add(new SqlParameter("title", "%" + title + "%")); }
            return DBCenter.Sel(TbName, where, "ID DESC", sp);
        }
        public PageSetting SelPage(int cpage, int psize, string ntype = "", int zstatus = -100, int infoid = -100, string sdate = "", string title = "")
        {
            string where = "1=1 ";
            List<SqlParameter> sp = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(ntype)) { SafeSC.CheckIDSEx(ntype); where += " AND NType IN(" + ntype + ")"; }
            if (zstatus != -100) { where += " AND ZStatus=" + zstatus; }
            if (infoid != -100) { where += " AND InfoID=" + infoid; }
            if (!string.IsNullOrEmpty(sdate)) { where += " AND BeginDate>=@sdate"; sp.Add(new SqlParameter("sdate", sdate)); }
            if (!string.IsNullOrEmpty(title)) { where += " AND Title LIKE @title"; sp.Add(new SqlParameter("title", "%" + title + "%")); }
            PageSetting setting = PageSetting.Single(cpage, psize, TbName, PK, where, PK + " DESC", sp);
            DBCenter.SelPage(setting);
            return setting;
        }
        public void ChangeStatus(DataTable dt, int zstatus)
        {
            if (dt.Rows.Count < 1) { return; }
            string ids = "";
            foreach (DataRow dr in dt.Rows)
            {
                ids += dr["ID"] + ",";
            }
            ids = ids.TrimEnd();
            DBCenter.UpdateSQL(TbName, "ZStatus=" + zstatus, "ID IN (" + ids + ")");
        }
        public M_Common_Notify SelReturnModel(int ID)
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
        public bool UpdateByID(M_Common_Notify model)
        {
            return DBCenter.UpdateByID(model, model.ID);
        }
        public bool DelByIDS(string ids, int uid = -100)
        {
            string where = " 1=1 ";
            if (string.IsNullOrEmpty(ids)) { return false; }
            if (uid != -100) { where += " AND CUser=" + uid; }
            where += " AND ID IN (" + ids + ")";
            SafeSC.CheckIDSEx(ids);
            return DBCenter.DelByWhere(TbName, where);
        }
    }
}
