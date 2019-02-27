using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ZoomLa.SQLDAL;
using ZoomLa.Model.User;
using System.Data.Common;
using ConStatus = ZoomLa.Model.ZLEnum.ConStatus;
using System.Data.SqlClient;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLa.BLL.User
{
    public class B_User_Friend : B_Base<M_User_Friend>
    {
        private string TbName, PK;
        private M_User_Friend initMod = new M_User_Friend();
        public B_User_Friend()
        {
            TbName = initMod.TbName;
            PK = initMod.PK;
        }
        public int Insert(M_User_Friend model)
        {
            if (IsFriend(model.UserID, model.TUserID)) { return 0; }
            if (model.UserID < 1 || model.TUserID < 1) { return 0; }
            if (model.UserID == model.TUserID) { return 0; }
            return DBCenter.Insert(model);
        }
        public bool UpdateByID(M_User_Friend model)
        {
            return DBCenter.UpdateByID(model, model.ID);
        }
        public bool Del(int ID)
        {
            return DBCenter.Del(TbName, PK, ID);
        }
        public M_User_Friend SelReturnModel(int ID)
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
        //-----------User操作
        public DataTable SelMyFriend(int uid, string skey = "")
        {
            skey = skey.Replace(" ", "");
            string where = "(UserID=" + uid + " OR TUserID =" + uid + ") AND ZStatus=" + (int)ConStatus.Audited;
            DataTable dt = DBCenter.SelWithField(TbName, "UserID,TUserID", where);
            string uids = "";
            foreach (DataRow dr in dt.Rows)
            {
                if (DataConvert.CLng(dr["UserID"]) == uid) { uids += dr["TUserID"] + ","; }
                else { uids += dr["UserID"] + ","; }
            }
            uids = uids.TrimEnd(',');
            if (string.IsNullOrEmpty(uids)) return null;
            where = "UserID IN (" + uids + ")";
            List<SqlParameter> sp = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(skey)) { where += " AND UserName LIKE @skey"; sp.Add(new SqlParameter("skey", "%" + skey + "%")); }
            return DBCenter.SelWithField("ZL_User", "UserID,UserName,HoneyName,Salt", where, "RegTime DESC", sp);
        }
        public PageSetting SelMyFriend_SPage(int cpage, int psize, int uid, string skey)
        {
            string where = "(UserID=" + uid + " OR TUserID =" + uid + ") AND ZStatus=" + (int)ConStatus.Audited;
            DataTable dt = DBCenter.SelWithField(TbName, "UserID,TUserID", where);
            string uids = "";
            foreach (DataRow dr in dt.Rows)
            {
                if (DataConvert.CLng(dr["UserID"]) == uid) { uids += dr["TUserID"] + ","; }
                else { uids += dr["UserID"] + ","; }
            }
            uids = uids.TrimEnd(',');
            if (string.IsNullOrEmpty(uids)) return new PageSetting() { itemCount = 0, dt = new DataTable() };
            where = "UserID IN (" + uids + ")";
            List<SqlParameter> sp = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(skey)) { where += " AND UserName LIKE @skey";  sp.Add(new SqlParameter("skey", "%" + skey + "%")); }
            PageSetting setting = PageSetting.Single(cpage, psize, "ZL_User", "UserID", where, "RegTime DESC", sp, "UserID,UserName,HoneyName,Salt");
            DBCenter.SelPage(setting);
            return setting;
        }

        /// <summary>
        /// 双方是否为有效的好友,True:是,
        /// </summary>
        public bool IsFriend(int uid, int tuid)
        {
            string where = GetWhere(uid, tuid) + " AND ZStatus=" + (int)ConStatus.Audited;
            return DBCenter.IsExist(TbName, where);
        }
        /// <summary>
        /// 删除好友,则相当于双方都移除
        /// </summary>
        public void DelFriend(int uid, int tuid)
        {
            string where = GetWhere(uid, tuid);
            DBCenter.DelByWhere(TbName, where);
        }
        /// <summary>
        /// 添加新的好友
        /// </summary>
        public void AddFriendByApply(M_User_FriendApply applyMod)
        {
            M_User_Friend model = new M_User_Friend();
            model.UserID = applyMod.UserID;
            model.TUserID = applyMod.TUserID;
            model.ZStatus = (int)ConStatus.Audited;
            model.FType = 0;
            model.FGroupID = 0;
            Insert(model);
        }
        private string GetWhere(int uid, int tuid)
        {
            return "(UserID=" + uid + " AND TUserID=" + tuid + ") OR (UserID=" + tuid + " AND TUserID=" + uid + ")";
        }
    }
}
