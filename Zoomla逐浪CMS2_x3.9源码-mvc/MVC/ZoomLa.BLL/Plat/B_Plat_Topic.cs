using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ZoomLa.BLL.Helper;
using ZoomLa.Model.Plat;
using ZoomLa.SQLDAL;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLa.BLL.Plat
{
    public class B_Plat_Topic : ZL_Bll_InterFace<M_Plat_Topic>
    {
        private string TbName, PK;
        private M_Plat_Topic initMod = new M_Plat_Topic();
        public B_Plat_Topic()
        {
            TbName = initMod.TbName;
            PK = initMod.PK;
        }
        public int Insert(M_Plat_Topic model)
        {
            return DBCenter.Insert(model);
        }
        public bool Del(int ID)
        {
            return DBCenter.Del(TbName, PK, ID);
        }
        public M_Plat_Topic SelReturnModel(int ID)
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
        public M_Plat_Topic SelModelByName(int compID,string name)
        {
            string where = "TName=@name ";
            if (compID != -100) { where += " AND CompID=" + compID; }
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("name", name) };
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
        public DataTable Sel()
        {
            return DBCenter.Sel(TbName);
        }
        public DataTable SelWith(string key = "")
        {
            int pcount;
            return SelByPage(1, 50000, out pcount, -100, 0, "all", key);
        }
        /// <summary>
        /// 用于前端页面,话题过滤与分页
        /// </summary>
        public DataTable SelByPage(int cpage, int psize, out int pcount, int compID, int uid, string filter, string key)
        {
            string where = " 1=1 ";
            string order = "A.ID DESC";
            List<SqlParameter> sp = new List<SqlParameter>();
            switch (filter)
            {
                case "star"://星标与普通的贴子
                    where += " AND A.IsStar=1 ";
                    break;
                case "system"://普通贴子
                    where += " AND (A.IsStar IS NULL OR A.IsStar=0) ";
                    break;
                case "me":
                    List<string> topicList = GetMyTopics(uid);
                    string metopwhere = "";
                    for (int i = 0; i < topicList.Count; i++)
                    {
                        string name = "@t" + i;
                        sp.Add(new SqlParameter(name, topicList[i]));
                        metopwhere += name + ",";
                    }
                    where = " A.TName IN (" + metopwhere.Trim(',') + ")";
                    break;
                case "all":
                    break;
                default:
                    throw new Exception("话题筛选类型不正确");
            }
            if (compID != -100) { where += " AND A.CompID=" + compID; }
            if (!string.IsNullOrEmpty(key))
            {
                where += " AND A.TName LIKE @tname";
                sp.Add(new SqlParameter("tname", "%" + key + "%"));
            }
            PageSetting setting = new PageSetting()
            {
                cpage = cpage,
                psize = psize,
                pk = "A.ID",
                fields = "A.*,B.MsgContent,B.CUName",
                t1 = TbName,
                t2 = "ZL_Plat_BlogView",
                on = "A.LastMsgID=B.ID",
                where = where,
                order = order,
                sp = sp.ToArray()
            };
            DataTable dt = DBCenter.SelPage(setting);
            pcount = setting.pageCount;
            return dt;
        }
        /// <summary>
        /// 获取指定用户曾发表过的话题
        /// </summary>
        public List<string> GetMyTopics(int uid)
        {
            List<string> topicList = new List<string>();
            DataTable dt = DBCenter.SelWithField("ZL_Plat_Blog", "Topic", "CUser=" + uid + " AND (Topic IS NOT NULL AND Topic!='') GROUP BY Topic", "");
            foreach (DataRow dr in dt.Rows)
            {
                string[] topicArr = dr["Topic"].ToString().Replace(" ", "").Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                foreach (string topic in topicArr)
                {
                    if (!topicList.Contains(topic)) { topicList.Add(topic); }
                }
            }
            return topicList;
        }
        //public DataTable SelWith(int compID, string filter, string key = "")
        //{
        //    string where = " 1=1 ";
        //    string order = "A.ID DESC";
        //    List<SqlParameter> sp = new List<SqlParameter>();
        //    switch (filter)
        //    {
        //        case "star"://星标与普通的贴子
        //            where += " AND A.IsStar=1 ";
        //            break;
        //        case "system"://普通贴子
        //            where += " AND (A.IsStar IS NULL OR A.IsStar=0) ";
        //            break;
        //        default:
        //            break;
        //    }
        //    if (compID != -100) { where += " AND A.CompID=" + compID; }
        //    if (!string.IsNullOrEmpty(key))
        //    {
        //        where += " AND TName LIKE @tname";
        //        sp.Add(new SqlParameter("tname", "%" + key + "%"));
        //    }
        //    return DBCenter.JoinQuery("A.*,B.MsgContent,B.CUName", TbName, "ZL_Plat_BlogView", "A.LastMsgID=B.ID", where, order, sp.ToArray());
        //}
        ///// <summary>
        ///// 获取所有我的话题
        ///// </summary>
        //public DataTable SelMyTop(int uid)
        //{
        //    DataTable dt = DBCenter.SelWithField("ZL_Plat_Blog", "Topic", "CUser=" + uid + " AND (Topic IS NOT NULL AND Topic!='') GROUP BY Topic", "");
        //    List<string> topicList = new List<string>();
        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        string[] topicArr = dr["Topic"].ToString().Replace(" ", "").Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
        //        foreach (string topic in topicArr)
        //        {
        //            if (!topicList.Contains(topic)) { topicList.Add(topic); }
        //        }
        //    }
        //    if (topicList.Count < 1) { return null; }
        //    //参数化
        //    List<SqlParameter> sp = new List<SqlParameter>();
        //    string where = "";
        //    for (int i = 0; i < topicList.Count; i++)
        //    {
        //        string name = "@t" + i;
        //        sp.Add(new SqlParameter(name, topicList[i]));
        //        where += name + ",";
        //    }
        //    where = " TName IN (" + where.TrimEnd(',') + ")";
        //    return DBCenter.JoinQuery("A.*,B.MsgContent,B.CUName", TbName, "ZL_Plat_BlogView", "A.LastMsgID=B.ID", where, "A.ID DESC", sp.ToArray());
        //}
        public bool UpdateByID(M_Plat_Topic model)
        {
            return DBCenter.UpdateByID(model, model.ID);
        }
        /// <summary>
        /// 增加话题,如果不存则添加(根据名字),后期替换为存储过程,只支持最多五个话题,多则不加
        /// </summary>
        /// <param name="name">支持以,切割的话题字符串</param>
        public void UpdateByName(string topicStr, M_Blog_Msg msgMod)
        {
            topicStr = (topicStr ?? "").Replace(" ", "").Trim(',');
            string[] topicArr = topicStr.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            foreach (string topic in topicArr)
            {
                M_Plat_Topic model = SelModelByName(msgMod.CompID, topic);
                if (model != null)
                {
                    model.Count++;
                    model.LastMsgID = msgMod.ID;
                    model.LastUserID = msgMod.CUser;
                    model.CDate = DateTime.Now;
                    UpdateByID(model);
                }
                else
                {
                    model = new M_Plat_Topic();
                    model.CompID = msgMod.CompID;
                    model.TName = topic;
                    model.Count = 1;
                    model.CDate = DateTime.Now;
                    model.LastMsgID = msgMod.ID;
                    model.LastUserID = msgMod.CUser;
                    Insert(model);
                }
            }
        }
        /// <summary>
        /// 设置话题状态0:false,1:true
        /// </summary>
        /// <param name="flag">star|system</param>
        public void UpdateStatus(string ids, string flag, int status)
        {
            if (string.IsNullOrWhiteSpace(ids)) { return; }
            SafeSC.CheckIDSEx(ids);
            string field = "";
            switch (flag)
            {
                case "star":
                    field = "IsStar";
                    break;
                case "system":
                    field = "IsSystem";
                    break;
                default:
                    throw new Exception("类型不正确");
            }
            DBCenter.UpdateSQL(TbName, field + "=" + status, PK + " IN(" + ids + ")");
        }
    }
}
