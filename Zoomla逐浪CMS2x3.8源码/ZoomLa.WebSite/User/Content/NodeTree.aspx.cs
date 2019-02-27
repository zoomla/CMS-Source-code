namespace ZoomLa.WebSite.User.Content
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
    using ZoomLa.Common;
    using ZoomLa.BLL;
    using ZoomLa.Components;
    using System.Globalization;
    using System.Collections.Generic;
    using ZoomLa.Model;
    public partial class NodeTree : System.Web.UI.Page
    {
        //-----------
        B_User bll = new B_User();
        B_UserPromotions pbll = new B_UserPromotions();
        B_Node nodeBll = new B_Node();
        //DataTable nodeDts;
        protected void Page_Load(object sender, EventArgs e)
        {
            M_UserInfo uinfo = bll.SeachByID(DataConverter.CLng(bll.GetLogin().UserID));
            DataTable dts = pbll.GetSelectbyGroupID(uinfo.GroupID);//这里就获取到了用户的组列权限。
            //DataTable nodeDts= nodeBll.GetNode(dts.Rows[51]["NodeID"].ToString());
            NodeInit(dts);
        }
        private void NodeInit(DataTable dts)
        {

            B_Node bll = new B_Node();
            TreeNode tmpNd;
            tmpNd = new TreeNode();
            tmpNd.Value = "0";
            tmpNd.Text = SiteConfig.SiteInfo.SiteName;
            tmpNd.NavigateUrl = "javascript:";
            tmpNd.Target = "main_right";
            tmpNd.ImageUrl = "";
            tmpNd.ToolTip = "根节点";
            tvNav.Nodes.Add(tmpNd);//将一个TreeNode添加入节点树


            foreach (DataRow row in dts.Rows)
            {
                if (row["look"].ToString().Equals("0"))//如果查看权限都没有则不显示
                    continue;
                M_Node nodeMod = nodeBll.SelReturnModel(Convert.ToInt32(row["NodeID"]));
                if (nodeMod.ParentID==0)
                {

                    TreeNode childtmpNd = new TreeNode();
                    childtmpNd.SelectAction = TreeNodeSelectAction.Expand;
                    childtmpNd.Text = nodeMod.NodeName;
                    childtmpNd.NavigateUrl = "javascript:";
                    childtmpNd.Target = "main_right";
                    childtmpNd.ImageUrl = "";
                    tmpNd.ChildNodes.Add(childtmpNd);
                    bll.InitTreeNodeUser(childtmpNd.ChildNodes, Int32.Parse(row["NodeID"].ToString()), 1, dts);
                }
            }
            #region 互动管理
            TreeNode tmppubtrees = new TreeNode();
            B_Pub pub = new B_Pub();
            TreeNode tmppubtree = new TreeNode();
            string Pubid = "";
            DataTable pubtable = pub.SelByType(4);
            if (pubtable != null)
            {
                if (pubtable.Rows.Count > 0)
                {
                    tmppubtree.Value = "0";
                    tmppubtree.Text = "互动信息管理";
                    tmppubtree.NavigateUrl = "javascript:void(0);";
                    tmppubtree.Target = "";
                    tmppubtree.ImageUrl = "";
                    tmppubtree.ToolTip = "互动信息管理";
                    tmpNd.ChildNodes.Add(tmppubtree);
                }
                for (int p = 0; p < pubtable.Rows.Count; p++)
                {
                    tmppubtrees = new TreeNode();
                    tmppubtrees.Value = "0";
                    tmppubtrees.Text = pubtable.Rows[p]["PubName"].ToString();
                    Pubid = pubtable.Rows[p]["Pubid"].ToString();
                    tmppubtrees.NavigateUrl = "../pages/ViewPub.aspx?Pubid=" + Pubid;
                    tmppubtrees.Target = "main_right";
                    tmppubtrees.ImageUrl = "";
                    tmppubtrees.ToolTip = pubtable.Rows[p]["PubName"].ToString();
                    tmppubtree.ChildNodes.Add(tmppubtrees);
                }
            }
            #endregion
            tvNav.ExpandDepth = 1;
            tmpNd.SelectAction = TreeNodeSelectAction.Expand;
            if (tmpNd.ChildNodes.Count == 0)
            {
                //如果无任何节点权限，则不显示
                Response.Write("<script>parent.frames['I2'].location ='/User/Content/MyContent.aspx?NodeID=0000'</script>");
            }
            else
            {
                //如果不是，跳转到第一个拥有权限的子节点上
            }
        }



        private string GetManagePath()
        {
            return SiteConfig.SiteOption.ManageDir.ToLower(CultureInfo.CurrentCulture);
        }
        protected void tvNav_SelectedNodeChanged(object sender, EventArgs e)
        {
        }



    }
}