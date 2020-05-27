using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Design;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Model.Design;
using ZoomLa.SQLDAL;
using Newtonsoft.Json;

namespace ZoomLaCMS.Design.User.Node
{
    public partial class NodeList : System.Web.UI.Page
    {
        B_Design_SiteInfo sfBll = new B_Design_SiteInfo();
        B_Design_Node dnBll = new B_Design_Node();
        B_User buser = new B_User();
        B_Node nodeBll = new B_Node();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyBind();
            }
        }
        public void MyBind()
        {
            M_UserInfo mu = buser.GetLogin();
            M_Design_SiteInfo sfMod = sfBll.SelReturnModel(mu.SiteID);
            DataTable nodetable = nodeBll.SelForShowAll(0, true);
            nodetable.DefaultView.RowFilter = "NodeBySite='" + mu.SiteID + "'";
            nodetable = nodetable.DefaultView.ToTable();
            DataRow dr = nodetable.NewRow();//根节点
            dr["NodeID"] = 0;
            dr["NodeType"] = 0;
            dr["NodeName"] = sfMod.SiteName;
            dr["NodeDir"] = "root";
            dr["CDate"] = sfMod.CDate;
            dr["Depth"] = 0;
            dr["ItemCount"] = nodetable.Compute("SUM(ItemCount)", "ParentID=0");
            dr["ChildCount"] = nodetable.Rows.Count;
            dr["SafeGuard"] = 0;
            nodetable.Rows.InsertAt(dr, 0);
            RPT.DataSource = nodetable;
            RPT.DataBind();
        }
        public string ShowIcon()
        {
            return GetIcon(Eval("NodeName", ""), Convert.ToInt32(Eval("NodeID")), Convert.ToInt32(Eval("Depth")),
                Convert.ToInt32(Eval("NodeType")), Eval("NodeDir").ToString(), Convert.ToInt32(Eval("ChildCount")), Eval("contentModel").ToString());
        }
        public string GetIcon(string NodeName, int NodeID, int Depth, int nodetype, string nodeDir, int childCount, string contentModel)
        {
            string outstr = "";
            nodeDir = "";
            if (Depth > 0)//深度处理
            {
                for (int i = 1; i <= Depth - 1; i++)
                {
                    outstr = outstr + "<a href='ContentManage.aspx?NodeID=" + NodeID + "'><img src='/Images/TreeLineImages/tree_line4.gif' border='0' width='19' height='20' title='" + Resources.L.浏览内容管理 + "' /></a>";
                }
                outstr = outstr + "<a href='ContentManage.aspx?NodeID=" + NodeID + "'><img src='/Images/TreeLineImages/t.gif' border='0' title='" + Resources.L.浏览内容管理 + "' /></a>";
            }
            switch (nodetype)
            {
                case 0:
                case 1://普通栏目节点与根节点
                    if (childCount > 0)//如果有子节点
                    {
                        outstr = outstr + "<a href='ContentManage.aspx?NodeID=" + NodeID + "'><span data-type='icon' class='fa fa-folder' title='" + Resources.L.浏览内容管理 + "'></span></a>";
                    }
                    else
                    {
                        outstr = outstr + "<a href='ContentManage.aspx?NodeID=" + NodeID + "'><span class='" + GetIconPath(contentModel) + "' title='" + Resources.L.浏览内容管理 + "'></span></a>";
                    }
                    if (NodeID == 0)
                    {
                        outstr = outstr + "<span>" + NodeName + nodeDir + "</span>";
                    }
                    else
                    {
                        outstr = outstr + "<span><a href='EditNode.aspx?NodeID=" + NodeID + "'>" + NodeName + nodeDir + "</a></span>";
                    }
                    break;
                case 2://单页
                    outstr = outstr + "<a href='ContentManage.aspx?NodeID=" + NodeID + "'><span class='" + GetIconPath(contentModel) + "' title='" + Resources.L.浏览内容管理 + "'></span></a>";
                    outstr = outstr + "<span><a href='EditSinglePage.aspx?NodeID=" + NodeID + "'>" + NodeName + nodeDir + "</a></span>";
                    break;
                case 3://外部链接
                    outstr = outstr + "<a href='ContentManage.aspx?NodeID=" + NodeID + "'><i class='fa fa-chain' title='" + Resources.L.浏览内容管理 + "'></i></a>";
                    outstr = outstr + "<span><a href='AddOutLink.aspx?id=" + NodeID + "'>" + NodeName + nodeDir + "</a></span>";
                    break;
            }
            return outstr;
        }
        private string GetIconPath(string ContentModel)
        {
            B_Model bmod = new B_Model();
            string ItemIcon = "fa fa-globe";
            if (!string.IsNullOrEmpty(ContentModel))
            {
                try
                {
                    int ModelID = Convert.ToInt32(ContentModel.Split(',')[0]);
                    ItemIcon = bmod.GetWhere("ItemIcon", " And ModelID=" + ModelID, "").Rows[0][0].ToString(); //ZL_Model
                }
                catch { }
            }
            return ItemIcon;
        }
        public string GetOP()
        {
            string add = "<a href='AddNode.aspx?Pid=" + Eval("NodeID") + "'  class=\"option_style\" title='添加栏目'><i class='fa fa-plus'></i></a>";
            string edit = "<a href=\"AddNode.aspx\" class=\"option_style\" title='编辑栏目'><i class=\"fa fa-pencil\"></i></a>";
            string del = "<a href=\"javascript:;\" class=\"option_style\" title='删除栏目' onclick=\"node.del('" + Eval("NodeID") + "');\"><i class=\"fa fa-trash\"></i></a>";
            if (Eval("NodeID", "").Equals("0")) { return add; }
            else { return add + edit + del; }
        }
    }
}