using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;
using ZoomLa.Model.User;
using ZoomLa.SQLDAL;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLa.BLL.User
{
    public class B_User_InviteCode
    {
        private string PK, TbName = "";
        private M_User_InviteCode initMod = new M_User_InviteCode();
        public B_User_InviteCode()
        {
            PK = initMod.PK;
            TbName = initMod.TbName;
        }
        public int Insert(M_User_InviteCode model)
        {
            return DBCenter.Insert(model);
        }
        public bool UpdateByID(M_User_InviteCode model)
        {
            return DBCenter.UpdateByID(model, model.ID);
        }
        public bool Del(int ID)
        {
            return DBCenter.Del(TbName, PK, ID);
        }
        public void DelByIDS(string ids, int uid = 0)
        {
            if (string.IsNullOrEmpty(ids)) { return; }
            SafeSC.CheckIDSEx(ids);
            string where = " ID IN (" + ids + ") ";
            if (uid > 0) { where += " AND UserID=" + uid; }
            DBCenter.DelByWhere(TbName, where);
        }
        public M_User_InviteCode SelReturnModel(int ID)
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
        public M_User_InviteCode Code_SelModel(string code)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("code", code) };
            using (SqlDataReader reader = Sql.SelReturnReader(TbName, "Where code=@code", sp))
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
            return DBCenter.JoinQuery("A.*,B.GroupName", TbName, "ZL_Group", "A.JoinGroup=B.GroupID", "", PK + " DESC");
        }
        //-------------------------------Tools
        public bool Code_IsExist(string code)
        {
            string sql = "Select ID From " + TbName + " Where code=@code";
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("code", code) };
            return SqlHelper.ExecuteTable(CommandType.Text, sql, sp).Rows.Count > 0;
        }
        /// <summary>
        /// 获取我所拥有的邀请码
        /// </summary>
        public DataTable Code_Sel(int uid)
        {
            return DBCenter.Sel(TbName, "UserID=" + uid);
        }
        public PageSetting Code_SelPage(int cpage, int psize, int uid, string skey = "")
        {
            string where = "UserID=" + uid;
            List<SqlParameter> sp = new List<SqlParameter>();
            PageSetting setting = PageSetting.Single(cpage, psize, TbName, PK, where, "", sp);
            DBCenter.SelPage(setting);
            return setting;
        }
        /// <summary>
        /// 创建邀请码,model仅需指定创建人和所属人和将加入的组
        /// </summary>
        public bool Code_Create(int count, string format, M_User_InviteCode model)
        {
            model.Flow = DateTime.Now.ToString("yyyyMMddHHmmss") + function.GetRandomString(4);
            if (model.UserID < 1) { throw new Exception("未指定所属用户"); }
            for (int i = 0; i < count; i++)
            {
                model.Code = function.GetCodeByFormat(format);
                Insert(model);
            }
            return true;
        }
        /// <summary>
        /// 用户中心创建邀请码
        /// </summary>
        public bool Code_Create(int count, M_UserInfo mu)
        {
            M_User_InviteCode inviteMod = new M_User_InviteCode();
            inviteMod.CAdmin = 0;
            inviteMod.CUser = mu.UserID;
            inviteMod.UserID = mu.UserID;
            inviteMod.UserName = mu.UserName;
            inviteMod.JoinGroup = SiteConfig.UserConfig.InviteJoinGroup;
            return Code_Create(count, SiteConfig.UserConfig.InviteFormat, inviteMod);
        }
        /// <summary>
        /// 标明邀请号已使用,并记录使用人
        /// </summary>
        public bool Code_Used(int id, M_UserInfo mu)
        {
            List<SqlParameter> sp = new List<SqlParameter>() { new SqlParameter("UserName", mu.UserName), new SqlParameter("UserID", mu.UserID),new SqlParameter("UsedDate",DateTime.Now.ToString()) };
            return DBCenter.UpdateSQL(TbName, "ZStatus=99,UsedUserName=@UserName,UsedUserID=@UserID,UsedDate=@UsedDate", "ID=" + id, sp);
        }
    }
}
