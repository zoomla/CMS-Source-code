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
using ZoomLa.BLL;
using ZoomLa.Components;
using ZoomLa.Common;
using System.Globalization;
namespace ZoomLa.WebSite.Manage.Content
{
    public partial class NodeTree : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int t = 0;
            if (!this.Page.IsPostBack)
            {
                if (!string.IsNullOrEmpty(base.Request.QueryString["t"]))
                    t = DataConverter.CLng(base.Request.QueryString["t"]);
                else
                    t = 0;
                B_Node bll = new B_Node();
                TreeNode tmpNd;
                tmpNd = new TreeNode();
                tmpNd.Value = "0";
                tmpNd.Text = SiteConfig.SiteInfo.SiteName;
                tmpNd.NavigateUrl = "~/" + this.GetManagePath() + "/Content/ContentManage.aspx?NodeID=0";
                tmpNd.Target = "main_right";
                tmpNd.ImageUrl = "";
                tmpNd.ToolTip = "根节点";
                tvNav.Nodes.Add(tmpNd);
                bll.InitTreeNode(tmpNd.ChildNodes, 0, t);
                tvNav.ExpandAll();
            }
        }

        private string GetManagePath()
        {
            return SiteConfig.SiteOption.ManageDir.ToLower(CultureInfo.CurrentCulture);
        }
    }
}