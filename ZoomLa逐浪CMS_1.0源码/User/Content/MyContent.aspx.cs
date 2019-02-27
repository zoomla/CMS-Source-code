namespace ZoomLa.WebSite.User.Content
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
    using ZoomLa.Components;

    public partial class MyContent : System.Web.UI.Page
    {
        private B_Content bll = new B_Content();
        private B_Node bNode = new B_Node();
        private B_Model bmode = new B_Model();
        private B_User buser = new B_User();
        public int NodeID;
        public string flag;
        public string KeyWord;
        public M_UserInfo UserInfo;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                buser.CheckIsLogin();
                this.LblSiteName.Text = SiteConfig.SiteInfo.SiteName;
                string uname = HttpContext.Current.Request.Cookies["UserState"]["LoginName"];
                this.UserInfo = buser.GetUserByName(uname);
                if (string.IsNullOrEmpty(base.Request.QueryString["NodeID"]))
                {
                    this.NodeID = 0;                    
                }
                else
                {
                    this.NodeID = DataConverter.CLng(base.Request.QueryString["NodeID"]);
                }
                this.flag = base.Request.QueryString["type"];
                if (this.NodeID != 0)
                {
                    M_Node nod = this.bNode.GetNode(this.NodeID);
                    this.lblNodeName.Text = nod.NodeName;
                    string ModeIDList = nod.ContentModel;
                    string[] ModelID = ModeIDList.Split(',');
                    string AddContentlink = "";

                    for (int i = 0; i < ModelID.Length; i++)
                    {
                        AddContentlink = AddContentlink + "<a href=\"AddContent.aspx?ModelID=" + ModelID[i] + "&NodeID=" + this.NodeID + "\">" + this.bmode.GetModelById(DataConverter.CLng(ModelID[i])).ModelName + "</a>&nbsp;|&nbsp;";
                    }
                    this.lblAddContent.Text = AddContentlink;
                }
                else
                {
                    this.lblNodeName.Text = "全部节点";
                    this.lblAddContent.Text = "";
                }
                RepNodeBind();
            }
        }

        private void RepNodeBind()
        {
            this.Egv.DataSource = this.bll.ContentListUser(this.NodeID, this.flag, this.UserInfo.UserName);
            this.Egv.DataKeyNames = new string[] { "GeneralID" };
            this.Egv.DataBind();
        }
        protected void Egv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.Egv.PageIndex = e.NewPageIndex;
            this.RepNodeBind();
        }
        /// <summary>
        /// 全部选择控件设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i <= Egv.Rows.Count - 1; i++)
            {
                CheckBox cbox = (CheckBox)Egv.Rows[i].FindControl("chkSel");
                if (CheckBox2.Checked == true)
                {
                    cbox.Checked = true;
                }
                else
                {
                    cbox.Checked = false;
                }
            }
        }
        protected void btnDeleteAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= Egv.Rows.Count - 1; i++)
            {
                CheckBox cbox = (CheckBox)Egv.Rows[i].FindControl("chkSel");
                if (cbox.Checked == true)
                {
                    int itemID = Convert.ToInt32(Egv.DataKeys[i].Value);
                    this.bll.SetDel(itemID);
                }
            }
            RepNodeBind();
        }
        protected void Lnk_Click(object sender, GridViewCommandEventArgs e)
        {
            
                if (e.CommandName == "Edit")
                    Page.Response.Redirect("EditContent.aspx?GeneralID=" + e.CommandArgument.ToString());
                if (e.CommandName == "Del")
                {
                    string Id = e.CommandArgument.ToString();
                    this.bll.DelContent(DataConverter.CLng(Id));
                    RepNodeBind();
                }
            
        }
        public string GetStatus(string status)
        {
            int s = DataConverter.CLng(status);
            if (s == 0)
                return "待审核";
            if (s == 99)
                return "已审核";
            if (s == -1)
                return "退档";
            return "回收站";
        }
        public bool ChkStatus(string status)
        {
            int s = DataConverter.CLng(status);            
            if (s == 99)
                return false;
            else
                return true;
            return true;
        }
        public string GetUrl(string infoid)
        {
            int p = DataConverter.CLng(infoid);
            M_CommonData cinfo = this.bll.GetCommonData(p);
            if (cinfo.IsCreate == 1)
                return SiteConfig.SiteInfo.SiteUrl + cinfo.HtmlLink;
            else
                return "~/Content.aspx?ItemID=" + p;
        }
        public string GetCteate(string IsCreate)
        {
            int s = DataConverter.CLng(IsCreate);
            if (s != 1)
                return "<font color=red>×</font>";
            else
                return "<font color=green>√</font>";
        }
        protected void Btn_Search_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.TxtSearchTitle.Text.Trim()))
            {
                this.KeyWord = this.TxtSearchTitle.Text;
                RepNodeBind();
            }
        }
}
}