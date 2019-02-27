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

namespace ZoomLaCMS.Manage.Content
{
    public partial class DelNode : CustomerPageAction
    {
        private B_Node bll = new B_Node();
        public string ViewMode { get { return Request.QueryString["view"] ?? ""; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            B_ARoleAuth.CheckEx(ZLEnum.Auth.model, "NodeManage");
            int NodeID = DataConverter.CLng(base.Request.QueryString["NodeID"]);
            this.bll.DelNode(NodeID);
            if (ViewMode.Equals("design")) { function.WriteSuccessMsg("删除成功", customPath2 + "Content/DesignNodeManage.aspx"); }
            else if (ViewMode.Equals("child"))
            {
                function.WriteSuccessMsg("删除成功", customPath2 + "Content/DesignNodeList.aspx?pid=" + Request.QueryString["pid"]);
            }
            else { function.WriteSuccessMsg("删除成功", customPath2 + "Content/NodeManage.aspx"); }
        }
    }
}