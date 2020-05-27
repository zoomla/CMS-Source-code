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


namespace ZoomLaCMS.Manage.Content
{
    public partial class SetNodeOrder : CustomerPageAction
    {
        private B_Node bll = new B_Node();
        protected void Page_Load(object sender, EventArgs e)
        {
            B_Admin badmin = new B_Admin();
            B_ARoleAuth.CheckEx(ZLEnum.Auth.model, "NodeManage");
            string pid = base.Request.QueryString["ParentID"];
            if (string.IsNullOrEmpty(pid))
            {
                pid = "0";
            }
            this.ViewState["ParentID"] = pid;
            if (!this.Page.IsPostBack)
            {
                MyBind();
            }
            if (!IsPostBack)
            {
                Call.SetBreadCrumb(Master, "<li>系统设置</li><li>节点管理</li>");
            }
        }
        public void MyBind()
        {
            int ParentID;
            ParentID = DataConverter.CLng(this.ViewState["ParentID"].ToString());
            if (ParentID == 0)
                this.Literal1.Text = "根节点";
            else
            {
                this.Literal1.Text = this.bll.GetNode(ParentID).NodeName;
            }

            DataTable tables = bll.SelByPid(ParentID);
            tables.DefaultView.Sort = "OrderID ,NodeID desc";
            this.RepSystemModel.DataSource = tables;
            this.RepSystemModel.DataBind();
        }
        public string GetNodeType(string NodeType)
        {
            return B_Node.GetNodeType(DataConverter.CLng(NodeType));
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
            MyBind();
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            string NodeIDValue = Request.Form["NodeIDValue"];
            int ParentID = DataConverter.CLng(Request.QueryString["ParentID"]);

            if (NodeIDValue != "")
            {
                if (NodeIDValue.IndexOf(",") > -1)
                {
                    string[] nodeidarr = NodeIDValue.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < nodeidarr.Length; i++)
                    {
                        int nodeid = DataConverter.CLng(nodeidarr[i]);
                        string nodeorderidvalue = Request.Form["OrderField" + nodeidarr[i]];
                        M_Node nodeinfo = bll.GetNode(nodeid);
                        nodeinfo.OrderID = DataConverter.CLng(nodeorderidvalue);
                        bll.UpdateNode(nodeinfo);
                    }
                }
                else
                {
                    int nodeid = DataConverter.CLng(NodeIDValue);
                    string nodeorderidvalue = Request.Form["OrderField" + nodeid.ToString()];

                    M_Node nodeinfo = bll.GetNode(nodeid);
                    nodeinfo.OrderID = DataConverter.CLng(nodeorderidvalue);
                    bll.UpdateNode(nodeinfo);
                }
            }
            function.WriteSuccessMsg("批量更新排序成功！");
            Server.Transfer("SetNodeOrder.aspx?ParentID=" + ParentID);
        }
    }
}