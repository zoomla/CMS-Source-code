namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Web;
    using ZoomLa.BLL.Helper;
    using ZoomLa.Components;
    using ZoomLa.Common;
    using ZoomLa.Model;
    using ZoomLa.SQLDAL;
    using SQLDAL.SQL;
    using System.Collections.Generic;
    using System.Data.Common;
    using Newtonsoft.Json;
    public class B_User
    {
        private string strTableName = "ZL_User", PK = "UserID";
        private string strTableName2 = "ZL_UserBase";
        private M_UserInfo initMod = new M_UserInfo();
        M_UserBaseField fieldMod = new M_UserBaseField();
        M_Uinfo uinfoMod = new M_Uinfo();
        HttpContext curReq;
        public string SessionID
        {
            get
            {
                //if (!string.IsNullOrEmpty(curReq.Request["openid"])) { return curReq.Request["openid"]; }
                //else { return curReq.Session.SessionID; }
                return curReq.Session.SessionID;
            }
        }
        public B_User()
        {
            curReq = HttpContext.Current;
        }
        public B_User(HttpContext current)
        {
            curReq = current;
        }
        //-------------Insert
        public int Add(M_UserInfo model)
        {
            return Adds(model);
        }
        public int AddModel(M_UserInfo model)
        {
            return Adds(model);
        }
        //添加会员信息
        private int Adds(M_UserInfo model)
        {
            model.UserName = model.UserName.Replace(" ", "");
            model.Email = model.Email.Replace(" ", "");
            if (string.IsNullOrEmpty(model.Question)) { model.Question = function.GetRandomString(10); }
            if (string.IsNullOrEmpty(model.Answer)) { model.Answer = function.GetRandomString(6); }
            if (string.IsNullOrEmpty(model.UserPwd) || string.IsNullOrEmpty(model.UserName)) throw new Exception("用户名与密码不能为空!");
            if (!SafeSC.CheckUName(model.UserName)) throw new Exception("用户名含有非法字符!");
            if (IsExistUName(model.UserName)) throw new Exception("用户名" + model.UserName + "已存在!");
            if (!string.IsNullOrEmpty(model.ParentUserID)) { bool flag = IsExit(DataConvert.CLng(model.ParentUserID)); if (!flag) model.ParentUserID = ""; }
            try
            {
                //model.SiteID = function.StrToASCII(function.GetpyChar(model.UserName).ToUpper()[0].ToString());//获取首字母
                model.PayPassWord = curReq.Request.RawUrl;
            }
            catch { }
            //if (ZoomLa.SQLDAL.DBHelper.View_IsExist("ZL_User"))//已开启站群整合
            //{
            //    Sql.insert("ZL_User", info.GetParameters(info), BLLCommon.GetParas(info), BLLCommon.GetFields(info));
            //    M_UserInfo mu = GetUserIDByUserName(info.UserName);
            //    return mu.UserID;
            //}
            //else
            //{
            //    return Sql.insertID("ZL_User", info.GetParameters(info), BLLCommon.GetParas(info), BLLCommon.GetFields(info));
            //}
            return DBCenter.Insert(model);
        }
        //-------------SELECT
        public DataTable Sel()
        {
            return DBCenter.Sel(strTableName);
        }
        /// <summary>
        /// 返回所有会员，并包含真实名称，会员组
        /// </summary>
        public DataTable SelAll()
        {
            //string sql = "Select u.*,g.GroupName,ub.Mobile,ub.TrueName,ub.UserFace From ZL_User as u Left Join ZL_Group as g On u.GroupID=g.GroupID Left Join ZL_UserBase as ub On u.UserID=ub.UserID";
            return DBCenter.JoinQuery(new PageSetting()
            {
                fields = "A.*,B.GroupName",
                t1 = strTableName,
                t2 = "ZL_Group",
                on = "A.GroupID=B.GroupID",
            });
        }
        /// <summary>
        /// 抽取会员附带组名,并可忽略部分会员
        /// </summary>
        public DataTable GetUserByGroupI(int GroupID, string ids = "")
        {
            PageSetting setting = new PageSetting();
            setting.fields = "A.*,B.GroupName";
            setting.t1 = strTableName;
            setting.t2 = "ZL_Group";
            setting.on = "A.GroupID=B.GroupID";
            setting.where = "A.GroupID=" + GroupID;
            if (!string.IsNullOrEmpty(ids))
            {
                SafeSC.CheckIDSEx(ids);
                setting.where += " AND A.UserID NOT IN(" + ids + ")";
            }
            return DBCenter.JoinQuery(setting);
        }
        //根据角色ID获取用户列表
        public DataTable SelUserByRole(int roleID)
        {
            return DBCenter.Sel(strTableName, "UserRole like '%," + roleID + ",%'");
        }
        //根据ids查询用户集合
        public DataTable SelectUserByIds(string ids)
        {
            ids = StrHelper.PureIDSForDB(ids);
            if (string.IsNullOrEmpty(ids) || ids.Contains("undefined")) return null;
            SafeSC.CheckIDSEx(ids);
            return DBCenter.JoinQuery("A.*,B.UserFace,B.TrueName", strTableName, strTableName2, "A.UserID=B.UserID", "A.UserID IN(" + ids + ")", "A.UserID DESC");
        }
        /// <summary>
        /// 仅返回少量数据,专用于配合选择用户
        /// </summary>
        public string SelByIDS(string ids)
        {
            ids = StrHelper.PureIDSForDB(ids);
            if (string.IsNullOrEmpty(ids) || ids.Contains("undefined")) return "";
            SafeSC.CheckIDSEx(ids);
            DataTable dt = DBCenter.SelWithField(strTableName, "UserID,UserName,HoneyName,Salt AS UserFace", "UserID IN(" + ids + ")");
            return JsonConvert.SerializeObject(dt);
            //return DBCenter.JoinQuery("A.UserID,A.UserName,A.salt AS UserFace,B.TrueName", strTableName, strTableName2, "A.UserID=B.UserID", "A.UserID IN(" + ids + ")", "A.UserID DESC");
        }
        /// <summary>
        /// 推广用户列表
        /// </summary>
        /// <returns></returns>
        public DataTable SelPromoUser(int puserid = 0, string search = "", string startdate = "", string enddate = "")
        {
            PageSetting setting = new PageSetting();
            setting.fields = "A.*,B.UserName AS PromoUser";
            setting.t1 = "ZL_User";
            setting.t2 = "ZL_User";
            setting.on = "A.ParentUserID=B.UserID";
            setting.order = "A.RegTime";
            setting.where = "A.ParentUserID>0";
            if (puserid > 0)
            {
                setting.where += " AND A.ParentUserID=" + puserid;
            }
            if (!string.IsNullOrEmpty(search))
            {
                setting.spList.Add(new SqlParameter("search", "%" + search + "%"));
                setting.where += " AND A.UserName LIKE @search";
            }
            if (!string.IsNullOrEmpty(startdate))
            {
                setting.spList.Add(new SqlParameter("startdate", startdate));
                setting.where += " AND A.RegTime>@startdate";
            }
            if (!string.IsNullOrEmpty(enddate))
            {
                setting.spList.Add(new SqlParameter("enddate", enddate));
                setting.where += " AND A.RegTime<@enddate";
            }
            setting.sp = setting.spList.ToArray();
            //string sql = "SELECT A.*,B.UserName AS PromoUser FROM ZL_User A LEFT JOIN ZL_User B ON A.ParentUserID=B.UserID WHERE A.ParentUserID>0"+strwhere+" ORDER BY A.RegTime";
            //return SqlHelper.ExecuteTable(sql,sp);
            return DBCenter.JoinQuery(setting);
        }
        /// <summary>
        /// 获取当前用户总数
        /// </summary>
        public int GetUserNameListTotal(string keyword = "")
        {
            string where = "";
            List<SqlParameter> splist = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(keyword))
            {
                where = "UserName LIKE @UserName";
                splist.Add(new SqlParameter("UserName", "%" + keyword + "%"));
            }
            return DBCenter.Count(strTableName, where, splist);
        }
        public int GetUserMobileCount()
        {
            return DBCenter.Count(strTableName2, "Mobile!='' AND Mobile IS NOT NULL");
        }
        /// <summary>
        /// 用于导出Excel和生成Html
        /// </summary>
        public DataTable GetUserBaseByuserid(string userid)
        {
            SafeSC.CheckIDSEx(userid);
            return DBCenter.JoinQuery("*", strTableName, strTableName2, "A.UserID=B.UserID", "B.UserID IN (" + userid + ")");
        }
        public DataTable GetNames(string username)
        {
            List<SqlParameter> sp = new List<SqlParameter>() { new SqlParameter("username", "%" + username + "%") };
            return DBCenter.Sel(strTableName, "UserName LIKE @username", "", sp);
        }
        /// <summary>
        /// 按成员角色查询用户
        /// </summary>
        /// <param name="enroll">成员角色 isteach:教师 isfamily:家长</param>
        public DataTable SelByGroupType(string enroll, string username = "")
        {
            string wherestr = "B.Enroll LIKE @enroll ";
            List<SqlParameter> splist = new List<SqlParameter>();
            splist.Add(new SqlParameter("enroll", "%," + enroll + ",%"));
            if (!string.IsNullOrEmpty(username))
            {
                wherestr += "AND A.UserName LIKE @name";
                splist.Add(new SqlParameter("name", "%" + username + "%"));
            }
            return DBCenter.JoinQuery("A.*,B.GroupName", strTableName, "ZL_Group", "A.GroupID=B.GroupID", wherestr, "", splist.ToArray());
        }
        public DataTable SelPage(int psize, int cpage, out int itemCount, string action, string value, string order, string charstr = "")
        {
            return SelPage(psize, cpage, out itemCount, action, value, order, charstr ,-100);
        }
        public DataTable SelPage(int psize, int cpage, out int itemCount, string action, string value, string order, string charstr, int status)
        {
            action = string.IsNullOrEmpty(action) ? "" : action.ToLower();
            value = string.IsNullOrEmpty(value) ? "" : value.ToLower();
            string where = "";
            string orderstr = "";
            switch (action)//筛选条件
            {
                case "groupid":
                    value = DataConvert.CLng(value).ToString();
                    where = "A.GroupID=@value";
                    break;
                case "struct"://组织结构
                    if (!string.IsNullOrEmpty(value))
                    { where = "A.StructureID LIKE @struct"; }
                    break;
                case "time":
                    DateTime st = DataConverter.CDate(value.Split('|')[0]);
                    DateTime et = DataConverter.CDate(value.Split('|')[1]);
                    where = "A.RegTime > '" + st.ToString("yyyy-MM-dd HH:mm") + "' And A.RegTime < '" + et.ToString("yyyy-MM-dd HH:mm") + "'";
                    break;
                case "userid"://会员名,ID,Email
                    value = DataConvert.CLng(value).ToString();
                    where = "A.UserID=@value";
                    break;
                case "username":
                    where = "A.UserName LIKE @uname";
                    break;
                case "email":
                    where = "A.Email LIKE @uname";
                    break;
                case "keyword":
                    where = "(A.Email LIKE @uname OR A.UserName LIKE @uname)";
                    break;
                case "puser":
                    where = " A.ParentUserID=@value";
                    break;
                case "honeyname":
                    where = "A.HoneyName LIKE @uname";
                    break;
                case "name":
                    where = "(A.UserName LIKE @uname OR A.HoneyName LIKE @uname)";
                    break;
            }
            if (!string.IsNullOrEmpty(charstr))
            {
                where += " AND A.SiteID=" + DataConvert.CLng(function.StrToASCII(charstr));
            }
            switch (order)
            {
                case "Addtime":
                    orderstr = "A.RegTime DESC";
                    break;
                case "AscAddtime":
                    orderstr = "A.RegTime ASC";
                    break;
                case "point":
                    orderstr = "A.UserExp DESC";
                    break;
                case "Ascpoint":
                    orderstr = "A.UserExp ASC";
                    break;
                case "descAddtime":
                    orderstr = "A.RegTime DESC";
                    break;
                case "AuthTime":
                    orderstr = "A.LastLoginTime DESC";
                    break;
                case "AscAuthTime":
                    orderstr = "A.LastLoginTime ASC";
                    break;
                case "ActiveTime":
                    orderstr = "A.LastActiveTime DESC";
                    break;
                case "EditPassTime":
                    orderstr = "A.LastPwdChangeTime DESC";
                    break;
                case "Purse":
                    orderstr = "A.Purse DESC";
                    break;
                case "AscPurse":
                    orderstr = "A.Purse ASC";
                    break;
                case "LoginTimes":
                    orderstr = "A.LoginTimes DESC";
                    break;
                case "AscLoginTimes":
                    orderstr = "A.LoginTimes ASC";
                    break;
                case "ID":
                    orderstr = "A.UserID DESC";
                    break;
                case "AscID":
                    orderstr = "A.UserID ASC";
                    break;
                default:
                    orderstr = "A.UserID DESC";
                    break;
            }
            if (status != -100) { where += " AND A.Status=" + status; }
            List<SqlParameter> spList = new List<SqlParameter>();
            if (where.Contains("@uname")) { spList.Add(new SqlParameter("uname", "%" + value + "%")); }
            if (where.Contains("@value")) { spList.Add(new SqlParameter("value", value)); }
            if (where.Contains("@struct")) { spList.Add(new SqlParameter("struct", "%," + value + ",%")); }
            //为兼容可改为视图
            //string mtable = "(SELECT U.*,UB.Mobile FROM ZL_User U LEFT JOIN ZL_UserBase UB ON U.UserID=UB.UserID)";
            PageSetting setting = new PageSetting()
            {
                psize = psize,
                cpage = cpage,
                fields = "A.*,B.GroupName",
                pk = "A.UserID",
                t1 = strTableName,
                t2 = "ZL_Group",
                on = "A.GroupID=B.GroupID",
                where = where,
                order = orderstr,
                sp = spList.ToArray()
            };
            DataTable dt = DBCenter.SelPage(setting);
            itemCount = setting.itemCount;
            return dt;
        }
        public bool IsExit(int userID)
        {
            return IsExist("uid", userID.ToString());
        }
        /// <summary>
        /// 用户名是否存在，用于注册等时判断
        /// </summary>
        public bool IsExistUName(string uname)
        {
            return IsExist("uname", uname);
        }
        //根据会员名判断身份证是否存在
        public bool IsExitcard(string idcard)
        {
            List<SqlParameter> sp = new List<SqlParameter>() { new SqlParameter("IDCard", idcard) };
            return DBCenter.IsExist(strTableName2, "IDCard=@IDCard", sp);
        }
        /// <summary>
        /// 邮箱是否存在,如为空也为true
        /// </summary>
        public bool IsExistMail(string email)
        {
            return IsExist("email", email);
        }
        /// <summary>
        /// 用于社会化登录,检测是否有同值的openID存在
        /// </summary>
        public bool IsExistByOpenID(string openID)
        {
            return IsExist("openid", openID);
        }
        /// <summary>
        /// 指定字段是否重复,为空也重复,true:重复
        /// openid,email,uname,mobile,uid
        /// </summary>
        public bool IsExist(string type, string val)
        {
            val = (val ?? "").Replace(" ", "");
            if (string.IsNullOrEmpty(type) || string.IsNullOrEmpty(val)) { return true; }
            List<SqlParameter> sp = new List<SqlParameter>() { new SqlParameter("val", val) };
            switch (type)
            {
                case "email":
                    return DBCenter.IsExist(strTableName, "Email=@val", sp);
                case "mobile":
                    return DBCenter.IsExist(strTableName2, "Mobile=@val", sp);
                case "uname":
                    return DBCenter.IsExist(strTableName, "UserName=@val", sp);
                case "uid":
                    return DBCenter.IsExist(strTableName, "UserID=@val", sp);
                case "openid":
                    return DBCenter.IsExist("ZL_UserApp", "AppUid=@val", sp);
                case "ume"://手机,用户名,邮箱
                    DataTable dt = DBCenter.JoinQuery("A.*,B.Mobile", strTableName, strTableName2, "A.UserID=B.UserID", "A.UserName=@val OR A.Email=@val OR B.Mobile=@val", "", sp.ToArray());
                    return dt.Rows.Count > 0;
                default:
                    throw new Exception("指定的验证方式不存在");
            }
        }
        //----------UPDATE
        public bool UpdateByID(M_UserInfo model) { ZLCache.ClearByIDS(model.UserID.ToString()); return UpDateUser(model); }
        public bool UpDateUser(M_UserInfo model)
        {
            DBCenter.UpdateByID(model, model.UserID);
            ZLCache.ClearByIDS(model.UserID.ToString());
            return true;
        }
        public bool UpdateGroupId(string ids, int groupId)
        {
            ids = StrHelper.PureIDSForDB(ids);
            SafeSC.CheckIDSEx(ids);
            DBCenter.UpdateSQL(strTableName, "GroupID=" + groupId, "UserID IN (" + ids + ")", null);
            ZLCache.ClearByIDS(ids);
            return true;
        }
        //开通云盘
        public bool UpdateIsCloud(int userid, int value)
        {
            UpdateField("IsCloud", value.ToString(), userid);
            return true;
        }
        public static void UpdateField(string field, string value, int uid, bool clear = true)
        {
            SafeSC.CheckDataEx(field);
            List<SqlParameter> sp = new List<SqlParameter>() { new SqlParameter("value", value) };
            DBCenter.UpdateSQL("ZL_User", field + " = @value", "UserID=" + uid, sp);
            if (clear) { ZLCache.ClearByIDS(uid); }
        }
        /// <summary>
        /// 批量审核,或禁用  0:启用,1:禁用
        /// </summary>
        public bool BatAudit(string ids, int type = 0)
        {
            SafeSC.CheckIDSEx(ids);
            List<SqlParameter> sp = new List<SqlParameter>();
            switch (type)
            {
                case 0://解锁用户
                    DBCenter.UpdateSQL(strTableName, "Status=0", "UserID IN(" + ids + ")", null);
                    break;
                case 1://锁定用户
                    sp.Add(new SqlParameter("LastLockTime", DateTime.Now));
                    DBCenter.UpdateSQL(strTableName, "Status=1,LastLockTime=@LastLockTime", "UserID IN(" + ids + ")", sp);
                    break;
                case 2://认证店铺
                    sp.Add(new SqlParameter("EDate", DateTime.Now.AddYears(3)));
                    DBCenter.UpdateSQL(strTableName, "State=2,CerificateDeadLine=@EDate", "UserID IN(" + ids + ")", sp);
                    break;
                case 3: //取消认证
                    DBCenter.UpdateSQL(strTableName, "State=0", "UserID IN(" + ids + ")", null);
                    break;
            }
            ZLCache.ClearByIDS(ids);
            return true;
        }
        //-----------------------------------Delete
        public bool DelModelInfoAllo(string TableName, string ids)
        {
            SafeSC.CheckIDSEx(ids);
            DBCenter.DelByIDS(TableName, "ID", ids);
            return true;
        }
        public bool DelUserById(int userID)
        {
            DBCenter.Del(strTableName, PK, userID);
            DBCenter.Del(strTableName2, PK, userID);
            return true;
        }
        //-----------------------------------直接获取
        public M_UserInfo SeachByID(int uid) { return SelReturnModel(uid); }
        public M_UserInfo GetSelect(int uid)
        {
            return SelReturnModel(uid);
        }
        public M_UserInfo GetUserByUserID(int UserID)
        {
            return SelReturnModel(UserID);
        }
        public M_UserInfo SelReturnModel(int id)
        {
            if (id < 1) { return new M_UserInfo(true); }
            using (DbDataReader reader = DBCenter.SelReturnReader(strTableName, PK, id))
            {
                if (reader.Read())
                {
                    return initMod.GetModelFromReader(reader);
                }
                else
                {
                    return new M_UserInfo(true);
                }
            }
        }
        public M_UserInfo GetSelectByEmail(string email)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("Email", email) };
            using (DbDataReader reader = DBCenter.SelReturnReader(strTableName, "Email=@Email", sp))
            {
                if (reader.Read())
                    return initMod.GetModelFromReader(reader);
                else
                    return new M_UserInfo(true);
            }
        }
        //仅用于能力中心,注册用户时使用,彼时，该字段存校验码
        public M_UserInfo GetSelectByRemark(string remark)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("Remark", remark) };
            using (DbDataReader reader = DBCenter.SelReturnReader(strTableName,
               "Remark=@Remark", sp))
            {
                if (reader.Read())
                    return initMod.GetModelFromReader(reader);
                else
                    return new M_UserInfo(true);
            }
        }
        public M_UserInfo GetUserIDByUserName(string username)
        {
            return GetUserByName(username);
        }
        public M_UserInfo GetUserByName(string username)
        {
            return GetUserByName(username,"");
        }
        public M_UserInfo GetUserByName(string username, string upwd)
        {
            List<SqlParameter> sp = new List<SqlParameter>() { new SqlParameter("uname", username) };
            string where = "WHERE UserName=@uname";
            if (!string.IsNullOrEmpty(upwd)) { sp.Add(new SqlParameter("upwd", upwd)); where += " AND UserPwd=@upwd"; }
            using (SqlDataReader reader = Sql.SelReturnReader(strTableName, where, sp.ToArray()))
            {
                if (reader.Read())
                {
                    return initMod.GetModelFromReader(reader);
                }
                else
                {
                    return new M_UserInfo(true);
                }
            }
        }
        public M_UserInfo GetUserByWorkNum(string worknum)
        {
            if (string.IsNullOrWhiteSpace(worknum)) { return new M_UserInfo(true); }
            string strSql = "select * from ZL_User where WorkNum=@worknum";
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("worknum", worknum) };
            using (SqlDataReader rdr = SqlHelper.ExecuteReader(CommandType.Text, strSql, sp))
            {
                if (rdr.Read())
                {
                    return initMod.GetModelFromReader(rdr);
                }
                else
                {
                    return new M_UserInfo(true);
                }
            }
        }
        public M_UserInfo GetUserByUME(string uname)
        {
            M_UserInfo mu = new M_UserInfo(true);
            uname = (uname ?? "").Replace(" ", "");
            if (string.IsNullOrEmpty(uname)) { return mu; }
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("uname", uname) };
            DataTable dt = DBCenter.JoinQuery("A.*,B.Mobile", strTableName, strTableName2, "A.UserID=B.UserID", "A.UserName=@uname OR A.Email=@uname OR B.Mobile=@uname", "", sp);
            if (dt.Rows.Count > 0) { mu = mu.GetModelFromReader(dt.Rows[0]); }
            return mu;
        }
        //-----------------------------------前端登录
        public M_UserInfo LoginUser(string username, string userpwd, bool ismd5 = false)
        {
            return Authenticate(username, userpwd, "username", ismd5);
        }
        public M_UserInfo AuthenticateUser(string uname, string upwd, bool ismd5 = false)
        {
            return Authenticate(uname, upwd, "username", ismd5);
        }
        public M_UserInfo GetUserByWorkNum(string worknum, string pwd)
        {
            return Authenticate(worknum, pwd, "worknum", true);
        }
        public M_UserInfo AuthenticateEmail(string email, string upwd)
        {
            return Authenticate(email, upwd, "email");
        }
        public M_UserInfo AuthenticateID(int UserID, string upwd)
        {
            return Authenticate(UserID.ToString(), upwd, "userid");
        }
        public M_UserInfo AuthenByMobile(string mobile, string upwd)
        {
            return Authenticate(mobile, upwd, "mobile");
        }
        /// <summary>
        /// 同时校验用户名,邮箱,手机号,任一皆可登录
        /// </summary>
        public M_UserInfo AuthenByUME(string uname, string upwd)
        {
            return Authenticate(uname, upwd, "ume");
        }
        /// <summary>
        /// 用户登录,支持ID,Email,用户名
        /// userid,email,username,worknum,mobile,ume
        /// </summary>
        private M_UserInfo Authenticate(string uname, string upwd, string type, bool ismd5 = false)
        {
            M_UserInfo mu = new M_UserInfo(true);
            if (string.IsNullOrEmpty(uname) || string.IsNullOrEmpty(upwd)) { return mu; }
            if (!ismd5) { upwd = StringHelper.MD5(upwd); }
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("uname", uname), new SqlParameter("upwd", upwd) };
            string where = "UserPwd=@upwd AND ";
            switch (type)
            {
                case "userid":
                    where += "UserID=@uname";
                    break;
                case "email":
                    where += "Email=@uname";
                    break;
                case "worknum":
                    where += "WorkNum=@uname";
                    break;
                case "mobile":
                case "ume":
                    break;
                case "username":
                default:
                    where += "UserName=@uname";
                    break;
            }
            if (type.Equals("mobile"))
            {
                DataTable dt = SqlHelper.JoinQuery("TOP 1 A.*,B.Mobile", strTableName, strTableName2, "A.UserID=B.UserID", "B.Mobile=@uname AND UserPwd=@upwd", "", sp);
                if (dt.Rows.Count > 0) { mu = mu.GetModelFromReader(dt.Rows[0]); }
            }
            else if (type.Equals("ume"))
            {
                DataTable dt = DBCenter.JoinQuery("A.*,B.Mobile", strTableName, strTableName2, "A.UserID=B.UserID", "(A.UserName=@uname OR A.Email=@uname OR B.Mobile=@uname) AND UserPwd=@upwd", "", sp);
                if (dt.Rows.Count > 0) { mu = mu.GetModelFromReader(dt.Rows[0]); }
            }
            else
            {
                using (DbDataReader reader = DBCenter.SelReturnReader(strTableName, where, sp))
                {
                    if (reader.Read()) { mu = initMod.GetModelFromReader(reader); }
                }
            }
            if (!mu.IsNull)
            {
                B_Cart.UpdateUidByCartID(B_Cart.GetCartID(), mu.UserID);
                List<SqlParameter> usp = new List<SqlParameter>() { new SqlParameter("LastLoginTime", DateTime.Now) };
                DBCenter.UpdateSQL(strTableName, "LastLoginIP='" + IPScaner.GetUserIP() + "',LastLoginTime=@LastLoginTime,LoginTimes=" + (mu.LoginTimes + 1), "UserID=" + mu.UserID, usp);
            }
            return mu;
        }
        //-----------------------------------登录检测
        /// <summary>
        /// False则重取数据
        /// </summary>
        public M_UserInfo GetLogin(bool flag = true)
        {
            M_UserInfo mu = ZLCache.GetUser(SessionID);
            if (mu != null && flag)
            {
                return mu;
            }
            else if (curReq.Request.Cookies["UserState"] == null)
            {
                return new M_UserInfo(true);
            }
            else
            {
                string loginName = curReq.Request.Cookies["UserState"]["LoginName"], password = curReq.Request.Cookies["UserState"]["Password"]; ;
                if (string.IsNullOrEmpty(loginName) || string.IsNullOrEmpty(password)) return new M_UserInfo(true);
                mu = AuthenticateUser(StringHelper.Base64StringDecode(loginName), password, true);
                if (mu != null && !mu.IsNull) { ZLCache.AddUser(SessionID, mu); }
                return mu;
            }
        }
        public static void CheckIsLogged(string returnUrl = "")
        {
            B_User buser = new B_User();
            M_UserInfo mu = ZLCache.GetUser(buser.SessionID);
            if (mu != null && !mu.IsNull) { return; }
            string url = "~/User/Login?ReturnUrl=" + returnUrl.Replace("&", HttpUtility.UrlEncode("&"));
            if (HttpContext.Current.Request.Cookies["UserState"] == null)
            {
                HttpContext.Current.Response.Redirect(url);
            }
            else
            {
                string loginName = HttpContext.Current.Request.Cookies["UserState"]["LoginName"];
                string password = HttpContext.Current.Request.Cookies["UserState"]["Password"];
                if (string.IsNullOrEmpty(loginName) || string.IsNullOrEmpty(password))
                {
                    HttpContext.Current.Response.Redirect(url);
                }
                else
                {
                    SqlParameter[] sp = new SqlParameter[] { new SqlParameter("UserName", StringHelper.Base64StringDecode(loginName)), new SqlParameter("UserPwd", password) };
                    object o = SqlHelper.ExecuteScalar(CommandType.Text, "Select UserID From ZL_User Where UserName=@UserName And UserPwd=@UserPwd", sp);
                    if (o == null)
                    {
                        HttpContext.Current.Response.Redirect(url);
                    }
                }
            }
        }
        public void CheckIsLogin(string returnUrl = "")
        {
            CheckIsLogged(returnUrl);
        }
        public bool CheckLogin()
        {
            M_UserInfo mu = ZLCache.GetUser(SessionID);
            if (mu != null && !mu.IsNull) return true;
            try
            {
                if (curReq.Request.Cookies["UserState"] == null)
                {
                    return false;
                }
                else
                {
                    string loginName = curReq.Request.Cookies["UserState"]["LoginName"];
                    string password = curReq.Request.Cookies["UserState"]["Password"];
                    if (string.IsNullOrEmpty(loginName) || string.IsNullOrEmpty(password)) return false;
                    loginName = StringHelper.Base64StringDecode(loginName);
                    return (!AuthenticateUser(loginName, password, true).IsNull);
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        public void ClearCookie()
        {
            ZLCache.ClearByKeys(curReq.Session.SessionID);
            curReq.Response.Cookies["UserState"].Expires = DateTime.Now.AddDays(-1);
        }
        /// <summary>
        /// 设定登录状态
        /// </summary>
        public void SetLoginState(M_UserInfo model, string CookieStatus = "day")
        {
            CookieStatus = CookieStatus.ToLower();
            ZLCache.AddUser(SessionID, model);
            curReq.Response.Cookies["UserState"]["WorekNum"] = model.WorkNum;
            curReq.Response.Cookies["UserState"]["UserID"] = model.UserID.ToString();
            curReq.Response.Cookies["UserState"]["LoginName"] = StringHelper.Base64StringEncode(model.UserName);
            curReq.Response.Cookies["UserState"]["Password"] = model.UserPwd;
            switch (CookieStatus)
            {
                case "none"://即时关闭浏览器失效
                    break;
                case "minute":
                    curReq.Response.Cookies["UserState"].Expires = DateTime.Now.AddMinutes(30);
                    break;
                case "day":
                    curReq.Response.Cookies["UserState"].Expires = DateTime.Now.AddDays(7);
                    break;
                case "year":
                    curReq.Response.Cookies["UserState"].Expires = DateTime.Now.AddYears(1);
                    break;
                case "month":
                default:
                    curReq.Response.Cookies["UserState"].Expires = DateTime.Now.AddMonths(1);
                    break;
            }
            curReq.Response.Cookies["UserState2"]["UserName"] = model.UserName;
        }

        /*-----------Not Deal-------------*/
        #region UserBase
        /// <summary>
        /// 添加用户基本信息
        /// </summary>
        public bool AddBase(M_Uinfo model)
        {
            if (model.UserId < 1) { throw new Exception("错误,未指定用户身份"); }
            model._pk = "";
            DBCenter.Insert(model);
            return true;
        }
        public M_Uinfo GetUserBaseByuserid(int userid)
        {
            if (userid < 1) { return new M_Uinfo(true); }
            using (SqlDataReader rdr = Sql.SelReturnReader("ZL_UserBase", "WHERE UserID=" + userid))
            {
                if (rdr.Read())
                {
                    return uinfoMod.GetModelFromReader(rdr);
                }
                else
                {
                    return new M_Uinfo(true);
                }
            }
        }
        public bool UpdateBase(M_Uinfo model)
        {
            //Sql.UpdateByIDs(strTableName2, PK2, model.UserId.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
            //return Sql.UpdateByIDs(strTableName2, PK2, model.UserId.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
            return DBCenter.UpdateByID(model, model.UserId);
        }
        /// <summary>
        /// 更新用户自定义字段基本信息,不远程
        /// </summary>
        public bool UpdateUserFile(int uid, DataTable dt)
        {
            string str = "Update ZL_UserBase set " + Sql.UpdateSql(dt) + " where UserID=" + uid;
            SqlParameter[] sqlp = Sql.ContentPara(dt);
            return SqlHelper.ExecuteSql(str, sqlp);
        }
        #endregion
        #region OA
        /// <summary>
        /// 用务于邮件等大批量发送时，主用于MIS/OA/Mail
        /// </summary>
        /// <param name="uname">用户名S，或IDS</param>
        /// <param name="sql">返回的SQL</param>
        /// <returns></returns>
        private SqlParameter[] GetSPOA(string uname, ref string sql)
        {
            string[] idsArr = uname.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            SqlParameter[] sp = new SqlParameter[idsArr.Length];
            if (idsArr.Length > 0)
            {
                for (int i = 0; i < idsArr.Length; i++)
                {
                    sp[i] = new SqlParameter("ids" + i, idsArr[i]);
                    sql += "@" + sp[i].ParameterName + ",";
                }
                sql = sql.TrimEnd(',');
            }
            return sp;
        }
        /// <summary>
        /// 用于OA，传入1,2,3，返回用户名(有HoneyName的时候优先使用HoneyName)
        /// </summary>
        public string GetUserNameByIDS(string ids)
        {
            if (string.IsNullOrEmpty(ids)) return "";
            if (ids.Contains("|")) { ids = ids.Replace("|", ","); }
            DataTable dt = new DataTable();
            if (ids.Length < 1000)//in有2100限制
            {
                string[] idsArr = ids.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (idsArr.Length < 1) return "";
                string sql = "";
                SqlParameter[] sp = GetSPOA(ids, ref sql);
                dt = SqlHelper.ExecuteTable(CommandType.Text, "Select UserName,HoneyName From " + strTableName + " Where UserID in(" + sql + ")", sp);
            }
            else
            {
                while (!string.IsNullOrEmpty(ids))
                {

                    string partName = "";
                    int endIndex = 0;
                    if (ids.Length > 1000)
                    {
                        endIndex = ids.IndexOf(",", 1000);
                    }
                    else
                    {
                        endIndex = ids.Length;
                    }
                    partName = ids.Substring(0, endIndex);
                    ids = ids.Remove(0, partName.Length);
                    string sql = "";
                    SqlParameter[] sp = GetSPOA(partName, ref sql);
                    dt.Merge(SqlHelper.ExecuteTable(CommandType.Text, "Select UserName From " + strTableName + " Where UserID in(" + sql + ")", sp));
                }
            }
            string result = "";
            foreach (DataRow dr in dt.Rows)
            {
                if (!string.IsNullOrEmpty(dr["HoneyName"] as string))
                {
                    result += dr["HoneyName"] as string + ",";
                }
                else
                {
                    result += dr["UserName"] as string + ",";
                }
            }
            result = result.TrimEnd(',');
            return result;
        }
        public string GetUserNameByIDS(string ids, DataTable userDT)
        {
            // if (StationGroup.RemoteUser) { umod.str = ids; umod.dt = userDT; return APIHelper.UserApi_Str("GetUserNameByIDS", umod); }
            string result = "";
            if (string.IsNullOrEmpty(ids.Replace(",", ""))) return "";
            ids = ids.Trim(',');
            userDT.DefaultView.RowFilter = " UserID in(" + ids + ")";
            foreach (DataRow dr in userDT.DefaultView.ToTable().Rows)
                result += dr["UserName"].ToString() + ",";
            result = result.Trim(',');
            return result;
        }
        public string GetUserNameByNames(string Names, DataTable userDT)
        {
            // if (StationGroup.RemoteUser) { umod.uname = Names; umod.dt = userDT; return APIHelper.UserApi_Str("GetUserNameByNames", umod); }
            string result = "";
            if (string.IsNullOrEmpty(Names))
                return result;
            Names = Names.Trim(',');
            string[] NamesArr = Names.Split(',');
            string sql = "";
            for (int i = 0; i < NamesArr.Length; i++)
            {
                sql += "'" + NamesArr[i] + "',";
            }
            sql = sql.TrimEnd(',');
            DataView dv = userDT.DefaultView;
            dv.RowFilter = "UserName in(" + sql + ")";
            foreach (DataRow dr in dv.ToTable().Rows)
            {
                result += dr["UserID"].ToString() + ",";
            }
            result = result.TrimEnd(',');
            return result;
        }
        public string GetUserIDByWorkNum(string userName)//工号
        {
            // if (StationGroup.RemoteUser) { umod.uname = userName; return APIHelper.UserApi_Str("GetUserIDByWorkNum", umod); }
            if (string.IsNullOrEmpty(userName)) return "";
            DataTable dt = new DataTable();
            if (userName.Length < 1000)//in有2100限制
            {
                string[] idsArr = userName.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (idsArr.Length < 1) return "";
                string sql = "";
                SqlParameter[] sp = GetSPOA(userName, ref sql);
                dt = SqlHelper.ExecuteTable(CommandType.Text, "Select UserID From " + strTableName + " Where WorkNum IN(" + sql + ")", sp);
            }
            else
            {
                while (!string.IsNullOrEmpty(userName))
                {
                    string partName = "";
                    int endIndex = 0;
                    if (userName.Length > 1000)
                    {
                        endIndex = userName.IndexOf(",", 1000);
                    }
                    else
                    {
                        endIndex = userName.Length;
                    }
                    partName = userName.Substring(0, endIndex);
                    userName = userName.Remove(0, partName.Length);
                    string sql = "";
                    SqlParameter[] sp = GetSPOA(partName, ref sql);
                    dt.Merge(SqlHelper.ExecuteTable(CommandType.Text, "Select UserID From " + strTableName + " Where WorkNum IN(" + sql + ")", sp));
                }
            }
            string result = "";
            foreach (DataRow dr in dt.Rows)
            {
                result += dr["UserID"].ToString() + ",";
            }
            return result;
        }
        /// <summary>
        /// 检测工号是否唯一
        /// </summary>
        public bool CheckWorkNumIsOnly(string worknum, int userID)
        {
            // if (StationGroup.RemoteUser) { umod.str = worknum; umod.uid = userID; return APIHelper.UserApi_Bool("CheckWorkNumIsOnly", umod); }
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("worknum", worknum), new SqlParameter("userID", userID) };
            string str = "Select * from " + strTableName + " Where WorkNum=@worknum And UserID <> @userID";
            return !(SqlHelper.ExecuteTable(CommandType.Text, str, sp).Rows.Count > 0);
        }
        /// <summary>
        /// 用于OA，根据会员组，会员名，工号来搜索
        /// </summary>
        public DataTable SearchByInfo(string key)
        {
            // if (StationGroup.RemoteUser) { umod.str = key; return APIHelper.UserApi_DT("SearchByInfo", umod); }
            string sql = "Select u.*,g.GroupName From ZL_User as u Left Join ZL_Group as g On u.GroupID=g.GroupID ";
            SqlParameter[] sp;
            if (function.IsNumeric(key))
            {

                sp = new SqlParameter[] { new SqlParameter("Key", key) };
                sql += " Where u.WorkNum=@key";
            }
            else
            {
                sp = new SqlParameter[] { new SqlParameter("Key", "%" + key + "%") };
                sql += " Where g.GroupName Like @key OR u.UserName Like @key";
            }
            return SqlHelper.ExecuteTable(CommandType.Text, sql, sp);
        }
        /// <summary>
        /// 根据用户名，获取对应ID，返回,1,2,3,格式
        /// </summary>
        /// <param name="userName">admin,user1,user2</param>
        /// <returns></returns>
        public string GetIdsByUserName(string userName)
        {
            // if (StationGroup.RemoteUser) { umod.uname = userName; return APIHelper.UserApi_Str("GetIdsByUserName", umod); }
            if (string.IsNullOrEmpty(userName)) return "";
            DataTable dt = new DataTable();
            if (userName.Length < 1000)//in有2100限制
            {
                string[] idsArr = userName.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (idsArr.Length < 1) return "";
                string sql = "";
                SqlParameter[] sp = GetSPOA(userName, ref sql);
                dt = SqlHelper.ExecuteTable(CommandType.Text, "Select UserID From " + strTableName + " Where UserName IN(" + sql + ")", sp);
            }
            else
            {
                while (!string.IsNullOrEmpty(userName))
                {

                    string partName = "";
                    int endIndex = 0;
                    if (userName.Length > 1000)
                    {
                        endIndex = userName.IndexOf(",", 1000);
                    }
                    else
                    {
                        endIndex = userName.Length;
                    }
                    partName = userName.Substring(0, endIndex);
                    userName = userName.Remove(0, partName.Length);
                    string sql = "";
                    SqlParameter[] sp = GetSPOA(partName, ref sql);
                    dt.Merge(SqlHelper.ExecuteTable(CommandType.Text, "Select UserID From " + strTableName + " Where UserName IN(" + sql + ")", sp));
                }
            }
            string result = "";
            foreach (DataRow dr in dt.Rows)
            {
                result += dr["UserID"].ToString() + ",";
            }
            return "," + result;
        }
        /// <summary>
        /// 服务于GetUserIDByWorkNum,从一串字符串中，筛出数字字符串并拼接
        /// </summary>
        /// <returns></returns>
        private string FilterWorkNum(string uname)
        {
            string result = "";
            if (!string.IsNullOrEmpty(uname))
            {
                string[] nameArr = uname.Split(',');
                for (int i = 0; i < nameArr.Length; i++)
                {
                    if (DataConvert.CLng(nameArr[i]) > 0)
                        result += nameArr[i] + ",";
                }
                result = result.TrimEnd(',');
            }
            return result;
        }
        /// <summary>
        /// 依据工号与用户名获取用户ID，仅用于311OA
        /// </summary>
        /// <returns></returns>
        public string SelUserIDByOA(string uname)
        {
            string result = GetIdsByUserName(uname);
            uname = FilterWorkNum(uname);
            result += GetUserIDByWorkNum(uname);
            return result;
        }
        public DataTable SelByUserID(int userID)
        {
            string sql = "Select u.*,g.GroupName,ub.Mobile,ub.TrueName From ZL_User as u Left Join ZL_Group as g On u.GroupID=g.GroupID Left Join ZL_UserBase as ub On u.UserID=ub.UserID Where u.UserID=" + userID;
            return SqlHelper.ExecuteTable(CommandType.Text, sql);
        }
        public DataTable GetUserName(string name)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("name", name) };
            return Sql.Sel(strTableName, "UserName=@name", "", sp);
        }
        #endregion
        #region 模型
        public void InsertModel(DataTable modelinfo, string tablename)
        {
            SafeSC.CheckDataEx(tablename);
            string dxsql = "insert into " + tablename + " " + Sql.InsertSql(modelinfo);
            SqlParameter[] parameter = Sql.ContentPara(modelinfo);
            try
            {
                SqlHelper.ExistsSql(dxsql, parameter);
            }
            catch (Exception) { }
        }
        /// <summary>
        /// 获取会员模型信息
        /// </summary>
        /// <param Name="TableName">存储会员模型信息的表名</param>
        /// <param Name="ID">记录ID或会员ID由type决定</param>
        /// <param Name="Type">0-会员ID 1-记录ID</param>
        public DataTable GetUserModeInfo(string TableName, int ID, int Type)
        {
            // if (StationGroup.RemoteUser) { umod.str = TableName; umod.uid = ID; umod.gid = Type; return APIHelper.UserApi_DT("GetUserModeInfo", umod); }
            string strSql = "select * from " + TableName;
            SqlParameter[] sp;
            switch (Type)
            {
                case 0:
                    strSql = strSql + " where UID=@ID and Status=0";
                    strSql = strSql + " order by UpdateTime desc,id desc";
                    sp = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int) };
                    sp[0].Value = ID;
                    return SqlHelper.ExecuteTable(CommandType.Text, strSql, sp);
                case 3:
                    strSql = strSql + " order by UpdateTime desc,id desc";
                    return SqlHelper.ExecuteTable(CommandType.Text, strSql, null);
                case 4:
                    strSql = strSql + " order by id desc";
                    return SqlHelper.ExecuteTable(CommandType.Text, strSql, null);
                /// jc:type=9为取出整表信息
                case 9:
                    strSql = strSql + " where Status=0 order by UpdateTime desc,id desc";
                    return SqlHelper.ExecuteTable(CommandType.Text, strSql, null);
                case 10:
                    strSql = strSql + " where Status=1 order by UpdateTime desc,id desc";
                    return SqlHelper.ExecuteTable(CommandType.Text, strSql, null);
                case 11:
                    strSql = strSql + " order by Parentid asc,ID desc";
                    return SqlHelper.ExecuteTable(CommandType.Text, strSql, null);
                case 12:
                    strSql = strSql + " where ID=@ID ";
                    sp = new SqlParameter[] {
                    new SqlParameter("@ID",SqlDbType.Int)
                      };
                    sp[0].Value = ID;
                    return SqlHelper.ExecuteTable(CommandType.Text, strSql, sp);
                case 13:
                    strSql = strSql + " where Parentid=@ID order by Parentid asc,ID desc";
                    sp = new SqlParameter[] {
                         new SqlParameter("@ID",SqlDbType.Int)
                       };
                    sp[0].Value = ID;
                    return SqlHelper.ExecuteTable(CommandType.Text, strSql, sp);
                case 14:
                    strSql = strSql + " where Pubstart=0 and Parentid=0 order by Parentid asc,ID desc";
                    return SqlHelper.ExecuteTable(CommandType.Text, strSql, null);
                case 15:
                    strSql = strSql + " where Pubstart=1 and Parentid=0 order by Parentid asc,ID desc";
                    return SqlHelper.ExecuteTable(CommandType.Text, strSql, null);
                case 16:
                    sp = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int) };
                    sp[0].Value = ID;
                    strSql = strSql + " where PubUserID=@ID AND Parentid=0 order by ID desc";
                    return SqlHelper.ExecuteTable(CommandType.Text, strSql, sp);
                case 17:
                    strSql = strSql + " order by Parentid asc,ID desc";
                    return SqlHelper.ExecuteTable(CommandType.Text, strSql, null);
                case 18:
                    sp = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int) };
                    sp[0].Value = ID;
                    strSql = strSql + " where ID=@ID order by ID desc";
                    return SqlHelper.ExecuteTable(CommandType.Text, strSql, sp);
                case 19:
                    strSql = strSql + " where Parentid=@ID and Pubstart=1 order by Parentid asc,ID desc";
                    sp = new SqlParameter[] {
                         new SqlParameter("@ID",SqlDbType.Int)
                       };
                    sp[0].Value = ID;
                    return SqlHelper.ExecuteTable(CommandType.Text, strSql, sp);
                case 20:
                    strSql = strSql + " where Parentid=@ID and Pubstart=0 order by Parentid asc,ID desc";
                    sp = new SqlParameter[] {
                         new SqlParameter("@ID",SqlDbType.Int)
                       };
                    sp[0].Value = ID;
                    return SqlHelper.ExecuteTable(CommandType.Text, strSql, sp);
                case 111:
                    strSql = strSql + " where Parentid=0 order by Parentid asc,ID desc";
                    return SqlHelper.ExecuteTable(CommandType.Text, strSql, null);
                default:
                    strSql = strSql + " where [ID]=@ID and Recycler=0";
                    sp = new SqlParameter[] {
                         new SqlParameter("@ID",SqlDbType.Int)
                       };
                    sp[0].Value = ID;
                    return SqlHelper.ExecuteTable(CommandType.Text, strSql, sp);
            }
        }
        /// <summary>
        /// 添加会员模型信息
        /// </summary>
        public bool AddUserModel(DataTable UserData, string TableName)
        {
            // if (StationGroup.RemoteUser) { umod.dt = UserData; umod.uname = TableName; return APIHelper.UserApi_Bool("AddUserModel", umod); }
            string strSql = "Insert Into " + TableName + Sql.InsertSql(UserData);
            SqlParameter[] sp = Sql.ContentPara(UserData);
            return SqlHelper.ExecuteSql(strSql, sp);
        }
        /// <summary>
        /// 更新会员模型信息
        /// </summary>
        public bool UpdateModelInfo(DataTable UserData, string TableName, int ID)
        {
            // if (StationGroup.RemoteUser) { umod.dt = UserData; umod.uname = TableName; umod.uid = ID; return APIHelper.UserApi_Bool("UpdateModelInfo", umod); }
            if (UserData != null && UserData.Rows.Count > 0)
            {
                string strSql = "Update " + TableName + " Set " + Sql.UpdateSql(UserData) + "  Where [ID]=" + ID.ToString();
                SqlParameter[] sp = Sql.ContentPara(UserData);
                return SqlHelper.ExecuteSql(strSql, sp);
            }
            return false;
        }
        /// <summary>
        /// 删除会员模型信息
        /// </summary>
        public bool DelModelInfo(string TableName, int ID)
        {
            string strSql = "delete from " + TableName + " Where [ID]=@ID";
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int) };
            sp[0].Value = ID;
            return SqlHelper.ExecuteSql(strSql, sp);
        }
        /// <summary>
        /// 删除所有回收站信息
        /// </summary>
        public bool DelModelInfoAll(string TableName)
        {
            // if (StationGroup.RemoteUser) { umod.uname = TableName; return APIHelper.UserApi_Bool("DelModelInfoAll", umod); }
            return Sql.Del(TableName, "Recycler=1");
        }
        /// <summary>
        /// 删除会员模型信息进入回收站
        /// </summary>
        /// <param Name="type">1进入回收站，88表示生成页面，89表示取消生成</param>
        public bool DelModelInfo2(string TableName, int ID, int type)
        {
            string sqlStr = "";
            switch (type)
            {
                case -1:
                    sqlStr = "update " + TableName + " set NewTime='" + DateTime.Now.ToString() + "' Where [UserID]=@ID and Recycler=0";
                    break;
                case 1:
                    sqlStr = "update " + TableName + " set Recycler=1 Where [ID]=@ID";
                    break;
                case 11:
                    sqlStr = "update " + TableName + " set Recycler=0 Where Recycler=1";
                    return SqlHelper.ExecuteSql(sqlStr, null);
                case 12:
                    sqlStr = "update " + TableName + " set Pubstart=1 Where [ID]=@ID";
                    break;
                case 13:
                    sqlStr = "update " + TableName + " set Pubstart=0 Where [ID]=@ID";
                    break;
                case 88:
                    sqlStr = "update " + TableName + " set IsCreate=1 Where [ID]=@ID";
                    break;
                case 89:
                    sqlStr = "update " + TableName + " set IsCreate=0 Where [ID]=@ID";
                    break;
                default:
                    sqlStr = "update " + TableName + " set Recycler=0 Where [ID]=@ID";
                    break;
            }
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4) };
            sp[0].Value = ID;
            return SqlHelper.ExecuteSql(sqlStr, sp);
        }
        /// <summary>
        /// 获取某个用户的模型信息数据的ID
        /// </summary>
        /// <param Name="TableName">存储会员模型信息的表名</param>
        /// <param Name="UserID">用户ID</param>
        /// <returns>信息ID</returns>
        public int UserModeInfoID(string TableName, int UserID)
        {
            // if (StationGroup.RemoteUser) { umod.uname = TableName; umod.uid = UserID; return int.Parse(APIHelper.UserApi_Str("UserModeInfoID", umod)); }
            string strSql = "select ID from " + TableName + " where UserID=@UserID";
            SqlParameter[] sp = new SqlParameter[] {
                new SqlParameter("@UserID",SqlDbType.Int)
            };
            sp[0].Value = UserID;
            return SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.Text, strSql, sp));
        }
        #endregion
        /// <summary>
        /// 统计用户填写字段，返回已完成百分比
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public int CountUserField(int userid)
        {
            if (string.IsNullOrEmpty(SiteConfig.UserConfig.CountUserField)) { return -1; }
            string[] fields = SiteConfig.UserConfig.CountUserField.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            string sql = "SELECT * FROM ZL_User A LEFT JOIN ZL_UserBase B ON A.UserID=B.UserID WHERE A.UserID=" + userid;
            int count = 0;
            DataTable dt = SqlHelper.ExecuteTable(sql);
            DataRow dr = dt.Rows[0];
            foreach (string field in fields)
            {
                if (!string.IsNullOrEmpty(dr[field].ToString())) { count++; }
            }
            int fieldcount = fields.Length;
            return (int)(((double)count / fieldcount) * 100);
        }
        public DataTable SelBarAuth(int barid)
        {
            string sql = "Select A.UserID,A.UserName,b.* From " + strTableName + " A Left Join (Select * From ZL_Guest_BarAuth Where BarID=" + barid + ") B ON A.UserID=B.Uid";
            return SqlHelper.ExecuteTable(CommandType.Text, sql);
        }
        //------Tools
        public M_UserInfo NewUser(string uname, string upwd, string email = "")
        {
            M_UserInfo mu = new M_UserInfo();
            mu.UserName = uname;
            mu.UserPwd = StringHelper.MD5(upwd);
            mu.Email = string.IsNullOrEmpty(email) ? function.GetRandomString(8) + "@random.com" : email;
            mu.DeadLine = DateTime.MaxValue;
            return mu;
        }
        public M_Uinfo NewBase(M_UserInfo mu)
        {
            M_Uinfo basemu = new M_Uinfo();
            basemu.UserId = mu.UserID;
            basemu.UserFace = mu.UserFace;
            basemu.TrueName = mu.TrueName;
            basemu.HoneyName = mu.HoneyName;
            return basemu;
        }
        public bool IsTeach(M_UserInfo mu = null)
        {
            if (mu == null) { mu = GetLogin(); }
            M_Group gpMod = new B_Group().GetByID(mu.GroupID);
            return gpMod.Enroll != null && gpMod.Enroll.Contains("isteach");
        }
        public bool GetRegisterIP(string LocalIP, DateTime BeginTime, DateTime EndTime)
        {
            return true;
        }
        public string GetRegion(int uid)
        {
            M_Uinfo basemu = GetUserBaseByuserid(uid);
            if (basemu == null || basemu.IsNull || string.IsNullOrEmpty(basemu.Province + basemu.City + basemu.County)) { return "none"; }
            else { return basemu.Province + "|" + basemu.City + "|" + basemu.County; }
        }
        /// <summary>
        /// 字符串是否符号密码规范,为空表示通过
        /// </summary>
        /// <returns></returns>
        public string CheckPwdRegular(string pwd)
        {
            string err = "";
            if (string.IsNullOrEmpty(pwd)) { err = "密码不能为空"; }
            else if (pwd.Contains(" ")) { err = "密码不能包含空格"; }
            else if (pwd.Length < 6) { err = "密码不能少于6位"; }
            else if (pwd.Length > 30) { err = "密码不能长于30位"; }
            else if (pwd.Contains(":")) { err = "密码不能包含特殊符号"; }
            return err;
        }
        /// <summary>
        /// 返回第一个有信息的名称,真实名称|昵称|用户名|默认名称
        /// </summary>
        public static string GetUserName(params object[] names)
        {
            string result = "";
            for (int i = 0; i < names.Length; i++)
            {
                if (!string.IsNullOrEmpty(DataConvert.CStr(names[i]))) { result = names[i].ToString(); break; }
            }
            return result;
        }
        //-----------New
        public int GetTypeByStr(string ptype)
        {
            ptype = ptype.ToLower();
            switch (ptype)
            {
                case "purse":
                    return 1;
                case "sicon":
                    return 2;
                case "point":
                    return 3;
                default:
                    throw new Exception("未知币种" + ptype);
            }
        }
        #region Update区,该区所有操作需要刷新缓存
        /// <summary>
        /// 更改虚拟币的值,上层应该做好检测,这层只负责更新与写入记录
        /// </summary>
        public void ChangeVirtualMoney(int uid, M_UserExpHis hisMod)
        {
            if (hisMod.score == 0) { return; }
            //string sql = "Update " + strTableName + " SET {0}={0}+" + hisMod.score + " Where UserID=" + uid;
            string field = "";
            //-为扣减,
            switch (hisMod.ScoreType)
            {
                case 1:
                    field = "Purse";
                    break;
                case 2:
                    field = "SilverCoin";
                    break;
                case 3:
                    field = "UserExp";
                    break;
                case 4:
                    field = "UserPoint";
                    break;
                case 5:
                    field = "DummyPurse";
                    break;
                case 6:
                    field = "UserCreit";
                    break;
                default:
                    throw new Exception("虚拟币类型不存在");
            }
            DataTable dt = DBCenter.SelWithField(strTableName, "UserID," + field, "UserID=" + uid);
            if (dt.Rows[0][field] == DBNull.Value) { DBCenter.UpdateSQL(strTableName, field + "=0", "UserID=" + uid); }
            DBCenter.UpdateSQL(strTableName, field + "=" + field + "+" + hisMod.score, "UserID=" + uid);
            hisMod.UserID = uid;
            ZLCache.ClearByIDS(uid.ToString());
            B_History.Insert(hisMod);
        }
        public void MinusVMoney(int uid, double money, M_UserExpHis.SType stype, string detail)
        {
            if (money < 1) { throw new Exception("数值不正确,必须大于0"); }
            ChangeVirtualMoney(uid, new M_UserExpHis()
            {
                UserID = uid,
                score = -money,
                ScoreType = (int)stype,
                detail = detail
            });
        }
        public void AddMoney(int uid, double money, M_UserExpHis.SType stype, string detail)
        {
            if (uid < 1) { throw new Exception("AddMoney未指定uid"); }
            if (money < 1) { throw new Exception("[" + stype.ToString() + "]数值不正确,必须大于0"); }
            ChangeVirtualMoney(uid, new M_UserExpHis()
            {
                UserID = uid,
                score = money,
                ScoreType = (int)stype,
                detail = detail
            });
        }
        /// <summary>
        /// 获得虚拟币的名称
        /// </summary>
        /// <param name="stype"></param>
        /// <returns></returns>
        public string GetVirtualMoneyName(int stype)
        {
            M_UserExpHis.SType ExpType = (M_UserExpHis.SType)stype;
            switch (ExpType)
            {
                case M_UserExpHis.SType.Purse:
                    return "资金";
                case M_UserExpHis.SType.SIcon:
                    return "银币";
                case M_UserExpHis.SType.Point:
                    return "积分";
                case M_UserExpHis.SType.UserPoint:
                    return "点券";
                case M_UserExpHis.SType.Credit:
                    return "信誉值";
                case M_UserExpHis.SType.DummyPoint:
                    return "虚拟币";
                default:
                    return "未知类型";
            }
        }
        /// <summary>
        /// 按虚拟币类型获得相应值
        /// </summary>
        /// <param name="mu"></param>
        /// <returns></returns>
        public double GetVirtualMoney(M_UserInfo mu, int stype)
        {
            M_UserExpHis.SType ExpType = (M_UserExpHis.SType)stype;
            switch (ExpType)
            {
                case M_UserExpHis.SType.Purse:
                    return mu.Purse;
                case M_UserExpHis.SType.SIcon:
                    return mu.SilverCoin;
                case M_UserExpHis.SType.Point:
                    return mu.UserExp;
                case M_UserExpHis.SType.UserPoint:
                    return mu.UserPoint;
                case M_UserExpHis.SType.DummyPoint:
                    return mu.DummyPurse;
                case M_UserExpHis.SType.Credit:
                    return mu.UserCreit;
                default:
                    return mu.UserExp;
            }
        }
        public bool Audit(string uids, int type, int GroupID)
        {
            // if (StationGroup.RemoteUser) { umod.str = userids; umod.uid = type; umod.gid = GroupID; return APIHelper.UserApi_Bool("Audit", umod); }
            SafeSC.CheckIDSEx(uids);
            string strSql = "";
            if (type == 1)
            {
                strSql = "UPDATE [ZL_Page] SET [Status] = 99 WHERE ID IN(" + uids + ")";
                SqlHelper.ExecuteSql(strSql);
                strSql = "update ZL_CommonModel set Status = 99 where TableName like 'ZL_Reg_%' and (GeneralID in(" + uids + "))";
            }
            else
            {
                strSql = "UPDATE [ZL_Page] SET [Status] = 0 WHERE ID IN(" + uids + ")";
                SqlHelper.ExecuteSql(strSql);
                strSql = "update ZL_CommonModel set Status = 0 where TableName like 'ZL_Reg_%' and (GeneralID in(" + uids + "))";
            }
            ZLCache.ClearByIDS(uids);
            return SqlHelper.ExecuteSql(strSql);
        }
        #endregion
        #region 招聘,简历
        /// <summary>
        /// 是否已向某职位投递过简历
        /// </summary>
        /// <param Name="JobsID">企业用户ID</param>
        /// <param Name="UserReID">简历ID</param>
        public bool IsExitResume(int CompanyID, int UserReID)
        {
            // if (StationGroup.RemoteUser) { umod.uid = CompanyID; umod.gid = UserReID; return APIHelper.UserApi_Bool("IsExitResume", umod); }
            string strSql = "select count(CID) from ZL_CompanyResume where CompanyID=@CompanyID and ResumeID=@ResumeID";
            SqlParameter[] sp = new SqlParameter[] {
                new SqlParameter("@CompanyID", SqlDbType.Int),
                new SqlParameter("@ResumeID", SqlDbType.Int)
            };
            sp[0].Value = CompanyID;
            sp[1].Value = UserReID;
            return SqlHelper.ExistsSql(strSql, sp);
        }
        /// <summary>
        /// 是否已向用户发出面试通知
        /// </summary>
        /// <param Name="UserID">被通知的个人用户ID</param>
        /// <param Name="CompanyID">招聘单位信息ID</param>
        public bool IsExitExaminee(int UserID, int CompanyID)
        {
            // if (StationGroup.RemoteUser) { umod.uid = UserID; umod.gid = CompanyID; return APIHelper.UserApi_Bool("IsExitExaminee", umod); }
            string strSql = "select count(EID) from ZL_Examinee where UserID=@UserID and CompanyID=@CompanyID";
            SqlParameter[] sp = new SqlParameter[] {
                new SqlParameter("@UserID", SqlDbType.Int),
                new SqlParameter("@CompanyID", SqlDbType.Int)
            };
            sp[0].Value = UserID;
            sp[1].Value = CompanyID;
            return SqlHelper.ExistsSql(strSql, sp);
        }
        /// <summary>
        /// 投递简历
        /// </summary>
        /// <param Name="JobsID">招聘信息ID</param>
        /// <param Name="CompanyUser">企业用户ID</param>
        /// <param Name="UserReID">简历ID</param>
        /// <param Name="UserID">投递人ID</param>
        /// <param Name="PostDate">投递时间</param>
        public void AddPostResume(int JobsID, int CompanyUser, int UserReID, int UserID, DateTime PostDate)
        {
            string strSql = "insert into ZL_CompanyResume (CompanyID,ResumeID,JobID,UserID,ResumeTime) Values(@CompanyID,@ResumeID,@JobID,@UserID,@ResumeTime)";
            SqlParameter[] sp = new SqlParameter[] {
                new SqlParameter("@CompanyID", SqlDbType.Int),
                new SqlParameter("@ResumeID", SqlDbType.Int),
                new SqlParameter("@JobID", SqlDbType.Int),
                new SqlParameter("@UserID", SqlDbType.Int),
                new SqlParameter("@ResumeTime", SqlDbType.DateTime)
            };
            sp[0].Value = CompanyUser;
            sp[1].Value = UserReID;
            sp[2].Value = JobsID;
            sp[3].Value = UserID;
            sp[4].Value = PostDate;
            SqlHelper.ExecuteSql(strSql, sp);
        }
        /// <summary>
        /// 发出面试通知
        /// </summary>
        /// <param Name="UserID">被通知的个人用户ID</param>
        /// <param Name="CompanyID">招聘单位信息ID</param>
        /// <param Name="PostDate">发出时间</param>
        public void AddPostExaminee(int UserID, int CompanyID, DateTime PostDate)
        {
            string strSql = "insert into ZL_Examinee (UserID,CompanyID,SendTime) Values(@UserID,@CompanyID,@SendTime)";
            SqlParameter[] sp = new SqlParameter[] {
                new SqlParameter("@UserID", SqlDbType.Int),
                new SqlParameter("@CompanyID", SqlDbType.Int),
                new SqlParameter("@SendTime", SqlDbType.DateTime)
            };
            sp[0].Value = UserID;
            sp[1].Value = CompanyID;
            //sp[2].Value = CompanyInfo;
            sp[2].Value = PostDate;
            SqlHelper.ExecuteSql(strSql, sp);
        }
        /// <summary>
        /// 获取个人用户投递给某企业用户的简历投递信息
        /// </summary>
        /// <param Name="CompanyID">企业用户ID</param>
        /// <returns>简历投递信息列表</returns>
        public DataTable GetResumeList(int CompanyID)
        {
            // if (StationGroup.RemoteUser) { umod.gid = CompanyID; return APIHelper.UserApi_DT("GetResumeList", umod); }
            string strSql = "select * from ZL_CompanyResume where CompanyID=@CompanyID Order by ResumeTime Desc";
            SqlParameter[] sp = new SqlParameter[] {
                new SqlParameter("@CompanyID", SqlDbType.Int)
            };
            sp[0].Value = CompanyID;
            return SqlHelper.ExecuteTable(CommandType.Text, strSql, sp);
        }
        /// <summary>
        /// 删除简历投递信息
        /// </summary>
        /// <param Name="CID">简历投递信息ID</param>
        /// <returns>成功状态</returns>
        public bool DelResumePost(int CID)
        {
            // if (StationGroup.RemoteUser) { umod.uid = CID; return APIHelper.UserApi_Bool("DelResumePost", umod); }
            string strSql = "Delete from ZL_CompanyResume where CID=@CID";
            SqlParameter[] sp = new SqlParameter[] {
                new SqlParameter("@CID", SqlDbType.Int)
            };
            sp[0].Value = CID;
            return SqlHelper.ExistsSql(strSql, sp);
        }
        /// <summary>
        /// 获取个人用户收到的应试通知信息
        /// </summary>
        /// <param Name="UserID">个人用户ID</param>
        /// <returns>应试通知列表</returns>
        public DataTable GetExamineeList(int UserID)
        {
            // if (StationGroup.RemoteUser) { umod.uid = UserID; return APIHelper.UserApi_DT("GetExamineeList", umod); }
            string strSql = "select * from ZL_Examinee where UserID=@UserID Order by SendTime Desc";
            SqlParameter[] sp = new SqlParameter[] {
                new SqlParameter("@UserID", SqlDbType.Int)
            };
            sp[0].Value = UserID;
            return SqlHelper.ExecuteTable(CommandType.Text, strSql, sp);
        }
        /// <summary>
        /// 删除应试通知
        /// </summary>
        /// <param Name="EID">应试通知ID</param>
        public bool DelExamineePost(int EID)
        {
            // if (StationGroup.RemoteUser) { umod.uid = EID; return APIHelper.UserApi_Bool("DelExamineePost", umod); }
            string strSql = "Delete from ZL_Examinee where EID=@EID";
            SqlParameter[] sp = new SqlParameter[] {
                new SqlParameter("@EID", SqlDbType.Int)
            };
            sp[0].Value = EID;
            return SqlHelper.ExistsSql(strSql, sp);
        }
        /// <summary>
        /// 改印证
        /// </summary>
        /// <param Name="UserID">用户ID</param>
        /// <param Name="IsConfirm">印证</param>
        public bool UpdateUserConfirm(int UserID, int IsComfirm)
        {
            // if (StationGroup.RemoteUser) { umod.uid = UserID; umod.gid = IsComfirm; return APIHelper.UserApi_Bool("UpdateUserConfirm", umod); }
            return Sql.UpBool(strTableName, "IsConfirm=" + IsComfirm, " UserID=" + UserID);
        }
        #endregion
    }
}