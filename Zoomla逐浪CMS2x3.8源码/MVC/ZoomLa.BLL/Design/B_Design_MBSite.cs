using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ZoomLa.BLL.CreateJS;
using ZoomLa.BLL.Helper;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;
using ZoomLa.Model.Design;
using ZoomLa.SQLDAL;

namespace ZoomLa.BLL.Design
{
    public class B_Design_MBSite
    {
        public const int TlpNodeID = 788;
        public const int UserShopNodeID = 854;
        private string PK, TbName = "";
        private M_Design_MBSite initMod = new M_Design_MBSite();
        public B_Design_MBSite()
        {
            PK = initMod.PK;
            TbName = initMod.TbName;
        }
        public int Insert(M_Design_MBSite model)
        {
            return DBCenter.Insert(model);
        }
        public bool UpdateByID(M_Design_MBSite model)
        {
            return DBCenter.UpdateByID(model, model.ID);
        }
        public bool Del(int ID)
        {
            return DBCenter.Del(TbName, PK, ID);
        }
        public M_Design_MBSite SelReturnModel(int ID)
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
        public M_Design_MBSite SelModelByTlpID(int uid, int tlpid)
        {
            if (uid < 1 || tlpid < 1) { return null; }
            string where = "UserID=" + uid + " AND TlpID=" + tlpid;
            using (DbDataReader reader = DBCenter.SelReturnReader(TbName, where, "ID ASC"))
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
        public M_Design_MBSite SelModelByUid(int uid)
        {
            if (uid < 1) { return null; }
            using (DbDataReader reader = DBCenter.SelReturnReader(TbName, "UserID", uid))
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
        public M_Design_MBSite SelModelBySite(int siteid)
        {
            if (siteid < 1) { return null; }
            using (DbDataReader reader = DBCenter.SelReturnReader(TbName, "SiteID", siteid))
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
            return DBCenter.Sel(TbName, "", PK + " DESC");
        }
        public DataTable Sel(int uid)
        {
            string where = " 1=1 ";
            if (uid != 0) { where += " AND UserID=" + uid; }
            return DBCenter.Sel(TbName, where, "ID DESC");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="skey">微站名称或</param>
        /// <returns></returns>
        public DataTable Sel(string skey)
        {
            string where = "1=1";
            List<SqlParameter> sp = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(skey))
            {
                where += " AND (A.SiteName LIKE @skey OR B.UserName LIKE @skey)";
                sp.Add(new SqlParameter("skey", "%" + skey + "%"));
            }
            return SqlHelper.JoinQuery("A.*,B.UserName", TbName, "ZL_User", "A.UserID = B.UserID", where, "A.CDate DESC", sp.ToArray());
        }
        //----------Logical
        ////用户无微站信息,则自动创建
        //public M_Design_MBSite AutoCreate(M_UserInfo mu)
        //{
        //    if (mu == null || mu.IsNull) { return null; }
        //    if (DBCenter.IsExist(TbName, "UserID=" + mu.UserID)) { return null; }
        //    M_Design_MBSite mbMod = new M_Design_MBSite();
        //    mbMod.UserID = mu.UserID;
        //    mbMod.TlpID = 1;
        //    mbMod.SiteID = mu.SiteID;
        //    mbMod.ID = Insert(mbMod);
        //    return mbMod;
        //}
        /// <summary>
        /// 根据用户与模板信息,创建站点
        /// </summary>
        public M_Design_MBSite CreateSite(M_UserInfo mu, M_Design_MBSite mbMod, out string err)
        {
            B_Design_Node desNodeBll = new B_Design_Node();
            B_Node nodeBll = new B_Node();
            B_Product proBll = new B_Product();
            //int mbsitecount = SiteConfig.SiteOption.DN_MBSiteCount;
            int mbsitecount = 10000;//取消数量限制
            if (mu == null || mu.IsNull) { err = "用户不存在"; return null; }
            else if (mbMod.TlpID < 1 || mbMod.TlpID > 8) { err = "未指定模板或模板[" + mbMod.TlpID + "]不存在"; return null; }
            else if (GetSiteCount(mu.UserID) >= mbsitecount) { err = "用户只能创建" + mbsitecount + "个微站"; return null; }
            if (string.IsNullOrEmpty(mbMod.SiteName)) { mbMod.SiteName = B_User.GetUserName(mu.HoneyName, mu.UserName) + "的微站" + DBCenter.Count(TbName, "UserID=" + mu.UserID) + 1; }
            mbMod.UserID = mu.UserID;
            mbMod.ID = Insert(mbMod);
            //-----建立微站节点信息,从指定的模板处拷节点数据,父节点需要自建
            M_Node pnode = desNodeBll.GetUserRootNode(mu);
            if (pnode.IsNull)
            {
                pnode = desNodeBll.CreateUserRootNode(mu);
            }
            M_Node nodeMod = new M_Node();
            nodeMod.NodeName = "微建站";
            nodeMod.NodeDir = function.GetRandomString(6);
            nodeMod.CUser = mu.UserID;
            nodeMod.CUName = mu.UserName;
            nodeMod.NodeBySite = mbMod.ID;
            nodeMod.NodeType = 1;//标识自身为主栏目
            nodeMod.ParentID = pnode.NodeID;
            nodeMod.NodeID = nodeBll.Insert(nodeMod);
            //---导入对应的模板节点数据
            string nodename = "微建站" + mbMod.TlpID;
            DataTable nodeDT = SelNodeByPName(nodename);
            CopyNodeAndContent(nodeDT, nodeMod);
            //---如果有商品数据,则导入商品
            DataTable proDT = SelProByPName(nodename);
            foreach (DataRow dr in proDT.Rows)
            {
                M_Product proMod = new M_Product().GetModelFromReader(dr);
                proMod.ID = 0;
                proMod.ParentID = mbMod.ID;//所属哪个站点
                proMod.UserID = mu.UserID;
                proMod.AddUser = mu.UserName;
                proMod.Nodeid = UserShopNodeID;
                proMod.AddTime = DateTime.Now;
                proMod.UpdateTime = DateTime.Now;
                proBll.Insert(proMod);
            }
            err = "";
            return mbMod;
        }
        /// <summary>
        /// 移除站点,节点和相关的内容信息保留
        /// </summary>
        public bool DelSite(int id)
        {
            Del(id);
            return true;
        }
        /// <summary>
        /// 将指定节点文章拷到一个新节点下(将旧的也转换为此)
        /// </summary>
        /// <param name="nodeDT">需要拷贝的节点信息</param>
        /// <param name="pnode">父节点信息</param>
        /// <param name="mu">用户信息</param>
        private void CopyNodeAndContent(DataTable nodeDT, M_Node pnode)
        {
            if (pnode.CUser < 1) { throw new Exception("未指定用户ID"); }
            if (string.IsNullOrEmpty(pnode.CUName)) { throw new Exception("未指定用户名称"); }
            B_Node nodeBll = new B_Node();
            B_CodeModel conBll = new B_CodeModel("ZL_CommonModel");
            B_CodeModel artBll = new B_CodeModel("ZL_C_Article");
            foreach (DataRow nodedr in nodeDT.Rows)
            {
                M_Node cnode = new M_Node().GetModelFromReader(nodedr);//当前循环的写入数据库的节点
                cnode.ParentID = pnode.NodeID;
                cnode.CUser = pnode.CUser;
                cnode.CUName = pnode.CUName;
                cnode.NodeBySite = pnode.NodeBySite;
                cnode.CDate = DateTime.Now;
                cnode.NodeID = nodeBll.Insert(cnode);
                //拷贝相应的文章进入该节点
                DataTable conDT = DBCenter.Sel("ZL_CommonModel", "NodeID=" + nodedr["NodeID"]);
                DataTable artDT = DBCenter.Sel("ZL_C_Article", "ID IN (SELECT ItemID FROM ZL_CommonModel WHERE NodeID=" + nodedr["NodeID"] + ")");
                if (artDT.Columns.Contains("ppics")) { artDT.Columns.Remove("ppics"); }
                if (artDT.Columns.Contains("tpic")) { artDT.Columns.Remove("tpic"); }
                for (int i = 0; i < conDT.Rows.Count; i++)
                {
                    DataRow condr = conDT.Rows[i];
                    condr["CreateTime"] = DateTime.Now;
                    condr["UPDateTime"] = DateTime.Now;
                    condr["Inputer"] = pnode.CUName;
                    condr["NodeID"] = cnode.NodeID;
                    int itemID = Convert.ToInt32(condr["ItemID"]);
                    if (artDT.Select("ID='" + itemID + "'").Length > 0)
                    {
                        //其中可能包含没有的字段,需要一个方法,将其导入(根据站站迁移扩展)
                        DataRow artdr = artDT.Select("ID='" + itemID + "'")[0];
                        itemID = artBll.Insert(artdr);
                    }
                    else { itemID = 0; }
                    condr["ItemID"] = itemID;
                    conBll.Insert(condr, "GeneralID");
                }
            }
        }
        //获取指定节点名称下的下一级子节点
        private DataTable SelNodeByPName(string name)
        {
            List<SqlParameter> sp = new List<SqlParameter>() { new SqlParameter("name", name) };
            return DBCenter.Sel("ZL_Node", "ParentID=(SELECT NodeID FROM ZL_Node WHERE ParentID=788 AND NodeName=@name)", "", sp);
        }
        //指定节点下的商品信息
        private DataTable SelProByPName(string name)
        {
            //SELECT A.* FROM ZL_Commodities A LEFT JOIN ZL_Node B ON A.Nodeid=B.NodeID WHERE B.NodeName='微建站3'
            List<SqlParameter> sp = new List<SqlParameter>() { new SqlParameter("name", name) };
            return DBCenter.JoinQuery("A.*", "ZL_Commodities", "ZL_Node", "A.NodeID=B.NodeID", "B.NodeName=@name", "", sp.ToArray());
        }
        public int GetSiteCount(int uid)
        {
            return Convert.ToInt32(DBCenter.Count(TbName, "UserID=" + uid));
        }
        public DataTable GetTlpDT()
        {
            string[] tlps = "经典微站,简洁风格,微店样式,相册模式,文章资讯,人物展示,图标宫格,图文时尚".Split(',');
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("TlpName", typeof(string));
            dt.Columns.Add("PreviewImg", typeof(string));
            for (int i = 0; i < tlps.Length; i++)
            {
                DataRow dr = dt.NewRow();
                dr["ID"] = i + 1;
                dr["TlpName"] = tlps[i];
                dr["PreViewImg"] = "/design/mobile/tlp/" + dr["ID"] + "/view.jpg";
                dt.Rows.Add(dr);
            }
            return dt;
        }
    }
}
