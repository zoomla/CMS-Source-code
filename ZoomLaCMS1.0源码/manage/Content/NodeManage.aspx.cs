namespace ZoomLa.WebSite.Manage.Content
{
    using System;
    using System.Data;
    using System.Configuration;
    using System.Collections;
    using System.Web;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Web.UI.WebControls.WebParts;
    using System.Web.UI.HtmlControls;
    using ZoomLa.Web;
    using ZoomLa.BLL;
    using ZoomLa.Common;
    using ZoomLa.Components;
    using System.Globalization;

    public partial class NodeManage : System.Web.UI.Page
    {
        private B_Node bll = new B_Node();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                B_Admin badmin = new B_Admin();
                badmin.CheckMulitLogin();
                if (!badmin.ChkPermissions("NodeManage"))
                {
                    function.WriteErrMsg("没有权限进行此项操作");
                }
                this.Repeater1.DataSource = this.bll.GetNodeList(0);
                this.Repeater1.DataBind();
            }
        }
        public string GetIcon(string NodeName, string NodeID, string Depth, string NodeType)
        {
            string outstr = "";
            int Dep = DataConverter.CLng(Depth);
            int nodetype = DataConverter.CLng(NodeType);
            if (Dep > 0)
            {
                for (int i = 1; i <= Dep - 1; i++)
                {
                    outstr = outstr + "<img src=\"/TreeLineImages/tree_line4.gif\" border=\"0\" width=\"19\" height=\"20\" />";
                }
                outstr = outstr + "<img src=\"/TreeLineImages/t.gif\" border=\"0\" />";
            }
            if (nodetype == 0 || nodetype == 1)
            {
                outstr = outstr + "<img src=\"/TreeLineImages/plus.gif\" border=\"0\" />";
                if(NodeID!="0")
                    outstr = outstr + "<span><a href=\"EditNode.aspx?NodeID=" + NodeID + "\">" + NodeName + "</a></span>";
                else
                    outstr = outstr + "<span>" + NodeName + "</span>";
            }
            if (nodetype == 2)
            {
                outstr = outstr + "<img src=\"/TreeLineImages/singlepage.gif\" border=\"0\" width=\"19\" height=\"20\" />";
                outstr = outstr + "<span><a href=\"EditSinglePage.aspx?NodeID=" + NodeID + "\">" + NodeName + "</a></span>";
            }
            if (nodetype == 3)
            {
                outstr = outstr + "<img src=\"/TreeLineImages/outlink.gif\" border=\"0\" width=\"19\" height=\"20\" />";
                outstr = outstr + "<span><a href=\"EditOutLink.aspx?NodeID=" + NodeID + "\">" + NodeName + "</a></span>";
            }
            
            return outstr;
        }
        public string GetNodeType(string NodeType)
        {
            int nodetype = DataConverter.CLng(NodeType);
            string outstr = "";
            if (nodetype == 0)
                outstr = "根节点";
            if (nodetype == 1)
                outstr = "栏目节点";
            if (nodetype == 2)
                outstr = "单页节点";
            if (nodetype == 3)
                outstr = "外部链接";
            return outstr;
        }
        public string GetOper(string NodeID, string NodeType)
        {
            string outstr = "";
            int nodetype = DataConverter.CLng(NodeType);
            if (nodetype == 0)
            {
                outstr = "<a href=\"AddNode.aspx?ParentID=" + NodeID + "\">添加节点</a> | <a href=\"AddSinglePage.aspx?ParentID=" + NodeID + "\">添加单页</a> | <a href=\"AddOutLink.aspx?ParentID=" + NodeID + "\">添加链接</a> | <a href=\"SetNodeOrder.aspx?ParentID=0\">节点排序</a>";
            }
            if (nodetype == 1)
            {
                outstr = "<a href=\"AddNode.aspx?ParentID=" + NodeID + "\">添加节点</a> | <a href=\"AddSinglePage.aspx?ParentID=" + NodeID + "\">添加单页</a> | <a href=\"AddOutLink.aspx?ParentID=" + NodeID + "\">添加链接</a> | <a href=\"EditNode.aspx?NodeID=" + NodeID + "\">修改</a> | <a href=\"DelNode.aspx?NodeID=" + NodeID + "\" OnClick=\"return confirm('确实要删除此节点吗？');\">删除</a>";
                if (this.bll.GetNode(DataConverter.CLng(NodeID)).Child > 0)
                {
                    outstr = outstr + " | <a href=\"SetNodeOrder.aspx?ParentID=" + NodeID + "\">节点排序</a>";
                }
            }
            if (nodetype == 2)
                outstr = "<a href=\"EditSinglePage.aspx?NodeID=" + NodeID + "\">修改</a> | <a href=\"DelNode.aspx?NodeID=" + NodeID + "\" OnClick=\"return confirm('确实要删除此节点吗？');\">删除</a>";
            if (nodetype == 3)
                outstr = "<a href=\"EditOutLink.aspx?NodeID=" + NodeID + "\">修改</a> | <a href=\"DelNode.aspx?NodeID=" + NodeID + "\" OnClick=\"return confirm('确实要删除此节点吗？');\">删除</a>";
            return outstr;
        }
        
        public string GetManagePath()
        {
            return SiteConfig.SiteOption.ManageDir.ToLower(CultureInfo.CurrentCulture);
        }
    }
}