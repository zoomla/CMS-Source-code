using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ZoomLa.BLL.Plat;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Model.Plat;
using ZoomLa.SQLDAL;


namespace ZoomLa.BLL
{
    public class B_User_Plat : ZL_Bll_InterFace<M_User_Plat>
    {
        string TbName = "ZL_User_Plat", TbView = "ZL_User_PlatView";
        string PK = "UserID";
        M_User_Plat initMod = new M_User_Plat();
        B_User buser = new B_User();
        public void SyncUB(M_User_Plat model)
        {
            M_Uinfo ubMod = buser.GetUserBaseByuserid(model.UserID);
            ubMod.UserId = model.UserID;
            ubMod.Position = model.Post;
            ubMod.Mobile = model.Mobile;
            ubMod.UserFace = model.UserFace;
            ubMod.TrueName = model.TrueName;
            if (ubMod.IsNull)
            {
                buser.AddBase(ubMod);
            }
            else
            {
                buser.UpdateBase(ubMod);
            }
        }
        public int Insert(M_User_Plat model)
        {
            SyncUB(model);
            model.PK = "";
            return Sql.insert(TbName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        public bool UpdateByID(M_User_Plat model)
        {
            SyncUB(model);
            return Sql.UpdateByIDs(TbName, PK, model.UserID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
        //需改为枚举,0:未审核,-1离职,1:正常
        public bool UpdateStatus(string ids, int status)
        {
            SafeSC.CheckDataEx(ids);
            string sql = "Update " + TbName + " Set Status=" + status + " Where UserID in(" + ids + ")";
            return SqlHelper.ExecuteSql(sql);
        }
        public bool Del(int ID)
        {
            string sql = "Delete From " + TbName + " Where UserID=" + ID;
            return SqlHelper.ExecuteSql(sql);
        }
        public M_User_Plat SelReturnModel(int ID)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(TbView, PK, ID))
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
        //用于限定只能读取本公司的用户
        public M_User_Plat SelReturnModel(int id, int compid)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(TbView, " Where UserID=" + id + " And CompID=" + compid))
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
            return Sql.Sel(TbView);
        }
        /// <summary>
        /// 依据公司ID筛选
        /// </summary>
        /// 公司ID，组ID，关键词
        public DataTable SelByCompany(int compID, string key = "")
        {
            string sql = "Select * From " + TbView + " Where CompID=" + compID;
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("key", "%" + key + "%") };
            if (!string.IsNullOrEmpty(key))
            {
                sql += " And HoneyName Like @key";
            }
            return SqlHelper.ExecuteTable(CommandType.Text, sql, sp);
        }
        public DataTable SelUserFaceDT(int compID)
        {
            string sql = "Select UserID,TrueName,UserFace From " + TbView + " Where CompID=" + compID;
            return SqlHelper.ExecuteTable(CommandType.Text, sql);
        }
        /// <summary>
        /// 用于支持@功能
        /// </summary>
        public DataTable SelByCompWithAT(int compID, string key = "")
        {
            string sql = "Select UserID as id,suffix='',TrueName as name,UserFace as imageUrl,[Type]='User' From " + TbView + " Where CompID=" + compID + " AND UserStatus=0";
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("key", "%" + key + "%") };
            if (!string.IsNullOrEmpty(key))
            {
                sql += " And TrueName Like @key";
            }
            DataTable dt = SqlHelper.ExecuteTable(CommandType.Text, sql, sp);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //dt.Rows[i]["imageUrl"] = "<img src=\"" + dt.Rows[i]["imageUrl"] + "'\" onerror=\"this.src='/Images/userface/noface.gif\'\" width='25' style='width:25px;height:25px;'>";
                dt.Rows[i]["suffix"] = "[uid:" + dt.Rows[i]["id"] + "]";
            }
            return dt;
        }
        public DataTable SelByIDS(string ids)
        {
            ids = ids.Trim(',').Replace(",,", ",");
            if (string.IsNullOrEmpty(ids)) return null;
            SafeSC.CheckIDSEx(ids);
            string sql = "Select * From " + TbView + " Where UserID in(" + ids + ")";
            return SqlHelper.ExecuteTable(CommandType.Text, sql);
        }
        /// <summary>
        /// 获取该组下的成员
        /// </summary>
        public DataTable SelByGroup(int compid, int gid)
        {
            string sql = "Select * From " + TbView + " Where CompID=" + compid;
            if (gid == 0)//全部
            {

            }
            else if (gid == -1)//未分组
            {
                sql += " And Gid IS NULL OR Gid=''";
            }
            else
            {
                sql += " And Gid Like '%," + gid + ",%'";
            }
            return SqlHelper.ExecuteTable(CommandType.Text, sql);
        }
        /// <summary>
        /// 传入组IDS,返回这些组下面的所有成员IDS
        /// </summary>
        public string SelByGIDS(string gids)
        {
            if (string.IsNullOrEmpty(gids)) return "";
            SafeSC.CheckIDSEx(gids);
            string gsql = "", result = "";
            foreach (string gid in gids.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                gsql += " Gid Like '%," + gid + ",%' OR";
            }
            gsql = gsql.TrimEnd(new char[] { 'O', 'R' });
            string sql = "Select UserID From " + TbView + " Where" + gsql;
            DataTable dt = SqlHelper.ExecuteTable(CommandType.Text, sql);
            foreach (DataRow dr in dt.Rows)
            {
                result += dr["UserID"] + ",";
            }
            return result.TrimEnd(',');
        }
        public DataTable SelUFaceByIDS(string ids)
        {
            SafeSC.CheckIDSEx(ids);
            string sql = "Select UserID,TrueName,UserFace From " + TbView + " Where UserID IN(" + ids + ")";
            return SqlHelper.ExecuteTable(CommandType.Text, sql);
        }
        public string SelInfoByIDS(string ids, int type = 1)
        {
            ids = ids.Trim(',');
            if (string.IsNullOrEmpty(ids)) return "";
            SafeSC.CheckIDSEx(ids);
            string sql = "SELECT CAST({0} as varchar(30))+',' FROM " + TbName + "  Where UserID IN(" + ids + ") For XML Path('')";
            switch (type)
            {
                case 1://TrueName
                    sql = string.Format(sql, "TrueName");
                    break;
                case 2:
                    sql = string.Format(sql, "UserName");
                    break;
            }
            return SqlHelper.ExecuteScalar(CommandType.Text, sql).ToString();
        }
        public void AddAtCount(string uids)
        {
            string sql = "Update " + TbName + " Set AtCount=AtCount+1 Where UserID in(" + uids + ")";
            SqlHelper.ExecuteSql(sql);
        }
        public void RemoveAtCount(int uid)
        {
            string sql = "Update " + TbName + " Set AtCount=0 Where UserID=" + uid;
            SqlHelper.ExecuteSql(sql);
        }
        public static M_User_Plat GetLogin()
        {
            return GetLogin(true);
        }
        public static M_User_Plat GetLogin(bool flag)
        {
            B_User buser = new B_User();
            B_User_Plat upBll = new B_User_Plat();
            return upBll.SelReturnModel(buser.GetLogin(flag).UserID);
        }
        /// <summary>
        /// 0:非Plat用户,1:未审核用户,2:已离职用户,99:正常用户
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public static int IsPlatUser(int uid)
        {
            B_User_Plat upBll = new B_User_Plat();
            M_User_Plat upMod = upBll.SelReturnModel(uid);
            int result = 99;
            if (upMod == null)
            {
                result = 0;
            }
            else if (upMod.Status != 1)
            {
                result = 1;
            }
            return result;
        }
        public static int IsPlatUser()
        {
            B_User buser = new B_User();
            return IsPlatUser(buser.GetLogin().UserID);
        }
        /// <summary>
        /// 是否为 能力中心--用户管理员
        /// </summary>
        /// <returns></returns>
        public static bool IsAdmin()//是否为用户管理员
        {
            M_User_Plat upMod = GetLogin();
            string rid = "," + B_Plat_UserRole.SelSuperByCid(upMod.CompID) + ",";
            return upMod.Plat_Role.Contains(rid);
        }
        /// <summary>
        /// 检测当前登录用户是否有对应的权限
        /// </summary>
        public static bool AuthCheck(string authname)
        {
            if (B_User_Plat.IsAdmin())
            { return true; }
            else
            {
                B_Plat_UserRole urBll = new B_Plat_UserRole();
                return urBll.AuthCheck(B_User_Plat.GetLogin().Plat_Role, authname);
            }
        }
        public bool DelByIDS(string ids)
        {
            SafeSC.CheckIDSEx(ids);
            string sql = "DELETE FROM " + TbName + " WHERE UserID IN (" + ids + ")";
            return SqlHelper.ExecuteSql(sql);
        }
        public M_User_Plat SelByNameAndPwd(string uname, string upwd, bool isMd5 = false)
        {
            B_User buser = new B_User();
            B_User_Plat upBll = new B_User_Plat();
            return upBll.SelReturnModel(buser.LoginUser(uname, upwd, isMd5).UserID);
        }
        //-------------------------Logical
        /// <summary>
        /// 公司移除用户逻辑
        /// </summary>
        public void Comp_RemoveUser(int uid)
        {
            B_Plat_Comp compBll = new B_Plat_Comp();
            M_Plat_Comp compMod = compBll.SelModelByCUser(uid);
            M_User_Plat upMod = SelReturnModel(uid);
            if (upMod != null)
            {
                //如无创建人为自己的公司,则新建
                if (compMod == null) { compMod = compBll.CreateByUser(upMod); };
                //更改用户与公司状态(重新成为个人平台)
                //upMod.Status = -1;
                upMod.CompID = compMod.ID;
                UpdateByID(upMod);
            }
        }
        public M_User_Plat NewUser(M_UserInfo mu)
        {
            M_User_Plat upMod = new M_User_Plat();
            upMod.UserID = mu.UserID;
            upMod.UserFace = mu.UserFace;
            upMod.UserName = mu.UserName;
            upMod.TrueName = mu.HoneyName;
            upMod.UserPwd = mu.UserPwd;
            upMod.Status = 1;
            return upMod;
        }
        public M_User_Plat NewUser(M_UserInfo mu,M_Plat_Comp compMod) 
        {
            M_User_Plat upMod = new M_User_Plat();
            upMod.UserID = mu.UserID;
            upMod.UserFace = mu.UserFace;
            upMod.UserName = mu.UserName;
            upMod.TrueName = mu.HoneyName;
            upMod.UserPwd = mu.UserPwd;
            upMod.Status = 1;
            //--------------------------
            upMod.CompID = compMod.ID;
            upMod.CompName = compMod.CompName;
            upMod.CreateTime = DateTime.Now;
            return upMod;
        }
        /// <summary>
        /// 用于创建公司,审核认证
        /// </summary>
        public void NewByUserDT(M_Plat_Comp compMod, DataTable userDT)
        {
            B_Plat_Group gpBll = new B_Plat_Group();
            for (int i = 0; i < userDT.Rows.Count; i++)
            {
                string gname = userDT.Rows[i]["gname"].ToString();
                string uname = userDT.Rows[i]["uname"].ToString();
                string honey = userDT.Rows[i]["honey"].ToString();
                if (string.IsNullOrEmpty(uname)) { continue; }
                M_UserInfo newmu = buser.NewUser(uname, "111111");
                newmu.HoneyName = honey;
                newmu.UserID = buser.Add(newmu);
                M_Uinfo basemu = buser.NewBase(newmu);
                buser.AddBase(basemu);
                //----能力相关信息
                M_Plat_Group gpMod = gpBll.NewGroup(gname, compMod.ID, newmu.UserID);
                M_User_Plat upMod = NewUser(newmu, compMod);
                gpMod.ID = gpBll.Insert(gpMod);
                upMod.Gid = gpMod.ID.ToString();
                Insert(upMod);
            }
        }
        //------------------Tools
        public static string WordFace(object userid, string css, params string[] unames)
        {
            int uid = DataConvert.CLng(userid);
            string name = B_User.GetUserName(unames);
            name = name.Length > 0 ? name.Substring(0, 1) : "匿";
            string[] colorArr = "0094ff,FE7906,852b99,74B512,4B7F8C,00CCFF,A43AE3,22AFC2,F874A4,D0427C".Split(',');
            string color = "#" + colorArr[(uid % colorArr.Length)];
            return "<div class=\"uword " + css + "\" data-uid=\"" + uid + "\" style=\"background-color:" + color + "\">" + name + "</div>";
        }
    }
}
