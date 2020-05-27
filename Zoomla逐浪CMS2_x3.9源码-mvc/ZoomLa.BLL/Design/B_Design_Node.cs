using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.SQLDAL;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLa.BLL.Design
{
    public class B_Design_Node
    {
        private string TbName = "ZL_Node";
        /// <summary>
        /// 不包含根节点的列表
        /// </summary>
        public DataTable SelBy(int siteID, int pid = 0)
        {
            string where = "NodeBySite=" + siteID + " AND NodeType>0 ";
            if (pid > 0) { where += " AND ParentID=" + pid; }
            DataTable dt = DBCenter.Sel(TbName, where);
            dt.TableName = TbName;
            return dt;
        }
        /// <summary>
        /// 当前站点下的节点
        /// </summary>
        /// <param name="siteID"></param>
        /// <param name="type">all:全部,child:子节点</param>
        /// <returns></returns>
        public string SelToIDS(int siteID, string type = "")
        {
            string nids = "";
            if (siteID < 1) { return nids; }

            string where = "NodeBySite=" + siteID;
            if (!type.Equals("all")) { where += " AND NodeType>0"; }
            DataTable dt = DBCenter.SelWithField(TbName, "NodeID", where);
            foreach (DataRow dr in dt.Rows)
            {
                nids += dr["NodeID"] + ",";
            }
            return nids.TrimEnd(',');
        }
        /// <summary>
        /// 创建用户的根节点,站点数据均存其下
        /// </summary>
        public M_Node CreateUserRootNode(M_UserInfo mu)
        {
            B_Node nodeBll = new B_Node();
            M_Node nodeMod = new M_Node();
            nodeMod.ContentModel = "2";//内容模型
            nodeMod.NodeName = mu.UserName + "的根节点";
            nodeMod.NodeDir = function.GetRandomString(6).ToLower();
            nodeMod.CUser = mu.UserID;
            nodeMod.CUName = mu.UserName;
            nodeMod.NodeBySite = mu.SiteID == 0 ? -1 : mu.SiteID;
            nodeMod.NodeType = 0;//标识自身为主栏目
            nodeMod.NodeID = nodeBll.Insert(nodeMod);
            return nodeMod;
        }
        /// <summary>
        /// 获取用户的根节点
        /// </summary>
        public M_Node GetUserRootNode(M_UserInfo mu)
        {
            B_Node nodeBll = new B_Node();
            return nodeBll.SelModelByWhere("CUser=" + mu.UserID + " AND NodeType=0", null);
        }
    }
}
