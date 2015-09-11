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
namespace ZoomLa.WebSite.Manage.Content
{
    public partial class DelNode : System.Web.UI.Page
    {
        private B_Node bll = new B_Node();
        protected void Page_Load(object sender, EventArgs e)
        {
            B_Admin badmin = new B_Admin();
            badmin.CheckMulitLogin();
            if (!badmin.ChkPermissions("NodeManage"))
            {
                function.WriteErrMsg("没有权限进行此项操作");
            }
            int NodeID = DataConverter.CLng(base.Request.QueryString["NodeID"]);
            this.bll.DelNode(NodeID);
            Response.Redirect("NodeManage.aspx");
        }
    }
}