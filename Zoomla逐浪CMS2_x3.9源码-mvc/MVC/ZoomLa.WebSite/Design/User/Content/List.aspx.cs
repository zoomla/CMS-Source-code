using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Design;
using ZoomLa.BLL.Site;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Model.Design;
using ZoomLa.Model.Site;
using ZoomLa.SQLDAL;

namespace ZoomLaCMS.Design.User.Content
{
    public partial class List : System.Web.UI.Page
    {
        B_Content bll = new B_Content();
        B_Node bNode = new B_Node();
        B_Model bmode = new B_Model();
        B_User buser = new B_User();
        B_Design_SiteInfo sfBll = new B_Design_SiteInfo();
        B_Design_Node sfNodeBll = new B_Design_Node();
        public int SiteID { get { return DataConverter.CLng(Request.QueryString["SiteID"]); } }
        public int NodeID { get { return DataConverter.CLng(Request.QueryString["NodeID"]); } }
        public string Status { get { return (Request.QueryString["Status"] ?? ""); } }
        public string Type { get { return Request.QueryString["type"] ?? ""; } }
        public string Domain { get { return DataConvert.CStr(ViewState["Domain"]); } set { ViewState["Domain"] = value; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            //function.WriteErrMsg(StringHelper.MD5("admingood@123"));
            BindNode();
            if (!IsPostBack)
            {
                MyBind();
            }
        }
        private void MyBind()
        {
            M_UserInfo mu = buser.GetLogin();
            M_Design_SiteInfo sfMod = sfBll.SelReturnModel(SiteID);
            Domain = B_IDC_DomainList.GetMyDomain(sfMod.DomainID);
            sfBll.CheckAuthEx(sfMod, mu);
            string nids = "";
            if (NodeID > 0)
            {
                nids = NodeID.ToString();
                M_Node nod = bNode.SelReturnModel(NodeID);
                string ModeIDList = nod.ContentModel;
                string[] ModelID = ModeIDList.Split(',');
                string AddContentlink = "";
                for (int i = 0; i < ModelID.Length; i++)
                {
                    M_ModelInfo infoMod = bmode.SelReturnModel(DataConverter.CLng(ModelID[i]));
                    if (infoMod == null) continue;
                    if (infoMod.ModelType != 5)
                    {
                        AddContentlink += "<a href='AddContent.aspx?SiteID=" + SiteID + "&NodeID=" + NodeID + "&ModelID=" + infoMod.ModelID + "' class='btn btn-info' style='margin-right:5px;'><i class='fa fa-plus'></i> 添加" + infoMod.ItemName + "</a>";
                    }
                }
                AddContent_L.Text = AddContentlink;
            }
            else
            {
                nids = sfNodeBll.SelToIDS(SiteID);
            }

            //int type = DataConverter.CLng(DropDownList1.SelectedValue);
            if (Status.Equals("-2")) { Rel_Btn.Visible = true; }//回收站
            EGV.DataSource = bll.Search(TxtSearchTitle.Text, "", nids, Status);
            EGV.DataBind();
        }
        protected void BindNode()
        {
            DataTable dt = sfNodeBll.SelBy(SiteID);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["ParentID"] = 0;
            }
            MyTree.liAllTlp = "<a href='List.aspx?SiteID=" + SiteID + "'>全部内容</a>";
            MyTree.LiContentTlp = "<a href='List.aspx?NodeID=@NodeID&SiteID=" + SiteID + "'>@NodeName</a>";
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
            int id = DataConverter.CLng(e.CommandArgument);
            switch (e.CommandName.ToLower())
            {
                case "del2":
                    bll.DelContent(id);
                    break;
                case "reset":
                    bll.Reset(id);
                    break;
            }
            MyBind();
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