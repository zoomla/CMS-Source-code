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
       B_Content conBll = new B_Content();
        B_Admin badmin = new B_Admin();
        B_Node nodeBll = new B_Node();
        public string Mids { get { return Request.QueryString["id"]; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                B_ARoleAuth.CheckEx(ZLEnum.Auth.content, "ContentMange");
                if (string.IsNullOrEmpty(Mids)) { function.WriteErrMsg("没有指定要移动的内容ID", "../Content/ContentManage.aspx"); }
                else { this.TxtContentID.Text = Mids; }
                DataTable dt = nodeBll.GetNodeListContainXML(0);
                Nodes_Li.Text = nodeBll.CreateDP(dt);
                //this.DDLNode.DataSource = this.bnode.GetNodeListContain(0);
                //this.DDLNode.DataTextField = "NodeName";
                //this.DDLNode.DataValueField = "NodeID";
                //this.DDLNode.DataBind();
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='ContentManage.aspx'>内容管理</a></li><li class='active'>内容批量移动</li>");
            }
        }
        protected void BatMove_Btn_Click(object sender, EventArgs e)
        {
            int NodeID = DataConverter.CLng(Request.Form["targetNode"]);
            string Ids = this.TxtContentID.Text;
            if (conBll.MoveContent(Ids, NodeID))
            {
                function.WriteSuccessMsg("内容移动成功", "ContentManage.aspx");
            }
        }
    }
}