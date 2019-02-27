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

    public partial class MyFavori : System.Web.UI.Page
    {
        private B_Content bll = new B_Content();
        private B_User buser = new B_User();
        private B_Favorite bfav = new B_Favorite();
        public int NodeID;
        public string KeyWord="";
        public M_UserInfo UserInfo;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                buser.CheckIsLogin();
                this.LblSiteName.Text = SiteConfig.SiteInfo.SiteName;
                if (string.IsNullOrEmpty(base.Request.QueryString["NodeID"]))
                {
                    this.NodeID = 0;
                }
                else
                {
                    this.NodeID = DataConverter.CLng(base.Request.QueryString["NodeID"]);
                }
                this.ViewState["NodeID"] = this.NodeID;
                this.ViewState["KeyWord"] = this.KeyWord;
                RepNodeBind();
            }
        }
        /// <summary>
        /// 绑定数据到GridView
        /// </summary>
        private void RepNodeBind()
        {
            string uname = HttpContext.Current.Request.Cookies["UserState"]["LoginName"];
            this.UserInfo = buser.GetUserByName(uname);
            this.NodeID = DataConverter.CLng(this.ViewState["NodeID"]);
            this.KeyWord = this.ViewState["KeyWord"].ToString();
            this.Egv.DataSource = this.bfav.GetMyFavorite(this.UserInfo.UserID, this.NodeID, this.KeyWord);
            this.Egv.DataKeyNames = new string[] { "FavoriteID" };
            this.Egv.DataBind();
        }
        /// <summary>
        /// GridView 翻页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDeleteAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= Egv.Rows.Count - 1; i++)
            {
                CheckBox cbox = (CheckBox)Egv.Rows[i].FindControl("chkSel");
                if (cbox.Checked == true)
                {
                    int itemID = Convert.ToInt32(Egv.DataKeys[i].Value);
                    this.bfav.DelFavorite(itemID);
                }
            }
            RepNodeBind();
        }
        /// <summary>
        /// GridView操作事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Lnk_Click(object sender, GridViewCommandEventArgs e)
        {
            if (this.Page.IsValid)
            {                
                if (e.CommandName == "Del")
                {
                    string Id = e.CommandArgument.ToString();
                    this.bfav.DelFavorite(DataConverter.CLng(Id));
                    RepNodeBind();
                }
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.TxtSearchTitle.Text.Trim()))
            {
                this.KeyWord = this.TxtSearchTitle.Text;
                this.ViewState["KeyWord"] = this.KeyWord;
                RepNodeBind();
            }
        }
        public string GetCteate(string IsCreate)
        {
            int s = DataConverter.CLng(IsCreate);
            if (s != 1)
                return "<font color=red>×</font>";
            else
                return "<font color=green>√</font>";
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
}
}