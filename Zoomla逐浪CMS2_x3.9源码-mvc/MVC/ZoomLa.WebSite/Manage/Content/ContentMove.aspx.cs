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
using ZoomLa.Model;


namespace ZoomLaCMS.Manage.I.ASCX
{
    public partial class ContentMove : CustomerPageAction
    {
        B_Content bll = new B_Content();
        B_Node bnode = new B_Node();

        protected void Page_Load(object sender, EventArgs e)
        {
            B_Admin badmin = new B_Admin();
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='ContentManage.aspx'>内容管理</a></li><li class='active'>内容批量移动</li>");
            if (!IsPostBack)
            {
                B_ARoleAuth.CheckEx(ZLEnum.Auth.content, "ContentMange");
                string id = base.Request.QueryString["id"];
                if (string.IsNullOrEmpty(id))
                    function.WriteErrMsg("没有指定要移动的内容ID", "../Content/ContentManage.aspx");
                else
                    this.TxtContentID.Text = id.Trim();
                DataTable dt = bnode.GetNodeListContainXML(0);
                Nodes_Li.Text = bnode.CreateDP(dt);
                //this.DDLNode.DataSource = this.bnode.GetNodeListContain(0);
                //this.DDLNode.DataTextField = "NodeName";
                //this.DDLNode.DataValueField = "NodeID";
                //this.DDLNode.DataBind();
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                int NodeID = DataConverter.CLng(Request.Form["targetNode"]);
                string Ids = this.TxtContentID.Text;
                bool flag = this.bll.MoveContent(Ids, NodeID);
                if (flag)
                    function.WriteSuccessMsg("内容移动成功", "ContentManage.aspx");
            }
        }
    }
}