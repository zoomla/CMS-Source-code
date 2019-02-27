namespace ZoomLa.WebSite.User.Page
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
    using ZoomLa.Model;
    using ZoomLa.Components;
using ZoomLa.BLL.Page;
    using ZoomLa.Model.Page;
    /*
     * 依据用户所选定的样式,显示不同的栏目节点
     */ 
    public partial class NodeTree : System.Web.UI.Page
    {
        B_User buser = new B_User();
        B_Templata tlpBll = new B_Templata();
        B_PageReg regBll = new B_PageReg();
        protected void Page_Load(object sender, EventArgs e)
        {
            B_User.CheckIsLogged();
            string hasChild = "<a href='MyContent.aspx?ModelID={0}' id='a{0}' class='list1' target='main_right1'>{2}<span class='list_span'>{1}</span></a>";
            string noChild = "<a href='MyContent.aspx?ModelID={0}' target='main_right1'>{2}{1}</a>";
            if (!IsPostBack)
            {
                M_UserInfo mu = buser.GetLogin();
                M_PageReg regMod = regBll.SelModelByUid(mu.UserID);
                DataTable nodeDT = tlpBll.SelByStyleAndPid(regMod.NodeStyle);
                nodeHtml.Text = "<ul class='tvNav'><li><a  class='list1' id='a0' href='javascript:;' ><span class='list_span'>全部节点</span><span class='fa fa-list'></span></a>" + B_Node.GetLI(nodeDT,hasChild, noChild) + "</li></ul>";
            }
        }
    }
}