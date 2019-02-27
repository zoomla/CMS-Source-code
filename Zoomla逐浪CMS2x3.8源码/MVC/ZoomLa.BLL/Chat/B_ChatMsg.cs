using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Model.Chat;
using ZoomLa.SQLDAL;
using ZoomLa.SQLDAL.SQL;

public class B_ChatMsg
{
    B_User buser = new B_User();
    private static List<M_ChatMsg> _msglis;
    public static List<M_ChatMsg> MsgList
    {
        get { if (_msglis == null) _msglis = new List<M_ChatMsg>(); return _msglis; }
    }
    public static List<M_OnlineUser> OnlineUserList = new List<M_OnlineUser>();

    string TbName = "", PK = "";
    M_ChatMsg initMod = new M_ChatMsg();
    public B_ChatMsg()
    {
        TbName = initMod.TbName;
        PK = initMod.PK;
    }
    public DataTable Sel()
    {
        return Sql.Sel(TbName, "", "CDate Desc");
    }
    public DataTable SelByWhere(int uid, int ReceUser = 0, string sdate = "", string edate = "")
    {
        string strwhere = "";
        if (ReceUser > 0)
            strwhere += " AND ReceUser LIKE @ReceUser ";
        if (!string.IsNullOrEmpty(sdate) && !string.IsNullOrEmpty(edate))
            strwhere += " AND CDate BETWEEN @sdate AND @edate ";
        string sql = "SELECT * FROM " + TbName + " WHERE UserID=" + "'" + uid + "'" + strwhere;
        SqlParameter[] sp = new SqlParameter[] { new SqlParameter("@ReceUser", "%," + ReceUser + ",%"), new SqlParameter("@sdate", sdate), new SqlParameter("@edate", edate) };
        return SqlHelper.ExecuteTable(CommandType.Text, sql, sp);
    }
    public PageSetting SelPage(int cpage, int psize, int uid, int ReceUser = 0, string sdate = "", string edate = "")
    {
        string where = "UserID = " + uid;
        List<SqlParameter> sp = new List<SqlParameter>();
        if (ReceUser > 0) { where += " AND ReceUser LIKE @ReceUser"; sp.Add(new SqlParameter("ReceUser", "%," + ReceUser + ",%")); }
        if (!string.IsNullOrEmpty(sdate) && !string.IsNullOrEmpty(edate))
        {
            where += " AND CDate BETWEEN @sdate AND @edate";
            sp.Add(new SqlParameter("sdate", sdate));
            sp.Add(new SqlParameter("edate", edate));
        }
        PageSetting setting = PageSetting.Single(cpage, psize, TbName, PK, where, PK + " DESC", sp);
        DBCenter.SelPage(setting);
        return setting;
    }
    public int Insert(M_ChatMsg model)
    {
        return Sql.insert(TbName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
    }
    public bool UpdateByID(M_ChatMsg model)
    {
        return Sql.UpdateByIDs(TbName, PK, model.UserID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
    }
    public bool Del(int ID)
    {
        string sql = "Delete From " + TbName + " Where ID=" + ID;
        return SqlHelper.ExecuteSql(sql);
    }
    public bool DelByIds(string ids)
    {
        SafeSC.CheckIDSEx(ids);
        string sql = "Delete From " + TbName + " Where ID IN (" + ids + ")";
        return SqlHelper.ExecuteSql(sql);
    }
    public bool DelByWeek()
    {
        string sql = "Delete From " + TbName + " Where datediff(day,CDate,getdate())>7 AND datediff(day,CDate,getdate())<=14";
        return SqlHelper.ExecuteSql(sql);
    }
    public M_ChatMsg SelReturnModel(int ID)
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
    /// <summary>
    /// 历史聊天记录(后期改为分页)
    /// </summary>
    /// <param name="cuid">发送人</param>
    /// <param name="ruid">接收人</param>
    public DataTable SelHistoryMsg(string senduid, string receuid)
    {
        SqlParameter[] sp = new SqlParameter[] { new SqlParameter("suid", senduid), new SqlParameter("ruid", receuid), new SqlParameter("senduid", "%," + senduid + ",%"), new SqlParameter("receuid", "%," + receuid + ",%") };
        string sql = "Select * From " + TbName + " Where (UserID=@suid And ReceUser Like @receuid ) OR (UserID=@ruid And ReceUser Like @senduid ) ORDER BY CDate ASC";
        return SqlHelper.ExecuteTable(CommandType.Text, sql, sp);
    }
    //------MsgList
    /// <summary>
    /// 从内存中读取消息记录
    /// </summary>
    /// <param name="uid">我的ID</param>
    /// <param name="tuid">接收来自谁的信息</param>
    /// <returns></returns>
    public List<M_ChatMsg> GetMsgByUid(string uid, string tuid)
    {
        string suid = "," + uid + ",";
        List<M_ChatMsg> list = MsgList.Where(p => p.UserID.Equals(tuid) && p.ReceUser.Contains(suid)).ToList();
        foreach (M_ChatMsg model in list)
        {
            model.ReceUser = model.ReceUser.Replace(suid, ",");//读取消息后移除用户标识
        }
        return list;
    }
    //返回缓存中有多少给我的未读消息,用于消息提示,点击用户后再AJAX加载
    public string GetMsgCount(string uid)
    {
        string suid = "," + uid + ",";
        string result = "";
        List<M_ChatMsg> list = MsgList.Where(p => p.ReceUser.Contains(suid)).ToList();
        foreach (M_ChatMsg model in list)
        {
            result += model.UserID + ",";
        }
        return result.Trim(',');
    }
    //移除超时消息,每过十分钟自动检测一次,不放入AJAX中
    public void RemoveExpireMsg()
    {
        MsgList.RemoveAll(p => (DateTime.Now - p.CDate).Minutes > 10);
    }
    //----OnlineUser
    //移除超时的用户(getmsg中一并处理)
    public void AddOnLineUser(M_OnlineUser model)
    {
        //检测用户是否存在,存在则更新时间,不存在则移除
        bool isexist = false;
        foreach (M_OnlineUser user in OnlineUserList)
        {
            if (user.UserID.Equals(model.UserID))
            {
                user.LastTime = DateTime.Now;
                isexist = true;
                break;
            }
        }
        if (!isexist)
        {
            OnlineUserList.Add(model);
        }
    }
    //更新时间,避免超时被抛
    public void UpdateTime(string uid)
    {
        foreach (M_OnlineUser user in OnlineUserList)
        {
            if (user.UserID.Equals(uid))
            {
                user.LastTime = DateTime.Now;
                break;
            }
        }
    }
    //返回IDS格式
    public string GetOnlineStr()
    {
        RemoveExpireUser();
        string ids = "";
        foreach (M_OnlineUser user in OnlineUserList)
        {
            ids += user.UserID + ",";
        }
        ids = "," + ids;
        return ids;
    }
    public string GetOnlineJson(bool only_visitor)
    {
        RemoveExpireUser();
        string result = "";
        foreach (M_OnlineUser user in OnlineUserList)
        {
            if (!only_visitor || (only_visitor && user.IsVisitor))
                result += "{\"UserID\":\"" + user.UserID + "\",\"UserName\":\"" + user.UserName + "\",\"UserFace\":\"" + user.UserFace + "\"},";
        }
        return "[" + result.TrimEnd(',') + "]";
    }
    //获取在线游客
    public List<M_OnlineUser> GetOnlineVisitor()
    {
        RemoveExpireUser();
        return OnlineUserList.Where(p => p.IsVisitor).ToList();
    }
    public M_OnlineUser GetModelByUid(string uid)
    {
        foreach (M_OnlineUser user in OnlineUserList)
        {
            if (user.UserID.ToLower().Equals(uid.ToLower()))
            {
                return user;
            }
        }
        return GetUser();
    }
    public void RemoveExpireUser()
    {
        OnlineUserList.RemoveAll(p => (DateTime.Now - p.LastTime).Seconds > 10);
    }
    /// <summary>
    /// 生成OnlineUser用户,并且加入在线列表
    /// </summary>
    /// <param name="uname">用户名,无则默认生成</param>
    /// <param name="UserID">如果输入该值,无则新建</param>
    /// <returns></returns>
    public M_OnlineUser GetUser(string uname = "", string UserID = "")
    {
        HttpRequest request = HttpContext.Current.Request;
        HttpResponse response = HttpContext.Current.Response;
        M_OnlineUser model = GetLogin();
        //如果输入了用户ID,则强制刷新,避免缓存,用于用户行为追踪
        if (model != null && !string.IsNullOrEmpty(UserID) && !model.UserID.Equals(UserID)) { model = null; }
        if (model == null)
        {
            model = new M_OnlineUser();
            model.UserID = string.IsNullOrEmpty(UserID) ? function.GetRandomString(4).ToLower() : UserID;
            model.UserName = string.IsNullOrEmpty(uname) ? "游客" : uname;
            model.UserFace = "";
            model.IsVisitor = true;
            model.LastTime = DateTime.Now;
            response.Cookies["chat"]["chatuid"] = model.UserID;
            response.Cookies["chat"]["chatuname"] = HttpUtility.UrlEncode(model.UserName);
            response.Cookies["chat"].Expires = DateTime.Now.AddMonths(1);
        }
        AddOnLineUser(model);
        return model;
    }
    //获取已登录的用户,无则返回null
    public M_OnlineUser GetLogin()
    {
        HttpRequest request = HttpContext.Current.Request;
        HttpResponse response = HttpContext.Current.Response;
        M_UserInfo mu = buser.GetLogin();
        M_OnlineUser model = null;
        if (mu == null || mu.UserID < 1)
        {
            if (request.Cookies["chat"] != null)
            {
                model = new M_OnlineUser();
                model.UserID = request.Cookies["chat"]["chatuid"];
                model.UserName = HttpUtility.UrlDecode(request.Cookies["chat"]["chatuname"]);
                model.UserFace = "";
                model.IsVisitor = true;
                model.LastTime = DateTime.Now;
            }
        }
        else//已登录用户
        {
            model = new M_OnlineUser();
            model.UserID = mu.UserID.ToString();
            model.UserName = mu.HoneyName;
            model.UserFace = mu.UserFace;
            model.LastTime = DateTime.Now;
        }
        if (model != null)
        { AddOnLineUser(model); }
        return model;
    }
    //-------------后台查看聊天记录
    /// <summary>
    /// 以发起人和接收人归并信息,
    /// </summary>
    /// <param name="uids">发起人的UserIDS</param>
    /// <returns></returns>
    public DataTable SelBySR(string uids)
    {
        if (string.IsNullOrEmpty(uids)) return null;
        SafeSC.CheckIDSEx(uids);
        string sql = "SELECT * FROM " + TbName + " WHERE ID IN (SELECT min(ID) FROM " + TbName + " WHERE UserID IN(" + uids + ") GROUP BY UserID,ReceUser)";
        return SqlHelper.ExecuteTable(CommandType.Text, sql);
    }
    /// <summary>
    /// 获取内存中的未读消息
    /// </summary>
    /// <param name="uid"></param>
    /// <returns></returns>
    public List<M_ChatMsg> GetUnreadMsgByUid(string uid)
    {
        string suid = "," + uid + ",";
        List<M_ChatMsg> list = MsgList.Where(p => p.ReceUser.Contains(suid)).ToList();
        return list;
    }
}

//用户上线读取的是最近十分钟的数据
//写入触发条件,满十分钟||满1000条,调用批量插入,将数据写入.
public class ChatRoom
{
    public int ID { get; set; }
    public string Name { get; set; }
    public int RoomTyp { get; set; }
    public string Member { get; set; }
    public int CUser { get; set; }
    public string CUName { get; set; }
}