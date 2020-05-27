using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ZoomLa.Common;
using ZoomLa.Model.Plat;
using ZoomLa.SQLDAL;

namespace ZoomLa.BLL.Plat
{
    //需要用视图
    public class B_Plat_Group:ZL_Bll_InterFace<M_Plat_Group>
    {
        private string TbName, PK;
        private M_Plat_Group initMod = new M_Plat_Group();
        public B_Plat_Group()
        {
            TbName = initMod.TbName;
            PK = initMod.PK;
        }
        public int Insert(M_Plat_Group model)
        {
            model.OrderID = Order_GetMax(model.Pid);
            return DBCenter.Insert(model);
        }
        /// <summary>
        /// 指定组添加成员
        /// </summary>
        /// <param name="flag">1:添加成员,2:添加管理员(需要验证用户权限是否为网络管理员用户s)</param>
        public void AddMember(string ids, int gid, int flag = 1)
        {
            SafeSC.CheckDataEx(ids);
            string uids = "", uids2 = "", set = "";
            M_Plat_Group model = SelReturnModel(gid);
            uids = CombineIDS(model.MemberIDS, ids);
            switch (flag)
            {
                case 1:
                    set = "MemberIDS= '" + uids + "'";
                    break;
                case 2://添加为管理员的同时也添加为成员
                    uids2 = CombineIDS(model.ManageIDS, ids);
                    set = "MemberIDS='" + uids + "',ManageIDS='" + uids2 + "'";
                    break;
            }
            DBCenter.UpdateSQL(TbName, set, "ID=" + gid);
        }
        /// <summary>
        /// 移除用户,用法同于AddMember
        /// </summary>
        /// <param name="ids">需移除的用户</param>
        public void DelMember(string ids, int gid, int flag = 0)
        {
            SafeSC.CheckDataEx(ids);
            string uids = "", uids2 = "", set = "";
            M_Plat_Group model = SelReturnModel(gid);
            switch (flag)
            {
                case 0://管理员与成员
                    uids = function.RemoveRepeat(model.MemberIDS, ids);
                    uids2 = function.RemoveRepeat(model.ManageIDS, ids);
                    set = "MemberIDS= '" + uids + "',ManageIDS='" + uids2 + "'";
                    break;
                case 1:
                    uids = function.RemoveRepeat(model.MemberIDS, ids);
                    set = "MemberIDS= '" + uids + "'";
                    break;
                case 2:
                    uids = function.RemoveRepeat(model.ManageIDS, ids);
                    set = "ManageIDS= '" + uids + "'";
                    break;
            }
            DBCenter.UpdateSQL(TbName, set, "ID=" + gid);
        }
        public bool UpdateByID(M_Plat_Group model)
        {
            return DBCenter.UpdateByID(model, model.ID);
        }
        public bool Del(int ID)
        {
            return DBCenter.Del(TbName, PK, ID);
        }
        public M_Plat_Group SelReturnModel(int ID)
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
        public DataTable Sel() 
        {
            return DBCenter.Sel(TbName);
        }
        /// <summary>
        /// 我创建的,我加入的,我管理的
        /// </summary>
        public DataTable SelGroupByAuth(int uid, int filter = 0)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("uids", "%," + uid + ",%") };
            switch (filter)
            {
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
            }
            return DBCenter.JoinQuery("a.*,b.UserName", TbName, "ZL_User", "a.CreateUser=b.UserID", "a.CreateUser=" + uid + " OR ','+a.ManageIDS+',' Like @uids OR ','+a.MemberIDS+',' Like @uids", "", sp);
        }
        /// <summary>
        /// 获取本公司下所有群组,用于管理页
        /// </summary>
        public DataTable SelByCompID(int compID, int pid = -100)
        {
            //string sql = "with Tree as(SELECT * FROM ZL_Node WHERE ParentID=" + pid + " UNION ALL SELECT a.* FROM ZL_Node a JOIN Tree b on a.ParentID=b.NodeID) SELECT *,(SELECT Count(NodeID) From ZL_Node WHERE A.NodeID=ParentID) ChildCount FROM Tree AS A ORDER BY OrderID,NodeID ASC";
            string where = "B.CompID=" + compID;
            if (pid != -100) { where += " AND Pid=" + pid; }
            DataTable dt = DBCenter.JoinQuery("A.*,b.UserName", TbName, "ZL_User_PlatView", "a.CreateUser=b.UserID", where, "OrderID ASC,ID DESC");
            DataTable result = dt.Clone();
            AddToDT(dt, result, 0);
            return result;
        }
        /// <summary>
        /// 返回我的GIDS,便于筛选
        /// </summary>
        public string GetMyGids(int uid)
        {
            DataTable dt = SelGroupByAuth(uid);
            string gids = "";
            foreach (DataRow dr in dt.Rows)
            {
                gids += dr["ID"]+",";
            }
            return gids.TrimEnd(',');
        }
        //--------------------------------------------------------------
        /// <summary>
        /// 返回由0开始的深度(0=除根目录外的第一级目录)
        /// </summary>
        public int GetDepth(int pid)
        {
            if (pid == 0) { return 0; }
            string tree = "";
            SelFirstID(pid, ref tree);
            return tree.Trim(',').Split(',').Length;
        }
        public int SelFirstID(int id, ref string nodeTree)
        {
            if (id < 1) { return 0; }
            string sql = "with f as(SELECT * FROM " + TbName + " WHERE ID=" + id + " UNION ALL SELECT A.* FROM " + TbName + " A, f WHERE a.ID=f.PID) SELECT * FROM " + TbName + " WHERE ID IN(SELECT ID FROM f)";
            DataTable dt = DBCenter.ExecuteTable(sql);
            if (dt.Rows.Count < 1) { return 0; }
            foreach (DataRow dr in dt.Rows)
            {
                nodeTree += dr[PK] + ",";
            }
            nodeTree = nodeTree.Trim(',');
            return DataConvert.CLng(dt.Rows[0][PK]);
        }
        private void AddToDT(DataTable dt, DataTable result, int pid)
        {
            DataRow[] drs = dt.Select("Pid=" + pid);
            for (int i = 0; i < drs.Length; i++)
            {
                result.ImportRow(drs[i]);
                AddToDT(dt, result, Convert.ToInt32(drs[i][PK]));
            }
        }
        public M_Plat_Group NewGroup(string gname, int compid, int uid)
        {
            M_Plat_Group gpMod = new M_Plat_Group();
            gpMod.CompID = compid;
            gpMod.GroupName = gname;
            gpMod.CreateUser = uid;
            gpMod.ManageIDS = "," + uid + ",";
            gpMod.MemberIDS = "," + uid + ",";
            return gpMod;
        }
        //--------------------------------------------------------------Order
        /// <summary>
        /// 获取父类下最大MaxID
        /// </summary>
        private int Order_GetMax(int pid)
        {
            string where = "Pid=" + pid;
            return (DataConvert.CLng(DBCenter.ExecuteScala(TbName, "MAX(OrderID)", where)) + 1);
        }
        public void UpdateOrder(int id, int order) 
        {
            DBCenter.UpdateSQL(TbName, "OrderID=" + order, "ID=" + id);
        }
        //-------------------Tools
        private string IdsFormat(string ids)
        {
            return "," + (ids.Replace(" ", "").Replace(",,", ",").Trim(',')) + ",";
        }
        private string CombineIDS(string sids, string tids)
        {
            string[] arr = tids.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            sids = IdsFormat(sids);
            foreach (string id in arr)
            {
                if (!string.IsNullOrEmpty(id) && !sids.Contains("," + id + ","))
                    sids += id + ",";
            }
            return IdsFormat(sids);
        }
    }
}
