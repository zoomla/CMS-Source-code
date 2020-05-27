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

namespace ZoomLaCMS.Manage.Content
{
    public partial class AddOutLink : CustomerPageAction
    {
        private B_Node bll = new B_Node();
        protected string result;
        public int NodeID { get { return DataConverter.CLng(Request.QueryString["id"]); } }
        public int Pid { get { return DataConverter.CLng(Request.QueryString["ParentID"]); } }

        protected void Page_Load(object sender, EventArgs e)
        {
            B_Admin badmin = new B_Admin();
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='../Config/SiteOption.aspx'>系统设置</a></li><li><a href='NodeManage.aspx'>节点管理</a></li><li class=\"active\">添加外部链接</li>");
            if (!IsPostBack)
            {
                B_ARoleAuth.CheckEx(ZLEnum.Auth.model, "NodeEdit");
                if (Pid == 0) { this.LblNodeName.Text = "根节点"; }
                else
                {
                    M_Node node = this.bll.GetNodeXML(Pid);
                    if (node.IsNull)
                        this.LblNodeName.Text = "根节点";
                    else
                        this.LblNodeName.Text = node.NodeName;
                }
                if (NodeID > 0)
                {
                    M_Node nodemod = bll.GetNodeXML(NodeID);
                    this.HdnNodeID.Value = NodeID.ToString();
                    this.HdnDepth.Value = nodemod.Depth.ToString();
                    this.HdnParentId.Value = nodemod.ParentID.ToString();
                    this.HdnOrderID.Value = nodemod.OrderID.ToString();
                    this.TxtNodeName.Text = nodemod.NodeName;
                    this.TxtNodeDir.Text = nodemod.NodeDir;
                    this.TxtNodeUrl.Text = nodemod.NodeUrl;
                    this.TxtNodePicUrl.Text = nodemod.NodePic;
                    this.TxtTips.Text = nodemod.Tips;
                }
                else
                {
                    this.HdnParentId.Value = Pid.ToString();
                    this.HdnDepth.Value = bll.GetDepth(Pid).ToString();
                    this.HdnOrderID.Value = (bll.GetMaxOrder(Pid) + 1).ToString();
                }
            }
        }
        protected void EBtnSubmit_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                M_Node node = new M_Node();
                node.NodeID = 0;
                if (NodeID > 0) { node = bll.SelReturnModel(NodeID); }
                node.NodeName = this.TxtNodeName.Text;
                node.NodeType = 3;
                node.NodePic = this.TxtNodePicUrl.Text;
                node.NodeDir = this.TxtNodeDir.Text;
                node.NodeUrl = this.TxtNodeUrl.Text;
                node.ParentID = DataConverter.CLng(this.HdnParentId.Value);
                node.Depth = DataConverter.CLng(this.HdnDepth.Value);
                node.OrderID = DataConverter.CLng(this.HdnOrderID.Value);
                node.Tips = this.TxtTips.Text;
                node.OpenNew = DataConverter.CBool(this.RBLOpenType.SelectedValue);
                if (NodeID > 0) { bll.UpdateNode(node); function.WriteSuccessMsg("修改成功!", "NodeManage.aspx"); }
                else { this.bll.AddNode(node); function.WriteSuccessMsg("添加成功!", "NodeManage.aspx"); }

                Response.Redirect("NodeManage.aspx");
            }
        }
    }
}