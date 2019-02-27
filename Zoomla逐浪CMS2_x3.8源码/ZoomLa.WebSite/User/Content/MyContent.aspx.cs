namespace ZoomLa.WebSite.User.Content
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using ZoomLa.BLL;
    using ZoomLa.Common;
    using ZoomLa.Model;

    public partial class MyContentpage : System.Web.UI.Page
    {
        B_Content bll = new B_Content();
        B_Node bNode = new B_Node();
        B_Model bmode = new B_Model();
        B_User buser = new B_User();
        B_UserPromotions ups = new B_UserPromotions();
        public int NodeID { get { return DataConverter.CLng(Request.QueryString["NodeID"]); } }
        public string Status { get { return (Request.QueryString["Status"] ?? ""); } }
        public string Type { get { return Request.QueryString["type"] ?? ""; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            BindNode();
            if (!IsPostBack)
            {
                M_UserInfo mu = buser.GetLogin();
                if (NodeID != 0)
                {
                    M_Node nod = bNode.GetNodeXML(NodeID);
                    if (nod.NodeListType == 2)
                    {
                        Response.Redirect("Myproduct.aspx?NodeID=" + NodeID);//跳转到商城
                    }
                    string ModeIDList = nod.ContentModel;
                    string[] ModelID = ModeIDList.Split(',');
                    string AddContentlink = "";
                    for (int i = 0; i < ModelID.Length; i++)
                    {
                        M_ModelInfo infoMod = bmode.SelReturnModel(DataConverter.CLng(ModelID[i]));
                        if (infoMod == null) continue;
                        if (infoMod.ModelType != 5)
                        {
                            AddContentlink += "<a href='AddContent.aspx?NodeID=" + NodeID + "&ModelID=" + infoMod.ModelID + "' class='btn btn-info' style='margin-right:5px;'><i class='fa fa-plus'></i> 添加" + infoMod.ItemName + "</a>";
                            //AddContentlink = AddContentlink + "<input id=\"addall" + i.ToString() + "\" name=\"btn" + i.ToString() + "\" class=\"btn btn-primary\"  type=\"button\" value=\"批量添加" + infoMod.ItemName + "\" onclick=\"javascript:window.location.href='Release.aspx?ModelID=" + ModelID[i] + "&NodeID=" + NodeID + "';\" />&nbsp;&nbsp;";
                        }
                    }
                    AddContent_L.Text = AddContentlink;
                }
                else
                {
                    AddContent_L.Text = ""; 
                }
                MyBind();
            }
        }
        private void MyBind()
        {
            string uname = buser.GetLogin().UserName;
            int type = DataConverter.CLng(DropDownList1.SelectedValue);
            string SearchTitle = TxtSearchTitle.Text.Trim();
            if (Status.Equals("-2")) { Rel_Btn.Visible = true; }//回收站
            EGV.DataSource = bll.ContentListUser(NodeID, Type, uname, SearchTitle, type, Status);
            EGV.DataKeyNames = new string[] { "GeneralID" };
            EGV.DataBind();
        }
        protected void BindNode()
        {
            DataTable dt = bNode.SelByPid(0, true);
            string nodeids = ups.GetNodeIDS(buser.GetLogin().GroupID);
            if (!string.IsNullOrEmpty(nodeids))
            {
                dt.DefaultView.RowFilter = "NodeID in(" + nodeids + ")";
            }
            else
            {
                dt.DefaultView.RowFilter = "1>2";//无权限，则去除所有
            }
            dt = dt.DefaultView.ToTable();
            //dt.DefaultView.Sort="OrderID,NodeID ASC";
            MyTree.liAllTlp = "<a href='MyContent.aspx'>全部内容</a>";
            MyTree.LiContentTlp = "<a href='MyContent.aspx?NodeID=@NodeID'>@NodeName</a>";
            MyTree.SelectedNode = NodeID;//选中节点
            MyTree.DataSource = dt;
            MyTree.DataBind();
        }
        protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EGV.PageIndex = e.NewPageIndex;
            MyBind();
        }
        protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName.ToLower())
            {
                case "edit":
                    Response.Redirect("AddContent.aspx?GeneralID=" + e.CommandArgument.ToString());
                    break;
                case "view":
                    Response.Redirect("/Item/" + e.CommandArgument.ToString() + ".aspx");
                    break;
                case "del":
                    bll.SetDel(DataConverter.CLng(e.CommandArgument));
                    break;
                case "reset":
                    bll.Reset(e.CommandArgument.ToString());
                    break;
            }
            MyBind();
        }
        protected override void Render(HtmlTextWriter writer)
        {
            foreach (GridViewRow row in EGV.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    row.Attributes["ondblclick"] = String.Format("javascript:location.href='ShowContent.aspx?Gid={0}'", EGV.DataKeys[row.RowIndex].Value.ToString());
                    row.Attributes["style"] = "cursor:pointer";
                    row.Attributes["title"] = "双击查看";
                }
            }
            base.Render(writer);
        }
        //-------------------------------------------------
        public string GetStatus(string status)
        {
            return ZLEnum.GetConStatus(DataConverter.CLng(status));
        }
        public bool GetIsDe(string status)
        {
            int s = DataConverter.CLng(status);
            if (s != 99)
            {
                if (s == -2)
                {
                    return false;
                }
                return true;
            }
            else
            {
                return false;
            } 
        }
        public bool GetIsRe(string status)
        {
            int s = DataConverter.CLng(status);
            return s == (int)ZLEnum.ConStatus.Recycle;
        }
        //用于修改与浏览按钮权限
        public bool GetIsNRe(string status, string action)
        {
            int s = DataConverter.CLng(status);
            if (action.Equals("edit"))
            {
                return s != (int)ZLEnum.ConStatus.Recycle;
            }
            else
            {
                return s == (int)ZLEnum.ConStatus.Audited;
            }
        }
        public string GetUrl(string infoid)
        {
            string link = Eval("HtmlLink", "");
            if (string.IsNullOrEmpty(link)) { return "/Item/" + infoid + ".aspx"; }
            else { return link; }
        }
        public string GetCteate(string IsCreate)
        {
            switch (IsCreate)
            {
                case "1":
                    return "<i class='fa fa-check' style='color:green;'></i>";
                default:
                    return "<i class='fa fa-close' style='color:red;'></i>";
            }
        }
        //-------------------------------------------------
        protected void Btn_Search_Click(object sender, EventArgs e)
        {
            MyBind();
        }
        protected void Rel_Btn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.Form["idchk"]))
            {
                bll.UpdateStatus(Request.Form["idchk"], 0);
            }
            MyBind();
        }
        protected void btnDeleteAll_Click(object sender, EventArgs e)
        {
            string ids = Request.Form["idchk"];
            if (!string.IsNullOrEmpty(ids))
            {
                if (Status.Equals("-2"))
                {
                    bll.DelByIDS(ids);
                }
                else
                {
                    for (int i = 0; i < ids.Split(',').Length; i++)
                    {
                        bll.SetDel(Convert.ToInt32(ids.Split(',')[i]));
                    }
                }
            }
            MyBind();
        }
    }
}