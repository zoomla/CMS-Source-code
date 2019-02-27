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
using ZoomLa.Model;
using ZoomLa.Common;
using ZoomLa.Web;
namespace ZoomLa.WebSite.Manage.Content
{
    public partial class EditOutLink : AdminPage
    {
        private B_Node bll = new B_Node();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                B_Admin badmin = new B_Admin();
                badmin.CheckMulitLogin();
                if (!badmin.ChkPermissions("NodeEdit"))
                {
                    function.WriteErrMsg("没有权限进行此项操作");
                }
                string mNodeID = base.Request.QueryString["NodeID"];
                if (string.IsNullOrEmpty(mNodeID))
                {
                    AdminPage.WriteErrMsg("没有指定要编辑的节点ID");
                }
                M_Node node = this.bll.GetNode(DataConverter.CLng(mNodeID));
                if (node.IsNull)
                {
                    AdminPage.WriteErrMsg("指定要编辑的节点不存在");
                }
                else
                {
                    if (node.ParentID == 0)
                        this.LblNodeName.Text = "根节点";
                    else
                    {
                        M_Node node1 = this.bll.GetNode(node.ParentID);
                        this.LblNodeName.Text = node1.NodeName;
                    }
                    this.HdnNodeID.Value = mNodeID;
                    this.HdnDepth.Value = node.Depth.ToString();
                    this.HdnParentId.Value = node.ParentID.ToString();
                    this.HdnOrderID.Value = node.OrderID.ToString();
                    this.TxtNodeName.Text = node.NodeName;
                    this.TxtNodeDir.Text = node.NodeDir;
                    this.TxtNodeUrl.Text = node.NodeUrl;
                    this.TxtNodePicUrl.Text = node.NodePic;
                    this.TxtTips.Text = node.Tips;
                    this.RBLOpenType.SelectedValue = DataConverter.CLng(node.OpenNew).ToString();
                }
            }
        }
        protected void EBtnSubmit_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                M_Node node = new M_Node();
                node.NodeID = DataConverter.CLng(this.HdnNodeID.Value);
                node.NodeName = this.TxtNodeName.Text;
                node.NodeType = 3;
                node.NodePic = this.TxtNodePicUrl.Text;
                node.NodeDir = this.TxtNodeDir.Text;
                node.NodeUrl = this.TxtNodeUrl.Text;
                node.ParentID = DataConverter.CLng(this.HdnParentId.Value);
                node.Child = 0;
                node.Depth = DataConverter.CLng(this.HdnDepth.Value);
                node.OrderID = DataConverter.CLng(this.HdnOrderID.Value);
                node.Tips = this.TxtTips.Text;
                node.Description = "";
                node.Meta_Keywords = "";
                node.Meta_Description = "";
                node.OpenNew = DataConverter.CBool(this.RBLOpenType.SelectedValue);
                node.ItemOpenType = true;
                node.PurviewType = false;
                node.CommentType = false;
                node.HitsOfHot = 0;
                node.ListTemplateFile = "";
                node.ContentModel = "";
                node.ListPageHtmlEx = 0;
                node.ContentFileEx = 0;
                node.ContentPageHtmlRule = 0;
                this.bll.UpdateNode(node);
                Response.Redirect("NodeManage.aspx");
            }
        }
    }
}