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
using ZoomLa.Web;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Common;
namespace ZoomLa.WebSite.Manage.Content
{
    public partial class AddSinglePage : System.Web.UI.Page
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
                string mParentID = base.Request.QueryString["ParentID"];
                int ParentID;
                if (string.IsNullOrEmpty(mParentID))
                    ParentID = 0;
                else
                    ParentID = DataConverter.CLng(mParentID);
                if (ParentID == 0)
                    this.LblNodeName.Text = "根节点";
                else
                {
                    M_Node node = this.bll.GetNode(ParentID);
                    if (node.IsNull)
                        this.LblNodeName.Text = "根节点";
                    else
                        this.LblNodeName.Text = node.NodeName;
                }
                this.HdnParentId.Value = ParentID.ToString();
                this.HdnDepth.Value = this.bll.GetDepth(ParentID).ToString();
                this.HdnOrderID.Value = this.bll.GetOrder(ParentID).ToString();
            }
        }
        protected void EBtnSubmit_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                M_Node node = new M_Node();
                node.NodeID = 0;
                node.NodeName = this.TxtNodeName.Text;
                node.NodeType = 2;
                node.NodePic = this.TxtNodePicUrl.Text;
                node.NodeDir = this.TxtNodeDir.Text;
                node.NodeUrl = "";
                node.ParentID = DataConverter.CLng(this.HdnParentId.Value);
                node.Child = 0;
                node.Depth = DataConverter.CLng(this.HdnDepth.Value);
                node.OrderID = DataConverter.CLng(this.HdnOrderID.Value);
                node.Tips = this.TxtTips.Text;
                node.Description = this.TxtDescription.Text;
                node.Meta_Keywords = this.TxtMetaKeywords.Text;
                node.Meta_Description = this.TxtMetaDescription.Text;
                node.OpenNew = DataConverter.CBool(this.RBLOpenType.SelectedValue);
                node.ItemOpenType = false;
                node.PurviewType = true;
                node.CommentType = true;
                node.HitsOfHot = 0;
                node.ListTemplateFile = this.TxtTemplate.Text;
                node.IndexTemplate = "";
                node.ContentModel = "";
                node.ListPageHtmlEx = DataConverter.CLng(this.RBLListEx.SelectedValue);
                node.ContentFileEx = 0;
                node.ContentPageHtmlRule = 0;
                this.bll.AddNode(node);
                if (node.ParentID > 0)
                    this.bll.SetChildAdd(node.ParentID);
                Response.Redirect("NodeManage.aspx");
            }
        }
    }
}