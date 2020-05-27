using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using ZoomLa.SQLDAL;
using ZoomLa.Model.Plat;
using System.Data.SqlClient;
using ZoomLa.Model;
using ZoomLa.BLL.Helper;
using ZoomLa.SQLDAL.SQL;
using System.Data.Common;

namespace ZoomLa.BLL.Plat
{
    public class B_Blog_Msg : ZL_Bll_InterFace<M_Blog_Msg>
    {
        private string TbName, TbView = "ZL_Plat_BlogView", PK;
        private M_Blog_Msg initMod = new M_Blog_Msg();
        public B_Blog_Msg()
        {
            TbName = initMod.TbName;
            PK = initMod.PK;
        }
        public int Insert(M_Blog_Msg model)
        {
            model.ID = DBCenter.Insert(model);
            if (!string.IsNullOrEmpty(model.Topic))
            {
                new B_Plat_Topic().UpdateByName(model.Topic, model);
            }
            return model.ID;
        }
        public bool UpdateByID(M_Blog_Msg model)
        {
            return DBCenter.UpdateByID(model,model.ID);
        }
        /// <summary>
        /// 增加,移除ColledIDS中的该用户ID
        /// </summary>
        /// <action>1:增加,2移除</action>
        public bool UpdateColled(int uid, int id, int action)
        {
            string ids = DBCenter.ExecuteScala(TbName, "ColledIDS", "ID=" + id).ToString();
            switch (action)
            {
                case 1://增加
                    ids = StrHelper.AddToIDS(ids, uid.ToString());
                    break;
                case 2://移除
                    ids = StrHelper.RemoveToIDS(ids, uid.ToString());
                    break;
            }
            return DBCenter.UpdateSQL(TbName, "ColledIDS='" + ids + "'", "ID=" + id);
        }
        public bool Del(int ID)
        {
            return DBCenter.Del(TbName,PK,ID);
        }
        public bool DelByUID(int id,int uid) 
        {
            return DBCenter.UpdateSQL(TbName, "Status=0", "ID=" + id + " And CUser=" + uid);
        }
        public M_Blog_Msg SelReturnModel(int ID)
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
        #region 查询
        public DataTable Sel()
        {
            return DBCenter.Sel(TbView, "", "CDate Desc");
        }
        public int SelByDateForNotify(string date, M_User_Plat upMod)
        {
            if (upMod.UserID < 1 || upMod.CompID < 1) { return 0; }
            if (string.IsNullOrEmpty(date)) { return 0; }
            List<SqlParameter> sp = new List<SqlParameter>() { new SqlParameter("date", date) };
            return DBCenter.Count(TbView, "CDate >@date AND Pid=0 AND CompID=" + upMod.CompID + " AND CUser!=" + upMod.UserID, sp);
        }
        /// <summary>
        /// plat,bar
        /// </summary>
        public DataTable Sel(int pid, string form = "")
        {
            string where = "pid=" + pid;
            List<SqlParameter> sp = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(form))
            {
                sp.Add(new SqlParameter("form", form));
                where += " AND Source=@form ";
            }
            return DBCenter.JoinQuery("A.*,B.Salt AS UserFace", TbView, "ZL_User", "A.CUser=B.UserID", where, "CDate DESC", sp.ToArray());
        }
        /// <summary>
        /// 获取留言列表,附带回复者的用户名与用户ID,需改为存储过程
        /// </summary>
        public DataTable SelByPid(int pageSize, int curPage, out int totalCount, int pid = 0)
        {
            return SelByPid(pageSize, curPage, out totalCount, pid, null, "ALL", 0, "", "", "", "", "", 0, "A.CDate ASC");
        }
        /// <summary>
        /// 获取并筛选数据,回复留言不过滤Gids,排序推荐不更改,否则会抽不出贴吧的信息
        /// </summary>
        /// <param name="filter">colled,onlyme,notme传参,</param>
        /// <param name="msgtype">发文类型,为空表示全部</param>
        /// <param name="proid">项目ID,项目外部发的均为0</param>
        /// <param name="uids">查看他人,组的详情,IDS</param>
        public DataTable SelByPid(int psize, int cpage, out int pageCount, int pid, M_User_Plat upMod, string gids, int proid, string filter = "", string msgtype = "", string skey = "", string uids = "", string dateStr = "", int msgid = 0, string Order = "A.CDate Desc")
        {
            Order = "A.CDate Desc";
            string where = " A.Pid=" + pid + " AND A.Status>0 ";
            if (msgid > 0) { where += " AND A.ID=" + msgid; }
            List<SqlParameter> sp = new List<SqlParameter>() { new SqlParameter("skey", "%" + skey + "%") };
            if (gids.Equals("ALL"))//不过滤,用于显示回复信息
            { }
            else if (string.IsNullOrEmpty(gids.Replace(",", "").Replace(" ", "")))//无组信息,则直接返回
            {
                where += " AND (A.GroupIDS='' OR A.GroupIDS IS NULL OR A.CUser=" + upMod.UserID + ") ";//抽取所有人(不包含组)的信息
            }
            else
            {
                SafeSC.CheckIDSEx(gids);
                where += " And ({0} OR A.CUser=" + upMod.UserID + " OR A.GroupIDS='') ";
                string gidSql = "";
                string[] gidArr = gids.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string gid in gidArr)
                {
                    gidSql += " A.GroupIDS Like '%," + gid + ",%' OR";
                }
                where = string.Format(where, gidSql.TrimEnd(new char[] { 'O', 'R' }));
            }
            //------CompID过滤
            if (upMod != null)
            {
                where += " And A.CompID=" + upMod.CompID;
            }
            //------ProID过滤
            //where += " And A.ProID=" + proid;
            //------查看他人详情UIDS
            if (!string.IsNullOrEmpty(uids))
            {
                SafeSC.CheckIDSEx(uids);
                where += " And A.CUser in(" + uids + ")";
            }
            //------Filter过滤
            if (!string.IsNullOrEmpty(filter))//支持多条件
            {
                string suid = "'%," + upMod.UserID + ",%'";
                filter = filter.ToLower().Trim();
                if (filter.Contains("colled"))//我收藏的
                {
                    where += " And A.ColledIDS Like " + suid;
                }
                if (filter.Contains("onlyme"))//只看我的
                {
                    where += " And A.CUser=" + upMod.UserID;
                }
                if (filter.Contains("atuser"))//@我的
                {
                    where += " And A.ATUser Like " + suid;
                }
            }
            //------MsgType过滤
            if (!string.IsNullOrEmpty(msgtype))//支持,号分隔多条件,暂不支持
            {
                SafeSC.CheckIDSEx(msgtype);
                where += " And A.MsgType in(" + msgtype + ")";
            }
            if (!string.IsNullOrEmpty(skey))
            {
                where += " And (A.Title LIKE @skey OR A.MsgContent Like @skey)";
            }
            //-----日期过滤
            if (!string.IsNullOrWhiteSpace(dateStr))
            {
                string stime = "", etime = "";
                DateHelper.GetSETime(dateStr, ref stime, ref etime);
                if (!string.IsNullOrEmpty(stime)) { where += " AND CDate>=@stime "; sp.Add(new SqlParameter("stime",stime)); }
                if (!string.IsNullOrEmpty(etime)) { where += " AND CDate<=@etime "; sp.Add(new SqlParameter("etime", etime)); }
            }
            PageSetting setting = new PageSetting()
            {
                psize = psize,
                cpage = cpage,
                pk = "A.ID",
                t1 = TbView,
                t2 = "ZL_User",
                fields = "A.*,B.Salt AS UserFace,B.UserName,B.HoneyName",
                on = "A.CUser=B.UserID",
                where = where,
                order = Order,
                sp = sp.ToArray()
            };
            DataTable dt = DBCenter.SelPage(setting);
            pageCount = setting.pageCount;

            //ZLLog.L(setting.sql+"||||"+setting.countsql);
            return dt;
        }
        /// <summary>
        /// 用于发送消息后直接返回查看,限定用户ID(安全)
        /// </summary>
        public DataTable SelByPid(int id, int uid)
        {
            PageSetting setting = new PageSetting()
            {
                psize = 1,
                cpage = 1,
                pk = "A.ID",
                t1 = TbView,
                t2 = "ZL_User",
                fields = "A.*,B.Salt AS UserFace,B.UserName,B.HoneyName",
                on = "A.CUser=B.UserID",
                where = "A.CUser=" + uid + " AND A.ID=" + id
            };
            return DBCenter.SelPage(setting);
        }
        /// <summary>
        /// 选取拥有附件的分享
        /// </summary>
        public DataTable SelHasAttach(int uid) 
        {
            return DBCenter.Sel(TbView, "CUser=" + uid + " And Attach Is Not Null And Attach !=''");
        }
        public DataTable GetContentByID(int id)
        {
            return DBCenter.SelWithField(TbView, "MsgContent,CDate", "ID=" + id);
        }
        #endregion
        //-----------------Vote相关
        /// <summary>
        /// 增加投票结果,投票后不能移除
        /// </summary>
        public void AddUserVote(int id, int opid, int uid) //留言ID，选项ID,用户ID
        {
            M_Blog_Msg model = SelReturnModel(id);
            string result = "," + opid + ":" + uid + ",";
            if (!string.IsNullOrEmpty(model.VoteResult))
            {
                result = (model.VoteResult + result).Replace(",,", ",");
            }
            List<SqlParameter> sp = new List<SqlParameter>() { new SqlParameter("VoteResult", result) };
            DBCenter.UpdateSQL(TbName, "VoteResult=@VoteResult", "ID=" + id, sp);
        }
        //按选项归类
        public DataTable GetVoteResultDT(string voteOP, string voteResult)
        {
            //opid是选项的ID,按其前后生成,从1开始
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("opid", typeof(int)));
            dt.Columns.Add(new DataColumn("opName", typeof(string)));
            dt.Columns.Add(new DataColumn("count", typeof(double)));//实际人数
            dt.Columns.Add(new DataColumn("percent", typeof(double)));//所占百分比

            string[] voteArr = voteResult.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < voteOP.Split(',').Length; i++)
            {
                DataRow dr = dt.NewRow();
                dr["opid"] = i;
                dr["opname"] = voteOP.Split(',')[i];
                int count = voteArr.Where(p => p.Split(':')[0].Equals(i.ToString())).ToArray().Length;//指定opid有多少人选了
                dr["count"] = count;
                dr["percent"] = (Convert.ToDouble(count) / Convert.ToDouble(voteArr.Length)) * 100;
                dt.Rows.Add(dr);
            }
            return dt;
        }
        //--------临时，后期移除,使用连接查询并入
        public int GetSumCount(int pid)
        {
            return DBCenter.Count(TbName, "pid=" + pid + " AND Status>0");
        }
        /// <summary>
        /// true:真正删除
        /// </summary>
        public bool DelByIds(string ids, bool flag = false)
        {
            SafeSC.CheckIDSEx(ids);
            if (flag) { DBCenter.DelByIDS(TbName, PK, ids); }
            else { DBCenter.UpdateSQL(TbName, "Status=0", "ID IN (" + ids + ")"); }
            return true;
        }
        /// <summary>
        /// 恢复信息
        /// </summary>
        public void RelByIDS(string ids)
        {
            SafeSC.CheckIDSEx(ids);
            DBCenter.UpdateSQL(TbName, "Status=1", "ID IN (" + ids + ")");
        }
        public void ClearRecycle() 
        {
            DBCenter.DelByWhere(TbName,"Status=0");
        }
    }
}
