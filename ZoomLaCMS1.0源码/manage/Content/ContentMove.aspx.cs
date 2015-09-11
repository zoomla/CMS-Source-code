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
    using ZoomLa.BLL;
    using ZoomLa.Common;

    public partial class ContentMove : System.Web.UI.Page
    {
        B_Content bll = new B_Content();
        B_Node bnode = new B_Node();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                B_Admin badmin = new B_Admin();
                badmin.CheckMulitLogin();
                if (!badmin.ChkPermissions("ContentMange"))
                {
                    function.WriteErrMsg("没有权限进行此项操作");
                }
                string id = base.Request.QueryString["id"];
                if (string.IsNullOrEmpty(id))
                    function.WriteErrMsg("没有指定要移动的内容ID", "../Content/ContentManage.aspx");
                else
                    this.TxtContentID.Text = id.Trim();
                this.DDLNode.DataSource = this.bnode.GetNodeListContain(0);
                this.DDLNode.DataTextField = "NodeName";
                this.DDLNode.DataValueField = "NodeID";
                this.DDLNode.DataBind();
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                int NodeID = DataConverter.CLng(this.DDLNode.SelectedValue);
                string Ids = this.TxtContentID.Text;
                bool flag = this.bll.MoveContent(Ids, NodeID);
                if (flag)
                    function.WriteSuccessMsg("内容移动成功", "../Content/ContentManage.aspx");
            }
        }
}
}