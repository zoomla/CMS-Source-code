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
    using ZoomLa.Model;

    public partial class SetNodeOrder : System.Web.UI.Page
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
            string pid = base.Request.QueryString["ParentID"];
            if (string.IsNullOrEmpty(pid))
            {
                pid = "0";
            }
            this.ViewState["ParentID"] = pid;
            if (!this.Page.IsPostBack)
            {
                DataBindList();
            }
        }
        public void DataBindList()
        {
            int ParentID;
            ParentID = DataConverter.CLng(this.ViewState["ParentID"].ToString());
            if (ParentID == 0)
                this.Literal1.Text = "根节点";
            else
            {
                this.Literal1.Text = this.bll.GetNode(ParentID).NodeName;
            }
            this.RepSystemModel.DataSource = bll.GetNodeChildList(ParentID);
            this.RepSystemModel.DataBind();
        }
        public string GetNodeType(string NodeType)
        {
            int nodetype = DataConverter.CLng(NodeType);
            string outstr = "";
            if (nodetype == 0)
                outstr = "根节点";
            if (nodetype == 1)
                outstr = "栏目节点";
            if (nodetype == 2)
                outstr = "单页节点";
            if (nodetype == 3)
                outstr = "外部链接";
            return outstr;
        }

        protected void Repeater1_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "UpMove")
            {
                int Id = DataConverter.CLng(e.CommandArgument);
                M_Node Nodeinfo = this.bll.GetNode(Id);
                if (Nodeinfo.OrderID != this.bll.GetMinOrder(Nodeinfo.ParentID))
                {
                    M_Node NodePre = this.bll.GetPreNode(Nodeinfo.ParentID, Nodeinfo.OrderID);
                    int CurrOrder = Nodeinfo.OrderID;
                    Nodeinfo.OrderID = NodePre.OrderID;
                    NodePre.OrderID = CurrOrder;
                    this.bll.UpdateNode(Nodeinfo);
                    this.bll.UpdateNode(NodePre);
                }
            }
            if (e.CommandName == "DownMove")
            {
                int Id = DataConverter.CLng(e.CommandArgument);
                M_Node Nodeinfo = this.bll.GetNode(Id);
                if (Nodeinfo.OrderID != this.bll.GetMaxOrder(Nodeinfo.ParentID))
                {
                    M_Node NodePre = this.bll.GetNextNode(Nodeinfo.ParentID, Nodeinfo.OrderID);
                    int CurrOrder = Nodeinfo.OrderID;
                    Nodeinfo.OrderID = NodePre.OrderID;
                    NodePre.OrderID = CurrOrder;
                    this.bll.UpdateNode(Nodeinfo);
                    this.bll.UpdateNode(NodePre);
                }
            }
            DataBindList();
        }
    }
}