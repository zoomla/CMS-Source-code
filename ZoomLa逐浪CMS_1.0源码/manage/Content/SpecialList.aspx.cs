namespace ZoomLa.Web.Site.Content
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
    using ZoomLa.BLL;
    using ZoomLa.Common;

    public partial class SpecialList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            B_Admin badmin = new B_Admin();
            badmin.CheckMulitLogin();
            if (!badmin.ChkPermissions("ContentEdit"))
            {
                function.WriteErrMsg("没有权限进行此项操作");
            }
            TreeNode tmpNd;
            tmpNd = new TreeNode();
            tmpNd.Value = "0";
            tmpNd.Text = "网站专题";
            tmpNd.NavigateUrl = "";
            tmpNd.Target = "";
            tvNav.Nodes.Add(tmpNd);
            B_SpecCate bll = new B_SpecCate();
            bll.InitTreeNode(tmpNd.ChildNodes);
            tvNav.ExpandAll();
        }
    }
}