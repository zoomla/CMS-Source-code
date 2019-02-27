using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using ZoomLa.BLL.Helper;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Model.Message;
using ZoomLa.SQLDAL;
using ZoomLa.SQLDAL.SQL;
using ConStatus = ZoomLa.Model.ZLEnum.ConStatus;

namespace ZoomLa.BLL
{
    public class B_Guest_Bar : ZL_Bll_InterFace<M_Guest_Bar>
    {
        private string TbName, PK, TbView = "ZL_Guest_BarView";
        private M_Guest_Bar initMod = new M_Guest_Bar();
        public B_Guest_Bar()
        {
            TbName = initMod.TbName;
            PK = initMod.PK;
        }
        public int Insert(M_Guest_Bar model)
        {
            if (model.Pid > 0)
            {
                UpdateR_Info(model);
            }
            return DBCenter.Insert(model);
        }
        public bool UpdateByID(M_Guest_Bar model)
        {
            DBCenter.UpdateByID(model, model.ID);
            return true;
        }
        /// <summary>
        /// 根据回贴，更新主题贴数据(不用SQL查询,性能消耗过大)
        /// </summary>
        /// <param name="model"></param>
        public void UpdateR_Info(M_Guest_Bar model)
        {
            List<SqlParameter> sp = new List<SqlParameter>() { new SqlParameter("uname", model.CUName), new SqlParameter("time", DateTime.Now) };
            DBCenter.UpdateSQL(TbName, "R_CUser=" + model.CUser + ",R_CUName=@uname,R_CDate=@time", "ID=" + model.Pid, sp);
        }
        public bool Del(int ID)
        {
            return DBCenter.Del(TbName, PK, ID);
        }
        public bool DelByCid(int cateid)
        {
            return DBCenter.Del(TbName, "CateID", cateid);
        }
        public M_Guest_Bar SelReturnModel(int ID)
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
        /// 获取指定用户最后一个主题贴[?]
        /// </summary>
        public M_Guest_Bar SelLastModByUid(M_UserInfo mu, bool istopic = true)
        {
            //IDCode
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("IDCode", mu.UserID <= 0 ? mu.WorkNum : mu.UserID.ToString()) };
            string where = "IDCode=@idcode And Pid" + (istopic ? "=0" : ">0") + " Order BY CDate DESC";//是取主题贴,还是回复贴
            using (DbDataReader reader = DBCenter.SelReturnReader(TbName, where, sp))
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
        /// 根据贴子ID,返回贴子的贴吧ID
        /// </summary>
        public int SelCateIDByPost(int postid)
        {
            return SelReturnModel(postid).CateID;
        }
        public System.Data.DataTable Sel()
        {
            return DBCenter.Sel(TbName);
        }
        public PageSetting SelPage(int cpage, int psize)
        {
            PageSetting setting = PageSetting.Single(cpage, psize, TbName, PK, "");
            DBCenter.SelPage(setting);
            return setting;
        }
        /// <summary>
        /// 按标题搜索,不包含未开放贴吧的内容(指定用户)
        /// </summary>
        public DataTable SelByTitle(string skey)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("Title", "%" + skey + "%") };
            return DBCenter.JoinQuery("A.*,B.CateName,(Select COUNT(ID) From " + TbName + " Where Pid=A.ID ) RCount", TbName, "ZL_Guestcate", "B.Cateid=A.CateID", " A.Pid=0 AND A.Title Like @Title", "", sp);
        }
        public DataTable SelByUid(int uid)
        {
            return DBCenter.JoinQuery("A.*,B.CateName,(Select COUNT(ID) From " + TbName + " Where Pid=A.ID ) RCount", TbName, "ZL_Guestcate", "B.Cateid=A.CateID", "A.Pid=0 And A.Status>0 And CUser=" + uid);
        }

        public DataTable SelByLikes(int uid)
        {
            return DBCenter.JoinQuery("A.*,B.CateName,(Select COUNT(ID) From " + TbName + " Where Pid=A.ID ) RCount", TbName, "ZL_Guestcate", "B.Cateid=A.CateID", "A.Pid=0 And A.Status>0 And A.LikeIDS Like '%" + uid + ",%'");
        }
        //用于全站搜索
        public DataTable SelByTitle2(string title)
        {
            //自我描述PageUrl
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("Title", "%" + title + "%") };
            DataTable dt = DBCenter.JoinQuery("A.ID,A.Title,A.CDate CreateTime,TagKey='',PageUrl='',A.CateID", TbName, "ZL_Guestcate", "B.Cateid=A.CateID", " B.NeedLog!=2 AND A.Title Like @Title", "", sp);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["PageUrl"] = B_Guest_Bar.CreateUrl(1, Convert.ToInt32(dt.Rows[i]["cateid"]));
            }
            return dt;
        }
        /// <summary>
        /// 搜索贴吧,留言,百科,用于百科左边栏聚合搜索
        /// </summary>
        /// <param name="skey">关键词</param>
        /// <param name="list">来源</param>
        /// <returns></returns>
        public DataTable SelAllByTitle(string skey, string source)
        {
            source = string.IsNullOrEmpty(source) ? "" : source;
            string sql = "Select A.*,B.UserName From (SELECT ID,CUser,CDate,Title,CateID,SType=1 FROM ZL_Guest_Bar Where Title Like @skey";
            if (source.Equals("bar"))//只取贴吧
            {

            }
            else
            {
                sql += " UNION SELECT Gid AS ID,Userid AS CUser,Gdate as CDate, Title,CateID,SType=2 FROM ZL_Guestbook Where Title Like @skey"
                    + " UNION SELECT ID,UserId AS CUser,AddTime AS CDate,Qcontent AS Title,CateID=-1,SType=3 FROM ZL_Ask Where Qcontent Like @skey";
            }
            sql += " ) A LEFT JOIN ZL_User B ON A.CUser=B.UserID";
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("skey", "%" + skey + "%") };
            return SqlHelper.ExecuteTable(CommandType.Text, sql, sp);
        }
        public DataTable SelByID(int id)
        {
            return DBCenter.JoinQuery("A.*,B.salt AS UserFace,B.HoneyName", "ZL_Guest_Bar", "ZL_User", "A.CUser=B.UserID", "A.ID=" + id + " OR A.Pid=" + id + " AND (A.Status>0)", "PID,CDate ASC");
        }
        //PostContent
        public DataTable SelByPid(int pid)
        {
            return DBCenter.JoinQuery("A.*,B.salt AS UserFace,B.HoneyName", "ZL_Guest_Bar", "ZL_User", "A.CUser=B.UserID", "Pid=" + pid + " And A.Status>0", "CDATE ASC");
        }
        public DataTable SelByCateID(int pageSize, int curPage, out int itemCount, string key, int flag = 1, bool isaudit = false)
        {
            string where = " Pid=0 ";
            switch (flag)
            {
                case 1://cateid查询
                    where += "AND CateID=@id ";
                    break;
                case 2://用户ID查询
                    where += "AND CUser=@id ";
                    break;
                case 3://关键字查询
                    where += "AND Title LIKE @key ";
                    break;
                case 4://收藏查询
                    where += "AND LikeIDS LIKE @key ";
                    break;
            }
            if (isaudit)
            {
                where += " AND Status>0 And Status IS NOT NULL";
            }
            string order = "OrderFlag DESC,R_CDate DESC,CDate DESC";
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("id", key), new SqlParameter("key", "%" + key + "%") };
            return DBCenter.SelPage(pageSize, curPage, out itemCount, "ID", "*", TbView, where, order, sp);
        }
        //PostList
        public DataTable SelByCateID(string cid, int flag = 1, bool isaudit = true)
        {
            List<SqlParameter> splist = new List<SqlParameter>();
            string where = "A.Pid=0 ";
            switch (flag)
            {
                case 1://cateid查询
                    where += "AND (A.CateID=@Title OR A.OrderFlag=2) ";
                    splist.Add(new SqlParameter("Title", cid));
                    break;
                case 2://用户ID查询
                    where += "AND A.CUser=@Title ";
                    splist.Add(new SqlParameter("Title", cid));
                    break;
                case 3://关键字查询
                    string[] searchs = cid.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                    string searwhere = "";
                    for (int i = 0; i < searchs.Length; i++)
                    {
                        splist.Add(new SqlParameter("SearchStr" + i, "%" + searchs[i] + "%"));
                        searwhere += " OR Title LIKE @SearchStr" + i;
                    }
                    where += "AND " + string.Format("(1=2 {0})", searwhere);// "A.Title Like @Title ";
                    break;
                case 4://收藏查询
                    splist.Add(new SqlParameter("Title", "%," + cid + ",%"));
                    where += "AND A.ColledIDS Like @Title ";
                    break;
            }
            if (isaudit)//是否必须审核
            {
                where += "And ((A.Status>0 And A.Status is not null) OR (A.Status=0 AND A.C_Status=3) OR A.C_Status=1) AND A.Status!=" + (int)ConStatus.Recycle;
            }
            return DBCenter.Sel(TbView, where, "OrderFlag DESC,R_CDate DESC,A.CDate DESC", splist);
        }
        public DataTable SelByCateIDS(string ids, string key = "", int status = -10)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("key", "%" + key + "%") };
            string wherestr = "";
            if (!string.IsNullOrEmpty(ids))
            {
                SafeSC.CheckIDSEx(ids);
                wherestr += " AND A.CateID IN (" + ids + ")";
            }
            if (!string.IsNullOrEmpty(key))
                wherestr += " And Title Like @key";
            if (status != -10)
            {
                wherestr += " And A.[Status]=" + status;
            }
            wherestr += " AND B.GType=1";
            return DBCenter.JoinQuery("A.*,B.CateName,(Select COUNT(ID) From " + TbName + " Where Pid=A.ID ) RCount", TbName, "ZL_GuestCate", "A.CateID=B.CateID", "A.Pid=0" + wherestr, "OrderFlag Desc,A.CDate Desc", sp);
        }
        public DataTable SelByStatus(int status)
        {
            return DBCenter.JoinQuery("a.*,b.CateName", TbName, "ZL_GuestCate", "a.CateID=b.CateID", "A.Status=" + status);
        }
        //获取焦点数据
        public DataTable SelFocus()
        {
            //后期改为不获取加密社区
            int itemcount = 0;
            return DBCenter.SelPage(10, 1, out itemcount, "A.ID", "a.*,b.CateName", TbName, "ZL_GuestCate", "a.CateID=b.CateID", "Pid=0 And A.Status>0 AND CateName !='华成会' AND B.GType=1", "CDate Desc");
        }
        //本周热门,如本周无数据,则取总数据
        public DataTable SelTop(int num)
        {
            string stime = "", etime = "";
            DateHelper.GetWeekSE(DateTime.Now, ref stime, ref etime);
            Sql_Where whereMod = new Sql_Where() { join = "AND", field = "CDate", type = "date", stime = stime, etime = etime };
            string where = "Pid = 0 And Status != " + ((int)ConStatus.Recycle);
            PageSetting setting = new PageSetting()
            {
                psize = num,
                cpage = 1,
                pk = PK,
                t1 = TbName,
                fields = "*",
                where = where + DBCenter.DB.GetDateSql(whereMod),
                order = "HitCount DESC"
            };
            DataTable dt = DBCenter.SelPage(setting);
            if (dt.Rows.Count < 1) { setting.where = where; dt = DBCenter.SelPage(setting); }
            dt.Columns.Add(new DataColumn("IndexNum", typeof(int)));
            for (int i = 0; i < dt.Rows.Count; i++) { dt.Rows[i]["IndexNum"] = (i + 1); }
            return dt;
        }
        //首页抽取最近的一个精华
        public DataTable SelTop1(int num)
        {
            int itemCount = 0;
            return DBCenter.SelPage(num, 1, out itemCount, PK, "A.*", TbName, "ZL_Guestcate", "A.Cateid=B.Cateid", "Subtitle Like '%<img%' AND PostFlag Like '%Recommend%' And B.NeedLog!=2", "CDate Desc");
        }
        public int DelByUID(int uid)
        {
            int count = DBCenter.Count(TbName, "CUser = " + uid + " AND Status != " + ((int)ConStatus.Recycle));
            DBCenter.UpdateSQL(TbName, "Status=" + ((int)ConStatus.Recycle), "CUser=" + uid);
            return count;
        }
        public bool DelByUID(int id, int uid)
        {
            return DBCenter.UpdateSQL(TbName, "Status=" + (int)ConStatus.Recycle, "ID=" + id + " And CUser=" + uid);
        }
        /// <summary>
        /// 吧主删贴
        /// </summary>
        public void DelByIDS(int cateid, string ids, bool realdeal = false)
        {
            if (string.IsNullOrEmpty(ids)) { return; }
            SafeSC.CheckIDSEx(ids);
            if (realdeal) { DBCenter.DelByIDS(TbName, PK, ids); }
            else { DBCenter.UpdateSQL(TbName, "Status=" + (int)ConStatus.Recycle, "ID IN(" + ids + ") AND CateID=" + cateid); }
        }
        public void RelByIDS(string ids)
        {
            SafeSC.CheckIDSEx(ids);
            DBCenter.UpdateSQL(TbName, "Status=" + (int)ConStatus.UnAudit, "ID IN (" + ids + ")");
        }
        /// <summary>
        /// 审核贴子
        /// </summary>
        public void CheckByIDS(string ids)
        {
            if (string.IsNullOrEmpty(ids)) { return; }
            SafeSC.CheckIDSEx(ids);
            DBCenter.UpdateSQL(TbName, "Status=" + (int)ConStatus.Audited, "ID IN (" + ids + ")");
        }
        /// <summary>
        /// 删除贴子
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="realdel">true:真正删除,false:更改状态</param>
        public void DelByIDS(string ids, bool realdel = false)
        {
            SafeSC.CheckIDSEx(ids);
            if (realdel) { DBCenter.DelByIDS(TbName, PK, ids); }
            else { DBCenter.UpdateSQL(TbName, "Status=" + (int)ConStatus.Recycle, "ID IN(" + ids + ")"); }
        }
        /// <summary>
        /// 清空回收站
        /// </summary>
        public bool DelAll(int cateid)
        {
            return DBCenter.DelByWhere(TbName, "CateID = " + cateid + "AND Status=" + (int)ConStatus.Recycle);
        }
        public void UnCheckByIDS(string ids)
        {
            SafeSC.CheckIDSEx(ids);
            DBCenter.UpdateSQL(TbName, "Status=" + ((int)ConStatus.UnAudit), "ID IN(" + ids + ")");
        }
        public DataTable SelByIDS(string ids)
        {
            SafeSC.CheckIDSEx(ids);
            return DBCenter.Sel(TbView, "ID IN(" + ids + ")");
        }

        public bool UpdateStatus(int cateid, string ids, int status)
        {
            SafeSC.CheckIDSEx(ids);
            return DBCenter.UpdateSQL(TbName, "Status=" + status, "ID IN(" + ids + ") And CateID=" + cateid);
        }
        /// <summary>
        /// 主题数,回贴数
        /// </summary>
        public void GetCount(int cateid, out int mcount, out int recount)
        {
            mcount = DBCenter.Count(TbName, "Pid=0 And CateID=" + cateid);
            recount = DBCenter.Count(TbName, "Pid!=0 And CateID=" + cateid);
        }
        /// <summary>
        /// 置顶操作
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="status">1,版面置顶;2,全局置顶;0,取消置顶;-1,沉底</param>
        public void UpdateOrderFlag(string ids, int status)
        {
            SafeSC.CheckIDSEx(ids);
            DBCenter.UpdateSQL(TbName, "OrderFlag = " + status, "ID IN (" + ids + ") AND Pid=0");
        }
        public void UpdateRecommend(string ids, bool isadd)
        {
            AddPostFlag(ids, "Recommend", isadd);
        }
        /// <summary>
        /// 为贴子增加标记
        /// </summary>
        /// <param name="flag">置顶等标记</param>
        private void AddPostFlag(string ids, string flag, bool isadd)
        {
            DBCenter.UpdateSQL(TbName, "PostFlag=''", "PostFlag IS NULL");
            SafeSC.CheckIDSEx(ids);
            List<SqlParameter> sp = new List<SqlParameter>() { new SqlParameter("flag", "," + flag + ",") };
            string set = "", where = "";
            if (isadd)
            {
                sp.Add(new SqlParameter("flag2", "%," + flag + ",%"));
                set = DBCenter.GetSqlByDB("PostFlag=PostFlag+@flag", "PostFlag=PostFlag||:flag");
                where = "ID IN (" + ids + ") And (PostFlag NOT Like @flag2 OR POSTFLAG IS NULL) AND Pid=0";//兼容Oracle,增加is null
            }
            else
            {
                set = DBCenter.GetSqlByDB("PostFlag=REPLACE(REPLACE(PostFlag,@flag,','),',,',',')", "PostFlag=REPLACE(REPLACE(PostFlag,@flag,''),',,',',')");
                where = "ID IN (" + ids + ")";
            }
            DBCenter.UpdateSQL(TbName, set, where, sp);
        }
        /// <summary>
        /// 昨日发贴主数,今日发贴总数,共有主题个数
        /// </summary>
        public string SelYTCount(string cateids = "")
        {
            string where = "Pid=0";
            if (!string.IsNullOrEmpty(cateids))
            {
                SafeSC.CheckIDSEx(cateids);
                where += " AND CateID IN (" + cateids + ")";
            }
            int total = DBCenter.Count(TbName, where);//TotalCount
            int tcount = DBCenter.Count(TbName, where, null, new Sql_Where() { type = "date", join = "AND", field = "CDate", stime = DateTime.Now.ToString("yyyy-MM-dd 00:00:00"), etime = DateTime.Now.ToString("yyyy-MM-dd 23:59:59") });
            int ycount = DBCenter.Count(TbName, where, null, new Sql_Where() { type = "date", join = "AND", field = "CDate", stime = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd 00:00:00"), etime = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd 23:59:59") });
            return ycount + ":" + tcount + ":" + total;
        }
        public void AddHitCount(int id)
        {
            //需测试是否支持+1(支持)
            DBCenter.UpdateSQL(TbName, "HitCount=0", "ID=" + id + " AND  HitCount is null");
            DBCenter.UpdateSQL(TbName, " HitCount=HitCount + 1", "ID = " + id);
        }
        /// <summary>
        /// 转移贴子
        /// </summary>
        public void ShiftPost(string ids, int cateid)
        {
            SafeSC.CheckIDSEx(ids);
            DBCenter.UpdateSQL(TbName, "CateID = " + cateid, "ID IN(" + ids + ")");
        }
        public bool LikeTie(int id, int uid, int action, string field = "LikeIDS")
        {
            //需测试是否支持Replace
            if (uid < 1) { return false; }
            string suid = "'," + uid + ",'";
            switch (field)
            {
                case "ColledIDS":
                    break;
                default:
                    field = "LikeIDS";
                    break;
            }
            string set = "";
            switch (action)
            {
                case 1://增加
                    set = string.Format("{0} = {0}+" + suid, field);
                    break;
                case 2://移除
                    set = string.Format("{0} = REPLACE(REPLACE({0}," + suid + ",','),',,',',')", field);
                    break;
            }
            return DBCenter.UpdateSQL(TbName, set, "ID=" + id);
        }
        //同步到能力中心的贴子
        public DataTable SelBlogBar(int cid)
        {
            int itemcount = 0;
            string fields = "A.ID,A.CUName,A.Title,A.CateID,A.Status,A.Pid,A.HoneyName,B.Cateid";
            string where = "B.Cateid=" + cid + " AND A.Pid=0 AND A.Status>0 AND (B.Status=1 OR A.Status=" + (int)ConStatus.Recycle + ")";
            return DBCenter.SelPage(8, 1, out itemcount, PK, fields, TbView, "ZL_Guestcate", "A.CateID=B.Cateid", where, "CDate DESC");
        }
        //开启了同步到能力中心的版块
        public DataTable SelBlogCate(int compid)
        {
            return DBCenter.Sel("ZL_GuestCate", "IsPlat = " + compid + " AND ParentID > 0");
        }
        //-----Tools
        /// <summary>
        /// 生成链接
        /// </summary>
        /// <param name="type">1:栏目,2:贴子</param>
        public static string CreateUrl(int type, int id, int cpage = 1)
        {
            string url = "";
            switch (type)
            {
                case 1://栏目
                    url = "/PClass";
                    if (id > 0)
                        url += "?id=" + id + (cpage > 1 ? "&cpage=" + cpage : "");
                    break;
                case 2://贴子
                    url = "/PItem?id=" + id + (cpage > 1 ? "&cpage=" + cpage : "");
                    break;
            }
            return url;
        }
        public string GetUName(object nick, object name)
        {
            return (nick == DBNull.Value || nick == null || string.IsNullOrEmpty(nick.ToString())) ? name.ToString() : nick.ToString();
        }
        //public M_UserInfo GetUser(string uname = "匿名用户")
        //{
        //    M_UserInfo mu = new M_UserInfo();
        //    B_User buser = new B_User();
        //    if (buser.CheckLogin())
        //    {
        //        mu = buser.GetLogin();
        //    }
        //    else //匿名逻辑
        //    {
        //        mu.UserName = uname;
        //        mu.UserID = 0;
        //        mu.WorkNum = function.GetRandomString(6);
        //        mu.RegTime = DateTime.Now;
        //        ZLCache.AddUser(buser.SessionID, mu);
        //    }
        //    return mu;
        //}
    }
}
