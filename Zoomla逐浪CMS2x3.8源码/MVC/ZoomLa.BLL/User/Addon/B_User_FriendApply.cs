using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using ZoomLa.Model.User;
using ZoomLa.SQLDAL;
using ZoomLa.SQLDAL.SQL;
using ConStatus = ZoomLa.Model.ZLEnum.ConStatus;

namespace ZoomLa.BLL.User
{
    public class B_User_FriendApply : B_Base<M_User_FriendApply>
    {
        private string TbName, PK;
        private M_User_FriendApply initMod = new M_User_FriendApply();
        public B_User_FriendApply()
        {
            TbName = initMod.TbName;
            PK = initMod.PK;
        }
        public int Insert(M_User_FriendApply model)
        {
            return DBCenter.Insert(model);
        }
        /// <summary>
        /// 是否已存在未确认的好友申请,True已存在
        /// </summary>
        public bool IsExist(int suid, int tuid)
        {
            string where = "UserID=" + suid + " AND TUserID=" + tuid + " AND ZStatus=" + (int)ConStatus.UnAudit;
            return DBCenter.IsExist(TbName, where);
        }
        public bool UpdateByID(M_User_FriendApply model)
        {
            return DBCenter.UpdateByID(model, model.ID);
        }
        public bool Del(int ID)
        {
            return DBCenter.Del(TbName, PK, ID);
        }
        public M_User_FriendApply SelReturnModel(int ID)
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
        //----------------------用户操作
        /// <summary>
        /// 我收到的未处理的好友申请
        /// </summary>
        public DataTable SelMyReceApply(int uid)
        {
            string where = "A.TUserID=" + uid + " AND A.ZStatus=" + (int)ConStatus.UnAudit;
            return DBCenter.JoinQuery("A.*,B.Salt,B.HoneyName,B.UserName", TbName, "ZL_User", "A.UserID=B.UserID", where, "A.CDATE DESC");
        }
        /// <summary>
        /// 我收到的未处理的好友申请
        /// </summary>
        public PageSetting SelMyReceApply_SPage(int cpage, int psize, int uid)
        {
            string where = "A.TUserID=" + uid + " AND A.ZStatus=" + (int)ConStatus.UnAudit;
            PageSetting setting = PageSetting.Double(cpage, psize, TbName, "ZL_User", "A." + PK, "A.UserID=B.UserID", where, "", null, "A.*,B.Salt,B.HoneyName,B.UserName");
            DBCenter.SelPage(setting);
            return setting;
        }
        /// <summary>
        /// 我发送的好友申请
        /// </summary>
        public DataTable SelMySendApply(int uid)
        {
            string where = "A.UserID=" + uid;
            return DBCenter.JoinQuery("A.*,B.Salt,B.HoneyName,B.UserName", TbName, "ZL_User", "A.TUserID=B.UserID", where, "A.CDATE DESC");
        }
        /// <summary>
        /// 我发送的好友申请
        /// </summary>
        public PageSetting SelMySendApply_SPage(int cpage, int psize, int uid)
        {
            PageSetting setting = PageSetting.Double(cpage, psize, TbName, "ZL_User", "A." + PK, "A.TUserID=B.UserID", "A.UserID=" + uid, "", null, "A.*,B.Salt,B.HoneyName,B.UserName");
            DBCenter.SelPage(setting);
            return setting;
        }
        /// <summary>
        /// 确认或拒绝好友申请
        /// </summary>
        public void SureApply(int id, ConStatus status)
        {
            B_User_Friend friBll = new B_User_Friend();
            switch (status)
            {
                case ConStatus.Reject:
                    break;
                case ConStatus.Audited://通过申请,将其加为好友

                    M_User_FriendApply model = SelReturnModel(id);
                    if (!friBll.IsFriend(model.UserID, model.TUserID))
                    {
                        friBll.Insert(new M_User_Friend()
                        {
                            UserID = model.UserID,
                            TUserID = model.TUserID,
                            ZStatus = (int)ConStatus.Audited,
                            FType = 0
                        });
                    }
                    break;
            }
            DBCenter.UpdateSQL(TbName, "ZStatus=" + (int)status, "ID=" + id);
        }
    }
}
