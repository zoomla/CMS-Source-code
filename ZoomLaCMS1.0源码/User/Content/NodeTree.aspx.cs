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

    public partial class NodeTree : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                int t = DataConverter.CLng(Request.QueryString["t"]);
                if (t == 0)
                    t = 1;
                B_Node bll = new B_Node();
                TreeNode tmpNd;
                tmpNd = new TreeNode();
                tmpNd.Value = "0";
                tmpNd.Text = SiteConfig.SiteInfo.SiteName;
                tmpNd.NavigateUrl = "MyContent.aspx";
                tmpNd.Target = "main_right";
                tmpNd.ImageUrl = "";                
                tmpNd.ToolTip = "根节点";
                tvNav.Nodes.Add(tmpNd);
                bll.InitTreeNodeUser(tmpNd.ChildNodes, 0,t);
                tvNav.ExpandAll();
            }
        }
    }
}