using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ZoomLa.BLL.Helper;
using ZoomLa.Model;
using ZoomLa.SQLDAL;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLa.BLL
{
    public class B_Common_UserApply : B_Base<M_Common_UserApply>
    {
        private string PK, TbName = "";
        private M_Common_UserApply initMod = new M_Common_UserApply();
        public B_Common_UserApply()
        {
            PK = initMod.PK;
            TbName = initMod.TbName;
        }
        public int Insert(M_Common_UserApply model)
        {
            if (string.IsNullOrEmpty(model.IP)) { model.IP = IPScaner.GetUserIP(); }
            return DBCenter.Insert(model);
        }
        public bool UpdateByID(M_Common_UserApply model)
        {
            return DBCenter.UpdateByID(model, model.ID);
        }
        public bool Del(int ID)
        {
            return DBCenter.Del(TbName, PK, ID);
        }
        public void DelByIDS(string ids)
        {
            SafeSC.CheckIDSEx(ids);
            DBCenter.DelByIDS(TbName, PK, ids);
        }
        public DataTable Sel()
        {
            return DBCenter.Sel(TbName, "", PK + " ASC");
        }
        public M_Common_UserApply SelReturnModel(int ID)
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
        public PageSetting SelPage(int cpage, int psize, string ztype = "", string ids = "", string remind = "", int status = -100, int uid = 0)
        {
            string where = "1=1 ";
            List<SqlParameter> sp = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(ztype)) { where += " AND A.ZType=@ztype"; sp.Add(new SqlParameter("ztype", ztype)); }
            if (!string.IsNullOrEmpty(ids)) { SafeSC.CheckIDSEx(ids); where += " AND A.ID IN (" + ids + ")"; }
            if (!string.IsNullOrEmpty(remind)) { where += " AND A.Remind=@remind"; sp.Add(new SqlParameter("remind", remind)); }
            if (status != -100) { where += " AND A.Status=" + status; }
            if (uid > 0) { where += " AND A.UserID=" + uid; }
            PageSetting setting = PageSetting.Single(cpage, psize, TbName, PK, where, PK + " DESC", sp);
            DBCenter.SelPage(setting);
            return setting;
        }
        public DataTable Search(string ztype, string ids, string remind, int status, int uid)
        {
            List<SqlParameter> sp = new List<SqlParameter>();
            string where = " 1=1 ";
            if (!string.IsNullOrEmpty(ztype)) { where += " AND A.ZType=@ztype"; sp.Add(new SqlParameter("ztype", ztype)); }
            if (!string.IsNullOrEmpty(remind)) { where += " AND A.Remind=@remind"; sp.Add(new SqlParameter("remind", remind)); }
            if (status != -100) { where += " AND A.Status=" + status; }
            if (uid > 0) { where += " AND A.UserID=" + uid; }
            if (!string.IsNullOrEmpty(ids)) { SafeSC.CheckIDSEx(ids); where += " AND A.ID IN (" + ids + ")"; }
            return DBCenter.JoinQuery("A.*,B.UserFace", TbName, "ZL_User_PlatView", "A.UserID=B.UserID", where, "A.ID DESC", sp.ToArray());
        }
        //------------------Packed
        public DataTable JoinComp_Sel(string compid, int status, string ids = "")
        {
            return Search("plat_joincomp", ids, compid, status, 0);
        }
        public bool JoinComp_Exist(int uid)
        {
            return Search("plat_joincomp", "", "", (int)ZLEnum.ConStatus.UnAudit, uid).Rows.Count > 0;
        }
        public DataTable CompCert_Sel(int status, string ids = "", int uid = 0)
        {
            return Search("plat_compcert", ids, "", status, uid);
        }
        public bool CompCert_Exist(int uid)
        {
            return Search("plat_compcert", "", "", (int)ZLEnum.ConStatus.UnAudit, uid).Rows.Count > 0;
        }
        public bool IsExist(string plat, int uid)
        {
            return Search(plat, "", "", (int)ZLEnum.ConStatus.UnAudit, uid).Rows.Count > 0;
        }
        //------------------Operation
        /// <summary>
        /// 同意或拒绝用户
        /// </summary>
        public void ChangeByIDS(string ids, int status, string adminRemind = "")
        {
            if (string.IsNullOrEmpty(ids)) { return; }
            SafeSC.CheckIDSEx(ids);
            List<SqlParameter> sp = new List<SqlParameter>() {
                new SqlParameter("remind", adminRemind),
                new SqlParameter("date",DateTime.Now.ToString())
            };
            DBCenter.UpdateSQL(TbName, "Status=" + status + ",AdminRemind=@remind,AuditDate=@date", "ID IN (" + ids + ")", sp);
        }
    }
}
