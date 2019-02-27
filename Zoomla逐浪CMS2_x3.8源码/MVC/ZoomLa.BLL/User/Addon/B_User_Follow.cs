using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ZoomLa.Model.User;
using ZoomLa.SQLDAL;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLa.BLL.User
{
    public class B_User_Follow : ZL_Bll_InterFace<M_User_Follow>
    {
        private string PK, TbName = "";
        private M_User_Follow initMod = new M_User_Follow();
        public B_User_Follow()
        {
            PK = initMod.PK;
            TbName = initMod.TbName;
        }
        public int Insert(M_User_Follow model)
        {
            return DBCenter.Insert(model);
        }
        public bool UpdateByID(M_User_Follow model)
        {
            return DBCenter.UpdateByID(model, model.ID);
        }
        public bool Del(int ID)
        {
            return DBCenter.Del(TbName, PK, ID);
        }
        public M_User_Follow SelReturnModel(int ID)
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
        public DataTable Sel()
        {
            return DBCenter.Sel(TbName, "", PK + " DESC");
        }
        public PageSetting SelPage(int cpage, int psize)
        {
            PageSetting setting = PageSetting.Single(cpage, psize, TbName, PK, "");
            DBCenter.SelPage(setting);
            return setting;
        }
        //---------------
        public bool Add(M_User_Follow model, ref string err)
        {
            bool flag = false;
            if (model.UserID < 1) { err = "用户未登录"; }
            else if (model.TUserID < 1) { err = "被关注人不能为空"; }
            else if (model.UserID == model.TUserID) { err = "关注人与被关注人不能是同用户"; }
            else if (!new B_User().IsExist("uid", model.TUserID.ToString())) { err = "被关注用户不存在"; }
            else if (DBCenter.IsExist(TbName, "UserID=" + model.UserID + " AND TUserID=" + model.TUserID)) { err = "关注信息已存在"; }
            else { Insert(model); flag = true; }
            return flag;
        }
        public void Del(int suid, int tuid)
        {
            List<SqlParameter> sp = new List<SqlParameter>();
            string where = "UserID=" + suid + " AND TUserID=" + tuid;
            DBCenter.DelByWhere(TbName, where);
        }
        public bool Has(int suid, int tuid)
        {
            string where = "UserID=" + suid + " AND TUserID=" + tuid;
            return DBCenter.IsExist(TbName, where);
        }
        /// <summary>
        /// 我关注的用户
        /// </summary>
        public DataTable SelByUser(int suid, string skey = "")
        {
            string where = "A.UserID=" + suid;
            List<SqlParameter> sp = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(skey)) { where += " AND (B.UserName LIKE @skey OR B.HoneyName LIKE @skey)"; sp.Add(new SqlParameter("skey", "%" + skey + "%")); }
            return DBCenter.JoinQuery("A.*,B.UserName,B.HoneyName,B.salt", TbName, "ZL_User", "A.TUserID=B.UserID", where, PK + " DESC", sp.ToArray());
        }
        public PageSetting SelByUser_SPage(int cpage, int psize, int suid, string skey = "")
        {
            string where = "A.UserID=" + suid;
            List<SqlParameter> sp = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(skey)) { where += " AND (B.UserName LIKE @skey OR B.HoneyName LIKE @skey)"; sp.Add(new SqlParameter("skey", "%" + skey + "%")); }
            PageSetting setting = PageSetting.Double(cpage, psize, TbName, "ZL_User", "A." + PK, "A.TUserID=B.UserID", where, "A." + PK + " DESC", sp, "A.*,B.UserName,B.HoneyName,B.salt");
            DBCenter.SelPage(setting);
            return setting;
        }
        /// <summary>
        /// 关注我的用户
        /// </summary>
        public DataTable SelByTUser(int tuid, string skey = "")
        {
            string where = "A.TUserID=" + tuid;
            List<SqlParameter> sp = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(skey)) { where += " AND (B.UserName LIKE @skey OR B.HoneyName LIKE @skey)"; sp.Add(new SqlParameter("skey", "%" + skey + "%")); }
            return DBCenter.JoinQuery("A.*,B.UserName,B.HoneyName,B.salt", TbName, "ZL_User", "A.UserID=B.UserID", where, PK + " DESC", sp.ToArray());
        }
        public PageSetting SelByTUser_SPage(int cpage, int psize, int tuid, string skey = "")
        {
            string where = "A.TUserID=" + tuid;
            List<SqlParameter> sp = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(skey)) { where += " AND (B.UserName LIKE @skey OR B.HoneyName LIKE @skey)"; sp.Add(new SqlParameter("skey", "%" + skey + "%")); }
            PageSetting setting = PageSetting.Double(cpage, psize, TbName, "ZL_User", "A." + PK, "A.UserID=B.UserID", where, "A." + PK + " DESC", sp, "A.*,B.UserName,B.HoneyName,B.salt");
            DBCenter.SelPage(setting);
            return setting;
        }
        public bool DelByIDS(string ids)
        {
            SafeSC.CheckIDSEx(ids);
            return DBCenter.DelByIDS(TbName, PK, ids);
        }
    }
}
